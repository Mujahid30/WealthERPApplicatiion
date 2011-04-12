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
using System.Configuration;

namespace WealthERP.Customer
{
    public partial class ViewCustomerAllBankDetails : System.Web.UI.UserControl
    {
        CustomerBankAccountVo customerBankAccountVo = new CustomerBankAccountVo();
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        CustomerVo customerVo = new CustomerVo();
        int customerId;
        int customerBankAccId;
        string state = string.Empty;
        string path;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

                customerVo = (CustomerVo)Session["CustomerVo"];
                customerId = customerVo.CustomerId;

                customerBankAccId = int.Parse(Session["CustBankAccId"].ToString());
                customerBankAccountVo = customerBankAccountBo.GetCustomerBankAccount(customerId, customerBankAccId);

                lblAccNum.Text = customerBankAccountVo.AccountNum.ToString();
                //lblAccType.Text = customerBankAccountVo.AccountType;
                if (customerBankAccountVo.AccountType == "CC")
                {
                    lblAccType.Text = "C.C.";
                }
                if (customerBankAccountVo.AccountType == "CR")
                {
                    lblAccType.Text = "Current";
                }
                if (customerBankAccountVo.AccountType == "FR")
                {
                    lblAccType.Text = "F.C.N.R.";
                }
                if (customerBankAccountVo.AccountType == "NE")
                {
                    lblAccType.Text = "NRE";
                }
                if (customerBankAccountVo.AccountType == "NO")
                {
                    lblAccType.Text = "NRO";
                }
                if (customerBankAccountVo.AccountType == "OD")
                {
                    lblAccType.Text = "O.D.";
                }
                if (customerBankAccountVo.AccountType == "SV")
                {
                    lblAccType.Text = "Savings";
                }
                if (customerBankAccountVo.AccountType == "OT")
                {
                    lblAccType.Text = "Other";
                }
                if (customerBankAccountVo.AccountType == "TBC")
                {
                    lblAccType.Text = "To Be Categorized";
                }
                //lblAccType.Text = XMLBo.GetBankAccountTypes(path, customerBankAccountVo.AccountType);
                if (customerBankAccountVo.ModeOfOperation == "SO")
                {
                    lblModeOfOperation.Text = "Self Only";
                }
                if (customerBankAccountVo.ModeOfOperation == "SI")
                {
                    lblModeOfOperation.Text = "Singly";
                }
                if (customerBankAccountVo.ModeOfOperation == "SE")
                {
                    lblModeOfOperation.Text = "Severaly";
                }
                if (customerBankAccountVo.ModeOfOperation == "JO")
                {
                    lblModeOfOperation.Text = "Jointly";
                }
                if (customerBankAccountVo.ModeOfOperation == "AS")
                {
                    lblModeOfOperation.Text = "Anyone or Survivor";
                }
                if (customerBankAccountVo.ModeOfOperation == "BR")
                {
                    lblModeOfOperation.Text = "As per Board Resolution";
                }
                if (customerBankAccountVo.ModeOfOperation == "ES")
                {
                    lblModeOfOperation.Text = "Either or Survivor";
                }
                if (customerBankAccountVo.ModeOfOperation == "FS")
                {
                    lblModeOfOperation.Text = "Former or Survivor";
                }
                if (customerBankAccountVo.ModeOfOperation == "TBC")
                {
                    lblModeOfOperation.Text = "To Be Categorized";
                }
                //lblModeOfOperation.Text = customerBankAccountVo.ModeOfOperation;
                lblBankName.Text = customerBankAccountVo.BankName.ToString();
                lblBranchName.Text = customerBankAccountVo.BranchName.ToString();
                lblLine1.Text = customerBankAccountVo.BranchAdrLine1.ToString();
                lblLine2.Text = customerBankAccountVo.BranchAdrLine2.ToString();
                lblLine3.Text = customerBankAccountVo.BranchAdrLine3.ToString();
                lblPinCode.Text = customerBankAccountVo.BranchAdrPinCode.ToString();
                lblCity.Text = customerBankAccountVo.BranchAdrCity.ToString();
                lblIfsc.Text = customerBankAccountVo.IFSC.ToString();
                lblMicr.Text = customerBankAccountVo.MICR.ToString();
                state = XMLBo.GetStateName(path, customerBankAccountVo.BranchAdrState);
                lblState.Text = state;
                lblCountry.Text = customerBankAccountVo.BranchAdrCountry;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewCustomerAllBankDetails.ascx:PageLoad()");
                object[] objects = new object[3];
                objects[0] = customerBankAccId;
                objects[1] = customerBankAccountVo;
                objects[2] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                customerBankAccId = int.Parse(Session["CustBankAccId"].ToString());

                if (customerBankAccountBo.DeleteCustomerBankAccount(customerBankAccId))
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewBankDetails','none');", true);
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
                FunctionInfo.Add("Method", "ViewCustomerAllBankDetails.ascx:btnDelete_Click()");
                object[] objects = new object[1];
                objects[0] = customerBankAccId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewBankDetails','none');", true);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                customerBankAccId = int.Parse(Session["CustBankAccId"].ToString());


                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EditCustomerBankAccount','none');", true);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewCustomerAllBankDetails.ascx:btnDelete_Click()");
                object[] objects = new object[1];
                objects[0] = customerBankAccId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
    }
}