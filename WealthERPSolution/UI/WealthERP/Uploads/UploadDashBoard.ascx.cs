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
        int roleId=0;

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
                BindReptUploadGrid();
            }
        }

        private void BindReptUploadGrid()
        {
            DataTable dtUploadTreeSubSubNode = new DataTable();
            DataTable dtUploadTreeSubNode = new DataTable();
            DataTable dtRoleAssociationTreeNode = new DataTable();
            DataSet dsTreeNodes = new DataSet();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            dsTreeNodes = XMLBo.GetSuperAdminTreeNodes(path);
            dtRoleAssociationTreeNode = XMLBo.GetRoleAssociationTreeNode(path);

            DataRow[] drXmlTreeSubSubNode;
            DataRow[] drXmlRoleToTreeSubSubNode;
            DataRow drUploadTreeNode;
            DataTable dtUploadTreeNode = new DataTable();

            dtUploadTreeNode.Columns.Add("TreeNode1", typeof(Int32));

            dtUploadTreeNode.Columns.Add("TreeNodeText1", typeof(String));

            dtUploadTreeNode.Columns.Add("Path1", typeof(String));

            dtUploadTreeNode.Columns.Add("TreeNode2", typeof(Int32));

            dtUploadTreeNode.Columns.Add("TreeNodeText2", typeof(String));

            dtUploadTreeNode.Columns.Add("Path2", typeof(String));

            dtUploadTreeNode.Columns.Add("TreeNode3", typeof(Int32));

            dtUploadTreeNode.Columns.Add("TreeNodeText3", typeof(String));

            dtUploadTreeNode.Columns.Add("Path3", typeof(String));

            dtUploadTreeNode.Columns.Add("TreeNode4", typeof(Int32));

            dtUploadTreeNode.Columns.Add("TreeNodeText4", typeof(String));

            dtUploadTreeNode.Columns.Add("Path4", typeof(String));

            //For upload 2009 is Tree Node Id in Sub Table in XML...
            int treeSubNodeId = 2009;
            drXmlTreeSubSubNode = dsTreeNodes.Tables[2].Select("TreeSubNodeCode=" + treeSubNodeId.ToString());

            int count = 0;
            int roleCount =0;
             drUploadTreeNode = dtUploadTreeNode.NewRow();
                foreach (DataRow drSubSubNode in drXmlTreeSubSubNode)
                {
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
                            drUploadTreeNode["TreeNode1"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                            drUploadTreeNode["TreeNodeText1"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                            drUploadTreeNode["Path1"] = drSubSubNode["Path"].ToString();
                            dtUploadTreeNode.Rows.Add(drUploadTreeNode);

                        }
                        else if (count == 1)
                        {
                            count++;
                            drUploadTreeNode["TreeNode2"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                            drUploadTreeNode["TreeNodeText2"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                            drUploadTreeNode["Path2"] = drSubSubNode["Path"].ToString();

                        }
                        else if (count == 2)
                        {
                            count++;
                            drUploadTreeNode["TreeNode3"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                            drUploadTreeNode["TreeNodeText3"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                            drUploadTreeNode["Path3"] = drSubSubNode["Path"].ToString();

                        }
                        else if (count == 3)
                        {
                            count++;
                            drUploadTreeNode["TreeNode4"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                            drUploadTreeNode["TreeNodeText4"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                            drUploadTreeNode["Path4"] = drSubSubNode["Path"].ToString();
                            count = 0;
                            drUploadTreeNode = dtUploadTreeNode.NewRow();
                        }
                    }
                    else
                    {
                        break;
                    }
                  
                    
                }

                rptTree.DataSource = dtUploadTreeNode;

                rptTree.DataBind();

            }


            //dtUploadTreeSubSubNode = XMLBo.GetSuperAdminTreeSubSubNodes(path);
            //dtUploadTreeSubNode = XMLBo.GetSuperAdminTreeSubNodes(path);
           
            //dsTreeNodes.Tables.Add(dtUploadTreeNode);
            //dsTreeNodes.Tables.Add(dtUploadTreeSubNode);
            //dsTreeNodes.Tables.Add(dtUploadTreeSubSubNode);
            //dsTreeNodes.Tables[0].PrimaryKey = new DataColumn[] { dsTreeNodes.Tables[0].Columns["TreeNodeCode"] };
            //dsTreeNodes.Tables[1].PrimaryKey = new DataColumn[] { dsTreeNodes.Tables[1].Columns["TreeSubNodeCode"] };
            //dsTreeNodes.Tables[2].PrimaryKey = new DataColumn[] { dsTreeNodes.Tables[2].Columns["TreeSubSubNodeCode"] };

            //DataRelation drel = new DataRelation("EquiJoin12", dsTreeNodes.Tables[0].Columns["TreeNodeCode"], dsTreeNodes.Tables[1].Columns["TreeNodeCode"]);

            //dsTreeNodes.Relations.Add(drel);
            //DataRelation drelFull = new DataRelation("EquiJoin23", dsTreeNodes.Tables[1].Columns["TreeSubNodeCode"], dsTreeNodes.Tables[2].Columns["TreeSubSubNodeCode"]);

             //DataRow[] drSubNodeDetails;
             //if (dtUploadTreeNode.Rows.Count > 0)
             //{
             //    drSubNodeDetails = dtUploadTreeNode.Select("TreeNodeCode =" + treeSubNodeId.ToString());

             //    foreach (DataRow dr in drSubNodeDetails)
             //    {
             //        drSubNode = dtSubNodeDetails.NewRow();
             //        drSubNode["TreeSubNodeId"] = dr["TreeSubNodeCode"].ToString();
             //        drSubNode["TreeSubNodetext"] = dr["TreeSubNodeText"].ToString();
             //        dtSubNodeDetails.Rows.Add(drSubNode);
             //    }
             //}
           // dtUploadTreeNode = XMLBo.GetUploadTreeNode(path);

            //DataTable jt = new DataTable("Joinedtable");

            //jt.Columns.Add("TreeNodeCode", typeof(Int32));

            //jt.Columns.Add("TreeSubNodeCode", typeof(Int32));

            //jt.Columns.Add("TreeSubSubNodeCode", typeof(Int32));

            //jt.Columns.Add("Path", typeof(String));

            //jt.Columns.Add("TreeSubSubNodeText", typeof(String));

            //dsTreeNodes.Tables.Add(jt);

            //foreach (DataRow dr in dsTreeNodes.Tables[0].Rows)
            //{

            //    DataRow parent = dr.GetParentRow("EquiJoin12");

            //    DataRow current = jt.NewRow();

            //    // Just add all the columns' data in "dr" to the New table.

            //    for (int i = 0; i < dsTreeNodes.Tables[0].Columns.Count; i++)
            //    {

            //        current[i] = dr[i];

            //    }

            //    // Add the column that is not present in the child, which is present in the parent.

            //    current["TreeNodeCode"] = parent["TreeNodeCode"];

            //    jt.Rows.Add(current);

            //}

            //string expression = "NodeType = 'Upload'";
            //dtUploadTreeNode.DefaultView.RowFilter = expression;
            //rptTree.DataSource = dtUploadTreeNode.DefaultView.ToTable();

            //rptTree.DataBind();

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

                if (imgBtn1.CommandArgument == "3002" || lnkbtn1.CommandArgument == "3002")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('CustomerUpload','login');", true);
                }
                else if (imgBtn1.CommandArgument == "3006" || lnkbtn1.CommandArgument == "3006")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedSystematicTransactionStaging','login');", true);
                }
                else if (imgBtn1.CommandArgument == "3010" || lnkbtn1.CommandArgument == "3010")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('ReverseTransactionExceptionHandling','login');", true);
                }
                //else if (imgBtn1.CommandArgument == "3011" || lnkbtn1.CommandArgument == "3011")
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedWERPProfile','login');", true);
                //}
            }
            if (e.CommandName == "Tree_Navi_Row2")
            {

                if (imgBtn2.CommandArgument == "3003" || lnkbtn2.CommandArgument == "3003")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('ViewUploadProcessLog','login');", true);
                }
                else if (imgBtn2.CommandArgument == "3007" || lnkbtn2.CommandArgument == "3007")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('TrailCommisionTransactionRejects','login');", true);
                }
                else if (imgBtn2.CommandArgument == "3011" || lnkbtn2.CommandArgument == "3011")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedWERPProfile','login');", true);
                }
            }

            if (e.CommandName == "Tree_Navi_Row3")
            {
                if (imgBtn3.CommandArgument == "3004" || lnkbtn3.CommandArgument == "3004")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedMFFolio','login');", true);
                }
                else if (imgBtn3.CommandArgument == "3008" || lnkbtn3.CommandArgument == "3008")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedTradeAccountStaging','login');", true);
                }
                //else if (imgBtn3.CommandArgument == "3010" || lnkbtn3.CommandArgument == "3010")
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('ReverseTransactionExceptionHandling','login');", true);
                //}
            }

            if (e.CommandName == "Tree_Navi_Row4")
            {
                if (imgBtn3.CommandArgument == "3005" || lnkbtn3.CommandArgument == "3005")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedMFTransactionStaging','login');", true);
                }
                else if (imgBtn3.CommandArgument == "3009" || lnkbtn3.CommandArgument == "3009")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedEquityTransactionStaging','login');", true);
                }
                //else if (imgBtn3.CommandArgument == "3010" || lnkbtn3.CommandArgument == "3010")
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('ReverseTransactionExceptionHandling','login');", true);
                //}
            }


        }
        protected void rptTree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ImageButton col1 = e.Item.FindControl("imgbtnUpload2") as ImageButton;
                ImageButton col0 = e.Item.FindControl("imgbtnUpload1") as ImageButton;
                ImageButton col2 = e.Item.FindControl("imgbtnUpload3") as ImageButton;
                ImageButton col3 = e.Item.FindControl("imgbtnUpload4") as ImageButton;
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
