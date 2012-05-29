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

namespace WealthERP.Reports
{
    public partial class EquityReports : System.Web.UI.UserControl
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
        CustomerVo customerVo = new CustomerVo();
        AdvisorVo advisorVo;
        bool CustomerLogin = false;
        bool strFromCustomerDashBoard = false;
        bool isGrpHead = false;

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            userVo = (UserVo)Session["UserVo"];
            

            if (Request.Form["ctrl_EquityReports$btnView"] != "View Report")
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
                    isGrpHead = customerBo.CheckCustomerGroupHead(customerVo.CustomerId);
                    if (isGrpHead == false)
                    {
                        TabPanel1.Visible = false;
                        TabPanel2.Visible = true;
                    }
                    else
                    {
                        TabPanel1.Visible = true;
                        TabPanel2.Visible = true;
                    }

                    CustomerLogin = true;
                    hndCustomerLogin.Value = "true";
                    Session["hndCustomerLogin"] = hndCustomerLogin.Value;
                    trCustomerButton.Visible = true;
                    trAdminRmButton.Visible = false;
                }
                else
                {
                    hndCustomerLogin.Value = "false";
                    Session["hndCustomerLogin"] = hndCustomerLogin.Value;
                    trCustomerButton.Visible = false;
                    trAdminRmButton.Visible = true;
                    
                }

                BindPeriodDropDown();
                //txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                //txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();

                if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                    advisorVo = (AdvisorVo)Session["advisorVo"]; 

                if (!IsPostBack)
                {
                    CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
                    DataSet ds = customerTransactionBo.GetLastTradeDate();
                    if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["WTD_Date"] != null)
                        txtAsOnDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();

                    if (CustomerLogin == true)
                    {
                        trCustomerGrHead.Visible = true;
                        trAdminCustomer.Visible = false;
                        trAdminIndiCustomer.Visible = false;
                        trCustomerInd.Visible = true;
                        trStepIndi.Visible = false;
                        trStepGrHead.Visible = false;
                        IndivisulCustomerLogin();
                        ShowGroupCustomers();
                        
                    }
                    else
                    {
                        trCustomerGrHead.Visible = false;
                        trAdminCustomer.Visible = true;
                        trAdminIndiCustomer.Visible = true;
                        trCustomerInd.Visible = false;
                        trStepIndi.Visible = true;
                        trStepGrHead.Visible = true;
                        

                        //This for Customer Search AutoCompelete TextBox Dynamic Assign Service Method.
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
                    
                    
                }

                if (IsPostBack && !string.IsNullOrEmpty(Request.Form["ctrl_EquityReports$hidTabIndex"]))
                {
                    activeTabIndex = Convert.ToInt32(Request.Form["ctrl_EquityReports$hidTabIndex"]);
                }



            }


        }
        protected void rbtnDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPickDate.Checked == true)
            {
                trRange.Visible = true;
                trPeriod.Visible = false;
            }
            else if (rbtnPickPeriod.Checked == true)
            {
                trRange.Visible = false;
                trPeriod.Visible = true;
                BindPeriodDropDown();
            }
            //gvMFTransactions.DataSource = null;
            //gvMFTransactions.DataBind();
            //lblCurrentPage.Text = string.Empty;
            //lblTotalRows.Text = string.Empty;
            //mypager.Visible = false;
        }
        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
           
            if (txtCustomerId.Value != string.Empty)
            {
                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtCustomerId.Value));
                DataRow dr = dt.Rows[0];
                customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                Session["customerVo"] = customerVo;
                txtPanParent.Text = dr["C_PANNum"].ToString();
                trCustomerDetails.Visible = true;
                trPortfolioDetails.Visible = true;
                ShowFolios();
            }
            TabContainer1.ActiveTab = TabContainer1.Tabs[activeTabIndex];
            TabContainer1.ActiveTabIndex = activeTabIndex;

        }
        protected void txtParentCustomerId_ValueChanged(object sender, EventArgs e)
        {

            customerVo = customerBo.GetCustomer(int.Parse(txtParentCustomerId.Value));
            Session["customerVo"] = customerVo;
            ShowGroupCustomers();

        }
        private void ShowGroupCustomers()
        {
            CustomerBo customerBo = new CustomerBo();
            if (txtParentCustomerId.Value != string.Empty || Session["UserType"].ToString() == "Customer")
            {
                CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
               // DataTable dt = customerFamilyBo.GetAllCustomerAssociates(int.Parse(txtParentCustomerId.Value));
                if (CustomerLogin == true)
                {
                    lblCustomerGrHead.Text = customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName;
                }
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
                        strCustomers.Append("<td>&nbsp;</td>");
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
            TabContainer1.ActiveTab = TabContainer1.Tabs[activeTabIndex];
            TabContainer1.ActiveTabIndex = activeTabIndex;
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
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            //CalculateDateRange();
            //Page.RegisterStartupScript("startup","<script>alert('robinthomas')<script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "startup", "<script>window.open('Reports/Display.aspx')</script>");
        }

        protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPortfolioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowFolios();
        }
        protected void ddlGroupPortfolioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGroupCustomers();
        }

        private void ShowFolios()
        {

            PortfolioBo portfolioBo = new PortfolioBo();
            divPortfolios.InnerHtml = string.Empty;
            if (!String.IsNullOrEmpty(txtCustomerId.Value) || Session["UserType"].ToString() == "Customer") //Note : customer Id assigned to txtCustomerId(hidden field) when the user selects customer from customer name suggestion text box
            {
                int customerId = 0;
                if (CustomerLogin == true)
                     customerId = customerVo.CustomerId;
                else
                    customerId = Convert.ToInt32(txtCustomerId.Value);
                 List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId);
               
                //Get all the portfolios of the selected customer.
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
        }

        private string ShowGroupFolios(int customerId)
        {
            StringBuilder checkbox = new StringBuilder();
            PortfolioBo portfolioBo = new PortfolioBo();
            if (!String.IsNullOrEmpty(txtParentCustomerId.Value) || Session["UserType"].ToString() == "Customer") //Note : customer Id assigned to txtCustomerId(hidden field) when the user selects customer from customer name suggestion text box
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
                        if (ddlGroupPortfolioGroup.SelectedValue == "MANAGED" && (custPortfolio.IsMainPortfolio != 1))
                        {
                            if (customerPortfolioVos.Count == 1)
                                checkbox.Append("<span class='Error'>No portfolio</span>");
                            continue;
                        }
                        else if (ddlGroupPortfolioGroup.SelectedValue == "UN_MANAGED" & (custPortfolio.IsMainPortfolio != 0))
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

        //protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        //{

        //}
        protected void IndivisulCustomerLogin()
        {
            //CustomerBo customerBo = new CustomerBo();
            lblCustomerIndi.Text = customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName;
            DataTable dt = customerBo.GetCustomerPanAddress(customerVo.CustomerId);
            DataRow dr = dt.Rows[0];
            hdnCustomerId1.Value = customerVo.CustomerId.ToString();
            txtPanParent.Text = dr["C_PANNum"].ToString();
            trCustomerDetails.Style.Add("display", "block");
            ShowFolios();
            trCustomerDetails.Visible = true;
            trPortfolioDetails.Visible = true;
            TabContainer1.ActiveTab = TabContainer1.Tabs[activeTabIndex];
            TabContainer1.ActiveTabIndex = activeTabIndex;
        }



    }
}