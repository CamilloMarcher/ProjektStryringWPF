using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektStryring.Models
{
    public class Course
    {
        private int id;
        private int courseNumber;
        private DateTime startCourse;
        private DateTime endCourse;
        //made so it makes a list of names in html
        private readonly string students;

        public int ID { get => id; set => id = value; }
        public int CourseNumber { get => courseNumber; set => courseNumber = value; }
        public DateTime StartCourse { get => startCourse; set => startCourse = value; }
        public DateTime EndCourse { get => endCourse; set => endCourse = value; }
        public string Students { get => students; }

        //mades for administrator calendar
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Desc { get; set; }

        public Course()
        {
            startCourse = new DateTime();
            endCourse = new DateTime();
        }
        public Course(int id,  int courseNumber, DateTime startCourse, DateTime endCourse)
        {
            this.id = id;
            this.courseNumber = courseNumber;
            this.startCourse = startCourse;
            this.endCourse = endCourse;

        }
        public Course(int id, int courseNumber, DateTime startCourse, DateTime endCourse, List<string> studentsName)
        {
            this.id = id;
            this.courseNumber = courseNumber;
            this.startCourse = startCourse;
            this.endCourse = endCourse;
            students = "<br />";
            foreach(string s in studentsName)
            {
                students += s + "<br />";
            }
        }
    }
}