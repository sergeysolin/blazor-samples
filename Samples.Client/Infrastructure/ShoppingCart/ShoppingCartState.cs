using Samples.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Client.Infrastructure.ShoppingCart
{
    public class ShoppingCartState
    {
        public IEnumerable<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public decimal Amount { get; set; }

        public decimal Tax { get; set; }

        public int ItemsCount => Items.Sum(c => c.Qty);
    }
}
