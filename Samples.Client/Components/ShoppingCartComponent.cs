using BlazorRedux;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using Samples.Client.Infrastructure.ShoppingCart;
using Samples.Client.Infrastructure.ShoppingCart.Actions;
using Samples.Client.Services;
using Samples.Shared;

namespace Samples.Client.Components
{
    public class ShoppingCartComponent : ReduxComponent<ShoppingCartState, IAction>
    {
        [Inject]
        public IInventoryItemService InventoryItemService { get; set; }

        protected void RemoveItem(ShoppingCartItem item, int qty)
        {
            Dispatch(new RemoveItemAction() { ItemId = item.Id, Qty = qty });
        }

        protected void AddItem(int itemId, int qty)
        {
            var item = InventoryItemService.GetItem(itemId);

            if(item != null)
            {
                Dispatch(new AddItemAction()
                {
                    ItemId = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    Qty = qty
                });
            }
        }
    }
}
