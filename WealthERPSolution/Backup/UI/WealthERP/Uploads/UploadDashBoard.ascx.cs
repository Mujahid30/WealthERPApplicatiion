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

            DataRow[] drMFXmlTreeSubSubNode;
            DataRow[] drEQXmlTreeSubSubNode;
            DataRow[] drFIXmlTreeSubSubNode;

            DataRow[] drMFXmlRoleToTreeSubSubNode;
            DataRow[] drEQXmlRoleToTreeSubSubNode;
            DataRow[] drFIXmlRoleToTreeSubSubNode;
            
            DataRow drUploadTreeNode;
            DataTable dtUploadTreeNode = new DataTable();

            dtUploadTreeNode.Columns.Add("TreeNode1", typeof(Int32));

            dtUploadTreeNode.Columns.Add("TreeNodeText1", typeof(String));

            dtUploadTreeNode.Columns.Add("Path1", typeof(String));

            dtUploadTreeNode.Columns.Add("TreeNode2", typeof(Int32));

            dtUploadTreeNode.Columns.Add("TreeNodeText2", typeof(String));

            dtUploadTreeNode.Columns.Add("Path2", typeof(String));

            //dtUploadTreeNode.Columns.Add("TreeNode3", typeof(Int32));

            //dtUploadTreeNode.Columns.Add("TreeNodeText3", typeof(String));

            //dtUploadTreeNode.Columns.Add("Path3", typeof(String));

            //dtUploadTreeNode.Columns.Add("TreeNode4", typeof(Int32));

            //dtUploadTreeNode.Columns.Add("TreeNodeText4", typeof(String));

            //dtUploadTreeNode.Columns.Add("Path4", typeof(String));
            
            DataTable dtEQTreeNodes = new DataTable();
            dtEQTreeNodes = dtUploadTreeNode.Clone();
            DataTable dtFITreeNodes = new DataTable();
            dtFITreeNodes = dtUploadTreeNode.Clone();
          
            //For upload 2009 is Tree Node Id in Sub Table in XML...
            int treeSubNodeId = 2009;
            DataTable dtSubtreeNodes = new DataTable();
            DataTable dtMFSubtreeNodes = new DataTable();
            DataTable dtEQSubtreeNodes = new DataTable();
            DataTable dtFISubtreeNodes = new DataTable();
            dtSubtreeNodes = dsTreeNodes.Tables[2].Clone();
            dsTreeNodes.Tables[2].Select("Deleted=" + "false").CopyToDataTable(dtSubtreeNodes, LoadOption.Upsert);
            dtMFSubtreeNodes = dtSubtreeNodes.Clone();
            dtEQSubtreeNodes = dtSubtreeNodes.Clone();
            dtFISubtreeNodes = dtSubtreeNodes.Clone();
            dtSubtreeNodes.Select("Category='" + "MF" +"'").CopyToDataTable(dtMFSubtreeNodes, LoadOption.OverwriteChanges);
            dtSubtreeNodes.Select("Category='" + "EQ" + "'").CopyToDataTable(dtEQSubtreeNodes, LoadOption.Upsert);
            dtSubtreeNodes.Select("Category='" + "FI" + "'").CopyToDataTable(dtFISubtreeNodes, LoadOption.Upsert);

            drMFXmlTreeSubSubNode = dtMFSubtreeNodes.Select("TreeSubNodeCode=" + treeSubNodeId.ToString());

            int count = 0;
            int roleCount;
             drUploadTreeNode = dtUploadTreeNode.NewRow();
             foreach (DataRow drSubSubNode in drMFXmlTreeSubSubNode)
                {
                    drMFXmlRoleToTreeSubSubNode = dtRoleAssociationTreeNode.Select("TreeSubSubNodeCode=" + drSubSubNode["TreeSubSubNodeCode"].ToString());
                    roleCount = 0;
                    if (drMFXmlRoleToTreeSubSubNode.Count() > 0)
                    {
                        foreach (DataRow drUserRole in drMFXmlRoleToTreeSubSubNode)
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
                            count = 0;
                            drUploadTreeNode = dtUploadTreeNode.NewRow();

                        }
                        //else if (count == 2)
                        //{
                        //    count++;
                        //    drUploadTreeNode["TreeNode3"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                        //    drUploadTreeNode["TreeNodeText3"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                        //    drUploadTreeNode["Path3"] = drSubSubNode["Path"].ToString();

                        //}
                        //else if (count == 3)
                        //{
                        //    count++;
                        //    drUploadTreeNode["TreeNode4"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                        //    drUploadTreeNode["TreeNodeText4"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                        //    drUploadTreeNode["Path4"] = drSubSubNode["Path"].ToString();
                        //    count = 0;
                        //    drUploadTreeNode = dtUploadTreeNode.NewRow();
                        //}
                    }
                    else
                    {
                        //break;
                    }
                  
                    
                }

             rptMFTree.DataSource = dtUploadTreeNode;

             rptMFTree.DataBind();



            //----------------EQ __----------------------------

                //drEQXmlTreeSubSubNode = dtEQSubtreeNodes.Select("TreeSubNodeCode=" + treeSubNodeId.ToString());

                //count = 0;
            
                //DataRow drEqUploadTreeNode = dtEQTreeNodes.NewRow();
                //foreach (DataRow drSubSubNode in drEQXmlTreeSubSubNode)
                //{
                //    drEQXmlRoleToTreeSubSubNode = dtRoleAssociationTreeNode.Select("TreeSubSubNodeCode=" + drSubSubNode["TreeSubSubNodeCode"].ToString());
                //    roleCount = 0;
                //    if (drEQXmlRoleToTreeSubSubNode.Count() > 0)
                //    {
                //        foreach (DataRow drUserRole in drEQXmlRoleToTreeSubSubNode)
                //        {
                //            if (int.Parse(drUserRole["UserRoleId"].ToString()) == roleId)
                //            {
                //                roleCount++;
                //                break;
                //            }
                //        }
                //    }
                //    if (roleCount > 0)
                //    {
                //        if (count == 0)
                //        {

                //            count++;
                //            drEqUploadTreeNode["TreeNode1"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                //            drEqUploadTreeNode["TreeNodeText1"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                //            drEqUploadTreeNode["Path1"] = drSubSubNode["Path"].ToString();
                //            dtEQTreeNodes.Rows.Add(drEqUploadTreeNode);

                //        }
                //        else if (count == 1)
                //        {
                //            count++;
                //            drEqUploadTreeNode["TreeNode2"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                //            drEqUploadTreeNode["TreeNodeText2"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                //            drEqUploadTreeNode["Path2"] = drSubSubNode["Path"].ToString();
                //            count = 0;
                //            drEqUploadTreeNode = dtEQTreeNodes.NewRow();
                //        }
                //        //else if (count == 2)
                //        //{
                //        //    count++;
                //        //    drEqUploadTreeNode["TreeNode3"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                //        //    drEqUploadTreeNode["TreeNodeText3"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                //        //    drEqUploadTreeNode["Path3"] = drSubSubNode["Path"].ToString();

                //        //}
                //        //else if (count == 3)
                //        //{
                //        //    count++;
                //        //    drEqUploadTreeNode["TreeNode4"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                //        //    drEqUploadTreeNode["TreeNodeText4"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                //        //    drEqUploadTreeNode["Path4"] = drSubSubNode["Path"].ToString();
                //        //    count = 0;
                //        //    drEqUploadTreeNode = dtEQTreeNodes.NewRow();
                //        //}
                //    }
                //    else
                //    {
                //        //break;
                //    }


                //}

                //rptTreenodeEQ.DataSource = dtEQTreeNodes;

                //rptTreenodeEQ.DataBind();
          


    //-----------------------FI---------------------------
        
        
             drFIXmlTreeSubSubNode = dtFISubtreeNodes.Select("TreeSubNodeCode=" + treeSubNodeId.ToString());
             count = 0;

             DataRow drFIUploadTreeNode = dtFITreeNodes.NewRow();    
             foreach (DataRow drSubSubNode in drFIXmlTreeSubSubNode)
                {
                    drFIXmlRoleToTreeSubSubNode = dtRoleAssociationTreeNode.Select("TreeSubSubNodeCode=" + drSubSubNode["TreeSubSubNodeCode"].ToString());
                    roleCount = 0;
                    if (drFIXmlRoleToTreeSubSubNode.Count() > 0)
                    {
                        foreach (DataRow drUserRole in drFIXmlRoleToTreeSubSubNode)
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
                            drFIUploadTreeNode["TreeNode1"] = drSubSubNode["TreeSubSubNodeCode"].ToString();
                            drFIUploadTreeNode["TreeNodeText1"] = drSubSubNode["TreeSubSubNodeText"].ToString();
                            drFIUploadTreeNode["Path1"] = drSubSubNode["Path"].ToString();
                            dtFITreeNodes.Rows.Add(drFIUploadTreeNode);

                        }
                    }
                    else
                    {
                        //break;
                    }
                  
                    
                }

             rptTreenodeFI.DataSource = dtFITreeNodes;

             rptTreenodeFI.DataBind();

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
} 

        protected void rptMFTree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lnkbtn1 = e.Item.FindControl("lnkMFTreeNode1") as LinkButton;

            LinkButton lnkbtn2 = e.Item.FindControl("lnkMFTreeNode2") as LinkButton;

            //LinkButton lnkbtn3 = e.Item.FindControl("lnkMFTreeNode3") as LinkButton;

            if (e.CommandName == "Tree_MF_Row1")
            {

                if ( lnkbtn1.CommandArgument == "3004")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedMFFolio','login');", true);
                }
                else if ( lnkbtn1.CommandArgument == "3006")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedSystematicTransactionStaging','login');", true);
                }
                else if (lnkbtn1.CommandArgument == "3010")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('ReverseTransactionExceptionHandling','login');", true);
                }
                else if (lnkbtn1.CommandArgument == "3029")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedMFAutoSIPTransactions','login');", true);
                }
                //else if (imgBtn1.CommandArgument == "3011" || lnkbtn1.CommandArgument == "3011")
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedWERPProfile','login');", true);
                //}
            }
            if (e.CommandName == "Tree_MF_Row2")
            {

                if ( lnkbtn2.CommandArgument == "3005")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedMFTransactionStaging','login');", true);
                }
                else if (lnkbtn2.CommandArgument == "3007")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('TrailCommisionTransactionRejects','login');", true);
                }
                else if ( lnkbtn2.CommandArgument == "3011")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedWERPProfile','login');", true);
                }
            }


        }
        protected void rptMFTree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton col2 = e.Item.FindControl("lnkMFTreeNode2") as LinkButton;

                if (col2.Text == "")
                {
                    var a = e.Item.FindControl("div2MF");                    
                     a.Visible = false;
                   

                }

                //ImageButton col1 = e.Item.FindControl("imgbtnUpload2") as ImageButton;
                //ImageButton col0 = e.Item.FindControl("imgbtnUpload1") as ImageButton;
                //ImageButton col2 = e.Item.FindControl("imgbtnUpload3") as ImageButton;
                //ImageButton col3 = e.Item.FindControl("imgbtnUpload4") as ImageButton;
                //if (col1.ImageUrl == "")
                //{
                //    var a = e.Item.FindControl("td2");
                //    a.Visible = false;
                //}
                //if (col0.ImageUrl == "")
                //{
                //    var a = e.Item.FindControl("td1");
                //    a.Visible = false;
                //}
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

        protected void rptTreenodeEQ_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lnkbtn1 = e.Item.FindControl("lnkEQTreeNode1") as LinkButton;

            LinkButton lnkbtn2 = e.Item.FindControl("lnkEQTreeNode2") as LinkButton;

            //LinkButton lnkbtn3 = e.Item.FindControl("lnkEQTreeNode3") as LinkButton;

            if (e.CommandName == "Tree_EQ_Row1")
            {

                if (lnkbtn1.CommandArgument == "3008")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedTradeAccountStaging','login');", true);
                }
            }
            if (e.CommandName == "Tree_EQ_Row2")
            {

                if (lnkbtn2.CommandArgument == "3009")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('RejectedEquityTransactionStaging','login');", true);
                }
            }


        }
        protected void rptTreenodeEQ_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                //ImageButton col1 = e.Item.FindControl("imgbtnUpload2") as ImageButton;
                //ImageButton col0 = e.Item.FindControl("imgbtnUpload1") as ImageButton;
                //ImageButton col2 = e.Item.FindControl("imgbtnUpload3") as ImageButton;
                //ImageButton col3 = e.Item.FindControl("imgbtnUpload4") as ImageButton;
                //if (col1.ImageUrl == "")
                //{
                //    var a = e.Item.FindControl("td2");
                //    a.Visible = false;
                //}
                //if (col0.ImageUrl == "")
                //{
                //    var a = e.Item.FindControl("td1");
                //    a.Visible = false;
                //}
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
        protected void rptTreenodeFI_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lnkbtn1 = e.Item.FindControl("lnkFITreeNode1") as LinkButton;

            if (e.CommandName == "Tree_FI_Row1")
            {

                if (lnkbtn1.CommandArgument == "3033")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('FixedIncomeReject','login');", true);
                }
            }
        }
        protected void rptTreenodeFI_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
    }
}
