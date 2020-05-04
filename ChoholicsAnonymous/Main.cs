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
            hideAllPanels();
            panel_home.Visible = true;
        }

        #region UI event handlers

        private void Main_Load(object sender, EventArgs e)
        {
            hideAllPanels();
            panel_home.Visible = true;
            //show required items
            initilizeToolbar();
            DataCenter.initilize();

            try
            {
                Date newDate = new Date("07-12-1998"); 
            }catch (InvalidCastException ex)
            {
                //if there are letters where there should be numbers 
            }catch (ArgumentException ex)
            {
                //date is not in valid format of MM-DD-YYYY
            }
        }

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
                    //need to take care of cases like when you add 1 to 12th month so it's not 13 e.t.c
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
            DataCenter.addMember(newMember);
            MessageBox.Show("Member Successfully Added");
        }

        //changes which panel is displayed on the member search page based on which radio button is selected
        private void searchMem_rad_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (button.Checked)
            {
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
            string tag = "";

            try
            {
                //cast dropdown menu items
                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                tag = item.Tag.ToString();
            }
            catch (InvalidCastException ex)
            {
                try
                {
                    //cast buttons
                    ToolStripButton item = (ToolStripButton)sender;
                    tag = item.Tag.ToString();
                }
                catch (Exception oEx)
                {
                    MessageBox.Show("An Unknown Error Occured While Casting Panel Information -- " + oEx.Message);
                }
            }


            hideAllPanels();
            switchPanel(tag);
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

        //want to use to control initilization of panels when they are set to visible  --UNUSED--
        private void panel__VisibleChanged(object sender, EventArgs e)
        {

        }

        //customize the toolbar based on what type of user is logged into system
        private void initilizeToolbar()
        {
            //set the toolbar for whichever user is logged in 
            if (User.Manager == true)
            {
                toolStrip_verifyMember.Visible = false;
                toolStrip_newMember.Visible = false;
                toolStrip_providerDirectory.Visible = false;
                toolStrip_newProvider.Visible = false;
                toolStrip_billing.Visible = false;
                toolStrip_print.Visible = false;

            }
            else if (User.Provider == true)
            {
                toolStrip_newMember.Visible = false;
                toolStrip_newProvider.Visible = false;
                toolStrip_runReports.Visible = false;
                toolStrip_print.Visible = false;
            }
            else if (User.Operator == true)
            {
                toolStrip_billing.Visible = false;
                toolStrip_reporting.Visible = false;
            }
            else
            {
                MessageBox.Show("User information not set correctly upon login");
            }
        }

        //swtich which panel is visible to the user
        private void switchPanel(string buttonTag)
        {
            switch (buttonTag)
            {
                case "mem_search":
                    panel_searchMem.Visible = true;
                    break;
                case "newMember":
                    panel_newMember.Visible = true;
                    break;
                case "billing":
                    panel_billing.Visible = true;
                    break;
                case "verify":
                    panel_verifyMember.Visible = true;
                    break;
                case "newProvider":
                    panel_newProvider.Visible = true;
                    break;
                case "searchProvider":
                    panel_searchProvider.Visible = true;
                    break;
                case "runReports":
                    panel_runReports.Visible = true;
                    break;
                case "viewReports":
                    panel_viewReports.Visible = true;
                    break;
                case "print":
                    panel_print.Visible = true;
                    break;
                default:
                    MessageBox.Show("Panel Not Yet Created...");
                    break;
            }
        }
        #endregion

        //searches for member information and populates GUI with retrieved information 
        private void searchMem_bttn_search_Click(object sender, EventArgs e)
        {
            Member searchResults = DataCenter.searchMember(Int32.Parse(searchMem_inMemID.Text));


            searchMem_res_firstName.Text = searchResults.FirstName;
            searchMem_res_lastName.Text = searchResults.LastName;
            searchMem_res_email.Text = searchResults.Email;
            searchMem_res_street.Text = searchResults.Address.street;
            searchMem_res_city.Text = searchResults.Address.city;
            searchMem_res_state.Text = searchResults.Address.state;
            searchMem_res_post.Text = searchResults.Address.postalCode;
            searchMem_res_ccNum.Text = searchResults.Payment.CardNumber;
            searchMem_res_cvc.Text = searchResults.Payment.Cvc;
            searchMem_res_ccExp.Text = searchResults.Payment.ExpDate.convToString();
            searchMem_res_subExp.Text = searchResults.SubscriptionExpiration.convToString();
            searchMem_res_providerID.Text = searchResults.ProviderID.ToString();
            // searchMem_res_birthday.Text = searchResults.Birthday.convToString();
        }

        private void searchMem_bttn_update_Click(object sender, EventArgs e)
        {
            int memberID = Int32.Parse(searchMem_inMemID.Text);
            //DataCenter.MemberList[Int32.Parse(searchMem_res_inMemID.Text)].MemberID = Int32.Parse(searchMem_res_memID.Text);
            DataCenter.MemberList[memberID].FirstName = searchMem_res_firstName.Text;

            DataCenter.MemberList[memberID].LastName = searchMem_res_lastName.Text;
            DataCenter.MemberList[memberID].Email = searchMem_res_email.Text;
            DataCenter.MemberList[memberID].Address.street = searchMem_res_street.Text;
            DataCenter.MemberList[memberID].Address.city = searchMem_res_city.Text;
            DataCenter.MemberList[memberID].Address.state = searchMem_res_state.Text;
            DataCenter.MemberList[memberID].Address.postalCode = searchMem_res_post.Text;
            DataCenter.MemberList[memberID].Payment.CardNumber = searchMem_res_ccNum.Text;
            DataCenter.MemberList[memberID].Payment.Cvc = searchMem_res_cvc.Text;
            DataCenter.MemberList[memberID].ProviderID = Int32.Parse(searchMem_res_providerID.Text);
            //DataCenter.MemberList[memberID].Birthday = new Date(searchMem_res_birthday.Text.ToString());
            //string month = searchMem_res_ccExp.Text.Substring(0, 2);
            //string year = searchMem_res_ccExp.Text.Substring(2, 2);
            //DataCenter.MemberList[Int32.Parse(searchMem_inMemID.Text)].Payment.ExpDate.Month = Int32.Parse(month);
            //DataCenter.MemberList[Int32.Parse(searchMem_inMemID.Text)].Payment.ExpDate.Year = Int32.Parse(year);


            //still gotta update subscription expiry data, service type, provider id.
        }
        private void searchMem_bttn_removeMem_Click(object sender, EventArgs e)
        {
            int memberID = Int32.Parse(searchMem_inMemID.Text);
            DataCenter.deleteMember(memberID);
        }

        //Provider events

        private void searchProvider_update_Click(object sender, EventArgs e)
        {
            int providerID = Int32.Parse(textBox7.Text);

            DataCenter.ProviderList[providerID].ProviderName = searchProvider_firstName.Text;
            DataCenter.ProviderList[providerID].Address.street = textBox6.Text;
            DataCenter.ProviderList[providerID].Address.state = searchProvider_state.Text;
            DataCenter.ProviderList[providerID].Address.city = textBox8.Text;
            DataCenter.ProviderList[providerID].Address.postalCode = newProvider_postal.Text;

        }
        private void searchProvider_remove_Click(object sender, EventArgs e)
        {

            DataCenter.deleteMember(Int32.Parse(textBox7.Text));
        }
        
        private void newPro_bttn_submit_Click(object sender, EventArgs e)
        {
            Provider newProvider = new Provider();
            newProvider.ProviderName = newProvider_name.Text;
            newProvider.PhoneNumber = newProvider_phoneNumber.Text;
            newProvider.Address.street = newProvider_street.Text;
            newProvider.Address.state = newProvider_state.Text;
            newProvider.Address.city = newProvider_city.Text;
            newProvider.Address.postalCode = newProvider_postal.Text;
            DataCenter.addProvider(newProvider);
            MessageBox.Show("Provider Successfully Added");
        }


        //helper methods
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataCenter.writeToFile("Members.xml", "member");
        }

        private void searchProvider_search_Click(object sender, EventArgs e)
        {
            //No last name or email when provider is added, no phone number in search
            //name could be  better e.g textbox7 is vague
            Provider searchResults = DataCenter.searchProvider(Int32.Parse(textBox7.Text));
            searchProvider_firstName.Text = searchResults.ProviderName;
            //searchProvider_phoneNumber.Text = searchResults.PhoneNumber;
            textBox6.Text = searchResults.Address.street;
            searchProvider_state.Text = searchResults.Address.state;
            textBox8.Text = searchResults.Address.city;
            newProvider_postal.Text = searchResults.Address.postalCode;
        }

        //Event for session/billing
        private void billing_session_submit_Click(object sender, EventArgs e)
        {
            Session newSession = new Session();
            newSession.memberID = Int32.Parse(session_MemberID.Text);
            newSession.providerID = Int32.Parse(User.UserID);
            //newSession.DateOfService = session_serviceDate.Text;
            newSession.serviceID = Int32.Parse(session_serviceCode.Text);
            newSession.serviceName = session_service_Name.Text;
            newSession.Comments = session_Comments.Text;

            //add to abvSessList
            DataCenter.addAbvSession(newSession.memberID, newSession.sessionID, newSession.providerID);
            

            //write to a text file
            DataCenter.createSessionFile(newSession, newSession.sessionID);

        }

        //display "verified" on page if the member id is valid 
        private void verifyMember_verify_Click(object sender, EventArgs e)
        {
            int memID = Int32.Parse(verifyMember_memberID.Text);

            if (DataCenter.memberExists(memID))
            {
                verifyMember_verified.Visible = true; 
                MessageBox.Show("Member with id: " + memID + "exists");
            }
            /*
            else
            {
                MessageBox.Show("Member with id: " + memID + " does not exist");
            }
            */ 
        }
    }  
    
}
