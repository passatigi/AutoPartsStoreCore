using AutoPartsStore.Command;
using AutoPartsStore.Model;
using AutoPartsStore.DataBaseConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoPartsStore.View;
using System.Runtime.CompilerServices;
using AutoPartsStore.BusinessLogicLayer.Service;


namespace AutoPartsStore.ViewModel
{
    public class CategoriesViewModel : BaseViewModel
    {
        
        private string inputCategoryString;
        public string InputCategoryString
        {
            get
            {
                return inputCategoryString ?? (inputCategoryString = "");
            }
            set
            {
                inputCategoryString = value;
                NotifyPropertyChanged("InputCategoryString");
                if (inputCategoryString.Equals(""))
                {
                    ObservableCollection<Category> categories = storeService.CategoryService.GetMainCategory().Nodes;
                    MainCategoryNode.Nodes = categories;
                }
            }
        }

        #region commands
        private RelayCommand addCategoryOemNumberCommand;
        public RelayCommand AddCategoryOemNumberCommand
        {
            get
            {
                return addCategoryOemNumberCommand ?? (addCategoryOemNumberCommand = new RelayCommand(action =>
                {
                    if (action is int)
                    {
                        if (mainViewModel.ChooseCarViewModel.SelectedVehicleEngine == null)
                        {
                            MessageBox.Show("В начале выберите тачку");
                        }
                        else
                        {
                            UserConfiguration.GetUserConfiguration().SelectedCategory = storeService.CategoryService.GetCategoryById((int)action);
                            WindowProvider.OpenAddOemToVehicleCategoryWindow();
                            //mainViewModel.AddOemToCarCategoryViewModel.UpdateOemToCarCategoryPage();
                        }
                    }

                }, func =>
                {
                    return true;
                }));
            }
        }
        
        private RelayCommand findProductCommand;
        public RelayCommand FindProductCommand
        {
            get
            {
                return findProductCommand ?? (findProductCommand = new RelayCommand(action =>
                {
                    if (mainViewModel.ChooseCarViewModel.SelectedVehicleEngine == null)
                    {
                        MessageBox.Show("В начале выберите тачку");
                    }
                    else
                    {
                        UserConfiguration.GetUserConfiguration().SelectedCategory = storeService.CategoryService.GetCategoryById((int)action);
                        WindowProvider.OpenProductsList();
                        if (mainViewModel.ProductsViewModel != null)
                        {
                            mainViewModel.ProductsViewModel.UpdateProductsList();
                        }
                        
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand findCategoryCommand;
        public RelayCommand FindCategoryCommand
        {
            get
            {
                return findCategoryCommand ?? (findCategoryCommand = new RelayCommand(action =>
                {
                    if (inputCategoryString.Equals("") || inputCategoryString == null)
                    {
                        try
                        {
                            mainViewModel.CategoriesViewModel.UpdateCategoryList();
                        }
                        catch (Exception e)
                        {
                            WindowProvider.NotifyWindow(e.Message);
                        }
                    }
                    else
                    {
                        
                        try
                        {
                            //ProductViewModel.ProductViewModelObject.
                            mainCategoryNode.Nodes.Clear();
                            foreach (Category category in
                            storeService.CategoryService.GetAllCategories().Where(c => c.Name.Contains(inputCategoryString)))
                            {
                                mainCategoryNode.Nodes.Add(category);
                            }
                        }
                        catch (Exception e)
                        {
                            WindowProvider.NotifyWindow(e.Message);
                        }
                    }

                }, func =>
                {
                    return true;
                }));
            }
        }



        public void OpenNewCategoryWindow(int parentId)
        {
            try
            {
                mainViewModel.UserConfiguration.SelectedCategory = categoryService.GetCategoryById(parentId);
                WindowProvider.AdminOpenEditCategoryWindow();
                if (mainViewModel.EditCategoryViewModel != null)
                {
                    mainViewModel.EditCategoryViewModel.UpdateSelectedCategory();
                }

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private RelayCommand addIntoCategoryCommand;
        public RelayCommand AddIntoCategoryCommand
        {
            get
            {
                return addIntoCategoryCommand ?? (addIntoCategoryCommand = new RelayCommand(action =>
                {
                    if (action is int)
                    {
                        OpenNewCategoryWindow((int)action);
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand addWithCategoryCommand;
        public RelayCommand AddWithCategoryCommand
        {
            get
            {
                return addWithCategoryCommand ?? (addWithCategoryCommand = new RelayCommand(action =>
                {
                    if(action is int)
                    {
                        OpenNewCategoryWindow(categoryService.GetParentId((int)action));
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }


        private RelayCommand editCategoryCommand;
        public RelayCommand EditCategoryCommand
        {
            get
            {
                return editCategoryCommand ?? (editCategoryCommand = new RelayCommand(action =>
                {
                    if(action is int)
                    {
                        OpenNewCategoryWindow((int)action);
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }

        #endregion

        public void UpdateCategoryList()
        {
            MainCategoryNode = storeService.CategoryService.GetMainCategory();
        }

        private Category mainCategoryNode;


        private Category newCategory;

        #region Properties

        public Category NewCategory
        {
            get
            {
                return newCategory;
            }
            set
            {
                SetProperty(ref newCategory, value);
            }
        }
        public Category MainCategoryNode
        {
            get
            {
                return mainCategoryNode;
            }
            set
            {
                mainCategoryNode = value;
                NotifyPropertyChanged(nameof(MainCategoryNode));
            }
        }
        #endregion

        MainViewModel mainViewModel;

        IStoreService storeService;
        CategoryService categoryService;

       
        public CategoriesViewModel()
        {
                storeService = StoreService.GetStoreService();
            mainViewModel = MainViewModel.GetMainViewModel();
                mainViewModel.CategoriesViewModel = this;



            MainCategoryNode = new Category();

            UpdateCategoryList();
            categoryService = storeService.CategoryService;
        }

    }
}
