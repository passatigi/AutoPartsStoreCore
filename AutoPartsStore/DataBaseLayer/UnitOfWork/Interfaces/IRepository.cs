using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWithCondition(object condition);
        T GetById(int id);
        T GetAs(T item);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
