using Content.Client.Cargo.UI;
using Content.Shared._Invicta.Economy.Cargo;
using Content.Shared.Cargo.BUI;
using Content.Shared.Cargo.Prototypes;
using Robust.Shared.GameObjects;

namespace Content.Client.Cargo.BUI;

public sealed partial class CargoOrderConsoleBoundUserInterface
{
    private string? _InvictaCurrencyId;

    partial void OnMenuOpenedExtended(CargoConsoleMenu menu)
    {
        menu.SetCurrency(_InvictaCurrencyId);
    }

    partial void OnStateUpdatedExtended(BoundUserInterfaceState state)
    {
        if (state is CargoConsoleInvictaInterfaceState invictaState)
        {
            _InvictaCurrencyId = invictaState.CurrencyPrototype;

            // Применяем расширенное состояние (продукты/станция/заказы)
            if (_menu != null)
            {
                _menu.ProductCatalogue = invictaState.Products;
                _menu.UpdateStation(EntMan.GetEntity(invictaState.Station));
                _menu.PopulateProducts();
                _menu.PopulateCategories();
                _menu.PopulateOrders(invictaState.Orders);
                _menu.PopulateAccountActions();
                _menu.SetFundsTabVisible(false);
            }

            _menu?.SetCurrency(_InvictaCurrencyId);
            return;
        }

        _InvictaCurrencyId = null;
        if (_menu != null)
        {
            _menu.SetFundsTabVisible(true);
            _menu.SetCurrency(_InvictaCurrencyId);
        }
    }
}
