using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
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
    public partial class PortfolioProperty : System.Web.UI.UserControl
    {
        PropertyBo propertyBo = new PropertyBo();
        PropertyVo propertyVo;
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountsBo = new CustomerAccountBo();

        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        static int portfolioId;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
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
                FunctionInfo.Add("Method", "PortfolioProperty.ascx.cs:OnInit()");
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
                customerVo = (CustomerVo)Session["customerVo"];               
                LoadPropertyGrid(portfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioProperty.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = portfolioId;
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
                FunctionInfo.Add("Method", "PortfolioProperty.ascx.cs:GetPageCount()");
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
                userVo = (UserVo)Session["userVo"];
                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                if (!IsPostBack)
                {                    
                    BindPortfolioDropDown();
                    LoadPropertyGrid(portfolioId);
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
                FunctionInfo.Add("Method", "PortfolioProperty.ascx:Page_Load()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
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
            LoadPropertyGrid(portfolioId);

            if (Session["genDictPortfolioDetails"] != null)
            {
                genDictPortfolioDetails = (Dictionary<int, int>)Session["genDictPortfolioDetails"];
            }
            var keyValuePair = genDictPortfolioDetails.FirstOrDefault(x => x.Key == portfolioId);
            //int value = keyValuePair.Value;

            hdnIsMainPortfolio.Value = keyValuePair.Value.ToString();
            hdnIsCustomerLogin.Value = userVo.UserType;

        }
        public void LoadPropertyGrid(int portfolioId)
        {
            List<PropertyVo> propertyList = new List<PropertyVo>();
            try
            {
                string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                int count;
              
                propertyList = propertyBo.GetPropertyPortfolio(portfolioId, mypager.CurrentPage, hdnSort.Value, out count);
                if (count > 0)
                {
                    lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                    tblPager.Visible = true;
                }
                if (propertyList != null)
                {
                    //lblMsg.Visible = false;
                    tblMessage.Visible = false;
                    ErrorMessage.Visible = false;
                    ErrorMessage.InnerText = "No Records Found...!";
                    DataTable dtProperty = new DataTable();
                    //dtProperty.Columns.Add("SI.No");
                    dtProperty.Columns.Add("PropertyId");
                    dtProperty.Columns.Add("Sub Category");
                    dtProperty.Columns.Add("Particulars");
                    dtProperty.Columns.Add("City");
                    dtProperty.Columns.Add("Area");
                    dtProperty.Columns.Add("Measurement Unit");
                    dtProperty.Columns.Add("Purchase Date");
                    dtProperty.Columns.Add("Purchase Cost", typeof(Double));
                    dtProperty.Columns.Add("Current Value", typeof(Double));
                    dtProperty.Columns.Add("JntName");
                    dtProperty.Columns.Add("NName");
                    DataRow drProperty;
                    for (int i = 0; i < propertyList.Count; i++)
                    {
                        drProperty = dtProperty.NewRow();
                        propertyVo = new PropertyVo();
                        propertyVo = propertyList[i];
                        //drProperty[0] = (i + 1).ToString();
                        drProperty[0] = propertyVo.PropertyId.ToString();
                        drProperty[1] = propertyVo.AssetSubCategoryName.ToString();
                        drProperty[2] = propertyVo.Name.ToString();
                        drProperty[10] = propertyVo.JointHolderName.ToString();
                        drProperty[9] = propertyVo.Nominee.ToString();
                        drProperty[3] = propertyVo.PropertyCity.ToString();
                        drProperty[4] = propertyVo.Quantity.ToString("f0");
                        drProperty[5] = XMLBo.GetMeasureCodeName(path, propertyVo.MeasureCode.ToString());
                        if (propertyVo.PurchaseDate != DateTime.MinValue)
                            drProperty[6] = propertyVo.PurchaseDate.ToString("dd/MM/yyyy");
                        else
                            drProperty[6] = string.Empty;
                        drProperty[7] = propertyVo.PurchaseValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drProperty[8] = propertyVo.CurrentValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                        dtProperty.Rows.Add(drProperty);
                    }
                    gvrProperty.DataSource = dtProperty;
                    gvrProperty.DataBind();
                    if (Cache["gvrProperty" + userVo.UserId.ToString()] == null)
                    {
                        Cache.Insert("gvrProperty" + userVo.UserId.ToString(), dtProperty);
                    }
                    else
                    {
                        Cache.Remove("gvrProperty" + userVo.UserId.ToString());
                    }
                }
                else
                {
                    //lblMsg.Visible = true;
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "No Records Found...!";
                    gvrProperty.DataSource = null;
                    gvrProperty.DataBind();
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
                FunctionInfo.Add("Method", "PortfolioProperty.ascx:LoadPropertyGrid()");
                object[] objects = new object[2];
                objects[0] = propertyVo;
                objects[1] = propertyList;
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
                //GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;                
                //int selectedRow = gvr.RowIndex;
                //int propertyId = int.Parse(gvrProperty.DataKeys[selectedRow].Value.ToString());
                DropDownList ddlAction = (DropDownList)sender;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;
                int propertyId = int.Parse(gvrProperty.MasterTableView.DataKeyValues[selectedRow - 1]["PropertyId"].ToString());
                hdndeleteId.Value = propertyId.ToString();

                // Set the VO into the Session
                propertyVo = propertyBo.GetPropertyAsset(propertyId);
                Session["propertyVo"] = propertyVo;
                Session["customerAccountVo"] = customerAccountsBo.GetCustomerPropertyAccount(propertyVo.AccountId);

                if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {
                    if (hdnIsCustomerLogin.Value == "Customer" && hdnIsMainPortfolio.Value == "1")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Permisssion denied for Manage Portfolio !!');", true);
                    else
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioPropertyEntry','action=Edit');", true);
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioPropertyEntry','action=View');", true);
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "Delete")
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
                FunctionInfo.Add("Method", "PortfolioProperty.ascx:ddlMenu_SelectedIndexChanged()");
                object[] objects = new object[1];
                objects[0] = propertyVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {

            gvrProperty.ExportSettings.OpenInNewWindow = true;
            gvrProperty.ExportSettings.IgnorePaging = true;
            gvrProperty.ExportSettings.HideStructureColumns = true;
            gvrProperty.ExportSettings.ExportOnlyData = true;
            gvrProperty.ExportSettings.FileName = "Portfolio Property";
           // gvrProperty.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvrProperty.MasterTableView.GetColumn("action").Visible = false;
            gvrProperty.MasterTableView.ExportToExcel();
        }
        protected void gvrProperty_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            gvrProperty.Visible = true;
            DataTable dtProperty = new DataTable();
            btnExportFilteredData.Visible = true;
            if (Cache["gvrProperty" + userVo.UserId.ToString()] != null)
            {
                dtProperty = (DataTable)Cache["gvrProperty" + userVo.UserId.ToString()];
                gvrProperty.DataSource = dtProperty;
            }
        }




        protected void gvrProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           // gvrProperty.PageIndex = e.NewPageIndex;
            gvrProperty.DataBind();
        }

        protected void gvrProperty_Sorting(object sender, GridViewSortEventArgs e)
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
                portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
                LoadPropertyGrid(portfolioId);
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

        protected void gvrProperty_DataBound(object sender, EventArgs e)
        {
          
        }

        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                propertyBo.DeletePropertyPortfolio(int.Parse(hdndeleteId.Value));
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioProperty','login');", true);
                msgRecordStatus.Visible = true;
            }
        }

    }
}