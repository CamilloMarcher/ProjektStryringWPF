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
    /// Interaction logic for PersonsPage.xaml
    /// </summary>
    public partial class PersonsPage : Page
    {
        private bool name;
        private bool uni;
        private bool studing;
        private bool startDate;
        private bool endDate;
        private bool nameasc;
        private bool uniasc;
        private bool studingasc;
        private bool startDateasc;
        private bool endDateasc;
        private List<Models.Person> people;
        private string search = "";
        private DispatcherTimer timer;

        public PersonsPage()
        {
            people = MainWindow.logic.GetPersonList();
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            BuildList(people.OrderBy(p => p.Name).ToList());
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            people = MainWindow.logic.GetPersonList();
        }

        private void BuildList(List<Models.Person> people)
        {
            Name.Children.Clear();
            UNI.Children.Clear();
            Studing.Children.Clear();
            StartDate.Children.Clear();
            EndDate.Children.Clear();
            Button button = new Button();
            Style style = (Style)Resources["TransparentBTN"];
            for (int i = 0; i < people.Count; i++)
            {
                button = new Button()
                {
                    Content = people[i].Name,
                    Style = style,
                    Margin = new Thickness(0, 30 * i, 0, 0),
                    VerticalAlignment = VerticalAlignment.Top,
                    Tag = people[i].UNI,
                    Height = 30,
                };
                button.Click += Person_Click;
                Name.Children.Add(button);
                button = new Button()
                {
                    Content = people[i].UNI,
                    Style = style,
                    Margin = new Thickness(0, 30 * i, 0, 0),
                    VerticalAlignment = VerticalAlignment.Top,
                    Tag = people[i].UNI,
                    Height = 30,
                };
                button.Click += Person_Click;
                UNI.Children.Add(button);
                button = new Button()
                {
                    Content = people[i].Studing,
                    Style = style,
                    Margin = new Thickness(0, 30 * i, 0, 0),
                    VerticalAlignment = VerticalAlignment.Top,
                    Tag = people[i].UNI,
                    Height = 30,
                };
                button.Click += Person_Click;
                Studing.Children.Add(button);
                if (people[i].StartCourse > new DateTime())
                {
                    button = new Button()
                    {
                        Content = people[i].StartCourse.ToString("dd-MM-yyyy"),
                        Style = style,
                        Margin = new Thickness(0, 30 * i, 0, 0),
                        VerticalAlignment = VerticalAlignment.Top,
                        Tag = people[i].UNI,
                        Height = 30,
                    };
                }
                else
                {
                    button = new Button()
                    {
                        Style = style,
                        Margin = new Thickness(0, 30 * i, 0, 0),
                        VerticalAlignment = VerticalAlignment.Top,
                        Tag = people[i].UNI,
                        Height = 30,
                    };
                }
                button.Click += Person_Click;
                StartDate.Children.Add(button);
                if (people[i].EndCourse > new DateTime())
                {
                    button = new Button()
                    {
                        Content = people[i].EndCourse.ToString("dd-MM-yyyy"),
                        Style = style,
                        Margin = new Thickness(0, 30 * i, 0, 0),
                        VerticalAlignment = VerticalAlignment.Top,
                        Tag = people[i].UNI,
                        Height = 30,
                    };
                }
                else
                {
                    button = new Button()
                    {
                        Style = style,
                        Margin = new Thickness(0, 30 * i, 0, 0),
                        VerticalAlignment = VerticalAlignment.Top,
                        Tag = people[i].UNI,
                        Height = 30,
                    };
                }
                button.Click += Person_Click;
                EndDate.Children.Add(button);
            }
        }

        private void Person_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage(((Button)sender).Tag.ToString()));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Tag)
            {
                case "Name":
                    name = true;
                    nameasc = !nameasc;
                    uni = studing = startDate = endDate = false;
                    uniasc = studingasc = startDateasc = endDateasc = false;
                    break;
                case "UNI":
                    uni = true;
                    uniasc = !uniasc;
                    name = studing = startDate = endDate = false;
                    nameasc = studingasc = startDateasc = endDateasc = false;
                    break;
                case "Studing":
                    studing = true;
                    studingasc = !studingasc;
                    name = uni = startDate = endDate = false;
                    nameasc = uniasc = startDateasc = endDateasc = false;
                    break;
                case "StartDate":
                    startDate = true;
                    startDateasc = !startDateasc;
                    name = studing = uni = endDate = false;
                    nameasc = studingasc = uniasc = endDateasc = false;
                    break;
                case "EndDate":
                    endDate = true;
                    endDateasc = !endDateasc;
                    name = studing = startDate = uni = false;
                    nameasc = studingasc = startDateasc = uniasc = false;
                    break;
            }
            BuildList(SortList(SearchList(people)));
        }

        private List<Models.Person> SortList(List<Models.Person> people)
        {
            List<Models.Person> Output = people;
            if (name)
            {
                if (!nameasc)
                {
                    Output = people.OrderBy(p => p.Name).ToList();
                }
                else
                {
                    Output = people.OrderByDescending(p => p.Name).ToList();
                }
            }
            else if (uni)
            {
                if (!uniasc)
                {
                    Output = people.OrderBy(p => p.UNI).ToList();
                }
                else
                {
                    Output = people.OrderByDescending(p => p.UNI).ToList();
                }
            }
            else if (studing)
            {
                if (!studingasc)
                {
                    Output = people.OrderBy(p => p.Studing).ToList();
                }
                else
                {
                    Output = people.OrderByDescending(p => p.Studing).ToList();
                }
            }
            else if (startDate)
            {
                if (!startDateasc)
                {
                    Output = people.OrderBy(p => p.StartCourse).ToList();
                }
                else
                {
                    Output = people.OrderByDescending(p => p.StartCourse).ToList();
                }
            }
            else if (endDate)
            {
                if (!endDate)
                {
                    Output = people.OrderBy(p => p.EndCourse).ToList();
                }
                else
                {
                    Output = people.OrderByDescending(p => p.EndCourse).ToList();
                }
            }
            return Output;
        }

        private List<Models.Person> SearchList(List<Models.Person> people)
        {
            List<Models.Person> PeopleSorted = new List<Models.Person>();
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].Name.ToLower().Contains(search.ToLower()) || people[i].Studing.ToLower().Contains(search.ToLower()) || people[i].UNI.ToLower().Contains(search.ToLower()) || people[i].StartCourse.ToString("dd-MM-yyyy").ToLower().Contains(search.ToLower()) || people[i].EndCourse.ToString("dd-MM-yyyy").ToLower().Contains(search.ToLower()) || search == "")
                {
                    PeopleSorted.Add(people[i]);
                }
            }
            return PeopleSorted;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            search = ((TextBox)sender).Text;
            BuildList(SortList(SearchList(people)));
        }
    }
}
