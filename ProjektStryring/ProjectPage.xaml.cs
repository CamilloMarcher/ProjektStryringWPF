using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ProjektStryring
{
    public partial class ProjectPage : Page
    {
        private int teamID;
        private DispatcherTimer timer;
        private bool adminInGroup = false;
        private bool userInGroup = false;

        public ProjectPage(int ID)
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.Tick += Update;
            timer.Start();
            Models.Group group = MainWindow.logic.GetInfoFromSingleGroup(ID);
            teamID = group.Id;
            InitializeComponent();
            Name.Text = group.Name;
            Desc.Text = group.Description;
            Update(new object(), new EventArgs());
            if (MainWindow.logic.IsUserAdminInGroup(MainWindow.logic.GetUni(), teamID) || MainWindow.logic.IsAdmin(MainWindow.logic.GetUni()))
            {
                adminInGroup = true;
            }
            if (MainWindow.logic.IsUserInGroup(MainWindow.logic.GetUni(), teamID) || MainWindow.logic.IsAdmin(MainWindow.logic.GetUni()))
            {
                userInGroup = true;
            }
            else
            {
                CommentField.Visibility = Visibility.Hidden;
                CommentSend.Visibility = Visibility.Hidden;
                Save.Visibility = Visibility.Hidden;
                Desc.IsReadOnly = true;
            }
        }

        private void UpGradeMember(object sender, RoutedEventArgs e)
        {
            MainWindow.logic.UpdateUserinfoToTeam(((MenuItem)sender).Tag.ToString(), teamID, 1);
            Update(new object(), new EventArgs());
        }
        private void DownGradeMember(object sender, RoutedEventArgs e)
        {
            MainWindow.logic.UpdateUserinfoToTeam(((MenuItem)sender).Tag.ToString(), teamID, 0);
            Update(new object(), new EventArgs());
        }
        private void RemoveMember(object sender, RoutedEventArgs e)
        {
            MainWindow.logic.RemoveConnetionToTeam(((MenuItem)sender).Tag.ToString(), teamID);
            Update(new object(), new EventArgs());
        }

        private void AddMember(object sender, RoutedEventArgs e)
        {
            if (adminInGroup)
            {
                AddMember addMember = new AddMember(teamID);
                addMember.Show();
            }
        }

        private void AddDeadLine(object sender, RoutedEventArgs e)
        {
            TaskEditAdd taskEditAdd = new TaskEditAdd(teamID);
            taskEditAdd.Show();
        }

        private void Update(object sender, EventArgs e)
        {
            Models.Group group = MainWindow.logic.GetInfoFromSingleGroup(teamID);
            #region commet
            Comment.Items.Clear();
            for (int i = 0; i < group.Comments.Count; i++)
            {
                Comment.Items.Add(new ListBoxItem() { FontWeight = FontWeights.Bold, IsEnabled = false, Content = group.Comments[i].Name + "\t\t\t\t\t\t\t" + group.Comments[i].Datetime });
                Comment.Items.Add(new ListBoxItem() { IsEnabled = false, Content = group.Comments[i].CommentText });
            }
            #endregion
            #region leftside
            LeftSide.Children.Clear();
            int height = 40;
            Style style = (Style)Resources["TransparentBTN"];
            MenuItem mi = new MenuItem();
            ContextMenu ct = new ContextMenu();
            Button button = new Button()
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Content = "Members  +",
                Style = style,
                Height = 40,
                Width = 200,
            };
            button.Click += AddMember;
            LeftSide.Children.Add(button);
            for (int i = 0; i < group.Members.Count; i++)
            {
                ct = new ContextMenu();
                if (adminInGroup)
                {
                    mi = new MenuItem() { Header = "Gør til Administrator", Tag = group.Members[i].UNI };
                    mi.Click += UpGradeMember;
                    ct.Items.Add(mi);
                    mi = new MenuItem() { Header = "Gør til Normal medlemmer", Tag = group.Members[i].UNI };
                    mi.Click += DownGradeMember;
                    ct.Items.Add(mi);
                    mi = new MenuItem() { Header = "Fjern fra projektet", Tag = group.Members[i].UNI };
                    mi.Click += RemoveMember;
                    ct.Items.Add(mi);
                }
                button = new Button()
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Content = group.Members[i].Name.Split()[0],
                    Tag = group.Members[i].UNI,
                    Style = style,
                    Height = 40,
                    Width = 200,
                    Margin = new Thickness(0, height, 0, 0),
                    ContextMenu = ct,
                };
                button.Click += MemberClick;
                height += 40;
                LeftSide.Children.Add(button);
            }
            if (userInGroup)
            {

                height += 40;
                button = new Button()
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Content = "Deadlines  +",
                    Style = style,
                    Height = 40,
                    Width = 200,
                    Margin = new Thickness(0, height, 0, 0),
                };
                button.Click += AddDeadLine;
                LeftSide.Children.Add(button);
                height += 40;
                for (int i = 0; i < group.Tasks.Count; i++)
                {
                    ct = new ContextMenu();
                    mi = new MenuItem() { Header = "Fjern opgave", Tag = group.Tasks[i].ID };
                    mi.Click += RemoveTask;
                    ct.Items.Add(mi);
                    button = new Button()
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Content = group.Tasks[i].Title,
                        Tag = group.Tasks[i],
                        Style = style,
                        Height = 40,
                        Width = 200,
                        Margin = new Thickness(0, height, 0, 0),
                        ContextMenu = ct,
                    };
                    button.Click += TaskClick;
                    height += 40;
                    LeftSide.Children.Add(button);
                }
            }
            #endregion
        }

        private void RemoveTask(object sender, RoutedEventArgs e)
        {
            MainWindow.logic.RemoveTask(teamID, int.Parse(((MenuItem)sender).Tag.ToString()));
            Update(new object(), new EventArgs());
        }
        private void TaskClick(object sender, RoutedEventArgs e)
        {
            TaskEditAdd taskEditAdd = new TaskEditAdd((Models.Task)((Button)sender).Tag, teamID);
            taskEditAdd.Show();
        }

        private void MemberClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage(((Button)sender).Tag.ToString()));
        }

        private void SaveGroup(object sender, RoutedEventArgs e)
        {
            MainWindow.logic.UpdateGroup(teamID, Desc.Text);
            Update(new object(), new EventArgs());
        }
        private void AddComment(object sender, RoutedEventArgs e)
        {
            MainWindow.logic.AddComment(MainWindow.logic.GetUni(), teamID, DateTime.Now, CommentField.Text);
            CommentField.Text = "";
            Update(new object(), new EventArgs());
        }
    }
}
