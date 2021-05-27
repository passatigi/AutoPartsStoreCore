using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.Model
{
    public class Administrator
    {
        public int Id { get; set; }

        public string AdminPassword { get; set; }

        public Customer Customer { get; set; }
    }
}
