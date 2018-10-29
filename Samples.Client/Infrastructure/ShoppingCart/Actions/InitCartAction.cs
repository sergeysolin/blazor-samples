using BlazorRedux;
using Samples.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Client.Infrastructure.ShoppingCart.Actions
{
    public class InitCartAction : IAction
    {
        public InitCartAction()
        {
            CartItems = new List<ShoppingCartItem>();
        }

        public Guid CartId { get; set; }
        public List<ShoppingCartItem> CartItems { get; set; }
    }
}
