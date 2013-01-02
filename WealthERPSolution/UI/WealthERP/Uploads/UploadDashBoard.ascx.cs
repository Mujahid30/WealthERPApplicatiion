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


namespace WealthERP.Uploads
{
    public partial class UploadDashBoard : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        AdvisorVo advisorVo;
        string path;
        DataTable dtUploadTreeNode;
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
                BindReptUploadGrid();
            }
        }
    }
            private void BindReptUploadGrid()
        {
            dtUploadTreeNode = XMLBo.GetUploadTreeNode(path);

            string expression = "NodeType = 'Upload'";
            dtUploadTreeNode.DefaultView.RowFilter = expression;
            rptTree.DataSource = dtUploadTreeNode.DefaultView.ToTable();

            rptTree.DataBind();
        }
        protected void rptTree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ImageButton imgBtn1 = e.Item.FindControl("imgbtnUpload1") as ImageButton;
            LinkButton lnkbtn1 = e.Item.FindControl("lnkTreeNode1") as LinkButton;

            ImageButton imgBtn2 = e.Item.FindControl("imgbtnUpload2") as ImageButton;
            LinkButton lnkbtn2 = e.Item.FindControl("lnkTreeNode2") as LinkButton;

            ImageButton imgBtn3 = e.Item.FindControl("imgbtnUpload3") as ImageButton;
            LinkButton lnkbtn3 = e.Item.FindControl("lnkTreeNode3") as LinkButton;
            if (e.CommandName == "Tree_Navi_Row1")
            {
 
                if (imgBtn1.CommandArgument == "Upload" || lnkbtn1.CommandArgument == "Upload")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('CustomerUpload','login');", true);
                }
                else if (imgBtn1.CommandArgument == "MFTransaction_Exception" || lnkbtn1.CommandArgument == "MFTransaction_Exception")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedMFTransactionStaging','login');", true);
                }
                else if (imgBtn1.CommandArgument == "EQTrade_Account_Exception" || lnkbtn1.CommandArgument == "EQTrade_Account_Exception")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedTradeAccountStaging','login');", true);
                }
                else if (imgBtn1.CommandArgument == "Profile_Exception" || lnkbtn1.CommandArgument == "Profile_Exception")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedWERPProfile','login');", true);
                }
            }
            if (e.CommandName == "Tree_Navi_Row2")
            {

                if (imgBtn2.CommandArgument == "Upload_History" || lnkbtn2.CommandArgument == "Upload_History")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('ViewUploadProcessLog','login');", true);
                }
                else if (imgBtn2.CommandArgument == "SIP_Exception" || lnkbtn2.CommandArgument == "SIP_Exception")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedSystematicTransactionStaging','login');", true);
                }
                else if (imgBtn2.CommandArgument == "EQTransaction_Exception" || lnkbtn2.CommandArgument == "EQTransaction_Exception")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedEquityTransactionStaging','login');", true);
                }
            }

            if (e.CommandName == "Tree_Navi_Row3")
            {
                if (imgBtn3.CommandArgument == "Folio_Exception" || lnkbtn3.CommandArgument == "Folio_Exception")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedMFFolio','login');", true);
                }
                else if (imgBtn3.CommandArgument == "Trail_Exception" || lnkbtn3.CommandArgument == "Trail_Exception")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('TrailCommisionTransactionRejects','login');", true);
                }
                else if (imgBtn3.CommandArgument == "MFReversal_Tranx_Exception" || lnkbtn3.CommandArgument == "MFReversal_Tranx_Exception")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('ReverseTransactionExceptionHandling','login');", true);
                }
            }

            
        }
        protected void rptTree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ImageButton col1 = e.Item.FindControl("imgbtnUpload2") as ImageButton;
                ImageButton col0 = e.Item.FindControl("imgbtnUpload1") as ImageButton;
                ImageButton col2 = e.Item.FindControl("imgbtnUpload3") as ImageButton;
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