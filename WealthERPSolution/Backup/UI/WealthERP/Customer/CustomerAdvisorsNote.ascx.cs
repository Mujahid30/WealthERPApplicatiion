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


namespace WealthERP.Customer
{
    public partial class CustomerAdvisorsNote : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo;
        string path = "";
        AdvisorBo advisorBo = new AdvisorBo();
        AdvisorVo advisorVo = new AdvisorVo();
        DataSet classificationDs;
        int advisorId;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            customerVo = (CustomerVo)Session["CustomerVo"];
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];


            if (!Page.IsPostBack)
            {
                advisorId = advisorVo.advisorId;
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

                bindClassification();
                if (!string.IsNullOrEmpty(customerVo.CustomerClassificationID.ToString()))
                {
                    if (customerVo.CustomerClassificationID == 0)
                    {
                        ddlClassification.SelectedIndex = 0;
                    }
                    else
                        ddlClassification.SelectedValue = customerVo.CustomerClassificationID.ToString();

                }


            }
        }

        protected void bindClassification()
        {
            classificationDs = new DataSet();
            classificationDs = advisorBo.GetAdviserCustomerCategory(advisorId);
            //classificationDs = advisorBo.GetAdviserClassification(1000);
            ddlClassification.DataSource = classificationDs;
            ddlClassification.DataValueField = classificationDs.Tables[0].Columns["ACC_CustomerCategoryCode"].ToString();
            ddlClassification.DataTextField = classificationDs.Tables[0].Columns["ACC_customerCategoryName"].ToString();
            ddlClassification.DataBind();
            ddlClassification.Items.Insert(0, new ListItem("Select a Classification", "Select a Classification"));

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (chkdeactivatecustomer.Checked)
            {
                customerVo.IsActive = 0;
            }
            else
            {
                customerVo.IsActive = 1;
            }
            customerVo.AdviseNote = txtComments.Text.ToString();
            customerVo.CustomerClassificationID = int.Parse(ddlClassification.SelectedValue.ToString());
            if (customerBo.UpdateCustomer(customerVo,userVo.UserId))
            {
                customerVo = customerBo.GetCustomer(customerVo.CustomerId);
                Session["CustomerVo"] = customerVo;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Classification updated Succesfully');", true);
                //if (customerVo.Type.ToUpper().ToString() == "IND")
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewCustomerIndividualProfile','none');", true);
                //}
                //else
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewNonIndividualProfile','none');", true);
                //}
            }
        }
        protected void ddlClassification_Load(object sender, EventArgs e)
        {

            ddlClassification.Items.FindByValue("8").Enabled = false;
            ddlClassification.Items.FindByValue("9").Enabled = false;



        }

    }
}

