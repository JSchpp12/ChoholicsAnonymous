using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ChoholicsAnonymous
{
    class Email
    {
        private string FilePath    { get; set; }
        public  string addInfo     { get; set; }
        public  Date   dateEntered { get; set; } //date entered by user
        public  int    numOfServices;            //number of services provided

        private StreamWriter sw;
        private TextWriter   tw;

        public Email(Member member)
        {
            FilePath = member.FirstName + member.LastName + dateEntered + ".txt"; //EX. JaneDoe01012020.txt

            try
            {
                using (sw = File.CreateText(FilePath))
                {

                    //gets member info from session and puts it in file
                    sw.Write(member.FirstName + " " + member.LastName + "\n");
                    sw.Write(member.MemberID + "\n");
                    sw.Write(member.Address.street + "\n");
                    sw.Write(member.Address.city + ", " + member.Address.state 
                        + " " + member.Address.postalCode + "\n");

                    //gets service info and puts it in the file
                    for (int i = 1; i <= numOfServices; i++)
                    {
                        sw.Write("Service " + i + ":\n");
                        //sw.Write("\t" + member.dateOfService + "\n");
                        //sw.Write("\t" + member.providerName + "\n");
                        //sw.Write("\t" + member.serviceName + "\n");
                    }

                }

            }
            catch { }
        }

        public Email(Provider provider)
        {
            FilePath = provider.ProviderName.ToString() + dateEntered + ".txt"; //EX. Provider01012020.txt

            try
            {
                using (sw = File.CreateText(FilePath))
                {

                    //gets provider info from session and puts it in file
                    sw.Write(provider.ProviderName + "\n");
                    sw.Write(provider.ProviderID + "\n");
                    sw.Write(provider.Address.street + "\n");
                    sw.Write(provider.Address.city + ", " + provider.Address.state
                        + " " + provider.Address.postalCode + "\n");

                    //gets service info and puts it in the file
                    for (int i = 1; i <= numOfServices; i++)
                    {
                        sw.Write("Service " + i + ":\n");
                        //sw.Write("\t" + provider.dateOfService + "\n");
                        //sw.Write("\t" + provider.dateRecieved + "\n");
                        //sw.Write("\t" + provider.memberFirstName + " " + provider.memberLastName + "\n");
                        //sw.Write("\t" + provider.memberID + "\n");
                        //sw.Write("\t" + provider.serviceType + "\n");
                        //sw.Write("\tFee to be paid: $" + provider.feeToBePaid + "\n");
                    }

                    //sw.Write("Total number of member consultations: "provider.totalNumConsultations + "\n");
                    //sw.Write("Total fees for the week: $" + provider.totalWeekFee + "\n");

                }

            }
            catch { }
        }

        public Email(Session session)
        {

            FilePath = session.DateOfSession.ToString() + "_" + session.sessionID.ToString() + ".txt"; //EX. 01012020_123456789
            String currDate = DateTime.Now.ToString();

            try
            {
                using (sw = File.CreateText(FilePath))
                {

                    //gets service info and puts it in file
                    sw.Write(currDate + "\n");
                    sw.Write(session.DateOfSession + "\n");
                    sw.Write(session.providerID + "\n");
                    sw.Write(session.memberID + "\n");
                    sw.Write(session.serviceType + "\n"); //service code
                    sw.Write(session.comments + "\n");
                }
            }
            catch { }
        }

        ~Email()
        {
            sw.Close();
        }

        public void AddMemberInfo(Member member)
        {
            FilePath = member.FirstName + member.LastName + dateEntered + ".txt"; //EX. JaneDoe01012020.txt

            try
            {
                //adds info/writes a line of text to a file
                tw = new StreamWriter(FilePath);
                tw.WriteLine(addInfo);
                tw.Close();
            }
            catch (FileNotFoundException e)
            {
                //error for if file not found
                Console.WriteLine("Error: file not found.", e);
            }

        }

        public void AddProviderInfo(Provider provider)
        {
            FilePath = provider.ProviderName.ToString() + dateEntered + ".txt"; //EX. Provider01012020.txt

            try
            {
                //adds info/writes a line of text to a file
                tw = new StreamWriter(FilePath);
                tw.WriteLine(addInfo);
                tw.Close();
            }
            catch (FileNotFoundException e)
            {
                //error for if file not found
                Console.WriteLine("Error: file not found.", e);
            }
        }

        public void AddSessionInfo(Session session)
        {
            FilePath = session.DateOfSession.ToString() + "_" + session.sessionID.ToString() + ".txt"; //EX. 01012020_123456789

            try
            {
                //adds info/writes a line of text to a file
                tw = new StreamWriter(FilePath);
                tw.WriteLine(addInfo);
                tw.Close();
            }
            catch (FileNotFoundException e)
            {
                //error for if file not found
                Console.WriteLine("Error: file not found.", e);
            }
        }
    }
}