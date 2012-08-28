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
        bool GridViewCultureFlag = true; //For gridview currency culture export to excel

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";

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
                    rbtnPickDate.Checked = true;
                    rbtnPickPeriod.Checked = false;
                    trRange.Visible = true;
                    trPeriod.Visible = false;
                    tdGroupHead.Visible = false;
                    lblGroupHead.Visible = false;
                    txtParentCustomer.Visible = false;
                    rfvGroupHead.Visible = false;
                    BindLastTradeDate();
                    BindGrid(DateTime.Parse(txtFromDate.SelectedDate.ToString()), DateTime.Parse(txtToDate.SelectedDate.ToString()));

                    trMessage.Visible = false;
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
            txtFromDate.SelectedDate = DateTime.Parse((ds.Tables[0].Rows[0][0].ToString()));
            txtToDate.SelectedDate = DateTime.Parse((ds.Tables[0].Rows[0][0].ToString()));

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

        private void BindGroupHead()
        {

        }
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
            BindGrid(currentPage, convertedFromDate, convertedToDate);
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

                if (ds.Tables[0].Rows.Count > 0)
                {
                    trMessage.Visible = false;
                    gvMFTransactions.DataSource = ds;
                    gvMFTransactions.DataBind();
                    gvMFTransactions.Visible = true;

                    if (Cache["EquityTransactionDetails" + advisorVo.advisorId.ToString()] == null)
                    {
                        Cache.Insert("EquityTransactionDetails" + advisorVo.advisorId.ToString(), ds);
                    }
                    else
                    {
                        Cache.Remove("EquityTransactionDetails" + advisorVo.advisorId.ToString());
                        Cache.Insert("EquityTransactionDetails" + advisorVo.advisorId.ToString(), ds);
                    }
                }
                else
                {
                    gvMFTransactions.DataSource = null;
                    gvMFTransactions.DataBind();
                    
                    trMessage.Visible = true;
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


        protected void gvMFTransactions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double otherCharges = 0;
                double brokerage = 0;
                double grossPrice = 0;
                double quantity = 0;

                double rate = 0;
                if (e.Row.Cells[6].Text != string.Empty)
                    rate = Convert.ToDouble(e.Row.Cells[6].Text);
                double STT = 0;

                if (e.Row.Cells[8].Text != string.Empty)
                    brokerage = Convert.ToDouble(e.Row.Cells[8].Text);
                if (e.Row.Cells[7].Text != string.Empty)
                    quantity = Convert.ToDouble(e.Row.Cells[7].Text);

                if (e.Row.Cells[9].Text != string.Empty)
                    otherCharges = Convert.ToDouble(e.Row.Cells[9].Text);
                if (e.Row.Cells[10].Text != string.Empty)
                    STT = Convert.ToDouble(e.Row.Cells[10].Text);

                grossPrice = ((rate) * quantity) + STT + brokerage + otherCharges;

                totalGrossPrice += grossPrice;

                e.Row.Cells[11].Text = grossPrice.ToString();
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[7].Text = "Total ";
                if (GridViewCultureFlag == true)
                    e.Row.Cells[8].Text = double.Parse(totalBrokerage.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    e.Row.Cells[8].Text = double.Parse(totalBrokerage.ToString()).ToString();
                e.Row.Cells[8].Attributes.Add("align", "Right");

                if (GridViewCultureFlag == true)
                    e.Row.Cells[9].Text = double.Parse(totalOtherCharges.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    e.Row.Cells[9].Text = double.Parse(totalOtherCharges.ToString()).ToString();
                e.Row.Cells[9].Attributes.Add("align", "Right");

                if (GridViewCultureFlag == true)
                    e.Row.Cells[10].Text = double.Parse(totalSTT.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    e.Row.Cells[10].Text = double.Parse(totalSTT.ToString()).ToString();
                e.Row.Cells[10].Attributes.Add("align", "Right");

                if (GridViewCultureFlag == true)
                    e.Row.Cells[11].Text = double.Parse(totalGrossPrice.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    e.Row.Cells[11].Text = double.Parse(totalGrossPrice.ToString()).ToString();
                e.Row.Cells[11].Attributes.Add("align", "Right");

            }
        }

        protected void gvMFTransactions_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dtProcessLogDetails = new DataSet();
            dtProcessLogDetails = (DataSet)Cache["EquityTransactionDetails" + advisorVo.advisorId.ToString()];
            gvMFTransactions.DataSource = dtProcessLogDetails;
            if (gvMFTransactions.DataSource != null)
                gvMFTransactions.Visible = true;
            else
                gvMFTransactions.Visible = false;
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