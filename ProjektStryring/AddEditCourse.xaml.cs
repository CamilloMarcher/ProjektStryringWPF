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
using System.Windows.Shapes;

namespace ProjektStryring
{
    /// <summary>
    /// Interaction logic for AddEditCourse.xaml
    /// </summary>
    public partial class AddEditCourse : Window
    {
        private int ID = 0;
        public AddEditCourse()
        {
            InitializeComponent();
            Models.Person person = MainWindow.logic.GetFullPerson(MainWindow.logic.GetUni());
            for (int i = 1; i <= 3; i++)
            {
                CourseNumber.Items.Add(new ComboBoxItem() { Content = i });
            }
            if (!(person.Studing == "IT-supporter"))
            {
                for (int i = 4; i <= 5; i++)
                {
                    CourseNumber.Items.Add(new ComboBoxItem() { Content = i });
                }
            }
            StartDate.SelectedDate = DateTime.Now;
            EndDate.SelectedDate = DateTime.Now;
            if (person.Courses.Count == 0 || person.Courses[person.Courses.Count - 1].CourseNumber > CourseNumber.Items.Count)
            {
                CourseNumber.SelectedIndex = 0;
            }
            else
            {
                CourseNumber.SelectedIndex = person.Courses[person.Courses.Count - 1].CourseNumber;
            }
            Remove.Visibility = Visibility.Hidden;
        }
        public AddEditCourse(int courseIndex)
        {
            InitializeComponent();
            Models.Person person = MainWindow.logic.GetFullPerson(MainWindow.logic.GetUni());
            for (int i = 1; i <= 3; i++)
            {
                CourseNumber.Items.Add(new ComboBoxItem() { Content = i });
            }
            if (!(person.Studing == "IT-supporter"))
            {
                for (int i = 4; i <= 5; i++)
                {
                    CourseNumber.Items.Add(new ComboBoxItem() { Content = i });
                }
            }
            CourseNumber.SelectedIndex = person.Courses[courseIndex].CourseNumber - 1;
            StartDate.SelectedDate = person.Courses[courseIndex].StartCourse;
            EndDate.SelectedDate = person.Courses[courseIndex].EndCourse;
            this.ID = person.Courses[courseIndex].ID;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ID > 0)
            {
                Models.Course course = GetUserCourse();
                course.ID = ID;
                MainWindow.logic.UpdateCourse(MainWindow.logic.GetUni(), course);
            }
            else
            {
                MainWindow.logic.AddCourse(GetUserCourse(), MainWindow.logic.GetUni());
            }
            this.Close();
        }
        private Models.Course GetUserCourse()
        {
            return new Models.Course(0, int.Parse(((ComboBoxItem)CourseNumber.SelectedItem).Content.ToString()), (DateTime)StartDate.SelectedDate, (DateTime)EndDate.SelectedDate);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.logic.RemoveCourse(MainWindow.logic.GetUni(), ID);
            this.Close();
        }
    }
}
