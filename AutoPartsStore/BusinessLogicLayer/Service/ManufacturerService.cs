using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.BusinessLogicLayer.Service
{
    public class ManufacturerService
    {
        IUnitOfWork unitOfWork;
        public ManufacturerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Manufacturer> GetAllManufacturers()
        {
            return unitOfWork.ManufacturerRepository.GetAll();
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            unitOfWork.ManufacturerRepository.Add(manufacturer);
        }
    }
}
