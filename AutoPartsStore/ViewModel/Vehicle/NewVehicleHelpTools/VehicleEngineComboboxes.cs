using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;


namespace AutoPartsStore.ViewModel.NewVehicleHelpTools
{
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

}
