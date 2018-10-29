using BlazorRedux;
using Samples.Client.Infrastructure.ShoppingCart;
using Samples.Client.Infrastructure.ShoppingCart.Actions;
using Samples.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Client.Infrastructure
{
    public static class ShoppingCartReducer
    {
        public static ShoppingCartState Root(ShoppingCartState state, IAction action)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            switch (action)
            {
                case AddItemAction item:
                    return AddItem(state, item);
                case RemoveItemAction item:
                    return RemoveItem(state, item);
                default:
                    break;
            }

            return state;
        }

        private static ShoppingCartState AddItem(ShoppingCartState state, AddItemAction addAction)
        {
            var items = new List<ShoppingCartItem>(state.Items);

            var found = items.FirstOrDefault(c => c.Id == addAction.ItemId);

            if(found != null)
            {
                found.Qty += addAction.Qty;
            }
            else
            {
                items.Add(new ShoppingCartItem()
                {
                    Id = addAction.ItemId,
                    Name = addAction.Name,
                    Description = addAction.Description,
                    Price = addAction.Price,
                    Qty = addAction.Qty
                });
            }

            return BuildState(items);
        }

        private static ShoppingCartState BuildState(List<ShoppingCartItem> items)
        {
            return new ShoppingCartState()
            {
                Items = items,
                Amount = items.Sum(c => c.Price * c.Qty),
                Tax = items.Sum(c => c.Price * c.Qty) * 0.21M
            };
        }

        private static ShoppingCartState RemoveItem(ShoppingCartState state, RemoveItemAction removeAction)
        {
            var items = new List<ShoppingCartItem>(state.Items);

            for (int i = 0; i < removeAction.Qty; i++)
            {
                var found = items.FirstOrDefault(c => c.Id == removeAction.ItemId);
                if(found != null)
                {
                    items.Remove(found);
                }
            }

            return BuildState(items);
        }
    }
}
