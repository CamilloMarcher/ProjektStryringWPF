using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjektStryring.Logic
{
    public class Createtor
    {
        //makes a group object with all
        public Models.Group GetSingleGroup(int id, Dal.AD ad, Dal.SQL sql)
        {
            List<Models.Person> members = new List<Models.Person>();
            List<Models.Person> membersSmall = new List<Models.Person>();
            List<Models.Task> tasks = new List<Models.Task>();

            //makes the list of members
            membersSmall = sql.GetMemberList(id);
            foreach (Models.Person p in membersSmall)
            {
                Models.Person person = ad.GetUserInfo(p.UNI);
                members.Add(new Models.Person(person.Name, p.UNI, person.Studing, p.Admin));
            }

            //makes the list of tasks
            tasks = sql.GetTaskList(id, DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss"));

            //gets the base info about the group
            Models.Group group = sql.GetbasicInfoAboutGroup(id);

            //gets all the comments for that group
            List<Models.Comment> comments = sql.GetCommentList(id);
            for (int i = 0; i < comments.Count; i++)
            {
                comments[i] = new Models.Comment(comments[i].ID, ad.GetUserInfo(comments[i].Name).Name, comments[i].Datetime, comments[i].CommentText, true);
            }

            return new Models.Group(group.Name, group.Id, group.Description, group.Complete, group.StartDate, members, tasks, comments, group.EndDate);

        }
        //makes a object with all the info for the basic layout
        public Models.Layout GetLayoutInfo(string UNI, Dal.AD ad, Dal.SQL sql)
        {
            Models.Person person = ad.GetUserInfo(UNI);
            string name = person.Name;
            bool admin = person.Admin;
            bool webAdmin = sql.IsWebAdmin(UNI);
            List<Models.Group> groupList = sql.GetGroupListForUser(UNI);

            return new Models.Layout(groupList, name, admin, webAdmin);
        }
        //makes a person with all information of the user
        public Models.Person GetFullPerson(string UNI, Dal.AD ad, Dal.SQL sql)
        {
            Models.Person p = sql.GetUserInfo(UNI);
            string uni = UNI;
            List<Models.Group> groups = sql.GetGroupListForUser(UNI);
            List<Models.Course> courses = p.Courses;

            p = ad.GetUserInfo(UNI);
            string name = p.Name;
            string studing = p.Studing;
            bool admin = p.Admin;
            Models.Person person = new Models.Person(name, UNI, studing, courses, groups, admin);
            return person;
        }
        //makes a list with all persons in db with the information from ad
        public List<Models.Person> GetPeopleList(Dal.AD ad, Dal.SQL sql)
        {
            List<Models.Person> people = sql.GetPersonList();
            List<Models.Person> peopleList = new List<Models.Person>();
            foreach (Models.Person p in people)
            {
                Models.Person person = ad.GetUserInfo(p.UNI);
                peopleList.Add(new Models.Person(person.UNI, person.Name, person.Studing, p.StartCourse, p.EndCourse));
            }
            return peopleList;
        }
        //makes a list of all course
        public List<Models.Course> Courses(Dal.AD ad, Dal.SQL sql)
        {
            List<Models.Course> Output = new List<Models.Course>();
            List<Models.Course> courses = sql.GetCourseList();
            foreach (Models.Course c in courses)
            {
                List<string> students = sql.GetStudentsForCourse(c.CourseNumber, c.StartCourse, c.EndCourse);
                for (int i = 0; i < students.Count; i++)
                {
                    students[i] = ad.GetUserInfo(students[i]).Name;
                }
                Output.Add(new Models.Course(0, c.CourseNumber, c.StartCourse, c.EndCourse, students));
            }
            return Output;
        }
        public List<Models.Group> GetGroupListFull(Dal.AD ad, Dal.SQL sql)
        {
            List<Models.Group> groups = sql.GetGroupList(false);
            for (int i = 0; i < groups.Count; i++)
            {
                groups[i].Members = sql.GetMemberList(groups[i].Id);
                for (int j = 0; j < groups[i].Members.Count; j++)
                {
                    Models.Person person = new Models.Person();
                    Models.Course personCourse = sql.GetPersonsCourse(groups[i].Members[j].UNI);
                    if (personCourse.StartCourse < DateTime.Now && personCourse.EndCourse > DateTime.Now)
                    {
                        person.OnCourse = true;
                    }
                    else
                    {
                        person.OnCourse = false;
                    }
                    person.Name = ad.GetUserInfo(groups[i].Members[j].UNI).Name;
                    groups[i].Members[j] = person;
                }
            }
            return groups;
        }
    }
}