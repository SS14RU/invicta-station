using Robust.Shared.Serialization;

namespace Content.Shared._Invicta.Skills;

[Serializable, NetSerializable]
public enum SkillLevel : int
{
    NonSkilled,
    Basic,
    Trained,
    Experienced,
    Master,
}
