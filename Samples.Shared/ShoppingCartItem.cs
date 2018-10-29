using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Shared
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
    }
}
