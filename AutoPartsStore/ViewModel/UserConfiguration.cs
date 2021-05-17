﻿using AutoPartsStore.Model;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    class UserConfiguration
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

        }

        public VehicleEngine SelectedVehicleEngine { get; set; }
        public Category SelectedCategory { get; set; }
    }
}