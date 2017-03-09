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
    public partial class CustomerAUM : System.Web.UI.UserControl
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
        int IsAssociate;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            associatesVo = (AssociatesVO)Session["associatesVo"];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
            {
                userType = "associates";
                txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
            }
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            advisorId = advisorVo.advisorId;
            int RMId = rmVo.RMId;
            customerId = customerVo.CustomerId;
            //AgentId = associatesVo.AAC_AdviserAgentId;
            rmId = rmVo.RMId;
            bmID = rmVo.RMId;
            if (!IsPostBack)
            {
                ddlCustomerType.Visible = false;
                lblSelectTypeOfCustomer.Visible = false;
                trCustomerSearch.Visible = false;
                //divGvMultiProductMIS.Visible = false;
                pnlMultiProductMIS.Visible = false;
                rquiredFieldValidatorIndivudialCustomer.Visible = false;
                btnMultiProductMIS.Visible = false;

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
                    BindBranchDropDown();
                    BindRMDropDown();
                    ddlCustomerType.Visible = false;
                    lblSelectTypeOfCustomer.Visible = false;
                    trBranchRM.Visible = false;                    
                }
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
        protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnCustomerId.Value.ToString().Trim()))
            {
                customerId = int.Parse(hdnCustomerId.Value);
            }
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
                else if (Session[SessionContents.CurrentUserRole].ToString() == "associates")
                {
                    txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";
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
                if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";

                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "associates")
                {
                    txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
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
                //ddlCustomerType.Visible = false;
                //lblSelectTypeOfCustomer.Visible = false;
                //trCustomerSearch.Visible = false;
                //rquiredFieldValidatorIndivudialCustomer.Visible = false;
                //ddlCustomerType.SelectedIndex = 0;

            }
            if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer")
            {
                if (userType != "associates")
                {
                    ddlCustomerType.Visible = true;
                    lblSelectTypeOfCustomer.Visible = true;
                }
               // lblSelectTypeOfCustomer.Visible = true;
                txtIndividualCustomer.Text = string.Empty;
                trCustomerSearch.Visible = true;
                rquiredFieldValidatorIndivudialCustomer.Visible = true;
                ddlCustomerType.SelectedIndex = 0;

            }
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            SetParametersforDifferentRoles();
            bindRadGrid_rgvMultiProductMIS();
        }

        public void SetParametersforDifferentRoles()
        {
            if (ddlCustomerType.SelectedValue == "0")
                isGroup = 0;
            else if (ddlCustomerType.SelectedValue == "1")
                isGroup = 1;

            if (ddlBranch.SelectedValue != "0")
                branchId = int.Parse(ddlBranch.SelectedValue);
            else
                branchId = 0;

            if (ddlRM.SelectedValue != "0")
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
                    IsAssociate = 0;
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
                    IsAssociate = 0;
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
                    IsAssociate = 0;
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
                    IsAssociate = 1;                                   
                    AgentId = associatesVo.AAC_AdviserAgentId;
                    ddlCustomerType.Visible = false;
                    lblSelectTypeOfCustomer.Visible = false;
                    trBranchRM.Visible = false;
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
            }
        }

        private void bindRadGrid_rgvMultiProductMIS()
        {
            int tempID = 0;

            try
            {
                SetParametersforDifferentRoles();
                if (lblWrongCustomer.Text != "selected customer does not exist")
                {
                    dsGrpAssetNetHoldings = insuranceBo.GetAllProductMIS(advisorId, branchId, rmId, branchHeadId, customerId,AgentId,IsAssociate,isGroup);

                    if (dsGrpAssetNetHoldings.Tables[0]== null)
                    {
                        rgvMultiProductMIS.DataSource = dsGrpAssetNetHoldings.Tables[0];
                        rgvMultiProductMIS.DataBind();
                        btnMultiProductMIS.Visible = false;

                    }
                    else
                    {

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
                        dtGrpAssetNetHoldings.Columns.Add("TotalAUM", typeof(double));

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
                        //trLabelMessage.Visible = true;
                        rgvMultiProductMIS.Visible = true;
                        //hdnCustomerId.Value = null;
                        btnMultiProductMIS.Visible = true;
                        pnlMultiProductMIS.Visible = true;
                        divGvMultiProductMIS.Visible = true;

                    }
                }
                else
                {
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
        protected void rgvMultiProductMIS_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                GridDataItem item = (GridDataItem)e.Item;
                string value = item.GetDataKeyValue("C_CustomerId").ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('MFReturns','strCustomreId=" + value + " ');", true);

            }
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
        protected void rgvMultiProductMIS_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["MultiProductMIS"] != null)
            {
                dt = (DataTable)ViewState["MultiProductMIS"];
                rgvMultiProductMIS.DataSource = dt;
            }
        }
        public void btnMultiProductMIS_Click(object sender, ImageClickEventArgs e)
        {
            rgvMultiProductMIS.ExportSettings.OpenInNewWindow = true;
            rgvMultiProductMIS.ExportSettings.IgnorePaging = true;
            rgvMultiProductMIS.ExportSettings.HideStructureColumns = true;
            rgvMultiProductMIS.ExportSettings.ExportOnlyData = true;
            rgvMultiProductMIS.ExportSettings.FileName = "MultiProductMIS Details";
            rgvMultiProductMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rgvMultiProductMIS.MasterTableView.ExportToExcel();
        }
    }
}