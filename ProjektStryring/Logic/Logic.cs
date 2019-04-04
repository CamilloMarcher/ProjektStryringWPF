using System;
using System.Collections.Generic;

namespace ProjektStryring.Logic
{
    public class Logic
    {
        private readonly Dal.SQL sql;
        private readonly Dal.AD ad;
        private readonly Createtor createtor;

        public Logic()
        {
            sql = new Dal.SQL();
            ad = new Dal.AD();
            createtor = new Createtor();
        }

        public bool IsWebAdmin(string UNI)
        {
            return sql.IsWebAdmin(UNI);
        }
        //looks if a user is admin or webadmin
        public bool IsAdmin(string UNI)
        {
            return (GetPerson(UNI).Admin || sql.IsWebAdmin(UNI));
        }
        #region AD
        #region Getinfo
        //looks in the AD to see if the user is in it and gave the right info
        public bool Login(string uni, string password)
        {
            return ad.ValidateUser(uni, password);
        }
        //get info from the AD
        public Models.Person GetPerson(string UNI)
        {
            return ad.GetUserInfo(UNI);
        }
        public bool IsUserLogin()
        {
            return ad.IsUserLogin();
        }
        #region addsinfo
        public void SaveUser(string username, string password)
        {
            ad.SaveUser(username, password);
        }
        #endregion
        #endregion
        #endregion
        #region DB
        #region gets info from db
        //looks if the user is in the database
        public bool IsUserInDB(string uni)
        {
            return sql.IsUserInDB(uni);
        }
        //looks if the user is admin in the group
        public bool IsUserAdminInGroup(string uni, int teamID)
        {
            return sql.IsUserAdminInGroup(uni, teamID);
        }
        //Looks if the user is in the group
        public bool IsUserInGroup(string uni, int teamID)
        {
            return sql.IsUserInGroup(uni, teamID);
        }
        //makes tasks for the group to events for the calender. 
        public List<Models.Events> GetTasksAsEvents(int teamID)
        {
            List<Models.Events> events = new List<Models.Events>();
            List<Models.Task> tasks = sql.GetTaskList(teamID, "2019-01-01");
            foreach (Models.Task t in tasks)
            {
                events.Add(new Models.Events(t.Title, t.Desc, t.StartDate, t.EndDate, false, "#06BABA"));
            }
            return events;
        }
        //gets a list over groups with the option for complete or not
        public List<Models.Group> GetGroupList(bool complete)
        {
            return sql.GetGroupList(complete);
        }
        //looks if the group is in the db and active(not complete)
        public bool IsTeamActive(int teamID)
        {
            return sql.IsTeamActive(teamID);
        }
        //gets the events/holidays and give them there colour
        public List<Models.Events> GetEvents()
        {
            List<Models.Events> events = sql.GetEvents();
            for (int i = 0; i < events.Count; i++)
            {
                if (events[i].Speciel)
                {
                    events[i] = new Models.Events(events[i].Title, events[i].Desc, events[i].Start_Date, events[i].End_Date, false, "#DD1515", events[i].ID);
                }
                else if (bool.Parse(events[i].Colour))
                {
                    events[i] = new Models.Events(events[i].Title, events[i].Desc, events[i].Start_Date, events[i].End_Date, false, "#AAAAAA", events[i].ID);
                }
                else
                {
                    events[i] = new Models.Events(events[i].Title, events[i].Desc, events[i].Start_Date, events[i].End_Date, false, "#F1C40F", events[i].ID);
                }
            }
            return events;
        }
        #endregion
        #region adds to db
        //adds a person to the database
        public bool AddToDatabase(string uni)
        {
            return sql.MakeUser(uni);
        }
        //adds a course to the user
        public void AddCourse(string uni, DateTime start, DateTime end, string courseNumber)
        {
            sql.AddCourse(uni, start.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToInt32(courseNumber));
        }
        //adds a task to a group
        public void AddTask(int teamID, string title, string desc, DateTime startdate, DateTime enddate, DateTime startTime, DateTime endTime)
        {
            sql.AddTask(teamID, title, desc, startdate.ToString("yyyy-MM-dd") + " " + startTime.ToString("HH:mm:ss"), enddate.ToString("yyyy-MM-dd") + " " + endTime.ToString("HH:mm:ss"));
        }

