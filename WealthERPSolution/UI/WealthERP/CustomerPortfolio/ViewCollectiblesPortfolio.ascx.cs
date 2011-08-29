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
using WealthERP.Base;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class ViewCollectiblesPortfolio : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CollectiblesBo collectiblesBo = new CollectiblesBo();
        CollectiblesVo collectiblesVo = new CollectiblesVo();
        List<CollectiblesVo> collectiblesList = new List<CollectiblesVo>();
        int collectibleId;

        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        static int portfolioId;
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
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
                FunctionInfo.Add("Method", "ViewCollectiblesPortfolio.ascx.cs:OnInit()");
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
                FunctionInfo.Add("Method", "ViewCollectiblesPortfolio.ascx.cs:HandlePagerEvent()");
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
                FunctionInfo.Add("Method", "ViewCollectiblesPortfolio.ascx.cs:GetPageCount()");
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
                BindPortfolioDropDown();
                this.BindData();
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
            BindData();

        }
        protected void BindData()
        {
            try
            {
                int count;
                customerVo = (CustomerVo)Session["CustomerVo"];
                
                collectiblesList = collectiblesBo.GetCollectiblesPortfolio(portfolioId, mypager.CurrentPage, hdnSort.Value, out count);
                if (count > 0)
                    DivPager.Style.Add("display", "visible");
                lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                if (collectiblesList == null)
                {
                    //lblMessage.Visible = true;
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "No Records Found...!";
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
                    DivPager.Visible = false;
                    gvCollectiblesPortfolio.DataSource = null;
                    gvCollectiblesPortfolio.DataBind();
                }
                else
                {
                    //lblMessage.Visible = false;
                    tblMessage.Visible = false;
                    ErrorMessage.Visible = false;
                    ErrorMessage.InnerText = "No Records Found...!";
                    lblTotalRows.Visible = true;
                    lblCurrentPage.Visible = true;
                    DivPager.Visible = true;
                    DataTable dtCollectiblesPortfolio = new DataTable();

                    dtCollectiblesPortfolio.Columns.Add("CollectibleId");
                    dtCollectiblesPortfolio.Columns.Add("Instrument Category");
                    dtCollectiblesPortfolio.Columns.Add("Particulars");
                    dtCollectiblesPortfolio.Columns.Add("Purchase Date");
                    dtCollectiblesPortfolio.Columns.Add("Purchase Value");
                    dtCollectiblesPortfolio.Columns.Add("Current Value");
                    dtCollectiblesPortfolio.Columns.Add("Remarks");

                    DataRow drCollectiblesPortfolio;

                    for (int i = 0; i < collectiblesList.Count; i++)
                    {
                        drCollectiblesPortfolio = dtCollectiblesPortfolio.NewRow();
                        collectiblesVo = new CollectiblesVo();
                        collectiblesVo = collectiblesList[i];
                        drCollectiblesPortfolio[0] = collectiblesVo.CollectibleId.ToString();
                        drCollectiblesPortfolio[1] = collectiblesVo.AssetCategoryName.ToString();
                        drCollectiblesPortfolio[2] = collectiblesVo.Name.ToString();

                        if (collectiblesVo.PurchaseDate != DateTime.MinValue)
                            drCollectiblesPortfolio[3] = collectiblesVo.PurchaseDate.ToShortDateString().ToString();
                              else
                        
                        drCollectiblesPortfolio[3] = " ";
                            
                        
                        if (collectiblesVo.PurchaseValue.ToString() != "")
                            drCollectiblesPortfolio[4] = String.Format("{0:n2}", decimal.Parse(collectiblesVo.PurchaseValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        else
                            drCollectiblesPortfolio[4] = "0";
                        drCollectiblesPortfolio[5] = String.Format("{0:n2}", decimal.Parse(collectiblesVo.CurrentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drCollectiblesPortfolio[6] = collectiblesVo.Remark.ToString();

                        dtCollectiblesPortfolio.Rows.Add(drCollectiblesPortfolio);
                    }
                    gvCollectiblesPortfolio.DataSource = dtCollectiblesPortfolio;
                    gvCollectiblesPortfolio.DataBind();
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

                FunctionInfo.Add("Method", "ViewCollectiblesPortfolio.ascx:Page_Load()");

                object[] objects = new object[3];
                objects[0] = customerVo;
                objects[1] = collectiblesVo;
                objects[2] = collectiblesList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvCollectiblesPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["isselected"] = true;


            //}
        }

        protected void gvCollectiblesPortfolio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCollectiblesPortfolio.PageIndex = e.NewPageIndex;
            gvCollectiblesPortfolio.DataBind();
        }

        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlAction = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
                int selectedRow = gvr.RowIndex;
                int collectiblesId = int.Parse(gvCollectiblesPortfolio.DataKeys[selectedRow].Value.ToString());
                Session["collectiblesVo"] = collectiblesBo.GetCollectiblesAsset(collectiblesId);
                if (ddlAction.SelectedValue.ToString() == "Edit")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioCollectiblesEntry','action=EditCol');", true);
                }
                if (ddlAction.SelectedValue.ToString() == "View")
                {
                    
                    
                    
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioCollectiblesEntry','action=ViewCol');", true);
                }
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewCollectiblesPortfolio.ascx:ddlAction_OnSelectedIndexChange()");
                object[] objects = new object[1];
                objects[0] = collectiblesVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvCollectiblesPortfolio_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                hdnSort.Value = sortExpression + " DESC";
                this.BindData();
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                hdnSort.Value = sortExpression + " ASC";
                this.BindData();

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



        protected void gvCollectiblesPortfolio_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }

        protected void gvCollectiblesPortfolio_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvCollectiblesPortfolio_PreRender(object sender, EventArgs e)
        {
            if ((Convert.ToBoolean(ViewState["sorted"]) == true) && (Convert.ToBoolean(ViewState["isselected"]) == true))
            {
                int collectiblesId = int.Parse(this.gvCollectiblesPortfolio.DataKeys[this.gvCollectiblesPortfolio.SelectedRow.RowIndex].Value.ToString());
                Session["collectiblesVo"] = collectiblesBo.GetCollectiblesAsset(collectiblesId);
            }
        }

        protected void gvCollectiblesPortfolio_Sorted(object sender, EventArgs e)
        {
            ViewState["sorted"] = true;
        }

        protected void gvCollectiblesPortfolio_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            ViewState["sorted"] = true;
        }

        protected void gvCollectiblesPortfolio_DataBound(object sender, EventArgs e)
        {


        }
    }
}