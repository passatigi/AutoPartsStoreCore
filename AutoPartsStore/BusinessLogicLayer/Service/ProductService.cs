using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AutoPartsStore.BusinessLogicLayer.Service
{
    public class ProductService
    {
        IUnitOfWork unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void AddProduct(Product product)
        {
            unitOfWork.ProductRepository.Add(product);
            unitOfWork.Save();
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return unitOfWork.ProductRepository.GetAll();
        }
        public IEnumerable<Product> GetProductsByVehiclePart(VehiclePart vehiclePart)
        {
            ProductOEMNumber productOEMNumber = new ProductOEMNumber();
            productOEMNumber.VehicleBrand = vehiclePart.VehicleEngine.VehicleModification.VehicleBrand;
            List<Product> products = new List<Product>();
            foreach (ConcretVehiclePartOemNumber OEM in vehiclePart.ConcretVehiclePartOemNumbers)
            {
                productOEMNumber.OEM = OEM.OEMNumber;
                foreach (ProductOEMNumber productOEM in unitOfWork.ProductOEMNumberRepository.GetAs(productOEMNumber))
                {
                    products.Add(productOEM.Product);
                }
            }
            return products.Distinct();
        }
         
        public Product GetById(long id)
        {
            return unitOfWork.ProductRepository.GetById(id);
        }
    }
}
