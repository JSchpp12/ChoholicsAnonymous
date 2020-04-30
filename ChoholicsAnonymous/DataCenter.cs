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
        private static HashSet<Provider> providerSet = new HashSet<Provider>();


        //add a member to the data set
        public static void AddMember(Member newMember)
        {
            memberList.Add(newMember);
            writeMembersToFile("Member.xml");
            
        }

        public static void writeMembersToFile(string fileName)
        {
            
             string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
             string temp = path.Replace("\\bin\\Debug", "\\"+ fileName);
             XmlSerializer serial = new XmlSerializer(typeof(List<Member>));
             StreamWriter file = new StreamWriter(temp);
             serial.Serialize(file, DataCenter.memberList);
             file.Close();

        }
       

        
        public static Member searchMember(int memberId)
        {
            Member memberResult = new Member();
            for (int i = 0; i<memberList.Count(); i++)
            {
                if (memberList[i].MemberID == memberId)
                {
                    memberResult = memberList[i];
                    break;
                }
            }
            return memberResult;
        }
        

        public static  void deleteMember(int memberId)
        {
                for (int i = 0; i < DataCenter.memberList.Count(); i++)
            {
                if (memberList[i].MemberID == memberId)
                {
                    DataCenter.memberList.Remove(memberList[i]);
                    break;
                }
            }
        }

        
       
        
    }
}
