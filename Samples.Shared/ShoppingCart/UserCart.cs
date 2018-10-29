using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Samples.Shared.ShoppingCart
{
    public class UserCart
    {
        [Key]
        public Guid CartId { get; set; }

        public string UserId { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public DateTime Expiration { get; set; }
    }
}
