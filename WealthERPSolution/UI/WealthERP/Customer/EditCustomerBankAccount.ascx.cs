using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerProfiling;
using VoUser;
using BoCustomerProfiling;
using BoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;
using System.Data;
using System.Configuration;

namespace WealthERP.Customer
{
    public partial class EditCustomerBankAccount : System.Web.UI.UserControl
    {
        CustomerBankAccountVo customerBankAccountVo = new CustomerBankAccountVo();
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        CustomerVo customerVo = new CustomerVo();
        int customerId;
        int customerBankAccId;
        DataTable dtStates = new DataTable();
        string path;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                BindSatesToDP(path);
            }

            try
            {
                SessionBo.CheckSession();
                customerVo = (CustomerVo)Session["customerVo"];
                customerId = customerVo.CustomerId;
                customerBankAccId = int.Parse(Session["CustBankAccId"].ToString());
                customerBankAccountVo = customerBankAccountBo.GetCustomerBankAccount(customerId, customerBankAccId);

                txtAccountNumber.Text = customerBankAccountVo.AccountNum.ToString();
                ddlAccountType.SelectedValue = customerBankAccountVo.AccountType;
                if(customerBankAccountVo.ModeOfOperation!=null && customerBankAccountVo.ModeOfOperation!="")
                    ddlModeOfOperation.SelectedValue = customerBankAccountVo.ModeOfOperation;
                txtBankName.Text = customerBankAccountVo.BankName.ToString();
                txtBranchName.Text = customerBankAccountVo.BranchName.ToString();
                txtBankAdrLine1.Text = customerBankAccountVo.BranchAdrLine1.ToString();
                txtBankAdrLine2.Text = customerBankAccountVo.BranchAdrLine2.ToString();
                txtBankAdrLine3.Text = customerBankAccountVo.BranchAdrLine3.ToString();
                txtBankAdrPinCode.Text = customerBankAccountVo.BranchAdrPinCode.ToString();
                txtBankAdrCity.Text = customerBankAccountVo.BranchAdrCity.ToString();
                txtIfsc.Text = customerBankAccountVo.IFSC.ToString();
                txtMicr.Text = customerBankAccountVo.MICR.ToString();
                if (customerBankAccountVo.BranchAdrState != null && customerBankAccountVo.BranchAdrState != "")
                    ddlBankAdrState.SelectedValue = customerBankAccountVo.BranchAdrState;
                //ddlBankAdrState.SelectedValue = customerBankAccountVo.BranchAdrState;
                ddlBankAdrCountry.SelectedValue = customerBankAccountVo.BranchAdrCountry;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EditCustomerBankAccount.ascx:Page_Load()");
                object[] objects = new object[3];
                objects[0] = customerBankAccountVo;
                objects[1] = customerId;
                objects[2] = customerBankAccId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindSatesToDP(string path)
        {
           
            try
            {
                
                // Bind State Drop Downs
                dtStates = XMLBo.GetStates(path);
                ddlBankAdrState.DataSource = dtStates;
                ddlBankAdrState.DataTextField = "StateName";
                ddlBankAdrState.DataValueField = "StateCode";
                ddlBankAdrState.DataBind();
                ddlBankAdrState.Items.Insert(0, new ListItem("Select", "Select"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditCustomerBankAccount.ascx:BindBranchDropDown()");

                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = dtStates;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                customerVo = (CustomerVo)Session["customerVo"];
                customerId = customerVo.CustomerId;
                customerBankAccId = int.Parse(Session["CustBankAccId"].ToString());

                customerBankAccountVo.CustBankAccId = customerBankAccId;
                customerBankAccountVo.AccountNum = txtAccountNumber.Text.ToString();
                customerBankAccountVo.AccountType = ddlAccountType.SelectedItem.Value.ToString();
                customerBankAccountVo.ModeOfOperation = ddlModeOfOperation.SelectedItem.Value.ToString();
                customerBankAccountVo.BankName = txtBankName.Text.ToString();
                customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
                customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
                customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
                customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
                if (txtBankAdrPinCode.Text.ToString() != "")
                   customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
                else
                    customerBankAccountVo.BranchAdrPinCode =0;
                customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
                if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
                    customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();
             
                //customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedItem.Value.ToString();
                customerBankAccountVo.BranchAdrCountry = ddlBankAdrCountry.SelectedItem.Value.ToString();
                customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
                if (txtMicr.Text.ToString() != "" )
                    customerBankAccountVo.MICR = int.Parse(txtMicr.Text.ToString());
                else
                    customerBankAccountVo.MICR = 0;
                customerBankAccountBo.UpdateCustomerBankAccount(customerBankAccountVo, customerId);

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewBankDetails','none');", true);
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EditCustomerBankAccount.ascx:btnEdit_Click()");
                object[] objects = new object[3];
                objects[0] = customerBankAccountVo;
                objects[1] = customerId;
                objects[2] = customerBankAccId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


    }
}


