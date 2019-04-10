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
    /// Interaction logic for TaskEditAdd.xaml
    /// </summary>
    public partial class TaskEditAdd : Window
    {
        private int id = 0;
        private int teamID = 0;
        public TaskEditAdd(int teamID)
        {
            this.teamID = teamID;
            InitializeComponent();
            Title.Text = "";
            Description.Text = "";
            StartDate.SelectedDate = DateTime.Now;
            EndDate.SelectedDate = DateTime.Now;
            id = 0;
        }
        public TaskEditAdd(Models.Task task, int teamID)
        {
            this.teamID = teamID;
            InitializeComponent();
            Title.Text = task.Title;
            Description.Text = task.Desc;
            StartDate.SelectedDate = task.StartDate;
            EndDate.SelectedDate = task.EndDate;
            StartTime.Text = task.StartDate.ToString("HH") + ":" + task.StartDate.ToString("mm");
            EndTime.Text = task.EndDate.ToString("HH") + ":" + task.EndDate.ToString("mm");
            id = task.ID;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (id > 0)
            {
                DateTime start = MakeToOneDateTime((DateTime)StartDate.SelectedDate, StartTime.Text);
                DateTime end = MakeToOneDateTime((DateTime)EndDate.SelectedDate, EndTime.Text);
                MainWindow.logic.UpdateTask(new Models.Task(id, Title.Text, Description.Text, start, end), id, teamID);
            }
            else
            {
                DateTime start = MakeToOneDateTime((DateTime)StartDate.SelectedDate, StartTime.Text);
                DateTime end = MakeToOneDateTime((DateTime)EndDate.SelectedDate, EndTime.Text);
                MainWindow.logic.AddTask(teamID, Title.Text, Description.Text, start, end, start, end);
            }
            Close(sender, e);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private DateTime MakeToOneDateTime(DateTime date, string time)
        {
            string[] timeSplit = time.Split(':');
            if (timeSplit.Length == 2)
            {
                date = new DateTime(date.Year, date.Month, date.Day, int.Parse(timeSplit[0]), int.Parse(timeSplit[1]), 0);
            }
            return date;
        }
    }
}
