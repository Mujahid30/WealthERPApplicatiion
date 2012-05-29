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
using System.Web.UI.HtmlControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using System.Collections;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class AllTransactions : System.Web.UI.UserControl
    {
        int index;
        CustomerVo customerVo = new CustomerVo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        MFTransactionVo mfTransactionVo = new MFTransactionVo();
        List<MFTransactionVo> mfTransactionList = null;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        int customerId;
        decimal price = 0;
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        static int portfolioId;
        DropDownList ddlStatus;
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();
        Hashtable ht = new Hashtable();
        static double totalAmount = 0;
        static double totalUnits = 0;
        static float totalsst = 0;
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
                FunctionInfo.Add("Method", "TransactionsView.ascx.cs:OnInit()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            try
            {
                GetPageCount();
                this.BindGridView(customerVo.CustomerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "TransactionsView.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[1];
                objects[0] = customerVo;
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
                FunctionInfo.Add("Method", "TransactionsView.ascx.cs:GetPageCount()");
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

        protected void Page_Load(object sender, EventArgs e)
        {     

            SessionBo.CheckSession();
            this.Page.Culture = "en-GB";
            customerVo = (CustomerVo)Session["CustomerVo"];
            customerId = customerVo.CustomerId;
            userVo = (UserVo)Session["userVo"];
            rmVo = (RMVo)Session["rmVo"];
            
            if (!IsPostBack)
            {
                mypager.CurrentPage = 1;
                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                // Bind Grid
                BindPortfolioDropDown();
                //hdnFolioFilter.Value = string.Empty;
                if (Session["tranDates"] != null)
                {
                    ht = (Hashtable)Session["tranDates"];
                    txtFromTran.Text = ht["From"].ToString();
                    txtToTran.Text = ht["To"].ToString();
                    BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                    Session.Remove("tranDates");
                }
                else
                {
                    txtFromTran.Text = DateTime.Now.ToShortDateString().ToString();
                    txtToTran.Text = DateTime.Now.ToShortDateString().ToString();
                    BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                    //ErrorMessage.Visible = true;
                    //ErrorMessage.Text = "Please select the transaction period..";
                }

            }


        }

        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();
            //ddlPortfolio.Items.Insert(0, "Select the Portfolio");

            ddlPortfolio.SelectedValue = portfolioId.ToString();

        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
        }

        private void BindGridView(int CustomerId, int CurrentPage, int export, DateTime from, DateTime to)
        {
            Dictionary<string, string> genDictTranType = new Dictionary<string, string>();
            Dictionary<string, string> genDictTranTrigger = new Dictionary<string, string>();
            Dictionary<string, string> genDictTranDate = new Dictionary<string, string>();
            totalsst = 0;
            totalAmount = 0;
            totalUnits = 0;
            if (Session["Scheme"] != null)
            {
                hdnSchemeFilter.Value = Session["Scheme"].ToString();
                Session.Remove("Scheme");
                lbBack.Visible = true;
            }
            if (Session["Folio"] != null)
            {
                hdnFolioFilter.Value = Session["Folio"].ToString();
                Session.Remove("Folio");
            }
            
                
            //ddlStatus = (DropDownList)gvMFTransactions.FindControl("ddlStatus");
            DataTable dtMFTransactions = new DataTable();
            try
            {
                if (hdnCurrentPage.Value.ToString() != "")
                {
                    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                    hdnCurrentPage.Value = "";
                }

                int Count;
                if (export == 1)
                {
                    mfTransactionList = customerTransactionBo.GetMFTransactions(CustomerId, portfolioId, 1, CurrentPage, out Count, hdnSchemeFilter.Value.Trim(), hdnTranType.Value.Trim(), hdnTranTrigger.Value.Trim(), hdnStatus.Value.Trim(), hdnTranDate.Value.Trim(), out genDictTranType, out genDictTranTrigger, out genDictTranDate, hdnSort.Value, from, to, hdnFolioFilter.Value.ToString());
                   
                }
                else
                {
                    mfTransactionList = customerTransactionBo.GetMFTransactions(CustomerId, portfolioId, 0, CurrentPage, out Count, hdnSchemeFilter.Value.Trim(), hdnTranType.Value.Trim(), hdnTranTrigger.Value.Trim(), hdnStatus.Value.Trim(), hdnTranDate.Value.Trim(), out genDictTranType, out genDictTranTrigger, out genDictTranDate, hdnSort.Value, from, to, hdnFolioFilter.Value.ToString());
                    hdnRecordCount.Value = lblTotalRows.Text = Count.ToString();
                }
                // customerTransactionBo.GetMFTransactions(customerVo.CustomerId,"C").ToString();



                if (mfTransactionList != null)
                {
                    //tblGV.Visible = true;
                    trPager.Visible = true;
                    ErrorMessage.Visible = false;
                    lblTotalRows.Visible = true;
                    lblCurrentPage.Visible = true;
                    dtMFTransactions.Columns.Add("TransactionId");
                    dtMFTransactions.Columns.Add("Folio Number");
                    dtMFTransactions.Columns.Add("Scheme Name");
                    dtMFTransactions.Columns.Add("Transaction Type");
                    dtMFTransactions.Columns.Add("Transaction Date");
                    dtMFTransactions.Columns.Add("Price");
                    dtMFTransactions.Columns.Add("Units");
                    dtMFTransactions.Columns.Add("Amount");
                    dtMFTransactions.Columns.Add("STT");
                    dtMFTransactions.Columns.Add("Transaction Status");
                    dtMFTransactions.Columns.Add("ADUL_ProcessId");
                    dtMFTransactions.Columns.Add("CMFT_SubBrokerCode");
                    dtMFTransactions.Columns.Add("PAISC_AssetInstrumentSubCategoryName");
                    DataRow drMFTransaction;
                    string transactiontype;
                    for (int i = 0; i < mfTransactionList.Count; i++)
                    {
                        
                        drMFTransaction = dtMFTransactions.NewRow();
                        mfTransactionVo = new MFTransactionVo();
                        mfTransactionVo = mfTransactionList[i];
                        drMFTransaction[0] = mfTransactionVo.TransactionId.ToString();
                        drMFTransaction[1] = mfTransactionVo.Folio.ToString();
                        drMFTransaction[2] = mfTransactionVo.SchemePlan.ToString();

                        drMFTransaction[3] = mfTransactionVo.TransactionType.ToString();
                        drMFTransaction[4] = mfTransactionVo.TransactionDate.ToShortDateString().ToString();
                        drMFTransaction[5] = decimal.Parse(mfTransactionVo.Price.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drMFTransaction[6] = mfTransactionVo.Units.ToString("f4");
                        transactiontype=customerTransactionBo.GetTransactionType(mfTransactionVo.TransactionType.ToString());
                        if (transactiontype == "B")
                        {
                            totalUnits = totalUnits + mfTransactionVo.Units;
                        }
                        else if (transactiontype == "S")
                        {
                            totalUnits = totalUnits - mfTransactionVo.Units;
                        }
                        drMFTransaction[7] = decimal.Parse(mfTransactionVo.Amount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        totalAmount = totalAmount + mfTransactionVo.Amount;
                        drMFTransaction[8] = decimal.Parse(mfTransactionVo.STT.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        totalsst = totalsst + mfTransactionVo.STT;
                        drMFTransaction[9] = mfTransactionVo.TransactionStatus.ToString();
                        if (mfTransactionVo.ProcessId == 0)
                            drMFTransaction[10] = "N/A";
                        else
                            drMFTransaction[10] = int.Parse(mfTransactionVo.ProcessId.ToString());
                        drMFTransaction[11] = mfTransactionVo.SubBrokerCode;
                        drMFTransaction[12] = mfTransactionVo.SubCategoryName;

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

                    //if (genDictTranTrigger.Count > 0)
                    //{
                    //    DropDownList ddlTranTrigger = GetTranTriggerDDL();
                    //    if (ddlTranTrigger != null)
                    //    {
                    //        ddlTranTrigger.DataSource = genDictTranTrigger;
                    //        ddlTranTrigger.DataTextField = "Key";
                    //        ddlTranTrigger.DataValueField = "Value";
                    //        ddlTranTrigger.DataBind();
                    //        ddlTranTrigger.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                    //    }

                    //    if (hdnTranTrigger.Value != "")
                    //    {
                    //        ddlTranTrigger.SelectedValue = hdnTranTrigger.Value.ToString().Trim();
                    //    }
                    //}

                    if (genDictTranDate.Count > 0)
                    {
                        DropDownList ddlTranDate = GetTranDateDDL();
                        if (ddlTranDate != null)
                        {
                            ddlTranDate.DataSource = genDictTranDate;
                            ddlTranDate.DataTextField = "Key";
                            ddlTranDate.DataValueField = "Value";
                            ddlTranDate.DataBind();
                            ddlTranDate.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }

                        if (hdnTranDate.Value != "")
                        {
                            ddlTranDate.SelectedValue = hdnTranDate.Value.ToString().Trim();
                        }
                    }

                    TextBox txtScheme = GetSchemeTextBox();
                    TextBox txtFolio = GetFolioTextBox();
                    if (txtScheme != null)
                    {
                        if (hdnSchemeFilter.Value != "")
                        {
                            txtScheme.Text = hdnSchemeFilter.Value.ToString().Trim();
                        }
                    }
                    if (txtFolio != null)
                    {
                        if (hdnFolioFilter.Value != "")
                        {
                            txtFolio.Text = hdnFolioFilter.Value.ToString().Trim();
                        }
                    }
                    GetPageCount();
                }
                else
                {
                    hdnRecordCount.Value = "0";
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
                    trPager.Visible = false;
                    ErrorMessage.Visible = true;                    
                    gvMFTransactions.DataSource = null;
                    gvMFTransactions.DataBind();
                    //tblGV.Visible = false;
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
                FunctionInfo.Add("Method", "TransactionsView.ascx:BindGridView()");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = mfTransactionList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
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

        private TextBox GetFolioTextBox()
        {
            TextBox txt = new TextBox();
            if (gvMFTransactions.HeaderRow != null)
            {
                if ((TextBox)gvMFTransactions.HeaderRow.FindControl("txtFolioSearch") != null)
                {
                    txt = (TextBox)gvMFTransactions.HeaderRow.FindControl("txtFolioSearch");
                }
            }
            else
                txt = null;

            return txt;
        }
        private DropDownList GetTranDateDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvMFTransactions.HeaderRow != null)
            {
                if ((DropDownList)gvMFTransactions.HeaderRow.FindControl("ddlTranDate") != null)
                {
                    ddl = (DropDownList)gvMFTransactions.HeaderRow.FindControl("ddlTranDate");
                }
            }
            else
                ddl = null;

            return ddl;
        }

        private DropDownList GetTranTriggerDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvMFTransactions.HeaderRow != null)
            {
                if ((DropDownList)gvMFTransactions.HeaderRow.FindControl("ddlTranTrigger") != null)
                {
                    ddl = (DropDownList)gvMFTransactions.HeaderRow.FindControl("ddlTranTrigger");
                }
            }
            else
                ddl = null;

            return ddl;
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

        protected void gvMFTransactions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMFTransactions.PageIndex = e.NewPageIndex;
            gvMFTransactions.DataBind();
        }

        protected void gvMFTransactions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Hashtable hshTranDates;
            try
            {
                if (ViewState["dtFrom"] != null)
                {
                    dtFrom = DateTime.Parse(ViewState["dtFrom"].ToString());
                }
                else if(txtFromTran.Text != "")
                {
                    dtFrom = DateTime.Parse(txtFromTran.Text);
                }

                if (ViewState["dtTo"] != null)
                {
                    dtTo = DateTime.Parse(ViewState["dtTo"].ToString());
                }
                else if (txtToTran.Text != null)
                {
                    dtTo = DateTime.Parse(txtToTran.Text);
                }

                BindGridView(customerId, mypager.CurrentPage, 0, dtFrom, dtTo);

                if (e.CommandName.ToString() != "Sort")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    int transactionId = int.Parse(gvMFTransactions.DataKeys[index].Value.ToString());
                    Session["MFTransactionVo"] = customerTransactionBo.GetMFTransaction(transactionId);

                    int month = 0;
                    int amcCode = mfTransactionList[index].AMCCode;
                    int schemeCode = mfTransactionList[index].MFCode;
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
                    string schemeName = mfTransactionList[index].SchemePlan;


                    if (e.CommandName == "Select")
                    {
                        hshTranDates = new Hashtable();
                        hshTranDates.Add("From", txtFromTran.Text);
                        hshTranDates.Add("To", txtToTran.Text);
                        Session["tranDates"] = hshTranDates;
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewMFTransaction','none');", true);
                        Session["MFEditValue"] = "Value";
                    }
                    if (e.CommandName == "NavigateToMarketData")
                    {
                        

                        Response.Redirect("ControlHost.aspx?pageid=AdminPriceList&SchemeCode=" + schemeCode + "&Year=" + year + "&Month=" + month + "&SchemeName=" + schemeName + "&AMCCode=" + amcCode + "", false);
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
                FunctionInfo.Add("Method", "TransactionsView.ascx:gvMFTransactions_RowCommand()");
                object[] objects = new object[1];
                objects[0] = index;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        foreach (GridViewRow dr in gvMFTransactions.Rows)
        //        {
        //            customerVo = (CustomerVo)Session["CustomerVo"];
        //            customerId = customerVo.CustomerId;

        //            CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
        //            if (checkBox.Checked)
        //            {
        //                int TransactionID = Convert.ToInt32(gvMFTransactions.DataKeys[dr.RowIndex].Value);
        //                if (customerTransactionBo.DeleteMFTransaction(TransactionID))
        //                {
        //                    // Success Message

        //                }
        //            }
        //        }
        //        BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "TransactionsView.ascx:btnDeleteSelected_Click()");
        //        object[] objects = new object[1];
        //        objects[0] = customerVo;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //}

        protected void gvMFTransactions_DataBound(object sender, EventArgs e)
        {

            //mfTransactionList = customerTransactionBo.GetMFTransactions(customerId);
            //gvMFTransactions.FooterRow.Cells[2].Text = "Total Records : " + mfTransactionList.Count.ToString();
            //gvMFTransactions.FooterRow.Cells[2].ColumnSpan = gvMFTransactions.FooterRow.Cells.Count - 2;
            //for (int i = 3; i < gvMFTransactions.FooterRow.Cells.Count; i++)
            //{
            //    gvMFTransactions.FooterRow.Cells[i].Visible = false;
            //}
        }

        protected void gvMFTransactions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total ";
                e.Row.Cells[8].Text = double.Parse(totalAmount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[8].Attributes.Add("align", "Right");
                e.Row.Cells[7].Text = double.Parse(totalUnits.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[7].Attributes.Add("align", "Right");
                e.Row.Cells[9].Text = double.Parse(totalsst.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[9].Attributes.Add("align", "Right");


            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                ddlStatus.SelectedValue = hdnStatus.Value;
            }
        }

        protected void btnTranSchemeSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetSchemeTextBox();
            TextBox txtFolio = GetFolioTextBox();
            if (txtName != null)
            {
                hdnSchemeFilter.Value = txtName.Text.Trim();
                BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            }

            if (txtFolio != null)
            {
                hdnFolioFilter.Value = txtFolio.Text.Trim();
                BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
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
                    BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnTranType.Value = "";
                    BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
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
                mypager.CurrentPage = 1;
                hdnCurrentPage.Value = "1";
                BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
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

        protected void ddlTranTrigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlTranTrig = GetTranTriggerDDL();

            if (ddlTranTrig != null)
            {
                if (ddlTranTrig.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnTranTrigger.Value = ddlTranTrig.SelectedValue;
                    BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnTranTrigger.Value = "";
                    BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                }
            }
        }

        protected void ddlTranDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlTranDate = GetTranDateDDL();
            //CultureInfo ci = new CultureInfo("en-GB");
            if (ddlTranDate != null)
            {
                if (ddlTranDate.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values

                    hdnTranDate.Value = Convert.ToDateTime(ddlTranDate.SelectedValue.ToString()).ToString("MM/dd/yyyy");
                    BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnTranDate.Value = "";
                    BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                }
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            gvMFTransactions.Columns[0].Visible = false;
            gvMFTransactions.Columns[1].Visible = false;
            if (rbAll.Checked)
            {
                gvMFTransactions.AllowPaging = false;
                BindGridView(customerId, mypager.CurrentPage, 1, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            }
            else if(rbCurrent.Checked)
            {
                BindGridView(customerId, int.Parse(hdnCurrentPage.Value.ToString()), 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            }

            PrepareGridViewForExport(gvMFTransactions);           
            ExportGridView("Excel");

            BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            gvMFTransactions.Columns[0].Visible = true;
            gvMFTransactions.Columns[1].Visible = true;
        }
        //protected void btnExport_Click(object sender, EventArgs e)
        //{
        //    gvMFTransactions.Columns[0].Visible = false;
        //    gvMFTransactions.Columns[1].Visible = false;
        //    if (rbtnMultiple.Checked)
        //    {
        //        gvMFTransactions.AllowPaging = false;
        //        BindGridView(customerId, mypager.CurrentPage, 1, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));

        //    }
        //    else
        //    {
        //        BindGridView(customerId, int.Parse(hdnCurrentPage.Value.ToString()), 1, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
        //    }

        //    PrepareGridViewForExport(gvMFTransactions);
        //    if (rbtnExcel.Checked)
        //    {
        //        ExportGridView("Excel");
        //    }
        //    else if (rbtnPDF.Checked)
        //    {

        //        ExportGridView("PDF");
        //    }
        //    else if (rbtnWord.Checked)
        //    {
        //        ExportGridView("Word");
        //    }
        //    else
        //    {
        //        ExportGridView("Excel");
        //    }
        //    BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
        //    gvMFTransactions.Columns[0].Visible = true;
        //    gvMFTransactions.Columns[1].Visible = true;
        //}

        protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtender1.Show();
            ModalPopupExtender1.TargetControlID = "imgBtnExport";

        }

        private void ExportGridView(string Filetype)
        {
            // float ReportTextSize = 7;
            {
                HtmlForm frm = new HtmlForm();
                System.Web.UI.WebControls.Table tbl = new System.Web.UI.WebControls.Table();
                frm.Controls.Clear();
                frm.Attributes["runat"] = "server";
                if (Filetype == "Excel")
                {
                    // gvCustomer.Columns.Remove(this.gvCustomer.Columns[0]);
                    string temp = customerVo.FirstName + customerVo.LastName + "'s_MFTransactionList.xls";
                    string attachment = "attachment; filename=" + temp;
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);


                    Response.Output.Write("<table border=\"0\"><tbody ><caption align=\"left\"><FONT FACE=\"ARIAL\"  SIZE=\"4\">Mutual Fund Transaction</FONT></caption><tr><td>");
                    Response.Output.Write("Advisor Name : ");
                    Response.Output.Write("</td>");
                    Response.Output.Write("<td>");
                    Response.Output.Write(userVo.FirstName + userVo.LastName);
                    Response.Output.Write("</td></tr>");
                    Response.Output.Write("<tr><td>");
                    Response.Output.Write("Customer Name  : ");
                    Response.Output.Write("</td>");
                    Response.Output.Write("<td>");
                    Response.Output.Write(customerVo.FirstName + customerVo.MiddleName + customerVo.LastName);
                    Response.Output.Write("</td></tr>");
                    Response.Output.Write("<tr><td>");
                    Response.Output.Write("Contact Person  : ");
                    Response.Output.Write("</td>");
                    Response.Output.Write("<td>");
                    Response.Output.Write(rmVo.FirstName + rmVo.MiddleName + rmVo.LastName);
                    Response.Output.Write("</td></tr><tr><td>");
                    Response.Output.Write("Date : ");
                    Response.Output.Write("</td><td align=\"left\">");
                    System.DateTime tDate1 = System.DateTime.Now;
                    Response.Output.Write(tDate1.ToLongDateString());
                    Response.Output.Write("</td></tr>");
                    Response.Output.Write("</tbody></table>");

                

                    if (gvMFTransactions.HeaderRow != null)
                    {
                        PrepareControlForExport(gvMFTransactions.HeaderRow);
                        //tbl.Rows.Add(gvMFTransactions.HeaderRow);
                    }
                    foreach (GridViewRow row in gvMFTransactions.Rows)
                    {
                        
                        PrepareControlForExport(row);
                        
                        //tbl.Rows.Add(row);
                    }
                    if (gvMFTransactions.FooterRow != null)
                    {
                        PrepareControlForExport(gvMFTransactions.FooterRow);
                        // tbl.Rows.Add(gvMFTransactions.FooterRow);
                    }
                    
                   // tbl.GridLines = GridLines.Both;

                    ////tbl.Controls.Add(frm);
                    ////frm.Controls.Add(tbl);
                    //tbl.RenderControl(htw);
                    gvMFTransactions.Parent.Controls.Add(frm);
                    frm.Controls.Add(gvMFTransactions);
                    frm.RenderControl(htw);

                    Response.Write(sw.ToString());
                    Response.End();


                }
                //else if (Filetype == "PDF")
                //{
                //    string temp = customerVo.FirstName + customerVo.LastName + "'s_MFTransactionList";

                //    gvMFTransactions.AllowPaging = false;
                //    gvMFTransactions.DataBind();
                //    iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gvMFTransactions.Columns.Count - 3);

                //    table.HeaderRows = 4;
                //    iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);

                //    Phrase phApplicationName = new Phrase("WWW.PrincipalConsulting.net", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
                //    PdfPCell clApplicationName = new PdfPCell(phApplicationName);
                //    clApplicationName.Border = PdfPCell.NO_BORDER;
                //    clApplicationName.HorizontalAlignment = Element.ALIGN_LEFT;



                //    Phrase phDate = new Phrase(DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
                //    PdfPCell clDate = new PdfPCell(phDate);
                //    clDate.HorizontalAlignment = Element.ALIGN_RIGHT;
                //    clDate.Border = PdfPCell.NO_BORDER;


                //    headerTable.AddCell(clApplicationName);
                //    headerTable.AddCell(clDate);
                //    headerTable.DefaultCell.Border = PdfPCell.NO_BORDER;


                //    PdfPCell cellHeader = new PdfPCell(headerTable);
                //    cellHeader.Border = PdfPCell.NO_BORDER;
                //    cellHeader.Colspan = gvMFTransactions.Columns.Count - 3;
                //    table.AddCell(cellHeader);

                //    Phrase phHeader = new Phrase(temp, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                //    PdfPCell clHeader = new PdfPCell(phHeader);
                //    clHeader.Colspan = gvMFTransactions.Columns.Count - 3;
                //    clHeader.Border = PdfPCell.NO_BORDER;
                //    clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                //    table.AddCell(clHeader);


                //    Phrase phSpace = new Phrase("\n");
                //    PdfPCell clSpace = new PdfPCell(phSpace);
                //    clSpace.Border = PdfPCell.NO_BORDER;
                //    clSpace.Colspan = gvMFTransactions.Columns.Count - 3;
                //    table.AddCell(clSpace);

                //    GridViewRow HeaderRow = gvMFTransactions.HeaderRow;
                //    if (HeaderRow != null)
                //    {
                //        string cellText = "";
                //        for (int j = 3; j < gvMFTransactions.Columns.Count; j++)
                //        {
                //            if (j == 3)
                //            {
                //                cellText = "Folio Number";
                //            }
                //            else if (j == 4)
                //            {
                //                cellText = "Scheme Name";
                //            }
                //            else if (j == 5)
                //            {
                //                cellText = "Transaction Type";
                //            }

                //            else if (j == 6)
                //            {
                //                cellText = "Transaction Date";
                //            }
                //            else if (j == 7)
                //            {
                //                cellText = "Price (Rs)";
                //            }
                //            else if (j == 8)
                //            {
                //                cellText = "Units";
                //            }
                //            else if (j == 9)
                //            {
                //                cellText = "Amount (Rs)";
                //            }


                //            else if (j == 10)
                //            {
                //                cellText = "STT (Rs)";
                //            }

                //            else
                //            {
                //                cellText = Server.HtmlDecode(gvMFTransactions.HeaderRow.Cells[j].Text);
                //            }
                //            Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD));
                //            table.AddCell(ph);
                //        }

                //    }


                //    for (int i = 0; i < gvMFTransactions.Rows.Count; i++)
                //    {
                //        string cellText = "";
                //        if (gvMFTransactions.Rows[i].RowType == DataControlRowType.DataRow)
                //        {

                //            for (int j = 3; j < gvMFTransactions.Columns.Count; j++)
                //            {
                //                //if ((Label)gvMFTransactions.Rows[i].Cells[j].FindControl("lblSchemeHeader")!=null)
                //                if (j == 4)
                //                {

                //                    cellText = ((Label)gvMFTransactions.Rows[i].FindControl("lblSchemeHeader")).Text;
                //                }
                //                else if (j == 5)
                //                {

                //                    cellText = ((Label)gvMFTransactions.Rows[i].FindControl("lblTranTypeHeader")).Text;
                //                }

                //                else if (j == 6)
                //                {
                //                    cellText = ((Label)gvMFTransactions.Rows[i].FindControl("lblTranDateHeader")).Text;
                //                }
                //                else
                //                {
                //                    cellText = gvMFTransactions.Rows[i].Cells[j].Text;
                //                }


                //                Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL));
                //                iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
                //                table.AddCell(ph);

                //            }

                //        }

                //    }



                //    //Create the PDF Document

                //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                //    pdfDoc.Open();
                //    pdfDoc.Add(table);
                //    pdfDoc.Close();
                //    Response.ContentType = "application/pdf";
                //    temp = "filename=" + temp + ".pdf";
                //    //    Response.AddHeader("content-disposition", "attachment;" + "filename=GridViewExport.pdf");
                //    Response.AddHeader("content-disposition", "attachment;" + temp);
                //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //    Response.Write(pdfDoc);
                //    Response.End();



                //}
                //else if (Filetype == "Word")
                //{
                //    gvMFTransactions.Columns.Remove(this.gvMFTransactions.Columns[0]);
                //    string temp = customerVo.FirstName + customerVo.LastName + "'s_MFTransactionList.doc";
                //    string attachment = "attachment; filename=" + temp;
                //    Response.ClearContent();
                //    Response.AddHeader("content-disposition", attachment);
                //    Response.ContentType = "application/msword";
                //    StringWriter sw = new StringWriter();
                //    HtmlTextWriter htw = new HtmlTextWriter(sw);

                //    Response.Output.Write("<table border=\"0\"><tbody><caption><FONT FACE=\"ARIAL\" SIZE=\"4\">Mutual Fund Transaction</FONT></caption><tr><td>");
                //    Response.Output.Write("Advisor Name : ");
                //    Response.Output.Write("</td>");
                //    Response.Output.Write("<td>");
                //    Response.Output.Write(userVo.FirstName + userVo.LastName);
                //    Response.Output.Write("</td></tr>");
                //    Response.Output.Write("<tr><td>");
                //    Response.Output.Write("Customer Name  : ");
                //    Response.Output.Write("</td>");
                //    Response.Output.Write("<td>");
                //    Response.Output.Write(customerVo.FirstName + customerVo.MiddleName + customerVo.LastName);
                //    Response.Output.Write("</td></tr>");
                //    Response.Output.Write("<tr><td>");
                //    Response.Output.Write("Contact Person  : ");
                //    Response.Output.Write("</td>");
                //    Response.Output.Write("<td>");
                //    Response.Output.Write(rmVo.FirstName + rmVo.MiddleName + rmVo.LastName);
                //    Response.Output.Write("</td></tr><tr><td>");
                //    Response.Output.Write("Date : ");
                //    Response.Output.Write("</td><td>");
                //    System.DateTime tDate1 = System.DateTime.Now;
                //    Response.Output.Write(tDate1);
                //    Response.Output.Write("</td></tr>");
                //    Response.Output.Write("</tbody></table>");
                //    if (gvMFTransactions.HeaderRow != null)
                //    {
                //        PrepareControlForExport(gvMFTransactions.HeaderRow);
                //    }
                //    foreach (GridViewRow row in gvMFTransactions.Rows)
                //    {
                //        PrepareControlForExport(row);
                //    }
                //    if (gvMFTransactions.FooterRow != null)
                //    {
                //        PrepareControlForExport(gvMFTransactions.FooterRow);
                //    }
                //    gvMFTransactions.Parent.Controls.Add(frm);
                //    frm.Controls.Add(gvMFTransactions);
                //    frm.RenderControl(htw);
                //    Response.Write(sw.ToString());
                //    Response.End();

                //}

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
                    gv.Controls.AddAt(i,l);
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

        //private void ShowPdf(string strS)
        //{
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader
        //    ("Content-Disposition", "attachment; filename=" + strS);
        //    Response.TransmitFile(strS);
        //    Response.End();
        //    Response.Flush();
        //    Response.Clear();

        //}

        //protected void btnPrint_Click(object sender, EventArgs e)
        //{
        //    gvMFTransactions.Columns[0].Visible = false;
        //    gvMFTransactions.Columns[1].Visible = false;
        //    if (rbtnMultiple.Checked)
        //    {
        //        BindGridView(customerId, mypager.CurrentPage, 1, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
        //    }
        //    else
        //    {
        //        BindGridView(customerId, int.Parse(hdnCurrentPage.Value.ToString()), 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
        //    }
        //    PrepareGridViewForExport(gvMFTransactions);
        //    if (gvMFTransactions.HeaderRow != null)
        //    {
        //        PrepareControlForExport(gvMFTransactions.HeaderRow);
        //    }
        //    foreach (GridViewRow row in gvMFTransactions.Rows)
        //    {
        //        PrepareControlForExport(row);
        //    }
        //    if (gvMFTransactions.FooterRow != null)
        //    {
        //        PrepareControlForExport(gvMFTransactions.FooterRow);
        //    }
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_TransactionsView_tbl','ctrl_TransactionsView_btnPrintGrid');", true);

        //}

        //protected void btnPrintGrid_Click(object sender, EventArgs e)
        //{
        //    BindGridView(customerId, int.Parse(hdnCurrentPage.Value.ToString()), 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
        //    gvMFTransactions.Columns[0].Visible = true;
        //    gvMFTransactions.Columns[1].Visible = true;
        //}

        protected void gvMFTransactions_Sort(object sender, GridViewSortEventArgs e)
        {
            customerVo = (CustomerVo)Session["CustomerVo"];
            customerId = customerVo.CustomerId;

            string sortExpression = null;
            try
            {
                sortExpression = e.SortExpression;
                ViewState["sortExpression"] = sortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    hdnSort.Value = sortExpression + " DESC";
                    this.BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    hdnSort.Value = sortExpression + " ASC";
                    this.BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));

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

                FunctionInfo.Add("Method", "TransactionsView.ascx.cs:gvMFTransactions_Sort()");

                object[] objects = new object[1];
                objects[0] = sortExpression;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void txtToTran_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnViewTran_Click(object sender, EventArgs e)
        {
            
            dtFrom = DateTime.Parse(txtFromTran.Text);
            ViewState["dtFrom"] = dtFrom;
            dtTo = DateTime.Parse(txtToTran.Text);
            ViewState["dtTo"] = dtTo;
            hdnStatus.Value = "1";
            BindGridView(customerId, mypager.CurrentPage, 0, dtFrom, dtTo);
        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewMutualFundPortfolio','none');", true);

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

        }
    }
}