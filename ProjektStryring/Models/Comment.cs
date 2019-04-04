using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektStryring.Models
{
    public class Comment
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public DateTime Datetime { get; private set; }
        public string CommentText { get; private set; }


        public Comment(string name, DateTime datetime, string comment)
        {
            this.Name = name;
            this.Datetime = datetime;
            this.CommentText = comment;
        }

        public Comment(int ID, string name, DateTime datetime, string comment)
        {
            this.ID = ID;
            this.Name = name;
            this.Datetime = datetime;
            this.CommentText = comment;
        }
        public Comment(int ID, string name, DateTime datetime, string comment, bool fullName)
        {
            this.ID = ID;
            string[] spiltName = name.Split(' ');
            this.Name = spiltName[0] + " " + spiltName[spiltName.Length-1];
            this.Datetime = datetime;
            this.CommentText = comment;
        }
    }
}