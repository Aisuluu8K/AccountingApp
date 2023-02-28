using AccountingApp.Data;
using AccountingApp.Data.Controllers;

namespace AccountingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Seed();

                MeinMenu menu= new MeinMenu();

                menu.GetMenu();

            }

        }

    }
}