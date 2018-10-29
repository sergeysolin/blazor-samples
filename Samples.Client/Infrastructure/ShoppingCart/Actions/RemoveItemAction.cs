using BlazorRedux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Client.Infrastructure.ShoppingCart.Actions
{
    public class RemoveItemAction : IAction
    {
        public int ItemId { get; set; }
        public int Qty { get; set; }
    }
}
