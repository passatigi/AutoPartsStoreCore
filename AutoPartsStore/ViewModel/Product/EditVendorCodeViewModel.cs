using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    class EditVendorCodeViewModel : BaseViewModel
    {
        private  VendorCode vendoreCode;
        public VendorCode VendoreCode
        {
            get
            {
                return vendoreCode;
            }
            set
            {
                SetProperty(ref vendoreCode, value);
            }
        }
    }
}
