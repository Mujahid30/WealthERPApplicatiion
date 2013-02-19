using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using System.Collections;
using iTextSharp.text.pdf;
using BoCommon;
using Telerik.Web.UI;

namespace WealthERP.CustomerPortfolio
{
    public partial class EquityTransactionsView : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        EQTransactionVo equityTransactionVo = new EQTransactionVo();
        List<EQTransactionVo> equityTransactionList = null;
        List<EQPortfolioVo> equityPortfolioList = null;
        int customerId;
        int index;
        static int portfolioId;
        PortfolioBo portfolioBo = new PortfolioBo();
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();
        Hashtable ht = new Hashtable();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        RadComboBox rcbTradeDate = new RadComboBox();
        //protected override void OnInit(EventArgs e)
        //{
        //    try
        //    {
        //        ((Pager)Pager1).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
        //        Pager1.EnableViewState = true;
        //        base.OnInit(e);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "EquityTransactionsView.ascx.cs:OnInit()");
        //        object[] objects = new object[0];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }


        //}

        //public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        GetPageCount();
        //        this.BindGridView(customerVo.CustomerId, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "EquityTransactionsView.ascx.cs:HandlePagerEvent()");
        //        object[] objects = new object[1];
        //        objects[0] = customerVo;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //}

        //private void GetPageCount()
        //{
        //    string upperlimit = "";
        //    int rowCount = 0;
        //    int ratio = 0;
        //    string lowerlimit = "";
        //    string PageRecords = "";
        //    try
        //    {

        //        if (hdnRecordCount.Value != "")
        //            rowCount = Convert.ToInt32(hdnRecordCount.Value);
        //        if (rowCount > 0)
        //        {
        //            ratio = rowCount / 30;
        //            Pager1.PageCount = rowCount % 30 == 0 ? ratio : ratio + 1;
        //            Pager1.Set_Page(Pager1.CurrentPage, Pager1.PageCount);
        //            lowerlimit = (((Pager1.CurrentPage - 1) * 30)+1).ToString();
        //            upperlimit = (Pager1.CurrentPage * 30).ToString();
        //            if (Pager1.CurrentPage == Pager1.PageCount)
        //                upperlimit = hdnRecordCount.Value;
        //            PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
        //            lblCurrentPage.Text = PageRecords;

