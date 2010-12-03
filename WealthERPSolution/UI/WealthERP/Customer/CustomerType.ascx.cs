using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using VoCustomerProfiling;
using BoUser;
using VoUser;
using BoCommon;
using BoUploads;
using BoCustomerProfiling;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoAdvisorProfiling;

namespace WealthERP.Customer
{
    public partial class CustomerType : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        RMVo rmVo = new RMVo();
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        UserVo tempUserVo = new UserVo();
        DataTable dtCustomerSubType = new DataTable();
        string assetInterest;
        string path;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                rmVo = (RMVo)Session["rmVo"];
                if (!IsPostBack)
                {
                    lblPanDuplicate.Visible = false;
                    rbtnIndividual.Checked = true;
                    trIndividualName.Visible = false;
                    trNonIndividualName.Visible = false;
                    BindListBranch(rmVo.RMId, "rm");
                    BindSubTypeDropDown();
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
                FunctionInfo.Add("Method", "CustomerType.ascx:Page_Load()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = rmVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        public bool chkAvailability()
        {
            bool result = false;
            string id;
            try
            {
                id = txtEmail.Text;
                result = userBo.ChkAvailability(id);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerType.ascx:chkAvailability()");


                object[] objects = new object[1];
                objects[0] = result;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public bool ChkMailId(string email)
        {
            bool bResult = false;
            try
            {
                if (email == null)
                {
                    bResult = false;
                }
                int nFirstAT = email.IndexOf('@');
                int nLastAT = email.LastIndexOf('@');

                if ((nFirstAT > 0) && (nLastAT == nFirstAT) && (nFirstAT < (email.Length - 1)))
                {

                    bResult = true;
                }
                else
                {

                    bResult = false;
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

                FunctionInfo.Add("Method", "CustomerType.ascx:ChkMailId()");


                object[] objects = new object[1];
                objects[0] = email;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool Validation()
        {
            bool result = true;
            int adviserId = (int)Session["adviserId"];
            try
            {
                if(customerBo.PANNumberDuplicateCheck(adviserId,txtPanNumber.Text.ToString(),customerVo.CustomerId))
                {
                    result = false;
                    lblPanDuplicate.Visible = true;
                }

                //if (!ChkMailId(txtEmail.Text.ToString()))
                //{
                //    result = false;
                //    lblEmail.CssClass = "Error";
                //}
                //if (txtFirstName.Text.ToString() == "" || txtLastName.Text.ToString() == "")
                //{
                //    lblName.CssClass = "Error";
                //    result = false;
                //}
                //else
                //{
                //    lblName.CssClass = "FieldName";
                //    result = true;
                //}
                //if (txtCompanyName.Text.ToString() == "")
                //{
                //    lblCompanyName.CssClass = "Error";
                //    result = false;
                //}
                //else
                //{
                //    lblCompanyName.CssClass = "FieldName";
                //    result = true;
                //}

                //if (ddlCustomerSubType.SelectedItem.Value.ToString() == "")
                //{
                //    lblCustomerSubType.CssClass = "Error";
                //    result = false;
                //}
                //else
                //{
                //    lblCustomerSubType.CssClass = "FieldName";
                //    result = true;
                //}
                //if ((rbtnIndividual.Checked == false) && (rbtnNonIndividual.Checked == false))
                //{
                //    lblCustomerType.CssClass = "Error";
                //    result = false;
                //}
                //else
                //{
                //    lblCustomerType.CssClass = "FieldName";
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
                FunctionInfo.Add("Method", "CustomerType.ascx:Validation()");
                object[] objects = new object[1];
                objects[0] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            BindSubTypeDropDown();
            trSalutation.Visible = true;
        }

        private void BindSubTypeDropDown()
        {
            try
            {
                dtCustomerSubType = XMLBo.GetCustomerSubType(path, "IND");
                ddlCustomerSubType.DataSource = dtCustomerSubType;
                ddlCustomerSubType.DataTextField = "CustomerTypeName";
                ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
                ddlCustomerSubType.DataBind();
                ddlCustomerSubType.Items.Insert(0, new ListItem("Select a Sub-Type", "Select a Sub-Type"));

                //txtFirstName.Visible = true;
                //txtMiddleName.Visible = true;
                //txtLastName.Visible = true;
                //lblName.Visible = true;
                //txtCompanyName.Visible = false;
                //lblCompanyName.Visible = false;
                trIndividualName.Visible = true;
                trNonIndividualName.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerType.ascx:rbtnIndividual_CheckedChanged()");
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
                trIndividualName.Visible = false;
                trNonIndividualName.Visible = true;
                trSalutation.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerType.ascx:rbtnNonIndividual_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<int> customerIds = null;
            try
            {
                Nullable<DateTime> dt = new DateTime();
                customerIds = new List<int>();
                lblPanDuplicate.Visible = false;
                if (Validation())
                {
                    userVo = new UserVo();
                    if (rbtnIndividual.Checked)
                    {
                        rmVo = (RMVo)Session["rmVo"];
                        tempUserVo = (UserVo)Session["userVo"];
                        customerVo.RmId = rmVo.RMId;
                        customerVo.Type = "IND";
                        customerVo.FirstName = txtFirstName.Text.ToString();
                        customerVo.MiddleName = txtMiddleName.Text.ToString();
                        customerVo.LastName = txtLastName.Text.ToString();
                        if (ddlSalutation.SelectedIndex==0)
                        {
                            customerVo.Salutation = "";
                        }
                        else
                        {
                            customerVo.Salutation = ddlSalutation.SelectedValue.ToString();
                        }
                        userVo.FirstName = txtFirstName.Text.ToString();
                        userVo.MiddleName = txtMiddleName.Text.ToString();
                        userVo.LastName = txtLastName.Text.ToString();
                    }
                    else if (rbtnNonIndividual.Checked)
                    {
                        rmVo = (RMVo)Session["rmVo"];
                        tempUserVo = (UserVo)Session["userVo"];
                        customerVo.RmId = rmVo.RMId;
                        customerVo.Type = "NIND";
                        customerVo.CompanyName = txtCompanyName.Text.ToString();
                        customerVo.FirstName = txtCompanyName.Text.ToString();
                        userVo.FirstName = txtCompanyName.Text.ToString();
                    }
                    customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue);
                    if (chkdummypan.Checked)
                    {
                        customerVo.DummyPAN = 1;
                    }
                    else
                    {
                        customerVo.DummyPAN = 0;
                    }
                    if (chkprospect.Checked)
                    {
                        customerVo.IsProspect = 1;
                    }
                    else
                    {
                        customerVo.IsProspect = 0;
                    }

                    customerVo.SubType = ddlCustomerSubType.SelectedItem.Value;
                    customerVo.Email = txtEmail.Text.ToString();
                    customerVo.PANNum = txtPanNumber.Text.ToString();
                    customerVo.Dob = DateTime.MinValue;
                    customerVo.RBIApprovalDate = DateTime.MinValue;
                    customerVo.CommencementDate = DateTime.MinValue;
                    customerVo.RegistrationDate = DateTime.MinValue;
                    customerVo.Adr1State = null;
                    customerVo.Adr2State = null;
                    customerVo.ProfilingDate = DateTime.Today;
                    customerVo.UserId = userVo.UserId;
                    userVo.Email = txtEmail.Text.ToString();
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PortfolioName = "MyPortfolio";
                    customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, tempUserVo.UserId);
                    Session["Customer"] = "Customer";
                    if (customerIds != null)
                    {
                        CustomerFamilyVo familyVo = new CustomerFamilyVo();
                        CustomerFamilyBo familyBo = new CustomerFamilyBo();
                        familyVo.AssociateCustomerId = customerIds[1];
                        familyVo.CustomerId = customerIds[1];
                        familyVo.Relationship = "SELF";
                        familyBo.CreateCustomerFamily(familyVo, customerIds[1], userVo.UserId);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMCustomer','none');", true);
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
                FunctionInfo.Add("Method", "CustomerType.ascx:btnSubmit_Click()");
                object[] objects = new object[5];
                objects[0] = customerIds;
                objects[1] = customerVo;
                objects[2] = rmVo;
                objects[3] = userVo;
                objects[4] = customerPortfolioVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnCustomerProfile_Click(object sender, EventArgs e)
        {
            List<int> customerIds = null;
            try
            {
                if (Validation())
                {
                    customerIds = new List<int>();
                    if (rbtnIndividual.Checked)
                    {
                        customerVo.Type = "IND";
                        customerVo.FirstName = txtFirstName.Text.ToString();
                        customerVo.MiddleName = txtMiddleName.Text.ToString();
                        customerVo.LastName = txtLastName.Text.ToString();
                        if (ddlSalutation.SelectedIndex == 0)
                        {
                            customerVo.Salutation = "";
                        }
                        else
                        {
                            customerVo.Salutation = ddlSalutation.SelectedValue.ToString();
                        }
                        userVo.FirstName = txtFirstName.Text.ToString();
                        userVo.MiddleName = txtMiddleName.Text.ToString();
                        userVo.LastName = txtLastName.Text.ToString();
                    }
                    else if (rbtnNonIndividual.Checked)
                    {
                        customerVo.Type = "NIND";
                        customerVo.FirstName = txtCompanyName.Text.ToString();
                        customerVo.LastName = txtFirstName.Text.ToString();
                        customerVo.MiddleName = txtMiddleName.Text.ToString();
                        //customerVo.FirstName = txtLastName.Text.ToString();
                        userVo.LastName = txtCompanyName.Text.ToString();
                    }
                    //customerVo.CustomerId = customerBo.GenerateId();
                    customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue);
                    customerVo.SubType = ddlCustomerSubType.SelectedItem.Value;
                    customerVo.Email = txtEmail.Text.ToString();
                    customerVo.PANNum = txtPanNumber.Text.ToString();
                    userVo.Email = txtEmail.Text.ToString();
                    userVo.UserType = "Customer";
                    customerVo.Dob = DateTime.MinValue;
                    customerVo.ProfilingDate = DateTime.Today;
                    customerVo.RBIApprovalDate = DateTime.MinValue;
                    customerVo.CommencementDate = DateTime.MinValue;
                    customerVo.RegistrationDate = DateTime.MinValue;
                    customerVo.RmId = rmVo.RMId;
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PortfolioName = "MyPortfolio";
                    customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, tempUserVo.UserId);
                    Session["customerIds"] = customerIds;
                    if (customerIds != null)
                    {
                        CustomerFamilyVo familyVo = new CustomerFamilyVo();
                        CustomerFamilyBo familyBo = new CustomerFamilyBo();
                        familyVo.AssociateCustomerId = customerIds[1];
                        familyVo.CustomerId = customerIds[1];
                        familyVo.Relationship = "SELF";
                        familyBo.CreateCustomerFamily(familyVo, customerIds[1], userVo.UserId);

                    }
                    Session["CustomerVo"] = customerBo.GetCustomer(customerIds[1]);
                    if (rbtnNonIndividual.Checked)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerNonIndividualAdd','none');", true);
                    }
                    else if (rbtnIndividual.Checked)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerIndividualAdd','none');", true);
                    }
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

                FunctionInfo.Add("Method", "CustomerType.ascx:btnCustomerProfile_Click()");

                object[] objects = new object[5];
                objects[0] = customerPortfolioVo;
                objects[1] = customerVo;
                objects[2] = rmVo;
                objects[3] = userVo;
                objects[4] = customerIds;
                objects[5] = assetInterest;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindListBranch(int rmId, string userType)
        {
            UploadCommonBo uploadCommonBo = new UploadCommonBo();
            //DataSet ds = uploadCommonBo.GetAdviserBranchList(rmId, userType);
            DataSet dsAssociatedBranch=advisorBranchBo.GetRMBranchAssociation(rmVo.RMId, 0, "A");
            if (dsAssociatedBranch!=null && dsAssociatedBranch.Tables[0].Rows.Count > 0)
            {
                ddlAdviserBranchList.DataSource = dsAssociatedBranch.Tables[0];
                ddlAdviserBranchList.DataTextField = "AB_BranchName";
                ddlAdviserBranchList.DataValueField = "AB_BranchId";
                ddlAdviserBranchList.DataBind();
                ddlAdviserBranchList.Items.Insert(0, new ListItem("Select a Branch", "Select a Branch"));
            }
            else
            {
                ddlAdviserBranchList.Items.Insert(0, new ListItem("No Branches Available to Associate", "No Branches Available to Associate"));
                ddlAdviserBranchList_CompareValidator2.ValueToCompare = "No Branches Available to Associate";
                ddlAdviserBranchList_CompareValidator2.ErrorMessage = "Cannot Add Customer Without a Branch";
            }
        }
    }
}
