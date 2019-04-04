using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektStryring.Models
{
    public class Person
    {
        //only used when login will be removed after login
        public string Password { get; set; }

        //get from the database
        public string Name { get; set; }
        public string UNI { get; set; }
        public string Studing { get; set; }
        public bool OnCourse { get; set; }
        public DateTime StartCourse { get; set; }
        public DateTime EndCourse { get; set; }
        public bool Admin { get; set; }
        public List<Course> Courses { get; set; }
        public List<Group> Groups { get; set; }

        //if you only need a person with no info
        public Person() {
            Groups = new List<Group>();
            Courses = new List<Course>();
        }
        //person with UNI
        public Person(string UNI)
        {
            this.UNI = UNI;
            Groups = new List<Group>();
            Courses = new List<Course>();
        }
        public Person(string uni, bool admin)
        {
            UNI = uni;
            Admin = admin;
        }
        //person with uni and next course
        public Person(string UNI, DateTime startCourse, DateTime endCourse)
        {
            this.UNI = UNI;
            StartCourse = startCourse;
            EndCourse = endCourse;
            if (DateTime.Now > startCourse && DateTime.Now < endCourse)
            {
                OnCourse = true;
            }
            Groups = new List<Group>();
            Courses = new List<Course>();
        }
        //person for groups with most useful info 
        public Person(string name, string uni, string studing, bool admin)
        {
            string[] tmpName = name.Split(' ');
            Name = tmpName[0];
            Name += " " + tmpName[tmpName.Length - 1];
            UNI = uni;
            Studing = studing;
            Admin = admin;
        }
        //person with all the info of the person and next course
        public Person(string UNI, string name,string studing, DateTime startCourse, DateTime endCourse)
        {
            this.UNI = UNI;
            Name = name;
            Studing = studing;
            StartCourse = startCourse;
            EndCourse = endCourse;
            if (DateTime.Now > startCourse && DateTime.Now < endCourse)
            {
                OnCourse = true;
            }
            Groups = new List<Group>();
            Courses = new List<Course>();
        }
        //person with all the information
        public Person(string name, string uni, string studing, List<Course> courses, List<Group> groups, bool admin)
        {
            Name = name;
            UNI = uni;
            Studing = studing;
            Courses = courses;
            Groups = groups;
            Admin = admin;
            foreach(Course c in courses)
            {
                if (c.StartCourse > DateTime.Now && c.EndCourse < DateTime.Now)
                {
                    OnCourse = true;
                }
            }
        }
    }
}