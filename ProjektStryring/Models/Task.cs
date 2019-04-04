using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektStryring.Models
{
    public class Task
    {
        //this is use for groupTask
        // the information will come from DB
        
        private int id;
        private string title;
        private string desc;
        private DateTime startDate;
        private DateTime endDate;
        public int ID { get => id; set => id = value; }
        public string ModalID { get => "Modal"+id; }
        public string Title { get => title; set => title = value; }
        public string Desc { get => desc; set => desc = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Task()
        {

        }
        public Task(int id, string title, string desc, DateTime startDate, DateTime endDate)
        {
            this.id = id;
            this.title = title;
            this.desc = desc;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}