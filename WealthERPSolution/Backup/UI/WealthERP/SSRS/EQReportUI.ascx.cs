using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCustomerProfiling;
using BoCommon;
using VoUser;
using BoAdvisorProfiling;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using System.Data;
using System.Configuration;
using WealthERP.Base;
using System.Text;

namespace WealthERP.SSRS
{
    public partial class EQReportUI : System.Web.UI.UserControl
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
        DateTime LatestValuationdate = new DateTime();
        AdvisorMISBo adviserMISBo = new AdvisorMISBo();
        int advisorId;
        CustomerVo customerVo = new CustomerVo();
        AdvisorVo advisorVo = null;
        bool CustomerLogin = false;
        bool strFromCustomerDashBoard = false;
        bool isGrpHead = false;
        DateTime chckdate;
        string UserType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            UserType = Session["UserType"].ToString().Trim();
            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
                rmVo = (RMVo)Session[SessionContents.RmVo];

            if (UserType == "Customer")
            {
                if (!string.IsNullOrEmpty(Session["CustomerVo"].ToString()))
                    customerVo = (CustomerVo)Session["CustomerVo"];
                isGrpHead = customerBo.CheckCustomerGroupHead(customerVo.CustomerId);
                if (isGrpHead == false)
                {
                    trCustomerType.Visible = false;

                }
                else
                {
                    trCustomerType.Visible = true;
                }

                CustomerLogin = true;
                hndCustomerLogin.Value = "true";
                Session["hndCustomerLogin"] = hndCustomerLogin.Value;

                trAdminRmButton.Visible = true;
                trAdminCustomer.Visible = false;
                lblReportStep.Text = customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName;
                trGroupCustomerDetails.Visible = true;
            }
            else
            {
                hndCustomerLogin.Value = "false";
                Session["hndCustomerLogin"] = hndCustomerLogin.Value;

                trAdminRmButton.Visible = true;

            }

            if (!IsPostBack)
            {
                advisorId = advisorVo.advisorId;
              
                if (CustomerLogin == true)
                {

                    trAdminCustomer.Visible = false;
                    hdnCustomerId1.Value = customerVo.CustomerId.ToString();
                    IndivisulCustomerLogin();
                    ShowFolios();

                }
                else
                {

                    trAdminCustomer.Visible = true;
                    trStepGrHead.Visible = true;


                   
                    if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                    {
                        txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString() == "Ops")
                    {
                        txtParentCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                    }
                }

                txtAsOnDate.Text = DateTime.Now.ToShortDateString();
                txtFromDate.Text = DateTime.Now.ToShortDateString(); ;
                txtToDate.Text = DateTime.Now.ToShortDateString(); ;


            }

        }

        private void IndivisulCustomerLogin()
        {
           
            DataTable dt = customerBo.GetCustomerPanAddress(customerVo.CustomerId);
            DataRow dr = dt.Rows[0];
            hdnCustomerId1.Value = customerVo.CustomerId.ToString();
            txtPAN.Text = customerVo.PANNum;
           

        }


        protected void txtParentCustomerId_ValueChanged(object sender, EventArgs e)
        {
            CustomerBo customerBo = new CustomerBo();
            hdnCustomerId1.Value = txtParentCustomerId.Value.ToString();
            customerVo = customerBo.GetCustomer(int.Parse(hdnCustomerId1.Value));
            trGroupCustomerDetails.Visible = true;
            txtPAN.Text = customerVo.PANNum;
            Session["CusVo"] = customerVo;
            if (ddlCustomerType.SelectedValue == "1")
            {
                ShowGroupCustomers();
            }
            else
            {
                ShowFolios();
            }

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

         



        }

      
        protected void ddlGroupPortfolioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlCustomerType.SelectedValue == "1")
            {
                ShowGroupCustomers();

            }
            else
            {
                ShowFolios();

            }


        }
        protected void ddlCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserType != "Customer")
            {
                hdnCustomerId1.Value = "0";
                txtParentCustomer.Text = "";
                txtParentCustomerId.Value = "0";
                ddlGroupPortfolioGroup.SelectedValue = "MANAGED";
                divGroupCustomers.InnerHtml = string.Empty;
                trGroupCustomerDetails.Visible = false;
                txtPAN.Text = "";
                if (ddlCustomerType.SelectedValue == "0")
                {
                    if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                    {
                        txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString() == "Ops")
                    {
                        txtParentCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                    }

                }
                else
                {
                    if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                    {

                        txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString() == "Ops")
                    {
                        txtParentCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";
                    }
                }
            }
            if (UserType == "Customer")
            {
                ddlGroupPortfolioGroup.SelectedValue = "MANAGED";
                divGroupCustomers.InnerHtml = string.Empty;
                if (ddlCustomerType.SelectedValue == "1")
                {

                    ShowGroupCustomers();
                }
                else
                {
                    ShowFolios();
                }
            }

        }

        private void ShowFolios()
        {

            PortfolioBo portfolioBo = new PortfolioBo();

            if (!String.IsNullOrEmpty(hdnCustomerId1.Value)) //Note : customer Id assigned to hdnCustomerId(hidden field) when the user selects customer from customer name suggestion text box
            {
                int customerId = Convert.ToInt32(hdnCustomerId1.Value);
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); //Get all the portfolios of the selected customer.
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) //One or more folios available for selected customer
                {
                    StringBuilder checkbox = new StringBuilder();


                    foreach (CustomerPortfolioVo custPortfolio in customerPortfolioVos)
                    {
                        if (ddlGroupPortfolioGroup.SelectedValue == "MANAGED" && (custPortfolio.IsMainPortfolio != 1))
                        {
                            if (customerPortfolioVos.Count == 1)
                                checkbox.Append("<span class='Error'>No managed portfolios found for this customer.Can't create report.</span>");
                            continue;
                        }
                        else if (ddlGroupPortfolioGroup.SelectedValue == "UN_MANAGED" & (custPortfolio.IsMainPortfolio != 0))
                        {
                            if (customerPortfolioVos.Count == 1)
                                checkbox.Append("<span class='Error'>No unmanaged portfolios found for this customer.Can't create report.</span>");
                            continue;
                        }
                        if (String.IsNullOrEmpty(custPortfolio.PortfolioName))
                            custPortfolio.PortfolioName = "No Name";

                        checkbox.Append("<input type='checkbox' checked name='chk--" + custPortfolio.PortfolioId + "' id='chk--" + custPortfolio.PortfolioId + "'>" + custPortfolio.PortfolioName);
                    }
                    divGroupCustomers.InnerHtml = checkbox.ToString();

                }
                else //No portfolios found for this customer.
                {
                    divGroupCustomers.InnerHtml = "<span class='Error'>No portfolios found for this customer.Can't create report.</span>";
                }
            }
            else // Something went wrong :( customer id is not assigned to the hidden field.
            {
                divGroupCustomers.InnerHtml = "<span class='Error'>Invalid Customer selected.</span>";
            }
          

        }

        private string ShowGroupFolios(int customerId)
        {
            StringBuilder checkbox = new StringBuilder();
            PortfolioBo portfolioBo = new PortfolioBo();
            if (!String.IsNullOrEmpty(hdnCustomerId1.Value)) //Note : customer Id assigned to txtCustomerId(hidden field) when the user selects customer from customer name suggestion text box
            {

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


                    }

                }
                else //No portfolios found for this customer.
                {
                    checkbox.Append("--");
                }

            }
          
            return checkbox.ToString();
        }

    }
}