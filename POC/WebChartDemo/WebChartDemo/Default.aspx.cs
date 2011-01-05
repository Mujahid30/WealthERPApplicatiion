using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using WebChart;
using System.Web.UI.DataVisualization.Charting;


namespace WebChartDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        private string m_MailServer = ConfigurationManager.AppSettings["SMTPSvr"];
        protected void Page_Load(object sender, EventArgs e)
        {
            double[] yValues = { 70, 75, 73 };
            string[] xValues = { "Sujith", "Sashidhar", "Benson" };
            MyChart.ImageStorageMode = System.Web.UI.DataVisualization.Charting.ImageStorageMode.UseImageLocation;
            Series series = new Series("MyNewPieSeries");
            Legend legend = new Legend("MyPieLegend");
            legend.IsDockedInsideChartArea = true;
            legend.LegendStyle = LegendStyle.Column;
            series.ChartType = SeriesChartType.Pie;
            series.BorderWidth = 3;
            series.ShadowOffset = 2;
            series.IsVisibleInLegend = true;
            
            series.Points.DataBindXY(xValues, yValues);
            series.IsValueShownAsLabel = true;
            series["CollectedThresholdUsePercent"] = "true";
            series.Points[0]["Exploded"] = "true";
            series.Points[1]["Exploded"] = "true";
            series.Label = "#PERCENT";
            series.ToolTip = "#VALX: #PERCENT (#VAL)";
            MyChart.Series.Clear();
            MyChart.Legends.Clear();
            MyChart.Series.Add(series);
            MyChart.Legends.Add(legend);
            MyChart.ChartAreas[0].Area3DStyle.Enable3D = true;
           // MyChart.Series[0]["PieLabelStyle"] = "Outside";
          
            

        }

        private void SendMail(string mailFrom, string mailTo, string mailCC, string mailBCC, string subject, string mailBody)
        {
            try
            {

                System.Net.Mail.MailMessage SendMail = new System.Net.Mail.MailMessage(mailFrom, mailTo);
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                //client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;//Once have a central smtp remove this - Vimal
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
                    client.Port = 25;
                    client.UseDefaultCredentials = true;
                }
                

                client.Send(SendMail);
            }
            catch
            {
                //do nothing
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SendMail("sujisays@gmail.com", "sujith.stanly@yahoo.co.in", "", "", "Test Mail", "Hello Sujith");

        }
    }
}
