using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Data.Interfaces
{
    internal interface IUser<T>
    {
        public T InputForAdd();
        public T InputForDelete();
        public List<T> InputForEdit();
    }
}
