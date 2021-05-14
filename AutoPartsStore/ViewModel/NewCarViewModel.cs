
using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoPartsStore.ViewModel
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
                if(newCarViewModel != null && newCarViewModel.NewVehicleModificationHelper  != null)
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
    public class VehicleModificationComboboxes
    {
        #region methods
        public void ClearVehicleModificationComboboxes()
        {
            vehicleModificationModels.Clear();
            vehicleModificationModelCodes.Clear();
            vehicleModificationReleaseStarts.Clear();
            vehicleModificationReleaseEnds.Clear();
        }
        public void FillVehicleModificationComboboxes(ObservableCollection<VehicleModification> vehicleModifications)
        {
            ClearVehicleModificationComboboxes();
            foreach (string model in
                vehicleModifications.Select(vm => vm.Model).Distinct()
                )
            {
                vehicleModificationModels.Add(model);
            }
            foreach (string modelCode in
                vehicleModifications.Select(vm => vm.ModelCode).Distinct()
                )
            {
                vehicleModificationModelCodes.Add(modelCode);
            }
            foreach (string releaseStart in
                vehicleModifications.Select(vm => vm.ReleaseStart).Distinct()
                )
            {
                vehicleModificationReleaseStarts.Add(releaseStart);
            }
            foreach (string releaseEnd in
                vehicleModifications.Select(vm => vm.ReleaseEnd).Distinct()
                )
            {
                vehicleModificationReleaseEnds.Add(releaseEnd);
            }
        }
        
        #endregion

        public ObservableCollection<string> vehicleModificationModels { get; set; }

        public ObservableCollection<string> vehicleModificationModelCodes { get; set; }

        public ObservableCollection<string> vehicleModificationReleaseStarts { get; set; }

        public ObservableCollection<string> vehicleModificationReleaseEnds { get; set; }


        public VehicleModificationComboboxes()
        {
            vehicleModificationModels = new ObservableCollection<string>();
            vehicleModificationModelCodes = new ObservableCollection<string>();
            vehicleModificationReleaseStarts = new ObservableCollection<string>();
            vehicleModificationReleaseEnds = new ObservableCollection<string>();
        }
    }
    public class NewVehicleModificationHelper : BaseViewModel
    {
        public VehicleModificationComboboxes VehicleModificationComboboxes { get; set; }
        public VehicleModification TextsVehicleModification { get; set; }

        private VehicleModification selectedVehicleModification;
        private VehicleModification newVehicleModification;
        private ObservableCollection<VehicleModification> vehicleModifications;

        #region properties
        
        public VehicleModification SelectedVehicleModification
        {
            get
            {
                return selectedVehicleModification;
            }
            set
            {
                SetProperty(ref selectedVehicleModification, value);
                if (newCarViewModel != null && newCarViewModel.NewVehicleEngineHelper != null)
                {
                    newCarViewModel.NewVehicleEngineHelper.FillVehicleEngines(value);
                }
            }
        }

        public VehicleModification NewVehicleModification
        {
            get
            {
                return newVehicleModification;
            }
            set
            {
                SetProperty(ref newVehicleModification, value);
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
        #endregion

        private RelayCommand addVehicleModificationPropertyCommand;
        private RelayCommand addVehicleModificationCommand;

        #region command realization
        public RelayCommand AddVehicleModificationPropertyCommand
        {
            get
            {
                return addVehicleModificationPropertyCommand ?? (addVehicleModificationPropertyCommand = new RelayCommand(action =>
                {
                    if(action is ObservableCollection<string>)
                    {
                        ObservableCollection<string> resultCombobox = action as ObservableCollection<string>;
                        string value;
                        if (resultCombobox == VehicleModificationComboboxes.vehicleModificationModels)
                        {
                            value = TextsVehicleModification.Model;
                            resultCombobox.Add(value);
                            NewVehicleModification.Model = value;
                        }
                        else if (resultCombobox == VehicleModificationComboboxes.vehicleModificationModelCodes)
                        {
                            value = TextsVehicleModification.ModelCode;
                            resultCombobox.Add(value);
                            NewVehicleModification.ModelCode = value;
                        }
                        else if (resultCombobox == VehicleModificationComboboxes.vehicleModificationReleaseStarts)
                        {
                            value = TextsVehicleModification.ReleaseStart;
                            resultCombobox.Add(value);
                            NewVehicleModification.ReleaseStart = value;
                        }
                        else if (resultCombobox == VehicleModificationComboboxes.vehicleModificationReleaseEnds)
                        {
                            value = TextsVehicleModification.ReleaseEnd;
                            resultCombobox.Add(value);
                            NewVehicleModification.ReleaseEnd = value;
                        }
                        else
                        {
                            throw new Exception("ne tuda popal");
                        }

                        NotifyPropertyChanged(nameof(NewVehicleModification));
                       
                    }
                    else
                    {
                        throw new InvalidCastException("addVehicleModificationPropertyCommand");
                    }
                },
                newCarViewModel.ModificationAccessible
                ));
            }
        }

       
        public RelayCommand AddVehicleModificationCommand
        {
            get
            {
                return addVehicleModificationCommand ?? (addVehicleModificationCommand = new RelayCommand(action =>
                {
                    if (CheckVehicleModification(NewVehicleModification))
                    {
                        VehicleModification vehicleModification = new VehicleModification(NewVehicleModification);
                        vehicleModification.Vehicle = newCarViewModel.GetSelectedVehicleBrand();
                        storeService.VehicleService.AddModification(vehicleModification);
                        vehicleModifications.Add(vehicleModification);
                        SelectedVehicleModification = vehicleModification;

                    }
                    else
                    {
                        MessageBox.Show("ne zapolnena modificathia");
                    }
                },
                newCarViewModel.ModificationAccessible
                ));
            }
        }
#endregion

        #region  methods
        public void FillVehicleModifications(VehicleBrand vehicleBrand)
        {
            vehicleModifications.Clear();
            foreach (VehicleModification vehicleModification in storeService.VehicleService.GetModifications(vehicleBrand))
            {
                vehicleModifications.Add(vehicleModification);
            }
            VehicleModificationComboboxes.FillVehicleModificationComboboxes(vehicleModifications);
        }
        public void ClearVehicleModifications()
        {
            vehicleModifications.Clear();
            VehicleModificationComboboxes.ClearVehicleModificationComboboxes();
        }

        private bool CheckVehicleModification(VehicleModification vehicleModification)
        {
            if (vehicleModification.Model == null || vehicleModification.Model == "" ||
                   vehicleModification.ModelCode == null || vehicleModification.ModelCode == "" ||
                   vehicleModification.ReleaseStart == null || vehicleModification.ReleaseStart == "" ||
                   vehicleModification.ReleaseEnd == null || vehicleModification.ReleaseEnd == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void EmtyTextsVehicleModification()
        {
            TextsVehicleModification.Model = "";
            TextsVehicleModification.ModelCode = "";
            TextsVehicleModification.ReleaseStart = "";
            TextsVehicleModification.ReleaseEnd = "";
        }
        #endregion


        IStoreService storeService;
        NewCarViewModel newCarViewModel;
        public NewVehicleModificationHelper(IStoreService storeService, NewCarViewModel newCarViewModel) 
        {
            this.storeService = storeService;
            this.newCarViewModel = newCarViewModel;


            VehicleModificationComboboxes = new VehicleModificationComboboxes();
            TextsVehicleModification = new VehicleModification();

            NewVehicleModification = new VehicleModification();

            EmtyTextsVehicleModification();
            vehicleModifications = new ObservableCollection<VehicleModification>();
        }
        
    }

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


    public class VehicleEngineComboboxes
    {
        #region methods
        public void ClearVehicleEngineComboboxes()
        {
            VehicleEngineVolumes.Clear();
            VehicleEngineModifications.Clear();
            VehicleEnginePowers.Clear();
            VehicleEngineModelCodes.Clear();
            VehicleEngineReleaseStarts.Clear();
            VehicleEngineReleaseEnds.Clear();
        }
        public void FillVehicleEngineComboboxes(ObservableCollection<VehicleEngine> VehicleEngines)
        {
            ClearVehicleEngineComboboxes();
            foreach (float volume in
                VehicleEngines.Select(vm => vm.Volume).Distinct()
                )
            {
                VehicleEngineVolumes.Add(volume);
            }
            foreach (string modification in
                VehicleEngines.Select(vm => vm.Modification).Distinct()
                )
            {
                VehicleEngineModifications.Add(modification);
            }
            foreach (short power in
                VehicleEngines.Select(vm => vm.Power).Distinct()
                )
            {
                VehicleEnginePowers.Add(power);
            }
            foreach (string modelCode in
                VehicleEngines.Select(vm => vm.ModelCode).Distinct()
                )
            {
                VehicleEngineModelCodes.Add(modelCode);
            }
            foreach (string releaseStart in
                VehicleEngines.Select(vm => vm.ReleaseStart).Distinct()
                )
            {
                VehicleEngineReleaseStarts.Add(releaseStart);
            }
            foreach (string releaseEnd in
                VehicleEngines.Select(vm => vm.ReleaseEnd).Distinct()
                )
            {
                VehicleEngineReleaseEnds.Add(releaseEnd);
            }
        }

        #endregion

        public ObservableCollection<float> VehicleEngineVolumes { get; set; }
        public ObservableCollection<string> VehicleEngineModifications { get; set; }
        public ObservableCollection<short> VehicleEnginePowers { get; set; }

        public ObservableCollection<string> VehicleEngineModelCodes { get; set; }

        public ObservableCollection<string> VehicleEngineReleaseStarts { get; set; }

        public ObservableCollection<string> VehicleEngineReleaseEnds { get; set; }


        public VehicleEngineComboboxes()
        {
            VehicleEngineVolumes = new ObservableCollection<float>();
            VehicleEngineModifications = new ObservableCollection<string>();
            VehicleEnginePowers = new ObservableCollection<short>();
            VehicleEngineModelCodes = new ObservableCollection<string>();
            VehicleEngineReleaseStarts = new ObservableCollection<string>();
            VehicleEngineReleaseEnds = new ObservableCollection<string>();
        }
    }
    public class NewVehicleEngineHelper : BaseViewModel
    {
        public VehicleEngineComboboxes VehicleEngineComboboxes { get; set; }
        public VehicleEngine TextsVehicleEngine { get; set; }

        private VehicleEngine selectedVehicleEngine;
        private VehicleEngine newVehicleEngine;
        private ObservableCollection<VehicleEngine> vehicleEngines;

        #region properties

        public VehicleEngine SelectedVehicleEngine
        {
            get
            {
                return selectedVehicleEngine;
            }
            set
            {
                SetProperty(ref selectedVehicleEngine, value);
                //newCarModificationHelper.FillVehicleEngines(value);
            }
        }

        public VehicleEngine NewVehicleEngine
        {
            get
            {
                return newVehicleEngine;
            }
            set
            {
                SetProperty(ref newVehicleEngine, value);
            }
        }


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
        #endregion

        private RelayCommand addVehicleEnginePropertyCommand;
        private RelayCommand addVehicleEngineCommand;

        #region command realization
        public RelayCommand AddVehicleEnginePropertyCommand
        {
            get
            {
                return addVehicleEnginePropertyCommand ?? (addVehicleEnginePropertyCommand = new RelayCommand(action =>
                {
                    if (action is ObservableCollection<string>)
                    {
                        ObservableCollection<string> resultCombobox = action as ObservableCollection<string>;
                        string value;
                        if (resultCombobox == VehicleEngineComboboxes.VehicleEngineModifications)
                        {
                            value = TextsVehicleEngine.Modification;
                            resultCombobox.Add(value);
                            NewVehicleEngine.Modification = value;
                        }
                        else if (resultCombobox == VehicleEngineComboboxes.VehicleEngineModelCodes)
                        {
                            value = TextsVehicleEngine.ModelCode;
                            resultCombobox.Add(value);
                            NewVehicleEngine.ModelCode = value;
                        }
                        else if (resultCombobox == VehicleEngineComboboxes.VehicleEngineReleaseStarts)
                        {
                            value = TextsVehicleEngine.ReleaseStart;
                            resultCombobox.Add(value);
                            NewVehicleEngine.ReleaseStart = value;
                        }
                        else if (resultCombobox == VehicleEngineComboboxes.VehicleEngineReleaseEnds)
                        {
                            value = TextsVehicleEngine.ReleaseEnd;
                            resultCombobox.Add(value);
                            NewVehicleEngine.ReleaseEnd = value;
                        }
                        else
                        {
                            throw new Exception("ne tuda popal");
                        }
                    }
                    else if(action is ObservableCollection<float>)
                    {
                        ObservableCollection<float> resultCombobox = action as ObservableCollection<float>;
                        float value;
                        if (resultCombobox == VehicleEngineComboboxes.VehicleEngineVolumes)
                        {
                            value = TextsVehicleEngine.Volume;
                            resultCombobox.Add(value);
                            NewVehicleEngine.Volume = value;
                        }
                    }
                    else if (action is ObservableCollection<short>)
                    {
                        ObservableCollection<short> resultCombobox = action as ObservableCollection<short>;
                        short value;
                        if (resultCombobox == VehicleEngineComboboxes.VehicleEnginePowers)
                        {
                            value = TextsVehicleEngine.Power;
                            resultCombobox.Add(value);
                            NewVehicleEngine.Power = value;
                        }
                    }
                    else
                    {
                        throw new InvalidCastException("addVehicleEnginePropertyCommand");
                    }
                    NotifyPropertyChanged(nameof(NewVehicleEngine));
                },
                newCarViewModel.EngineAccessible
                ));
            }
        }


        public RelayCommand AddVehicleEngineCommand
        {
            get
            {
                return addVehicleEngineCommand ?? (addVehicleEngineCommand = new RelayCommand(action =>
                {
                    if (CheckVehicleEngine(NewVehicleEngine))
                    {
                        VehicleEngine VehicleEngine = new VehicleEngine(newCarViewModel.GetSelectedVehicleModification(), NewVehicleEngine);
                        storeService.VehicleService.AddEngine(VehicleEngine);
                        VehicleEngines.Add(VehicleEngine);
                        SelectedVehicleEngine = VehicleEngine;
                    }
                    else
                    {
                        MessageBox.Show("ne zapolnen engine");
                    }
                },
                newCarViewModel.EngineAccessible
                ));
            }
        }
        #endregion

        #region  methods
        public void FillVehicleEngines(VehicleModification vehicleModification)
        {
            VehicleEngines.Clear();
            foreach (VehicleEngine VehicleEngine in storeService.VehicleService.GetEngines(vehicleModification))
            {
                VehicleEngines.Add(VehicleEngine);
            }
            VehicleEngineComboboxes.FillVehicleEngineComboboxes(VehicleEngines);
        }
        public void ClearVehicleEngines()
        {
            VehicleEngines.Clear();
            VehicleEngineComboboxes.ClearVehicleEngineComboboxes();
        }

        private bool CheckVehicleEngine(VehicleEngine VehicleEngine)
        {
            if (VehicleEngine.Volume == 0 || VehicleEngine.Power == 0 ||
                   VehicleEngine.Modification == null || VehicleEngine.ReleaseEnd == "" ||
                   VehicleEngine.ModelCode == null || VehicleEngine.ModelCode == "" ||
                   VehicleEngine.ReleaseStart == null || VehicleEngine.ReleaseStart == "" ||
                   VehicleEngine.ReleaseEnd == null || VehicleEngine.ReleaseEnd == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void EmtyTextsVehicleEngine()
        {
            TextsVehicleEngine.Volume = 0;
            TextsVehicleEngine.Modification = "";
            TextsVehicleEngine.Power = 0;
            TextsVehicleEngine.ModelCode = "";
            TextsVehicleEngine.ReleaseStart = "";
            TextsVehicleEngine.ReleaseEnd = "";
        }
        #endregion


        IStoreService storeService;
        NewCarViewModel newCarViewModel;
        public NewVehicleEngineHelper(IStoreService storeService, NewCarViewModel newCarViewModel)
        {
            this.storeService = storeService;
            this.newCarViewModel = newCarViewModel;


            VehicleEngineComboboxes = new VehicleEngineComboboxes();
            TextsVehicleEngine = new VehicleEngine();

            NewVehicleEngine = new VehicleEngine();

            EmtyTextsVehicleEngine();
            VehicleEngines = new ObservableCollection<VehicleEngine>();
        }

    }

}