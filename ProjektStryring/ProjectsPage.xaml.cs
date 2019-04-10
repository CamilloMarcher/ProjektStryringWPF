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
    /// <summary>
    /// Interaction logic for ProjectsPage.xaml
    /// </summary>
    public partial class ProjectsPage : Page
    {
        public ProjectsPage()
        {
            List<Models.Group> projectsNow = MainWindow.logic.GetGroupList(false);
            List<Models.Group> projectsPre = MainWindow.logic.GetGroupList(true);
            InitializeComponent();
            Button projectName = new Button();
            Button projectDate = new Button();
            Style style = (Style)Resources["TransparentBTN"];
            for (int i = 0; i < projectsNow.Count; i++)
            {
                projectName = new Button()
                {
                    Margin = new Thickness(0, 30 * i, 0, 0),
                    Content = projectsNow[i].Name,
                    Width = 350,
                    Height = 30,
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 75, 75, 75)),
                    FontSize = 14,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Style = style,
                    Tag = projectsNow[i].Id
                };
                projectName.Click += Project_Click;
                projectDate = new Button()
                {
                    Margin = new Thickness(0, 30 * i, 0, 0),
                    Content = projectsNow[i].StartDate.ToString("dd-MM-yyyy"),
                    Width = 100,
                    Height = 30,
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 75, 75, 75)),
                    FontSize = 14,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Style = style,
                    Tag = projectsNow[i].Id
                };
                projectDate.Click += Project_Click;
                this.NowProjectName.Children.Add(projectName);
                this.NowProjectDate.Children.Add(projectDate);
            }
            for (int i = 0; i < projectsPre.Count; i++)
            {
                projectName = new Button()
                {
                    Margin = new Thickness(0, 30 * i, 0, 0),
                    Content = projectsPre[i].Name,
                    Width = 350,
                    Height = 30,
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 75, 75, 75)),
                    FontSize = 14,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Style = style,
                    Tag = projectsPre[i].Id
                };
                projectName.Click += Project_Click;
                projectDate = new Button()
                {
                    Margin = new Thickness(0, 30 * i, 0, 0),
                    Content = projectsPre[i].EndDate.ToString("dd-MM-yyyy"),
                    Width = 100,
                    Height = 30,
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 75, 75, 75)),
                    FontSize = 14,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Style = style,
                    Tag = projectsPre[i].Id
                };
                projectDate.Click += Project_Click;
                this.PreProjectName.Children.Add(projectName);
                this.PreProjectDate.Children.Add(projectDate);
            }
        }
        private void Project_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ProjectPage(int.Parse(((Button)sender).Tag.ToString())));
        }
        private void BtnMakeProject(object sender, RoutedEventArgs e)
        {

            MakeProject mp = new MakeProject();
            mp.Show();
        }
    }
}
