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
        DateTime chckdate;

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
                        if (!IsPostBack)
                        {
                            HdnIsGroupHeadReport.Value = "1";
                        }
                    }
                    else
                    {
                        TabPanel1.Visible = true;
                        TabPanel2.Visible = true;
                        if (!IsPostBack)
                        {
                            HdnIsGroupHeadReport.Value = "0";
                        }
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

                if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                    advisorVo = (AdvisorVo)Session["advisorVo"];

                if (!IsPostBack)
                {
                    txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
                    DataSet ds = customerTransactionBo.GetLastTradeDate();
                    if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["WTD_Date"] != null)
                        txtAsOnDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                    txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    if (CustomerLogin == true)
                    {
                        trCustomerGrHead.Visible = true;
                        trAdminCustomer.Visible = false;
                        trAdminIndiCustomer.Visible = false;
                        trCustomerInd.Visible = true;
                        trStepIndi.Visible = false;
                        trStepGrHead.Visible = false;
                        hdnCustomerId1.Value = customerVo.CustomerId.ToString();
                        ShowGroupCustomers();
                        IndivisulCustomerLogin();

                    }
                    else
                    {
                        trCustomerGrHead.Visible = false;
                        trAdminCustomer.Visible = true;
                        trAdminIndiCustomer.Visible = true;
                        trCustomerInd.Visible = false;
                        trStepIndi.Visible = true;
                        trStepGrHead.Visible = true;


                        if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                        {
                            txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                            txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                            txtCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                            txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                        }
                        else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString() == "Ops")
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
        public void ChckBussDate_Textchanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtAsOnDate.Text))
            {
                chckdate = DateTime.Parse(txtAsOnDate.Text);
                ValidateBussDate(chckdate);
            }
        }
        public void ChckBussFromDate_Textchanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtFromDate.Text))
            {
                chckdate = DateTime.Parse(txtFromDate.Text);
                ValidateBussDate(chckdate);
               
            }
          
        }
        public void ChckBussToDate_Textchanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtToDate.Text))
            {
                chckdate = DateTime.Parse(txtToDate.Text);
                ValidateBussDate(chckdate);
            }
          
        }
        public void ValidateBussDate( DateTime chckdate)
        {
            CustomerBo customerBo = new CustomerBo();
            bool isCorrect = false;
             isCorrect = customerBo.ChckBussinessDate(chckdate);
            if (isCorrect == true)
            {
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Invalid!!!Choose a Valid Bussiness Date ');", true);

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
        }
        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {

            if (txtCustomerId.Value != string.Empty)
            {
                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtCustomerId.Value));
                DataRow dr = dt.Rows[0];
                hdnCustomerId1.Value = txtCustomerId.Value.ToString();
                customerVo = customerBo.GetCustomer(int.Parse(hdnCustomerId1.Value));
                Session["customerVo"] = customerVo;
                txtPanParent.Text = dr["C_PANNum"].ToString();
                trCustomerDetails.Visible = true;
                trPortfolioDetails.Visible = true;              
                ShowFolios();
               
            }
            txtParentCustomerId.Value = null;
            txtParentCustomer.Text = string.Empty;            
            TabContainer1.ActiveTab = TabContainer1.Tabs[activeTabIndex];
            TabContainer1.ActiveTabIndex = activeTabIndex;
            divGroupCustomers.InnerHtml = string.Empty;
            

        }
        protected void txtParentCustomerId_ValueChanged(object sender, EventArgs e)
        {
            hdnCustomerId1.Value = txtParentCustomerId.Value.ToString();
            customerVo = customerBo.GetCustomer(int.Parse(hdnCustomerId1.Value));
            Session["customerVo"] = customerVo;
            ShowGroupCustomers();

        }
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
            txtCustomerId.Value = null;
            txtCustomer.Text = String.Empty;
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
            if (!String.IsNullOrEmpty(hdnCustomerId1.Value)) 
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
            
        }

        private string ShowGroupFolios(int customerId)
        {
            StringBuilder checkbox = new StringBuilder();
            PortfolioBo portfolioBo = new PortfolioBo();
            if (!String.IsNullOrEmpty(hdnCustomerId1.Value)) 
            {
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); 
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) 
                {

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

       
        protected void IndivisulCustomerLogin()
        {
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