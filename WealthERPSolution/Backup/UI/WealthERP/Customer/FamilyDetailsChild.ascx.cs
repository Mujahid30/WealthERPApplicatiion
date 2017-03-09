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
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using BoCommon;
using System.Data;
using System.Configuration;
namespace WealthERP.Customer
{
    public partial class FamilyDetailsChild : System.Web.UI.UserControl
    {
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerVo parentCustomerVo = new CustomerVo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        CustomerFamilyVo customerFamilyVo = new CustomerFamilyVo();
        UserVo userVo = new UserVo();
        UserVo tempUserVo = new UserVo();
        UserBo userBo = new UserBo();
        RMVo rmVo = new RMVo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        string path;
        DataTable dtCustomerSubType = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            this.Page.Culture = "en-GB";
            txtDob_CompareValidator.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            individualdrop(sender,e);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            UserVo newUserVo = new UserVo();
            
            try
            {
              //  if (Validation())
                {
                    this.Page.Culture = "en-GB";
                    userVo = (UserVo)Session["userVo"];
                    rmVo = (RMVo)Session["rmVo"];
                    parentCustomerVo = (CustomerVo)Session["CustomerVo"];
                    customerVo.RmId = rmVo.RMId;

                    //customerVo.CustomerId = int.Parse(Session["CustomerId2"].ToString());
                    if (txtEmail.Text == "")
                    {
                        customerVo.Email = txtFirstName.Text.ToString() + "@mail.com";
                    }
                    else
                    {
                        customerVo.Email = txtEmail.Text.ToString();
                    }
                    //customerVo.UserId = customerBo.GenerateId();
                    customerVo.FirstName = txtFirstName.Text.ToString();
                    customerVo.MiddleName = txtMiddleName.Text.ToString();
                    customerVo.LastName = txtLastName.Text.ToString();
                    customerVo.BranchId = parentCustomerVo.BranchId;
                    newUserVo.FirstName = txtFirstName.Text.ToString();
                    newUserVo.MiddleName = txtMiddleName.Text.ToString();
                    newUserVo.LastName = txtLastName.Text.ToString();
                    newUserVo.Email = txtEmail.Text.ToString();
                    newUserVo.UserType = "Customer";
                    if (txtDob.Text.ToString() != "")
                        customerVo.Dob = DateTime.Parse(txtDob.Text.ToString());



                    customerVo.ProfilingDate = DateTime.Today;


                    if (rbtnIndividual.Checked)
                    {
                        customerVo.Type = "IND";
                    }
                    else
                    {
                        customerVo.Type = "NIND";
                    }
                    customerVo.SubType = ddlCustomerSubType.SelectedItem.Value.ToString();

                    //userVo.UserId = customerVo.UserId;

                    customerPortfolioVo.CustomerId = customerFamilyVo.AssociateCustomerId;
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PMSIdentifier = "";
                    customerPortfolioVo.PortfolioName = "MyPortfolio";
                    List<int> CustomerIds = new List<int>();
                    CustomerIds = customerBo.CreateCompleteCustomer(customerVo, newUserVo, customerPortfolioVo, userVo.UserId);

                    customerFamilyVo.AssociateCustomerId = CustomerIds[1];
                    customerFamilyVo.Relationship = Session["relationship"].ToString();


                    customerFamilyBo.CreateCustomerFamily(customerFamilyVo, parentCustomerVo.CustomerId, userVo.UserId);


                    txtDob_CompareValidator.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
                    //portfolioBo.CreateCustomerPortfolio(customerPortfolioVo, userVo.UserId);


                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerFamily','none');", true);
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
                FunctionInfo.Add("Method", "FamilyDetailsChild.ascx:btnSubmit_Click()");
                object[] objects = new object[5];
                objects[0] = customerVo;
                objects[1] = customerFamilyVo;
                objects[2] = userVo;
                objects[3] = rmVo;
                objects[4] = parentCustomerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        //public bool Validation()
        //{


        //    bool result = true;

        //    try
        //    //{
        //    //    if (txtFirstName.Text.ToString() == "")
        //    //    {
        //    //        lblerror.CssClass = "Error";
        //    //        //lblerror.Visible = true;
        //    //        result = false;

        //    //    }
        //    //    else
        //    //    {
        //    //        lblName.CssClass = "FieldName";
        //    //        //lblerror.Visible = false;
        //    //        result = true;
        //    //    }
        //    //    if (txtDob.Text.ToString() == "")
        //    //    {
        //    //        lblDob.CssClass = "Error";
        //    //        result = false;

        //    //    }
        //    //    else
        //    //    {
        //    //        lblDob.CssClass = "FieldName";
        //    //        result = true;
        //    //    }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }

        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "FamilyDetailsChild.ascx:Validation()");
        //        object[] objects = new object[1];
        //        objects[0] = result;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }

        //        return result;
        //    }
        

        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dtCustomerSubType = XMLBo.GetCustomerSubType(path, "IND");
                ddlCustomerSubType.DataSource = dtCustomerSubType;
                ddlCustomerSubType.DataTextField = "CustomerTypeName";
                ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
                ddlCustomerSubType.DataBind();
                ddlCustomerSubType.Items.Insert(0, new ListItem("Select a Sub-Type", "Select a Sub-Type"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FamilyDetailsChild.ascx:rbtnIndividual_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void individualdrop(object sender, EventArgs e)
        {

            try
            {
                dtCustomerSubType = XMLBo.GetCustomerSubType(path, "IND");
                ddlCustomerSubType.DataSource = dtCustomerSubType;
                ddlCustomerSubType.DataTextField = "CustomerTypeName";
                ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
                ddlCustomerSubType.DataBind();
                ddlCustomerSubType.Items.Insert(0, new ListItem("Select a Sub-Type", "Select a Sub-Type"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FamilyDetailsChild.ascx:rbtnIndividual_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }


        protected void rbtnNonIndividual_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dtCustomerSubType = XMLBo.GetCustomerSubType(path, "NIND");
                ddlCustomerSubType.DataSource = dtCustomerSubType;
                ddlCustomerSubType.DataTextField = "CustomerTypeName";
                ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
                ddlCustomerSubType.DataBind();
                ddlCustomerSubType.Items.Insert(0, new ListItem("Select a Sub-Type", "Select a Sub-Type"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FamilyDetailsChild.ascx:rbtnNonIndividual_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

    }
}
