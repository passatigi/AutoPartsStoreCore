using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.ViewModel
{
    public class AddOemToCarCategoryViewModel : BaseViewModel
    {
        IStoreService storeService;
        MainViewModel mainViewModel;
        UserConfiguration userConfiguration;
        public AddOemToCarCategoryViewModel()
        {
            storeService = StoreService.GetStoreService();

            mainViewModel = MainViewModel.GetMainViewModel();
            mainViewModel.AddOemToCarCategoryViewModel = this;

            userConfiguration = UserConfiguration.GetUserConfiguration();


            ChooseCarViewModel = new ChooseCarViewModel(userConfiguration.SelectedVehicleEngine);
            Categories = new ObservableCollection<Category>();
            UpdateOemToCarCategoryPage();
        }

        public void UpdateOemToCarCategoryPage()
        {
            VehiclePart = storeService.VehiclePartService.GetVehiclePart(
                userConfiguration.SelectedVehicleEngine,
                userConfiguration.SelectedCategory
                );
            if(ChooseCarViewModel.SelectedVehicleEngine.Id != VehiclePart.VehicleEngine.Id)
            {
                ChooseCarViewModel.FillVehicleByEngine(VehiclePart.VehicleEngine);
            }
           
            FillCategories("");
            SelectedCategory = Categories.Where(c => c.Id == VehiclePart.Category.Id).FirstOrDefault();
        }

        void FillCategories(string searchString)
        {
            Categories.Clear();
            foreach (Category category in storeService.CategoryService.GetAllCategories())
            {
                Categories.Add(category);
            }
        }

        public ChooseCarViewModel ChooseCarViewModel { get; set; }

        private VehiclePart vehiclePart;
        public VehiclePart VehiclePart
        {
            get
            {
                return vehiclePart;
            }
            set
            {
                SetProperty(ref vehiclePart, value);
            }
        }

        private ConcretVehiclePartOemNumber concretVehiclePartOemNumber;
        public ConcretVehiclePartOemNumber ConcretVehiclePartOemNumber
        {
            get
            {
                return concretVehiclePartOemNumber;
            }
            set
            {
                SetProperty(ref concretVehiclePartOemNumber, value);
            }
        }


        private Category selectedCategory;
        public Category SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                SetProperty(ref selectedCategory, value);
            }
        }

        private string oemNumberString;
        public string OemNumberString
        {
            get
            {
                return oemNumberString;
            }
            set
            {
                SetProperty(ref oemNumberString, value);
            }
        }


        private ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                SetProperty(ref categories, value);
            }
        }

        
        private RelayCommand addOemNumberCommand;
        public RelayCommand AddOemNumberCommand
        {
            get
            {
                return addOemNumberCommand ?? (addOemNumberCommand = new RelayCommand(action =>
                {
                    concretVehiclePartOemNumber = new ConcretVehiclePartOemNumber();
                    concretVehiclePartOemNumber.OEMNumber = oemNumberString;
                    concretVehiclePartOemNumber.VehiclePart = vehiclePart;
                    VehiclePart.ConcretVehiclePartOemNumbers.Add(concretVehiclePartOemNumber);
                }, func =>
                {
                    return true;
                }));
            }
        }
        private RelayCommand updateVehiclePartCommand;
        public RelayCommand UpdateVehiclePartCommand
        {
            get
            {
                return updateVehiclePartCommand ?? (updateVehiclePartCommand = new RelayCommand(action =>
                {
                    VehiclePart = storeService.VehiclePartService.GetVehiclePart(
                        ChooseCarViewModel.SelectedVehicleEngine,
                        SelectedCategory
                        );
                }, func =>
                {
                    return true;
                }));
            }
        }
         private RelayCommand updateVehiclePartOemNumbersCommand;
        public RelayCommand UpdateVehiclePartOemNumbersCommand
        {
            get
            {
                return updateVehiclePartOemNumbersCommand ?? (updateVehiclePartOemNumbersCommand = new RelayCommand(action =>
                {
                    storeService.VehiclePartService.SaveChanges();
                }, func =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand deleteVehiclePartOemNumbersCommand;
        public RelayCommand DeleteVehiclePartOemNumbersCommand
        {
            get
            {
                return deleteVehiclePartOemNumbersCommand ?? (deleteVehiclePartOemNumbersCommand = new RelayCommand(action =>
                {
                    if (action is string) 
                    {
                        string oem = (string)action;
                        VehiclePart.ConcretVehiclePartOemNumbers.Remove(
                             VehiclePart.ConcretVehiclePartOemNumbers.Where(p => p.OEMNumber.Equals(oem)).LastOrDefault()
                            );
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }

    }
}
