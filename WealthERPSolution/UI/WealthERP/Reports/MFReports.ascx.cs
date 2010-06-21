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
using CrystalDecisions.CrystalReports.Engine;

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
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            // cvAsOnDate.ValueToCompare = DateTime.Now.ToShortDateString();
            if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$btnViewReport"] != "View Report" && Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlEmailReports$btnEmailReport"] != "Email Report")
            {
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                rmVo = (RMVo)Session[SessionContents.RmVo];

                BindPeriodDropDown();
                txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();

                if (!IsPostBack)
                {
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
                    tabViewAndEmailReports.ActiveTabIndex = 0;
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
                customerVo = customerBo.GetCustomer(int.Parse(hdnCustomerId.Value));
                Session["CusVo"] = customerVo;

                hdnCustomerId.Value = "";
            }
            tabViewAndEmailReports.ActiveTab = tabViewAndEmailReports.Tabs[activeTabIndex];
            tabViewAndEmailReports.ActiveTabIndex = activeTabIndex;
        }

        //protected void txtParentCustomerId_ValueChanged(object sender, EventArgs e)
        //{

        //    ShowGroupCustomers();

        //}

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
            if (rdoIndividual.Checked)
                ShowFolios();
            else
                ShowGroupCustomers();
        }

        //protected void ddlGroupPortfolioGroup_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ShowGroupCustomers();
        //}

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
    }
}