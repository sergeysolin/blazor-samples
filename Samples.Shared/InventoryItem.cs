﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Shared
{
    public class InventoryItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
