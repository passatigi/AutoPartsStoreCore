using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AutoPartsStore.ViewModel
{
    public class UserViewModel : BaseViewModel
    {

        MainViewModel mainViewModel;

        IStoreService storeService;

        private bool isLogined;
        public bool IsLogined
        {
            get
            {
                return isLogined;
            }
            set
            {
                SetProperty(ref isLogined, value);
            }
        }
        public UserViewModel()
        {
            storeService = StoreService.GetStoreService();
            mainViewModel = MainViewModel.GetMainViewModel();
            mainViewModel.UserViewModel = this;

            Customer = new Customer();
            ConfirmPassword = "";
        }

        private Customer customer;
        public Customer Customer
        {
            get
            {
                return customer;
            }
            set
            {
                SetProperty(ref customer, value);
            }
        }
        private Customer editCustomer;
        public Customer EditCustomer
        {
            get
            {
                return editCustomer;
            }
            set
            {
                SetProperty(ref editCustomer, value);
            }
        }
        private void ValidateUser(Customer customer)
        {

        }
        
        public string ConfirmPassword { get; set; }

        bool CheckFields()
        {
            if(customer.FullName == null || customer.FullName == "" ||
                customer.Address == null || customer.Address == "" ||
                customer.Mail == null || customer.Mail == "" ||
                customer.PhoneNumber == null || customer.PhoneNumber == "" ||
                customer.Password == null || customer.Password == ""
                )
            {
                return false;
            }
            return customer.Password.Equals(ConfirmPassword);
        }
        public void UpdateUser()
        {
            Customer = UserConfiguration.GetUserConfiguration().Customer;
        }
        private const string adminParm = "--admin";
        private RelayCommand authorizationCommand;
        public RelayCommand AuthorizationCommand
        {
            get
            {
                return authorizationCommand ?? (authorizationCommand = new RelayCommand(action =>
                {
                    if (action is string)
                    {
                        string str = action as string;
                        if (str.Equals("login")){
                            if(customer.Mail == null || customer.Mail == "" ||
                            customer.Password == null || customer.Password == ""
                            )
                            {
                                WindowProvider.NotifynWindow("неправильно поля заполненгы ");
                            }
                            else
                            {
                                try
                                {
                                    bool isAdmin = false;
                                    if (customer.Mail.StartsWith(adminParm))
                                    {
                                        isAdmin = true;
                                        customer.Mail = customer.Mail.Substring(adminParm.Length);
                                    }
                                    if (storeService.UserService.HasCustomer(Customer))
                                    {
                                        Customer tempCustomer = storeService.UserService.TryLogIn(Customer);
                                        if (tempCustomer == null)
                                        {
                                            WindowProvider.NotifynWindow("Неправильный пароль");
                                        }
                                        else
                                        {
                                            if (isAdmin)
                                            {
                                                if (storeService.AdminService.SetAdministrator(tempCustomer))
                                                {
                                                    WindowProvider.OpenConfirmAdminWindow();
                                                }
                                                else
                                                {
                                                    WindowProvider.NotifynWindow("Администратор отсутствует");
                                                }            
                                            }
                                            else
                                            {
                                                UserConfiguration.GetUserConfiguration().Customer = tempCustomer;
                                                Customer = tempCustomer;
                                            }
                                            
                                        }
                                    }
                                    else
                                    {
                                        WindowProvider.NotifynWindow("Пользователь отсутсвует");

                                    }
                                }
                                catch (Exception e)
                                {
                                    WindowProvider.NotifynWindow(e.Message);
                                }
                            }
                        }
                        else if (str.Equals("reg")){
                            if (CheckFields())
                            {
                                try {
                                    if (storeService.UserService.HasCustomer(Customer))
                                    {
                                        WindowProvider.NotifynWindow("Мэйл занят");
                                    }
                                    else
                                    {
                                        if (storeService.UserService.AddCustomer(Customer))
                                        {
                                            WindowProvider.NotifynWindow("Успешно добавлен");
                                            ConfirmPassword = "";
                                            Customer = new Customer();
                                            ConfirmPassword = "";
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    WindowProvider.NotifynWindow(e.Message);
                                }
                            }
                            else
                            {
                                WindowProvider.NotifynWindow("неправильно поля заполненгы ");
                            }

                        }
                    }

                }, func =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand editUserCommand;
        public RelayCommand EditUserCommand
        {
            get
            {
                return editUserCommand ?? (editUserCommand = new RelayCommand(action =>
                {
                    if (action is string)
                    {
                        string str = action as string;
                        if (str.Equals("edit"))
                        {
                            EditCustomer = new Customer();
                            EditCustomer.Update(Customer);
                            EditCustomer.Password = "";
                            
                        }
                        else if (str.Equals("save"))
                        {
                            try
                            {
                                if (!ConfirmPassword.Equals(Customer.Password))
                                {
                                    throw new Exception("Неверный пароль");
                                }
                                ValidateUser(EditCustomer);
                                Customer.Update(editCustomer);
                                storeService.UserService.UpdateCustomer(Customer);
                                EditCustomer = null;
                            }
                            catch (Exception e)
                            {
                                WindowProvider.NotifynWindow(e.Message);
                            }
                        }
                        else if (str.Equals("logout"))
                        {
                            Customer = new Customer();
                            UserConfiguration.GetUserConfiguration().Customer = null;
                            UserConfiguration.GetUserConfiguration().UnsetAdmin();

                        }
                        else if (str.Equals("back"))
                        {
                            EditCustomer = null;
                        }

                    }

                }, func =>
                {
                    return true;
                }));
            }
        }
    }
}
