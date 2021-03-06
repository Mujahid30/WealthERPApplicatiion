﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoCustomerProfiling;
using BoProductMaster;
using WealthERP.Base;
using VoUser;
using BoCommon;
using BoUploads;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Numeric;
using VoAdvisorProfiling;
using Telerik.Web.UI;

namespace WealthERP.BusinessMIS
{
    public partial class MFSIPProjection : System.Web.UI.UserControl
    {
        SystematicSetupVo systematicSetupVo;
        SystematicSetupBo systematicSetupBo = new SystematicSetupBo();
        AdvisorVo advisorVo = new AdvisorVo();
        DataSet dsSystematicMIS = new DataSet();
        DataTable dtSystematicTransactionType;
        DataTable dtAMC;
        DataTable dtCategory;
        DataTable dtScheme;
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        string strAmcCode = null;
        String userType;
        int advisorId = 0;
        int customerId = 0;
        double sumTotal;
        double totalAmount = 0;
        int rmId = 0;
        int bmID = 0;
        double monthlyTotalSipAmount = 0;
        RMVo rmVo = new RMVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();

        DataSet dsBindGvSystematicMIS = new DataSet();
        DataTable dtSystematicMIS1 = new DataTable();
        DataTable dtSystematicMIS2 = new DataTable();
        DataTable dtSystematicMIS3 = new DataTable();
        DataTable dtSystematicMIS4 = new DataTable();

        DateTime startDate = new DateTime();
        DateTime endDate = new DateTime();
        string frequency = "";
        int systematicDate = 0;
        int monthCode = 0;
        int year = 0;
        int isIndividualOrGroup = 0;

        int all;
        string customerType = string.Empty;
        int PageSize;
        private decimal totalSIPAmount = 0;
        private decimal totalSWPAmount = 0;
        private int totalNoOfSIP = 0;
        private int totalNoOfFreshSIP = 0;
        private int totalNoOfSWP = 0;
        private int totalNoOfFreshSWP = 0;
        AdvisorPreferenceVo advisorPrefrenceVo = new AdvisorPreferenceVo();       
        string path;
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            advisorPrefrenceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            advisorId = advisorVo.advisorId;
            int RMId = rmVo.RMId;
            customerId = customerVo.CustomerId;
            rmId = rmVo.RMId;
            bmID = rmVo.RMId;

