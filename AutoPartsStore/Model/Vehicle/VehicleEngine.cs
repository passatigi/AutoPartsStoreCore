using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.Model.Vehicle
{
    public class VehicleEngine : BasicModel
    {
        public int VehicleModificationId { get; set; }
        public VehicleModification VehicleModification { get; set; }

        private long id;

        private char type;
        private float volume;
        private string modification;
        private short power;
        private string modelCode;
        private string releaseStart;
        private string releaseEnd;
        
        public VehicleEngine()
        {

        }
        public VehicleEngine(VehicleModification vehicleModification, VehicleEngine vehicleEngine)
        {
            this.VehicleModification = vehicleModification;
            this.volume = vehicleEngine.volume;
            this.modification = vehicleEngine.modification;
            this.power = vehicleEngine.power;
            this.modelCode = vehicleEngine.modelCode;
            this.releaseStart = vehicleEngine.releaseStart;
            this.releaseEnd = vehicleEngine.releaseEnd;
        }

        public void CloneProperties(VehicleEngine vehicleEngine)
        {
            this.volume = vehicleEngine.volume;
            this.modification = vehicleEngine.modification;
            this.power = vehicleEngine.power;
            this.modelCode = vehicleEngine.modelCode;
            this.releaseStart = vehicleEngine.releaseStart;
            this.releaseEnd = vehicleEngine.releaseEnd;
        }

        public override string ToString()
        {
            return $"{type}: {volume} ({power} л.с.) ({releaseStart} - {releaseEnd}) ({modelCode})";
        }

        

        #region Properties
        
        public long Id
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
        public char Type
        {
            get
            {
                return type;
            }
            set
            {
                SetProperty(ref type, value);
            }
        }
        public float Volume
        {
            get
            {
                return volume;
            }
            set
            {
                SetProperty(ref volume, value);
            }
        }


        public string Modification
        {
            get
            {
                return modification;
            }
            set
            {
                SetProperty(ref modification, value);
            }
        }


        public short Power
        {
            get
            {
                return power;
            }
            set
            {
                SetProperty(ref power, value);
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
        #endregion
    }
}
