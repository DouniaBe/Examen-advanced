using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_DatabaseConnection.Models;

namespace WPF_DatabaseConnection.Data
{
    internal static class Initializer
    {
        public static class DbInitializer
        {
            public static void Initialize( MyDbContext context)
            {
                context.Database.EnsureCreated();

                // Controleer of er al gegevens in de database staan

                // Controleer of er al gegevens in de database staan
                if (context.Products.Any() || context.Orders.Any() || context.Users.Any())
                {
                    return; // De database is al geïnitialiseerd
                }

                // Voeg drie voorbeeldproducten toe met beschrijvingen
                var product1 = new Product { Name = "Logo-ontwerp", Price = 199.99m, Description = "Professioneel ontworpen logo voor je bedrijf." };
                var product2 = new Product { Name = "Flyer-ontwerp", Price = 149.99m, Description = "Aantrekkelijke flyer ontworpen om de aandacht van je doelgroep te trekken." };
                var product3 = new Product { Name = "Visitekaartjes", Price = 79.99m, Description = "Unieke visitekaartjes die een blijvende indruk achterlaten." };

                context.Products.AddRange(product1, product2, product3);

                // Voeg een voorbeeldorder toe voor elk product
                var order1 = new Order { ProductId = product1.Id, Quantity = 2 };
                var order2 = new Order { ProductId = product2.Id, Quantity = 1 };
                var order3 = new Order { ProductId = product3.Id, Quantity = 3 };

                context.Orders.AddRange(order1, order2, order3);

                // Voeg een voorbeeldgebruiker toe
                var user = new User { UserName = "VoorbeeldGebruiker", Password = "Wachtwoord123" };
                context.Users.Add(user);

                context.SaveChanges();
            }
        }
    }
}