            ErrorMessage.Visible = false;
            hdnRecordCount.Value = "1";
            if (!IsPostBack)
            {
                ddlSelectCutomer.Visible = false;
                lblSelectTypeOfCustomer.Visible = false;
                lblselectCustomer.Visible = false;
                txtIndividualCustomer.Visible = false;
                //trPager.Visible = false;
                dsSystematicMIS = systematicSetupBo.GetAllDropdownBinding(strAmcCode);
                BindDropDowns(path);
                BindAMCDropDown(dsSystematicMIS.Tables[0]);
                SchemeDropdown(dsSystematicMIS.Tables[1]);
                CategoryDropdown(dsSystematicMIS.Tables[2]);
                Session["ButtonGo"] = null;
                rquiredFieldValidatorIndivudialCustomer.Visible = false;
                DateTime fromDate = DateTime.Now.AddYears(-2);
                txtFrom.SelectedDate = fromDate;
                txtTo.SelectedDate = DateTime.Now;
                if (userType == "advisor")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                }
                else if (userType == "rm")
                {
                    trBranchRM.Visible = false;

                }
                if (userType == "bm")
                {
                    BindBranchForBMDropDown();
                    BindRMforBranchDropdown(0, bmID);
                }

            }



        }
        private void BindDropDowns(string path)
        {
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            //Binding  Systematic Type
            dtSystematicTransactionType = XMLBo.GetSystematicTransactionType(path);
            ddlSystematicType.DataSource = dtSystematicTransactionType;
            ddlSystematicType.DataTextField = "SystemationTypeCode";
            ddlSystematicType.DataValueField = "SystemationTypeCode";
            ddlSystematicType.DataBind();
            ddlSystematicType.Items.Insert(0, "All");
            ddlSystematicType.Items.Remove("STP");
        }
        private void BindAMCDropDown(DataTable dtAMC)
        {
            try
            {
                if (dtAMC != null)
                {
                    ddlAMC.DataSource = dtAMC;
                    ddlAMC.DataValueField = dtAMC.Columns["PA_AMCCode"].ToString();
                    ddlAMC.DataTextField = dtAMC.Columns["PA_AMCName"].ToString();
                    ddlAMC.DataBind();
                }
                ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void SchemeDropdown(DataTable dtScheme)
        {
            try
            {

                if (dtScheme.Rows.Count > 0)
                {
                    ddlScheme.DataSource = dtScheme;
                    ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                    ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                    ddlScheme.DataBind();
                }
                ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "Select"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void CategoryDropdown(DataTable dtCategory)
        {
            try
            {
                if (dtCategory != null)
                {
                    //dtCategory = dsSystematicMIS.Tables[2];
                    ddlCategory.DataSource = dtCategory;
                    ddlCategory.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                    ddlCategory.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                    ddlCategory.DataBind();
                }
                ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindBranchDropDown()
        {
            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlBranch.DataSource = ds;
                    ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindRMDropDown()
        {
            try
            {
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
                if (dt.Rows.Count > 0)
                {
                    ddlRM.DataSource = dt;
                    ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
                    ddlRM.DataTextField = dt.Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "2"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindRMDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindBranchForBMDropDown()
        {
            try
            {
                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, bmID, 0);
                if (ds != null)
                {
                    ddlBranch.DataSource = ds.Tables[1]; ;
                    ddlBranch.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {

            try
            {

                DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
                if (ds != null)
                {
                    ddlRM.DataSource = ds.Tables[0]; ;
                    ddlRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlRM.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserEQMIS.ascx:BindRMforBranchDropdown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlAMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC.SelectedIndex != 0)
            {
                strAmcCode = ddlAMC.SelectedValue.ToString();
                dsSystematicMIS = systematicSetupBo.GetAllDropdownBinding(strAmcCode);
                SchemeDropdown(dsSystematicMIS.Tables[1]);
            }
            else
            {
                ddlAMC.SelectedIndex = 0;
            }
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0);
            }
        }
        protected void ddlSelectCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectCustomer.SelectedItem.Value == "All Customer")
            {
                ddlSelectCutomer.Visible = false;
                lblSelectTypeOfCustomer.Visible = false;
                txtIndividualCustomer.Visible = false;
                lblselectCustomer.Visible = false;
                rquiredFieldValidatorIndivudialCustomer.Visible = false;

                ViewState["GroupHeadCustomers"] = null;
                ViewState["IndividualCustomers"] = null;
                //gvCalenderDetailView.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
            }
            if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer")
            {
                ddlSelectCutomer.Visible = true;
                lblSelectTypeOfCustomer.Visible = true;
                txtIndividualCustomer.Visible = true;
                txtIndividualCustomer.Text = string.Empty;
                lblselectCustomer.Visible = true;
                rquiredFieldValidatorIndivudialCustomer.Visible = true;
                ViewState["GroupHeadCustomers"] = null;
                ViewState["IndividualCustomers"] = null;
                ddlSelectCutomer.SelectedIndex = 0;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
            }
        }
        protected void ddlSelectCutomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIndividualCustomer.Text = string.Empty;
            hdnIndividualOrGroup.Value = ddlSelectCutomer.SelectedItem.Value;
            if (ddlSelectCutomer.SelectedItem.Value == "Group Head")
            {
                customerType = "GROUP";
                if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllGroupCustomers";
                    }
                    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMGroupCustomers";
                    }
                }

                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetBMParentCustomerNames";
                    }
                    if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllGroupCustomers";
                    }
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                    }
                    if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMGroupCustomers";
                    }
                }
            }
            else if (ddlSelectCutomer.SelectedItem.Value == "Individual")
            {
                txtIndividualCustomer.Visible = true;
                customerType = "IND";

                //rquiredFieldValidatorIndivudialCustomer.Visible = true;
                if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";

                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {

                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllIndividualCustomers";
                    }
                    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMIndividualCustomers";
                    }
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllIndividualCustomers";
                    }
                    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMIndividualCustomers";
                    }
                }
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            Session["ButtonGo"] = "buttonClicked";
            ViewState["StartDate"] = null;
            ViewState["EndDate"] = null;
            ViewState["SystematicTypeDropDown"] = null;
            ViewState["AMCDropDown"] = null;
            ViewState["CategoryDropDown"] = null;
            ViewState["SchemeDropDown"] = null;
            ViewState["GroupHeadCustomers"] = null;
            ViewState["IndividualCustomers"] = null;
            ViewState["CustomerId"] = null;
            SetParameter();
            CreateCalenderViewSummaryDataTable();
        }
        private void SetParameter()
        {
            if ((ddlSelectCustomer.SelectedItem.Value == "All Customer") && (userType == "advisor"))
            {
                hdnCustomerId.Value = "0";
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnAll.Value = "0";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "3";
                }

            }
            else if (ddlSelectCustomer.SelectedItem.Value == "All Customer" && userType == "rm")
            {
                hdnrmId.Value = rmVo.RMId.ToString();
                hdnAll.Value = "0";

            }
            else if (ddlSelectCustomer.SelectedItem.Value == "All Customer" && userType == "bm")
            {
                hdnCustomerId.Value = "0";
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {

                    hdnbranchheadId.Value = bmID.ToString();
                    hdnAll.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchheadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchheadId.Value = bmID.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchheadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "3";
                }
            }


            if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer" && userType == "advisor")
            {

                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnAll.Value = "4";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = "0";
                    hdnAll.Value = "5";

                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {

                    hdnbranchId.Value = "0";
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "6";

                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {

                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "7";
                }
            }
            else if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer" && userType == "rm")
            {
                hdnAll.Value = "1";
            }


            else if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer" && userType == "bm")
            {

                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnAll.Value = "4";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = "0";
                    hdnAll.Value = "5";

                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {

                    hdnbranchId.Value = "0";
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "6";

                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {

                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "7";
                }
            }

            // Check Start Date and EndDate Selection.. 
            if (ddlDateFilter.SelectedIndex == 0)
            {
                hdnendDate.Value = "";
                hdnstartdate.Value = "StartDate";
                ViewState["StartDate"] = hdnstartdate.Value;
            }
            else if (ViewState["StartDate"] != null)
            {
                hdnendDate.Value = "";
                hdnstartdate.Value = "StartDate";
            }

            if (ddlDateFilter.SelectedIndex != 0)
            {
                hdnstartdate.Value = "";
                hdnendDate.Value = "EndDate";
                ViewState["EndDate"] = hdnendDate.Value;
            }
            else if (ViewState["EndDate"] != null)
            {
                hdnstartdate.Value = "";
                hdnendDate.Value = "EndDate";
            }

            if (hdnCustomerId.Value != "")
            {
                ViewState["CustomerId"] = hdnCustomerId.Value;
            }
            else if (ViewState["CustomerId"] != null)
            {
                hdnCustomerId.Value = ViewState["CustomerId"].ToString();
            }

            if (hdnIndividualOrGroup.Value == "Group Head")
            {
                isIndividualOrGroup = 1;
                ViewState["GroupHeadCustomers"] = isIndividualOrGroup.ToString();
            }
            else if (ViewState["GroupHeadCustomers"] != null)
            {
                isIndividualOrGroup = int.Parse(ViewState["GroupHeadCustomers"].ToString());
            }

            if (hdnIndividualOrGroup.Value == "Individual")
            {
                isIndividualOrGroup = 2;
                ViewState["IndividualCustomers"] = isIndividualOrGroup.ToString();
            }
            else if (ViewState["IndividualCustomers"] != null)
            {
                isIndividualOrGroup = int.Parse(ViewState["IndividualCustomers"].ToString());
            }



            if (ddlAMC.SelectedIndex != 0)
            {
                hdnamcCode.Value = ddlAMC.SelectedValue;
                ViewState["AMCDropDown"] = hdnamcCode.Value;
            }
            else if (ViewState["AMCDropDown"] != null)
            {
                hdnamcCode.Value = ViewState["AMCDropDown"].ToString();
            }
            else
            {
                hdnamcCode.Value = "";
            }



            if (ddlScheme.SelectedIndex != 0)
            {
                hdnschemeCade.Value = ddlScheme.SelectedValue;
                ViewState["SchemeDropDown"] = hdnschemeCade.Value;
            }
            else if (ViewState["SchemeDropDown"] != null)
            {
                hdnschemeCade.Value = ViewState["SchemeDropDown"].ToString();
            }
            else
            {
                hdnschemeCade.Value = "";
            }


            if (ddlCategory.SelectedIndex != 0)
            {
                hdnCategory.Value = ddlCategory.SelectedValue;
                ViewState["CategoryDropDown"] = hdnCategory.Value;
            }
            else if (ViewState["CategoryDropDown"] != null)
            {
                hdnCategory.Value = ViewState["CategoryDropDown"].ToString();
            }
            else
            {
                hdnCategory.Value = "";
            }

            if (ddlSystematicType.SelectedIndex != 0)
            {
                hdnSystematicType.Value = ddlSystematicType.SelectedValue;
                ViewState["SystematicTypeDropDown"] = hdnSystematicType.Value;
            }
            else if (ViewState["SystematicTypeDropDown"] != null)
            {
                hdnSystematicType.Value = ViewState["SystematicTypeDropDown"].ToString();
            }
            else
            {
                hdnSystematicType.Value = "";
            }


            if (txtFrom.SelectedDate.ToString() != "")
            {
                hdnFromDate.Value = txtFrom.SelectedDate.ToString();
                ViewState["txtFromDate"] = txtFrom.SelectedDate.ToString();
            }
            else if (ViewState["txtFromDate"].ToString() != null)
            {
                hdnFromDate.Value = DateTime.Parse(ViewState["txtFromDate"].ToString()).ToString();
            }
            else
                hdnFromDate.Value = DateTime.MinValue.ToString();



            if (txtTo.SelectedDate.ToString() != "")
            {
                hdnTodate.Value = txtTo.SelectedDate.ToString();
                ViewState["txtToDate"] = txtTo.SelectedDate.ToString();
            }
            else if (ViewState["txtToDate"].ToString() != null)
            {
                hdnTodate.Value = DateTime.Parse(ViewState["txtToDate"].ToString()).ToString();
            }
            else
                hdnTodate.Value = DateTime.MinValue.ToString();


            if (hdnbranchheadId.Value == "")
                hdnbranchheadId.Value = "0";

            if (hdnbranchId.Value == "")
                hdnbranchId.Value = "0";

            if (hdnCustomerId.Value == "")
                hdnCustomerId.Value = "0";

            if (hdnadviserId.Value == "")
                hdnadviserId.Value = "0";

            if (hdnrmId.Value == "")
                hdnrmId.Value = "0";



        }
        protected void CreateCalenderViewSummaryDataTable()
        {
            DataSet dsCalenderSummaryView;
            DataTable dtSIPDetails;
            dsCalenderSummaryView = systematicSetupBo.GetCalenderSummaryView(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnCustomerId.Value), int.Parse(hdnbranchheadId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnAll.Value), hdnCategory.Value, hdnSystematicType.Value, hdnamcCode.Value, hdnschemeCade.Value, DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnTodate.Value), isIndividualOrGroup, hdnstartdate.Value, hdnendDate.Value,int.Parse(ddlPortfolioGroup.SelectedItem.Value.ToString()));
            //dtSIPDetails = dsBindGvSystematicMIS.Tables[2];
            dtSIPDetails = dsCalenderSummaryView.Tables[0];
            if (dtSIPDetails.Rows.Count > 0)
            {
                DataTable dtCalenderSymmary = new DataTable();
                dtCalenderSymmary.Columns.Add("Year");
                dtCalenderSymmary.Columns.Add("Month");
                dtCalenderSymmary.Columns.Add("FinalMonth");
                dtCalenderSymmary.Columns.Add("NoOfSIP", typeof(Int16));
                dtCalenderSymmary.Columns.Add("SIPAmount", typeof(Decimal));
                dtCalenderSymmary.Columns.Add("NoOfFreshSIP", typeof(Int16));
                //dtCalenderSymmary.Columns.Add("SWPDate");            
                dtCalenderSymmary.Columns.Add("NoOfSWP", typeof(Int16));
                dtCalenderSymmary.Columns.Add("SWPAmount", typeof(Decimal));
                dtCalenderSymmary.Columns.Add("NoOfFreshSWP", typeof(Int16));
                DataRow drCalenderSummary;
                DateTime startSipSwpDate = DateTime.Parse(dtSIPDetails.Rows[0]["StartDate"].ToString());
                DateTime endSipSwpDate = DateTime.Parse(dsCalenderSummaryView.Tables[1].Rows[0]["LastDate"].ToString());
                //DateTime endSipSwpDate = DateTime.Parse(dtSIPDetails.Rows[dtSIPDetails.Rows.Count - 1]["EndDate"].ToString());

                int startMonth = startSipSwpDate.Month;
                int startYear = startSipSwpDate.Year;
                int endMonth = endSipSwpDate.Month;
                int endYear = endSipSwpDate.Year;
                DataRow[] drYearWiseSIPDetails;

                //double monthlyTotalSipAmount = 0;
                double monthlyTotalSwpAmount = 0;
                int newSipCount = 0;
                int runningSipCount = 0;
                int newSwpCount = 0;
                int runningSwpCount = 0;



                while (startSipSwpDate <= endSipSwpDate)
                {
                    for (int month = 1; month <= 12; month++)
                    {
                        drCalenderSummary = dtCalenderSymmary.NewRow();

                        drCalenderSummary["Year"] = startYear;
                        drCalenderSummary["Month"] = month;
                        drCalenderSummary["FinalMonth"] = GetMonth(month);

                        foreach (DataRow dr in dtSIPDetails.Rows)
                        {
                            if (DateTime.Parse(dr["StartDate"].ToString()).Month == month && dr["TypeCode"].ToString().Trim() == "SIP" && DateTime.Parse(dr["StartDate"].ToString()).Year == startSipSwpDate.Year)
                            {
                                if (startYear >= DateTime.Parse(dr["StartDate"].ToString()).Year && startYear <= DateTime.Parse(dr["EndDate"].ToString()).Year)
                                {

                                }
                                newSipCount++;

                            }
                            if (DateTime.Parse(dr["StartDate"].ToString()).Month == month && dr["TypeCode"].ToString().Trim() == "SWP" && DateTime.Parse(dr["StartDate"].ToString()).Year == startSipSwpDate.Year)
                            {
                                if (startYear >= DateTime.Parse(dr["StartDate"].ToString()).Year && startYear <= DateTime.Parse(dr["EndDate"].ToString()).Year)
                                {

                                }
                                newSwpCount++;
                            }

                            DateTime tempStartSipSwp = DateTime.Parse(dr["StartDate"].ToString());
                            DateTime tempEndSipSwp = DateTime.Parse(dr["EndDate"].ToString());
                            //**************************DAILY***********************

                            if (dr["FrequencyCode"].ToString().Trim() == "DA")
                            {
                                while (tempStartSipSwp <= tempEndSipSwp)
                                {
                                    if (tempStartSipSwp.Year == startYear && tempStartSipSwp.Month == month)
                                    {
                                        if (dr["TypeCode"].ToString().Trim() == "SIP")
                                        {
                                            monthlyTotalSipAmount += double.Parse(dr["Amount"].ToString());
                                            runningSipCount++;
                                        }
                                        else if (dr["TypeCode"].ToString().Trim() == "SWP")
                                        {
                                            monthlyTotalSwpAmount += double.Parse(dr["Amount"].ToString());
                                            runningSwpCount++;
                                        }
                                    }
                                    tempStartSipSwp = tempStartSipSwp.AddDays(1);
                                }
                            }

                            //**************************DAILY***********************                                

                            //**************************WEEKLY***********************

                            if (dr["FrequencyCode"].ToString().Trim() == "WK")
                            {
                                while (tempStartSipSwp <= tempEndSipSwp)
                                {
                                    if (tempStartSipSwp.Year == startYear && tempStartSipSwp.Month == month)
                                    {
                                        if (dr["TypeCode"].ToString().Trim() == "SIP")
                                        {
                                            monthlyTotalSipAmount += double.Parse(dr["Amount"].ToString());
                                            runningSipCount++;
                                        }
                                        else if (dr["TypeCode"].ToString().Trim() == "SWP")
                                        {
                                            monthlyTotalSwpAmount += double.Parse(dr["Amount"].ToString());
                                            runningSwpCount++;
                                        }
                                    }
                                    tempStartSipSwp = tempStartSipSwp.AddDays(7);
                                }
                            }

                            //**************************WEEKLY***********************

                            //**************************MONTHLY***********************


                            if (dr["FrequencyCode"].ToString().Trim() == "MN")
                            {
                                while (tempStartSipSwp <= tempEndSipSwp)
                                {
                                    if (tempStartSipSwp.Year == startYear && tempStartSipSwp.Month == month)
                                    {
                                        if (dr["TypeCode"].ToString().Trim() == "SIP")
                                        {
                                            monthlyTotalSipAmount += double.Parse(dr["Amount"].ToString());
                                            runningSipCount++;
                                        }
                                        else if (dr["TypeCode"].ToString().Trim() == "SWP")
                                        {
                                            monthlyTotalSwpAmount += double.Parse(dr["Amount"].ToString());
                                            runningSwpCount++;
                                        }
                                    }
                                    tempStartSipSwp = tempStartSipSwp.AddMonths(1);
                                }
                            }

                            //**************************MONTHLY***********************


                            //**************************QUATERLY***********************


                            if (dr["FrequencyCode"].ToString().Trim() == "QT")
                            {
                                while (tempStartSipSwp <= tempEndSipSwp)
                                {
                                    if (tempStartSipSwp.Year == startYear && tempStartSipSwp.Month == month)
                                    {
                                        if (dr["TypeCode"].ToString().Trim() == "SIP")
                                        {
                                            monthlyTotalSipAmount += double.Parse(dr["Amount"].ToString());
                                            runningSipCount++;
                                        }
                                        else if (dr["TypeCode"].ToString().Trim() == "SWP")
                                        {
                                            monthlyTotalSwpAmount += double.Parse(dr["Amount"].ToString());
                                            runningSwpCount++;
                                        }
                                    }
                                    tempStartSipSwp = tempStartSipSwp.AddMonths(3);
                                }
                            }

                            //**************************QUATERLY***********************


                            //**************************HALFYEARLY***********************


                            if (dr["FrequencyCode"].ToString().Trim() == "HY")
                            {
                                while (tempStartSipSwp <= tempEndSipSwp)
                                {
                                    if (tempStartSipSwp.Year == startYear && tempStartSipSwp.Month == month)
                                    {
                                        if (dr["TypeCode"].ToString().Trim() == "SIP")
                                        {
                                            monthlyTotalSipAmount += double.Parse(dr["Amount"].ToString());
                                            runningSipCount++;
                                        }
                                        else if (dr["TypeCode"].ToString().Trim() == "SWP")
                                        {
                                            monthlyTotalSwpAmount += double.Parse(dr["Amount"].ToString());
                                            runningSwpCount++;
                                        }
                                    }
                                    tempStartSipSwp = tempStartSipSwp.AddMonths(6);
                                }
                            }

                            //**************************HALFYEARLY***********************

                        }

                        drCalenderSummary["NoOfSIP"] = runningSipCount;
                        drCalenderSummary["SIPAmount"] = monthlyTotalSipAmount;
                        drCalenderSummary["NoOfFreshSIP"] = newSipCount;

                        drCalenderSummary["NoOfSWP"] = runningSwpCount;
                        drCalenderSummary["SWPAmount"] = monthlyTotalSwpAmount;
                        drCalenderSummary["NoOfFreshSWP"] = newSwpCount;

                        dtCalenderSymmary.Rows.Add(drCalenderSummary);

                        runningSipCount = 0;
                        monthlyTotalSipAmount = 0;
                        newSipCount = 0;
                        runningSwpCount = 0;
                        monthlyTotalSwpAmount = 0;
                        newSwpCount = 0;

                    }

                    startSipSwpDate = startSipSwpDate.AddYears(1);
                    startYear++;
                }

                //reptCalenderSummaryView.MasterTableView.ExpandCollapseColumn.Display = false;


                //GridGroupByExpression expression1 = GridGroupByExpression.Parse("Year [year] Group By Year");
                ////this.CustomizeExpression(expression1);
                //this.reptCalenderSummaryView.MasterTableView.GroupByExpressions.Add(expression1);
                // Get the DefaultViewManager of a DataTable.
                DataView calenderView = dtCalenderSymmary.DefaultView;
                // By default, the first column sorted ascending.
                calenderView.Sort = "Year DESC";

                reptCalenderSummaryView.DataSource = calenderView;
                reptCalenderSummaryView.PageSize = advisorPrefrenceVo.GridPageSize;
                reptCalenderSummaryView.DataBind();
                //reptCalenderSummaryView.Columns[0].Visible = false;
                if (dtCalenderSymmary.Rows.Count > 0)
                {
                    reptCalenderSummaryView.Visible = true;
                    tblMessage.Visible = false;
                    ErrorMessage.Visible = false;
                }
            }
            else
            {
                reptCalenderSummaryView.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
                //trPager.Visible = false;
            }


        }
        private String GetMonth(int monthCode)
        {
            String finalMonth = "";
            switch (monthCode)
            {
                case 1:
                    finalMonth = "January";
                    break;
                case 2:
                    finalMonth = "February";
                    break;
                case 3:
                    finalMonth = "March";
                    break;
                case 4:
                    finalMonth = "April";
                    break;
                case 5:
                    finalMonth = "May";
                    break;
                case 6:
                    finalMonth = "June";
                    break;
                case 7:
                    finalMonth = "July";
                    break;
                case 8:
                    finalMonth = "August";
                    break;
                case 9:
                    finalMonth = "September";
                    break;
                case 10:
                    finalMonth = "October";
                    break;
                case 11:
                    finalMonth = "November";
                    break;
                case 12:
                    finalMonth = "December";
                    break;

            }
            return finalMonth;
        }
        public static int GetDaysInMonth(int month, int year)
        {
            if (month < 1 || month > 12)
            {
                throw new System.ArgumentOutOfRangeException("month", month, "month must be between 1 and 12");
            }
            if (1 == month || 3 == month || 5 == month || 7 == month || 8 == month || 10 == month || 12 == month)
            {
                return 31;
            }
            else if (2 == month)
            {
                // Check for leap year
                if (0 == (year % 4))
                {
                    // If date is divisible by 400, it's a leap year.
                    // Otherwise, if it's divisible by 100 it's not.
                    if (0 == (year % 400))
                    {
                        return 29;
                    }
                    else if (0 == (year % 100))
                    {
                        return 28;
                    }

                    // Divisible by 4 but not by 100 or 400
                    // so it leaps
                    return 29;
                }
                // Not a leap year
                return 28;
            }
            return 30;
        }

        protected void reptCalenderSummaryView_PreRender(object sender, EventArgs e)
        {
            //HideExpandColumnRecursive(reptCalenderSummaryView.MasterTableView);
            foreach (GridColumn column in reptCalenderSummaryView.MasterTableView.RenderColumns)
            {
                if (column is GridGroupSplitterColumn)
                {
                    column.HeaderStyle.Width = Unit.Pixel(1);
                    column.ItemStyle.Width = Unit.Pixel(1);
                    column.Resizable = false;
                }
            }
            reptCalenderSummaryView.Rebind();
        }
    }
}