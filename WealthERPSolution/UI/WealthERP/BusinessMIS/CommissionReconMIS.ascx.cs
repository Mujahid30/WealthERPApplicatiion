using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoWerpAdmin;
using System.Globalization;
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
using BoUser;



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
            SessionBo.CheckSession();
            if (!IsPostBack)
            {
                BindMutualFundDropDowns();
                BindNAVCategory();
                LoadAllSchemeList(0);
                BindProductDropdown();
                BindMonthsAndYear();
                int day = 1;
                //txtFrom.SelectedDate = DateTime.Parse(day.ToString()+'/'+DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString());
                //txtTo.SelectedDate = DateTime.Now;
                btnExportFilteredData.Visible = false;
            }
        }
        private void BindMonthsAndYear()
        {
            for (int i = 1; i <=12; i++)
            {
                string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                ddlMnthQtr.Items.Add(new ListItem(monthName, i.ToString().PadLeft(2, '0')));
            }
            ddlMnthQtr.Items.Add(new ListItem("Quater-1", "13"));
            ddlMnthQtr.Items.Add(new ListItem("Quater-2", "14"));
            ddlMnthQtr.Items.Add(new ListItem("Quater-3", "15"));
            ddlMnthQtr.Items.Add(new ListItem("Quater-4", "16"));
            ddlMnthQtr.Items.Insert(0, new ListItem("Select", "0"));
            for (int i = 1980; i <=2030; i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
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
        protected void ddlIssueType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueType.SelectedIndex != 0)
            {
                ddlIssueName.Items.Clear();
                ddlIssueName.DataBind();
                BindMappedIssues(ddlIssueType.SelectedValue,ddlProduct.SelectedValue,int.Parse(ddlSelectMode.SelectedValue));
                
            }
        }
        private void BindMappedIssues(string ModeOfIssue, string productType, int isOnlineIssue)
        {
            DataSet dsCommissionReceivable = commisionReceivableBo.GetIssuesStructureMapings(advisorVo.advisorId, "MappedIssue", ModeOfIssue, productType, isOnlineIssue);
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
            
            if (asset == "MF")
            {
                trSelectMutualFund.Visible = true;
                trNCDIPO.Visible = false;
                tdFromDate.Visible =true;
                tdToDate.Visible = true;
                tdFrom.Visible = true;
                tdTolbl.Visible = true;
                cvddlIssueType.Enabled = false;

            }
            else if (asset == "FI" ||asset == "IP")
            {
                trSelectMutualFund.Visible = false;
                trNCDIPO.Visible =true;
                tdFromDate.Visible = false;
                tdToDate.Visible = false;
                tdFrom.Visible = false;
                tdTolbl.Visible = false;
                ddlIssueName.Items.Clear();
                ddlIssueName.DataBind();
                cvddlIssueType.Enabled = true;

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
                if (string.IsNullOrEmpty(ddlMnthQtr.SelectedItem.Value.ToString())!=true)
                    hdnFromDate.Value = ddlMnthQtr.SelectedItem.Value.ToString();
                if (string.IsNullOrEmpty(ddlYear.SelectedItem.Value.ToString()) != true)
                    hdnToDate.Value = ddlYear.SelectedItem.Value.ToString();
                if (string.IsNullOrEmpty(ddlScheme.SelectedItem.Value.ToString()) != true)
                    hdnschemeId.Value = ddlScheme.SelectedItem.Value.ToString();
                if (string.IsNullOrEmpty(ddlCategory.SelectedItem.Value.ToString()) != true)
                    hdnCategory.Value = ddlCategory.SelectedItem.Value.ToString();
                if (string.IsNullOrEmpty(ddlIssuer.SelectedItem.Value.ToString()) != true)
                    hdnSBbrokercode.Value = ddlIssuer.SelectedItem.Value.ToString();
                if (string.IsNullOrEmpty(ddlIssueName.SelectedValue.ToString()) != true)
                    hdnIssueId.Value = ddlIssueName.SelectedValue.ToString();
                else
                    hdnIssueId.Value = "0";
             
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
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("Age").Visible = false;

                }
                else
                {
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("totalNAV").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("PerDayAssets").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("perDayTrail").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("Age").Visible =true;


                }
            }

        }
        protected void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            if (ddlProduct.SelectedValue.ToString() == "MF")
            {
                gvCommissionReceiveRecon.ExportSettings.OpenInNewWindow = true;
                gvCommissionReceiveRecon.ExportSettings.IgnorePaging = true;
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
            ds = adviserMFMIS.GetCommissionReceivableRecon(ddlProduct.SelectedValue, int.Parse(ddlSelectMode.SelectedValue), advisorVo.advisorId, int.Parse(hdnschemeId.Value), int.Parse(hdnFromDate.Value), int.Parse(hdnToDate.Value), hdnCategory.Value, null, ddlCommType.SelectedValue, int.Parse(hdnSBbrokercode.Value),int.Parse(hdnIssueId.Value));
            if (ds.Tables[0] != null)
            {
                if (ddlProduct.SelectedValue.ToString() == "MF")
                {
                    btnExportFilteredData.Visible = true;
                    dvMfMIS.Visible = true;
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
                else
                {
                    btnExportFilteredData.Visible = true;
                    dvNCDIPOMIS.Visible = true;
                    rgNCDIPOMIS.DataSource = ds.Tables[0];
                    rgNCDIPOMIS.DataBind();
                    if (Cache["rgNCDIPOMIS" + userVo.UserId.ToString()] == null)
                    {
                        Cache.Insert("rgNCDIPOMIS" + userVo.UserId.ToString(), ds.Tables[0]);
                    }
                    else
                    {
                        Cache.Remove("rgNCDIPOMIS" + userVo.UserId.ToString());
                        Cache.Insert("rgNCDIPOMIS" + userVo.UserId.ToString(), ds.Tables[0]);
                    }
                }
            }
           
        }
        protected void ddlProduct_SelectedIndexChanged(object source, EventArgs e)
        {
            if(ddlProduct.SelectedValue!="Select")
            {
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
    }
}