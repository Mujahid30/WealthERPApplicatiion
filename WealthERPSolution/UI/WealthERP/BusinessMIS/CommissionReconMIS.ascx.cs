using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using BoAdvisorProfiling;
using BOAssociates;
using BoCommisionManagement;
using BoCommon;
using BoCustomerGoalProfiling;
using BoOnlineOrderManagement;
using BoUploads;
using BoUser;
using BoWerpAdmin;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Telerik.Web.UI;
using VOAssociates;
using VoUser;
using WealthERP.Base;
using BoCommon;



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
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        WERPTaskRequestManagementBo werpTaskRequestManagementBo = new WERPTaskRequestManagementBo();
        string categoryCode = string.Empty;
        int amcCode = 0;
        string AgentCode;


        protected void Page_load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            SessionBo.CheckSession();

            if (!IsPostBack)
            {

                BindProductAmc();
                BindNAVCategory();
                LoadAllSchemeList(0);
                GetProductList();
                BindMonthsAndYear(ddlMnthQtr, ddlYear);
                BindMonthsAndYear(ddlRequesttMnthQtr, ddlRequestYear);
                hdnAgentCode.Value = "0";
                btnExportFilteredData.Visible = false;
                associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                if (associateuserheirarchyVo != null && associateuserheirarchyVo.AgentCode != null)
                {
                    hdnAgentCode.Value = associateuserheirarchyVo.AgentCode.ToString();
                    ddlSelectMode.Items.FindByText("Both").Enabled = false;
                    ddlSelectMode.Items.FindByText("Online-Only").Enabled = false;
                }
            }


        }
        private void BindMonthsAndYear(DropDownList ddlMnthQtr, DropDownList ddlYear)
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
                td1.Visible = false;
                td2.Visible = false;
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

        public void BindMutualFundDropDowns(DropDownList ddlIssuer, DataTable dtGetMutualFundList)
        {

            ddlIssuer.DataSource = dtGetMutualFundList;
            ddlIssuer.DataTextField = dtGetMutualFundList.Columns["PA_AMCName"].ToString();
            ddlIssuer.DataValueField = dtGetMutualFundList.Columns["PA_AMCCode"].ToString();
            ddlIssuer.DataBind();
            ddlIssuer.Items.Insert(0, new ListItem("All", "0"));
        }
        public void BindProductAmc()
        {
            PriceBo priceBo = new PriceBo();
            DataTable dtGetMutualFundList = new DataTable();
            dtGetMutualFundList = priceBo.GetMutualFundList();
            BindMutualFundDropDowns(ddlIssuer, dtGetMutualFundList);
            BindMutualFundDropDowns(ddlRequestAmc, dtGetMutualFundList);

        }
        private void ShowHideControlsBasedOnProduct(string asset)
        {
            tdCategory.Visible = false;
            tdDdlCategory.Visible = false;
            ddlProductCategory.Items.Clear();
            ddlProductCategory.DataBind();
            td2.Visible = true;
            td1.Visible = true;
            Label3.Visible = true;
            ddlOrderType.Visible = true;
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
        protected void rgNCDIPOMIS_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                rgNCDIPOMIS.MasterTableView.GetColumn("Rec_rate").Visible = true;
                rgNCDIPOMIS.MasterTableView.GetColumn("Rec_WCU_UnitCode").Visible = true;
                rgNCDIPOMIS.MasterTableView.GetColumn("Rec_brokeragevalue").Visible = true;
                rgNCDIPOMIS.MasterTableView.GetColumn("Rec_borkageExpectedvalue").Visible = true;
                rgNCDIPOMIS.MasterTableView.GetColumn("rate").Visible = true;
                rgNCDIPOMIS.MasterTableView.GetColumn("WCU_UnitCode").Visible = true;
                rgNCDIPOMIS.MasterTableView.GetColumn("brokeragevalue").Visible = true;
                rgNCDIPOMIS.MasterTableView.GetColumn("borkageExpectedvalue").Visible = true;
                rgNCDIPOMIS.MasterTableView.GetColumn("Retention1").Visible = true;
                rgNCDIPOMIS.MasterTableView.GetColumn("AIIC_InvestorCatgeoryName").Visible = true;
                if (hdnAgentCode.Value.ToString() != "0")
                {
                    rgNCDIPOMIS.MasterTableView.GetColumn("Rec_rate").Visible = false;
                    rgNCDIPOMIS.MasterTableView.GetColumn("Rec_WCU_UnitCode").Visible = false;
                    rgNCDIPOMIS.MasterTableView.GetColumn("Rec_brokeragevalue").Visible = false;
                    rgNCDIPOMIS.MasterTableView.GetColumn("Rec_borkageExpectedvalue").Visible = false;
                    rgNCDIPOMIS.MasterTableView.GetColumn("Retention1").Visible = false;
                }
                if (hdnProductCategory.Value.ToString() == "FICGCG")
                {
                    rgNCDIPOMIS.MasterTableView.GetColumn("AIIC_InvestorCatgeoryName").Visible = false;
                }




            }
        }
        protected void gvWERPTrans_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("totalNAV").Visible = false;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("PerDayAssets").Visible = false;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("perDayTrail").Visible = false;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("Age").Visible = false;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("action").Visible = false;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("CMFT_ReceivedCommissionAdjustment").Visible = false;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("UpdatedExpectedAmount").Visible = false;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("RecborkageExpectedvalue").Visible = true;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("ACSR_BrokerageValue").Visible = true;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("WCU_UnitCode").Visible = true;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("expectedamount").Visible = true;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("PayborkageExpectedvalue").Visible = true;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("Retention1").Visible = false;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("Rec_WCU_UnitCode").Visible = true;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("Rec_Expectedamount").Visible = true;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("Rec_ACSR_BrokerageValue").Visible = true;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("TransactionAsOnDate").Visible = false;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("CumNAv").Visible = false;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("CMFT_NAV").Visible = false;
                gvCommissionReceiveRecon.MasterTableView.GetColumn("ClS_NAV").Visible = false;

                if (dataItem["commissionType"].Text == "Trail")
                {
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("totalNAV").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("PerDayAssets").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("perDayTrail").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("Age").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("TransactionAsOnDate").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("CumNAv").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("CMFT_NAV").Visible = true;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("ClS_NAV").Visible = true;

                }

                if (hdnAgentCode.Value.ToString() != "0")
                {
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("Rec_WCU_UnitCode").Visible = false;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("Rec_Expectedamount").Visible = false;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("Rec_ACSR_BrokerageValue").Visible = false;
                    gvCommissionReceiveRecon.MasterTableView.GetColumn("RecborkageExpectedvalue").Visible = false;
                }

            }

        }
        protected void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {

            if (ViewState["ProductType"].ToString() == "MF")
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
        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "RI")
            {
                tdlblRequestId.Visible = true;
                tdtxtRequestId.Visible = true;


                tdlbFromdate.Visible = false;
                tdtxtReqFromDate.Visible = false;
                tdlblToDate.Visible = false;
                tdtxtReqToDate.Visible = false;
                tdbtnGo2.Visible = true;

                rfvFromDate.Visible = false;
                rfvToDate.Visible = false;
                txtReqFromDate.SelectedDate = null;
                txtReqToDate.SelectedDate = null;
                gvBrokerageRequestStatus.Visible = false;
            }
            else if (ddlType.SelectedValue == "RT")
            {

                tdlblRequestId.Visible = false;
                tdtxtRequestId.Visible = false;
                tdlbFromdate.Visible = false;
                tdtxtReqFromDate.Visible = false;
                tdlblToDate.Visible = false;
                tdtxtReqToDate.Visible = false;
                tdbtnGo2.Visible = true;
                rfvRequestId.Visible = false;
                rfvFromDate.Visible = false;
                rfvToDate.Visible = false;
                txtRequestId.Text = "";
                txtReqFromDate.SelectedDate = null;
                txtReqToDate.SelectedDate = null;
                gvBrokerageRequestStatus.Visible = false;
            }
            else if (ddlType.SelectedValue == "RD")
            {
                tdlbFromdate.Visible = true;
                tdtxtReqFromDate.Visible = true;
                tdlblToDate.Visible = true;
                tdtxtReqToDate.Visible = true;
                tdlblRequestId.Visible = false;
                tdtxtRequestId.Visible = false;
                tdbtnGo2.Visible = true;
                rfvRequestId.Visible = false;
                txtRequestId.Text = "";
                gvBrokerageRequestStatus.Visible = false;
            }
        }
        protected void GdBind_Click(Object sender, EventArgs e)
        {
            SetParameters();
            DataSet ds = new DataSet();
            int reqId = 0;
            WERPTaskRequestManagementBo werpTaskRequestManagementBo = new WERPTaskRequestManagementBo();
            string requestHash = werpTaskRequestManagementBo.RequestCalculationHash(12, ddlProduct.SelectedValue, int.Parse(ddlSelectMode.SelectedValue), advisorVo.advisorId, int.Parse(hdnschemeId.Value), int.Parse(hdnFromDate.Value), int.Parse(hdnToDate.Value), hdnCategory.Value, ddlCommType.SelectedValue, int.Parse(hdnSBbrokercode.Value), int.Parse(hdnIssueId.Value), Convert.ToInt32(ddlSearchType.SelectedValue), ddlOrderStatus.SelectedValue, hdnAgentCode.Value.ToString(), hdnProductCategory.Value, int.Parse(ddlOrderType.SelectedValue));
            if (!werpTaskRequestManagementBo.CheckCalculationRequestExists(requestHash, advisorVo.advisorId))
            {
                werpTaskRequestManagementBo.CreateTaskRequestForBrokerageCalculation(12, userVo.UserId, out reqId, ddlProduct.SelectedValue, int.Parse(ddlSelectMode.SelectedValue), advisorVo.advisorId, int.Parse(hdnschemeId.Value), int.Parse(hdnFromDate.Value), int.Parse(hdnToDate.Value), hdnCategory.Value, null, ddlCommType.SelectedValue, int.Parse(hdnSBbrokercode.Value), int.Parse(hdnIssueId.Value), Convert.ToInt32(ddlSearchType.SelectedValue), ddlOrderStatus.SelectedValue, hdnAgentCode.Value.ToString(), hdnProductCategory.Value, int.Parse(ddlOrderType.SelectedValue), requestHash);
                if (reqId > 0)
                {
                    ShowMessage("Request Id-" + reqId.ToString() + "-Generated SuccessFully", "S");
                }
                else
                {
                    ShowMessage("Not able to create Request,Try again", "F");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Request already exists');", true);
            }
            //ds.ReadXml(Server.MapPath(@"\Sample.xml"));
            //dvMfMIS.Visible = false;
            //dvNCDIPOMIS.Visible = false;
            //ds = adviserMFMIS.GetCommissionReceivableRecon(ddlProduct.SelectedValue, int.Parse(ddlSelectMode.SelectedValue), advisorVo.advisorId, int.Parse(hdnschemeId.Value), int.Parse(hdnFromDate.Value), int.Parse(hdnToDate.Value), hdnCategory.Value, null, ddlCommType.SelectedValue, int.Parse(hdnSBbrokercode.Value), int.Parse(hdnIssueId.Value), Convert.ToInt32(ddlSearchType.SelectedValue), ddlOrderStatus.SelectedValue, hdnAgentCode.Value.ToString(), hdnProductCategory.Value, int.Parse(ddlOrderType.SelectedValue));
            //if (ds.Tables[0] != null)
            //{
            //    if (ddlProduct.SelectedValue.ToString() == "MF")
            //    {

            //        btnExportFilteredData.Visible = true;
            //        dvMfMIS.Visible = true;
            //        gvCommissionReceiveRecon.DataSource = ds.Tables[0];
            //        DataTable dtGetAMCTransactionDeatails = new DataTable();
            //        gvCommissionReceiveRecon.DataBind();
            //        if (Cache["gvCommissionReconMIs" + userVo.UserId.ToString()] != null)
            //        {
            //            Cache.Remove("gvCommissionReconMIs" + userVo.UserId.ToString());

            //        }
            //            Cache.Insert("gvCommissionReconMIs" + userVo.UserId.ToString(), ds.Tables[0]);


            //    }
            //    else
            //    {

            //        btnExportFilteredData.Visible = true;
            //        dvNCDIPOMIS.Visible = true;
            //        rgNCDIPOMIS.DataSource = ds.Tables[0];
            //        rgNCDIPOMIS.DataBind();
            //        if (Cache["rgNCDIPOMIS" + userVo.UserId.ToString()]!= null)
            //        {
            //            Cache.Remove("rgNCDIPOMIS" + userVo.UserId.ToString());
            //        }
            //            Cache.Insert("rgNCDIPOMIS" + userVo.UserId.ToString(), ds.Tables[0]);

            //    }
            //}

        }

        protected void gvBrokerageRequestStatus_OnDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                //if (string.IsNullOrEmpty(dataItem["Test"].Text))
                if (dataItem["StatusMsg"].Text != "Exported_Successfully")
                {
                    dataItem["Calculation"].Text = String.Empty;
                }
            }
        }
        protected void RequestModification_Click(object sender, EventArgs e)
        {

            Button btnSender = (Button)sender;
            int i = 0;
            StringBuilder sbRequestIds = new StringBuilder();
            string sRequestIds = string.Empty;
            if (werpTaskRequestManagementBo == null) werpTaskRequestManagementBo = new WERPTaskRequestManagementBo();
            foreach (GridDataItem dataItem in gvBrokerageRequestStatus.MasterTableView.Items)
            {
                if ((dataItem.FindControl("chkItem") as CheckBox).Checked)
                {
                    i = i + 1;
                    sbRequestIds.Append(gvBrokerageRequestStatus.MasterTableView.DataKeyValues[dataItem.ItemIndex]["WR_RequestId"].ToString() + "~");
                }
            }

            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Check Alteast one checkBox !');", true);
                return;
            }
            else
            {

                sRequestIds = sbRequestIds.Remove(sbRequestIds.Length - 1, 1).ToString();
                if (btnSender.CommandName == "Reprocess")
                {

                    werpTaskRequestManagementBo.UpdateBrokereageCalculationRequest(sRequestIds, userVo.UserId, 'U');
                }
                else if (btnSender.CommandName == "Delete")
                {
                    werpTaskRequestManagementBo.UpdateBrokereageCalculationRequest(sRequestIds, userVo.UserId, 'D');
                }
            }
        }
        protected void btnGo2_OnClick(object sender, EventArgs e)
        {

            DataTable DtStatus = werpTaskRequestManagementBo.GetBrokerageCalculationStatus(ddlRequestProduct.SelectedValue, null, int.Parse(ddlRequestAmc.SelectedValue), 0, ddlRequestCommissionType.SelectedValue, int.Parse(ddlRequesttMnthQtr.SelectedValue), int.Parse(ddlRequestYear.SelectedValue));
            gvBrokerageRequestStatus.DataSource = DtStatus;
            gvBrokerageRequestStatus.DataBind();
            gvBrokerageRequestStatus.Visible = true;
            btnExportFilteredData.Visible = false;
            dvMfMIS.Visible = false;
            dvNCDIPOMIS.Visible = false;
            if (DtStatus.Rows.Count > 0)
            {
                btnDelete.Visible = true;
                btnReprocess.Visible = true;
            }
            if (Cache["gvBulkOrderStatusList" + userVo.UserId.ToString()] != null)
            {
                Cache.Remove("gvBulkOrderStatusList" + userVo.UserId.ToString());

            }
            Cache.Insert("gvBulkOrderStatusList" + userVo.UserId.ToString(), DtStatus);


        }
        protected void gvBrokerageRequestStatus_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (e.CommandName == "Calculation")
            {
                GridDataItem ditem = (GridDataItem)e.Item;
                string RequestId = ditem["WR_RequestId"].Text.ToString().Trim();
                //string RequestDateTime = ditem["RequestDateTime"].Text.ToString().Substring(0, ditem["RequestDateTime"].Text.ToString().IndexOf(' '));
                //ViewState["ProductType"] = ditem["ProductType"].Text.ToString().Trim();
                bindProductWiseBrokerageGrid(int.Parse(RequestId));
                gvBrokerageRequestStatus.Visible = false;
            }
        }
        private void bindProductWiseBrokerageGrid(int reqId)
        {
            string productType, productCategory, commissionType;
            DataTable dtBrokerageDetails = new DataTable();
            dtBrokerageDetails = werpTaskRequestManagementBo.GetBrokerageCalculationDetails(reqId, out productType, out productCategory, out commissionType);
            if (productType == "MF")
            {
                btnExportFilteredData.Visible = true;
                dvMfMIS.Visible = true;
                gvCommissionReceiveRecon.DataSource = dtBrokerageDetails;
                gvCommissionReceiveRecon.DataBind();

                if (Cache["gvCommissionReconMIs" + userVo.UserId.ToString()] != null)
                {
                    Cache.Remove("gvCommissionReconMIs" + userVo.UserId.ToString());

                }
                Cache.Insert("gvCommissionReconMIs" + userVo.UserId.ToString(), dtBrokerageDetails);
            }
            else
            {
                btnExportFilteredData.Visible = true;
                dvNCDIPOMIS.Visible = true;
                rgNCDIPOMIS.DataSource = dtBrokerageDetails;
                rgNCDIPOMIS.DataBind();
                if (dtBrokerageDetails.Rows.Count > 0)
                {
                    btnDelete.Visible = true;
                    btnReprocess.Visible = true;
                }
                if (Cache["rgNCDIPOMIS" + userVo.UserId.ToString()] != null)
                {
                    Cache.Remove("rgNCDIPOMIS" + userVo.UserId.ToString());
                }
                Cache.Insert("rgNCDIPOMIS" + userVo.UserId.ToString(), dtBrokerageDetails);
            }

        }
        protected void gvBrokerageRequestStatus_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache[" gvBrokerageRequestStatus" + userVo.UserId];
            gvBrokerageRequestStatus.DataSource = dt;
            gvBrokerageRequestStatus.Visible = true;

        }
        protected void ddlProduct_SelectedIndexChanged(object source, EventArgs e)
        {

            ddlCommType.Items[3].Enabled = false;

            if (ddlProduct.SelectedValue != "Select")
            {
                if (ddlProduct.SelectedValue == "MF")
                {

                    ddlOrderStatus.Items[2].Enabled = false;
                    ddlOrderStatus.Items[1].Enabled = true;
                    ddlCommType.Items[3].Enabled = false;
                }
                else
                {
                    ddlOrderStatus.Items[1].Enabled = false;
                    ddlOrderStatus.Items[2].Enabled = true;

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
        private void ShowMessage(string msg, string type)
        {

            tblMessagee.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MessageForRequest", " showMsg('" + msg + "','" + type + "');", true);
        }
        private void BindProductDropdown(DropDownList ddlProductlist, DataTable dtProductList)
        {

            //Populating the product dropdown
            ddlProductlist.DataSource = dtProductList;
            ddlProductlist.DataValueField = dtProductList.Columns["PAG_AssetGroupCode"].ToString();
            ddlProductlist.DataTextField = dtProductList.Columns["PAG_AssetGroupName"].ToString();
            ddlProductlist.DataBind();
            ddlProductlist.Items.Insert(0, new ListItem("Select", "Select"));
        }
        protected void GetProductList()
        {
            try
            {

                DataSet dtProductList = commisionReceivableBo.GetProductType();
                BindProductDropdown(ddlProduct, dtProductList.Tables[0]);
                BindProductDropdown(ddlRequestProduct, dtProductList.Tables[0]);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GetProductList()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }



        }

        private void BindProductDropdown(DropDownList ddlProduct, DataSet dtProductList)
        {
            throw new NotImplementedException();
        }
        protected void GetCommisionTypes()
        {
            //DataSet dscommissionTypes;
            //dscommissionTypes = commisionReceivableBo.GetCommisionTypes();

            //ddlSearchType.DataSource = dscommissionTypes.Tables[0];
            //ddlSearchType.DataValueField = "WCMV_LookupId";
            //ddlSearchType.DataTextField = "WCMV_Name";
            //ddlSearchType.DataBind();
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
