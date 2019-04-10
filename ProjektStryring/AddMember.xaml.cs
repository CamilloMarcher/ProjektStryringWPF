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
    /// Interaction logic for AddMember.xaml
    /// </summary>
    public partial class AddMember : Window
    {
        private int teamID;
        public AddMember(int teamID)
        {
            this.teamID = teamID;
            List<Models.Person> people = MainWindow.logic.GetPersonList();
            List<Models.Person> Members = MainWindow.logic.GetInfoFromSingleGroup(teamID).Members;
            List<string> MembersUNI = new List<string>();
            foreach(Models.Person p in Members)
            {
                MembersUNI.Add(p.UNI);
            }
            InitializeComponent();
            for (int i = 0; i < people.Count; i++)
            {
                if (!MembersUNI.Contains(people[i].UNI))
                {
                    PersonList.Items.Add(new ListBoxItem() { Content = people[i].Name, Tag = people[i].UNI });
                }
            }
        }

        private void AddMemberToGroup(object sender, RoutedEventArgs e)
        {
            System.Collections.IList newMembers = PersonList.SelectedItems;
            string members = "";
            for (int i = 0; i < newMembers.Count; i++)
            {
                members += "\n" + ((ListBoxItem)newMembers[i]).Content.ToString();
                MainWindow.logic.AddPersonToGroup(teamID, ((ListBoxItem)newMembers[i]).Tag.ToString());
            }
            MessageBox.Show(members + "\n er nu tilføjet");
        }
    }
}
