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
        int isGroup = 0;

        string customerType = string.Empty;

        DataRow drMultiProduct;

        AssetBo assetBo = new AssetBo();
        DataSet dsGrpAssetNetHoldings = new DataSet();
        DataTable dtGrpAssetNetHoldings = new DataTable();
        DataRow drNetHoldings;
        int portfolioId;


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
                rgvFixedIncomeMIS.Visible = false;
                rgvGeneralInsurance.Visible = false;
                rgvLifeInsurance.Visible = false;
                rgvMultiProductMIS.Visible = false;
                //Session["ButtonGo"] = null;                

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

        //protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(hdnCustomerId.Value.ToString().Trim()))
        //    {
        //        customerVo = customerBo.GetCustomer(int.Parse(hdnCustomerId.Value));
        //    }
        //}

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
        }

        protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnCustomerId.Value.ToString().Trim()))
            {
                customerVo = customerBo.GetCustomer(int.Parse(txtIndividualCustomer_autoCompleteExtender.ContextKey));
            }
        }


        protected void ddlCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIndividualCustomer.Text = string.Empty;
            hdnIndividualOrGroup.Value = ddlCustomerType.SelectedItem.Value;
            if (ddlCustomerType.SelectedItem.Value == "Group Head")
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
            else if (ddlCustomerType.SelectedItem.Value == "Individual")
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

                ViewState["GroupHeadCustomers"] = null;
                ViewState["IndividualCustomers"] = null;
            }
            if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer")
            {
                ddlCustomerType.Visible = true;
                lblSelectTypeOfCustomer.Visible = true;
                txtIndividualCustomer.Visible = true;
                txtIndividualCustomer.Text = string.Empty;
                lblselectCustomer.Visible = true;
                rquiredFieldValidatorIndivudialCustomer.Visible = true;

                ViewState["GroupHeadCustomers"] = null;
                ViewState["IndividualCustomers"] = null;
                ddlCustomerType.SelectedIndex = 0;
            }
        }

        public void SetParametersforDifferentRoles()
        {
            if (ddlCustomerType.SelectedValue == "0")
                isGroup = 0;
            else if (ddlCustomerType.SelectedValue == "1")
                isGroup = 1;


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
                    isGroup = 0;
                }

                    //for all branch all customers and particular RM
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0 && ddlSelectCustomer.SelectedIndex == 0)
                {
                    advisorId = 0;
                    branchId = 0;
                    customerId = 0;
                    isGroup = 0;
                    branchHeadId = 0;
                }
                //for particular branch particular customer and all RM
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex == 0 && ddlSelectCustomer.SelectedIndex != 0)
                {
                    advisorId = 0;
                    branchId = 0;
                    rmId = 0;
                    isGroup = 0;
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
                    isGroup = 0;
                }
                //for particular branch ,particular rm and all customers
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0 && ddlSelectCustomer.SelectedIndex == 0)
                {
                    advisorId = 0;
                    customerId = 0;
                    branchHeadId = 0;
                    isGroup = 0;
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
                    isGroup = 0;
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
                    isGroup = 0;
                }
                else if (ddlSelectCustomer.SelectedIndex != 0)
                {
                    advisorId = 0;
                    branchId = 0;
                    branchHeadId = 0;
                    isGroup = 0;
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
                    isGroup = 0;
                }

                    //for all branch all customers and particular RM
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0 && ddlSelectCustomer.SelectedIndex == 0)
                {
                    advisorId = 0;
                    branchId = 0;
                    customerId = 0;
                    isGroup = 0;
                }
                //for particular branch particular customer and all RM
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex == 0 && ddlSelectCustomer.SelectedIndex != 0)
                {
                    advisorId = 0;
                    branchId = 0;
                    rmId = 0;
                    isGroup = 0;
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
                    isGroup = 0;
                }
                //for particular branch ,particular rm and all customers
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0 && ddlSelectCustomer.SelectedIndex == 0)
                {
                    advisorId = 0;
                    customerId = 0;
                    branchHeadId = 0;
                    isGroup = 0;
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
                    isGroup = 0;
                }
            }
        }

        //public void bindRadGrid_rgvMultiProductMIS()
        //{
        //    int tempCustId = 0;
        //    int i = 0;
        //    int isGroup = 0;
        //    int branchId = 0;
        //    DataSet dsMultiProductMIS = new DataSet();
        //    DataTable dtMultiProduct = new DataTable();
        //    DataTable dtMultiProductMIS = new DataTable();
        //    AssetBo assetBo = new AssetBo();
        //    try
        //    {
        //        branchId = int.Parse(ddlBranch.SelectedValue);

        //        if (ddlCustomerType.SelectedValue == "Group Head")
        //            isGroup = 0;
        //        else
        //            isGroup = 1;

        //        if (txtIndividualCustomer.Text == "")
        //        {
        //            customerId = 0;
        //            dsMultiProductMIS = assetBo.GetAllProductMIS(advisorId, userType, rmId, branchId, customerId, isGroup);
        //        }
        //        else
        //            dsMultiProductMIS = assetBo.GetAllProductMIS(advisorId, userType, rmId, branchId, customerId, isGroup);

        //        dtMultiProductMIS = dsMultiProductMIS.Tables[0];

        //       dtMultiProduct.Columns.Add("CustomerName");
        //       dtMultiProduct.Columns.Add("Equity");
        //       dtMultiProduct.Columns.Add("Mutual_Fund");
        //       dtMultiProduct.Columns.Add("Fixed_Income");
        //       dtMultiProduct.Columns.Add("Government_Savings");
        //       dtMultiProduct.Columns.Add("Property");
        //       dtMultiProduct.Columns.Add("Pension_and_Gratuity");
        //       dtMultiProduct.Columns.Add("Personal_Assets");
        //       dtMultiProduct.Columns.Add("Gold_Assets");
        //       dtMultiProduct.Columns.Add("Collectibles");
        //       dtMultiProduct.Columns.Add("Cash_and_Savings");
        //       dtMultiProduct.Columns.Add("Assets_Total");
        //       dtMultiProduct.Columns.Add("CustomerId");                

        //       foreach (DataRow dr in dtMultiProductMIS.Rows)
        //        {
        //            i++;
        //            if (int.Parse(dr["CustomerId"].ToString()) != tempCustId)
        //            {  
        //                if (tempCustId != 0)
        //                {
        //                    drMultiProduct[11] = String.Format("{0:n2}", (double.Parse(drMultiProduct[1].ToString()) + double.Parse(drMultiProduct[2].ToString()) +
        //                        double.Parse(drMultiProduct[3].ToString()) + double.Parse(drMultiProduct[4].ToString()) + double.Parse(drMultiProduct[5].ToString()) +
        //                        double.Parse(drMultiProduct[6].ToString()) + double.Parse(drMultiProduct[7].ToString()) + double.Parse(drMultiProduct[8].ToString()) +
        //                        double.Parse(drMultiProduct[9].ToString()) + double.Parse(drMultiProduct[10].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                    //drMultiProduct[13] = String.Format("{0:n2}", (double.Parse(drMultiProduct[11].ToString()) - double.Parse(drMultiProduct[12].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                    dtMultiProduct.Rows.Add(drMultiProduct);
        //                }
        //                tempCustId = int.Parse(dr["CustomerId"].ToString());
        //                drMultiProduct = dtMultiProduct.NewRow();
        //                drMultiProduct[0] = dr["CustomerName"].ToString();
        //                drMultiProduct[12] = dr["CustomerId"].ToString();
                        
        //                if (dr["AssetType"].ToString() == "Equity")
        //                    drMultiProduct[1] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Mutual Fund")
        //                    drMultiProduct[2] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Fixed Income")
        //                    drMultiProduct[3] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Government Savings")
        //                    drMultiProduct[4] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Property")
        //                    drMultiProduct[5] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Pension and Gratuities")
        //                    drMultiProduct[6] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Personal Assets")
        //                    drMultiProduct[7] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Gold Assets")
        //                    drMultiProduct[8] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Collectibles")
        //                    drMultiProduct[9] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Cash and Savings")
        //                    drMultiProduct[10] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                //else if (dr["AssetType"].ToString() == "Liabilities")
        //                //    drMultiProduct[12] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                if (i == dsMultiProductMIS.Tables[0].Rows.Count)
        //                {
        //                drMultiProduct[11] = double.Parse(drMultiProduct[1].ToString()) + double.Parse(drMultiProduct[2].ToString()) +
        //                    double.Parse(drMultiProduct[3].ToString()) + double.Parse(drMultiProduct[4].ToString()) + double.Parse(drMultiProduct[5].ToString()) +
        //                    double.Parse(drMultiProduct[6].ToString()) + double.Parse(drMultiProduct[7].ToString()) + double.Parse(drMultiProduct[8].ToString()) +
        //                    double.Parse(drMultiProduct[9].ToString()) + double.Parse(drMultiProduct[10].ToString());
        //                    //drMultiProduct[13] = double.Parse(drMultiProduct[11].ToString()) - double.Parse(drMultiProduct[12].ToString());
        //                    dtMultiProduct.Rows.Add(drMultiProduct);
        //                }
        //            }
        //            else
        //            {
        //                if (dr["AssetType"].ToString() == "Equity")
        //                    drMultiProduct[1] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Mutual Fund")
        //                    drMultiProduct[2] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Fixed Income")
        //                    drMultiProduct[3] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Government Savings")
        //                    drMultiProduct[4] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Property")
        //                    drMultiProduct[5] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Pension and Gratuities")
        //                    drMultiProduct[6] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Personal Assets")
        //                    drMultiProduct[7] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Gold Assets")
        //                    drMultiProduct[8] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Collectibles")
        //                    drMultiProduct[9] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                else if (dr["AssetType"].ToString() == "Cash and Savings")
        //                    drMultiProduct[10] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                //else if (dr["AssetType"].ToString() == "Liabilities")
        //                //    drMultiProduct[12] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                if (i == dsMultiProductMIS.Tables[0].Rows.Count)
        //                {
        //                    drMultiProduct[11] = double.Parse(drMultiProduct[1].ToString()) + double.Parse(drMultiProduct[2].ToString()) +
        //                        double.Parse(drMultiProduct[3].ToString()) + double.Parse(drMultiProduct[4].ToString()) + double.Parse(drMultiProduct[5].ToString()) +
        //                        double.Parse(drMultiProduct[6].ToString()) + double.Parse(drMultiProduct[7].ToString()) + double.Parse(drMultiProduct[8].ToString()) +
        //                        double.Parse(drMultiProduct[9].ToString()) + double.Parse(drMultiProduct[10].ToString());
        //                    //drMultiProduct[13] = double.Parse(drMultiProduct[11].ToString()) - double.Parse(drMultiProduct[12].ToString());
        //                    dtMultiProduct.Rows.Add(drMultiProduct);
        //                }                       
        //            }
        //        }

        //        //drMultiProduct = dtMultiProduct.NewRow();  //DataRow which holds the total of all columns to show in the footer
        //        //drMultiProduct[0] = "Total :";
        //        //foreach (DataRow dr in dtMultiProduct.Rows)
        //        //{
        //        //    drMultiProduct[1] = String.Format("{0:n2}", (double.Parse(drMultiProduct[1].ToString()) + double.Parse(dr[1].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //        //    drMultiProduct[2] = String.Format("{0:n2}", (double.Parse(drMultiProduct[2].ToString()) + double.Parse(dr[2].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //        //    drMultiProduct[3] = String.Format("{0:n2}", (double.Parse(drMultiProduct[3].ToString()) + double.Parse(dr[3].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //        //    drMultiProduct[4] = String.Format("{0:n2}", (double.Parse(drMultiProduct[4].ToString()) + double.Parse(dr[4].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //        //    drMultiProduct[5] = String.Format("{0:n2}", (double.Parse(drMultiProduct[5].ToString()) + double.Parse(dr[5].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //        //    drMultiProduct[6] = String.Format("{0:n2}", (double.Parse(drMultiProduct[6].ToString()) + double.Parse(dr[6].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //        //    drMultiProduct[7] = String.Format("{0:n2}", (double.Parse(drMultiProduct[7].ToString()) + double.Parse(dr[7].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //        //    drMultiProduct[8] = String.Format("{0:n2}", (double.Parse(drMultiProduct[8].ToString()) + double.Parse(dr[8].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //        //    drMultiProduct[9] = String.Format("{0:n2}", (double.Parse(drMultiProduct[9].ToString()) + double.Parse(dr[9].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //        //    drMultiProduct[10] = String.Format("{0:n2}", (double.Parse(drMultiProduct[10].ToString()) + double.Parse(dr[10].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //        //    //drMultiProduct[11] = String.Format("{0:n2}", (double.Parse(drMultiProduct[11].ToString()) + double.Parse(dr[11].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //        //    //drMultiProduct[12] = String.Format("{0:n2}", (double.Parse(drMultiProduct[12].ToString()) + double.Parse(dr[12].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //        //    //drMultiProduct[13] = String.Format("{0:n2}", (double.Parse(drMultiProduct[13].ToString()) + double.Parse(dr[13].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //        //}
        //        //drMultiProduct[12] = "0";

        //        rgvMultiProductMIS.DataSource = dtMultiProduct;
        //        //rgvMultiProductMIS.DataSourceID = String.Empty;
        //        rgvMultiProductMIS.DataBind();
        //    }

        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "MultiProductMIS.cs:bindRadGrid_rgvMultiProductMIS()");
        //        object[] objects = new object[2];
        //        //objects[0] = portfolioId;
        //        //objects[1] = dsGrpAssetNetHoldings;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        public void BindAssetInvestments()
        {
            int tempCustId = 0;
            int i = 0;
            customerId = 34152;
            branchId = 1148;
            try
            {
                SetParametersforDifferentRoles();
                dsGrpAssetNetHoldings = insuranceBo.GetAllProductMIS(advisorId, branchId, rmId, branchHeadId, customerId, isGroup);

                if (dsGrpAssetNetHoldings.Tables[0].Rows.Count == 0)
                {
                    lblErrorMsg.Text = "No records found for Multi-Product MIS";
                    lblErrorMsg.Visible = true;
                }
                else
                {
                    
                    //lblErrorMsg.Visible = false;
                    dtGrpAssetNetHoldings.Columns.Add("Customer_Name");
                    dtGrpAssetNetHoldings.Columns.Add("Equity");
                    dtGrpAssetNetHoldings.Columns.Add("Mutual_Fund");
                    dtGrpAssetNetHoldings.Columns.Add("Fixed_Income");
                    dtGrpAssetNetHoldings.Columns.Add("Government_Savings");
                    dtGrpAssetNetHoldings.Columns.Add("Property");
                    dtGrpAssetNetHoldings.Columns.Add("Pension_and_Gratuity");
                    dtGrpAssetNetHoldings.Columns.Add("Personal_Assets");
                    dtGrpAssetNetHoldings.Columns.Add("Gold_Assets");
                    dtGrpAssetNetHoldings.Columns.Add("Collectibles");
                    dtGrpAssetNetHoldings.Columns.Add("Cash_and_Savings");
                    //dtGrpAssetNetHoldings.Columns.Add("Assets_Total");
                    //dtGrpAssetNetHoldings.Columns.Add("Liabilities_Total");
                    //dtGrpAssetNetHoldings.Columns.Add("Net_Worth");
                    dtGrpAssetNetHoldings.Columns.Add("CustomerId");
                    //dtGrpAssetNetHoldings.Columns.Add("CustomerType");

                    foreach (DataRow dr in dsGrpAssetNetHoldings.Tables[0].Rows)
                    {


                        drNetHoldings = dtGrpAssetNetHoldings.NewRow();

                        //drNetHoldings[0] = dr["Customer_Name"].ToString();
                        //drNetHoldings[1] = dr["Equity"].ToString();
                        //drNetHoldings[2] = dr["Mutual_Fund"].ToString();
                        //drNetHoldings[3] = dr["Fixed_Income"].ToString();
                        //drNetHoldings[4] = dr["Government_Savings"].ToString();
                        //drNetHoldings[5] = dr["Property"].ToString();
                        //drNetHoldings[6] = dr["Pension_and_Gratuity"].ToString();
                        //drNetHoldings[7] = dr["Personal_Assets"].ToString();
                        //drNetHoldings[8] = dr["Gold_Assets"].ToString();
                        //drNetHoldings[9] = dr["Collectibles"].ToString();
                        //drNetHoldings[10] = dr["Cash_and_Savings"].ToString();
                        //drNetHoldings[11] = dr["C_CustomerId"].ToString();
                        //drNetHoldings[11] = dr["PAG_AssetGroupCode"].ToString(); 
                                           
                        drNetHoldings[0] = dr["Customer_Name"].ToString();
                        if (dr["AssetType"].ToString() == "DE")
                            if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                drNetHoldings[1] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else
                                drNetHoldings[1] = "N/A";

                        else if (dr["AssetType"].ToString() == "MF")
                            if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                drNetHoldings[2] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else
                                drNetHoldings[2] = "N/A";
                        else if (dr["AssetType"].ToString() == "FI")
                            if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                drNetHoldings[3] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else
                                drNetHoldings[3] = "N/A";
                        else if (dr["AssetType"].ToString() == "GS")
                            if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                drNetHoldings[4] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else
                                drNetHoldings[4] = "N/A";
                        else if (dr["AssetType"].ToString() == "PR")
                            if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                drNetHoldings[5] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else
                                drNetHoldings[5] = "N/A";
                        else if (dr["AssetType"].ToString() == "PG")
                            if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                drNetHoldings[6] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else
                                drNetHoldings[6] = "N/A";
                        else if (dr["AssetType"].ToString() == "PI")
                            if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                drNetHoldings[7] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else
                                drNetHoldings[7] = "N/A";
                        else if (dr["AssetType"].ToString() == "GD")
                            if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                drNetHoldings[8] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else
                                drNetHoldings[8] = "N/A";
                        else if (dr["AssetType"].ToString() == "CL")
                            if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                drNetHoldings[9] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else
                                drNetHoldings[9] = "N/A";
                        else if (dr["AssetType"].ToString() == "CS") 
                            if (dr["CFPAGD_WERPManagedValue"].ToString() != "")
                                drNetHoldings[10] = String.Format("{0:n2}", double.Parse(dr["CFPAGD_WERPManagedValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else
                                drNetHoldings[10] = "N/A";
                        dtGrpAssetNetHoldings.Rows.Add(drNetHoldings);                         
                    }
                    rgvMultiProductMIS.DataSource = dtGrpAssetNetHoldings;
                    rgvMultiProductMIS.DataBind();
                    rgvMultiProductMIS.Visible = true;
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
                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindAssetInvestments()");
                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = dsGrpAssetNetHoldings;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        //public void BindAssetInvestments()
        //{
        //    customerId = 34152;
        //    branchId = 1148;
        //    try
        //    {
        //        SetParametersforDifferentRoles();                
        //        dsGrpAssetNetHoldings = insuranceBo.GetAllProductMIS(advisorId, branchId, rmId, branchHeadId, customerId, isGroup);

        //        //liabilityValue = assetBo.GetCustomerPortfolioLiability(portfolioId);
        //        //if (dsGrpAssetNetHoldings.Tables[0].Rows.Count == 0)
        //        //{
        //        //    lblErrorMsg.Text = "No records found for Multi-Product MIS";
        //        //    lblErrorMsg.Visible = true;
        //        //}
        //        //else
        //        //{
                    
        //            //lblErrorMsg.Visible = false;
        //            dtGrpAssetNetHoldings.Columns.Add("Customer_Name");
        //            dtGrpAssetNetHoldings.Columns.Add("Equity");
        //            dtGrpAssetNetHoldings.Columns.Add("Mutual_Fund");
        //            dtGrpAssetNetHoldings.Columns.Add("Fixed_Income");
        //            dtGrpAssetNetHoldings.Columns.Add("Government_Savings");
        //            dtGrpAssetNetHoldings.Columns.Add("Property");
        //            dtGrpAssetNetHoldings.Columns.Add("Pension_and_Gratuity");
        //            dtGrpAssetNetHoldings.Columns.Add("Personal_Assets");
        //            dtGrpAssetNetHoldings.Columns.Add("Gold_Assets");
        //            dtGrpAssetNetHoldings.Columns.Add("Collectibles");
        //            dtGrpAssetNetHoldings.Columns.Add("Cash_and_Savings");
        //            //dtGrpAssetNetHoldings.Columns.Add("Assets_Total");
        //            //dtGrpAssetNetHoldings.Columns.Add("Liabilities_Total");
        //            //dtGrpAssetNetHoldings.Columns.Add("Net_Worth");
        //            dtGrpAssetNetHoldings.Columns.Add("CustomerId");
        //            //dtGrpAssetNetHoldings.Columns.Add("CustomerType");

        //            foreach (DataRow dr in dsGrpAssetNetHoldings.Tables[0].Rows)
        //            {

        //                drNetHoldings = dtGrpAssetNetHoldings.NewRow();

        //                drNetHoldings[0] = dr["Customer_Name"].ToString();
        //                drNetHoldings[1] = dr["Equity"].ToString();
        //                drNetHoldings[2] = dr["Mutual_Fund"].ToString();
        //                drNetHoldings[3] = dr["Fixed_Income"].ToString();
        //                drNetHoldings[4] = dr["Government_Savings"].ToString();
        //                drNetHoldings[5] = dr["Property"].ToString();
        //                drNetHoldings[6] = dr["Pension_and_Gratuity"].ToString();
        //                drNetHoldings[7] = dr["Personal_Assets"].ToString();
        //                drNetHoldings[8] = dr["Gold_Assets"].ToString();
        //                drNetHoldings[9] = dr["Collectibles"].ToString();
        //                drNetHoldings[10] = dr["Cash_and_Savings"].ToString();
        //                drNetHoldings[11] = dr["CustomerId"].ToString();

        //                dtGrpAssetNetHoldings.Rows.Add(drNetHoldings);
        //            }

        //            rgvMultiProductMIS.DataSource = dtGrpAssetNetHoldings;
        //            rgvMultiProductMIS.DataBind();
        //            rgvMultiProductMIS.Visible = true;
        //        }
        //    //}
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "MultiProductMIS.ascx:BindAssetInvestments()");
        //        object[] objects = new object[2];
        //        objects[0] = portfolioId;
        //        objects[1] = dsGrpAssetNetHoldings;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        public void BindFixedincomeMIS()
        {
            DataSet dsFIMIS = new DataSet();
            DataTable dtFIMIS = new DataTable();
            DataRow drFIMIS;
            try
            {
                SetParametersforDifferentRoles();
                dsFIMIS = insuranceBo.GetFixedincomeMISDetails(advisorId, branchId, rmId, branchHeadId, customerId, isGroup);
                //if (dsFIMIS.Tables[0].Rows.Count == 0)
                //{
                //    lblErrorMsg.Text = "No records found for Fixed Income MIS";
                //    lblErrorMsg.Visible = true;
                //}
                //else
                //{
                //  lblErrorMsg.Visible = false;
                    dtFIMIS.Columns.Add("Customer_Name");
                    dtFIMIS.Columns.Add("PAIC_AssetInstrumentCategoryName");
                    dtFIMIS.Columns.Add("CFINP_Name");
                    dtFIMIS.Columns.Add("CFINP_PurchaseDate");
                    dtFIMIS.Columns.Add("CFINP_MaturityDate");
                    dtFIMIS.Columns.Add("CFINP_SubsequentDepositAmount");
                    dtFIMIS.Columns.Add("CFINP_InterestRate");
                    dtFIMIS.Columns.Add("CFINP_CurrentValue");
                    dtFIMIS.Columns.Add("CFINP_MaturityValue");
                    dtFIMIS.Columns.Add("CustomerId");

                    foreach (DataRow dr in dsFIMIS.Tables[0].Rows)
                    {
                        drFIMIS = dtFIMIS.NewRow();

                        drFIMIS[0] = dr["CustomerName"].ToString();
                        drFIMIS[1] = dr["PAIC_AssetInstrumentCategoryName"].ToString();

                        if (dr["CFINP_Name"].ToString() != "")
                            drFIMIS[2] = dr["CFINP_Name"].ToString();
                        else
                            drFIMIS[2] = "N/A";

                        if (dr["CFINP_PurchaseDate"].ToString() != "")
                            drFIMIS[3] = (DateTime.Parse(dr["CFINP_PurchaseDate"].ToString())).ToShortDateString();
                        else
                            drFIMIS[3] = "N/A";

                        if (dr["CFINP_MaturityDate"].ToString() != "")
                            drFIMIS[4] = (DateTime.Parse(dr["CFINP_MaturityDate"].ToString())).ToShortDateString();
                        else
                            drFIMIS[4] = "N/A";


                        drFIMIS[5] = dr["CFINP_PrincipalAmount"].ToString();
                        drFIMIS[6] = dr["CFINP_InterestRate"].ToString();
                        drFIMIS[7] = dr["CFINP_CurrentValue"].ToString();
                        drFIMIS[8] = dr["CFINP_MaturityValue"].ToString();
                        drFIMIS[9] = dr["CustomerId"].ToString();

                        dtFIMIS.Rows.Add(drFIMIS);
                    }
                    rgvFixedIncomeMIS.DataSource = dtFIMIS;
                    rgvFixedIncomeMIS.DataBind();
                    rgvFixedIncomeMIS.Visible = true;
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

                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindCustomerAssetMaturityDates()");


                object[] objects = new object[2];

                objects[0] = portfolioId;
                objects[1] = dsFIMIS;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }


        protected void lnkCustomerNameAssetsGrid_Click(object sender, EventArgs e)
        {
            //GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
            //int rowIndex = gvRow.RowIndex;
            //DataKey dk = rgvMultiProductMIS.DataKeys[rowIndex];
            //int customerId = Convert.ToInt32(dk.Value);

            //customerVo = customerBo.GetCustomer(customerId);
            //Session["CustomerVo"] = customerVo;
            //Session["custStatusToShowGroupDashBoard"] = "customerStatus";

            //customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerId);
            //Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;

            //if (Session["S_CurrentUserRole"] == "Customer")
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrolCustomer('PortfolioDashboard','none');", true);
            //else
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioDashboard','none');", true);

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
        }

        protected void lnkBtnMultiProductMIS_Click(object sender, EventArgs e)
        {
            //bindRadGrid_rgvMultiProductMIS();
            rgvFixedIncomeMIS.Visible = false;
            rgvGeneralInsurance.Visible = false;
            rgvLifeInsurance.Visible = false;
            rgvMultiProductMIS.Visible = true;
            BindAssetInvestments();           
        }     

        protected void lnkBtnInvestmentMIS_Click(object sender, EventArgs e)
        {
            rgvFixedIncomeMIS.Visible = true;
            rgvGeneralInsurance.Visible = false;
            rgvLifeInsurance.Visible = false;
            rgvMultiProductMIS.Visible = false;
            BindFixedincomeMIS();
        }

        
        public void lnkBtnLifeInsuranceMIS_OnClick(object sender, EventArgs e)
        {
            rgvFixedIncomeMIS.Visible = false;
            rgvGeneralInsurance.Visible = false;
            rgvLifeInsurance.Visible = true;
            rgvMultiProductMIS.Visible = false;
            BindLifeInsuranceDetails();
        }

        public void lnkBtnGeneralInsuranceMIS_OnClick(object sender, EventArgs e)
        {
            rgvFixedIncomeMIS.Visible = false;
            rgvGeneralInsurance.Visible = true;
            rgvLifeInsurance.Visible = false;
            rgvMultiProductMIS.Visible = false;
            BindGeneralInsuranceDetails();
        }

        public void BindLifeInsuranceDetails()
        {
            int isGroup;
            if (ddlCustomerType.SelectedValue == "Select")
               isGroup = 85;
            else
             isGroup = int.Parse(ddlCustomerType.SelectedValue);
            string asset = lnkBtnLifeInsuranceMIS.Text;
            DataTable dtLifeInsDetails = new DataTable();
            try
            {
                SetParametersforDifferentRoles();
                //Binding the Life Insurance Grid
                dsInsuranceDetails = insuranceBo.GetMultiProductMISInsuranceDetails(advisorId, branchId, branchHeadId, rmId, customerId, asset, isGroup);
                if (dsInsuranceDetails.Tables[0].Rows.Count == 0)
                {
                    //rgvGeneralInsurance.MasterTableView.Rebind();
                    //rgvGeneralInsurance.Rebind();
                    lblErrorMsg.Text = "No records found for Life Insurance MIS";
                    lblErrorMsg.Visible = true;
                }
                else
                {
                    lblErrorMsg.Visible = false;

                    dtLifeInsDetails.Columns.Add("CustomerName");
                    dtLifeInsDetails.Columns.Add("PolicyIssuerName");
                    dtLifeInsDetails.Columns.Add("Particulars");

                    dtLifeInsDetails.Columns.Add("InsuranceType");
                    dtLifeInsDetails.Columns.Add("SumAssured");
                    dtLifeInsDetails.Columns.Add("PremiumAmount");
                    dtLifeInsDetails.Columns.Add("PremiumFrequency");
                    dtLifeInsDetails.Columns.Add("CommencementDate");
                    dtLifeInsDetails.Columns.Add("MaturityValue");
                    dtLifeInsDetails.Columns.Add("MaturityDate");

                    dtLifeInsDetails.Columns.Add("CustomerId");
                    dtLifeInsDetails.Columns.Add("InsuranceNPId");

                    foreach (DataRow dr in dsInsuranceDetails.Tables[0].Rows)
                    {
                        drLifeInsurance = dtLifeInsDetails.NewRow();

                        drLifeInsurance[0] = dr["CustomerName"].ToString();
                        drLifeInsurance[1] = dr["PolicyIssuerName"].ToString();
                        drLifeInsurance[2] = dr["Particulars"].ToString();
                        drLifeInsurance[3] = dr["InsuranceType"].ToString();
                        drLifeInsurance[4] = String.Format("{0:n2}", decimal.Parse(dr["SumAssured"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drLifeInsurance[5] = String.Format("{0:n2}", decimal.Parse(dr["PremiumAmount"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drLifeInsurance[6] = dr["PremiumFrequency"].ToString();
                        drLifeInsurance[7] = DateTime.Parse(dr["CommencementDate"].ToString()).ToShortDateString();
                        drLifeInsurance[8] = String.Format("{0:n2}", decimal.Parse(dr["MaturityValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drLifeInsurance[9] = DateTime.Parse(dr["MaturityDate"].ToString()).ToShortDateString();
                        drLifeInsurance[10] = dr["CustomerId"].ToString();
                        drLifeInsurance[11] = dr["InsuranceNPId"].ToString();

                        dtLifeInsDetails.Rows.Add(drLifeInsurance);
                    }
                    rgvLifeInsurance.DataSource = dtLifeInsDetails;
                    rgvLifeInsurance.DataBind();
                    rgvLifeInsurance.Visible = true;
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

                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindGroupInsuranceDetails()");

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

            //int isGroup = int.Parse(ddlCustomerType.SelectedValue);
            branchId = int.Parse(ddlBranch.SelectedValue);
            string asset = lnkBtnGeneralInsuranceMIS.Text;
            if (hdnCustomerId.Value != "")
                customerId = int.Parse(hdnCustomerId.Value);
            else
                customerId = 0;
            DataTable dtGenInsDetails = new DataTable();
            try
            {
                SetParametersforDifferentRoles();
                //Binding the General Insurance Grid
                dsInsuranceDetails = insuranceBo.GetMultiProductMISInsuranceDetails(advisorId, branchId, branchHeadId, rmId, customerId, asset, isGroup);
                if (dsInsuranceDetails.Tables[0].Rows.Count == 0)
                {
                    //rgvGeneralInsurance.MasterTableView.Rebind();
                    //rgvGeneralInsurance.Rebind();
                    lblErrorMsg.Text = "No records found for General Insurance MIS";
                    lblErrorMsg.Visible = true;
                }
                else
                {
                    lblErrorMsg.Visible = false;

                    dtGenInsDetails.Columns.Add("CustomerName");
                    dtGenInsDetails.Columns.Add("PolicyIssuerName");
                    dtGenInsDetails.Columns.Add("Particulars");
                    dtGenInsDetails.Columns.Add("InsuranceType");
                    dtGenInsDetails.Columns.Add("SumAssured");
                    dtGenInsDetails.Columns.Add("PremiumAmount");
                    dtGenInsDetails.Columns.Add("PremiumFrequency");
                    dtGenInsDetails.Columns.Add("CommencementDate");
                    //dtGenInsDetails.Columns.Add("MaturityValue");
                    dtGenInsDetails.Columns.Add("MaturityDate");
                    dtGenInsDetails.Columns.Add("CustomerId");
                    dtGenInsDetails.Columns.Add("GenInsuranceNPId");


                    foreach (DataRow dr in dsInsuranceDetails.Tables[0].Rows)
                    {
                        drGeneralInsurance = dtGenInsDetails.NewRow();

                        drGeneralInsurance[0] = dr["CustomerName"].ToString();
                        drGeneralInsurance[1] = dr["PolicyIssuerName"].ToString();
                        drGeneralInsurance[2] = dr["Particulars"].ToString();
                        drGeneralInsurance[3] = dr["InsuranceType"].ToString();
                        drGeneralInsurance[4] = String.Format("{0:n2}", decimal.Parse(dr["SumAssured"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drGeneralInsurance[5] = String.Format("{0:n2}", decimal.Parse(dr["PremiumAmount"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drGeneralInsurance[6] = dr["PremiumFrequency"].ToString();
                        drGeneralInsurance[7] = DateTime.Parse(dr["CommencementDate"].ToString()).ToShortDateString();
                        drGeneralInsurance[8] = DateTime.Parse(dr["MaturityDate"].ToString()).ToShortDateString();
                        drGeneralInsurance[9] = dr["CustomerId"].ToString();
                        drGeneralInsurance[10] = dr["GenInsuranceNPId"].ToString();
                        
                        dtGenInsDetails.Rows.Add(drGeneralInsurance);
                    }
                    rgvGeneralInsurance.DataSource = dtGenInsDetails;
                    rgvGeneralInsurance.DataBind();
                    rgvGeneralInsurance.Visible = true;
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

                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindGroupInsuranceDetails()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}