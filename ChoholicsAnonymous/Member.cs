using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoholicsAnonymous
{
    class Member
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public int memberID { get; set; }
        public int providerID { get; set; }
        public int ccNumber { get; set; }
        public Address address { get; set; } 
    }
}
