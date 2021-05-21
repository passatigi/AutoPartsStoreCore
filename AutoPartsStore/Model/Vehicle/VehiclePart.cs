using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.Model.Vehicle
{
    

    public class VehiclePart : BasicModel
    {
        public long Id { get; set; }

        private VehicleEngine vehicleEngine;
        private Category category;

        private ObservableCollection<ConcretVehiclePartOemNumber> concretVehiclePartOemNumbers;
        public VehiclePart()
        {
            concretVehiclePartOemNumbers = new ObservableCollection<ConcretVehiclePartOemNumber>();
        }
        

        #region properties

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

        public ObservableCollection<ConcretVehiclePartOemNumber> ConcretVehiclePartOemNumbers
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
