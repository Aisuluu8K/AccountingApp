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
    public class ClientsController : IClient, IRepository<Client>, IUser<Client>
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

            if (item != null)
            {
                var orders = CountOrders(item.id);
                if (orders != 0)
                {
                    Console.WriteLine("У клиента есть заказы. В случае удаления клиента, все его заказы будут удалены.\n" +
                        " Вы уверены что хотите удалить клиента? (Y|N)");
                    string choice = Console.ReadLine();
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



        }

        public void Edit(List<Client> item)
        {
            DbSet<Client> dbSet = _context.Set<Client>();

            if (dbSet == default(DbSet<Client>))
                return;
            var client = item[0];

            var edit = dbSet.FirstOrDefault(x => x.id == client.id);

            dbSet.Update(edit);

            _context.SaveChanges();

            Console.WriteLine("Данные пользователя успешно изменены!");
            Console.WriteLine("Список клиентов");
            Console.WriteLine("     ID     |      Имя      |      Фамилия     |");

            foreach (var i in dbSet)
            {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"{i.id} | {i.FirstName} | {i.SecondName}");
            }
        }

        public uint CountOrders(uint clientId)
        {
            List<Order> list = new List<Order>();

            if (_context.Orders != null || _context.Clients != null)
            {
                list = _context.Orders.Where(x => x.ClientID == clientId).ToList();
            }


            uint count = (uint)list.Count();

            return count;
        }

        public List<Order> ShowClientsOrders(uint clientId)
        {
            DbSet<Order> orders = _context.Set<Order>();

            DbSet<Client> client = _context.Set<Client>();

            List<Order> list = orders.Where(x => x.ClientID == clientId).ToList();

            var clientName = client.Where(a => a.id == clientId).FirstOrDefault();

            if (CountOrders(clientName.id) == null)
            {
                Console.WriteLine("Клиент не найден");
                return null;
            }
            else
            {
                Console.WriteLine($"Заказчик: {clientName.SecondName} {clientName.FirstName}");
                foreach(var i in list)
                {
                    Console.WriteLine($"Номер заказа {i.id} Описание заказа: {i.Description} Цена: {i.OrderPrice}");
                }
            }



            return list;
        }

        public Client InputForAdd()
        {
            Client client = new Client();

            Console.Write("Введите ваше имя: ");
            string firstName = Console.ReadLine();

            InputController.NullException(firstName);

            client.FirstName = firstName;

            Console.Write("Введите вашу фамилию: ");
            string secondName = Console.ReadLine();

            InputController.NullException(secondName);

            client.SecondName = secondName;

            Console.Write("Введите ваш номер телефона: ");
            string phoneNum = Console.ReadLine();

            InputController.NullException(phoneNum);

            client.PhoneNum = phoneNum;

            client.OrderAmount = 0;
            client.ClientOrders = ShowClientsOrders(client.id);

            return client;
        }

        public Client InputForDelete()
        {
            Client client = new Client();

            Console.WriteLine("Список клиентов ");

            foreach (var i in _context.Clients)
            {
                Console.WriteLine("     ID     |      Имя      |      Фамилия     |");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($" {i.id}    | {i.FirstName} | {i.SecondName}");
            }

            Console.WriteLine("Введите Id клиента заказы которого хотите посмотреть ");
            uint numberId;
            while (!uint.TryParse(Console.ReadLine(), out numberId))
            {
                Console.WriteLine("Введите Id клиента заказы которого хотите посмотреть цифрами ");
            }
            client = _context.Clients.Where(x => x.id == numberId).FirstOrDefault();

            return client;
        }

        public List<Client> InputForEdit()
        {
            Console.WriteLine("Список клиентов");

            Console.WriteLine("     ID     |      Имя      |      Фамилия     |");

            foreach (var i in _context.Clients)
            {
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

            foreach (Client client in clientInfo)
            {
                switch (field)
                {
                    case 1:
                        Console.WriteLine("Введите новое имя клиента ");
                        string name = Console.ReadLine();

                        InputController.NullException(name);

                        client.FirstName = name;
                        break;
                    case 2:
                        Console.WriteLine("Введите новую фамилию клиента ");
                        string secondName = Console.ReadLine();

                        InputController.NullException(secondName);

                        client.SecondName = secondName;
                        break;
                    case 3:
                        Console.WriteLine("Введите новый телефон клиента ");
                        string phone = Console.ReadLine();

                        InputController.NullException(phone);

                        client.PhoneNum = phone;
                        break;
                }

                uint orderAmount = CountOrders(clientId);

                client.OrderAmount = orderAmount;
            }

            return clientInfo;
        }

        public uint ClientIdInput()
        {
            Console.WriteLine("Введите Id клиента заказы которого хотите посмотреть");

            uint numberId = 0;
            while (!uint.TryParse(Console.ReadLine(), out numberId))
            {
                Console.WriteLine("Введите Id клиента заказы которого хотите посмотреть цифрами");
            }

            return numberId;
        }
    }
}
