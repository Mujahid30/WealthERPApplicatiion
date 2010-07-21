using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using VoCustomerProfiling;
using BoUser;
using BoCustomerProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using WealthERP.Base;
using BoCommon;
using System.Configuration;
using System.Text;
namespace WealthERP.Advisor
{
    public partial class RMCustomerDashboard : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        RMVo rmVo = new RMVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        CustomerBankAccountVo customerBankAccountVo = new CustomerBankAccountVo();
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        List<CustomerBankAccountVo> customerBankAccountList = new List<CustomerBankAccountVo>();
        string path = "";
        List<CustomerFamilyVo> customerFamilyList = null;
        DataSet ds = new DataSet();
        DataSet dsBankDetails = new DataSet();
        int customerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            try
            {
              //  customerFamilyList = new List<CustomerFamilyVo>();

                customerVo = (CustomerVo)Session["CustomerVo"];
                rmVo = (RMVo)Session["RmVo"];
                customerId = customerVo.CustomerId;
                StringBuilder sbAddress = new StringBuilder();
                
                lblName.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                lblPhone.Text = customerVo.ResISDCode.ToString() + " - " + customerVo.ResSTDCode.ToString() + " - " + customerVo.ResPhoneNum.ToString();
                lblEmail.Text = customerVo.Email.ToString();

                sbAddress.Append(customerVo.Adr1Line1.ToString());
                sbAddress.Append("<br />");
                sbAddress.Append(customerVo.Adr1Line2.ToString());
                sbAddress.Append("<br />");
                sbAddress.Append(customerVo.Adr1Line3.ToString());
                sbAddress.Append("<br />");
                sbAddress.Append(customerVo.Adr1PinCode.ToString());
                sbAddress.Append("<br />");

                if (customerVo.Adr1State.ToString() != "")
                {
                    sbAddress.Append(XMLBo.GetStateName(path, customerVo.Adr1State.ToString()));
                    sbAddress.Append("<br />");
                }
                
                sbAddress.Append(customerVo.Adr1City.ToString());
                sbAddress.Append("<br />");
                sbAddress.Append(customerVo.Adr1Country.ToString());

                lblAddress.Text = sbAddress.ToString();

                Session["RmVo"] = rmVo;
                // Session["CustomerVo"] = customerVo;
                Session["Check"] = "Dashboard";

                //Binding the Customer Family Grid
                customerFamilyList = customerFamilyBo.GetCustomerFamily(customerVo.CustomerId);
                if (customerFamilyList == null)
                {
                    tdFamilyDetailsHeader.Visible=false;
                    tdFamilyDetailsGrid.Visible=false;
                }
                else
                {
                    DataTable dtCustomerFamilyList = new DataTable();
                    dtCustomerFamilyList.Columns.Add("AssociationId");
                    dtCustomerFamilyList.Columns.Add("Name");
                    dtCustomerFamilyList.Columns.Add("Relationship");

                    DataRow drCustomerFamily;
                    for (int i = 0; i < customerFamilyList.Count; i++)
                    {
                        drCustomerFamily = dtCustomerFamilyList.NewRow();
                        CustomerFamilyVo customerFamilyVo = new CustomerFamilyVo();
                        customerFamilyVo = customerFamilyList[i];
                        drCustomerFamily[0] = customerFamilyVo.AssociationId.ToString();
                        drCustomerFamily[1] = customerFamilyVo.AssociateCustomerName.ToString();
                        drCustomerFamily[2] = customerFamilyVo.Relationship;
                        dtCustomerFamilyList.Rows.Add(drCustomerFamily);

                    }
                    if (dtCustomerFamilyList.Rows.Count > 0)
                    {
                        gvFamilyMembers.DataSource = dtCustomerFamilyList;
                        gvFamilyMembers.DataBind();
                        gvFamilyMembers.Visible = true;
                    }
                    else
                    {
                        gvFamilyMembers.DataSource = null;
                        gvFamilyMembers.DataBind();
                    }
                }

                //Call the function to bind the Bank Details
                BindBankDetails();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RMCustomerDashboard.ascx:Page_Load()");
                object[] objects = new object[4];
                objects[0] = customerFamilyList;
                objects[1] = rmVo;
                objects[2] = customerVo;
                objects[3] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvFamilyMembers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFamilyMembers.PageIndex = e.NewPageIndex;
            gvFamilyMembers.DataBind();
        }

        public void BindBankDetails()
        {
            try
            {
                customerBankAccountList = customerBankAccountBo.GetCustomerBankAccounts(customerId);
                if (customerBankAccountList.Count != 0)
                {
                    DataTable dtCustomerBankAccounts = new DataTable();
                    //dtCustomerBankAccounts.Columns.Add("CustBankAccId");
                    dtCustomerBankAccounts.Columns.Add("Bank Name");
                    //dtCustomerBankAccounts.Columns.Add("Branch Name");
                    dtCustomerBankAccounts.Columns.Add("Account Type");
                    //dtCustomerBankAccounts.Columns.Add("Mode Of Operation");
                    dtCustomerBankAccounts.Columns.Add("Account Number");


                    DataRow drCustomerBankAccount;
                    for (int i = 0; i < customerBankAccountList.Count; i++)
                    {
                        drCustomerBankAccount = dtCustomerBankAccounts.NewRow();
                        customerBankAccountVo = new CustomerBankAccountVo();
                        customerBankAccountVo = customerBankAccountList[i];
                        //drCustomerBankAccount[0] = customerBankAccountVo.CustBankAccId.ToString();
                        drCustomerBankAccount[0] = customerBankAccountVo.BankName.ToString();
                        //drCustomerBankAccount[2] = customerBankAccountVo.BranchName.ToString();
                        drCustomerBankAccount[1] = customerBankAccountVo.AccountType.ToString();
                        //drCustomerBankAccount[4] = customerBankAccountVo.ModeOfOperation.ToString();
                        drCustomerBankAccount[2] = customerBankAccountVo.AccountNum.ToString();

                        dtCustomerBankAccounts.Rows.Add(drCustomerBankAccount);
                    }

                    gvBankDetails.DataSource = dtCustomerBankAccounts;
                    gvBankDetails.DataBind();
                    gvBankDetails.Visible = true;
                }
                else
                {
                    tdBankDetailsHeader.Visible = false;
                    tdBankDetailsGrid.Visible = false;
                    gvBankDetails.DataSource = null;
                    gvBankDetails.DataBind();
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

                FunctionInfo.Add("Method", "RMCustIndividualDashboard.ascx:BindBankDetails()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

    }
}