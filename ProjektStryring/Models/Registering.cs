using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektStryring.Models
{
    public class Registering
    {
        public string CourseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool NoCourseKnown { get; set; }
        public bool AgreeToCompletedform { get; set; }

    }
}