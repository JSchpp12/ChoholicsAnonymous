using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoholicsAnonymous
{
    public class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int MemberID { get; set; }
        public int ProviderID { get; set; }
        public Address Address { get; set; }
        public CreditCard Payment { get; set; }
        public Date SubscriptionExpiration { get; set; } //when the member's subscription will expire 
        public Date SubscriptionStart { get; set; } //the day the member started their subscription

        public Date Birthday { get; set; }
        public Member()
        {

        }
        public Member(bool newMember)
        {
            this.SubscriptionStart = new Date();
            this.SubscriptionExpiration = new Date();
            this.Payment = new CreditCard();
            this.Address = new Address();
            this.Birthday = new Date();

            if (newMember)
            {
                String currDate = DateTime.Now.ToString();
                DateTime dateValue = (Convert.ToDateTime(currDate.ToString()));
                this.SubscriptionStart.Day = dateValue.Day;
                this.SubscriptionStart.Month = dateValue.Month;
                this.SubscriptionStart.Year = dateValue.Year;
                this.MemberID = getNewMemberID();
            }
        }

        private int getNewMemberID()
        {
            return DataCenter.SessionCount++;
        }
    } 
}
