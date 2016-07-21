﻿using System;
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
    public partial class PortfolioViewDashboard : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        PensionAndGratuitiesVo pensionAndGratuitiesVo = new PensionAndGratuitiesVo();
        PensionAndGratuitiesBo pensionAndGratuitiesBo = new PensionAndGratuitiesBo();
        CustomerAccountBo customerAccountsBo = new CustomerAccountBo();
        static int portfolioId;
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        Dictionary<int, int> genDictPortfolioDetails = new Dictionary<int, int>();

 

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            try
            {
                GetPageCount();
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["customerVo"];
                portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
                LoadGridview(portfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioViewDashboard.ascx.cs:HandlePagerEvent()");
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
                FunctionInfo.Add("Method", "PortfolioViewDashboard.ascx.cs:GetPageCount()");
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
                FunctionInfo.Add("Method", "PortfolioViewDashboard.ascx.cs:Page_Load()");
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

        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();
            //ddlPortfolio.Items.Insert(0, "Select the Portfolio");

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

        public void LoadGridview(int portfolioId)
        {
            List<PensionAndGratuitiesVo> pensionAndGratuitiesList = new List<PensionAndGratuitiesVo>();
            int count;
            try
            {
                pensionAndGratuitiesList = pensionAndGratuitiesBo.GetPensionAndGratuitiesList(portfolioId, mypager.CurrentPage, hdnSort.Value, out count);
                PensionAndGratuitiesVo pensionAndGratuitiesVo;
                if (count > 0)
                {
                    lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                    tblPager.Visible = true;
                    trTotalRows.Visible = true;
                }
                else
                {
                    tblPager.Visible = false;
                    trTotalRows.Visible = false;
                }
                if (pensionAndGratuitiesList != null)
                {
                    //lblMsg.Visible = false;

                    tblMessage.Visible = false;
                    ErrorMessage.Visible = false;
                    ErrorMessage.InnerText = "No Records Found...!";
                    DataTable dtPension = new DataTable();
                    //dtPension.Columns.Add("SI.No");
                    dtPension.Columns.Add("PortfolioId");
                    dtPension.Columns.Add("Organization Name");
                    dtPension.Columns.Add("Category");
                    dtPension.Columns.Add("Deposit Amount");
                    dtPension.Columns.Add("Current Value");
                    DataRow drPension;
                    for (int i = 0; i < pensionAndGratuitiesList.Count; i++)
                    {
                        drPension = dtPension.NewRow();
                        pensionAndGratuitiesVo = new PensionAndGratuitiesVo();
                        pensionAndGratuitiesVo = pensionAndGratuitiesList[i];
                        //drPension[0] = (i + 1).ToString();
                        drPension[0] = pensionAndGratuitiesVo.PensionGratuitiesPortfolioId.ToString();
                        drPension[1] = pensionAndGratuitiesVo.OrganizationName.ToString();
                        drPension[2] = pensionAndGratuitiesVo.AssetInstrumentCategoryName.ToString();
                        drPension[3] = String.Format("{0:n2}", decimal.Parse(pensionAndGratuitiesVo.DepositAmount.ToString("f2")));
                        drPension[4] = String.Format("{0:n2}", decimal.Parse(pensionAndGratuitiesVo.CurrentValue.ToString("f2")));
                        dtPension.Rows.Add(drPension);

                    }

                    gvrPensionAndGratuities.DataSource = dtPension;
                    gvrPensionAndGratuities.DataBind();
                    gvrPensionAndGratuities.Visible = true;
                    this.GetPageCount();
                }
                else
                {
                    gvrPensionAndGratuities.DataSource = null;
                    gvrPensionAndGratuities.DataBind();
                    //lblMsg.Visible = true;

                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "No Records Found...!";
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
                FunctionInfo.Add("Method", "PortfolioViewDashboard.ascx.cs:LoadGridview()");
                object[] objects = new object[2];

                objects[0] = pensionAndGratuitiesList;
                objects[1] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvrPensionAndGratuities_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string menu;

            try
            {
                DropDownList MyDropDownList = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)MyDropDownList.NamingContainer;
                int selectedRow = gvr.RowIndex;
                //portfolioId = int.Parse(gvrPensionAndGratuities.DataKeys[selectedRow].Value.ToString());
                portfolioId = int.Parse(gvrPensionAndGratuities.MasterTableView.DataKeyValues[selectedRow - 1]["portfolioId"].ToString());

                hdndeleteId.Value = portfolioId.ToString();
                pensionAndGratuitiesVo = pensionAndGratuitiesBo.GetPensionAndGratuities(portfolioId);
                Session["pensionAndGratuitiesVo"] = pensionAndGratuitiesVo;
                Session["customerAccountVo"] = customerAccountsBo.GetCustomerPensionAndGratuitiesAccount(pensionAndGratuitiesVo.AccountId);

                menu = MyDropDownList.SelectedItem.Value.ToString();
                if (menu == "Edit")
                {
                    if (hdnIsCustomerLogin.Value == "Customer" && hdnIsMainPortfolio.Value == "1")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Permisssion denied for Manage Portfolio !!');", true);
                    else
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PensionAndGratuities','action=edit');", true);
                }
                if (menu == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PensionAndGratuities','action=view');", true);
                }
                if (menu == "Delete")
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

                FunctionInfo.Add("Method", "PensionPortfolio.ascx:ddlMenu_SelectedIndexChanged()");

                object[] objects = new object[3];
                objects[0] = pensionAndGratuitiesBo;
                objects[1] = pensionAndGratuitiesVo;
                objects[2] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        //protected void gvrPensionAndGratuities_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvrPensionAndGratuities.PageIndex = e.NewPageIndex;
        //    gvrPensionAndGratuities.DataBind();
        //}

        protected void gvrPensionAndGratuities_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                hdnSort.Value = sortExpression + " DESC";
                //  sortGridViewBranches(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                hdnSort.Value = sortExpression + " ASC";
                //    sortGridViewBranches(sortExpression, ASCENDING);
            }
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["customerVo"];
            portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
            LoadGridview(portfolioId);
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

        private void sortGridViewBranches(string sortExpression, string direction)
        {

        }

        protected void gvrPensionAndGratuities_DataBound(object sender, EventArgs e)
        {

        }
        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvrPensionAndGratuities.ExportSettings.OpenInNewWindow = true;
            gvrPensionAndGratuities.ExportSettings.IgnorePaging = true;
            gvrPensionAndGratuities.ExportSettings.HideStructureColumns = true;
            gvrPensionAndGratuities.ExportSettings.ExportOnlyData = true;
            gvrPensionAndGratuities.ExportSettings.FileName = "Pension And Gratuities Details";
           // gvrPensionAndGratuities.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvrPensionAndGratuities.MasterTableView.ExportToExcel();
        }

        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                pensionAndGratuitiesBo.DeletePensionAndGratuitiesPortfolio(int.Parse(hdndeleteId.Value), pensionAndGratuitiesVo.AccountId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PensionPortfolio','login');", true);
                msgRecordStatus.Visible = true;
            }
        }
    }
}
