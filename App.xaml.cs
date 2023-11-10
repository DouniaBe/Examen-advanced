using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_DatabaseConnection;
using static WPF_DatabaseConnection.Data.Initializer;

namespace Examen_advanced
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Roep de database-initializer aan
            using (var context = new MyDbContext())
            {
                DbInitializer.Initialize(context);
            }

            // Maak en toon het hoofdvenster
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
