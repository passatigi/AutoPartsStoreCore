
using AutoPartsStore.DataBaseConnector;
using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.BusinessLogicLayer.Service
{
    
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
        public CategoryService CategoryService
        {
            get
            {
                return categoryService ?? (categoryService = new CategoryService(unitOfWork));
            }
        }

        private UserService userService;
        public UserService UserService
        {
            get
            {
                return userService ?? (userService = new UserService(unitOfWork));
            }
        }
        private AdminService adminService;
        public AdminService AdminService
        {
            get
            {
                return adminService ?? (adminService = new AdminService(unitOfWork));
            }
        }
        private OrderService orderService;
        public OrderService OrderService
        {
            get
            {
                return orderService ?? (orderService = new OrderService(unitOfWork));
            }
        }
        private ReviewService reviewService;
        public ReviewService ReviewService
        {
            get
            {
                return reviewService ?? (reviewService = new ReviewService(unitOfWork));
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

        private VehiclePartService vehiclePartService;
        public VehiclePartService VehiclePartService
        {
            get
            {
                return vehiclePartService ?? (vehiclePartService = new VehiclePartService(unitOfWork));
            }
        }


    }
}
