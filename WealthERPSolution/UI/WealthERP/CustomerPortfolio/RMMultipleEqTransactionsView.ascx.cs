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
                txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();

                if (!IsPostBack)
                {
                    hdnSchemeSearch.Value = string.Empty;
                    hdnTranType.Value = string.Empty;
                    hdnCustomerNameSearch.Value = string.Empty;
                    // hdnFolioNumber.Value = string.Empty;
                    rbtnPickDate.Checked = true;
                    rbtnPickPeriod.Checked = false;
                    trRange.Visible = true;
                    trPeriod.Visible = false;
                    tdGroupHead.Visible = false;
                    lblGroupHead.Visible = false;
                    txtParentCustomer.Visible = false;
                    rfvGroupHead.Visible = false;
                    BindLastTradeDate();
                    BindGrid(mypager.CurrentPage, DateTime.Parse(txtFromDate.SelectedDate.ToString()), DateTime.Parse(txtToDate.SelectedDate.ToString()));

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
            lblCurrentPage.Text = string.Empty;
            lblTotalRows.Text = string.Empty;
            mypager.Visible = false;
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
            lblCurrentPage.Text = string.Empty;
            lblTotalRows.Text = string.Empty;
            mypager.Visible = false;
        }

        private void BindGroupHead()
        {

        }
        protected void btnGo_Click(object sender, EventArgs e)
        {

            hdnSchemeSearch.Value = string.Empty;
            hdnTranType.Value = string.Empty;
            hdnCustomerNameSearch.Value = string.Empty;
            //hdnFolioNumber.Value = string.Empty;
            CalculateDateRange();
            BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
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
            BindGrid(currentPage, convertedFromDate, convertedToDate, 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="convertedFromDate"></param>
        /// <param name="convertedToDate"></param>
        /// <param name="exportStaus">0 -No Export, 1- Export Current Page , 2 - Export All Pages </param>
        private void BindGrid(int currentPage, DateTime convertedFromDate, DateTime convertedToDate, int exportFlag)
        {
            Dictionary<string, string> genDictTranType = new Dictionary<string, string>();
            DataSet ds = new DataSet();
            int Count = 0;


            string extraSearch = "";
            try
            {

                if (rbtnGroup.Checked)
                {
                    //rmVo.RMId = 1037;
                    ds = customerTransactionBo.GetRMCustomerEqTransactions(out Count, currentPage, rmVo.RMId, int.Parse(txtParentCustomerId.Value), convertedFromDate, convertedToDate, int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()), hdnCustomerNameSearch.Value.Trim(), hdnSchemeSearch.Value.Trim(), hdnTranType.Value.Trim(), out genDictTranType, extraSearch, exportFlag);
                    hdnRecordCount.Value = lblTotalRows.Text = Count.ToString();
                }
                else
                {
                    //rmVo.RMId = 1037;
                    ds = customerTransactionBo.GetRMCustomerEqTransactions(out Count, currentPage, rmVo.RMId, 0, convertedFromDate, convertedToDate, int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()), hdnCustomerNameSearch.Value.Trim(), hdnSchemeSearch.Value.Trim(), hdnTranType.Value.Trim(), out genDictTranType, extraSearch, exportFlag);
                    hdnRecordCount.Value = lblTotalRows.Text = Count.ToString();
                }
                DataTable dtTransactions = new DataTable();
                dtTransactions = ds.Tables[0];
                if (dtTransactions.Rows.Count > 0)
                {
                    trMessage.Visible = false;
                    lblCurrentPage.Visible = true;
                    trPager.Visible = true;
                    lblTotalRows.Visible = true;

                    DataTable dtEQTransactions = new DataTable();

                    dtEQTransactions.Columns.Add("TransactionId");
                    dtEQTransactions.Columns.Add("Customer Name");
                    dtEQTransactions.Columns.Add("Scrip");
                    dtEQTransactions.Columns.Add("TradeAccNumber");
                    dtEQTransactions.Columns.Add("Type");
                    dtEQTransactions.Columns.Add("Date");
                    dtEQTransactions.Columns.Add("Rate");
                    dtEQTransactions.Columns.Add("Quantity");
                    dtEQTransactions.Columns.Add("Brokerage");
                    dtEQTransactions.Columns.Add("OtherCharges");
                    dtEQTransactions.Columns.Add("STT");
                    dtEQTransactions.Columns.Add("Gross Price");
                    dtEQTransactions.Columns.Add("Speculative Or Delivery");
                    dtEQTransactions.Columns.Add("Portfolio Name");

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
                        drEQTransaction["Gross Price"] = dtTransactions.Rows[i]["XES_SourceCode"].ToString();
                        drEQTransaction["Speculative Or Delivery"] = dtTransactions.Rows[i]["CET_IsSpeculative"].ToString();
                        drEQTransaction["Portfolio Name"] = dtTransactions.Rows[i]["CP_PortfolioName"].ToString();

                        dtEQTransactions.Rows.Add(drEQTransaction);
                        totalBrokerage += Convert.ToDouble(drEQTransaction["Brokerage"].ToString());
                        totalOtherCharges += Convert.ToDouble(drEQTransaction["OtherCharges"].ToString());
                        totalSTT += Convert.ToDouble(drEQTransaction["STT"].ToString());

                    }

                    gvMFTransactions.DataSource = dtEQTransactions;
                    gvMFTransactions.DataBind();
                    gvMFTransactions.Visible = true;

                    if (genDictTranType.Count > 0)
                    {
                        DropDownList ddlTranType = GetTranTypeDDL();
                        if (ddlTranType != null)
                        {
                            ddlTranType.DataSource = genDictTranType;
                            ddlTranType.DataTextField = "Key";
                            ddlTranType.DataValueField = "Value";
                            ddlTranType.DataBind();
                            ddlTranType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }

                        if (hdnTranType.Value != "")
                        {
                            ddlTranType.SelectedValue = hdnTranType.Value.ToString().Trim();
                        }
                    }


                    GetPageCount();
                    mypager.Visible = true;
                }
                else
                {
                    gvMFTransactions.DataSource = null;
                    gvMFTransactions.DataBind();
                    hdnRecordCount.Value = "0";
                    lblCurrentPage.Visible = false;
                    trPager.Visible = false;
                    lblTotalRows.Visible = false;
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
                // objects[0] = userVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private DropDownList GetTranTypeDDL()
        {

            DropDownList ddl = new DropDownList();
            if (gvMFTransactions.HeaderRow != null)
            {
                if ((DropDownList)gvMFTransactions.HeaderRow.FindControl("ddlTranType") != null)
                {
                    ddl = (DropDownList)gvMFTransactions.HeaderRow.FindControl("ddlTranType");
                }
            }
            else
                ddl = null;

            return ddl;

        }
        protected override void OnInit(EventArgs e)
        {
            try
            {
                ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
                mypager.EnableViewState = true;
                base.OnInit(e);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RMMultipleTransactionView.ascx.cs:OnInit()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }


        protected void ddlTranType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlTranType = GetTranTypeDDL();

            if (ddlTranType != null)
            {
                if (ddlTranType.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnTranType.Value = ddlTranType.SelectedValue;
                    BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnTranType.Value = "";
                    BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
                }
            }
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            try
            {
                GetPageCount();
                this.BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RMMultipleTransactionView.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void GetPageCount()
        {
            string upperlimit = "";
            int rowCount = 0;
            int ratio = 0;
            string lowerlimit = "";
            string PageRecords = "";
            try
            {
                if (hdnRecordCount.Value != "")
                    rowCount = Convert.ToInt32(hdnRecordCount.Value);
                if (rowCount > 0)
                {
                    ratio = rowCount / 10;
                    mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
                    upperlimit = (mypager.CurrentPage * 10).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnRecordCount.Value;
                    PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;
                    hdnCurrentPage.Value = mypager.CurrentPage.ToString();
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
                FunctionInfo.Add("Method", "RMMultipleTransactionView.ascx.cs:GetPageCount()");
                object[] objects = new object[5];
                objects[0] = upperlimit;
                objects[1] = rowCount;
                objects[2] = ratio;
                objects[3] = lowerlimit;
                objects[4] = PageRecords;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetCustomerNameTextBox();

            if (txtName != null)
            {
                hdnCustomerNameSearch.Value = txtName.Text.Trim();
                BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
            }
        }

        private TextBox GetCustomerNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvMFTransactions.HeaderRow != null)
            {
                if ((TextBox)gvMFTransactions.HeaderRow.FindControl("txtNameSearch") != null)
                {
                    txt = (TextBox)gvMFTransactions.HeaderRow.FindControl("txtNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        protected void btnScripSearch_Click(object sender, EventArgs e)
        {
            TextBox txtSchemeName = GetSchemeTextBox();

            if (txtSchemeName != null)
            {
                hdnSchemeSearch.Value = txtSchemeName.Text.Trim();
                BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
            }
        }

        private TextBox GetSchemeTextBox()
        {
            TextBox txt = new TextBox();
            if (gvMFTransactions.HeaderRow != null)
            {
                if ((TextBox)gvMFTransactions.HeaderRow.FindControl("txtSchemeSearch") != null)
                {
                    txt = (TextBox)gvMFTransactions.HeaderRow.FindControl("txtSchemeSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        protected void gvMFTransactions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Calculating gross price 

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

        //protected void btnFolioNumberSearch_Click(object sender, EventArgs e)
        //{
        //    TextBox txtFolioNumber = GetFolioNumberTextBox();

        //    if (txtFolioNumber != null)
        //    {
        //        hdnFolioNumber.Value = txtFolioNumber.Text.Trim();
        //        BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
        //    }
        //}

        private TextBox GetFolioNumberTextBox()
        {
            TextBox txt = new TextBox();
            if (gvMFTransactions.HeaderRow != null)
            {
                if ((TextBox)gvMFTransactions.HeaderRow.FindControl("txtFolioNumberSearch") != null)
                {
                    txt = (TextBox)gvMFTransactions.HeaderRow.FindControl("txtFolioNumberSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void imgBtnWord_Click(object sender, ImageClickEventArgs e)
        {


        }

        protected void imgBtnPdf_Click(object sender, ImageClickEventArgs e)
        {
        }
        protected void imgBtnPrint_Click(object sender, ImageClickEventArgs e)
        {
            if (hdnDownloadPageType.Value.ToString() == "multiple")
            {
                BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "AferExportAll('ctrl_RMMultipleTransactionView_btnPrintGrid');", true);
            }

        }
        //protected void btnPrintGrid_Click(object sender, EventArgs e)
        //{
        //    BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
        //    gvMFTransactions.Columns[0].Visible = true;


        //    // GridView_Print();
        //}
        //protected void btnExportExcel_Click(object sender, EventArgs e)
        //{
        //    gvMFTransactions.Columns[0].Visible = false;

        //    gvMFTransactions.HeaderRow.Visible = true;

        //    if (hdnDownloadPageType.Value.ToString() == "single")
        //    {
        //        BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
        //    }
        //    else
        //    {
        //        BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "AferExportAll('ctrl_RMMultipleTransactionView_btnPrintGrid');", true);
        //    }

        //    ExportGridView(hdnDownloadFormat.Value.ToString());
        //    //
        //    //BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
        //    //gvMFTransactions.Columns[0].Visible = true;
        //}




        private void GridView_Print()
        {
            gvMFTransactions.Columns[0].Visible = false;
            //gvMFTransactions.Columns[1].Visible = false;
            if (hdnDownloadPageType.Value == "single")
            {
                BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
            }
            else
            {
                BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
            }

            PrepareGridViewForExport(gvMFTransactions);
            if (gvMFTransactions.HeaderRow != null)
            {
                PrepareControlForExport(gvMFTransactions.HeaderRow);
            }
            foreach (GridViewRow row in gvMFTransactions.Rows)
            {
                PrepareControlForExport(row);
            }
            if (gvMFTransactions.FooterRow != null)
            {
                PrepareControlForExport(gvMFTransactions.FooterRow);
            }



            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_RMMultipleTransactionView_tbl','ctrl_RMMultipleTransactionView_btnPrintGrid');", true);

        }

        private void PrepareGridViewForExport(Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    if ((gv.Controls[i] as DropDownList).SelectedItem != null)
                        l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(TextBox))
                {
                    l.Text = (gv.Controls[i] as TextBox).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }

            }

        }

        private void ExportGridView(string Filetype)
        {
            {
                HtmlForm frm = new HtmlForm();
                System.Web.UI.WebControls.Table tbl = new System.Web.UI.WebControls.Table();
                frm.Controls.Clear();
                frm.Attributes["runat"] = "server";
                if (Filetype.ToLower() == "excel")
                {
                    string temp = "Equity_Transactions.xls";
                    string attachment = "attachment; filename=" + temp;
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);


                    Response.Output.Write("<table border=\"0\"><tbody ><caption align=\"left\"><FONT FACE=\"ARIAL\"  SIZE=\"4\">");
                    Response.Output.Write("Equity Transactions for " + convertedFromDate.ToString("MMM-dd-yyyy") + " to " + convertedToDate.ToString("MMM-dd-yyyy") + "</FONT></caption>");
                    Response.Output.Write("<tr><td>");
                    Response.Output.Write("Report Generated on  : ");
                    Response.Output.Write(DateTime.Now.ToString("MMM-dd-yyyy hh:ss tt"));
                    Response.Output.Write("</td></tr>");
                    Response.Output.Write("</tbody></table>");

                    PrepareGridViewForExport(gvMFTransactions);

                    if (gvMFTransactions.HeaderRow != null)
                    {
                        PrepareControlForExport(gvMFTransactions.HeaderRow);

                    }
                    foreach (GridViewRow row in gvMFTransactions.Rows)
                    {

                        PrepareControlForExport(row);

                    }
                    if (gvMFTransactions.FooterRow != null)
                    {
                        PrepareControlForExport(gvMFTransactions.FooterRow);

                    }


                    gvMFTransactions.Parent.Controls.Add(frm);
                    frm.Controls.Add(gvMFTransactions);
                    frm.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }

            }

        }

        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedValue.ToString()));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
              CalculateDateRange();
            if (hdnDownloadPageType.Value == "multiple")
            {
                GridViewCultureFlag = false;
                BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate, 2);
                GridViewCultureFlag = true;
            }
            else if (hdnDownloadPageType.Value == "single")
            {
                GridViewCultureFlag = false;
                BindGrid(Convert.ToInt32(hdnCurrentPage.Value), convertedFromDate, convertedToDate, 1);
                GridViewCultureFlag = true;
            }
            ExportGridView("excel");
        }

    }
}