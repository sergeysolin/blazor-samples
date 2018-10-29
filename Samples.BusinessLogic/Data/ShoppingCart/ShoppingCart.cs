using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Samples.BusinessLogic.Data
{
    public class ShoppingCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime ExpirationDate { get; set; }

        public ICollection<ShoppingCartItem> Items { get; set; }

        public Customer Customer { get; set; }
    }
}
