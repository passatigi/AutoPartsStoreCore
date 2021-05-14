using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.BusinessLogicLayer.Interfaces
{
    public interface ICategoryService
    {
        Category GetMainCategory();

        Category AddCategory(Category parentCategory, string name);

        IEnumerable<Category> GetAllCategories();
        int GetParentId(int id);
        Category GetCategoryById(int id);
        void UpdateCategory(Category category);
        void DeleteCategoryById(int id);
    }
}
