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
                if (verifyLogin(login_managerID.Text))
                    User.UserID = login_managerID.Text;
                else
                    invalidID = true;
            }
            if (User.Operator == true)
            {
                if (verifyLogin(login_operatorID.Text))
                    User.UserID = login_operatorID.Text;
                else
                    invalidID = true;
            }
            if (User.Provider == true)
            {
                if (verifyLogin(login_providerID.Text))
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
            if (id.Length == 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //will switch to lock screen 
        private void LockTimer_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            if (currentTime.Hour >= 21 || currentTime.Hour < 6)
            {
                LockTimer.Enabled = false;
                //lock screen 
                Form newLocked = new Locked();
                newLocked.Show();
                this.Hide();
            }
        }

        //enables or disables the clocks associated with the form 
        private void Login_VisibleChanged(object sender, EventArgs e)
        {
            Form currentForm = (Form)sender;
            if (currentForm.Visible == true)
                LockTimer.Enabled = true;
            else
                LockTimer.Enabled = false;
        }
    }
}
