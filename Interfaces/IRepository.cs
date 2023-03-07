using AccountingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Data.Repository
{
    public interface IRepository<T>
    {
        public T Add(T item);
        public void Edit(List<T> item);
        public void Delete(T item);

    }
}
