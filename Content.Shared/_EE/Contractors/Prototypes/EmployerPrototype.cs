using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Shared._EE.Contractors.Prototypes;

/// <summary>
/// Prototype representing a character's employer.
/// </summary>
[Prototype("employer")]
public sealed partial class EmployerPrototype : IPrototype
{
    [IdDataField, ViewVariables]
    public string ID { get; } = string.Empty;

    [DataField]
    public string NameKey { get; } = string.Empty;

    [DataField]
    public string DescriptionKey { get; } = string.Empty;

    [DataField]
    public Color PrimaryColour { get; } = Color.FromHex("#23BB32");

    [DataField]
    public Color SecondaryColour { get; } = Color.FromHex("#AABB32");

    [DataField, ViewVariables]
    public HashSet<ProtoId<EmployerPrototype>> Rivals { get; } = new();

    [DataField]
    public List<ProtoId<JobPrototype>> BlockingJobs { get; } = new();
}
