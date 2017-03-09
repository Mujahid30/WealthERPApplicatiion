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
    public partial class TransactionDashBoard : System.Web.UI.UserControl
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
                BindReptTransactionDashBoard();

                if (advisorVo.advisorId ==Convert.ToInt32(ConfigurationSettings.AppSettings["ONLINE_ADVISER"]))
                {
                    foreach (RepeaterItem ri in rptTransationTree.Items)
                        ri.FindControl("eqtrans").Visible = false;
                }
                else
                {
                    foreach (RepeaterItem ri in rptTransationTree.Items)
                        ri.FindControl("eqtrans").Visible = true;
                }
            }
        }

        private void BindReptTransactionDashBoard()
        {
            DataSet dsTreeNodes = new DataSet();
            dsTreeNodes = XMLBo.GetSuperAdminTreeNodes(path);
            DataRow[] drXmlTreeSubSubNode;
            DataRow drTransationTreeNode;
            DataTable dtTransationTreeNode = new DataTable();

            dtTransationTreeNode.Columns.Add("TreeNode1", typeof(Int32));

            dtTransationTreeNode.Columns.Add("TreeNodeText1", typeof(String));

           // dtTransationTreeNode.Columns.Add("Path1", typeof(String));

            dtTransationTreeNode.Columns.Add("TreeNode2", typeof(Int32));

            dtTransationTreeNode.Columns.Add("TreeNodeText2", typeof(String));

            //For Transaction 2014 is Tree Node Id in Sub Table in XML...
            int treeSubNodeId = 2014;
            drXmlTreeSubSubNode = dsTreeNodes.Tables[2].Select("TreeSubNodeCode=" + treeSubNodeId.ToString());

            int count = 0;
            drTransationTreeNode = dtTransationTreeNode.NewRow();
            foreach (DataRow drSubSubNode in drXmlTreeSubSubNode)
            {
                if (count == 0)
                {

                    count++;
                    drTransationTreeNode["TreeNode1"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drTransationTreeNode["TreeNodeText1"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                    //drTransationTreeNode["Path1"] = drSubSubNode["Path"].ToString();
                    dtTransationTreeNode.Rows.Add(drTransationTreeNode);

                }
                else if (count == 1)
                {
                    count++;
                    drTransationTreeNode["TreeNode2"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                    drTransationTreeNode["TreeNodeText2"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                 //   drTransationTreeNode["Path2"] = drSubSubNode["Path"].ToString();
                    count = 0;
                    drTransationTreeNode = dtTransationTreeNode.NewRow();

                }
             

            }
            rptTransationTree.DataSource = dtTransationTreeNode;
            rptTransationTree.DataBind();
        }
        protected void rptTransationTree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
           // ImageButton imgBtn1 = e.Item.FindControl("imgbtnTransation1") as ImageButton;
            LinkButton lnkbtn1 = e.Item.FindControl("lnkTransationTreeNode1") as LinkButton;

          //  ImageButton imgBtn2 = e.Item.FindControl("imgbtnTransation2") as ImageButton;
            
            LinkButton lnkbtn2 = e.Item.FindControl("lnkTransationTreeNode2") as LinkButton;
            
            if (e.CommandName == "Tree_Navi_Row1")
            {

                if (lnkbtn1.CommandArgument == "3024")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadTransaction", "loadcontrol('RMMultipleTransactionView','login');", true);
                }
            }
            if (e.CommandName == "Tree_Navi_Row2")
            {
               
                if (lnkbtn2.CommandArgument == "3025")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMEQMultipleTransactionsView','login');", true);
                }

            }


        }
        protected void rptTransationTree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{

            //    ImageButton col0 = e.Item.FindControl("imgbtnTransation1") as ImageButton;
            //    ImageButton col1 = e.Item.FindControl("imgbtnTransation2") as ImageButton;
            //    ImageButton col2 = e.Item.FindControl("imgbtnTransation3") as ImageButton;
            //    ImageButton col3 = e.Item.FindControl("imgbtnTransation4") as ImageButton;
            //    if (col1.ImageUrl == "")
            //    {
            //        var a = e.Item.FindControl("td2");
            //        a.Visible = false;
            //    }
            //    if (col0.ImageUrl == "")
            //    {
            //        var a = e.Item.FindControl("td1");
            //        a.Visible = false;
            //    }
            //    if (col2.ImageUrl == "")
            //    {
            //        var a = e.Item.FindControl("td3");
            //        a.Visible = false;
            //    }
            //    if (col3.ImageUrl == "")
            //    {
            //        var a = e.Item.FindControl("td4");
            //        a.Visible = false;
            //    }

            //}
        }
    }
}