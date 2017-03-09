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
using System.Reflection;
using DaoCustomerPortfolio;

namespace WealthERP.CustomerPortfolio
{
    public partial class EquityTransactionsView : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        AdvisorVo advisorVo = new AdvisorVo();
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
         Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();


        protected void Page_Load(object sender, EventArgs e)
        {

            txtFromTran.Attributes.Add("onkeydown", "return (event.keyCode!=13);");

            txtToTran.Attributes.Add("onkeydown", "return (event.keyCode!=13);");

            try
            {
                SessionBo.CheckSession();
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                advisorVo = (AdvisorVo)Session["advisorVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                customerId = customerVo.CustomerId;
                userVo = (UserVo)Session["userVo"];
                rmVo = (RMVo)Session["rmVo"];
                if (Request.QueryString["CurrentPageIndex"] != null)
                {
                    int CustomerId = int.Parse(customerVo.CustomerId.ToString());
                    int PortfolioId = int.Parse(Request.QueryString["portfolioId"].ToString());
                    ddlPortfolio.SelectedValue = PortfolioId.ToString();
                    DateTime From = DateTime.Parse(Request.QueryString["FrmDt"].ToString());
                    txtFromTran.SelectedDate = From;
                    DateTime To = DateTime.Parse(Request.QueryString["ToDt"].ToString());
                    txtToTran.SelectedDate = To;
                    string Currency = Request.QueryString["Currency"].ToString();
                    ddl_type.SelectedValue = Currency.ToString();
                    BindGridView(customerId, txtFromTran.SelectedDate.Value, txtToTran.SelectedDate.Value);
                    gvEquityTransactions.CurrentPageIndex = int.Parse(Request.QueryString["CurrentPageIndex"]);
                    gvEquityTransactions.Rebind();
                    PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                    // make collection editable
                    isreadonly.SetValue(this.Request.QueryString, false, null);
                    Request.QueryString.Clear();
                }

                if (!IsPostBack)
                {
                    if (Request.QueryString["From"] != null && Request.QueryString["To"] != null)
                    {
                        BindPortfolioDropDown();
                        int CustomerId = int.Parse(Request.QueryString["CustomerId"].ToString());
                        int PortfolioId = int.Parse(Request.QueryString["PortfolioId"].ToString());
                        ddlPortfolio.SelectedValue = PortfolioId.ToString();
                        DateTime From = DateTime.Parse(Request.QueryString["From"].ToString());
                        txtFromTran.SelectedDate = From;
                        DateTime To = DateTime.Parse(Request.QueryString["To"].ToString());
                        txtToTran.SelectedDate = To;
                        string Currency = Request.QueryString["Currency"].ToString();
                        ddl_type.SelectedValue = Currency.ToString();
                        BindGridView(CustomerId, From, To);

                    }
                    else
                    {

                        ViewState["filter"] = null;
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
                                BindGridView(customerId, txtFromTran.SelectedDate.Value, txtToTran.SelectedDate.Value);
                        }
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

        public enum Constants
        {
            EQ = 0,     // explicitly specifying the enum constant values will improve performance
            MF = 1,
            EQDate = 2,
            MFDate = 3
        };

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

            DataSet dsExange = new DataSet(); ;
            DataTable dtEquityTransactions = new DataTable();
            DataTable dtTradeDate = new DataTable();

            Dictionary<string, string> genDictTranType = new Dictionary<string, string>();
            Dictionary<string, string> genDictExchange = new Dictionary<string, string>();
            Dictionary<string, string> genDictTradeDate = new Dictionary<string, string>();
            int Visible = 0;

            try
            {

                if (Request.QueryString["From"] != null && Request.QueryString["To"] != null)
                {
                    equityTransactionList = customerTransactionBo.GetEquityTransactions(customerId, int.Parse(Request.QueryString["PortfolioId"].ToString()), int.Parse(Request.QueryString["ScripCode"].ToString()), from, to, Request.QueryString["Currency"].ToString());
                    Visible = 1;
                }
                else
                {
                    equityTransactionList = customerTransactionBo.GetEquityTransactions(customerId, portfolioId, 0, from, to, (ddl_type.SelectedValue));
                }

                if (equityTransactionList != null)
                {
                    ErrorMessage.Visible = false;
                    gvEquityTransactions.Visible = true;
                    dtEquityTransactions.Columns.Add("TransactionId");
                    dtEquityTransactions.Columns.Add("InvestorName");
                    dtEquityTransactions.Columns.Add("PanNo");
                    dtEquityTransactions.Columns.Add("ManagerName");
                    dtEquityTransactions.Columns.Add("BrokerName");
                    dtEquityTransactions.Columns.Add("TradeAccountNum");
                    dtEquityTransactions.Columns.Add("DematAccountNo");
                    dtEquityTransactions.Columns.Add("SettlementNo");
                    dtEquityTransactions.Columns.Add("BillNo");
                    dtEquityTransactions.Columns.Add("Scheme Name");
                    dtEquityTransactions.Columns.Add("Scripcode");

                    dtEquityTransactions.Columns.Add("Transaction Type");
                    dtEquityTransactions.Columns.Add("Transaction Code");
                    dtEquityTransactions.Columns.Add("Dividend Status");
                    dtEquityTransactions.Columns.Add("Exchange");
                    dtEquityTransactions.Columns.Add("TradeDate", typeof(DateTime));
                    dtEquityTransactions.Columns.Add("Quantity", typeof(double));
                    dtEquityTransactions.Columns.Add("Rate", typeof(double));

                    dtEquityTransactions.Columns.Add("FORExRate");
                    dtEquityTransactions.Columns.Add("FORExDate", typeof(DateTime));

                    dtEquityTransactions.Columns.Add("Gross Trade Consideration", typeof(double));
                    dtEquityTransactions.Columns.Add("Brokerage", typeof(double));
                    dtEquityTransactions.Columns.Add("Net Rate With Brokerage", typeof(double));
                    dtEquityTransactions.Columns.Add("Net Trade Consideration With Brokerage", typeof(double));
                    dtEquityTransactions.Columns.Add("SebiTurnOverFee", typeof(double));
                    dtEquityTransactions.Columns.Add("TransactionCharges", typeof(double));
                    dtEquityTransactions.Columns.Add("StampCharges", typeof(double));
                    dtEquityTransactions.Columns.Add("STT", typeof(double));
                    dtEquityTransactions.Columns.Add("ServiceTax", typeof(double));
                    dtEquityTransactions.Columns.Add("OtherCharges", typeof(double));
                    dtEquityTransactions.Columns.Add("Difference In Amount", typeof(double));
                    dtEquityTransactions.Columns.Add("Net Rate With All Charges", typeof(double));
                    dtEquityTransactions.Columns.Add("TradeTotal", typeof(double));
                    dtEquityTransactions.Columns.Add("TransactionStatus");
                    dtEquityTransactions.Columns.Add("Purpose");
                    dtEquityTransactions.Columns.Add("Demat Charge", typeof(double));
                    dtEquityTransactions.Columns.Add("FXCurencyRate");
                    dtEquityTransactions.Columns.Add("MktClosingForexRate");
                    dtEquityTransactions.Columns.Add("CET_CreatedOn", typeof(DateTime));

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
                            mode = "Delivery";
                        }
                        else
                        {
                            mode = "Speculation";
                        }
                        if (equityTransactionVo.TransactionCode == 1 || equityTransactionVo.TransactionCode == 2)
                        {
                            drEquityTransaction["Transaction Type"] = equityTransactionVo.TransactionType + "/" + mode;
                        }
                        else
                        {
                            drEquityTransaction["Transaction Type"] = equityTransactionVo.TransactionType;
                        }
                        drEquityTransaction["Transaction Code"] = equityTransactionVo.TransactionCode.ToString();
                        drEquityTransaction["Exchange"] = equityTransactionVo.Exchange.ToString();
                        drEquityTransaction["TradeDate"] = DateTime.Parse(equityTransactionVo.TradeDate.ToString());
                        drEquityTransaction["Rate"] = decimal.Parse(equityTransactionVo.Rate.ToString()).ToString();
                        drEquityTransaction["Quantity"] = equityTransactionVo.Quantity.ToString("f0");

                        drEquityTransaction["Brokerage"] = decimal.Parse(equityTransactionVo.Brokerage.ToString());
                        drEquityTransaction["TradeTotal"] = decimal.Parse(equityTransactionVo.TradeTotal.ToString()).ToString();

                        drEquityTransaction["TransactionStatus"] = equityTransactionVo.TransactionStatus.ToString();
                        drEquityTransaction["ManagerName"] = equityTransactionVo.ManagerName.ToString();
                        drEquityTransaction["BrokerName"] = equityTransactionVo.BrokerName.ToString();
                        drEquityTransaction["DematAccountNo"] = equityTransactionVo.DpclientId.ToString();
                        drEquityTransaction["Purpose"] = equityTransactionVo.Purpose.ToString();
                        drEquityTransaction["InvestorName"] = equityTransactionVo.InvestorName.ToString();
                        drEquityTransaction["PanNo"] = equityTransactionVo.PanNo.ToString();
                        drEquityTransaction["Scripcode"] = equityTransactionVo.Scripcode.ToString();
                        drEquityTransaction["SettlementNo"] = equityTransactionVo.SettlementNo.ToString();
                        drEquityTransaction["BillNo"] = equityTransactionVo.BillNo.ToString();
                        drEquityTransaction["SebiTurnOverFee"] = equityTransactionVo.SebiTurnOverFee.ToString();
                        drEquityTransaction["TransactionCharges"] = equityTransactionVo.TransactionCharges.ToString();
                        drEquityTransaction["StampCharges"] = equityTransactionVo.StampCharges.ToString();
                        drEquityTransaction["STT"] = equityTransactionVo.STT.ToString();
                        drEquityTransaction["ServiceTax"] = equityTransactionVo.ServiceTax.ToString();
                        drEquityTransaction["Difference In Amount"] = equityTransactionVo.DifferenceInBrokerage.ToString();
                        drEquityTransaction["OtherCharges"] = equityTransactionVo.OtherCharges.ToString();
                        drEquityTransaction["Gross Trade Consideration"] = equityTransactionVo.GrossConsideration.ToString();
                        drEquityTransaction["Net Rate With Brokerage"] = equityTransactionVo.RateInclBrokerage.ToString();
                        drEquityTransaction["Net Trade Consideration With Brokerage"] = equityTransactionVo.TradeTotalIncBrokerage.ToString();
                        drEquityTransaction["Net Rate With All Charges"] = equityTransactionVo.RateIncBrokerageAllCharges.ToString();
                        if (equityTransactionVo.DividendRecieved == true)
                        {
                            drEquityTransaction["Dividend Status"] = "Received";
                        }
                        else if (equityTransactionVo.DividendRecieved == false)
                        {
                            drEquityTransaction["Dividend Status"] = "Receivable";
                        }
                        drEquityTransaction["Demat Charge"] = equityTransactionVo.DematCharge;
                        drEquityTransaction["FORExRate"] = equityTransactionVo.ForExRate;

                        drEquityTransaction["FORExRate"] = equityTransactionVo.ForExRate;
                        if (equityTransactionVo.ForExRateDate != DateTime.MinValue)
                            drEquityTransaction["FORExDate"] = equityTransactionVo.ForExRateDate;

                        drEquityTransaction["FXCurencyRate"] = equityTransactionVo.FXCurencyRate;
                        drEquityTransaction["MktClosingForexRate"] = equityTransactionVo.MktClosingForexRate;
                        drEquityTransaction["CET_CreatedOn"] = DateTime.Parse(equityTransactionVo.CreatedOn.ToString());

                        dtEquityTransactions.Rows.Add(drEquityTransaction);

                    }
                    gvEquityTransactions.DataSource = dtEquityTransactions;

                    ViewState["asd"] = dtEquityTransactions;
                    gvEquityTransactions.DataBind();
                    gvEquityTransactions.Visible = true;
                    if (Visible == 1)
                        gvEquityTransactions.MasterTableView.GetColumn("action").Visible = false;
                    Panel1.Visible = true;
                    trSelectAction.Visible = true;
                    ddlAction.SelectedIndex = 0;


                    if (!advisorVo.A_IsFamilyOffice)
                    {
                        gvEquityTransactions.MasterTableView.GetColumn("ManagerName").Visible = false;
                        ddlAction.Items[2].Enabled = false;
                        ddlAction.Items[3].Enabled = false;
                        ddlAction.Items[4].Enabled = false;
                    }
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
            if (ddl_type.SelectedValue == "$")
            {
                gvEquityTransactions.MasterTableView.GetColumn("Rate").HeaderText = "Rate($)";
                gvEquityTransactions.MasterTableView.GetColumn("Brokerage").HeaderText = "Brokerage($)";
                gvEquityTransactions.MasterTableView.GetColumn("Net Rate With Brokerage").HeaderText = "Rate Inclusive Brokerage ($)";
                gvEquityTransactions.MasterTableView.GetColumn("Net Trade Consideration With Brokerage").HeaderText = "Gross Consideration with Brokerage ($)";
                gvEquityTransactions.MasterTableView.GetColumn("SebiTurnOverFee").HeaderText = "Sebi TurnOver Fee($)";
                gvEquityTransactions.MasterTableView.GetColumn("OtherCharges").HeaderText = "OtherCharges($)";
                gvEquityTransactions.MasterTableView.GetColumn("TradeTotal").HeaderText = "TradeTotal($)";
                gvEquityTransactions.MasterTableView.GetColumn("Net Rate With All Charges").HeaderText = "Net Rate Inclusive All Charges ($)";
                gvEquityTransactions.MasterTableView.GetColumn("Gross Trade Consideration").HeaderText = "Net Consideration Of All Charges ($)";
                //----
                gvEquityTransactions.MasterTableView.GetColumn("TransactionCharges").HeaderText = "Transaction Charges ($)";
                gvEquityTransactions.MasterTableView.GetColumn("StampCharges").HeaderText = "Stamp Charges ($)";
                gvEquityTransactions.MasterTableView.GetColumn("STT").HeaderText = "STT ($)";
                gvEquityTransactions.MasterTableView.GetColumn("ServiceTax").HeaderText = "Service Tax ($)";
                gvEquityTransactions.MasterTableView.GetColumn("Demat Charge").HeaderText = "Demat Charge ($)";
                gvEquityTransactions.MasterTableView.GetColumn("Difference In Amount").HeaderText = "Difference In Amount ($)";
            }
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
                portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                Session[SessionContents.PortfolioId] = portfolioId;
                BindGridView(customerId, txtFromTran.SelectedDate.Value, txtToTran.SelectedDate.Value);
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
        protected void gvEquityTransactions_OnItemDataBound(object sender, GridCommandEventArgs e)
        {

            if (e.CommandName == RadGrid.FilterCommandName)
            {
                double BuySum = 0, SellSum = 0, Total = 0;
                Pair filterPair = (Pair)e.CommandArgument;
                string value = filterPair.Second.ToString();//accessing function name
                if (value == "SettlementNo")
                {
                    TextBox tbPattern = (e.Item as GridFilteringItem)["SettlementNo"].Controls[0] as TextBox;
                    string[] values = tbPattern.Text.Split(' ');
                    string s = values[0];
                    ViewState["filter"] = s;
                    DataTable dt = new DataTable();
                    dt = (DataTable)ViewState["asd"];
                    DataTable dt1 = dt.Select("SettlementNo like '" + s + "' ").CopyToDataTable();

                    foreach (DataRow dr in dt1.Rows)
                    {
                        if (dr["Transaction Type"].ToString() == "Buy/Delivery" || dr["Transaction Type"].ToString() == "Buy/Speculation" && Convert.ToInt16(dr["Transaction Code"]) == 1)
                        {
                            BuySum = BuySum + Convert.ToDouble(dr["TradeTotal"]);

                        }
                        if (dr["Transaction Type"].ToString() == "Sell/Delivery" || dr["Transaction Type"].ToString() == "Sell/Speculation" && Convert.ToInt16(dr["Transaction Code"]) == 2)
                        {
                            SellSum = SellSum + Convert.ToDouble(dr["TradeTotal"]);

                        }
                    }
                    Total = SellSum - BuySum;
                    Session["Total"] = Total;
                }
            }


        }

        protected void RadGrid1_DataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            double total = 0;
            string settlementno = string.Empty;
            if (ViewState["filter"] != null)
                settlementno = ViewState["filter"].ToString();
            if (settlementno != "")
            {

                if (e.Item is GridFooterItem)
                {
                    GridFooterItem footer = (GridFooterItem)e.Item;
                    if (gvEquityTransactions.Items.Count > 0)

                        total = Convert.ToDouble(Session["Total"]);
                    footer["TradeTotal"].Text = total.ToString("f2");

                }

            }
            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem header = (GridHeaderItem)e.Item;
                if (ddl_type.SelectedValue == "$")
                {
                    header["Rate"].Text = "Rate($)";
                    header["Brokerage"].Text = "Brokerage($)";
                    header["Net Rate With Brokerage"].Text = "Rate Inclusive Brokerage ($)";
                    header["Net Trade Consideration With Brokerage"].Text = "Gross Consideration with Brokerage ($)";
                    header["SebiTurnOverFee"].Text = "Sebi TurnOver Fee($)";
                    header["OtherCharges"].Text = "OtherCharges($)";
                    header["TradeTotal"].Text = "Gross Consideration ($)";
                    header["Net Rate With All Charges"].Text = "Net Rate Inclusive All Charges ($)";
                    header["Gross Trade Consideration"].Text = "Net Consideration Of All Charges ($)";
                    //----
                    header["TransactionCharges"].Text = "Transaction Charges ($)";
                    header["StampCharges"].Text = "Stamp Charges ($)";
                    header["STT"].Text = "STT ($)";
                    header["ServiceTax"].Text = "Service Tax ($)";
                    header["Demat Charge"].Text = "Demat Charge ($)";
                    header["Difference In Amount"].Text = "Difference In Amount ($)";
                }
            }

            if (ddl_type.SelectedValue.ToString() == "INR")
            {
                gvEquityTransactions.MasterTableView.GetColumn("ForExRate").Display = false;
                gvEquityTransactions.MasterTableView.GetColumn("ForExDate").Display = false;
                gvEquityTransactions.MasterTableView.GetColumn("FXCurencyRate").Display = false;
                gvEquityTransactions.MasterTableView.GetColumn("MktClosingForexRate").Display = false;


            }
            else
            {
                gvEquityTransactions.MasterTableView.GetColumn("ForExRate").Display = true;
                gvEquityTransactions.MasterTableView.GetColumn("ForExDate").Display = true;
                gvEquityTransactions.MasterTableView.GetColumn("FXCurencyRate").Display = true;
                gvEquityTransactions.MasterTableView.GetColumn("MktClosingForexRate").Display = true;
            }
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
                Session["EquityTransactionVo"] = customerTransactionBo.GetEquityTransaction(transactionId, ddl_type.SelectedValue.ToString());


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
        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int transactionId;
                RadComboBox ddlAction = (RadComboBox)sender;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;

                transactionId = int.Parse(gvEquityTransactions.MasterTableView.DataKeyValues[selectedRow - 1]["TransactionId"].ToString());
                Session["TransactionId"] = transactionId;
                Session["EquityTransactionVo"] = customerTransactionBo.GetEquityTransaction(transactionId, ddl_type.SelectedValue.ToString());
                int currentPageIndex = int.Parse(gvEquityTransactions.CurrentPageIndex.ToString());
                int portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                string FrmDt = txtFromTran.SelectedDate.ToString();
                string ToDt = txtToTran.SelectedDate.ToString();
                string Currency = ddl_type.SelectedValue.ToString();

                if (ddlAction.SelectedValue.ToString() == "Edit")
                {

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EquityManualSingleTransaction','?action=Edit &CurrentPageIndex=" + currentPageIndex + "&FrmDt=" + FrmDt.ToString() + "&ToDt=" + ToDt + "&Currency=" + Currency + "&PortfolioId=" + portfolioId + "');", true);
                }
                if (ddlAction.SelectedValue.ToString() == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EquityManualSingleTransaction','action=View &CurrentPageIndex=" + currentPageIndex + "&FrmDt=" + FrmDt.ToString() + "&ToDt=" + ToDt + "&Currency=" + Currency + "&PortfolioId=" + portfolioId + "');", true);
                }
                if (ddlAction.SelectedValue.ToString() == "Delete")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "ShowAlertToDelete();", true);
                }

            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerMFFolioView.ascx:ddlAction_OnSelectedIndexChange()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }







        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
            string HiddenVal = hdnStatusValue.Value;
            int transactionId = 0;
            if (Session["TransactionId"] != null)
                transactionId = int.Parse(Session["TransactionId"].ToString());
            if (HiddenVal == "1")
            {
                if (hddeletiontype.Value == "Bulk")
                {
                    string transid = Session["TransId"].ToString();
                    customerTransactionBo.BulkEqTransactionDeletion(transid);

                    ddlAction.SelectedIndex = 0;

                }
                else
                {
                    customerTransactionBo.DeleteEquityTransaction(transactionId);
                }

            }
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EquityTransactionsView','none');", true);
        }
        protected void ddlActionSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
            string transid = string.Empty;

            foreach (GridDataItem gvr in this.gvEquityTransactions.Items)
            {
                if (((CheckBox)gvr.FindControl("chk_id")).Checked == true)
                {
                    i = i + 1;
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to process!');", true);
                ddlAction.SelectedIndex = 0;
            }
            else
            {
                foreach (GridDataItem gvr in this.gvEquityTransactions.Items)
                {
                    if (((CheckBox)gvr.FindControl("chk_id")).Checked == true)
                    {
                        transid += Convert.ToString(gvEquityTransactions.MasterTableView.DataKeyValues[gvr.ItemIndex]["TransactionId"]) + "~";
                    }

                }
                if (ddlAction.SelectedValue == "Del")
                {
                    hddeletiontype.Value = "Bulk";
                    Session["TransId"] = transid;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "ShowAlertToDelete();", true);


                }
                else if (ddlAction.SelectedValue == "MapToCi")
                {
                    string Type = "Ci";
                    customerTransactionBo.MapEQTransactionToCIAndPI(transid, Type);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EquityTransactionsView','none');", true);
                    ddlAction.SelectedIndex = 0;
                }
                else if (ddlAction.SelectedValue == "MapToPi")
                {
                    string Type = "Pi";
                    customerTransactionBo.MapEQTransactionToCIAndPI(transid, Type);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EquityTransactionsView','none');", true);
                    ddlAction.SelectedIndex = 0;
                }
                else if (ddlAction.SelectedValue == "MapToManager")
                {
                    customerTransactionBo.MapEQToManager(transid);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EquityTransactionsView','none');", true);
                    ddlAction.SelectedIndex = 0;
                }
            }
        }


        public List<EQTransactionVo> GetEquityTransactions(int customerId, int portfolioId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            List<EQTransactionVo> eqTransactionsList = new List<EQTransactionVo>();
            try
            {
                eqTransactionsList = customerTransactionDao.GetEquityTransactions(customerId, portfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetEquityTransactions()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqTransactionsList;
        }







        protected void ddlDateRange_OnCheckedChanged(object sender, EventArgs e)
        {
              if (ddlDateRange.SelectedValue == "DateRange")
             {
            //  if ((DateTime.Now.Month <= 3))
            //txtPickDate.SelectedDate = DateTime.Parse("1/04/" + ((DateTime.Now.Year) - 1));
            // else
            // txtPickDate.SelectedDate = DateTime.Parse("1/04/" + (DateTime.Now.Year));

              txtToTran.SelectedDate = DateTime.Now;
              tdlblDate.Visible = true;
              tdToDate.Visible = true;
              lblDate.Text = "To:";
              Panel1.Visible = false;
             }
              else
              {
           // txtPickDate.SelectedDate = DateTime.Parse(genDict[Constants.EQDate.ToString()].ToString());
            tdlblDate.Visible = false;
             tdToDate.Visible = false;
            lblDate.Text = "As On:";
            Panel1.Visible = false;
             }
              }

        }
    }
