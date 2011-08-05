using System;
using System.Data;
using BoCustomerProfiling;
using BoCommon;
using VoUser;
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
        DateTime dtTo = new DateTime();
        DateTime dtFrom = new DateTime();
        int activeTabIndex = 0;
        AdvisorVo advisorVo = null;
        MFReportVo mfReport = new MFReportVo();
        string reportSubType=string.Empty;
        int reportFlag = 0;
        //For Storing customer Details in to session for Report
        CustomerVo customerVo = new CustomerVo();
        bool isGrpHead = false;
        bool CustomerLogin = false;
        bool strFromCustomerDashBoard = false;

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

                if (userVo.UserType == "Customer" || strFromCustomerDashBoard==true)
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
                    ddlMFTransactionTypeBind();
                    if (CustomerLogin == true)
                    {
                        trCustomerName.Visible = true;
                        trIndCustomer.Visible=false;
                        trGroupCustomer.Visible=false;
                        IndivisulCustomerLogin();

                        trAdvisorRadioList.Visible=false;
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
                        else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                        {

                            dtGroupCustomerList = customerBo.GetAdviserGroupCustomerName("BULKMAIL", int.Parse(advisorVo.advisorId.ToString()));

                        }
            

                        LBCustomer.DataSource = dtGroupCustomerList;
                        LBCustomer.DataTextField = "C_FirstName";
                        LBCustomer.DataValueField = "C_CustomerId";
                        LBCustomer.DataBind();
                    }
                    CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
                    DataSet ds = customerTransactionBo.GetLastTradeDate();
                    if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["WTD_Date"] != null)
                    {
                        txtAsOnDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                        txtFromDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                        txtToDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                        txtEmailAsOnDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                        txtEmailFromDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                        txtEmailToDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                    }
                    //Transaction Subreport search invissible intitialy..
                    //trTranFilter1.Visible = false;
                    //trTranFilter2.Visible = false;
                    tabViewAndEmailReports.ActiveTabIndex = 0;
                    //ShowFolios();
                }
                if (CustomerLogin == false)
                {
                    if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                    {
                        txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                        txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                    {
                        txtCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtParentCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                        txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";

                    }
                }

                if (IsPostBack && !string.IsNullOrEmpty(Request.Form["ctrl_MFReports$hidTabIndex"]))
                {
                    activeTabIndex = Convert.ToInt32(Request.Form["ctrl_MFReports$hidTabIndex"]);
                    tabViewAndEmailReports.ActiveTabIndex = activeTabIndex;
                    
                }
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
                trCustomerDetails.Style.Add("display","block");
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
            else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
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
            else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
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
            String AllCustomerId="";
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
            else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
            {

                dtIndiviCustomerList = customerBo.GetAllCustomerName("BULKMAIL", int.Parse(advisorVo.advisorId.ToString()));

            }


            LBCustomer.DataSource = dtIndiviCustomerList;
            LBCustomer.DataTextField = "C_FirstName";
            LBCustomer.DataValueField = "C_CustomerId";
            LBCustomer.DataBind();
            tabViewAndEmailReports.ActiveTabIndex = activeTabIndex;

        }

        //protected void btnEmailReport_Click(object sender, EventArgs e)
        //{
        //}

    }
}
