using AccountingApp.Data.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Data.Models
{
    internal class Client
    {
        public uint id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PhoneNum { get; set; }
        public uint OrderAmount { get; set; }
        public DateTime DateAdd => DateTime.Now;
        public List<Order> ClientOrders { get; set; }
    }
}
