using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    class UserViewModel : BaseViewModel
    {
        private Customer customer;
        public Customer Customer
        {
            get
            {
                return customer;
            }
            set
            {
                SetProperty(ref customer, value);
            }
        }
        public string ConfirmPassword;

    }
}
