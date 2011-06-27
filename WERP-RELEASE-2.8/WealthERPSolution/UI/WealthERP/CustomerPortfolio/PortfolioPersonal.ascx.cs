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
using WealthERP.Base;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioPersonal : System.Web.UI.UserControl
    {
        PersonalBo personalBo = new PersonalBo();
        PersonalVo personalVo;
        CustomerVo customerVo = new CustomerVo();
        static int portfolioId;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
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
                FunctionInfo.Add("Method", "PortfolioPersonal.ascx.cs:OnInit()");
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
                this.LoadPersonalGrid();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioPersonal.ascx.cs:HandlePagerEvent()");
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
                if (hdnRecordCount.Value != "")
                    rowCount = Convert.ToInt32(hdnRecordCount.Value);
                if (rowCount > 0)
                {
                    ratio = rowCount / 10;
                    mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    lowerlimit = (((mypager.CurrentPage - 1) * 10)+1).ToString();
                    upperlimit = (mypager.CurrentPage * 10).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnRecordCount.Value;
                    PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;
                    hdnCurrentPage.Value = mypager.CurrentPage.ToString();
                }
            }
         



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
            //    lowerlimit = ((mypager.CurrentPage - 1) * 10).ToString();
            //    upperlimit = (mypager.CurrentPage * 10).ToString();
            //    if (mypager.CurrentPage == mypager.PageCount)
            //        upperlimit = hdnRecordCount.Value;
            //    PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
            //    lblCurrentPage.Text = PageRecords;
            //}
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioPersonal.ascx.cs:GetPageCount()");
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
            LoadPersonalGrid();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                customerVo = (CustomerVo)Session["customerVo"];
                if (!IsPostBack)
                {
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();
                    LoadPersonalGrid();
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
                FunctionInfo.Add("Method", "PortfolioPersonal.ascx.cs:Page_Load()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void LoadPersonalGrid()
        {
            List<PersonalVo> personalList = new List<PersonalVo>();
            try
            {
                int count;
                personalList = personalBo.GetPersonalPortfolio(portfolioId, mypager.CurrentPage, hdnSort.Value, out count);
                if (count > 0)
                {
                    lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                   
                   mypager.Visible = true;

                }

                if (personalList != null)
                {
                    lblMsg.Visible = false;
                  
                  mypager.Visible = true;
                    DataTable dtPersonal = new DataTable();
                    dtPersonal.Columns.Add("SI.No");
                    dtPersonal.Columns.Add("PersonalId");
                    dtPersonal.Columns.Add("Name");
                    dtPersonal.Columns.Add("Category");
                    dtPersonal.Columns.Add("Quantity");
                    dtPersonal.Columns.Add("Current Value");
                    dtPersonal.Columns.Add("Purchase Value");
                    dtPersonal.Columns.Add("Purchase Date");
                    DataRow drPersonal;
                    for (int i = 0; i < personalList.Count; i++)
                    {
                        drPersonal = dtPersonal.NewRow();
                        personalVo = new PersonalVo();
                        personalVo = personalList[i];
                        drPersonal[0] = (i + 1).ToString();
                        drPersonal[1] = personalVo.PersonalPortfolioId.ToString();
                        drPersonal[2] = personalVo.Name.ToString();
                        drPersonal[3] = personalVo.AssetSubCategoryName.ToString();
                        drPersonal[4] = personalVo.Quantity.ToString("f0");
                        if (Convert.ToDecimal(personalVo.CurrentValue.ToString()) == 0)
                            drPersonal[5] = "0";
                        else
                            drPersonal[5] = decimal.Parse(personalVo.CurrentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                      
                       // drPersonal[5] = String.Format("{0:n2}", decimal.Parse(personalVo.CurrentValue.ToString("f2")));
                        if (Convert.ToDecimal(personalVo.PurchaseValue.ToString()) == 0)
                            drPersonal[6] = "0";
                        else
                            drPersonal[6] = decimal.Parse(personalVo.PurchaseValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                      
                       // drPersonal[6] = String.Format("{0:n2}", decimal.Parse(personalVo.PurchaseValue.ToString("f2")));
                        if (personalVo.PurchaseDate != DateTime.MinValue)
                            drPersonal[7] = personalVo.PurchaseDate.ToShortDateString();
                        else
                            drPersonal[7] = "";
                        dtPersonal.Rows.Add(drPersonal);

                    }
                    gvrPersonal.DataSource = dtPersonal;
                    gvrPersonal.DataBind();
                    this.GetPageCount();
                }
                else
                {
                    lblMsg.Visible = true;
                    gvrPersonal.DataSource = null;
                    gvrPersonal.DataBind();
                    mypager.Visible = false;

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
                FunctionInfo.Add("Method", "PortfolioPersonal.ascx.cs:LoadPersonalGrid()");
                object[] objects = new object[2];
                objects[0] = personalList;
                objects[1] = portfolioId;
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

            ddlPortfolio.SelectedValue = portfolioId.ToString();

        }


        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlAction = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
                int selectedRow = gvr.RowIndex;
                int personalsId = int.Parse(gvrPersonal.DataKeys[selectedRow].Value.ToString());

                // Set the VO into the Session
                Session["personalVo"] = personalBo.GetPersonalAsset(personalsId);
                if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioPersonalEntry','action=edit');", true);
                }
                if (ddlAction.SelectedItem.Value.ToString() == "View")
                {

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioPersonalEntry','action=view');", true);
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
                FunctionInfo.Add("Method", "PortfolioPersonal.ascx:ddlMenu_SelectedIndexChanged()");
                //object[] objects = new object[1];
                //objects[0] = collectiblesVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void gvrPersonal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvrPersonal.PageIndex = e.NewPageIndex;
            gvrPersonal.DataBind();
        }

        protected void gvrPersonal_Sorting(object sender, GridViewSortEventArgs e)
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
            this.LoadPersonalGrid();
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


    }
}