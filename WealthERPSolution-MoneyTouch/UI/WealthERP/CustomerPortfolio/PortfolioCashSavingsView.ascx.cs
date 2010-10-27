using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using WealthERP.Base;
using BoCommon;


namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioCashSavingsView : System.Web.UI.UserControl
    {
        CashAndSavingsBo customerCashSavingsBo = new CashAndSavingsBo();
        CashAndSavingsVo customerCashSavingsVo;
        List<CashAndSavingsVo> customerCashSavingsList = null;
        CustomerVo customerVo = null;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        int customerId;
        string assetGroupCode = "CS";
        string assetGroupName = "Cash & Savings";
        int portfolioId;
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountsVo accVo = null;
        PortfolioBo portfolioBo = new PortfolioBo();
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
                FunctionInfo.Add("Method", "PortfolioCashSavingsView.ascx.cs:OnInit()");
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
                this.BindDetails("All");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioCashSavingsView.ascx.cs:HandlePagerEvent()");
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
                rowCount = Convert.ToInt32(hdnRecordCount.Value);
                ratio = rowCount / 10;
                mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = (((mypager.CurrentPage - 1) * 10)+1).ToString();
                upperlimit = (mypager.CurrentPage * 10).ToString();
                if (mypager.CurrentPage == mypager.PageCount)
                    upperlimit = hdnRecordCount.Value;
                PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                lblCurrentPage.Text = PageRecords;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioCashSavingsView.ascx.cs:GetPageCount()");
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
                customerVo = (CustomerVo)Session["customerVo"];
                if (!IsPostBack)
                {

                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();
                    BindDetails("All");
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
                FunctionInfo.Add("Method", "PortfolioCashSavingsView.ascx.cs:Page_Load()");
                object[] objects = new object[1];
                objects[0] = portfolioId;
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
            //ddlPortfolio.Items.Insert(0, "Select the Portfolio");

            ddlPortfolio.SelectedValue = portfolioId.ToString();

        }
        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            BindDetails("All");

        }
        protected void gvCustomerCashSavings_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                hdnSort.Value = sortExpression + " DESC";
                this.BindDetails("All");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                hdnSort.Value = sortExpression + " ASC";
                this.BindDetails("All");

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

        protected void gvCustomerCashSavings_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        private void ShowHidePanels(string Panel)
        {
            BindDetails("All");
        }

        private void BindDetails(string bind)
        {
            int Count = 0;
            try
            {
                if (bind == "All")
                {
                    customerCashSavingsList = customerCashSavingsBo.GetCustomerCashSavings(portfolioId, mypager.CurrentPage, hdnSort.Value, out Count);
                    lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
                    if (Count > 0)
                        DivPager.Style.Add("display", "visible");
                    if (customerCashSavingsList != null)
                    {
                        DivPager.Visible = true;
                        lblMsg.Visible = false;
                        DataTable dtCustomerCashSavings = new DataTable();

                        dtCustomerCashSavings.Columns.Add("CashSavingsPortfolioId");
                        //dtCustomerCashSavings.Columns.Add("Name");
                        dtCustomerCashSavings.Columns.Add("Instrument Category");
                        dtCustomerCashSavings.Columns.Add("AccountWithBank");
                        //dtCustomerCashSavings.Columns.Add("Debt Issuer");
                        dtCustomerCashSavings.Columns.Add("Deposit Amount");
                        dtCustomerCashSavings.Columns.Add("InterestAmount");
                        //dtCustomerCashSavings.Columns.Add("Deposit Date");
                        //dtCustomerCashSavings.Columns.Add("Interest Basis");
                        //dtCustomerCashSavings.Columns.Add("Interest Rate");
                        //dtCustomerCashSavings.Columns.Add("Current Value");
                        //dtCustomerCashSavings.Columns.Add("Remarks");

                        DataRow drCustomerCashSavings;
                        for (int i = 0; i < customerCashSavingsList.Count; i++)
                        {
                            drCustomerCashSavings = dtCustomerCashSavings.NewRow();
                            customerCashSavingsVo = new CashAndSavingsVo();
                            accVo = new CustomerAccountsVo();
                            customerCashSavingsVo = customerCashSavingsList[i];
                            drCustomerCashSavings[0] = customerCashSavingsVo.CashSavingsPortfolioId.ToString().Trim();
                            //drCustomerCashSavings[1] = customerCashSavingsVo.Name.ToString().Trim();
                            drCustomerCashSavings[1] = customerCashSavingsVo.AssetInstrumentCategoryName.ToString().Trim();
                            accVo = customerAccountBo.GetCashAndSavingsAccount(Convert.ToInt32(customerCashSavingsVo.AccountId));
                            drCustomerCashSavings[2] = accVo.BankName.ToString();
                            if (customerCashSavingsVo.DepositAmount != 0)
                                drCustomerCashSavings[3] = String.Format("{0:n2}", decimal.Parse(customerCashSavingsVo.DepositAmount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else
                                drCustomerCashSavings[3] = 0;
                            if (customerCashSavingsVo.InterestAmntPaidOut != null)
                                drCustomerCashSavings[4] = String.Format("{0:n2}", decimal.Parse(customerCashSavingsVo.InterestAmntPaidOut.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                            dtCustomerCashSavings.Rows.Add(drCustomerCashSavings);
                        }

                        gvCustomerCashSavings.DataSource = dtCustomerCashSavings;
                        gvCustomerCashSavings.DataBind();
                        gvCustomerCashSavings.Visible = true;
                    }
                    else
                    {
                        gvCustomerCashSavings.DataSource = null;
                        gvCustomerCashSavings.DataBind();
                        gvCustomerCashSavings.Visible = false;
                        DivPager.Visible = false;
                        lblMsg.Visible = true;
                    }
                }
                this.GetPageCount();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioCashSavingsView.ascx:BindDetails()");

                object[] objects = new object[5];

                objects[0] = customerVo;
                objects[1] = bind;
                objects[2] = customerCashSavingsVo;
                objects[3] = customerCashSavingsList;
                objects[4] = Count;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


        protected void btnEditDetails_Click(object sender, EventArgs e)
        {
            // Load the Edit Cash Savings Control
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadcontrol", "loadcontrol('PortfolioCashSavingsEdit','none');", true);
        }

        protected void gvCustomerCashSavings_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomerCashSavings.PageIndex = e.NewPageIndex;
            gvCustomerCashSavings.DataBind();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)ddl.NamingContainer;
                int index = gvr.RowIndex;
                int portfolioId = int.Parse(gvCustomerCashSavings.DataKeys[index].Value.ToString());
                Session["CashSavingsPortfolioId"] = gvCustomerCashSavings.DataKeys[index].Value.ToString();
                if (ddl.SelectedItem.Value.ToString() == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadcontrol", "loadcontrol('PortfolioCashSavingsEntry','?action=ViewCS');", true);

                }
                else if (ddl.SelectedItem.Value.ToString() == "Edit")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadcontrol", "loadcontrol('PortfolioCashSavingsEntry','?action=EditCS');", true);
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
                FunctionInfo.Add("Method", "PortfolioCashSavingsView.ascx:ddlAction_OnSelectedIndexChanges()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }



    }
}