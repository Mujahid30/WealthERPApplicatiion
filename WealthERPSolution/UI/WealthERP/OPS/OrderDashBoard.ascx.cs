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

namespace WealthERP.OPS
{
    public partial class OrderDashBoard : System.Web.UI.UserControl
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
                BindReptOrderDashBoard();
            }
        }

        private void BindReptOrderDashBoard()
        {
            DataSet dsTreeNodes = new DataSet();
            dsTreeNodes = XMLBo.GetSuperAdminTreeNodes(path);
            DataRow[] drXmlTreeSubSubNode;
            DataRow drOrderTreeNode;
            DataTable dtOrderTreeNode = new DataTable();

            dtOrderTreeNode.Columns.Add("TreeNode1", typeof(Int32));

            dtOrderTreeNode.Columns.Add("TreeNodeText1", typeof(String));

            dtOrderTreeNode.Columns.Add("Path1", typeof(String));

            dtOrderTreeNode.Columns.Add("TreeNode2", typeof(Int32));

            dtOrderTreeNode.Columns.Add("TreeNodeText2", typeof(String));

            dtOrderTreeNode.Columns.Add("Path2", typeof(String));

            dtOrderTreeNode.Columns.Add("TreeNode3", typeof(Int32));

            dtOrderTreeNode.Columns.Add("TreeNodeText3", typeof(String));

            dtOrderTreeNode.Columns.Add("Path3", typeof(String));

            dtOrderTreeNode.Columns.Add("TreeNode4", typeof(Int32));

            dtOrderTreeNode.Columns.Add("TreeNodeText4", typeof(String));

            dtOrderTreeNode.Columns.Add("Path4", typeof(String));

            //For Order 2011 is Tree Node Id in Sub Table in XML...
            int treeSubNodeId = 2011;
            drXmlTreeSubSubNode = dsTreeNodes.Tables[2].Select("TreeSubNodeCode=" + treeSubNodeId.ToString());

            int count = 0;
            drOrderTreeNode = dtOrderTreeNode.NewRow();
            foreach (DataRow drSubSubNode in drXmlTreeSubSubNode)
            {
                if (count == 0)
                {

                    count++;
                    drOrderTreeNode["TreeNode1"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drOrderTreeNode["TreeNodeText1"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drOrderTreeNode["Path1"] = drSubSubNode["Path"].ToString();
                    dtOrderTreeNode.Rows.Add(drOrderTreeNode);

                }
                else if (count == 1)
                {
                    count++;
                    drOrderTreeNode["TreeNode2"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drOrderTreeNode["TreeNodeText2"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drOrderTreeNode["Path2"] = drSubSubNode["Path"].ToString();

                }
                else if (count == 2)
                {
                    count++;
                    drOrderTreeNode["TreeNode3"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drOrderTreeNode["TreeNodeText3"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drOrderTreeNode["Path3"] = drSubSubNode["Path"].ToString();
                }
                else if (count == 3)
                {
                    count++;
                    drOrderTreeNode["TreeNode4"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drOrderTreeNode["TreeNodeText4"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    drOrderTreeNode["Path4"] = drSubSubNode["Path"].ToString();
                    count = 0;
                    drOrderTreeNode = dtOrderTreeNode.NewRow();
                }


            }
            rptOrderTree.DataSource = dtOrderTreeNode;
            rptOrderTree.DataBind();
        }
        protected void rptOrderTree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ImageButton imgBtn1 = e.Item.FindControl("imgbtnOrder1") as ImageButton;
            LinkButton lnkbtn1 = e.Item.FindControl("lnkOrderTreeNode1") as LinkButton;

            ImageButton imgBtn2 = e.Item.FindControl("imgbtnOrder2") as ImageButton;
            LinkButton lnkbtn2 = e.Item.FindControl("lnkOrderTreeNode2") as LinkButton;

            ImageButton imgBtn3 = e.Item.FindControl("imgbtnOrder3") as ImageButton;
            LinkButton lnkbtn3 = e.Item.FindControl("lnkOrderTreeNode3") as LinkButton;

            ImageButton imgBtn4 = e.Item.FindControl("imgbtnOrder4") as ImageButton;
            LinkButton lnkbtn4 = e.Item.FindControl("lnkOrderTreeNode4") as LinkButton;

            if (e.CommandName == "Tree_Navi_Row1")
            {

                if (imgBtn1.CommandArgument == "3016" || lnkbtn1.CommandArgument == "3016")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('MFOrderEntry','login');", true);
                }
            }
            if (e.CommandName == "Tree_Navi_Row2")
            {

                if (imgBtn2.CommandArgument == "3017" || lnkbtn2.CommandArgument == "3017")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('LifeInsuranceOrderEntry','login');", true);
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
        protected void rptOrderTree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ImageButton col0 = e.Item.FindControl("imgbtnOrder1") as ImageButton;
                ImageButton col1 = e.Item.FindControl("imgbtnOrder2") as ImageButton;
                ImageButton col2 = e.Item.FindControl("imgbtnOrder3") as ImageButton;
                ImageButton col3 = e.Item.FindControl("imgbtnOrder4") as ImageButton;
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