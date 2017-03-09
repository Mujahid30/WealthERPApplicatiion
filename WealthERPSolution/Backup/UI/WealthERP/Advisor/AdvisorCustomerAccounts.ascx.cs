using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerProfiling;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoCommon;
using System.Configuration;

namespace WealthERP.Advisor
{
    public partial class AdvisorCustomerAccounts : System.Web.UI.UserControl
    {

        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountsVo customerMFAccountsVo;
        CustomerVo customerVo = new CustomerVo();
        AdvisorVo advisorVo = new AdvisorVo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = new UserVo();

        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

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
                FunctionInfo.Add("Method", "AdvisorCustomerAccounts.ascx.cs:OnInit()");
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
                //customerVo = (CustomerVo)Session["customerVo"];
                advisorVo = (AdvisorVo)Session["advisorVo"];
                //portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
                LoadAdvisorFolioGrid(advisorVo.advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorCustomerAccounts.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = advisorVo;
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
                ratio = rowCount / 20;
                mypager.PageCount = rowCount % 20 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = (((mypager.CurrentPage - 1) * 20)+1).ToString();
                upperlimit = (mypager.CurrentPage * 20).ToString();
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
                FunctionInfo.Add("Method", "AdvisorCustomerAccounts.ascx.cs:GetPageCount()");
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
                //customerVo = (CustomerVo)Session["customerVo"];
                //portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
                advisorVo = (AdvisorVo)Session["advisorVo"];
                userVo = (UserVo)Session["userVo"];
                LoadAdvisorFolioGrid(mypager.CurrentPage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorCustomerAccounts.ascx:Page_Load()");
                object[] objects = new object[1];
                objects[1] = advisorVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void LoadAdvisorFolioGrid(int CurrentPage)
        {
            List<CustomerAccountsVo> customerMFAccountsList = new List<CustomerAccountsVo>();
            Dictionary<string, string> genDictAMC = new Dictionary<string, string>();

            try
            {
                advisorVo = (AdvisorVo)Session["advisorVo"];
                string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                int count;

                customerMFAccountsList = customerAccountBo.GetAdviserMFAccountList(advisorVo.advisorId, mypager.CurrentPage, hdnSort.Value, out count, hdnNameFilter.Value, hdnAMCFilter.Value, out genDictAMC);
                if (count > 0)
                {
                    lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                    tblPager.Visible = true;
                }
                if (customerMFAccountsList!= null)
                {
                    lblMsg.Visible = false;
                    DataTable dtCustomerMFAccounts = new DataTable();
                    //dtProperty.Columns.Add("SI.No");
                    dtCustomerMFAccounts.Columns.Add("MFAccountId");
                    dtCustomerMFAccounts.Columns.Add("CustomerId");
                    dtCustomerMFAccounts.Columns.Add("PortfolioId");
                    dtCustomerMFAccounts.Columns.Add("Customer Name");
                    dtCustomerMFAccounts.Columns.Add("AMC Name");
                    dtCustomerMFAccounts.Columns.Add("Folio Number");

                    DataRow drCustomerMFAccounts;
                    for (int i = 0; i < customerMFAccountsList.Count; i++)
                    {
                        drCustomerMFAccounts = dtCustomerMFAccounts.NewRow();
                        customerMFAccountsVo = new CustomerAccountsVo();
                        customerMFAccountsVo = customerMFAccountsList[i];
                        //drProperty[0] = (i + 1).ToString();
                        drCustomerMFAccounts[0] = customerMFAccountsVo.AccountId.ToString();
                        drCustomerMFAccounts[1] = customerMFAccountsVo.CustomerId.ToString();
                        drCustomerMFAccounts[2] = customerMFAccountsVo.PortfolioId.ToString();
                        drCustomerMFAccounts[3] = customerMFAccountsVo.CustomerName.ToString();
                        drCustomerMFAccounts[4] = customerMFAccountsVo.AMCName.ToString();
                        drCustomerMFAccounts[5] = customerMFAccountsVo.AccountNum.ToString();

                        dtCustomerMFAccounts.Rows.Add(drCustomerMFAccounts);
                    }
                    gvrMFAccounts.DataSource = dtCustomerMFAccounts;
                    gvrMFAccounts.DataBind();

                    if (genDictAMC.Count > 0)
                    {
                        DropDownList ddlAMC = GetAMCDDL();
                        if (ddlAMC != null)
                        {
                            ddlAMC.DataSource = genDictAMC;
                            ddlAMC.DataTextField = "Key";
                            ddlAMC.DataValueField = "Value";
                            ddlAMC.DataBind();
                            ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select AMC", "Select AMC"));
                        }
                        if (hdnAMCFilter.Value != "")
                        {
                            ddlAMC.SelectedValue = hdnAMCFilter.Value.ToString();
                        }
                    }

                    TextBox txtName = GetCustNameTextBox();
                    if (txtName != null)
                    {
                        if (hdnNameFilter.Value != "")
                        {
                            txtName.Text = hdnNameFilter.Value.ToString();
                        }
                    }

                    this.GetPageCount();
                }
                else
                {
                    lblMsg.Visible = true;
                    gvrMFAccounts.DataSource = null;
                    gvrMFAccounts.DataBind();
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
                FunctionInfo.Add("Method", "AdvisorCustomerAccounts.ascx:LoadAdvisorFolioGrid()");
                object[] objects = new object[1];
                objects[0] = customerMFAccountsVo;
                objects[1] = customerMFAccountsList;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


        //protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DropDownList ddlAction = (DropDownList)sender;
        //        GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
        //        int selectedRow = gvr.RowIndex;
        //        int systematicSetupId = int.Parse(gvrSystematicSchemes.DataKeys[selectedRow].Value.ToString());

        //        // Set the VO into the Session
        //        //systematicSetupVo = systematicSetupBo.GetPropertyAsset(propertyId);
        //        Session["systematicSetupVo"] = systematicSetupVo;
        //        //Session["customerAccountVo"] = customerAccountsBo.GetCustomerPropertyAccount(systematicSetupVo.AccountId);

        //        if (ddlAction.SelectedItem.Value.ToString() == "Edit")
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioSystematicEntry','?action=edit');", true);
        //        }
        //        if (ddlAction.SelectedItem.Value.ToString() == "View")
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioSystematicEntry','?action=view');", true);
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "PortfolioProperty.ascx:ddlMenu_SelectedIndexChanged()");
        //        object[] objects = new object[1];
        //        objects[0] = systematicSetupVo;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //}

        //protected void gvrSystematicSetup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvrMFAccounts.PageIndex = e.NewPageIndex;
        //    gvrMFAccounts.DataBind();
        //}

        protected void gvrMFAccounts_Sorting(object sender, GridViewSortEventArgs e)
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
                //customerVo = (CustomerVo)Session["customerVo"];
                //portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
                advisorVo = (AdvisorVo)Session["AdvisorVo"];
                LoadAdvisorFolioGrid(advisorVo.advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorCustomerAccounts.ascx:gvrMFAccounts_Sorting()");
                object[] objects = new object[1];
                objects[1] = advisorVo;
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

        private TextBox GetCustNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvrMFAccounts.HeaderRow != null)
            {
                if ((TextBox)gvrMFAccounts.HeaderRow.FindControl("txtCustNameSearch") != null)
                {
                    txt = (TextBox)gvrMFAccounts.HeaderRow.FindControl("txtCustNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private DropDownList GetAMCDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvrMFAccounts.HeaderRow != null)
            {
                if ((DropDownList)gvrMFAccounts.HeaderRow.FindControl("ddlAMCName") != null)
                {
                    ddl = (DropDownList)gvrMFAccounts.HeaderRow.FindControl("ddlAMCName");
                }
            }
            else
                ddl = null;

            return ddl;
        }

        protected void btnNameSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetCustNameTextBox();

            if (txtName != null)
            {
                hdnNameFilter.Value = txtName.Text.Trim();

                this.LoadAdvisorFolioGrid(mypager.CurrentPage);

            }
        }

        protected void ddlAMCName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAMC = GetAMCDDL();

            if (ddlAMC != null)
            {
                if (ddlAMC.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnAMCFilter.Value = ddlAMC.SelectedValue;
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnAMCFilter.Value = "";
                }


                this.LoadAdvisorFolioGrid(mypager.CurrentPage);

            }
        }

        protected void gvrMFAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvrMFAccounts.SelectedRow;

            Label folioNum = row.FindControl("lblFolioNum") as Label;
            string folioNumber = folioNum.Text.ToString();
            int accountId = int.Parse(gvrMFAccounts.DataKeys[row.RowIndex].Values[0].ToString());
            int customerId = int.Parse(gvrMFAccounts.DataKeys[row.RowIndex].Values[1].ToString());
            int portfolioId = int.Parse(gvrMFAccounts.DataKeys[row.RowIndex].Values[2].ToString());

            customerVo = customerBo.GetCustomer(customerId);

            Session["customerVo"] = customerVo;
            Session[SessionContents.PortfolioId] = portfolioId;
            Session["folioNum"] = folioNumber;

            GetLatestValuationDate();

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewMutualFundPortfolio','none');", true);


        }

        private void GetLatestValuationDate()
        {
            DateTime EQValuationDate = new DateTime();
            DateTime MFValuationDate = new DateTime();
            PortfolioBo portfolioBo = null;
            Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
            AdvisorVo advisorVo = new AdvisorVo();
            int adviserId = 0;
            try
            {
                portfolioBo = new PortfolioBo();
                if (userVo.UserType == "Advisor")
                {

                    advisorVo = (AdvisorVo)Session["advisorVo"];
                    adviserId = advisorVo.advisorId;
                }
                else if (userVo.UserType == "RM")
                {
                    adviserId = int.Parse(Session["adviserId"].ToString());

                }

                if (portfolioBo.GetLatestValuationDate(adviserId, "EQ") != null)
                {
                    EQValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, "EQ").ToString());
                }
                if (portfolioBo.GetLatestValuationDate(adviserId, "MF") != null)
                {
                    MFValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, "MF").ToString());
                }
                genDict.Add("EQDate", EQValuationDate);
                genDict.Add("MFDate", MFValuationDate);
                Session["ValuationDate"] = genDict;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioDashboard.ascx.cs:GetLatestValuationDate()");
                object[] objects = new object[3];
                objects[0] = EQValuationDate;
                objects[1] = adviserId;
                objects[2] = MFValuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        //private void sortGridViewBranches(string sortExpression, string direction)
        //{


        //}

        //protected void gvrSystematicSetup_DataBound(object sender, EventArgs e)
        //{

        //}

    }
}