using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ChoholicsAnonymous
{

    static class DataCenter
    {
        public static int MemberCount { get; set; }
        public static int SessionCount { get; set; }
        public static int ProviderCount { get; set; }
        public static List<Member> MemberList = new List<Member>();
        public static List<Provider> ProviderList = new List<Provider>();
        public static List<AbvSession> abvSessList = new List<AbvSession>();

        //add a member to the data set
        public static void addMember(Member newMember)
        {
            MemberList.Add(newMember);
            //writeMembersToFile("Member.xml");

        }

        public static void writeToFile(string fileName, string dataType)
        {

            string fullPath = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string editedPath = fullPath.Replace("\\bin\\Debug", "\\" + fileName);
            if (dataType == "member")
            {
                XmlSerializer serial = new XmlSerializer(typeof(List<Member>));
                StreamWriter file = new StreamWriter(editedPath);
                serial.Serialize(file, MemberList);
                file.Close();
            }
            else if (dataType == "provider")
            {
                XmlSerializer serial = new XmlSerializer(typeof(List<Provider>));
                StreamWriter file = new StreamWriter(editedPath);
                serial.Serialize(file, ProviderList);
                file.Close();
            }
        }

        //read all files and build structures for data storage 
        public static void initilize()
        {
            readMembers("Members.xml");
            SessionCount = getSessionCount("sessionCount.txt");
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
            else
            {
                //fail state
                return false;
            }
        }

        public static Member searchMember(int memberId)
        {
            Member memberResult = new Member(false);
            for (int i = 0; i < MemberList.Count(); i++)
            {
                if (MemberList[i].MemberID == memberId)
                {
                    memberResult = MemberList[i];
                    return MemberList[i];
               
                }
            }
            return memberResult;

        }


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
        private static int getIndexOfMember(int memberID)
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
                return MemberList[index].MemberID;
            else
                return -1;
        }

        private static void readMembers(string fileName)
        {
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string filePath = path.Replace("\\bin\\Debug", "\\" + fileName);
            XmlSerializer reader = new XmlSerializer(typeof(List<Member>));
            StreamReader file = new StreamReader(filePath);
            DataCenter.MemberList = (List<Member>)reader.Deserialize(file);
            file.Close();
        }

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
        public static void addProvider(Provider newProvider)
        {
            ProviderList.Add(newProvider);
            // writeMembersToFile("Member.xml");

        }

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

        //creates an abbreviated session and adds it to the list 
        public static void addAbvSession(int memberID, int sessionID, int providerID)
        {
            AbvSession newSession = new AbvSession();
            newSession.MemberID = memberID;
            newSession.SessionID = sessionID;
            newSession.ProviderID = providerID;
            abvSessList.Add(newSession); 
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
            string sessionCountFile = fullPath.Replace("\\bin\\Debug", "\\SessionsDirectory\\" + "sessionCount.txt");
            using (StreamWriter sw = new StreamWriter(sessionCountFile))
            {

               sw.WriteLine(SessionCount);
                
            }

        }

        public static int getSessionCount(string fileName)
        {
            string fullPath = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string sessionCountFile = fullPath.Replace("\\bin\\Debug", "\\SessionsDirectory\\" + fileName);
            int count;
            using (StreamReader sr = new StreamReader(sessionCountFile))
            {
                count = sr.Read();
                
            }
            return count;
        }

    }
}