using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCommon;
using VoUser;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCustomerPortfolio;
using System.Globalization;
using BoCustomerProfiling;
using WealthERP.Base;
using VoCustomerPortfolio;
using System.IO;
using VoAdvisorProfiling;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

namespace WealthERP.CustomerPortfolio
{
    public partial class RMMultipleEqTransactionsView : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        string userType;
        string path = string.Empty;
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        CustomerBo customerBo = new CustomerBo();
        int GroupHead = 0;
        List<MFTransactionVo> mfTransactionList = null;
        MFTransactionVo mfTransactionVo = new MFTransactionVo();
        DateTime dtTo = new DateTime();
        DateBo dtBo = new DateBo();
        DateTime dtFrom = new DateTime();
        static DateTime convertedFromDate = new DateTime();
        static DateTime convertedToDate = new DateTime();
        double totalBrokerage = 0;
        double totalOtherCharges = 0;
        double totalSTT = 0;
        double totalGrossPrice = 0;
        AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();
        bool GridViewCultureFlag = true; //For gridview currency culture export to excel
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //if (BindGroupHead() == "IE7 Compatibility View")
            //{
            //    HtmlMeta force = new HtmlMeta();
            //    force.HttpEquiv = "X-UA-Compatible";
            //    force.Content = "IE=edge";
              

