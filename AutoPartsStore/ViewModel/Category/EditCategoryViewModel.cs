using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace AutoPartsStore.ViewModel
{
    public class EditCategoryViewModel : BaseViewModel
    {
        private Category selectedCategory;
        public Category SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                SetProperty(ref selectedCategory, value);
            }
        }
        private ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                SetProperty(ref categories, value);
            }
        }

        private string searchCategoryString;
        public string SearchCategoryString
        {
            get
            {
                return searchCategoryString;
            }
            set
            {
                SetProperty(ref searchCategoryString, value);
            }
        }
        private string renameCategoryString;
        public string RenameCategoryString
        {
            get
            {
                return renameCategoryString;
            }
            set
            {
                SetProperty(ref renameCategoryString, value);
            }
        }
        private string insertCategoryString;
        public string InsertCategoryString
        {
            get
            {
                return insertCategoryString;
            }
            set
            {
                SetProperty(ref insertCategoryString, value);
            }
        }

        private RelayCommand findStringCommand;
        public RelayCommand FindStringCommand
        {
            get
            {
                return findStringCommand ?? (findStringCommand = new RelayCommand(action =>
                {
                    if (action == categories)
                    {
                        if (SearchCategoryString != null)
                        {
                            Categories.Clear();
                            foreach (Category category in
                            storeService.CategoryService.GetAllCategoriesWithTop().Where(c => c.Name.Contains(SearchCategoryString)))
                            {
                                Categories.Add(category);
                            }
                        }
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
                    if (selectedCategory == null)
                    {
                        WindowProvider.NotifyWindow("В начале выберите категорию");
                    }
                    else
                    {
                        if (action is string)
                        {
                            try
                            {
                                string str = action as string;
                                if (str.Equals("rename"))
                                {
                                    if (renameCategoryString != null && renameCategoryString != "")
                                    {
                                        selectedCategory.Name = renameCategoryString;
                                        storeService.CategoryService.UpdateCategory(selectedCategory);
                                        mainViewModel.CategoriesViewModel.UpdateCategoryList();
                                        FillCategories();
                                    }
                                    else
                                    {
                                        WindowProvider.NotifyWindow("Неправильно заполнено поле");
                                    }
                                }
                                else if (str.Equals("insert"))
                                {
                                    if (insertCategoryString != null && insertCategoryString != "")
                                    {
                                        Category category = storeService.CategoryService.AddCategory(selectedCategory, insertCategoryString);
                                        category.Nodes = new ObservableCollection<Category>();
                                        mainViewModel.CategoriesViewModel.UpdateCategoryList();
                                        FillCategories();
                                    }
                                    else
                                    {
                                        WindowProvider.NotifyWindow("Неправильно заполнено поле");
                                    }

                                }
                                else if (str.Equals("delete"))
                                {
                                if (selectedCategory.Id != 1)
                                {
                                    storeService.CategoryService.DeleteCategoryById(selectedCategory.Id);
                                }
                                else
                                {
                                    WindowProvider.NotifyWindow("Невозможно удалить главную категорию");
                                }
                            }
                        }
                            catch (Exception e)
                        {
                            WindowProvider.NotifyWindow(e.Message);
                        }

                    }
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand updateCategoryListCommand;
        public RelayCommand UpdateCategoryListCommand
        {
            get
            {
                return updateCategoryListCommand ?? (updateCategoryListCommand = new RelayCommand(action =>
                {
                    try
                    {
                        FillCategories();
                    }
                    catch (Exception e)
                    {
                        WindowProvider.NotifyWindow(e.Message);
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }
        int lastSelectedCategoryId;
        private void FillCategories()
        {
            if(selectedCategory != null) { 
                lastSelectedCategoryId = selectedCategory.Id;
            }
            Categories.Clear();
            foreach (Category category in storeService.CategoryService.GetAllCategoriesWithTop())
            {
                Categories.Add(category);
            }
            SelectedCategory = Categories.Where(c => c.Id == lastSelectedCategoryId).FirstOrDefault();
        }

        public void UpdateSelectedCategory()
        {
            SelectedCategory = mainViewModel.UserConfiguration.SelectedCategory;
        }

        IStoreService storeService;
        MainViewModel mainViewModel;
        public EditCategoryViewModel()
        {
            
            mainViewModel = MainViewModel.GetMainViewModel();
            mainViewModel.EditCategoryViewModel = this;
            storeService = mainViewModel.StoreService;

            Categories = new ObservableCollection<Category>();
            selectedCategory = new Category { Id = 0 };
            FillCategories();
            selectedCategory = mainViewModel.UserConfiguration.SelectedCategory;
        }
    }
}
