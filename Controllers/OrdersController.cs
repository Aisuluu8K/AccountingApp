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
        : IRepository<Order>, IUser<Order>

    {
        protected readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }
        public OrdersController()
        {
        }

        public Order Add(Order item)
        {
            DbSet<Order> dbSet = _context.Set<Order>();

            if (dbSet == default(DbSet<Order>))
                return default(Order);


            Order result = dbSet.Add(item).Entity;
            _context.SaveChanges();

            Console.WriteLine("Данные заказа успешно добавлены!");
            Console.WriteLine("Список Заказов");
            Console.WriteLine("     ID     |      Описание      |      Цена     |  Дата регистрации   |   Дата завершения | Id Клиента |");

            foreach (var i in dbSet)
            {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"{i.id} | {i.Description} | {i.OrderPrice} | {i.OrderDate}  | {i.CloseDate}  |  {i.ClientID} ");
            }

            return result;
        }

        public void Delete(Order item)
        {
            DbSet<Order> dbSet = _context.Set<Order>();

            if (dbSet == default(DbSet<Order>))
                return;

            if (item != null)
            {
                string choice = Console.ReadLine();
                InputController.NullException(choice);

                Console.WriteLine("Вы точно хотите удалить данный заказ? Введите (y/n)");
                if (choice.ToLower() == "n" || choice.ToLower() == "N")
                {
                    InputController.Return();
                }
                if (choice.ToLower() == "y" || choice.ToLower() == "Y")
                {
                    dbSet.Remove(item);
                    _context.SaveChanges();
                }
            }
        }

        public void Edit(List<Order> item)
        {
            DbSet<Order> dbSet = _context.Set<Order>();

            DbSet<Client> client = _context.Set<Client>();

            if (dbSet == default(DbSet<Order>))
                return;


            var order = item[0];

            var edit = dbSet.FirstOrDefault(x => x.id == order.id);


            dbSet.Update(edit);

            _context.SaveChanges();

            Console.WriteLine("Данные заказа успешно изменены!");
            Console.WriteLine("Список Заказов");
            Console.WriteLine("     ID     |      Описание      |      Цена     |  Дата регистрации   |   Дата завершения | Id Клиента |");

            foreach (var i in dbSet)
            {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"{i.id} | {i.Description} | {i.OrderPrice} | {i.OrderDate}  | {i.CloseDate}  |  {i.ClientID} ");
            }


        }

        public Order InputForAdd()
        {
            Order order = new Order();

            DbSet<Client> client = _context.Set<Client>();

            Console.Write("Введите id клиента: ");
            uint numberId;
            while (!uint.TryParse(Console.ReadLine(), out numberId))
            {
                Console.WriteLine("Введите Id клиента котороу хотите добавить заказ цифрами ");
            }
            if (client == null)
            {
                Console.WriteLine("Клиент не найден");
                InputController.Return();
            }

            var clientId = client.Where(x => x.id == numberId).FirstOrDefault();
            order.ClientID = clientId.id;

            Console.WriteLine("Введите описание заказа ");
            string description = Console.ReadLine();

            InputController.NullException(description);

            order.Description = description;

            Console.WriteLine("Введите цену заказа");
            float orderPrice = 0;
            while (!float.TryParse(Console.ReadLine(), out orderPrice))
            {
                Console.WriteLine("Введите цену заказа цифрами");
            }
            order.OrderPrice = orderPrice;

            return order;
        }

        public Order InputForDelete()
        {
            Order order = new Order();

            Console.WriteLine("Список Заказов");
            Console.WriteLine("     ID     |      Описание      |      Цена     |  Дата регистрации   |   Дата завершения | Id Клиента |");

            foreach (var i in _context.Orders)
            {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"{i.id} | {i.Description} | {i.OrderPrice} | {i.OrderDate}  | {i.CloseDate}  |  {i.ClientID} ");
            }

            Console.WriteLine("Введите Id заказа");
            int numberId = 0;
            while (!int.TryParse(Console.ReadLine(), out numberId))
            {
                Console.WriteLine("Введите Id заказа цифрами");
            }

            order = _context.Orders.Where(x => x.id == numberId).FirstOrDefault();

            return order;
        }

        public List<Order> InputForEdit()
        {
            Console.WriteLine("Список Заказов");
            Console.WriteLine("     ID     |      Описание      |      Цена     |  Дата регистрации   |   Дата завершения | Id Клиента |");

            foreach (var i in _context.Orders)
            {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"{i.id} | {i.Description} | {i.OrderPrice} | {i.OrderDate}  | {i.CloseDate}  |  {i.ClientID} ");
            }

            List<Order> orderInfo = new();


            Console.Write("Введите id заказа для редактирования: ");
            uint.TryParse(Console.ReadLine(), out uint orderId);

            Console.WriteLine("Какое поле записи вы хотите исправить\n" +
                "1. Описание заказа\n" +
                "2. Цена  заказа\n" +
                "3. Id клиента");

            int.TryParse(Console.ReadLine(), out int field);

            orderInfo = _context.Orders.Where(x => x.id == orderId).ToList();

            foreach (Order order in orderInfo)
            {
                switch (field)
                {
                    case 1:
                        Console.WriteLine("Введите новое описание заказа ");
                        string desription = Console.ReadLine();

                        InputController.NullException(desription);

                        order.Description = desription;
                        break;
                    case 2:
                        Console.WriteLine("Введите новую цену заказа ");
                        float orderPrice;
                        while (!float.TryParse(Console.ReadLine(), out orderPrice))
                        {
                            Console.WriteLine("Введите цену заказа цифрами ");
                        }
                        order.OrderPrice = orderPrice;
                        break;
                    case 3:
                        Console.WriteLine("Введите id клиента ");
                        uint clientId;
                        while (!uint.TryParse(Console.ReadLine(), out clientId))
                        {
                            Console.WriteLine("Введите id клиента цифрами ");
                        }
                        order.ClientID = clientId;
                        break;
                }
            }
            return orderInfo;
        }
    }
}
