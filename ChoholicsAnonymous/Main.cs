using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChoholicsAnonymous
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            
        }
        
        #region UI event handlers
        //submit the new member information from the form to memory 
        private void newMem_submit_Click_1(object sender, EventArgs e)
        {
            int day, month; 

            Member newMember = new Member();
            newMember.FirstName              = newMem_firstName.Text;
            newMember.LastName               = newMem_lastName.Text;
            newMember.Email                  = newMem_email.Text;
            newMember.PhoneNumber            = newMem_phoneNumber.Text;
            newMember.Address.street         = newMem_addStreet.Text;
            newMember.Address.state          = newMem_addState.Text;
            newMember.Address.city           = newMem_addCity.Text;
            newMember.Address.postalCode     = newMem_addCity.Text;
            newMember.Payment.CardNumber     = newMem_ccNumber.Text;
            newMember.Payment.Cvc            = newMem_cvc.Text;

            if (int.TryParse(newMem_expMonth.Text, out month))
            {
                newMember.Payment.ExpDate.Month = month;
            }
            else
            {
                //throw error 
                MessageBox.Show("Expiration Month is not in a valid form");
                return; 
            }
            if (int.TryParse(newMem_expDay.Text, out day))
            {
                newMember.Payment.ExpDate.Day = day;
            }
            else
            {
                //throw error 
                MessageBox.Show("Expiration Day is not in a valid form");
                return; 
            }
            DataCenter.AddMember(newMember);
            MessageBox.Show("Member Successfully Added"); 
        }
        #endregion
        #region Supporting Methods 
        #endregion
    }

}
