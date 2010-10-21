using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCommon;
using BoUploads;
using VoCustomerProfiling;
using BoCustomerProfiling;
using BoAdvisorProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using System.Configuration;
using WealthERP.Base;

namespace WealthERP.Customer{
    public partial class CustomerAdvisorsNote : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = null;
        string path = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            customerVo = (CustomerVo)Session["CustomerVo"];
            userVo = (UserVo)Session["userVo"];
            if (customerVo.IsActive == 1)
            {
                chkdeactivatecustomer.Checked = false;
            }
            else
            {
                chkdeactivatecustomer.Checked = true;
            }
            if (customerVo.AdviseNote != "")
            {
                txtComments.Text = customerVo.AdviseNote;
            }
            else
            {
                txtComments.Text = "";
            }
            
        }

        protected void btnEdit_Click(object sender, EventArgs e)
            {
                if (chkdeactivatecustomer.Checked)
                {
                    customerVo.IsActive = 1;
                }
                else
                {
                    customerVo.IsActive = 0;
                }
                customerVo.AdviseNote = txtComments.Text.ToString();

                if (customerBo.UpdateCustomer(customerVo))
                {
                    customerVo = customerBo.GetCustomer(customerVo.CustomerId);
                    Session["CustomerVo"] = customerVo;
                    if (customerVo.Type.ToUpper().ToString() == "IND")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewCustomerIndividualProfile','none');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewNonIndividualProfile','none');", true);
                    }
                }
            }

        }
    }

