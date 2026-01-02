using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Shared._Invicta.Contractors.Prototypes;

/// <summary>
/// Prototype representing a character's citizenship.
/// </summary>
[Prototype("citizenship")]
public sealed partial class CitizenshipPrototype : IPrototype
{
    [IdDataField, ViewVariables]
    public string ID { get; } = string.Empty;

    [DataField("name")]
    public string Name { get; } = string.Empty;

    [DataField("description")]
    public string Description { get; } = string.Empty;

    [DataField("sortOrder")]
    [ViewVariables]
    public int SortOrder { get; private set; }

    [DataField]
    public List<ProtoId<JobPrototype>> BlockingJobs { get; } = new();

    [DataField("passportPrototype")]
    public string? PassportPrototype { get; private set; }
}
