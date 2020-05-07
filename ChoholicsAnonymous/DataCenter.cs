using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace ChoholicsAnonymous
{

    static class DataCenter
    {
        public static int MemberCount   { get; set; }
        public static int SessionCount  { get; set; }
        public static int ProviderCount { get; set; }

        public static List<Member>     MemberList     = new List<Member>();
        public static List<Provider>   ProviderList   = new List<Provider>();
        public static List<AbvSession> AbvSessionList = new List<AbvSession>();
        public static List<Service>    ServiceList    = new List<Service>();

        private static System.Threading.Timer weeklyTimer = new System.Threading.Timer(runWeeklyReport);  

        public static void writeToFile(string fileName, string dataType)
        {
            string fullPath       = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string editedPath     = fullPath.Replace("\\bin\\Debug", "\\" + fileName);
            string abvSessionFile = fullPath.Replace("\\bin\\Debug", "\\SessionsDirectory\\" + fileName);

            if (dataType == "member")
            {
                XmlSerializer serial = new XmlSerializer(typeof(List<Member>));
                StreamWriter  file   = new StreamWriter(editedPath);
                serial.Serialize(file, MemberList);
                file.Close();
            }
            else if (dataType == "provider")
            {
                XmlSerializer serial = new XmlSerializer(typeof(List<Provider>));
                StreamWriter  file   = new StreamWriter(editedPath);
                serial.Serialize(file, ProviderList);
                file.Close();
            }
            else if (dataType == "abvSession")
            {
                XmlSerializer serial = new XmlSerializer(typeof(List<AbvSession>));
                StreamWriter  file   = new StreamWriter(abvSessionFile);
                serial.Serialize(file, AbvSessionList);
                file.Close();
            }
        }

        //read all files and build structures for data storage 
        public static void initilize()
        {
            readInformation("Members.xml");
            readInformation("Providers.xml");
            readInformation("abvSessions.xml");
            initilizeServices();
            initilizeWeeklyTimer();
           

            //testing working of session below with hard coded parameters,
            //Session       sessionFromsessionID = getSessionInfo_sessionID(1);
            //List<Session> sessions_memID       = getSessionInfo_memberID(44);
            //List<Session> sessions_provID      = getSessionInfo_providerID(55);
        }

        private static void readInformation(string fileName)
        {
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;

            if (fileName == "Members.xml")
            {
                string        filePath = path.Replace("\\bin\\Debug", "\\" + fileName);
                XmlSerializer reader   = new XmlSerializer(typeof(List<Member>));
                StreamReader  file     = new StreamReader(filePath);
                DataCenter.MemberList  = (List<Member>)reader.Deserialize(file);
                file.Close();

                MemberCount = MemberList.Count();
            }

            if (fileName == "Providers.xml")
            {
                string        filePath  = path.Replace("\\bin\\Debug", "\\" + fileName);
                XmlSerializer reader    = new XmlSerializer(typeof(List<Provider>));
                StreamReader  file      = new StreamReader(filePath);
                DataCenter.ProviderList = (List<Provider>)reader.Deserialize(file);
                file.Close();

                ProviderCount = ProviderList.Count();
            }

            else if (fileName == "abvSessions.xml")
            {
                string        filePath    = path.Replace("\\bin\\Debug", "\\SessionsDirectory\\" + fileName);
                XmlSerializer reader      = new XmlSerializer(typeof(List<AbvSession>));
                StreamReader  file        = new StreamReader(filePath);
                DataCenter.AbvSessionList = (List<AbvSession>)reader.Deserialize(file);
                file.Close();
            }
        }

        #region Member Functions

        //add a member to the data set
        public static void addMember(Member newMember)
        {
            MemberList.Add(newMember);
            //writeMembersToFile("Member.xml");

        }

        //update the member information in the list 
        public static bool updateMember(Member updatedMember)
        {
            int indexOfMember = getIndexOfMember(updatedMember.MemberID);

            if (indexOfMember != -1)
            {
                MemberList[indexOfMember] = updatedMember;
                return true;
            }
            else { return false; }
        }

        //searches for member in the list
        public static Member searchMember(int memberId)
        {
            Member memberResult = new Member(false);
            for (int i = 0; i < MemberList.Count(); i++)
            {
                if (MemberList[i].MemberID == memberId)
                {
                    memberResult = MemberList[i];
                    break;               
                }
            }
            return memberResult;
        }

        //searches for member via member phone number
        public static bool checkMemNum(long memberNum)
        {
            bool numResult = false;

            for (int i = 0; i < MemberList.Count(); i++)
            {
                if (Int32.Parse(MemberList[i].PhoneNumber) == memberNum)
                {
                    numResult = true;
                    break;
                }
            }
            return numResult;
        }

        //removes member from list
        public static void deleteMember(int memberID)
        {
            for (int i = 0; i < MemberList.Count(); i++)
            {
                if (MemberList[i].MemberID == memberID)
                {
                    MemberList.Remove(MemberList[i]);
                    break;
                }
            }
        }

        //returns the index of the member object in the list based on the given member id
        public static int getIndexOfMember(int memberID)
        {
            int none = -1;
            for (int i = 0; i < MemberList.Count(); i++)
            {
                if (MemberList[i].MemberID == memberID)
                {
                    return i;
                }
            }
            return none; //will return -1 if member is not in list 
        }

        //returns member id of member object at a given index
        private static int getMemberId(int index)
        {
            if (index < MemberList.Count())
            {
                return MemberList[index].MemberID;
            }
            else { return -1; }
        }

        //returns if member ID exists
        public static bool memberExists(int id)
        {
            bool verified = false;
            for (int i = 0; i<MemberList.Count(); i++)
            {
                if (MemberList[i].MemberID == id)
                {
                    verified = true;
                    break;
                }
            }
            return verified;     
        }

        //returns a new member ID, will be an unused ID 
        public static int getNewMemberID()
        {
            int lastID = -1;
            //search through the list and keep track of which IDs are in use until one that is not is found 
            for (int i = 0; i < MemberList.Count; i++)
            {
                if (lastID != MemberList[i].MemberID - 1)
                {
                    //member object is null 
                    MemberCount++;
                    lastID++;
                    return lastID;
                }
                lastID = MemberList[i].MemberID;
            }
            return MemberCount++;
        }
        #endregion

        #region Provider Functions 

        //adds a provider to the list
        public static void addProvider(Provider newProvider)
        {
            ProviderList.Add(newProvider);
            // writeMembersToFile("Member.xml");

        }

        //returns the index of the provider
        public static int getIndexOfProvider(int providerID)
        {
            int none = -1;
            for (int i = 0; i < ProviderList.Count(); i++)
            {
                if (ProviderList[i].ProviderID == providerID)
                {
                    return i;
                }
            }
            return none; //will return -1 if member is not in list 
        }

        //searches for a provider via the provider ID
        public static Provider searchProvider(int providerId)
        {
            Provider providerResult = new Provider();
            for (int i = 0; i < ProviderList.Count(); i++)
            {
                if (ProviderList[i].ProviderID == providerId)
                {
                    providerResult = ProviderList[i];
                    break;
                }
            }
            return providerResult;
        }

        //searches for provider via provider phone number
        public static bool checkProNum(int providerNum)
        {
            bool numResult = false;

            for (int i = 0; i < ProviderList.Count(); i++)
            {
                if (int.Parse(ProviderList[i].PhoneNumber) == providerNum)
                {
                    numResult = true;
                    break;
                }
            }
            return numResult;
        }

        //deletes a provider from the list
        public static void deleteProvider(int providerId)
        {
            for (int i = 0; i < ProviderList.Count(); i++)
            {
                if (ProviderList[i].ProviderID == providerId)
                {
                    ProviderList.Remove(ProviderList[i]);
                    break;
                }
            }
        }

        //returns if the provider ID exists
        public static bool providerExists(int id)
        {
            bool verified = false;
            for (int i = 0; i < ProviderList.Count(); i++)
            {
                if (ProviderList[i].ProviderID == id)
                {
                    verified = true;
                    break;
                }
            }
            return verified;
        }

        //returns a new provider ID, will be an unused ID 
        public static int getNewProviderID()
        {
            for (int i = 0; i < ProviderList.Count; i++)
            {
                if (ProviderList[i] == null)
                {
                    ProviderCount++;
                    return i;
                }
            }
            return ProviderCount++;
        }

        #endregion

        //creates an abbreviated session and adds it to the list 
        public static void addAbvSession(int memberID, int sessionID, int providerID)
        {
            AbvSession newSession = new AbvSession();

            newSession.MemberID   = memberID;
            newSession.SessionID  = sessionID;
            newSession.ProviderID = providerID;
            AbvSessionList.Add(newSession); 
        }     

        public static void createSessionFile(Session session, int sessionID)
        {
            string fullPath = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string editedPath = fullPath.Replace("\\bin\\Debug", "\\SessionsDirectory\\" + "session" + sessionID.ToString() + ".xml");

            XmlSerializer serial = new XmlSerializer(typeof(Session));
            StreamWriter file = new StreamWriter(editedPath);
            serial.Serialize(file, session);
            file.Close();
        }
      
        public static Session getSessionInfo_sessionID(int sessionID)
        {
            Session sessionInfo = new Session();
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string filePath = path.Replace("\\bin\\Debug", "\\SessionsDirectory\\" +"session"+ sessionID.ToString() +".xml");
            XmlSerializer reader = new XmlSerializer(typeof(Session));
            StreamReader file = new StreamReader(filePath);
            sessionInfo = (Session)reader.Deserialize(file);
            file.Close();
            return sessionInfo;
        }

        public static List<Session> getSessionInfo_memberID(int memberID)
        {
            List<Session> sessionList = new List<Session>();
            for(int i = 0; i < AbvSessionList.Count(); i++ )
            {
                if(AbvSessionList[i].MemberID == memberID)
                {
                    sessionList.Add(getSessionInfo_sessionID(AbvSessionList[i].SessionID));
                }
            }
            return sessionList;
        }

        public static List<Session> getSessionInfo_providerID(int providerID)
        {
            List<Session> sessionList = new List<Session>();
            for (int i = 0; i < AbvSessionList.Count(); i++)
            {
                if (AbvSessionList[i].ProviderID == providerID)
                {
                    sessionList.Add(getSessionInfo_sessionID(AbvSessionList[i].SessionID));
                }
            }
            return sessionList;
        }

        /*weekly session
         * Adds weekly sessions to a file named after date of current week's friday
         */

            public static void generateWeeklySessionIDs(int sessionID)
            {
            string path = getWeeklyFileName();
            File.AppendAllText(path, sessionID.ToString() + Environment.NewLine);
                
             }
        public static string getWeeklyFileName()
        {
            DayOfWeek today = DateTime.Now.DayOfWeek;
            string currentTime = DateTime.Now.ToString("t");

            // if(today != DayOfWeek.Friday && currentTime != ("12:00 PM") ) 
            //  {
            // add to the weekly file with friday's date of current week

            string _date = DateTime.UtcNow.ToString("MM-dd-yyyy");
            DateTime the_Date = DateTime.Parse(_date);
            int num_days = DayOfWeek.Friday - the_Date.DayOfWeek;
            if (num_days < 0) num_days += 7;
            DateTime fridayDate = the_Date.AddDays(num_days);
            string fridayFile = fridayDate.ToString("MM-dd-yyyy");

            //write to file
            string fullPath = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string editedPath = fullPath.Replace("\\bin\\Debug", "\\SessionsDirectory\\weeklysessions\\" + fridayFile + ".txt");
            return editedPath;
        }
        public static Service lookupService(int serviceID)
        {
            Service service = new Service();
            for (int i = 0; i < ServiceList.Count; i++)
            {
                if (ServiceList[i].ID == serviceID)
                {
                    service = ServiceList[i];
                    break;
                }
            }
            return service;
        }


        public static void initilizeServices()
        {
            string fullPath = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string editedPath = fullPath.Replace("\\bin\\Debug", "\\" + "ProviderDirectory.xml");

            XmlSerializer reader = new XmlSerializer(typeof(List<Service>));
            StreamReader file = new StreamReader(editedPath);
            ServiceList = (List<Service>)reader.Deserialize(file); 
            file.Close();
        }

        //is called at midnight on friday
        private static void runWeeklyReport(object state)
        {
            //disable clock object 
            weeklyTimer.Dispose();

            //run report
            emailReport();

            //set up clock for next week
            initilizeWeeklyTimer(); 
        }
        private static void  emailReport()
        {
              
            string[] ids = File.ReadAllLines(getWeeklyFileName());
            Dictionary<int, List<Session>> memberReport = new Dictionary<int, List<Session>>();
            Dictionary<int, List<Session>> providerReport = new Dictionary<int, List<Session>>();
            foreach (string id in ids)
                {
                    int sessionID = Int32.Parse(id);
                    Session thisSession = DataCenter.getSessionInfo_sessionID(sessionID);
                 
                    if (!memberReport.ContainsKey(thisSession.memberID))
                    {
                        memberReport.Add(thisSession.memberID, DataCenter.getSessionInfo_memberID(thisSession.memberID));
                    }
                    if (!providerReport.ContainsKey(thisSession.providerID))
                    {
                        providerReport.Add(thisSession.providerID, DataCenter.getSessionInfo_providerID(thisSession.providerID));
                    }

            }
            //to email members
            foreach (KeyValuePair<int, List<Session>> pair in memberReport)
            {
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    Email email = new Email( searchMember(getSessionInfo_sessionID(pair.Value[i].sessionID).memberID));
                }
            }
            //to email providers
            foreach (KeyValuePair<int, List<Session>> pair in providerReport)
            {
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    Email email = new Email(searchProvider(getSessionInfo_sessionID(pair.Value[i].sessionID).providerID));
                }
            }
        }
        
            

        //sets up the timer object to trigger the weekly reporting on monday 
        private static void initilizeWeeklyTimer()
        {

            //DateTime today = DateTime.Today;
            DateTime now = DateTime.Now;
            //DateTime triggerTime = new DateTime(); 
            DateTime triggerTime = DateTime.Today; 
            int daysUntilFriday = (((int)DayOfWeek.Friday - (int)now.DayOfWeek + 7) % 7) + 1;
            triggerTime = triggerTime.AddDays(daysUntilFriday);

           int msUntilTrigger = (int)((triggerTime - now).TotalMilliseconds);

            weeklyTimer = new System.Threading.Timer(new TimerCallback(runWeeklyReport), null, msUntilTrigger, 0); 
        }

        //To create weekly reports
        public static string getReport(string type)
        {  
            // read sessionids from week file
            string weekFile = DataCenter.getWeeklyFileName();
            string[] ids = File.ReadAllLines(weekFile);
            string display = "";
            // print member report info
            if (type == "member")
            {
                string memberDisplay = "";
                Dictionary<int, List<Session>> memberReport = new Dictionary<int, List<Session>>();

                foreach (string id in ids)
                {
                    int sessionID = Int32.Parse(id);
                    Session thisSession = DataCenter.getSessionInfo_sessionID(sessionID);

                    if (!memberReport.ContainsKey(thisSession.memberID))
                    {
                        memberReport.Add(thisSession.memberID, DataCenter.getSessionInfo_memberID(thisSession.memberID));
                    }



                }
                
                foreach (KeyValuePair<int, List<Session>> pair in memberReport)
                {

                    Member lookUP = DataCenter.searchMember(pair.Key);
                    memberDisplay += "Member name: " + lookUP.FirstName + " " + lookUP.LastName + " \n"
                     + "Member number: " + lookUP.MemberID + "\n"
                     + "Member street address: " + lookUP.Address.street + "\n"
                     + "Member city: " + lookUP.Address.city + "\n"
                     + "Member state: " + lookUP.Address.state + "\n"
                     + "Member ZIP code: " + lookUP.Address.postalCode + "\n\n";
                    for (int i = 0; i < pair.Value.Count; i++)
                    {
                        memberDisplay += "\t" + "Date of Service:" + pair.Value[i].DateOfSession.convToString() + "\n\t"
                         + "Provider name: " + DataCenter.searchProvider(pair.Value[i].providerID).ProviderName + "\n\t"
                          + "Service name: " + pair.Value[i].serviceName + "\n\n";

                    }

                }
                //report_box.Text = display;
                display = memberDisplay;
            }
            // print provider report info
            if (type == "provider")
            {
                string providerDisplay = "";
                Dictionary<int, List<Session>> providerReport = new Dictionary<int, List<Session>>();

                foreach (string id in ids)
                {
                    int sessionID = Int32.Parse(id);
                    Session thisSession = DataCenter.getSessionInfo_sessionID(sessionID);

                    if (!providerReport.ContainsKey(thisSession.providerID))
                    {
                        providerReport.Add(thisSession.providerID, DataCenter.getSessionInfo_memberID(thisSession.providerID));
                    }


                }
                //string display = "";
                int consultations = 0;
                int totalFee = 0;
                foreach (KeyValuePair<int, List<Session>> pair in providerReport)
                {
                    Provider lookUP = DataCenter.searchProvider(pair.Key);

                    providerDisplay += "Provider name: " + lookUP.ProviderName + " \n"
                     + "Provider number: " + lookUP.ProviderID + "\n"
                     + "Provider street address: " + lookUP.Address.street + "\n"
                     + "Provider city: " + lookUP.Address.city + "\n"
                     + "Provider state: " + lookUP.Address.state + "\n"
                     + "Provider ZIP code: " + lookUP.Address.postalCode + "\n";

                    for (int i = 0; i < pair.Value.Count; i++)
                    {
                        consultations = pair.Value.Count;
                        providerDisplay += "\n\t" + "Date of Service:" + pair.Value[i].DateOfSession.convToString() + "\n\t"
                          + "Date and Time data is received: " + pair.Value[i].ComputerDateTime + "\n\t"
                          + "Member name: " + DataCenter.searchMember(pair.Value[i].memberID).FirstName + " "
                          + DataCenter.searchMember(pair.Value[i].memberID).LastName + "\n\t"
                           + "Member number: " + pair.Value[i].memberID.ToString() + "\n\t"
                           + "Service Code: " + pair.Value[i].serviceID + "\n\t";
                        int fee = DataCenter.lookupService(pair.Value[i].serviceID).Fee;
                        providerDisplay += "Fee to be Paid: " + fee.ToString() + "\n";
                        totalFee += fee;
                    }

                    providerDisplay += "Total number of Consultations with members: " + consultations.ToString() + "\n"
                     + "Total fee for week :" + totalFee.ToString() + "\n\n";
                }
                //report_box.Text = display + "\n\n\n";
                display = providerDisplay;
            }
            return display;
        }


    }
}