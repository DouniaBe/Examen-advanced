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
        internal static void DbSetInitializer(MyDbContext context)
        {

            if (!context.Categorieen.Any())
            {
                context.Add(new Categorie { Naam = "-", Omschrijving = "-" });
                context.Add(new Categorie { Naam = "Kaarsen", Omschrijving = "Verschillende soorten kaarsen" });
                context.Add(new Categorie { Naam = "Honingpot", Omschrijving = "Diverse soorten honingpotten" });
                context.Add(new Categorie { Naam = "Gepersonaliseerde Chocolade", Omschrijving = "Chocolade met aangepaste berichten" });
                context.SaveChanges();
            }

            Categorie dummyCategorie = context.Categorieen.First(c => c.Naam == "-");
            if (!context.Producten.Any())
            {
                context.Add(new Product { Naam = "-", Omschrijving = "-", Categorie = dummyCategorie });
            }

          

            if (!context.Prijzen.Any())
            {
      
                foreach (Product product in context.Producten)
                    context.Add(new Prijs { Bedrag = 0, Product = product });
            }

            context.SaveChanges();
        }
    }
}
