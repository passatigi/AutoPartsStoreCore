﻿using AutoPartsStore.BusinessLogicLayer.Interfaces;
using AutoPartsStore.DataBaseConnector;
using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.BusinessLogicLayer.Service
{
    public interface IStoreService
    {
        public ICategoryService CategoryService { get; }
        public VehicleService VehicleService { get; }
        public ManufacturerService ManufacturerService { get; }

        public ProductService ProductService { get; }
    }
    class StoreService : IStoreService
    {
        #region singleton

        private static StoreService storeService;
        private static object syncRoot = new Object(); 
        public static StoreService GetStoreService()
        {
            if (storeService == null)
            {
                lock (syncRoot)
                {
                    if (storeService == null)
                        storeService = new StoreService();
                }
            }
            return storeService;
        }
        #endregion

        private IUnitOfWork unitOfWork;

        protected StoreService()
        {
            unitOfWork = EfStoreUnitOfWork.GetStoreUnitOfWork();
        }

        private CategoryService categoryService;
        public ICategoryService CategoryService
        {
            get
            {
                return categoryService ?? (categoryService = new CategoryService(unitOfWork));
            }
        }

        private VehicleService vehicleService;
        public VehicleService VehicleService
        {
            get
            {
                return vehicleService ?? (vehicleService = new VehicleService(unitOfWork));
            }
        }

        private ManufacturerService manufacturerService;
        public ManufacturerService ManufacturerService
        {
            get
            {
                return manufacturerService ?? (manufacturerService = new ManufacturerService(unitOfWork));
            }
        }

        private ProductService productService;
        public ProductService ProductService
        {
            get
            {
                return productService ?? (productService = new ProductService(unitOfWork));
            }
        }
    }
}
