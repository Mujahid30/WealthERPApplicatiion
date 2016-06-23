using System;
using System.Data;
using BoCustomerProfiling;
using BoCommon;
using VoUser;
using BoAdvisorProfiling;
using System.Configuration;
using WealthERP.Base;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI;
using VoReports;
using BoReports;
using CrystalDecisions.CrystalReports.Engine;
using DanLudwig;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using BoSuperAdmin;

namespace WealthERP.Reports
{
    public partial class MFReports : System.Web.UI.UserControl
    {
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
        DataTable dtAdviserList = new DataTable();
        RMVo rmVo = new RMVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        DataTable dtRelationship = new DataTable();
        UserVo userVo = new UserVo();
        string path = string.Empty;
        DateTime convertedFromDate;
        DateTime convertedToDate;
        DateBo dtBo = new DateBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        DateTime dtTo = new DateTime();
        DateTime dtFrom = new DateTime();
        int activeTabIndex = 0;
        AdvisorVo advisorVo = null;
        MFReportVo mfReport = new MFReportVo();
        string reportSubType = string.Empty;
        int reportFlag = 0;
        DateTime chckdate = new DateTime();
        DateTime LatestValuationdate = new DateTime();
        AdvisorMISBo adviserMISBo = new AdvisorMISBo();
        int advisorId;
        CustomerVo customerVo = new CustomerVo();
        bool isGrpHead = false;
        bool CustomerLogin = false;
        bool strFromCustomerDashBoard = false;
        WERPTaskRequestManagementBo taskRequestManagementBo = new WERPTaskRequestManagementBo();
        Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();

        DataSet dsRequestListStatus = null;
        DataTable dtRequestStatusList = new DataTable();

        public enum Constants
        {
            MF = 1,
            MFDate = 3
        };

        protected void Page_Init(object sender, EventArgs e)
        {
          
            if (Request.Browser.Browser.Equals("Firefox")
                && Request.Browser.MajorVersion < 2
                && Request.Browser.MinorVersion < 0.5)
            {
                ScriptManager.GetCurrent(Page).EnablePartialRendering = false;
            }
        }




        protected void gvRequestStatus_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGvSchemeDetails = new DataTable();
            if (advisorVo != null)
            {
                if (Cache["gvRequestStatus" + advisorVo.advisorId] != null)
                {
                    dtGvSchemeDetails = (DataTable)Cache["gvRequestStatus" + advisorVo.advisorId];
                    gvRequestStatus.DataSource = dtGvSchemeDetails;
                    RadTabStrip2.Tabs[2].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 2;
                }
            }


           
        }

        

        protected void ListBoxSource_Transferred(object source , Telerik.Web.UI.RadListBoxTransferredEventArgs e)
        {
            LBCustomer.Items.Sort();
        }
       
        private void moveSelectedItems(DanLudwig.Controls.Web.ListBox source, DanLudwig.Controls.Web.ListBox target, bool moveAllItems)
        {
            for (int i = source.Items.Count - 1; i >= 0; i--)
            {
                ListItem item = source.Items[i];

                if (moveAllItems)
                    item.Selected = true;

                if (item.Selected)
                {
                    if (target.Items.Count > 0)
                    {
                        for (int ii = 0; ii < target.Items.Count; ii++)
                        {
                            if (target.Items[ii].Text.CompareTo(item.Text) > 0)
                            {
                                target.Items.Insert(ii, item);
                                item.Selected = false;
                                break;
                            }
                        }
                    }

                    if (item.Selected)
                    {
                        target.Items.Add(item);
                        item.Selected = false;
                    }

                    source.Items.Remove(item);
                }
            }

        }

        public void SelectLastItem(DanLudwig.Controls.Web.ListBox ListBox1)
        {
            for (int i = ListBox1.Items.Count - 1; i >= 0; i--)
            {
                ListItem item = ListBox1.Items[i];
                if (i == ListBox1.Items.Count - 1)
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }


            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BindAdviserDropDownList();
                SessionBo.CheckSession();
                userVo = (UserVo)Session["UserVo"];
                pnlGvRequestStatus.Visible = true;


                rdoGroup.Attributes.Add("onClick", "javascript:ChangeCustomerSelectionTextBox(value);");

                rdoIndividual.Attributes.Add("onClick", "javascript:ChangeCustomerSelectionTextBox(value);");

                rdoCustomerGroup.Attributes.Add("onClick", "javascript:ChangeGroupOrSelf(value);");

                rdoCustomerIndivisual.Attributes.Add("onClick", "javascript:ChangeGroupOrSelf(value);");
                if (Session["advisorVo"]!=null)
                    advisorVo = (AdvisorVo)Session["advisorVo"];

                
               

                if (userVo.UserType!="SuperAdmin")
                {
                    trAdviserSelection.Visible = false;
                      if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$btnViewReport"] != "View Report" && Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlEmailReports$btnEmailReport"] != "Email Report")
                    {
                        path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                        if (Session[SessionContents.RmVo] != null)
                        {
                            if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
                                rmVo = (RMVo)Session[SessionContents.RmVo];
                        }

                        if (Session["UserType"] != null)
                        {
                            if (Session["UserType"].ToString() == "Customer")
                                strFromCustomerDashBoard = true;
                            imgBtnrgHoldings.Visible = false;
                        }

                        if (Session["UserType"] != null)
                        {
                            if (Session["UserType"].ToString().Trim() == "Customer" && strFromCustomerDashBoard == true)
                            {
                                if (!string.IsNullOrEmpty(Session["CustomerVo"].ToString()))
                                    customerVo = (CustomerVo)Session["CustomerVo"];
                                CustomerLogin = true;
                                hndCustomerLogin.Value = "true";
                                Session["hndCustomerLogin"] = hndCustomerLogin.Value;
                              
                                RadTabStrip2.Tabs[1].Visible = false;
                                RadTabStrip2.Tabs[2].Visible = false;


                            }
                            else
                            {
                                hndCustomerLogin.Value = "false";
                                Session["hndCustomerLogin"] = hndCustomerLogin.Value;

                            }
                        }

                        BindPeriodDropDown();

                      

                        #region new gr
                        if (CustomerLogin == true)
                        {
                            trCustomerName.Visible = true;
                            trIndCustomer.Visible = false;
                            trGroupCustomer.Visible = false;
                            IndivisulCustomerLogin();

                            trAdvisorRadioList.Visible = false;
                            trCustomerRadioList.Visible = true;

                            trAdminRM.Visible = false;
                            trCustomer.Visible = true;

                            isGrpHead = customerBo.CheckCustomerGroupHead(customerVo.CustomerId);
                            if (isGrpHead == false)
                            {
                                trCustomerRadioList.Visible = false;
                                rdoCustomerIndivisual.Checked = true;
                                divGroupCustomers.Visible = false;
                                hndSelfOrGroup.Value = "self";
                                ShowFolios();
                            }
                            else
                            {
                                rdoCustomerGroup.Checked = true;
                                hndSelfOrGroup.Value = "";
                            }

                        }
                        else
                        {
                            trCustomerName.Visible = false;
                            trIndCustomer.Visible = true;
                            trGroupCustomer.Visible = true;

                            trAdvisorRadioList.Visible = true;
                            trCustomerRadioList.Visible = false;

                            trAdminRM.Visible = true;
                            trCustomer.Visible = false;
                           
                        }


                        if (CustomerLogin == false)
                        {
                           
                            CustomerBo customerBo = new CustomerBo();
                            DataTable dtGroupCustomerList = new DataTable();
                            if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                            {
                                RadTabStrip2.Tabs[1].Visible = false;
                                RadTabStrip2.Tabs[2].Visible = false;
                                dtGroupCustomerList = customerBo.GetParentCustomerName("BULKMAIL", int.Parse(rmVo.RMId.ToString()));
                                imgBtnrgHoldings.Visible = false;
                            }
                            else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString() == "Ops")
                            {
                                
                                RadTabStrip2.Tabs[1].Visible = true;
                                dtGroupCustomerList = customerBo.GetAdviserGroupCustomerName("BULKMAIL", int.Parse(advisorVo.advisorId.ToString()));
                                imgBtnrgHoldings.Visible = true;
                            }
                            else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                            {
                              
                                RadTabStrip2.Tabs[1].Visible = false;
                                RadTabStrip2.Tabs[2].Visible = false;
                                imgBtnrgHoldings.Visible = false;
                            }

                            if (!Page.IsPostBack)
                            {
                                txtEmailAsOnDate.SelectedDate = DateTime.Now;
                                txtEmailAsOnDate.Calendar.SpecialDays.Clear();
                                txtEmailAsOnDate.MaxDate = DateTime.Today;

                                if (Session["UserType"] != null)
                                {
                                    if (Session["UserType"].ToString().ToLower() == "adviser" || Session["UserType"].ToString().ToLower() == "ops")
                                    {

                                        DataRow dr = dtGroupCustomerList.NewRow();
                                        dr["C_FirstName"] = "CUSTOMER LIST";
                                        dr["C_CustomerId"] = 0;

                                        dtGroupCustomerList.Rows.InsertAt(dr, 0);

                                        LBCustomer.DataSource = dtGroupCustomerList;
                                        LBCustomer.DataTextField = "C_FirstName";
                                        LBCustomer.DataValueField = "C_CustomerId";
                                        LBCustomer.DataBind();
                                    }
                                }

                            }
                        }
                        #endregion
                        ddlMFTransactionTypeBind();
                        if (!IsPostBack)
                        {


                            if (Cache["gvRequestStatus" + advisorVo.advisorId] != null)
                            {
                                Cache.Remove("gvRequestStatus" + advisorVo.advisorId);
                            }
                            pnlGvRequestStatus.Visible = false;

                            rbtnGrp.Checked = true;
                            rdpShowRequestStausGrid.SelectedDate = DateTime.Now;
                            lblNote2.Visible = true;

                            

                            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
                            DataSet ds = customerTransactionBo.GetLastMFTradeDate();
                            DateTime AsonDate = new DateTime();

                            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["WTD_Date"] != null)
                            {
                               
                            }
                          
