using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoholicsAnonymous
{
    public class Session
    {
       public int sessionID { get; set; }
       public int serviceType { get; set; } //may not need it
       public int memberID { get; set; }
       public int providerID { get; set; }
       public Date DateOfSession { get; set; }
       public Date TimeOfCreation { get; set; } //time when the session object is created 
       public string Comments { get; set; }
       public string DateOfService { get; set; }
       public int serviceID { get; set; }
       public string serviceName { get; set; }
        public Session()
        {
            String currDate              = DateTime.Now.ToString();
            DateTime dateValue           = (Convert.ToDateTime(currDate.ToString()));
            this.DateOfSession           = new Date();
            this.TimeOfCreation          = new Date();
            this.TimeOfCreation.Day      = dateValue.Day;
            this.TimeOfCreation.Month    = dateValue.Month;
            this.TimeOfCreation.Year     = dateValue.Year;
            this.sessionID               = getSessionID();
        }

        private int getSessionID()
        {
            return DataCenter.MemberCount++;
        }
        //this will add the new session object to the end of the sessions file
        //could also add abvSession object to list in datacenter
        //public void WriteToFile()
        //{
        //    //
        //}

    }
}
