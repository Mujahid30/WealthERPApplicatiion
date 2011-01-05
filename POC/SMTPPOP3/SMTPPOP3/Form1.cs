using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using Pop3;
using System.Configuration;
using System.Collections.Specialized;

namespace SMTPPOP3
{
    public partial class Form1 : Form
    {
        
        private string m_OpMailFrom = ConfigurationManager.AppSettings["MailFrom"];
        private string m_MailServer = ConfigurationManager.AppSettings["SMTPSvr"];

        public Form1()
        {
            InitializeComponent();
        }
        int mcount = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            bool isHtml=false;
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("All three fields are mandatory");
                return;
            }
            if(chkIsHtml.Checked)
                   isHtml=true;
            SendMail(m_OpMailFrom,textBox1.Text,"","",textBox2.Text,textBox3.Text,isHtml);

            //MailMessage oMM = new MailMessage("sujisays@gmail.com", textBox1.Text);
            //oMM.Subject = textBox2.Text;
            //oMM.Body = textBox3.Text;
            //oMM.IsBodyHtml = true;
            
            //SmtpClient oSC = new SmtpClient("smtp.gmail.com", 587);
            //System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("sujisays@gmail.com", "parayan#2711");
            //oSC.EnableSsl = true;
            //oSC.UseDefaultCredentials = false;
            //oSC.Credentials = SMTPUserInfo;
            //oSC.DeliveryMethod = SmtpDeliveryMethod.Network;
            //oSC.Send(oMM);
        }
        private void SendMail(string mailFrom, string mailTo, string mailCC, string mailBCC, string subject, string mailBody,bool isBodyHTML)
        {
            try
            {

                System.Net.Mail.MailMessage SendMail = new System.Net.Mail.MailMessage(mailFrom, mailTo);
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;//Once have a central smtp remove this - Vimal
                SendMail.BodyEncoding = Encoding.UTF8;
                SendMail.IsBodyHtml = false;
                SendMail.Subject = subject;
                SendMail.Body = mailBody;

                if (mailCC != null && mailCC.Trim().Length > 0)
                    SendMail.CC.Add(mailCC);
                if (mailBCC != null && mailBCC.Trim().Length > 0)
                    SendMail.Bcc.Add(mailBCC);

                if (!(m_MailServer == "" || m_MailServer == null))
                {
                    client.Host = m_MailServer;
                    
                }

                client.Send(SendMail);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //do nothing
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int lMailCount = 0;
            string sEmail = "";
            
            GetEmail("pop.gmail.com", "sujisays@gmail.com", "parayamnilam", false, out lMailCount, out sEmail);

            label4.Text = (lMailCount == 0) ? "No mails found" : "Total " + lMailCount.ToString() + " found Displaying " + (lMailCount-mcount).ToString() + "Message";
            
            textBox4.Text = sEmail;
            button2.Text = "Read Next";
            mcount++;
        }

        public void GetEmail(string sServer, string sUserName, string sPassword, bool bSSLConnection, out int lMailCount, out string sEmail)
		{
            Pop3MailClient oClient = new Pop3MailClient(sServer, 995, true, sUserName, sPassword);
			string sUIDL = "";
            List<EmailUid> oList;

            oClient.Connect();
            oClient.GetUniqueEmailIdList(out oList);
            lMailCount = oList.Count;
            sEmail = "";
			if (oList.Count > 0)
			{
                oClient.GetRawEmail(lMailCount-mcount, out sEmail);
                
			}

            oClient.Disconnect();
		}
	}
}

