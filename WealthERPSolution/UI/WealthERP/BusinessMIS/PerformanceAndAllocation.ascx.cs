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
    public partial class PerformanceAndAllocation : System.Web.UI.UserControl
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
                BindReptPerformanceDashBoard();
                if (advisorVo.advisorId == Convert.ToInt32(ConfigurationSettings.AppSettings["ONLINE_ADVISER"]))
                {
                    foreach (RepeaterItem ri in rptPerformanceTree.Items)
                        ri.FindControl("eqtrans").Visible = false;
                }
                else
                {
                    foreach (RepeaterItem ri in rptPerformanceTree.Items)
                        ri.FindControl("eqtrans").Visible = true;
                }
            }
        }
        private void BindReptPerformanceDashBoard()
        {
            DataSet dsTreeNodes = new DataSet();
            dsTreeNodes = XMLBo.GetSuperAdminTreeNodes(path);
            DataRow[] drXmlTreeSubSubNode;
            DataRow drPerformanceTreeNode;
            DataTable dtPerformanceTreeNode = new DataTable();

            dtPerformanceTreeNode.Columns.Add("TreeNode1", typeof(Int32));

            dtPerformanceTreeNode.Columns.Add("TreeNodeText1", typeof(String));



            dtPerformanceTreeNode.Columns.Add("TreeNode2", typeof(Int32));

            dtPerformanceTreeNode.Columns.Add("TreeNodeText2", typeof(String));


            //For PerformanceAllocation 2016 is Tree Node Id in Sub Table in XML...
            int treeSubNodeId = 2016;
            drXmlTreeSubSubNode = dsTreeNodes.Tables[2].Select("TreeSubNodeCode=" + treeSubNodeId.ToString());

            int count = 0;
            drPerformanceTreeNode = dtPerformanceTreeNode.NewRow();
            foreach (DataRow drSubSubNode in drXmlTreeSubSubNode)
            {
                if (count == 0)
                {

                    count++;
                    drPerformanceTreeNode["TreeNode1"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drPerformanceTreeNode["TreeNodeText1"] = drSubSubNode["TreeSubSubNodeText"].ToString();

                    dtPerformanceTreeNode.Rows.Add(drPerformanceTreeNode);

                }
                else if (count == 1)
                {
                    count++;
                    drPerformanceTreeNode["TreeNode2"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drPerformanceTreeNode["TreeNodeText2"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    count = 0;
                    drPerformanceTreeNode = dtPerformanceTreeNode.NewRow();

                }


            }
            rptPerformanceTree.DataSource = dtPerformanceTreeNode;
            rptPerformanceTree.DataBind();
        }
        protected void rptPerformanceTree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lnkbtn1 = e.Item.FindControl("lnkPerformanceTreeNode1") as LinkButton;

            LinkButton lnkbtn2 = e.Item.FindControl("lnkPerformanceTreeNode2") as LinkButton;

            if (e.CommandName == "Tree_Navi_Row1")
            {

                if (lnkbtn1.CommandArgument == "3027")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('MFReturns','login');", true);
                }
                if (lnkbtn1.CommandArgument == "3027")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('MFReturns','login');", true);
                }
            }
            if (e.CommandName == "Tree_Navi_Row2")
            {

                if (lnkbtn2.CommandArgument == "3028")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('EquityReturns','login');", true);
                }
                if (lnkbtn2.CommandArgument == "3028")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('EquityReturns','login');", true);
                }
            }

        }
    }
}