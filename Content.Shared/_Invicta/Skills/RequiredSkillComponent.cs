using Robust.Shared.GameStates;

namespace Content.Shared._Invicta.Skills;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class RequiredSkillComponent : Component
{
    [AutoNetworkedField]
    [ViewVariables(VVAccess.ReadWrite), DataField(required: true)]
    public SkillContainer Container = new();
}
