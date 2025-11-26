using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Shared._EE.Contractors.Prototypes;

/// <summary>
/// Prototype representing a character's lifepath/background.
/// </summary>
[Prototype("lifepath")]
public sealed partial class LifepathPrototype : IPrototype
{
    [IdDataField, ViewVariables]
    public string ID { get; } = string.Empty;

    [DataField]
    public string NameKey { get; } = string.Empty;

    [DataField]
    public string DescriptionKey { get; } = string.Empty;

    [DataField]
    public List<ProtoId<JobPrototype>> BlockingJobs { get; } = new();
}
