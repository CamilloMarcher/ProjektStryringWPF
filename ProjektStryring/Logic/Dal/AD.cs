using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using ProjektStryring.Properties;

namespace ProjektStryring.Logic.Dal
{
    public class AD
    {
        private string[] userInfo = new string[2] { Settings.Default.Username, Settings.Default.Password };
        private string name;
        private bool isStudent;
        private string studing;

        public string[] UserInfo { get => userInfo; }
        public string Name { get => name; }
        public bool IsStudent { get => isStudent; }
        public string Studing { get => studing; }
        public string Fail { get; private set; }


        //trys to logon with the information the user gave in login page to see if the user can login
        public bool ValidateUser(string userName, string password)
        {
            bool Valid = false;
            try
            {
                PrincipalContext pc = new PrincipalContext(ContextType.Domain, "skpit-slagelse");
                Valid = pc.ValidateCredentials(userName, password);
                if (Valid)
                {
                    userInfo[1] = password;
                    userInfo[0] = userName;
                    DirectorySearcher search = new DirectorySearcher(new DirectoryEntry("LDAP://skpit-slagelse.lokal", userName, password))
                    {
                        //makes a filter the searches for users with the same UNI(sAMAccountName)
                        Filter = string.Format("(&(objectClass=user)(objectCategory=person)(sAMAccountName={0}))", userName)
                    };
                    //asks for name and what group it is member of
                    search.PropertiesToLoad.Add("cn");
                    //gets the information
                    SearchResultCollection resultCol = search.FindAll();

                }
            }
            catch
            {
            }
            return Valid; //true = user authenticated!
        }

        //gets the name and what it's Studing from AD 
        public Models.Person GetUserInfo(string UNI)
        {
            try
            {
                //login to ad with the server 
                DirectorySearcher search = new DirectorySearcher(new DirectoryEntry("LDAP://skpit-slagelse.lokal", userInfo[0], userInfo[1]))
                {
                    //makes a filter the searches for users with the same UNI(sAMAccountName)
                    Filter = string.Format("(&(objectClass=user)(objectCategory=person)(sAMAccountName={0}))", UNI)
                };
                //asks for name and what group it is member of
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("title");
                SearchResult result;
                //gets the information
                SearchResultCollection resultCol = search.FindAll();
                //if it gets any
                if (resultCol != null)
                {
                    //goes one by one
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        result = resultCol[counter];
                        //looks if it contains what we need
                        if (result.Properties.Contains("cn") && result.Properties.Contains("title"))
                        {
                            //sets the name
                            name = result.GetDirectoryEntry().Properties["cn"].Value.ToString();
                            //sets the group after we trim it a bit
                            studing = TrimGroupName(result.GetDirectoryEntry().Properties["title"].Value.ToString());
                            //looks if you are admin/Instrutur
                            if (studing == "Instruktør")
                            {
                                isStudent = false;
                            }
                            else
                            {
                                isStudent = true;
                            }
                        }
                    }
                    //makes a person with the new information
                    Models.Person person = new Models.Person
                    {
                        Name = Name,
                        Studing = Studing,
                        Admin = !IsStudent,
                        UNI = UNI
                    };
                    return person;
                }
                return new Models.Person();
            }
            catch
            {
                return new Models.Person();
            }
        }

        //trims the value so we only have the first word (What it's studing)
        private string TrimGroupName(string text)
        {
            return text.Split(' ')[0];
        }

        //looks if the user is login 
        public bool IsUserLogin()
        {
            if (userInfo[0] != "" && userInfo[1] != "")
            {
                return true;
            }
            return false;
        }
        //saves a user in the object
        public void SaveUser(string username, string password)
        {
            ValidateUser(username, password);
        }
    }
}