        //            hdnCurrentPage.Value = Pager1.CurrentPage.ToString();
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "TransactionsView.ascx.cs:GetPageCount()");
        //        object[] objects = new object[5];
        //        objects[0] = upperlimit;
        //        objects[1] = rowCount;
        //        objects[2] = ratio;
        //        objects[3] = lowerlimit;
        //        objects[4] = PageRecords;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //}

        protected void Page_Load(object sender, EventArgs e)
        {

            txtFromTran.Attributes.Add("onkeydown", "return (event.keyCode!=13);");

            txtToTran.Attributes.Add("onkeydown", "return (event.keyCode!=13);");

            try
            {
                SessionBo.CheckSession();
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");



                customerVo = (CustomerVo)Session["CustomerVo"];
                customerId = customerVo.CustomerId;
                userVo = (UserVo)Session["userVo"];
                rmVo = (RMVo)Session["rmVo"];
                if (!IsPostBack)
                {

                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();
                    if (Session["tranDates"] != null)
                    {
                        ht = (Hashtable)Session["tranDates"];
                        txtFromTran.SelectedDate = DateTime.Parse(ht["From"].ToString());
                        txtToTran.SelectedDate = DateTime.Parse(ht["To"].ToString());
                        BindGridView(customerId, txtFromTran.SelectedDate.Value, txtToTran.SelectedDate.Value);
                        Session.Remove("tranDates");
                    }
                    else
                    {
                        if (txtFromTran.SelectedDate != null || txtToTran.SelectedDate != null)
                            // txtFromTran.SelectedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                            // if (txtToTran.SelectedDate == null)
                            //txtToTran.SelectedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                            //BindGridView(customerId, txtFromTran.SelectedDate.Value, txtToTran.SelectedDate.Value);
                            BindGridView(customerId, txtFromTran.SelectedDate.Value, txtToTran.SelectedDate.Value);
                    }

                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EquityTransactionsView.ascx:Page_Load()");
                object[] objects = new object[3];
                objects[0] = customerId;
                objects[1] = customerVo;
                objects[2] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();

            ddlPortfolio.SelectedValue = portfolioId.ToString();

        }

        private void BindGridView(int customerId, DateTime from, DateTime to)
        {
            string type, mode;
            RadComboBox radComboBoxTradeDate = new RadComboBox();
            //DataRow drTradedate;

            DataSet dsExange = new DataSet(); ;
            DataTable dtEquityTransactions = new DataTable();
            DataTable dtTradeDate = new DataTable();

            Dictionary<string, string> genDictTranType = new Dictionary<string, string>();
            Dictionary<string, string> genDictExchange = new Dictionary<string, string>();
            Dictionary<string, string> genDictTradeDate = new Dictionary<string, string>();

            try
            {

                equityTransactionList = customerTransactionBo.GetEquityTransactions(customerId, portfolioId, from, to);

                if (equityTransactionList != null)
                {
                    // lblMsg.Visible = false;
                    ErrorMessage.Visible = false;
                    gvEquityTransactions.Visible = true;
                    dtEquityTransactions.Columns.Add("TransactionId");
                    dtEquityTransactions.Columns.Add("TradeAccountNum");
                    dtEquityTransactions.Columns.Add("Scheme Name");
                    dtEquityTransactions.Columns.Add("Transaction Type");
                    dtEquityTransactions.Columns.Add("Exchange");
                    dtEquityTransactions.Columns.Add("TradeDate", typeof(DateTime));
                    dtEquityTransactions.Columns.Add("Rate", typeof(double));
                    dtEquityTransactions.Columns.Add("Quantity", typeof(double));
                    dtEquityTransactions.Columns.Add("Brokerage", typeof(double));
                    dtEquityTransactions.Columns.Add("TradeTotal", typeof(double));
                    dtEquityTransactions.Columns.Add("OtherCharges", typeof(double));
                    dtEquityTransactions.Columns.Add("TransactionStatus");

                    //dtTradeDate.Columns.Add("TransactionId");
                    //dtTradeDate.Columns.Add("TradeDate");

                    DataRow drEquityTransaction;
                    DataRow drTradedate;

                    for (int i = 0; i < equityTransactionList.Count; i++)
                    {
                        drEquityTransaction = dtEquityTransactions.NewRow();
                        drTradedate = dtTradeDate.NewRow();

                        equityTransactionVo = new EQTransactionVo();
                        equityTransactionVo = equityTransactionList[i];
                        drEquityTransaction["TransactionId"] = equityTransactionVo.TransactionId.ToString();
                        drEquityTransaction["TradeAccountNum"] = equityTransactionVo.TradeAccountNum.ToString();
                        drEquityTransaction["Scheme Name"] = equityTransactionVo.ScripName.ToString();

                        if (equityTransactionVo.TradeType.ToString() == "D")
                        {
                            //drEquityTransaction[2] = "Delivery";
                            mode = "Delivery";
                        }
                        else
                        {
                            //drEquityTransaction[2] = "Speculation";
                            mode = "Speculation";
                        }
                        drEquityTransaction["Transaction Type"] = equityTransactionVo.TransactionType + "/" + mode;
                        drEquityTransaction["Exchange"] = equityTransactionVo.Exchange.ToString();
                        //  dsExange.Tables[0].Rows[i][0] = equityTransactionVo.Exchange;
                        drEquityTransaction["TradeDate"] = equityTransactionVo.TradeDate.ToShortDateString().ToString();
                        drEquityTransaction["Rate"] = decimal.Parse(equityTransactionVo.Rate.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drEquityTransaction["Quantity"] = equityTransactionVo.Quantity.ToString("f0");
                        drEquityTransaction["Brokerage"] = (decimal.Parse(equityTransactionVo.Brokerage.ToString()) * decimal.Parse(equityTransactionVo.Quantity.ToString("f0").ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drEquityTransaction["TradeTotal"] = decimal.Parse(equityTransactionVo.TradeTotal.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drEquityTransaction["OtherCharges"] = (decimal.Parse(equityTransactionVo.OtherCharges.ToString()) * decimal.Parse(equityTransactionVo.Quantity.ToString("f0").ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drEquityTransaction["TransactionStatus"] = equityTransactionVo.TransactionStatus.ToString();


                        dtEquityTransactions.Rows.Add(drEquityTransaction);


                    }


                    gvEquityTransactions.DataSource = dtEquityTransactions;
                    gvEquityTransactions.DataBind();
                    gvEquityTransactions.Visible = true;
                    Panel1.Visible = true;
                    //RadComboBoxItem rci = new RadComboBoxItem();




                    if (Cache["EquityTransactionsDetails" + customerPortfolioVo.CustomerId.ToString()] == null)
                    {
                        Cache.Insert("EquityTransactionsDetails" + customerPortfolioVo.CustomerId.ToString(), dtEquityTransactions);
                    }
                    else
                    {
                        Cache.Remove("EquityTransactionsDetails" + customerPortfolioVo.CustomerId.ToString());
                        Cache.Insert("EquityTransactionsDetails" + customerPortfolioVo.CustomerId.ToString(), dtEquityTransactions);
                    }
                    imgBtnExport.Visible = true;
                }
                else
                {
                    imgBtnExport.Visible = false;
                    ErrorMessage.Visible = true;
                    gvEquityTransactions.DataSource = null;
                    Panel1.Visible = false;
                    gvEquityTransactions.Visible = false;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EquityTransactionsView.ascx:BindGridView()");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = equityTransactionList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


        protected void rcbTradeDate_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataTable dt = (DataTable)Session["equityDetails"];
            RadComboBox rcbTradeDate = (RadComboBox)o;
            GridFilteringItem filterItem = rcbTradeDate.NamingContainer as GridFilteringItem;
            RadComboBox rcbTradeDateFilter = filterItem.FindControl("rcbTradeDate") as RadComboBox;
            string statusOrderCode = rcbTradeDateFilter.SelectedValue;



        }

        public void imgBtnGvEquityTransactions_OnClick(object sender, ImageClickEventArgs e)
        {
            gvEquityTransactions.ExportSettings.OpenInNewWindow = true;
            gvEquityTransactions.ExportSettings.IgnorePaging = true;
            gvEquityTransactions.ExportSettings.HideStructureColumns = true;
            gvEquityTransactions.ExportSettings.ExportOnlyData = true;
            gvEquityTransactions.ExportSettings.FileName = "Equity Transaction Details";
            gvEquityTransactions.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvEquityTransactions.MasterTableView.ExportToExcel();
        }



        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            BindGridView(customerId, txtFromTran.SelectedDate.Value, txtToTran.SelectedDate.Value);
        }


        private SortDirection GridSortDirection
        {
            get
            {
                if (ViewState["GridSortDirection"] == null)
                    ViewState["GridSortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["GridSortDirection"];
            }
            set { ViewState["GridSortDirection"] = value; }
        }

        protected void btnViewTran_Click(object sender, EventArgs e)
        {
            if (txtFromTran.SelectedDate != null || txtToTran.SelectedDate != null)
            {
                //if (ddlPortfolio.SelectedIndex != 0)
                //{
                //    dtFrom = txtFromTran.SelectedDate.Value;
                //    dtTo = txtToTran.SelectedDate.Value;
                //    // hdnStatus.Value = "1";
                //    BindGridView(customerId, dtFrom, dtTo);
                //}
                //else
                //{
                    portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                    Session[SessionContents.PortfolioId] = portfolioId;
                    BindGridView(customerId, txtFromTran.SelectedDate.Value, txtToTran.SelectedDate.Value);
                //}
            }
        }

        protected void gvEquityTransactions_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFromTran.SelectedDate != null || txtToTran.SelectedDate != null)
            {
                DataTable dtEquityTransactionsDetails = new DataTable();
                dtEquityTransactionsDetails = (DataTable)Cache["EquityTransactionsDetails" + customerPortfolioVo.CustomerId.ToString()];
                gvEquityTransactions.DataSource = dtEquityTransactionsDetails;
            }
            else
                Panel1.Visible = false;
        }



        protected void btnViewDetails_OnClick(object sender, EventArgs e)
        {
            LinkButton btnViewDetails = (LinkButton)sender;
            Hashtable hshTranDates;
            try
            {
                int transactionId;

                GridDataItem gvr = (GridDataItem)btnViewDetails.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;
                transactionId = int.Parse(gvEquityTransactions.MasterTableView.DataKeyValues[selectedRow - 1]["TransactionId"].ToString());
                Session["EquityTransactionVo"] = customerTransactionBo.GetEquityTransaction(transactionId);


                hshTranDates = new Hashtable();
                hshTranDates.Add("From", txtFromTran.SelectedDate.Value);
                hshTranDates.Add("To", txtToTran.SelectedDate.Value);
                Session["tranDates"] = hshTranDates;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewEquityTransaction','none');", true);
                Session["EQUITYEditValue"] = "Value";

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EquityTransactionsView.ascx:gvEquityTransactions_RowCommand()");
                object[] objects = new object[1];
                objects[0] = index;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

    }
}