            //    //Header.GetType.AddAt(0, force);
            //}
            try
            {
                
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                advisorPreferenceVo = (AdvisorPreferenceVo)(Session["AdvisorPreferenceVo"]);
                
               // PageSize = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"].ToString();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                rmVo = (RMVo)Session[SessionContents.RmVo];

                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    txtParentCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                {
                    txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                }

               
                if (!IsPostBack)
                {

                    if (Cache["EquityTransactionDetails" + advisorVo.advisorId.ToString()] != null)
                        Cache.Remove("EquityTransactionDetails" + advisorVo.advisorId.ToString());
                    rbtnPickDate.Checked = true;
                    rbtnPickPeriod.Checked = false;
                    trRange.Visible = true;
                    trPeriod.Visible = false;
                    tdGroupHead.Visible = false;
                    lblGroupHead.Visible = false;
                    txtParentCustomer.Visible = false;
                    rfvGroupHead.Visible = false;
                    Msgerror.Visible = false;
                    gvMFTransactions.Visible = false;
                    BindLastTradeDate();
                    txtFromDate.SelectedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    txtToDate.SelectedDate = DateTime.Parse(DateTime.Now.ToShortDateString());

                //    if (txtFromDate.SelectedDate != null || txtToDate.SelectedDate != null)
                //        BindGrid(txtFromDate.SelectedDate.Value, txtToDate.SelectedDate.Value);
                //    else
                //        //  Panel2.Visible = false;
                //        Msgerror.Visible = false;
                //   // trMessage.Visible = false;
               }
                Msgerror.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMMultipleTransactionView.ascx:PageLoad()");

                object[] objects = new object[2];
                objects[0] = advisorVo;
                objects[1] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindLastTradeDate()
        {
            DataSet ds = customerTransactionBo.GetLastTradeDate();
            //txtFromDate.SelectedDate = DateTime.Parse((ds.Tables[0].Rows[0][0].ToString()));
            //txtToDate.SelectedDate = DateTime.Parse((ds.Tables[0].Rows[0][0].ToString()));

        }

        protected void rbtnDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPickDate.Checked == true)
            {
                trRange.Visible = true;
                trPeriod.Visible = false;
            }
            else if (rbtnPickPeriod.Checked == true)
            {
                trRange.Visible = false;
                trPeriod.Visible = true;
                gvMFTransactions.Visible = false;
                BindPeriodDropDown();
            }
            gvMFTransactions.DataSource = null;
            gvMFTransactions.DataBind();

        }

        private void BindPeriodDropDown()
        {
            DataTable dtPeriod;
            dtPeriod = XMLBo.GetDatePeriod(path);

            ddlPeriod.DataSource = dtPeriod;
            ddlPeriod.DataTextField = "PeriodType";
            ddlPeriod.DataValueField = "PeriodCode";
            ddlPeriod.DataBind();
            ddlPeriod.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select a Period", "Select a Period"));
            ddlPeriod.Items.RemoveAt(15);
        }



        protected void rbtnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAll.Checked == true)
            {
                lblGroupHead.Visible = false;
                txtParentCustomer.Visible = false;
                rfvGroupHead.Visible = false;
                tdGroupHead.Visible = false;
            }
            else if (rbtnGroup.Checked == true)
            {

                lblGroupHead.Visible = true;
                txtParentCustomer.Visible = true;
                rfvGroupHead.Visible = true;
                tdGroupHead.Visible = true;

            }
            gvMFTransactions.DataSource = null;
            gvMFTransactions.DataBind();

        }

        //private string  BindGroupHead()
        //{
            //string mode = "";
            //string userAgent = Request.UserAgent; //entire UA string
            //string browser = Request.Browser.Type; //Browser name and Major Version #

            //if (userAgent.Contains("Trident/6.0")) //IE9 has this token
            //{
            //    if (browser == "IE10")
            //    {
            //        mode = "IE7 Compatibility View";
            //    }
            //    else
            //    {
            //        mode = "IE7 Standard";
            //    }
            //}
            //else if (userAgent.Contains("Trident/4.0")) //IE8 has this token
            //{
            //    if (browser == "IE10")
            //    {
            //        mode = "IE8 Compatibility View";
            //    }
            //    else
            //    {
            //        mode = "IE8 Standard";
            //    }
            //}
            //else if (!userAgent.Contains("Trident")) //Earlier versions of IE do not contain the Trident token
            //{
            //    mode = browser;
            //}

            //return mode;

       // }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            CalculateDateRange();
            BindGrid(convertedFromDate, convertedToDate);
        }

        private void CalculateDateRange()
        {
            if (rbtnPickDate.Checked)
            {
                convertedFromDate = Convert.ToDateTime(txtFromDate.SelectedDate);
                convertedToDate = Convert.ToDateTime(txtToDate.SelectedDate);
            }
            else
            {
                if (ddlPeriod.SelectedIndex != 0)
                {
                    dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue, out dtFrom, out dtTo);
                    convertedFromDate = dtFrom;
                    convertedToDate = dtTo;
                }
            }
        }

        private void BindGrid(int currentPage, DateTime convertedFromDate, DateTime convertedToDate)
        {
           //BindGrid(currentPage, convertedFromDate, convertedToDate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="convertedFromDate"></param>
        /// <param name="convertedToDate"></param>
        /// <param name="exportStaus">0 -No Export, 1- Export Current Page , 2 - Export All Pages </param>
        private void BindGrid(DateTime convertedFromDate, DateTime convertedToDate)
        {
            Dictionary<string, string> genDictTranType = new Dictionary<string, string>();
            DataSet ds = new DataSet();
            int Count = 0;
           


            string extraSearch = "";
            try
            {

                if (rbtnGroup.Checked)
                {
                    ds = customerTransactionBo.GetRMCustomerEqTransactions(rmVo.RMId, advisorVo.advisorId, int.Parse(txtParentCustomerId.Value), convertedFromDate, convertedToDate, int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()));
                }
                else
                {
                    ds = customerTransactionBo.GetRMCustomerEqTransactions(rmVo.RMId, advisorVo.advisorId, 0, convertedFromDate, convertedToDate, int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()));
                }

                 DataTable dtTransactions = new DataTable();
                dtTransactions = ds.Tables[0];
                if (dtTransactions.Rows.Count > 0)
                {
                    Msgerror.Visible = false;
                   

                    DataTable dtEQTransactions = new DataTable();

                    dtEQTransactions.Columns.Add("TransactionId");
                    dtEQTransactions.Columns.Add("Customer Name");
                    dtEQTransactions.Columns.Add("Scrip");
                    dtEQTransactions.Columns.Add("TradeAccNumber");
                    dtEQTransactions.Columns.Add("Date",typeof(DateTime));
                    dtEQTransactions.Columns.Add("Type");
                    dtEQTransactions.Columns.Add("Rate",typeof(double));
                    dtEQTransactions.Columns.Add("Quantity", typeof(double));
                    dtEQTransactions.Columns.Add("Brokerage", typeof(double));
                    dtEQTransactions.Columns.Add("OtherCharges", typeof(double));
                    dtEQTransactions.Columns.Add("STT", typeof(double));
                    dtEQTransactions.Columns.Add("GrossPrice", typeof(double));
                    dtEQTransactions.Columns.Add("Speculative Or Delivery");
                    dtEQTransactions.Columns.Add("Portfolio Name");
                    dtEQTransactions.Columns.Add("TransactionStatus");

                    DataRow drEQTransaction;

                    for (int i = 0; i < dtTransactions.Rows.Count; i++)
                    {

                        drEQTransaction = dtEQTransactions.NewRow();
                        drEQTransaction["TransactionId"] = dtTransactions.Rows[i]["CET_EqTransId"].ToString();
                        drEQTransaction["Customer Name"] = dtTransactions.Rows[i]["Name"].ToString();
                        drEQTransaction["Scrip"] = dtTransactions.Rows[i]["PEM_CompanyName"].ToString();
                        drEQTransaction["TradeAccNumber"] = dtTransactions.Rows[i]["CETA_TradeAccountNum"].ToString();
                        drEQTransaction["Type"] = dtTransactions.Rows[i]["WETT_TransactionTypeName"].ToString();
                        if (dtTransactions.Rows[i]["CET_TradeDate"] != DBNull.Value)
                        {
                            //drEQTransaction["Date"] = dtTransactions.Rows[i]["CET_TradeDate"].ToString();
                            drEQTransaction["Date"] = Convert.ToDateTime(dtTransactions.Rows[i]["CET_TradeDate"].ToString()).ToShortDateString();
                        }

                        drEQTransaction["Rate"] = dtTransactions.Rows[i]["CET_Rate"].ToString();
                        if (dtTransactions.Rows[i]["CET_Quantity"] != DBNull.Value)
                            drEQTransaction["Quantity"] = Convert.ToInt32(dtTransactions.Rows[i]["CET_Quantity"]);
                        drEQTransaction["Brokerage"] = dtTransactions.Rows[i]["CET_Brokerage"].ToString();
                        drEQTransaction["OtherCharges"] = dtTransactions.Rows[i]["CET_OtherCharges"].ToString();
                        drEQTransaction["STT"] = dtTransactions.Rows[i]["CET_STT"].ToString();
                        //if (dtTransactions.Rows[i]["XES_SourceCode"] != DBNull.Value)
                        //    drEQTransaction["GrossPrice"] = Convert.ToDouble( dtTransactions.Rows[i]["XES_SourceCode"].ToString());
                        drEQTransaction["Speculative Or Delivery"] = dtTransactions.Rows[i]["CET_IsSpeculative"].ToString();
                        drEQTransaction["Portfolio Name"] = dtTransactions.Rows[i]["CP_PortfolioName"].ToString();

                        dtEQTransactions.Rows.Add(drEQTransaction);
                        totalBrokerage += Convert.ToDouble(drEQTransaction["Brokerage"].ToString());
                        totalOtherCharges += Convert.ToDouble(drEQTransaction["OtherCharges"].ToString());
                        totalSTT += Convert.ToDouble(drEQTransaction["STT"].ToString());
                        drEQTransaction["TransactionStatus"] = dtTransactions.Rows[i]["WTS_TransactionStatus"].ToString();

                    }

                    gvMFTransactions.DataSource = dtEQTransactions;
                    gvMFTransactions.PageSize = advisorPreferenceVo.GridPageSize;
                    gvMFTransactions.Visible = true;
                    gvMFTransactions.DataBind();
                    Msgerror.Visible = false;

                    if (Cache["EquityTransactionDetails" + advisorVo.advisorId.ToString()] == null)
                    {
                        Cache.Insert("EquityTransactionDetails" + advisorVo.advisorId.ToString(), dtEQTransactions);
                    }
                    else
                    {
                        Cache.Remove("EquityTransactionDetails" + advisorVo.advisorId.ToString());
                        Cache.Insert("EquityTransactionDetails" + advisorVo.advisorId.ToString(), dtEQTransactions);
                    }
                    imgBtnrgHoldings.Visible = true;
                    //Panel2.Visible = true;
                }
                else
                {
                    gvMFTransactions.DataSource = null;
                    gvMFTransactions.DataBind();
                    imgBtnrgHoldings.Visible = false;
                    Msgerror.Visible = true;
                   // trMessage.Visible = true;
                   // Panel2.Visible = false;
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

                FunctionInfo.Add("Method", "RMMultipleEqTransactionsView.ascx.cs:BindGrid()");

                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


        protected void gvMFTransactions_RowDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    double otherCharges = 0;
            //    double brokerage = 0;
            //    double grossPrice = 0;
            //    double quantity = 0;

            //    double rate = 0;
            //    GridDataItem dataItem = e.Item as GridDataItem;
            //    if (dataItem["Rate"].Text != string.Empty)
            //        rate = Convert.ToDouble(dataItem["Rate"].Text.ToString().Trim());
            //    double STT = 0;

            //    if (dataItem["Brokerage"].Text != string.Empty)
            //        brokerage = Convert.ToDouble(dataItem["Brokerage"].Text.ToString().Trim());
            //    if (dataItem["Quantity"].Text != string.Empty)
            //        quantity = Convert.ToDouble(dataItem["Quantity"].Text.ToString().Trim());

            //    if (dataItem["OtherCharges"].Text != string.Empty)
            //        otherCharges = Convert.ToDouble(dataItem["OtherCharges"].Text.ToString().Trim());
            //    if (dataItem["STT"].Text != string.Empty)
            //        STT = Convert.ToDouble(dataItem["STT"].Text.ToString().Trim());

            //    grossPrice = ((rate) * quantity) + STT + brokerage + otherCharges;

            //    totalGrossPrice += grossPrice;

            //    dataItem["GrossPrice"].Text = grossPrice.ToString();
            //    //dataItem["GrossPrice"]. = Convert.ToDouble(dataItem["GrossPrice"].Text.ToString().Trim());
            //}
            //if (e.Item is GridFooterItem)
            //{
            //    GridFooterItem Footeritem = e.Item as GridFooterItem;
            //    Footeritem["GrossPrice"].Text = totalGrossPrice.ToString();
            //}
        }

        protected void gvMFTransactions_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtProcessLogDetails = new DataTable();

            GridFilteringItem FilterItem = gvMFTransactions.MasterTableView.GetItems(GridItemType.FilteringItem)[0] as GridFilteringItem;
            //DateTime dt = Convert.ToDateTime(FilterItem.OwnerTableView.FilterExpression.Split());

            string srt = FilterItem.OwnerTableView.FilterExpression;
            if (!string.IsNullOrEmpty(srt))
            {
                string[] dt = srt.Split('\'');
                string date = dt[1];
                ViewState["date1"] = date;

               
            }

            if(srt=="")
            {
                 RadDatePicker rdp = new RadDatePicker();
                rdp = (RadDatePicker)FilterItem.FindControl("Datepk");
                if (ViewState["date1"] != null)
                    rdp.SelectedDate = null;
                ViewState["date1"] = null;
            }

           dtProcessLogDetails = (DataTable)Cache["EquityTransactionDetails" + advisorVo.advisorId.ToString()];
            if (dtProcessLogDetails != null)
            {
                gvMFTransactions.DataSource = dtProcessLogDetails;
            }
 
            //FilterItem.OwnerTableView.FilterExpression =ViewState["date1"].ToString();
            //GridColumn column = gvMFTransactions.MasterTableView.GetColumnSafe("Date");
            //column.CurrentFilterValue = ViewState["date1"].ToString();
            //column.FilterControlAltText=  ViewState["date1"].ToString();
            
        }

        protected void gvMFTransactions_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem)
            {
                GridFilteringItem item = (GridFilteringItem)e.Item;
                

                //TextBox txtFilter = item.Controls[4];
                GridFilteringItem FilterItem = gvMFTransactions.MasterTableView.GetItems(GridItemType.FilteringItem)[0] as GridFilteringItem;

                RadDatePicker rdp = new RadDatePicker();
                rdp=(RadDatePicker)FilterItem.FindControl("Datepk");
                if (ViewState["date1"]!=null)
                rdp.SelectedDate =Convert.ToDateTime(ViewState["date1"].ToString());
            }
        } 


        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvMFTransactions.ExportSettings.OpenInNewWindow = true;
            gvMFTransactions.ExportSettings.IgnorePaging = true;
            gvMFTransactions.ExportSettings.HideStructureColumns = true;
            gvMFTransactions.ExportSettings.ExportOnlyData = true;
            gvMFTransactions.ExportSettings.FileName = "Equity Transaction Details";
            gvMFTransactions.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvMFTransactions.MasterTableView.ExportToExcel();
        }
    }
}