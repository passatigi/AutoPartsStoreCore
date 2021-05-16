using AutoPartsStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.Model
{
    public class Category : BaseViewModel
    {

        private Category parentCategory;

        private int id;
        private int categoryLevel;
        private string name;
        private ObservableCollection<Category> nodes;

        public Category()
        {
            nodes = new ObservableCollection<Category>();
        }
        public Category(Category parentCategory, string category)
        {
            this.parentCategory = parentCategory;
            this.name = category;
            if(parentCategory != null)
            {
                categoryLevel = parentCategory.CategoryLevel + 1;
            }
            
        }
        public override string ToString()
        {
            return $"{Name} level: {categoryLevel}";
        }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                NotifyPropertyChanged(nameof(Id));

            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        public  ObservableCollection<Category> Nodes
        {
            get
            {
                return nodes;
            }
            set
            {
                nodes = value;
                NotifyPropertyChanged(nameof(Nodes));
            }
        }
        public int CategoryLevel {
            get
            {
                return categoryLevel;
            }
            set
            {
                categoryLevel = value;
                NotifyPropertyChanged(nameof(CategoryLevel));
            }

        }
        public Category ParentCategory 
        {
            get
            {
                return parentCategory;
            }
            set
            {
                parentCategory = value;
                NotifyPropertyChanged(nameof(ParentCategory));
            }
        }

        //public Category GetNodeById(string id)
        //{
        //    return nodes.Where(n => n.categoryId.Equals(id)).First();
        //}

    }
}
