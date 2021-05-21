using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.Model.Vehicle
{
    public class VehicleModification : BasicModel
    {
        public int VehicleId { get; set; }
        public VehicleBrand VehicleBrand { get; set; }
        
        private int id;
        private string model;
        private string modelCode;
        private string releaseStart;
        private string releaseEnd;
        private ObservableCollection<VehicleEngine> vehicleEngines;

        

        public VehicleModification()
        {

        }
        public VehicleModification(VehicleModification vehicleModification)
        {
            this.VehicleBrand = vehicleModification.VehicleBrand;
            this.model = vehicleModification.model;
            this.modelCode = vehicleModification.modelCode;
            this.releaseStart = vehicleModification.releaseStart;
            this.releaseEnd = vehicleModification.releaseEnd;
        }

        public VehicleModification(VehicleBrand vehicle, string model, string modelCode, string releaseStart, string releaseEnd)
        {
            this.VehicleBrand = vehicle;
            this.model = model;
            this.modelCode = modelCode;
            this.releaseStart = releaseStart;
            this.releaseEnd = releaseEnd;
        }
        public VehicleModification(VehicleBrand vehicle, VehicleModification vehicleModification)
        {
            this.VehicleBrand = vehicle;
            this.model = vehicleModification.model;
            this.modelCode = vehicleModification.modelCode;
            this.releaseStart = vehicleModification.releaseStart;
            this.releaseEnd = vehicleModification.releaseEnd;
        }

        public override string ToString()
        {
            return $"{model} ({modelCode}) ({releaseStart} - {releaseEnd})";
        }

        #region Properties
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                SetProperty(ref id, value);
            }
        }

        public string Model
        {
            get
            {
                return model;
            }
            set
            {
                SetProperty(ref model, value);
            }
        }

        public string ModelCode
        {
            get
            {
                return modelCode;
            }
            set
            {
                SetProperty(ref modelCode, value);
            }
        }

        public string ReleaseStart
        {
            get
            {
                return releaseStart;
            }
            set
            {
                SetProperty(ref releaseStart, value);
            }
        }

        public string ReleaseEnd
        {
            get
            {
                return releaseEnd;
            }
            set
            {
                SetProperty(ref releaseEnd, value);
            }
        }

        public ObservableCollection<VehicleEngine> VehicleEngines
        {
            get
            {
                return vehicleEngines;
            }
            set
            {
                SetProperty(ref vehicleEngines, value);
            }
        }
        #endregion

    }
}
