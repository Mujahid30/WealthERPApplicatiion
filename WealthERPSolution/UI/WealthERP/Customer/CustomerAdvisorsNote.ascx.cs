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
using System.Data.Sql;


namespace WealthERP.Customer{
    public partial class CustomerAdvisorsNote : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = null;
        string path = "";
        AdvisorBo advisorBo = new AdvisorBo();
        DataSet classificationDs;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            customerVo = (CustomerVo)Session["CustomerVo"];
            userVo = (UserVo)Session["userVo"];
            

            if (!Page.IsPostBack)
            {
                if (customerVo.IsActive == 1)
                {
                    chkdeactivatecustomer.Checked = true;
                }
                else
                {
                    chkdeactivatecustomer.Checked = false;
                }
                if (customerVo.AdviseNote != "")
                {
                    txtComments.Text = customerVo.AdviseNote;
                }
                else
                {
                    txtComments.Text = "";
                }
                if (!string.IsNullOrEmpty(customerVo.CustomerClassificationID.ToString()))
                {
                    ddlClassification.SelectedValue = customerVo.CustomerClassificationID.ToString();

                }
                bindClassification();
 
            }
        }

        protected void bindClassification()
        {
            classificationDs = new DataSet();
            classificationDs = advisorBo.GetAdviserClassification(1000);
            ddlClassification.DataSource = classificationDs;
            ddlClassification.DataValueField = classificationDs.Tables[0].Columns["ACC_CustomerClassificationId"].ToString();
            ddlClassification.DataTextField = classificationDs.Tables[0].Columns["ACC_CustomerClassification"].ToString();
            ddlClassification.DataBind();
 
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
                customerVo.CustomerClassificationID = int.Parse(ddlClassification.SelectedValue.ToString());
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

