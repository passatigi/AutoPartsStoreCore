using AutoPartsStore.View.Manufacturer;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    public static class WindowProvider
    {
        public static void OpenAddOemToVehicleCategoryWindow()
        {
            //NewCategoryWindow newCategoryWindow = new NewCategoryWindow();
            //if (newCategory == null)
            //{
            //    newCategory = new Category();
            //}
            //newCategory.ParentCategory = categoryService.GetCategoryById(parentId);
            //ClearNewCategory();
            //newCategoryWindow.Show();
        }
        public static void OpenAddManufacturerWindow()
        {
            AddManufacturerWindow addManufacturerWindow = new AddManufacturerWindow();
            addManufacturerWindow.Show();
        }
    }
}
