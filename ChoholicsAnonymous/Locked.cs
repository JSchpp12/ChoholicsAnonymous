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
    public partial class Locked : Form
    {
        public Locked()
        {
            InitializeComponent();
        }

        //displays current time and will unlock the console at specified time 
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            if (now.Hour < 21 && now.Hour >= 6)
            {
                //lock screen 
                Form newLogin = new Login();
                newLogin.Show();
                this.Hide();
            }
            else
            {
                if (now.Hour > 12)
                    currentTime.Text = "Current Time: " + (now.Hour % 12).ToString("D2") + ":" + now.Minute.ToString("D2") + ":" + now.Second.ToString("D2") + " PM";
                else
                    currentTime.Text = "Current Time: " + now.Hour.ToString("D2") + ":" + now.Minute.ToString("D2") + ":" + now.Second.ToString("D2") + " AM";
            }
        }

        private void Locked_VisibleChanged(object sender, EventArgs e)
        {
            Form current = (Form)sender;
            if (current.Visible == true)
                LockedTimer.Enabled = true;
            else
                LockedTimer.Enabled = false; 
        }
    }
}
