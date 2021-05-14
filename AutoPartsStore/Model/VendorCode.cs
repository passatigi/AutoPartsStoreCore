using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.Model
{
   


    public class VendorCode
    {
        public long id { get; set; }
        public string VendorCodeString { get; set; }
        public ObservableCollection<VendorCodeOEMNumbers> NumbersOEM { get; set; }

    } 
}
