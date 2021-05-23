using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.Model
{
    public class OrderPart
    {
        public int Id { get; set; }

        public Order order { get; set; }

        public Product Product { get; set; }

        public short ProductCount { get; set; }
    }
}
