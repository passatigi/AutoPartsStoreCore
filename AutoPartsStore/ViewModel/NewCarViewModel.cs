
using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model.Vehicle;
using AutoPartsStore.ViewModel.NewVehicleHelpTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoPartsStore.ViewModel
{

    public class NewCarViewModel
    {
        public NewVehicleBrandHelper NewVehicleBrandHelper { get; set; }
        public NewVehicleModificationHelper NewVehicleModificationHelper { get; set; }
        public NewVehicleEngineHelper NewVehicleEngineHelper { get; set; }

        #region methods
        
        public VehicleBrand GetSelectedVehicleBrand()
        {
            return NewVehicleBrandHelper.SelectedVehicleBrand;
        }
        public VehicleModification GetSelectedVehicleModification()
        {
            return NewVehicleModificationHelper.SelectedVehicleModification;
        }

        public bool ModificationAccessible(object parm)
        {
            if(NewVehicleBrandHelper.SelectedVehicleBrand == null)
            {
                return false;
            }
            return true;
        }
        public bool EngineAccessible(object parm)
        {
            if (NewVehicleModificationHelper.SelectedVehicleModification == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        IStoreService storeService;
        MainViewModel mainViewModel;
        public NewCarViewModel()
        {
            storeService = StoreService.GetStoreService();

            mainViewModel = MainViewModel.GetMainViewModel();
            mainViewModel.NewCarViewModel = this;


            NewVehicleEngineHelper = new NewVehicleEngineHelper(storeService, this);
            NewVehicleModificationHelper = new NewVehicleModificationHelper(storeService, this);
            NewVehicleBrandHelper = new NewVehicleBrandHelper(storeService, this);
        }
    }



}