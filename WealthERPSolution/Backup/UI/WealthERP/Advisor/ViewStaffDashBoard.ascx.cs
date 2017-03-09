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

namespace WealthERP.Advisor
{
    public partial class ViewStaffDashBoard : System.Web.UI.UserControl
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
                BindReptCommissionDashBoard();
            }
        }

        private void BindReptCommissionDashBoard()
        {
            DataSet dsTreeNodes = new DataSet();
            dsTreeNodes = XMLBo.GetSuperAdminTreeNodes(path);
            DataRow[] drXmlTreeSubSubNode;
            DataRow drCommissionTreeNode;
            DataTable dtCommissionTreeNode = new DataTable();

            dtCommissionTreeNode.Columns.Add("TreeNode1", typeof(Int32));

            dtCommissionTreeNode.Columns.Add("TreeNodeText1", typeof(String));

            dtCommissionTreeNode.Columns.Add("Path1", typeof(String));

            dtCommissionTreeNode.Columns.Add("TreeNode2", typeof(Int32));

            dtCommissionTreeNode.Columns.Add("TreeNodeText2", typeof(String));
            dtCommissionTreeNode.Columns.Add("Path2", typeof(String));

            //For Commission 2015 is Tree Node Id in Sub Table in XML...
            int treeSubNodeId = 2017;
            drXmlTreeSubSubNode = dsTreeNodes.Tables[2].Select("TreeSubNodeCode=" + treeSubNodeId.ToString());

            int count = 0;
            drCommissionTreeNode = dtCommissionTreeNode.NewRow();
            foreach (DataRow drSubSubNode in drXmlTreeSubSubNode)
            {
                if (count == 0)
                {

                    count++;
                    drCommissionTreeNode["TreeNode1"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drCommissionTreeNode["TreeNodeText1"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drCommissionTreeNode["Path1"] = drSubSubNode["Path"].ToString();
                    dtCommissionTreeNode.Rows.Add(drCommissionTreeNode);

                }
                else if (count == 1)
                {
                    count++;
                    drCommissionTreeNode["TreeNode2"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drCommissionTreeNode["TreeNodeText2"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drCommissionTreeNode["Path2"] = drSubSubNode["Path"].ToString();
                    count = 0;
                    drCommissionTreeNode = dtCommissionTreeNode.NewRow();

                }
                //else if (count == 1)
                //{
                //    count++;
                //    drCommissionTreeNode["TreeNode2"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                //    drCommissionTreeNode["TreeNodeText2"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                //    drCommissionTreeNode["Path2"] = drSubSubNode["Path"].ToString();

                //}
                //else if (count == 2)
                //{
                //    count++;
                //    drCommissionTreeNode["TreeNode3"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                //    drCommissionTreeNode["TreeNodeText3"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                //    drCommissionTreeNode["Path3"] = drSubSubNode["Path"].ToString();
                //}
                //else if (count == 3)
                //{
                //    count++;
                //    drCommissionTreeNode["TreeNode4"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                //    drCommissionTreeNode["TreeNodeText4"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                //    drCommissionTreeNode["Path4"] = drSubSubNode["Path"].ToString();
                //    count = 0;
                //    drCommissionTreeNode = dtCommissionTreeNode.NewRow();
                //}


            }
            rptCommissionTree.DataSource = dtCommissionTreeNode;
            rptCommissionTree.DataBind();
        }

        protected void rptCommissionTree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lnkbtn1 = e.Item.FindControl("lnkCommissionTreeNode1") as LinkButton;
            LinkButton lnkbtn2 = e.Item.FindControl("lnkCommissionTreeNode2") as LinkButton;

            if (e.CommandName == "Tree_Navi_Row1")
            {

                if (lnkbtn1.CommandArgument == "3031")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('ViewRM','login');", true);
                }
            }
            if (e.CommandName == "Tree_Navi_Row2")
            {

                if (lnkbtn2.CommandArgument == "3032")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewStaff','login');", true);
                }

            }


        }

    }
}