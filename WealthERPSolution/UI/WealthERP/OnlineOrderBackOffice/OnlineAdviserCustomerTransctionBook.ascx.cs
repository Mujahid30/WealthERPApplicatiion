using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoOnlineOrderManagement;
using VoUser;
using BoCommon;
using BoWerpAdmin;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using BoProductMaster;
using WealthERP.Base;
using VoAdvisorProfiling;
using VOAssociates;
using BoCustomerProfiling;
namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineAdviserCustomerTransctionBook : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        RMVo rmVo = new RMVo();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        AdvisorVo adviserVo = new AdvisorVo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        OnlineOrderMISBo OnlineOrderMISBo = new OnlineOrderMISBo();
        UserVo userVo = new UserVo();
        AssociatesVO associatesVo = new AssociatesVO();
        PriceBo priceBo = new PriceBo();
        ProductMFBo productMFBo = new ProductMFBo();
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        AdvisorPreferenceVo advisorPrefernceVo = new AdvisorPreferenceVo();
        List<MFTransactionVo> mfTransactionList = null;
        AssociatesUserHeirarchyVo assocUsrHeirVo;
        VoCustomerPortfolio.MFTransactionVo mfTransactionVo = new VoCustomerPortfolio.MFTransactionVo();
        DateTime fromDate;
        bool Isdemat;
        DateTime toDate;
         string customerNamefilter;
         string custCode;
        string panNo;
        int schemePlanCode = 0;
        int rowCount = 0;
        int AccountId = 0;
        int IsSourceAA = 0;
        int systematicId = 0;
        int schemeplanCode = 0;
        int customerId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
               SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            advisorPrefernceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];
            rmVo = (RMVo)Session["rmVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            associatesVo = (AssociatesVO)Session["associatesVo"];
            assocUsrHeirVo = (AssociatesUserHeirarchyVo)Session["associatesUserHeirarchyVo"];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops" || userVo.AdviserRole.ContainsValue("CNT"))
            {
                txtCustomerName_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserAllCustomerName";
                txtPansearch_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtPansearch_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerPan";
                txtClientCode_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtClientCode_autoCompleteExtender.ServiceMethod = "GetCustCode";


            }
            
            if (Request.QueryString["reqId"] != null)
            {
                OnlineUserSessionBo.CheckSession();
                adviserVo = (AdvisorVo)Session["advisorVo"];
                customerVo = (CustomerVo)Session["customerVo"];
                userVo = (UserVo)Session["userVo"];
                BindTransactionGrid();
            }
            else
            {
                OnlineUserSessionBo.CheckSession();
                adviserVo = (AdvisorVo)Session["advisorVo"];
                customerVo = (CustomerVo)Session["customerVo"];
                userVo = (UserVo)Session["userVo"];

               
                fromDate = DateTime.Now.AddMonths(-1);
                txtFrom.SelectedDate = fromDate.Date;
                txtTo.SelectedDate = DateTime.Now;
            }
            if (!Page.IsPostBack)
            {
                BindAMC();
                Bindscheme();
                if (Request.QueryString["systematicId"] != null && Request.QueryString["AccountId"] != null && Request.QueryString["schemeplanCode"] != null && Request.QueryString["IsSourceAA"] != null && Request.QueryString["customerId"] != null)
                {
                    systematicId = int.Parse(Request.QueryString["systematicId"].ToString());
                    AccountId = int.Parse(Request.QueryString["AccountId"].ToString());
                    schemeplanCode = int.Parse(Request.QueryString["schemeplanCode"].ToString());
                    IsSourceAA = int.Parse(Request.QueryString["IsSourceAA"].ToString());
                    customerId = int.Parse(Request.QueryString["customerId"].ToString());
                    BindTransactionGrid();


                }

             

            }
            else
            {
                Bindscheme();
                fromDate = DateTime.Now.AddMonths(-1);
                txtFrom.SelectedDate = fromDate.Date;
                txtTo.SelectedDate = DateTime.Now;
            }

         

        }
        protected void ddlAmc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Bindscheme();
        }
        private void Bindscheme()
        {
            DataTable dt;
            if (ddlAmc.SelectedValue == "0")
            {
                dt = productMFBo.GetSchemePlanName(int.Parse(ddlAmc.SelectedValue));
                ddlSchemeList.DataSource = dt;
                ddlSchemeList.DataValueField = "PASP_SchemePlanCode";
                ddlSchemeList.DataTextField = "PASP_SchemePlanName";
                ddlSchemeList.DataBind();
                ddlSchemeList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            else
            {
                dt = productMFBo.GetSchemePlanName(int.Parse(ddlAmc.SelectedValue));
                ddlSchemeList.DataSource = dt;
                ddlSchemeList.DataValueField = "PASP_SchemePlanCode";
                ddlSchemeList.DataTextField = "PASP_SchemePlanName";
                ddlSchemeList.DataBind();
                ddlSchemeList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));

            }
        }
        protected void ddlOptionSearch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOptionSearch.SelectedValue == "Name")
            {
                tdtxtCustomerName.Visible = true;
                tdtxtClientCode.Visible = false;
                tdtxtPansearch.Visible = false;

            }
            else if (ddlOptionSearch.SelectedValue == "Panno")
            {
                tdtxtPansearch.Visible = true;
                tdtxtCustomerName.Visible = false;
                tdtxtClientCode.Visible = false;
            }
            else
            {
                tdtxtClientCode.Visible = true;
                tdtxtPansearch.Visible = false;
                tdtxtCustomerName.Visible = false;
            }
        }
        private string GetSelectedFilterValue()
        {

            string FilterOn;
            if (Request.QueryString["CustCode"] != null || Request.QueryString["action"] != null)
            {
                FilterOn = "customer";
            }
            else
            {
                if (ddlOptionSearch.SelectedValue == "Name" || ddlOptionSearch.SelectedValue == "Panno" || ddlOptionSearch.SelectedValue == "Clientcode")
                {
                    FilterOn = "customer";

                }
               
                else
                {
                    FilterOn = ddlOptionSearch.SelectedValue;
                    ddlOptionSearch.SelectedValue = string.Empty;
                }
            }

            return FilterOn;
        }
        protected void ddlsearchcustomertype_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsearchcustomertype.SelectedValue == "Individual")
            {
                ddlOptionSearch.Visible = true;
                lblCustomerSearch.Visible = true;
                txtCustomerName.Text = "";
                txtClientCode.Text = "";
                txtPansearch.Text = "";
            }
            else
            {

                lblCustomerSearch.Visible = false;
                ddlOptionSearch.Visible = false;
                    txtPansearch.Visible=false;
                    txtClientCode.Visible = false;
                    txtCustomerName.Visible = false;
            }
        }
        //private void BindAMC()
        //{
            
        //    try
        //    {
        //        ddlAmc.Items.Clear();
        //        DataSet ds = new DataSet();
        //        DataTable dtAmc = new DataTable();
        //        ds = OnlineMFOrderBo.GetTransAllAmcDetails(customerId);
        //        dtAmc = ds.Tables[0];
        //        if (dtAmc.Rows.Count > 0)
        //        {
        //            ddlAmc.DataSource = dtAmc;
        //            ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
        //            ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
        //            ddlAmc.DataBind();
        //            //BindFolioNumber(int.Parse(ddlAmc.SelectedValue));

        //        }
        //        ddlAmc.Items.Insert(0, new ListItem("All", "0"));
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "OnlineSchemeSetUp.ascx:BindAmcDropDown()");
        //        object[] objects = new object[3];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}
        protected void BindAMC()
        {
            ddlAmc.Items.Clear();
            if (ddlAmc.SelectedIndex == 0) return;

            DataTable dtAmc = commonLookupBo.GetProdAmc(0, true);
            if (dtAmc == null) return;

            if (dtAmc.Rows.Count > 0)
            {
                ddlAmc.DataSource = dtAmc;
                ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();
            }
            
            ddlAmc.Items.Insert(0, new ListItem("All", "0"));

        }
        protected void btnViewTransaction_Click(object sender, EventArgs e)
        {
            hdnSchemeSearch.Value = string.Empty;
            hdnCustomerNameSearch.Value = string.Empty;
           
            BindTransactionGrid();
            btnExport.Visible = true;
        }
        protected void BindTransactionGrid()
        {

            DataTable dtBindTransactionGrid;
            if (ddlsearchcustomertype.SelectedValue != "All")
            {
                if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
                    customerId = int.Parse(txtCustomerId.Value);
            }
           
            else
            {
                
            }
            if (txtFrom.SelectedDate != null)
                fromDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
            if (txtTo.SelectedDate != null)
                toDate = DateTime.Parse(txtTo.SelectedDate.ToString());
            if (Request.QueryString["systematicId"] != null && Request.QueryString["AccountId"] != null && Request.QueryString["schemeplanCode"] != null)
            {
                dtBindTransactionGrid = OnlineOrderMISBo.GetAdviserCustomerTransactionsBookSIP(adviserVo.advisorId, customerId, systematicId, IsSourceAA, AccountId, schemeplanCode, 0);
                gvTransationBookMIS.DataSource = dtBindTransactionGrid;
                gvTransationBookMIS.DataBind();
                pnlTransactionBook.Visible = true;
            }
            else if (Request.QueryString["reqId"] != null)
            {
                int requestId = int.Parse(Request.QueryString["reqId"]);
                dtBindTransactionGrid = OnlineOrderMISBo.GetAdviserCustomerTransactionsBookSIP(adviserVo.advisorId, 0, 0, IsSourceAA, 0, schemeplanCode, requestId);
                gvTransationBookMIS.DataSource = dtBindTransactionGrid;
                gvTransationBookMIS.DataBind();
                pnlTransactionBook.Visible = true;
                trAMC.Visible = false;
            }
            else
            {

                dtBindTransactionGrid = BindTransaction(adviserVo.advisorId, int.Parse(ddlAmc.SelectedValue), fromDate, toDate, gvTransationBookMIS.PageSize, gvTransationBookMIS.CurrentPageIndex + 1, txtCustomerName.Text, txtClientCode.Text, txtPansearch.Text, null, null, null, null, null, 0, out rowCount,Convert.ToBoolean(ddlMode.SelectedValue));
                gvTransationBookMIS.DataSource = dtBindTransactionGrid;
                gvTransationBookMIS.VirtualItemCount = rowCount;
                gvTransationBookMIS.DataBind();
                pnlTransactionBook.Visible = true;
               
              
            }
        }
        protected DataTable BindTransaction(int adviserId, int AmcCode, DateTime fromDate, DateTime toDate, int pageSize, int currentPage, string customerNamefilter, string custCode, string panNo, string folioNo, string schemeName, string type, string dividentType, string fundName, int orderNo, out int rowCount, bool Isdemat)
        {
            DataTable dtIPOIssueList = new DataTable();
            try
            {
                //Isdemat = Convert.ToBoolean(Convert.ToInt16(ddlMode.SelectedItem.Value));
                if (txtFrom.SelectedDate != null)
                    fromDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
                if (txtTo.SelectedDate != null)
                    toDate = DateTime.Parse(txtTo.SelectedDate.ToString());
                if (!string.IsNullOrEmpty(txtClientCode.Text))
                {
                    string ClientCode = txtClientCode.Text;
                }
                if (!string.IsNullOrEmpty(txtPansearch.Text))
                {
                    string pansearch = txtPansearch.Text;
                }
                if (!string.IsNullOrEmpty(ddlSchemeList.SelectedValue))
                {
                    schemePlanCode = int.Parse(ddlSchemeList.SelectedValue);
                }


                dtIPOIssueList = OnlineOrderMISBo.GetAdviserCustomerTransaction(adviserVo.advisorId, int.Parse(ddlAmc.SelectedValue), fromDate, toDate, gvTransationBookMIS.PageSize, gvTransationBookMIS.CurrentPageIndex + 1, customerNamefilter, custCode, panNo, folioNo, schemeName, type, dividentType, fundName, orderNo, out rowCount, Isdemat, schemePlanCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:BindCustomerGrid()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOIssueList;
        }
        protected void gvTransationBookMIS_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.FilterCommandName)
            {

                GridFilteringItem item = gvTransationBookMIS.MasterTableView.GetItems(GridItemType.FilteringItem)[0] as GridFilteringItem;
                gvTransationBookMIS.CurrentPageIndex = 0;
                hdncustomername.Value = (item["Name"].Controls[0] as TextBox).Text;
                hdnfoliono.Value = (item["CMFA_FolioNum"].Controls[0] as TextBox).Text;
                hdnschemename.Value = (item["PASP_SchemePlanName"].Controls[0] as TextBox).Text;
                hdncustcode.Value = (item["c_CustCode"].Controls[0] as TextBox).Text;
                hdnpanno.Value = (item["c_PANNum"].Controls[0] as TextBox).Text;
                hdnamcname.Value = (item["PA_AMCName"].Controls[0] as TextBox).Text;
                hdndividenttype.Value = (item["CMFOD_DividendOption"].Controls[0] as TextBox).Text;
                hdntype.Value = (item["WMTT_TransactionClassificationName"].Controls[0] as TextBox).Text;
            }
        }
        protected void gvTransationBookMIS_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindTransactionGrid = new DataTable();
            dtBindTransactionGrid = BindTransaction(adviserVo.advisorId, int.Parse(ddlAmc.SelectedValue), fromDate, toDate, gvTransationBookMIS.PageSize, gvTransationBookMIS.CurrentPageIndex + 1, hdncustomername.Value, hdncustcode.Value, hdnpanno.Value, hdnfoliono.Value, hdnschemename.Value, hdntype.Value, hdndividenttype.Value, hdnamcname.Value, 0, out rowCount, Isdemat);
            gvTransationBookMIS.DataSource = dtBindTransactionGrid;
            gvTransationBookMIS.VirtualItemCount = rowCount;

        }
        protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        {
            gvTransationBookMIS.ExportSettings.OpenInNewWindow = true;
            gvTransationBookMIS.ExportSettings.IgnorePaging = true;
            gvTransationBookMIS.ExportSettings.HideStructureColumns = true;
            gvTransationBookMIS.ExportSettings.ExportOnlyData = true;
            gvTransationBookMIS.ExportSettings.FileName = "Transaction Book Details";
            gvTransationBookMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvTransationBookMIS.MasterTableView.ExportToExcel();
        }
    }
}