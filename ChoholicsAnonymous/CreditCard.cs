using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoholicsAnonymous
{
    class CreditCard
    {
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public Date ExpDate { get; set; }

        public CreditCard()
        {
            this.ExpDate = new Date(); 
        }
    }
}
