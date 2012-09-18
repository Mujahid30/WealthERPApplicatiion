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

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
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
            int customerId1 = 0;
            int customerOld1 = 0;
            DataSet dsGetAssetAllocationlist = new DataSet();
            DataTable dtAssetAllocation = new DataTable();
            DataTable dtRiskProfile = new DataTable();
            
            SetParameter();
            dsGetAssetAllocationlist = riskprofilebo.GetAssetAllocationMIS(int.Parse(hdnadviserId.Value));

            DataTable dtAssetallocationDetails = new DataTable();

            dtAssetallocationDetails.Columns.Add("CustomerName");
            dtAssetallocationDetails.Columns.Add("AssetClassificationCode");
            dtAssetallocationDetails.Columns.Add("EquityCurrentValue");
            dtAssetallocationDetails.Columns.Add("EquityCurrentPer");
            dtAssetallocationDetails.Columns.Add("EquityRecomendedValue");
            dtAssetallocationDetails.Columns.Add("EquityRecomendedPer");
            dtAssetallocationDetails.Columns.Add("DebtCurrentValue");
            dtAssetallocationDetails.Columns.Add("DebtCurrentPer");
            dtAssetallocationDetails.Columns.Add("DebtRecomendedValue");
            dtAssetallocationDetails.Columns.Add("DebtRecomendedPer");
            dtAssetallocationDetails.Columns.Add("CashCurrentValue");
            dtAssetallocationDetails.Columns.Add("CashCurrentPer");
            dtAssetallocationDetails.Columns.Add("CashRecomendedValue");
            dtAssetallocationDetails.Columns.Add("CashRecomendedPer");
            dtAssetallocationDetails.Columns.Add("AlternateCurrentValue");
            dtAssetallocationDetails.Columns.Add("AlternateCurrentPer");
            dtAssetallocationDetails.Columns.Add("AlternateRecomendedValue");
            dtAssetallocationDetails.Columns.Add("AlternateRecomendedPer");

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
                        
                   if (drAssetAllocationCustomerWise.Count() > 0)
                   {
                    foreach (DataRow dr in drAssetAllocationCustomerWise)
                    {
                        sumAssetTotal = sumAssetTotal + double.Parse(dr["CurrentValue"].ToString());
                    }
                   }

                   if (drAssetAllocationCustomerWise.Count() > 0)
                   {
                       foreach (DataRow dr in drAssetAllocationCustomerWise)
                       {

                           classificationCode = int.Parse(dr["WAC_AssetClassificationCode"].ToString());
                           switch (classificationCode)
                           {
                               case 1:
                                   {
                                       drAssetAllocationDetails["EquityCurrentValue"] = dr["CurrentValue"].ToString();
                                       drAssetAllocationDetails["EquityCurrentPer"] = (double.Parse(dr["CurrentValue"].ToString()) / sumAssetTotal) * 100;
                                       break;
                                   }
                               case 2:
                                   {
                                       drAssetAllocationDetails["DebtCurrentValue"] = dr["CurrentValue"].ToString();
                                       drAssetAllocationDetails["DebtCurrentPer"] = (double.Parse(dr["CurrentValue"].ToString()) / sumAssetTotal) * 100;
                                       break;
                                   }
                               case 3:
                                   {
                                       drAssetAllocationDetails["CashCurrentValue"] = dr["CurrentValue"].ToString();
                                       drAssetAllocationDetails["CashCurrentPer"] = (double.Parse(dr["CurrentValue"].ToString()) / sumAssetTotal) * 100;
                                       break;
                                   }
                               case 4:
                                   {
                                       drAssetAllocationDetails["AlternateCurrentValue"] = dr["CurrentValue"].ToString();
                                       drAssetAllocationDetails["AlternateCurrentPer"] = (double.Parse(dr["CurrentValue"].ToString()) / sumAssetTotal) * 100;
                                       break;
                                   }
                           }
                           DataRow[] drAssetType;

                           drAssetType = dtRiskProfile.Select("C_CustomerId=" + customerId.ToString());
                           //drAssetType = dtRiskProfile.Select("C_CustomerId=" + customerId1.ToString());
                           if (customerId != customerOld1)
                           {
                               if (drAssetType.Count() > 0)
                               {
                                   customerOld1 = customerId;
                                   foreach (DataRow dr1 in drAssetType)
                                   {
                                       int assetCode = int.Parse(dr1["WAC_AssetClassificationCode"].ToString());
                                       switch (assetCode)
                                       {
                                           case 1:
                                               {
                                                   if (!string.IsNullOrEmpty(dr1["CAA_RecommendedPercentage"].ToString().Trim()))
                                                   {
                                                       drAssetAllocationDetails["EquityRecomendedValue"] = double.Parse(dr1["CAA_RecommendedPercentage"].ToString()) * (sumAssetTotal / 100);
                                                       drAssetAllocationDetails["EquityRecomendedPer"] = dr1["CAA_RecommendedPercentage"];
                                                   }
                                                   else
                                                   {
                                                       drAssetAllocationDetails["EquityRecomendedValue"] = 0;
                                                       drAssetAllocationDetails["EquityRecomendedPer"] = 0;
                                                   }
                                                   break;
                                               }
                                           case 2:
                                               {
                                                   if (!string.IsNullOrEmpty(dr1["CAA_RecommendedPercentage"].ToString().Trim()))
                                                   {
                                                       drAssetAllocationDetails["DebtRecomendedValue"] = double.Parse(dr1["CAA_RecommendedPercentage"].ToString()) * (sumAssetTotal / 100);
                                                       drAssetAllocationDetails["DebtRecomendedPer"] = dr1["CAA_RecommendedPercentage"];
                                                   }
                                                   else
                                                   {
                                                       drAssetAllocationDetails["DebtRecomendedValue"] = 0;
                                                       drAssetAllocationDetails["DebtRecomendedPer"] = 0;
                                                   }
                                                   break;
                                               }
                                           case 3:
                                               {
                                                   if (!string.IsNullOrEmpty(dr1["CAA_RecommendedPercentage"].ToString().Trim()))
                                                   {
                                                       drAssetAllocationDetails["CashRecomendedValue"] = double.Parse(dr1["CAA_RecommendedPercentage"].ToString()) * (sumAssetTotal / 100);
                                                       drAssetAllocationDetails["CashRecomendedPer"] = dr1["CAA_RecommendedPercentage"];
                                                   }
                                                   else
                                                   {
                                                       drAssetAllocationDetails["CashRecomendedValue"] = 0;
                                                       drAssetAllocationDetails["CashRecomendedPer"] = 0;
                                                   }
                                                   break;
                                               }
                                           case 4:
                                               {
                                                   if (!string.IsNullOrEmpty(dr1["CAA_RecommendedPercentage"].ToString().Trim()))
                                                   {
                                                       drAssetAllocationDetails["AlternateRecomendedValue"] = double.Parse(dr1["CAA_RecommendedPercentage"].ToString()) * (sumAssetTotal / 100);
                                                       drAssetAllocationDetails["AlternateRecomendedPer"] = dr1["CAA_RecommendedPercentage"];
                                                   }
                                                   else
                                                   {
                                                       drAssetAllocationDetails["AlternateRecomendedValue"] = 0;
                                                       drAssetAllocationDetails["AlternateRecomendedPer"] = 0;
                                                   }
                                                   break;
                                               }
                                       }
                                   }
                               }


                               else
                               {
                                   drAssetAllocationDetails["EquityRecomendedValue"] = 0;
                                   drAssetAllocationDetails["EquityRecomendedPer"] = 0;
                                   drAssetAllocationDetails["DebtRecomendedValue"] = 0;
                                   drAssetAllocationDetails["DebtRecomendedPer"] = 0;
                                   drAssetAllocationDetails["CashRecomendedValue"] = 0;
                                   drAssetAllocationDetails["CashRecomendedPer"] = 0;
                                   drAssetAllocationDetails["AlternateRecomendedValue"] = 0;
                                   drAssetAllocationDetails["AlternateRecomendedPer"] = 0;
                               }
                           }
                       }
                   }
                   dtAssetallocationDetails.Rows.Add(drAssetAllocationDetails); 
                }//*
    
                }//**

            }//***
                gvAssetAllocationMIS.DataSource = dtAssetallocationDetails;
                gvAssetAllocationMIS.DataBind();
                gvAssetAllocationMIS.Visible = true;
                ErrorMessage.Visible = false;
          }
            else
            {
                gvAssetAllocationMIS.Visible = false;
                ErrorMessage.Visible = true;
                
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
    }

}
