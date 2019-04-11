using ProjektStryring.Properties;
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

namespace ProjektStryring
{
    public partial class MainWindow : Window
    {
        public readonly static Logic.Logic logic = new Logic.Logic();
        public MainWindow()
        {
            if (Settings.Default.Username.ToString() != "" && Settings.Default.Password.ToString() != "")
            {
                MainWindow.logic.SaveUser(Settings.Default.Username, Settings.Default.Password);
            }
            InitializeComponent();
            Models.Layout layout = logic.GetLayoutInfo(Settings.Default.Username);
            List<Models.Group> groups = layout.GroupList;

            Name.Content = layout.Name;
            if (logic.IsAdmin(logic.GetUni()) && !logic.IsWebAdmin(logic.GetUni()))
            {
                Main.Content = new PersonsPage();
            }
            else
            {
                Main.Content = new HomePage();
            }
        }
        public MainWindow(string uni, string password)
        {
            if (Settings.Default.Username.ToString() != "" && Settings.Default.Password.ToString() != "")
            {
                MainWindow.logic.SaveUser(Settings.Default.Username, Settings.Default.Password);
            }
            else
            {
                MainWindow.logic.SaveUser(uni, password);
            }
            InitializeComponent();
            Models.Layout layout = logic.GetLayoutInfo(uni);
            List<Models.Group> groups = layout.GroupList;
            Name.Content = layout.Name;
            if (logic.IsAdmin(uni) && !logic.IsWebAdmin(uni))
            {
                Main.Content = new PersonsPage();
            }
            else
            {
                Main.Content = new HomePage();
            }
        }

        private void btnHome (object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage();
        }

        private void btnProject (object sender, RoutedEventArgs e)
        {
            Main.Content = new ProjectsPage();
        }

        private void btnCalender (object sender, RoutedEventArgs e)
        {
            Main.Content = new CalendarPage();
        }

        private void btnPersons (object sender, RoutedEventArgs e)
        {
            Main.Content = new PersonsPage();
        }
        private void btnName(object sender, RoutedEventArgs e)
        {
            bool logout = false;
            MessageBoxResult result = MessageBox.Show("Vil du logge af\nklik \"OK\" for at logge af", "Logger af", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    logout = true;
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
            if (logout)
            {
                Settings.Default.Username = "";
                Settings.Default.Password = "";
                Settings.Default.Save();
                Settings.Default.Reload();
                logic.SaveUser("", "");
                LoginPage lp = new LoginPage(true);
                lp.Show();
                Close();
            }
        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }

        public void NewPage(Page newPage)
        {
            Main.Content = newPage;
        }
    }
}
