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

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioProperty : System.Web.UI.UserControl
    {
        PropertyBo propertyBo = new PropertyBo();
        PropertyVo propertyVo;
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountsBo = new CustomerAccountBo();

        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        static int portfolioId;
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
                if (!IsPostBack)
                {
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
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

        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {

            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            LoadPropertyGrid(portfolioId);

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
                    dtProperty.Columns.Add("Purchase Cost");
                    dtProperty.Columns.Add("Current Value");
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
                    this.GetPageCount();
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
                DropDownList ddlAction = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
                int selectedRow = gvr.RowIndex;
                int propertyId = int.Parse(gvrProperty.DataKeys[selectedRow].Value.ToString());

                // Set the VO into the Session
                propertyVo = propertyBo.GetPropertyAsset(propertyId);
                Session["propertyVo"] = propertyVo;
                Session["customerAccountVo"] = customerAccountsBo.GetCustomerPropertyAccount(propertyVo.AccountId);

                if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioPropertyEntry','action=Edit');", true);
                }
                if (ddlAction.SelectedItem.Value.ToString() == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioPropertyEntry','action=View');", true);
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

        protected void gvrProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvrProperty.PageIndex = e.NewPageIndex;
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

    }
}