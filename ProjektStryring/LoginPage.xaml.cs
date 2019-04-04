using System.Windows;
using ProjektStryring.Properties;

namespace ProjektStryring
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private MainWindow main = new MainWindow();
        public LoginPage()
        {
            InitializeComponent();
            if (Settings.Default.Username != "" && Settings.Default.Password != "")
            {
                MainWindow.logic.SaveUser(Settings.Default.Username, Settings.Default.Password);
            }
            if (MainWindow.logic.IsUserLogin())
            {
                main.Show();
                this.Close();
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
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("failed to login");
            }


        }
    }
}
