using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using BoOnlineOrderManagement;
using Telerik.Web.UI;
namespace WealthERP.OnlineOrder
{
    public partial class Banner : System.Web.UI.UserControl
    {
        public string assetCategory { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnlineOrderBo onlineOrderBo = new OnlineOrderBo();
                string path = ConfigurationManager.AppSettings["BANNER_IMAGE_PATH"].ToString();
                if (Request.QueryString["Type"] != null)
                {
                    if (Request.QueryString["Type"] == "Demo")
                        SetDemoLink("MF");
                    else if (Request.QueryString["Type"] == "FAQ")
                        SetFAQLink("MF");
                }
                else
                {
                    string innerHtml = String.Empty;
                    DataTable dtProductBannerDetails = new DataTable();

                    dtProductBannerDetails = onlineOrderBo.GetImageListForBanner(assetCategory);

                    foreach (DataRow dr in dtProductBannerDetails.Rows)
                    {
                        innerHtml += string.Format(@"<li><img src=""{0}{1}"" /></li>", path.Replace("~", ".."), dr["PDB_BannerImage"].ToString());
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "SetBannerImageDetails", @"SetBannerImageDetails('" + innerHtml + "');", true);
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
                radFaq.DataSource = dt;
                radFaq.DataBind();
            }
            //lnkFAQ.OnClientClick = @"window.open('" + path.Replace("~", "..") + dt.Rows[0][0].ToString() + "','_blank'); ";
        }
        protected void lbtFaq1_OnClick(object sender, EventArgs e)
        {
            //LinkButton btn = (LinkButton)sender;
            //GridViewRow row = (GridViewRow)btn.NamingContainer;
            //string pdfName = gridFAQ.DataKeys[row.RowIndex].Value.ToString();
            string path = ConfigurationManager.AppSettings["BANNER_IMAGE_PATH"].ToString();
            //Response.Redirect(path + pdfName);
            //Response.Redirect("~/Images/InvestorPageSlider/535638211_Jun2014.PDF");

            GridDataItem grdrow = (GridDataItem)((LinkButton)sender).NamingContainer;

            string associateid = radFaq.MasterTableView.DataKeyValues[grdrow.ItemIndex]["PUHD_HelpDetails"].ToString();
            Response.Redirect(path + associateid);
            //btnPreviewSend.PostBackUrl = "~/Reports/Display.aspx?welcomeNote=1&associateId=" + associateId.ToString();
        }

        protected void radFaq_ItemDataBound(Object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                GridDataItem item = (GridDataItem)e.Item;
                LinkButton lbtFaq1 = (LinkButton)item.FindControl("lbtnradFaq");
                lbtFaq1.OnClientClick = "window.document.forms[0].target='_blank'; setTimeout(function(){window.document.forms[0].target='';}, 500);";
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
    }
}