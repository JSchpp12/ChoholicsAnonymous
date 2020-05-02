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
            DataCenter.initilize();
        }

        #region UI event handlers

        private void Main_Load(object sender, EventArgs e)
        {
            hideAllPanels();
            panel_home.Visible = true; 
            //show required items
            initilizeToolbar(); 

        }

        //submit the new member information from the form to memory 
        private void newMem_submit_Click_1(object sender, EventArgs e)
        {
            int day, month; 

            Member newMember = new Member();
            newMember.FirstName              = newMem_firstName.Text;
            newMember.LastName               = newMem_lastName.Text;
            newMember.Email                  = newMem_email.Text;
            newMember.PhoneNumber            = newMem_phoneNumber.Text;
            newMember.Address.street         = newMem_Street.Text;
            newMember.Address.state          = newMem_State.Text;
            newMember.Address.city           = newMem_City.Text;
            newMember.Address.postalCode     = newMem_City.Text;
            newMember.Payment.CardNumber     = newMem_ccNum.Text;
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

        //changes which panel is displayed on the member search page based on which radio button is selected
        private void searchMem_rad_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender; 
            if (button.Checked){
                if (button.Tag.ToString() == "memID")
                {
                    searchMem_panel_ID.Visible = true;
                    searchMem_panel_Name.Visible = false; 
                }
                else if(button.Tag.ToString() == "memName")
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
            }catch (InvalidCastException ex)
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
                toolStrip_verifyMember.Visible       = false;
                toolStrip_newMember.Visible          = false;
                toolStrip_providerDirectory.Visible  = false;
                toolStrip_newProvider.Visible        = false;
                toolStrip_billing.Visible            = false;
                toolStrip_print.Visible              = false;
                
            }
            else if (User.Provider == true)
            {
                toolStrip_newMember.Visible          = false;
                toolStrip_newProvider.Visible        = false;
                toolStrip_runReports.Visible         = false;
                toolStrip_print.Visible              = false; 
            }
            else if (User.Operator == true)
            {
                toolStrip_billing.Visible            = false;
                toolStrip_reporting.Visible          = false; 
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
        }

        //Provider events start here

        private void newPro_bttn_submit_Click(object sender, EventArgs e)
        {
            int day, month;

            Provider newProvider = new Provider();

            newProvider.ProviderName = newProvider_name.Text;
            newProvider.PhoneNumber = newProvider_phoneNumber.Text;
            newProvider.Address.street = newProvider_street.Text;
            newProvider.Address.city = newProvider_city.Text;
            newProvider.Address.state = newProvider_state.Text;
            newProvider.Address.postalCode = newProvider_postal.Text;
            DataCenter.addProvider(newProvider);
            MessageBox.Show("Provider Successfully Added");
        }


        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataCenter.writeToFile("Member.xml", "member");
            DataCenter.writeToFile("Provider.xml", "provider");
        }
    }

}
