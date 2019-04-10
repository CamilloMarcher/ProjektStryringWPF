using System;
using System.Windows;
using System.Windows.Threading;
using ProjektStryring.Properties;

namespace ProjektStryring
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private byte trys = 0;
        private DispatcherTimer timer;
        public LoginPage()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            InitializeComponent();
            if (Settings.Default.Username.ToString() != "" && Settings.Default.Password.ToString() != "")
            {
                MainWindow.logic.SaveUser(Settings.Default.Username, Settings.Default.Password);
            }
            if (MainWindow.logic.IsUserLogin())
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                if (Settings.Default.lockedOut.AddMinutes(5) <= DateTime.Now)
                {
                    timer.Start();
                    UNI.IsEnabled = false;
                    Password.IsEnabled = false;
                    Login.IsEnabled = false;
                }
            }
        }


        public LoginPage(bool loggedOut)
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;

            if (loggedOut)
            {
                InitializeComponent();
                if (Settings.Default.lockedOut.AddMinutes(5) <= DateTime.Now)
                {
                    timer.Start();
                    UNI.IsEnabled = false;
                    Password.IsEnabled = false;
                    Login.IsEnabled = false;
                }
            }
            else
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Settings.Default.lockedOut.AddMinutes(5) <= DateTime.Now)
            {
                timer.Stop();
                UNI.IsEnabled = true;
                Password.IsEnabled = true;
                Login.IsEnabled = true;
                trys = 0;
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.logic.Login(UNI.Text, Password.Password))
            {
                if ((bool)KeepLogin.IsChecked)
                {
                    Settings.Default.Username = UNI.Text;
                    Settings.Default.Password = Password.Password;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
                MainWindow main = new MainWindow(UNI.Text, Password.Password);
                main.Show();
                this.Close();
            }
            else
            {
                trys++;
                if (trys >= 3)
                {
                    MessageBox.Show("du er låst ude i 5 minutter");
                    Settings.Default.lockedOut = DateTime.Now;
                    timer.Start();
                    UNI.IsEnabled = false;
                    Password.IsEnabled = false;
                    Login.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("failed to login\n du har " + (3 - trys) + " forsøg tilbage");
                }
            }
        }
    }
}
