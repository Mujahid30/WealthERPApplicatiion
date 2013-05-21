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

namespace WealthERP.Reports
{
    public partial class MFReports : System.Web.UI.UserControl
    {

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
        //For Storing customer Details in to session for Report
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
            // When using the ListBox with UpdatePanels, you should disable 
            // partial rendering for FF < v1.5. If you decide not to, reloads
            // (F5) can still cause client / server loss of synchronization.
            // The least you should do is disable page caching for FF < v1.5.
            if (Request.Browser.Browser.Equals("Firefox")
                && Request.Browser.MajorVersion < 2
                && Request.Browser.MinorVersion < 0.5)
            {
                ScriptManager.GetCurrent(Page).EnablePartialRendering = false;
                //Response.Cache.SetNoStore();
            }
        }


        /// <summary>
        /// This will add selected list Items(Customer) From One Lst to Other List. Author:Pramod 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="moveAllItems"></param>

        private void moveSelectedItems(DanLudwig.Controls.Web.ListBox source, DanLudwig.Controls.Web.ListBox target, bool moveAllItems)
        {
            // loop through all source items to find selected ones
            for (int i = source.Items.Count - 1; i >= 0; i--)
            {
                ListItem item = source.Items[i];

                if (moveAllItems)
                    item.Selected = true;

                if (item.Selected)
                {
                    // if the target already contains items, loop through
                    // them to place this new item in correct sorted order
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

                    // if item is still selected, it must be appended
                    if (item.Selected)
                    {
                        target.Items.Add(item);
                        item.Selected = false;
                    }

                    // remove the item from the source list
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
            SessionBo.CheckSession();
            userVo = (UserVo)Session["UserVo"];

            //ddlReportSubType.Attributes.Add("onchange", "ChangeDates()");
            rdoGroup.Attributes.Add("onClick", "javascript:ChangeCustomerSelectionTextBox(value);");
            rdoIndividual.Attributes.Add("onClick", "javascript:ChangeCustomerSelectionTextBox(value);");
            rdoCustomerGroup.Attributes.Add("onClick", "javascript:ChangeGroupOrSelf(value);");
            rdoCustomerIndivisual.Attributes.Add("onClick", "javascript:ChangeGroupOrSelf(value);");

            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];
            // cvAsOnDate.ValueToCompare = DateTime.Now.ToShortDateString();

            //GetRequestStatusList(151586, Convert.ToDateTime("2013-05-08"));

            if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$btnViewReport"] != "View Report" && Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlEmailReports$btnEmailReport"] != "Email Report")
            {
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
                    rmVo = (RMVo)Session[SessionContents.RmVo];

                if (Session["UserType"] != null)
                {
                    if (Session["UserType"].ToString() == "Customer")
                        strFromCustomerDashBoard = true;
                }

                if (Session["UserType"].ToString().Trim() == "Customer" && strFromCustomerDashBoard == true)
                {
                    if (!string.IsNullOrEmpty(Session["CustomerVo"].ToString()))
                        customerVo = (CustomerVo)Session["CustomerVo"];
                    CustomerLogin = true;
                    hndCustomerLogin.Value = "true";
                    Session["hndCustomerLogin"] = hndCustomerLogin.Value;
                    tabpnlEmailReports.Visible = false;

                }
                else
                {
                    hndCustomerLogin.Value = "false";
                    Session["hndCustomerLogin"] = hndCustomerLogin.Value;

                }

                BindPeriodDropDown();

                //if (CustomerLogin == false)
                //{
                //    txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                //    txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                //}
                if (!IsPostBack)
                {
                    lblNote2.Visible = true;
                    ddlMFTransactionTypeBind();
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


                    //tabpnlEmailReports.Visible = false;
                    if (CustomerLogin == false)
                    {
                        //This for Customer Search AutoCompelete TextBox Dynamic Assign Service Method.
                        //if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                        //{
                        //    txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        //    txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        //    txtCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                        //    txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                        //}
                        //else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                        //{
                        //    txtCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        //    txtParentCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        //    txtCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                        //    txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";

                        //}


                        //ListBox horizontal Scorling enabled false
                        LBCustomer.HorizontalScrollEnabled = false;
                        LBSelectCustomer.HorizontalScrollEnabled = false;

                        CustomerBo customerBo = new CustomerBo();
                        DataTable dtGroupCustomerList = new DataTable();
                        //dtGroupCustomerList = customerBo.GetParentCustomerName("BULKMAIL", int.Parse(rmVo.RMId.ToString()));
                        if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                        {
                            dtGroupCustomerList = customerBo.GetParentCustomerName("BULKMAIL", int.Parse(rmVo.RMId.ToString()));
                        }
                        else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString() == "Ops")
                        {
                            tabpnlEmailReports.Visible = true;
                            dtGroupCustomerList = customerBo.GetAdviserGroupCustomerName("BULKMAIL", int.Parse(advisorVo.advisorId.ToString()));

                        }
                        else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                        {
                            tabpnlEmailReports.Visible = false;
                        }


                        LBCustomer.DataSource = dtGroupCustomerList;
                        LBCustomer.DataTextField = "C_FirstName";
                        LBCustomer.DataValueField = "C_CustomerId";
                        LBCustomer.DataBind();
                    }
                    CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
                    DataSet ds = customerTransactionBo.GetLastMFTradeDate();
                    DateTime AsonDate = new DateTime();

                    if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["WTD_Date"] != null)
                    {
                        //AsonDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]);
                        //AsonDate = AsonDate.AddDays(-1);

                        //txtAsOnDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                        ////txtAsOnDate1 = DateTime.Parse(txtAsOnDate.Text.ToString());
                        //txtFromDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                        //txtToDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                        //txtEmailAsOnDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                        //txtEmailFromDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                        //txtEmailToDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                    }
                    //Transaction Subreport search invissible intitialy..
                    //trTranFilter1.Visible = false;
                    //trTranFilter2.Visible = false;
                    tabViewAndEmailReports.ActiveTabIndex = 0;
                    //ShowFolios();
                    advisorId = advisorVo.advisorId;
                    //---------------------------------- Old code to get last Valuation date from History----
                    //LatestValuationdate = adviserMISBo.GetLatestValuationDateFromHistory(advisorId, "MF");
                    //hdnValuationDate.Value = LatestValuationdate.ToString();

                    //---------------------------------- New code to get last Valuation date----------------------
                    if (Session[SessionContents.ValuationDate] == null)
                        GetLatestValuationDate();
                    genDict = (Dictionary<string, DateTime>)Session[SessionContents.ValuationDate];
                    LatestValuationdate = genDict[Constants.MFDate.ToString()];
                    hdnValuationDate.Value = LatestValuationdate.ToString();
                    txtAsOnDate.Text = LatestValuationdate.ToShortDateString();
                    txtFromDate.Text = LatestValuationdate.ToShortDateString();
                    txtToDate.Text = LatestValuationdate.ToShortDateString();
                    txtEmailAsOnDate.Text = LatestValuationdate.ToShortDateString();
                    txtEmailAsOnDate.Text = LatestValuationdate.ToShortDateString();
                    txtEmailFromDate.Text = LatestValuationdate.ToShortDateString();
                    txtEmailToDate.Text = LatestValuationdate.ToShortDateString();

                }
                //if (ddlReportSubType.SelectedValue.ToString() == "RETURNS_PORTFOLIO" || ddlReportSubType.SelectedValue.ToString() == "COMPREHENSIVE" || ddlReportSubType.SelectedValue.ToString() == "CATEGORY_WISE" || ddlReportSubType.SelectedValue.ToString() == "REALIZED_REPORT")
                //{
                //    //LatestValuationdate = adviserMISBo.GetLatestValuationDateFromHistory(advisorId, "MF");
                //    LatestValuationdate = DateTime.Parse(portfolioBo.GetLatestValuationDate(advisorId, "MF").ToString());
                //    hdnValuationDate.Value = LatestValuationdate.ToString();
                //    txtAsOnDate.Text = LatestValuationdate.ToShortDateString();

