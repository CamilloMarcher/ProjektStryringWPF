using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektStryring.Models
{
    public class Layout
    {
        private readonly List<Group> groupList;
        private readonly string name;
        private readonly bool admin;
        private readonly bool webAdmin;

        public List<Group> GroupList { get => groupList; }
        public string Name { get => name; }
        public bool Admin { get => admin; }
        public bool WebAdmin { get => webAdmin; }

        public Layout(List<Group> groupList, string name, bool admin, bool webAdmin)
        {
            this.groupList = groupList;
            this.name = name;
            this.admin = admin;
            this.webAdmin = webAdmin;
        }
    }
}