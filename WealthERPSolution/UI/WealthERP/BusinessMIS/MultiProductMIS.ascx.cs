using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using VoUser;
using BoUploads;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using VOAssociates;
using BOAssociates;
using Telerik.Web.UI;


namespace WealthERP.BusinessMIS
{
    public partial class MultiProductMIS : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        CustomerBo customerBo = new CustomerBo();
        InsuranceBo insuranceBo = new InsuranceBo();
        AssociatesVO associatesVo = new AssociatesVO();

        int advisorId = 0;
        int customerId = 0;
        String userType;
        int rmId = 0;
        int bmID = 0;
        int AgentId = 0;
        int IsAsociate;
        int all = 0;
        int branchId = 0;
        int branchHeadId = 0;
        int isGroup;

        string customerType = string.Empty;

        DataRow drMultiProduct;

        AssetBo assetBo = new AssetBo();
        DataSet dsGrpAssetNetHoldings = new DataSet();
        DataTable dtGrpAssetNetHoldings = new DataTable();
        DataRow drNetHoldings;
        int portfolioId;
        string asset;

        DataSet dsInsuranceDetails = new DataSet();
        DataRow drGeneralInsurance;
        DataRow drLifeInsurance;
        string pageType;

        protected void Page_Load(object sender, EventArgs e)
        {
            trPanel1.Visible = false;
            trLifeInsuranceMIS.Visible = false;
            trGeneralInsuranceMis.Visible = false;
            trFixedIncomeMIS.Visible = false;
            trExportFilteredData.Visible = false;
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
                userType = "associates";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            advisorId = advisorVo.advisorId;
            int RMId = rmVo.RMId;
            customerId = customerVo.CustomerId;
            rmId = rmVo.RMId;
            bmID = rmVo.RMId;
            AgentId = associatesVo.AAC_AdviserAgentId;

            if (!IsPostBack)
            {
                if (Request.QueryString["action"] != null)
                {
                    if (Request.QueryString["action"] == "LI")
                    {
                        pageType = "LI";
                        lblpageHeader.Text = "Life Insurance MIS";
                    }
                    else if (Request.QueryString["action"] == "GI")
                    {
                        pageType = "GI";
                        lblpageHeader.Text = "General Insurance MIS";
                    }
                    else if (Request.QueryString["action"] == "FI")
                    {
                        pageType = "FI";
                        lblpageHeader.Text = "Fixed Income MIS";
                    }
                }
                lblErrorMsg.Visible = false;
                ddlCustomerType.Visible = false;
                lblSelectTypeOfCustomer.Visible = false;
                trCustomerSearch.Visible = false;
                //lblselectCustomer.Visible = false;
                //txtIndividualCustomer.Visible = false;              
                rquiredFieldValidatorIndivudialCustomer.Visible = false;
                btnMultiProductMIS.Visible = false;
                btnFixedIncomeMIS.Visible = false;
                btnGeneralInsurance.Visible = false;
                btnLifeInsurance.Visible = false;
                GridsVisibility();

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
                if (userType == "associates")
                {
                    trBranchRM.Visible = false;

                }
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
            GridsVisibility();
        }

        protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnCustomerId.Value.ToString().Trim()))
            {
                customerId = int.Parse(hdnCustomerId.Value);
                GridsVisibility();
                //customerVo = customerBo.GetCustomer(int.Parse(txtIndividualCustomer_autoCompleteExtender.ContextKey));
            }
        }

        protected void GridsVisibility()
        {
            trMultiProduct.Visible = false;
            trLifeInsurance.Visible = false;
            trGeneralInsurance.Visible = false;
            trFixedIncome.Visible = false;

            //rgvFixedIncomeMIS.Visible = false;
            //rgvGeneralInsurance.Visible = false;
            //rgvLifeInsurance.Visible = false;
            //rgvMultiProductMIS.Visible = false;
            trLabelMessage.Visible = false;
            trMISType.Visible = false;
            trWrongCustomer.Visible = false;
            lblMISType.Visible = false;
            //lblWrongCustomer.Visible = false;
        }

        protected void ddlCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIndividualCustomer.Text = string.Empty;
            hdnIndividualOrGroup.Value = ddlCustomerType.SelectedItem.Value;
            rquiredFieldValidatorIndivudialCustomer.Visible = true;
            if (ddlCustomerType.SelectedItem.Value == "0")
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
            else if (ddlCustomerType.SelectedItem.Value == "1")
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

        protected void ddlSelectCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectCustomer.SelectedItem.Value == "All Customer")
            {
                ddlCustomerType.Visible = false;
                lblSelectTypeOfCustomer.Visible = false;
                //txtIndividualCustomer.Visible = false;
                //lblselectCustomer.Visible = false;
                trCustomerSearch.Visible = false;
                rquiredFieldValidatorIndivudialCustomer.Visible = false;
                ddlCustomerType.SelectedIndex = 0;
                GridsVisibility();
            }
            if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer")
            {
                ddlCustomerType.Visible = true;
                lblSelectTypeOfCustomer.Visible = true;
                //txtIndividualCustomer.Visible = true;
                txtIndividualCustomer.Text = string.Empty;
                //lblselectCustomer.Visible = true;
                trCustomerSearch.Visible = true;
                rquiredFieldValidatorIndivudialCustomer.Visible = true;

                //if (ddlCustomerType.SelectedIndex != 0)
                //{
                //    rquiredFieldValidatorIndivudialCustomer.Visible = true;
                //}
                ddlCustomerType.SelectedIndex = 0;
                GridsVisibility();
            }
        }

        public void SetParametersforDifferentRoles()
        {
            if (ddlCustomerType.SelectedValue == "0")
                isGroup = 0;
            else if (ddlCustomerType.SelectedValue == "1")
                isGroup = 1;

            if (ddlBranch.SelectedValue != "0" && ddlBranch.SelectedValue != "")
            {
                branchId = int.Parse(ddlBranch.SelectedValue);
            }
            else
            {
                branchId = 0;
            }
            if (ddlRM.SelectedValue != "0" && ddlRM.SelectedValue != "")
                rmId = int.Parse(ddlRM.SelectedValue);
            else
                rmId = 0;

            if (txtIndividualCustomer.Text != "")
            {
                if (hdnCustomerId.Value != "")
                {
                    customerId = int.Parse(hdnCustomerId.Value);
                }
            }

            if (txtIndividualCustomer.Text != "" && hdnCustomerId.Value == "")
            {
                lblWrongCustomer.Text = "selected customer does not exist";
                lblWrongCustomer.Visible = true;
            }
            else
            {
                lblWrongCustomer.Text = "";
                lblWrongCustomer.Visible = false;

                if (userType == "advisor")
                {
                    lblBranch.Visible = true;
                    ddlBranch.Visible = true;
                    lblRM.Visible = true;
                    ddlRM.Visible = true;

                    //for all branch all RM and particular customer
                    if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0 && ddlSelectCustomer.SelectedIndex != 0)
                    {
                        advisorId = 0;
                        branchId = 0;
                        rmId = 0;
                        branchHeadId = 0;
                    }

                        //for all branch all customers and particular RM
                    else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0 && ddlSelectCustomer.SelectedIndex == 0)
                    {
                        advisorId = 0;
                        branchId = 0;
                        customerId = 0;
                        branchHeadId = 0;
                    }
                    //for particular branch particular customer and all RM
                    else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex == 0 && ddlSelectCustomer.SelectedIndex != 0)
                    {
                        advisorId = 0;
                        branchId = 0;
                        rmId = 0;
                        branchHeadId = 0;
                    }
                    //for all branch all customers and all rm
                    else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0 && ddlSelectCustomer.SelectedIndex == 0)
                    {
                        branchId = 0;
                        rmId = 0;
                        customerId = 0;
                    }
                    //for particular branch and all rm and all customers
                    else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex == 0 && ddlSelectCustomer.SelectedIndex == 0)
                    {
                        advisorId = 0;
                        rmId = 0;
                        customerId = 0;
                        branchHeadId = 0;
                    }
                    //for particular branch ,particular rm and all customers
                    else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0 && ddlSelectCustomer.SelectedIndex == 0)
                    {
                        advisorId = 0;
                        customerId = 0;
                        branchHeadId = 0;
                    }
                    //for all branch ,particular rm and particular customer
                    else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0 && ddlSelectCustomer.SelectedIndex != 0)
                    {
                        advisorId = 0;
                        branchId = 0;
                        rmId = 0;
                        branchHeadId = 0;
                        isGroup = 0;
                    }
                    //for particular branch ,particular rm and particular customer
                    else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0 && ddlSelectCustomer.SelectedIndex != 0)
                    {
                        advisorId = 0;
                        branchId = 0;
                        rmId = 0;
                        branchHeadId = 0;
                    }
                }
                else if (userType == "rm")
                {
                    lblBranch.Visible = false;
                    ddlBranch.Visible = false;
                    lblRM.Visible = false;
                    ddlRM.Visible = false;
                    if (ddlSelectCustomer.SelectedIndex == 0)
                    {
                        advisorId = 0;
                        branchId = 0;
                        customerId = 0;
                        branchHeadId = 0;
                    }
                    else if (ddlSelectCustomer.SelectedIndex != 0)
                    {
                        advisorId = 0;
                        branchId = 0;
                        branchHeadId = 0;
                    }

                }
                else if (userType == "bm")
                {
                    branchHeadId = bmID;
                    //for all branch all RM and particular customer
                    if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0 && ddlSelectCustomer.SelectedIndex != 0)
                    {
                        advisorId = 0;
                        branchId = 0;
                        rmId = 0;
                        branchHeadId = 0;
                    }

                        //for all branch all customers and particular RM
                    else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0 && ddlSelectCustomer.SelectedIndex == 0)
                    {
                        advisorId = 0;
                        branchId = 0;
                        customerId = 0;
                    }
                    //for particular branch particular customer and all RM
                    else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex == 0 && ddlSelectCustomer.SelectedIndex != 0)
                    {
                        advisorId = 0;
                        branchId = 0;
                        rmId = 0;
                        branchHeadId = 0;
                    }
                    //for all branch all customers and all rm
                    else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0 && ddlSelectCustomer.SelectedIndex == 0)
                    {
                        advisorId = 0;
                        branchId = 0;
                        rmId = 0;
                        customerId = 0;
                    }
                    //for particular branch and all rm and all customers
                    else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex == 0 && ddlSelectCustomer.SelectedIndex == 0)
                    {
                        advisorId = 0;
                        rmId = 0;
                        customerId = 0;
                        branchHeadId = 0;
                    }
                    //for particular branch ,particular rm and all customers
                    else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0 && ddlSelectCustomer.SelectedIndex == 0)
                    {
                        advisorId = 0;
                        customerId = 0;
                        branchHeadId = 0;
                    }
                    //for all branch ,particular rm and particular customer
                    else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0 && ddlSelectCustomer.SelectedIndex != 0)
                    {
                        advisorId = 0;
                        branchId = 0;
                        rmId = 0;
                        branchHeadId = 0;
                    }
                    //for particular branch ,particular rm and particular customer
                    else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0 && ddlSelectCustomer.SelectedIndex != 0)
                    {
                        advisorId = 0;
                        branchId = 0;
                        rmId = 0;
                        branchHeadId = 0;
                    }
                }
                else if (userType == "associates")
                {
                    AgentId = associatesVo.AAC_AdviserAgentId;
                    trBranchRM.Visible = false;
                }
            }
        }

        public void bindRadGrid_rgvMultiProductMIS()
        {
            int tempID = 0;
            
            try
            {
                SetParametersforDifferentRoles();
                if (lblWrongCustomer.Text != "selected customer does not exist")
                {
                    dsGrpAssetNetHoldings = insuranceBo.GetAllProductMIS(advisorId, branchId, rmId, branchHeadId, customerId,AgentId,IsAsociate,isGroup);

                    if (dsGrpAssetNetHoldings.Tables[0].Rows.Count == 0)
                    {
                        trPanel1.Visible = false;
                        trExportFilteredData.Visible = false;
                        lblErrorMsg.Text = "No records found for Multi-Product MIS";
                        lblErrorMsg.Visible = true;
                        trMultiProduct.Visible = false;
                        lblMISType.Visible = false;
                        trLabelMessage.Visible = false;
                        //rgvMultiProductMIS.Visible = false;
                        btnMultiProductMIS.Visible = false;
                        btnFixedIncomeMIS.Visible = false;
                        btnGeneralInsurance.Visible = false;
                        btnLifeInsurance.Visible = false;
                    }
                    else
                    {
                        trPanel1.Visible = true;
                        trExportFilteredData.Visible = true;
                        lblErrorMsg.Visible = false;
                        lblMISType.Text = "Multi-Product MIS";
                        trMISType.Visible = true;
                        lblMISType.Visible = true;

                        dtGrpAssetNetHoldings.Columns.Add("Customer_Name");
                        dtGrpAssetNetHoldings.Columns.Add("RmName");
                        dtGrpAssetNetHoldings.Columns.Add("BranchName");
                        dtGrpAssetNetHoldings.Columns.Add("Equity", typeof(double));
                        dtGrpAssetNetHoldings.Columns.Add("Mutual_Fund", typeof(double));
                        dtGrpAssetNetHoldings.Columns.Add("Fixed_Income", typeof(double));
                        dtGrpAssetNetHoldings.Columns.Add("Government_Savings", typeof(double));
                        dtGrpAssetNetHoldings.Columns.Add("Property", typeof(double));
                        dtGrpAssetNetHoldings.Columns.Add("Pension_and_Gratuity", typeof(double));
                        dtGrpAssetNetHoldings.Columns.Add("Personal_Assets", typeof(double));
                        dtGrpAssetNetHoldings.Columns.Add("Gold_Assets", typeof(double));
                        dtGrpAssetNetHoldings.Columns.Add("Collectibles", typeof(double));
                        dtGrpAssetNetHoldings.Columns.Add("Cash_and_Savings", typeof(double));
                        dtGrpAssetNetHoldings.Columns.Add("C_CustomerId");
                        dtGrpAssetNetHoldings.Columns.Add("TotalAUM",typeof(double));
                        
                        double totalAUM1 = 0;
                        foreach (DataRow dr in dsGrpAssetNetHoldings.Tables[0].Rows)
                        {
                            totalAUM1 = 0;
                            if (int.Parse(dr["C_CustomerId"].ToString()) != tempID)
                            {                                                  
                                tempID = int.Parse(dr["C_CustomerId"].ToString());
                                drNetHoldings = dtGrpAssetNetHoldings.NewRow();

                                drNetHoldings["C_CustomerId"] = dr["C_CustomerId"].ToString();
                                drNetHoldings["RmName"] = dr["RmName"].ToString();
                                drNetHoldings["BranchName"] = dr["BranchName"].ToString();
                                drNetHoldings["Equity"] = 0;
                                drNetHoldings["Mutual_Fund"] = 0;
                                drNetHoldings["Fixed_Income"] = 0;
                                drNetHoldings["Government_Savings"] = 0;
                                drNetHoldings["Property"] = 0;
                                drNetHoldings["Pension_and_Gratuity"] = 0;
                                drNetHoldings["Personal_Assets"] = 0;
                                drNetHoldings["Gold_Assets"] = 0;
                                drNetHoldings["Collectibles"] = 0;
                                drNetHoldings["Cash_and_Savings"] = 0;                               
                                

                                drNetHoldings[0] = dr["Customer_Name"].ToString();
                                if (dr["AssetType"].ToString() == "DE")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Equity"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Equity"].ToString());
                                    }
                                    else
                                        drNetHoldings["Equity"] = "N/A";

                                else if (dr["AssetType"].ToString() == "MF")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Mutual_Fund"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Mutual_Fund"].ToString());
                                    }
                                    else
                                        drNetHoldings["Mutual_Fund"] = "N/A";
                                else if (dr["AssetType"].ToString() == "FI")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Fixed_Income"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Fixed_Income"].ToString());
                                    }
                                    else
                                        drNetHoldings["Fixed_Income"] = "N/A";
                                else if (dr["AssetType"].ToString() == "GS")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Government_Savings"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Government_Savings"].ToString());
                                    }
                                    else
                                        drNetHoldings["Government_Savings"] = "N/A";
                                else if (dr["AssetType"].ToString() == "PR")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Property"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Property"].ToString());
                                    }
                                    else
                                        drNetHoldings["Property"] = "N/A";
                                else if (dr["AssetType"].ToString() == "PG")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Pension_and_Gratuity"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Pension_and_Gratuity"].ToString());
                                    }
                                    else
                                        drNetHoldings["Pension_and_Gratuity"] = "N/A";
                                else if (dr["AssetType"].ToString() == "PI")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Personal_Assets"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Personal_Assets"].ToString());
                                    }
                                    else
                                        drNetHoldings["Personal_Assets"] = "N/A";
                                else if (dr["AssetType"].ToString() == "GD")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Gold_Assets"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Gold_Assets"].ToString());
                                    }
                                    else
                                        drNetHoldings["Gold_Assets"] = "N/A";
                                else if (dr["AssetType"].ToString() == "CL")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Collectibles"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Collectibles"].ToString());
                                    }
                                    else
                                        drNetHoldings["Collectibles"] = "N/A";
                                else if (dr["AssetType"].ToString() == "CS")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Cash_and_Savings"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Cash_and_Savings"].ToString());
                                    }
                                    else
                                        drNetHoldings["Cash_and_Savings"] = "N/A";
                                //drNetHoldings["TotalAUM"] = totalAUM;
                                drNetHoldings["TotalAUM"] = totalAUM1;
                                dtGrpAssetNetHoldings.Rows.Add(drNetHoldings);
                            }
                            else
                            {                                
                                if (dr["AssetType"].ToString() == "DE")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                       drNetHoldings["Equity"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                       totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Equity"].ToString());
                                    }
                                    else
                                        drNetHoldings["Equity"] = "N/A";
                                else if (dr["AssetType"].ToString() == "MF")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Mutual_Fund"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Mutual_Fund"].ToString());
                                    }
                                    else
                                        drNetHoldings["Mutual_Fund"] = "N/A";
                                else if (dr["AssetType"].ToString() == "FI")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Fixed_Income"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Fixed_Income"].ToString());
                                    }
                                    else
                                        drNetHoldings["Fixed_Income"] = "N/A";
                                else if (dr["AssetType"].ToString() == "GS")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Government_Savings"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Government_Savings"].ToString());
                                    }
                                    else
                                        drNetHoldings["Government_Savings"] = "N/A";
                                else if (dr["AssetType"].ToString() == "PR")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Property"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Property"].ToString());
                                    }
                                    else
                                        drNetHoldings["Property"] = "N/A";
                                else if (dr["AssetType"].ToString() == "PG")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Pension_and_Gratuity"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Pension_and_Gratuity"].ToString());
                                    }
                                    else
                                        drNetHoldings["Pension_and_Gratuity"] = "N/A";
                                else if (dr["AssetType"].ToString() == "PI")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Personal_Assets"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Personal_Assets"].ToString());
                                    }
                                    else
                                        drNetHoldings["Personal_Assets"] = "N/A";
                                else if (dr["AssetType"].ToString() == "GD")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Gold_Assets"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Gold_Assets"].ToString());
                                    }
                                    else
                                        drNetHoldings["Gold_Assets"] = "N/A";
                                else if (dr["AssetType"].ToString() == "CL")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Collectibles"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Collectibles"].ToString());
                                    }
                                    else
                                        drNetHoldings["Collectibles"] = "N/A";
                                else if (dr["AssetType"].ToString() == "CS")
                                    if (dr["CFPAGD_TotalValue"].ToString() != "")
                                    {
                                        drNetHoldings["Cash_and_Savings"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_TotalValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                        totalAUM1 = totalAUM1 + double.Parse(drNetHoldings["Cash_and_Savings"].ToString());
                                    }
                                    else
                                        drNetHoldings["Cash_and_Savings"] = "N/A";
                                drNetHoldings["TotalAUM"] = double.Parse(drNetHoldings["TotalAUM"].ToString()) + totalAUM1;
                            }                            
                        }
                        rgvMultiProductMIS.DataSource = dtGrpAssetNetHoldings;
                        ViewState["MultiProductMIS"] = dtGrpAssetNetHoldings;
                        rgvMultiProductMIS.DataBind();
                        trMultiProduct.Visible = true;
                        trLabelMessage.Visible = true;
                        //rgvMultiProductMIS.Visible = true;
                        //hdnCustomerId.Value = null;
                        btnMultiProductMIS.Visible = true;
                        btnFixedIncomeMIS.Visible = false;
                        btnGeneralInsurance.Visible = false;
                        btnLifeInsurance.Visible = false;
                    }
                }
                else
                {
                    trMultiProduct.Visible = false;
                    trLabelMessage.Visible = false;
                    trMISType.Visible = false;
                    trWrongCustomer.Visible = true;
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
                FunctionInfo.Add("Method", "MultiProductMIS.ascx:bindRadGrid_rgvMultiProductMIS()");
                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = dsGrpAssetNetHoldings;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void BindFixedincomeMIS()
        {
            DataSet dsFIMIS = new DataSet();
            DataTable dtFIMIS = new DataTable();
            DataRow drFIMIS;

            try
            {
                SetParametersforDifferentRoles();
                if (lblWrongCustomer.Text != "selected customer does not exist")
                {
                    dsFIMIS = insuranceBo.GetFixedincomeMISDetails(advisorId, branchId, rmId, branchHeadId, customerId, isGroup);
                    if (dsFIMIS.Tables[0].Rows.Count == 0)
                    {
                        trPanel1.Visible = false;
                        trFixedIncomeMIS.Visible = false;
                        lblErrorMsg.Text = "No records found for Fixed Income MIS";
                        lblErrorMsg.Visible = true;
                        trFixedIncome.Visible = false;
                        lblMISType.Visible = false;
                        //rgvFixedIncomeMIS.Visible = false;
                        btnMultiProductMIS.Visible = false;
                        btnFixedIncomeMIS.Visible = false;
                        btnGeneralInsurance.Visible = false;
                        btnLifeInsurance.Visible = false;
                    }
                    else
                    {
                        trPanel1.Visible = true;
                        trFixedIncomeMIS.Visible = true;
                        lblErrorMsg.Visible = false;
                        lblMISType.Text = "Fixed Income MIS";
                        trMISType.Visible = true;
                        lblMISType.Visible = true;

                        dtFIMIS.Columns.Add("Customer_Name");
                        dtFIMIS.Columns.Add("PAIC_AssetInstrumentCategoryName");
                        dtFIMIS.Columns.Add("CFINP_Name");
                        dtFIMIS.Columns.Add("CFINP_PurchaseDate", typeof(DateTime));
                        dtFIMIS.Columns.Add("CFINP_MaturityDate", typeof(DateTime));
                        dtFIMIS.Columns.Add("CFINP_PrincipalAmount", typeof(double));
                        dtFIMIS.Columns.Add("CFINP_InterestRate", typeof(double));
                        dtFIMIS.Columns.Add("CFINP_CurrentValue", typeof(double));
                        dtFIMIS.Columns.Add("CFINP_MaturityValue", typeof(double));
                        dtFIMIS.Columns.Add("CustomerId");

                        foreach (DataRow dr in dsFIMIS.Tables[0].Rows)
                        {
                            drFIMIS = dtFIMIS.NewRow();

                            drFIMIS["Customer_Name"] = dr["CustomerName"].ToString();
                            drFIMIS["PAIC_AssetInstrumentCategoryName"] = dr["PAIC_AssetInstrumentCategoryName"].ToString();

                            if (dr["CFINP_Name"].ToString() != "")
                                drFIMIS["CFINP_Name"] = dr["CFINP_Name"].ToString();
                            else
                                drFIMIS["CFINP_Name"] = "N/A";

                            if (dr["CFINP_PurchaseDate"].ToString() != "")
                                drFIMIS["CFINP_PurchaseDate"] = (DateTime.Parse(dr["CFINP_PurchaseDate"].ToString())).ToShortDateString();
                            //else
                            //    drFIMIS["CFINP_PurchaseDate"] = "N/A";

                            if (dr["CFINP_MaturityDate"].ToString() != "")
                                drFIMIS["CFINP_MaturityDate"] = (DateTime.Parse(dr["CFINP_MaturityDate"].ToString())).ToShortDateString();
                            //else
                            //    drFIMIS["CFINP_MaturityDate"] = "N/A";

                            drFIMIS["CFINP_PrincipalAmount"] = dr["CFINP_PrincipalAmount"].ToString();
                            drFIMIS["CFINP_InterestRate"] = dr["CFINP_InterestRate"].ToString();
                            drFIMIS["CFINP_CurrentValue"] = dr["CFINP_CurrentValue"].ToString();
                            drFIMIS["CFINP_MaturityValue"] = dr["CFINP_MaturityValue"].ToString();
                            drFIMIS["CustomerId"] = dr["CustomerId"].ToString();
                            dtFIMIS.Rows.Add(drFIMIS);
                        }
                        rgvFixedIncomeMIS.DataSource = dtFIMIS;
                        rgvFixedIncomeMIS.DataBind();
                        trFixedIncome.Visible = true;
                        //rgvFixedIncomeMIS.Visible = true;
                        ViewState["FixedIncomeMIS"] = dtFIMIS;
                        //hdnCustomerId.Value = null;
                        btnMultiProductMIS.Visible = false;
                        btnFixedIncomeMIS.Visible = true;
                        btnGeneralInsurance.Visible = false;
                        btnLifeInsurance.Visible = false;
                    }
                }
                else
                {
                    trFixedIncome.Visible = false;
                    trMISType.Visible = false;
                    trWrongCustomer.Visible = true;
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

                FunctionInfo.Add("Method", "MultiProductMIS.ascx:BindFixedincomeMIS()");

                object[] objects = new object[2];

                objects[0] = portfolioId;
                objects[1] = dsFIMIS;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private DateTime GetNextPremiumDate(string frequency, DateTime startDate, DateTime endDate)
        {
            DateTime nextPremiumDate = new DateTime();
            DateTime currentDate = DateTime.Now;
            int startDateOnly = Convert.ToInt32(startDate.Day);

            if (endDate >= currentDate)
            {
                nextPremiumDate = new DateTime(currentDate.Year, currentDate.Month, 1);
                nextPremiumDate = nextPremiumDate.AddDays(startDateOnly - 1);
                switch (frequency)
                {
                    case "Daily":
                        nextPremiumDate = nextPremiumDate.AddDays(1);
                        break;
                    case "FortNightly":
                        nextPremiumDate = nextPremiumDate.AddDays(15);
                        break;
                    case "Weekly":
                        nextPremiumDate = nextPremiumDate.AddDays(7);
                        break;
                    case "Monthly":
                        nextPremiumDate = nextPremiumDate.AddMonths(1);
                        break;
                    case "Quarterly":
                        nextPremiumDate = nextPremiumDate.AddMonths(4);
                        break;
                    case "HalfYearly":
                        nextPremiumDate = nextPremiumDate.AddMonths(6);
                        break;
                    case "Yearly":
                        nextPremiumDate = nextPremiumDate.AddYears(1);
                        break;
                }
            }
            else
            {
                nextPremiumDate = DateTime.MinValue;
            }

            return nextPremiumDate;
        }

        public void BindLifeInsuranceDetails()
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            string frequency = "";
            //int isGroup;
            //if (ddlCustomerType.SelectedValue == "Select")
            //    isGroup = 0;
            //else
            //    isGroup = int.Parse(ddlCustomerType.SelectedValue);
            asset = "LifeInsurance";
            DataTable dtLifeInsDetails = new DataTable();
            try
            {
                SetParametersforDifferentRoles();
                if (lblWrongCustomer.Text != "selected customer does not exist")
                {
                    //Binding the Life Insurance Grid
                    dsInsuranceDetails = insuranceBo.GetMultiProductMISInsuranceDetails(advisorId, branchId, branchHeadId, rmId, customerId, asset, isGroup);
                    if (dsInsuranceDetails.Tables[0].Rows.Count == 0)
                    {
                        trPanel1.Visible = false;
                        trLifeInsuranceMIS.Visible = false;
                        //rgvGeneralInsurance.MasterTableView.Rebind();
                        //rgvGeneralInsurance.Rebind();
                        lblErrorMsg.Text = "No records found for Life Insurance MIS";
                        lblErrorMsg.Visible = true;
                        trLifeInsurance.Visible = false;
                        lblMISType.Visible = false;
                        //rgvLifeInsurance.Visible = false;
                        btnMultiProductMIS.Visible = false;
                        btnFixedIncomeMIS.Visible = false;
                        btnGeneralInsurance.Visible = false;
                        btnLifeInsurance.Visible = false;
                    }
                    else
                    {
                        trPanel1.Visible = true;
                        trLifeInsuranceMIS.Visible = true;
                        lblErrorMsg.Visible = false;
                        lblMISType.Text = "Life Insurance MIS";
                        trMISType.Visible = true;
                        lblMISType.Visible = true;

                        dtLifeInsDetails.Columns.Add("CustomerName");
                        dtLifeInsDetails.Columns.Add("PolicyIssuerName");
                        dtLifeInsDetails.Columns.Add("Particulars");

                        dtLifeInsDetails.Columns.Add("InsuranceType");
                        dtLifeInsDetails.Columns.Add("SumAssured", typeof(double));
                        dtLifeInsDetails.Columns.Add("PremiumAmount", typeof(double));
                        dtLifeInsDetails.Columns.Add("PremiumFrequency");
                        dtLifeInsDetails.Columns.Add("CommencementDate", typeof(DateTime));
                        dtLifeInsDetails.Columns.Add("NextPremiumDate", typeof(DateTime));
                        dtLifeInsDetails.Columns.Add("MaturityValue", typeof(double));
                        dtLifeInsDetails.Columns.Add("MaturityDate", typeof(DateTime));

                        dtLifeInsDetails.Columns.Add("CustomerId");
                        dtLifeInsDetails.Columns.Add("InsuranceNPId");

                        foreach (DataRow dr in dsInsuranceDetails.Tables[0].Rows)
                        {
                            drLifeInsurance = dtLifeInsDetails.NewRow();

                            drLifeInsurance["CustomerName"] = dr["CustomerName"].ToString();
                            drLifeInsurance["PolicyIssuerName"] = dr["PolicyIssuerName"].ToString();
                            drLifeInsurance["Particulars"] = dr["Particulars"].ToString();
                            drLifeInsurance["InsuranceType"] = dr["InsuranceType"].ToString();
                            drLifeInsurance["SumAssured"] = String.Format("{0:n2}", decimal.Parse(dr["SumAssured"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            drLifeInsurance["PremiumAmount"] = String.Format("{0:n2}", decimal.Parse(dr["PremiumAmount"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            drLifeInsurance["PremiumFrequency"] = dr["PremiumFrequency"].ToString();
                            drLifeInsurance["CommencementDate"] = DateTime.Parse(dr["CommencementDate"].ToString()).ToShortDateString();
                            drLifeInsurance["MaturityValue"] = String.Format("{0:n2}", decimal.Parse(dr["MaturityValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            drLifeInsurance["MaturityDate"] = DateTime.Parse(dr["MaturityDate"].ToString()).ToShortDateString();


                            startDate = DateTime.Parse(dr["CINP_FirstPremiumDate"].ToString());
                            endDate = DateTime.Parse(dr["MaturityDate"].ToString());
                            frequency = dr["PremiumFrequency"].ToString();

                            DateTime nextPremiumDate = GetNextPremiumDate(frequency, startDate, endDate);
                            if (nextPremiumDate != DateTime.MinValue)
                            {
                                drLifeInsurance["NextPremiumDate"] = nextPremiumDate.ToShortDateString();
                            }
                            else
                            {
                                //drLifeInsurance["NextPremiumDate"] = "---";
                            }

                            drLifeInsurance["CustomerId"] = dr["CustomerId"].ToString();
                            drLifeInsurance["InsuranceNPId"] = dr["InsuranceNPId"].ToString();

                            dtLifeInsDetails.Rows.Add(drLifeInsurance);
                        }
                        rgvLifeInsurance.DataSource = dtLifeInsDetails;
                        rgvLifeInsurance.DataBind();
                        trLifeInsurance.Visible = true;
                        //rgvLifeInsurance.Visible = true;
                        ViewState["LifeInsuranceMIS"] = dtLifeInsDetails;
                        //hdnCustomerId.Value = null;
                        btnMultiProductMIS.Visible = false;
                        btnFixedIncomeMIS.Visible = false;
                        btnGeneralInsurance.Visible = false;
                        btnLifeInsurance.Visible = true;
                    }
                }
                else
                {
                    trLifeInsurance.Visible = false;
                    trMISType.Visible = false;
                    trWrongCustomer.Visible = true;
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

                FunctionInfo.Add("Method", "MultiProductMIS.ascx:BindLifeInsuranceDetails()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        //function to populate the General Insurance Grid
        public void BindGeneralInsuranceDetails()
        {
            string asset = "General Insurance";
            DataTable dtGenInsDetails = new DataTable();
            try
            {
                SetParametersforDifferentRoles();
                if (lblWrongCustomer.Text != "selected customer does not exist")
                {
                    //Binding the General Insurance Grid
                    dsInsuranceDetails = insuranceBo.GetMultiProductMISInsuranceDetails(advisorId, branchId, branchHeadId, rmId, customerId, asset, isGroup);
                    if (dsInsuranceDetails.Tables[0].Rows.Count == 0)
                    {
                        trPanel1.Visible = false;
                        trGeneralInsuranceMis.Visible = false;
                        lblErrorMsg.Visible = true;
                        lblErrorMsg.Text = "No records found for General Insurance MIS";
                        
                        trGeneralInsurance.Visible = false;
                        lblMISType.Visible = false;
                        //rgvGeneralInsurance.Visible = false;
                        btnMultiProductMIS.Visible = false;
                        btnFixedIncomeMIS.Visible = false;
                        btnGeneralInsurance.Visible = false;
                        btnLifeInsurance.Visible = false;
                      
                    }
                    else
                    {
                        trPanel1.Visible = true;
                        trGeneralInsuranceMis.Visible = true;
                        lblErrorMsg.Visible = false;
                        lblMISType.Text = "General Insurance MIS";
                        trMISType.Visible = true;
                        lblMISType.Visible = true;

                        dtGenInsDetails.Columns.Add("CustomerName");
                        dtGenInsDetails.Columns.Add("PolicyIssuerName");
                        dtGenInsDetails.Columns.Add("Particulars");
                        dtGenInsDetails.Columns.Add("InsuranceType");
                        dtGenInsDetails.Columns.Add("SumAssured", typeof(double));
                        dtGenInsDetails.Columns.Add("PremiumAmount", typeof(double));
                        dtGenInsDetails.Columns.Add("PremiumFrequency");
                        dtGenInsDetails.Columns.Add("CommencementDate", typeof(DateTime));
                        //dtGenInsDetails.Columns.Add("MaturityValue");
                        dtGenInsDetails.Columns.Add("MaturityDate", typeof(DateTime));
                        dtGenInsDetails.Columns.Add("CustomerId");
                        dtGenInsDetails.Columns.Add("GenInsuranceNPId");


                        foreach (DataRow dr in dsInsuranceDetails.Tables[0].Rows)
                        {
                            drGeneralInsurance = dtGenInsDetails.NewRow();

                            drGeneralInsurance["CustomerName"] = dr["CustomerName"].ToString();
                            drGeneralInsurance["PolicyIssuerName"] = dr["PolicyIssuerName"].ToString();
                            drGeneralInsurance["Particulars"] = dr["Particulars"].ToString();
                            drGeneralInsurance["InsuranceType"] = dr["InsuranceType"].ToString();
                            drGeneralInsurance["SumAssured"] = String.Format("{0:n2}", decimal.Parse(dr["SumAssured"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            drGeneralInsurance["PremiumAmount"] = String.Format("{0:n2}", decimal.Parse(dr["PremiumAmount"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            drGeneralInsurance["PremiumFrequency"] = dr["PremiumFrequency"].ToString();
                            //if (dr["CommencementDate"].ToString() == "" || dr["CommencementDate"].ToString() == DateTime.MinValue.ToString())
                            //    drGeneralInsurance["CommencementDate"] = "N/A";
                            //else
                            if (dr["CommencementDate"].ToString() != "" && dr["CommencementDate"].ToString() != DateTime.MinValue.ToString())
                                drGeneralInsurance["CommencementDate"] = Convert.ToDateTime(dr["CommencementDate"].ToString()).ToShortDateString();

                            //if (dr["MaturityDate"].ToString() == "" || dr["MaturityDate"].ToString() == DateTime.MinValue.ToString())
                            //    drGeneralInsurance["CommencementDate"] = "N/A";
                            //else
                            if (dr["MaturityDate"].ToString() != "" && dr["MaturityDate"].ToString() != DateTime.MinValue.ToString())
                                drGeneralInsurance["MaturityDate"] = Convert.ToDateTime(dr["MaturityDate"].ToString()).ToShortDateString();
                            drGeneralInsurance["CustomerId"] = dr["CustomerId"].ToString();
                            drGeneralInsurance["GenInsuranceNPId"] = dr["GenInsuranceNPId"].ToString();

                            dtGenInsDetails.Rows.Add(drGeneralInsurance);
                        }
                        rgvGeneralInsurance.DataSource = dtGenInsDetails;
                        rgvGeneralInsurance.DataBind();
                        trGeneralInsurance.Visible = true;
                        //rgvGeneralInsurance.Visible = true;
                        ViewState["GeneralInsuranceMIS"] = dtGenInsDetails;
                        //hdnCustomerId.Value = null;
                        btnMultiProductMIS.Visible = false;
                        btnFixedIncomeMIS.Visible = false;
                        btnGeneralInsurance.Visible = true;
                        btnLifeInsurance.Visible = false;
                    }
                }
                else
                {
                    trGeneralInsurance.Visible = false;
                    trMISType.Visible = false;
                    trWrongCustomer.Visible = true;
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

                FunctionInfo.Add("Method", "MultiProductMIS.ascx:BindGeneralInsuranceDetails()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void lnkBtnMultiProductMIS_Click(object sender, EventArgs e)
        {
            trFixedIncome.Visible = false;
            trGeneralInsurance.Visible = false;
            trLifeInsurance.Visible = false;
            trMultiProduct.Visible = true;
            trLabelMessage.Visible = true;
            
            bindRadGrid_rgvMultiProductMIS();
        }

        protected void lnkBtnInvestmentMIS_Click(object sender, EventArgs e)
        {
            trFixedIncome.Visible = true;
            trGeneralInsurance.Visible = false;
            trLifeInsurance.Visible = false;
            trMultiProduct.Visible = false;
            trLabelMessage.Visible = false;
            BindFixedincomeMIS();
        }

        public void lnkBtnLifeInsuranceMIS_OnClick(object sender, EventArgs e)
        {
            trFixedIncome.Visible = false;
            trGeneralInsurance.Visible = false;
            trLifeInsurance.Visible = true;
            trMultiProduct.Visible = false;
            trLabelMessage.Visible = false;
            BindLifeInsuranceDetails();
        }

        public void lnkBtnGeneralInsuranceMIS_OnClick(object sender, EventArgs e)
        {
            trFixedIncome.Visible = false;
            trGeneralInsurance.Visible = true;
            trLifeInsurance.Visible = false;
            trMultiProduct.Visible = false;
            trLabelMessage.Visible = false;
            BindGeneralInsuranceDetails();
        }

        protected void rgvFixedIncomeMIS_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            trPanel1.Visible = true;
            trFixedIncomeMIS.Visible = true;
            DataTable dt = new DataTable();
            if (ViewState["FixedIncomeMIS"] != null)
            {
                dt = (DataTable)ViewState["FixedIncomeMIS"];
                rgvFixedIncomeMIS.DataSource = dt;
            }
        }

        protected void rgvGeneralInsurance_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            trPanel1.Visible = true;
            trGeneralInsuranceMis.Visible = true;
            DataTable dt = new DataTable();
            if (ViewState["GeneralInsuranceMIS"] != null)
            {
                dt = (DataTable)ViewState["GeneralInsuranceMIS"];
                rgvGeneralInsurance.DataSource = dt;
            }
        }

        protected void rgvLifeInsurance_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            trPanel1.Visible = true;
            trLifeInsuranceMIS.Visible = true;
            DataTable dt = new DataTable();
            if (ViewState["LifeInsuranceMIS"] != null)
            {
                dt = (DataTable)ViewState["LifeInsuranceMIS"];
                rgvLifeInsurance.DataSource = dt;
            }
        }

        protected void rgvMultiProductMIS_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            trPanel1.Visible = true;
            trExportFilteredData.Visible = true;
            DataTable dt = new DataTable();
            if (ViewState["MultiProductMIS"] != null)
            {
                dt = (DataTable)ViewState["MultiProductMIS"];
                rgvMultiProductMIS.DataSource = dt;
            }
        }

        //protected void txtIndividualCustomer_Click(object sender, EventArgs e)
        //{
        //    txtIndividualCustomer.Clear();
        //}
        private void txtIndividualCustomer_Enter(object sender, System.EventArgs e)
        {
            txtIndividualCustomer.Text = ""; // Clears the text field            
        }

        protected void ddlRM_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridsVisibility();
        }

        protected void rgvMultiProductMIS_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                GridDataItem item = (GridDataItem)e.Item;
                string value = item.GetDataKeyValue("Customer_Name").ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('RMCustomerAMCSchemewiseMIS','strCustomreId=" + value + " ');", true);

            }
        }
        protected void btnFixedIncomeMIS_OnClick(object sender, ImageClickEventArgs e)
        {

            rgvFixedIncomeMIS.ExportSettings.OpenInNewWindow = true;
            rgvFixedIncomeMIS.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in rgvFixedIncomeMIS.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            rgvFixedIncomeMIS.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {

            rgvMultiProductMIS.ExportSettings.OpenInNewWindow = true;
            rgvMultiProductMIS.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in rgvMultiProductMIS.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            rgvMultiProductMIS.MasterTableView.ExportToExcel();
        }
        protected void btnLifeInsurance_OnClick(object sender, ImageClickEventArgs e)
        {

            rgvLifeInsurance.ExportSettings.OpenInNewWindow = true;
            rgvLifeInsurance.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in rgvLifeInsurance.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            rgvLifeInsurance.MasterTableView.ExportToExcel();
        }
        protected void btnGeneralInsurance_OnClick(object sender, ImageClickEventArgs e)
        {

            rgvGeneralInsurance.ExportSettings.OpenInNewWindow = true;
            rgvGeneralInsurance.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in rgvGeneralInsurance.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            rgvGeneralInsurance.MasterTableView.ExportToExcel();
        }
        protected void rgvMultiProductMIS_DataBound(object sender, System.EventArgs e)
        {
            DataTable gridTable = (DataTable)ViewState["MultiProductMIS"];
            double grandTotal = 0;
            if (gridTable.Rows.Count > 0)
            {
                foreach (DataRow row in gridTable.Rows)
                {
                    grandTotal = grandTotal + double.Parse(row["Mutual_Fund"].ToString());
                }
                GridFooterItem footerItem = (GridFooterItem)rgvMultiProductMIS.MasterTableView.GetItems(GridItemType.Footer)[0];
                footerItem["Mutual_Fund"].Text = grandTotal.ToString();
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"] == "LI")
                {
                    lblpageHeader.Text = "Life Insurance MIS";
                    trFixedIncome.Visible = false;
                    trGeneralInsurance.Visible = false;
                    trLifeInsurance.Visible = true;
                    trMultiProduct.Visible = false;
                    trLabelMessage.Visible = false;
                    BindLifeInsuranceDetails();
                }
                else if (Request.QueryString["action"] == "GI")
                {
                    lblpageHeader.Text = "General Insurance MIS";
                    trFixedIncome.Visible = false;
                    trGeneralInsurance.Visible = true;
                    trLifeInsurance.Visible = false;
                    trMultiProduct.Visible = false;
                    trLabelMessage.Visible = false;
                    BindGeneralInsuranceDetails();
                }
                else if (Request.QueryString["action"] == "FI")
                {
                    lblpageHeader.Text = "Fixed Income MIS";
                    trFixedIncome.Visible = true;
                    trGeneralInsurance.Visible = false;
                    trLifeInsurance.Visible = false;
                    trMultiProduct.Visible = false;
                    trLabelMessage.Visible = false;
                    BindFixedincomeMIS();
                }
            }
            else
            {
                lblErrorMsg.Text = "No records found";
                lblErrorMsg.Visible = true;
            }

        }
    }
}
