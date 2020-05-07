using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms; 

namespace ChoholicsAnonymous
{
    public static class LockTimer
    {
        private static System.Threading.Timer lockTimer = new System.Threading.Timer(lockScreen);

        //returns true if the current time is in lock time period 
        public static bool checkIfLock()
        {
            if (verifyCurrentTime())
                return false;
            else
                return true; 
        }
        public static void initilize(bool callLocked)
        {
            if (callLocked)
            {
                if (verifyCurrentTime())
                    initilizeTimerObject();
                else
                    lockScreen();
            }
            else
                initilizeTimerObject(); 
            //check if current time is in locked time zone 
        

        }

        private static void initilizeTimerObject()
        {
            ////DateTime today = DateTime.Today;
            //DateTime now = DateTime.Now;
            ////DateTime triggerTime = new DateTime(); 

            //int daysUntilFriday = (((int)DayOfWeek.Friday - (int)now.DayOfWeek + 7) % 7) + 1;
            //triggerTime = triggerTime.AddDays(daysUntilFriday);

            //int msUntilTrigger = (int)((triggerTime - now).TotalMilliseconds);

            //weeklyTimer = new System.Threading.Timer(new TimerCallback(runWeeklyReport), null, msUntilTrigger, 0);

            DateTime now = DateTime.Now;
            DateTime triggerTime = DateTime.Today;
            TimeSpan ts = new TimeSpan(21, 0, 0);
            triggerTime = triggerTime.Date + ts;

            int msUntilTrigger = (int)((triggerTime - now).TotalMilliseconds);

            lockTimer = new System.Threading.Timer(new TimerCallback(lockScreen), null, msUntilTrigger, 0); 
        }

        //checks if the current time is within the lock time 
        private static bool verifyCurrentTime()
        {
            //check if the time is after 9pm OR before 6am 
            DateTime now = DateTime.Now; 
            if (now.Hour >= 21 || now.Hour <= 6)
            {
                return false;
            }
            else
            {
                return true; 
            }
        }

        private static void lockScreen(object state)
        {
            lockTimer.Dispose();
            foreach (Form i in Application.OpenForms)
            {
                i.Hide(); 
            }

            Form newLockScreen = new Locked();
            newLockScreen.Show(); 
            //lock the screens 
        }
        //manually lock screens 
        private static void lockScreen()
        {
            foreach (Form i in Application.OpenForms)
            {
                i.Hide();
            }
            Form newLockScreen = new Locked();
            newLockScreen.Show();
        }
    }
}
