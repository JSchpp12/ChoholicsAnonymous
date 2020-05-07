using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChoholicsAnonymous
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Main());

            if (LockTimer.checkIfLock())
                Application.Run(new Locked());
            else
               // LockTimer.initilize(false); //set up the lockout timer -- and dont call lock screen
                Application.Run(new Login()); 
        }
    }
}
