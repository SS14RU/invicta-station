using Content.Shared.VendingMachines;
using Robust.Shared.GameObjects;

namespace Content.Server.VendingMachines;

// Invicta: emitted on selection before vending to allow banking to intercept.
public sealed class VendingMachineSelectAttemptEvent : HandledEntityEventArgs
{
    public EntityUid? Actor { get; }
    public InventoryType Type { get; }
    public string ID { get; }
    public VendingMachineInventoryEntry? Entry { get; }

    public VendingMachineSelectAttemptEvent(EntityUid? actor, InventoryType type, string id, VendingMachineInventoryEntry? entry)
    {
        Actor = actor;
        Type = type;
        ID = id;
        Entry = entry;
    }
}
