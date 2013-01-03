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
    public partial class RMMultipleTransactionView : System.Web.UI.UserControl
    {

        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        string userType;
        string path = string.Empty;
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        UserVo userVo = new UserVo();
        int GroupHead = 0;
        int customerId=0;
        List<MFTransactionVo> mfTransactionList = null;
        MFTransactionVo mfTransactionVo = new MFTransactionVo();
        DateTime dtTo = new DateTime();
        DateBo dtBo = new DateBo();
        DateTime dtFrom = new DateTime();
        static DateTime convertedFromDate = new DateTime();
        static DateTime convertedToDate = new DateTime();
        static double totalAmount = 0;
        static double totalUnits = 0;
        int PasssedFolioValue = 0;
        bool GridViewCultureFlag = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                userVo = (UserVo)Session["userVo"];

                advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                rmVo = (RMVo)Session[SessionContents.RmVo];

                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
                    userType = "advisor";

                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                    userType = "rm";
                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "Customer")
                    userType = "Customer";
                else
                    userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

                customerVo = (CustomerVo)Session["CustomerVo"];
                if (Session["CustomerVo"] != null)
                {
                    customerId = customerVo.CustomerId;
                    trRangeNcustomer.Visible = false;
                    trRange.Visible = false;
                }

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
                    //tdGroupHead.Visible = false;
                    //lblGroupHead.Visible = false;
                    //txtParentCustomer.Visible = false;
                    trGroupHead.Visible = false;
                    hdnProcessIdSearch.Value = "0";
                    Panel2.Visible = false;


                    if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                    {
                        txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                    {
                        txtParentCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";

                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                    {
                        txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetBMParentCustomerNames";

                    }

                    if (Request.QueryString["folionum"] != null)
                    {
                        int accountId = int.Parse(Request.QueryString["folionum"].ToString());
                        PasssedFolioValue = accountId;


                        BindLastTradeDate();
                        string fromdate = "01-01-1990";
                        txtFromDate.SelectedDate = DateTime.Parse(fromdate);
                    }
                    else
                    {
                        BindLastTradeDate();
                    }

                    BindGrid( DateTime.Parse(txtFromDate.SelectedDate.ToString()), DateTime.Parse(txtToDate.SelectedDate.ToString()));
                    ErrorMessage.Visible = false;
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
            txtFromDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0][0].ToString());
            txtToDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0][0].ToString());

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
                //lblGroupHead.Visible = false;
                //txtParentCustomer.Visible = false;
                //tdGroupHead.Visible = false;
                trGroupHead.Visible = false;

            }
            else if (rbtnGroup.Checked == true)
            {
                //lblGroupHead.Visible = true;
                //txtParentCustomer.Visible = true;
                //tdGroupHead.Visible = true;
                trGroupHead.Visible = true;
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
            hdnProcessIdSearch.Value = "0";
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
            if (Session["CustomerVo"] != null)
            {
                trRange.Visible = true;
            }
            BindGrid(convertedFromDate, convertedToDate);
            
        }

        private void BindGrid(DateTime convertedFromDate, DateTime convertedToDate)
            {
            //Dictionary<string, string> genDictTranType = new Dictionary<string, string>();
            //Dictionary<string, string> genDictCategory = new Dictionary<string, string>();
            //Dictionary<string, int> genDictAMC = new Dictionary<string, int>();
            DataSet ds = new DataSet();
            //int Count = 0;
            //totalAmount = 0;
            //totalUnits = 0;
            int rmID = 0;
            int AdviserId = 0;

            if (userType == "advisor" || userType == "ops")
                AdviserId = advisorVo.advisorId;
            else
                rmID = rmVo.RMId;

            if(!string.IsNullOrEmpty(txtParentCustomerId.Value.ToString().Trim()))
                customerId = int.Parse(txtParentCustomerId.Value);
            try
            {//pramod
                   if (rbtnGroup.Checked)
                    {
                        mfTransactionList = customerTransactionBo.GetRMCustomerMFTransactions(rmID, AdviserId, customerId, convertedFromDate, convertedToDate, int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()),PasssedFolioValue);
                    }
                    else
                    {
                        mfTransactionList = customerTransactionBo.GetRMCustomerMFTransactions(rmID, AdviserId, 0, convertedFromDate, convertedToDate, int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()), PasssedFolioValue);
                    }

                
                if (mfTransactionList.Count != 0)
                {
                    ErrorMessage.Visible = false;
                    Panel2.Visible = true;
                    DataTable dtMFTransactions = new DataTable();

                    dtMFTransactions.Columns.Add("TransactionId");
                    dtMFTransactions.Columns.Add("Customer Name");
                    dtMFTransactions.Columns.Add("Folio Number");                    
                    dtMFTransactions.Columns.Add("Scheme Name");
                    dtMFTransactions.Columns.Add("Transaction Type");
                    dtMFTransactions.Columns.Add("Transaction Date");
                    dtMFTransactions.Columns.Add("Price", typeof(double));
                    dtMFTransactions.Columns.Add("Units", typeof(double));
                    dtMFTransactions.Columns.Add("Amount", typeof(double));
                    dtMFTransactions.Columns.Add("STT", typeof(double));
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

                    GridBoundColumn gbcCustomer = gvMFTransactions.MasterTableView.Columns.FindByUniqueName("Customer Name") as GridBoundColumn;
                    if (Session["CustomerVo"] != null)
                    {
                        gbcCustomer.Visible = false;
                    }
                    else
                        gbcCustomer.Visible = true;
                    gvMFTransactions.DataSource = dtMFTransactions;
                    gvMFTransactions.DataBind();
                    Panel2.Visible = true;
                    gvMFTransactions.Visible = true;
                    if (Cache["ViewTransaction" + userVo.UserId] == null)
                    {
                        Cache.Insert("ViewTransaction" + userVo.UserId, dtMFTransactions);
                    }
                    else
                    {
                        Cache.Remove("ViewTransaction" + userVo.UserId);
                        Cache.Insert("ViewTransaction" + userVo.UserId, dtMFTransactions);
                    }
                    btnTrnxExport.Visible = true;
                }
                else
                {
                    gvMFTransactions.Visible = false;
                    hdnRecordCount.Value = "0";
                    ErrorMessage.Visible = true;
                    Panel2.Visible = false;
                    btnTrnxExport.Visible = false;
                }

            }
            catch (Exception e)
            {
            }
        }

        protected void gvMFTransactions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton lnkView = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkView.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            int transactionId = int.Parse((gvMFTransactions.MasterTableView.DataKeyValues[selectedRow - 1]["TransactionId"].ToString()));
            mfTransactionVo = customerTransactionBo.GetMFTransaction(transactionId);
            Session["MFTransactionVo"] = mfTransactionVo;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMCustomerIndividualDashboard", "loadcontrol('ViewMFTransaction','login');", true);
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewMFTransaction','none');", true);
        }
        protected void gvMFTransactions_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Scheme")
            {
                LinkButton lnkScheme = (LinkButton)e.Item.FindControl("lnkprAmc");
                GridDataItem gdi;
                gdi = (GridDataItem)lnkScheme.NamingContainer;
                int selectedRow = gdi.ItemIndex + 1;
                int transactionId = int.Parse((gvMFTransactions.MasterTableView.DataKeyValues[selectedRow - 1]["TransactionId"].ToString()));
                mfTransactionVo = customerTransactionBo.GetMFTransaction(transactionId);
                int month = 0;
                int amcCode = mfTransactionVo.AMCCode;
                int schemeCode = mfTransactionVo.MFCode;
                int year = 0;

                if (DateTime.Now.Month != 1)
                {
                    month = DateTime.Now.Month - 1;
                    year = DateTime.Now.Year;
                }
                else
                {
                    month = 12;
                    year = DateTime.Now.Year - 1;
                }
                string schemeName = mfTransactionVo.SchemePlan;
                Response.Redirect("ControlHost.aspx?pageid=AdminPriceList&SchemeCode=" + schemeCode + "&Year=" + year + "&Month=" + month + "&SchemeName=" + schemeName + "&AMCCode=" + amcCode + "", false);
            }
        }
        protected void gvMFTransactions_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtMFTransactions = new DataTable();
            dtMFTransactions = (DataTable)Cache["ViewTransaction" + userVo.UserId];
            gvMFTransactions.DataSource = dtMFTransactions;
            gvMFTransactions.Visible = true;
        }
        protected void lbBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewMutualFundPortfolio','none');", true);

        }

         protected void btnTrnxExport_Click(object sender, ImageClickEventArgs e)
        {
            gvMFTransactions.ExportSettings.OpenInNewWindow = true;
            gvMFTransactions.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvMFTransactions.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvMFTransactions.MasterTableView.ExportToExcel();
        }
        

    }
}
