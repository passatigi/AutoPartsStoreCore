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

        #region command
        public RelayCommand AddVehicleModificationPropertyCommand
        {
            get
            {
                return addVehicleModificationPropertyCommand ?? (addVehicleModificationPropertyCommand = new RelayCommand(action =>
                {
                    if (action is ObservableCollection<string>)
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
                        try
                        {
                            VehicleModification vehicleModification = new VehicleModification(NewVehicleModification);
                            vehicleModification.VehicleBrand = newCarViewModel.GetSelectedVehicleBrand();
                            storeService.VehicleService.AddModification(vehicleModification);
                            vehicleModifications.Add(vehicleModification);
                            SelectedVehicleModification = vehicleModification;
                        }
                        catch(Exception e)
                        {
                            WindowProvider.NotifyWindow(e.Message);
                        }

                    }
                    else
                    {
                        WindowProvider.NotifyWindow("ne zapolnena modificathia");
                    }
                },
                newCarViewModel.ModificationAccessible
                ));
            }
        }
        private RelayCommand editVehicleModificationCommand;
        public RelayCommand EditVehicleModificationCommand
        {
            get
            {
                return editVehicleModificationCommand ?? (editVehicleModificationCommand = new RelayCommand(action =>
                {
                    if (CheckVehicleModification(NewVehicleModification))
                    {
                        if (SelectedVehicleModification != null) 
                        {
                            try
                            {
                                SelectedVehicleModification.CloneProperties(NewVehicleModification);
                                storeService.VehicleService.EditModification(SelectedVehicleModification);
                                NotifyPropertyChanged(nameof(SelectedVehicleModification));
                                FillVehicleModifications(newCarViewModel.GetSelectedVehicleBrand());

                            }
                            catch (Exception e)
                            {
                                WindowProvider.NotifyWindow(e.Message);
                            }
                        }
                        else
                        {
                            WindowProvider.NotifyWindow("Перед изменением выберите нужный двигатель");
                        }
                    }
                    else
                    {
                        WindowProvider.NotifyWindow("ne zapolnena modificathia");
                    }
                },
                newCarViewModel.ModificationAccessible
                ));
            }
        }

        private RelayCommand deleteVehicleModificationCommand;
        public RelayCommand DeleteVehicleModificationCommand
        {
            get
            {
                return deleteVehicleModificationCommand ?? (deleteVehicleModificationCommand = new RelayCommand(action =>
                {

                        if (SelectedVehicleModification != null)
                        {
                            try
                            {
                                storeService.VehicleService.DeleteVehicleModification(SelectedVehicleModification);
                                NotifyPropertyChanged(nameof(SelectedVehicleModification));
                                FillVehicleModifications(newCarViewModel.GetSelectedVehicleBrand());

                            }
                            catch (Exception e)
                            {
                                WindowProvider.NotifyWindow(e.Message);
                            }
                        }
                        else
                        {
                            WindowProvider.NotifyWindow("Необходимо выбрать двигатель");
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

}
