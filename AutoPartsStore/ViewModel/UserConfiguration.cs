using AutoPartsStore.Model;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    public class UserConfiguration
    {
        #region singleton

        private static UserConfiguration userConfiguration;
        private static object syncRoot = new Object(); public static UserConfiguration GetUserConfiguration()
        {
            if (userConfiguration == null)
            {
                lock (syncRoot)
                {
                    if (userConfiguration == null)
                        userConfiguration = new UserConfiguration();
                }
            }
            return userConfiguration;
        }

        #endregion

        private UserConfiguration()
        {
            isAdmin = false;
            //isAdmin = true;
            UpdateShopingCart();
        }

        public VehicleEngine SelectedVehicleEngine { get; set; }
        public Category SelectedCategory { get; set; }
        public Product SelectedProduct { get; set; }

        private bool isAdmin;

        public bool IsAdmin
        {
            get
            {
                if(administrator != null)
                {
                    return true;
                }
                return false;
            }
            private set
            {

            }
        }
        public void SetAdmin(Administrator administrator, string password)
        {
            if (administrator.AdminPassword.Equals(password))
            {
                this.administrator = administrator;
            }
            else
            {
                this.administrator = null;
            }
        }
        public void UnsetAdmin()
        {
            this.administrator = null;
        }
        private Administrator administrator;
        public Customer Customer { get; set; }
        public Order ShoppingCart { get; private set; }

        public void UpdateShopingCart()
        {
            ShoppingCart = new Order();
            ShoppingCart.OrderParts = new ObservableCollection<OrderPart>();
        }

    }
}
