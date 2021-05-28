using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.BusinessLogicLayer.Service
{
    public interface IStoreService
    {
        public CategoryService CategoryService { get; }
        public VehicleService VehicleService { get; }
        public ManufacturerService ManufacturerService { get; }

        public ProductService ProductService { get; }

        public VehiclePartService VehiclePartService { get; }
        public UserService UserService { get; }
        public OrderService OrderService { get; }
        public ReviewService ReviewService { get; }
        public AdminService AdminService { get; }
    }
}
