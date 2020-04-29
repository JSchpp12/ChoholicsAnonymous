using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoholicsAnonymous
{
    class Session
    {
        int sessionID { get; set; }
        int serviceType { get; set; }
        int memberID { get; set; }
        int providerID { get; set; }
        Date DateOfSession { get; set; }
        Date TimeOfCreation { get; set; } //time when the session object is created 
        string comments { get; set; }

        public Session()
        {
            String currDate              = DateTime.Now.ToString();
            DateTime dateValue           = (Convert.ToDateTime(currDate.ToString()));
            this.DateOfSession           = new Date();
            this.TimeOfCreation          = new Date();
            this.TimeOfCreation.Day      = dateValue.Day;
            this.TimeOfCreation.Month    = dateValue.Month;
            this.TimeOfCreation.Year     = dateValue.Year; 
        }
       
    }
}
