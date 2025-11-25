using System;
using System.Collections.Generic;
using Content.Server._Invicta.Economy.Bank;
using Content.Server._Invicta.Economy.Cargo;
using Content.Server.Cargo.Components;
using Content.Shared._Invicta.Economy.Cargo;
using Content.Shared.Cargo.Components;
using Content.Shared.Cargo.BUI;
using Content.Shared.Cargo.Prototypes;
using Robust.Shared.GameObjects;
using Robust.Shared.Log;
using Robust.Shared.Prototypes;
using Content.Shared._Invicta.Economy.Bank;

namespace Content.Server.Cargo.Systems;

public sealed partial class CargoSystem
{
    [Dependency] private readonly EconomyBankAccountSystem _economyBankAccount = default!;

    private EntityQuery<EconomyCargoAccountComponent> _cargoAccountQuery = default!;
    private ISawmill _invictaBridgeSawmill = default!;
    private readonly HashSet<EntityUid> _pendingAccountSync = new();

    partial void InitializeInvictaBridge()
    {
        _invictaBridgeSawmill = Logger.GetSawmill("cargo.invicta_bridge");
        _cargoAccountQuery = GetEntityQuery<EconomyCargoAccountComponent>();

        SubscribeLocalEvent<StationBankAccountComponent, ComponentStartup>(OnStationBankStartup);
        SubscribeLocalEvent<EconomyCargoAccountComponent, ComponentStartup>(OnCargoAccountStartup);
        SubscribeLocalEvent<EconomyBankAccountComponent, ComponentStartup>(OnEconomyAccountStartup);
    }

    private void OnStationBankStartup(EntityUid uid, StationBankAccountComponent component, ref ComponentStartup args)
    {
        TrySyncStationAccounts(uid, component);
        PushBankUpdate(uid, component);
    }

    private void OnCargoAccountStartup(EntityUid uid, EconomyCargoAccountComponent component, ref ComponentStartup args)
    {
        if (!TryComp(uid, out StationBankAccountComponent? bank))
            return;

        TrySyncStationAccounts(uid, bank, component);
        PushBankUpdate(uid, bank);
    }

    partial void BeforeCargoBankUpdate(EntityUid station, StationBankAccountComponent component, Dictionary<ProtoId<CargoAccountPrototype>, double> accountDistribution, ref int amount, ref bool handled)
    {
        if (!_cargoAccountQuery.TryGetComponent(station, out var cargoAccount))
            return;

        if (!TryApplyAccountDelta(station, component, cargoAccount, accountDistribution, amount))
            return;

        handled = true;
    }

    partial void AfterCargoBankUpdate(EntityUid station, StationBankAccountComponent component, Dictionary<ProtoId<CargoAccountPrototype>, double> accountDistribution, int amount, bool handled)
    {
        TrySyncStationAccounts(station, component);

        if (handled)
            PushBankUpdate(station, component);
    }

    partial void EnsureInvictaBalanceSync(EntityUid station)
    {
        if (!TryComp(station, out StationBankAccountComponent? bank))
            return;

        TrySyncStationAccounts(station, bank);
    }

    partial void ShouldSkipCargoPassiveIncome(EntityUid station, StationBankAccountComponent bank, ref bool skip)
    {
        if (_cargoAccountQuery.HasComponent(station))
            skip = true;
    }

    partial void AdjustCargoInterfaceState(EntityUid station, StationCargoOrderDatabaseComponent orderDatabase, StationBankAccountComponent bankAccount, ref CargoConsoleInterfaceState state)
    {
        if (!_cargoAccountQuery.TryGetComponent(station, out var account))
            return;

        state = new CargoConsoleInvictaInterfaceState(
            state.Name,
            state.Count,
            state.Capacity,
            state.Station,
            state.Orders,
            state.Products,
            account.Currency);
    }

    private bool TryApplyAccountDelta(
        EntityUid station,
        StationBankAccountComponent bank,
        EconomyCargoAccountComponent cargoAccount,
        Dictionary<ProtoId<CargoAccountPrototype>, double> accountDistribution,
        int delta)
    {
        // No changes to apply, but still mirror balances.
        if (delta == 0)
        {
            TrySyncStationAccounts(station, bank, cargoAccount);
            return true;
        }

        if (accountDistribution.Count == 0)
            return false;

        var changes = new List<(string AccountId, long Change)>();

        foreach (var (cargoProto, percent) in accountDistribution)
        {
            if (!cargoAccount.AccountMapping.TryGetValue(cargoProto, out var accountId) ||
                string.IsNullOrWhiteSpace(accountId))
            {
                _invictaBridgeSawmill.Warning($"Station {station} missing economy account mapping for cargo account {cargoProto}.");
                return false;
            }

            var change = (long) Math.Round(percent * delta);

            if (change == 0)
                continue;

            changes.Add((accountId, change));
        }

        foreach (var (accountId, change) in changes)
        {
            if (_economyBankAccount.TryChangeAccountBalance(accountId, change, "Cargo bridge adjustment"))
                continue;

            _invictaBridgeSawmill.Warning($"Failed to adjust economy account {accountId} by {change} for station {station}.");
            return false;
        }

        TrySyncStationAccounts(station, bank, cargoAccount);
        return true;
    }

    private void TrySyncStationAccounts(EntityUid station, StationBankAccountComponent bank)
    {
        if (!_cargoAccountQuery.TryGetComponent(station, out var cargoAccount))
            return;

        TrySyncStationAccounts(station, bank, cargoAccount);
    }

    private bool TrySyncStationAccounts(EntityUid station, StationBankAccountComponent bank, EconomyCargoAccountComponent cargoAccount)
    {
        var allSynced = true;
        foreach (var (cargoAccountId, economyAccountId) in cargoAccount.AccountMapping)
        {
            if (string.IsNullOrWhiteSpace(economyAccountId))
                continue;

            if (!_economyBankAccount.TryGetAccount(economyAccountId, out var accountEntity))
            {
                _invictaBridgeSawmill.Warning($"Unable to locate economy account {economyAccountId} for station {station}.");
                allSynced = false;
                continue;
            }

            var newBalance = accountEntity.Value.Comp.Balance > int.MaxValue
                ? int.MaxValue
                : (int) accountEntity.Value.Comp.Balance;

            bank.Accounts[cargoAccountId] = newBalance;
        }

        if (!allSynced)
            _pendingAccountSync.Add(station);
        else
            _pendingAccountSync.Remove(station);

        return allSynced;
    }

    private void PushBankUpdate(EntityUid station, StationBankAccountComponent bank)
    {
        var ev = new BankBalanceUpdatedEvent(station, bank.Accounts);
        RaiseLocalEvent(station, ref ev, true);
        Dirty(station, bank);
        UpdateOrders(station);
    }

    private void OnEconomyAccountStartup(EntityUid uid, EconomyBankAccountComponent component, ref ComponentStartup args)
    {
        if (_pendingAccountSync.Count == 0)
            return;

        var pendingCopy = new List<EntityUid>(_pendingAccountSync);
        foreach (var station in pendingCopy)
        {
            if (!TryComp(station, out StationBankAccountComponent? bank) ||
                !_cargoAccountQuery.TryGetComponent(station, out var cargoAccount))
                continue;

            if (!TrySyncStationAccounts(station, bank, cargoAccount))
                continue;

            PushBankUpdate(station, bank);
        }
    }
}
