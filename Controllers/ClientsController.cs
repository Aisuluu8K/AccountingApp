using AccountingApp.Data;
using AccountingApp.Data.Interfaces;
using AccountingApp.Data.Models;
using AccountingApp.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace AccountingApp.Data.Controllers
{
    internal class ClientsController : IClient, IRepository<Client>, IUser<Client>
    {
        protected readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }
        public ClientsController()
        {
        }

        public Client Add(Client item)
        {
            DbSet<Client> dbSet = _context.Set<Client>();

            if (dbSet == default(DbSet<Client>))
                return default(Client);

            Client result = dbSet.Add(item).Entity;
            _context.SaveChanges();

            return result;
        }

        public void Delete(Client item)
        {
            DbSet<Client> dbSet = _context.Set<Client>();

            if (dbSet == default(DbSet<Client>))
                return;

            dbSet.Remove(item);
            _context.SaveChanges();
        }

        public void Edit(Client item)
        {
            DbSet<Client> dbSet = _context.Set<Client>();

            if (dbSet == default(DbSet<Client>))
                return;

            dbSet.Update(item);

            _context.SaveChanges();

            Console.WriteLine("Данные пользователя успешно изменены!");
            Console.WriteLine("Список клиентов");

            foreach (var i in dbSet)
            {
                Console.WriteLine("     ID     |      Имя      |      Фамилия     |");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"{i.id} | {i.FirstName} | {i.SecondName}");
            }
        }

        public uint CountOrders(uint clientId)
        {
            DbSet<Order> orders = _context.Set<Order>();

            List<Order> list = orders.Where(x => x.ClientID == clientId).ToList();

            uint count = (uint)list.Count();

            return count;
        }

        public List<Order> ShowClientsOrders(uint clientId)
        {
            DbSet<Order> orders = _context.Set<Order>();

            //if (orders == default(DbSet<Order>))
            //    return default(Order);

            DbSet<Client> client = _context.Set<Client>();

            //if (client == default(DbSet<Client>))
            //    return default();


            List<Order> list = orders.Where(x => x.ClientID == clientId).ToList();

            var clientName = client.Where(a => a.id == clientId).Select(p => p.SecondName);

            Console.WriteLine($"Фимилия заказчика: {clientName}\n");

            return list;
        }

        public Client InputForAdd()
        {
            Client client = new Client();

            Console.Write("Введите ваше имя");
            string firstName = Console.ReadLine();

            InputController.NullException(firstName);

            client.FirstName = firstName;

            Console.Write("Введите вашу фамилию");
            string secondName = Console.ReadLine();

            InputController.NullException(secondName);

            client.SecondName = secondName;

            Console.Write("Введите ваш номер телефона");
            string phoneNum = Console.ReadLine();

            InputController.NullException(phoneNum);

            client.PhoneNum = phoneNum;

            client.OrderAmount = CountOrders(client.id);

            return client;
        }

        public Client InputForDelete()
        {
            Client client = new Client();

            Console.WriteLine("Список клиентов");

            foreach (var i in _context.Clients)
            {
                Console.WriteLine("     ID     |      Имя      |      Фамилия     |");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"{i.id} | {i.FirstName} | {i.SecondName}");
            }

            Console.WriteLine("Введите Id клиента заказы которого хотите посмотреть");
            
            while (!uint.TryParse(Console.ReadLine(), out uint numberId))
            {
                Console.WriteLine("Введите Id клиента заказы которого хотите посмотреть цифрами");
            }





            return client;
        }

        public Client InputForEdit()
        {
            Console.WriteLine("Список клиентов");

            foreach (var i in _context.Clients)
            {
                Console.WriteLine("     ID     |      Имя      |      Фамилия     |");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"{i.id} | {i.FirstName} | {i.SecondName}");
            }

            List<Client> clientInfo = new();

            Console.Write("Введите id клиента для редактирования: ");
            uint.TryParse(Console.ReadLine(), out uint clientId);

            Console.WriteLine("Какое поле записи вы хотите исправить\n" +
                "1. Имя клиента\n" +
                "2. Фамилия клиента\n" +
                "3.Телефон клиента");
            int.TryParse(Console.ReadLine(), out int field);
            clientInfo = _context.Clients.Where(x => x.id == clientId).ToList();

            
            switch (field)
            {
                case 1:
                    Console.WriteLine("Введите новое имя клиента ");
                    string name = Console.ReadLine();

                    InputController.NullException(name);

                    clientInfo.FirstName = name;
                    break;
                case 2:
                    Console.WriteLine("Введите новую фамилию клиента ");
                    string secondName = Console.ReadLine();

                    InputController.NullException(secondName);

                    clientInfo.SecondName = secondName;
                    break;
                case 3:
                    Console.WriteLine("Введите новый телефон клиента ");
                    string phone = Console.ReadLine();

                    InputController.NullException(phone);

                    clientInfo.PhoneNum = phone;
                    break;
            }

            uint orderAmount = 0;

            clientInfo.OrderAmount = orderAmount;

            return clientInfo;
        }
    }
}
