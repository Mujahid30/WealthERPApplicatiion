using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCustomerProfiling;
using VoCustomerProfiling;
using VoUser;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;


namespace WealthERP.Customer
{
    public partial class ViewBankDetails : System.Web.UI.UserControl
    {
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        CustomerBankAccountVo customerBankAccountVo;
        List<CustomerBankAccountVo> customerBankAccountList = null;
        CustomerVo customerVo = null;
        int custBankAccId;
        int customerId;
        //string customerId = Session["customerId"].ToString();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                customerVo = (CustomerVo)Session["CustomerVo"];
                customerId = customerVo.CustomerId;

                customerBankAccountList = customerBankAccountBo.GetCustomerBankAccounts(customerId);
                if (customerBankAccountList.Count!=0)
                {
                    lblMsg.Visible = false;
                    DataTable dtCustomerBankAccounts = new DataTable();
                    // dtAdvisorBranches.Columns.Add(Select");
                    dtCustomerBankAccounts.Columns.Add("CustBankAccId");
                    dtCustomerBankAccounts.Columns.Add("Bank Name");
                    dtCustomerBankAccounts.Columns.Add("Branch Name");
                    dtCustomerBankAccounts.Columns.Add("Account Type");
                    dtCustomerBankAccounts.Columns.Add("Mode Of Operation");
                    dtCustomerBankAccounts.Columns.Add("Account Number");


                    DataRow drCustomerBankAccount;
                    for (int i = 0; i < customerBankAccountList.Count; i++)
                    {
                        drCustomerBankAccount = dtCustomerBankAccounts.NewRow();
                        customerBankAccountVo = new CustomerBankAccountVo();
                        customerBankAccountVo = customerBankAccountList[i];
                        drCustomerBankAccount[0] = customerBankAccountVo.CustBankAccId.ToString();
                        drCustomerBankAccount[1] = customerBankAccountVo.BankName.ToString();
                        drCustomerBankAccount[2] = customerBankAccountVo.BranchName.ToString();
                        drCustomerBankAccount[3] = customerBankAccountVo.AccountType.ToString();
                        drCustomerBankAccount[4] = customerBankAccountVo.ModeOfOperation.ToString();
                        drCustomerBankAccount[5] = customerBankAccountVo.AccountNum.ToString();

                        dtCustomerBankAccounts.Rows.Add(drCustomerBankAccount);
                    }

                    gvCustomerBankAccounts.DataSource = dtCustomerBankAccounts;
                    gvCustomerBankAccounts.DataBind();
                    gvCustomerBankAccounts.Visible = true;
                }
                else
                {
                    gvCustomerBankAccounts.DataSource = null;
                    gvCustomerBankAccounts.DataBind();
                    lblMsg.Visible = true;
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
                FunctionInfo.Add("Method", "ViewBankDetails.ascx:Page_Load()");
                object[] objects = new object[5];
                objects[0] = customerVo;
                objects[2] = customerBankAccountVo;
                objects[3] = customerBankAccountList;
                objects[4] = custBankAccId;
                objects[5] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvCustomerBankAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            Session["CustBankAccId"] = gvCustomerBankAccounts.SelectedDataKey.Value.ToString();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerAllBankDetails','none');", true);


        }
    }
}