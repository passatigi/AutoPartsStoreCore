using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.Model
{
    public class ProductOEMNumber
    {
        public int Id { get; set; }
        
        public Product Product { get; set; }
        public VehicleBrand VehicleBrand { get; set; }
        public string OEM { get; set; }
    }
}
