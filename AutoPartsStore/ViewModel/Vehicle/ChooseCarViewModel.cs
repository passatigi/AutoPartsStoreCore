using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.ViewModel
{
    class ChooseCarViewModel : BaseViewModel
    {
        IStoreService storeService;
        MainViewModel mainViewModel;
        public ChooseCarViewModel()
        {

            
            storeService = StoreService.GetStoreService();

            mainViewModel = MainViewModel.GetMainViewModel();
            mainViewModel.ChooseCarViewModel = this;


            

            vehicleBrands = new ObservableCollection<VehicleBrand>();
            vehicleModifications = new ObservableCollection<VehicleModification>();
            vehicleEngines = new ObservableCollection<VehicleEngine>();

            //selectedvehiclebrand = new vehiclebrand();
            //selectedvehiclemodification = new vehiclemodification();
            //selectedvehicleengine = new vehicleengine();
            FillVehicleBrands();
        }

        public ChooseCarViewModel(VehicleEngine vehicleEngine)
        {


            storeService = StoreService.GetStoreService();

            mainViewModel = MainViewModel.GetMainViewModel();
            //mainViewModel.ChooseCarViewModel = this;

            vehicleEngines = new ObservableCollection<VehicleEngine>();
            vehicleBrands = new ObservableCollection<VehicleBrand>();
            vehicleModifications = new ObservableCollection<VehicleModification>();

            FillVehicleBrands();
            SelectedVehicleBrand = vehicleEngine.VehicleModification.VehicleBrand;
            SelectedVehicleModification = vehicleEngine.VehicleModification;
            SelectedVehicleEngine = vehicleEngine;            
        }

        void FillVehicleBrands()
        {
            vehicleBrands.Clear();
            foreach (VehicleBrand vehicle in storeService.VehicleService.GetAllBrands())
            {
                vehicleBrands.Add(vehicle);
            }
            NotifyPropertyChanged(nameof(VehicleBrands));
        }
        void FillVehicleModifications(VehicleBrand vehicleBrand)
        {
            vehicleModifications.Clear();
            foreach (VehicleModification vehicle in storeService.VehicleService.GetModifications(vehicleBrand))
            {
                vehicleModifications.Add(vehicle);
            }
            NotifyPropertyChanged(nameof(VehicleModifications));
        }

        void FillVehicleEngines(VehicleModification vehicleModification)
        {
            vehicleEngines.Clear();
            foreach (VehicleEngine vehicle in storeService.VehicleService.GetEngines(vehicleModification))
            {
                vehicleEngines.Add(vehicle);
            }
            NotifyPropertyChanged(nameof(vehicleEngines));
        }

        private VehicleBrand selectedVehicleBrand;
        private VehicleModification selectedVehicleModification;
        private VehicleEngine selectedVehicleEngine;

        #region CurrentVehicleProperties
        public VehicleBrand SelectedVehicleBrand
        {
            get
            {
                return selectedVehicleBrand;
            }
            set
            {
                SetProperty(ref selectedVehicleBrand, value);
                if (value != null)
                {
                    FillVehicleModifications(selectedVehicleBrand);
                }
            }
        }

        public VehicleModification SelectedVehicleModification
        {
            get
            {
                return selectedVehicleModification;
            }
            set
            {
                SetProperty(ref selectedVehicleModification, value);
                if (value != null)
                {
                    FillVehicleEngines(selectedVehicleModification);
                }
            }
        }
        public VehicleEngine SelectedVehicleEngine
        {
            get
            {
                return selectedVehicleEngine;
            }
            set
            {
                SetProperty(ref selectedVehicleEngine, value);
                    UserConfiguration.GetUserConfiguration().SelectedVehicleEngine = value;

            }
        }




        #endregion

        private ObservableCollection<VehicleEngine> vehicleEngines;

        private ObservableCollection<VehicleModification> vehicleModifications;

        private ObservableCollection<VehicleBrand> vehicleBrands;

        #region ComboboxCollections 
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
        public ObservableCollection<VehicleModification> VehicleModifications
        {
            get
            {
                return vehicleModifications;
            }
            set
            {
                SetProperty(ref vehicleModifications, value);
            }
        }

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
        #endregion
    }
}
