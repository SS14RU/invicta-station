using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Shared._Invicta.Contractors.Prototypes;

/// <summary>
/// Template-like tag for background job restrictions.
/// </summary>
[Prototype("backgroundTag")]
public sealed partial class BackgroundTagPrototype : IPrototype
{
    [IdDataField, ViewVariables]
    public string ID { get; } = string.Empty;

    [DataField("name")]
    public string Name { get; } = string.Empty;

    [DataField("blockAllJobs")]
    public bool BlockAllJobs { get; } = false;

    [DataField("restrictedOnly")]
    public bool RestrictedOnly { get; } = false;

    [DataField]
    public List<ProtoId<JobPrototype>> BlockingJobs { get; } = new();

    [DataField]
    public List<ProtoId<JobPrototype>> AllowedJobs { get; } = new();
}
