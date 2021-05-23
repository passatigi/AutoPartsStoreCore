using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces
{
    public interface IRepository<T, IDT> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWithCondition(object condition);
        T GetById(IDT id);
        IEnumerable<T> GetAs(T item);
        void Add(T item);
        void Update(T item);
        void Delete(IDT id);
    }
}
