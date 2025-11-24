using Content.Shared.VendingMachines;
using Robust.Shared.GameObjects;

namespace Content.Server.VendingMachines;

// Invicta: raised after price recalculation so economy system can adjust costs.
public sealed class VendingMachineRecalculatePriceEvent : HandledEntityEventArgs
{
    public EntityUid VendingMachine { get; }
    public VendingMachineComponent Component { get; }

    public VendingMachineRecalculatePriceEvent(EntityUid vendingMachine, VendingMachineComponent component)
    {
        VendingMachine = vendingMachine;
        Component = component;
    }
}
