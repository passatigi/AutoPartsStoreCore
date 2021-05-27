using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.BusinessLogicLayer.Service
{
    public class AdminService
    {
        IUnitOfWork unitOfWork;
        public AdminService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        Administrator administrator;
        public void SetAdministrator(Customer customer)
        {
            administrator = unitOfWork.AdminRepository.GetAs(new Administrator { Customer = customer }).FirstOrDefault();
        }
        public Administrator ConfirmAdminPassword(string password)
        {
            if(administrator != null)
            {
                if (administrator.AdminPassword.Equals(password))
                {
                    Administrator tempAdmin = administrator;
                    UpdateAdmin();
                    return tempAdmin;
                }
            }
                return null;
        }
        public void UpdateAdmin()
        {
            administrator = null;
        }

    }
}
