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

            dtReportTreeNode.Columns.Add("TreeNode2", typeof(Int32));

            dtReportTreeNode.Columns.Add("TreeNodeText2", typeof(String));

            if (advisorVo.advisorId == Convert.ToInt32(ConfigurationSettings.AppSettings["ONLINE_ADVISER"]))
            {
               
            }
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
                         
                            dtReportTreeNode.Rows.Add(drReportTreeNode);

                        }
                        else if (count == 1)
                        {
                            count++;
                            drReportTreeNode["TreeNode2"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                            drReportTreeNode["TreeNodeText2"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                       
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
            LinkButton lnkbtn1 = e.Item.FindControl("lnkReportTreeNode1") as LinkButton;

            LinkButton lnkbtn2 = e.Item.FindControl("lnkReportTreeNode2") as LinkButton;

            if (e.CommandName == "Tree_Navi_Row1")
            {

                if ( lnkbtn1.CommandArgument == "3012")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('MFReportUI','login');", true);
                }
                //else if (imgBtn1.CommandArgument == "3015" || lnkbtn1.CommandArgument == "3015")
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('FPSectional','login');", true);
                //}

                if (lnkbtn1.CommandArgument == "3014")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('EQReportUI','login');", true);
                }
            }
            if (e.CommandName == "Tree_Navi_Row2")
            {

                if (lnkbtn2.CommandArgument == "3013")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('PortfolioReports','login');", true);
                }

                if ( lnkbtn2.CommandArgument == "3015")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('FPSectional','login');", true);
                }


            }

          
       

        }
        protected void rptReportTree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                LinkButton col1 = e.Item.FindControl("lnkReportTreeNode1") as LinkButton;
                LinkButton col0 = e.Item.FindControl("lnkReportTreeNode2") as LinkButton;
                if (col1.Text == "Equity Report")
                {
                   
                }
                LinkButton col1E = e.Item.FindControl("lnkReportTreeNode1") as LinkButton;
                //ImageButton col0 = e.Item.FindControl("imgbtnReport1") as ImageButton;
                ImageButton col2 = e.Item.FindControl("imgbtnReport3") as ImageButton;
                ImageButton col3 = e.Item.FindControl("imgbtnReport4") as ImageButton;
                if (advisorVo.advisorId == Convert.ToInt32(ConfigurationSettings.AppSettings["ONLINE_ADVISER"]))
                {
                    if (col1 != null)
                    {

                        var a = e.Item.FindControl("td2");
                        a.Visible = false;
                    }
                    if (col1E.Text == "Equity Report")
                    {
                        var a = e.Item.FindControl("td1");
                        a.Visible = false;
                    }
                }
                //if (col2.ImageUrl == "")
                //{
                //    var a = e.Item.FindControl("td3");
                //    a.Visible = false;
                //}
                //if (col3.ImageUrl == "")
                //{
                //    var a = e.Item.FindControl("td4");
                //    a.Visible = false;
                //}

            }
        }
    }
}