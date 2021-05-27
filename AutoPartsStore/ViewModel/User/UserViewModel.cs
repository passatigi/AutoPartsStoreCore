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
                                MessageBox.Show("неправильно поля заполненгы ");
                            }
                            else
                            {

                                if (storeService.UserService.HasCustomer(Customer))
                                {
                                    Customer tempCustomer = storeService.UserService.TryLogIn(Customer);
                                    if (tempCustomer == null)
                                    {
                                        MessageBox.Show("Неправильный пароль");
                                    }
                                    else
                                    {
                                        UserConfiguration.GetUserConfiguration().Customer = tempCustomer;
                                        Customer = tempCustomer;
                                        IsLogined = true;

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Пользователь отсутсвует ( ");

                                }
                            }
                        }
                        else if (str.Equals("reg")){
                            if (CheckFields())
                            {
                              if (storeService.UserService.HasCustomer(Customer))
                                {
                                    MessageBox.Show("Пользователь занят ( ");
                                }
                                else
                                {
                                    if (storeService.UserService.AddCustomer(Customer))
                                    {
                                        MessageBox.Show("Успешно добавлен");
                                        ConfirmPassword = "";
                                        Customer = new Customer();
                                        ConfirmPassword = "";

                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("неправильно поля заполненгы ");
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
                                MessageBox.Show(e.Message);
                            }
                        }
                        else if (str.Equals("logout"))
                        {
                            Customer = new Customer();
                            UserConfiguration.GetUserConfiguration().Customer = null;
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
