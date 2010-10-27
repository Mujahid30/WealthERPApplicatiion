using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Net.Mail;

namespace Microsoft.ApplicationBlocks.ExceptionManagement.CustomPublisher
{
	/// <summary>
	/// Summary description for EmailExceptionPublisher.
	/// </summary>
	public class EmailExceptionPublisher:IExceptionPublisher,IExceptionXmlPublisher
	{
		
		#region Variables
		
		private string m_OpMail = ConfigurationManager.AppSettings["ExceptionMailTo"];
		private string m_OpMailFrom = ConfigurationManager.AppSettings["ExceptionMailFrom"];
		private string m_MailServer =  ConfigurationManager.AppSettings["SMTPSvr"];
		#endregion

		public EmailExceptionPublisher()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IExceptionPublisher Members

		public void Publish(Exception exception, System.Collections.Specialized.NameValueCollection AdditionalInfo, System.Collections.Specialized.NameValueCollection configSettings)
		{
			#region Thread abort exception
			//if(exception.GetType().Equals(typeof(System.Threading.ThreadAbortException)))
			//{
				//return;
			//}
			#endregion

			#region Publish Exception
			// Load Config values if they are provided.
			if (configSettings != null)
			{
				if (configSettings["operatorMail"] !=null && 
					configSettings["operatorMail"].Length > 0)
				{
					m_OpMail = configSettings["operatorMail"];
				}
				if (configSettings["operatorMailFrom"] !=null && 
					configSettings["operatorMailFrom"].Length > 0)
				{
					m_OpMailFrom = configSettings["operatorMailFrom"];
				}
				if (configSettings["MailServer"] !=null && 
					configSettings["MailServer"].Length > 0)
				{
					m_MailServer = configSettings["MailServer"];
				}
			}
			// Create StringBuilder to maintain publishing information.
			StringBuilder strInfo = new StringBuilder();
			
			// Record the contents of the AdditionalInfo collection.
			if (AdditionalInfo != null)
			{
				// Record General information.
				strInfo.AppendFormat("{0}General Information{0}", Environment.NewLine);
				strInfo.AppendFormat("{0}Additonal Info:", Environment.NewLine);
				foreach (string i in AdditionalInfo)
				{
					strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, i, AdditionalInfo.Get(i));
				}
			}
			
			// Append the exception text
			strInfo.AppendFormat("{0}{0}Exception Information{0}{1}", Environment.NewLine, exception.ToString());

			strInfo.AppendFormat("{0}{0}", Environment.NewLine);

			#region construct the subject 
			// format of subject. E.g. "Error on Regulus: (501) General processing error"
			StringBuilder subject = new StringBuilder();
			subject.Append("Error on "); 
			subject.Append(Environment.MachineName); 
			subject.Append(": "); 
			subject.Append(exception.Message);  
			#endregion

			// send notification email if operatorMail attribute was provided
			if (m_OpMail.Length > 0)
			{
				SendMail(m_OpMailFrom, m_OpMail, "", "", subject.ToString(), strInfo.ToString());
			}
			#endregion
		}

		#endregion

		#region IExceptionXmlPublisher Members

		void Microsoft.ApplicationBlocks.ExceptionManagement.IExceptionXmlPublisher.Publish(System.Xml.XmlDocument exceptionInfo, System.Collections.Specialized.NameValueCollection configSettings)
		{
			#region Publish Exception
			// Load Config values if they are provided.
			if (configSettings != null)
			{
				if (configSettings["operatorMail"] !=null && 
					configSettings["operatorMail"].Length > 0)
				{
					m_OpMail = configSettings["operatorMail"];
				}
				if (configSettings["operatorMailFrom"] !=null && 
					configSettings["operatorMailFrom"].Length > 0)
				{
					m_OpMailFrom = configSettings["operatorMailFrom"];
				}
				if (configSettings["MailServer"] !=null && 
					configSettings["MailServer"].Length > 0)
				{
					m_MailServer = configSettings["MailServer"];
				}
			}
			// Create StringBuilder to maintain publishing information.
			StringBuilder strInfo = new StringBuilder();
			// Record General information.
			strInfo.AppendFormat("{0}General Information{0}", Environment.NewLine);
			// Append the exception text
			strInfo.AppendFormat("{0}{0}Exception Information{0}{1}", Environment.NewLine, exceptionInfo.InnerXml);
			strInfo.AppendFormat("{0}{0}", Environment.NewLine);
			#region construct the subject 
			// format of subject. E.g. "Error on Regulus: (501) General processing error"
			StringBuilder subject = new StringBuilder();
			subject.Append("Error on "); 
			subject.Append(Environment.MachineName); 
			#endregion

			// send notification email if operatorMail attribute was provided
			if (m_OpMail.Length > 0)
			{
				SendMail(m_OpMailFrom, m_OpMail, "", "", subject.ToString(), strInfo.ToString());
			}
			#endregion
		}

		#endregion

		/// <summary>
		///	Utility function to send email.
		/// </summary>
		/// <param name="mailFrom">From email address</param>
		/// <param name="mailTo">To Email address</param>
		/// <param name="mailCC">CC Email address</param>
		/// <param name="mailBCC">BCC Email address</param>
		/// <param name="subject">Subject of the mail</param>
		/// <param name="mailBody">Content of the mail</param>
		public void SendMail(string mailFrom, string mailTo,string mailCC, string mailBCC,string subject,string mailBody)
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
                    client.Host = m_MailServer;

                client.Send(SendMail);
			}
			catch
			{
				//do nothing
			}
		}
	}
}
