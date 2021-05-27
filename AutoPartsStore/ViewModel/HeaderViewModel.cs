using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    public class HeaderViewModel : BaseViewModel
    {
        private string searchString;
        public string SearchString
        {

            get
            {
                return searchString ?? (searchString = "");
            }
            set
            {
                SetProperty(ref searchString, value);
            }
        }

        private RelayCommand openWindowCommand;
        public RelayCommand OpenWindowCommand
        {
            get
            {
                return openWindowCommand ?? (openWindowCommand = new RelayCommand(action =>
                {
                    if (action is string)
                    {
                        string parm = action as string;
                        if (parm.Equals("User"))
                        {
                            WindowProvider.OpenUserWindow();
                        }
                        else if (parm.Equals("shoppingCart"))
                        {
                            WindowProvider.OpenShoppingCartWindow();
                        }
                        else if (parm.Equals("orders"))
                        {
                            WindowProvider.OpenOrderWindow();
                        }
                        else if (parm.Equals("FirstPage"))
                        {
                            WindowProvider.OpenFirstPage();
                        }
                    }

                }, func =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                return searchCommand ?? (searchCommand = new RelayCommand(action =>
                {
                    mainViewModel.SearchString(searchString);
                }, func =>
                {
                    return true;
                }));
            }
        }

        MainViewModel mainViewModel;
        public HeaderViewModel()
        {

            mainViewModel = MainViewModel.GetMainViewModel();
            mainViewModel.HeaderViewModel = this;
        }
    }
}
