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
namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineAdviserCustomerTransctionBook : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        OnlineOrderMISBo OnlineOrderMISBo = new OnlineOrderMISBo();
        UserVo userVo = new UserVo();
        PriceBo priceBo = new PriceBo();
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        List<MFTransactionVo> mfTransactionList = null;
        VoCustomerPortfolio.MFTransactionVo mfTransactionVo = new VoCustomerPortfolio.MFTransactionVo();
        DateTime fromDate;
        DateTime toDate;
        int rowCount = 0;
        int AccountId = 0;
        int IsSourceAA = 0;
        int systematicId = 0;
        int schemeplanCode = 0;
        int customerId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["reqId"] != null)
            {
                OnlineUserSessionBo.CheckSession();
                advisorVo = (AdvisorVo)Session["advisorVo"];
                customerVO = (CustomerVo)Session["customerVo"];
                userVo = (UserVo)Session["userVo"];
                BindTransactionGrid();
            }
            else
            {
                OnlineUserSessionBo.CheckSession();
                advisorVo = (AdvisorVo)Session["advisorVo"];
                customerVO = (CustomerVo)Session["customerVo"];
                userVo = (UserVo)Session["userVo"];

                BindAMC();
                fromDate = DateTime.Now.AddMonths(-1);
                txtFrom.SelectedDate = fromDate.Date;
                txtTo.SelectedDate = DateTime.Now;
            }
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["systematicId"] != null && Request.QueryString["AccountId"] != null && Request.QueryString["schemeplanCode"] != null && Request.QueryString["IsSourceAA"] != null && Request.QueryString["customerId"] != null)
                {
                    systematicId = int.Parse(Request.QueryString["systematicId"].ToString());
                    AccountId = int.Parse(Request.QueryString["AccountId"].ToString());
                    schemeplanCode = int.Parse(Request.QueryString["schemeplanCode"].ToString());
                    IsSourceAA = int.Parse(Request.QueryString["IsSourceAA"].ToString());
                    customerId = int.Parse(Request.QueryString["customerId"].ToString());
                    BindTransactionGrid();
                    //divConditional.Visible = false;

                }
            }
            else
            {
                fromDate = DateTime.Now.AddMonths(-1);
                txtFrom.SelectedDate = fromDate.Date;
                txtTo.SelectedDate = DateTime.Now;
            }

        }
        private void BindAMC()
        {
            //ddlAmc.Items.Clear();
            //try
            //{
            //    PriceBo priceBo = new PriceBo();
            //    DataTable dtGetAMCList = new DataTable();
            //    {
            //        dtGetAMCList = priceBo.GetMutualFundList();
            //        ddlAmc.DataSource = dtGetAMCList;
            //        ddlAmc.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
            //        ddlAmc.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
            //        ddlAmc.DataBind();
            //    }
            //    ddlAmc.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            //}
            try
            {
                ddlAmc.Items.Clear();
                DataSet ds = new DataSet();
                DataTable dtAmc = new DataTable();
                ds = OnlineMFOrderBo.GetTransAllAmcDetails(customerId);
                dtAmc = ds.Tables[0];
                if (dtAmc.Rows.Count > 0)
                {
                    ddlAmc.DataSource = dtAmc;
                    ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                    ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                    ddlAmc.DataBind();
                    //BindFolioNumber(int.Parse(ddlAmc.SelectedValue));

                }
                ddlAmc.Items.Insert(0, new ListItem("All", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeSetUp.ascx:BindAmcDropDown()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void btnViewTransaction_Click(object sender, EventArgs e)
        {
            BindTransactionGrid();
            btnExport.Visible = true;
        }
        protected void BindTransactionGrid()
        {

            DataTable dtBindTransactionGrid;
            if (txtFrom.SelectedDate != null)
                fromDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
            if (txtTo.SelectedDate != null)
                toDate = DateTime.Parse(txtTo.SelectedDate.ToString());
            if (Request.QueryString["systematicId"] != null && Request.QueryString["AccountId"] != null && Request.QueryString["schemeplanCode"] != null)
            {
                dtBindTransactionGrid = OnlineOrderMISBo.GetAdviserCustomerTransactionsBookSIP(advisorVo.advisorId, customerId, systematicId, IsSourceAA, AccountId, schemeplanCode,0);
                gvTransationBookMIS.DataSource = dtBindTransactionGrid;
                gvTransationBookMIS.DataBind();
                pnlTransactionBook.Visible = true;
            }
            else if (Request.QueryString["reqId"] != null)
            {
                int requestId = int.Parse(Request.QueryString["reqId"]);
                dtBindTransactionGrid = OnlineOrderMISBo.GetAdviserCustomerTransactionsBookSIP(advisorVo.advisorId, 0, 0, IsSourceAA, 0, schemeplanCode,requestId);
                gvTransationBookMIS.DataSource = dtBindTransactionGrid;
                gvTransationBookMIS.DataBind();
                pnlTransactionBook.Visible = true;
                trAMC.Visible = false;
            }
            else
            {
                dtBindTransactionGrid = BindTransaction(advisorVo.advisorId, int.Parse(ddlAmc.SelectedValue), fromDate, toDate, gvTransationBookMIS.PageSize, gvTransationBookMIS.CurrentPageIndex + 1, null, null, null, null, null, null, null, null, 0, out rowCount);
                gvTransationBookMIS.DataSource = dtBindTransactionGrid;
                gvTransationBookMIS.VirtualItemCount = rowCount;
                gvTransationBookMIS.DataBind();
                pnlTransactionBook.Visible = true;
            }
        }
        protected DataTable BindTransaction(int adviserId, int AmcCode, DateTime fromDate, DateTime toDate, int pageSize, int currentPage, string customerNamefilter, string custCode, string panNo, string folioNo, string schemeName, string type, string dividentType, string fundName, int orderNo, out int rowCount)
        {
            DataTable dtIPOIssueList = new DataTable();
            try
            {
                if (txtFrom.SelectedDate != null)
                    fromDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
                if (txtTo.SelectedDate != null)
                    toDate = DateTime.Parse(txtTo.SelectedDate.ToString());

                dtIPOIssueList = OnlineOrderMISBo.GetAdviserCustomerTransaction(advisorVo.advisorId, int.Parse(ddlAmc.SelectedValue), fromDate, toDate, gvTransationBookMIS.PageSize, gvTransationBookMIS.CurrentPageIndex + 1, customerNamefilter, custCode, panNo, folioNo, schemeName, type, dividentType, fundName, orderNo, out rowCount);

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
                //hdnOrderno.Value = (item["Co_OrderId"].Controls[0] as TextBox).Text;
            }
        }
        protected void gvTransationBookMIS_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindTransactionGrid;
            dtBindTransactionGrid = BindTransaction(advisorVo.advisorId, int.Parse(ddlAmc.SelectedValue), fromDate, toDate, gvTransationBookMIS.PageSize, gvTransationBookMIS.CurrentPageIndex + 1, hdncustomername.Value, hdncustcode.Value, hdnpanno.Value, hdnfoliono.Value, hdnschemename.Value, hdntype.Value, hdndividenttype.Value, hdnamcname.Value, 0, out rowCount);
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