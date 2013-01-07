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
        int roleId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            userVo = (UserVo)Session[SessionContents.UserVo];
            AssetBo assetBo = new AssetBo();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
                roleId = 1000;
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                roleId = 1004;
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                roleId = 1001;
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                roleId = 1002;

            if (!IsPostBack)
            {
                BindReptReportDashBoard();
            }
        }
        private void BindReptReportDashBoard()
        {
           // dtReportTreeNode = XMLBo.GetUploadTreeNode(path);
            DataSet dsTreeNodes = new DataSet();
            DataTable dtRoleAssociationTreeNode = new DataTable();
            //string expression = "NodeType = 'Reports'";
            //dtReportTreeNode.DefaultView.RowFilter = expression;
            dsTreeNodes = XMLBo.GetSuperAdminTreeNodes(path);
            dtRoleAssociationTreeNode = XMLBo.GetRoleAssociationTreeNode(path);

            DataRow[] drXmlTreeSubSubNode;
            DataRow[] drXmlRoleToTreeSubSubNode;

            DataRow drReportTreeNode;
            DataTable dtReportTreeNode = new DataTable();

            dtReportTreeNode.Columns.Add("TreeNode1", typeof(Int32));

            dtReportTreeNode.Columns.Add("TreeNodeText1", typeof(String));

            dtReportTreeNode.Columns.Add("Path1", typeof(String));

            dtReportTreeNode.Columns.Add("TreeNode2", typeof(Int32));

            dtReportTreeNode.Columns.Add("TreeNodeText2", typeof(String));

            dtReportTreeNode.Columns.Add("Path2", typeof(String));

            dtReportTreeNode.Columns.Add("TreeNode3", typeof(Int32));

            dtReportTreeNode.Columns.Add("TreeNodeText3", typeof(String));

            dtReportTreeNode.Columns.Add("Path3", typeof(String));

            dtReportTreeNode.Columns.Add("TreeNode4", typeof(Int32));

            dtReportTreeNode.Columns.Add("TreeNodeText4", typeof(String));

            dtReportTreeNode.Columns.Add("Path4", typeof(String));

            //For upload 2009 is Tree Node Id in Sub Table in XML...
            int treeSubNodeId = 2010;
            drXmlTreeSubSubNode = dsTreeNodes.Tables[2].Select("TreeSubNodeCode=" + treeSubNodeId.ToString());

            int count = 0;
           
            drReportTreeNode = dtReportTreeNode.NewRow();
            foreach (DataRow drSubSubNode in drXmlTreeSubSubNode)
            {
                int roleCount = 0;
                drXmlRoleToTreeSubSubNode = dtRoleAssociationTreeNode.Select("TreeSubSubNodeCode=" + drSubSubNode["TreeSubSubNodeCode"].ToString());

                    if (drXmlRoleToTreeSubSubNode.Count() > 0)
                    {
                        foreach (DataRow drUserRole in drXmlRoleToTreeSubSubNode)
                        {
                            if ( int.Parse(drUserRole["UserRoleId"].ToString()) == roleId)
                            {
                                roleCount++;
                                 break;
                            }
                        }
                    }
                    if (roleCount > 0)
                    {
                        if (count == 0)
                        {

                            count++;
                            drReportTreeNode["TreeNode1"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                            drReportTreeNode["TreeNodeText1"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                            drReportTreeNode["Path1"] = drSubSubNode["Path"].ToString();
                            dtReportTreeNode.Rows.Add(drReportTreeNode);

                        }
                        else if (count == 1)
                        {
                            count++;
                            drReportTreeNode["TreeNode2"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                            drReportTreeNode["TreeNodeText2"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                            drReportTreeNode["Path2"] = drSubSubNode["Path"].ToString();

                        }
                        else if (count == 2)
                        {
                            count++;
                            drReportTreeNode["TreeNode3"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                            drReportTreeNode["TreeNodeText3"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                            drReportTreeNode["Path3"] = drSubSubNode["Path"].ToString();
                        }
                        else if (count == 3)
                        {
                            count++;
                            drReportTreeNode["TreeNode4"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                            drReportTreeNode["TreeNodeText4"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                            drReportTreeNode["Path4"] = drSubSubNode["Path"].ToString();
                            count = 0;
                            drReportTreeNode = dtReportTreeNode.NewRow();
                        }
                    }
                    else
                    {
                       // break;
                    }


            }
            rptReportTree.DataSource = dtReportTreeNode;
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

            ImageButton imgBtn4 = e.Item.FindControl("imgbtnReport4") as ImageButton;
            LinkButton lnkbtn4 = e.Item.FindControl("lnkReportTreeNode4") as LinkButton;

            if (e.CommandName == "Tree_Navi_Row1")
            {

                if (imgBtn1.CommandArgument == "3012" || lnkbtn1.CommandArgument == "3012")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('MFReports','login');", true);
                }
                //else if (imgBtn1.CommandArgument == "3015" || lnkbtn1.CommandArgument == "3015")
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('FPSectional','login');", true);
                //}
            }
            if (e.CommandName == "Tree_Navi_Row2")
            {

                if (imgBtn2.CommandArgument == "3013" || lnkbtn2.CommandArgument == "3013")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('PortfolioReports','login');", true);
                }

                if (imgBtn2.CommandArgument == "3015" || lnkbtn2.CommandArgument == "3015")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('FPSectional','login');", true);
                }


            }

          
            if (e.CommandName == "Tree_Navi_Row3")
            {
                if (imgBtn3.CommandArgument == "3014" || lnkbtn3.CommandArgument == "3014")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('EquityReports','login');", true);
                }

            }
            if (e.CommandName == "Tree_Navi_Row4")
            {
                if (imgBtn4.CommandArgument == "3015" || lnkbtn4.CommandArgument == "3015")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('FPSectional','login');", true);
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
                ImageButton col3 = e.Item.FindControl("imgbtnReport4") as ImageButton;
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