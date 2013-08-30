using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoPriceAlerts;
using System.Configuration;
using System.Data.Common;
using System.Data;
using PCGMailLib;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;

namespace PriceAlerts
{
    class Program
    {
        static void Main(string[] args)
        {
            PriceAlertsBo priceAlertsBo = new PriceAlertsBo();
            DataSet dsPriceDetails = new DataSet();
            SmtpClient smtpclient = new SmtpClient();
            MailMessage mailMessage = new MailMessage();
            string message = "";
            for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                string DbName = ConfigurationManager.ConnectionStrings[i].Name;
                NetworkCredential basicCredential = new NetworkCredential("admin@wealtherp.com", "Ampsys123#");

                if (DbName != "" && DbName != "LocalSqlServer")
                {
                    dsPriceDetails = priceAlertsBo.GetDailyPriceDetails(DbName);
                    mailMessage = new MailMessage();
                    string mailTo = ConfigurationManager.AppSettings["mailTo"].ToString();
                    string[] toMail = mailTo.Split(';');
                    for (int j = 0; j < toMail.Length; j++)
                    {
                        mailMessage.To.Add(toMail[j].ToString());
                    }
                    mailMessage.IsBodyHtml = true;
                    mailMessage.From = new MailAddress("admin@wealtherp.com");
                    mailMessage.Subject = DbName.ToString() + " Price & ValuationStatus Alerts";

                    //---------Added new---------------\\
                    message = "<html><body><table><tr><td colspan=\"2\" ><strong>Adviser MF Valuation Failed for Below Mentioned Advisers</strong></td><td></td></tr>";

                    if (dsPriceDetails.Tables[6].Rows.Count > 0)
                    {
                        message = "<html><body><table><tr><td colspan=\"2\" ><strong>Adviser MF Valuation Failed for Below Mentioned Advisers</strong></td><td></td></tr>";

                        message = message + "<tr><td style=\"border-style:solid;border-width:solid;border-color:Green;Color:Blue;\">AdviserId</td><td style=\"border-style:solid;border-width:solid;border-color:Green;Color:Blue;\">Adviser</td></tr>";

                        foreach (DataRow dr5 in dsPriceDetails.Tables[6].Rows)
                        {
                            message = message + "<tr><td style=\"border-style:solid;border-width:thin;Text-color:Black\">" + int.Parse(dr5[0].ToString()) + "</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + dr5[1].ToString() + "</td> </tr>";
                        }
                    }
                    else
                    {
                        message = message + "<tr><td colspan=\"2\" style=\"color:Green\"> <strong>Valuation Completed For All Advisers </strong></td><td></td></tr>";
                    }
                    message = message + "<tr><td colspan=\"2\"><strong>Abnormal Change In Valuation</strong></td><td></td></tr>";
                 
                    if (dsPriceDetails.Tables[5].Rows.Count > 0)
                    {
                        message = message + "<tr><td style=\"border-style:solid;border-width:solid;border-color:Black;\">AdviserId</td><td style=\"border-style:solid;border-width:solid;border-color:Black\">Adviser</td><td style=\"border-style:solid;border-width:solid;border-color:Black\">CurrentAUM</td><td style=\"border-style:solid;border-width:solid;border-color:Black\">CurrentValuationDate</td><td style=\"border-style:solid;border-width:solid;border-color:Black\">PreviousDateAUM</td><td style=\"border-style:solid;border-width:solid;border-color:Black\">PreviousValuationDate</td><td style=\"border-style:solid;border-width:solid;border-color:Black\">%Change</td></tr>";

                        foreach (DataRow dr5 in dsPriceDetails.Tables[5].Rows)
                        {
                            message = message + "<tr><td style=\"border-style:solid;border-width:thin;Text-color:Black\">" + int.Parse(dr5[0].ToString()) + "</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + dr5[1].ToString() + "</td> <td style=\"border-style:solid;border-width:thin;border-color:Black\">" + Decimal.Parse(dr5[2].ToString()) + "</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + DateTime.Parse(dr5[3].ToString()).ToLongDateString() + "</td> <td style=\"border-style:solid;border-width:thin;border-color:Black\">" + Decimal.Parse(dr5[4].ToString()) + "</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + DateTime.Parse(dr5[5].ToString()).ToLongDateString() + "</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + Decimal.Parse(dr5[6].ToString()) + "</td></tr>";
                        }
                    }
                    else
                    {
                        message = message + "<tr><td><strong>No Records found Value with greater or less than +-10% Change </strong></td><td></td></tr>";
                    }
                    //---------------end------------\\

                    message = message + "<tr><td><strong>MF Price Alerts</strong></td><td></td></tr>";
                    message = message + "<tr><td colspan=\"2\"></br></td></tr>";
                    message = message + "<tr><td><strong>Current SnapoShot</strong></td><td></td></tr>";
                    message = message + "<tr><td style=\"border-style:solid;border-width:thin;border-color:Black\">Date</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">Count</td></tr>";
                    foreach (DataRow dr1 in dsPriceDetails.Tables[0].Rows)
                    {
                        message = message + "<tr><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + DateTime.Parse(dr1[0].ToString()).ToLongDateString() + "</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + dr1[1].ToString() + "</td></tr>";
                    }
                    message = message + "<tr><td><strong>History Price</strong></td><td></td></tr>";
                    message = message + "<tr><td style=\"border-style:solid;border-width:thin;border-color:Black\">Date</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">Count</td></tr>";
                    foreach (DataRow dr2 in dsPriceDetails.Tables[1].Rows)
                    {
                        message = message + "<tr><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + DateTime.Parse(dr2[0].ToString()).ToLongDateString() + "</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + dr2[1].ToString() + "</td></tr>";
                    }
                    message = message + "<tr><td><strong>Equity Price Alerts</strong></td><td></td></tr>";
                    message = message + "<tr><td colspan=\"2\"></br></td></tr>";
                    message = message + "<tr><td><strong>Current Snapshot</strong></td><td></td></tr>";
                    message = message + "<tr><td style=\"border-style:solid;border-width:thin;border-color:Black\">Date</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">Count</td></tr>";
                    foreach (DataRow dr3 in dsPriceDetails.Tables[2].Rows)
                    {
                        message = message + "<tr><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + DateTime.Parse(dr3[0].ToString()).ToLongDateString() + "</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + dr3[1].ToString() + "</td></tr>";
                    }
                    message = message + "<tr><td><strong>History Price</strong></td><td></td></tr>";
                    message = message + "<tr><td style=\"border-style:solid;border-width:thin;border-color:Black\">Date</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">Count</td></tr>";
                    foreach (DataRow dr4 in dsPriceDetails.Tables[3].Rows)
                    {
                        message = message + "<tr><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + DateTime.Parse(dr4[0].ToString()).ToLongDateString() + "</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + dr4[1].ToString() + "</td></tr>";
                    }


                    //-----------Gold Price------------------\\

                    message = message + "<tr><td><strong>Gold Price</strong></td><td></td></tr>";
                    message = message + "<tr><td style=\"border-style:solid;border-width:thin;border-color:Black\">Date</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">Price</td></tr>";
                    foreach (DataRow dr4 in dsPriceDetails.Tables[4].Rows)
                    {
                        message = message + "<tr><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + DateTime.Parse(dr4[0].ToString()).ToLongDateString() + "</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + dr4[1].ToString() + "</td></tr>";
                    }


                   


                    //-------------------\\

                    message = message + "<tr><td><strong>Rejected Details</strong></td><td></td></tr>";
                    message = message + "<tr><td style=\"border-style:solid;border-width:thin;border-color:Black\">RejectCount</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">ProcessId</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">Reject Reason</td></tr>";
                    foreach (DataRow dr4 in dsPriceDetails.Tables[7].Rows)
                    {
                        message = message + "<tr><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + dr4[0].ToString() + "</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + dr4[1].ToString() + "</td><td style=\"border-style:solid;border-width:thin;border-color:Black\">" + dr4[2].ToString() + "</td></tr>";
                    }


                    //--------------------

                    message = message + "</table></body></html>";
                    mailMessage.Body = message;
                    smtpclient.Host = "smtpout.secureserver.net";
                    smtpclient.Port = 3535;
                    smtpclient.UseDefaultCredentials = false;
                    smtpclient.Credentials = basicCredential;
                    smtpclient.Send(mailMessage);

                }
            }
        }
    }
}
