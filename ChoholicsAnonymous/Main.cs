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


namespace ChoholicsAnonymous
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            hideAllPanels();
            panel_home.Visible = true;
            readMembers("Member.xml");
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
           // newMember.Address.street         = newMem_Street.Text;
           // newMember.Address.state          = newMem_State.Text;
            //newMember.Address.city           = newMem_City.Text;
            //newMember.Address.postalCode     = newMem_City.Text;
            //newMember.Payment.CardNumber     = newMem_ccNum.Text;
            //newMember.Payment.Cvc            = newMem_cvc.Text;

            if (int.TryParse(newMem_expMonth.Text, out month))
            {
               // newMember.Payment.ExpDate.Month = month;
            }
            else
            {
                //throw error 
                MessageBox.Show("Expiration Month is not in a valid form");
                return; 
            }
            if (int.TryParse(newMem_expDay.Text, out day))
            {
                //newMember.Payment.ExpDate.Day = day;
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

        public void readMembers(string filePath)
        {
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName; // return the application.exe current folder
            string fileName = Path.Combine(path, filePath);

            XmlSerializer reader = new XmlSerializer(typeof(HashSet<Member>));
          StreamReader file = new  StreamReader(fileName);
          DataCenter.memberSet = (HashSet<Member>)  reader.Deserialize(file);
            file.Close();
        }

        //searches for member information and populates GUI with retrieved information 
        private void searchMem_bttn_search_Click(object sender, EventArgs e)
        {


            //to write to a file 
            string filePath = "Member.xml";
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName; // return the application.exe current folder
            string fileName = Path.Combine(path, filePath);

            XmlSerializer serial = new XmlSerializer(typeof(HashSet<Member>));
            StreamWriter file = new StreamWriter(fileName);
            serial.Serialize(file,DataCenter.memberSet);
            file.Close();
           


        }

    }

}
