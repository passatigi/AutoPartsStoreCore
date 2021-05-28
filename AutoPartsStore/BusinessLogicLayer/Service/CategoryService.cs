
using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.BusinessLogicLayer.Service
{
    public class CategoryService
    {
        IUnitOfWork unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }



        public Category AddCategory(Category parentCategory, string name)
        {
            Category category = new Category(
                parentCategory,
                name
                );
            unitOfWork.CategoryRepository.Add(category);
            unitOfWork.Save();
            return category;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return unitOfWork.CategoryRepository.GetAll().Where(c => c.Id != 1);
        }
        public IEnumerable<Category> GetAllCategoriesWithTop()
        {
            return unitOfWork.CategoryRepository.GetAll();
        }
        public void DeleteCategoryById(int id)
        {
            unitOfWork.CategoryRepository.Delete(id);
            unitOfWork.Save();
        }

        public Category GetCategoryById(int id)
        {
            return unitOfWork.CategoryRepository.GetById(id);
        }

        public Category GetMainCategory()
        {
            List<Category> list = unitOfWork.CategoryRepository.GetAll().ToList();

            return list.Where(c => c.Id == 1).FirstOrDefault();
        }

        public int GetParentId(int id)
        {
            return unitOfWork.CategoryRepository.GetById(id).ParentCategory.Id;
        }

        public void UpdateCategory(Category category)
        {
            unitOfWork.CategoryRepository.Update(category);
            unitOfWork.Save();
        }
    }
}
