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

        private void button1_Click(object sender, EventArgs e)
        {
            Member newMember = new Member();
            newMember.firstName = newMem_firstName.Text;
            newMember.lastName = newMem_lastName.Text;
            newMember.email = newMem_email.Text; 
            newMember.phoneNumber = newMem_phoneNumber.Text;
            newMember.address.street = newMem_addStreet.Text;
            newMember.address.state = newMem_addState.Text;
            newMember.address.city = newMem_addCity.Text;
            newMember.address.postalCode = newMem_addCity.Text;
            newMember.payment.cardNumber = newMem_ccNumber.Text; 
            //still need to fill out date information 
        }
    }
}
