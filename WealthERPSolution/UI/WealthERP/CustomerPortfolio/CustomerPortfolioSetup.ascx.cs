using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using System.Configuration;
using System.Data;
using WealthERP.Base;
using VoUser;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoCustomerPortfolio;

namespace WealthERP.CustomerPortfolio
{
    public partial class CustomerPortfolioSetup : System.Web.UI.UserControl
    {
        string path;
        int customerId;
        int associateId;
        int portfolioId;
        AdvisorVo advisorVo = new AdvisorVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerBo customerBo = new CustomerBo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        CustomerPortfolioVo newCustomerPortfolioVo;
        DataSet dsPortfolioType = new DataSet();
        DataTable dtPortfolioType = new DataTable();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["UserVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();

            if (!IsPostBack)
            {
                BindPortfolioTypeDropDown();
                if (Request.QueryString["action"] == "EditCustomerPortfolio")
                {
                    btnSave.Visible = false;
                    portfolioId = int.Parse(Session["PortfolioId"].ToString());
                    dsPortfolioType = portfolioBo.GetCustomerPortfolioDetails(portfolioId);
                    dtPortfolioType = dsPortfolioType.Tables[0];

                    txtCustomer.Text = dtPortfolioType.Rows[0]["C_FirstName"].ToString();
                    txtCustomerId.Value = dtPortfolioType.Rows[0]["C_CustomerId"].ToString();

                    txtPanCustomer.Text = dtPortfolioType.Rows[0]["C_PANNum"].ToString();
                    txtAddressCustomer.Text = dtPortfolioType.Rows[0]["C_Adr1Line1"].ToString();

                    txtPortfolioName.Text = dtPortfolioType.Rows[0]["CP_PortfolioName"].ToString();
                    txtPMSIdentifier.Text = dtPortfolioType.Rows[0]["CP_PMSIdentifier"].ToString();

                    ddlPortfolioType.SelectedValue = dtPortfolioType.Rows[0]["XPT_PortfolioTypeCode"].ToString();
                    btnSubmit.Text = "Update";
                }
                
            }
        }

        private void BindPortfolioTypeDropDown()
        {
            dtPortfolioType = XMLBo.GetPortfolioType(path);
            ddlPortfolioType.DataSource = dtPortfolioType;
            ddlPortfolioType.DataTextField = "PortfolioType";
            ddlPortfolioType.DataValueField = "PortfolioTypeCode";
            ddlPortfolioType.DataBind();
            ddlPortfolioType.Items.Insert(0, new ListItem("Select Portfolio Type", "Select Portfolio Type"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Submit")
                SavePortfolioDetails();
            else
                UpdatePortfolioDetails();

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerPortfolio','none');", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SavePortfolioDetails();
            ClearAll();
        }

        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            if (txtCustomerId.Value != string.Empty)
            {
                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtCustomerId.Value));
                DataRow dr = dt.Rows[0];
                txtPanCustomer.Text = dr["C_PANNum"].ToString();
                txtAddressCustomer.Text = dr["C_Adr1Line1"].ToString();
            }
        }

        private void SavePortfolioDetails()
        {
            newCustomerPortfolioVo = new CustomerPortfolioVo();
            try
            {
                if (txtCustomerId.Value != string.Empty)
                {
                    customerId = int.Parse(txtCustomerId.Value);
                }
                newCustomerPortfolioVo.CustomerId = customerId;
                newCustomerPortfolioVo.IsMainPortfolio = 0;
                newCustomerPortfolioVo.PMSIdentifier = txtPMSIdentifier.Text;
                newCustomerPortfolioVo.PortfolioName = txtPortfolioName.Text;
                newCustomerPortfolioVo.PortfolioTypeCode = ddlPortfolioType.SelectedValue;

                portfolioBo.CreateCustomerPortfolio(newCustomerPortfolioVo, rmVo.UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GroupAccountSetup.ascx:SavePortfolioDetails()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void UpdatePortfolioDetails()
        {
            newCustomerPortfolioVo = new CustomerPortfolioVo();
            try
            {
                if (txtCustomerId.Value != string.Empty)
                {
                    customerId = int.Parse(txtCustomerId.Value);
                }
                newCustomerPortfolioVo.PortfolioId = int.Parse(Session["PortfolioId"].ToString());
                newCustomerPortfolioVo.CustomerId = customerId;
                newCustomerPortfolioVo.IsMainPortfolio = 0;
                newCustomerPortfolioVo.PMSIdentifier = txtPMSIdentifier.Text;
                newCustomerPortfolioVo.PortfolioName = txtPortfolioName.Text;
                newCustomerPortfolioVo.PortfolioTypeCode = ddlPortfolioType.SelectedValue;

                portfolioBo.UpdateCustomerPortfolio(newCustomerPortfolioVo, rmVo.UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GroupAccountSetup.ascx:SavePortfolioDetails()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void ClearAll()
        {
            txtCustomer.Text = "";
            txtCustomerId.Value = "";
            txtPanCustomer.Text = "";
            txtAddressCustomer.Text = "";
            txtPMSIdentifier.Text = "";
            txtPortfolioName.Text = "";
            ddlPortfolioType.Items.Clear();
        }
    }
}