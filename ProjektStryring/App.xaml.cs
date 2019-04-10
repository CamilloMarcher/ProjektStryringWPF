using ProjektStryring.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektStryring
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Logic.Logic logic = new Logic.Logic();
            if (Settings.Default.Username.ToString() != "" && Settings.Default.Password.ToString() != "")
            {
                logic.SaveUser(Settings.Default.Username, Settings.Default.Password);
            }
            if (!logic.IsUserLogin())
            {
                LoginPage lp = new LoginPage();
                lp.Show();
            }
            else
            {
                MainWindow window = new MainWindow();
                window.Show();
            }
        }
    }
}
