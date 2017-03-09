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
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoOnlineOrderManagement;

namespace WealthERP.OnlineOrderManagement
{
    public partial class ProductOnlineFundNews : System.Web.UI.UserControl
    {
        OnlineMFSchemeDetailsBo OnlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNewsHeading();
                if (Request.QueryString["NewsDetsiisId"] != null)
                    BindNewsDetails(int.Parse(Request.QueryString["NewsDetsiisId"]));

            }
        }
        protected void BindNewsHeading()
        {
            try
            {
                DataSet theDataSet = OnlineMFSchemeDetailsBo.GetAPIData(ConfigurationSettings.AppSettings["NEWS_HEADING"] + ConfigurationSettings.AppSettings["NEWS_DETAICOUNTS"]);
                RepNews.DataSource = theDataSet.Tables[1];
                RepNews.DataBind();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineMFSchemeDetailsDao.cs:BindNewsHeading()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void BindNewsDetails(int NewsId)
        {
             try
            {
            string SectoreDetais = ConfigurationSettings.AppSettings["NEWS_DETAILS"] + NewsId;
            DataSet theDataSet = OnlineMFSchemeDetailsBo.GetAPIData(SectoreDetais);
            repFundDetails.DataSource = theDataSet.Tables[1];
            repFundDetails.DataBind();
            divNewsDetails.Visible = true;
            FundNews.Visible = false;
            lblBack.Visible = true;
            }
             catch (BaseApplicationException Ex)
             {
                 throw Ex;
             }
             catch (Exception Ex)
             {
                 BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                 NameValueCollection FunctionInfo = new NameValueCollection();
                 FunctionInfo.Add("Method", "BindNewsDetails(int NewsId)");
                 object[] objects = new object[0];
                 FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                 exBase.AdditionalInformation = FunctionInfo;
                 ExceptionManager.Publish(exBase);
                 throw exBase;

             }
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