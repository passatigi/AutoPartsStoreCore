using AutoPartsStore.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    class HeaderViewModel : BaseViewModel
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
                    }

                }, func =>
                {
                    return true;
                }));
            }
        }

    }
}
