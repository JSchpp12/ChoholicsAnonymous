using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using ChoholicsAnonymous;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace ChoholicsAnonymous
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            hideAllPanels();
            panel_home.Visible = true;
            DataCenter.initilize();
            Date newDate = new Date("07-12-1998"); 
        }

        #region UI event handlers
        //submit the new member information from the form to memory 
        private void newMem_submit_Click_1(object sender, EventArgs e)
        {
            int day, month;

            Member newMember = new Member(true);
            newMember.FirstName = newMem_firstName.Text;
            newMember.LastName = newMem_lastName.Text;
            newMember.Email = newMem_email.Text;
            newMember.PhoneNumber = newMem_phoneNumber.Text;
            newMember.Address.street = newMem_Street.Text;
            newMember.Address.state = newMem_State.Text;
            newMember.Address.city = newMem_City.Text;
            newMember.Address.postalCode = newMem_City.Text;
            newMember.Payment.CardNumber = newMem_ccNum.Text;
            newMember.Payment.Cvc = newMem_cvc.Text;
            //newMember.Birthday = new Date(newMem_birthday.Text.ToString()); 
            string subLength = newMem_subLength.Text;
            DateTime date = DateTime.Now;
            switch (subLength)
            {
                case "1 Month":
                    newMember.SubscriptionExpiration.Month = date.Month + 1;
                    newMember.SubscriptionExpiration.Day = date.Day;
                    newMember.SubscriptionExpiration.Year = date.Year;
                    break;
                case "2 Months":
                    newMember.SubscriptionExpiration.Month = date.Month + 2;
                    newMember.SubscriptionExpiration.Day = date.Day;
                    newMember.SubscriptionExpiration.Year = date.Year;
                    break;
                case "3 Months":
                    newMember.SubscriptionExpiration.Month = date.Month + 3;
                    newMember.SubscriptionExpiration.Day = date.Day;
                    newMember.SubscriptionExpiration.Year = date.Year;
                    break;
                case "8 Months":
                    newMember.SubscriptionExpiration.Month = date.Month + 8;
                    newMember.SubscriptionExpiration.Day = date.Day;
                    newMember.SubscriptionExpiration.Year = date.Year;
                    break;
                case "12 Months":
                    newMember.SubscriptionExpiration.Month = date.Month;
                    newMember.SubscriptionExpiration.Day = date.Day;
                    newMember.SubscriptionExpiration.Year = date.Year + 1;
                    break;
                    //need to add cases for more months
            }
            
            

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

        //changes which panel is displayed on the member search page based on which radio button is selected
        private void searchMem_rad_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (button.Checked) {
                if (button.Tag.ToString() == "memID")
                {
                    searchMem_panel_ID.Visible = true;
                    searchMem_panel_Name.Visible = false;
                }
                else if (button.Tag.ToString() == "memName")
                {
                    searchMem_panel_ID.Visible = false;
                    searchMem_panel_Name.Visible = true;
                }
                else
                {
                    MessageBox.Show("An Unknown Error Has Occured when changing panels");
                }
            }
        }

        #region navigation menu handlers 

        //swtich between panels when navigation toolbar is clicked 
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            hideAllPanels();

            switch (item.Tag)
            {
                case "mem_search":
                    panel_searchMem.Visible = true;
                    break;
                case "newMember":
                    panel_newMember.Visible = true;
                    break;
                default:
                    MessageBox.Show("Panel Not Yet Created...");
                    break;
            }
            Console.Write(sender.ToString());
        }
        #endregion 

        #endregion

        #region Supporting Methods 

        //hides all panels currently in the control (FORM)
        private void hideAllPanels()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Panel)
                    c.Visible = false;
            }
        }

        //want to use to control initilization of panels when they are set to visible 
        private void panel__VisibleChanged(object sender, EventArgs e)
        {

        }
        #endregion



        //searches for member information and populates GUI with retrieved information 
        private void searchMem_bttn_search_Click(object sender, EventArgs e)
        {
             Member searchResults = DataCenter.searchMember(Int32.Parse(searchMem_inMemID.Text));

            
             searchMem_res_firstName.Text  = searchResults.FirstName;
             searchMem_res_lastName.Text   = searchResults.LastName;
             searchMem_res_email.Text      = searchResults.Email;
             searchMem_res_street.Text     = searchResults.Address.street;
             searchMem_res_city.Text       = searchResults.Address.city;
             searchMem_res_state.Text      = searchResults.Address.state;
             searchMem_res_post.Text       = searchResults.Address.postalCode;
             searchMem_res_ccNum.Text      = searchResults.Payment.CardNumber;
             searchMem_res_cvc.Text        = searchResults.Payment.Cvc;
             searchMem_res_birthday.Text = searchResults.Birthday.convToString(); 
        }

        private void searchMem_bttn_update_Click(object sender, EventArgs e)
        {
            int memberID = Int32.Parse(searchMem_inMemID.Text);
            //DataCenter.memberList[Int32.Parse(searchMem_res_inMemID.Text)].MemberID = Int32.Parse(searchMem_res_memID.Text);
            DataCenter.memberList[memberID].FirstName = searchMem_res_firstName.Text;
            
            DataCenter.memberList[memberID].LastName = searchMem_res_lastName.Text;
            DataCenter.memberList[memberID].Email = searchMem_res_email.Text;
            DataCenter.memberList[memberID].Address.street = searchMem_res_street.Text;
            DataCenter.memberList[memberID].Address.city = searchMem_res_city.Text;
            DataCenter.memberList[memberID].Address.state = searchMem_res_state.Text;
            DataCenter.memberList[memberID].Address.postalCode = searchMem_res_post.Text;
            DataCenter.memberList[memberID].Payment.CardNumber = searchMem_res_ccNum.Text;
            DataCenter.memberList[memberID].Payment.Cvc = searchMem_res_cvc.Text;
            DataCenter.memberList[memberID].Birthday = new Date(searchMem_res_birthday.Text.ToString()); 
            //string month = searchMem_res_ccExp.Text.Substring(0, 2);
            //string year = searchMem_res_ccExp.Text.Substring(2, 2);
            //DataCenter.memberList[Int32.Parse(searchMem_inMemID.Text)].Payment.ExpDate.Month = Int32.Parse(month);
            //DataCenter.memberList[Int32.Parse(searchMem_inMemID.Text)].Payment.ExpDate.Year = Int32.Parse(year);


            //still gotta update subscription expiry data, service type, provider id.
        }

        private void searchMem_bttn_removeMem_Click(object sender, EventArgs e)
        {
            DataCenter.deleteMember(Int32.Parse(searchMem_res_memID.Text));
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
           //DataCenter.writeMembersToFile("Member.xml");
        }
    }
}


