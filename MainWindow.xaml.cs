using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_DatabaseConnection.Data;
using WPF_DatabaseConnection.Models;
using WPF_DatabaseConnection;
using Microsoft.EntityFrameworkCore;

namespace Examen_advanced
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Categorie> categorieen = null;
        Categorie selectedCategorie = null;
        List<Product> producten = null;
        Product selectedProduct = null;

        MyDbContext context = new MyDbContext();

        public MainWindow()
        {
            Initializer.DbSetInitializer(context);

            InitializeComponent();

            categorieen = context.Categorieen.Where(c => c.Naam != "-").ToList();
            lbCategorieen.ItemsSource = categorieen;
        }

        private void lbCategorieen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearMessage();
            selectedCategorie = categorieen[lbCategorieen.SelectedIndex];
            tbCategorie.Text = selectedCategorie.Naam;

            producten = context.Producten.Where(p => p.CategorieId == selectedCategorie.Id).ToList();
            lbProducten.ItemsSource = producten;
            btAddProduct.Visibility = Visibility.Visible;
        }

        private void tbCategorie_LostFocus(object sender, RoutedEventArgs e)
        {
            ClearMessage();
            try
            {
                Categorie cat = context.Categorieen.First(c => c.Naam == tbCategorie.Text);
            }
            catch
            {
                spOmschrijving.Visibility = Visibility.Visible;
            }
        }

        private void tbOmschrijving_LostFocus(object sender, RoutedEventArgs e)
        {
            ClearMessage();
            if (tbOmschrijving.Text != "")
            {
                spOmschrijving.Visibility = Visibility.Hidden;
                context.Categorieen.Add(new Categorie { Naam = tbCategorie.Text, Omschrijving = tbOmschrijving.Text });
                context.SaveChanges();
                categorieen = context.Categorieen.Where(c => c.Naam != "-").ToList();
                lbCategorieen.ItemsSource = categorieen;
            }
        }

        private void btAddProduct_Click(object sender, RoutedEventArgs e)
        {
            selectedProduct = null;
            spProduct.Visibility = Visibility.Visible;
            ClearMessage();
        }

        private void btBewaarProduct_Click(object sender, RoutedEventArgs e)
        {
            ClearMessage();
            try
            {
                double bedrag = Convert.ToDouble(tbPrijs.Text);

                if (selectedProduct == null)
                {
                    Product product = new Product
                    {
                        Naam = tbProductNaam.Text,
                        Omschrijving = tbProductOmschrijving.Text,
                        Categorie = selectedCategorie
                    };
                    context.Producten.Add(product);
                    context.Prijzen.Add(new Prijs { Bedrag = bedrag, Product = product });
                }
                else
                {
                    selectedProduct.Naam = tbProductNaam.Text;
                    selectedProduct.Omschrijving = tbProductOmschrijving.Text;
                    context.Update(selectedProduct);
                    if (context.Prijzen.OrderBy(prijs => prijs.Vanaf).Last(prijs => prijs.ProductId == selectedProduct.Id).Bedrag != bedrag)
                    {
                        context.Prijzen.Add(new Prijs { Bedrag = bedrag, Product = selectedProduct });
                    }
                }
                context.SaveChanges();
                spProduct.Visibility = Visibility.Hidden;
                producten = context.Producten.Where(p => p.CategorieId == selectedCategorie.Id).ToList();
                lbProducten.ItemsSource = producten;
            }
            catch
            {
                ShowMessage("Er is een probleem met je prijs. Los dat op !!!");
            }
        }

        private void lbProducten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProduct = (Product)lbProducten.SelectedItem;
            spProduct.Visibility = Visibility.Visible;
            tbProductNaam.Text = selectedProduct.Naam;
            tbProductOmschrijving.Text = selectedProduct.Omschrijving;
            tbPrijs.Text = context.Prijzen.OrderBy(prijs => prijs.Vanaf).Last(prijs => prijs.ProductId == selectedProduct.Id).Bedrag.ToString();
            ClearMessage();
        }

        private void ClearMessage()
        {
            tbMessage.Visibility = Visibility.Hidden;
        }

        private void ShowMessage(string message, bool serious = true)
        {
            tbMessage.Text = message;
            tbMessage.Background = serious ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
            tbMessage.Height = 30;
            tbMessage.FontWeight = FontWeights.Bold;
            tbMessage.FontSize = 13;
            tbMessage.VerticalAlignment = VerticalAlignment.Center;
            tbMessage.Visibility = Visibility.Visible;
        }

        private void btShowCase_Click(object sender, RoutedEventArgs e)
        {
            if (lbShowCase.Visibility == Visibility.Visible)
            {
                lbShowCase.Visibility = Visibility.Hidden;
                lbLinq.Visibility = Visibility.Hidden;
            }
            else
            {
                DateTime startTijd = DateTime.Now.AddMonths(-1);
                List<Categorie> categorien = context.Categorieen
                                                    .Where(c => c.Naam != "-")
                                                    .Include(c => c.Producten)
                                                    .ThenInclude(p => p.Prijzen
                                                        .Where(prijs => prijs.Vanaf > startTijd))
                                                    .ToList();
                List<Categorie> categorien2 = categorien.Where(c => c.Producten.Any()).ToList();

                lbShowCase.ItemsSource = categorien2;
                lbShowCase.Visibility = Visibility.Visible;

                var categorieQuery = from categorie in context.Categorieen
                                     from product in categorie.Producten
                                     from prijs in context.Prijzen
                                     where categorie.Naam != "-"
                                       && product.Naam != "-"
                                       && product.CategorieId == categorie.Id
                                       && prijs.ProductId == product.Id
                                       && prijs.Vanaf > startTijd
                                     select new
                                     {
                                         Naam = categorie.Naam,
                                         ProductNaam = product.Naam,
                                         Bedrag = prijs.Bedrag,
                                         Vanaf = prijs.Vanaf
                                     };
                lbLinq.ItemsSource = categorieQuery.ToList();
                lbLinq.Visibility = Visibility.Visible;
            }
        }
    }
}