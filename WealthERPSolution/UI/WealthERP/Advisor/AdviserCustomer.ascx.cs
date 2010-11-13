using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUser;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using System.Data;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System.Xml;
using System.Text;
using iTextSharp.text.html.simpleparser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class AdviserCustomer : System.Web.UI.UserControl
    {


        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        List<CustomerVo> customerList = null;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        int customerId;
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();
        static string user = "";
        static Dictionary<string, string> genDictRM;
        static Dictionary<string, string> genDictReassignRM;
        static string ExportFormat = string.Empty;
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
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
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:OnInit()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
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
                if (hdnRecordCount.Value.ToString() != "")
                    rowCount = Convert.ToInt32(hdnRecordCount.Value);
                if (rowCount > 0)
                {
                    ratio = rowCount / 20;
                    mypager.PageCount = rowCount % 20 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    if (((mypager.CurrentPage - 1) * 20) != 0)
                        lowerlimit = (((mypager.CurrentPage - 1) * 20) + 1).ToString();
                    else
                        lowerlimit = "1";
                    upperlimit = (mypager.CurrentPage * 20).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnRecordCount.Value;
                    PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;
                    hdnCurrentPage.Value = mypager.CurrentPage.ToString();
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

                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:GetPageCount()");

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

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            try
            {
                GetPageCount();
                if (Session["Customer"].ToString() == "Customer")
                {
                    this.BindGrid(mypager.CurrentPage, 0);
                }
                else
                {
                    this.BindCustomer(mypager.CurrentPage);
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
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[2];
                objects[0] = mypager.CurrentPage;
                objects[1] = user;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            if (!IsPostBack)
            {

                //trPageChoice.Visible = false;

                if (Session["Current_Link"].ToString() == "AdvisorLeftPane" || Session["Current_Link"].ToString() == "RMCustomerIndividualLeftPane")
                {
                    if (Session["Customer"].ToString() == "Customer")
                    {
                        this.BindGrid(mypager.CurrentPage, 0);
                    }
                    else
                    {
                        this.BindCustomer(mypager.CurrentPage);
                    }
                }

            }
        }

        protected void BindGrid(int CurrentPage, int export)
        {
            Dictionary<string, string> genDictParent = new Dictionary<string, string>();
            genDictReassignRM = new Dictionary<string, string>();
            genDictRM = new Dictionary<string, string>();

            RMVo customerRMVo = new RMVo();
            CustomerBo customerBo = new CustomerBo();

            try
            {
                DropDownList ddl = new DropDownList();
                Label lbl = new Label();

                adviserVo = (AdvisorVo)Session["advisorVo"];

                if (export == 1)
                {
                    trMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    trPager.Visible = false;
                    lblTotalRows.Visible = false;
                    gvCustomers.AllowPaging = false;
                    customerList = advisorBo.GetAdviserAllCustomerList(adviserVo.advisorId);
                }
                else
                {
                    if (hdnCurrentPage.Value.ToString() != "")
                    {
                        mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                        hdnCurrentPage.Value = "";
                    }

                    int Count;

                    customerList = advisorBo.GetAdviserCustomerList(adviserVo.advisorId, mypager.CurrentPage, out Count, hdnSort.Value, hdnNameFilter.Value, hdnAreaFilter.Value, hdnPincodeFilter.Value, hdnParentFilter.Value, hdnRMFilter.Value, hdnactive.Value, out genDictParent, out genDictRM, out genDictReassignRM);
                    lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
                }

                if (customerList != null)
                {

                    trMessage.Visible = false;
                    ErrorMessage.Visible = false;
                    trPager.Visible = true;
                    lblTotalRows.Visible = true;
                    lblCurrentPage.Visible = true;
                    DataTable dtRMCustomer = new DataTable();
                    dtRMCustomer.Columns.Add("CustomerId");
                    dtRMCustomer.Columns.Add("UserId");
                    dtRMCustomer.Columns.Add("RMId");
                    dtRMCustomer.Columns.Add("Parent");
                    dtRMCustomer.Columns.Add("Cust_Comp_Name");
                    dtRMCustomer.Columns.Add("PAN Number");
                    dtRMCustomer.Columns.Add("Mobile Number");
                    dtRMCustomer.Columns.Add("Phone Number");
                    dtRMCustomer.Columns.Add("Email");
                    dtRMCustomer.Columns.Add("Address");
                    dtRMCustomer.Columns.Add("Area");
                    dtRMCustomer.Columns.Add("City");
                    dtRMCustomer.Columns.Add("Pincode");
                    dtRMCustomer.Columns.Add("Assigned RM");
                    dtRMCustomer.Columns.Add("IsActive");
                    dtRMCustomer.Columns.Add("IsProspect");
                    dtRMCustomer.Columns.Add("IsFPClient");

                    DataRow drRMCustomer;

                    for (int i = 0; i < customerList.Count; i++)
                    {
                        drRMCustomer = dtRMCustomer.NewRow();
                        customerVo = new CustomerVo();
                        customerVo = customerList[i];
                        drRMCustomer[0] = customerVo.CustomerId.ToString();
                        drRMCustomer[1] = customerVo.UserId.ToString();
                        drRMCustomer[2] = customerVo.RmId.ToString();

                        if (customerVo.ParentCustomer != null)
                        {
                            drRMCustomer[3] = customerVo.ParentCustomer.ToString();
                        }
                        drRMCustomer[4] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        //if (customerVo.Type.ToUpper().ToString() == "IND" || customerVo.Type == "")
                        //{
                        //    if (customerVo.ParentCustomer != null)
                        //    {
                        //        drRMCustomer[3] = customerVo.ParentCustomer.ToString();
                        //    }
                        //    else
                        //    {
                        //        drRMCustomer[3] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        //    }
                        //    drRMCustomer[4] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        //}
                        //else if (customerVo.Type.ToUpper().ToString() == "NIND")
                        //{
                        //    if (customerVo.ParentCompany != null)
                        //    {
                        //        drRMCustomer[3] = customerVo.ParentCompany.ToString();
                        //    }
                        //    else
                        //    {
                        //        drRMCustomer[3] = customerVo.CompanyName.ToString();
                        //    }
                        //    drRMCustomer[4] = customerVo.CompanyName.ToString();
                        //}
                        if (customerVo.PANNum != null)
                            drRMCustomer[5] = customerVo.PANNum.ToString();
                        else
                            drRMCustomer[5] = "";
                        drRMCustomer[6] = customerVo.Mobile1.ToString();
                        drRMCustomer[7] = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                        drRMCustomer[8] = customerVo.Email.ToString();
                        if (customerVo.Adr1City == null)
                            customerVo.Adr1City = "";
                        if (customerVo.Adr1Line1 == null)
                            customerVo.Adr1Line1 = "";
                        if (customerVo.Adr1Line2 == null)
                            customerVo.Adr1Line2 = "";
                        if (customerVo.Adr1Line3 == null)
                            customerVo.Adr1Line3 = "";
                        if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() == "")
                        {
                            drRMCustomer[9] = "-";
                        }
                        else if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() != "")
                        {
                            drRMCustomer[9] = customerVo.Adr1Line2.ToString();
                        }
                        else if (customerVo.Adr1Line1.ToString() != "" && customerVo.Adr1Line2.ToString() == "")
                        {
                            drRMCustomer[9] = customerVo.Adr1Line1.ToString();
                        }
                        else
                            drRMCustomer[9] = customerVo.Adr1Line1.ToString() + "," + customerVo.Adr1Line2.ToString();
                        drRMCustomer[10] = customerVo.Adr1Line3.ToString();
                        drRMCustomer[11] = customerVo.Adr1City.ToString();
                        drRMCustomer[12] = customerVo.Adr1PinCode.ToString();
                        //customerRMVo = advisorStaffBo.GetAdvisorStaff(advisorStaffBo.GetUserId(customerVo.RmId));
                        drRMCustomer[13] = customerVo.AssignedRM.ToString();
                        //if (hdnactive.Value == 'A')
                        //{

                        //}
                        drRMCustomer[13] = customerVo.AssignedRM.ToString();
                        if (customerVo.IsActive == 1)
                        {
                            drRMCustomer[14] = "Active";
                        }
                        else
                        {
                            drRMCustomer[14] = "In Active";

                        }
                        if (customerVo.IsProspect == 1)
                        {
                            drRMCustomer[15] = "Yes";
                        }
                        else
                        {
                            drRMCustomer[15] = "No";
                        }
                        if (customerVo.IsFPClient == 1)
                        {
                            drRMCustomer[16] = "Yes";
                        }
                        else
                        {
                            drRMCustomer[16] = "No";
                        }

                        //customerRMVo.FirstName.ToString() + " " + customerRMVo.MiddleName.ToString() + " " + customerRMVo.LastName.ToString();
                        dtRMCustomer.Rows.Add(drRMCustomer);
                    }
                    gvCustomers.DataSource = dtRMCustomer;
                    gvCustomers.DataBind();

                    ReAssignRMControl(genDictReassignRM);

                    if (genDictParent.Count > 0)
                    {
                        DropDownList ddlParent = GetParentDDL();
                        if (ddlParent != null)
                        {
                            ddlParent.DataSource = genDictParent;
                            ddlParent.DataTextField = "Key";
                            ddlParent.DataValueField = "Value";
                            ddlParent.DataBind();
                            ddlParent.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }
                        if (hdnParentFilter.Value != "")
                        {
                            ddlParent.SelectedValue = hdnParentFilter.Value.ToString();
                        }
                    }

                    if (genDictRM.Count > 0)
                    {
                        DropDownList ddlRM = GetRMDDL();
                        if (ddlRM != null)
                        {
                            ddlRM.DataSource = genDictRM;
                            ddlRM.DataTextField = "Value";
                            ddlRM.DataValueField = "Key";
                            ddlRM.DataBind();
                            ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }
                        if (hdnRMFilter.Value != "")
                        {
                            ddlRM.SelectedValue = hdnRMFilter.Value.ToString();
                        }
                    }

                    TextBox txtName = GetCustNameTextBox();
                    if (txtName != null)
                    {
                        if (hdnNameFilter.Value != "")
                        {
                            txtName.Text = hdnNameFilter.Value.ToString();
                        }
                    }

                    TextBox txtPincode = GetPincodeTextBox();
                    if (txtPincode != null)
                    {
                        if (hdnPincodeFilter.Value != "")
                        {
                            txtPincode.Text = hdnPincodeFilter.Value.ToString();
                        }
                    }

                    TextBox txtArea = GetAreaTextBox();
                    if (txtArea != null)
                    {
                        if (hdnNameFilter.Value != "")
                        {
                            txtArea.Text = hdnAreaFilter.Value.ToString();
                        }
                    }

                    this.GetPageCount();
                }
                else
                {
                    hdnRecordCount.Value = "0";
                    trMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    trPager.Visible = false;
                    lblTotalRows.Visible = false;
                    tblGV.Visible = false;
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
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:BindGrid()");
                object[] objects = new object[4];
                objects[0] = user;
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
                FunctionInfo.Add("Method", "AdviserCustomer.ascx:gvCustomers_SelectedIndexChanged()");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        {
            DropDownList ddlAction = null;
            GridViewRow gvr = null;
            int selectedRow = 0;
            int userId = 0;
            UserVo tempUser = null;
            UserBo userBo = new UserBo();
            bool isGrpHead = false;

            if (Session[SessionContents.PortfolioId] != null)
            {
                Session.Remove(SessionContents.PortfolioId);
            }

            try
            {
                ddlAction = (DropDownList)sender;
                gvr = (GridViewRow)ddlAction.NamingContainer;
                selectedRow = gvr.RowIndex;
                customerId = int.Parse(gvCustomers.DataKeys[selectedRow].Values["CustomerId"].ToString());
                userId = int.Parse(gvCustomers.DataKeys[selectedRow].Values["UserId"].ToString());
                customerVo = customerBo.GetCustomer(customerId);
                Session["CustomerVo"] = customerVo;

                if (ddlAction.SelectedItem.Value.ToString() == "Dashboard")
                {
                    Session["IsDashboard"] = "true";
                    isGrpHead = customerBo.CheckCustomerGroupHead(customerId);
                    if (isGrpHead == true)
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdvisorRMCustGroupDashboard','none');", true);
                    else
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdvisorRMCustIndiDashboard','none');", true);
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "Profile")
                {
                    Session["IsDashboard"] = "false";
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerId);
                    Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                    Session["customerPortfolioVo"] = customerPortfolioVo;

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomerIndividualDashboard','none');", true);

                }
                else if (ddlAction.SelectedItem.Value.ToString() == "Portfolio")
                {

                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerId);
                    Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioDashboard','none');", true);
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "Alerts")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMAlertNotifications','none');", true);
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "User Details")
                {
                    //tempUser = new UserVo();
                    //tempUser = userBo.GetUserDetails(userId);
                    //Session["CustomerUser"] = tempUser;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('GenerateLoginPassword','?GenLoginPassword_UserId=" + userId + "');", true);

                }
                else if (ddlAction.SelectedItem.Value.ToString() == "FinancialPlanning")
                {
                    if (customerId != 0)
                    {
                        Session[SessionContents.FPS_ProspectList_CustomerId] = customerId;
                    }
                    Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "View";
                    if (customerVo.Type == "Individual")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerProspect','login');", true);
                    }
                    if (customerVo.Type == "Non Individual")
                    {
                        //I'm not passing login parameter in this function.... that is becuase in JScript.js page the code corresponding to load RMCustomerIndividualLeftPane or RMCustomerNonIndividualLeftPane
                        //have been written in that way. so Please try to understand before modifying the code
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerProspect');", true);
                    }


                }

            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserCustomer.ascx:ddlAction_OnSelectedIndexChange()");


                object[] objects = new object[5];
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

        private void BindCustomer(int CurrentPage)
        {
            Dictionary<string, string> genDictParent = new Dictionary<string, string>();
            Dictionary<string, string> genDictRM = new Dictionary<string, string>();
            Dictionary<string, string> genDictReassignRM = new Dictionary<string, string>();

            string customer = "";
            AdvisorBo adviserBo = new AdvisorBo();
            List<CustomerVo> customerList = new List<CustomerVo>();
            RMVo customerRMVo = new RMVo();
            DataTable dtRMCustomer = new DataTable();
            try
            {

                DropDownList ddl = new DropDownList();
                Label lbl = new Label();
                if (gvCustomers.HeaderRow != null)
                {
                    if ((DropDownList)gvCustomers.HeaderRow.FindControl("ddlReassignRM") != null)
                    {
                        ddl = (DropDownList)gvCustomers.HeaderRow.FindControl("ddlReassignRM");
                        lbl = (Label)(gvCustomers.HeaderRow.FindControl("lblAssignedRMHeader"));
                        ddl.Visible = false;
                        lbl.Visible = true;

                    }
                }


                customer = Session["Customer"].ToString();
                if (customer.ToLower().Trim() == "find customer" || customer.ToLower().Trim() == "")
                    customer = "";

                rmVo = (RMVo)Session["rmVo"];

                adviserVo = (AdvisorVo)Session["advisorVo"];

                if (hdnCurrentPage.Value.ToString() != "")
                {
                    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                    hdnCurrentPage.Value = "";
                }

                int Count = 0;

                // Search Term is input into this hidden field
                hdnNameFilter.Value = customer;

                customerList = adviserBo.GetAdviserCustomerList(adviserVo.advisorId, mypager.CurrentPage, out Count, hdnSort.Value, hdnNameFilter.Value, hdnAreaFilter.Value, hdnPincodeFilter.Value, hdnParentFilter.Value, hdnRMFilter.Value, hdnactive.Value, out genDictParent, out genDictRM, out genDictReassignRM);

                lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();

                if (customerList == null)
                {
                    hdnRecordCount.Value = "0";
                    trMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    trPager.Visible = false;
                    lblTotalRows.Visible = false;
                    tblGV.Visible = false;
                }
                else
                {
                    trMessage.Visible = false;
                    ErrorMessage.Visible = false;
                    trPager.Visible = true;
                    lblTotalRows.Visible = true;

                    dtRMCustomer.Columns.Add("CustomerId");
                    dtRMCustomer.Columns.Add("UserId");
                    dtRMCustomer.Columns.Add("RMId");
                    dtRMCustomer.Columns.Add("Parent");
                    dtRMCustomer.Columns.Add("Cust_Comp_Name");
                    dtRMCustomer.Columns.Add("PAN Number");
                    dtRMCustomer.Columns.Add("Mobile Number");
                    dtRMCustomer.Columns.Add("Phone Number");
                    dtRMCustomer.Columns.Add("Email");
                    dtRMCustomer.Columns.Add("Address");
                    dtRMCustomer.Columns.Add("Area");
                    dtRMCustomer.Columns.Add("City");
                    dtRMCustomer.Columns.Add("Pincode");
                    dtRMCustomer.Columns.Add("IsActive");
                    dtRMCustomer.Columns.Add("IsProspect");
                    dtRMCustomer.Columns.Add("IsFPClient");
                    dtRMCustomer.Columns.Add("Assigned RM");

                    DataRow drRMCustomer;

                    for (int i = 0; i < customerList.Count; i++)
                    {
                        drRMCustomer = dtRMCustomer.NewRow();
                        customerVo = new CustomerVo();
                        customerVo = customerList[i];
                        drRMCustomer[0] = customerVo.CustomerId.ToString();
                        drRMCustomer[1] = customerVo.UserId.ToString();
                        drRMCustomer[2] = customerVo.RmId.ToString();

                        if (customerVo.ParentCustomer != null)
                        {
                            drRMCustomer[3] = customerVo.ParentCustomer.ToString();
                        }

                        drRMCustomer[4] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();

                        //if (customerVo.Type.ToUpper().ToString() == "IND" || customerVo.Type == "")
                        //{
                        //    if (customerVo.ParentCustomer != null)
                        //    {
                        //        drRMCustomer[3] = customerVo.ParentCustomer.ToString();
                        //    }
                        //    else
                        //    {
                        //        drRMCustomer[3] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        //    }
                        //    drRMCustomer[4] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        //}
                        //else if (customerVo.Type.ToUpper().ToString() == "NIND")
                        //{
                        //    if (customerVo.ParentCompany != null)
                        //    {
                        //        drRMCustomer[3] = customerVo.ParentCompany.ToString();
                        //    }
                        //    else
                        //    {
                        //        drRMCustomer[3] = customerVo.CompanyName.ToString();
                        //    }
                        //    drRMCustomer[4] = customerVo.CompanyName.ToString();
                        //}
                        if (customerVo.PANNum != null)
                            drRMCustomer[5] = customerVo.PANNum.ToString();
                        else
                            drRMCustomer[5] = "";
                        drRMCustomer[6] = customerVo.Mobile1.ToString();
                        drRMCustomer[7] = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                        drRMCustomer[8] = customerVo.Email.ToString();
                        if (customerVo.Adr1Line1 == null)
                            customerVo.Adr1Line1 = "";
                        if (customerVo.Adr1Line2 == null)
                            customerVo.Adr1Line2 = "";
                        if (customerVo.Adr1Line3 == null)
                            customerVo.Adr1Line3 = "";
                        if (customerVo.Adr1City == null)
                            customerVo.Adr1City = "";
                        if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() == "")
                            drRMCustomer[9] = "-";
                        else if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() != "")
                            drRMCustomer[9] = customerVo.Adr1Line2.ToString();
                        else if (customerVo.Adr1Line1.ToString() != "" && customerVo.Adr1Line2.ToString() == "")
                            drRMCustomer[9] = customerVo.Adr1Line1.ToString();
                        else
                            drRMCustomer[9] = customerVo.Adr1Line1.ToString() + "," + customerVo.Adr1Line2.ToString();

                        drRMCustomer[10] = customerVo.Adr1Line3.ToString();
                        drRMCustomer[11] = customerVo.Adr1City.ToString();
                        drRMCustomer[12] = customerVo.Adr1PinCode.ToString();
                        drRMCustomer[13] = customerVo.IsActive.ToString();
                        if (customerVo.IsProspect == 1)
                        {
                            drRMCustomer[14] = "Yes";
                        }
                        else
                        {
                            drRMCustomer[14] = "No";
                        }
                        if (customerVo.IsFPClient == 1)
                        {
                            drRMCustomer[15] = "Yes";
                        }
                        else
                        {
                            drRMCustomer[15] = "No";
                        }
                        drRMCustomer[16] = customerVo.AssignedRM.ToString();

                        dtRMCustomer.Rows.Add(drRMCustomer);
                    }

                    gvCustomers.DataSource = dtRMCustomer;
                    gvCustomers.DataBind();

                    ReAssignRMControl(genDictRM);

                    if (genDictParent.Count > 0)
                    {
                        DropDownList ddlParent = GetParentDDL();
                        if (ddlParent != null)
                        {
                            ddlParent.DataSource = genDictParent;
                            ddlParent.DataTextField = "Key";
                            ddlParent.DataValueField = "Value";
                            ddlParent.DataBind();
                            ddlParent.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }
                        if (hdnParentFilter.Value != "")
                        {
                            ddlParent.SelectedValue = hdnParentFilter.Value.ToString();
                        }
                    }

                    if (genDictRM.Count > 0)
                    {
                        DropDownList ddlRM = GetRMDDL();
                        if (ddlRM != null)
                        {
                            ddlRM.DataSource = genDictRM;
                            ddlRM.DataTextField = "Value";
                            ddlRM.DataValueField = "Key";
                            ddlRM.DataBind();
                            ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }
                        if (hdnRMFilter.Value != "")
                        {
                            ddlRM.SelectedValue = hdnRMFilter.Value.ToString();
                        }
                    }

                    TextBox txtName = GetCustNameTextBox();
                    if (txtName != null)
                    {
                        if (hdnNameFilter.Value != "")
                        {
                            txtName.Text = hdnNameFilter.Value.ToString();
                        }
                    }

                    TextBox txtPincode = GetPincodeTextBox();
                    if (txtPincode != null)
                    {
                        if (hdnPincodeFilter.Value != "")
                        {
                            txtPincode.Text = hdnPincodeFilter.Value.ToString();
                        }
                    }

                    TextBox txtArea = GetAreaTextBox();
                    if (txtArea != null)
                    {
                        if (hdnNameFilter.Value != "")
                        {
                            txtArea.Text = hdnAreaFilter.Value.ToString();
                        }
                    }

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
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:BindCustomer()");
                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = customerVo;
                objects[2] = customerList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void ReAssignRMControl(Dictionary<string, string> genDictReassignRM)
        {
            // genDictRM = new Dictionary<string, string>();
            if (gvCustomers.HeaderRow != null)
            {
                if (hdnReassignRM.Value != "")
                {
                    ((CheckBox)gvCustomers.HeaderRow.FindControl("chkReassignRM")).Checked = true;
                }
                else
                {
                    ((CheckBox)gvCustomers.HeaderRow.FindControl("chkReassignRM")).Checked = false;
                }
                if (((CheckBox)gvCustomers.HeaderRow.FindControl("chkReassignRM")).Checked)
                {
                    DropDownList ddl1 = new DropDownList();
                    Label lbl1 = new Label();
                    foreach (GridViewRow gvr in gvCustomers.Rows)
                    {
                        if ((DropDownList)gvr.FindControl("ddlReassignRM") != null)
                        {
                            ddl1 = (DropDownList)gvr.FindControl("ddlReassignRM");
                            ddl1.Visible = true;
                            ddl1.DataSource = genDictReassignRM;
                            ddl1.DataTextField = "Value";
                            ddl1.DataValueField = "Key";
                            ddl1.DataBind();
                            lbl1 = (Label)(gvr.FindControl("lblAssignedRMHeader"));
                            lbl1.Visible = false;
                            ddl1.SelectedValue = gvCustomers.DataKeys[gvr.RowIndex].Values["RMId"].ToString();
                        }
                    }
                }
                else
                {
                    DropDownList ddl1 = new DropDownList();
                    Label lbl1 = new Label();
                    foreach (GridViewRow gvr in gvCustomers.Rows)
                    {
                        if ((DropDownList)gvr.FindControl("ddlReassignRM") != null)
                        {
                            ddl1 = (DropDownList)gvr.FindControl("ddlReassignRM");
                            ddl1.Visible = false;
                            lbl1 = (Label)(gvr.FindControl("lblAssignedRMHeader"));
                            lbl1.Visible = true;
                        }
                    }
                }

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
                    this.BindGrid(1, 0);
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    hdnSort.Value = sortExpression + " ASC";
                    this.BindGrid(1, 0);
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

                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:gvCustomers_Sort()");

                object[] objects = new object[1];
                objects[0] = sortExpression;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedValue.ToString()));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }

        //private void ExportGridView(string ExportFormat)
        //{


        //    gvCustomers.Columns[0].Visible = false;
        //    gvCustomers.HeaderRow.Visible = true;
        //    if (rbtnMultiple.Checked)
        //    {

        //        BindGrid(mypager.CurrentPage, 1);

        //    }
        //    else
        //    {
        //        BindGrid(mypager.CurrentPage, 0);
        //    }

        //    PrepareGridViewForExport(gvCustomers);

        //    if (ExportFormat == "Excel")
        //    {
        //        ExportGridView("Excel");
        //    }
        //    if (ExportFormat == "Word")
        //    {
        //        ExportGridView("Word");
        //    }
        //    if (ExportFormat == "PDF")
        //    {
        //        ExportGridView("PDF");

        //    }
        //    if (ExportFormat == "Print")
        //    {
        //        GridView_Print();
        //    }

        //    BindGrid(mypager.CurrentPage, 0);
        //    gvCustomers.Columns[0].Visible = true;

        //}

        private void PrepareGridViewForExport(Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            string name = String.Empty;

            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (gv.Controls[i] as DropDownList).SelectedValue.ToString();
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(TextBox))
                {
                    l.Text = (gv.Controls[i] as TextBox).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                    gv.Controls.Remove(gv.Controls[i]);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }

            }


        }

        private void ExportGridView(string Filetype)
        {
            {
                HtmlForm frm = new HtmlForm();
                HtmlImage image = new HtmlImage();

                frm.Controls.Clear();
                frm.Attributes["runat"] = "server";
                if (Filetype.ToLower() == "print")
                {
                    GridView_Print();
                }
                else if (Filetype.ToLower() == "excel")
                {
                    // gvCustomer.Columns.Remove(this.gvCustomer.Columns[0]);
                    string temp = userVo.FirstName + userVo.LastName + "Customer.xls";
                    string attachment = "attachment; filename=" + temp;
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    Response.Output.Write("<table border=\"0\"><tbody><tr><td>");
                    Response.Output.Write("Advisor Name : ");
                    Response.Output.Write("</td>");
                    Response.Output.Write("<td>");
                    Response.Output.Write(userVo.FirstName + userVo.LastName);
                    Response.Output.Write("</td></tr>");
                    Response.Output.Write("<tr><td>");
                    Response.Output.Write("Report  : ");
                    Response.Output.Write("</td>");
                    Response.Output.Write("<td>");
                    Response.Output.Write("Customer List");
                    Response.Output.Write("</td></tr><tr><td>");
                    Response.Output.Write("Date : ");
                    Response.Output.Write("</td><td>");
                    System.DateTime tDate1 = System.DateTime.Now;
                    Response.Output.Write(tDate1);
                    Response.Output.Write("</td></tr>");
                    Response.Output.Write("</tbody></table>");

                    PrepareGridViewForExport(gvCustomers);

                    if (gvCustomers.HeaderRow != null)
                    {
                        PrepareControlForExport(gvCustomers.HeaderRow);
                        //tbl.Rows.Add(gvMFTransactions.HeaderRow);
                    }
                    foreach (GridViewRow row in gvCustomers.Rows)
                    {

                        PrepareControlForExport(row);

                        //tbl.Rows.Add(row);
                    }
                    if (gvCustomers.FooterRow != null)
                    {
                        PrepareControlForExport(gvCustomers.FooterRow);
                        // tbl.Rows.Add(gvMFTransactions.FooterRow);
                    }

                    gvCustomers.Parent.Controls.Add(frm);
                    frm.Controls.Add(gvCustomers);
                    frm.RenderControl(htw);
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }


                else if (Filetype.ToLower() == "pdf")
                {
                    string temp = userVo.FirstName + userVo.LastName + "_Customer List";

                    gvCustomers.AllowPaging = false;
                    gvCustomers.DataBind();
                    iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gvCustomers.Columns.Count - 1);

                    table.HeaderRows = 4;
                    iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);
                    Phrase phApplicationName = new Phrase("WWW.PrincipalConsulting.net", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
                    PdfPCell clApplicationName = new PdfPCell(phApplicationName);
                    clApplicationName.Border = PdfPCell.NO_BORDER;
                    clApplicationName.HorizontalAlignment = Element.ALIGN_LEFT;


                    Phrase phDate = new Phrase(DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
                    PdfPCell clDate = new PdfPCell(phDate);
                    clDate.HorizontalAlignment = Element.ALIGN_RIGHT;
                    clDate.Border = PdfPCell.NO_BORDER;


                    headerTable.AddCell(clApplicationName);
                    headerTable.AddCell(clDate);
                    headerTable.DefaultCell.Border = PdfPCell.NO_BORDER;

                    PdfPCell cellHeader = new PdfPCell(headerTable);
                    cellHeader.Border = PdfPCell.NO_BORDER;
                    cellHeader.Colspan = gvCustomers.Columns.Count - 1;
                    table.AddCell(cellHeader);

                    Phrase phHeader = new Phrase(temp, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                    PdfPCell clHeader = new PdfPCell(phHeader);
                    clHeader.Colspan = gvCustomers.Columns.Count - 1;
                    clHeader.Border = PdfPCell.NO_BORDER;
                    clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(clHeader);


                    Phrase phSpace = new Phrase("\n");
                    PdfPCell clSpace = new PdfPCell(phSpace);
                    clSpace.Border = PdfPCell.NO_BORDER;
                    clSpace.Colspan = gvCustomers.Columns.Count - 1;
                    table.AddCell(clSpace);

                    GridViewRow HeaderRow = gvCustomers.HeaderRow;
                    if (HeaderRow != null)
                    {
                        string cellText = "";
                        for (int j = 1; j < gvCustomers.Columns.Count; j++)
                        {
                            if (j == 1)
                            {
                                cellText = "Parent";
                            }
                            else if (j == 2)
                            {
                                cellText = "Customer Name / Company Name";
                            }
                            else if (j == 7)
                            {
                                cellText = "Area";
                            }
                            else if (j == 9)
                            {
                                cellText = "Pincode";
                            }
                            else if (j == 10)
                            {
                                cellText = "Assigned RM";
                            }
                            else
                            {
                                cellText = Server.HtmlDecode(gvCustomers.HeaderRow.Cells[j].Text);
                            }

                            Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD));
                            table.AddCell(ph);
                        }

                    }

                    for (int i = 0; i < gvCustomers.Rows.Count; i++)
                    {
                        string cellText = "";
                        if (gvCustomers.Rows[i].RowType == DataControlRowType.DataRow)
                        {
                            for (int j = 1; j < gvCustomers.Columns.Count; j++)
                            {
                                if (j == 1)
                                {
                                    cellText = ((Label)gvCustomers.Rows[i].FindControl("lblParenteHeader")).Text;
                                }
                                else if (j == 2)
                                {
                                    cellText = ((Label)gvCustomers.Rows[i].FindControl("lblCustNameHeader")).Text;
                                }
                                else if (j == 7)
                                {
                                    cellText = ((Label)gvCustomers.Rows[i].FindControl("lblAreaHeader")).Text;
                                }
                                else if (j == 9)
                                {
                                    cellText = ((Label)gvCustomers.Rows[i].FindControl("lblPincodeHeader")).Text;
                                }
                                else if (j == 10)
                                {
                                    cellText = ((Label)gvCustomers.Rows[i].FindControl("lblAssignedRMHeader")).Text;
                                }
                                else
                                {
                                    cellText = Server.HtmlDecode(gvCustomers.Rows[i].Cells[j].Text);
                                }
                                Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL));
                                iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
                                table.AddCell(ph);

                            }

                        }

                    }



                    //Create the PDF Document

                    Document pdfDoc = new Document(PageSize.A5, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    pdfDoc.Add(table);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    temp = "filename=" + temp + ".pdf";
                    //    Response.AddHeader("content-disposition", "attachment;" + "filename=GridViewExport.pdf");
                    Response.AddHeader("content-disposition", "attachment;" + temp);
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();



                }
                else if (Filetype.ToLower() == "word")
                {
                    gvCustomers.Columns.Remove(this.gvCustomers.Columns[0]);
                    string temp = userVo.FirstName + userVo.LastName + "_Customer.doc";
                    string attachment = "attachment; filename=" + temp;
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/msword";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);

                    Response.Output.Write("<table border=\"0\"><tbody><tr><td>");
                    Response.Output.Write("Advisor Name : ");
                    Response.Output.Write("</td>");
                    Response.Output.Write("<td>");
                    Response.Output.Write(userVo.FirstName + userVo.LastName);
                    Response.Output.Write("</td></tr>");
                    Response.Output.Write("<tr><td>");
                    Response.Output.Write("Report  : ");
                    Response.Output.Write("</td>");
                    Response.Output.Write("<td>");
                    Response.Output.Write("Customer List");
                    Response.Output.Write("</td></tr><tr><td>");
                    Response.Output.Write("Date : ");
                    Response.Output.Write("</td><td>");
                    System.DateTime tDate1 = System.DateTime.Now;
                    Response.Output.Write(tDate1);
                    Response.Output.Write("</td></tr>");
                    Response.Output.Write("</tbody></table>");

                    PrepareGridViewForExport(gvCustomers);


                    if (gvCustomers.HeaderRow != null)
                    {
                        PrepareControlForExport(gvCustomers.HeaderRow);
                    }
                    foreach (GridViewRow row in gvCustomers.Rows)
                    {
                        PrepareControlForExport(row);
                    }
                    if (gvCustomers.FooterRow != null)
                    {
                        PrepareControlForExport(gvCustomers.FooterRow);
                    }
                    gvCustomers.Parent.Controls.Add(frm);
                    frm.Controls.Add(gvCustomers);
                    frm.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();

                }

            }

        }

        private void ShowPdf(string strS)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader
            ("Content-Disposition", "attachment; filename=" + strS);
            Response.TransmitFile(strS);
            Response.End();
            Response.Flush();
            Response.Clear();

        }

        private TextBox GetCustNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvCustomers.HeaderRow != null)
            {
                if ((TextBox)gvCustomers.HeaderRow.FindControl("txtCustNameSearch") != null)
                {
                    txt = (TextBox)gvCustomers.HeaderRow.FindControl("txtCustNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetAreaTextBox()
        {
            TextBox txt = new TextBox();
            if (gvCustomers.HeaderRow != null)
            {
                if ((TextBox)gvCustomers.HeaderRow.FindControl("txtAreaSearch") != null)
                {
                    txt = (TextBox)gvCustomers.HeaderRow.FindControl("txtAreaSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetPincodeTextBox()
        {
            TextBox txt = new TextBox();
            if (gvCustomers.HeaderRow != null)
            {
                if ((TextBox)gvCustomers.HeaderRow.FindControl("txtPincodeSearch") != null)
                {
                    txt = (TextBox)gvCustomers.HeaderRow.FindControl("txtPincodeSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private DropDownList GetParentDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvCustomers.HeaderRow != null)
            {
                if ((DropDownList)gvCustomers.HeaderRow.FindControl("ddlParent") != null)
                {
                    ddl = (DropDownList)gvCustomers.HeaderRow.FindControl("ddlParent");
                }
            }
            else
                ddl = null;

            return ddl;
        }

        private DropDownList GetRMDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvCustomers.HeaderRow != null)
            {
                if ((DropDownList)gvCustomers.HeaderRow.FindControl("ddlAssignedRM") != null)
                {
                    ddl = (DropDownList)gvCustomers.HeaderRow.FindControl("ddlAssignedRM");
                }
            }
            else
                ddl = null;

            return ddl;
        }

        protected void btnPincodeSearch_Click(object sender, EventArgs e)
        {
            TextBox txtPincode = GetPincodeTextBox();

            if (txtPincode != null)
            {
                hdnPincodeFilter.Value = txtPincode.Text.Trim();
                if (Session["Customer"].ToString() == "Customer")
                {
                    this.BindGrid(mypager.CurrentPage, 0);
                }
                else
                {
                    this.BindCustomer(mypager.CurrentPage);
                }
            }
        }

        protected void btnAreaSearch_Click(object sender, EventArgs e)
        {
            TextBox txtArea = GetAreaTextBox();

            if (txtArea != null)
            {
                hdnAreaFilter.Value = txtArea.Text.Trim();
                if (Session["Customer"].ToString() == "Customer")
                {
                    this.BindGrid(mypager.CurrentPage, 0);
                }
                else
                {
                    this.BindCustomer(mypager.CurrentPage);
                }
            }
        }

        protected void btnNameSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetCustNameTextBox();

            if (txtName != null)
            {
                hdnNameFilter.Value = txtName.Text.Trim();
                this.BindGrid(mypager.CurrentPage, 0);
            }
        }

        protected void ddlAssignedRM_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlRM = GetRMDDL();

            if (ddlRM != null)
            {
                if (ddlRM.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    //hdnRMFilter.Value = ddlRM.SelectedValue;
                    hdnRMFilter.Value = ddlRM.SelectedItem.Text;
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnRMFilter.Value = "";
                }

                if (Session["Customer"].ToString() == "Customer")
                {
                    this.BindGrid(mypager.CurrentPage, 0);
                }
                else
                {
                    this.BindCustomer(mypager.CurrentPage);
                }
            }
        }

        protected void ddlActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlFilter = (DropDownList)gvCustomers.HeaderRow.FindControl("ddlActiveFilter");
            if (int.Parse(ddlFilter.SelectedValue) == 1)
            {
                hdnactive.Value = "A";
            }
            if (int.Parse(ddlFilter.SelectedValue) == 0)
            {
                hdnactive.Value = "I";
            }
            if (int.Parse(ddlFilter.SelectedValue) == 2)
            {
                hdnactive.Value = "D";
            }
            this.BindGrid(mypager.CurrentPage, 0);
        }
        protected void ddlParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlParent = GetParentDDL();

            if (ddlParent != null)
            {
                if (ddlParent.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnParentFilter.Value = ddlParent.SelectedValue;
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnParentFilter.Value = "";
                }

                if (Session["Customer"].ToString() == "Customer")
                {
                    this.BindGrid(mypager.CurrentPage, 0);
                }
                else
                {
                    this.BindCustomer(mypager.CurrentPage);
                }
            }
        }

        protected void chkReassignRM_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)gvCustomers.HeaderRow.FindControl("chkReassignRM")).Checked)
            {
                hdnReassignRM.Value = "1";
            }
            else
            {
                hdnReassignRM.Value = "";
            }
            ReAssignRMControl(genDictReassignRM);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int[] customerIds;
            int[] rmIds;
            DropDownList ddl1 = new DropDownList();
            string tempCustomerId = "";

            if (((CheckBox)gvCustomers.HeaderRow.FindControl("chkReassignRM")).Checked)
            {
                customerIds = new int[gvCustomers.Rows.Count];
                rmIds = new int[gvCustomers.Rows.Count];

                //  foreach (DataKey key in gvCustomers.DataKeys)
                foreach (GridViewRow gvr in gvCustomers.Rows)
                {
                    tempCustomerId = gvCustomers.DataKeys[gvr.RowIndex].Values[0].ToString();

                    customerIds[gvr.RowIndex] = int.Parse(tempCustomerId);

                    if ((DropDownList)gvr.FindControl("ddlReassignRM") != null)
                    {
                        ddl1 = (DropDownList)gvr.FindControl("ddlReassignRM");
                        rmIds[gvr.RowIndex] = int.Parse(ddl1.SelectedValue.ToString());

                    }

                }
                if (customerIds.Count() == rmIds.Count())
                {
                    customerBo.UpdateCustomerAssignedRM(customerIds, rmIds);
                }
            }


        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            gvCustomers.Columns[0].Visible = false;
            gvCustomers.HeaderRow.Visible = true;

            if (hdnDownloadPageType.Value.ToString() == "single")
            {
                BindGrid(mypager.CurrentPage, 0);
            }
            else
            {
                BindGrid(mypager.CurrentPage, 1);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "AferExportAll('ctrl_AdviserCustomer_btnPrintGrid');", true);
            }

            ExportGridView(hdnDownloadFormat.Value.ToString());
            //
            BindGrid(mypager.CurrentPage, 0);
            gvCustomers.Columns[0].Visible = true;
        }

        //protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
        //{
        //    trPageChoice.Visible = true;
        //    ExportFormat = "Excel";
        //}



        protected void btnPrintGrid_Click(object sender, EventArgs e)
        {
            BindGrid(mypager.CurrentPage, 0);
            gvCustomers.Columns[0].Visible = true;


            // GridView_Print();
        }
        //protected void rbtnSingle_CheckedChanged(object sender, EventArgs e)
        //{
        //    gvCustomers.Columns[0].Visible = false;
        //    gvCustomers.HeaderRow.Visible = true;
        //    rbtnSingle.Checked = false;
        //    BindGrid(mypager.CurrentPage, 0);
        //    PrepareGridViewForExport(gvCustomers);
        //    ExportGridView(hdnDownloadType.Value.ToString());
        //    BindGrid(mypager.CurrentPage, 0);
        //    gvCustomers.Columns[0].Visible = true;
        //}

        //protected void rbtnMultiple_CheckedChanged(object sender, EventArgs e)
        //{
        //    gvCustomers.Columns[0].Visible = false;
        //    gvCustomers.HeaderRow.Visible = true;
        //    BindGrid(mypager.CurrentPage, 1);
        //    PrepareGridViewForExport(gvCustomers);
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "AferExportAll('ctrl_AdviserCustomer_btnPrintGrid');", true);
        //    ExportGridView(hdnDownloadType.Value.ToString());
        //    BindGrid(mypager.CurrentPage, 0);
        //    gvCustomers.Columns[0].Visible = true;
        //}

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            GridView_Print();

        }

        private void GridView_Print()
        {
            gvCustomers.Columns[0].Visible = false;
            if (hdnDownloadPageType.Value.ToString() == "single")
            {
                BindGrid(mypager.CurrentPage, 0);
            }
            else
            {
                BindGrid(mypager.CurrentPage, 1);
            }

            if (gvCustomers.HeaderRow != null)
            {
                PrepareGridViewForExport(gvCustomers.HeaderRow);
            }
            foreach (GridViewRow row in gvCustomers.Rows)
            {
                PrepareGridViewForExport(row);
            }
            if (gvCustomers.FooterRow != null)
            {
                PrepareGridViewForExport(gvCustomers.FooterRow);
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_AdviserCustomer_tbl','ctrl_AdviserCustomer_btnPrintGrid');", true);

        }

        protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtender1.TargetControlID = "imgBtnExport";
            ModalPopupExtender1.Show();
        }

        protected void imgBtnWord_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtender1.TargetControlID = "imgBtnWord";
            ModalPopupExtender1.Show();

        }

        protected void imgBtnPdf_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtender1.TargetControlID = "imgBtnPdf";
            ModalPopupExtender1.Show();
        }

        protected void imgBtnPrint_Click(object sender, ImageClickEventArgs e)
        {
            if (hdnDownloadPageType.Value.ToString() == "multiple")
            {
                BindGrid(mypager.CurrentPage, 1);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "AferExportAll('ctrl_RMCustomer_btnPrintGrid');", true);
            }
            ModalPopupExtender1.TargetControlID = "imgBtnPrint";
            ModalPopupExtender1.Show();

        }


    }
}
