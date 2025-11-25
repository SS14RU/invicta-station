using Robust.Shared.Prototypes;

namespace Content.Shared._Invicta.Contractors;

/// <summary>
/// Простая заглушка под национальности из Invicta-контента.
/// Пока логика не интегрирована, класс нужен только, чтобы прототипы успешно грузились.
/// </summary>
[Prototype("nationality")]
public sealed partial class NationalityPrototype : IPrototype
{
    [IdDataField] public string ID { get; private set; } = default!;

    [DataField("nameKey")] public string? NameKey { get; private set; }

    [DataField("descriptionKey")] public string? DescriptionKey { get; private set; }

    [DataField("sortOrder")] public int SortOrder { get; private set; }

    [DataField("passportPrototype")] public string? PassportPrototype { get; private set; }
}
