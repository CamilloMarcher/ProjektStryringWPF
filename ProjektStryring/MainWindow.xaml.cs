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
            InitializeComponent();
            Models.Layout layout = logic.GetLayoutInfo(Settings.Default.Username);
            List<Models.Group> groups = layout.GroupList;
            Button button = new Button();
            for (int i = 0; i < groups.Count; ++i)
            {
                button = new Button()
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    Content = groups[i].Name,
                    Margin = new Thickness(0, i * 30.0, 0, 0),
                    Height = 30,
                    Style = RemoveHoverPressed(),
                    Background = null,
                    Foreground = new SolidColorBrush(Color.FromRgb(148, 157, 154)),
                    Tag = groups[i].Id
                };
                button.Click += new RoutedEventHandler(Button_Click);
                this.SideBar.Children.Add(button);
            }


            

            button = new Button()
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                Content = layout.Name,
                Height = 30,
                BorderThickness = new Thickness(0),
                Style = RemoveHoverPressed(),
                Background = null,
                Foreground = new SolidColorBrush(Color.FromRgb(148, 157, 154))
            };
            button.Click += new RoutedEventHandler(Button_Click);
            this.User.Children.Add(button);
        }

        private void btnHome (object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage();
        }

        private void btnProject (object sender, RoutedEventArgs e)
        {
            Main.Content = new ProjectPage();
        }

        private void btnCalender (object sender, RoutedEventArgs e)
        {
            Main.Content = new CalendarPage();
        }

        private void btnPersons (object sender, RoutedEventArgs e)
        {
            Main.Content = new PersonsPage();
        }

        private Style RemoveHoverPressed()
        {
            Style style = new Style(typeof(Button));

            Trigger t = new Trigger
            {
                Property = Button.IsMouseOverProperty,
                Value = true
            };
            Setter setter = new Setter
            {
                Property = Button.BackgroundProperty,
                Value = Brushes.DarkGoldenrod
            };
            t.Setters.Add(setter);
            style.Triggers.Add(t); // Important: add a trigger before applying style
            return style;
        }
        public void Button_Click(object obj, EventArgs e)
        {

        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
