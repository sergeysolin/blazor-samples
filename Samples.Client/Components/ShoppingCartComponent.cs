using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using BlazorRedux;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using Samples.Client.Infrastructure.ShoppingCart;
using Samples.Client.Infrastructure.ShoppingCart.Actions;
using Samples.Client.Services;
using Samples.Client.Services.Clients;
using Samples.Shared;
using Samples.Shared.ShoppingCart;

namespace Samples.Client.Components
{
    public class ShoppingCartComponent : ReduxComponent<ShoppingCartState, IAction>
    {
        protected readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        [Inject]
        public IInventoryItemService InventoryItemService { get; set; }

        [Inject]
        public IIdentity Identity { get; set; }

        [Inject]
        public IApiClient<UserCart> ApiClient { get; set; }

        private bool _initialized { get; set; } = false;

        protected void RemoveItem(ShoppingCartItem item, int qty)
        {
            Dispatch(new RemoveItemAction() { ItemId = item.Id, Qty = qty });
        }

        protected async Task AddItem(int itemId, int qty)
        {
            var item = InventoryItemService.GetItem(itemId);

            if (item != null)
            {
                Dispatch(new AddItemAction()
                {
                    ItemId = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    Qty = qty
                });

                System.Console.WriteLine($"Cart id: {State.CartId}");

                await ApiClient.UpdateAsync(id: State.CartId, data: new UserCart()
                {
                    CartId = State.CartId,
                    Items = State.Items.ToList()
                }, _cancellationTokenSource.Token);
            }
        }

        protected override async Task OnInitAsync()
        {
            if (!_initialized)
            {
                _initialized = true;
                if (Identity.IsAuthenticated)
                {
                    var response = await ApiClient.GetAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var cart = response.GetContent<UserCart>();

                        if(cart != null)
                        {
                            Dispatch(new InitCartAction() { CartId = cart.CartId, CartItems = cart.Items });
                        }
                    }
                }
            }
        }
    }
}