        //makes a group and adds a person to admin
        //returns the id of the group
        public int MakeGroup(Models.Group group, string UNI)
        {
            return sql.MakeGroup(group, UNI);
        }
        //makes a group
        //returns the id of the group
        public int MakeGroup(Models.Group group)
        {
            return sql.MakeGroup(group);
        }
        //adds a person to a group
        public void AddPersonToGroup(int teamID, string UNI)
        {
            sql.AddPersonToGroup(teamID, UNI);
        }
        //adds the course to him in db 
        public void AddCourse(Models.Course course, string UNI)
        {
            sql.AddCourse(UNI, course.StartCourse.ToString("yyyy-MM-dd"), course.EndCourse.ToString("yyyy-MM-dd"), course.CourseNumber);
        }
        //adds a event/holiday to the db
        public void AddEvent(Models.Course course)
        {
            if (course.Type == "VE")
            {
                sql.AddEvent(course.Title, course.Desc, course.StartCourse.ToString("yyyy-MM-dd") + course.StartTime.ToString(" HH:mm:ss"), course.EndCourse.ToString("yyyy-MM-dd") + course.EndTime.ToString(" HH:mm:ss"), true, false);
            }
            else if (course.Type == "F")
            {
                sql.AddEvent(course.Title, course.Desc, course.StartCourse.ToString("yyyy-MM-dd"), course.EndCourse.ToString("yyyy-MM-dd") + " 23:59:59", false, true);
            }
            else
            {
                sql.AddEvent(course.Title, course.Desc, course.StartCourse.ToString("yyyy-MM-dd") + course.StartTime.ToString(" HH:mm:ss"), course.EndCourse.ToString("yyyy-MM-dd") + course.EndTime.ToString(" HH:mm:ss"), false, false);
            }
        }
        //adds a comment to the group
        public void AddComment(string UNI, int teamID, DateTime date, string comment)
        {
            sql.AddComment(UNI, teamID, date.ToString("yyyy-MM-dd HH:mm:ss"), comment);
        }
        #endregion
        #region edits info in db
        //udates the connetion between the user and the group
        public bool UpdateUserinfoToTeam(string uni, int teamID, int UpdateTo)
        {
            return sql.UpdateUserinfoToTeam(uni, teamID, UpdateTo);
        }
        //update the task for a group
        public void UpdateTask(Models.Task task, int ID, int teamID)
        {
            sql.UpdateTask(task, ID, teamID);
        }
        //updates the description
        public void UpdateGroup(int teamID, string desc)
        {
            sql.UpdateGroup(teamID, desc);
        }
        //makes the project complete
        public void CompleteGroup(int teamID)
        {
            sql.CompleteGroup(teamID);
        }
        //Reopens a group
        public void ReOpenGroup(int teamID)
        {
            sql.ReOpenGroup(teamID);
        }
        //updates the course for the user
        public void UpdateCourse(string UNI, Models.Course course)
        {
            sql.UpdateCourse(UNI, course);
        }
        //update the event/holiday for all
        public void UpdateEvent(Models.Course course)
        {
            sql.UpdateEvent(course);
        }
        //UPdates a persons webadmin value
        public void UpdateWebAdmin(string UNI, bool webAdmin)
        {
            sql.UpdateWebAdmin(UNI, webAdmin);
        }
        #endregion
        #region removes from db
        //removes the coonetion between the user and the group
        public bool RemoveConnetionToTeam(string uni, int teamID)
        {
            return sql.RemoveConnetionToTeam(uni, teamID);
        }
        //deletes the poject 
        public void DeleteGroup(int teamID)
        {
            sql.DeleteGroup(teamID);
        }
        //removes a task from a project
        public void RemoveTask(int teamID, int TaskID)
        {
            sql.RemoveTask(teamID, TaskID);
        }
        //removes a course from the user
        public void RemoveCourse(string UNI, int CourseID)
        {
            sql.RemoveCourse(UNI, CourseID);
        }
        //remove a event/holiday for all
        public void RemoveEvent(int ID)
        {
            sql.RemoveEvent(ID);
        }
        //remove a Comment from the group
        public void RemoveComment(string UNI, int teamID, int ID)
        {
            sql.RemoveComment(UNI, teamID, ID);
        }
        //remove user from db
        public void RemoveUser(string UNI)
        {
            sql.RemoveUser(UNI);
        }
        #endregion
        #endregion
        #region Creators
        //makes a objet for one group with everything
        public Models.Group GetInfoFromSingleGroup(int teamID)
        {
            return createtor.GetSingleGroup(teamID, ad, sql);
        }
        //makes a object with all the info for the basic layout
        public Models.Layout GetLayoutInfo(string UNI)
        {
            return createtor.GetLayoutInfo(UNI, ad, sql);
        }
        //gets a list of users that is in the database with the information from ad
        public List<Models.Person> GetPersonList()
        {
            return createtor.GetPeopleList(ad, sql);
        }
        //get all the info about the user
        public Models.Person GetFullPerson(string UNI)
        {
            return createtor.GetFullPerson(UNI, ad, sql);
        }
        //get a list of all courses in db with names of the students
        public List<Models.Course> Getcourses()
        {
            return createtor.Courses(ad, sql);
        }
        //gets the list of groups with members
        public List<Models.Group> GetGroupListFull()
        {
            return createtor.GetGroupListFull(ad, sql);

        }
        #endregion
        #region usefull things
        //looks if a person is in a list of persons
        //it only looks at the uni nothing else
        public bool IsItInTheList(List<Models.Person> people, Models.Person person)
        {
            foreach (Models.Person p in people)
            {
                if (p.UNI == person.UNI)
                {
                    return true;
                }
            }
            return false;
        }
        //looks if you are admin in a group from a list-
        public bool IsItAdminForGroup(List<Models.Person> people, Models.Person person)
        {
            foreach (Models.Person p in people)
            {
                if (p.UNI == person.UNI)
                {
                    return p.Admin;
                }
            }
            return false;
        }
        #endregion
    }
}