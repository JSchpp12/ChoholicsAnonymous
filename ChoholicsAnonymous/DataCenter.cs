using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        public static List<Service> ServiceList = new List<Service>();

        private static Timer weeklyTimer = new Timer(runWeeklyReport);  

        //add a member to the data set
        public static void addMember(Member newMember)
        {
            MemberList.Add(newMember);
            //writeMembersToFile("Member.xml");

        }

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
            Session       sessionFromsessionID = getSessionInfo_sessionID(1);
            List<Session> sessions_memID       = getSessionInfo_memberID(44);
            List<Session> sessions_provID      = getSessionInfo_providerID(55);
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

//Member functions starts here

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

//Provider functions start here

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

            //to write number of sessions in file
            //string sessionCountFile = fullPath.Replace("\\bin\\Debug", "\\SessionsDirectory\\" + "sessionCount.txt");
            //using (StreamWriter sw = new StreamWriter(sessionCountFile))
            //{

            //   sw.WriteLine(SessionCount);

            //}
        }
      

        //public static int getSessionCount(string fileName)
        //{
        //    string fullPath = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
        //    string sessionCountFile = fullPath.Replace("\\bin\\Debug", "\\SessionsDirectory\\" + fileName);
        //    string value;
        //    using (StreamReader sr = new StreamReader(sessionCountFile))
        //    {
        //        value = sr.ReadLine();
                
        //    }
        //    return Convert.ToInt32(value);
        //}

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

                File.AppendAllText(editedPath , sessionID.ToString() + Environment.NewLine);
                

          //  }         
                
             //else if it's friday past midnigt, write into next week session file
             //else if ()
             //   {

             //   }

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

        //returns a new provider ID, will be an unused ID 
        public static int getNewProviderID()
        {
            for (int i = 0; i<ProviderList.Count; i++)
            {
                if (ProviderList[i] == null)
                {
                    ProviderCount++; 
                    return i; 
                }
            }
            return ProviderCount++; 
        }

        //will run the weekly report 
        private static void runWeeklyReport(object state)
        {
            //disable clock object 
            weeklyTimer.Dispose();

            //run report 

            //set up clock for next week
            initilizeWeeklyTimer(); 
        }

        //sets up the timer object to trigger the weekly reporting on monday 
        private static void initilizeWeeklyTimer()
        {
            DateTime today = DateTime.Today;
            DateTime triggerTime = new DateTime(); 
            int daysUntilFriday = ((int)DayOfWeek.Friday - (int)today.DayOfWeek + 7) % 7;
            triggerTime = today.AddDays(daysUntilFriday);

            int msUntilTrigger = (int)((triggerTime - today).TotalMilliseconds);

            weeklyTimer = new Timer(runWeeklyReport);
            weeklyTimer.Change(msUntilTrigger, Timeout.Infinite); 
        }
    }
}