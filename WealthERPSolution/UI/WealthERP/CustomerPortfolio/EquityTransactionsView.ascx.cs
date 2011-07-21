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
        protected override void OnInit(EventArgs e)
        {
            try
            {
                ((Pager)Pager1).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
                Pager1.EnableViewState = true;
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
                FunctionInfo.Add("Method", "EquityTransactionsView.ascx.cs:OnInit()");
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
                this.BindGridView(customerVo.CustomerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EquityTransactionsView.ascx.cs:HandlePagerEvent()");
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
                    ratio = rowCount / 30;
                    Pager1.PageCount = rowCount % 30 == 0 ? ratio : ratio + 1;
                    Pager1.Set_Page(Pager1.CurrentPage, Pager1.PageCount);
                    lowerlimit = (((Pager1.CurrentPage - 1) * 30)+1).ToString();
                    upperlimit = (Pager1.CurrentPage * 30).ToString();
                    if (Pager1.CurrentPage == Pager1.PageCount)
                        upperlimit = hdnRecordCount.Value;
                    PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;

                    hdnCurrentPage.Value = Pager1.CurrentPage.ToString();
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
                        txtFromTran.Text = ht["From"].ToString();
                        txtToTran.Text = ht["To"].ToString();
                        BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                        Session.Remove("tranDates");
                    }
                    else
                    {
                        txtFromTran.Text = DateTime.Now.ToShortDateString().ToString();
                        txtToTran.Text = DateTime.Now.ToShortDateString().ToString();
                        BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));

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

        private void BindGridView(int customerId, int CurrentPage, int export, DateTime from, DateTime to)
        {
            string type, mode;

            DataSet dsExange = new DataSet(); ;
            DataTable dtEquityTransactions = new DataTable();

            Dictionary<string, string> genDictTranType = new Dictionary<string, string>();
            Dictionary<string, string> genDictExchange = new Dictionary<string, string>();
            Dictionary<string, string> genDictTradeDate = new Dictionary<string, string>();

            try
            {
                if (hdnCurrentPage.Value.ToString() != "")
                {
                    Pager1.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                    hdnCurrentPage.Value = "";
                }

                int Count;
                if (export == 1)
                {
                    equityTransactionList = customerTransactionBo.GetEquityTransactions(customerId, portfolioId, 1, CurrentPage, out Count, hdnScripFilter.Value.Trim(), hdnTradeNum.Value.Trim(), hdnTranType.Value.Trim(), hdnExchange.Value.Trim(), hdnTradeDate.Value.Trim(), out genDictTranType, out genDictExchange, out genDictTradeDate,hdnSort.Value, from, to);
                }
                else
                {
                    equityTransactionList = customerTransactionBo.GetEquityTransactions(customerId, portfolioId, 0, CurrentPage, out Count, hdnScripFilter.Value.Trim(), hdnTradeNum.Value.Trim(), hdnTranType.Value.Trim(), hdnExchange.Value.Trim(), hdnTradeDate.Value.Trim(), out genDictTranType, out genDictExchange, out genDictTradeDate, hdnSort.Value, from, to);
                    hdnRecordCount.Value = lblTotalRows.Text = Count.ToString();
                }

                if (equityTransactionList != null)
                {
                   // lblMsg.Visible = false;
                    ErrorMessage.Visible = false;
                    tblGv.Visible = true;
                    dtEquityTransactions.Columns.Add("TransactionId");
                    dtEquityTransactions.Columns.Add("TradeNum");
                    dtEquityTransactions.Columns.Add("Scheme Name");
                    dtEquityTransactions.Columns.Add("Transaction Type");
                    dtEquityTransactions.Columns.Add("Exchange");
                    dtEquityTransactions.Columns.Add("TradeDate");
                    dtEquityTransactions.Columns.Add("Rate");
                    dtEquityTransactions.Columns.Add("Quantity");
                    dtEquityTransactions.Columns.Add("Brokerage");
                    dtEquityTransactions.Columns.Add("TradeTotal");
                    dtEquityTransactions.Columns.Add("OtherCharges");

                    DataRow drEquityTransaction;

                    for (int i = 0; i < equityTransactionList.Count; i++)
                    {
                        drEquityTransaction = dtEquityTransactions.NewRow();
                        equityTransactionVo = new EQTransactionVo();
                        equityTransactionVo = equityTransactionList[i];
                        drEquityTransaction[0] = equityTransactionVo.TransactionId.ToString();
                        drEquityTransaction[1] = equityTransactionVo.TradeNum.ToString();
                        drEquityTransaction[2] = equityTransactionVo.ScripName.ToString();
                        //if (equityTransactionVo.BuySell.ToString() == "B")
                        //{
                        //    if (Session["Holdings"] != null)
                        //    {
                        //        type = "Holdings";
                        //        Session.Remove("Holdings");
                        //        if (Session["Holdings"] == null)
                        //        {
 
                        //        }
                        //    }
                        //    else
                        //    {
                        //        type = "Buy";
                        //    }
                        //}
                        //else
                        //{
                        //    //drEquityTransaction[1] = "SELL";
                        //    type = "Sell";
                        //}
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
                        drEquityTransaction[3] = equityTransactionVo.TransactionType + "/" + mode;
                        drEquityTransaction[4] = equityTransactionVo.Exchange.ToString();
                        //  dsExange.Tables[0].Rows[i][0] = equityTransactionVo.Exchange;
                        drEquityTransaction[5] = equityTransactionVo.TradeDate.ToShortDateString().ToString();
                        drEquityTransaction[6] =  decimal.Parse(equityTransactionVo.Rate.ToString()).ToString("n2",System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drEquityTransaction[7] = equityTransactionVo.Quantity.ToString("f0");
                        drEquityTransaction[8] =  (decimal.Parse(equityTransactionVo.Brokerage.ToString()) * decimal.Parse(equityTransactionVo.Quantity.ToString("f0").ToString())).ToString("n2",System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drEquityTransaction[9] =  decimal.Parse(equityTransactionVo.TradeTotal.ToString()).ToString("n2",System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drEquityTransaction[10] = (decimal.Parse(equityTransactionVo.OtherCharges.ToString()) * decimal.Parse(equityTransactionVo.Quantity.ToString("f0").ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                        dtEquityTransactions.Rows.Add(drEquityTransaction);
                    }

                    gvEquityTransactions.DataSource = dtEquityTransactions;
                    gvEquityTransactions.DataBind();
                    gvEquityTransactions.Visible = true;

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

                    if (genDictExchange.Count > 0)
                    {
                        DropDownList ddlExchange = GetExchangeDDL();
                        if (ddlExchange != null)
                        {
                            ddlExchange.DataSource = genDictExchange;
                            ddlExchange.DataTextField = "Key";
                            ddlExchange.DataValueField = "Value";
                            ddlExchange.DataBind();
                            ddlExchange.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }

                        if (hdnExchange.Value != "")
                        {
                            ddlExchange.SelectedValue = hdnExchange.Value.ToString().Trim();
                        }
                    }

                    if (genDictTradeDate.Count > 0)
                    {
                        DropDownList ddlTradeDate = GetTradeDateDDL();
                        if (ddlTradeDate != null)
                        {
                            ddlTradeDate.DataSource = genDictTradeDate;
                            ddlTradeDate.DataTextField = "Key";
                            ddlTradeDate.DataValueField = "Value";
                            ddlTradeDate.DataBind();
                            ddlTradeDate.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }

                        if (hdnTradeDate.Value != "")
                        {
                            ddlTradeDate.SelectedValue = hdnTradeDate.Value.ToString().Trim();
                        }
                    }

                    TextBox txtScrip = GetScripTextBox();
                    if (txtScrip != null)
                    {
                        if (hdnScripFilter.Value != "")
                        {
                            txtScrip.Text = hdnScripFilter.Value.ToString().Trim();
                        }
                    }

                    TextBox txtTradeNum = GetTradeNumTextBox();
                    if (txtTradeNum != null)
                    {
                        if (hdnTradeNum.Value != "")
                        {
                            txtTradeNum.Text = hdnTradeNum.Value.ToString().Trim();
                        }
                    }

                    GetPageCount();
                }
                else
                {
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
                    trPager.Visible = false;
                    ErrorMessage.Visible = true;
                    gvEquityTransactions.DataSource = null;
                    gvEquityTransactions.DataBind();
                    tblGv.Visible = false;
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

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        foreach (GridViewRow dr in gvEquityTransactions.Rows)
        //        {
        //            customerVo = (CustomerVo)Session["CustomerVo"];
        //            customerId = customerVo.CustomerId;

        //            CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
        //            if (checkBox.Checked)
        //            {
        //                int TransactionID = Convert.ToInt32(gvEquityTransactions.DataKeys[dr.RowIndex].Value);
        //                customerTransactionBo.DeleteEQTransaction(TransactionID);
        //            }
        //        }
        //        BindGridView(customerId, Pager1.CurrentPage, 0,dtFrom,dtTo);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "EquityTransactionsView.ascx:btnDeleteSelected_Click()");
        //        object[] objects = new object[2];
        //        objects[0] = customerId;
        //        objects[1] = customerVo;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        protected void gvEquityTransactions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Hashtable hshTranDates;
            try
            {
                if (e.CommandName.ToString() != "Sort")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    int transactionId = int.Parse(gvEquityTransactions.DataKeys[index].Value.ToString());
                    Session["EquityTransactionVo"] = customerTransactionBo.GetEquityTransaction(transactionId);

                    if (e.CommandName == "Select")
                    {
                        hshTranDates = new Hashtable();
                        hshTranDates.Add("From", txtFromTran.Text);
                        hshTranDates.Add("To", txtToTran.Text);
                        Session["tranDates"] = hshTranDates;
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewEquityTransaction','none');", true);
                        Session["EQUITYEditValue"] = "Value";
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
                FunctionInfo.Add("Method", "EquityTransactionsView.ascx:gvEquityTransactions_RowCommand()");
                object[] objects = new object[1];
                objects[0] = index;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvEquityTransactions_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected DataSet BindExchange()
        {

            DataSet ds;
            try
            {
                ds = customerTransactionBo.PopulateDDExchange(int.Parse(ddlPortfolio.SelectedItem.Value.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EquityTransactionsView.ascx:BindExchange()");
                object[] objects = new object[1];
                objects[0] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;

        }

        protected void gvEquityTransactions_DataBound(object sender, EventArgs e)
        {
            Dictionary<string, string> genDictTranType = new Dictionary<string, string>();
            Dictionary<string, string> genDictExchange = new Dictionary<string, string>();
            Dictionary<string, string> genDictTradeDate = new Dictionary<string, string>();

            int Count;

            try
            {
                equityTransactionList = customerTransactionBo.GetEquityTransactions(customerId, portfolioId, 0, Pager1.CurrentPage, out Count, hdnScripFilter.Value.Trim(), hdnTradeNum.Value.Trim(), hdnTranType.Value.Trim(), hdnExchange.Value.Trim(), hdnTradeDate.Value.Trim(), out genDictTranType, out genDictExchange, out genDictTradeDate, hdnSort.Value, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                if (equityTransactionList != null)
                {
                    gvEquityTransactions.FooterRow.Cells[1].Text = "Total Records : " + equityTransactionList.Count.ToString();

                    gvEquityTransactions.FooterRow.Cells[1].ColumnSpan = gvEquityTransactions.FooterRow.Cells.Count - 1;
                    gvEquityTransactions.FooterRow.Cells[1].Attributes.Add("align", "right");

                    for (int i = 2; i < gvEquityTransactions.FooterRow.Cells.Count; i++)
                    {
                        gvEquityTransactions.FooterRow.Cells[i].Visible = false;
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
                FunctionInfo.Add("Method", "EquityTransactionsView.ascx:gvEquityTransactions_DataBound()");
                object[] objects = new object[1];
                objects[0] = customerId;
                objects[1] = equityTransactionList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
        }

        protected void btnScripSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetScripTextBox();

            if (txtName != null)
            {
                hdnScripFilter.Value = txtName.Text.Trim();
                BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            }
        }

        protected void btnTradeNumSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetTradeNumTextBox();

            if (txtName != null)
            {
                hdnTradeNum.Value = txtName.Text.Trim();
                BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
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
                    BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnTranType.Value = "";
                    BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                }
            }
        }

        protected void ddlExchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlExchange = GetExchangeDDL();

            if (ddlExchange != null)
            {
                if (ddlExchange.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnExchange.Value = ddlExchange.SelectedValue;
                    BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnExchange.Value = "";
                    BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                }
            }
        }

        protected void ddlTradeDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlTradeDate = GetTradeDateDDL();

            if (ddlTradeDate != null)
            {
                if (ddlTradeDate.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnTradeDate.Value =DateTime.Parse( ddlTradeDate.SelectedValue).ToString("MM/dd/yyyy");
                    BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnTradeDate.Value = "";
                    BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                }
            }
        }

        private TextBox GetScripTextBox()
        {
            TextBox txt = new TextBox();
            if (gvEquityTransactions.HeaderRow != null)
            {
                if ((TextBox)gvEquityTransactions.HeaderRow.FindControl("txtScripSearch") != null)
                {
                    txt = (TextBox)gvEquityTransactions.HeaderRow.FindControl("txtScripSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetTradeNumTextBox()
        {
            TextBox txt = new TextBox();
            if (gvEquityTransactions.HeaderRow != null)
            {
                if ((TextBox)gvEquityTransactions.HeaderRow.FindControl("txtTradeNumSearch") != null)
                {
                    txt = (TextBox)gvEquityTransactions.HeaderRow.FindControl("txtTradeNumSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private DropDownList GetTranTypeDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvEquityTransactions.HeaderRow != null)
            {
                if ((DropDownList)gvEquityTransactions.HeaderRow.FindControl("ddlTranType") != null)
                {
                    ddl = (DropDownList)gvEquityTransactions.HeaderRow.FindControl("ddlTranType");
                }
            }
            else
                ddl = null;

            return ddl;
        }

        private DropDownList GetExchangeDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvEquityTransactions.HeaderRow != null)
            {
                if ((DropDownList)gvEquityTransactions.HeaderRow.FindControl("ddlExchange") != null)
                {
                    ddl = (DropDownList)gvEquityTransactions.HeaderRow.FindControl("ddlExchange");
                }
            }
            else
                ddl = null;

            return ddl;
        }

        private DropDownList GetTradeDateDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvEquityTransactions.HeaderRow != null)
            {
                if ((DropDownList)gvEquityTransactions.HeaderRow.FindControl("ddlTradeDate") != null)
                {
                    ddl = (DropDownList)gvEquityTransactions.HeaderRow.FindControl("ddlTradeDate");
                }
            }
            else
                ddl = null;

            return ddl;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvEquityTransactions.Columns[0].Visible = false;
            gvEquityTransactions.Columns[1].Visible = false;
            if (rbtnMultiple.Checked)
            {

                BindGridView(customerId, Pager1.CurrentPage, 1, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));

            }
            else
            {
                BindGridView(customerId, int.Parse(hdnCurrentPage.Value.ToString()), 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            }
            PrepareGridViewForExport(gvEquityTransactions);
            if (rbtnExcel.Checked)
            {
                ExportGridView("Excel");
            }
            else if (rbtnPDF.Checked)
            {

                ExportGridView("PDF");
            }
            else if (rbtnWord.Checked)
            {
                ExportGridView("Word");
            }
            BindGridView(customerId, int.Parse(hdnCurrentPage.Value.ToString()), 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            gvEquityTransactions.Columns[0].Visible = true;
            gvEquityTransactions.Columns[1].Visible = true;
        }

        private void ExportGridView(string Filetype)
        {

            {
                HtmlForm frm = new HtmlForm();
                frm.Controls.Clear();
                frm.Attributes["runat"] = "server";
                if (Filetype == "Excel")
                {

                    string temp = customerVo.FirstName + customerVo.LastName + "'s_EquityTransactionList.xls";
                    string attachment = "attachment; filename=" + temp;
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);


                    Response.Output.Write("<table border=\"0\"><tbody><caption><FONT FACE=\"ARIAL\"  SIZE=\"4\">Equity Transactions</FONT></caption><tr><td>");
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
                    Response.Output.Write("</td><td>");
                    System.DateTime tDate1 = System.DateTime.Now;
                    Response.Output.Write(tDate1.ToLongDateString());
                    Response.Output.Write("</td></tr>");
                    Response.Output.Write("</tbody></table>");

                    if (gvEquityTransactions.HeaderRow != null)
                    {
                        PrepareControlForExport(gvEquityTransactions.HeaderRow);
                    }
                    foreach (GridViewRow row in gvEquityTransactions.Rows)
                    {
                        PrepareControlForExport(row);
                    }
                    if (gvEquityTransactions.FooterRow != null)
                    {
                        PrepareControlForExport(gvEquityTransactions.FooterRow);
                    }

                    gvEquityTransactions.Parent.Controls.Add(frm);
                    frm.Controls.Add(gvEquityTransactions);
                    frm.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();


                }
                else if (Filetype == "PDF")
                {
                    string temp = customerVo.FirstName + customerVo.LastName + "'s_EquityTransactionList";

                    gvEquityTransactions.AllowPaging = false;
                    gvEquityTransactions.DataBind();
                    iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gvEquityTransactions.Columns.Count - 2);

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
                    cellHeader.Colspan = gvEquityTransactions.Columns.Count - 2;
                    table.AddCell(cellHeader);

                    Phrase phHeader = new Phrase(temp, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                    PdfPCell clHeader = new PdfPCell(phHeader);
                    clHeader.Colspan = gvEquityTransactions.Columns.Count - 2;
                    clHeader.Border = PdfPCell.NO_BORDER;
                    clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(clHeader);


                    Phrase phSpace = new Phrase("\n");
                    PdfPCell clSpace = new PdfPCell(phSpace);
                    clSpace.Border = PdfPCell.NO_BORDER;
                    clSpace.Colspan = gvEquityTransactions.Columns.Count - 2;
                    table.AddCell(clSpace);

                    GridViewRow HeaderRow = gvEquityTransactions.HeaderRow;
                    if (HeaderRow != null)
                    {
                        string cellText = "";
                        for (int j = 2; j < gvEquityTransactions.Columns.Count; j++)
                        {
                            if (j == 2)
                            {
                                cellText = "Trade Number";
                            }
                            else if (j == 3)
                            {
                                cellText = "Scrip Name";
                            }
                            else if (j == 4)
                            {
                                cellText = "Transaction Type";
                            }
                            else if (j == 5)
                            {
                                cellText = "Exchange";
                            }
                            else if (j == 6)
                            {
                                cellText = "Trade Date";
                            }
                            else if (j == 7)
                            {
                                cellText = "Rate (Rs)";
                            }
                            else if (j == 8)
                            {
                                cellText = "No Of Shares";
                            }
                            else if (j == 9)
                            {
                                cellText = "Brokerage (Rs)";
                            }
                            else if (j == 10)
                            {
                                cellText = "Other Charges (Rs)";
                            }
                            else if (j == 11)
                            {
                                cellText = "Trade total (Rs)";
                            }
                            else
                            {
                                cellText = Server.HtmlDecode(gvEquityTransactions.HeaderRow.Cells[j].Text);
                            }
                            Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD));
                            table.AddCell(ph);
                        }

                    }


                    for (int i = 0; i < gvEquityTransactions.Rows.Count; i++)
                    {
                        string cellText = "";
                        if (gvEquityTransactions.Rows[i].RowType == DataControlRowType.DataRow)
                        {

                            for (int j = 2; j < gvEquityTransactions.Columns.Count; j++)
                            {
                                //if ((Label)gvMFTransactions.Rows[i].Cells[j].FindControl("lblSchemeHeader")!=null)
                                if (j == 2)
                                {

                                    cellText = ((Label)gvEquityTransactions.Rows[i].FindControl("lblTradeNumHeader")).Text;
                                }
                                else if (j == 3)
                                {

                                    cellText = ((Label)gvEquityTransactions.Rows[i].FindControl("lblScripHeader")).Text;
                                }
                                else if (j == 4)
                                {
                                    cellText = ((Label)gvEquityTransactions.Rows[i].FindControl("lblTranTypeHeader")).Text;
                                }
                                else if (j == 5)
                                {
                                    cellText = ((Label)gvEquityTransactions.Rows[i].FindControl("lblExchangeHeader")).Text;
                                }
                                else if (j == 6)
                                {
                                    cellText = ((Label)gvEquityTransactions.Rows[i].FindControl("lblTradeDateHeader")).Text;
                                }
                                else
                                {
                                    cellText = gvEquityTransactions.Rows[i].Cells[j].Text;
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
                else if (Filetype == "Word")
                {
                    gvEquityTransactions.Columns.Remove(this.gvEquityTransactions.Columns[0]);
                    string temp = customerVo.FirstName + customerVo.LastName + "'s_EquityTransactionList.doc";
                    string attachment = "attachment; filename=" + temp;
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/msword";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);

                    Response.Output.Write("<table border=\"0\"><tbody><caption><FONT FACE=\"ARIAL\" SIZE=\"4\">Equity Transactions</FONT></caption><tr><td>");
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
                    Response.Output.Write("</td><td>");
                    System.DateTime tDate1 = System.DateTime.Now;
                    Response.Output.Write(tDate1);
                    Response.Output.Write("</td></tr>");
                    Response.Output.Write("</tbody></table>");
                    if (gvEquityTransactions.HeaderRow != null)
                    {
                        PrepareControlForExport(gvEquityTransactions.HeaderRow);
                    }
                    foreach (GridViewRow row in gvEquityTransactions.Rows)
                    {
                        PrepareControlForExport(row);
                    }
                    if (gvEquityTransactions.FooterRow != null)
                    {
                        PrepareControlForExport(gvEquityTransactions.FooterRow);
                    }
                    gvEquityTransactions.Parent.Controls.Add(frm);
                    frm.Controls.Add(gvEquityTransactions);
                    frm.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();

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

        private void ShowPdf(string strS)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader
            ("Content-Disposition", "attachment; filename=" + strS);
            Response.TransmitFile(strS);
            Response.End();
            Response.Flush();
            Response.Clear();

        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            gvEquityTransactions.Columns[0].Visible = false;
            gvEquityTransactions.Columns[1].Visible = false;
            if (rbtnMultiple.Checked)
            {
                BindGridView(customerId, Pager1.CurrentPage, 1, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            }
            else
            {
                BindGridView(customerId, int.Parse(hdnCurrentPage.Value.ToString()), 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            }
            PrepareGridViewForExport(gvEquityTransactions);
            if (gvEquityTransactions.HeaderRow != null)
            {
                PrepareControlForExport(gvEquityTransactions.HeaderRow);
            }
            foreach (GridViewRow row in gvEquityTransactions.Rows)
            {
                PrepareControlForExport(row);
            }
            if (gvEquityTransactions.FooterRow != null)
            {
                PrepareControlForExport(gvEquityTransactions.FooterRow);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_EquityTransactionsView_tbl','ctrl_EquityTransactionsView_btnPrintGrid');", true);

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
        protected void btnPrintGrid_Click(object sender, EventArgs e)
        {
            BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            gvEquityTransactions.Columns[0].Visible = true;
            gvEquityTransactions.Columns[1].Visible = true;
        }
        protected void rbtnSingle_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbtnMultiple_CheckedChanged(object sender, EventArgs e)
        {
            BindGridView(customerId, Pager1.CurrentPage, 1, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "AferExportAll('ctrl_EquityTransactionsView_btnPrintGrid');", true);
        }


        protected void gvEquityTransactions_Sort(object sender, GridViewSortEventArgs e)
        {
            customerVo = (CustomerVo)Session["CustomerVo"];
            customerId = customerVo.CustomerId;

            string sortExpression = null;
            try
            {
                sortExpression = e.SortExpression;
                ViewState["sortExpression"] = sortExpression;
                if (GridSortDirection == SortDirection.Ascending)
                {
                    GridSortDirection = SortDirection.Descending;
                    hdnSort.Value = sortExpression + " DESC";
                    this.BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
                }
                else
                {
                    GridSortDirection = SortDirection.Ascending;
                    hdnSort.Value = sortExpression + " ASC";
                    this.BindGridView(customerId, Pager1.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));

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

                FunctionInfo.Add("Method", "EquityTransactionsView.ascx.cs:gvEquityTransactions_Sort()");

                object[] objects = new object[1];
                objects[0] = sortExpression;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
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

            dtFrom = DateTime.Parse(txtFromTran.Text);
            dtTo = DateTime.Parse(txtToTran.Text);
           // hdnStatus.Value = "1";
            BindGridView(customerId,Pager1.CurrentPage, 0, dtFrom, dtTo);
        }
    }
}