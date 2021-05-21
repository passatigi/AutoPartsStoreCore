using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.BusinessLogicLayer.Service
{
    public class VehiclePartService
    {
        IUnitOfWork unitOfWork;
        public VehiclePartService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public VehiclePart GetVehiclePart(VehicleEngine vehicleEngine, Category category)
        {
            VehiclePart vehiclePart = new VehiclePart();
            vehiclePart.Category = category;
            vehiclePart.VehicleEngine = vehicleEngine;
            VehiclePart outVehiclePart = unitOfWork.VehiclePartRepository.GetAs(vehiclePart);
            if(outVehiclePart == null)
            {
                unitOfWork.VehiclePartRepository.Add(vehiclePart);
                outVehiclePart = vehiclePart;
            }
            return outVehiclePart;
        }
        public void SaveChanges()
        {
            unitOfWork.Save();
        }


        //public void AddVehiclePart(VehiclePart vehiclePart)
        //{
        //    unitOfWork.VehiclePartRepository.Add(vehiclePart);
        //}
        //public void AddVehiclePart(VehiclePart vehiclePart)
        //{
        //    unitOfWork.VehiclePartRepository.Add(vehiclePart);
        //}

    }
}
