using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoCommon;
using System.Configuration;
using Telerik.Web.UI;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioSystematicView : System.Web.UI.UserControl
    {
        SystematicSetupBo systematicSetupBo = new SystematicSetupBo();
        SystematicSetupVo systematicSetupVo;
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountsBo = new CustomerAccountBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        UserVo userVo = new UserVo();

        Dictionary<int, int> genDictPortfolioDetails = new Dictionary<int, int>();
        int portfolioId;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        //protected override void OnInit(EventArgs e)
        //{

        //    try
        //    {
        //        ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
        //        mypager.EnableViewState = true;
        //        base.OnInit(e);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "PortfolioSystematicView.ascx.cs:OnInit()");
        //        object[] objects = new object[0];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }


        //}

        //public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        GetPageCount();
        //        customerVo = (CustomerVo)Session["customerVo"];
        //        portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
        //        LoadSystematicSetupGrid(portfolioId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "PortfolioSystematicView.ascx.cs:HandlePagerEvent()");
        //        object[] objects = new object[2];
        //        objects[0] = customerVo;
        //        objects[1] = portfolioId;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //}

        //private void GetPageCount()
        //{
        //    string upperlimit = "";
        //    int rowCount = 0;
        //    int ratio = 0;
        //    string lowerlimit = "";
        //    string PageRecords = "";
        //    try
        //    {
        //        rowCount = Convert.ToInt32(hdnRecordCount.Value);
        //        ratio = rowCount / 10;
        //        mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
        //        mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
        //        lowerlimit = (((mypager.CurrentPage - 1) * 10)+1).ToString();
        //        upperlimit = (mypager.CurrentPage * 10).ToString();
        //        if (mypager.CurrentPage == mypager.PageCount)
        //            upperlimit = hdnRecordCount.Value;
        //        PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
        //        lblCurrentPage.Text = PageRecords;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "PortfolioSystematicView.ascx.cs:GetPageCount()");
        //        object[] objects = new object[5];
        //        objects[0] = upperlimit;
        //        objects[1] = rowCount;
        //        objects[2] = ratio;
        //        objects[3] = lowerlimit;
        //        objects[4] = PageRecords;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                customerVo = (CustomerVo)Session["customerVo"];
                userVo = (UserVo)Session["userVo"];

                portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
                 if (!Page.IsPostBack)
                {
                    //remove what ever is in the cache
                    Cache.Remove("SIP Details" + customerVo.CustomerId.ToString());
               
                   
                    BindPortfolioDropDown(customerVo.CustomerId);
                    ddlportfolio.SelectedValue = portfolioId.ToString();
                    LoadSystematicSetupGrid(portfolioId);

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
                FunctionInfo.Add("Method", "PortfolioSystematicView.ascx:Page_Load()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void LoadSystematicSetupGrid(int portfolioId)
        {
            List<SystematicSetupVo> systematicSetupList = new List<SystematicSetupVo>();
            try
            {
                string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());  
                systematicSetupList = systematicSetupBo.GetSystematicSchemeSetupList(portfolioId);
                    
                    gvrSystematicSchemes.Visible = true;
                    DataTable dtSystematicSetup = new DataTable();
                    dtSystematicSetup.Columns.Add("SystematicSetupId");
                    dtSystematicSetup.Columns.Add("Scheme Name");
                    dtSystematicSetup.Columns.Add("Folio");
                    dtSystematicSetup.Columns.Add("Systematic Transaction Type");
                    dtSystematicSetup.Columns.Add("Start Date");
                    dtSystematicSetup.Columns.Add("End Date");
                   // dtSystematicSetup.Columns.Add("Systematic Date");
                    dtSystematicSetup.Columns.Add("HistoricalCreated");
                    dtSystematicSetup.Columns.Add("AutoTransaction");
                 
                    dtSystematicSetup.Columns.Add("Amount",typeof (double));
                    dtSystematicSetup.Columns.Add("Frequency");
                    dtSystematicSetup.Columns.Add("SubBrokerCode");
                    DataRow drSystematicSetup;
                    if (systematicSetupList != null)
                    {
                        imgBtnrgHoldings.Visible = true;
                        for (int i = 0; i < systematicSetupList.Count; i++)
                        {
                            drSystematicSetup = dtSystematicSetup.NewRow();
                            systematicSetupVo = new SystematicSetupVo();
                            systematicSetupVo = systematicSetupList[i];
                            drSystematicSetup[0] = systematicSetupVo.SystematicSetupId.ToString();
                            drSystematicSetup[1] = systematicSetupVo.SchemePlan.ToString();//add to Vo scheme name and use join in SP
                            drSystematicSetup[2] = systematicSetupVo.Folio.ToString();//add folio to Vo and use join in SP
                            drSystematicSetup[3] = systematicSetupVo.SystematicType.ToString();//join
                            drSystematicSetup[4] = systematicSetupVo.StartDate.ToShortDateString();
                            drSystematicSetup[5] = systematicSetupVo.EndDate.ToShortDateString();
                          
                            drSystematicSetup[6] = systematicSetupVo.IsHistoricalCreated.ToString();
                            drSystematicSetup[7] = systematicSetupVo.IsAutoTransaction.ToString();
                          
                            drSystematicSetup[8] = systematicSetupVo.Amount;
                            drSystematicSetup[9] = systematicSetupVo.Frequency.ToString();//join
                            drSystematicSetup["SubBrokerCode"] = systematicSetupVo.SubBrokerCode;

                            dtSystematicSetup.Rows.Add(drSystematicSetup);
                        }

                        gvrSystematicSchemes.DataSource = dtSystematicSetup;
                        gvrSystematicSchemes.DataBind();
                        if (Cache["SIP Details" + customerVo.CustomerId.ToString()] == null)
                        {
                            Cache.Insert("SIP Details" + customerVo.CustomerId.ToString(), dtSystematicSetup);
                        }
                        else
                        {
                            Cache.Remove("SIP Details" + customerVo.CustomerId.ToString());
                            Cache.Insert("SIP Details" + customerVo.CustomerId.ToString(), dtSystematicSetup);
                        }

                    }
                    else
                    {
                        Cache.Remove("SIP Details" + customerVo.CustomerId.ToString());
                        imgBtnrgHoldings.Visible = false;
                        gvrSystematicSchemes.DataSource = dtSystematicSetup;
                        gvrSystematicSchemes.DataBind();
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
                FunctionInfo.Add("Method", "PortfolioSystematicView.ascx:LoadSystematicSetupGrid()");
                object[] objects = new object[1];
                objects[0] = systematicSetupVo;
                objects[1] = systematicSetupList;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RadComboBox ddlAction = (RadComboBox)sender;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1; ;
                int systematicSetupId = Convert.ToInt32(gvrSystematicSchemes.MasterTableView.DataKeyValues[selectedRow - 1]["SystematicSetupId"].ToString());
                

                //DropDownList ddlAction = (DropDownList)sender;
                //GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
                //int selectedRow = gvr.RowIndex;
                //int systematicSetupId = int.Parse(gvrSystematicSchemes.DataKeys[selectedRow].Value.ToString());

                // Set the VO into the Session
                systematicSetupVo = systematicSetupBo.GetSystematicSchemeSetupDetails(systematicSetupId);
                Session["systematicSetupVo"] = systematicSetupVo;
                //Session["customerAccountVo"] = customerAccountsBo.GetCustomerPropertyAccount(systematicSetupVo.AccountId);

                if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {
                    if (hdnIsCustomerLogin.Value == "Customer" && hdnIsMainPortfolio.Value == "1")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Permisssion denied for Manage Portfolio !!');", true);
                    else
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioSystematicEntry','action=edit');", true);
                }
                if (ddlAction.SelectedItem.Value.ToString() == "View")
                {
                    
                        

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioSystematicEntry','action=view');", true);
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
                FunctionInfo.Add("Method", "PortfolioProperty.ascx:ddlMenu_SelectedIndexChanged()");
                object[] objects = new object[1];
                objects[0] = systematicSetupVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        //protected void gvrSystematicSetup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvrSystematicSchemes.PageIndex = e.NewPageIndex;
        //    gvrSystematicSchemes.DataBind();
        //}

        //protected void gvrSystematicSetup_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    try
        //    {
        //        string sortExpression = e.SortExpression;
        //        ViewState["sortExpression"] = sortExpression;
        //        if (GridViewSortDirection == SortDirection.Ascending)
        //        {
        //            GridViewSortDirection = SortDirection.Descending;
        //            hdnSort.Value = sortExpression + " DESC";
        //        }
        //        else
        //        {
        //            GridViewSortDirection = SortDirection.Ascending;
        //            hdnSort.Value = sortExpression + " ASC";
        //        }
        //        customerVo = (CustomerVo)Session["customerVo"];
        //        portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
        //        LoadSystematicSetupGrid(portfolioId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "PortfolioSystematicView.ascx:gvrSystematicSetup_Sorting()");
        //        object[] objects = new object[2];
        //        objects[0] = customerVo;
        //        objects[1] = portfolioId;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //}

        //private SortDirection GridViewSortDirection
        //{
        //    get
        //    {
        //        if (ViewState["sortDirection"] == null)
        //            ViewState["sortDirection"] = SortDirection.Ascending;
        //        return (SortDirection)ViewState["sortDirection"];
        //    }
        //    set { ViewState["sortDirection"] = value; }
        //}

        //private void sortGridViewBranches(string sortExpression, string direction)
        //{


        //}

        //protected void gvrSystematicSetup_DataBound(object sender, EventArgs e)
        //{

        //}

        private void BindPortfolioDropDown(int CustomerId)
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(CustomerId);
            ddlportfolio.DataSource = ds;
            ddlportfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlportfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlportfolio.DataBind();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                genDictPortfolioDetails.Add(int.Parse(dr["CP_PortfolioId"].ToString()), int.Parse(dr["CP_IsMainPortfolio"].ToString()));
            }

            var keyValuePair = genDictPortfolioDetails.FirstOrDefault(x => x.Key == portfolioId);

            hdnIsMainPortfolio.Value = keyValuePair.Value.ToString();
            Session["genDictPortfolioDetails"] = genDictPortfolioDetails;
            hdnIsCustomerLogin.Value = userVo.UserType;

           
        }

        protected void ddlportfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlportfolio.SelectedValue);
            LoadSystematicSetupGrid(portfolioId);
            if (Session["genDictPortfolioDetails"] != null)
            {
                genDictPortfolioDetails = (Dictionary<int, int>)Session["genDictPortfolioDetails"];
            }
            var keyValuePair = genDictPortfolioDetails.FirstOrDefault(x => x.Key == portfolioId);
            //int value = keyValuePair.Value;

            hdnIsMainPortfolio.Value = keyValuePair.Value.ToString();
            hdnIsCustomerLogin.Value = userVo.UserType;
        }


        protected void gvMFFolio_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtProcessLogDetails = new DataTable();
            dtProcessLogDetails = (DataTable)Cache["SIP Details" + customerVo.CustomerId.ToString()];
            gvrSystematicSchemes.DataSource = dtProcessLogDetails;
        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {

            //Cache["SIP Details" + customerVo.CustomerId.ToString()]

            DataTable dtFolioDetails = new DataTable();
            dtFolioDetails = (DataTable)Cache["SIP Details" + customerVo.CustomerId.ToString()];
            gvrSystematicSchemes.DataSource = dtFolioDetails;
            gvrSystematicSchemes.ExportSettings.OpenInNewWindow = true;
            gvrSystematicSchemes.ExportSettings.IgnorePaging = true;
            gvrSystematicSchemes.ExportSettings.HideStructureColumns = true;
            gvrSystematicSchemes.ExportSettings.ExportOnlyData = true;
            gvrSystematicSchemes.ExportSettings.FileName = "SIP Details";
            gvrSystematicSchemes.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvrSystematicSchemes.MasterTableView.ExportToExcel();
        }


    }
}
