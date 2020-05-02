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
        public static int ProviderCount { get; set; }
        public static List<Member> memberList = new List<Member>();
        public static List<Provider> providerList = new List<Provider>();

        //add a member to the data set
        public static void AddMember(Member newMember)
        {
            memberList.Add(newMember);
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
                serial.Serialize(file, memberList);
                file.Close();
            }
            else if (dataType == "provider")
            {
                XmlSerializer serial = new XmlSerializer(typeof(List<Provider>));
                StreamWriter file = new StreamWriter(editedPath);
                serial.Serialize(file, providerList);
                file.Close();
            }
        }

        //read all files and build structures for data storage 
        public static void initilize()
        {
            readMembers("Member.xml");
        }

        //update the member information in the list 
        public static bool updateMember(Member updatedMember)
        {
            int indexOfMember = getIndexOfMember(updatedMember.MemberID);
            if (indexOfMember != -1)
            {
                memberList[indexOfMember] = updatedMember;
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
            Member memberResult = new Member();
            for (int i = 0; i < memberList.Count(); i++)
            {
                if (memberList[i].MemberID == memberId)
                {
                    memberResult = memberList[i];
                    break;
                }
            }
            return memberResult;
        }


        public static void deleteMember(int memberID)
        {
            for (int i = 0; i < memberList.Count(); i++)
            {
                if (memberList[i].MemberID == memberID)
                {
                    memberList.Remove(memberList[i]);
                    break;
                }
            }
        }

        //returns the index of the member object in the list based on the given member id
        private static int getIndexOfMember(int memberID)
        {
            int none = -1;
            for (int i = 0; i < memberList.Count(); i++)
            {
                if (memberList[i].MemberID == memberID)
                {
                    return i;
                }
            }
            return none; //will return -1 if member is not in list 
        }

        //returns member id of member object at a given index
        private static int getMemberId(int index)
        {
            if (index < memberList.Count())
                return memberList[index].MemberID;
            else
                return -1;
        }

        private static void readMembers(string fileName)
        {
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string filePath = path.Replace("\\bin\\Debug", "\\" + fileName);
            XmlSerializer reader = new XmlSerializer(typeof(List<Member>));
            StreamReader file = new StreamReader(filePath);
            DataCenter.memberList = (List<Member>)reader.Deserialize(file);
            file.Close();
        }

        //Provider functions start here
        public static void addProvider(Provider newProvider)
        {
            providerList.Add(newProvider);
            // writeMembersToFile("Member.xml");

        }
    }
}