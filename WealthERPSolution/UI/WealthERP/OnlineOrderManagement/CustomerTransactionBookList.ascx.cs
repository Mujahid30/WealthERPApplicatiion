﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using BoCommon;
using VoUser;
using WealthERP.Base;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using VoCustomerProfiling;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using System.Globalization;

namespace WealthERP.OnlineOrderManagement
{
    public partial class CustomerTransactionBookList : System.Web.UI.UserControl
    {
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        List<MFTransactionVo> mfTransactionList = null;
        MFTransactionVo mfTransactionVo = new MFTransactionVo();
        UserVo userVo = new UserVo();
        int schemePlanCode = 0;
        int customerId = 0;
        DateTime fromDate;
        DateTime toDate;
        int AccountId = 0;
        static double totalAmount = 0;
        static double totalUnits = 0;
        DataTable dtMFTransactions = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            customerId = customerVO.CustomerId;
            BindOrderStatus();
            BindAmc();
            BindLink();
            lbBack.Attributes.Add("onClick", "javascript:history.back(); return false;");
            if (!Page.IsPostBack)
            {
                fromDate = DateTime.Now.AddMonths(-1);
                txtFrom.SelectedDate = fromDate.Date;
                txtTo.SelectedDate = DateTime.Now;
            }
        }
        private void BindLastTradeDate()
        {
            DataSet ds = customerTransactionBo.GetLastTradeDate();
            txtFrom.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0][0].ToString());
            txtTo.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0][0].ToString());

        }
        /// <summary>
        /// Get Bind Orderstatus
        /// </summary>
        private void BindOrderStatus()
        {
            //ddlOrderStatus.Items.Clear();
            //DataSet dsOrderStatus;
            //DataTable dtOrderStatus;
            //dsOrderStatus = OnlineMFOrderBo.GetOrderStatus();
            //dtOrderStatus = dsOrderStatus.Tables[0];
            //if (dtOrderStatus.Rows.Count > 0)
            //{
            //    ddlOrderStatus.DataSource = dtOrderStatus;
            //    ddlOrderStatus.DataTextField = dtOrderStatus.Columns["WOS_OrderStep"].ToString();
            //    ddlOrderStatus.DataValueField = dtOrderStatus.Columns["WOS_OrderStepCode"].ToString();
            //    ddlOrderStatus.DataBind();
            //}
            //ddlOrderStatus.Items.Insert(0, new ListItem("All", "0"));
        }
        /// <summary>
        /// get AMC
        /// </summary>
        protected void BindAmc()
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
        protected void BindLink()
        {
            if (Request.QueryString["folionum"] != null && Request.QueryString["SchemePlanCode"] != null)
            {
                int accountId = int.Parse(Request.QueryString["folionum"].ToString());
                int SchemePlanCode = int.Parse(Request.QueryString["SchemePlanCode"].ToString());
                AccountId = int.Parse(accountId.ToString());
                BindLastTradeDate();
                string fromdate = "01-01-1990";
                txtFrom.SelectedDate = DateTime.Parse(fromdate);
                ViewState["SchemePlanCode"] = SchemePlanCode;
                hdnAmc.Value = "0";
                hdnOrderStatus.Value = "0";
                BindGrid();
                lbBack.Visible = true;
            }
        }
                

        /// <summary>
        /// Get Folio Account for Customer
        /// </summary>
        private void BindFolioAccount()
        {
        //    ddlAccount.Items.Clear();
        //    DataSet dsFolioAccount;
        //    DataTable dtFolioAccount;
        //    dsFolioAccount = OnlineMFOrderBo.GetFolioAccount(customerId);
        //    dtFolioAccount = dsFolioAccount.Tables[0];
        //    if (dtFolioAccount.Rows.Count > 0)
        //    {
        //        ddlAccount.DataSource = dsFolioAccount.Tables[0];
        //        ddlAccount.DataTextField = dtFolioAccount.Columns["CMFA_FolioNum"].ToString();
        //        ddlAccount.DataValueField = dtFolioAccount.Columns["CMFA_AccountId"].ToString();
        //        ddlAccount.DataBind();
        //    }
        //    ddlAccount.Items.Insert(0, new ListItem("All", "0"));
        }
         private void SetParameter()
        {
           //if (ddlOrderStatus.SelectedIndex != 0)
           // {
           //     hdnOrderStatus.Value = ddlOrderStatus.SelectedValue;
           //     ViewState["OrderstatusDropDown"] = hdnOrderStatus.Value;
           // }
           // else
           // {
           //     hdnOrderStatus.Value = "0";
           // }
            if (ddlAmc.SelectedIndex != 0)
            {
                hdnAmc.Value = ddlAmc.SelectedValue;
                ViewState["AMCDropDown"] = hdnAmc.Value;
            }
            else
            {
                hdnAmc.Value = "0";
            }
            
        }
         protected void btnViewTransaction_Click(object sender, EventArgs e)
         {
             SetParameter();
             BindGrid();
         }
        private void BindGrid()
        {
          
            if (Request.QueryString["strPortfolio"] != null)
            {
                string portfolio = Request.QueryString["strPortfolio"].ToString();
                if (portfolio != "MyPortfolio")
                {
                    ddlPortfolioGroup.SelectedItem.Value = "0";
                    ddlPortfolioGroup.SelectedItem.Text = "UnManaged";
                }
                else
                {
                    ddlPortfolioGroup.SelectedItem.Value = "1";
                    ddlPortfolioGroup.SelectedItem.Text = "Managed";
                }

            }
            
            DataSet ds = new DataSet();          
            if (txtFrom.SelectedDate != null)
            fromDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
            if (txtTo.SelectedDate != null)
            toDate = DateTime.Parse(txtTo.SelectedDate.ToString());
            schemePlanCode = Convert.ToInt32(ViewState["SchemePlanCode"]);
            mfTransactionList = customerTransactionBo.GetCustomerTransactionsBook(advisorVo.advisorId, customerId, fromDate, toDate, int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()), int.Parse(hdnAmc.Value),AccountId,schemePlanCode);
                if (mfTransactionList.Count != 0)
                {
                   
                    DataTable dtMFTransactions = new DataTable();
                    dtMFTransactions.Columns.Add("TransactionId");
                    dtMFTransactions.Columns.Add("Customer Name");                   
                    dtMFTransactions.Columns.Add("Folio Number");
                    dtMFTransactions.Columns.Add("Scheme Name");
                    dtMFTransactions.Columns.Add("Transaction Type");
                    dtMFTransactions.Columns.Add("Transaction Date", typeof(DateTime));
                    dtMFTransactions.Columns.Add("Price", typeof(double));
                    dtMFTransactions.Columns.Add("Units", typeof(double));
                    dtMFTransactions.Columns.Add("Amount", typeof(double));
                    dtMFTransactions.Columns.Add("STT", typeof(double));
                    dtMFTransactions.Columns.Add("Portfolio Name");
                    dtMFTransactions.Columns.Add("TransactionStatus");
                    dtMFTransactions.Columns.Add("Category");
                    dtMFTransactions.Columns.Add("AMC");
                    dtMFTransactions.Columns.Add("ADUL_ProcessId");
                    dtMFTransactions.Columns.Add("CMFT_SubBrokerCode");
                    dtMFTransactions.Columns.Add("PAISC_AssetInstrumentSubCategoryName");
                    dtMFTransactions.Columns.Add("CreatedOn");
                    dtMFTransactions.Columns.Add("CMFT_ExternalBrokerageAmount", typeof(double));
                    dtMFTransactions.Columns.Add("CMFT_Area");
                    dtMFTransactions.Columns.Add("CMFT_EUIN"); 
                    dtMFTransactions.Columns.Add("CurrentNAV");
                    dtMFTransactions.Columns.Add("DivReinvestment");
                    dtMFTransactions.Columns.Add("DivFrequency");
                    dtMFTransactions.Columns.Add("Channel");
                    dtMFTransactions.Columns.Add("OrderNo");
                    //dtMFTransactions.Columns.Add("TransactionNumber");
                    dtMFTransactions.Columns.Add("CO_OrderDate", typeof(DateTime));
                    
                    

                    DataRow drMFTransaction;
                    for (int i = 0; i < mfTransactionList.Count; i++)
                    {
                        drMFTransaction = dtMFTransactions.NewRow();
                        mfTransactionVo = new MFTransactionVo();
                        mfTransactionVo = mfTransactionList[i];
                        drMFTransaction[0] = mfTransactionVo.TransactionId.ToString();
                        drMFTransaction[1] = mfTransactionVo.CustomerName.ToString();
                        drMFTransaction[2] = mfTransactionVo.Folio.ToString();
                        drMFTransaction[3] = mfTransactionVo.SchemePlan.ToString();
                        drMFTransaction[4] = mfTransactionVo.TransactionType.ToString();
                        drMFTransaction[5] = mfTransactionVo.TransactionDate.ToShortDateString().ToString();
                        drMFTransaction[6] = decimal.Parse(mfTransactionVo.Price.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                       
                        drMFTransaction[7] = mfTransactionVo.Units.ToString("f4");
                        totalUnits = totalUnits + mfTransactionVo.Units;
                        drMFTransaction[8] = decimal.Parse(mfTransactionVo.Amount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                       
                        totalAmount = totalAmount + mfTransactionVo.Amount;
                        drMFTransaction[9] = decimal.Parse(mfTransactionVo.STT.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                       
                        drMFTransaction[10] = mfTransactionVo.PortfolioName.ToString();
                        drMFTransaction[11] = mfTransactionVo.TransactionStatus.ToString();
                        drMFTransaction[12] = mfTransactionVo.Category;
                        drMFTransaction[13] = mfTransactionVo.AMCName;

                        if (mfTransactionVo.ProcessId == 0)
                            drMFTransaction[14] = "N/A";
                        else
                            drMFTransaction[14] = int.Parse(mfTransactionVo.ProcessId.ToString());
                        drMFTransaction[15] = mfTransactionVo.SubBrokerCode;
                        drMFTransaction[16] = mfTransactionVo.SubCategoryName;
                        drMFTransaction[17] = mfTransactionVo.CreatedOn;
                        drMFTransaction[18] = decimal.Parse(mfTransactionVo.BrokerageAmount.ToString());
                        drMFTransaction["CMFT_Area"] = mfTransactionVo.Area.ToString();
                        drMFTransaction["CMFT_EUIN"] = mfTransactionVo.EUIN.ToString();                       
                        drMFTransaction["DivReinvestment"] = mfTransactionVo.DivReinvestmen.ToString(); ;
                        drMFTransaction["DivFrequency"] = mfTransactionVo.Divfrequency.ToString(); ;
                        drMFTransaction["Channel"] = mfTransactionVo.channel.ToString();                        
                        drMFTransaction["OrderNo"] = mfTransactionVo.orderNo;
                        drMFTransaction["CurrentNav"] = mfTransactionVo.latestNav;
                        //drMFTransaction["TransactionNumber"] = mfTransactionVo.TrxnNo.ToString();
                        drMFTransaction["CO_OrderDate"] = DateTime.Parse(mfTransactionVo.OrdDate.ToString());

                        dtMFTransactions.Rows.Add(drMFTransaction);
                    }                   
                   
                    if (Cache["ViewTransaction" + userVo.UserId ] == null)
                    {
                        Cache.Insert("ViewTransaction" + userVo.UserId , dtMFTransactions);
                    }
                    else
                    {
                        Cache.Remove("ViewTransaction" + userVo.UserId );
                        Cache.Insert("ViewTransaction" + userVo.UserId, dtMFTransactions);
                    }
                    gvTransationBookMIS.CurrentPageIndex = 0;
                    gvTransationBookMIS.DataSource = dtMFTransactions;
                    Session["gvMFTransactions"] = dtMFTransactions;
                    gvTransationBookMIS.DataBind();
                    pnlTransactionBook.Visible = true;
                    //ErrorMessage.Visible = false;
                    gvTransationBookMIS.Visible = true;
                    btnExport.Visible = true;
                    trNoRecords.Visible = false;
                }
                else
                {
                    gvTransationBookMIS.DataSource = dtMFTransactions;
                    gvTransationBookMIS.DataBind();
                    //hdnRecordCount.Value = "0";
                    //ErrorMessage.Visible = true;
                    trNoRecords.Visible = true;
                    pnlTransactionBook.Visible = true;
                    btnExport.Visible = false;

                }                
            }
        protected void gvTransationBookMIS_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtMFTransaction = new DataTable();
            dtMFTransaction = (DataTable)Cache["ViewTransaction" + userVo.UserId];
            if (dtMFTransaction != null)
            {
                gvTransationBookMIS.DataSource = dtMFTransaction;
                gvTransationBookMIS.Visible = true;               
               
            }           

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