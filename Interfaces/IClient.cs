using AccountingApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Data.Interfaces
{
    internal interface IClient
    {
        public List<Order> ShowClientsOrders(uint clientId);
        public uint CountOrders(uint clientId);

    }
}
