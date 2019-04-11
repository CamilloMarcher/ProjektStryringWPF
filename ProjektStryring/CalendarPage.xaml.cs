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
using System.Windows.Threading;

namespace ProjektStryring
{
    /// <summary>
    /// Interaction logic for CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        private int userHeight = 0;
        private bool admin;
        private bool student;
        private string uni;
        private DispatcherTimer timer;
        public CalendarPage()
        {
            uni = MainWindow.logic.GetUni();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += Update;
            timer.Start();
            InitializeComponent();

            UserCourses.Visibility = Visibility.Collapsed;
            AdminPlace.Visibility = Visibility.Collapsed;
            if (!MainWindow.logic.IsAdmin(uni) || MainWindow.logic.IsWebAdmin(uni))
            {
                student = true;
                UserCourses.Visibility = Visibility.Visible;
                userHeight = 40;
                BuildStudentSite();
            }
            if (MainWindow.logic.IsAdmin(uni))
            {
                admin = true;
                AdminPlace.Margin = new Thickness(0, userHeight, 0, 0);
                AdminPlace.Visibility = Visibility.Visible;
                BuildAdminSite();
            }
        }

        private void Update(object sender, EventArgs e)
        {
            if (admin)
            {
                BuildAdminSite();
            }
            if (student)
            {
                BuildStudentSite();
            }
        }

        private void BuildStudentSite()
        {
            Models.Person person = MainWindow.logic.GetFullPerson(uni);
            Courses.Children.Clear();
            UpComing.Children.Clear();
            Style style = (Style)Resources["TransparentBTN"];
            #region Course
            Button button = new Button()
            {
                Content = "Hovedforløb \t\t\t +Tilføj",
                Height = 30,
                Margin = new Thickness(0, 0, 0, 0),
                Style = style,
                VerticalAlignment = VerticalAlignment.Top,
            };
            button.Click += AddCourse;
            userHeight = 70;
            Courses.Children.Add(button);
            for (int i = 0; i < person.Courses.Count; i++)
            {
                button = new Button()
                {
                    Content = "Hovedforløb " + person.Courses[i].CourseNumber + "\t\t" + person.Courses[i].StartCourse.ToString("dd-MM-yyyy") + " - " + person.Courses[i].EndCourse.ToString("dd-MM-yyyy"),
                    Tag = i,
                    Height = 30,
                    Margin = new Thickness(0, (userHeight - 40), 0, 0),
                    Style = style,
                    VerticalAlignment = VerticalAlignment.Top,
                };
                button.Click += EditCourse;
                userHeight += 30;
                Courses.Children.Add(button);
            }
            #endregion
            #region UpComing
            int topMargin = 0;
            button = new Button()
            {
                Content = "UpComing \t\t\t\t Type",
                IsEnabled = false,
                Height = 30,
                Margin = new Thickness(0, 0, 0, 0),
                Style = style,
                VerticalAlignment = VerticalAlignment.Top,
            };
            topMargin += 30;
            UpComing.Children.Add(button);
            List<Models.Events> events = MainWindow.logic.GetEvents();
            string tabs = "";
            for (int i = 0; i < events.Count; i++)
            {
                tabs = "";
                if (events[i].Title.Length <= 39)
                {
                    tabs += "\t";
                }
                if (events[i].Title.Length <= 30)
                {
                    tabs += "\t";
                }
                if (events[i].Title.Length <= 21)
                {
                    tabs += "\t";
                }
                if (events[i].Title.Length <= 15)
                {
                    tabs += "\t";
                }
                if (events[i].Title.Length <= 9)
                {
                    tabs += "\t";
                }
                switch (events[i].Colour)
                {
                    case "#DD1515":
                        button = new Button()
                        {
                            Content = events[i].Title + tabs + " Vigtig Event \n" + events[i].Start_Date.ToString("dd/MM/yyyy HH:mm") + " - " + events[i].End_Date.ToString("dd/MM/yyyy HH:mm"),
                            Height = 50,
                            Margin = new Thickness(0, topMargin, 0, 0),
                            Style = style,
                            VerticalAlignment = VerticalAlignment.Top,
                            Tag = events[i],
                        };
                        button.Click += UpComing_Click;
                        UpComing.Children.Add(button);
                        break;
                    case "#AAAAAA":
                        button = new Button()
                        {
                            Content = events[i].Title + tabs + " Ferie \n" + events[i].Start_Date.ToString("dd/MM/yyyy") + " - " + events[i].End_Date.ToString("dd/MM/yyyy"),
                            Height = 50,
                            Margin = new Thickness(0, topMargin, 0, 0),
                            Style = style,
                            VerticalAlignment = VerticalAlignment.Top,
                            Tag = events[i],
                        };
                        button.Click += UpComing_Click;
                        UpComing.Children.Add(button);
                        break;
                    case "#F1C40F":
                        button = new Button()
                        {
                            Content = events[i].Title + tabs + " Event \n" + events[i].Start_Date.ToString("dd/MM/yyyy HH:mm") + " - " + events[i].End_Date.ToString("dd/MM/yyyy HH:mm"),
                            Height = 50,
                            Margin = new Thickness(0, topMargin, 0, 0),
                            Style = style,
                            VerticalAlignment = VerticalAlignment.Top,
                            Tag = events[i],
                        };
                        button.Click += UpComing_Click;
                        UpComing.Children.Add(button);
                        break;

                }
                topMargin += 50;
            }
            #endregion
        }

        private void UpComing_Click(object sender, RoutedEventArgs e)
        {
            EventShow eventShow = new EventShow()
        }

        private void EditCourse(object sender, RoutedEventArgs e)
        {
            AddEditCourse aeCourse = new AddEditCourse(int.Parse(((Button)sender).Tag.ToString()));
            aeCourse.Show();
        }

        private void AddCourse(object sender, RoutedEventArgs e)
        {
            AddEditCourse aeCourse = new AddEditCourse();
            aeCourse.Show();
        }

        private void BuildAdminSite()
        {
            Style style = (Style)Resources["TransparentBTN"];

        }
    }
}
