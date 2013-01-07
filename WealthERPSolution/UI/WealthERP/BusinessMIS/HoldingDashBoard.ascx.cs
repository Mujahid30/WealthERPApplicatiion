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

namespace WealthERP.BusinessMIS
{
    public partial class HoldingDashBoard : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        AdvisorVo advisorVo;
        string path;
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
                BindReptHoldingsDashBoard();
            }
        }

        private void BindReptHoldingsDashBoard()
        {
            DataSet dsTreeNodes = new DataSet();
            dsTreeNodes = XMLBo.GetSuperAdminTreeNodes(path);
            DataRow[] drXmlTreeSubSubNode;
            DataRow drHoldingsTreeNode;
            DataTable dtHoldingsTreeNode = new DataTable();

            dtHoldingsTreeNode.Columns.Add("TreeNode1", typeof(Int32));

            dtHoldingsTreeNode.Columns.Add("TreeNodeText1", typeof(String));

            dtHoldingsTreeNode.Columns.Add("Path1", typeof(String));

            dtHoldingsTreeNode.Columns.Add("TreeNode2", typeof(Int32));

            dtHoldingsTreeNode.Columns.Add("TreeNodeText2", typeof(String));

            dtHoldingsTreeNode.Columns.Add("Path2", typeof(String));

            dtHoldingsTreeNode.Columns.Add("TreeNode3", typeof(Int32));

            dtHoldingsTreeNode.Columns.Add("TreeNodeText3", typeof(String));

            dtHoldingsTreeNode.Columns.Add("Path3", typeof(String));

            dtHoldingsTreeNode.Columns.Add("TreeNode4", typeof(Int32));

            dtHoldingsTreeNode.Columns.Add("TreeNodeText4", typeof(String));

            dtHoldingsTreeNode.Columns.Add("Path4", typeof(String));

            //For holding dashboard 2013 is Tree Node Id in Sub Table in XML...
            int treeSubNodeId = 2013;
            drXmlTreeSubSubNode = dsTreeNodes.Tables[2].Select("TreeSubNodeCode=" + treeSubNodeId.ToString());

            int count = 0;
            drHoldingsTreeNode = dtHoldingsTreeNode.NewRow();
            foreach (DataRow drSubSubNode in drXmlTreeSubSubNode)
            {
                if (count == 0)
                {

                    count++;
                    drHoldingsTreeNode["TreeNode1"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drHoldingsTreeNode["TreeNodeText1"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drHoldingsTreeNode["Path1"] = drSubSubNode["Path"].ToString();
                    dtHoldingsTreeNode.Rows.Add(drHoldingsTreeNode);

                }
                else if (count == 1)
                {
                    count++;
                    drHoldingsTreeNode["TreeNode2"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drHoldingsTreeNode["TreeNodeText2"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drHoldingsTreeNode["Path2"] = drSubSubNode["Path"].ToString();

                }
                else if (count == 2)
                {
                    count++;
                    drHoldingsTreeNode["TreeNode3"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drHoldingsTreeNode["TreeNodeText3"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drHoldingsTreeNode["Path3"] = drSubSubNode["Path"].ToString();
                }
                else if (count == 3)
                {
                    count++;
                    drHoldingsTreeNode["TreeNode4"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drHoldingsTreeNode["TreeNodeText4"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drHoldingsTreeNode["Path4"] = drSubSubNode["Path"].ToString();
                    count = 0;
                    drHoldingsTreeNode = dtHoldingsTreeNode.NewRow();
                }


            }
            rptHoldingTree.DataSource = dtHoldingsTreeNode;
            rptHoldingTree.DataBind();
        }
        protected void rptHoldingTree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ImageButton imgBtn1 = e.Item.FindControl("imgbtnHolding1") as ImageButton;
            LinkButton lnkbtn1 = e.Item.FindControl("lnkHoldingTreeNode1") as LinkButton;

            ImageButton imgBtn2 = e.Item.FindControl("imgbtnHolding2") as ImageButton;
            LinkButton lnkbtn2 = e.Item.FindControl("lnkHoldingTreeNode2") as LinkButton;

            ImageButton imgBtn3 = e.Item.FindControl("imgbtnHolding3") as ImageButton;
            LinkButton lnkbtn3 = e.Item.FindControl("lnkHoldingTreeNode3") as LinkButton;

            ImageButton imgBtn4 = e.Item.FindControl("imgbtnHolding4") as ImageButton;
            LinkButton lnkbtn4 = e.Item.FindControl("lnkHoldingTreeNode4") as LinkButton;

            if (e.CommandName == "Tree_Navi_Row1")
            {

                if (imgBtn1.CommandArgument == "3020" || lnkbtn1.CommandArgument == "3020")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('MutualFundMIS','login');", true);
                }
            }
            if (e.CommandName == "Tree_Navi_Row2")
            {

                if (imgBtn2.CommandArgument == "3021" || lnkbtn2.CommandArgument == "3021")
                {

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('MultiProductMIS','action=LI');", true);
                }

            }


            if (e.CommandName == "Tree_Navi_Row3")
            {
                if (imgBtn3.CommandArgument == "3022" || lnkbtn3.CommandArgument == "3022")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('MultiProductMIS','action=GI');", true);
                }

            }
            if (e.CommandName == "Tree_Navi_Row4")
            {
                if (imgBtn4.CommandArgument == "3023" || lnkbtn4.CommandArgument == "3023")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('MultiProductMIS','action=FI');", true);
                }

            }


        }
        protected void rptHoldingTree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ImageButton col0 = e.Item.FindControl("imgbtnHolding1") as ImageButton;
                ImageButton col1 = e.Item.FindControl("imgbtnHolding2") as ImageButton;
                ImageButton col2 = e.Item.FindControl("imgbtnHolding3") as ImageButton;
                ImageButton col3 = e.Item.FindControl("imgbtnHolding4") as ImageButton;
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
                if (col3.ImageUrl == "")
                {
                    var a = e.Item.FindControl("td4");
                    a.Visible = false;
                }

            }
        }
    }
}