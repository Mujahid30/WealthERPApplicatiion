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
using BoCustomerRiskProfiling;

namespace WealthERP.BusinessMIS
{
    public partial class AssetAllocationMIS : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        CustomerBo customerBo = new CustomerBo();
        RiskProfileBo riskprofilebo = new RiskProfileBo();
        UserVo userVo = new UserVo();

        int advisorId = 0;
        int customerId = 0;
        String userType;
        int rmId = 0;
        int bmID = 0;

        int all = 0;
        int branchId = 0;
        int branchHeadId = 0;
        int isGroup;
        int isIndividualOrGroup = 0;

        string customerType = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();
            if (!IsPostBack)
            {
                ErrorMessage.Visible = false;
                ddlCustomerType.Visible = false;
                lblSelectTypeOfCustomer.Visible = false;
                trCustomerSearch.Visible = false;
                //lblselectCustomer.Visible = false;
                //txtIndividualCustomer.Visible = false;              
                rquiredFieldValidatorIndivudialCustomer.Visible = false;
                btnImagExport.Visible = false;
                tbl.Visible = false;
                gvAssetAllocationMIS.Visible = false;

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
        protected void ddlSelectCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectCustomer.SelectedItem.Value == "All Customer")
            {
                ddlCustomerType.Visible = false;
                lblSelectTypeOfCustomer.Visible = false;
                trCustomerSearch.Visible = false;
                rquiredFieldValidatorIndivudialCustomer.Visible = false;
                ddlCustomerType.SelectedIndex = 0;
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
                ddlCustomerType.SelectedIndex = 0;

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

        protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnCustomerId.Value.ToString().Trim()))
            {
                customerId = int.Parse(hdnCustomerId.Value);
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            BindAssetAllocationMISGrid();
        }

        private void BindAssetAllocationMISGrid()
        {
            
            int classificationCode;
            int customerId = 0 ;
            int customerIdOld = 0;
            int customerOld1 = 0;
            int customerIdFilter = 0;
            DataSet dsGetAssetAllocationlist = new DataSet();
            DataTable dtAssetAllocation = new DataTable();
            DataTable dtRiskProfile = new DataTable();
            if (!string.IsNullOrEmpty(hdnCustomerId.Value.ToString().Trim()))
            {
                customerIdFilter = int.Parse(hdnCustomerId.Value);
            }
            if (ddlCustomerType.SelectedValue == "0")
                isGroup = 0;
            else if (ddlCustomerType.SelectedValue == "1")
                isGroup = 1;
            SetParameter();
            dsGetAssetAllocationlist = riskprofilebo.GetAssetAllocationMIS(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), customerIdFilter, int.Parse(hdnbranchheadId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnAll.Value), isGroup);

            DataTable dtAssetallocationDetails = new DataTable();

            dtAssetallocationDetails.Columns.Add("CustomerName");
            dtAssetallocationDetails.Columns.Add("AssetClassificationCode");
            dtAssetallocationDetails.Columns.Add("EquityCurrentValue", typeof(double));
            dtAssetallocationDetails.Columns.Add("EquityCurrentPer", typeof(double));
            dtAssetallocationDetails.Columns.Add("EquityRecomendedValue", typeof(double));
            dtAssetallocationDetails.Columns.Add("EquityRecomendedPer", typeof(double));
            dtAssetallocationDetails.Columns.Add("EqValueAction", typeof(double));
            dtAssetallocationDetails.Columns.Add("EqPerAction", typeof(double));
            

            dtAssetallocationDetails.Columns.Add("DebtCurrentValue", typeof(double));
            dtAssetallocationDetails.Columns.Add("DebtCurrentPer", typeof(double));
            dtAssetallocationDetails.Columns.Add("DebtRecomendedValue", typeof(double));
            dtAssetallocationDetails.Columns.Add("DebtRecomendedPer", typeof(double));
            dtAssetallocationDetails.Columns.Add("DtValueAction", typeof(double));
            dtAssetallocationDetails.Columns.Add("DtPerAction", typeof(double));
            

            dtAssetallocationDetails.Columns.Add("CashCurrentValue", typeof(double));
            dtAssetallocationDetails.Columns.Add("CashCurrentPer", typeof(double));
            dtAssetallocationDetails.Columns.Add("CashRecomendedValue", typeof(double));
            dtAssetallocationDetails.Columns.Add("CashRecomendedPer", typeof(double));
            dtAssetallocationDetails.Columns.Add("CaValueAction", typeof(double));
            dtAssetallocationDetails.Columns.Add("CaPerAction", typeof(double));
            

            dtAssetallocationDetails.Columns.Add("AlternateCurrentValue", typeof(double));
            dtAssetallocationDetails.Columns.Add("AlternateCurrentPer", typeof(double));
            dtAssetallocationDetails.Columns.Add("AlternateRecomendedValue", typeof(double));
            dtAssetallocationDetails.Columns.Add("AlternateRecomendedPer", typeof(double));
            dtAssetallocationDetails.Columns.Add("AltValueAction", typeof(double));
            dtAssetallocationDetails.Columns.Add("AltPerAction", typeof(double));
            

            #region Data Table Default value

            dtAssetallocationDetails.Columns["EquityCurrentValue"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["EquityCurrentPer"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["EquityRecomendedValue"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["EquityRecomendedPer"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["EqValueAction"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["EqPerAction"].DefaultValue = 0;

            dtAssetallocationDetails.Columns["DebtCurrentValue"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["DebtCurrentPer"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["DebtRecomendedValue"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["DebtRecomendedPer"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["DtValueAction"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["DtPerAction"].DefaultValue = 0;

            dtAssetallocationDetails.Columns["CashCurrentValue"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["CashCurrentPer"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["CashRecomendedValue"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["CashRecomendedPer"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["CaValueAction"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["CaPerAction"].DefaultValue = 0;

            dtAssetallocationDetails.Columns["AlternateCurrentValue"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["AlternateCurrentPer"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["AlternateRecomendedValue"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["AlternateRecomendedPer"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["AltValueAction"].DefaultValue = 0;
            dtAssetallocationDetails.Columns["AltPerAction"].DefaultValue = 0;

            #endregion Data Table Default value



            DataTable dtFinanceTotal = new DataTable();
            if (dsGetAssetAllocationlist != null)
            {
                if (dsGetAssetAllocationlist.Tables.Count > 2)
                {
                    dtFinanceTotal = dsGetAssetAllocationlist.Tables[2];
                }
            }
            DataRow drAssetAllocationDetails;
            double  sumAssetTotal=0;
            DataRow[] drAssetAllocationCustomerWise;
            if (dsGetAssetAllocationlist.Tables.Count > 1)
            {
                dtAssetAllocation = dsGetAssetAllocationlist.Tables[0];
                dtRiskProfile = dsGetAssetAllocationlist.Tables[1];
                //if (dtAssetAllocation.Rows.Count > 0)
                //{
                foreach (DataRow drAssetAllCustomer in dtAssetAllocation.Rows)
                {
                  
                    Int32.TryParse(drAssetAllCustomer["C_CustomerId"].ToString(), out customerId);

                    if(customerId != customerIdOld)
                    { //go for another row to find new customer
                        customerIdOld = customerId;
                        drAssetAllocationDetails = dtAssetallocationDetails.NewRow();
                    if(customerId != 0)
                    { // add row in manual datatable within this brace end
                       drAssetAllocationCustomerWise = dtAssetAllocation.Select("C_CustomerId=" + customerId.ToString());
                        drAssetAllocationDetails["CustomerName"] = drAssetAllCustomer["CustomerName"].ToString();
                        double sumFinancialAssetTotal = 0;
                   object sumObject;
                   sumObject = dtFinanceTotal.Compute("Sum(FinancialTotal)", "C_CustomerId = " + "'" + customerId + "'");
                   double.TryParse(Convert.ToString(sumObject), out sumFinancialAssetTotal);

                   sumObject = dtAssetAllocation.Compute("Sum(CurrentValue)", "C_CustomerId = " + "'" + customerId + "'");
                   double.TryParse(Convert.ToString(sumObject), out sumAssetTotal);
                   if (drAssetAllocationCustomerWise.Count() > 0)
                   {
                       double currentPercentage=0;
                       foreach (DataRow dr in drAssetAllocationCustomerWise)
                       {

                           classificationCode = int.Parse(dr["WAC_AssetClassificationCode"].ToString());
                           if(sumAssetTotal!=0)
                               currentPercentage = (double.Parse(dr["CurrentValue"].ToString()) / sumAssetTotal) * 100;

                           switch (classificationCode)
                           {
                               case 1:
                                   {
                                       //drAssetAllocationDetails["EquityCurrentValue"] =Math.Round( double.Parse(dr["CurrentValue"].ToString()),0);
                                       
                                       drAssetAllocationDetails["EquityCurrentPer"] = Math.Round(currentPercentage,2);
                                       drAssetAllocationDetails["EquityCurrentValue"] = Math.Round(((currentPercentage * sumFinancialAssetTotal) / 100), 2);
                                       break;
                                   }
                               case 2:
                                   {
                                       //drAssetAllocationDetails["DebtCurrentValue"] = Math.Round(double.Parse(dr["CurrentValue"].ToString()), 0);

                                       drAssetAllocationDetails["DebtCurrentPer"] = Math.Round(currentPercentage,2);
                                       drAssetAllocationDetails["DebtCurrentValue"] = Math.Round(((currentPercentage * sumFinancialAssetTotal) / 100), 2);
                                       break;
                                   }
                               case 3:
                                   {
                                       //drAssetAllocationDetails["CashCurrentValue"] = Math.Round(double.Parse(dr["CurrentValue"].ToString()), 0);
                                       drAssetAllocationDetails["CashCurrentPer"] = Math.Round(currentPercentage,2);
                                       drAssetAllocationDetails["CashCurrentValue"] = Math.Round(((currentPercentage * sumFinancialAssetTotal) / 100), 2);
                                       break;
                                   }
                               case 4:
                                   {
                                       //drAssetAllocationDetails["AlternateCurrentValue"] = Math.Round(double.Parse(dr["CurrentValue"].ToString()), 0);
                                       drAssetAllocationDetails["AlternateCurrentPer"] = Math.Round(currentPercentage, 2);
                                       drAssetAllocationDetails["AlternateCurrentValue"] = Math.Round(((currentPercentage * sumFinancialAssetTotal) / 100), 2);
                                       break;
                                   }
                           }
                           DataRow[] drAssetType;

                           drAssetType = dtRiskProfile.Select("C_CustomerId=" + customerId.ToString());
                           //drAssetType = dtRiskProfile.Select("C_CustomerId=" + customerId1.ToString());
                           int assetCode=0;
                           if (customerId != customerOld1)
                           {
                               if (drAssetType.Count() > 0)
                               {
                                   customerOld1 = customerId;
                                   foreach (DataRow dr1 in drAssetType)
                                   {
                                       if(!string.IsNullOrEmpty( dr1["WAC_AssetClassificationCode"].ToString().Trim()))
                                            assetCode = int.Parse(dr1["WAC_AssetClassificationCode"].ToString());
                                       switch (assetCode)
                                       {
                                           case 1:
                                               {
                                                   if (!string.IsNullOrEmpty(dr1["CAA_RecommendedPercentage"].ToString().Trim()))
                                                   {
                                                       drAssetAllocationDetails["EquityRecomendedValue"] = Math.Round((double.Parse(dr1["CAA_RecommendedPercentage"].ToString()) * (sumFinancialAssetTotal / 100)),0);
                                                       drAssetAllocationDetails["EquityRecomendedPer"] = Math.Round(double.Parse( dr1["CAA_RecommendedPercentage"].ToString()),2);                                                     
                                                   }
                                                   break;
                                               }
                                           case 2:
                                               {
                                                   if (!string.IsNullOrEmpty(dr1["CAA_RecommendedPercentage"].ToString().Trim()))
                                                   {
                                                       drAssetAllocationDetails["DebtRecomendedValue"] = Math.Round((double.Parse(dr1["CAA_RecommendedPercentage"].ToString()) * (sumFinancialAssetTotal / 100)), 0);
                                                       drAssetAllocationDetails["DebtRecomendedPer"] = Math.Round(double.Parse(dr1["CAA_RecommendedPercentage"].ToString()), 2);
                                                   }
                                                   break;
                                               }
                                           case 3:
                                              
                                                   if (!string.IsNullOrEmpty(dr1["CAA_RecommendedPercentage"].ToString().Trim()))
                                                   {
                                                       drAssetAllocationDetails["CashRecomendedValue"] = Math.Round((double.Parse(dr1["CAA_RecommendedPercentage"].ToString()) * (sumFinancialAssetTotal / 100)), 0);
                                                       drAssetAllocationDetails["CashRecomendedPer"] = Math.Round(double.Parse(dr1["CAA_RecommendedPercentage"].ToString()), 2);
                                                   }
                                                   break;

                                               
                                           case 4:
                                               {
                                                   if (!string.IsNullOrEmpty(dr1["CAA_RecommendedPercentage"].ToString().Trim()))
                                                   {
                                                       drAssetAllocationDetails["AlternateRecomendedValue"] = Math.Round((double.Parse(dr1["CAA_RecommendedPercentage"].ToString()) * (sumFinancialAssetTotal / 100)), 0);
                                                       drAssetAllocationDetails["AlternateRecomendedPer"] = Math.Round(double.Parse(dr1["CAA_RecommendedPercentage"].ToString()), 2);
                                                   }
                                                   break;
                                               }
                                       }
                                   }
                               }

                           }
                       }

                       drAssetAllocationDetails["EqValueAction"] = Math.Round(double.Parse(drAssetAllocationDetails["EquityRecomendedValue"].ToString())) - Math.Round(double.Parse(drAssetAllocationDetails["EquityCurrentValue"].ToString()));
                       drAssetAllocationDetails["EqPerAction"] = Math.Round(double.Parse(drAssetAllocationDetails["EquityRecomendedPer"].ToString())) - Math.Round(double.Parse(drAssetAllocationDetails["EquityCurrentPer"].ToString()));

                       drAssetAllocationDetails["DtValueAction"] = Math.Round(double.Parse(drAssetAllocationDetails["DebtRecomendedValue"].ToString())) - Math.Round(double.Parse(drAssetAllocationDetails["DebtCurrentValue"].ToString()));
                       drAssetAllocationDetails["DtPerAction"] = Math.Round(double.Parse(drAssetAllocationDetails["DebtRecomendedPer"].ToString())) - Math.Round(double.Parse(drAssetAllocationDetails["DebtCurrentPer"].ToString()));
      
                       drAssetAllocationDetails["AltValueAction"] = Math.Round(double.Parse(drAssetAllocationDetails["AlternateRecomendedValue"].ToString())) - Math.Round(double.Parse(drAssetAllocationDetails["AlternateCurrentValue"].ToString()));
                       drAssetAllocationDetails["AltPerAction"] = Math.Round(double.Parse(drAssetAllocationDetails["AlternateRecomendedPer"].ToString())) - Math.Round(double.Parse(drAssetAllocationDetails["AlternateCurrentPer"].ToString()));

                       drAssetAllocationDetails["CaValueAction"] = Math.Round(double.Parse(drAssetAllocationDetails["CashRecomendedValue"].ToString())) - Math.Round(double.Parse(drAssetAllocationDetails["CashCurrentValue"].ToString()));
                       drAssetAllocationDetails["CaPerAction"] = Math.Round(double.Parse(drAssetAllocationDetails["CashRecomendedPer"].ToString())) - Math.Round(double.Parse(drAssetAllocationDetails["CashCurrentPer"].ToString()));
                                                  
 

                   }
                   dtAssetallocationDetails.Rows.Add(drAssetAllocationDetails);

                   
                        
                }//*
    
                }//**

            }//***
                if (dtAssetallocationDetails.Rows.Count > 0)
                {
                    gvAssetAllocationMIS.DataSource = dtAssetallocationDetails;
                    gvAssetAllocationMIS.DataBind();
                    tbl.Visible = true;
                    gvAssetAllocationMIS.Visible = true;
                    if (Cache["AssetAllocationMIS" + advisorVo.advisorId] == null)
                    {
                        Cache.Insert("AssetAllocationMIS" + userVo.UserId, dtAssetallocationDetails);
                    }
                    else
                    {
                        Cache.Remove("AssetAllocationMIS" + userVo.UserId);
                        Cache.Insert("AssetAllocationMIS" + userVo.UserId, dtAssetallocationDetails);
                    }
                    ErrorMessage.Visible = false;
                    btnImagExport.Visible = true;
                }
                else
                {
                    gvAssetAllocationMIS.DataSource = null;
                    gvAssetAllocationMIS.DataBind();
                    ErrorMessage.Visible = true;
                    btnImagExport.Visible = false;
                }
          }
            else
            {
                tbl.Visible = false;
                gvAssetAllocationMIS.Visible = false;
                ErrorMessage.Visible = true;
                btnImagExport.Visible = false;
            }
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
        protected void gvAssetAllocationMIS_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtAssetallocationDetails = new DataTable();
            dtAssetallocationDetails = (DataTable)Cache["AssetAllocationMIS" + userVo.UserId];
            gvAssetAllocationMIS.DataSource = dtAssetallocationDetails;
            gvAssetAllocationMIS.Visible = true;
        }
        protected void btnImagExport_Click(object sender, ImageClickEventArgs e)
        {
            gvAssetAllocationMIS.ExportSettings.OpenInNewWindow = true;
            gvAssetAllocationMIS.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvAssetAllocationMIS.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvAssetAllocationMIS.MasterTableView.ExportToExcel();
        }
        protected void gvAssetAllocationMIS_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //GridDataItem dataItem = e.Item as GridDataItem;
                //Image imgEqIndicator = (Image)dataItem.FindControl("imgEqIndicator");
                 //ImageButton imgEqIndicator = e.Item.FindControl("imgEqIndicator") as ImageButton;

                Image imgEqIndicator = e.Item.FindControl("imgEqIndicator") as Image;
                Image imgDtIndicator = e.Item.FindControl("imgDtIndicator") as Image;
                Image imgCaIndicator = e.Item.FindControl("imgCaIndicator") as Image;
                Image imgAltIndicator = e.Item.FindControl("imgAltIndicator") as Image;
                //imgDtIndicator
                //    imgCaIndicator
                //    imgAltIndicator
                ////System.Web.UI.WebControls.Image imgEqIndicator = e.Item.FindControl("imgEqIndicator") as System.Web.UI.WebControls.Image;
                //System.Web.UI.WebControls.Image imgDtIndicator = e.Item.FindControl("imgDtIndicator") as System.Web.UI.WebControls.Image;
                //System.Web.UI.WebControls.Image imgCaIndicator = e.Item.FindControl("imgCaIndicator") as System.Web.UI.WebControls.Image;
                //System.Web.UI.WebControls.Image imgAltIndicator = e.Item.FindControl("imgAltIndicator") as System.Web.UI.WebControls.Image;

                double eqCur = double.Parse(e.Item.Cells[3].Text); 
                double eqRec = double.Parse(e.Item.Cells[5].Text);
                double dtCur = double.Parse(e.Item.Cells[10].Text);
                double dtRec = double.Parse(e.Item.Cells[12].Text);
                double cashCur = double.Parse(e.Item.Cells[17].Text);
                double cashRec = double.Parse(e.Item.Cells[19].Text);
                double altCur = double.Parse(e.Item.Cells[24].Text);
                double altRec = double.Parse(e.Item.Cells[26].Text);
                GridDataItem item = (GridDataItem)e.Item;

                if (eqRec > eqCur)
                {
                    e.Item.Cells[7].ForeColor = System.Drawing.Color.Green;
                    e.Item.Cells[8].ForeColor = System.Drawing.Color.Green;
                    imgEqIndicator.Visible = true;
                    imgEqIndicator.ImageUrl = "~/Images/GreenUpArrow.png";
                }
                else if (eqCur > eqRec)
                {
                    e.Item.Cells[7].ForeColor = System.Drawing.Color.Red;
                    e.Item.Cells[8].ForeColor = System.Drawing.Color.Red;
                    imgEqIndicator.Visible = true;
                    imgEqIndicator.ImageUrl = "~/Images/RedDownArrow.png";
                }
                else if (eqRec == eqCur)
                {
                    e.Item.Cells[7].ForeColor = System.Drawing.Color.Black;
                    e.Item.Cells[8].ForeColor = System.Drawing.Color.Black;
                    imgEqIndicator.Visible = false;
                }
                //----------------------
                if (dtRec > dtCur)
                {
                    e.Item.Cells[14].ForeColor = System.Drawing.Color.Green;
                    e.Item.Cells[15].ForeColor = System.Drawing.Color.Green;
                    imgDtIndicator.ImageUrl = "~/Images/GreenUpArrow.png";
                }
                else if (dtCur > dtRec)
                {
                    e.Item.Cells[14].ForeColor = System.Drawing.Color.Red;
                    e.Item.Cells[15].ForeColor = System.Drawing.Color.Red;
                    imgDtIndicator.ImageUrl = "~/Images/RedDownArrow.png";
                }
                else if (dtCur == dtRec)
                {
                    imgDtIndicator.Visible = false;
                    e.Item.Cells[14].ForeColor = System.Drawing.Color.Black;
                    e.Item.Cells[15].ForeColor = System.Drawing.Color.Black;
                }
                //----------------------
                if (cashRec > cashCur)
                {
                    imgCaIndicator.ImageUrl = "~/Images/GreenUpArrow.png";
                    e.Item.Cells[21].ForeColor = System.Drawing.Color.Green;
                    e.Item.Cells[22].ForeColor = System.Drawing.Color.Green;
                }
                else if (cashCur > cashRec)
                {
                    e.Item.Cells[21].ForeColor = System.Drawing.Color.Red;
                    e.Item.Cells[22].ForeColor = System.Drawing.Color.Red;
                    imgCaIndicator.ImageUrl = "~/Images/RedDownArrow.png";
                }
                else if (cashCur == cashRec)
                {
                    imgCaIndicator.Visible = false;
                    e.Item.Cells[21].ForeColor = System.Drawing.Color.Black;
                    e.Item.Cells[22].ForeColor = System.Drawing.Color.Black;
                }

                //----------------------
                if (altRec > altCur)
                {
                    imgAltIndicator.ImageUrl = "~/Images/GreenUpArrow.png";
                    e.Item.Cells[28].ForeColor = System.Drawing.Color.Green;
                    e.Item.Cells[29].ForeColor = System.Drawing.Color.Green;
                }
                else if (altCur > altRec)
                {
                    e.Item.Cells[28].ForeColor = System.Drawing.Color.Red;
                    e.Item.Cells[29].ForeColor = System.Drawing.Color.Red;
                    imgAltIndicator.ImageUrl = "~/Images/RedDownArrow.png";
                }
                else if (altCur == altRec)
                {
                    imgAltIndicator.Visible = false;
                    e.Item.Cells[28].ForeColor = System.Drawing.Color.Black;
                    e.Item.Cells[29].ForeColor = System.Drawing.Color.Black;
                }
            } 

        }
    }

}
