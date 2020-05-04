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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void login_rad_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (button.Checked)
            {
                if (button.Tag.ToString() == "provider")
                {
                    panel_managerID.Visible = false;
                    panel_providerID.Visible = true;
                    panel_operatorID.Visible = false;
                    User.Provider = true;
                }
                else if (button.Tag.ToString() == "operator")
                {
                    panel_managerID.Visible = false;
                    panel_providerID.Visible = false;
                    panel_operatorID.Visible = true;
                    User.Operator = true;
                }
                else if (button.Tag.ToString() == "manager")
                {
                    panel_managerID.Visible = true;
                    panel_providerID.Visible = false;
                    panel_operatorID.Visible = false;
                    User.Manager = true;
                }
                else
                {
                    MessageBox.Show("An Unknown Error Has Occured when changing panels");
                }
                login_bttn_login.Visible = true;
            }
        }

        //call other panel and set user values 
        private void login_bttn_login_Click(object sender, EventArgs e)
        {
            bool invalidID = false; 
            //save user information
            if (User.Manager == true)
            {
                if (verifyLogin(login_managerID.ToString()))
                    User.UserID = login_managerID.ToString();
                else
                    invalidID = true; 
            }
            if (User.Operator == true)
            {
                if (verifyLogin(login_operatorID.ToString()))
                    User.UserID = login_operatorID.ToString();
                else
                    invalidID = true; 
            }
            if (User.Provider == true)
            {
                if (verifyLogin(login_providerID.ToString()))
                    User.UserID = login_providerID.Text;
                else
                    invalidID = true; 
            }
            if (!invalidID)
            {
                try
                {
                    Form newMainForm = new Main();
                    newMainForm.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while changing pages \n " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("User ID is invalid"); 
            }
            
        }

        //verifys if the given login is valid, returns true if so 
        private bool verifyLogin(string id)
        {
            char temp = id[1]; 
            //check if the first char is a blank
            if (temp != 'S') 
            {
                return true;
            }
            else
            {
                return false; 
            }
        }
    }
}
