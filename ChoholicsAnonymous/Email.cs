using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ChoholicsAnonymous
{
    public class Email
    {
        private string FilePath    { get; set; }
        public  string addInfo     { get; set; }
        
                   

        private StreamWriter sw;
        private TextWriter   tw;

        public Email(Member member)
        {   string dateOfReport = DateTime.UtcNow.ToString("MM-dd-yyyy");
            int  memberServices = DataCenter.getSessionInfo_memberID(member.MemberID).Count;
            string fullPath = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            FilePath  = fullPath.Replace("\\bin\\Debug", "\\Emails\\members\\" + member.FirstName + member.LastName + dateOfReport + ".txt"); //EX. JaneDoe01012020.txt
             

            try
            {
                using (sw = File.CreateText(FilePath))
                {

                    //gets member info from session and puts it in file
                    sw.Write("Member Name: " + member.FirstName + " " + member.LastName + "\n");
                    sw.Write("Member Number: "+ member.MemberID + "\n");
                    sw.Write("Member Address street: " + member.Address.street + "\n");
                    sw.Write("Member city: " + member.Address.city + "\n");
                    sw.Write("Member state: " + member.Address.state + "\n");
                    sw.Write("Member ZIP code: " + member.Address.postalCode + "\n\n");


                    //gets service info and puts it in the file
                    for (int i = 0 ; i <= memberServices; i++)
                    {
                       // sw.Write("Service " + i + ":\n");
                        sw.Write("\t Date of Service: " + DataCenter.getSessionInfo_memberID(member.MemberID)[i].DateOfSession.convToString() + "\n");
                        sw.Write("\t Provider Name: " + DataCenter.searchProvider(DataCenter.getSessionInfo_memberID(member.MemberID)[i].providerID).ProviderName + "\n");
                        sw.Write("\t Service Name: " + DataCenter.getSessionInfo_sessionID(member.MemberID).serviceName + "\n\n");
                        //sw.Write("\t" + member.dateOfService + "\n");
                        //sw.Write("\t" + member.providerName + "\n");
                        //sw.Write("\t" + member.serviceName + "\n");
                    }

                }

            }
            catch { }
        }

        public Email(Provider provider)
        {   string dateOfReport = DateTime.UtcNow.ToString("MM-dd-yyyy");
            int providerServices = DataCenter.getSessionInfo_memberID(provider.ProviderID).Count;
            string fullPath = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            FilePath = fullPath.Replace("\\bin\\Debug", "\\Emails\\providers\\" + provider.ProviderName + dateOfReport + ".txt");//EX. Provider01012020.txt
            int totalFee = 0;
            try
            {
                using (sw = File.CreateText(FilePath))
                {

                    //gets provider info from session and puts it in file
                    sw.Write("Provider Name: " +provider.ProviderName + "\n");
                    sw.Write("Provider Number: "+provider.ProviderID + "\n");
                    sw.Write("Provider street Address: " + provider.Address.street + "\n");
                    sw.Write("Provider city: " + provider.Address.city + "\n");
                    sw.Write("Provider state: " + provider.Address.state + "\n");   
                     sw.Write("Provider ZIP code: " + provider.Address.postalCode + "\n\n");


                    //gets service info and puts it in the file
                    
                    for (int i = 0; i <= providerServices; i++)
                    {
                       // sw.Write("Service " + i + ":\n");
                        sw.Write("\t Date of Service: " + DataCenter.getSessionInfo_providerID(provider.ProviderID)[i].DateOfSession.convToString() + "\n");
                        sw.Write("\t Date and Time Data Received: " + DateTime.UtcNow.ToString() + "\n");
                        sw.Write("\t Member Name:"   + DataCenter.searchMember(DataCenter.getSessionInfo_providerID(provider.ProviderID)[i].memberID).FirstName + " "
                           + DataCenter.searchMember(DataCenter.getSessionInfo_providerID(provider.ProviderID)[i].memberID).LastName +
                            "\n"); ;
                        sw.Write("\t Member Number: " + DataCenter.searchMember(DataCenter.getSessionInfo_providerID(provider.ProviderID)[i].memberID).MemberID + "\n");
                        sw.Write("\t Service Code: " + DataCenter.getSessionInfo_providerID(provider.ProviderID)[i].serviceID + "\n");
                        int fee = DataCenter.lookupService(DataCenter.getSessionInfo_providerID(provider.ProviderID)[i].serviceID).Fee;
                        sw.Write("\t Fee to be paid: " + fee.ToString() + "\n\n");
                        totalFee += fee;
                        
                        if (i == (providerServices - 1))
                        {
                            sw.Write("Total number of consultations: " + providerServices + "\n");
                            sw.Write("Total fees for the week: $" + totalFee.ToString() + "\n\n");
                        }
                    }
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
                    sw.Write(session.serviceID + "\n"); //service code
                    //sw.Write(session.comments + "\n");
                }
            }
            catch { }
        }

        ~Email()
        {
            sw.Close();
        }

        //public void AddMemberInfo(Member member)
        //{
        //    FilePath = member.FirstName + member.LastName + dateEntered + ".txt"; //EX. JaneDoe01012020.txt

        //    try
        //    {
        //        //adds info/writes a line of text to a file
        //        tw = new StreamWriter(FilePath);
        //        tw.WriteLine(addInfo);
        //        tw.Close();
        //    }
        //    catch (FileNotFoundException e)
        //    {
        //        //error for if file not found
        //        Console.WriteLine("Error: file not found.", e);
        //    }

        //}

        //public void AddProviderInfo(Provider provider)
        //{
        //    FilePath = provider.ProviderName.ToString() + dateEntered + ".txt"; //EX. Provider01012020.txt

        //    try
        //    {
        //        //adds info/writes a line of text to a file
        //        tw = new StreamWriter(FilePath);
        //        tw.WriteLine(addInfo);
        //        tw.Close();
        //    }
        //    catch (FileNotFoundException e)
        //    {
        //        //error for if file not found
        //        Console.WriteLine("Error: file not found.", e);
        //    }
        //}

        //public void AddSessionInfo(Session session)
        //{
        //    FilePath = session.DateOfSession.ToString() + "_" + session.sessionID.ToString() + ".txt"; //EX. 01012020_123456789

        //    try
        //    {
        //        //adds info/writes a line of text to a file
        //        tw = new StreamWriter(FilePath);
        //        tw.WriteLine(addInfo);
        //        tw.Close();
        //    }
        //    catch (FileNotFoundException e)
        //    {
        //        //error for if file not found
        //        Console.WriteLine("Error: file not found.", e);
        //    }
        //}
    }
}