using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.BusinessLogicLayer.Service
{
    public class UserService
    {
        IUnitOfWork unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public bool AddCustomer(Customer customer)
        {
            if (HasCustomer(customer))
            {
                return false;
            }
            else
            {
                unitOfWork.UserRepository.Add(customer);
                unitOfWork.Save();
                return true;
            }
        }
        public bool UpdateCustomer(Customer customer)
        {
                unitOfWork.UserRepository.Update(customer);
                unitOfWork.Save();
                return true;
        }

        public Customer GetCustomer(Customer customer)
        {
            Customer tempCustomer = unitOfWork.UserRepository.GetAs(customer).FirstOrDefault();
            return tempCustomer;
        }
        public bool HasCustomer(Customer customer)
        {
            if(unitOfWork.UserRepository.GetAs(customer).Count() == 0)
            {
                return false;
            }
            return true;
        }

        public Customer TryLogIn(Customer customer)
        {
            Customer temocustomer = GetCustomer(customer);
            if (temocustomer.Password.Equals(customer.Password)){
                return temocustomer;
            }
            else
            {
                return null;
            }


        }
    }
}
