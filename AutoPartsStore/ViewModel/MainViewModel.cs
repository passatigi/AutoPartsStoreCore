using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.ViewModel
{
    class MainViewModel
    {
        private static MainViewModel mainViewModel;
        private static object syncRoot = new Object();

        public CategoriesViewModel CategoriesViewModel { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
        public AddEntityPanelViewModel AddEntityPanelViewModel { get; set; }


        public NewCarViewModel NewCarViewModel { get; set; }

        public ChooseCarViewModel ChooseCarViewModel { get; set; }

        public AddOemToCarCategoryViewModel AddOemToCarCategoryViewModel { get; set; }


        public static MainViewModel GetMainViewModel()
        {
            if (mainViewModel == null)
            {
                lock (syncRoot)
                {
                    if (mainViewModel == null)
                        mainViewModel = new MainViewModel();
                }
            }
            return mainViewModel;
        }

        public void AddCarPartOemNumberIntoCategory(int categoryId)
        {

        }
    }
}
