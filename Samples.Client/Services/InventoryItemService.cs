using Samples.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Client.Services
{
    public interface IInventoryItemService
    {
        InventoryItem GetItem(int id);
        IEnumerable<InventoryItem> GetItems();
    }

    public class InventoryItemService : IInventoryItemService
    {
        IDictionary<string, decimal> items = new Dictionary<string, decimal>()
        {
            { "1 Hour Jump Package", 3.44M },
            { "3 Bread Sticks", 1.02M },
            { "BBQ Sandwich", 2.40M },
            { "Cheese Pizza", 30.05M },
            { "Steak Dinner", 2.44M },
            { "Bottled Coke", 0.88M },
            { "Small Drink", 0.77M }
        };

        public InventoryItem GetItem(int id)
        {
            return GetItems().FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<InventoryItem> GetItems()
        {
            return items.Select((c, index) => Build(index + 1, c.Key, c.Value, c.Key));
        }

        public static InventoryItem Build(int id, string name, decimal price, string description)
        {
            return new InventoryItem() { Id = id, Name = name, Price = price, Description = description };
        }
    }
}
