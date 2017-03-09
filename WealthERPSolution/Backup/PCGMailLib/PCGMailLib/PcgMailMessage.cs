using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Web;

namespace PCGMailLib
{
    /// <summary>
    /// This class is empty now and its inheareting from MailMassage class of .Net .so we can provide some sutomization in future if we wish .
    /// probably we can insert default logo and some Html Helper function to form mail massage :) 
    /// </summary>
    public class PcgMailMessage : MailMessage
    {
        public bool SendRegistrationMail(string fromEmail, string toEmail, string name, string LoginId, string password,string path)
        {
            bool bResult = false;
       

            PcgMailMessage email = new PcgMailMessage();
           
            email.From = new MailAddress(fromEmail, "WealthERP Admin");
            email.To.Add(new MailAddress(toEmail, name));
            

            email.Subject = "Registration";
            email.Body = CreateHTMLMailBody("registration","", LoginId, password);

            email.IsBodyHtml = true;
           // PCGSmtpClient smtpClient = new PCGSmtpClient(@"d:\temp\emails", "192.168.0.2", 25, 60);
            PCGSmtpClient smtpClient = new PCGSmtpClient(@path, "192.168.0.2", 25, 60);
            //PCGSmtpClient smtpClient = new PCGSmtpClient(@"d:\temp\emails", "smtp.yourserver.com", 25, 60);
            smtpClient.Credentials = new System.Net.NetworkCredential("administrator", "Colours4*", "server");

            smtpClient.Send(email);


            return bResult;
        }

        public bool SendForgotPasswordMail(string fromEmail, string toEmail, string name, string LoginId, string password,string path)
        {
            bool bResult = false;

            PcgMailMessage email = new PcgMailMessage();

            email.From = new MailAddress(fromEmail, "WealthERP Admin");
            email.To.Add(new MailAddress(toEmail, name));


            email.Subject = "Forgot Password";
            email.Body = CreateHTMLMailBody("ForgotPassword","", LoginId, password);

            email.IsBodyHtml = true;
         //   PCGSmtpClient smtpClient = new PCGSmtpClient(@"d:\temp\emails", "192.168.0.2", 25, 60);
            PCGSmtpClient smtpClient = new PCGSmtpClient(path, "192.168.0.2", 25, 60);
            //PCGSmtpClient smtpClient = new PCGSmtpClient(@"d:\temp\emails", "smtp.yourserver.com", 25, 60);
            smtpClient.Credentials = new System.Net.NetworkCredential("administrator", "Colours4*", "server");

            smtpClient.Send(email);


            return bResult;
        }

        public bool SendCustomerLoginPassMail(string fromEmail, string toEmail, string senderOrgName, string receiverName, string LoginId, string password,string path)
        {
            bool bResult = false;

            PcgMailMessage email = new PcgMailMessage();

            email.From = new MailAddress(fromEmail, senderOrgName);
            email.To.Add(new MailAddress(toEmail, receiverName));


            email.Subject = senderOrgName+" Login Details";
            email.Body = CreateHTMLMailBody("Customer Login",senderOrgName, LoginId, password);

            email.IsBodyHtml = true;
           // PCGSmtpClient smtpClient = new PCGSmtpClient(@"d:\temp\emails", "192.168.0.2", 25, 60);
            PCGSmtpClient smtpClient = new PCGSmtpClient(path, "smtpout.secureserver.net", 3535, 60);
            //PCGSmtpClient smtpClient = new PCGSmtpClient(@"d:\temp\emails", "smtp.yourserver.com", 25, 60);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("admin@wealtherp.com", "Ampsys123#");
            
            
            smtpClient.Send(email);


            return bResult;
        }

        public string CreateHTMLMailBody(string type, string sender, string loginId, string password)
        {
            string body;

            if (type == "Registration")
            {
                body = "<p>Dear Valued Customer,<br><br> Thanks for registering with WealthERP<br><br>" +
                        "Your login information is as follows:<br> LoginId : " + loginId + "<br> Password : " + password +
                        "<br><br>Regards,<br>WealthERP Team<br>";
            }

            else if (type == "Forgot Password")
            {
                body = "<p>Dear Customer,<br><br> As per your request we have reset your password.<br><br>" +
                        "Your current login information is as follows:<br> LoginId : " + loginId + "<br> Password : " + password +
                        "<br><br>Regards,<br>WealthERP Team<br>";
            }
            else
            {
                body = "<p>Dear Customer,<br><br>An account has been created for you in WealthERP<br><br>" +
                        "You can login using the following LoginId and Password<br> LoginId : " + loginId + "<br> Password : " + password +
                        "<br><br>Regards,<br>WealthERP Team<br>";
            }

            return body;
        }
    }
}
