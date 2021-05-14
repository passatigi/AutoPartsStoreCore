using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.ViewModel
{
    class AddOemToCarCategoryViewModel : BaseViewModel
    {
        private Category selectedCategory;
        private Category SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                selectedCategory = value;
                NotifyPropertyChanged("InputCategoryString");
            }
        }
        private ObservableCollection<Category> allCategories;
        //public ObservableCollection<Category> AllCategories
        //{

        //}
    }
}
