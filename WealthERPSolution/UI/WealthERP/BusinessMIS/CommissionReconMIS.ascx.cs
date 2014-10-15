using System;
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
using BoAdvisorProfiling;
using System.Configuration;
using BOAssociates;
using BoCommisionManagement;



namespace WealthERP.BusinessMIS
{
    public partial class CommissionReconMIS : System.Web.UI.UserControl
    {

        PriceBo priceBo = new PriceBo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        AssociatesBo associatesBo = new AssociatesBo();
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();

        string categoryCode = string.Empty;
        int amcCode = 0;


        protected void Page_load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                BindMutualFundDropDowns();
                BindNAVCategory();
                LoadAllSchemeList(0);
                BindProductDropdown();
                int day = 1;
                gvCommissionReceiveRecon.Visible = false;
                txtFrom.SelectedDate = DateTime.Parse(day.ToString()+'/'+DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString());
                txtTo.SelectedDate = DateTime.Now;
                btnExportFilteredData.Visible = false;
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
        private void BindMappedIssues(string ModeOfIssue,string productType)
        {

        }
        protected void gvCommissionReceiveRecon_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            string rcbType = string.Empty;
            dt = (DataTable)Cache["gvCommissionReceiveRecon" + userVo.UserId];
            if (ViewState["CommissionReceiveRecon"] != null)
                rcbType = ViewState["CommissionReceiveRecon"].ToString();
            if (!string.IsNullOrEmpty(rcbType))
            {
                DataView dvStaffList = new DataView(dt, "CommissionReceiveRecon = '" + rcbType + "'", "schemeplanname,transactiondate,amount,transactiontype,Age,currentvalue,expectedamount,calculatedDate,receivedamount,diff,ACSR_CommissionStructureRuleId,CMFT_MFTransId,CMFT_ReceivableExpectedAmount,CMFT_ReceivedCommissionAdjustment", DataViewRowState.CurrentRows);
                // DataView dvStaffList = dtMIS.DefaultView;
                gvCommissionReceiveRecon.DataSource = dvStaffList.ToTable();
                gvCommissionReceiveRecon.Visible = true;

            }
            else
            {
                gvCommissionReceiveRecon.DataSource = dt;
                gvCommissionReceiveRecon.Visible = true;

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

            if (asset == "MF")
            {
                trSelectMutualFund.Visible = true;

            }
            else if (asset == "FI")
            {
                trSelectMutualFund.Visible = false;

            }
            else if (asset == "IP")
            {
                trSelectMutualFund.Visible = false;
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
            //if (userVo.UserType=="advisor")
            //{
                if (string.IsNullOrEmpty(txtFrom.SelectedDate.ToString())!=true)
                    hdnFromDate.Value = txtFrom.SelectedDate.ToString();
                if (string.IsNullOrEmpty(txtTo.SelectedDate.ToString()) != true)
                    hdnToDate.Value = txtTo.SelectedDate.ToString();
                if (string.IsNullOrEmpty(ddlScheme.SelectedItem.Value.ToString()) != true)
                    hdnschemeId.Value = ddlScheme.SelectedItem.Value.ToString();
                if (string.IsNullOrEmpty(ddlCategory.SelectedItem.Value.ToString()) != true)
                    hdnCategory.Value = ddlCategory.SelectedItem.Value.ToString();
                if (string.IsNullOrEmpty(ddlIssuer.SelectedItem.Value.ToString()) != true)
                    hdnSBbrokercode.Value = ddlIssuer.SelectedItem.Value.ToString();
            
            //}


        }
        protected void gvWERPTrans_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (ddlCommType.SelectedValue != "TC")
                {
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("totalNAV").Visible = false;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("PerDayAssets").Visible = false;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("perDayTrail").Visible = false;

                }
                else
                {
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("totalNAV").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("PerDayAssets").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("perDayTrail").Visible = true;


                }
            }

        }
        protected void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {

            gvCommissionReceiveRecon.ExportSettings.OpenInNewWindow = true;
            gvCommissionReceiveRecon.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvCommissionReceiveRecon.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvCommissionReceiveRecon.MasterTableView.ExportToExcel();

        }
        protected void GdBind_Click(Object sender, EventArgs e)
        {
            SetParameters();
            DataSet ds = new DataSet();
            //ds.ReadXml(Server.MapPath(@"\Sample.xml"));
            ds = adviserMFMIS.GetCommissionReceivableRecon(ddlProduct.SelectedValue, int.Parse(ddlSelectMode.SelectedValue), advisorVo.advisorId, int.Parse(hdnschemeId.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), hdnCategory.Value, null, ddlCommType.SelectedValue, int.Parse(hdnSBbrokercode.Value), int.Parse(ddlIssueName.SelectedValue));
            if (ds.Tables[0] != null)
            {
                btnExportFilteredData.Visible = true;
                gvCommissionReceiveRecon.Visible = true;
                gvCommissionReceiveRecon.DataSource = ds.Tables[0];
                DataTable dtGetAMCTransactionDeatails = new DataTable();
                gvCommissionReceiveRecon.DataBind();
                if (Cache["gvCommissionReconMIs" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("gvCommissionReconMIs" + userVo.UserId.ToString(), ds.Tables[0]);
                }
                else
                {
                    Cache.Remove("gvCommissionReconMIs" + userVo.UserId.ToString());
                    Cache.Insert("gvCommissionReconMIs" + userVo.UserId.ToString(), ds.Tables[0]);
                }
            }
            else {
                gvCommissionReceiveRecon.Visible = false;
            
            }
        }
        protected void ddlProduct_SelectedIndexChanged(object source, EventArgs e)
        {
            if(ddlProduct.SelectedValue!="Select")
            {
                ShowHideControlsBasedOnProduct(ddlProduct.SelectedValue);
            }
        }
        protected void gvCommissionReconMIs_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["gvCommissionReconMIs" + userVo.UserId];
            gvCommissionReceiveRecon.DataSource = dt;
            gvCommissionReceiveRecon.Visible = true;
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
    }
}