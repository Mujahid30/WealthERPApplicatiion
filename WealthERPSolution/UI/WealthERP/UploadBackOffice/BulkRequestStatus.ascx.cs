using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoOps;
using System.Globalization;
using Telerik.Web.UI;
using VoUser;
using BoCommon;
using BoUploads;
using WealthERP.Base;
using BoAdvisorProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VOAssociates;
using BOAssociates;
using VoOps;
using System.Collections;
using BoCommisionManagement;
using BoWerpAdmin;
using BoOnlineOrderManagement;

namespace WealthERP.UploadBackOffice
{
    public partial class BulkRequestStatus : System.Web.UI.UserControl
    {
       
        PriceBo priceBo = new PriceBo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        AssociatesBo associatesBo = new AssociatesBo();
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();
        AssociatesVO associatesVo = new AssociatesVO();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        string userType;
        string AgentCode = "0";
        string categoryCode = string.Empty;
        int amcCode = 0;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            associatesVo = (AssociatesVO)Session["associatesVo"];
            userVo = (UserVo)Session["userVo"];
           
            if (!IsPostBack)
            {
                if (Request.QueryString["IsRecevableReport"] != null)
                {
                    dvReceivable.Visible = false;
                    dvAssocicateReport.Visible = false;
                    if (Request.QueryString["IsRecevableReport"].ToString() == "1")
                    {
                        dvReceivable.Visible = true;
                        BindMutualFundDropDowns();
                       
                        LoadAllSchemeList(0);
                        BindProductDropdown();
                        BindMonthsAndYear();
                        associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                        if (associateuserheirarchyVo != null && associateuserheirarchyVo.AgentCode != null)
                        {
                            AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                            
                        }
                    }
                    else
                    {
                        dvAssocicateReport.Visible = true;
                    }
                   
                }
                
            }

        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            //Response.Redirect("~/OnlineOrder/MFOnlineOrder.aspx?ReportCode=100&agentCode=" + txtAgentCode.Text + "&fromDate=" + txtFromDate.Text.ToString() + "&toDate=" + txtToDate.Text.ToString() + "&IsDummyAgent="+cbIsDummyAgent.Checked.ToString());
            BindAssociatePayoutGrid(advisorVo.advisorId, txtAgentCode.Text, DateTime.Parse(txtFromDate.Text.ToString()), DateTime.Parse(txtToDate.Text.ToString()), cbIsDummyAgent.Checked);
        }
        private void BindAssociatePayoutGrid(int adviserId, String agentCode, DateTime fromDate, DateTime toDate, Boolean IsDummyAgent)
        {
            DataTable dtAssociatePayout = new DataTable();
            try
            {
                dtAssociatePayout = commisionReceivableBo.GetAssociateCommissionPayout(adviserId, agentCode, fromDate, toDate,IsDummyAgent);
                rdAssociatePayout.DataSource = dtAssociatePayout;
                rdAssociatePayout.DataBind();
                pnlOrderList.Visible = true;
                rdAssociatePayout.Visible = true;
                btnExportFilteredDupData.Visible = true;
                if (Cache["AssociatePayout" + userVo.UserId] != null)
                {

                    Cache.Remove("AssociatePayout" + userVo.UserId);
                }
                Cache.Insert("AssociatePayout" + userVo.UserId, dtAssociatePayout);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BindAssociatePayoutGrid(int adviserId,String agentCode,DateTime fromDate,DateTime toDate)");
                object[] objects = new object[4];
                objects[0] = adviserId;
                objects[1] = agentCode;
                objects[2] = toDate;
                objects[3] = fromDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void rdAssociatePayout_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGIDetails = new DataTable();
            dtGIDetails = (DataTable)Cache["AssociatePayout" + userVo.UserId];
            rdAssociatePayout.Visible = true;
            rdAssociatePayout.DataSource = dtGIDetails;
        }
        protected void rbAssocicatieAll_AssociationSelection(object sender, EventArgs e)
        {
            tdlblAgentCode.Visible = false;
            tdtxtAgentCode.Visible = false;
            cbIsDummyAgent.Visible = true;
            if (rdAssociateInd.Checked == true)
            {
                tdlblAgentCode.Visible = true;
                tdtxtAgentCode.Visible = true;
                cbIsDummyAgent.Visible = false;
            }
        }
        protected void btnExportFilteredDupData_OnClick(object sender, ImageClickEventArgs e)
        {

            rdAssociatePayout.ExportSettings.OpenInNewWindow = true;
            rdAssociatePayout.ExportSettings.IgnorePaging = true;
            rdAssociatePayout.ExportSettings.HideStructureColumns = true;
            rdAssociatePayout.ExportSettings.ExportOnlyData = true;
      
            rdAssociatePayout.ExportSettings.Excel.Format = GridExcelExportFormat.Html;
            rdAssociatePayout.MasterTableView.ExportToExcel();

        }
        protected void btnExportReceivableReport_OnClick(object sender, ImageClickEventArgs e)
        {

            rgReceivableReport.ExportSettings.OpenInNewWindow = true;
            rgReceivableReport.ExportSettings.IgnorePaging = true;
            rgReceivableReport.ExportSettings.HideStructureColumns = true;
            rgReceivableReport.ExportSettings.ExportOnlyData = true;

            rgReceivableReport.ExportSettings.Excel.Format = GridExcelExportFormat.Html;
            rgReceivableReport.MasterTableView.ExportToExcel();

        }
        protected void btnExpand_Click(object sender, EventArgs e)
        {
            LinkButton button1 = (LinkButton)sender;
            if (button1.Text == "+")
            {
                foreach (GridDataItem gvr in this.rdAssociatePayout.Items)
                {

                    DataTable dtIssueDetail;
                    int issueId = 0;
                    string PAG_AssetGroupCode, PAISC_AssetInstrumentSubCategoryCode, AAC_AgentCode,commissionType;
                    DateTime PayOutDate;
                    LinkButton button = (LinkButton)gvr.FindControl("lbDetails");
                    RadGrid gvChildDetails = (RadGrid)gvr.FindControl("rgNCDIPOMIS");
                    Panel PnlChild = (Panel)gvr.FindControl("pnlchild");
                    issueId = int.Parse(rdAssociatePayout.MasterTableView.DataKeyValues[gvr.ItemIndex]["AIM_IssueId"].ToString());
                    AAC_AgentCode = rdAssociatePayout.MasterTableView.DataKeyValues[gvr.ItemIndex]["AgentCode"].ToString();
                    PAG_AssetGroupCode =rdAssociatePayout.MasterTableView.DataKeyValues[gvr.ItemIndex]["PAG_AssetGroupCode"].ToString();
                    PAISC_AssetInstrumentSubCategoryCode =rdAssociatePayout.MasterTableView.DataKeyValues[gvr.ItemIndex]["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                    commissionType = rdAssociatePayout.MasterTableView.DataKeyValues[gvr.ItemIndex]["WCD_CommissionType"].ToString();
                    PayOutDate = DateTime.Parse(rdAssociatePayout.MasterTableView.DataKeyValues[gvr.ItemIndex]["WCD_Act_Pay_BrokerageDate"].ToString());
                    dtIssueDetail = commisionReceivableBo.GetAgentProductWiseCommissionDetails(AAC_AgentCode, PAG_AssetGroupCode, PAISC_AssetInstrumentSubCategoryCode, issueId, advisorVo.advisorId, PayOutDate, commissionType);
                     gvChildDetails.DataSource = dtIssueDetail;
                     gvChildDetails.DataBind();
                    if (PnlChild.Visible == false)
                    {
                        PnlChild.Visible = true;
                        button.Text = "-";
                    }

                }
                button1.Text = "-";
            }
            else
            {
                foreach (GridDataItem gvr in this.rdAssociatePayout.Items)
                {
                    LinkButton button = (LinkButton)gvr.FindControl("lbDetails");
                    Panel PnlChild = (Panel)gvr.FindControl("pnlchild");
                    if (PnlChild.Visible == true)
                        PnlChild.Visible = false;
                    button.Text = "+";
                }
                button1.Text = "+";
            }

        }
        protected void btnExpandAll_Click(object sender, EventArgs e)
        {

            int count = rdAssociatePayout.MasterTableView.Items.Count;
            DataTable dtIssueDetail;
            int strIssuerId = 0;
            LinkButton buttonlink = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)buttonlink.NamingContainer;
            int issueId = 0;
            string PAG_AssetGroupCode, PAISC_AssetInstrumentSubCategoryCode, AAC_AgentCode, commissionType;
            DateTime PayOutDate;
            issueId = int.Parse(rdAssociatePayout.MasterTableView.DataKeyValues[gdi.ItemIndex]["AIM_IssueId"].ToString());
            AAC_AgentCode = rdAssociatePayout.MasterTableView.DataKeyValues[gdi.ItemIndex]["AgentCode"].ToString();
            PAG_AssetGroupCode = rdAssociatePayout.MasterTableView.DataKeyValues[gdi.ItemIndex]["PAG_AssetGroupCode"].ToString();
            PAISC_AssetInstrumentSubCategoryCode = rdAssociatePayout.MasterTableView.DataKeyValues[gdi.ItemIndex]["PAISC_AssetInstrumentSubCategoryCode"].ToString();
            commissionType = rdAssociatePayout.MasterTableView.DataKeyValues[gdi.ItemIndex]["WCD_CommissionType"].ToString();
            PayOutDate = DateTime.Parse(rdAssociatePayout.MasterTableView.DataKeyValues[gdi.ItemIndex]["WCD_Act_Pay_BrokerageDate"].ToString());
            dtIssueDetail = commisionReceivableBo.GetAgentProductWiseCommissionDetails(AAC_AgentCode, PAG_AssetGroupCode, PAISC_AssetInstrumentSubCategoryCode, issueId, advisorVo.advisorId, PayOutDate, commissionType);
            RadGrid gvChildDetails = (RadGrid)gdi.FindControl("rgNCDIPOMIS");
            Panel PnlChild = (Panel)gdi.FindControl("pnlchild");
            if (PnlChild.Visible == false)
            {
                PnlChild.Visible = true;
                buttonlink.Text = "-";
            }
            else if (PnlChild.Visible == true)
            {
                PnlChild.Visible = false;
                buttonlink.Text = "+";
            }
            //dtIssueDetail = commisionReceivableBo.GetAgentProductWiseCommissionDetails(AAC_AgentCode, PAG_AssetGroupCode, PAISC_AssetInstrumentSubCategoryCode, issueId, advisorVo.advisorId, PayOutDate, commissionType);
            gvChildDetails.DataSource = dtIssueDetail;
            gvChildDetails.DataBind();
        }
        private void BindMonthsAndYear()
        {
           

        }

        protected void ddlIssuer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssuer.SelectedIndex != 0)
            {
                int amcCode = int.Parse(ddlIssuer.SelectedValue);
               
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
                BindMappedIssues(ddlIssueType.SelectedValue, ddlProduct.SelectedValue, 2, (ddlProductCategory.SelectedValue == "") ? "FIFIIP" : ddlProductCategory.SelectedValue);

            }
        }
       
        protected void ddlIssueType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueType.SelectedIndex != 0)
            {
                ddlIssueName.Items.Clear();
                ddlIssueName.DataBind();
                BindMappedIssues(ddlIssueType.SelectedValue, ddlProduct.SelectedValue, 0, (ddlProductCategory.SelectedValue == "") ? "FIFIIP" : ddlProductCategory.SelectedValue);

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
                
                cvddlIssueType.Enabled = false;
                Label1.Visible = true;
                ddlCommType.Visible = true;
                td2.Visible = false;
                td1.Visible = false;


            }
            else if (asset == "FI" || asset == "IP")
            {
                trSelectMutualFund.Visible = false;
                trNCDIPO.Visible = true;
              
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
      
        protected void ddlProductCategory_OnSelectedIndexChanged(object Sender, EventArgs e)
        {
            if (ddlProductCategory.SelectedValue != "Select")
            {
                td1.Visible = false;
                td2.Visible = false;
                if (ddlProductCategory.SelectedValue != "FISDSD")
                {
                    
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
                
                //dtLoadAllScheme = priceBo.GetAllScehmeList(amcCode);
                dsLoadAllScheme = priceBo.GetSchemeListCategoryConcatenation(amcCode, categoryCode);
                dtLoadAllScheme = dsLoadAllScheme.Tables[0];
            }


        }
        private void SetParameters()
        {
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
            hdnFromDate.Value = rptTxtFromDate.Text.ToString();
        hdnToDate.Value=  rpttxtToDate.Text.ToString();
        hdnschemeId.Value = "0";

        }

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
        protected void GetCommisionTypes()
        {

            //ddlSearchType.Items.Insert(0, new ListItem("Select", "Select"));
            //ddlSearchType.Items.Insert(1, new ListItem("Brokerage", "0"));
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
        protected void btnGO_OnClick(object sender, EventArgs e)
        {
            SetParameters();
            Response.Redirect("~/Reports/CommonViewer.aspx?ReportCode=25&Product=" + ddlProduct.SelectedValue + "&ProductCategory=" + hdnProductCategory.Value + "&AmcCode=" + int.Parse(hdnSBbrokercode.Value) + "&SchemeId=" + int.Parse(hdnschemeId.Value) + "&AdviserId=" + advisorVo.advisorId + "&Issueid=" + int.Parse(hdnIssueId.Value) + "&Fromdate=" + hdnFromDate.Value+ "&Todate=" + hdnToDate.Value+"&IsReceivable="+ddlBrokerageType.SelectedValue);
        }
        protected void rgReceivableReport_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGIDetails = new DataTable();
            dtGIDetails = (DataTable)Cache["ProductCommissionReport" + userVo.UserId];
            rgReceivableReport.Visible = true;
            rgReceivableReport.DataSource = dtGIDetails;
        }
    }
}