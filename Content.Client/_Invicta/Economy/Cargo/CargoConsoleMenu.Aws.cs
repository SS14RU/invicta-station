using Content.Shared.Store;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Localization;
using Robust.Shared.Prototypes;

namespace Content.Client.Cargo.UI;

public sealed partial class CargoConsoleMenu
{
    private CurrencyPrototype? _InvictaCurrency;
    private int _InvictaCurrentBankBalance;
    private Label? _InvictaBalanceLabel;

    partial void OnMenuConstructed()
    {
        // PointsLabel is a RichTextLabel here; no direct label reuse available.

        UpdateBalanceLabel();
    }

    private partial string FormatPointCost(int cost, string defaultText)
    {
        if (_InvictaCurrency == null)
            return defaultText;

        return cost.ToString();
    }

    partial void OnBankDataUpdated(string name, int points)
    {
        _InvictaCurrentBankBalance = points;
        UpdateBalanceLabel();
        PointsLabel.Text = FormatPointCost(points, Loc.GetString("cargo-console-menu-points-amount", ("amount", points.ToString())));
    }

    public void SetCurrency(string? currencyId)
    {
        if (!string.IsNullOrWhiteSpace(currencyId) &&
            _protoManager.TryIndex<CurrencyPrototype>(currencyId, out var currency))
        {
            _InvictaCurrency = currency;
        }
        else
        {
            _InvictaCurrency = null;
        }

        UpdateBalanceLabel();
        PointsLabel.Text = FormatPointCost(_InvictaCurrentBankBalance, Loc.GetString("cargo-console-menu-points-amount", ("amount", _InvictaCurrentBankBalance.ToString())));
    }

    private void UpdateBalanceLabel()
    {
        if (_InvictaBalanceLabel == null)
            return;

        if (_InvictaCurrency == null)
        {
            _InvictaBalanceLabel.Text = Loc.GetString("cargo-console-menu-points-label");
            return;
        }

        var currencyName = Loc.GetString(_InvictaCurrency.DisplayName);
        _InvictaBalanceLabel.Text = Loc.GetString("Invicta-economy-cargo-console-balance-label", ("currency", currencyName));
    }
}
