using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
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
    }
}
