using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Shared._EE.Contractors.Prototypes;

/// <summary>
/// Prototype representing a character's nationality.
/// </summary>
[Prototype("nationality")]
public sealed partial class NationalityPrototype : IPrototype
{
    [IdDataField, ViewVariables]
    public string ID { get; } = string.Empty;

    [DataField]
    public string NameKey { get; } = string.Empty;

    [DataField]
    public string DescriptionKey { get; } = string.Empty;

    [DataField("sortOrder")]
    [ViewVariables]
    public int SortOrder { get; private set; }

    [DataField]
    public List<ProtoId<JobPrototype>> BlockingJobs { get; } = new();

    [DataField("passportPrototype")]
    public string? PassportPrototype { get; private set; }
}