                            RadTabStrip2.Tabs.FindTabByValue("tabpnlViewReports").Selected = true;
                            tabViewAndEmailReports.SelectedIndex = 0;

                            advisorId = advisorVo.advisorId;

                            if (Session[SessionContents.ValuationDate] == null)
                                GetLatestValuationDate();
                            genDict = (Dictionary<string, DateTime>)Session[SessionContents.ValuationDate];
                            LatestValuationdate = genDict[Constants.MFDate.ToString()];
                            hdnValuationDate.Value = LatestValuationdate.ToString();
                            txtAsOnDate.Text = LatestValuationdate.ToShortDateString();
                            txtFromDate.Text = LatestValuationdate.ToShortDateString();
                            txtToDate.Text = LatestValuationdate.ToShortDateString();
                            if (LatestValuationdate != DateTime.MinValue)
                            {
                                txtEmailAsOnDate.SelectedDate = LatestValuationdate;
                                txtEmailAsOnDate.SelectedDate = LatestValuationdate;
                                txtEmailFromDate.SelectedDate = LatestValuationdate;
                                txtEmailToDate.SelectedDate = LatestValuationdate;
                            }

                        }
                       
                        if (CustomerLogin == false)
                        {
                            if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                            {
                                hidBMLogin.Value = "False";
                                txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                                txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                                txtCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                                txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                            }
                            else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString() == "Ops")
                            {
                                hidBMLogin.Value = "False";
                                txtCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                                txtParentCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                                txtCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                                txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";


                            }
                            else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                            {
                                hidBMLogin.Value = "true";
                                txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                                txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                                txtCustomer_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                                txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetBMParentCustomerNames";

                            }
                        }

                        if (IsPostBack && !string.IsNullOrEmpty(Request.Form["ctrl_MFReports$hidTabIndex"]))
                        {
                            activeTabIndex = Convert.ToInt32(Request.Form["ctrl_MFReports$hidTabIndex"]);


                          
                            RadTabStrip2.Tabs[activeTabIndex].Selected = true;
                            tabViewAndEmailReports.SelectedIndex = 0;
                        }
                    }

                    gvRequestStatus.Rebind();
                    if (!IsPostBack)
                    {
                        if (Session["UserType"] != null)
                        {
                            if (Session["UserType"].ToString() == "adviser")
                            {
                                LBCustomer.Items[0].Enabled = false;
                            }
                        }
                    }
                }
                else if (userVo.UserType == "SuperAdmin")
                {
                    trAdviserSelection.Visible = true;
                    RadTabStrip2.Visible = false;
                    tabViewAndEmailReports.Visible = false;

                    if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$btnViewReport"] != "View Report" && Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlEmailReports$btnEmailReport"] != "Email Report")
                    {
                        path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                       
                      


                            hndCustomerLogin.Value = "false";
                            Session["hndCustomerLogin"] = hndCustomerLogin.Value;

                     


                        BindPeriodDropDown();

                      

                       
                            trCustomerName.Visible = false;
                            trIndCustomer.Visible = true;
                            trGroupCustomer.Visible = true;

                            trAdvisorRadioList.Visible = true;
                            trCustomerRadioList.Visible = false;

                            trAdminRM.Visible = true;
                            trCustomer.Visible = false;
                           


                        if (CustomerLogin == false)
                        {
                           

                            CustomerBo customerBo = new CustomerBo();
                            DataTable dtGroupCustomerList = new DataTable();
                           
                                RadTabStrip2.Tabs[1].Visible = true;
                                if (ddlAdviser.SelectedIndex!=0)
                                dtGroupCustomerList = customerBo.GetAdviserGroupCustomerName("BULKMAIL", int.Parse(ddlAdviser.SelectedValue.ToString()));
                                imgBtnrgHoldings.Visible = true;
                            
                           
                            if (!Page.IsPostBack)
                            {
                                txtEmailAsOnDate.SelectedDate = DateTime.Now;
                                txtEmailAsOnDate.Calendar.SpecialDays.Clear();
                                txtEmailAsOnDate.MaxDate = DateTime.Today;

                                if (Session["UserType"] != null)
                                {
                                    if (Session["UserType"].ToString().ToLower() == "adviser" || Session["UserType"].ToString().ToLower() == "ops")
                                    {

                                        DataRow dr = dtGroupCustomerList.NewRow();
                                        dr["C_FirstName"] = "CUSTOMER LIST";
                                        dr["C_CustomerId"] = 0;

                                        dtGroupCustomerList.Rows.InsertAt(dr, 0);

                                        LBCustomer.DataSource = dtGroupCustomerList;
                                        LBCustomer.DataTextField = "C_FirstName";
                                        LBCustomer.DataValueField = "C_CustomerId";
                                        LBCustomer.DataBind();
                                    }
                                }

                            }
                        }
                        ddlMFTransactionTypeBind();
                        if (!IsPostBack)
                        {


                            if (Cache["gvRequestStatus" + ddlAdviser.SelectedValue] != null)
                            {
                                Cache.Remove("gvRequestStatus" + ddlAdviser.SelectedValue);
                            }
                            pnlGvRequestStatus.Visible = false;

                            rbtnGrp.Checked = true;
                            rdpShowRequestStausGrid.SelectedDate = DateTime.Now;
                            lblNote2.Visible = true;

                            

                            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
                            DataSet ds = customerTransactionBo.GetLastMFTradeDate();
                            DateTime AsonDate = new DateTime();

                            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["WTD_Date"] != null)
                            {
                            }
                           


                            RadTabStrip2.Tabs.FindTabByValue("tabpnlViewReports").Selected = true;
                            tabViewAndEmailReports.SelectedIndex = 0;

                            if (ddlAdviser.SelectedIndex!=0)
                            advisorId =Convert.ToInt32(ddlAdviser.SelectedValue);
                          
                            if (Session[SessionContents.ValuationDate] == null)
                                GetLatestValuationDate();
                            genDict = (Dictionary<string, DateTime>)Session[SessionContents.ValuationDate];
                            LatestValuationdate = genDict[Constants.MFDate.ToString()];
                            hdnValuationDate.Value = LatestValuationdate.ToString();
                            txtAsOnDate.Text = LatestValuationdate.ToShortDateString();
                            txtFromDate.Text = LatestValuationdate.ToShortDateString();
                            txtToDate.Text = LatestValuationdate.ToShortDateString();
                            if (LatestValuationdate != DateTime.MinValue)
                            {
                                txtEmailAsOnDate.SelectedDate = LatestValuationdate;
                                txtEmailAsOnDate.SelectedDate = LatestValuationdate;
                                txtEmailFromDate.SelectedDate = LatestValuationdate;
                                txtEmailToDate.SelectedDate = LatestValuationdate;
                            }

                        }
                       
                        if (CustomerLogin == false)
                        {
                            
                                hidBMLogin.Value = "False";
                                if (advisorVo==null)
                                txtCustomer_autoCompleteExtender.ContextKey =  ddlAdviser.SelectedValue;
                                txtParentCustomer_autoCompleteExtender.ContextKey = ddlAdviser.SelectedValue;
                                txtCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                                txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";


                           
                        }

                        if (IsPostBack && !string.IsNullOrEmpty(Request.Form["ctrl_MFReports$hidTabIndex"]))
                        {
                            activeTabIndex = Convert.ToInt32(Request.Form["ctrl_MFReports$hidTabIndex"]);


                          
                            RadTabStrip2.Tabs[activeTabIndex].Selected = true;
                            tabViewAndEmailReports.SelectedIndex = 0;
                        }
                    }

                    gvRequestStatus.Rebind();
                    if (!IsPostBack)
                    {
                        if (Session["UserType"] != null)
                        {
                            if (Session["UserType"].ToString() == "adviser")
                            {
                                LBCustomer.Items[0].Enabled = false;
                            }
                        }
                    }
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
                FunctionInfo.Add("Method", "MFReports.ascx.cs:Page_Load(object sender, EventArgs e)");
                object[] objects = new object[2];
                objects[0] = sender;
                objects[1] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void BindAdviserDropDownList()
        {
            
            dtAdviserList = superAdminOpsBo.BindAdviserForUpload();

            if (dtAdviserList.Rows.Count > 0)
            {
                ddlAdviser.DataSource = dtAdviserList;
                ddlAdviser.DataTextField = "A_OrgName";
                ddlAdviser.DataValueField = "A_AdviserId";
                ddlAdviser.DataBind();
            }
            ddlAdviser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

        }

        protected void ddlAdviser_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            RadTabStrip2.Visible = true;
            tabViewAndEmailReports.Visible = true;
            RadTabStrip2.Tabs[0].Visible = true;
            RadTabStrip2.Tabs[1].Visible = false;
            RadTabStrip2.Tabs[2].Visible = false;
            
            txtCustomer.Text="";
            txtParentCustomer.Text = "";



            if (ddlAdviser.SelectedValue != "Select")
                advisorId = Convert.ToInt32(ddlAdviser.SelectedValue);

            if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$btnViewReport"] != "View Report" && Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlEmailReports$btnEmailReport"] != "Email Report")
            {
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
             


                BindPeriodDropDown();

              

                #region new gr
                
                    trCustomerName.Visible = false;
                    trIndCustomer.Visible = true;
                    trGroupCustomer.Visible = true;

                    trAdvisorRadioList.Visible = true;
                    trCustomerRadioList.Visible = false;

                    trAdminRM.Visible = true;
                    trCustomer.Visible = false;
                   
                if (CustomerLogin == false)
                {
                   

                  

                    CustomerBo customerBo = new CustomerBo();
                    DataTable dtGroupCustomerList = new DataTable();
                   
                        dtGroupCustomerList = customerBo.GetAdviserGroupCustomerName("BULKMAIL", int.Parse(advisorId.ToString()));
                        imgBtnrgHoldings.Visible = true;
                    

                    if (!Page.IsPostBack)
                    {
                        txtEmailAsOnDate.SelectedDate = DateTime.Now;
                        txtEmailAsOnDate.Calendar.SpecialDays.Clear();
                        txtEmailAsOnDate.MaxDate = DateTime.Today;

                        if (Session["UserType"] != null)
                        {
                            if (Session["UserType"].ToString().ToLower() == "adviser" || Session["UserType"].ToString().ToLower() == "ops")
                            {

                                DataRow dr = dtGroupCustomerList.NewRow();
                                dr["C_FirstName"] = "CUSTOMER LIST";
                                dr["C_CustomerId"] = 0;

                                dtGroupCustomerList.Rows.InsertAt(dr, 0);

                                LBCustomer.DataSource = dtGroupCustomerList;
                                LBCustomer.DataTextField = "C_FirstName";
                                LBCustomer.DataValueField = "C_CustomerId";
                                LBCustomer.DataBind();
                            }
                        }

                    }
                }
                #endregion
                ddlMFTransactionTypeBind();
                if (!IsPostBack)
                {


                    if (Cache["gvRequestStatus" + advisorVo.advisorId] != null)
                    {
                        Cache.Remove("gvRequestStatus" + advisorVo.advisorId);
                    }
                    pnlGvRequestStatus.Visible = false;

                    rbtnGrp.Checked = true;
                    rdpShowRequestStausGrid.SelectedDate = DateTime.Now;
                    lblNote2.Visible = true;

                    

                    CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
                    DataSet ds = customerTransactionBo.GetLastMFTradeDate();
                    DateTime AsonDate = new DateTime();

                    if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["WTD_Date"] != null)
                    {
                    }
                   
                    RadTabStrip2.Tabs.FindTabByValue("tabpnlViewReports").Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 0;

                    advisorId = advisorVo.advisorId;
                    if (Session[SessionContents.ValuationDate] == null)
                        GetLatestValuationDate();
                    genDict = (Dictionary<string, DateTime>)Session[SessionContents.ValuationDate];
                    LatestValuationdate = genDict[Constants.MFDate.ToString()];
                    hdnValuationDate.Value = LatestValuationdate.ToString();
                    txtAsOnDate.Text = LatestValuationdate.ToShortDateString();
                    txtFromDate.Text = LatestValuationdate.ToShortDateString();
                    txtToDate.Text = LatestValuationdate.ToShortDateString();
                    if (LatestValuationdate != DateTime.MinValue)
                    {
                        txtEmailAsOnDate.SelectedDate = LatestValuationdate;
                        txtEmailAsOnDate.SelectedDate = LatestValuationdate;
                        txtEmailFromDate.SelectedDate = LatestValuationdate;
                        txtEmailToDate.SelectedDate = LatestValuationdate;
                    }

                }
              
                if (CustomerLogin == false)
                {
                  
                        hidBMLogin.Value = "False";
                        txtCustomer_autoCompleteExtender.ContextKey = advisorId.ToString();
                        txtParentCustomer_autoCompleteExtender.ContextKey = advisorId.ToString();
                        txtCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                        txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";


                    
                    
                }

                if (IsPostBack && !string.IsNullOrEmpty(Request.Form["ctrl_MFReports$hidTabIndex"]))
                {
                    activeTabIndex = Convert.ToInt32(Request.Form["ctrl_MFReports$hidTabIndex"]);


                 
                    RadTabStrip2.Tabs[activeTabIndex].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 0;
                }
            }

            gvRequestStatus.Rebind();
            if (!IsPostBack)
            {
                if (Session["UserType"] != null)
                {
                    if (Session["UserType"].ToString() == "adviser")
                    {
                        LBCustomer.Items[0].Enabled = false;
                    }
                }
            }
            if (ddlAdviser.SelectedIndex != 0)
            {
                Session["SAReportsAdviserID"] = Convert.ToInt32(ddlAdviser.SelectedValue);
                Session["SAReportsAdviserOrgName"] = ddlAdviser.SelectedItem;
            }
        }

        private void GetLatestValuationDate()
        {
            DateTime EQValuationDate = new DateTime();
            DateTime MFValuationDate = new DateTime();
            PortfolioBo portfolioBo = null;
            genDict = new Dictionary<string, DateTime>();
            AdvisorVo advisorVo = new AdvisorVo();
            int adviserId = 0;
            try
            {
                portfolioBo = new PortfolioBo();
                advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                if (advisorVo != null)
                    adviserId = advisorVo.advisorId;
                else
                {
                    if (ddlAdviser.SelectedIndex != 0)
                    {
                        adviserId = Convert.ToInt32(ddlAdviser.SelectedValue);
                    }
                }
                if (portfolioBo.GetLatestValuationDate(adviserId, Constants.MF.ToString()) != null)
                {
                    MFValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, Constants.MF.ToString()).ToString());
                }
                genDict.Add(Constants.MFDate.ToString(), MFValuationDate);
                Session[SessionContents.ValuationDate] = genDict;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioDashboard.ascx.cs:GetLatestValuationDate()");
                object[] objects = new object[3];
                objects[0] = EQValuationDate;
                objects[1] = adviserId;
                objects[2] = MFValuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void BindGvRequestStatus(int adviserId, DateTime dtSelectedRequestDate)
        {
            GetRequestStatusList(adviserId, dtSelectedRequestDate);
            gvRequestStatus.DataSource = dtRequestStatusList;

            if (dtRequestStatusList.Rows.Count != 0)
                imgBtnrgHoldings.Visible = true;
            else
                imgBtnrgHoldings.Visible = false;
            gvRequestStatus.MasterTableView.UseAllDataFields = false;
            gvRequestStatus.DataBind();


            RadTabStrip2.Tabs[2].Selected = true;
            tabViewAndEmailReports.SelectedIndex = 2;


            gvRequestStatus.Rebind();
            if (dtRequestStatusList.Rows.Count != 0)
            {

            }
        }


        protected void btnShowRequestStausGrid_Click(object sender, EventArgs e)
        {
            BindGvRequestStatus(advisorVo.advisorId, Convert.ToDateTime(rdpShowRequestStausGrid.SelectedDate));
         
        }

        protected void rbtnDate_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        {
            CustomerBo customerBo = new CustomerBo();

            if (hdnCustomerId.Value != string.Empty)
            {
                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(hdnCustomerId.Value));
                DataRow dr = dt.Rows[0];
                hdnCustomerId1.Value = hdnCustomerId.Value;
                txtCustomerPAN.Text = dr["C_PANNum"].ToString();
                trCustomerDetails.Style.Add("display", "block");
                if (rdoIndividual.Checked)
                    ShowFolios();
                else
                    ShowGroupCustomers();
                customerVo = customerBo.GetCustomer(int.Parse(hdnCustomerId.Value));
                Session["CusVo"] = customerVo;
                txtParentCustomer.Text = customerVo.FirstName.ToString() + customerVo.MiddleName.ToString() + customerVo.LastName.ToString();
                txtCustomer.Text = customerVo.FirstName.ToString() + customerVo.MiddleName.ToString() + customerVo.LastName.ToString();
                hdnCustomerId.Value = "";
            }
          
            RadTabStrip2.Tabs[0].Selected = true;
            tabViewAndEmailReports.SelectedIndex = 0;

        }
       
        protected void IndivisulCustomerLogin()
        {
            lblCustomerName.Text = customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName;
            DataTable dt = customerBo.GetCustomerPanAddress(customerVo.CustomerId);
            DataRow dr = dt.Rows[0];
            hdnCustomerId1.Value = customerVo.CustomerId.ToString();
            txtCustomerPAN.Text = dr["C_PANNum"].ToString();
            trCustomerDetails.Style.Add("display", "block");

            if (rdoIndividual.Checked)
                ShowFolios();
            else
                ShowAllCustomer();
           
            RadTabStrip2.Tabs[activeTabIndex].Selected = true;
            tabViewAndEmailReports.SelectedIndex = 0;
        }
        protected void ChckBussDate_Textchanged(object sender, EventArgs e)
        {
            CustomerBo customerBo = new CustomerBo();
            bool isCorrect = false;
            DateTime dtAsOnDate = DateTime.Parse(txtAsOnDate.Text);
            DateTime maxValuationDate = new DateTime();
            maxValuationDate = DateTime.Parse(hdnValuationDate.Value);
            if (dtAsOnDate.Date <= maxValuationDate.Date)
            {
                isCorrect = customerBo.ChckBussinessDate(dtAsOnDate);
                if (isCorrect == true)
                {
                    btnEmailReport.Enabled = true;
                    btnExportToPDF.Enabled = true;
                    btnViewInDOC.Enabled = true;
                    btnViewReport.Enabled = true;


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Invalid!!!Choose a Valid Bussiness Date ');", true);
                    btnEmailReport.Enabled = false;
                    btnExportToPDF.Enabled = false;
                    btnViewInDOC.Enabled = false;
                    btnViewReport.Enabled = false;
                }


            }
            else
            {
                if (ddlReportSubType.SelectedValue.ToString() == "RETURNS_PORTFOLIO" || ddlReportSubType.SelectedValue.ToString() == "COMPREHENSIVE" || ddlReportSubType.SelectedValue.ToString() == "CATEGORY_WISE" || ddlReportSubType.SelectedValue.ToString() == "REALIZED_REPORT")
                {
                    isCorrect = customerBo.ChckBussinessDate(dtAsOnDate);
                    if (isCorrect == true)
                    {
                        btnEmailReport.Enabled = true;
                        btnExportToPDF.Enabled = true;
                        btnViewInDOC.Enabled = true;
                        btnViewReport.Enabled = true;


                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Invalid!!!Choose a Valid Bussiness Date ');", true);
                        btnEmailReport.Enabled = false;
                        btnExportToPDF.Enabled = false;
                        btnViewInDOC.Enabled = false;
                        btnViewReport.Enabled = false;
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select Prior Business Date');", true);
                    btnEmailReport.Enabled = false;
                    btnExportToPDF.Enabled = false;
                    btnViewInDOC.Enabled = false;
                    btnViewReport.Enabled = false;
                }

            }

        }

      
        private void ShowAllCustomer()
        {
            CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
            DataTable dt = customerFamilyBo.GetAllCustomerAssociates(customerVo.CustomerId);
            if (ddlPortfolioGroup.SelectedValue == "Select")
            {
                return;
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder strCustomers = new StringBuilder();
                strCustomers.Append("<table border='0'>");

                strCustomers.Append("<tr><td colspan='3'><b>All Customers Under Group Head :</b></td></tr>");

                foreach (DataRow dr in dt.Rows)
                {
                    strCustomers.Append("<tr>");
                    strCustomers.Append("<td>" + dr["CustomerName"].ToString() + "</td>");
                    strCustomers.Append("<td>" + ShowGroupFolioCustomerlogin(Convert.ToInt32(dr["C_AssociateCustomerId"])) + "</td>");
                    strCustomers.Append("</tr>");
                }
                strCustomers.Append("</table>");
                divGroupCustomers.InnerHtml = strCustomers.ToString();

            }
            else
            {
                divGroupCustomers.InnerHtml = "No Customers found";
            }

            
            RadTabStrip2.Tabs[activeTabIndex].Selected = true;
            tabViewAndEmailReports.SelectedIndex = activeTabIndex;

            divPortfolios.InnerHtml = string.Empty;
        }

        
        private void ShowGroupCustomers()
        {
            CustomerBo customerBo = new CustomerBo();
            if (ddlPortfolioGroup.SelectedValue == "Select")
            {
                return;
            }
            if ((hdnCustomerId1.Value != string.Empty))
            {
                CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
                DataTable dt = customerFamilyBo.GetAllCustomerAssociates(int.Parse(hdnCustomerId1.Value));
                if (dt != null && dt.Rows.Count > 0)
                {
                    StringBuilder strCustomers = new StringBuilder();
                    strCustomers.Append("<table border='0'>");

                    strCustomers.Append("<tr><td colspan='3'><b>All Customers Under Group Head :</b></td></tr>");

                    foreach (DataRow dr in dt.Rows)
                    {
                        strCustomers.Append("<tr>");
                        strCustomers.Append("<td>" + dr["CustomerName"].ToString() + "</td>");
                        strCustomers.Append("<td>" + ShowGroupFolios(Convert.ToInt32(dr["C_AssociateCustomerId"])) + "</td>");
                        strCustomers.Append("</tr>");
                    }
                    strCustomers.Append("</table>");
                    divGroupCustomers.InnerHtml = strCustomers.ToString();

                }
                else
                {
                    divGroupCustomers.InnerHtml = "No Customers found";
                }

            }
         
            RadTabStrip2.Tabs[activeTabIndex].Selected = true;
            tabViewAndEmailReports.SelectedIndex = activeTabIndex;

            divPortfolios.InnerHtml = string.Empty;
        }

    
        private void BindPeriodDropDown()
        {
            DataTable dtPeriod;
            dtPeriod = XMLBo.GetDatePeriod(path);

            ddlPeriod.DataSource = dtPeriod;
            ddlPeriod.DataTextField = "PeriodType";
            ddlPeriod.DataValueField = "PeriodCode";
            ddlPeriod.DataBind();
            ddlPeriod.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select a Period", "Select a Period"));

            ddlEmailDatePeriod.DataSource = dtPeriod;
            ddlEmailDatePeriod.DataTextField = "PeriodType";
            ddlEmailDatePeriod.DataValueField = "PeriodCode";
            ddlEmailDatePeriod.DataBind();
            ddlEmailDatePeriod.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select a Period", "Select a Period"));
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
        }

        protected void ddlPortfolioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selfCheck = string.Empty;
            if (!string.IsNullOrEmpty(hndSelfOrGroup.Value.ToString()))
            {
                selfCheck = hndSelfOrGroup.Value.ToString();
            }

            if (rdoIndividual.Checked || selfCheck == "self")
                ShowFolios();
            else
                ShowGroupCustomers();
        }

       

        private void ShowFolios()
        {

            PortfolioBo portfolioBo = new PortfolioBo();
            divPortfolios.InnerHtml = string.Empty;
           if(ddlPortfolioGroup.SelectedValue == "Select") 
           {
               return ;
           }
            if (!String.IsNullOrEmpty(hdnCustomerId1.Value) ) 
            {
                int customerId = Convert.ToInt32(hdnCustomerId1.Value);
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); 
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) 
                {
                    StringBuilder checkbox = new StringBuilder();

                  
                    foreach (CustomerPortfolioVo custPortfolio in customerPortfolioVos)
                    {
                        if (ddlPortfolioGroup.SelectedValue == "MANAGED" && (custPortfolio.IsMainPortfolio != 1))
                        {
                            if (customerPortfolioVos.Count == 1)
                                checkbox.Append("<span class='Error'>No managed portfolios found for this customer.Can't create report.</span>");
                            continue;
                        }
                        else if (ddlPortfolioGroup.SelectedValue == "UN_MANAGED" & (custPortfolio.IsMainPortfolio != 0))
                        {
                            if (customerPortfolioVos.Count == 1)
                                checkbox.Append("<span class='Error'>No unmanaged portfolios found for this customer.Can't create report.</span>");
                            continue;
                        }
                        if (String.IsNullOrEmpty(custPortfolio.PortfolioName))
                            custPortfolio.PortfolioName = "No Name";
                        checkbox.Append("<input type='checkbox' checked name='chk--" + custPortfolio.PortfolioId + "' id='chk--" + custPortfolio.PortfolioId + "'>" + custPortfolio.PortfolioName);
                    }
                    divPortfolios.InnerHtml = checkbox.ToString();
                }
                else 
                {
                    divPortfolios.InnerHtml = "<span class='Error'>No portfolios found for this customer.Can't create report.</span>";
                }
            }
            else 
            {
                divPortfolios.InnerHtml = "<span class='Error'>Invalid Customer selected.</span>";
            }
            divGroupCustomers.InnerHtml = string.Empty;
        }
     
        private string ShowGroupFolios(int customerId)
        {
            StringBuilder checkbox = new StringBuilder();
            PortfolioBo portfolioBo = new PortfolioBo();
            if (ddlPortfolioGroup.SelectedValue == "Select")
            {
                return "";
            }
            if (!String.IsNullOrEmpty(hdnCustomerId1.Value)) 
            {
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); 
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) 
                {
                    
                    foreach (CustomerPortfolioVo custPortfolio in customerPortfolioVos)
                    {
                        if (ddlPortfolioGroup.SelectedValue == "MANAGED" && (custPortfolio.IsMainPortfolio != 1))
                        {
                            if (customerPortfolioVos.Count == 1)
                                checkbox.Append("<span class='Error'>No portfolio</span>");
                            continue;
                        }
                        else if (ddlPortfolioGroup.SelectedValue == "UN_MANAGED" & (custPortfolio.IsMainPortfolio != 0))
                        {
                            if (customerPortfolioVos.Count == 1)
                                checkbox.Append("<span class='Error'>No portfolio</span>");
                            continue;
                        }
                        if (String.IsNullOrEmpty(custPortfolio.PortfolioName))
                            custPortfolio.PortfolioName = "No Name";
                        checkbox.Append("<input type='checkbox' checked name='chk--" + custPortfolio.PortfolioId + "' id='chk--" + custPortfolio.PortfolioId + "'>" + custPortfolio.PortfolioName);
                    }
                }
                else 
                {
                    checkbox.Append("--");
                }

            }
            else 
            {
                divPortfolios.InnerHtml = "<span class='Error'>Invalid Customer selected.</span>";
            }
            return checkbox.ToString();
        }

        
        private string ShowGroupFolioCustomerlogin(int customerId)
        {
            StringBuilder checkbox = new StringBuilder();
            PortfolioBo portfolioBo = new PortfolioBo();
            if (ddlPortfolioGroup.SelectedValue == "Select")
            {
                return "";
            }
            if (!String.IsNullOrEmpty(customerVo.CustomerId.ToString()))   
            {
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); 
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) 
                {
                  
                    foreach (CustomerPortfolioVo custPortfolio in customerPortfolioVos)
                    {
                        if (ddlPortfolioGroup.SelectedValue == "MANAGED" && (custPortfolio.IsMainPortfolio != 1))
                        {
                            if (customerPortfolioVos.Count == 1)
                                checkbox.Append("<span class='Error'>No portfolio</span>");
                            continue;
                        }
                        else if (ddlPortfolioGroup.SelectedValue == "UN_MANAGED" & (custPortfolio.IsMainPortfolio != 0))
                        {
                            if (customerPortfolioVos.Count == 1)
                                checkbox.Append("<span class='Error'>No portfolio</span>");
                            continue;
                        }
                        if (String.IsNullOrEmpty(custPortfolio.PortfolioName))
                            custPortfolio.PortfolioName = "No Name";
                        checkbox.Append("<input type='checkbox' checked name='chk--" + custPortfolio.PortfolioId + "' id='chk--" + custPortfolio.PortfolioId + "'>" + custPortfolio.PortfolioName);
                    }
                }
                else 
                {
                    checkbox.Append("--");
                }

            }
            else 
            {
                divPortfolios.InnerHtml = "<span class='Error'>Invalid Customer selected.</span>";
            }
            return checkbox.ToString();

        }

       

        protected void ddlMFTransactionTypeBind()
        {
            MFReportsBo mfReportBo = new MFReportsBo();
            DataSet ds = new DataSet();
            ds = mfReportBo.GetMFTransactionType();
            ddlMFTransactionType.DataSource = ds;
            ddlMFTransactionType.DataValueField = "TransCode";
            ddlMFTransactionType.DataTextField = "TransName";
            ddlMFTransactionType.DataBind();
            ddlMFTransactionType.Items.Insert(0, new ListItem("ALL", "0"));
            ddlMFTransactionType.SelectedIndex = 0;
            rddate.Checked = true;


        }

        protected void rbnGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (userVo.UserType != "SuperAdmin")
            {
                LBCustomer.Items.Clear();
                string controlName = this.Request.Params.Get("__EVENTTARGET");

                if (controlName != "ctrl_MFReports$btnEmailReport")
                {
                    RadListBoxDestination.Items.Clear();
                }

                CustomerBo customerBo = new CustomerBo();
                DataTable dtGroupCustomerList = new DataTable();

                if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    dtGroupCustomerList = customerBo.GetParentCustomerName("BULKMAIL", int.Parse(rmVo.RMId.ToString()));
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString() == "Ops")
                {

                 dtGroupCustomerList = customerBo.GetAdviserGroupCustomerName("BULKMAIL", int.Parse(advisorVo.advisorId.ToString()));

                }

                DataRow dr = dtGroupCustomerList.NewRow();
                if (dtGroupCustomerList.Rows.Count != 0)
                {
                    dr["C_FirstName"] = "CUSTOMER LIST";
                    dr["C_CustomerId"] = 0;

                    dtGroupCustomerList.Rows.InsertAt(dr, 0);
                    LBCustomer.DataSource = dtGroupCustomerList;
                    LBCustomer.DataTextField = "C_FirstName";
                    LBCustomer.DataValueField = "C_CustomerId";
                    LBCustomer.DataBind();

                    LBCustomer.Items[0].Enabled = false;
                }
                
                if (RadTabStrip2.Tabs[1].Selected != false)
                {
                    RadTabStrip2.Tabs[1].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 1;
                }
                else
                {
                    RadTabStrip2.Tabs[0].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 0;
                }
            }
            else
            {
                LBCustomer.Items.Clear();
                string controlName = this.Request.Params.Get("__EVENTTARGET");

                if (controlName != "ctrl_MFReports$btnEmailReport")
                {
                    RadListBoxDestination.Items.Clear();
                }

                CustomerBo customerBo = new CustomerBo();
                DataTable dtGroupCustomerList = new DataTable();

                


                

                DataRow dr = dtGroupCustomerList.NewRow();
                if (dtGroupCustomerList.Rows.Count != 0)
                {
                    dr["C_FirstName"] = "CUSTOMER LIST";
                    dr["C_CustomerId"] = 0;

                    dtGroupCustomerList.Rows.InsertAt(dr, 0);
                    LBCustomer.DataSource = dtGroupCustomerList;
                    LBCustomer.DataTextField = "C_FirstName";
                    LBCustomer.DataValueField = "C_CustomerId";
                    LBCustomer.DataBind();

                    LBCustomer.Items[0].Enabled = false;
                }
               
                if (RadTabStrip2.Tabs[1].Selected != false)
                {
                    RadTabStrip2.Tabs[1].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 1;
                }
                else
                {
                    RadTabStrip2.Tabs[0].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 0;
                }

            }
        }

        protected void rbnIndivisual_CheckedChanged(object sender, EventArgs e)
        {
            if (userVo.UserType != "SuperAdmin")
            {
               
                LBCustomer.Items.Clear();
                string controlName = this.Request.Params.Get("__EVENTTARGET");

                if (controlName != "ctrl_MFReports$btnEmailReport")
                {
                    RadListBoxDestination.Items.Clear();
                }
                CustomerBo customerBo = new CustomerBo();
                DataTable dtIndiviCustomerList = new DataTable();

                if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    dtIndiviCustomerList = customerBo.GetAllRMMemberCustomerName("BULKMAIL", int.Parse(rmVo.RMId.ToString()));
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString() == "Ops")
                {

                    dtIndiviCustomerList = customerBo.GetAdviserCustomerName("BULKMAIL", int.Parse(advisorVo.advisorId.ToString()));

                }



                DataRow dr = dtIndiviCustomerList.NewRow();
                dr["C_FirstName"] = "CUSTOMER LIST";
                dr["C_CustomerId"] = 0;

                dtIndiviCustomerList.Rows.InsertAt(dr, 0);

                LBCustomer.DataSource = dtIndiviCustomerList;
                LBCustomer.DataTextField = "C_FirstName";
                LBCustomer.DataValueField = "C_CustomerId";
                LBCustomer.DataBind();

                LBCustomer.Items[0].Enabled = false;


                if (RadTabStrip2.Tabs[1].Selected != false)
                {
                    RadTabStrip2.Tabs[1].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 1;
                }
                else
                {
                    RadTabStrip2.Tabs[0].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 0;
                }

            }
            else
            { 
                LBCustomer.Items.Clear();
                string controlName = this.Request.Params.Get("__EVENTTARGET");

                if (controlName != "ctrl_MFReports$btnEmailReport")
                {
                    RadListBoxDestination.Items.Clear();
                }
                CustomerBo customerBo = new CustomerBo();
                DataTable dtIndiviCustomerList = new DataTable();

               

                    dtIndiviCustomerList = customerBo.GetAdviserCustomerName("BULKMAIL", int.Parse(advisorVo.advisorId.ToString()));

               



                DataRow dr = dtIndiviCustomerList.NewRow();
                dr["C_FirstName"] = "CUSTOMER LIST";
                dr["C_CustomerId"] = 0;

                dtIndiviCustomerList.Rows.InsertAt(dr, 0);

                LBCustomer.DataSource = dtIndiviCustomerList;
                LBCustomer.DataTextField = "C_FirstName";
                LBCustomer.DataValueField = "C_CustomerId";
                LBCustomer.DataBind();

                LBCustomer.Items[0].Enabled = false;

              
                if (RadTabStrip2.Tabs[1].Selected != false)
                {
                    RadTabStrip2.Tabs[1].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 1;
                }
                else
                {
                    RadTabStrip2.Tabs[0].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 0;
                }


            }

        }


    
        private string GetAllSelectedCustomerID(DanLudwig.Controls.Web.ListBox CustomerSelectedListBox)
        {
            String AllCustomerId = "";
            for (int i = CustomerSelectedListBox.Items.Count - 1; i >= 0; i--)
            {
                ListItem TempItem = CustomerSelectedListBox.Items[i];

                AllCustomerId = AllCustomerId + "," + TempItem.Value.ToString();


            }
            return AllCustomerId;
        }



        protected void AddSelected_Click(object sender, EventArgs e)
        {

        }

        protected void RemoveSelected_Click(object sender, EventArgs e)
        {
           
        }

        protected void SelectAll_Click(object sender, EventArgs e)
        {
          

        }
        protected void RemoveAll_Click(object sender, EventArgs e)
        {
           
        }

        protected void rbnAllCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (userVo.UserType != "SuperAdmin")
            {
               
                LBCustomer.Items.Clear();
                string controlName = this.Request.Params.Get("__EVENTTARGET");

                if (controlName != "ctrl_MFReports$btnEmailReport")
                {
                    RadListBoxDestination.Items.Clear();
                }
                CustomerBo customerBo = new CustomerBo();
                DataTable dtIndiviCustomerList = new DataTable();

                if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    dtIndiviCustomerList = customerBo.GetMemberCustomerName("BULKMAIL", int.Parse(rmVo.RMId.ToString()));
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString() == "Ops")
                {

                    dtIndiviCustomerList = customerBo.GetAllCustomerName("BULKMAIL", int.Parse(advisorVo.advisorId.ToString()));

                }

                DataRow dr = dtIndiviCustomerList.NewRow();
                dr["C_FirstName"] = "CUSTOMER LIST";
                dr["C_CustomerId"] = 0;

                dtIndiviCustomerList.Rows.InsertAt(dr, 0);

                LBCustomer.DataSource = dtIndiviCustomerList;
                LBCustomer.DataTextField = "C_FirstName";
                LBCustomer.DataValueField = "C_CustomerId";
                LBCustomer.DataBind();

                LBCustomer.Items[0].Enabled = false;

               
                if (RadTabStrip2.Tabs[1].Selected != false)
                {
                    RadTabStrip2.Tabs[1].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 1;
                }
                else
                {
                    RadTabStrip2.Tabs[0].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 0;
                }
            }
            else
            { 
                LBCustomer.Items.Clear();
                string controlName = this.Request.Params.Get("__EVENTTARGET");

                if (controlName != "ctrl_MFReports$btnEmailReport")
                {
                    RadListBoxDestination.Items.Clear();
                }
                CustomerBo customerBo = new CustomerBo();
                DataTable dtIndiviCustomerList = new DataTable();

             

                    dtIndiviCustomerList = customerBo.GetAllCustomerName("BULKMAIL", int.Parse(advisorVo.advisorId.ToString()));

               
                DataRow dr = dtIndiviCustomerList.NewRow();
                dr["C_FirstName"] = "CUSTOMER LIST";
                dr["C_CustomerId"] = 0;

                dtIndiviCustomerList.Rows.InsertAt(dr, 0);

                LBCustomer.DataSource = dtIndiviCustomerList;
                LBCustomer.DataTextField = "C_FirstName";
                LBCustomer.DataValueField = "C_CustomerId";
                LBCustomer.DataBind();

                LBCustomer.Items[0].Enabled = false;

      
                if (RadTabStrip2.Tabs[1].Selected != false)
                {
                    RadTabStrip2.Tabs[1].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 1;
                }
                else
                {
                    RadTabStrip2.Tabs[0].Selected = true;
                    tabViewAndEmailReports.SelectedIndex = 0;
                }

            }

        }
        protected void btnViewStatus_Click(object sender, EventArgs e)
        {
            rdpShowRequestStausGrid.SelectedDate = DateTime.Now;
            BindGvRequestStatus(advisorVo.advisorId, DateTime.Now);


            RadTabStrip2.TabIndex = 2;
           
            tabViewAndEmailReports.SelectedIndex = 2;
            RadTabStrip2.Tabs[2].Selected = true;
            RadTabStrip2.SelectedIndex = 2;
            RadListBoxDestination.Items.Clear();

        }


        protected void btnEmailReport_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> bulkMailRequest = new Dictionary<string, object>();
            DateTime fromDateRangeRpt;
            DateTime toDateRangeRpt;
            
            StringBuilder allCustomerId = new StringBuilder();
            StringBuilder strReportAsOn = new StringBuilder();
            StringBuilder strReportRange = new StringBuilder();
            int isGroupHead = 0;
            if (rbtnGrp.Checked == true)
            {
                isGroupHead = 1;
            }

            CalculateDateRange(out fromDateRangeRpt, out toDateRangeRpt);

            bulkMailRequest.Add("TASK_ID", 1);

            foreach (RadListBoxItem ListItem in this.RadListBoxDestination.Items)
            {
                allCustomerId.Append(ListItem.Value.ToString()+"~");
            }
            bulkMailRequest.Add("CUST_IDS", allCustomerId);

            if (!string.IsNullOrEmpty(allCustomerId.ToString()))
            allCustomerId.Remove(allCustomerId.Length - 1, 1);

            foreach (ListItem chkItems in chkAsOnReportList.Items)
            {
                if (chkItems.Selected == true)
                {
                    strReportAsOn.Append(chkItems.Value.Trim() + "~");
                }
            }

            if (!string.IsNullOrEmpty(strReportAsOn.ToString()))
            strReportAsOn.Remove(strReportAsOn.Length - 1, 1);

            bulkMailRequest.Add("ASON_REPORT", strReportAsOn);

            foreach (ListItem chkItems in chkRangeReportList.Items)
            {
                if (chkItems.Selected == true)
                {
                    strReportRange.Append(chkItems.Value.Trim() + "~");
                }
            }
            if (!string.IsNullOrEmpty(strReportRange.ToString()))
            strReportRange.Remove(strReportRange.Length - 1, 1);

            bulkMailRequest.Add("RANGE_REPORT", strReportRange);

            bulkMailRequest.Add("ADVISER_ID", advisorVo.advisorId);
            bulkMailRequest.Add("USER_ID", userVo.UserId);
            bulkMailRequest.Add("IS_GROUP_HEAD_REPORT", isGroupHead);
            bulkMailRequest.Add("START_DATE", fromDateRangeRpt);
            bulkMailRequest.Add("END_DATE", toDateRangeRpt);
            bulkMailRequest.Add("ASON_DATE", txtAsOnDate.Text);

            taskRequestManagementBo.CreateBulkMailRequestRecord(bulkMailRequest);

            msgEmailSentComplete.Visible = true;

            tabViewAndEmailReports.SelectedIndex = 1;
            RadListBoxDestination.Items.Clear();

        }



        private MFReportVo GetReportInputData(ListItem chkItems, int customerId, int groupCustomerId, ref DateTime dtFrom, ref DateTime dtTo, string reportDateType)
        {

            MFReportVo mfReportVo = new MFReportVo();
            mfReport.ReportName = chkItems.Value.Trim();
            if (reportDateType == "ASON")
            {
                mfReport.FromDate = Convert.ToDateTime(txtEmailAsOnDate.SelectedDate);
                mfReport.ToDate = mfReport.FromDate;
            }
            else if (reportDateType == "RANGE")
            {
                mfReport.FromDate = dtFrom;
                mfReport.ToDate = dtTo;
            }
            mfReport.SubType = "MF";
            mfReport.AdviserId = advisorVo.advisorId;
            mfReport.CustomerId = customerId;
            mfReport.GroupHeadId = groupCustomerId;
            if (groupCustomerId != 0)
            {
                mfReport.PortfolioIds = GetGroupCustomerAllPortfolio(groupCustomerId);
            }
            else
            {
                mfReport.PortfolioIds = GetCustomerAllPortfolio(customerId);
            }

            return mfReport;
        }



      
        private string GetGroupCustomerAllPortfolio(int groupCustomerId)
        {
            string AllFolioIds = "";
            CustomerBo customerBo = new CustomerBo();
            CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();

            DataTable dt = customerFamilyBo.GetAllCustomerAssociates(groupCustomerId);
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    AllFolioIds = AllFolioIds + GetCustomerAllPortfolio(Convert.ToInt32(dr["C_AssociateCustomerId"]));

                }
            }
            if (!string.IsNullOrEmpty(AllFolioIds.Trim()))
                AllFolioIds = AllFolioIds.Substring(0, AllFolioIds.Length - 1);

            return AllFolioIds;
        }

      
        private string GetCustomerAllPortfolio(int customerId)
        {
            string portfolioIDs = "";
            PortfolioBo portfolioBo = new PortfolioBo();
            if (!String.IsNullOrEmpty(customerId.ToString())) 
            {
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); 
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) 
                {

                    foreach (CustomerPortfolioVo custPortfolio in customerPortfolioVos)
                    {
                        if (custPortfolio.PortfolioName == "MyPortfolio" || custPortfolio.PortfolioName == "MyPortfolioProspect")
                        {
                            portfolioIDs = portfolioIDs + custPortfolio.PortfolioId;
                            portfolioIDs = portfolioIDs + ",";
                        }
                                           }

                }

            }

            return portfolioIDs;
        }


        private void CalculateDateRange(out DateTime fromDate, out DateTime toDate)
        {
            if (rdoDatePeriod.Checked == true)
            {
                dtBo.CalculateFromToDatesUsingPeriod(ddlEmailDatePeriod.SelectedValue, out fromDate, out toDate);

            }
            else 
            {
                fromDate = Convert.ToDateTime(txtEmailFromDate.SelectedDate);
                toDate = Convert.ToDateTime(txtEmailToDate.SelectedDate);
            }
        }

        private DataTable GetRequestStatusList(int adviserId, DateTime requestedDate)
        {

            dsRequestListStatus = taskRequestManagementBo.GetRequestStatusList(adviserId, requestedDate);
            dtRequestStatusList = PrepareFinalRequestStatsuDataTable(dsRequestListStatus);
            return dtRequestStatusList;

        }
        private DataTable PrepareFinalRequestStatsuDataTable(DataSet dsRequestStatusList)
        {
            DataTable dtRequestList = dsRequestStatusList.Tables[0];
          
            DataTable dtFinalRequestListStatus = new DataTable();
            dtFinalRequestListStatus.Columns.Add("RequestId");
            dtFinalRequestListStatus.Columns.Add("TaskName");
            dtFinalRequestListStatus.Columns.Add("RequeTime");
            dtFinalRequestListStatus.Columns.Add("RequestedBy");
            dtFinalRequestListStatus.Columns.Add("RequestStatus");
            dtFinalRequestListStatus.Columns.Add("AttemptCount");
            dtFinalRequestListStatus.Columns.Add("DependentRequestId");
            dtFinalRequestListStatus.Columns.Add("ParentRequestId");
            dtFinalRequestListStatus.Columns.Add("CreatedOn");

            dtFinalRequestListStatus.Columns.Add("CustomerName");
            dtFinalRequestListStatus.Columns.Add("ReportName");
            dtFinalRequestListStatus.Columns.Add("FromDate");
            dtFinalRequestListStatus.Columns.Add("ToDate");

            dtFinalRequestListStatus.Columns.Add("ExecutionStartTime");
            dtFinalRequestListStatus.Columns.Add("ExecutionEndTime");
            dtFinalRequestListStatus.Columns.Add("Message");
            dtFinalRequestListStatus.Columns.Add("statusYN");

            try
            {

                foreach (DataRow drRequest in dtRequestList.Rows)
                {
                    DataRow drFinalStatus = dtFinalRequestListStatus.NewRow();
                    Int32 requestId = Convert.ToInt32(drRequest["WR_RequestId"].ToString());
                    drFinalStatus["RequestId"] = requestId;
                    drFinalStatus["TaskName"] = drRequest["WT_Task"].ToString();
                    drFinalStatus["RequeTime"] = drRequest["WR_RequestDateTime"].ToString();
                    drFinalStatus["RequestStatus"] = drRequest["WR_Status"].ToString();
                    drFinalStatus["AttemptCount"] = drRequest["WR_AttemptCount"].ToString();
                    drFinalStatus["DependentRequestId"] = drRequest["WR_DependentOn"].ToString();
                    drFinalStatus["ParentRequestId"] = drRequest["WR_ParentRequestId"].ToString();
                    drFinalStatus["CreatedOn"] = drRequest["WR_CreatedOn"].ToString();
                    drFinalStatus["RequestedBy"] = drRequest["WR_CreatedBy"].ToString();


                    #region //-----------------------Changed Region----------------------\\

                    drFinalStatus["ExecutionStartTime"] = drRequest["WRL_ExecuteStartTime"].ToString();
                    drFinalStatus["ExecutionEndTime"] = drRequest["WRL_EndTime"].ToString();
                    drFinalStatus["Message"] = drRequest["WRL_Message"].ToString();
                    #endregion

                    if (drFinalStatus["RequestStatus"].ToString() == "1")
                    {
                        drFinalStatus["statusYN"] = "Yes";
                    }
                    else
                    {
                        drFinalStatus["statusYN"] = "No";
                    }
                    if (drRequest["WT_Task"].ToString() == "Email Generation")
                    {
                        drFinalStatus["CustomerName"] = drRequest["CustomerName_Report_Bulk_Mail_Request"].ToString();
                    }
                    else if (drRequest["WT_Task"].ToString() == "Report Generation")
                    {
                         drFinalStatus["CustomerName"] = drRequest["CustomerName_Report_Generation"].ToString();
                    }

                    drFinalStatus["ReportName"] = drRequest["ReportName"].ToString();
                    drFinalStatus["FromDate"] = drRequest["FromDate"].ToString();
                    drFinalStatus["ToDate"] = drRequest["ToDate"].ToString();

                 

                    dtFinalRequestListStatus.Rows.Add(drFinalStatus);


                }

             
                if (Cache["gvRequestStatus" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("gvRequestStatus" + advisorVo.advisorId, dtFinalRequestListStatus);
                }
                else
                {
                    Cache.Remove("gvRequestStatus" + advisorVo.advisorId);
                    Cache.Insert("gvRequestStatus" + advisorVo.advisorId, dtFinalRequestListStatus);
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
                FunctionInfo.Add("Method", "MFReports.ascx.cs:PrepareFinalRequestStatsuDataTable()");
                object[] objects = new object[1];
                objects[0] = dsRequestStatusList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtFinalRequestListStatus;
        }

        protected void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            if (gvRequestStatus.DataSource != null)
            {

                gvRequestStatus.ExportSettings.OpenInNewWindow = true;
                gvRequestStatus.ExportSettings.IgnorePaging = true;
                gvRequestStatus.ExportSettings.HideStructureColumns = true;
                gvRequestStatus.ExportSettings.ExportOnlyData = true;
                gvRequestStatus.ExportSettings.FileName = "Bulk Mail Status Details";
                gvRequestStatus.MasterTableView.ExportToExcel();
            }
        }
    }
}
