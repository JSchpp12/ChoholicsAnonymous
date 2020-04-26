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
        public static Dictionary<int, Member> memberSet = new Dictionary<int, Member>();
        private static HashSet<Provider> providerSet = new HashSet<Provider>();

        //add a member to the data set
        public static void AddMember(Member newMember)
        {
            memberSet.Add(newMember.MemberID, newMember); 
        }

        /*
        public static Member searchMember(string firstName, string lastName)
        {

        }
        */

        public static Member searchMember(int memberID)
        {
            Member memberObj = new Member();
            //DataCenter.memberSet.Contains(object.Member())
            if(DataCenter.memberSet.ContainsKey(memberID))
            {
                memberObj = memberSet[memberID];
            }


            return memberObj;
        }

        public static  void deleteMember(int memberID)
        {
            DataCenter.memberSet.Remove(memberID);
        }

       
        
    }
}
