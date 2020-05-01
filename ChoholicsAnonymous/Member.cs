using System;
using System.Collections;
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
        public int CCNumber { get; set; }
        public Address Address { get; set; } 
        public CreditCard Payment { get; set; }
        public Date SubscriptionExpiation { get; set; } //when the member's subscription will expire 
        public Date SubscriptionStart { get; set; } //the day the member started their subscription

        public Member()
        {
            this.SubscriptionStart       = new Date();
            this.SubscriptionExpiation   = new Date();
            this.Payment                 = new CreditCard();
            this.Address                 = new Address(); 
        }

        //if true will include the current date in the member object and give the member a new id
        public Member(bool newMember)
        {
            if (newMember)
            {
                //creating a new member for submission
                String currDate                  = DateTime.Now.ToString();
                DateTime dateValue               = (Convert.ToDateTime(currDate.ToString()));
                this.SubscriptionStart           = new Date();
                this.SubscriptionExpiation       = new Date();
                this.Payment                     = new CreditCard();
                this.Address                     = new Address();
                this.SubscriptionStart.Day       = dateValue.Day;
                this.SubscriptionStart.Month     = dateValue.Month;
                this.SubscriptionStart.Year      = dateValue.Year;
                this.MemberID                    = getNewMemberID();
            }
            else
            {
                this.SubscriptionStart       = new Date();
                this.SubscriptionExpiation   = new Date();
                this.Payment                 = new CreditCard();
                this.Address                 = new Address();
            }

        }
        private int getNewMemberID()
        {
            return DataCenter.MemberCount++; 
        }
    }
}
