using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Content.Client.UserInterface.Controls;
using Content.Client.UserInterface.Systems.Guidebook;
using Content.Shared.Guidebook;
using JetBrains.Annotations;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.RichText;
using Robust.Shared.Input;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Prototypes;
using Robust.Shared.Maths;
using Robust.Shared.Utility;

namespace Content.Client.Guidebook.RichText;

/// <summary>
/// Safe guidebook link tag: [guideentry id="SomeGuide"]Link text[/guideentry].
/// Works client-side only and only opens existing GuideEntry prototypes.
/// INV: Custom tag for Invicta guide links to avoid unsafe navigation.
/// </summary>
[UsedImplicitly]
public sealed class GuideEntryLinkTag : IMarkupTagHandler
{
    [Dependency] private readonly IEntitySystemManager _entSys = default!;
    [Dependency] private readonly IPrototypeManager _protos = default!;

    public string Name => "guideentry";

    public bool TryCreateControl(MarkupNode node, [NotNullWhen(true)] out Control? control)
    {
        control = null;

        if (!node.Value.TryGetString(out var text)
            || !node.Attributes.TryGetValue("id", out var idParam)
            || !idParam.TryGetString(out var guideId))
        {
            return false;
        }

        if (!_protos.HasIndex<GuideEntryPrototype>(guideId))
            return false;

        var label = new Label
        {
            Text = text,
            MouseFilter = Control.MouseFilterMode.Stop,
            FontColorOverride = Color.FromHex("#61a8ff"),
            DefaultCursorShape = Control.CursorShape.Hand
        };

        label.OnMouseEntered += _ => label.FontColorOverride = Color.LightSkyBlue;
        label.OnMouseExited += _ => label.FontColorOverride = Color.FromHex("#61a8ff");
        label.OnKeyBindDown += args => OnKeyBindDown(args, guideId);

        control = label;
        return true;
    }

    private void OnKeyBindDown(GUIBoundKeyEventArgs args, string guideId)
    {
        if (args.Function != EngineKeyFunctions.UIClick)
            return;

        OpenGuide(guideId);
    }

    private void OpenGuide(string guideId)
    {
        var guidebook = _entSys.GetEntitySystem<GuidebookSystem>();
        guidebook.OpenHelp(new List<ProtoId<GuideEntryPrototype>> { guideId });
    }
}
