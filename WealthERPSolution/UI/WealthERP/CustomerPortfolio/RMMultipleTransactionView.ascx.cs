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

namespace WealthERP.CustomerPortfolio
{
    public partial class RMMultipleTransactionView : System.Web.UI.UserControl
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
        static double totalAmount = 0;
        static double totalUnits = 0;
        string PasssedFolioValue = null;
        bool GridViewCultureFlag = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";

                advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                rmVo = (RMVo)Session[SessionContents.RmVo];

                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
                    userType = "advisor";
                else
                    userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();
                
                
                txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();

                if (!IsPostBack)
                {
                    hdnSchemeSearch.Value = string.Empty;
                    hdnTranType.Value = string.Empty;
                    hdnCustomerNameSearch.Value = string.Empty;
                    hdnFolioNumber.Value = string.Empty;
                    hdnCategory.Value = string.Empty;
                    hdnAMC.Value = "0";
                    rbtnPickDate.Checked = true;
                    rbtnPickPeriod.Checked = false;
                    trRange.Visible = true;
                    trPeriod.Visible = false;
                    tdGroupHead.Visible = false;
                    lblGroupHead.Visible = false;
                    txtParentCustomer.Visible = false;

                    if (Request.QueryString["folionum"] != null)
                    {
                        string folionum = Request.QueryString["folionum"].ToString();
                        DataSet ds = customerTransactionBo.GetPortfolioType(folionum);
                        ddlPortfolioGroup.SelectedValue = ds.Tables[0].Rows[0][0].ToString();
                        //hdnFolioNumber.Value = folionum;
                        PasssedFolioValue = folionum;


                        BindLastTradeDate();
                        string fromdate = "01-01-1990";
                        txtFromDate.Text = DateTime.Parse(fromdate).ToShortDateString();
                    }
                    else
                    {
                        BindLastTradeDate();
                    }

                    BindGrid(0,mypager.CurrentPage, DateTime.Parse(txtFromDate.Text), DateTime.Parse(txtToDate.Text));
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
            txtFromDate.Text = DateTime.Parse(ds.Tables[0].Rows[0][0].ToString()).ToShortDateString();
            txtToDate.Text = DateTime.Parse(ds.Tables[0].Rows[0][0].ToString()).ToShortDateString();

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
                tdGroupHead.Visible = false;

            }
            else if (rbtnGroup.Checked == true)
            {
                lblGroupHead.Visible = true;
                txtParentCustomer.Visible = true;
                tdGroupHead.Visible = true;
                BindGroupHead();

            }
        }

        private void BindGroupHead()
        {

        }
        protected void btnGo_Click(object sender, EventArgs e)
        {

            hdnSchemeSearch.Value = string.Empty;
            hdnTranType.Value = string.Empty;
            hdnCustomerNameSearch.Value = string.Empty;
            hdnFolioNumber.Value = string.Empty;
            if (rbtnPickDate.Checked)
            {
                convertedFromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
                convertedToDate = Convert.ToDateTime(txtToDate.Text.Trim());
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
            BindGrid(0,mypager.CurrentPage, convertedFromDate, convertedToDate);
            
        }

        private void BindGrid(int ExportGridView, int currentPage, DateTime convertedFromDate, DateTime convertedToDate)
            {
            Dictionary<string, string> genDictTranType = new Dictionary<string, string>();
            Dictionary<string, string> genDictCategory = new Dictionary<string, string>();
            Dictionary<string, int> genDictAMC = new Dictionary<string, int>();
            DataSet ds = new DataSet();
            int Count = 0;
            totalAmount = 0;
            totalUnits = 0;
            int rmID = 0;
            int AdviserId = 0;

            if (userType == "advisor")
                AdviserId = advisorVo.advisorId;
            else
                rmID = rmVo.RMId;
            
            
            try
            {//pramod
                if (ExportGridView == 1)
                {
                    if (rbtnGroup.Checked)
                    {

                        mfTransactionList = customerTransactionBo.GetRMCustomerMFTransactions(out Count, 5000, rmID, AdviserId, int.Parse(txtParentCustomerId.Value), convertedFromDate, convertedToDate, int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()), hdnCustomerNameSearch.Value.Trim(), hdnSchemeSearch.Value.Trim(), hdnTranType.Value.Trim(), hdnStatus.Value.Trim(), out genDictTranType, hdnFolioNumber.Value.Trim(), PasssedFolioValue, hdnCategory.Value.ToString(), int.Parse(hdnAMC.Value.ToString()), out genDictCategory, out genDictAMC);
                        hdnRecordCount.Value = lblTotalRows.Text = Count.ToString();
                    }
                    else
                    {

                        mfTransactionList = customerTransactionBo.GetRMCustomerMFTransactions(out Count, 5000, rmID, AdviserId, 0, convertedFromDate, convertedToDate, int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()), hdnCustomerNameSearch.Value.Trim(), hdnSchemeSearch.Value.Trim(), hdnTranType.Value.Trim(), hdnStatus.Value.Trim(), out genDictTranType, hdnFolioNumber.Value.Trim(), PasssedFolioValue, hdnCategory.Value.ToString(), int.Parse(hdnAMC.Value.ToString()), out genDictCategory, out genDictAMC);
                        hdnRecordCount.Value = lblTotalRows.Text = Count.ToString();
                    }

                }
                else 
                {
                    if (rbtnGroup.Checked)
                    {

                        mfTransactionList = customerTransactionBo.GetRMCustomerMFTransactions(out Count, currentPage, rmID, AdviserId, int.Parse(txtParentCustomerId.Value), convertedFromDate, convertedToDate, int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()), hdnCustomerNameSearch.Value.Trim(), hdnSchemeSearch.Value.Trim(), hdnTranType.Value.Trim(), hdnStatus.Value.Trim(), out genDictTranType, hdnFolioNumber.Value.Trim(), PasssedFolioValue, hdnCategory.Value.ToString(), int.Parse(hdnAMC.Value.ToString()), out genDictCategory, out genDictAMC);
                        hdnRecordCount.Value = lblTotalRows.Text = Count.ToString();
                    }
                    else
                    {

                        mfTransactionList = customerTransactionBo.GetRMCustomerMFTransactions(out Count, currentPage, rmID, AdviserId, 0, convertedFromDate, convertedToDate, int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()), hdnCustomerNameSearch.Value.Trim(), hdnSchemeSearch.Value.Trim(), hdnTranType.Value.Trim(), hdnStatus.Value.Trim(), out genDictTranType, hdnFolioNumber.Value.Trim(), PasssedFolioValue, hdnCategory.Value.ToString(), int.Parse(hdnAMC.Value.ToString()), out genDictCategory, out genDictAMC);
                        hdnRecordCount.Value = lblTotalRows.Text = Count.ToString();
                    }

                }
                
                if (mfTransactionList.Count != 0)
                {
                    trMessage.Visible = false;
                    lblCurrentPage.Visible = true;
                    trPager.Visible = true;
                    lblTotalRows.Visible = true;
                    DataTable dtMFTransactions = new DataTable();

                    dtMFTransactions.Columns.Add("TransactionId");
                    dtMFTransactions.Columns.Add("Customer Name");
                    dtMFTransactions.Columns.Add("Folio Number");                    
                    dtMFTransactions.Columns.Add("Scheme Name");
                    dtMFTransactions.Columns.Add("Transaction Type");
                    dtMFTransactions.Columns.Add("Transaction Date");
                    dtMFTransactions.Columns.Add("Price");
                    dtMFTransactions.Columns.Add("Units");
                    dtMFTransactions.Columns.Add("Amount");
                    dtMFTransactions.Columns.Add("STT");
                    dtMFTransactions.Columns.Add("Portfolio Name");
                    dtMFTransactions.Columns.Add("Transaction Status");
                    dtMFTransactions.Columns.Add("Category");
                    dtMFTransactions.Columns.Add("AMC");
                    dtMFTransactions.Columns.Add("ADUL_ProcessId");
                    dtMFTransactions.Columns.Add("CMFT_SubBrokerCode");
                    dtMFTransactions.Columns.Add("PAISC_AssetInstrumentSubCategoryName");
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
                        if (GridViewCultureFlag==true)
                        drMFTransaction[6] = decimal.Parse(mfTransactionVo.Price.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        else
                        {
                            drMFTransaction[6] = decimal.Parse(mfTransactionVo.Price.ToString()); 
                        }
                        //drMFTransaction[7] = mfTransactionVo.Units.ToString("f4");
                        //drMFTransaction[8] = decimal.Parse(mfTransactionVo.Amount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drMFTransaction[7] = mfTransactionVo.Units.ToString("f4");
                        totalUnits = totalUnits + mfTransactionVo.Units;
                        if (GridViewCultureFlag == true)
                        drMFTransaction[8] = decimal.Parse(mfTransactionVo.Amount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        else
                        {
                            drMFTransaction[8] = decimal.Parse(mfTransactionVo.Amount.ToString());
                        }
                        totalAmount = totalAmount + mfTransactionVo.Amount;
                        if (GridViewCultureFlag == true)
                        drMFTransaction[9] = decimal.Parse(mfTransactionVo.STT.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        else
                        {
                            drMFTransaction[9] = decimal.Parse(mfTransactionVo.STT.ToString());
                        }
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
                        dtMFTransactions.Rows.Add(drMFTransaction);
                    }

                    gvMFTransactions.DataSource = dtMFTransactions;
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
                    if (genDictCategory.Count > 0)
                    {
                        DropDownList ddlCategory = GetCategoryDDL();
                        if (ddlCategory != null)
                        {
                            ddlCategory.DataSource = genDictCategory;
                            ddlCategory.DataTextField = "Key";
                            ddlCategory.DataValueField = "Value";
                            ddlCategory.DataBind();
                            ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }

                        if (hdnCategory.Value != "")
                        {
                            ddlCategory.SelectedValue = hdnCategory.Value.ToString().Trim();
                        }
                    }
                    if (genDictAMC.Count > 0)
                    {
                        DropDownList ddlAMC = GetAMCDDL();
                        if (ddlAMC != null)
                        {
                            ddlAMC.DataSource = genDictAMC;
                            ddlAMC.DataTextField = "Key";
                            ddlAMC.DataValueField = "Value";
                            ddlAMC.DataBind();
                            ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }

                        if (hdnAMC.Value != "")
                        {
                            ddlAMC.SelectedValue = hdnAMC.Value.ToString().Trim();
                        }
                    }
                    if (hdnSchemeSearch.Value != "")
                    {
                        
                        if (gvMFTransactions.HeaderRow != null)
                        {
                            if ((TextBox)gvMFTransactions.HeaderRow.FindControl("txtSchemeSearch") != null)
                            {
                                TextBox txtScheme = (TextBox)gvMFTransactions.HeaderRow.FindControl("txtSchemeSearch");
                                txtScheme.Text = hdnSchemeSearch.Value.ToString();
                            }
                        }
                    }
                    if (hdnFolioNumber.Value != "")
                    {

                        if (gvMFTransactions.HeaderRow != null)
                        {
                            if ((TextBox)gvMFTransactions.HeaderRow.FindControl("txtFolioNumberSearch") != null)
                            {
                                TextBox txtFolio = (TextBox)gvMFTransactions.HeaderRow.FindControl("txtFolioNumberSearch");
                                txtFolio.Text = hdnFolioNumber.Value.ToString();
                            }
                        }
                    }
                    if (hdnCustomerNameSearch.Value != "")
                    {

                        if (gvMFTransactions.HeaderRow != null)
                        {
                            if ((TextBox)gvMFTransactions.HeaderRow.FindControl("txtNameSearch") != null)
                            {
                                TextBox txtName = (TextBox)gvMFTransactions.HeaderRow.FindControl("txtNameSearch");
                                txtName.Text = hdnCustomerNameSearch.Value.ToString();
                            }
                        }
                    }  
                    GetPageCount();
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
            catch (Exception e)
            {
            }
        }
        private void BindGrid4Export(int currentPage, DateTime convertedFromDate, DateTime convertedToDate)
        {
            Dictionary<string, string> genDictTranType = new Dictionary<string, string>();
            Dictionary<string, string> genDictCategory = new Dictionary<string, string>();
            Dictionary<string, int> genDictAMC = new Dictionary<string, int>();
            DataSet ds = new DataSet();
            int Count = 0;
            totalAmount = 0;
            totalUnits = 0;
            int rmID = 0;
            int AdviserId = 0;

            if (userType == "advisor")
                AdviserId = advisorVo.advisorId;
            else
                rmID = rmVo.RMId;

            try
            {

                if (rbtnGroup.Checked)
                {

                    mfTransactionList = customerTransactionBo.GetRMCustomerMFTransactions(out Count, currentPage, rmID, AdviserId, int.Parse(txtParentCustomerId.Value), convertedFromDate, convertedToDate, int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()), hdnCustomerNameSearch.Value.Trim(), hdnSchemeSearch.Value.Trim(), hdnTranType.Value.Trim(), hdnStatus.Value.Trim(), out genDictTranType, hdnFolioNumber.Value.Trim(), PasssedFolioValue, hdnCategory.Value.ToString(), int.Parse(hdnAMC.Value.ToString()), out genDictCategory, out genDictAMC);
                    hdnRecordCount.Value = lblTotalRows.Text = Count.ToString();
                }
                else
                {

                    mfTransactionList = customerTransactionBo.GetRMCustomerMFTransactions(out Count, currentPage, rmID, AdviserId, 0, convertedFromDate, convertedToDate, int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()), hdnCustomerNameSearch.Value.Trim(), hdnSchemeSearch.Value.Trim(), hdnTranType.Value.Trim(), hdnStatus.Value.Trim(), out genDictTranType, hdnFolioNumber.Value.Trim(), PasssedFolioValue, hdnCategory.Value.ToString(), int.Parse(hdnAMC.Value.ToString()), out genDictCategory, out genDictAMC);
                    hdnRecordCount.Value = lblTotalRows.Text = Count.ToString();
                }
                if (mfTransactionList.Count != 0)
                {
                    trMessage.Visible = false;
                    lblCurrentPage.Visible = true;
                    trPager.Visible = true;
                    lblTotalRows.Visible = true;
                    DataTable dtMFTransactions = new DataTable();

                    dtMFTransactions.Columns.Add("TransactionId");
                    dtMFTransactions.Columns.Add("Customer Name");
                    dtMFTransactions.Columns.Add("Folio Number");
                    dtMFTransactions.Columns.Add("Scheme Name");
                    dtMFTransactions.Columns.Add("Transaction Type");
                    dtMFTransactions.Columns.Add("Transaction Date");
                    dtMFTransactions.Columns.Add("Price");
                    dtMFTransactions.Columns.Add("Units");
                    dtMFTransactions.Columns.Add("Amount");
                    dtMFTransactions.Columns.Add("STT");
                    dtMFTransactions.Columns.Add("Portfolio Name");
                    dtMFTransactions.Columns.Add("Transaction Status");
                    dtMFTransactions.Columns.Add("Category");
                    dtMFTransactions.Columns.Add("AMC");

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
                        drMFTransaction[6] = decimal.Parse(mfTransactionVo.Price.ToString());
                        //drMFTransaction[7] = mfTransactionVo.Units.ToString("f4");
                        //drMFTransaction[8] = decimal.Parse(mfTransactionVo.Amount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drMFTransaction[7] = mfTransactionVo.Units.ToString("f4");
                        totalUnits = totalUnits + mfTransactionVo.Units;
                        drMFTransaction[8] = decimal.Parse(mfTransactionVo.Amount.ToString());
                        totalAmount = totalAmount + mfTransactionVo.Amount;
                        drMFTransaction[9] = decimal.Parse(mfTransactionVo.STT.ToString());
                        drMFTransaction[10] = mfTransactionVo.PortfolioName.ToString();
                        drMFTransaction[11] = mfTransactionVo.TransactionStatus.ToString();
                        drMFTransaction[12] = mfTransactionVo.Category;
                        drMFTransaction[13] = mfTransactionVo.AMCName;
                        dtMFTransactions.Rows.Add(drMFTransaction);
                    }

                    gvMFTransactions.DataSource = dtMFTransactions;
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

                    if (genDictCategory.Count > 0)
                    {
                        DropDownList ddlCategory = GetCategoryDDL();
                        if (ddlCategory != null)
                        {
                            ddlCategory.DataSource = genDictCategory;
                            ddlCategory.DataTextField = "Key";
                            ddlCategory.DataValueField = "Value";
                            ddlCategory.DataBind();
                            ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }

                        if (hdnCategory.Value != "")
                        {
                            ddlCategory.SelectedValue = hdnCategory.Value.ToString().Trim();
                        }
                    }
                    if (genDictAMC.Count > 0)
                    {
                        DropDownList ddlAMC = GetAMCDDL();
                        if (ddlAMC != null)
                        {
                            ddlAMC.DataSource = genDictAMC;
                            ddlAMC.DataTextField = "Key";
                            ddlAMC.DataValueField = "Value";
                            ddlAMC.DataBind();
                            ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }

                        if (hdnAMC.Value != "")
                        {
                            ddlAMC.SelectedValue = hdnAMC.Value.ToString().Trim();
                        }
                    }
                    if (hdnSchemeSearch.Value != "")
                    {

                        if (gvMFTransactions.HeaderRow != null)
                        {
                            if ((TextBox)gvMFTransactions.HeaderRow.FindControl("txtSchemeSearch") != null)
                            {
                                TextBox txtScheme = (TextBox)gvMFTransactions.HeaderRow.FindControl("txtSchemeSearch");
                                txtScheme.Text = hdnSchemeSearch.Value.ToString();
                            }
                        }
                    }
                    if (hdnFolioNumber.Value != "")
                    {

                        if (gvMFTransactions.HeaderRow != null)
                        {
                            if ((TextBox)gvMFTransactions.HeaderRow.FindControl("txtFolioNumberSearch") != null)
                            {
                                TextBox txtFolio = (TextBox)gvMFTransactions.HeaderRow.FindControl("txtFolioNumberSearch");
                                txtFolio.Text = hdnFolioNumber.Value.ToString();
                            }
                        }
                    }
                    if (hdnCustomerNameSearch.Value != "")
                    {

                        if (gvMFTransactions.HeaderRow != null)
                        {
                            if ((TextBox)gvMFTransactions.HeaderRow.FindControl("txtNameSearch") != null)
                            {
                                TextBox txtName = (TextBox)gvMFTransactions.HeaderRow.FindControl("txtNameSearch");
                                txtName.Text = hdnCustomerNameSearch.Value.ToString();
                            }
                        }
                    }  
                    GetPageCount();
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
            catch (Exception e)
            {
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
        private DropDownList GetCategoryDDL()
        {

            DropDownList ddl = new DropDownList();
            if (gvMFTransactions.HeaderRow != null)
            {
                if ((DropDownList)gvMFTransactions.HeaderRow.FindControl("ddlCategory") != null)
                {
                    ddl = (DropDownList)gvMFTransactions.HeaderRow.FindControl("ddlCategory");
                }
            }
            else
                ddl = null;

            return ddl;

        }
        private DropDownList GetAMCDDL()
        {

            DropDownList ddl = new DropDownList();
            if (gvMFTransactions.HeaderRow != null)
            {
                if ((DropDownList)gvMFTransactions.HeaderRow.FindControl("ddlAMC") != null)
                {
                    ddl = (DropDownList)gvMFTransactions.HeaderRow.FindControl("ddlAMC");
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
                    BindGrid(0,mypager.CurrentPage, convertedFromDate, convertedToDate);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnTranType.Value = "";
                    BindGrid(0,mypager.CurrentPage, convertedFromDate, convertedToDate);
                }
            }
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCategory = GetCategoryDDL();

            if (ddlCategory != null)
            {
                if (ddlCategory.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnCategory.Value = ddlCategory.SelectedValue;
                    BindGrid(0, mypager.CurrentPage, convertedFromDate, convertedToDate);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnCategory.Value = "";
                    BindGrid(0, mypager.CurrentPage, convertedFromDate, convertedToDate);
                }
            }
        }
        protected void ddlAMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAMC = GetAMCDDL();

            if (ddlAMC != null)
            {
                if (ddlAMC.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnAMC.Value = ddlAMC.SelectedValue;
                    BindGrid(0, mypager.CurrentPage, convertedFromDate, convertedToDate);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnAMC.Value = "0";
                    BindGrid(0, mypager.CurrentPage, convertedFromDate, convertedToDate);
                }
            }
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            try
            {
                GetPageCount();
                this.BindGrid(0,mypager.CurrentPage, convertedFromDate, convertedToDate);
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
                BindGrid(0,mypager.CurrentPage, convertedFromDate, convertedToDate);
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

        protected void btnSchemeSearch_Click(object sender, EventArgs e)
        {
            TextBox txtSchemeName = GetSchemeTextBox();

            if (txtSchemeName != null)
            {
                hdnSchemeSearch.Value = txtSchemeName.Text.Trim();
                BindGrid(0,mypager.CurrentPage, convertedFromDate, convertedToDate);
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
            // Commented the code to remove Total Ammount and Units from the Grid <<Vinayak Patil>>
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.Cells[1].Text = "Total ";
            //    if (GridViewCultureFlag == true)
            //        e.Row.Cells[10].Text = decimal.Parse(totalAmount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            //    else
            //    {
            //        e.Row.Cells[10].Text = decimal.Parse(totalAmount.ToString()).ToString();
            //    }
            //    e.Row.Cells[10].Attributes.Add("align", "Right");
            //    if (GridViewCultureFlag == true)
            //        e.Row.Cells[9].Text = decimal.Parse(totalUnits.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            //    else
            //    {
            //        e.Row.Cells[9].Text = decimal.Parse(totalUnits.ToString()).ToString();
            //    }
            //    e.Row.Cells[9].Attributes.Add("align", "Right");


            //}
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                ddlStatus.SelectedValue = hdnStatus.Value;
            }
        }

        protected void btnFolioNumberSearch_Click(object sender, EventArgs e)
        {
            TextBox txtFolioNumber = GetFolioNumberTextBox();

            if (txtFolioNumber != null)
            {
                hdnFolioNumber.Value = txtFolioNumber.Text.Trim();
                BindGrid(0,mypager.CurrentPage, convertedFromDate, convertedToDate);
            }
        }

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
            ModalPopupExtender1.TargetControlID = "imgBtnExport";
            ModalPopupExtender1.Show();
        }

        protected void imgBtnWord_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtender1.TargetControlID = "imgBtnWord";
            ModalPopupExtender1.Show();

        }

        protected void imgBtnPdf_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtender1.TargetControlID = "imgBtnPdf";
            ModalPopupExtender1.Show();
        }
        protected void imgBtnPrint_Click(object sender, ImageClickEventArgs e)
        {
            if (hdnDownloadPageType.Value.ToString() == "multiple")
            {
                BindGrid(0,mypager.CurrentPage, convertedFromDate, convertedToDate);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "AferExportAll('ctrl_RMMultipleTransactionView_btnPrintGrid');", true);
            }
            ModalPopupExtender1.TargetControlID = "imgBtnPrint";
            ModalPopupExtender1.Show();

        }
        //protected void btnPrintGrid_Click(object sender, EventArgs e)
        //{
        //    BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
        //    gvMFTransactions.Columns[0].Visible = true;


        //    // GridView_Print();
        //}
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
           
                gvMFTransactions.Columns[0].Visible = false;

                gvMFTransactions.HeaderRow.Visible = true;

                if (hdnDownloadPageType.Value.ToString() == "single")
                {
                    GridViewCultureFlag = false;
                    BindGrid(0,Convert.ToInt32(hdnCurrentPage.Value), convertedFromDate, convertedToDate);
                    GridViewCultureFlag = true;
                }
                else
                {
                    GridViewCultureFlag = false;
                    //1 for All record to be export to excel...(pramod)
                    BindGrid(1,mypager.CurrentPage, convertedFromDate, convertedToDate);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "AferExportAll('ctrl_RMMultipleTransactionView_btnPrintGrid');", true);
                    GridViewCultureFlag = true;
                }
               

                ExportGridView(hdnDownloadFormat.Value.ToString());
                 //
                //BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
                //gvMFTransactions.Columns[0].Visible = true;
            
        }




        private void GridView_Print()
        {
            gvMFTransactions.Columns[0].Visible = false;
            //gvMFTransactions.Columns[1].Visible = false;
            if (hdnDownloadPageType.Value.ToString() == "single")
            {
                BindGrid(0,mypager.CurrentPage, convertedFromDate, convertedToDate);
            }
            else
            {
                BindGrid(0,mypager.CurrentPage, convertedFromDate, convertedToDate);
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
        protected void btnPrintGrid_Click(object sender, EventArgs e)
        {
            BindGrid(0,mypager.CurrentPage, convertedFromDate, convertedToDate);
           // gvMFTransactions.Columns[0].Visible = true;
          //  gvMFTransactions.Columns[1].Visible = true;
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
            // float ReportTextSize = 7;
            {
                HtmlForm frm = new HtmlForm();
                System.Web.UI.WebControls.Table tbl = new System.Web.UI.WebControls.Table();
                frm.Controls.Clear();
                frm.Attributes["runat"] = "server";
                if (Filetype.ToLower() == "print")
                {
                    GridView_Print();
                }

                else if (Filetype.ToLower() == "excel")
                {
                    // gvCustomer.Columns.Remove(this.gvCustomer.Columns[0]);
                    string temp = rmVo.FirstName + rmVo.LastName + "Customer's MFTransactionList.xls";
                    string attachment = "attachment; filename=" + temp;
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);

                    Response.Output.Write("<table border=\"0\"><tbody ><caption align=\"left\"><FONT FACE=\"ARIAL\"  SIZE=\"4\">");
                    Response.Output.Write("Mutual Fund Transactions for " + convertedFromDate.ToString("MMM-dd-yyyy") + " to " + convertedToDate.ToString("MMM-dd-yyyy") + "</FONT></caption>");
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
                else if (Filetype.ToLower() == "pdf")
                {
                    string temp = rmVo.FirstName + rmVo.LastName + "MFTransactionList";

                    gvMFTransactions.AllowPaging = false;
                    gvMFTransactions.DataBind();
                    iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gvMFTransactions.Columns.Count-1 );

                    table.HeaderRows = 4;
                    iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);

                    Phrase phApplicationName = new Phrase("WWW.PrincipalConsulting.net", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
                    PdfPCell clApplicationName = new PdfPCell(phApplicationName);
                    clApplicationName.Border = PdfPCell.NO_BORDER;
                    clApplicationName.HorizontalAlignment = Element.ALIGN_LEFT;



                    Phrase phDate = new Phrase(DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
                    PdfPCell clDate = new PdfPCell(phDate);
                    clDate.HorizontalAlignment = Element.ALIGN_RIGHT;
                    clDate.Border = PdfPCell.NO_BORDER;


                    headerTable.AddCell(clApplicationName);
                    headerTable.AddCell(clDate);
                    headerTable.DefaultCell.Border = PdfPCell.NO_BORDER;


                    PdfPCell cellHeader = new PdfPCell(headerTable);
                    cellHeader.Border = PdfPCell.NO_BORDER;
                    cellHeader.Colspan = gvMFTransactions.Columns.Count-1;
                    table.AddCell(cellHeader);

                    Phrase phHeader = new Phrase(temp, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                    PdfPCell clHeader = new PdfPCell(phHeader);
                    clHeader.Colspan = gvMFTransactions.Columns.Count-1 ;
                    clHeader.Border = PdfPCell.NO_BORDER;
                    clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(clHeader);


                    Phrase phSpace = new Phrase("\n");
                    PdfPCell clSpace = new PdfPCell(phSpace);
                    clSpace.Border = PdfPCell.NO_BORDER;
                    clSpace.Colspan = gvMFTransactions.Columns.Count -1;
                    table.AddCell(clSpace);

                    GridViewRow HeaderRow = gvMFTransactions.HeaderRow;
                    if (HeaderRow != null)
                    {
                        string cellText = "";
                        for (int j = 1; j < gvMFTransactions.Columns.Count; j++)
                        {
                            if (j == 1)
                            {
                                cellText = "Customer Name";
                            }
                            else if (j == 2)
                            {
                                cellText = "Folio Number";
                            }
                            else if (j == 3)
                            {
                                cellText = "Scheme Name";
                            }
                            else if (j == 4)
                            {
                                cellText = "Transaction Type";
                            }

                            else if (j == 5)
                            {
                                cellText = "Transaction Date";
                            }
                            else if (j == 6)
                            {
                                cellText = "Price (Rs)";
                            }
                            else if (j == 7)
                            {
                                cellText = "Units";
                            }
                            else if (j == 8)
                            {
                                cellText = "Amount (Rs)";
                            }


                            else if (j == 9)
                            {
                                cellText = "STT (Rs)";
                            }

                            else
                            {
                                cellText = Server.HtmlDecode(gvMFTransactions.HeaderRow.Cells[j].Text);
                            }
                            Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD));
                            table.AddCell(ph);
                        }

                    }


                    for (int i = 0; i < gvMFTransactions.Rows.Count; i++)
                    {
                        string cellText = "";
                        if (gvMFTransactions.Rows[i].RowType == DataControlRowType.DataRow)
                        {

                            for (int j = 1; j < gvMFTransactions.Columns.Count; j++)
                            {

                                if (j == 1)
                                {

                                    cellText = ((Label)gvMFTransactions.Rows[i].FindControl("lblNameHeader")).Text;
                                }
                                else if (j == 2)
                                {

                                    cellText = ((Label)gvMFTransactions.Rows[i].FindControl("lblFolioNUmberHeader")).Text;
                                }
                                else if (j == 3)
                                {

                                    cellText = ((Label)gvMFTransactions.Rows[i].FindControl("lblSchemeHeader")).Text;
                                }
                                else if (j == 4)
                                {

                                    cellText = ((Label)gvMFTransactions.Rows[i].FindControl("lblTranTypeHeader")).Text;
                                }

                                else if (j == 5)
                                {
                                    cellText = ((Label)gvMFTransactions.Rows[i].FindControl("lblTranDateHeader")).Text;
                                }
                                else 
                                {
                                    cellText = gvMFTransactions.Rows[i].Cells[j].Text;
                                }


                                Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL));
                                iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
                                table.AddCell(ph);

                            }

                        }

                    }



                    //Create the PDF Document

                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    pdfDoc.Add(table);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    temp = "filename=" + temp + ".pdf";
                    //    Response.AddHeader("content-disposition", "attachment;" + "filename=GridViewExport.pdf");
                    Response.AddHeader("content-disposition", "attachment;" + temp);
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();



                }
                else if (Filetype.ToLower() == "word")
                {
                    gvMFTransactions.Columns.Remove(this.gvMFTransactions.Columns[0]);
                    string temp = rmVo.FirstName + rmVo.LastName + "MFTransactionList.doc";
                    string attachment = "attachment; filename=" + temp;
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/msword";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);

                    Response.Output.Write("<table border=\"0\"><tbody><caption><FONT FACE=\"ARIAL\" SIZE=\"4\">Mutual Fund Transaction</FONT></caption><tr><td>");
                    Response.Output.Write("RM Name : ");
                    Response.Output.Write("</td>");
                    Response.Output.Write("<td>");
                    Response.Output.Write(rmVo.FirstName + rmVo.LastName);
                    Response.Output.Write("</td></tr>");

                    Response.Output.Write("<tr><td>");
                    Response.Output.Write("Contact Person  : ");
                    Response.Output.Write("</td>");
                    Response.Output.Write("<td>");
                    Response.Output.Write(rmVo.FirstName + rmVo.MiddleName + rmVo.LastName);
                    Response.Output.Write("</td></tr><tr><td>");
                    Response.Output.Write("Date : ");
                    Response.Output.Write("</td><td>");
                    System.DateTime tDate1 = System.DateTime.Now;
                    Response.Output.Write(tDate1);
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

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlStatus = GetStatusDDL();

            if (ddlStatus != null)
            {
                if (ddlStatus.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnStatus.Value = ddlStatus.SelectedValue;
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnStatus.Value = "";
                }
                if (rbtnPickDate.Checked)
                {
                    convertedFromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
                    convertedToDate = Convert.ToDateTime(txtToDate.Text.Trim());
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
                BindGrid(0,mypager.CurrentPage, convertedFromDate, convertedToDate);
            }
        }

        private DropDownList GetStatusDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvMFTransactions.HeaderRow != null)
            {
                if ((DropDownList)gvMFTransactions.HeaderRow.FindControl("ddlStatus") != null)
                {
                    ddl = (DropDownList)gvMFTransactions.HeaderRow.FindControl("ddlStatus");
                }
            }
            else
                ddl = null;

            return ddl;
        }

        protected void gvMFTransactions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //private void ExportGridView(string Filetype)
        //{
        //    {
        //        HtmlForm frm = new HtmlForm();
        //        HtmlImage image = new HtmlImage();

        //        frm.Controls.Clear();
        //        frm.Attributes["runat"] = "server";
        //        if (Filetype.ToLower() == "print")
        //        {
        //            GridView_Print();
        //        }
        //        else if (Filetype.ToLower() == "excel")
        //        {
        //            // gvCustomer.Columns.Remove(this.gvCustomer.Columns[0]);
        //            string temp = userVo.FirstName + userVo.LastName + "Customer.xls";
        //            string attachment = "attachment; filename=" + temp;
        //            Response.ClearContent();
        //            Response.AddHeader("content-disposition", attachment);
        //            Response.ContentType = "application/ms-excel";
        //            StringWriter sw = new StringWriter();
        //            HtmlTextWriter htw = new HtmlTextWriter(sw);
        //            Response.Output.Write("<table border=\"0\"><tbody><tr><td>");
        //            Response.Output.Write("Advisor Name : ");
        //            Response.Output.Write("</td>");
        //            Response.Output.Write("<td>");
        //            Response.Output.Write(userVo.FirstName + userVo.LastName);
        //            Response.Output.Write("</td></tr>");
        //            Response.Output.Write("<tr><td>");
        //            Response.Output.Write("Report  : ");
        //            Response.Output.Write("</td>");
        //            Response.Output.Write("<td>");
        //            Response.Output.Write("Customer List");
        //            Response.Output.Write("</td></tr><tr><td>");
        //            Response.Output.Write("Date : ");
        //            Response.Output.Write("</td><td>");
        //            System.DateTime tDate1 = System.DateTime.Now;
        //            Response.Output.Write(tDate1);
        //            Response.Output.Write("</td></tr>");
        //            Response.Output.Write("</tbody></table>");

        //            PrepareGridViewForExport(gvCustomers);

        //            if (gvCustomers.HeaderRow != null)
        //            {
        //                PrepareControlForExport(gvCustomers.HeaderRow);
        //                //tbl.Rows.Add(gvMFTransactions.HeaderRow);
        //            }
        //            foreach (GridViewRow row in gvCustomers.Rows)
        //            {

        //                PrepareControlForExport(row);

        //                //tbl.Rows.Add(row);
        //            }
        //            if (gvCustomers.FooterRow != null)
        //            {
        //                PrepareControlForExport(gvCustomers.FooterRow);
        //                // tbl.Rows.Add(gvMFTransactions.FooterRow);
        //            }

        //            gvCustomers.Parent.Controls.Add(frm);
        //            frm.Controls.Add(gvCustomers);
        //            frm.RenderControl(htw);
        //            HttpContext.Current.Response.Write(sw.ToString());
        //            HttpContext.Current.Response.End();
        //        }


        //        else if (Filetype.ToLower() == "pdf")
        //        {
        //            string temp = userVo.FirstName + userVo.LastName + "_Customer List";

        //            gvCustomers.AllowPaging = false;
        //            gvCustomers.DataBind();
        //            iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gvCustomers.Columns.Count - 1);

        //            table.HeaderRows = 4;
        //            iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);
        //            Phrase phApplicationName = new Phrase("WWW.PrincipalConsulting.net", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
        //            PdfPCell clApplicationName = new PdfPCell(phApplicationName);
        //            clApplicationName.Border = PdfPCell.NO_BORDER;
        //            clApplicationName.HorizontalAlignment = Element.ALIGN_LEFT;


        //            Phrase phDate = new Phrase(DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
        //            PdfPCell clDate = new PdfPCell(phDate);
        //            clDate.HorizontalAlignment = Element.ALIGN_RIGHT;
        //            clDate.Border = PdfPCell.NO_BORDER;


        //            headerTable.AddCell(clApplicationName);
        //            headerTable.AddCell(clDate);
        //            headerTable.DefaultCell.Border = PdfPCell.NO_BORDER;

        //            PdfPCell cellHeader = new PdfPCell(headerTable);
        //            cellHeader.Border = PdfPCell.NO_BORDER;
        //            cellHeader.Colspan = gvCustomers.Columns.Count - 1;
        //            table.AddCell(cellHeader);

        //            Phrase phHeader = new Phrase(temp, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
        //            PdfPCell clHeader = new PdfPCell(phHeader);
        //            clHeader.Colspan = gvCustomers.Columns.Count - 1;
        //            clHeader.Border = PdfPCell.NO_BORDER;
        //            clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
        //            table.AddCell(clHeader);


        //            Phrase phSpace = new Phrase("\n");
        //            PdfPCell clSpace = new PdfPCell(phSpace);
        //            clSpace.Border = PdfPCell.NO_BORDER;
        //            clSpace.Colspan = gvCustomers.Columns.Count - 1;
        //            table.AddCell(clSpace);

        //            GridViewRow HeaderRow = gvCustomers.HeaderRow;
        //            if (HeaderRow != null)
        //            {
        //                string cellText = "";
        //                for (int j = 1; j < gvCustomers.Columns.Count; j++)
        //                {
        //                    if (j == 1)
        //                    {
        //                        cellText = "Parent";
        //                    }
        //                    else if (j == 2)
        //                    {
        //                        cellText = "Customer Name / Company Name";
        //                    }
        //                    else if (j == 7)
        //                    {
        //                        cellText = "Area";
        //                    }
        //                    else if (j == 9)
        //                    {
        //                        cellText = "Pincode";
        //                    }
        //                    else if (j == 10)
        //                    {
        //                        cellText = "Assigned RM";
        //                    }
        //                    else
        //                    {
        //                        cellText = Server.HtmlDecode(gvCustomers.HeaderRow.Cells[j].Text);
        //                    }

        //                    Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD));
        //                    table.AddCell(ph);
        //                }

        //            }

        //            for (int i = 0; i < gvCustomers.Rows.Count; i++)
        //            {
        //                string cellText = "";
        //                if (gvCustomers.Rows[i].RowType == DataControlRowType.DataRow)
        //                {
        //                    for (int j = 1; j < gvCustomers.Columns.Count; j++)
        //                    {
        //                        if (j == 1)
        //                        {
        //                            cellText = ((Label)gvCustomers.Rows[i].FindControl("lblParenteHeader")).Text;
        //                        }
        //                        else if (j == 2)
        //                        {
        //                            cellText = ((Label)gvCustomers.Rows[i].FindControl("lblCustNameHeader")).Text;
        //                        }
        //                        else if (j == 7)
        //                        {
        //                            cellText = ((Label)gvCustomers.Rows[i].FindControl("lblAreaHeader")).Text;
        //                        }
        //                        else if (j == 9)
        //                        {
        //                            cellText = ((Label)gvCustomers.Rows[i].FindControl("lblPincodeHeader")).Text;
        //                        }
        //                        else if (j == 10)
        //                        {
        //                            cellText = ((Label)gvCustomers.Rows[i].FindControl("lblAssignedRMHeader")).Text;
        //                        }
        //                        else
        //                        {
        //                            cellText = Server.HtmlDecode(gvCustomers.Rows[i].Cells[j].Text);
        //                        }
        //                        Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL));
        //                        iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
        //                        table.AddCell(ph);

        //                    }

        //                }

        //            }



        //            //Create the PDF Document

        //            Document pdfDoc = new Document(PageSize.A5, 10f, 10f, 10f, 0f);
        //            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //            pdfDoc.Open();
        //            pdfDoc.Add(table);
        //            pdfDoc.Close();
        //            Response.ContentType = "application/pdf";
        //            temp = "filename=" + temp + ".pdf";
        //            //    Response.AddHeader("content-disposition", "attachment;" + "filename=GridViewExport.pdf");
        //            Response.AddHeader("content-disposition", "attachment;" + temp);
        //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //            Response.Write(pdfDoc);
        //            Response.End();



        //        }
        //        else if (Filetype.ToLower() == "word")
        //        {
        //            gvCustomers.Columns.Remove(this.gvCustomers.Columns[0]);
        //            string temp = userVo.FirstName + userVo.LastName + "_Customer.doc";
        //            string attachment = "attachment; filename=" + temp;
        //            Response.ClearContent();
        //            Response.AddHeader("content-disposition", attachment);
        //            Response.ContentType = "application/msword";
        //            StringWriter sw = new StringWriter();
        //            HtmlTextWriter htw = new HtmlTextWriter(sw);

        //            Response.Output.Write("<table border=\"0\"><tbody><tr><td>");
        //            Response.Output.Write("Advisor Name : ");
        //            Response.Output.Write("</td>");
        //            Response.Output.Write("<td>");
        //            Response.Output.Write(userVo.FirstName + userVo.LastName);
        //            Response.Output.Write("</td></tr>");
        //            Response.Output.Write("<tr><td>");
        //            Response.Output.Write("Report  : ");
        //            Response.Output.Write("</td>");
        //            Response.Output.Write("<td>");
        //            Response.Output.Write("Customer List");
        //            Response.Output.Write("</td></tr><tr><td>");
        //            Response.Output.Write("Date : ");
        //            Response.Output.Write("</td><td>");
        //            System.DateTime tDate1 = System.DateTime.Now;
        //            Response.Output.Write(tDate1);
        //            Response.Output.Write("</td></tr>");
        //            Response.Output.Write("</tbody></table>");

        //            PrepareGridViewForExport(gvCustomers);


        //            if (gvCustomers.HeaderRow != null)
        //            {
        //                PrepareControlForExport(gvCustomers.HeaderRow);
        //            }
        //            foreach (GridViewRow row in gvCustomers.Rows)
        //            {
        //                PrepareControlForExport(row);
        //            }
        //            if (gvCustomers.FooterRow != null)
        //            {
        //                PrepareControlForExport(gvCustomers.FooterRow);
        //            }
        //            gvCustomers.Parent.Controls.Add(frm);
        //            frm.Controls.Add(gvCustomers);
        //            frm.RenderControl(htw);
        //            Response.Write(sw.ToString());
        //            Response.End();

        //        }

        //    }

        //}

    }
}
