using System;
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
using WealthERP.Base;
//using System.Web.Services.WebMethod;
using System.Data.SqlClient;
//using System.Web.Script.Services.ScriptMethod;

namespace WealthERP.Customer
{
    public partial class CustomerType : System.Web.UI.UserControl
    {
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        UserVo tempUserVo = new UserVo();
        DataTable dtCustomerSubType = new DataTable();
        string assetInterest;
        string path;
        string page = string.Empty;
        int bmID;
        DataSet dsProfileLookup;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                advisorVo = (AdvisorVo)Session["advisorVo"];
                userVo = (UserVo)Session["userVo"];
                rmVo = (RMVo)Session["rmVo"];
                bmID = rmVo.RMId;
                chkRealInvestor.Attributes.Add("onClick", "javascript:ShowSubmitAndSave();");
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
                {
                    btnCustomerProfile.Visible = false;
                }
                if (Request.QueryString["page"] != null)
                {
                    page = Request.QueryString["page"];
                    btnCustomerProfile.Visible = false;
                }
                if (Request.QueryString["AddMFCustLinkId"] != null)
                {
                    trBranchlist.Visible = false;
                    trRMlist.Visible = false;
                    btnCustomerProfile.Visible = false;
                    lblClientCode.Visible = false;
                    txtClientCode.Visible = false;
                    //  Session.Remove("AddMFCustLinkId");
                }
                if (!IsPostBack)
                {
                    lblPanDuplicate.Visible = false;
                    rbtnIndividual.Checked = true;
                    trIndividualName.Visible = false;
                    trNonIndividualName.Visible = false;
                    BindListBranch();
                    BindRMforBranchDropdown(0, 0);
                    //BindListBranch(rmVo.RMId, "rm");
                    BindSubTypeDropDown(1001);

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

        //public bool Validation()
        //{
        //    bool result = true;
        //    int adviserId = (int)Session["adviserId"];
        //    try
        //    {
        //        if (customerBo.PANNumberDuplicateChild(adviserId, txtPanNumber.Text.ToString()))
        //        {
        //            result = false;
        //            lblPanDuplicate.Visible = true;
        //        }

        //        //if (!ChkMailId(txtEmail.Text.ToString()))
        //        //{
        //        //    result = false;
        //        //    lblEmail.CssClass = "Error";
        //        //}
        //        //if (txtFirstName.Text.ToString() == "" || txtLastName.Text.ToString() == "")
        //        //{
        //        //    lblName.CssClass = "Error";
        //        //    result = false;
        //        //}
        //        //else
        //        //{
        //        //    lblName.CssClass = "FieldName";
        //        //    result = true;
        //        //}
        //        //if (txtCompanyName.Text.ToString() == "")
        //        //{
        //        //    lblCompanyName.CssClass = "Error";
        //        //    result = false;
        //        //}
        //        //else
        //        //{
        //        //    lblCompanyName.CssClass = "FieldName";
        //        //    result = true;
        //        //}

        //        //if (ddlCustomerSubType.SelectedItem.Value.ToString() == "")
        //        //{
        //        //    lblCustomerSubType.CssClass = "Error";
        //        //    result = false;
        //        //}
        //        //else
        //        //{
        //        //    lblCustomerSubType.CssClass = "FieldName";
        //        //    result = true;
        //        //}
        //        //if ((rbtnIndividual.Checked == false) && (rbtnNonIndividual.Checked == false))
        //        //{
        //        //    lblCustomerType.CssClass = "Error";
        //        //    result = false;
        //        //}
        //        //else
        //        //{
        //        //    lblCustomerType.CssClass = "FieldName";
        //        //    result = true;
        //        //}
        //    }

        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "CustomerType.ascx:Validation()");
        //        object[] objects = new object[1];
        //        objects[0] = result;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return result;
        //}
      
