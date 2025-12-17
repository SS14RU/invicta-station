using Robust.Shared.GameObjects;

namespace Content.Shared.PDA;

/// <summary>
/// Event fired to allow systems to supply PDA instruction text (display and copy variants).
/// </summary>
[ByRefEvent]
public struct PdaCollectInstructionEvent
{
    public bool Handled;
    public string? DisplayText;
    public string? CopyText;
}
