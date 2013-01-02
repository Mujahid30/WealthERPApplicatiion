using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon;
using System.Configuration;
using BoCustomerPortfolio;
using System.Data;

namespace WealthERP.Reports
{
    public partial class CustomerReportsDashBoard : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        AdvisorVo advisorVo;
        string path;
        DataTable dtReportTreeNode;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            userVo = (UserVo)Session[SessionContents.UserVo];
            AssetBo assetBo = new AssetBo();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            if (!IsPostBack)
            {
                BindReptReportDashBoard();
            }
        }
        private void BindReptReportDashBoard()
        {
            dtReportTreeNode = XMLBo.GetUploadTreeNode(path);

            string expression = "NodeType = 'Reports'";
            dtReportTreeNode.DefaultView.RowFilter = expression;
            rptReportTree.DataSource = dtReportTreeNode.DefaultView.ToTable();
            rptReportTree.DataBind();
        }
        protected void rptReportTree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ImageButton imgBtn1 = e.Item.FindControl("imgbtnReport1") as ImageButton;
            LinkButton lnkbtn1 = e.Item.FindControl("lnkReportTreeNode1") as LinkButton;

            ImageButton imgBtn2 = e.Item.FindControl("imgbtnReport2") as ImageButton;
            LinkButton lnkbtn2 = e.Item.FindControl("lnkReportTreeNode2") as LinkButton;

            ImageButton imgBtn3 = e.Item.FindControl("imgbtnReport3") as ImageButton;
            LinkButton lnkbtn3 = e.Item.FindControl("lnkReportTreeNode3") as LinkButton;
            if (e.CommandName == "Tree_Navi_Row1")
            {

                if (imgBtn1.CommandArgument == "MF_Report" || lnkbtn1.CommandArgument == "MF_Report")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('MFReports','login');", true);
                }
                else if (imgBtn1.CommandArgument == "FP_Report" || lnkbtn1.CommandArgument == "FP_Report")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('FPSectional','login');", true);
                }
            }
            if (e.CommandName == "Tree_Navi_Row2")
            {

                if (imgBtn2.CommandArgument == "MultiAsset_Report" || lnkbtn2.CommandArgument == "MultiAsset_Report")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('PortfolioReports','login');", true);
                }

            }

            if (e.CommandName == "Tree_Navi_Row3")
            {
                if (imgBtn3.CommandArgument == "Equity_Report" || lnkbtn3.CommandArgument == "Equity_Report")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('EquityReports','login');", true);
                }

            }


        }
        protected void rptReportTree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ImageButton col1 = e.Item.FindControl("imgbtnReport2") as ImageButton;
                ImageButton col0 = e.Item.FindControl("imgbtnReport1") as ImageButton;
                ImageButton col2 = e.Item.FindControl("imgbtnReport3") as ImageButton;
                if (col1.ImageUrl == "")
                {
                    var a = e.Item.FindControl("td2");
                    a.Visible = false;
                }
                if (col0.ImageUrl == "")
                {
                    var a = e.Item.FindControl("td1");
                    a.Visible = false;
                }
                if (col2.ImageUrl == "")
                {
                    var a = e.Item.FindControl("td3");
                    a.Visible = false;
                }

            }
        }
    }
}