                //    txtEmailAsOnDate.Text = LatestValuationdate.ToShortDateString();
                //    txtEmailAsOnDate.Text = LatestValuationdate.ToShortDateString();

                //}
                //else
                //{
                //    LatestValuationdate = adviserMISBo.GetLatestValuationDateFromHistory(advisorId, "MF");
                //    hdnValuationDate.Value = LatestValuationdate.ToString();
                //    txtAsOnDate.Text = LatestValuationdate.ToShortDateString();
                //    txtFromDate.Text = LatestValuationdate.ToShortDateString();
                //    txtToDate.Text = LatestValuationdate.ToShortDateString();
                //    txtEmailAsOnDate.Text = LatestValuationdate.ToShortDateString();
                //    txtEmailAsOnDate.Text = LatestValuationdate.ToShortDateString();
                //    txtEmailFromDate.Text = LatestValuationdate.ToShortDateString();
                //    txtEmailToDate.Text = LatestValuationdate.ToShortDateString();
                //}
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
                    tabViewAndEmailReports.ActiveTabIndex = activeTabIndex;

                }
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
                adviserId = advisorVo.advisorId;
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

        protected void rbtnDate_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbtnPickDate.Checked == true)
            //{
            //    trRange.Visible = true;
            //    trPeriod.Visible = false;
            //}
            //else if (rbtnPickPeriod.Checked == true)
            //{
            //    trRange.Visible = false;
            //    trPeriod.Visible = true;
            //    BindPeriodDropDown();
            //}
            ////gvMFTransactions.DataSource = null;
            ////gvMFTransactions.DataBind();
            ////lblCurrentPage.Text = string.Empty;
            ////lblTotalRows.Text = string.Empty;
            ////mypager.Visible = false;
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
                //trCustomerDetails.Visible = true;
                //trPortfolioDetails.Visible = true;
                if (rdoIndividual.Checked)
                    ShowFolios();
                else
                    ShowGroupCustomers();
                //Storing Customer details in session to Access in Display.aspx for passing report parameter
                //CustomerVo customerVo = new CustomerVo();
                customerVo = customerBo.GetCustomer(int.Parse(hdnCustomerId.Value));
                Session["CusVo"] = customerVo;
                txtParentCustomer.Text = customerVo.FirstName.ToString() + customerVo.MiddleName.ToString() + customerVo.LastName.ToString();
                txtCustomer.Text = customerVo.FirstName.ToString() + customerVo.MiddleName.ToString() + customerVo.LastName.ToString();
                hdnCustomerId.Value = "";
            }
            tabViewAndEmailReports.ActiveTab = tabViewAndEmailReports.Tabs[activeTabIndex];
            tabViewAndEmailReports.ActiveTabIndex = 0;
        }
        /// <summary>
        /// Getting Details of Customer for MFReport when Customer Indivisul login. Author:Pramod  
        /// </summary>
        protected void IndivisulCustomerLogin()
        {
            //CustomerBo customerBo = new CustomerBo();
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
            //Storing Customer details in session to Access in Display.aspx for passing report parameter



            tabViewAndEmailReports.ActiveTab = tabViewAndEmailReports.Tabs[activeTabIndex];
            tabViewAndEmailReports.ActiveTabIndex = 0;
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

        /// <summary>
        /// When Customer Indivisua login, then and group report is selected then Show all Customer. Author:Pramod
        /// </summary>
        private void ShowAllCustomer()
        {
            CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
            DataTable dt = customerFamilyBo.GetAllCustomerAssociates(customerVo.CustomerId);
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder strCustomers = new StringBuilder();
                strCustomers.Append("<table border='0'>");

                strCustomers.Append("<tr><td colspan='3'><b>All Customers Under Group Head :</b></td></tr>");
                //strCustomers.Append("<tr><td>Customer Name</td><td>Customer Id</td><td>&nbsp;</td></tr>");

                foreach (DataRow dr in dt.Rows)
                {
                    strCustomers.Append("<tr>");
                    strCustomers.Append("<td>" + dr["CustomerName"].ToString() + "</td>");
                    //strCustomers.Append("<td>" + dr["C_AssociateCustomerId"].ToString() + "</td>");
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
            tabViewAndEmailReports.ActiveTab = tabViewAndEmailReports.Tabs[activeTabIndex];
            tabViewAndEmailReports.ActiveTabIndex = activeTabIndex;
            divPortfolios.InnerHtml = string.Empty;
        }

        /// <summary>
        /// When group report is selected then Show all Customer belong to a Group Head. Author:Pramod
        /// </summary>
        private void ShowGroupCustomers()
        {
            CustomerBo customerBo = new CustomerBo();
            if (hdnCustomerId1.Value != string.Empty)
            {
                CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
                DataTable dt = customerFamilyBo.GetAllCustomerAssociates(int.Parse(hdnCustomerId1.Value));
                if (dt != null && dt.Rows.Count > 0)
                {
                    StringBuilder strCustomers = new StringBuilder();
                    strCustomers.Append("<table border='0'>");

                    strCustomers.Append("<tr><td colspan='3'><b>All Customers Under Group Head :</b></td></tr>");
                    //strCustomers.Append("<tr><td>Customer Name</td><td>Customer Id</td><td>&nbsp;</td></tr>");

                    foreach (DataRow dr in dt.Rows)
                    {
                        strCustomers.Append("<tr>");
                        strCustomers.Append("<td>" + dr["CustomerName"].ToString() + "</td>");
                        //strCustomers.Append("<td>" + dr["C_AssociateCustomerId"].ToString() + "</td>");
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
                //DataRow dr = dt.Rows[0];

                //txtPanParent.Text = dr["C_PANNum"].ToString();
                //trCustomerDetails.Visible = true;
                //trPortfolioDetails.Visible = true;
                //ShowFolios();
            }
            tabViewAndEmailReports.ActiveTab = tabViewAndEmailReports.Tabs[activeTabIndex];
            tabViewAndEmailReports.ActiveTabIndex = activeTabIndex;
            divPortfolios.InnerHtml = string.Empty;
        }

        /// <summary>
        /// Binding Period Dropdown From Xml File
        /// </summary>

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

        /// <summary>
        /// It Creats checkBox dynamically with folio ID and Name on indivisual report generate. 
        /// </summary>

        private void ShowFolios()
        {

            PortfolioBo portfolioBo = new PortfolioBo();
            divPortfolios.InnerHtml = string.Empty;
            if (!String.IsNullOrEmpty(hdnCustomerId1.Value)) //Note : customer Id assigned to hdnCustomerId(hidden field) when the user selects customer from customer name suggestion text box
            {
                int customerId = Convert.ToInt32(hdnCustomerId1.Value);
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); //Get all the portfolios of the selected customer.
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) //One or more folios available for selected customer
                {
                    StringBuilder checkbox = new StringBuilder();

                    //CheckBoxList checkboxList = new CheckBoxList();
                    //checkboxList.RepeatDirection = RepeatDirection.Horizontal;
                    //checkboxList.CssClass = "Field";
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
                        //checkboxList.Items.Add(new ListItem(custPortfolio.PortfolioName, custPortfolio.PortfolioId.ToString()));
                        checkbox.Append("<input type='checkbox' checked name='chk--" + custPortfolio.PortfolioId + "' id='chk--" + custPortfolio.PortfolioId + "'>" + custPortfolio.PortfolioName);
                    }
                    divPortfolios.InnerHtml = checkbox.ToString();
                    //divPortfolios.Controls.Add(checkboxList);
                }
                else //No portfolios found for this customer.
                {
                    divPortfolios.InnerHtml = "<span class='Error'>No portfolios found for this customer.Can't create report.</span>";
                }
            }
            else // Something went wrong :( customer id is not assigned to the hidden field.
            {
                divPortfolios.InnerHtml = "<span class='Error'>Invalid Customer selected.</span>";
            }
            divGroupCustomers.InnerHtml = string.Empty;
        }
        /// <summary>
        /// It Creats checkBox dynamically with folio ID and Name on Group report generate. 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private string ShowGroupFolios(int customerId)
        {
            StringBuilder checkbox = new StringBuilder();
            PortfolioBo portfolioBo = new PortfolioBo();
            if (!String.IsNullOrEmpty(hdnCustomerId1.Value)) //Note : customer Id assigned to txtCustomerId(hidden field) when the user selects customer from customer name suggestion text box
            {
                //int customerId = Convert.ToInt32(txtParentCustomerId.Value);
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); //Get all the portfolios of the selected customer.
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) //One or more folios available for selected customer
                {
                    // CheckBoxList checkboxList = new CheckBoxList();
                    // checkboxList.RepeatDirection = RepeatDirection.Horizontal;
                    //checkboxList.CssClass = "Field";
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
                        //checkboxList.Items.Add(new ListItem(custPortfolio.PortfolioName, custPortfolio.PortfolioId.ToString()));
                    }
                    //control.Controls.Add(checkboxList);
                }
                else //No portfolios found for this customer.
                {
                    checkbox.Append("--");
                }

            }
            else // Something went wrong :( customer id is not assigned to the hidden field.
            {
                divPortfolios.InnerHtml = "<span class='Error'>Invalid Customer selected.</span>";
            }
            return checkbox.ToString();
        }

        /// <summary>
        /// Create's CheckBox Dynamically  with name of PortfolioID. :Author Pramod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private string ShowGroupFolioCustomerlogin(int customerId)
        {
            StringBuilder checkbox = new StringBuilder();
            PortfolioBo portfolioBo = new PortfolioBo();
            if (!String.IsNullOrEmpty(customerVo.CustomerId.ToString())) //Note : customer Id assigned to txtCustomerId(hidden field) when the user selects customer from customer name suggestion text box
            {
                //int customerId = Convert.ToInt32(txtParentCustomerId.Value);
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); //Get all the portfolios of the selected customer.
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) //One or more folios available for selected customer
                {
                    // CheckBoxList checkboxList = new CheckBoxList();
                    // checkboxList.RepeatDirection = RepeatDirection.Horizontal;
                    //checkboxList.CssClass = "Field";
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
                        //checkboxList.Items.Add(new ListItem(custPortfolio.PortfolioName, custPortfolio.PortfolioId.ToString()));
                    }
                    //control.Controls.Add(checkboxList);
                }
                else //No portfolios found for this customer.
                {
                    checkbox.Append("--");
                }

            }
            else // Something went wrong :( customer id is not assigned to the hidden field.
            {
                divPortfolios.InnerHtml = "<span class='Error'>Invalid Customer selected.</span>";
            }
            return checkbox.ToString();

        }

        /* protected void ddlReportSubType_SelectedIndexChanged(object sender, EventArgs e)
         {
             if (ddlReportSubType.SelectedValue == "TRANSACTION_REPORT")
             {
                 MFReportsBo mfReportBo = new MFReportsBo();
                 DataSet ds=new DataSet();
                 ds = mfReportBo.GetMFTransactionType();
                 trTranFilter1.Visible = true;
                 trTranFilter2.Visible = true;
                 ddlMFTransactionType.DataSource = ds;
                 ddlMFTransactionType.DataValueField = "TransCode";
                 ddlMFTransactionType.DataTextField = "TransName";
                 ddlMFTransactionType.DataBind();
                 ddlMFTransactionType.Items.Insert(0, new ListItem("ALL", "0"));
                 ddlMFTransactionType.SelectedIndex = 0;
                 rddate.Checked = true;
             }
             else 
             {
                 if (trTranFilter1.Visible == true)
                     trTranFilter1.Visible = false;
                 if (trTranFilter2.Visible == true)
                     trTranFilter2.Visible = false;
              }
             tabViewAndEmailReports.ActiveTabIndex = activeTabIndex;
            
         }*/

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
            LBSelectCustomer.Items.Clear();
            LBCustomer.Items.Clear();

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


            LBCustomer.DataSource = dtGroupCustomerList;
            LBCustomer.DataTextField = "C_FirstName";
            LBCustomer.DataValueField = "C_CustomerId";
            LBCustomer.DataBind();
            tabViewAndEmailReports.ActiveTabIndex = activeTabIndex;

        }

        protected void rbnIndivisual_CheckedChanged(object sender, EventArgs e)
        {
            LBSelectCustomer.Items.Clear();
            LBCustomer.Items.Clear();
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


            LBCustomer.DataSource = dtIndiviCustomerList;
            LBCustomer.DataTextField = "C_FirstName";
            LBCustomer.DataValueField = "C_CustomerId";
            LBCustomer.DataBind();
            tabViewAndEmailReports.ActiveTabIndex = activeTabIndex;
        }


        /// <summary>
        /// Getting All Customer ID From ListBox containing all customers. Author:Pramod
        /// </summary>
        /// <param name="CustomerSelectedListBox"></param>
        /// <returns></returns>
        private string GetAllSelectedCustomerID(DanLudwig.Controls.Web.ListBox CustomerSelectedListBox)
        {
            String AllCustomerId = "";
            // loop through all source items to find selected ones
            for (int i = CustomerSelectedListBox.Items.Count - 1; i >= 0; i--)
            {
                ListItem TempItem = CustomerSelectedListBox.Items[i];

                AllCustomerId = AllCustomerId + "," + TempItem.Value.ToString();


            }
            return AllCustomerId;
        }



        protected void AddSelected_Click(object sender, EventArgs e)
        {

            this.moveSelectedItems(LBCustomer, LBSelectCustomer, false);
            SelectLastItem(LBSelectCustomer);
        }

        protected void RemoveSelected_Click(object sender, EventArgs e)
        {
            this.moveSelectedItems(LBSelectCustomer, LBCustomer, false);
            SelectLastItem(LBSelectCustomer);
        }

        protected void SelectAll_Click(object sender, EventArgs e)
        {
            this.moveSelectedItems(LBCustomer, LBSelectCustomer, true);
            LBSelectCustomer.Items[0].Selected = true;

        }
        protected void RemoveAll_Click(object sender, EventArgs e)
        {
            this.moveSelectedItems(LBSelectCustomer, LBCustomer, true);
        }

        protected void rbnAllCustomer_CheckedChanged(object sender, EventArgs e)
        {
            LBSelectCustomer.Items.Clear();
            LBCustomer.Items.Clear();
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


            LBCustomer.DataSource = dtIndiviCustomerList;
            LBCustomer.DataTextField = "C_FirstName";
            LBCustomer.DataValueField = "C_CustomerId";
            LBCustomer.DataBind();
            tabViewAndEmailReports.ActiveTabIndex = activeTabIndex;

        }

        protected void btnEmailReport_Click(object sender, EventArgs e)
        {
            String allCustomerId = SelectedCustomets4Email.Value;
            CustomerVo custVo = new CustomerVo();
            AdvisorStaffBo adviserStaffBo = new AdvisorStaffBo();
            RMVo customerRMVo = new RMVo();
            char[] separator = new char[] { ',' };
            int customerId = 0;
            string[] strSplitArr = allCustomerId.Split(separator);
            //bool isForGroupCustomer = false;
            int groupCustomerId = 0;
            int parentrequestId = 0;
            List<MFReportVo> mfReportVoList = new List<MFReportVo>();
            MFReportEmailVo mfReportEmailVo = new MFReportEmailVo();
            DateTime fromDateRangeRpt;
            DateTime toDateRangeRpt;

            CalculateDateRange(out fromDateRangeRpt, out toDateRangeRpt);


            foreach (string arrStr in strSplitArr)
            {
                if (!String.IsNullOrEmpty(arrStr))
                {
                    customerId = int.Parse(arrStr);
                    taskRequestManagementBo.CreateTaskRequest(1, userVo.UserId, out parentrequestId);
                    //If Group Customer radio Button is selected then assign group HeadId Else GroupCustomer FLAG Make false 
                    if (rbnGroup.Checked == true)
                    {
                        groupCustomerId = int.Parse(arrStr);
                    }

                    custVo = customerBo.GetCustomer(customerId);
                    customerRMVo = adviserStaffBo.GetAdvisorStaffDetails(custVo.RmId);
                    foreach (ListItem chkItems in chkAsOnReportList.Items)
                    {
                        if (chkItems.Selected == true)
                        {
                            mfReportVoList.Add(GetReportInputData(chkItems, customerId, groupCustomerId, ref fromDateRangeRpt, ref toDateRangeRpt, "ASON"));
                        }

                    }

                    foreach (ListItem chkItems in chkRangeReportList.Items)
                    {
                        if (chkItems.Selected == true)
                        {
                            mfReportVoList.Add(GetReportInputData(chkItems, customerId, groupCustomerId, ref fromDateRangeRpt, ref toDateRangeRpt, "RANGE"));
                        }

                    }

                    mfReportEmailVo.AdviserId = advisorVo.advisorId;
                    mfReportEmailVo.CustomerId = custVo.CustomerId;
                    mfReportEmailVo.CustomerEmail = custVo.Email;
                    mfReportEmailVo.RMEmail = customerRMVo.Email;
                    mfReportEmailVo.ReportTypeName = "Mutual Fund";

                    taskRequestManagementBo.CreateBulkReportRequest(mfReportVoList, mfReportEmailVo, parentrequestId, 1, userVo.UserId);

                }
            }



        }

        //protected void btnEmailReport_Click(object sender, EventArgs e)
        //{
        //}



        private MFReportVo GetReportInputData(ListItem chkItems, int customerId, int groupCustomerId, ref DateTime dtFrom, ref DateTime dtTo, string reportDateType)
        {

            MFReportVo mfReportVo = new MFReportVo();
            mfReport.ReportName = chkItems.Value.Trim();
            if (reportDateType == "ASON")
            {
                mfReport.FromDate = Convert.ToDateTime(txtEmailAsOnDate.Text.Trim());
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



        /// <summary>
        /// This Returns all portfolio Id of all customers of One Group Head Author:Pramod
        /// </summary>
        /// <returns></returns>

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

        /// <summary>
        /// This Returns all portfolio Id of a particular customer. Author:Pramod
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private string GetCustomerAllPortfolio(int customerId)
        {
            string portfolioIDs = "";
            PortfolioBo portfolioBo = new PortfolioBo();
            if (!String.IsNullOrEmpty(customerId.ToString())) //Note : customer Id assigned to txtCustomerId(hidden field) when the user selects customer from customer name suggestion text box
            {
                //int customerId = Convert.ToInt32(txtParentCustomerId.Value);
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); //Get all the portfolios of the selected customer.
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) //One or more folios available for selected customer
                {

                    foreach (CustomerPortfolioVo custPortfolio in customerPortfolioVos)
                    {
                        if (custPortfolio.PortfolioName == "MyPortfolio" || custPortfolio.PortfolioName == "MyPortfolioProspect")
                        {
                            portfolioIDs = portfolioIDs + custPortfolio.PortfolioId;
                            portfolioIDs = portfolioIDs + ",";
                        }
                        //checkbox.Append("<input type='checkbox' checked name='chk--" + custPortfolio.PortfolioId + "' id='chk--" + custPortfolio.PortfolioId + "'>" + custPortfolio.PortfolioName);
                        //checkboxList.Items.Add(new ListItem(custPortfolio.PortfolioName, custPortfolio.PortfolioId.ToString()));
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
            else //if (Request.Form[ctrlPrefix + "hidDateType"] == "AS_ON")
            {
                fromDate = Convert.ToDateTime(txtEmailFromDate.Text);
                toDate = Convert.ToDateTime(txtEmailToDate.Text);
            }
        }

        private DataTable GetRequestStatusList(int userId,DateTime requestedDate)
        {
            dsRequestListStatus = taskRequestManagementBo.GetRequestStatusList(userId, requestedDate);
            dtRequestStatusList = PrepareFinalRequestStatsuDataTable(dsRequestListStatus);
            return dtRequestStatusList;

        }
        private DataTable PrepareFinalRequestStatsuDataTable(DataSet dsRequestStatusList)
        {
            DataTable dtRequestList = dsRequestStatusList.Tables[0];
            DataTable dtRequestInputParameterList = dsRequestStatusList.Tables[1];
            DataTable dtRequestLog = dsRequestStatusList.Tables[2];
            DataTable dtFinalRequestListStatus = new DataTable();
            dtFinalRequestListStatus.Columns.Add("RequestId");
            dtFinalRequestListStatus.Columns.Add("TaskName");
            dtFinalRequestListStatus.Columns.Add("RequeTime");
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
                    DataView dvRequestInputParameter = new DataView(dtRequestInputParameterList, "WR_RequestId='" + requestId.ToString() + "'", "WR_RequestId", DataViewRowState.CurrentRows);
                    DataView dvRequestLog = new DataView(dtRequestLog, "WR_RequestId='" + requestId.ToString() + "'", "WR_RequestId", DataViewRowState.CurrentRows);

                    foreach (DataRow drParameter in dvRequestInputParameter.Table.Rows)
                    {
                        switch (Convert.ToInt32(drParameter["WTP_Id"].ToString()))
                        {
                            case 1006:
                                //drFinalStatus["CustomerName"] = drRequest["WRD_InputParameterValue"].ToString();
                                drFinalStatus["CustomerName"] = drParameter["WRD_InputParameterValue"].ToString();
                                break;
                            case 1009:
                                //drFinalStatus["CustomerName"] = drRequest["WRD_InputParameterValue"].ToString();
                                drFinalStatus["CustomerName"] = drParameter["WRD_InputParameterValue"].ToString();
                                break;
                            case 1000:
                                //drFinalStatus["ReportName"] = drRequest["WRD_InputParameterValue"].ToString();
                                drFinalStatus["ReportName"] = drParameter["WRD_InputParameterValue"].ToString();
                                break;
                            case 1001:
                                //drFinalStatus["FromDate"] = drRequest["WRD_InputParameterValue"].ToString();
                                drFinalStatus["FromDate"] = drParameter["WRD_InputParameterValue"].ToString();
                                break;
                            case 1002:
                                //drFinalStatus["ToDate"] = drRequest["WRD_InputParameterValue"].ToString();
                                drFinalStatus["ToDate"] = drParameter["WRD_InputParameterValue"].ToString();
                                break;

                        }

                    }

                    if (dvRequestLog.Table.Rows.Count > 0)
                    {
                        foreach (DataRow drlog in dvRequestLog.Table.Rows)
                        {
                            //drFinalStatus["ExecutionStartTime"] = drRequest["WRL_ExecuteStartTime"].ToString();
                            //drFinalStatus["ExecutionEndTime"] = drRequest["WRL_EndTime"].ToString();
                            //drFinalStatus["Message"] = drRequest["WRL_Message"].ToString();

                            drFinalStatus["ExecutionStartTime"] = drlog["WRL_ExecuteStartTime"].ToString();
                            drFinalStatus["ExecutionEndTime"] = drlog["WRL_EndTime"].ToString();
                            drFinalStatus["Message"] = drlog["WRL_Message"].ToString();
                        }
                    }
                    else
                    {
                        drFinalStatus["ExecutionStartTime"] = String.Empty;
                        drFinalStatus["ExecutionEndTime"] = String.Empty;
                        drFinalStatus["Message"] = String.Empty;

                    }

                    dtFinalRequestListStatus.Rows.Add(drFinalStatus);
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
            if (Cache["gvRequestStatus" + advisorVo.advisorId] == null)
            {
                Cache.Insert("gvRequestStatus" + advisorVo.advisorId, dtFinalRequestListStatus);
            }
            else
            {
                Cache.Remove("gvRequestStatus" + advisorVo.advisorId);
                Cache.Insert("gvRequestStatus" + advisorVo.advisorId, dtFinalRequestListStatus);
            }

            if (dtFinalRequestListStatus.Rows.Count!=0)
            {
                btnExportFilteredData.Visible = true;
                divGvRequestStatus.Visible = true;
            }
            else
            {
                btnExportFilteredData.Visible = false;
                divGvRequestStatus.Visible = true;
            }

            return dtFinalRequestListStatus;

           
        }


        protected void btnShowRequestStausGrid_Click(object sender, EventArgs e)
        {
            BindGvRequestStatus();
            gvRequestStatus.DataSource=dtRequestStatusList;
            gvRequestStatus.DataBind();
        }

        public void BindGvRequestStatus()
        {
            //GetRequestStatusList(151586, Convert.ToDateTime("2013-05-08"));
            GetRequestStatusList(userVo.UserId, Convert.ToDateTime(rdpShowRequestStausGrid.SelectedDate));
        }

        protected void gvRequestStatus_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGvSchemeDetails = new DataTable();
            dtGvSchemeDetails = (DataTable)Cache["gvRequestStatus" + advisorVo.advisorId];
            gvRequestStatus.DataSource = dtGvSchemeDetails;
        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            DataSet dtGvSchemeDetails = new DataSet();
            dtGvSchemeDetails = (DataSet)Cache["gvRequestStatus" + advisorVo.advisorId];
            gvRequestStatus.DataSource = dtGvSchemeDetails;

            gvRequestStatus.ExportSettings.OpenInNewWindow = true;
            gvRequestStatus.ExportSettings.IgnorePaging = true;
            gvRequestStatus.ExportSettings.HideStructureColumns = true;
            gvRequestStatus.ExportSettings.ExportOnlyData = true;
            gvRequestStatus.ExportSettings.FileName = "RequestStatus Details";
            gvRequestStatus.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvRequestStatus.MasterTableView.ExportToExcel();
        }

    }
}
