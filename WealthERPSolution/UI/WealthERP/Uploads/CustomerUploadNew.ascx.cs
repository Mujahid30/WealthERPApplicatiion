﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoWerpAdmin;
using BoAdvisorProfiling;
using BOAssociates;
using BoCommisionManagement;
using VOAssociates;
using BoCommon;
using System.Globalization;
using System.Data;
using BoOnlineOrderManagement;
using WealthERP.Base;
using Telerik.Web.UI;

namespace WealthERP.Uploads
{
    public partial class CustomerUploadNew : System.Web.UI.UserControl
    {
        PriceBo priceBo = new PriceBo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        RMVo rmVo = new RMVo();
        AssociatesBo associatesBo = new AssociatesBo();
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        string categoryCode = string.Empty;
        int amcCode = 0;
        string AgentCode = "0";
        bool lnk;
        bool btnUp;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session["rmVo"];
            
            if (!IsPostBack)
            {
                BindMutualFundDropDowns();
                BindNAVCategory();
                LoadAllSchemeList(0);
                BindProductDropdown();
                BindMonthsAndYear();
                Session["fltrIsPayLocked"] = null;
                Session["fltrIsRecLocked"] = null;

                int day = 1;
               
                    // btnExportFilteredData.Visible = false;
                    associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                    if (associateuserheirarchyVo != null && associateuserheirarchyVo.AgentCode != null)
                    {
                        AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                        ddlSelectMode.Items.FindByText("Both").Enabled = false;
                        ddlSelectMode.Items.FindByText("Online-Only").Enabled = false;
                    }

                }

            
        }
        private void BindMonthsAndYear()
        {
            for (int i = 1; i <= 12; i++)
            {
                string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                ddlMnthQtr.Items.Add(new ListItem(monthName, i.ToString().PadLeft(2, '0')));
            }
            ddlMnthQtr.Items.Add(new ListItem("Quarter April-June", "13"));
            ddlMnthQtr.Items.Add(new ListItem("Quarter July-September", "14"));
            ddlMnthQtr.Items.Add(new ListItem("Quarter October-December", "15"));
            ddlMnthQtr.Items.Add(new ListItem("Quarter January-March", "16"));
            ddlMnthQtr.Items.Insert(0, new ListItem("Select", "0"));
            for (int i = DateTime.Now.Year; i >= 2008; i--)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            lnk = true;
            BindGrid();
            lnkEdit.Visible = false;
            btnSave.Visible = true;
        }

