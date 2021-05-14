using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.Model.Vehicle
{
    

    public class VehiclePart : BasicModel
    {
        public long Id { get; set; }

        private VehicleBrand vehicle;
        private VehicleModification vehicleModification;
        private VehicleEngine vehicleEngine;

        private List<ConcretVehiclePartOemNumber> concretVehiclePartOemNumbers;

        private Category category;

        #region properties
        public VehicleBrand Vehicle
        {
            get
            {
                return vehicle;
            }
            set
            {
                SetProperty(ref vehicle, value);
            }
        }
        public VehicleModification VehicleModification
        {
            get
            {
                return vehicleModification;
            }
            set
            {
                SetProperty(ref vehicleModification, value);
            }
        }
        public VehicleEngine VehicleEngine
        {
            get
            {
                return vehicleEngine;
            }
            set
            {
                SetProperty(ref vehicleEngine, value);
            }
        }

        public List<ConcretVehiclePartOemNumber> ConcretVehiclePartOemNumbers
        {
            get
            {
                return concretVehiclePartOemNumbers;
            }
            set
            {
                SetProperty(ref concretVehiclePartOemNumbers, value);
            }
        }

        public Category Category
        {
            get
            {
                return category;
            }
            set
            {
                SetProperty(ref category, value);
            }
        }

        #endregion

    }

}
