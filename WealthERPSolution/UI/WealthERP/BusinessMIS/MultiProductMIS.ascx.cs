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

        int advisorId = 0;
        int customerId = 0;
        String userType;
        int rmId = 0;
        int bmID = 0;

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
        DataRow drGeneralInsurance ;
        DataRow drLifeInsurance;

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
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

            if (!IsPostBack)
            {
                ddlCustomerType.Visible = false;
                lblSelectTypeOfCustomer.Visible = false;
                lblselectCustomer.Visible = false;
                txtIndividualCustomer.Visible = false;              
                rquiredFieldValidatorIndivudialCustomer.Visible = false;
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
                //customerId = int.Parse(hdnCustomerId.Value);
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
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
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
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
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
                txtIndividualCustomer.Visible = false;
                lblselectCustomer.Visible = false;
                rquiredFieldValidatorIndivudialCustomer.Visible = false;
                ddlCustomerType.SelectedIndex = 0;
                GridsVisibility();
            }
            if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer")
            {
                ddlCustomerType.Visible = true;
                lblSelectTypeOfCustomer.Visible = true;
                txtIndividualCustomer.Visible = true;
                txtIndividualCustomer.Text = string.Empty;
                lblselectCustomer.Visible = true;
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
                    dsGrpAssetNetHoldings = insuranceBo.GetAllProductMIS(advisorId, branchId, rmId, branchHeadId, customerId, isGroup);

                    if (dsGrpAssetNetHoldings.Tables[0].Rows.Count == 0)
                    {
                        lblErrorMsg.Text = "No records found for Multi-Product MIS";
                        lblErrorMsg.Visible = true;
                        trMultiProduct.Visible = false;
                        //rgvMultiProductMIS.Visible = false;
                    }
                    else
                    {
                        lblErrorMsg.Visible = false;
                        lblMISType.Text = "Multi-Product MIS";
                        trMISType.Visible = true;
                        lblMISType.Visible = true;

                        dtGrpAssetNetHoldings.Columns.Add("Customer_Name");
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

                        foreach (DataRow dr in dsGrpAssetNetHoldings.Tables[0].Rows)
                        {
                            if (int.Parse(dr["C_CustomerId"].ToString()) != tempID)
                            {
                                tempID = int.Parse(dr["C_CustomerId"].ToString());
                                drNetHoldings = dtGrpAssetNetHoldings.NewRow();

                                drNetHoldings["C_CustomerId"] = dr["C_CustomerId"].ToString();
                                drNetHoldings["Equity"] = 0.00;
                                drNetHoldings["Mutual_Fund"] = 0.00;
                                drNetHoldings["Fixed_Income"] = 0.00;
                                drNetHoldings["Government_Savings"] = 0.00;
                                drNetHoldings["Property"] = 0.00;
                                drNetHoldings["Pension_and_Gratuity"] = 0.00;
                                drNetHoldings["Personal_Assets"] = 0.00;
                                drNetHoldings["Gold_Assets"] = 0.00;
                                drNetHoldings["Collectibles"] = 0.00;
                                drNetHoldings["Cash_and_Savings"] = 0.00;

                                drNetHoldings[0] = dr["Customer_Name"].ToString();
                                if (dr["AssetType"].ToString() == "DE")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Equity"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Equity"] = "N/A";

                                else if (dr["AssetType"].ToString() == "MF")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Mutual_Fund"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Mutual_Fund"] = "N/A";
                                else if (dr["AssetType"].ToString() == "FI")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Fixed_Income"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Fixed_Income"] = "N/A";
                                else if (dr["AssetType"].ToString() == "GS")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Government_Savings"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Government_Savings"] = "N/A";
                                else if (dr["AssetType"].ToString() == "PR")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Property"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Property"] = "N/A";
                                else if (dr["AssetType"].ToString() == "PG")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Pension_and_Gratuity"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Pension_and_Gratuity"] = "N/A";
                                else if (dr["AssetType"].ToString() == "PI")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Personal_Assets"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Personal_Assets"] = "N/A";
                                else if (dr["AssetType"].ToString() == "GD")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Gold_Assets"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Gold_Assets"] = "N/A";
                                else if (dr["AssetType"].ToString() == "CL")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Collectibles"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Collectibles"] = "N/A";
                                else if (dr["AssetType"].ToString() == "CS")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Cash_and_Savings"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Cash_and_Savings"] = "N/A";

                                dtGrpAssetNetHoldings.Rows.Add(drNetHoldings);
                            }
                            else
                            {
                                if (dr["AssetType"].ToString() == "DE")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Equity"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Equity"] = "N/A";

                                else if (dr["AssetType"].ToString() == "MF")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Mutual_Fund"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Mutual_Fund"] = "N/A";
                                else if (dr["AssetType"].ToString() == "FI")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Fixed_Income"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Fixed_Income"] = "N/A";
                                else if (dr["AssetType"].ToString() == "GS")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Government_Savings"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Government_Savings"] = "N/A";
                                else if (dr["AssetType"].ToString() == "PR")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Property"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Property"] = "N/A";
                                else if (dr["AssetType"].ToString() == "PG")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Pension_and_Gratuity"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Pension_and_Gratuity"] = "N/A";
                                else if (dr["AssetType"].ToString() == "PI")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Personal_Assets"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Personal_Assets"] = "N/A";
                                else if (dr["AssetType"].ToString() == "GD")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Gold_Assets"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Gold_Assets"] = "N/A";
                                else if (dr["AssetType"].ToString() == "CL")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Collectibles"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Collectibles"] = "N/A";
                                else if (dr["AssetType"].ToString() == "CS")
                                    if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                        drNetHoldings["Cash_and_Savings"] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                    else
                                        drNetHoldings["Cash_and_Savings"] = "N/A";
                            }
                        }
                        rgvMultiProductMIS.DataSource = dtGrpAssetNetHoldings;
                        rgvMultiProductMIS.DataBind();
                        trMultiProduct.Visible = true;
                        //rgvMultiProductMIS.Visible = true;
                        ViewState["MultiProductMIS"] = dtGrpAssetNetHoldings;
                        //hdnCustomerId.Value = null;
                    }
                }
                else
                {
                    trMultiProduct.Visible = false;
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
                        lblErrorMsg.Text = "No records found for Fixed Income MIS";
                        lblErrorMsg.Visible = true;
                        trFixedIncome.Visible = false;
                        //rgvFixedIncomeMIS.Visible = false;
                    }
                    else
                    {
                        lblErrorMsg.Visible = false;
                        lblMISType.Text = "Fixed Income MIS";
                        trMISType.Visible = true;
                        lblMISType.Visible = true;

                        dtFIMIS.Columns.Add("Customer_Name");
                        dtFIMIS.Columns.Add("PAIC_AssetInstrumentCategoryName");
                        dtFIMIS.Columns.Add("CFINP_Name");
                        dtFIMIS.Columns.Add("CFINP_PurchaseDate");
                        dtFIMIS.Columns.Add("CFINP_MaturityDate");
                        dtFIMIS.Columns.Add("CFINP_PrincipalAmount", typeof(double));
                        dtFIMIS.Columns.Add("CFINP_InterestRate");
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
                            else
                                drFIMIS["CFINP_PurchaseDate"] = "N/A";

                            if (dr["CFINP_MaturityDate"].ToString() != "")
                                drFIMIS["CFINP_MaturityDate"] = (DateTime.Parse(dr["CFINP_MaturityDate"].ToString())).ToShortDateString();
                            else
                                drFIMIS["CFINP_MaturityDate"] = "N/A";

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

        public void BindLifeInsuranceDetails()
        {
            //int isGroup;
            //if (ddlCustomerType.SelectedValue == "Select")
            //    isGroup = 0;
            //else
            //    isGroup = int.Parse(ddlCustomerType.SelectedValue);
            asset = lnkBtnLifeInsuranceMIS.Text;
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
                        //rgvGeneralInsurance.MasterTableView.Rebind();
                        //rgvGeneralInsurance.Rebind();
                        lblErrorMsg.Text = "No records found for Life Insurance MIS";
                        lblErrorMsg.Visible = true;
                        trLifeInsurance.Visible = false;
                        //rgvLifeInsurance.Visible = false;
                    }
                    else
                    {
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
                        dtLifeInsDetails.Columns.Add("CommencementDate");
                        dtLifeInsDetails.Columns.Add("MaturityValue", typeof(double));
                        dtLifeInsDetails.Columns.Add("MaturityDate");

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
            string asset = lnkBtnGeneralInsuranceMIS.Text;            
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
                        lblErrorMsg.Text = "No records found for General Insurance MIS";
                        lblErrorMsg.Visible = true;
                        trGeneralInsurance.Visible = false;
                        //rgvGeneralInsurance.Visible = false;
                    }
                    else
                    {
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
                        dtGenInsDetails.Columns.Add("CommencementDate");
                        //dtGenInsDetails.Columns.Add("MaturityValue");
                        dtGenInsDetails.Columns.Add("MaturityDate");
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
                            drGeneralInsurance["CommencementDate"] = DateTime.Parse(dr["CommencementDate"].ToString()).ToShortDateString();
                            drGeneralInsurance["MaturityDate"] = DateTime.Parse(dr["MaturityDate"].ToString()).ToShortDateString();
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
            bindRadGrid_rgvMultiProductMIS();
        } 

        protected void lnkBtnInvestmentMIS_Click(object sender, EventArgs e)
        {
            trFixedIncome.Visible = true;
            trGeneralInsurance.Visible = false;
            trLifeInsurance.Visible = false;
            trMultiProduct.Visible = false;
            BindFixedincomeMIS();
        }
        
        public void lnkBtnLifeInsuranceMIS_OnClick(object sender, EventArgs e)
        {
            trFixedIncome.Visible = false;
            trGeneralInsurance.Visible = false;
            trLifeInsurance.Visible = true;
            trMultiProduct.Visible = false;
            BindLifeInsuranceDetails();
        }

        public void lnkBtnGeneralInsuranceMIS_OnClick(object sender, EventArgs e)
        {
            trFixedIncome.Visible = false;
            trGeneralInsurance.Visible = true;
            trLifeInsurance.Visible = false;
            trMultiProduct.Visible = false;
            BindGeneralInsuranceDetails();
        }

        protected void rgvFixedIncomeMIS_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["FixedIncomeMIS"] != null)
            {
                dt = (DataTable)ViewState["FixedIncomeMIS"];
                rgvFixedIncomeMIS.DataSource = dt;
            }
        }

        protected void rgvGeneralInsurance_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["GeneralInsuranceMIS"] != null)
            {
                dt = (DataTable)ViewState["GeneralInsuranceMIS"];
                rgvGeneralInsurance.DataSource = dt;
            }
        }

        protected void rgvLifeInsurance_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["LifeInsuranceMIS"] != null)
            {
                dt = (DataTable)ViewState["LifeInsuranceMIS"];
                rgvLifeInsurance.DataSource = dt;
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

        //protected void txtIndividualCustomer_Click(object sender, EventArgs e)
        //{
        //    txtIndividualCustomer.Clear();
        //}
        private void txtIndividualCustomer_Enter(object sender, System.EventArgs e)
        {            
            txtIndividualCustomer.Text = ""; // Clears the text field            
        }
    }
}