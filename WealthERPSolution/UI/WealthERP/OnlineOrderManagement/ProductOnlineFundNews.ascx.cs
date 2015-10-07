using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.IO;
using System.Data;

namespace WealthERP.OnlineOrderManagement
{
    public partial class ProductOnlineFundNews : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNewsHeading();
            }
        }
        protected void BindNewsHeading()
        {
            string SectoreDetais = ConfigurationSettings.AppSettings["NEWS_HEADING"] + ConfigurationSettings.AppSettings["NEWS_DETAICOUNTS"];
            WebResponse response;
            string result;
            WebRequest request = HttpWebRequest.Create(SectoreDetais);
            response = request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
                reader.Close();
            }
            StringReader theReader = new StringReader(result);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);
            RepNews.DataSource = theDataSet.Tables[1];
            RepNews.DataBind();
        }
        protected void BindNewsDetails(int NewsId)
        {
            string SectoreDetais = ConfigurationSettings.AppSettings["NEWS_DETAILS"] +NewsId;
            WebResponse response;
            string result;
            WebRequest request = HttpWebRequest.Create(SectoreDetais);
            response = request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
                reader.Close();
            }
            StringReader theReader = new StringReader(result);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);
            repFundDetails.DataSource = theDataSet.Tables[1];
            repFundDetails.DataBind();
            divNewsDetails.Visible = true;
            FundNews.Visible = false;
            lblBack.Visible = true;
        }
        protected void repFundDetails_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                 int NewsDetsiisId = int.Parse(e.CommandArgument.ToString());
                 if (e.CommandName == "NewsDetailsLnk")
                 {
                     BindNewsDetails(NewsDetsiisId);
                 }
            }
        }
        protected void lnkBack_lnkMoreNews(object sender, EventArgs e)
        {
            BindNewsHeading();
            FundNews.Visible = true;
            divNewsDetails.Visible = false;
            lblBack.Visible = false;

        }
    }
}