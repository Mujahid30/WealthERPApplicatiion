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

namespace WealthERP.Customer
{
    public partial class ViewCustomerAllBankDetails : System.Web.UI.UserControl
    {
        CustomerBankAccountVo customerBankAccountVo = new CustomerBankAccountVo();
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        int customerId;
        int customerBankAccId;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                customerId = int.Parse(Session["CustomerId"].ToString());
                customerBankAccId = int.Parse(Session["CustBankAccId"].ToString());
                customerBankAccountVo = customerBankAccountBo.GetCustomerBankAccount(customerId, customerBankAccId);

                lblAccNum.Text = customerBankAccountVo.AccountNum.ToString();
                lblAccType.Text = customerBankAccountVo.AccountType;
                lblModeOfOperation.Text = customerBankAccountVo.ModeOfOperation;
                lblBankName.Text = customerBankAccountVo.BankName.ToString();
                lblBranchName.Text = customerBankAccountVo.BranchName.ToString();
                lblLine1.Text = customerBankAccountVo.BranchAdrLine1.ToString();
                lblLine2.Text = customerBankAccountVo.BranchAdrLine2.ToString();
                lblLine3.Text = customerBankAccountVo.BranchAdrLine3.ToString();
                lblPinCode.Text = customerBankAccountVo.BranchAdrPinCode.ToString();
                lblCity.Text = customerBankAccountVo.BranchAdrCity.ToString();
                lblIfsc.Text = customerBankAccountVo.IFSC.ToString();
                lblMicr.Text = customerBankAccountVo.MICR.ToString();
                lblState.Text = customerBankAccountVo.BranchAdrState;
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
    }
}