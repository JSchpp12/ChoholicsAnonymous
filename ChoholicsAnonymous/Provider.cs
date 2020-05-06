using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoholicsAnonymous
{
    public class Provider
    {
        public string  ProviderName { get; set; }
        public string  PhoneNumber  { get; set; }
        public string  Email        { get; set; }
        public int     ProviderID   { get; set; }
        public Address Address      { get; set; }
        
        public Provider()
        {
            this.Address    = new Address();
            this.ProviderID = getNewProviderID();
        }

        private int getNewProviderID()
        {
            return DataCenter.ProviderCount++;
        }

    }
}
