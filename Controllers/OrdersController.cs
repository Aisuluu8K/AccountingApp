using AccountingApp.Data.Interfaces;
using AccountingApp.Data.Models;
using AccountingApp.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AccountingApp.Data.Controllers
{
    internal class OrdersController 
        //:  IRepository<Order>, IUser<Order>

    {
        protected readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        //public Order Add(Order item)
        //{
        //    DbSet<Order> dbSet = _context.Set<Order>();

        //    if (dbSet == default(DbSet<Order>))
        //        return default(Order);

        //    Order result = dbSet.Add(item).Entity;
        //    _context.SaveChanges();

        //    return result;
        //}

        //public void Delete(Order item)
        //{
        //    DbSet<Order> dbSet = _context.Set<Order>();

        //    if (dbSet == default(DbSet<Order>))
        //        return;

        //    dbSet.Remove(item);
        //    _context.SaveChanges();
        //}

        //public void Edit(Order item)
        //{
        //    DbSet<Order> dbSet = _context.Set<Order>();

        //    if (dbSet == default(DbSet<Order>))
        //        return;

        //    dbSet.Update(item);

        //    _context.SaveChanges();
        //}

       
        //public Order InputForAdd()
        //{
        //    Order client = new Order();

        //    Console.Write("Введите ваше имя");
        //    string firstName = Console.ReadLine();

        //    InputController.NullException(firstName);

        //    client.FirstName = firstName;

        //    Console.Write("Введите вашу фамилию");
        //    string secondName = Console.ReadLine();

        //    InputController.NullException(secondName);

        //    client.SecondName = secondName;

        //    Console.Write("Введите ваш номер телефона");
        //    string phoneNum = Console.ReadLine();

        //    InputController.NullException(phoneNum);

        //    client.PhoneNum = phoneNum;

        //    client.OrderAmount = coCountOrders(client.id);

        //    return client;
        //}

        //public Order InputForDelete()
        //{
        //    Order order = new Order();



        //    return order;
        //}

        //public Order InputForEdit()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
