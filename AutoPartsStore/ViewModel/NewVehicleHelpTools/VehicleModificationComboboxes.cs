using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AutoPartsStore.ViewModel.NewVehicleHelpTools
{
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
}
