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
using AutoPartsStore.BusinessLogicLayer.Interfaces;

namespace AutoPartsStore.ViewModel
{
    class CategoriesViewModel : BaseViewModel
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
                        UserConfiguration.GetUserConfiguration().SelectedCategory = storeService.CategoryService.GetCategoryById((int)action);

                    }

                }, func =>
                {
                    return true;
                }));
            }
        }
        
        private RelayCommand findCommand;
        public RelayCommand FindCommand
        {
            get
            {
                return findCommand ?? (findCommand = new RelayCommand(action =>
                {

                    //ProductViewModel.ProductViewModelObject.
                    //mainCategoryNode.Nodes.Clear();
                    //foreach (Category category in 
                    //storeService.CategoryService.GetAllCategories().Where(c => c.Name.Contains(inputCategoryString)))
                    //{
                    //    mainCategoryNode.Nodes.Add(category);
                    //}

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

                    //ProductViewModel.ProductViewModelObject.
                    mainCategoryNode.Nodes.Clear();
                    foreach (Category category in 
                    storeService.CategoryService.GetAllCategories().Where(c => c.Name.Contains(inputCategoryString)))
                    {
                        mainCategoryNode.Nodes.Add(category);
                    }

                }, func =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand addCategoryCommand;
        public RelayCommand AddCategoryCommand
        {
            get
            {
                return addCategoryCommand ?? (addCategoryCommand = new RelayCommand(action =>
                {
                    //if(newCategory.ParentCategory.Nodes == null)
                    //{
                    //    newCategory.ParentCategory.Nodes = new ObservableCollection<Category>();
                    //}
                    Category category = categoryService.AddCategory(newCategory.ParentCategory, newCategory.Name);
                    category.Nodes = new ObservableCollection<Category>();
                    MainCategoryNode = storeService.CategoryService.GetMainCategory();

                    ClearNewCategory();

                }, func =>
                {
                    return true;
                }));
            }
        }

        public void OpenNewCategoryWindow(int parentId)
        {
           
            NewCategoryWindow newCategoryWindow = new NewCategoryWindow();
            if (newCategory == null)
            {
                newCategory = new Category();
            }
            newCategory.ParentCategory = categoryService.GetCategoryById(parentId);
            ClearNewCategory();
            newCategoryWindow.Show();
            NotifyPropertyChanged(nameof(NewCategory));
        }
        private void ClearNewCategory()
        {
            newCategory.Name = "";
        }

        private RelayCommand addIntoCategoryCommand;
        public RelayCommand AddIntoCategoryCommand
        {
            get
            {
                return addIntoCategoryCommand ?? (addIntoCategoryCommand = new RelayCommand(action =>
                {
                    MessageBox.Show((int)action + " добавить в");
                    OpenNewCategoryWindow((int)action);
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
                    MessageBox.Show((int)action + "добавить с");
                    OpenNewCategoryWindow(categoryService.GetParentId((int)action));
                }, func =>
                {
                    return true;
                }));
            }
        }


        private RelayCommand renameCategoryCommand;
        public RelayCommand RenameCategoryCommand
        {
            get
            {
                return renameCategoryCommand ?? (renameCategoryCommand = new RelayCommand(action =>
                {
                    MessageBox.Show((int)action + " переименовать ");

                    
                }, func =>
                {
                    return true;
                }));
            }
        }

       
        private RelayCommand deleteCategoryCommand;
        public RelayCommand DeleteCategoryCommand
        {
            get
            {
                return deleteCategoryCommand ?? (deleteCategoryCommand = new RelayCommand(action =>
                {
                    //выполнить проверку
                    MessageBox.Show((int)action + "удалить");
                    storeService.CategoryService.DeleteCategoryById((int)action);
                    //categoryАccess.DeleteCategory((int)action);
                }, func =>
                {
                    return true;
                }));
            }
        }
        #endregion


        private Category mainCategoryNode;
        private ObservableCollection<Category> categoryTree;


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
        ICategoryService categoryService;

       
        public CategoriesViewModel()
        {
                storeService = StoreService.GetStoreService();

                mainViewModel = MainViewModel.GetMainViewModel();
                mainViewModel.CategoriesViewModel = this;

                
            
            MainCategoryNode = storeService.CategoryService.GetMainCategory();
            categoryService = storeService.CategoryService;
        }

    }
}
