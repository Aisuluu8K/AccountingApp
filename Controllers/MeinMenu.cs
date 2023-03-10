using AccountingApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AccountingApp.Data.Controllers
{
    internal class MeinMenu
    {
        public void GetMenu()
        {
            using (AppDbContext db = new AppDbContext())
            {
                while (true)
                {
                    Console.WriteLine($"Выберите таблицу \n" +
                        $" Введите 1 - если таблица клиентов\n" +
                        $"Введите 2 - если таблица заказов \n" +
                        $"Введите 0 - для выхода");
                    if (int.TryParse(Console.ReadLine(), out int command))
                    {
                        if (command == 0)
                        {
                            InputController.Exit();
                        }
                        if (command == 1)
                        {
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("Клиенты \n" +
                                "введите цифру которая соответствует комманде:\n" +
                                "1 - Добавить\n" +
                                "2 - Редактировать\n" +
                                "3 - Удалить\n" +
                                "4 - Показать заказы клиента\n" +
                                "5 - Вернуться к выбору\n" +
                                "6 - Выйти");

                            ClientsController clientsController = new ClientsController(db);


                            Console.WriteLine("Список клиентов");
                            Console.WriteLine("     ID     |      Имя      |      Фамилия     |     Количество заказов  |     |");

                            foreach (var item in db.Clients)
                            {
                                Console.WriteLine("------------------------------------------------");
                                Console.WriteLine($"{item.id} | {item.FirstName} | {item.SecondName} |{item.OrderAmount}| {item.DateAdd} |");
                            }

                            int methodCod;
                            while (!int.TryParse(Console.ReadLine(), out methodCod))
                            {
                                Console.WriteLine("Введите цифру");
                            }


                            switch (methodCod)
                            {
                                case 1:
                                    clientsController.Add(clientsController.InputForAdd());
                                    break;
                                case 2:
                                    clientsController.Edit(clientsController.InputForEdit());
                                    break;
                                case 3:
                                    clientsController.Delete(clientsController.InputForDelete());
                                    break;
                                case 4:
                                    clientsController.ShowClientsOrders(clientsController.ClientIdInput());
                                    break;
                                case 5:
                                    InputController.Return();
                                    break;
                                case 6:
                                    InputController.Exit();
                                    break;

                            }
                            Console.WriteLine("Выйти в главное меню? (Y|N)");
                            string getmenu = Console.ReadLine();
                            if (getmenu.ToLower() == "y" || getmenu.ToLower() == "Y")
                            {
                                InputController.Return();
                            }
                            if (getmenu.ToLower() == "n" || getmenu.ToLower() == "N")
                            {
                                InputController.Exit();

                            }
                        }
                        if (command == 2)
                        {
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("Клиенты \n" +
                                "введите цифру которая соответствует комманде:\n" +
                                "1 - Добавить\n" +
                                "2 - Редактировать\n" +
                                "3 - Удалить\n" +
                                "4 - Вернуться к выбору\n" +
                                "5 - Выйти");

                            OrdersController ordersController = new OrdersController(db);

                            Console.WriteLine("Список Заказов");
                            Console.WriteLine("     ID     |      Описание      |      Цена     |  Дата регистрации   |   Дата завершения | Клиент |");

                            foreach (var i in db.Orders)
                            {
                                Console.WriteLine("------------------------------------------------");
                                Console.WriteLine($"{i.id} | {i.Description} | {i.OrderPrice} | {i.OrderDate}  | {i.CloseDate}  |  ");
                            }


                            int methodCod;
                            while (!int.TryParse(Console.ReadLine(), out methodCod))
                            {
                                Console.WriteLine("Введите цифру");
                            }


                            switch (methodCod)
                            {
                                case 1:
                                    ordersController.Add(ordersController.InputForAdd());
                                    break;
                                case 2:
                                    ordersController.Edit(ordersController.InputForEdit());
                                    break;
                                case 3:
                                    ordersController.Delete(ordersController.InputForDelete());
                                    break;
                                case 4:
                                    InputController.Return();
                                    break;
                                case 5:
                                    InputController.Exit();
                                    break;

                            }

                            Console.WriteLine("Выйти в главное меню? (Y|N)");
                            string getmenu = Console.ReadLine();
                            if (getmenu.ToLower() == "y" || getmenu.ToLower() == "Y")
                            {
                                InputController.Return();
                            }
                            if (getmenu.ToLower() == "n" || getmenu.ToLower() == "N")
                            {
                                InputController.Exit();

                            }
                        }

                       
                    }
                }
            }

        }

    }
}