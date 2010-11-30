using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using WealthERP.Base;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class ViewInsuranceDetails : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        InsuranceBo insuranceBo = new InsuranceBo();
        InsuranceVo insuranceVo = new InsuranceVo();
        CustomerAccountBo customerAccountsBo = new CustomerAccountBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        int portfolioId = 0;
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();

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
                FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx.cs:OnInit()");
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
                portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
                GetPageCount();
                this.LoadGridview(portfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx.cs:HandlePagerEvent()");
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
                if (hdnRecordCount.Value.Trim() != "")
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
                FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx.cs:GetPageCount()");
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["customerVo"];//SessionContents.CustomerVo;

                if (Session[SessionContents.PortfolioId] != null)
                {
                    portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
                }
                else
                {
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
                    Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                    portfolioId = customerPortfolioVo.PortfolioId;
                }

                if (!IsPostBack)
                {
                    ErrorMessage.Visible = false;
                    BindPortfolioDropDown();
                    LoadGridview(portfolioId);
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
                FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx:Page_Load()");
                object[] objects = new object[3];
                objects[0] = userVo;
                objects[1] = customerVo;
                objects[2] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void LoadGridview(int portfolioId)
        {
            List<InsuranceVo> insuranceList = new List<InsuranceVo>();
            try
            {
                int count;
                insuranceList = insuranceBo.GetInsurancePortfolio(portfolioId, mypager.CurrentPage, hdnSort.Value, out count);

                if (count > 0)
                {
                    lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                    tblPager.Visible = true;
                    trPager.Visible = true;
                }
                else
                {
                    ErrorMessage.Visible = true;
                    tblPager.Visible = false;
                    trPager.Visible = false;
                }

                int RecordsCount = 0;
                if (insuranceList != null)
                {
                    RecordsCount = insuranceList.Count;
                }

                if (RecordsCount > 0)
                {
                    ErrorMessage.Visible = false;
                    trPager.Visible = true;
                    InsuranceVo insuranceVo;
                    DataTable dtInsurance = new DataTable();
                    //dtInsurance.Columns.Add("SI.No");
                    dtInsurance.Columns.Add("InsuranceId");
                    dtInsurance.Columns.Add("Category");
                    dtInsurance.Columns.Add("Particulars");
                    dtInsurance.Columns.Add("Sum Assured");
                    dtInsurance.Columns.Add("Premium Amount");
                    dtInsurance.Columns.Add("Commencement Date");
                    dtInsurance.Columns.Add("Maturity Value");
                    dtInsurance.Columns.Add("Maturity Date");
                    DataRow drInsurance;

                    for (int i = 0; i < insuranceList.Count; i++)
                    {
                        drInsurance = dtInsurance.NewRow();
                        insuranceVo = new InsuranceVo();
                        insuranceVo = insuranceList[i];
                        //drInsurance[0] = (i + 1).ToString();
                        drInsurance[0] = insuranceVo.CustInsInvId.ToString();
                        drInsurance[1] = insuranceVo.AssetInstrumentCategoryName.ToString();
                        drInsurance[2] = insuranceVo.Name.ToString();
                        drInsurance[3] = String.Format("{0:n0}", decimal.Parse(insuranceVo.SumAssured.ToString("f0")));
                        drInsurance[4] = String.Format("{0:n2}", decimal.Parse(insuranceVo.PremiumAmount.ToString("f2")));

                        DateTime dtNow = DateTime.Now;
                        //DateTime dtPremiumPayDate;

                        //********************************************************************************************************************
                        //Code Commented as the premium date is not displayed properly in the grid. Instead adding Commencement date as of now
                        //********************************************************************************************************************

                        //int premiumPayDate = insuranceVo.PremiumPaymentDate;

                        //if (premiumPayDate != 0)
                        //{
                        //    dtPremiumPayDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        //    DateTime premiumPayDate1 = dtPremiumPayDate.AddDays(premiumPayDate - 1);

                        //    // Compare the two date time values
                        //    // Less than 0 -- dtNow is earlier than dtPremiumPayDate
                        //    // Greater than 0 -- dtNow is later than dtPremiumPayDate
                        //    // Equal to 0 -- dtNow is equal to dtPremiumPayDate
                        //    int compare = DateTime.Compare(dtNow, dtPremiumPayDate);

                        //    switch (insuranceVo.PremiumFrequencyCode.ToString())
                        //    {
                        //        case "AM":
                        //            drInsurance[5] = "N/A";
                        //            break;
                        //        case "DA":
                        //            if (compare < 0)
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else if (compare > 0)
                        //            { // Calculate the Next Payment Date and display it
                        //                dtPremiumPayDate.AddDays(1);
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            break;
                        //        case "FN":
                        //            if (compare < 0)
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else if (compare > 0)
                        //            { // Calculate the Next Payment Date and display it
                        //                dtPremiumPayDate.AddDays(15);
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            break;
                        //        case "HY":
                        //            if (compare < 0)
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else if (compare > 0)
                        //            { // Calculate the Next Payment Date and display it
                        //                dtPremiumPayDate.AddMonths(6);
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            break;
                        //        case "MN":
                        //            if (compare < 0)
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else if (compare > 0)
                        //            { // Calculate the Next Payment Date and display it
                        //                dtPremiumPayDate.AddMonths(1);
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            break;
                        //        case "NA":
                        //            drInsurance[5] = "N/A";
                        //            break;
                        //        case "QT":
                        //            if (compare < 0)
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else if (compare > 0)
                        //            { // Calculate the Next Payment Date and display it
                        //                dtPremiumPayDate.AddMonths(3);
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            break;
                        //        case "WK":
                        //            if (compare < 0)
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else if (compare > 0)
                        //            { // Calculate the Next Payment Date and display it
                        //                dtPremiumPayDate.AddDays(7);
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            break;
                        //        case "YR":
                        //            if (compare < 0)
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else if (compare > 0)
                        //            { // Calculate the Next Payment Date and display it
                        //                dtPremiumPayDate.AddYears(1);
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            else
                        //            { // Display the Premium Payment Date
                        //                drInsurance[5] = dtPremiumPayDate.ToShortDateString();
                        //            }
                        //            break;
                        //    }
                        //}
                        //else
                        //{
                        //    drInsurance[5] = "";
                        //}

                        drInsurance[5] = insuranceVo.StartDate.ToShortDateString();
                        drInsurance[6] = String.Format("{0:n2}", decimal.Parse(insuranceVo.MaturityValue.ToString("f2")));
                        drInsurance[7] = insuranceVo.EndDate.ToShortDateString();

                        dtInsurance.Rows.Add(drInsurance);
                    }

                    gvrLifeInsurance.DataSource = dtInsurance;
                    gvrLifeInsurance.DataBind();
                    gvrLifeInsurance.Visible = true;
                    this.GetPageCount();
                }
                else
                {
                    gvrLifeInsurance.DataSource = null;
                    gvrLifeInsurance.DataBind();
                    ErrorMessage.Visible = true;
                    trPager.Visible = false;
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
                FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx:LoadGridview()");
                object[] objects = new object[3];
                objects[0] = insuranceList;
                objects[1] = portfolioId;
                objects[2] = insuranceVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlAction = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
                int selectedRow = gvr.RowIndex;
                int insuranceId = int.Parse(gvrLifeInsurance.DataKeys[selectedRow].Value.ToString());

                // Set the VO into the Session
                insuranceVo = insuranceBo.GetInsuranceAsset(insuranceId);
                Session["insuranceVo"] = insuranceVo;
                Session["customerAccountVo"] = customerAccountsBo.GetCustomerInsuranceAccount(insuranceVo.AccountId);

                if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {
                    Session.Remove("table");
                    Session.Remove("moneyBackEpisodeList");
                    Session.Remove("insuranceULIPList");
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','action=edit');", true);
                }
                if (ddlAction.SelectedItem.Value.ToString() == "View")
                {
                    Session.Remove("table");
                    Session.Remove("moneyBackEpisodeList");
                    Session.Remove("insuranceULIPList");
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','action=view');", true);
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
                FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx:ddlMenu_SelectedIndexChanged()");
                object[] objects = new object[1];
                objects[0] = insuranceVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvrLifeInsurance_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvrLifeInsurance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvrLifeInsurance.PageIndex = e.NewPageIndex;
            gvrLifeInsurance.DataBind();
        }

        protected void gvrLifeInsurance_Sorting(object sender, GridViewSortEventArgs e)
        {

            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridVIew(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridVIew(sortExpression, ASCENDING);
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

        private void SortGridVIew(string sortExpression, string direction)
        {

            List<InsuranceVo> insuranceList = new List<InsuranceVo>();
            try
            {
                int count;

                insuranceList = insuranceBo.GetInsurancePortfolio(portfolioId, mypager.CurrentPage, hdnSort.Value.Trim(), out count);

                if (count > 0)
                {
                    lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                    tblPager.Visible = true;
                }

                InsuranceVo insuranceVo;
                DataTable dtInsurance = new DataTable();
                dtInsurance.Columns.Add("SI.No");
                dtInsurance.Columns.Add("InsuranceId");
                dtInsurance.Columns.Add("Category");
                dtInsurance.Columns.Add("Particulars");
                dtInsurance.Columns.Add("Premium Amount");
                dtInsurance.Columns.Add("Sum Assured");
                dtInsurance.Columns.Add("Maturity Value");
                DataRow drInsurance;
                for (int i = 0; i < insuranceList.Count; i++)
                {
                    drInsurance = dtInsurance.NewRow();
                    insuranceVo = new InsuranceVo();
                    insuranceVo = insuranceList[i];
                    drInsurance[0] = insuranceVo.CustInsInvId.ToString();
                    drInsurance[1] = insuranceVo.AssetInstrumentCategoryCode.ToString();
                    drInsurance[2] = insuranceVo.Name.ToString();
                    drInsurance[3] = String.Format("{0:n2}", decimal.Parse(insuranceVo.PremiumAmount.ToString("f2")));
                    drInsurance[4] = String.Format("{0:n0}", decimal.Parse(insuranceVo.SumAssured.ToString("f0")));
                    drInsurance[5] = String.Format("{0:n2}", decimal.Parse(insuranceVo.MaturityValue.ToString("f2")));

                    dtInsurance.Rows.Add(drInsurance);

                }

                DataView dv = new DataView(dtInsurance);
                dv.Sort = sortExpression + direction;
                gvrLifeInsurance.DataSource = dv;
                gvrLifeInsurance.DataBind();
                gvrLifeInsurance.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx:SortGridVIew()");
                object[] objects = new object[2];
                objects[0] = insuranceVo;
                objects[1] = insuranceList;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvrLifeInsurance_DataBound(object sender, EventArgs e)
        {
            //List<InsuranceVo> insuranceList = new List<InsuranceVo>();
            //try
            //{
            //    insuranceList = insuranceBo.GetInsurancePortfolio(portfolioId);
            //    if (insuranceList != null)
            //    {
            //        gvrLifeInsurance.FooterRow.Cells[0].Text = "Total Records : " + insuranceList.Count.ToString();

            //        gvrLifeInsurance.FooterRow.Cells[0].ColumnSpan = gvrLifeInsurance.FooterRow.Cells.Count;
            //        for (int i = 1; i < gvrLifeInsurance.FooterRow.Cells.Count; i++)
            //        {
            //            gvrLifeInsurance.FooterRow.Cells[i].Visible = false;
            //        }
            //    }
            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}
            //catch (Exception Ex)
            //{
            //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            //    NameValueCollection FunctionInfo = new NameValueCollection();
            //    FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx:gvrInsurance_DataBound()");
            //    object[] objects = new object[2];
            //    objects[0] = insuranceList;
            //    FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;
            //}
        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {

            portfolioId = int.Parse(ddlPortfolio.SelectedValue.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            LoadGridview(portfolioId);
        }

    }
}