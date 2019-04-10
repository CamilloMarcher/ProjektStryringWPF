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
    /// Interaction logic for MakeProject.xaml
    /// </summary>
    public partial class MakeProject : Window
    {
        public MakeProject()
        {
            InitializeComponent();
            ProjectStartDate.SelectedDate = DateTime.Now;
        }

        private void BtnMakeProject(object sender, RoutedEventArgs e)
        {
            MainWindow.logic.MakeGroup(new Models.Group() { Name = ProjectName.Text, Description = ProjectBeskrivelse.Text, StartDate = ProjectStartDate.SelectedDate.Value.Date },MainWindow.logic.GetUni());
            this.Close();
        }
        private void BtnCanel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
