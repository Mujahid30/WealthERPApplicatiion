﻿using System;
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
using WealthERP.Base;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;
using Telerik.Web.UI;

namespace WealthERP.CustomerPortfolio
{
    public partial class ViewGoldPortfolio : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        UserVo userVo = new UserVo();
        GoldBo goldBo = new GoldBo();
        GoldVo goldVo = new GoldVo();
        List<GoldVo> goldList = new List<GoldVo>();
        int goldPortfolioId;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        static int portfolioId;
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        Dictionary<int, int> genDictPortfolioDetails = new Dictionary<int, int>();

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
                FunctionInfo.Add("Method", "ViewGoldPortfolio.ascx.cs:OnInit()");
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
                this.BindData();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewGoldPortfolio.ascx.cs:HandlePagerEvent()");
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
                //lowerlimit = ((mypager.CurrentPage - 1) * 10).ToString();
                lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
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
                FunctionInfo.Add("Method", "ViewGoldPortfolio.ascx.cs:GetPageCount()");
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
            customerVo = (CustomerVo)Session["customerVo"];
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
        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {

            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            BindData();
            if (Session["genDictPortfolioDetails"] != null)
            {
                genDictPortfolioDetails = (Dictionary<int, int>)Session["genDictPortfolioDetails"];
            }
            var keyValuePair = genDictPortfolioDetails.FirstOrDefault(x => x.Key == portfolioId);
            //int value = keyValuePair.Value;

            hdnIsMainPortfolio.Value = keyValuePair.Value.ToString();
            hdnIsCustomerLogin.Value = userVo.UserType;


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            this.Page.Culture = "en-GB";
            portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
            userVo = (UserVo)Session["userVo"];
            if (!IsPostBack)
            {               
                BindPortfolioDropDown();
                this.BindData();
            }

        }

