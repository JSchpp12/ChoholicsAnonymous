using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoholicsAnonymous
{
    class Session
    {
        public int    sessionID      { get; set; }
        public int    serviceType    { get; set; }
        public int    memberID       { get; set; }
        public int    providerID     { get; set; }
        public Date   DateOfSession  { get; set; }
        public Date   TimeOfCreation { get; set; } //time when the session object is created 
        public string comments       { get; set; }

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
