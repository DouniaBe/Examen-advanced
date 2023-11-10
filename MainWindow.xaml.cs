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
        private readonly MyDbContext _context;

       // MyDbContext context = new MyDbContext();

        public MainWindow()
        {
            InitializeComponent();
            _context = new MyDbContext();
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                // Laad de producten in de ListBox
                ProductListBox.ItemsSource = _context.Products.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het laden van producten: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Voeg een nieuw product toe
                var newProduct = new Product { Name = "Nieuw Product", Price = 0.0m };
                _context.Products.Add(newProduct);
                _context.SaveChanges();

                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het toevoegen van een product: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Bewerk het geselecteerde product
                var selectedProduct = (Product)ProductListBox.SelectedItem;
                if (selectedProduct != null)
                {
                    // Voer hier de bewerkingen uit, bijvoorbeeld een nieuw venster openen om het product te bewerken
                    // In dit voorbeeld passen we eenvoudig de naam van het product aan
                    selectedProduct.Name += " - Bewerkt";
                    _context.SaveChanges();

                    LoadProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het bewerken van een product: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verwijder het geselecteerde product
                var selectedProduct = (Product)ProductListBox.SelectedItem;
                if (selectedProduct != null)
                {
                    _context.Products.Remove(selectedProduct);
                    _context.SaveChanges();

                    LoadProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het verwijderen van een product: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

