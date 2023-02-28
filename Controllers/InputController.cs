using AccountingApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Data.Controllers
{
    internal class InputController
    {
        public static void NullException(string parametr)
        {
            while (string.IsNullOrEmpty(parametr) || parametr.Trim().Length == 0)
            {
                Console.WriteLine("поле не может быть пустым");
                parametr = Console.ReadLine();
            }
        }


    }
}
