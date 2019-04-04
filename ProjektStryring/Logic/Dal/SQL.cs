using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjektStryring.Logic.Dal
{
    public class SQL
    {
        private SqlConnection connection = new SqlConnection();

        //MSSQL CONNECTION CODE
        public void SqlOpenConnection()
        {
            connection.ConnectionString =
                "Data Source=192.168.0.249;" +
                "Initial Catalog=ProjectStyring;" +
                "User id=user;" +
                "Password=ZJV8tMY_wS5qazg;";

            connection.Open();
        }

        #region default query execute
        private bool DBQueryExecute(string query)
        {
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                sqlQuery.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch
            {
                connection.Close();
                return false;
            }
        }
        #endregion
        //returns a bool
        #region Looks in the db
        //looks if the user is in the db
        public bool IsUserInDB(string uni)
        {
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spPerson_TestPerson '{0}'", uni);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    int count = 0;
                    while (reader.Read())
                    {
                        count++;
                        string name = reader.GetString(0);
                        if (name == "" || name == null)
                        {
                            connection.Close();
                            return false;
                        }
                    }
                    connection.Close();
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                connection.Close();
                return false;
            }
        }
        //looks if a user is admin for the group
        public bool IsUserAdminInGroup(string uni, int teamID)
        {
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spTeamPerson_IsUserAdmin {1}, '{0}'", uni, teamID);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetBoolean(0))
                        {
                            connection.Close();
                            return true;
                        }
                    }
                }
                connection.Close();
                return false;
            }
            catch
            {
                connection.Close();
                return false;
            }
        }
        //looks if a user is in the group
        public bool IsUserInGroup(string uni, int teamID)
        {
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spTeamPerson_IsUserAdmin {1}, '{0}'", uni, teamID);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetBoolean(0) || !reader.GetBoolean(0))
                        {
                            connection.Close();
                            return true;
                        }
                    }
                }
                connection.Close();
                return false;
            }
            catch
            {
                connection.Close();
                return false;
            }
        }
        //looks if the group is in the db and active(not complete)
        public bool IsTeamActive(int teamID)
        {
            SqlOpenConnection();
            bool completed = false;
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spTeam_IsTeamActive {0}", teamID);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        completed = reader.GetBoolean(0);
                        connection.Close();
                        return completed;
                    }
                }
                connection.Close();
                return completed;
            }
            catch
            {
                connection.Close();
                return completed;
            }
        }
        //looks if a person is webadmin
        public bool IsWebAdmin(string UNI)
        {
            SqlOpenConnection();
            bool webAdmin = false;
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spPerson_GetWebAdmin '{0}'", UNI);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        webAdmin = reader.GetBoolean(0);
                        connection.Close();
                        return webAdmin;
                    }
                }
                connection.Close();
                return webAdmin;
            }
            catch
            {
                connection.Close();
                return webAdmin;
            }

        }
        #endregion
        #region get info from db
        //gets the courses that the user has 
        public Models.Person GetUserInfo(string UNI)
        {
            Models.Person person = new Models.Person();
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);

            try
            {
                string query = string.Format("exec dbo.spCourse_GetUsersCourse '{0}'", UNI);
                SqlCommand sqlQuery = new SqlCommand(query, connection);
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            person.Courses.Add(new Models.Course(reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2), reader.GetDateTime(3)));
                        }
                        catch { }

                    }
                }
                connection.Close();
                return person;
            }
            catch
            {
                connection.Close();
                return person;
            }
        }
        //gets a list of all the persons in the db
        public List<Models.Person> GetPersonList()
        {
            List<Models.Person> people = new List<Models.Person>();
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spPerson_GetPersonList '{0}'", DateTime.Now.ToString("yyyy-MM-dd"));
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            people.Add(new Models.Person(reader.GetString(0), reader.GetDateTime(1), reader.GetDateTime(2)));
                        }
                        catch
                        {
                            people.Add(new Models.Person(reader.GetString(0)));
                        }

                    }
                    connection.Close();
                }
                return people;
            }
            catch
            {
                connection.Close();
                return people;
            }
        }
        //gets info about the group
        public Models.Group GetbasicInfoAboutGroup(int teamID)
        {
            Models.Group group = new Models.Group();
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spTeam_GetSinglegroup {0}", teamID);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            //makes a group with the information from db with enddate
                            group = new Models.Group(reader.GetString(1), reader.GetInt32(0), reader.GetString(5), reader.GetBoolean(4), reader.GetDateTime(2), reader.GetDateTime(3));
                        }
                        catch
                        {
                            //makes a group with the information from db with no enddate
                            group = new Models.Group(reader.GetString(1), reader.GetInt32(0), reader.GetString(5), reader.GetBoolean(4), reader.GetDateTime(2));
                        }
                    }
                    connection.Close();
                }
                return group;
            }
            catch
            {
                connection.Close();
                return null;
            }
        }
        //gets the small info from the members of the group
        public List<Models.Person> GetMemberList(int teamID)
        {
            List<Models.Person> people = new List<Models.Person>();
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spTeamPerson_GetmemberOfTeam {0}", teamID);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //makes a simple person
                        people.Add(new Models.Person(reader.GetString(0), reader.GetBoolean(1)));

                    }
                    connection.Close();
                }
                return people;
            }
            catch
            {
                connection.Close();
                return null;
            }
        }
        //gets all the tasks 
        public List<Models.Task> GetTaskList(int teamID, string minEndDate)
        {
            List<Models.Task> tasks = new List<Models.Task>();
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spTask_GetTask '{0}',{1}", minEndDate, teamID);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //makes a task with the information from db
                        tasks.Add(new Models.Task(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetDateTime(4)));

                    }
                    connection.Close();
                }
                return tasks;
            }
            catch
            {
                connection.Close();
                return null;
            }
        }
        //gets a list over groups with the option for complete or not
        public List<Models.Group> GetGroupList(bool complete)
        {
            List<Models.Group> groups = new List<Models.Group>();
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spTeam_GetListOverOption {0}", complete);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            //makes a group with the information from db with enddate
                            groups.Add(new Models.Group(reader.GetString(0), reader.GetInt32(1), reader.GetDateTime(2), reader.GetDateTime(3)));
                        }
                        catch
                        {
                            //makes a group with the information from db with no enddate
                            groups.Add(new Models.Group(reader.GetString(0), reader.GetInt32(1), reader.GetDateTime(2)));
                        }

                    }
                    connection.Close();
                }
                return groups;
            }
            catch
            {
                connection.Close();
                return groups;
            }
        }
        //gets a list over groups the user is in
        public List<Models.Group> GetGroupListForUser(string UNI)
        {
            List<Models.Group> groups = new List<Models.Group>();
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spTeamPerson_GetListOverGroupsWithUser '{0}'", UNI);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //makes a group with the information from db with enddate
                        groups.Add(new Models.Group(reader.GetString(0), reader.GetInt32(1)));

                    }
                    connection.Close();
                }
                return groups;
            }
            catch
            {
                connection.Close();
                return groups;
            }
        }
        //gets a list over course
        public List<Models.Course> GetCourseList()
        {
            List<Models.Course> Courses = new List<Models.Course>();
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spCourse_GetCourseList");
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //makes a group with the information from db with enddate
                        Courses.Add(new Models.Course(0, reader.GetInt32(0), reader.GetDateTime(1), reader.GetDateTime(2)));
                    }
                    connection.Close();
                }
                return Courses;
            }
            catch
            {
                connection.Close();
                return Courses;
            }
        }
        //gets the students of that course
        public List<string> GetStudentsForCourse(int CourseNumber, DateTime StartCourse, DateTime EndCourse)
        {
            List<string> StudentsUNI = new List<string>();
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spCourse_MembersOfCourse {0},'{1}','{2}'", CourseNumber, StartCourse.ToString("yyyy-MM-dd"), EndCourse.ToString("yyyy-MM-dd"));
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //makes a group with the information from db with enddate
                        StudentsUNI.Add(reader.GetString(0));
                    }
                    connection.Close();
                }
                return StudentsUNI;
            }
            catch
            {
                connection.Close();
                return StudentsUNI;
            }
        }
        //gets the events/Holidays
        public List<Models.Events> GetEvents()
        {
            List<Models.Events> events = new List<Models.Events>();
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spEventCalendar_GetEvents");
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //makes a group with the information from db with enddate
                        events.Add(new Models.Events(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetBoolean(4), reader.GetBoolean(5).ToString(), reader.GetInt32(6)));
                    }
                    connection.Close();
                }
                return events;
            }
            catch
            {
                connection.Close();
                return events;
            }
        }
        //gets the course the user is on or his next course
        public Models.Course GetPersonsCourse(string UNI)
        {
            Models.Course course = new Models.Course();
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spCourse_GetPersonsCourse '{0}','{1}'", UNI, DateTime.Now.ToString("yyyy-MM-dd"));
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        course = new Models.Course();
                        try
                        {
                            course.StartCourse = reader.GetDateTime(0);
                            course.EndCourse = reader.GetDateTime(1);
                        }
                        catch
                        {
                        }
                    }
                    connection.Close();
                }
                return course;
            }
            catch
            {
                connection.Close();
                return null;
            }
        }
        //gets a list of all the persons in the db
        public List<Models.Comment> GetCommentList(int teamid)
        {
            List<Models.Comment> comments = new List<Models.Comment>();
            SqlOpenConnection();
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            string query = string.Format("exec dbo.spComment_GetComments {0}", teamid);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comments.Add(new Models.Comment(reader.GetInt32(3), reader.GetString(0), reader.GetDateTime(1), reader.GetString(2)));
                    }
                    connection.Close();
                }
                return comments;
            }
            catch
            {
                connection.Close();
                return comments;
            }
        }
        #endregion
        #region adds somthing to the db
        //makes a user
        public bool MakeUser(string uni)
        {
            string query = string.Format("exec dbo.spPerson_AddPerson '{0}'", uni);
            return DBQueryExecute(query);
        }
        //adds a course to the user
        public void AddCourse(string uni, string startdate, string enddate, int coursenumber)
        {
            string query = string.Format("exec dbo.spCourse_AddCourse '{0}', '{1}', '{2}', {3}", uni, startdate, enddate + " 23:59:59", coursenumber);
            DBQueryExecute(query);
        }
        //adds a task to a team
        public void AddTask(int teamID, string title, string desc, string startdate, string enddate)
        {
            string query = string.Format("exec dbo.spTask_CreateTask '{0}', '{1}', '{2}', '{3}', {4}", startdate, enddate, title, desc, teamID);
            DBQueryExecute(query);
        }
        //adds a person to a group
        public void AddPersonToGroup(int teamID, string UNI)
        {
            string query = string.Format("exec dbo.spTeamPerson_AddPersonToTeam {0}, '{1}'", teamID, UNI);
            DBQueryExecute(query);
        }
        //adds a event/holiday to db
        public void AddEvent(string title, string desc, string start, string end, bool speciel, bool holiday)
        {
            string query = string.Format("exec dbo.spEventCalendar_AddEvent '{0}', '{1}', '{2}', '{3}', {4}, {5}", title, desc, start, end, speciel, holiday);
            DBQueryExecute(query);
        }
        //adds a comment to the group
        public void AddComment(string UNI, int teamID, string date, string comment)
        {
            string query = string.Format("exec dbo.spComment_AddComment '{0}', {1}, '{2}', '{3}'", UNI, teamID, date, comment);
            DBQueryExecute(query);
        }

        #endregion
        #region updates somthing in the db
        //update a users connetion to a group
        public bool UpdateUserinfoToTeam(string uni, int teamID, int UpdateTo)
        {
            string query = string.Format("exec dbo.spTeamPerson_editUser {1}, '{0}', {2}", uni, teamID, UpdateTo);
            return DBQueryExecute(query);
        }
        //updates a task for a group
        public void UpdateTask(Models.Task task, int ID, int teamID)
        {
            string query = string.Format("exec dbo.spTask_UpdateTask {0}, {1}, '{2}', '{3}', '{4}'", teamID, ID, task.Desc, task.StartDate.ToString("yyyy-MM-dd") + " " + task.StartTime.ToString("HH:mm:ss"), task.EndDate.ToString("yyyy-MM-dd") + " " + task.EndTime.ToString("HH:mm:ss"));
            DBQueryExecute(query);
        }
        //updates the description of the project
        public void UpdateGroup(int teamID, string desc)
        {
            string query = string.Format("exec dbo.spTeam_UpdateDesc {0}, '{1}'", teamID, desc);
            DBQueryExecute(query);
        }
        //makes the project complete
        public void CompleteGroup(int teamID)
        {
            string query = string.Format("exec dbo.spTeam_Complete {0}, '{1}'", teamID, DateTime.Now.ToString("yyyy-MM-dd"));
            DBQueryExecute(query);
        }
        //reopens a group
        public void ReOpenGroup(int teamID)
        {
            string query = string.Format("exec dbo.spTeam_ReOpen {0}", teamID);
            DBQueryExecute(query);
        }
        //updates the course for the user
        public void UpdateCourse(string UNI, Models.Course course)
        {
            string query = string.Format("exec dbo.spCourse_UpdateCourse '{0}', {1},'{2}','{3}'", UNI, course.ID, course.StartCourse.ToString("yyyy-MM-dd"), course.EndCourse.ToString("yyyy-MM-dd") + " 23:59:59");
            DBQueryExecute(query);
        }
        //updates the event/holiday for all
        public void UpdateEvent(Models.Course course)
        {
            string query = string.Format("exec dbo.spEventCalendar_UpdateEvent {0},'{1}','{2}'", course.ID, course.StartCourse.ToString("yyyy-MM-dd") + " " + course.StartTime.ToString("HH:mm"), course.EndCourse.ToString("yyyy-MM-dd") + " " + course.EndTime.ToString("HH:mm"));
            DBQueryExecute(query);
        }
        //Updates a persons webadmin value
        public void UpdateWebAdmin(string UNI, bool webAdmin)
        {
            string query = string.Format("exec dbo.spPerson_UpdateWebAdmin '{0}',{1}", UNI, webAdmin);
            DBQueryExecute(query);
        }
        #endregion
        #region removes somthing from the db
        //removes a users connetion to a group
        public bool RemoveConnetionToTeam(string uni, int teamID)
        {
            string query = string.Format("exec dbo.spTeamPerson_RemoveUser {1}, '{0}'", uni, teamID);
            return DBQueryExecute(query);
        }

        //Deletes the project
        //deletes everything the project has (Task, Connections to Persons)
        public void DeleteGroup(int teamID)
        {
            string query = string.Format("exec dbo.spTeam_Delete {0}", teamID);
            DBQueryExecute(query);
        }
        //removes a task from a project
        public void RemoveTask(int teamID, int TaskID)
        {
            string query = string.Format("exec dbo.spTask_RemoveTask {0}, {1}", teamID, TaskID);
            DBQueryExecute(query);
        }
        //removes a course from the user
        public void RemoveCourse(string UNI, int CourseID)
        {
            string query = string.Format("exec dbo.spCourse_RemoveCourse {0}, '{1}'", CourseID, UNI);
            DBQueryExecute(query);
        }
        //remove a event/holiday for all
        public void RemoveEvent(int ID)
        {
            string query = string.Format("exec dbo.spCourse_RemoveEvent {0}", ID);
            DBQueryExecute(query);
        }
        //remove a Comment from the group
        public void RemoveComment(string UNI, int teamID, int ID)
        {
            string query = string.Format("exec dbo.spComment_RemoveComment '{0}',{1},{2}", UNI, teamID, ID);
            DBQueryExecute(query);
        }
        //remove user from db
        public void RemoveUser(string UNI)
        {
            string query = string.Format("exec dbo.spPerson_Delete '{0}'", UNI);
            DBQueryExecute(query);
        }
        #endregion
        #region Add to db and gets something

        //makes a group and adds a person to admin
        //returns the id of the group
        public int MakeGroup(Models.Group group, string UNI)
        {
            SqlOpenConnection();
            int id = 0;
            string query = string.Format("exec dbo.spTeam_MakeTeamAndAddPerson'{0}','{1}','{2}','{3}'", group.Name, group.StartDate.ToString("yyyy-MM-dd"), group.Description, UNI);
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }
                    connection.Close();
                }
                return id;
            }
            catch
            {
                connection.Close();
                return 0;
            }
        }
        //makes a group
        //returns the id of the group
        public int MakeGroup(Models.Group group)
        {
            SqlOpenConnection();
            int id = 0;
            string query = string.Format("exec dbo.spTeam_MakeTeam'{0}','{1}','{2}'", group.Name, group.StartDate.ToString("yyyy-MM-dd"), group.Description);
            string sqlUse = "USE ProjectStyring;";
            SqlCommand sqlUseQuery = new SqlCommand(sqlUse, connection);
            SqlCommand sqlQuery = new SqlCommand(query, connection);

            try
            {
                sqlUseQuery.ExecuteNonQuery();
                using (SqlDataReader reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }
                    connection.Close();
                }
                return id;
            }
            catch
            {
                connection.Close();
                return 0;
            }
        }
        #endregion
    }
}