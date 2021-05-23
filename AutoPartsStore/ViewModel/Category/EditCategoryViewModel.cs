using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    public class EditCategoryViewModel : BaseViewModel
    {
        private Category category;
        public Category Category
        {
            get
            {
                return category;
            }
            set
            {
                SetProperty(ref category, value);
            }
        }


    }
}
