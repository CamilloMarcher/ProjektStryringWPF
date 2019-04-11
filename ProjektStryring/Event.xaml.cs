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
    /// Interaction logic for Event.xaml
    /// </summary>
    public partial class EventShow : Window
    {
        public EventShow(string title, string desc, string type)
        {
            InitializeComponent();
            Title.Content = title;
            Desc.Text = desc;
            Type.Content = type;
        }
    }
}
