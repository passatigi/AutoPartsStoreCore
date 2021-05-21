using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        AutoPartsStoreContext db;

        public CategoryRepository(AutoPartsStoreContext db)
        {
            this.db = db;
        }

        public void Add(Category item)
        {
            db.Categories.Add(item);
        }

        public void Delete(int id)
        {
            Category category = GetById(id);
            if (category == null)
            {
                throw new Exception("Category is null");
            }
            db.Categories.Remove(category);
            db.Categories.RemoveRange(db.Categories.Where(c => c.ParentCategory == null && c.Id != 1));
        }

        public IEnumerable<Category> GetAll()
        {
            //.AsNoTracking()
            return db.Categories.Include(c => c.ParentCategory);
        }

        public IEnumerable<Category> GetAllWithCondition(object condition)
        {
            throw new NotImplementedException();
        }

        public Category GetAs(Category item)
        {
            throw new NotImplementedException();
        }

        public Category GetById(int id)
        {
            return db.Categories.Where(c => c.Id == id).Include(c => c.Nodes).FirstOrDefault();
        }

        public void Update(Category item)
        {
            Category category = db.Categories.Where(c => c.Id == item.Id).FirstOrDefault();
            if (category is null)
            {
                throw new Exception("Category is null");
            }
            category.Name = item.Name;
            db.Categories.Update(category);
            //db.SaveChanges();
        }
    }
}
