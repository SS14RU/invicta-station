using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Shared._Invicta.Contractors.Prototypes;

/// <summary>
/// Prototype representing a character's planet.
/// </summary>
[Prototype("planet")]
public sealed partial class PlanetPrototype : IPrototype
{
    [IdDataField, ViewVariables]
    public string ID { get; } = string.Empty;

    [DataField]
    public string NameKey { get; } = string.Empty;

    [DataField]
    public string DescriptionKey { get; } = string.Empty;

    [DataField("backgroundTexture")]
    public string? BackgroundTexture { get; } = null;

    [DataField]
    public Color PrimaryColour { get; } = Color.FromHex("#23BB32");

    [DataField]
    public Color SecondaryColour { get; } = Color.FromHex("#AABB32");

    [DataField, ViewVariables]
    public HashSet<ProtoId<PlanetPrototype>> Rivals { get; } = new();

    [DataField]
    public List<ProtoId<JobPrototype>> BlockingJobs { get; } = new();

    /// <summary>
    /// Citizenships that can pick this planet. Empty = no restriction.
    /// </summary>
    [DataField]
    public List<ProtoId<CitizenshipPrototype>> AllowedCitizenships { get; } = new();
}
