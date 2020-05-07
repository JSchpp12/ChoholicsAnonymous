using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Threading; 

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
            }catch (InvalidCastException )
            {
                //if there are letters where there should be numbers 
            }catch (ArgumentException )
            {
                //date is not in valid format of MM-DD-YYYY
            }
        }

        //submit the new member information from the form to memory 
        private void newMem_submit_Click_1(object sender, EventArgs e)
        {
            int day, month;

            Member newMember = new Member(true);
            int    memNum    = Int32.Parse(newMem_phoneNumber.Text);

            //checks if provider already exists
            if (DataCenter.checkMemNum(memNum)) { MessageBox.Show("Member Already Exists"); }
            else
            { 
                newMember.FirstName          = newMem_firstName.Text;
                newMember.LastName           = newMem_lastName.Text;
                newMember.Email              = newMem_email.Text;
                newMember.PhoneNumber        = newMem_phoneNumber.Text;
                newMember.Address.street     = newMem_Street.Text;
                newMember.Address.state      = newMem_State.Text;
                newMember.Address.city       = newMem_City.Text;
                newMember.Address.postalCode = newMem_City.Text;
                newMember.Payment.CardNumber = newMem_ccNum.Text;
                newMember.Payment.Cvc        = newMem_cvc.Text;

                string   subLength = newMem_subLength.Text;
                DateTime date      = DateTime.Now;

                switch (subLength)
                {
                    case "1 Month":
                        newMember.SubscriptionExpiration.Month = date.Month + 1;
                        newMember.SubscriptionExpiration.Day   = date.Day;
                        newMember.SubscriptionExpiration.Year  = date.Year;
                        break;
                    case "2 Months":
                        newMember.SubscriptionExpiration.Month = date.Month + 2;
                        newMember.SubscriptionExpiration.Day   = date.Day;
                        newMember.SubscriptionExpiration.Year  = date.Year;
                        break;
                    case "3 Months":
                        newMember.SubscriptionExpiration.Month = date.Month + 3;
                        newMember.SubscriptionExpiration.Day   = date.Day;
                        newMember.SubscriptionExpiration.Year  = date.Year;
                        break;
                    case "8 Months":
                        newMember.SubscriptionExpiration.Month = date.Month + 8;
                        newMember.SubscriptionExpiration.Day   = date.Day;
                        newMember.SubscriptionExpiration.Year  = date.Year;
                        break;
                    case "12 Months":
                        newMember.SubscriptionExpiration.Month = date.Month;
                        newMember.SubscriptionExpiration.Day   = date.Day;
                        newMember.SubscriptionExpiration.Year  = date.Year + 1;
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
                resetPanel(panel_newMember);
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
            catch (InvalidCastException )
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


            //hideAllPanels();
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
                if (c is Panel) { c.Visible = false; }
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
                toolStrip_verifyMember.Visible      = false;
                toolStrip_newMember.Visible         = false;
                toolStrip_providerDirectory.Visible = false;
                toolStrip_newProvider.Visible       = false;
                toolStrip_billing.Visible           = false;
                toolStrip_print.Visible             = false;

            }
            else if (User.Provider == true)
            {
                toolStrip_newMember.Visible   = false;
                toolStrip_newProvider.Visible = false;
                toolStrip_runReports.Visible  = false;
                toolStrip_print.Visible       = false;
            }
            else if (User.Operator == true)
            {
                toolStrip_billing.Visible   = false;
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
                    hideAllPanels();
                    panel_searchMem.Visible = true;
                    break;
                case "newMember":
                    hideAllPanels();
                    panel_newMember.Visible = true;
                    break;
                case "billing":
                    hideAllPanels();
                    panel_billing.Visible = true;
                    break;
                case "verify":
                    hideAllPanels();
                    panel_verifyMember.Visible = true;
                    break;
                case "newProvider":
                    hideAllPanels();
                    panel_newProvider.Visible = true;
                    break;
                case "searchProvider":
                    hideAllPanels();
                    panel_searchProvider.Visible = true;
                    break;
                case "runReports":
                    hideAllPanels();
                    panel_runReports.Visible = true;
                    break;
                case "viewReports":
                    hideAllPanels();
                    panel_viewReports.Visible = true;
                    break;
                case "home":
                    hideAllPanels();
                    panel_home.Visible = true; 
                    break;
                case "logout":
                    //go back to login screen
                    Form newLogin = new Login();
                    newLogin.Show();
                    this.Hide();
                    break;
                case "directory":
                    Form newDirectory = new ProviderDirectory();
                    newDirectory.Show(); 
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
            int    memID         = Int32.Parse(searchMem_inMemID.Text);

            if (!DataCenter.memberExists(memID)) { MessageBox.Show("Member Does Not Exist"); }
            else
            {
                searchMem_res_firstName.Text  = searchResults.FirstName;
                searchMem_res_lastName.Text   = searchResults.LastName;
                searchMem_res_email.Text      = searchResults.Email;
                searchMem_res_street.Text     = searchResults.Address.street;
                searchMem_res_city.Text       = searchResults.Address.city;
                searchMem_res_state.Text      = searchResults.Address.state;
                searchMem_res_post.Text       = searchResults.Address.postalCode;
                searchMem_res_ccNum.Text      = searchResults.Payment.CardNumber;
                searchMem_res_cvc.Text        = searchResults.Payment.Cvc;
                searchMem_res_ccExp.Text      = searchResults.Payment.ExpDate.convToString();
                searchMem_res_subExp.Text     = searchResults.SubscriptionExpiration.convToString();
                searchMem_res_providerID.Text = searchResults.ProviderID.ToString();
                //searchMem_res_birthday.Text = searchResults.Birthday.convToString();
             }
        }

        private void searchMem_bttn_update_Click(object sender, EventArgs e)
        {
            //verify member ID before attempting update 

            int memberIndex = DataCenter.getIndexOfMember(Int32.Parse(searchMem_inMemID.Text));
            
            DataCenter.MemberList[memberIndex].FirstName              = searchMem_res_firstName.Text;
            DataCenter.MemberList[memberIndex].LastName               = searchMem_res_lastName.Text;
            DataCenter.MemberList[memberIndex].Email                  = searchMem_res_email.Text;
            DataCenter.MemberList[memberIndex].PhoneNumber            = searchMem_phone.Text;
            DataCenter.MemberList[memberIndex].Address.street         = searchMem_res_street.Text;
            DataCenter.MemberList[memberIndex].Address.city           = searchMem_res_city.Text;
            DataCenter.MemberList[memberIndex].Address.state          = searchMem_res_state.Text;
            DataCenter.MemberList[memberIndex].Address.postalCode     = searchMem_res_post.Text;
            DataCenter.MemberList[memberIndex].Payment.CardNumber     = searchMem_res_ccNum.Text;
            DataCenter.MemberList[memberIndex].Payment.Cvc            = searchMem_res_cvc.Text;
            DataCenter.MemberList[memberIndex].ProviderID             = Int32.Parse(searchMem_res_providerID.Text);
            DataCenter.MemberList[memberIndex].SubscriptionExpiration = new Date(searchMem_res_subExp.Text);
            DataCenter.MemberList[memberIndex].Payment.ExpDate        = new Date(searchMem_res_ccExp.Text);

            MessageBox.Show("Member Successfully Updated");
            resetPanel(searchMem_panel_Results);           

            //still gotta update subscription expiry data, service type, provider id.
        }

        private void searchMem_bttn_removeMem_Click(object sender, EventArgs e)
        {
            int memberID = Int32.Parse(searchMem_inMemID.Text);
            DataCenter.deleteMember(memberID);
            MessageBox.Show("Member Successfully Deleted");
            resetPanel(searchMem_panel_Results);
        }

        #region Provider Events

        private void searchProvider_update_Click(object sender, EventArgs e)
        {
            int providerIndex = DataCenter.getIndexOfProvider(Int32.Parse(searchProvider_providerID.Text));
           
            DataCenter.ProviderList[providerIndex].ProviderName       = searchProvider_firstName.Text;
            DataCenter.ProviderList[providerIndex].PhoneNumber        = searchProvider_phone.Text;
            DataCenter.ProviderList[providerIndex].Email              = searchProvider_email.Text;
            DataCenter.ProviderList[providerIndex].Address.street     = searchProvider_street.Text;
            DataCenter.ProviderList[providerIndex].Address.state      = searchProvider_state.Text;
            DataCenter.ProviderList[providerIndex].Address.city       = searchProvider_city.Text;
            DataCenter.ProviderList[providerIndex].Address.postalCode = searchProvider_postalCode.Text;
            MessageBox.Show("Provider Updated Successfully");
            resetPanel(panel_searchProvider);
        }

        private void searchProvider_remove_Click(object sender, EventArgs e)
        {

            DataCenter.deleteProvider(Int32.Parse(searchProvider_providerID.Text));
            MessageBox.Show("Provider Successfully Deleted");
            resetPanel(panel_searchProvider);
        }
        
        //adding a new provider
        private void newPro_bttn_submit_Click(object sender, EventArgs e)
        {
            Provider newProvider = new Provider();
            int      providerNum = int.Parse(newProvider_phone.Text);

            //checks if provider already exists
            if (DataCenter.checkProNum(providerNum)) { MessageBox.Show("Provider Already Exists"); }
            else
            {
                newProvider.ProviderName       = newProvider_name.Text;
                newProvider.PhoneNumber        = newProvider_phone.Text;
                newProvider.Address.street     = newProvider_street.Text;
                newProvider.Address.state      = newProvider_state.Text;
                newProvider.Address.city       = newProvider_city.Text;
                newProvider.Address.postalCode = newProvider_postal.Text;
                DataCenter.addProvider(newProvider);
                MessageBox.Show("Provider Successfully Added");
                resetPanel(panel_newProvider);
            }
        }

        //searching for a provider
        private void searchProvider_search_Click(object sender, EventArgs e)
        {
            Provider searchResults = DataCenter.searchProvider(Int32.Parse(searchProvider_providerID.Text));
            int      proID         = Int32.Parse(searchProvider_providerID.Text);

            //if provider DNE, pop up explaining, else provider info appears
            if (!DataCenter.providerExists(proID)) { MessageBox.Show("Provider Does Not Exist"); }
            else
            {
                searchProvider_firstName.Text  = searchResults.ProviderName;
                searchProvider_phone.Text      = searchResults.PhoneNumber;
                searchProvider_street.Text     = searchResults.Address.street;
                searchProvider_state.Text      = searchResults.Address.state;
                searchProvider_city.Text       = searchResults.Address.city;
                searchProvider_postalCode.Text = searchResults.Address.postalCode;
                resetPanel(panel_searchProvider);
            }
        }

        #endregion

        //helper methods
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataCenter.writeToFile("Members.xml", "member");
            DataCenter.writeToFile("Providers.xml", "provider");
            DataCenter.writeToFile("abvSessions.xml", "abvSession");
        }
                
        //Event for session/billing
        private void billing_session_submit_Click(object sender, EventArgs e)
        {
            Session newSession = new Session();

            newSession.memberID   = Int32.Parse(session_MemberID.Text);
            newSession.providerID = Int32.Parse(User.UserID);
            
            try
            {
                Date newDate = new Date(session_serviceDate.Text);
                newSession.DateOfSession = newDate;
            }
            catch (InvalidCastException)
            {
                //if there are letters where there should be numbers 
            }
            catch (ArgumentException )
            {
                //date is not in valid format of MM-DD-YYYY
            }
            
            newSession.serviceID   = Int32.Parse(session_serviceCode.Text);
            newSession.serviceName = session_service_Name.Text;
            newSession.Comments    = session_Comments.Text;

            //add to AbvSessionList
            DataCenter.addAbvSession(newSession.memberID, newSession.sessionID, newSession.providerID);
            

            //write to a text file
            DataCenter.createSessionFile(newSession, newSession.sessionID);

            
            //weekly session records
            DataCenter.generateWeeklySessionIDs(newSession.sessionID);

            //email
            Member memberDetails = DataCenter.searchMember(DataCenter.getSessionInfo_sessionID(newSession.sessionID).memberID);
            Email email = new Email(memberDetails);

            MessageBox.Show("Session successfully created");
            resetPanel(billing_panel_session);
        }

        //display "verified" on page if the member id is valid 
        private void verifyMember_verify_Click(object sender, EventArgs e)
        {
            int memID = Int32.Parse(verifyMember_memberID.Text);

            if (DataCenter.memberExists(memID))
            {
                verifyMember_verified.Visible = true; 
                //MessageBox.Show("Member with id: " + memID + "exists");
            }
            /*
            else
            {
                MessageBox.Show("Member with id: " + memID + " does not exist");
            }
            */ 
        }

        private void billing_verify_Click(object sender, EventArgs e)
        {
            int memID = Int32.Parse(session_MemberID.Text);

            if (DataCenter.memberExists(memID))
            {
                verify_SessionMember.Visible = true;
               // MessageBox.Show("Member with id: " + memID + " exists");
                //resetPanel(panel_billing);
            }
        }

        //this resets and clears the textboxes 
        private void resetPanel(Panel p)
        {
            foreach (Control field in p.Controls)
            {
                if (field is TextBox) { field.Text = ""; }                    
            }
        }

        //reporting

        public  void createReport(string type)
        {  //would need to dynamically pass filename based on week




            // read sessionids from week file
            string weekFile = DataCenter.getWeeklyFileName();
            string[] ids = File.ReadAllLines(weekFile);
           
            // print member report info
            if (type == "member")
            {
                
                Dictionary<int,List<Session>> memberReport = new Dictionary<int, List<Session>>();
                
                foreach (string id in ids)
                {
                    int sessionID = Int32.Parse(id);
                     Session thisSession = DataCenter.getSessionInfo_sessionID(sessionID);

                    if (!memberReport.ContainsKey(thisSession.memberID))
                    {
                        memberReport.Add(thisSession.memberID, DataCenter.getSessionInfo_memberID(thisSession.memberID));
                    }

                   
                }
                string display = "";
                foreach (KeyValuePair<int, List<Session>> pair in memberReport)
                {

                    Member lookUP = DataCenter.searchMember(pair.Key);
                   display+= "Member name: " + lookUP.FirstName + " " + lookUP.LastName + " \n"
                    + "Member number: " + lookUP.MemberID + "\n"
                    + "Member street address: " + lookUP.Address.street + "\n"
                    + "Member city: " + lookUP.Address.city + "\n"
                    + "Member state: " + lookUP.Address.state + "\n"
                    + "Member ZIP code: " + lookUP.Address.postalCode + "\n\n";
                    for (int i = 0;i< pair.Value.Count; i++)
                        {
                          display += "\t" + "Date of Service:" +  pair.Value[i].DateOfSession.convToString() + "\n\t"
                           +"Provider name: " + DataCenter.searchProvider(pair.Value[i].providerID).ProviderName + "\n\t"
                            + "Service name: " + pair.Value[i].serviceName + "\n\n";
                       
                        }

                }
                report_box.Text = display;
            }
            // print provider report info
            if (type == "provider")
            {
                Dictionary<int, List<Session>> providerReport = new Dictionary<int, List<Session>>();

                foreach (string id in ids)
                {
                    int sessionID = Int32.Parse(id);
                    Session thisSession = DataCenter.getSessionInfo_sessionID(sessionID);

                    if (!providerReport.ContainsKey(thisSession.providerID))
                    {
                        providerReport.Add(thisSession.providerID, DataCenter.getSessionInfo_memberID(thisSession.providerID));
                    }


                }
                string display = "";
                int consultations = 0;
                int totalFee = 0;
                foreach (KeyValuePair<int, List<Session>> pair in providerReport)
                {
                    Provider lookUP = DataCenter.searchProvider(pair.Key);

                    display += "Provider name: " + lookUP.ProviderName + " \n"
                     + "Provider number: " + lookUP.ProviderID + "\n"
                     + "Provider street address: " + lookUP.Address.street + "\n"
                     + "Provider city: " + lookUP.Address.city + "\n"
                     + "Provider state: " + lookUP.Address.state + "\n"
                     + "Provider ZIP code: " + lookUP.Address.postalCode + "\n";

                    for (int i = 0; i < pair.Value.Count; i++)
                    {
                        consultations = pair.Value.Count;
                       display += "\n\t" + "Date of Service:" + pair.Value[i].DateOfSession.convToString() + "\n\t"
                         + "Member name: " + DataCenter.searchMember(pair.Value[i].memberID).FirstName + " "
                         + DataCenter.searchMember(pair.Value[i].memberID).FirstName + "\n\t"
                          + "Member number: " + pair.Value[i].memberID.ToString() + "\n\t"
                          + "Service Code: " + pair.Value[i].serviceID + "\n\t";
                        int fee = DataCenter.lookupService(pair.Value[i].serviceID).Fee;
                        display += "Fee to be Paid: " + fee.ToString() + "\n";
                        totalFee += fee;
                    }
                      
                         display += "Total number of Consultations with members: " + consultations.ToString() + "\n"
                          +"Total fee for week :" + totalFee.ToString() + "\n\n";
                         
                    


                }
                report_box.Text = display + "\n\n\n";
            }




            }

        
        private void Run_Click(object sender, EventArgs e)
        {
            if (runReports_reportType.Text == "Provider Reports")
                createReport("provider");
            else if (runReports_reportType.Text == "Member Reports")
                createReport("member");
            //else if(runReports_reportType.Text == 1)
            //  createReport("member");
        }

        //will update 
        private void home_currentTimer_Tick(object sender, EventArgs e)
        {

        }
    }      
}
