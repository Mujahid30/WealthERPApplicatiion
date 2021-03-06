﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoWerpAdmin;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoAdvisorProfiling;
using VoUser;
using BoUploads;
using BoCustomerGoalProfiling;
using Telerik.Web.UI;
using BoCommon;
using System.Configuration;
using BOAssociates;
using System.Globalization;
using BoCommisionManagement;
using VOAssociates;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;

namespace WealthERP.BusinessMIS
{
    public partial class CommissionReceivableRecon : System.Web.UI.UserControl
    {
        PriceBo priceBo = new PriceBo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        AssociatesBo associatesBo = new AssociatesBo();
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        string categoryCode = string.Empty;
        int amcCode = 0;
        string AgentCode = "0";


        protected void Page_load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            SessionBo.CheckSession();

            if (!IsPostBack)
            {
                BindMutualFundDropDowns();
                BindNAVCategory();
                LoadAllSchemeList(0);
                BindProductDropdown();
                BindMonthsAndYear();
                //GetCommisionTypes();
                int day = 1;
                btnExportFilteredData.Visible = false;
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

        protected void ddlIssuer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssuer.SelectedIndex != 0)
            {
                int amcCode = int.Parse(ddlIssuer.SelectedValue);
                ddlCategory.SelectedIndex = 0;
                LoadAllSchemeList(amcCode);

            }
        }
        //protected void btnUpload_click(object sender, EventArgs e)
        //{
        //}


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
        protected void ddlSearchType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            td1.Visible = true;
            td2.Visible = true;
            if (ddlSearchType.SelectedValue != "Select" && ddlProduct.SelectedValue == "MF")
            {
                // td1.Visible = false;
                // td2.Visible = false;
            }
        }
        protected void ddlIssueType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueType.SelectedIndex != 0)
            {
                ddlIssueName.Items.Clear();
                ddlIssueName.DataBind();
                BindMappedIssues(ddlIssueType.SelectedValue, ddlProduct.SelectedValue, int.Parse(ddlSelectMode.SelectedValue), (ddlProductCategory.SelectedValue == "") ? "FIFIIP" : ddlProductCategory.SelectedValue);

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
            ddlIssuer.Items.Insert(0, new ListItem("Select", "Select"));


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
                Label3.Visible = false;
                ddlOrderType.Visible = false;

               
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
                BindMappedIssues(ddlIssueType.SelectedValue, ddlProduct.SelectedValue, int.Parse(ddlSelectMode.SelectedValue), (ddlProductCategory.SelectedValue == "") ? "FIFIIP" : ddlProductCategory.SelectedValue);
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
        private void SetParameters()
        {
            if (string.IsNullOrEmpty(ddlMnthQtr.SelectedItem.Value.ToString()) != true)
                hdnFromDate.Value = ddlMnthQtr.SelectedItem.Value.ToString();
            if (string.IsNullOrEmpty(ddlYear.SelectedItem.Value.ToString()) != true)
                hdnToDate.Value = ddlYear.SelectedItem.Value.ToString();
            if (string.IsNullOrEmpty(ddlScheme.SelectedItem.Value.ToString()) != true)
                hdnschemeId.Value = ddlScheme.SelectedItem.Value.ToString();
            if (string.IsNullOrEmpty(ddlCategory.SelectedItem.Value.ToString()) != true)
                hdnCategory.Value = ddlCategory.SelectedItem.Value.ToString();
            if (ddlIssuer.SelectedValue.ToString() != "Select")
                hdnSBbrokercode.Value = ddlIssuer.SelectedItem.Value.ToString();
            else
                hdnSBbrokercode.Value = "0";
            if (string.IsNullOrEmpty(ddlIssueName.SelectedValue.ToString()) != true)
                hdnIssueId.Value = ddlIssueName.SelectedValue.ToString();
            else
                hdnIssueId.Value = "0";
            if (ddlProductCategory.SelectedValue.ToString() != "Select")
                hdnProductCategory.Value = ddlProductCategory.SelectedValue.ToString();
            else
                hdnProductCategory.Value = "0";

        }

        protected void gvWERPTrans_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                 


                if (ddlCommType.SelectedValue == "UF")
                {
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("fromDt").Visible = false;

                    gvCommissionReceiveRecon.MasterTableView.GetColumn("ToDt").Visible = false;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("Age").Visible = false;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("AvgAsset").Visible = false;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("CumNAV").Visible = false; 

                }
                else if (ddlCommType.SelectedValue == "TC")
                {
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("fromDt").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("AvgAsset").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("CumNAV").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("ToDt").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("Age").Visible = true;

                }

            }

        }
        protected void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            if (ddlProduct.SelectedValue.ToString() == "MF")
            {
                gvCommissionReceiveRecon.ExportSettings.OpenInNewWindow = true;
                gvCommissionReceiveRecon.ExportSettings.IgnorePaging = true;
                gvCommissionReceiveRecon.ExportSettings.FileName = "CommissionExceptedMIS";

                foreach (GridFilteringItem filter in gvCommissionReceiveRecon.MasterTableView.GetItems(GridItemType.FilteringItem))
                {
                    filter.Visible = false;
                }
                gvCommissionReceiveRecon.MasterTableView.ExportToExcel();
            }
            else
            {
                rgNCDIPOMIS.ExportSettings.OpenInNewWindow = true;
                rgNCDIPOMIS.ExportSettings.IgnorePaging = true;
                rgNCDIPOMIS.ExportSettings.FileName = "CommissionExceptedMIS";
                foreach (GridFilteringItem filter in rgNCDIPOMIS.MasterTableView.GetItems(GridItemType.FilteringItem))
                {
                    filter.Visible = false;
                }
                rgNCDIPOMIS.MasterTableView.ExportToExcel();
            }

        }
        protected void GdBind_Click(Object sender, EventArgs e)
        {
            SetParameters();
            DataSet ds = new DataSet();
            //ds.ReadXml(Server.MapPath(@"\Sample.xml"));
            dvMfMIS.Visible = false;
            dvNCDIPOMIS.Visible = false;
            ds = adviserMFMIS.GetCommissionReceivableRecon_TrailComparision(ddlProduct.SelectedValue, int.Parse(ddlSelectMode.SelectedValue), advisorVo.advisorId, int.Parse(hdnschemeId.Value), int.Parse(hdnFromDate.Value), int.Parse(hdnToDate.Value), hdnCategory.Value, null, ddlCommType.SelectedValue, int.Parse(hdnSBbrokercode.Value), int.Parse(hdnIssueId.Value), Convert.ToInt32(ddlSearchType.SelectedValue), ddlOrderStatus.SelectedValue, AgentCode, hdnProductCategory.Value, Boolean.Parse(ddlOrderType.SelectedValue));
            if (ds.Tables[0] != null)
            {
                if (ddlProduct.SelectedValue.ToString() == "MF")
                {

                    btnExportFilteredData.Visible = true;
                    dvMfMIS.Visible = true;
                    gvCommissionReceiveRecon.DataSource = ds.Tables[0];
                    DataTable dtGetAMCTransactionDeatails = new DataTable();
                    gvCommissionReceiveRecon.DataBind();
                    if (Cache["gvCommissionReconMIs" + userVo.UserId.ToString()] != null)
                    {
                        Cache.Remove("gvCommissionReconMIs" + userVo.UserId.ToString());

                    }
                    Cache.Insert("gvCommissionReconMIs" + userVo.UserId.ToString(), ds.Tables[0]);


                }
                else
                {

                    btnExportFilteredData.Visible = true;
                    dvNCDIPOMIS.Visible = true;
                    rgNCDIPOMIS.DataSource = ds.Tables[0];
                    rgNCDIPOMIS.DataBind();
                    if (Cache["rgNCDIPOMIS" + userVo.UserId.ToString()] != null)
                    {
                        Cache.Remove("rgNCDIPOMIS" + userVo.UserId.ToString());
                    }
                    Cache.Insert("rgNCDIPOMIS" + userVo.UserId.ToString(), ds.Tables[0]);

                }
            }

        }
        protected void ddlProduct_SelectedIndexChanged(object source, EventArgs e)
        {

            if (ddlProduct.SelectedValue != "Select")
            {
                if (ddlProduct.SelectedValue == "MF")
                {

                    ddlOrderStatus.Items[1].Enabled = true;
                  
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
        protected void gvCommissionReceiveRecon_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["gvCommissionReconMIs" + userVo.UserId];
            gvCommissionReceiveRecon.DataSource = dt;
            gvCommissionReceiveRecon.Visible = true;
        }

        protected void rgNCDIPOMIS_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["rgNCDIPOMIS" + userVo.UserId];
            rgNCDIPOMIS.DataSource = dt;
            rgNCDIPOMIS.Visible = true;
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
        protected void GetCommisionTypes()
        {
            DataSet dscommissionTypes;
            dscommissionTypes = commisionReceivableBo.GetCommisionTypes();

            ddlSearchType.DataSource = dscommissionTypes.Tables[0];
            ddlSearchType.DataValueField = "WCMV_LookupId";
            ddlSearchType.DataTextField = "WCMV_Name";
            ddlSearchType.DataBind();
            ddlSearchType.Items.Insert(0, new ListItem("Select", "Select"));
            ddlSearchType.Items.Insert(1, new ListItem("Brokerage", "0"));
        }
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string adjustValue = string.Empty;

            string UpdatedExpectedAmt = string.Empty;


            GridFooterItem footerRow = (GridFooterItem)gvCommissionReceiveRecon.MasterTableView.GetItems(GridItemType.Footer)[0];
            foreach (GridDataItem dr in gvCommissionReceiveRecon.Items)
            {
                if (dr["ReconStatus"].Text == "closed")
                {
                    msgReconClosed.Visible = true;
                }
                else
                {
                    if (((TextBox)footerRow.FindControl("txtAdjustAmountMultiple")).Text.Trim() == "")
                    {
                        adjustValue = ((TextBox)dr.FindControl("txtAdjustAmount")).Text;

                    }
                    else
                    {
                        adjustValue = ((TextBox)footerRow.FindControl("txtAdjustAmountMultiple")).Text;

                    }
                    CheckBox checkBox = (CheckBox)dr.FindControl("chkId");
                    if (checkBox.Checked)
                    {
                        if (!(string.IsNullOrEmpty(dr["expectedamount"].Text)) & !string.IsNullOrEmpty(adjustValue))
                        {
                            UpdatedExpectedAmt = (double.Parse(dr["expectedamount"].Text) + double.Parse(adjustValue)).ToString();
                            dr["UpdatedExpectedAmount"].Text = UpdatedExpectedAmt;
                        }
                        else if (!(string.IsNullOrEmpty(dr["expectedamount"].Text)))
                        {
                            UpdatedExpectedAmt = (double.Parse(dr["expectedamount"].Text)).ToString();
                            dr["UpdatedExpectedAmount"].Text = UpdatedExpectedAmt;

                        }
                        else if (!string.IsNullOrEmpty(adjustValue))
                        {
                            UpdatedExpectedAmt = (double.Parse(adjustValue)).ToString();
                            dr["UpdatedExpectedAmount"].Text = UpdatedExpectedAmt;
                        }
                    }
                }
            }

        }

        protected void btnUpload_OnClick(object Sender, EventArgs e)
        {

        }
    }
}