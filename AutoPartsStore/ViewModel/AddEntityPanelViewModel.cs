using AutoPartsStore.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AutoPartsStore.ViewModel
{
    public class AddEntityPanelViewModel
    {
        MainViewModel mainViewModel;
        public AddEntityPanelViewModel()
        {
            mainViewModel = MainViewModel.GetMainViewModel();
            mainViewModel.AddEntityPanelViewModel = this;
        }

        private RelayCommand addIntoCategoryCommand;
        public RelayCommand AddIntoCategoryCommand
        {
            get
            {
                return addIntoCategoryCommand ?? (addIntoCategoryCommand = new RelayCommand(action =>
                {
                    mainViewModel.CategoriesViewModel.OpenNewCategoryWindow(1);
                    //OpenNewCategoryWindow((int)action);
                }, func =>
                {
                    return true;
                }));
            }
        }
    }
}
