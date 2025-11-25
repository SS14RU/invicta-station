using Content.Server.Access.Components;
using Content.Server._Invicta.Economy.Insurance;
using Content.Server._Lavaland.Procedural.Components;
using Content.Server.GameTicking;
using Content.Server.Spawners.EntitySystems;
using Content.Server.Station.Systems;
using Content.Shared.Access.Components;
using Content.Shared._Invicta.Economy.Bank;
using Content.Shared.Inventory;
using Content.Shared.PDA;
using JetBrains.Annotations;
using Robust.Shared.Map;
using Robust.Shared.GameObjects;
using System.Collections.Generic;

namespace Content.Server._Invicta.Economy.Bank;

[UsedImplicitly]
public sealed class InvictaBankOnSpawnSystem : EntitySystem
{
    [Dependency] private readonly InventorySystem _inventorySystem = default!;
    [Dependency] private readonly EconomyBankAccountSystem _bankAccountSystem = default!;
    [Dependency] private readonly StationSystem _stationSystem = default!;
    [Dependency] private readonly GameTicker _gameTicker = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<PlayerSpawningEvent>(OnPlayerSpawn,
            after: new[] { typeof(SpawnPointSystem) },
            before: new[] { typeof(EconomyInsuranceSystem) });
    }

    private void OnPlayerSpawn(PlayerSpawningEvent ev)
    {
        if (ev.SpawnResult is not { } player || ev.HumanoidCharacterProfile is null)
            return;

        if (!_inventorySystem.TryGetSlotEntity(player, "id", out var idSlot) || idSlot is null)
            return;

        var cardUid = idSlot.Value;
        if (TryComp<PdaComponent>(cardUid, out var pda) && pda.ContainedId is { } containedId)
            cardUid = containedId;

        if (!TryComp<IdCardComponent>(cardUid, out var card))
            return;

        var characterName = card.FullName ?? ev.HumanoidCharacterProfile.Name;

        var preset = EnsureComp<PresetIdCardComponent>(cardUid);
        if (ev.Job is { } job)
            preset.JobName = job;

        var holder = EnsureComp<EconomyAccountHolderComponent>(cardUid);
        holder.AccountName = characterName;
        holder.AccountSetup ??= new BankAccountSetup();
        holder.AccountSetup.AllowedCurrency ??= "Thaler";
        holder.AccountSetup.AccountTags ??= new List<BankAccountTag> { BankAccountTag.Personal };
        Dirty(cardUid, holder);

        if (ShouldAssignBankAccount(player, ev.Station))
            _bankAccountSystem.TryActivate((cardUid, holder), out _);
    }

    private bool ShouldAssignBankAccount(EntityUid entity, EntityUid? station, TransformComponent? xform = null)
    {
        if (!Resolve(entity, ref xform, logMissing: false))
            return false;

        // Mid-round joins and typical spawns pass station explicitly (arrivals shuttle etc.).
        if (station != null)
            return true;

        if (xform.GridUid is EntityUid grid && HasComp<LavalandStationComponent>(grid))
            return true;

        var defaultMap = _gameTicker.DefaultMap;
        if (defaultMap == MapId.Nullspace)
            return false;

        var mainStation = _stationSystem.GetStationInMap(defaultMap);
        if (mainStation == null)
            return false;

        var owningStation = _stationSystem.GetOwningStation(entity, xform);
        return owningStation == mainStation;
    }

    private bool IsEligibleMainStation(EntityUid station)
    {
        var defaultMap = _gameTicker.DefaultMap;
        if (defaultMap == MapId.Nullspace)
            return false;

        var mainStation = _stationSystem.GetStationInMap(defaultMap);
        return mainStation == station;
    }
}
