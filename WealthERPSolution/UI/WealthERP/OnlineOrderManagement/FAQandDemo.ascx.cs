using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using BoOnlineOrderManagement;
using System.Data;

namespace WealthERP.OnlineOrderManagement
{
    public partial class FAQandDemo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string assetCategory = Request.QueryString["Cat"];
            if (Request.QueryString["TYP"] == "Demo")
                {
                    SetDemoLink(assetCategory);

                }
            else if (Request.QueryString["TYP"] == "FAQ")
                {
                    SetFAQLink(assetCategory);

                }
            
        }
        protected void Repeater1_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                string DemoLink = e.CommandArgument.ToString();
                if (e.CommandName == "OpenDemo")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetDemo", @"window.open('ReferenceFiles/HelpVideo.htm?Link=" + DemoLink + "', 'newwindow', 'width=655, height=520'); ", true);
                }
            }
        }
        protected void Repeater2_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                string pdfName = e.CommandArgument.ToString();
                if (e.CommandName == "OpenFaq")
                {
                    string path = ConfigurationManager.AppSettings["BANNER_IMAGE_PATH"].ToString();
                    Response.Redirect(path + pdfName);
                    //System.Web.UI.HtmlControls.HtmlAnchor delete = (System.Web.UI.HtmlControls.HtmlAnchor)Repeater2.Items[0].FindControl("aFAQ");
                    //delete.HRef = path + pdfName;
                    //string urlPath = "<script type='text/javascript'>window.parent.location.href = '" + path + pdfName + "'; </script>";
                    //Page.ClientScript.RegisterClientScriptBlock(GetType(), "Load", urlPath);
                }
            }
        }
        protected void SetDemoLink(string productType)
        {
            Div1.Visible = true;
            OnlineOrderBo onlineOrderBo = new OnlineOrderBo();
            string assetCategory = String.Empty;
            string innerHtml = String.Empty;
            DataTable dt = new DataTable();
            switch (productType)
            {
                case "MF":
                    assetCategory = "MF";
                    break;
                case "NCD":
                    assetCategory = "FI";
                    break;
                case "IPO":
                    assetCategory = "IP";
                    break;
            }

            dt = onlineOrderBo.GetAdvertisementData(assetCategory, "Demo");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "SetDemo", @"window.open('ReferenceFiles/HelpVideo.htm?Link=" + dt.Rows[0][0].ToString() + "', 'newwindow', 'width=655, height=520'); ", true);
            if (dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            //lnkDemo.OnClientClick = @"window.open('ReferenceFiles/HelpVideo.htm?Link=" + dt.Rows[0][0].ToString() + "', 'newwindow', 'width=655, height=520'); ";

        }
        protected void SetFAQLink(string productType)
        {

            Div3.Visible = true;
            OnlineOrderBo onlineOrderBo = new OnlineOrderBo();
            string assetCategory = String.Empty;

            DataTable dt = new DataTable();
            switch (productType)
            {
                case "MF":
                    assetCategory = "MF";
                    break;
                case "NCD":
                    assetCategory = "FI";
                    break;
                case "IPO":
                    assetCategory = "IP";
                    break;
            }
            dt = onlineOrderBo.GetAdvertisementData(assetCategory, "FAQ");
            string path = ConfigurationManager.AppSettings["BANNER_IMAGE_PATH"].ToString();
            //Response.Redirect(path + dt.Rows[0][0].ToString());
            if (dt.Rows.Count > 0)
            {
                Repeater2.DataSource = dt;
                Repeater2.DataBind();
              
            }
            //lnkFAQ.OnClientClick = @"window.open('" + path.Replace("~", "..") + dt.Rows[0][0].ToString() + "','_blank'); ";
        }
    }
}