using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using System.Data;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;


namespace WealthERP.Advisor
{
    public partial class CustomerUserDetails : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
      //  List<int> customerList = null;
        AdvisorBo advisorBo = new AdvisorBo();
        int customerId;
        AdvisorVo advisorVo = new AdvisorVo();

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

        protected override void OnInit(EventArgs e)
        {

            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {

            GetPageCount();
            this.BindGrid(mypager.CurrentPage);
        }


        private void GetPageCount()
        {
            string upperlimit = null;
            int rowCount = 0;
            int ratio = 0;
            string lowerlimit = null;
            string PageRecords = null;
            try
            {

                rowCount = Convert.ToInt32(hdnRecordCount.Value);
                ratio = rowCount / 10;
                mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = ((mypager.CurrentPage - 1) * 10).ToString();
                upperlimit = (mypager.CurrentPage * 10).ToString();
                if (mypager.CurrentPage == mypager.PageCount)
                    upperlimit = hdnRecordCount.Value;
                PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
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

                FunctionInfo.Add("Method", "CustomerUserDetails.ascx.cs:GetPageCount()");

                object[] objects = new object[5];
                objects[0] = upperlimit;
                objects[0] = rowCount;
                objects[0] = ratio;
                objects[0] = lowerlimit;
                objects[0] = PageRecords;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo=(AdvisorVo)Session["advisorVo"];

            if (!IsPostBack)
                this.BindGrid(mypager.CurrentPage);
        }

        protected void BindGrid(int CurrentPage)
        {
            List<CustomerVo> customerList = null;

            Dictionary<string, string> genDictParent = new Dictionary<string, string>();
            Dictionary<string, string> genDictRM = new Dictionary<string, string>();
            Dictionary<string, string> genDictReassignRM = new Dictionary<string, string>();
            try
            {
                rmVo = (RMVo)Session["rmVo"];

                int Count = 0;

                customerList = advisorBo.GetAdviserCustomerList(advisorVo.advisorId, mypager.CurrentPage, out Count, "", "", "", "", "","", "", out genDictParent, out genDictRM,out genDictReassignRM);

                lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();

                if (customerList.Count == 0)
                {
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Visible = false;
                    DataTable dtRMCustomer = new DataTable();

                    dtRMCustomer.Columns.Add("S.No");
                    dtRMCustomer.Columns.Add("CustomerId");
                    dtRMCustomer.Columns.Add("Customer Name");
                    dtRMCustomer.Columns.Add("Email");
                    dtRMCustomer.Columns.Add("Phone Number");

                    DataRow drRMCustomer;


                    for (int i = 0; i < customerList.Count; i++)
                    {
                        drRMCustomer = dtRMCustomer.NewRow();
                        customerVo = new CustomerVo();
                        customerVo = customerList[i];
                        drRMCustomer[0] = (i + 1).ToString();
                        drRMCustomer[1] = customerVo.CustomerId.ToString();
                        drRMCustomer[2] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        drRMCustomer[3] = customerVo.Email.ToString();
                        drRMCustomer[4] = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                        dtRMCustomer.Rows.Add(drRMCustomer);
                    }
                    gvCustomers.DataSource = dtRMCustomer;
                    gvCustomers.DataBind();
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

                FunctionInfo.Add("Method", "CustomerUserDetails.ascx.cs:BindGrid()");


                object[] objects = new object[5];
                objects[0] = customerBo;
                objects[1] = rmVo;
                objects[2] = customerVo;
                objects[3] = customerList;
              

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }



        protected void gvCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                customerId = int.Parse(gvCustomers.SelectedDataKey.Value.ToString());
                customerVo = customerBo.GetCustomer(customerId);
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

                FunctionInfo.Add("Method", "RMCustomer.ascx:gvCustomers_SelectedIndexChanged()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = customerVo;



                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomers.PageIndex = e.NewPageIndex;
            gvCustomers.DataBind();
        }

        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        {
            DropDownList ddlAction = null;
            GridViewRow gvr = null;
            int selectedRow = 0;
            try
            {
                ddlAction = (DropDownList)sender;
                gvr = (GridViewRow)ddlAction.NamingContainer;
                selectedRow = gvr.RowIndex;
                customerId = int.Parse(gvCustomers.DataKeys[selectedRow].Value.ToString());
                customerVo = customerBo.GetCustomer(customerId);
                Session["CustomerVo"] = customerVo;

                if (ddlAction.SelectedItem.Value.ToString() == "Dashboard")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdvisorRMCustIndiDashboard','none');", true);
                }
                if (ddlAction.SelectedItem.Value.ToString() == "Profile")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomerIndividualDashboard','none');", true);
                }
                if (ddlAction.SelectedItem.Value.ToString() == "Portfolio")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioDashboard','none');", true);
                }
                if (ddlAction.SelectedItem.Value.ToString() == "Alerts")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMAlertDashBoard','none');", true);
                }

            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerUserDetails.ascx.cs:ddlAction_OnSelectedIndexChange()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = customerVo;
                objects[2] = ddlAction;
                objects[3] = gvr;
                objects[4] = selectedRow;



                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }


        protected void gvCustomers_Sort(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = null;
            try
            {
                sortExpression = e.SortExpression;
                ViewState["sortExpression"] = sortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    hdnSort.Value = sortExpression + " DESC";
                    this.BindGrid(1);
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    hdnSort.Value = sortExpression + " ASC";
                    this.BindGrid(1);

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

                FunctionInfo.Add("Method", "CustomerUserDetails.ascx.cs:gvCustomers_Sort()");

                object[] objects = new object[1];
                objects[0] = sortExpression;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }


    }
}