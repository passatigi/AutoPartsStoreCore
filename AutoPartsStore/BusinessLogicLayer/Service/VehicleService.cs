using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.BusinessLogicLayer.Service
{
    public class VehicleService
    {
        IUnitOfWork unitOfWork;
        public VehicleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<VehicleBrand> GetAllBrands()
        {
            return unitOfWork.VehicleRepository.GetAll();
        }
        public void AddBrand(VehicleBrand vehicleBrand)
        {
            unitOfWork.VehicleRepository.Add(vehicleBrand);
            unitOfWork.Save();
        }
        public IEnumerable<VehicleModification> GetModifications(VehicleBrand vehicleBrand) {
            return unitOfWork.VehicleModificationRepository.GetAll().Where(vm => vm.VehicleBrand == vehicleBrand);
        }
        public void AddModification(VehicleModification vehicleModification)
        {
            unitOfWork.VehicleModificationRepository.Add(vehicleModification);
            unitOfWork.Save();
        }

        public IEnumerable<VehicleEngine> GetEngines(VehicleModification vehicleModification)
        {
            return unitOfWork.VehicleEngineRepository.GetAll().Where(ve => ve.VehicleModification == vehicleModification);
        }
        public void AddEngine(VehicleEngine vehicleEngine)
        {
            unitOfWork.VehicleEngineRepository.Add(vehicleEngine);
            unitOfWork.Save();
        }

        public void EditModification(VehicleModification vehicleModification)
        {
            unitOfWork.VehicleModificationRepository.Update(vehicleModification);
            unitOfWork.Save();
        }
        public void EditEgnine(VehicleEngine vehicleEngine)
        {
            unitOfWork.VehicleEngineRepository.Update(vehicleEngine);
            unitOfWork.Save();
        }
        public void DeleteVehicleModification(VehicleModification vehicleModification)
        {
            unitOfWork.VehicleModificationRepository.Delete(vehicleModification.Id);
            unitOfWork.Save();
        }

        public void DeleteVehicleEgnine(VehicleEngine vehicleEngine)
        {
            unitOfWork.VehicleEngineRepository.Delete(vehicleEngine.Id);
            unitOfWork.Save();
        }
    }
}
