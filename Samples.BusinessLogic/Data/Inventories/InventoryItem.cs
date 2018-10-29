using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Samples.BusinessLogic.Data
{
    public class InventoryItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Inventory Inventory { get; set; }

        public StockRoom StockRoom { get; set; }
        
        public int Qty { get; set; }

        public decimal Price { get; set; }
    }
}