        protected void ddlIssuer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssuer.SelectedIndex != 0)
            {
                int amcCode = int.Parse(ddlIssuer.SelectedValue);
                ddlCategory.SelectedIndex = 0;
                LoadAllSchemeList(amcCode);

            }
        }



        protected void ddlSelectMode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueType.SelectedIndex == 0)
            {
                return;
            }
            else
            {
                ddlIssueName.Items.Clear();
                ddlIssueName.DataBind();
                BindMappedIssues(ddlIssueType.SelectedValue, ddlProduct.SelectedValue, int.Parse(ddlSelectMode.SelectedValue), (ddlProductCategory.SelectedValue == "") ? "FIFIIP" : ddlProductCategory.SelectedValue);

            }
        }
        //protected void ddlSearchType_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    td1.Visible = true;
        //    td2.Visible = true;
        //    if (ddlSearchType.SelectedValue != "Select" && ddlProduct.SelectedValue == "MF")
        //    {
        //        td1.Visible = false;
        //        td2.Visible = false;
        //    }
        //}
        protected void ddlIssueType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueType.SelectedIndex != 0)
            {
                ddlIssueName.Items.Clear();
                ddlIssueName.DataBind();
                BindMappedIssues(ddlIssueType.SelectedValue, ddlProduct.SelectedValue, 2, (ddlProductCategory.SelectedValue == "") ? "FIFIIP" : ddlProductCategory.SelectedValue);

            }
        }
        private void BindMappedIssues(string ModeOfIssue, string productType, int isOnlineIssue, string SubCategoryCode)
        {
            DataSet dsCommissionReceivable = commisionReceivableBo.GetIssuesStructureMapings(advisorVo.advisorId, "MappedIssue", ModeOfIssue, productType, isOnlineIssue, 0, SubCategoryCode);
            if (dsCommissionReceivable.Tables[0].Rows.Count > 0)
            {
                ddlIssueName.DataSource = dsCommissionReceivable.Tables[0];
                ddlIssueName.DataTextField = dsCommissionReceivable.Tables[0].Columns["AIM_IssueName"].ToString();
                ddlIssueName.DataValueField = dsCommissionReceivable.Tables[0].Columns["AIM_IssueId"].ToString();
                ddlIssueName.DataBind();
                ddlIssueName.Items.Insert(0, new ListItem("All", "0"));
            }

        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssuer.SelectedIndex != 0)
            {
                if (ddlCategory.SelectedIndex != 0)
                {
                    int amcCode = int.Parse(ddlIssuer.SelectedValue);
                    LoadAllSchemeList(amcCode);
                    //GdBind_Click(sender,e);
                }

            }

        }

        public void BindMutualFundDropDowns()
        {
            PriceBo priceBo = new PriceBo();
            DataTable dtGetMutualFundList = new DataTable();
            dtGetMutualFundList = priceBo.GetMutualFundList();
            ddlIssuer.DataSource = dtGetMutualFundList;
            ddlIssuer.DataTextField = dtGetMutualFundList.Columns["PA_AMCName"].ToString();
            ddlIssuer.DataValueField = dtGetMutualFundList.Columns["PA_AMCCode"].ToString();
            ddlIssuer.DataBind();
            ddlIssuer.Items.Insert(0, new ListItem("All", "0"));


        }
        private void ShowHideControlsBasedOnProduct(string asset)
        {
            tdCategory.Visible = false;
            tdDdlCategory.Visible = false;
            ddlProductCategory.Items.Clear();
            ddlProductCategory.DataBind();
            td2.Visible = true;
            td1.Visible = true;


            if (asset == "MF")
            {
                trSelectMutualFund.Visible = true;
                trNCDIPO.Visible = false;
                tdFrom.Visible = true;
                tdTolbl.Visible = true;
                cvddlIssueType.Enabled = false;
                Label1.Visible = true;
                ddlCommType.Visible = true;
                td2.Visible = true;
                td1.Visible = true;


            }
            else if (asset == "FI" || asset == "IP")
            {
                trSelectMutualFund.Visible = false;
                trNCDIPO.Visible = true;
                tdTolbl.Visible = true;
                ddlIssueName.Items.Clear();
                ddlIssueName.DataBind();
                cvddlIssueType.Enabled = true;
                Label1.Visible = false;
                ddlCommType.Visible = false;
                td2.Visible = false;
                td1.Visible = false;


            }
            if (asset == "FI")
            {
                BindBondCategories();
                tdCategory.Visible = true;
                tdDdlCategory.Visible = true;
            }

        }
        private void BindNAVCategory()
        {
            DataSet dsNavCategory;
            DataTable dtNavCategory;
            dsNavCategory = priceBo.GetNavOverAllCategoryList();
            dtNavCategory = dsNavCategory.Tables[0];
            if (dtNavCategory.Rows.Count > 0)
            {

                ddlCategory.DataSource = dtNavCategory;
                ddlCategory.DataValueField = dtNavCategory.Columns["Category_Code"].ToString();
                ddlCategory.DataTextField = dtNavCategory.Columns["Category_Name"].ToString();
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("All", "All"));

            }
        }
        protected void ddlProductCategory_OnSelectedIndexChanged(object Sender, EventArgs e)
        {
            if (ddlProductCategory.SelectedValue != "Select")
            {
                td1.Visible = false;
                td2.Visible = false;
                if (ddlProductCategory.SelectedValue != "FISDSD")
                {
                    tdFrom.Visible = true;
                    tdTolbl.Visible = true;
                    td1.Visible = true;

                    td2.Visible = true;
                }
                BindMappedIssues(ddlIssueType.SelectedValue, ddlProduct.SelectedValue, 0, (ddlProductCategory.SelectedValue == "") ? "FIFIIP" : ddlProductCategory.SelectedValue);
            }
        }
        private void LoadAllSchemeList(int amcCode)
        {
            DataSet dsLoadAllScheme = new DataSet();
            DataTable dtLoadAllScheme = new DataTable();
            if (ddlIssuer.SelectedIndex != 0)
            {
                amcCode = int.Parse(ddlIssuer.SelectedValue.ToString());
                categoryCode = ddlCategory.SelectedValue;
                //dtLoadAllScheme = priceBo.GetAllScehmeList(amcCode);
                dsLoadAllScheme = priceBo.GetSchemeListCategoryConcatenation(amcCode, categoryCode);
                dtLoadAllScheme = dsLoadAllScheme.Tables[0];
            }

            if (dtLoadAllScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtLoadAllScheme;
                ddlScheme.DataTextField = dtLoadAllScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataValueField = dtLoadAllScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlScheme.DataSource = null;
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("All", "0"));
            }

        }
        //private void SetParameters()
        //{
        //    if (string.IsNullOrEmpty(ddlMnthQtr.SelectedItem.Value.ToString()) != true)
        //        hdnFromDate.Value = ddlMnthQtr.SelectedItem.Value.ToString();
        //    if (string.IsNullOrEmpty(ddlYear.SelectedItem.Value.ToString()) != true)
        //        hdnToDate.Value = ddlYear.SelectedItem.Value.ToString();
        //    if (string.IsNullOrEmpty(ddlScheme.SelectedItem.Value.ToString()) != true)
        //        hdnschemeId.Value = ddlScheme.SelectedItem.Value.ToString();
        //    if (string.IsNullOrEmpty(ddlCategory.SelectedItem.Value.ToString()) != true)
        //        hdnCategory.Value = ddlCategory.SelectedItem.Value.ToString();
        //    if (ddlIssuer.SelectedValue.ToString() != "Select")
        //        hdnSBbrokercode.Value = ddlIssuer.SelectedItem.Value.ToString();
        //    else
        //        hdnSBbrokercode.Value = "0";
        //    if (string.IsNullOrEmpty(ddlIssueName.SelectedValue.ToString()) != true)
        //        hdnIssueId.Value = ddlIssueName.SelectedValue.ToString();
        //    else
        //        hdnIssueId.Value = "0";
        //    if (ddlProductCategory.SelectedValue.ToString() != "Select")
        //        hdnProductCategory.Value = ddlProductCategory.SelectedValue.ToString();
        //    else
        //        hdnProductCategory.Value = "0";

        //}

        protected void ddlProduct_SelectedIndexChanged(object source, EventArgs e)
        {

            ddlCommType.Items[3].Enabled = false;

            if (ddlProduct.SelectedValue != "Select")
            {
                if (ddlProduct.SelectedValue == "MF")
                {


                    ddlCommType.Items[3].Enabled = false;
                }
                else
                {
                    //ddlOrderStatus.Items[1].Enabled = false;
                    //ddlOrderStatus.Items[2].Enabled = true;

                }
                if (ddlProduct.SelectedValue == "FI")
                {
                    ddlCommType.Items[2].Enabled = false;
                }
                else
                {
                    ddlCommType.Items[2].Enabled = true;
                }
                ShowHideControlsBasedOnProduct(ddlProduct.SelectedValue);
            }

        }
        private void BindProductDropdown()
        {
            DataSet dsLookupData = commisionReceivableBo.GetProductType();
            //Populating the product dropdown
            ddlProduct.DataSource = dsLookupData.Tables[0];
            ddlProduct.DataValueField = dsLookupData.Tables[0].Columns["PAG_AssetGroupCode"].ToString();
            ddlProduct.DataTextField = dsLookupData.Tables[0].Columns["PAG_AssetGroupName"].ToString();
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("Select", "Select"));
        }
        //protected void GetCommisionTypes()
        //{

        //    ddlSearchType.Items.Insert(0, new ListItem("Select", "Select"));
        //    ddlSearchType.Items.Insert(1, new ListItem("Brokerage", "0"));
        //}
        private void BindBondCategories()
        {
            OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
            DataTable dtCategory = new DataTable();
            dtCategory = onlineNCDBackOfficeBo.BindNcdCategory("SubInstrumentCat", "").Tables[0];
            if (dtCategory.Rows.Count > 0)
            {
                ddlProductCategory.DataSource = dtCategory;
                ddlProductCategory.DataValueField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlProductCategory.DataTextField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlProductCategory.DataBind();
            }
            ddlProductCategory.Items.Insert(0, new ListItem("Select", "Select"));

        }
        private void BindGrid()
        {
            DataSet ds = new DataSet();
            //ds.ReadXml(Server.MapPath(@"\Sample.xml"));
            btnExportFilteredDupData.Visible = false;
            tblUpdate.Visible = false;
            int IssueId = 0;
            string productCategory = string.Empty;
            string category = string.Empty;
            int amcCode = 0;
            int schemeCode = 0;
            gvbrokerageRecon.MasterTableView.GetColumn("PerDayAsset").Visible = false;
            gvbrokerageRecon.MasterTableView.GetColumn("CumulativeNAV").Visible = false;
            gvbrokerageRecon.MasterTableView.GetColumn("ClosingNAV").Visible = false;
            gvbrokerageRecon.MasterTableView.GetColumn("TotalNAV").Visible = false;
            gvbrokerageRecon.MasterTableView.GetColumn("WCD_AID_IssueDetailName").Visible = false;
            gvbrokerageRecon.MasterTableView.GetColumn("WCD_AIIC_InvestorCatgeoryName").Visible = false;
            if (ddlProduct.SelectedValue == "MF")
            {
                amcCode = Int32.Parse(ddlIssuer.SelectedValue);
                schemeCode = Int32.Parse(ddlScheme.SelectedValue);
                category = ddlCategory.SelectedValue;
                gvbrokerageRecon.MasterTableView.GetColumn("AIM_IssueName").HeaderText = "Scheme Plan Name";
                gvbrokerageRecon.MasterTableView.GetColumn("CO_ApplicationNumber").HeaderText = "Transaction No.";
                if (ddlCommType.SelectedValue == "TC")
                {
                    gvbrokerageRecon.MasterTableView.GetColumn("PerDayAsset").Visible = true;
                    gvbrokerageRecon.MasterTableView.GetColumn("CumulativeNAV").Visible = true;
                    gvbrokerageRecon.MasterTableView.GetColumn("ClosingNAV").Visible = true;
                    gvbrokerageRecon.MasterTableView.GetColumn("TotalNAV").Visible = true;
                }
            }

            else if (ddlProduct.SelectedValue == "IP" || ddlProduct.SelectedValue == "FI")
            {
                if (!String.IsNullOrEmpty(ddlIssueName.SelectedValue))
                {
                    IssueId = Int32.Parse(ddlIssueName.SelectedValue);
                }
            }
            if (ddlProduct.SelectedValue == "FI")
            {
                productCategory = ddlProductCategory.SelectedValue;
                if (ddlProductCategory.SelectedValue == "FISDSD")
                {
                    gvbrokerageRecon.MasterTableView.GetColumn("WCD_AID_IssueDetailName").Visible = true;
                    gvbrokerageRecon.MasterTableView.GetColumn("WCD_AIIC_InvestorCatgeoryName").Visible = true;
                }

            }
            if (ddlProduct.SelectedValue == "MF")
            {
                productCategory = ddlCommType.SelectedValue;
            }
            ds = adviserMFMIS.GetWERPCommissionDetails(ddlProduct.SelectedValue, advisorVo.advisorId, Int32.Parse(ddlMnthQtr.SelectedValue), Int32.Parse(ddlYear.SelectedValue), category, IssueId, productCategory, amcCode, schemeCode, Convert.ToInt32(ddlDateFilterType.SelectedValue), Convert.ToInt32(ddlSelectMode.SelectedValue));
            if (ds.Tables[0] != null)
            {

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "BrokerageReconvalidation", "validation();", true);

                btnExportFilteredDupData.Visible = true;
                tblUpdate.Visible = true;
                gvbrokerageRecon.Visible = true;
                gvbrokerageRecon.DataSource = ds.Tables[0];
                DataTable dtGetAMCTransactionDeatails = new DataTable();
                gvbrokerageRecon.DataBind();
                if (Cache["gvbrokerageRecon" + userVo.UserId.ToString()] != null)
                {
                    Cache.Remove("gvbrokerageRecon" + userVo.UserId.ToString());

                }
                Cache.Insert("gvbrokerageRecon" + userVo.UserId.ToString(), ds.Tables[0]);

                chkBulkReceivedSys.Checked = false;
                chkBulkReceived.Checked = false;
                chkBulkPayble.Checked = false;
                chkBulkPayableDate.Checked = false;
                chkBulkReceivedDate.Checked = false;
            }

        }

        protected void GdBind_Click(Object sender, EventArgs e)
        {
           
            BindGrid();
            Session["fltrIsPayLocked"] = null;
            Session["fltrIsRecLocked"] = null;
          
        }

        protected void gvbrokerageRecon_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["gvbrokerageRecon" + userVo.UserId];
            if (dt != null)
            {
                gvbrokerageRecon.DataSource = dt;
                gvbrokerageRecon.Visible = true;
            }

        }

        protected void gvbrokerageRecon_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem)
            {
                //Is Receivable Locked Filter
                GridFilteringItem filteringItemRec = (e.Item as GridFilteringItem);
                filteringItemRec["IsRecLocked"].Controls.Clear();
                DropDownList ddlIsRecLocked = new DropDownList();
                ddlIsRecLocked.AutoPostBack = true;
                ddlIsRecLocked.CssClass = "cmbField";
                ddlIsRecLocked.Width = 100;
                ddlIsRecLocked.SelectedIndexChanged += new System.EventHandler(ddlIsRecLocked_SelectedIndexChanged);
                ddlIsRecLocked.Items.Add(new ListItem("Show all", "2"));
                ddlIsRecLocked.Items.Add(new ListItem("Checked", "1"));
                ddlIsRecLocked.Items.Add(new ListItem("Unchecked", "0"));
                if (Session["fltrIsRecLocked"] != null)
                {
                    ddlIsRecLocked.Items.FindByValue((string)Session["fltrIsRecLocked"]).Selected = true;
                }
                filteringItemRec["IsRecLocked"].Controls.Add(ddlIsRecLocked);

                //Is Pay Locked Filter
                GridFilteringItem filteringItemPay = (e.Item as GridFilteringItem);
                filteringItemPay["IsPayLocked"].Controls.Clear();
                DropDownList ddlIsPayLocked = new DropDownList();
                ddlIsPayLocked.AutoPostBack = true;
                ddlIsPayLocked.CssClass = "cmbField";
                ddlIsPayLocked.Width = 100;
                ddlIsPayLocked.SelectedIndexChanged += new System.EventHandler(ddlIsPayLocked_SelectedIndexChanged);
                ddlIsPayLocked.Items.Add(new ListItem("Show all", "2"));
                ddlIsPayLocked.Items.Add(new ListItem("Checked", "1"));
                ddlIsPayLocked.Items.Add(new ListItem("Unchecked", "0"));
                if (Session["fltrIsPayLocked"] != null)
                {
                    ddlIsPayLocked.Items.FindByValue((string)Session["fltrIsPayLocked"]).Selected = true;
                }
                filteringItemPay["IsPayLocked"].Controls.Add(ddlIsPayLocked);
            }
           
        }
        protected void gvbrokerageRecon_OnItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

           
                if (rmVo.IsChecker == true)
                {
                    if (lnk == false)
                    {
                       
                        if (e.Item is GridDataItem)
                        {

                            GridDataItem dataItem = e.Item as GridDataItem;

                            CheckBox chk = (CheckBox)dataItem["IsRecLocked"].FindControl("chkIdRec");
                           
                                if (chk.Checked)
                                {
                                    chk.Enabled = false;
                                  
                                    TextBox text = (TextBox)dataItem["WCD_Act_Rec_Brokerage"].FindControl("txtActRecBrokerage");
                                    text.Enabled = false;
                                    TextBox text1 = (TextBox)dataItem["actionRec"].FindControl("txtRecDate");
                                    text1.Enabled = false;
                                }
                                CheckBox chk2 = (CheckBox)dataItem["IsPayLocked"].FindControl("chkIdPay");
                                if (chk2.Checked)
                                {
                                    chk2.Enabled = false;
                                    TextBox text2 = (TextBox)dataItem["WCD_Act_Pay_brokerage"].FindControl("txtActPaybrokerage");
                                    text2.Enabled = false;
                                    TextBox text3 = (TextBox)dataItem["actionPay"].FindControl("txtPaybleDate");
                                    text3.Enabled = false;
                                }

                            }
                        }
                        lnkEdit.Visible = true;
                    }


                

                if (rmVo.IsMaker == true)
                {
                   
                    
                        if (e.Item is GridDataItem)
                        {

                            GridDataItem dataItem = e.Item as GridDataItem;

                            CheckBox chk = (CheckBox)dataItem["IsRecLocked"].FindControl("chkIdRec");
                            if (chk.Checked)
                            {
                                chk.Enabled = false;
                                
                                TextBox text = (TextBox)dataItem["WCD_Act_Rec_Brokerage"].FindControl("txtActRecBrokerage");
                                text.Enabled = false;
                                TextBox text1 = (TextBox)dataItem["actionRec"].FindControl("txtRecDate");
                                text1.Enabled = false;
                            }
                                CheckBox chk2 = (CheckBox)dataItem["IsPayLocked"].FindControl("chkIdPay");
                            if(chk2.Checked)
                            {
                                chk2.Enabled = false;
                                TextBox text2 = (TextBox)dataItem["WCD_Act_Pay_brokerage"].FindControl("txtActPaybrokerage");
                                text2.Enabled = false;
                                TextBox text3 = (TextBox)dataItem["actionPay"].FindControl("txtPaybleDate");
                                text3.Enabled = false;
                            }

                            }
                        
                    }
                if (e.Item is GridPagerItem)
                {
                    RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
                    
                    PageSizeCombo.Items[0].Text = "10";
                    PageSizeCombo.Items[0].Value = "10";
                    PageSizeCombo.Items[1].Text = "50";
                    PageSizeCombo.Items[1].Value = "50";
                    PageSizeCombo.Items[2].Text = "100";
                    PageSizeCombo.Items[2].Value = "100";
                    PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true; 
                } 

                }
                
            
            
        
        
        protected void ddlIsPayLocked_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DropDownList ddList = (DropDownList)sender;
            Session["fltrIsPayLocked"] = ddList.SelectedValue;
            gvbrokerageRecon.MasterTableView.FilterExpression = gvbrokerageRecon.MasterTableView.FilterExpression.Contains("IsPayLocked") ? null : gvbrokerageRecon.MasterTableView.FilterExpression;
            switch (ddList.SelectedValue)
            {
                case "1":
                    gvbrokerageRecon.MasterTableView.FilterExpression += String.IsNullOrEmpty(gvbrokerageRecon.MasterTableView.FilterExpression) ? "(IsPayLocked = 1) " : "AND (IsPayLocked = 1) ";
                    //foreach (GridColumn column in gvbrokerageRecon.MasterTableView.Columns)
                    //{
                    //    column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                    //    column.CurrentFilterValue = String.Empty;
                    //}
                    gvbrokerageRecon.MasterTableView.Rebind();
                    break;
                case "0":
                    gvbrokerageRecon.MasterTableView.FilterExpression += String.IsNullOrEmpty(gvbrokerageRecon.MasterTableView.FilterExpression) ? "(IsPayLocked = 0) " : "AND (IsPayLocked = 0) ";
                    //foreach (GridColumn column in gvbrokerageRecon.MasterTableView.Columns)
                    //{
                    //    column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                    //    column.CurrentFilterValue = String.Empty;
                    //}
                    gvbrokerageRecon.MasterTableView.Rebind();
                    break;
                case "2":
                    gvbrokerageRecon.MasterTableView.FilterExpression = String.Empty;
                    gvbrokerageRecon.MasterTableView.FilterExpression += String.IsNullOrEmpty(gvbrokerageRecon.MasterTableView.FilterExpression) ? "(IsPayLocked <> 2) " : "AND (IsPayLocked <> 2) ";
                    //foreach (GridColumn column in gvbrokerageRecon.MasterTableView.Columns)
                    //{
                    //    column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                    //    column.CurrentFilterValue = String.Empty;
                    //}
                    gvbrokerageRecon.MasterTableView.Rebind();
                    break;
            }
        }
        protected void ddlIsRecLocked_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DropDownList ddList = (DropDownList)sender;
            Session["fltrIsRecLocked"] = ddList.SelectedValue;
            gvbrokerageRecon.MasterTableView.FilterExpression = gvbrokerageRecon.MasterTableView.FilterExpression.Contains("IsRecLocked") ? null : gvbrokerageRecon.MasterTableView.FilterExpression;
            switch (ddList.SelectedValue)
            {
                case "1":
                    gvbrokerageRecon.MasterTableView.FilterExpression += String.IsNullOrEmpty(gvbrokerageRecon.MasterTableView.FilterExpression) ? "(IsRecLocked = 1) " : "AND (IsRecLocked = 1) ";
                    //foreach (GridColumn column in gvbrokerageRecon.MasterTableView.Columns)
                    //{
                    //    column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                    //    column.CurrentFilterValue = String.Empty;
                    //}
                    gvbrokerageRecon.MasterTableView.Rebind();
                    break;
                case "0":
                    gvbrokerageRecon.MasterTableView.FilterExpression += String.IsNullOrEmpty(gvbrokerageRecon.MasterTableView.FilterExpression) ? "(IsRecLocked = 0) " : "AND (IsRecLocked = 0) ";
                    //foreach (GridColumn column in gvbrokerageRecon.MasterTableView.Columns)
                    //{
                    //    column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                    //    column.CurrentFilterValue = String.Empty;
                    //}
                    gvbrokerageRecon.MasterTableView.Rebind();
                    break;
                case "2":
                    gvbrokerageRecon.MasterTableView.FilterExpression += String.IsNullOrEmpty(gvbrokerageRecon.MasterTableView.FilterExpression) ? "(IsRecLocked <> 2) " : "AND (IsRecLocked <> 2) ";
                    //foreach (GridColumn column in gvbrokerageRecon.MasterTableView.Columns)
                    //{
                    //    column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                    //    column.CurrentFilterValue = String.Empty;
                    //}
                    gvbrokerageRecon.MasterTableView.Rebind();
                    break;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
         
            int i = 0;
            foreach (GridDataItem dr in gvbrokerageRecon.Items)
            {
              
                    int id = 0;
                    decimal ActRec = 0;
                    decimal ActPay = 0;
                    bool blResult = false;
                    bool IsPayLocked = false;
                    bool IsRecLocked = false;
                    DateTime? paybleDate = DateTime.MinValue;
                    DateTime? receivedDate = DateTime.MinValue;
                    DateTime? bulkPaybleDate = DateTime.MinValue;
                    DateTime? bulkReceivedDate = DateTime.MinValue;
                    CheckBox checkBox = (CheckBox)dr.FindControl("Ranjan");
                    CheckBox chkReceived = (CheckBox)dr.FindControl("chkIdRec");
                    CheckBox chkPayable = (CheckBox)dr.FindControl("chkIdPay");
                    decimal rtaAmount = 0;
                    if (((checkBox.Checked == true) && (chkReceived.Enabled == true)) || ((checkBox.Checked == true) && (chkPayable.Enabled == true)))
                    {
                        i = i + 1;
                        if (((TextBox)dr.FindControl("txtActRecBrokerage")).Text.Trim() != "")
                        {
                        ActRec = Convert.ToDecimal(((TextBox)dr.FindControl("txtActRecBrokerage")).Text.Trim());
                        
                        }
                        IsPayLocked = ((CheckBox)dr.FindControl("chkIdPay")).Checked;
                        IsRecLocked = ((CheckBox)dr.FindControl("chkIdRec")).Checked;
                        
                        if (((TextBox)dr.FindControl("txtActPaybrokerage")).Text.Trim() != "")
                        {

                            ActPay = Convert.ToDecimal(((TextBox)dr.FindControl("txtActPaybrokerage")).Text.Trim());

                        }
                        if (((TextBox)dr.FindControl("txtPaybleDate")).Text.Trim() != "")
                        {
                            paybleDate = Convert.ToDateTime(((TextBox)dr.FindControl("txtPaybleDate")).Text.Trim());
                        }
                       
                        if (((TextBox)dr.FindControl("txtRecDate")).Text.Trim() != "")
                        {
                            receivedDate = Convert.ToDateTime(((TextBox)dr.FindControl("txtRecDate")).Text.Trim());
                        }
                        if ((((CheckBox)dr.FindControl("chkIdRec")).Checked)&&(!(chkBulkReceivedDate.Checked)))
                        {
                            if (((TextBox)dr.FindControl("txtRecDate")).Text.Trim() == "")
                            {

                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please select Actual Received Date.');", true);
                                return;
                            }
                        }
                        if ((((CheckBox)dr.FindControl("chkIdPay")).Checked)&&(!(chkBulkPayableDate.Checked)))
                        {
                            if (((TextBox)dr.FindControl("txtPaybleDate")).Text.Trim() == "")
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please select Actual Payout Date.');", true);
                                return;
                            }
                        }
                        
                      
                        if (chkBulkReceivedDate.Checked)
                        {
                            bulkReceivedDate = rdpBulkReceivedDate.SelectedDate;
                        }
                        if (chkBulkPayableDate.Checked)
                        {
                            bulkPaybleDate = rdpBulkPayableDate.SelectedDate;
                        }

                        int selectedRow = 0;
                        GridDataItem gdi;
                        gdi = (GridDataItem)checkBox.NamingContainer;
                        selectedRow = gdi.ItemIndex;
                        if (chkBulkReceived.Checked)
                        {
                            rtaAmount = decimal.Parse((gvbrokerageRecon.MasterTableView.DataKeyValues[selectedRow]["RTABrokerageAmt"].ToString()));
                        }
                        id = int.Parse((gvbrokerageRecon.MasterTableView.DataKeyValues[selectedRow]["WCD_Id"].ToString()));

                        blResult = adviserMFMIS.UpdateActualPayAndRec(id, ActPay, ActRec, paybleDate, receivedDate, IsPayLocked,
                            IsRecLocked, chkBulkPayble.Checked,
                            rtaAmount, chkBulkReceivedSys.Checked, chkBulkReceived.Checked, bulkReceivedDate, bulkPaybleDate);

                    //}
                }
                }
            
           
            BindGrid();
           
            if (i > 0)
                ShowMessage("Updated Successfully", "S");
            else
                ShowMessage("Select a record to update.", "F");
        }
        protected void btnExportFilteredDupData_OnClick(object sender, ImageClickEventArgs e)
        {

            gvbrokerageRecon.ExportSettings.OpenInNewWindow = true;
            gvbrokerageRecon.ExportSettings.IgnorePaging = true;
            gvbrokerageRecon.ExportSettings.HideStructureColumns = true;
            gvbrokerageRecon.ExportSettings.ExportOnlyData = true;
            gvbrokerageRecon.ExportSettings.FileName = ddlProduct.SelectedValue + "-" + ddlCommType.SelectedValue + "-" + ddlMnthQtr.SelectedItem.Text + "-" + ddlYear.SelectedValue + "-" + ddlIssuer.SelectedItem.Text;
            gvbrokerageRecon.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvbrokerageRecon.MasterTableView.ExportToExcel();

        }
        private void ShowMessage(string msg, string type)
        {
            //tblMessage.Visible = true;
            //msgRecordStatus.InnerText = msg;
            ////--S(success)
            ////--F(failure)
            ////--W(warning)
            ////--I(information)
            ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnnghj", " showMsg('" + msg + "','W');", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mykey", "hide();", true);
            tblMessagee.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "BrokerageRecon", " showMsg('" + msg + "','" + type + "');", true);
        }

    }
}