        public static List<string> GetCompletionList(string prefixText, int count)  
        {  
            return AutoFillProducts(prefixText);  
  
        }
        private static List<string> AutoFillProducts(string prefixText)  
        {  
            using (SqlConnection con = new SqlConnection())  
            {  
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
                 using (SqlCommand com = new SqlCommand())  
                {  
                    com.CommandText = "select client code from ???????? where " + "ProductName like @Search + '%'";  
  
                    com.Parameters.AddWithValue("@Search", prefixText);  
                    com.Connection = con;  
                    con.Open();  
                    List<string> countryNames = new List<string>();  
                    using (SqlDataReader sdr = com.ExecuteReader())  
                    {  
                        while (sdr.Read())  
                        {  
                            countryNames.Add(sdr["Client Code"].ToString());  
                        }  
                    }  
                    con.Close();  
                    return countryNames;
                 }
            }
        }

        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            ddlAdviserBranchList.Items.Clear();
            ddlAdviseRMList.Items.Clear();
            BindListBranch();
            BindRMforBranchDropdown(0, 0);
            BindSubTypeDropDown(1001);
            trSalutation.Visible = true;
        }

        private void BindSubTypeDropDown(int lookupParentId)
        {
            DataTable dtCustomerTaxSubTypeLookup = new DataTable();
            try
            {
                dtCustomerTaxSubTypeLookup = commonLookupBo.GetWERPLookupMasterValueList(2000, lookupParentId);
                ddlCustomerSubType.DataSource = dtCustomerTaxSubTypeLookup;
                ddlCustomerSubType.DataTextField = "WCMV_Name";
                ddlCustomerSubType.DataValueField = "WCMV_LookupId";
                ddlCustomerSubType.DataBind();
                ddlCustomerSubType.Items.Insert(0, new ListItem("--SELECT--", "0"));

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
                //dtCustomerSubType = XMLBo.GetCustomerSubType(path, "NIND");
                //ddlCustomerSubType.DataSource = dtCustomerSubType;
                //ddlCustomerSubType.DataTextField = "CustomerTypeName";
                //ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
                //ddlCustomerSubType.DataBind();
                //ddlCustomerSubType.Items.Insert(0, new ListItem("Select", "Select"));

                BindSubTypeDropDown(1002);
                ddlAdviserBranchList.Items.Clear();
                ddlAdviseRMList.Items.Clear();
                BindListBranch();
                BindRMforBranchDropdown(0, 0);

                //ddlAdviserBranchList.EnableViewState = false;
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
                if (chkdummypan.Checked)
                {
                    customerVo.DummyPAN = 1;
                }
                else
                {
                    customerVo.DummyPAN = 0;
                }
                Nullable<DateTime> dt = new DateTime();
                customerIds = new List<int>();
                lblPanDuplicate.Visible = false;
                if (Validation())
                {
                    lblPanDuplicate.Visible = false;
                        userVo = new UserVo();
                        if (rbtnIndividual.Checked)
                        {
                            rmVo = (RMVo)Session["rmVo"];
                            tempUserVo = (UserVo)Session["userVo"];
                            //customerVo.RmId = rmVo.RMId;
                            if (customerVo.RmId == rmVo.RMId)
                            {
                                customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue.ToString());
                            }
                            else
                            {
                                customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue);
                            }
                            customerVo.Type = "IND";

                            customerVo.TaxStatusCustomerSubTypeId = Int16.Parse(ddlCustomerSubType.SelectedValue.ToString());
                            customerVo.CustCode = txtClientCode.Text.Trim();
                            customerVo.IsRealInvestor = chkRealInvestor.Checked;
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
                            rmVo = (RMVo)Session["rmVo"];
                            tempUserVo = (UserVo)Session["userVo"];
                            //customerVo.RmId = rmVo.RMId;
                            //customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue.ToString());
                            if (customerVo.RmId == rmVo.RMId)
                            {
                                customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue.ToString());
                            }
                            else
                            {
                                customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue);
                            }
                            customerVo.Type = "NIND";

