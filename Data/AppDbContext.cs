using AccountingApp.Data.Controllers;
using AccountingApp.Data.Interfaces;
using AccountingApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Data
{
    internal class AppDbContext : DbContext
    {
        ClientsController controller = new ClientsController();
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string? connectionString = config
               .GetConnectionString("ConnectionString");

            optionsBuilder
                .UseSqlServer(connectionString);
        }


        public void Seed()
        {
            if (Orders.Any() || Clients.Any())
                return;

            Orders.Add(new Order
            {
                ClientID = 1,
                Description = "описание",
                OrderPrice = 200
            });

            Orders.Add(new Order
            {
                ClientID = 1,
                Description = "описание",
                OrderPrice = 200
            });

            Clients.Add(new Client
            {
                FirstName = "test",
                SecondName = "Test",
                PhoneNum = "0500255255",
                OrderAmount = 2
            });

            SaveChanges();


        }
    }
}
