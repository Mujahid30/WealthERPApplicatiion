using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Collections.Specialized;

namespace Microsoft.ApplicationBlocks.ExceptionManagement.CustomPublisher
{
	/// <summary>
	/// Summary description for FileExceptionPublisher.
	/// </summary>
	public class FileExceptionPublisher:IExceptionPublisher,IExceptionXmlPublisher
	{
		
		#region Variables
		private string m_LogName = @"C:\ExceptionLog.txt";
		#endregion
		
		public FileExceptionPublisher()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IExceptionPublisher Members

		public void Publish(Exception exception, System.Collections.Specialized.NameValueCollection additionalInfo, System.Collections.Specialized.NameValueCollection configSettings)
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
				if (configSettings["fileName"] != null &&  
					configSettings["fileName"].Length > 0)
				{  
					m_LogName = configSettings["fileName"];
				}
			}
			// Create StringBuilder to maintain publishing information.
			StringBuilder strInfo = new StringBuilder();
			
			// Record the contents of the AdditionalInfo collection.
			if (additionalInfo != null)
			{
				// Record General information.
				strInfo.AppendFormat("{0}General Information{0}", Environment.NewLine);
				strInfo.AppendFormat("{0}Additonal Info:", Environment.NewLine);
				foreach (string i in additionalInfo)
				{
					strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, i, additionalInfo.Get(i));
				}
			}
			
			// Append the exception text
			strInfo.AppendFormat("{0}{0}Exception Information{0}{1}", Environment.NewLine, exception.ToString());
			strInfo.AppendFormat("{0}{0}", Environment.NewLine);

			if (m_LogName != "")
			{
				// Write the entry to the log file.   
				WriteToFile(m_LogName, strInfo);
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
				if (configSettings["fileName"] != null &&  
					configSettings["fileName"].Length > 0)
				{  
					m_LogName = configSettings["fileName"];
				}
			}
			// Create StringBuilder to maintain publishing information.
			StringBuilder strInfo = new StringBuilder();
			// Record General information.
			strInfo.AppendFormat("{0}General Information{0}", Environment.NewLine);
			// Append the exception text
			strInfo.AppendFormat("{0}{0}Exception Information{0}{1}", Environment.NewLine, exceptionInfo.InnerXml);
			strInfo.AppendFormat("{0}{0}", Environment.NewLine);
			
			if (m_LogName != "")
			{
				// Write the entry to the log file.   
				WriteToFile(m_LogName, strInfo);
			}
			#endregion
		}

		#endregion

		/// <summary>
		/// Write entry to Log File
		/// </summary>
		/// <param name="m_LogName"></param>
		/// <param name="strInfo"></param>
		private void WriteToFile(string m_LogName, StringBuilder strInfo)
		{
			try
			{
				using ( FileStream fs = File.Open(m_LogName,
							FileMode.Append, FileAccess.Write))
				{
					using (StreamWriter sw = new StreamWriter(fs))
					{
						sw.Write(strInfo.ToString());
					}
				}
			}
			catch(Exception ex)
			{
				string str = ex.Message;
			}
		}
	}
}
