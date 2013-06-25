﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WealthERP.Base;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCommon;
using Telerik.Web.UI;

namespace WealthERP.CustomerPortfolio
{
    public partial class ViewGovtSavings : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        GovtSavingsVo govtSavingsVo = new GovtSavingsVo();
        GovtSavingsBo govtSavingsBo = new GovtSavingsBo();
       
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        static int portfolioId;
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        Dictionary<int, int> genDictPortfolioDetails = new Dictionary<int, int>();

        protected override void OnInit(EventArgs e)
        {
            //try
            //{
            //    this.Page.Culture = "en-GB";
            //    ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            //    mypager.EnableViewState = true;
            //    base.OnInit(e);
            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}
            //catch (Exception Ex)
            //{
            //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            //    NameValueCollection FunctionInfo = new NameValueCollection();
            //    FunctionInfo.Add("Method", "ViewGovtSavings.ascx.cs:OnInit()");
            //    object[] objects = new object[0];

            //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;
            //}
         
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            //try
            //{
            //    GetPageCount();
            //    this.LoadGridview(portfolioId);
            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}
            //catch (Exception Ex)
            //{
            //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            //    NameValueCollection FunctionInfo = new NameValueCollection();
            //    FunctionInfo.Add("Method", "ViewGovtSavings.ascx.cs:HandlePagerEvent()");
            //    object[] objects = new object[0];

            //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;
            //}
            
        }

        private void GetPageCount()
        {
            //string upperlimit = "";
            //int rowCount = 0;
            //int ratio = 0;
            //string lowerlimit = "";
            //string PageRecords = "";
            //try
            //{
            //    rowCount = Convert.ToInt32(hdnRecordCount.Value);
            //    ratio = rowCount / 10;
            //    mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
            //    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
            //    lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
            //    upperlimit = (mypager.CurrentPage * 10).ToString();
            //    if (mypager.CurrentPage == mypager.PageCount)
            //        upperlimit = hdnRecordCount.Value;
            //    PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                //lblCurrentPage.Text = PageRecords;
            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}
            //catch (Exception Ex)
            //{
            //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            //    NameValueCollection FunctionInfo = new NameValueCollection();
            //    FunctionInfo.Add("Method", "ViewGovtSavings.ascx.cs:GetPageCount()");
            //    object[] objects = new object[5];
            //    objects[0] = upperlimit;
            //    objects[1] = rowCount;
            //    objects[2] = ratio;
            //    objects[3] = lowerlimit;
            //    objects[4] = PageRecords;
            //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;
            //}
           
        }
        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {

            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            LoadGridview(portfolioId);

            if (Session["genDictPortfolioDetails"] != null)
            {
                genDictPortfolioDetails = (Dictionary<int, int>)Session["genDictPortfolioDetails"];
            }
            var keyValuePair = genDictPortfolioDetails.FirstOrDefault(x => x.Key == portfolioId);
            //int value = keyValuePair.Value;

            hdnIsMainPortfolio.Value = keyValuePair.Value.ToString();
            hdnIsCustomerLogin.Value = userVo.UserType;
        }
        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();

            ddlPortfolio.SelectedValue = portfolioId.ToString();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                genDictPortfolioDetails.Add(int.Parse(dr["CP_PortfolioId"].ToString()), int.Parse(dr["CP_IsMainPortfolio"].ToString()));
            }

            var keyValuePair = genDictPortfolioDetails.FirstOrDefault(x => x.Key == portfolioId);

            hdnIsMainPortfolio.Value = keyValuePair.Value.ToString();
            Session["genDictPortfolioDetails"] = genDictPortfolioDetails;
            hdnIsCustomerLogin.Value = userVo.UserType;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["customerVo"];
                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());

                if (!IsPostBack)
                {                    
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
                FunctionInfo.Add("Method", "ViewGovtSavings.ascx.cs:Page_Load()");
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

        public void LoadGridview(int portfolioId)
        {
            List<GovtSavingsVo> govtSavingsList = new List<GovtSavingsVo>();
            int count=0;
            try
            {
                govtSavingsList = govtSavingsBo.GetGovtSavingsNPList(portfolioId, mypager.CurrentPage, hdnSort.Value, out count);
                //if (count > 0)
                //{
                //    lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                //    tblPager.Visible = true;
                //}

                if (govtSavingsList != null)
                {
                    //lblMsg.Visible = false;
                    tblMessage.Visible = false;
                    ErrorMessage.Visible = false;
                    btnExportFilteredData.Visible = true;
                    ErrorMessage.InnerText = "No Records Found...!";
                    GovtSavingsVo govtSavingsVo;

                    DataTable dtGovtSavings = new DataTable();
                    dtGovtSavings.Columns.Add("SI.No");
                    dtGovtSavings.Columns.Add("GovtSavingId");
                    dtGovtSavings.Columns.Add("Category");
                    dtGovtSavings.Columns.Add("Particulars");
                    dtGovtSavings.Columns.Add("Deposit Date");
                    dtGovtSavings.Columns.Add("Maturity Date");
                    dtGovtSavings.Columns.Add("Deposit Amount");
                    dtGovtSavings.Columns.Add("Rate Of Interest");
                    dtGovtSavings.Columns.Add("Current Value");
                    dtGovtSavings.Columns.Add("Maturity Value");
                    DataRow drGovtSavings;

                    for (int i = 0; i < govtSavingsList.Count; i++)
                    {
                        drGovtSavings = dtGovtSavings.NewRow();
                        govtSavingsVo = new GovtSavingsVo();
                        govtSavingsVo = govtSavingsList[i];
                        drGovtSavings[0] = (i + 1).ToString();
                        drGovtSavings[1] = govtSavingsVo.GoveSavingsPortfolioId.ToString();
                        drGovtSavings[2] = govtSavingsVo.AssetInstrumentCategoryName.ToString();
                        drGovtSavings[3] = govtSavingsVo.Name.ToString();
                        if (govtSavingsVo.PurchaseDate != DateTime.MinValue)
                        drGovtSavings[4] = govtSavingsVo.PurchaseDate.ToShortDateString().ToString();
                        if (govtSavingsVo.MaturityDate != DateTime.MinValue)
                        drGovtSavings[5] = govtSavingsVo.MaturityDate.ToShortDateString().ToString();
                      //  if (govtSavingsVo.DepositAmt != null)
                        drGovtSavings[6] = govtSavingsVo.DepositAmt.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        //if (govtSavingsVo.InterestRate != null)
                        drGovtSavings[7] = govtSavingsVo.InterestRate.ToString();
                        //if (govtSavingsVo.CurrentValue != null)
                        drGovtSavings[8] = govtSavingsVo.CurrentValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        //if(govtSavingsVo.MaturityValue != null)
                        drGovtSavings[9] = govtSavingsVo.MaturityValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                        dtGovtSavings.Rows.Add(drGovtSavings);

                    }

                    gvGovtSavings.DataSource = dtGovtSavings;
                    gvGovtSavings.DataBind();
                    gvGovtSavings.Visible = true;
                   // this.GetPageCount();
                }
                else
                {
                    //lblMsg.Visible = true;
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "No Records Found...!";
                    gvGovtSavings.DataSource = null;
                    gvGovtSavings.DataBind();
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
                FunctionInfo.Add("Method", "ViewGovtSavings.ascx.cs:LoadGridview()");
                object[] objects = new object[2];
                objects[0] = govtSavingsList;
                objects[1] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvrGovtSavings_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlMenu = (DropDownList)sender;
                GridDataItem gvr = (GridDataItem)ddlMenu.NamingContainer;               
                int selectedRow = gvr.ItemIndex;
                int govtSavingsId = int.Parse(gvGovtSavings.MasterTableView.DataKeyValues[selectedRow]["GovtSavingId"].ToString());
                hdndeleteId.Value = govtSavingsId.ToString();
                Session["govtSavingsVo"] = govtSavingsBo.GetGovtSavingsDetails(govtSavingsId);
                if (ddlMenu.SelectedItem.Value.ToString() == "Edit")
                {
                    if (hdnIsCustomerLogin.Value == "Customer" && hdnIsMainPortfolio.Value == "1")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Permisssion denied for Manage Portfolio !!');", true);
                    else
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioGovtSavingsEntry','action=Edit');", true);
                }
                if (ddlMenu.SelectedItem.Value.ToString() == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioGovtSavingsEntry','action=View');", true);
                }
                if (ddlMenu.SelectedItem.Value.ToString() == "Delete")
                {
                    if (hdnIsCustomerLogin.Value == "Customer" && hdnIsMainPortfolio.Value == "1")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Permisssion denied for Manage Portfolio !!');", true);
                    else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
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
                FunctionInfo.Add("Method", "ViewGovtSavings.ascx:ddlMenu_SelectedIndexChanged()");
                object[] objects = new object[1];
                objects[1] = govtSavingsVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvrGovtSavings_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvrGovtSavings.PageIndex = e.NewPageIndex;
            //gvrGovtSavings.DataBind();
        }

        protected void gvrGovtSavings_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                hdnSort.Value = sortExpression + " DESC";
              // SortGridVIew(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                hdnSort.Value = sortExpression + " ASC";
             //  SortGridVIew(sortExpression, ASCENDING);
            }
            this.LoadGridview(portfolioId);
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

         
        }

        protected void gvrGovtSavings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GovtSavingsBo govtSavingsBo=new GovtSavingsBo();
            //try
            //{
            //    string govtSavingId = "";
            //    int index = Convert.ToInt16(e.CommandArgument.ToString());
            //    int portfolioId = int.Parse(gvGovtSavings.MasterTableView.DataKeyValues[index].ToString());
            //    govtSavingId = gvGovtSavings.MasterTableView.DataKeyValues[index]["GovtSavingId"].ToString();
            //    hdndeleteId.Value = govtSavingId;
            //    Session["govtSavingsVo"] = govtSavingsBo.GetGovtSavingsDetails(portfolioId);
            //    if (e.CommandName.ToString() == "Edit")
            //    {
            //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('PortfolioGovtSavingsEntry','EditGS');", true);
            //    }
            //    else if (e.CommandName.ToString() == "View")
            //    {
            //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('PortfolioGovtSavingsEntry','ViewGS');", true);
            //    }
            //    else if (e.CommandName.ToString() == "Delete")
            //    {
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
            //    }
            //    else if (e.CommandName.ToString() == "Sort")
            //    {
            //        string sortExpression = e.CommandArgument.ToString();
            //        ViewState["sortExpression"] = sortExpression;
            //        if (GridViewSortDirection == SortDirection.Ascending)
            //        {
            //            GridViewSortDirection = SortDirection.Descending;
            //            SortGridVIew(sortExpression, DESCENDING);
            //        }
            //        else
            //        {
            //            GridViewSortDirection = SortDirection.Ascending;
            //            SortGridVIew(sortExpression, ASCENDING);
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
            //    FunctionInfo.Add("Method", "ViewGovtSavings.ascx:gvrGovtSavings_RowCommand()");
            //    object[] objects = new object[1];
            //    objects[1] = portfolioId;
            //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;

            //}
        }

        protected void gvrGovtSavings_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvGovtSavings_ItemCommand(object sender, GridCommandEventArgs e)
        {
         if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GovtSavingsBo govtSavingsBo = new GovtSavingsBo();
                string govtSavingId = "";              
                int portfolioId = int.Parse(gvGovtSavings.MasterTableView.DataKeyValues[e.Item.ItemIndex].ToString());
                govtSavingId = gvGovtSavings.MasterTableView.DataKeyValues[e.Item.ItemIndex]["GovtSavingId"].ToString();
                hdndeleteId.Value = govtSavingId;
                Session["govtSavingsVo"] = govtSavingsBo.GetGovtSavingsDetails(portfolioId);
                if (e.CommandName == "Edit")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('PortfolioGovtSavingsEntry','EditGS');", true);
                }
                else if (e.CommandName.ToString() == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('PortfolioGovtSavingsEntry','ViewGS');", true);
                }
                else if (e.CommandName.ToString() == "Delete")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
                }               
            }
           
        }
        protected void gvrGovtSavings_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvGovtSavings.ExportSettings.OpenInNewWindow = true;
            gvGovtSavings.ExportSettings.IgnorePaging = true;
            gvGovtSavings.ExportSettings.HideStructureColumns = true;
            gvGovtSavings.ExportSettings.ExportOnlyData = true;
            gvGovtSavings.ExportSettings.FileName = "Govt Savings";
            gvGovtSavings.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvGovtSavings.MasterTableView.ExportToExcel();
        }
        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                govtSavingsBo.DeleteGovtSavingsPortfolio(int.Parse(hdndeleteId.Value));
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewGovtSavings','login');", true);
                msgRecordStatus.Visible = true;
            }
        }
    }
}