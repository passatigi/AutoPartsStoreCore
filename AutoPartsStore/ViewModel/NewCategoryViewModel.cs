using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    class NewCategoryViewModel
    {
        public CategoriesViewModel CategoriesViewModel { get; private set; }
        public NewCategoryViewModel()
        {
            CategoriesViewModel = MainViewModel.GetMainViewModel().CategoriesViewModel;
        }
    }
}
