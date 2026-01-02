using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Shared._Invicta.Contractors.Prototypes;

/// <summary>
/// Prototype representing a character's background.
/// </summary>
[Prototype("background")]
public sealed partial class BackgroundPrototype : IPrototype
{
    [IdDataField, ViewVariables]
    public string ID { get; } = string.Empty;

    [DataField("name")]
    public string Name { get; } = string.Empty;

    [DataField("description")]
    public string Description { get; } = string.Empty;

    [DataField("backgroundTexture")]
    public string? BackgroundTexture { get; } = null;

    [DataField]
    public List<ProtoId<JobPrototype>> BlockingJobs { get; } = new();

    [DataField("backgroundTags")]
    public List<ProtoId<BackgroundTagPrototype>> BackgroundTags { get; } = new();

    /// <summary>
    /// Planets that allow this background. Empty = no restriction.
    /// </summary>
    [DataField]
    public List<ProtoId<PlanetPrototype>> AllowedPlanets { get; } = new();
}
