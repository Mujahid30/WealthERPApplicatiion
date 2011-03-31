using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using BoCustomerProfiling;
using VoCustomerProfiling;
using VoUser;
using BoCommon;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.General;

namespace WealthERP.Customer
{
    public partial class AddBankDetails : System.Web.UI.UserControl
    {
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        CustomerBankAccountVo customerBankAccountVo = new CustomerBankAccountVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();

        RMVo rmVo = new RMVo();
        DataTable dtAccountType = new DataTable();
        DataTable dtModeofOperation = new DataTable();
        DataTable dtStates = new DataTable();
        int customerId;
        int userId;
        string chk;
        string path;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

                if (!IsPostBack)
                {
                    BindDropDowns(path);
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
                FunctionInfo.Add("Method", "AddBankDetails.ascx:Page_Load()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        private void BindDropDowns(string path)
        {
            try
            {
                dtAccountType = XMLBo.GetBankAccountTypes(path);
                ddlAccountType.DataSource = dtAccountType;
                ddlAccountType.DataTextField = "BankAccountType";
                ddlAccountType.DataValueField = "BankAccountTypeCode";
                ddlAccountType.DataBind();
                ddlAccountType.Items.Insert(0, new ListItem("Select an Account Type", "Select an Account Type"));

                dtModeofOperation = XMLBo.GetModeOfHolding(path);
                ddlModeOfOperation.DataSource = dtModeofOperation;
                ddlModeOfOperation.DataTextField = "ModeOfHolding";
                ddlModeOfOperation.DataValueField = "ModeOfHoldingCode";
                ddlModeOfOperation.DataBind();
                ddlModeOfOperation.Items.Insert(0, new ListItem("Select a Mode of Holding", "Select a Mode of Holding"));

                dtStates = XMLBo.GetStates(path);
                ddlBankAdrState.DataSource = dtStates;
                ddlBankAdrState.DataTextField = "StateName";
                ddlBankAdrState.DataValueField = "StateCode";
                ddlBankAdrState.DataBind();
                ddlBankAdrState.Items.Insert(0, new ListItem("Select a State", "Select a State"));
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AddBankDetails.ascx:BindDropDowns()");
                object[] objects = new object[4];
                objects[0] = path;
                objects[1] = dtAccountType;
                objects[2] = dtModeofOperation;
                objects[3] = dtStates;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            List<int> customerIds = new List<int>();
            int customerId = 0;
            try
            {
                if (Validation())
                {
                    rmVo = (RMVo)Session["RmVo"];
                    //  customerVo = (CustomerVo)Session["CustomerVo"];
                    userId = rmVo.UserId;
                    // customerId = customerVo.CustomerId;
                    if (Session["Check"] != null)
                    {
                        chk = Session["Check"].ToString();
                    }
                    if (Session["CustomerIds"] != null)
                    {
                        customerIds = (List<int>)Session["CustomerIds"];
                        customerId = customerIds[1];
                    }
                    else
                    {
                        customerVo = (CustomerVo)Session["customerVo"];
                        customerId = customerVo.CustomerId;
                    }

                    //if (ddlModeOfOperation.SelectedValue.ToString() != "Select a Mode of Holding")
                    //    customerBankAccountVo.ModeOfOperation = ddlModeOfOperation.SelectedValue.ToString();
                    //customerBankAccountVo.BankName = txtBankName.Text.ToString();
                    //customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
                    //customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
                    //customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
                    //customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
                    //if (txtBankAdrPinCode.Text.ToString() != "")
                    //    customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
                    //customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
                    //if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
                    //    customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();
                    //customerBankAccountVo.BranchAdrCountry = ddlBankAdrCountry.SelectedValue.ToString();
                    //if (txtMicr.Text.ToString() != "")
                    //    customerBankAccountVo.MICR = long.Parse(txtMicr.Text.ToString());
                    //customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
                    //customerBankAccountVo.Balance = 0;

                    //customerBankAccountBo.CreateCustomerBankAccount(customerBankAccountVo, customerId, userId);

                    customerBankAccountVo.AccountType = ddlAccountType.SelectedValue.ToString();
                    customerBankAccountVo.AccountNum = txtAccountNumber.Text.ToString();

                    if (ddlModeOfOperation.SelectedValue.ToString() != "Select a Mode of Holding")
                        customerBankAccountVo.ModeOfOperation = ddlModeOfOperation.SelectedValue.ToString();
                    customerBankAccountVo.BankName = txtBankName.Text.ToString();
                    customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
                    customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
                    customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
                    customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
                    if (txtBankAdrPinCode.Text.ToString() != "")
                        customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
                    customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
                    if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
                        customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();
                    customerBankAccountVo.BranchAdrCountry = ddlBankAdrCountry.SelectedValue.ToString();
                    if (txtMicr.Text.ToString() != "")
                        customerBankAccountVo.MICR = long.Parse(txtMicr.Text.ToString());
                    customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
                    customerBankAccountVo.Balance = 0;
                    //customerBankAccountVo.Balance = long.Parse(txtBalance.Text.ToString());

                    customerBankAccountBo.CreateCustomerBankAccount(customerBankAccountVo, customerId, userId);


                    txtAccountNumber.Text = "";
                    txtBankAdrLine1.Text = "";
                    txtBankAdrLine2.Text = "";
                    txtBankAdrLine3.Text = "";
                    txtBankAdrPinCode.Text = "";
                    txtBankAdrCity.Text = "";
                    txtBankName.Text = "";
                    txtBranchName.Text = "";
                    txtIfsc.Text = "";
                    txtMicr.Text = "";
                    ddlAccountType.SelectedIndex = 0;
                    ddlModeOfOperation.SelectedIndex = 0;

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Bank details added successfully');", true);
                   
                    
                }
                else
                {

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
                FunctionInfo.Add("Method", "AddBankDetails.ascx:btnYes_Click()");
                object[] objects = new object[3];
                objects[0] = customerBankAccountVo;
                objects[1] = customerVo;
                objects[2] = rmVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            List<int> customerIds = new List<int>();
            int customerId = 0;
            try
            {
               // if (Validation())
                {
                    rmVo = (RMVo)Session["RmVo"];

                    if (Session["Check"] != null)
                    {
                        chk = Session["Check"].ToString();
                    }
                    userId = rmVo.UserId;

                    if (Session["CustomerIds"] != null)
                    {
                        customerIds = (List<int>)Session["CustomerIds"];
                        customerId = customerIds[1];
                    }
                    else
                    {
                        customerVo = (CustomerVo)Session["customerVo"];
                        customerId = customerVo.CustomerId;
                    }
                    customerBankAccountVo.AccountType = ddlAccountType.SelectedValue.ToString();
                    customerBankAccountVo.AccountNum = txtAccountNumber.Text.ToString();

                    if (ddlModeOfOperation.SelectedValue.ToString() !="Select a Mode of Holding")
                    customerBankAccountVo.ModeOfOperation = ddlModeOfOperation.SelectedValue.ToString();
                    customerBankAccountVo.BankName = txtBankName.Text.ToString();
                    customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
                    customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
                    customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
                    customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
                    if(txtBankAdrPinCode.Text.ToString()!="")
                    customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
                    customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
                    if(ddlBankAdrState.SelectedValue.ToString()!="Select a State")
                    customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();
                    customerBankAccountVo.BranchAdrCountry = ddlBankAdrCountry.SelectedValue.ToString();
                    if(txtMicr.Text.ToString()!="")
                    customerBankAccountVo.MICR = long.Parse(txtMicr.Text.ToString());
                    customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
                    customerBankAccountVo.Balance = 0;
                    //customerBankAccountVo.Balance = long.Parse(txtBalance.Text.ToString());

                    customerBankAccountBo.CreateCustomerBankAccount(customerBankAccountVo, customerId, userId);

                    if (chk == "CustomerAdd")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerProofsAdd','none');", true);
                    }
                    else if (chk == "Family")
                    {

                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerFamily','none');", true);
                    }
                    else if (chk == "Dashboard" || Session["Check"] == null)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewBankDetails','none');", true);
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
                FunctionInfo.Add("Method", "AddBankDetails.ascx:btnNo_Click()");
                object[] objects = new object[3];
                objects[0] = customerBankAccountVo;
                objects[1] = customerVo;
                objects[2] = rmVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public bool Validation()
        {
            bool result = true;
            try
            {
                if (ddlAccountType.SelectedItem.Value == "")
                {
                    lblAccountType.CssClass = "Error";
                    result = false;
                }
                else
                {
                    lblAccountType.CssClass = "FieldName";
                    result = true;
                }
                if (txtAccountNumber.Text.ToString() == "")
                {
                    lblAccountNumber.CssClass = "Error";
                    result = false;
                }
                else
                {
                    lblAccountNumber.CssClass = "FieldName";
                    result = true;
                }
                if (ddlModeOfOperation.SelectedItem.Value == "")
                {
                    lblModeOfOperation.CssClass = "Error";
                    result = false;
                }
                else
                {
                    lblModeOfOperation.CssClass = "FieldName";
                    result = true;
                }
                if (txtBankName.Text.ToString() == "")
                {
                    lblBankName.CssClass = "Error";
                    result = false;
                }
                else
                {
                    lblBankName.CssClass = "FieldName";
                    result = true;
                }
                if (txtBranchName.Text.ToString() == "")
                {
                    lblBranchName.CssClass = "Error";
                    result = false;
                }
                else
                {
                    lblBranchName.CssClass = "FieldName";
                    result = true;
                }
                //if (txtBankAdrLine1.Text.ToString() == "")
                //{
                //    lblAdrLine1.CssClass = "Error";
                //    result = false;
                //}
                //else
                //{
                //    lblAdrLine1.CssClass = "FieldName";
                //    result = true;
                //}
                //if (txtBankAdrPinCode.Text.ToString() == "")
                //{
                //    lblPinCode.CssClass = "Error";
                //    result = false;
                //}
                //else
                //{
                //    lblPinCode.CssClass = "FieldName";
                //    result = true;
                //}
                //if (txtBankAdrCity.Text.ToString() == "")
                //{
                //    lblCity.CssClass = "Error";
                //    result = false;
                //}
                //else
                //{
                //    lblCity.CssClass = "FieldName";
                //    result = true;
                //}
                //if (txtMicr.Text.ToString() == "")
                //{
                //    lblMicr.CssClass = "Error";
                //    result = false;
                //}
                //else
                //{
                //    lblMicr.CssClass = "FieldName";
                //    result = true;
                //}
                //if (txtIfsc.Text.ToString() == "")
                //{
                //    lblIfsc.CssClass = "Error";
                //    result = false;
                //}
                //else
                //{
                //    lblIfsc.CssClass = "FieldName";
                //    result = true;
                //}
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AddBankDetails.ascx:Validation()");
                object[] objects = new object[1];
                objects[0] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }
    }
}