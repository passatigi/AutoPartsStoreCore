using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.Model
{
    public class VendorCodeOEMNumber
    {
        public VendorCode VendorCode { get; set; }
        
        public string Id { get; set; }
        public VehicleBrand VehicleBrand { get; set; }
        public string OEM { get; set; }
    }
}
