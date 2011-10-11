using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using BoCustomerProfiling;
using System.Data;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;
using BoCommon;
using System.Configuration;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioFixedIncomeView : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        FixedIncomeBo fixedincomeBo = new FixedIncomeBo();
        FixedIncomeVo fixedincomeVo = new FixedIncomeVo();
        List<FixedIncomeVo> fixedincomeList = new List<FixedIncomeVo>();
        PortfolioBo portfolioBo = new PortfolioBo();
        string path;
        int fixedincomeId;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        static int portfolioId = 0;

        protected override void OnInit(EventArgs e)
        {

            try
            {
                this.Page.Culture = "en-GB";
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
                FunctionInfo.Add("Method", "PortfolioFixedIncomeView.ascx.cs:OnInit()");
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
                this.LoadGridView();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioFixedIncomeView.ascx.cs:HandlePagerEvent()");
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
                FunctionInfo.Add("Method", "PortfolioFixedIncomeView.ascx.cs:GetPageCount()");
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
            if (!IsPostBack)
            {
                this.Page.Culture = "en-GB";
                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                this.LoadGridView();
                BindPortfolioDropDown();
                
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
        protected void LoadGridView()
        {
            try
            {
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
                customerVo = (CustomerVo)Session["CustomerVo"];
                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                int count;
                fixedincomeList = fixedincomeBo.GetFixedIncomePortfolioList(portfolioId, mypager.CurrentPage, hdnSort.Value, out count);
                if (count > 0)
                {
                    lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                    tblPager.Visible = true;
                }
                if (fixedincomeList == null)
                {
                    //lblMessage.Visible = true;
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "No Records Found...!";

                }
                else
                {
                    //lblMessage.Visible = false;
                    tblMessage.Visible = false;
                    ErrorMessage.Visible = false;
                    ErrorMessage.InnerText = "No Records Found...!";
                    DataTable dtFixedIncomePortfolio = new DataTable();

                    dtFixedIncomePortfolio.Columns.Add("FITransactionId");
                    dtFixedIncomePortfolio.Columns.Add("Name");
                    dtFixedIncomePortfolio.Columns.Add("Category");
                    dtFixedIncomePortfolio.Columns.Add("Purchase Date");
                    dtFixedIncomePortfolio.Columns.Add("Maturity Date");
                    dtFixedIncomePortfolio.Columns.Add("Deposit Amount");
                    dtFixedIncomePortfolio.Columns.Add("Interest Rate");
                    dtFixedIncomePortfolio.Columns.Add("Current Value");
                    dtFixedIncomePortfolio.Columns.Add("Maturity Value");

                    DataRow drFixedIncomePortfolio;

                    for (int i = 0; i < fixedincomeList.Count; i++)
                    {
                        drFixedIncomePortfolio = dtFixedIncomePortfolio.NewRow();
                        fixedincomeVo = new FixedIncomeVo();
                        fixedincomeVo = fixedincomeList[i];
                        drFixedIncomePortfolio[0] = fixedincomeVo.FITransactionId.ToString();
                        drFixedIncomePortfolio[1] = fixedincomeVo.Name.ToString();
                        drFixedIncomePortfolio[2] = fixedincomeVo.AssetInstrumentCategoryName.ToString();
                        if (fixedincomeVo.PurchaseDate != DateTime.MinValue)
                            drFixedIncomePortfolio[3] = fixedincomeVo.PurchaseDate.ToShortDateString();
                        else
                            drFixedIncomePortfolio[3] = "";
                        if (fixedincomeVo.MaturityDate != DateTime.MinValue)
                            drFixedIncomePortfolio[4] = fixedincomeVo.MaturityDate.ToShortDateString();
                        else
                            drFixedIncomePortfolio[4] = "";
                        if (Convert.ToDecimal(fixedincomeVo.PrinciaplAmount.ToString()) == 0)
                            drFixedIncomePortfolio[5] = "0";
                        else
                            drFixedIncomePortfolio[5] = decimal.Parse(fixedincomeVo.PrinciaplAmount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drFixedIncomePortfolio[6] = fixedincomeVo.InterestRate.ToString();
                        if (Convert.ToDecimal(fixedincomeVo.CurrentValue.ToString()) == 0)
                            drFixedIncomePortfolio[7] = "0";
                        else
                            drFixedIncomePortfolio[7] = decimal.Parse(fixedincomeVo.CurrentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                      
                      //  drFixedIncomePortfolio[7] = String.Format("{0:n2}", decimal.Parse(fixedincomeVo.CurrentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        if (Convert.ToDecimal(fixedincomeVo.MaturityValue.ToString()) == 0)
                            drFixedIncomePortfolio[8] = "0";
                        else
                            drFixedIncomePortfolio[8] = decimal.Parse(fixedincomeVo.MaturityValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                      
                       // drFixedIncomePortfolio[8] = String.Format("{0:n2}", decimal.Parse(fixedincomeVo.MaturityValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                        dtFixedIncomePortfolio.Rows.Add(drFixedIncomePortfolio);
                    }

                    gvFixedIncomePortfolio.DataSource = dtFixedIncomePortfolio;
                    gvFixedIncomePortfolio.DataBind();
                    this.GetPageCount();
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
                FunctionInfo.Add("Method", "PortfolioFixedIncomeView.ascx:Page_Load()");
                object[] objects = new object[5];
                objects[0] = customerVo;
                objects[1] = fixedincomeVo;
                objects[2] = fixedincomeList;
                objects[3] = portfolioId;
                objects[4] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlAction = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
                int selectedRow = gvr.RowIndex;
                int portfolioId = int.Parse(gvFixedIncomePortfolio.DataKeys[selectedRow].Value.ToString());
                Session["fixedIncomeVo"] = fixedincomeBo.GetFixedIncomePortfolio(portfolioId);
                hdndeleteId.Value = portfolioId.ToString();

                if (ddlAction.SelectedItem.Value.ToString() == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadcontrol", "loadcontrol('PortfolioFixedIncomeEntry','action=ViewFI');", true);
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadcontrol", "loadcontrol('PortfolioFixedIncomeEntry','action=EditFI');", true);
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "Delete")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
                }
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioFixedIncomeView.ascx:ddlAction_OnSelectedIndexChange()");
                object[] objects = new object[0];
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

        protected void gvFixedIncomePortfolio_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvFixedIncomePortfolio_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortExpression = e.SortExpression;
                ViewState["sortExpression"] = sortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    hdnSort.Value = sortExpression + " DESC";
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    hdnSort.Value = sortExpression + " ASC";
                }
                customerVo = (CustomerVo)Session["customerVo"];
                LoadGridView();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioProperty.ascx:gvrProperty_Sorting()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = portfolioId;
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
        }

        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                fixedincomeBo.DeleteFixedIncomePortfolio(int.Parse(hdndeleteId.Value), fixedincomeVo.AccountId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioFixedIncomeView','login');", true);
                msgRecordStatus.Visible = true;
            }
        }
    }
}