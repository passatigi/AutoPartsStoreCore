using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.Model.Vehicle
{
    public class ConcretVehiclePartOemNumber
    {
        [Key]
        public long Id { get; set; }

        public VehiclePart VehiclePart { get; set; }
        public string OEMNumber { get; set; }
    }
}
