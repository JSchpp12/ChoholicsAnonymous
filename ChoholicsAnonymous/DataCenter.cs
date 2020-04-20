using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChoholicsAnonymous
{

    static class DataCenter
    {
        public static int MemberCount { get; set; }
        public static int ProviderCount { get; set; }
        public static HashSet<Member> memberSet = new HashSet<Member>();
        private static HashSet<Provider> providerSet = new HashSet<Provider>();

        //add a member to the data set
        public static void AddMember(Member newMember)
        {
            memberSet.Add(newMember); 
        }

        /*
        public static Member searchMember(string firstName, string lastName)
        {

        }
        */

        public static Member searchMember(int memberID)
        {
            Member memberObj = new Member();
            //XmlDocument doc = new XmlDocument();
            //doc.Load("Member.xml");
            //foreach (XmlNode node in doc.DocumentElement)
            //{
            //    int id = Int32.Parse(node.Attributes[0].InnerText);
            //    if(id == memberID)
            //    {
            //        foreach (XmlNode child in doc.ChildNodes)
            //        {
            //            Console.WriteLine(child.InnerText);
            //        }
            //    }
            //}

            return memberObj;
        }
        
    }
}
