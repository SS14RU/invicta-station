using Robust.Shared.GameObjects;

namespace Content.Shared._Invicta.MapCompat;

/// <summary>
/// Stub to allow loading legacy maps that still reference removed engine components.
/// </summary>
[RegisterComponent]
public sealed partial class PhysicsMapComponent : Component
{
}

/// <summary>
/// Stub for legacy map data; functionality now lives inside physics systems.
/// </summary>
[RegisterComponent]
public sealed partial class MovedGridsComponent : Component
{
}

/// <summary>
/// Stub for legacy render order component referenced by old map exports.
/// </summary>
[RegisterComponent]
public sealed partial class RenderOrderComponent : Component
{
}
