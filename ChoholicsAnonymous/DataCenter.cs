using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoholicsAnonymous
{

    static class DataCenter
    {
        public static int MemberCount { get; set; }
        public static int ProviderCount { get; set; }
        private static SortedSet<Member> memberSet = new SortedSet<Member>();
        private static SortedSet<Provider> providerSet = new SortedSet<Provider>(); 

        //add a member to the data set
        public static void AddMember(Member newMember)
        {
            memberSet.Add(newMember); 
        }

        /*
        public static Member searchMember(string firstName, string lastName)
        {

        }

        public static Member searchMember(int memberID)
        {

        }
        */ 
    }
}
