using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChoholicsAnonymous
{
    public partial class ProviderDirectory : Form
    {
        public ProviderDirectory()
        {
            InitializeComponent();
            populateTextField(); 
        }

        private void populateTextField()
        {
            string textBox = ""; 

            foreach (Service service in DataCenter.ServiceList)
            {
                textBox += "Service ID: " + service.ID + "\n";
                textBox += "Service Name: " + service.Name + "\n";
                textBox += "Fee: $" + service.Fee + "\n";
                textBox += "--------------------------\n"; 
            }
            directoryText.Text = textBox; 
        }
    }
}
