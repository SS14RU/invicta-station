using Content.Shared.Cargo.Prototypes;
using Content.Shared.Store;
using Robust.Shared.Prototypes;

namespace Content.Server._Invicta.Economy.Cargo;

/// <summary>
/// Maps station cargo account balances onto economy bank accounts.
/// </summary>
[RegisterComponent]
public sealed partial class EconomyCargoAccountComponent : Component
{
    /// <summary>
    /// Currency to display in cargo consoles.
    /// </summary>
    [DataField]
    public ProtoId<CurrencyPrototype> Currency = "Thaler";

    /// <summary>
    /// Mapping from cargo account prototypes to economy bank account IDs.
    /// </summary>
    [DataField]
    public Dictionary<ProtoId<CargoAccountPrototype>, string> AccountMapping = new()
    {
        { "Cargo", "NT-Cargo" },
        { "Engineering", "NT-Engineering" },
        { "Medical", "NT-Medical" },
        { "Science", "NT-Science" },
        { "Security", "NT-Security" },
        { "Service", "NT-Service" },
    };
}