                            customerVo.TaxStatusCustomerSubTypeId = Int16.Parse(ddlCustomerSubType.SelectedValue.ToString());
                            customerVo.CustCode = txtClientCode.Text.Trim();
                            customerVo.IsRealInvestor = chkRealInvestor.Checked;
                            customerVo.CompanyName = txtCompanyName.Text.ToString();
                            customerVo.FirstName = txtCompanyName.Text.ToString();
                            userVo.FirstName = txtCompanyName.Text.ToString();
                        }
                        if (customerVo.BranchId == rmVo.BranchId)
                        {
                            customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue);
                        }
                        else
                        {
                            customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue);
                        }

                        //if (chkprospect.Checked)
                        //{
                        //    customerVo.IsProspect = 1;
                        //}
                        //else
                        //{
                        //    customerVo.IsProspect = 0;
                        //}

                        //customerVo.SubType = ddlCustomerSubType.SelectedItem.Value;
                        customerVo.Email = txtEmail.Text.ToString();
                        customerVo.PANNum = txtPanNumber.Text.ToString();
                        if (!string.IsNullOrEmpty(txtMobileNumber.Text.ToString()))
                        {
                            customerVo.Mobile1 = Convert.ToInt64(txtMobileNumber.Text.ToString());
                        }
                        else
                            customerVo.Mobile1 = 0;
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
                        customerVo.ViaSMS = 1;
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

                            if (page == "Entry")
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('OrderEntry','?CustomerId=" + familyVo.CustomerId + " ');", true);

                            }
                            else
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Customer Added Successfully!!');", true);
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AdviserCustomer','none');", true);
                            //trSumbitSuccess.Visible = true;
                            MakeReadonlyControls();
                            if (Request.QueryString["AddMFCustLinkId"] != null)
                            {
                                lblPanDuplicate.Visible = false;
                                MakeReadonlyControls();
                                Response.Write("<script>alert('Customer has been successfully added');</script>");
                                Response.Write("<script>window.close();</" + "script>");
                            }
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

        private void MakeReadonlyControls()
        {
            ddlAdviserBranchList.Enabled = false;
            ddlAdviseRMList.Enabled = false;
            ddlCustomerSubType.Enabled = false;
            txtPanNumber.Enabled = false;
            ddlSalutation.Enabled = false;
            txtFirstName.Enabled = false;
            txtMiddleName.Enabled = false;
            txtLastName.Enabled = false;
            txtEmail.Enabled = false;
            btnSubmit.Enabled = false;
            btnCustomerProfile.Enabled = false;
            trBtnSaveMsg.Visible = true;

        }

        protected void btnCustomerProfile_Click(object sender, EventArgs e)
        {
            List<int> customerIds = null;
            try
            {

                if (chkdummypan.Checked)
                {
                    customerVo.DummyPAN = 1;
                }
                else
                {
                    customerVo.DummyPAN = 0;
                }
                if (Validation())
                {
                    customerIds = new List<int>();
                    if (rbtnIndividual.Checked)
                    {
                        customerVo.Type = "IND";
                        customerVo.FirstName = txtFirstName.Text.ToString();
                        customerVo.MiddleName = txtMiddleName.Text.ToString();
                        customerVo.LastName = txtLastName.Text.ToString();

                        customerVo.TaxStatusCustomerSubTypeId = Int16.Parse(ddlCustomerSubType.SelectedValue.ToString());
                        // customerVo.AccountId = txtClientCode.Text.Trim();
                        customerVo.CustCode = txtClientCode.Text.Trim();
                        customerVo.IsRealInvestor = chkRealInvestor.Checked;

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

                        customerVo.TaxStatusCustomerSubTypeId = Int16.Parse(ddlCustomerSubType.SelectedValue.ToString());
                        //    customerVo.AccountId = txtClientCode.Text.Trim();
                        customerVo.IsRealInvestor = chkRealInvestor.Checked;
                        customerVo.CustCode = txtClientCode.Text.Trim();
                        customerVo.CompanyName = txtCompanyName.Text.ToString();
                        customerVo.FirstName = txtCompanyName.Text.ToString();
                        customerVo.LastName = txtFirstName.Text.ToString();
                        customerVo.MiddleName = txtMiddleName.Text.ToString();
                        //customerVo.FirstName = txtLastName.Text.ToString();
                        userVo.LastName = txtCompanyName.Text.ToString();
                    }

                    //customerVo.CustomerId = customerBo.GenerateId();
                    customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue);
                    //customerVo.SubType = ddlCustomerSubType.SelectedItem.Value;
                    customerVo.Email = txtEmail.Text.ToString();
                    customerVo.PANNum = txtPanNumber.Text.ToString();
                    userVo.Email = txtEmail.Text.ToString();
                    userVo.UserType = "Customer";
                    customerVo.Dob = DateTime.MinValue;
                    if (chkdummypan.Checked)
                        customerVo.DummyPAN = 1;
                    else
                        customerVo.DummyPAN = 0;
                    if (!string.IsNullOrEmpty(txtMobileNumber.Text.ToString()))
                    {
                        customerVo.Mobile1 = Convert.ToInt64(txtMobileNumber.Text.ToString());
                    }
                    else
                        customerVo.Mobile1 = 0;
                    customerVo.ProfilingDate = DateTime.Today;
                    customerVo.RBIApprovalDate = DateTime.MinValue;
                    customerVo.CommencementDate = DateTime.MinValue;
                    customerVo.RegistrationDate = DateTime.MinValue;
                    customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue.ToString());
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
                        // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerNonIndividualAdd','none');", true);
                        Response.Redirect("ControlHost.aspx?pageid=CustomerNonIndividualAdd&RmId=" + customerVo.RmId + "", false);
                    }
                    else if (rbtnIndividual.Checked)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerIndividualAdd','none');", true);
                        Response.Redirect("ControlHost.aspx?pageid=EditCustomerIndividualProfile&RmId=" + customerVo.RmId + "&action=" + "Edit" + "", false);
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

        //private void BindListBranch(int rmId, string userType)
        //{
        //    UploadCommonBo uploadCommonBo = new UploadCommonBo();
        //    //DataSet ds = uploadCommonBo.GetAdviserBranchList(rmId, userType);
        //    DataSet dsAssociatedBranch=advisorBranchBo.GetRMBranchAssociation(rmVo.RMId, 0, "A");
        //    if (dsAssociatedBranch!=null && dsAssociatedBranch.Tables[0].Rows.Count > 0)
        //    {
        //        ddlAdviserBranchList.DataSource = dsAssociatedBranch.Tables[0];
        //        ddlAdviserBranchList.DataTextField = "AB_BranchName";
        //        ddlAdviserBranchList.DataValueField = "AB_BranchId";
        //        ddlAdviserBranchList.DataBind();
        //        ddlAdviserBranchList.Items.Insert(0, new ListItem("Select a Branch", "Select a Branch"));
        //    }
        //    else
        //    {
        //        ddlAdviserBranchList.Items.Insert(0, new ListItem("No Branches Available to Associate", "No Branches Available to Associate"));
        //        ddlAdviserBranchList_CompareValidator2.ValueToCompare = "No Branches Available to Associate";
        //        ddlAdviserBranchList_CompareValidator2.ErrorMessage = "Cannot Add Customer Without a Branch";
        //    }
        //}
        private void BindListBranch()
        {
            if (advisorVo.A_AgentCodeBased != 1)
            {
                UploadCommonBo uploadCommonBo = new UploadCommonBo();
                DataSet ds = uploadCommonBo.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlAdviserBranchList.DataSource = ds.Tables[0];
                    ddlAdviserBranchList.DataTextField = "AB_BranchName";
                    ddlAdviserBranchList.DataValueField = "AB_BranchId";
                    //ddlAdviserBranchList.SelectedValue = "1339";
                    ddlAdviserBranchList.DataBind();
                    ddlAdviserBranchList.Items.Insert(0, new ListItem("Select", "Select"));
                }
                else
                {
                    ddlAdviserBranchList.Items.Insert(0, new ListItem("No Branches Available to Associate", "No Branches Available to Associate"));
                    ddlAdviserBranchList_CompareValidator2.ValueToCompare = "No Branches Available to Associate";
                    ddlAdviserBranchList_CompareValidator2.ErrorMessage = "Cannot Add Customer Without a Branch";
                }
            }
            else
            {
                DataSet BMDs = new DataSet();
                DataTable BMList = new DataTable();
                //BMList.Columns.Add("AB_BranchId");
                //BMList.Columns.Add("AB_BranchName");
                BMDs = advisorBranchBo.GetBMRoleForAgentBased(advisorVo.advisorId);

                if (BMDs.Tables[0].Rows.Count > 0)
                {
                    ddlAdviserBranchList.DataSource = BMDs;
                    ddlAdviserBranchList.DataValueField = BMDs.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlAdviserBranchList.DataTextField = BMDs.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlAdviserBranchList.DataBind();
                    ddlAdviserBranchList.Enabled = false;
                }
                else
                {
                    ddlAdviseRMList.Enabled = false;
                }

            }
        }

        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {

            try
            {
                if (advisorVo.A_AgentCodeBased != 1)
                {
                    DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlAdviseRMList.DataSource = ds.Tables[0];
                        ddlAdviseRMList.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                        ddlAdviseRMList.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                        // ddlAdviseRMList.SelectedValue = "4682";
                        ddlAdviseRMList.DataBind();
                        //ddlAdviseRMList.Items.Remove("No RM Available");
                        //ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                        //CompareValidator2.ValueToCompare = "Select";
                        //CompareValidator2.ErrorMessage = "Please select a RM";
                    }
                    else
                    {
                        if (!IsPostBack)
                        {
                            ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                            CompareValidator2.ValueToCompare = "Select";
                            CompareValidator2.ErrorMessage = "Please select a RM";

                        }
                        else
                        {
                            if (rbtnNonIndividual.Checked == true)
                            {
                                if ((IsPostBack) && (ddlAdviserBranchList.SelectedIndex == 0))
                                {
                                    ddlAdviseRMList.Items.Clear();
                                    ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                                    CompareValidator2.ValueToCompare = "Select";
                                    CompareValidator2.ErrorMessage = "Please select a RM";
                                }
                                else
                                {
                                    ddlAdviseRMList.Items.Clear();
                                    ddlAdviseRMList.Items.Remove("Select");
                                    ddlAdviseRMList.Items.Insert(0, new ListItem("No RM Available", "No RM Available"));
                                    CompareValidator2.ValueToCompare = "No RM Available";
                                    CompareValidator2.ErrorMessage = "Cannot Add Customer Without a RM";


                                }
                            }
                            else
                            {
                                if ((IsPostBack) && (ddlAdviserBranchList.SelectedIndex == 0))
                                {
                                    ddlAdviseRMList.Items.Clear();
                                    ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                                    CompareValidator2.ValueToCompare = "Select";
                                    CompareValidator2.ErrorMessage = "Please select a RM";
                                }
                                else
                                {
                                    ddlAdviseRMList.Items.Clear();
                                    ddlAdviseRMList.Items.Remove("Select");
                                    ddlAdviseRMList.Items.Insert(0, new ListItem("No RM Available", "No RM Available"));
                                    CompareValidator2.ValueToCompare = "No RM Available";
                                    CompareValidator2.ErrorMessage = "Cannot Add Customer Without a RM";
                                }
                            }
                        }
                    }
                }
                else
                {
                    DataSet Rmds = new DataSet();
                    //DataTable Rmlist = Rmds.Tables[0];
                    //Rmlist.Columns.Add("RmID");
                    //Rmlist.Columns.Add("RMName");
                    Rmds = advisorBranchBo.GetRMRoleForAgentBased(advisorVo.advisorId);

                    if (Rmds.Tables[0].Rows.Count > 0)
                    {
                        ddlAdviseRMList.DataSource = Rmds;
                        ddlAdviseRMList.DataValueField = Rmds.Tables[0].Columns["RmID"].ToString();
                        ddlAdviseRMList.DataTextField = Rmds.Tables[0].Columns["RMName"].ToString();
                        ddlAdviseRMList.DataBind();
                        ddlAdviseRMList.Enabled = false;
                    }
                    else
                    {
                        ddlAdviseRMList.Enabled = false;
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

                FunctionInfo.Add("Method", "AdviserMFMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


        protected void ddlAdviserBranchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAdviserBranchList.SelectedIndex == 0)
            {
                //BindRMforBranchDropdown(0, bmID);
                BindRMforBranchDropdown(0, 0);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlAdviserBranchList.SelectedValue.ToString()), 0);

            }
        }
        public bool Validation()
        {
            bool result = true;
            int adviserId = ((AdvisorVo)Session["advisorVo"]).advisorId;
            try
            {
                if (customerBo.PANNumberDuplicateCheck(adviserId, txtPanNumber.Text.ToString(), customerVo.CustomerId))
                {
                    result = false;
                    lblPanDuplicate.Visible = true;
                }
                else
                {
                    lblPanDuplicate.Visible = false;
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

    }
}
