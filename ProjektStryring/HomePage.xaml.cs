using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProjektStryring
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private string uni = "";
        public HomePage()
        {
            InitializeComponent();
            StartUpSide(MainWindow.logic.GetUni());
        }


        public HomePage(string UNI)
        {
            InitializeComponent();
            StartUpSide(UNI);
        }
        public void StartUpSide(string uni)
        {
            Models.Person person = MainWindow.logic.GetFullPerson(uni);
            this.Name.Text = person.Name;
            this.Study.Text = person.Studing;
            this.UNI.Text = person.UNI;
            this.uni = person.UNI;
            for (int i = 0; i < person.Courses.Count; i++)
            {
                StudyingAt.Items.Add(new ListBoxItem() { IsEnabled = false, Content = "\bHovedforløb " + person.Courses[i].CourseNumber + "\t" + person.Courses[i].StartCourse.ToString("dd/MM/yyyy") + " - " + person.Courses[i].EndCourse.ToString("dd/MM/yyyy") });
            }
            Style style = (Style)Resources["TransparentBTN"];
            for (int i = 1; i <= person.Groups.Count; i++)
            {
                Button button = new Button()
                {
                    Margin = new Thickness(0, 50 * i, 0, 0),
                    Content = person.Groups[i - 1].Name,
                    Width = 280,
                    Height = 50,
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 100, 100, 100)),
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Style = style,
                    Tag = person.Groups[i - 1].Id
                };
                button.Click += Button_Click;
                this.Groups.Children.Add(button);
            }
            if (MainWindow.logic.IsAdmin(MainWindow.logic.GetUni()))
            {
                if (MainWindow.logic.IsWebAdmin(uni))
                {
                    WebAdminSw.Content = "Gør til normal bruger";
                }
                else
                {
                    WebAdminSw.Content = "Gør til WebAdminstrator";

                }
            }
            else
            {
                Remove.Visibility = Visibility.Hidden;
                WebAdminSw.Visibility = Visibility.Hidden;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ProjectPage(int.Parse(((Button)sender).Tag.ToString())));
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.logic.RemoveUser(uni);
            this.NavigationService.Navigate(new HomePage());
        }

        private void WebAdminSw_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.logic.IsWebAdmin(uni))
            {
                MainWindow.logic.UpdateWebAdmin(uni, false);
            }
            else
            {
                MainWindow.logic.UpdateWebAdmin(uni, true);
            }
            this.NavigationService.Navigate(new HomePage(uni));
        }
    }
}
