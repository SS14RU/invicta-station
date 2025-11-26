using Robust.Shared.Configuration;

namespace Content.Shared.CCVar;

/// <summary>
/// CVars for background/contractor system.
/// </summary>
public sealed partial class CCVars
{
    /// <summary>
    /// Toggle background-based character requirements.
    /// </summary>
    public static readonly CVarDef<bool> ContractorsCharacterRequirementsEnabled =
        CVarDef.Create("contractors.character_requirements", true, CVar.SERVER | CVar.REPLICATED);

    /// <summary>
    /// Toggle execution of background trait functions (kept for compatibility).
    /// </summary>
    public static readonly CVarDef<bool> ContractorsTraitFunctionsEnabled =
        CVarDef.Create("contractors.trait_functions", true, CVar.SERVER | CVar.REPLICATED);

    /// <summary>
    /// Master switch for the background system.
    /// </summary>
    public static readonly CVarDef<bool> ContractorsEnabled =
        CVarDef.Create("contractors.enabled", true, CVar.SERVER | CVar.REPLICATED);
}
