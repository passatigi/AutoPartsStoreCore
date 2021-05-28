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
                    else if (action is ObservableCollection<float>)
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
                        try
                        {
                            VehicleEngine VehicleEngine = new VehicleEngine(newCarViewModel.GetSelectedVehicleModification(), NewVehicleEngine);
                            storeService.VehicleService.AddEngine(VehicleEngine);
                            VehicleEngines.Add(VehicleEngine);
                            SelectedVehicleEngine = VehicleEngine;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
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

        private RelayCommand editVehicleEngineCommand;
        public RelayCommand EditVehicleEngineCommand
        {
            get
            {
                return editVehicleEngineCommand ?? (editVehicleEngineCommand = new RelayCommand(action =>
                {
                    if (CheckVehicleEngine(NewVehicleEngine))
                    {
                        if (SelectedVehicleEngine != null)
                        {
                            try
                            {
                                SelectedVehicleEngine.CloneProperties(newVehicleEngine);
                                storeService.VehicleService.EditEgnine(SelectedVehicleEngine);
                                NotifyPropertyChanged(nameof(SelectedVehicleEngine));
                                FillVehicleEngines(newCarViewModel.GetSelectedVehicleModification());
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Перед изменением выберите нужный двигатель");
                        }
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
        private RelayCommand deleteVehicleModificationCommand;
        public RelayCommand DeleteVehicleModificationCommand
        {
            get
            {
                return deleteVehicleModificationCommand ?? (deleteVehicleModificationCommand = new RelayCommand(action =>
                {

                    if (SelectedVehicleEngine != null)
                    {
                        try
                        {
                            storeService.VehicleService.DeleteVehicleEgnine(SelectedVehicleEngine);
                            NotifyPropertyChanged(nameof(SelectedVehicleEngine));
                            FillVehicleEngines(newCarViewModel.GetSelectedVehicleModification());

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Необходимо выбрать модификацию");
                    }
                },
                newCarViewModel.ModificationAccessible
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
