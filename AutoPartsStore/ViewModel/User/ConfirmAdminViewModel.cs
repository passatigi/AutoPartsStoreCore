using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AutoPartsStore.ViewModel
{
    class ConfirmAdminViewModel : BaseViewModel
    {
        private string confirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                SetProperty(ref confirmPassword, value);
            }
        }
        private RelayCommand confirmAdminCommand;
        public RelayCommand ConfirmAdminCommand
        {
            get
            {
                return confirmAdminCommand ?? (confirmAdminCommand = new RelayCommand(action =>
                {
                    Administrator administrator = storeService.AdminService.ConfirmAdminPassword(confirmPassword);
                    if (administrator != null)
                    {
                        UserConfiguration userConfiguration = UserConfiguration.GetUserConfiguration();
                        userConfiguration.Customer = administrator.Customer;
                        userConfiguration.SetAdmin(administrator, ConfirmPassword);
                    }
                    else
                    {
                        MessageBox.Show("Не верный пароль администратора");
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }

        IStoreService storeService;
        public ConfirmAdminViewModel()
        {
            storeService = StoreService.GetStoreService();

        }
        }
    }
