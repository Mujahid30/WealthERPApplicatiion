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
using System.Web.UI.HtmlControls;

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



            dtTurnoverTreeNode.Columns.Add("TreeNode2", typeof(Int32));

            dtTurnoverTreeNode.Columns.Add("TreeNodeText2", typeof(String));
            dtTurnoverTreeNode.Columns.Add("TreeNode3", typeof(Int32));

            dtTurnoverTreeNode.Columns.Add("TreeNodeText3", typeof(String));

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

                    dtTurnoverTreeNode.Rows.Add(drTurnoverTreeNode);

                }
                else if (count == 1)
                {
                    count++;
                    drTurnoverTreeNode["TreeNode2"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drTurnoverTreeNode["TreeNodeText2"] = drSubSubNode["TreeSubSubNodeText"].ToString();

                    //count = 0;
                    //drTurnoverTreeNode = dtTurnoverTreeNode.NewRow();
                }
                else if (count == 2)
                {

                    drTurnoverTreeNode["TreeNode3"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drTurnoverTreeNode["TreeNodeText3"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    count = 0;
                    drTurnoverTreeNode = dtTurnoverTreeNode.NewRow();
                }
            }


            rptTurnoverTree.DataSource = dtTurnoverTreeNode;
            rptTurnoverTree.DataBind();
        }
        protected void rptTurnoverTree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lnkbtn1 = e.Item.FindControl("lnkTurnoverTreeNode1") as LinkButton;
            LinkButton lnkbtn2 = e.Item.FindControl("lnkTurnoverTreeNode2") as LinkButton;
            LinkButton lnkbtn3 = e.Item.FindControl("lnkOrderTurnOver") as LinkButton;


            if (e.CommandName == "Tree_Navi_Row1")
            {

                if (lnkbtn1.CommandArgument == "3018")
                {
                    if (advisorVo.A_AgentCodeBased == 1)
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('MFTransactionTurnOverMIS','login');", true);
                    else
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('MFTurnOverMIS','login');", true);
                }
                if (lnkbtn1.CommandArgument == "3018")
                {
                    if (advisorVo.A_AgentCodeBased == 1)
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('MFTransactionTurnOverMIS','login');", true);
                    else
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('MFTurnOverMIS','login');", true);
                }
            }
            if (e.CommandName == "Tree_Navi_Row2")
            {

                if (lnkbtn2.CommandArgument == "3019")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('EquityTurnover','login');", true);
                }
                if (lnkbtn2.CommandArgument == "3019")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('EquityTurnover','login');", true);
                }


            }
            if (e.CommandName == "Tree_Navi_Row3")
            {

                if (lnkbtn3.CommandArgument == "3031")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('MFTurnOverMISSales','login');", true);
                }
                if (lnkbtn3.CommandArgument == "3031")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('MFTurnOverMISSales','login');", true);
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
            if (userVo.UserType == "Advisor") return; 

            TableCell td1 = e.Item.FindControl("td1") as TableCell;
            TableCell td2 = e.Item.FindControl("td2") as TableCell;

            if (td1 != null) td1.Visible = false;
            if (td2 != null) td2.Visible = false;
        }
    }
}