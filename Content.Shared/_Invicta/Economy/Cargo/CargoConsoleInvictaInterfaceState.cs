using Content.Shared.Cargo;
using Content.Shared.Cargo.BUI;
using Content.Shared.Cargo.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._Invicta.Economy.Cargo;

[NetSerializable, Serializable]
public sealed class CargoConsoleInvictaInterfaceState : CargoConsoleInterfaceState
{
    public string? CurrencyPrototype;

    public CargoConsoleInvictaInterfaceState(
        string name,
        int count,
        int capacity,
        NetEntity station,
        List<CargoOrderData> orders,
        List<ProtoId<CargoProductPrototype>> products,
        string? currencyPrototype)
        : base(name, count, capacity, station, orders, products)
    {
        CurrencyPrototype = currencyPrototype;
    }
}
