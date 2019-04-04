using System;
using System.Collections.Generic;
namespace ProjektStryring.Models
{
    public class Group
    {
        private string name;
        private readonly int id;
        private string description;
        private List<Person> members;
        private readonly List<Task> tasks;
        private readonly bool complete;
        private DateTime startDate;
        private DateTime endDate;
        private readonly List<Comment> comments;
        

        public string Name { get => name; set => name = value; }
        public int Id { get => id; }
        public string Description { get => description; set => description = value; }
        public List<Person> Members { get => members; set => members = value; }
        public List<Task> Tasks { get => tasks; }
        public List<Comment> Comments { get => comments; }
        public bool Complete { get => complete; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public Group() { }
        public Group(string name, int id)
        {
            this.id = id;
            this.name = name;
            members = new List<Person>();
            tasks = new List<Task>();
            comments = new List<Comment>();
        }
        public Group(string name, int id, DateTime startDate, DateTime endDate = new DateTime())
        {
            this.id = id;
            this.name = name;
            this.startDate = startDate;
            this.endDate = endDate;
            members = new List<Person>();
            tasks = new List<Task>();
            comments = new List<Comment>();
        }
        public Group(string name, int id, string desc, bool complete, DateTime startDate, DateTime endDate = new DateTime())
        {
            this.id = id;
            this.name = name;
            this.complete = complete;
            this.startDate = startDate;
            this.endDate = endDate;
            this.description = desc;
            members = new List<Person>();
            tasks = new List<Task>();
            comments = new List<Comment>();
        }
        public Group(string name, int id, string Description, bool complete, DateTime startDate, List<Person> members, List<Task> tasks, DateTime endDate = new DateTime())
        {
            this.id = id;
            this.name = name;
            this.description = Description;
            this.complete = complete;
            this.startDate = startDate;
            this.endDate = endDate;
            this.members = members;
            this.tasks = tasks;
            comments = new List<Comment>();
        }
        public Group(string name, int id, string Description, bool complete, DateTime startDate, List<Person> members, List<Task> tasks,List<Comment> comments, DateTime endDate = new DateTime())
        {
            this.id = id;
            this.name = name;
            this.description = Description;
            this.complete = complete;
            this.startDate = startDate;
            this.endDate = endDate;
            this.members = members;
            this.tasks = tasks;
            this.comments = comments;
        }
    }
}