        protected void BindData()
        {

            try
            {
                customerVo = (CustomerVo)Session["CustomerVo"];

                int count;
                goldList = goldBo.GetGoldNetPosition(portfolioId, mypager.CurrentPage, hdnSort.Value, out count);
                lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                if (count > 0)
                    DivPager.Style.Add("display", "visible");
                if (goldList == null)
                {
                    //lblMessage.Visible = true;
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "No Records Found...!";
                    btnUpdateNP.Visible = false;
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
                    gvGoldPortfolio.DataSource = null;
                    gvGoldPortfolio.DataBind();
                }
                else
                {
                    //lblMessage.Visible = false;
                    tblMessage.Visible = false;
                    ErrorMessage.Visible = false;
                    ErrorMessage.InnerText = "No Records Found...!";
                    btnUpdateNP.Visible = true;
                    lblTotalRows.Visible = true;
                    lblCurrentPage.Visible = true;
                    DataTable dtGoldPortfolio = new DataTable();

                    dtGoldPortfolio.Columns.Add("GoldNPId");
                    dtGoldPortfolio.Columns.Add("Instrument Category");
                    dtGoldPortfolio.Columns.Add("Purchase Date");
                    dtGoldPortfolio.Columns.Add("Purchase Value",typeof(Double));
                    dtGoldPortfolio.Columns.Add("Current Value", typeof(Double));
                 
                    dtGoldPortfolio.Columns.Add("Remarks");


                    DataRow drGoldPortfolio;


                    for (int i = 0; i < goldList.Count; i++)
                    {
                        drGoldPortfolio = dtGoldPortfolio.NewRow();
                        goldVo = new GoldVo();
                        goldVo = goldList[i];
                        drGoldPortfolio[0] = goldVo.GoldNPId.ToString();
                        drGoldPortfolio[1] = goldVo.AssetCategoryName.ToString();
                        if (goldVo.PurchaseDate != DateTime.MinValue)
                            drGoldPortfolio[2] = goldVo.PurchaseDate.ToString("dd/MM/yyyy");
                        
                        drGoldPortfolio[3] = goldVo.PurchaseValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drGoldPortfolio[4] = goldVo.CurrentValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drGoldPortfolio[5] = goldVo.Remarks.ToString();

                        dtGoldPortfolio.Rows.Add(drGoldPortfolio);
                    }
                    gvGoldPortfolio.DataSource = dtGoldPortfolio;
                    gvGoldPortfolio.DataBind();
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
                FunctionInfo.Add("Method", "ViewGoldPortfolio.ascx:BindData()");
                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = goldVo;
                objects[2] = goldList;
                objects[3] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void gvGoldPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //goldPortfolioId = int.Parse(gvGoldPortfolio.SelectedDataKey.Value.ToString());
                //goldVo = goldBo.GetGoldPortfolio(goldPortfolioId);
                Session["CustomerVo"] = customerVo;
                if (customerVo.Type == "Individual")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomerIndividualDashboard','none');", true);
                }
                if (customerVo.Type == "Non Individual")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomerNonIndividualDashboard','none');", true);
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
                FunctionInfo.Add("Method", "ViewGoldPortfolio.ascx:gvGoldPortfolio_SelectedIndexChanged()");
                object[] objects = new object[2];
                objects[0] = goldPortfolioId;
                objects[1] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvGoldPortfolio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           // gvGoldPortfolio.PageIndex = e.NewPageIndex;
            gvGoldPortfolio.DataBind();

        }

        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlAction = (DropDownList)sender;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;
                // GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
               // int selectedRow = gvr.RowIndex;
                int goldId = int.Parse(gvGoldPortfolio.MasterTableView.DataKeyValues[selectedRow - 1]["GoldNPId"].ToString());

                hdndeleteId.Value = goldId.ToString();
                Session["goldVo"] = goldBo.GetGoldAsset(goldId);

                if (ddlAction.SelectedItem.Value.ToString() == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioGoldEntry','action=ViewGold');", true);
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {
                    if (hdnIsCustomerLogin.Value == "Customer" && hdnIsMainPortfolio.Value == "1")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Permisssion denied for Manage Portfolio !!');", true);
                    else
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioGoldEntry','action=EditGold');", true);
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "Delete")
                {
                    if (hdnIsCustomerLogin.Value == "Customer" && hdnIsMainPortfolio.Value == "1")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Permisssion denied for Manage Portfolio !!');", true);
                    else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
                }

            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewGoldPortfolio.ascx:ddlAction_OnSelectedIndexChange()");
                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvGoldPortfolio_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                hdnSort.Value = sortExpression + " DESC";

                //SortGridVIew(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                //SortGridVIew(sortExpression, ASCENDING);
                hdnSort.Value = sortExpression + " ASC";
            }
            this.BindData();
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

        protected void btn_Update(object sender, EventArgs e)
        {
            //goldList = goldBo.GetGoldNetPosition(portfolioId, mypager.CurrentPage, hdnSort.Value, out count);

        }
        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            //gvGoldPortfolio.ExportSettings.OpenInNewWindow = true;
            //gvGoldPortfolio.ExportSettings.IgnorePaging = true;
            //gvGoldPortfolio.ExportSettings.HideStructureColumns = true;
            //gvGoldPortfolio.ExportSettings.ExportOnlyData = true;
            //gvGoldPortfolio.ExportSettings.FileName = "Gold Details";
            //gvGoldPortfolio.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            //gvGoldPortfolio.MasterTableView.ExportToExcel();
            gvGoldPortfolio.ExportSettings.OpenInNewWindow = true;
            gvGoldPortfolio.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvGoldPortfolio.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvGoldPortfolio.MasterTableView.ExportToExcel();

        }
        protected void gvGoldPortfolio_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            gvGoldPortfolio.Visible = true;
            DataTable dtGoldPortfolio = new DataTable();

            btnExportFilteredData.Visible = true;
            dtGoldPortfolio = (DataTable)Cache["dtGoldPortfolio + '" + customerVo.CustomerId + "'"];
            gvGoldPortfolio.DataSource = dtGoldPortfolio;
        }
        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                goldBo.DeleteGoldPortfolio(int.Parse(hdndeleteId.Value));
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewGoldPortfolio','login');", true);
                msgRecordStatus.Visible = true;
            }
        }

    }

}