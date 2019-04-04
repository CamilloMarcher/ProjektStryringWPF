using System;

namespace ProjektStryring.Models
{
    public class Events
    {
        private readonly string title;
        private readonly string desc;
        private readonly DateTime start_Date;
        private readonly DateTime end_Date;
        private readonly bool speciel;
        private readonly string colour;
        private readonly int id;

        public string Title { get => title; }
        public string Desc { get => desc; }
        public DateTime Start_Date { get => start_Date; }
        public DateTime End_Date { get => end_Date; }
        public bool Speciel { get => speciel; }
        public string Colour { get => colour; }
        public int ID { get => id; }
        public Events(string title, string desc, string startDate, string endDate, bool speciel)
        {
            this.title = title;
            this.desc = desc;
            //this.start_Date = startDate;
            //this.end_Date = endDate;
            this.speciel = speciel;
        }
        public Events(string title, string desc, DateTime startDate, DateTime endDate, bool speciel, string colour)
        {
            this.title = title;
            this.desc = desc;
            this.start_Date = startDate;
            this.end_Date = endDate;
            this.speciel = speciel;
            this.colour = colour;
        }
        public Events(string title, string desc, DateTime startDate, DateTime endDate, bool speciel, string colour, int id)
        {
            this.title = title;
            this.desc = desc;
            this.start_Date = startDate;
            this.end_Date = endDate;
            this.speciel = speciel;
            this.colour = colour;
            this.id = id;
        }
    }
}