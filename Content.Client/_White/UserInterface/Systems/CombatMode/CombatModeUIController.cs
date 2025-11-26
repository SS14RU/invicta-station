using Content.Client._White.UserInterface.Systems.CombatMode.Widgets;
using Content.Client.CombatMode;
using Content.Client.Gameplay;
using Content.Shared.CombatMode;
using Robust.Client.UserInterface.Controllers;

namespace Content.Client._White.UserInterface.Systems.CombatMode;

public sealed class CombatModeUIController : UIController, IOnStateEntered<GameplayState>, IOnSystemChanged<CombatModeSystem>
{
    private CombatModeComponent? _combatModeComponent;

    private CombatModeGui? CombatModGui => UIManager.GetActiveUIWidgetOrNull<CombatModeGui>();

    public void OnSystemLoaded(CombatModeSystem system)
    {
        system.LocalPlayerCombatModeUpdated += OnCombatModeUpdated;
        system.LocalPlayerCombatModeAdded += OnCombatModeAdded;
        system.LocalPlayerCombatModeRemoved += OnCombatModeRemoved;
        system.LocalPlayerCombatModeEnabledChanged += OnCombatModeEnableChanged;
    }

    public void OnSystemUnloaded(CombatModeSystem system)
    {
        system.LocalPlayerCombatModeUpdated -= OnCombatModeUpdated;
        system.LocalPlayerCombatModeAdded -= OnCombatModeAdded;
        system.LocalPlayerCombatModeRemoved -= OnCombatModeRemoved;
        system.LocalPlayerCombatModeEnabledChanged -= OnCombatModeEnableChanged;
    }

    public void OnStateEntered(GameplayState state)
    {
        if (CombatModGui != null)
            CombatModGui.SetCombatEnabled(true); // show by default; will update when state arrives
    }

    private void OnCombatModeUpdated(bool inCombatMode)
    {
        CombatModGui?.OnCombatModeUpdated(inCombatMode);
    }

    private void OnCombatModeAdded(CombatModeComponent component)
    {
        _combatModeComponent = component;
        CombatModGui?.SetCombatEnabled(component.Enable);
        OnCombatModeUpdated(component.IsInCombatMode);
    }

    private void OnCombatModeRemoved()
    {
        if (CombatModGui != null)
            CombatModGui.SetCombatEnabled(false);

        _combatModeComponent = null;
    }

    private void OnCombatModeEnableChanged(bool enabled)
    {
        if (CombatModGui != null)
            CombatModGui.SetCombatEnabled(enabled);
    }

    public void ToggleCombatMode() => EntityManager.RaisePredictiveEvent(new ToggleCombatModeRequestEvent());
}
