using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace AutoPartsStore.ViewModel.NewVehicleHelpTools
{
    public class NewVehicleBrandHelper : BaseViewModel
    {
        private VehicleBrand selectedVehicleBrand;

        public VehicleBrand SelectedVehicleBrand
        {
            get
            {
                return selectedVehicleBrand;
            }
            set
            {
                SetProperty(ref selectedVehicleBrand, value);
                if (newCarViewModel != null && newCarViewModel.NewVehicleModificationHelper != null)
                {
                    newCarViewModel.NewVehicleModificationHelper.FillVehicleModifications(value);
                }
            }
        }

        private ObservableCollection<VehicleBrand> vehicleBrands;
        public ObservableCollection<VehicleBrand> VehicleBrands
        {
            get
            {
                return vehicleBrands;
            }
            set
            {
                SetProperty(ref vehicleBrands, value);
            }
        }

        private string newVehicleBrandText;
        public string NewVehicleBrandText
        {
            get
            {
                return newVehicleBrandText;
            }
            set
            {
                SetProperty(ref newVehicleBrandText, value);
            }
        }

        private RelayCommand addVehicleBrandCommand;
        public RelayCommand AddVehicleBrandCommand
        {
            get
            {
                return addVehicleBrandCommand ?? (addVehicleBrandCommand = new RelayCommand(action =>
                {
                    VehicleBrand vehicleBrand = new VehicleBrand(newVehicleBrandText);
                    storeService.VehicleService.AddBrand(vehicleBrand);
                    vehicleBrands.Add(vehicleBrand);
                    SelectedVehicleBrand = vehicleBrand;
                }, func =>
                {
                    return true;
                }));
            }
        }
        IStoreService storeService;

        NewCarViewModel newCarViewModel;
        public NewVehicleBrandHelper(IStoreService storeService, NewCarViewModel newCarViewModel)
        {
            this.storeService = storeService;
            this.newCarViewModel = newCarViewModel;

            NewVehicleBrandText = "";
            //selectedVehicleBrand = new VehicleBrand();
            vehicleBrands = new ObservableCollection<VehicleBrand>();
            foreach (VehicleBrand vehicleBrand in storeService.VehicleService.GetAllBrands())
            {
                vehicleBrands.Add(vehicleBrand);
            }
        }
    }

}
