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
    public partial class PortfolioReports : System.Web.UI.UserControl
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
        AdvisorVo advisorVo = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
                rmVo = (RMVo)Session[SessionContents.RmVo];
            if (Request.Form["ctrl_PortfolioReports$btnView"] != "View Report")
            {                
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());             

                BindPeriodDropDown();
                //txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                //txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();

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

                if (!IsPostBack)
                {
                    CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
                    DataSet ds = customerTransactionBo.GetLastTradeDate();
                    if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["WTD_Date"] != null)
                    {
                        txtAsOnDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                        txtFromDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                        txtToDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WTD_Date"]).ToShortDateString();
                    }
                }

                if (IsPostBack && !string.IsNullOrEmpty(Request.Form["ctrl_PortfolioReports$hidTabIndex"]))
                {
                    activeTabIndex = Convert.ToInt32(Request.Form["ctrl_PortfolioReports$hidTabIndex"]);
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
           
        }
        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            CustomerBo customerBo = new CustomerBo();
            if (txtCustomerId.Value != string.Empty)
            {
                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtCustomerId.Value));
                DataRow dr = dt.Rows[0];

                customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                Session["CusVo"] = customerVo;

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
            CustomerBo customerBo = new CustomerBo();

            customerVo = customerBo.GetCustomer(int.Parse(txtParentCustomerId.Value.ToString()));
            Session["CusVo"] = customerVo;
            ShowGroupCustomers();

        }
        private void ShowGroupCustomers()
        {
            CustomerBo customerBo = new CustomerBo();
            if (txtParentCustomerId.Value != string.Empty)
            {
                CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
                DataTable dt = customerFamilyBo.GetAllCustomerAssociates(int.Parse(txtParentCustomerId.Value));
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
            divPortfolios.InnerHtml = string.Empty;
            if (!String.IsNullOrEmpty(txtCustomerId.Value)) //Note : customer Id assigned to txtCustomerId(hidden field) when the user selects customer from customer name suggestion text box
            {
                int customerId = Convert.ToInt32(txtCustomerId.Value);
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); //Get all the portfolios of the selected customer.
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) //One or more folios available for selected customer
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
            if (!String.IsNullOrEmpty(txtParentCustomerId.Value)) //Note : customer Id assigned to txtCustomerId(hidden field) when the user selects customer from customer name suggestion text box
            {
                //int customerId = Convert.ToInt32(txtParentCustomerId.Value);
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); //Get all the portfolios of the selected customer.
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) //One or more folios available for selected customer
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

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {

        }
    }
}