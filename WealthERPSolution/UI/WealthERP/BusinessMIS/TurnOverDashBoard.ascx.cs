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
    public partial class TurnOverDashBoard : System.Web.UI.UserControl
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
                BindReptTurnoverDashBoard();
            }
        }
        private void BindReptTurnoverDashBoard()
        {
            DataSet dsTreeNodes = new DataSet();
            dsTreeNodes = XMLBo.GetSuperAdminTreeNodes(path);
            DataRow[] drXmlTreeSubSubNode;
            DataRow drTurnoverTreeNode;
            DataTable dtTurnoverTreeNode = new DataTable();

            dtTurnoverTreeNode.Columns.Add("TreeNode1", typeof(Int32));

            dtTurnoverTreeNode.Columns.Add("TreeNodeText1", typeof(String));

            dtTurnoverTreeNode.Columns.Add("Path1", typeof(String));

            dtTurnoverTreeNode.Columns.Add("TreeNode2", typeof(Int32));

            dtTurnoverTreeNode.Columns.Add("TreeNodeText2", typeof(String));

            dtTurnoverTreeNode.Columns.Add("Path2", typeof(String));

            dtTurnoverTreeNode.Columns.Add("TreeNode3", typeof(Int32));

            dtTurnoverTreeNode.Columns.Add("TreeNodeText3", typeof(String));

            dtTurnoverTreeNode.Columns.Add("Path3", typeof(String));

            dtTurnoverTreeNode.Columns.Add("TreeNode4", typeof(Int32));

            dtTurnoverTreeNode.Columns.Add("TreeNodeText4", typeof(String));

            dtTurnoverTreeNode.Columns.Add("Path4", typeof(String));

            //For Turnover 2012 is Tree Node Id in Sub Table in XML...
            int treeSubNodeId = 2012;
            drXmlTreeSubSubNode = dsTreeNodes.Tables[2].Select("TreeSubNodeCode=" + treeSubNodeId.ToString());

            int count = 0;
            drTurnoverTreeNode = dtTurnoverTreeNode.NewRow();
            foreach (DataRow drSubSubNode in drXmlTreeSubSubNode)
            {
                if (count == 0)
                {

                    count++;
                    drTurnoverTreeNode["TreeNode1"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drTurnoverTreeNode["TreeNodeText1"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drTurnoverTreeNode["Path1"] = drSubSubNode["Path"].ToString();
                    dtTurnoverTreeNode.Rows.Add(drTurnoverTreeNode);

                }
                else if (count == 1)
                {
                    count++;
                    drTurnoverTreeNode["TreeNode2"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drTurnoverTreeNode["TreeNodeText2"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drTurnoverTreeNode["Path2"] = drSubSubNode["Path"].ToString();

                }
                else if (count == 2)
                {
                    count++;
                    drTurnoverTreeNode["TreeNode3"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drTurnoverTreeNode["TreeNodeText3"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drTurnoverTreeNode["Path3"] = drSubSubNode["Path"].ToString();
                }
                else if (count == 3)
                {
                    count++;
                    drTurnoverTreeNode["TreeNode4"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drTurnoverTreeNode["TreeNodeText4"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drTurnoverTreeNode["Path4"] = drSubSubNode["Path"].ToString();
                    count = 0;
                    drTurnoverTreeNode = dtTurnoverTreeNode.NewRow();
                }


            }
            rptTurnoverTree.DataSource = dtTurnoverTreeNode;
            rptTurnoverTree.DataBind();
        }
        protected void rptTurnoverTree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ImageButton imgBtn1 = e.Item.FindControl("imgbtnTurnover1") as ImageButton;
            LinkButton lnkbtn1 = e.Item.FindControl("lnkTurnoverTreeNode1") as LinkButton;

            ImageButton imgBtn2 = e.Item.FindControl("imgbtnTurnover2") as ImageButton;
            LinkButton lnkbtn2 = e.Item.FindControl("lnkTurnoverTreeNode2") as LinkButton;

            ImageButton imgBtn3 = e.Item.FindControl("imgbtnTurnover3") as ImageButton;
            LinkButton lnkbtn3 = e.Item.FindControl("lnkTurnoverTreeNode3") as LinkButton;

            ImageButton imgBtn4 = e.Item.FindControl("imgbtnTurnover4") as ImageButton;
            LinkButton lnkbtn4 = e.Item.FindControl("lnkTurnoverTreeNode4") as LinkButton;

            if (e.CommandName == "Tree_Navi_Row1")
            {

                if (imgBtn1.CommandArgument == "3018" || lnkbtn1.CommandArgument == "3018")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('MFTurnOverMIS','login');", true);
                }
            }
            if (e.CommandName == "Tree_Navi_Row2")
            {

                if (imgBtn2.CommandArgument == "3019" || lnkbtn2.CommandArgument == "3019")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Comming Soon shortly......');", true);
                }

            }


            //if (e.CommandName == "Tree_Navi_Row3")
            //{
            //    if (imgBtn3.CommandArgument == "3014" || lnkbtn3.CommandArgument == "3014")
            //    {
            //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('EquityReports','login');", true);
            //    }

            //}
            //if (e.CommandName == "Tree_Navi_Row4")
            //{
            //    if (imgBtn4.CommandArgument == "3015" || lnkbtn4.CommandArgument == "3015")
            //    {
            //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('FPSectional','login');", true);
            //    }

            //}


        }
        protected void rptTurnoverTree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ImageButton col0 = e.Item.FindControl("imgbtnTurnover1") as ImageButton;
                ImageButton col1 = e.Item.FindControl("imgbtnTurnover2") as ImageButton;
                ImageButton col2 = e.Item.FindControl("imgbtnTurnover3") as ImageButton;
                ImageButton col3 = e.Item.FindControl("imgbtnTurnover4") as ImageButton;
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