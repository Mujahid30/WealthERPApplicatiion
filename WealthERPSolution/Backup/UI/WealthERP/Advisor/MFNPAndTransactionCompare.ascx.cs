using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoAdvisorProfiling;
using VoUser;
using BoUploads;
using BoSuperAdmin;
using Telerik.Web.UI;
using BoCustomerPortfolio;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class MFNPAndTransactionCompare : System.Web.UI.UserControl
    {

        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        UserVo userVo = new UserVo();
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
        AdvisorMISBo adviserMFMISBo = new AdvisorMISBo();
        Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();

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
        string strValuationDate;

        public enum Constants
        {
            MF = 1,
            MFDate = 3
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            if (Session["UserType"] == "SuperAdmin")
            {
                userType = "SuperAdmin";
                
            }
            else if (Session["UserType"] == "adviser")
            {
                userType = "advisor";
                advisorId = advisorVo.advisorId;
                int RMId = rmVo.RMId;
                customerId = customerVo.CustomerId;
                rmId = rmVo.RMId;
                bmID = rmVo.RMId;
            }
            if (userType == "advisor")
            {
                advisorId = advisorVo.advisorId;
                if (Session[SessionContents.ValuationDate] == null)
                    GetLatestValuationDate();
                genDict = (Dictionary<string, DateTime>)Session[SessionContents.ValuationDate];
                strValuationDate = genDict[Constants.MFDate.ToString()].ToShortDateString();
                if (strValuationDate == "01/01/0001")
                    txtAsOnDate.SelectedDate = DateTime.Now;
                else
                txtAsOnDate.SelectedDate = DateTime.Parse(genDict[Constants.MFDate.ToString()].ToString());
                hdnDate.Value = txtAsOnDate.SelectedDate.ToString();

            }
            else
            {
                //txtAsOnDate.SelectedDate = DateTime.Parse(genDict[Constants.MFDate.ToString()].ToString());
                txtAsOnDate.SelectedDate = DateTime.Now;
                hdnDate.Value = txtAsOnDate.SelectedDate.ToString();
            }

            if (userType == "SuperAdmin")
            {
                if (ddlAdviser.SelectedIndex != -1 && ddlAdviser.SelectedIndex != 0)
                {
                    advisorId = Convert.ToInt32(ddlAdviser.SelectedValue.ToString());
                    //if (hfRmId.Value != "")
                    //{
                    //    rmId = Convert.ToInt32(hfRmId.Value);
                    //}
                }
                else
                {
                    advisorId = 1000;
                }
            }
            else
            {
                tdAdviser.Visible = false;
                ddlAdviser.Visible = false;
                advisorId = advisorVo.advisorId;
                rmId = rmVo.RMId;
            }

            if (!IsPostBack)
            {
                ddlCustomerType.Visible = false;
                lblSelectTypeOfCustomer.Visible = false;
                trCustomerSearch.Visible = false;
                txtIndividualCustomer.Visible = false;
                pnlMfNPTranxCompare.Visible = false;
                if (userType == "advisor")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                }
                if (userType == "SuperAdmin")
                {
                    trBranchRM.Visible = false;
                    TrCustomerType.Visible = false;
                    BindAdviserDropDownList();
                }
            }
        }
        protected void ddlAdviser_SelectedIndexChanged(object sender, EventArgs e)
        {
            trBranchRM.Visible = true;
            TrCustomerType.Visible = true;
            if (ddlAdviser.SelectedIndex != 0)
            {
                advisorId = int.Parse(ddlAdviser.SelectedValue);
                //if (Session[SessionContents.ValuationDate] == null)
                    GetLatestValuationDate();
                genDict = (Dictionary<string, DateTime>)Session[SessionContents.ValuationDate];          
                    strValuationDate = genDict[Constants.MFDate.ToString()].ToShortDateString();
                    txtAsOnDate.SelectedDate = DateTime.Parse(genDict[Constants.MFDate.ToString()].ToString());
                    hdnDate.Value = txtAsOnDate.SelectedDate.ToString();           
            }
            BindBranchDropDown();
            BindRMDropDown();
        }

        private void GetLatestValuationDate()
        {
            DateTime EQValuationDate = new DateTime();
            DateTime MFValuationDate = new DateTime();
            PortfolioBo portfolioBo = null;
            genDict = new Dictionary<string, DateTime>();
            AdvisorVo advisorVo = new AdvisorVo();
            try
            {
                portfolioBo = new PortfolioBo();
                advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                if (portfolioBo.GetLatestValuationDate(advisorId, Constants.MF.ToString()) != null)
                    {
                        MFValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(advisorId, Constants.MF.ToString()).ToString());
                    }
                    genDict.Add(Constants.MFDate.ToString(), MFValuationDate);
                    Session[SessionContents.ValuationDate] = genDict;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioDashboard.ascx.cs:GetLatestValuationDate()");
                object[] objects = new object[3];
                objects[0] = EQValuationDate;
                objects[1] = advisorId;
                objects[2] = MFValuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void BindAdviserDropDownList()
        {
            DataTable dtAdviserList = new DataTable();
            dtAdviserList = superAdminOpsBo.BindAdviserForUpload();

            if (dtAdviserList.Rows.Count > 0)
            {
                ddlAdviser.DataSource = dtAdviserList;
                ddlAdviser.DataTextField = "A_OrgName";
                ddlAdviser.DataValueField = "A_AdviserId";
                ddlAdviser.DataBind();
            }
            ddlAdviser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

        }

        private void BindBranchDropDown()
        {

            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            //int bmID = rmVo.RMId;
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorId, "adviser");
                if (ds != null)
                {
                    ddlBranch.DataSource = ds;
                    ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
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
                DataTable dt = advisorStaffBo.GetAdviserRM(advisorId);
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
        protected void ddlCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIndividualCustomer.Text = string.Empty;
            hdnIndividualOrGroup.Value = ddlCustomerType.SelectedItem.Value;
            rquiredFieldValidatorIndivudialCustomer.Visible = true;
            if (ddlCustomerType.SelectedIndex == 0)
            {
                trCustomerSearch.Visible = false;
                txtIndividualCustomer.Visible = false;
            }
            else
            {
                trCustomerSearch.Visible = true;
                txtIndividualCustomer.Visible = true;
                if (ddlCustomerType.SelectedItem.Value == "0")
                {
                    customerType = "GROUP";
                    if (userType == "advisor" || userType=="SuperAdmin")
                    {
                        if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                        {
                            txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorId.ToString();
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

                }
                else if (ddlCustomerType.SelectedItem.Value == "1")
                {
                    txtIndividualCustomer.Visible = true;
                    customerType = "IND";
                    if (userType == "advisor" || userType == "SuperAdmin")
                    {
                        if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                        {
                            txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorId.ToString();
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
                }
            }
        }
        protected void ddlSelectCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectCustomer.SelectedItem.Value == "All Customer")
            {
                ddlCustomerType.Visible = false;
                lblSelectTypeOfCustomer.Visible = false;
                trCustomerSearch.Visible = false;
                txtIndividualCustomer.Visible = false;
                rquiredFieldValidatorIndivudialCustomer.Visible = false;
                ddlCustomerType.SelectedIndex = 0;

            }
            if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer")
            {
                ddlCustomerType.Visible = true;
                lblSelectTypeOfCustomer.Visible = true;
                txtIndividualCustomer.Text = string.Empty;
                trCustomerSearch.Visible = false;
                txtIndividualCustomer.Visible = false;
                rquiredFieldValidatorIndivudialCustomer.Visible = true;
                ddlCustomerType.SelectedIndex = 0;
            }
        }

        protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnCustomerId.Value.ToString().Trim()))
            {
                customerId = int.Parse(hdnCustomerId.Value);
            }
        }

        private void SetParameters()
        {
            if (ddlAdviser.SelectedIndex != -1 && ddlAdviser.SelectedIndex != 0)
                advisorId = int.Parse(ddlAdviser.SelectedValue);
            //if (advisorVo != null)
            //    advisorId = advisorVo.advisorId;
            if ((ddlSelectCustomer.SelectedItem.Value == "All Customer") && (userType == "advisor")
                || (ddlSelectCustomer.SelectedItem.Value == "All Customer") && (userType == "SuperAdmin"))
            {
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnadviserId.Value = advisorId.ToString();
                    hdnAll.Value = "0";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnadviserId.Value = advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorId.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "3";
                }

            }
            if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer" && userType == "advisor"
                || (ddlSelectCustomer.SelectedItem.Value == "Pick Customer") && (userType == "SuperAdmin"))
            {

                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnadviserId.Value = advisorId.ToString();
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
            if (hdnIndividualOrGroup.Value == "0")
            {
                isGroup = 1;
                ViewState["GroupHeadCustomers"] = isGroup.ToString();
            }
            else if (ViewState["GroupHeadCustomers"] != null)
            {
                isGroup = int.Parse(ViewState["GroupHeadCustomers"].ToString());
            }

            if (hdnIndividualOrGroup.Value == "1")
            {
                isGroup = 2;
                ViewState["IndividualCustomers"] = isGroup.ToString();
            }
            else if (ViewState["IndividualCustomers"] != null)
            {
                isGroup = int.Parse(ViewState["IndividualCustomers"].ToString());
            }

            if (!String.IsNullOrEmpty(hdnCustomerId.Value))
                customerId = int.Parse(hdnCustomerId.Value);
            else
                customerId = 0;


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

        protected void btnGo_Click(object sender, EventArgs e)
        {
            ViewState["GroupHeadCustomers"] = null;
            ViewState["IndividualCustomers"] = null;
            ViewState["CustomerId"] = null;
            SetParameters();
            BindMFNPTransactionHoldingDetails();
        }

        private void BindMFNPTransactionHoldingDetails()
        {
            DataTable dtMFNPTransactionHoldingDetails;
            dtMFNPTransactionHoldingDetails = adviserMFMISBo.MFNPTransactionHoldingDetails(int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), customerId, int.Parse(hdnbranchId.Value), int.Parse(hdnbranchheadId.Value), int.Parse(hdnAll.Value), isGroup, hdnDate.Value);
            if (dtMFNPTransactionHoldingDetails != null)
            {
                gvMfNPTranxCompare.DataSource = dtMFNPTransactionHoldingDetails;
                gvMfNPTranxCompare.DataBind();
                btnMFNPTranxCompare.Visible = true;
                pnlMfNPTranxCompare.Visible = true;
                if (Cache["gvMfNPTranxCompare" + userVo.UserId + userType] == null)
                {
                    Cache.Insert("gvMfNPTranxCompare" + userVo.UserId + userType, dtMFNPTransactionHoldingDetails);
                }
                else
                {
                    Cache.Remove("gvMfNPTranxCompare" + userVo.UserId + userType);
                    Cache.Insert("gvMfNPTranxCompare" + userVo.UserId + userType, dtMFNPTransactionHoldingDetails);
                }
            }
            else
            {
                gvMfNPTranxCompare.DataSource = null;
                gvMfNPTranxCompare.DataBind();
            }

        }

        protected void btnMFNPTranxCompare_Click(object sender, ImageClickEventArgs e)
        {
            gvMfNPTranxCompare.ExportSettings.OpenInNewWindow = true;
            gvMfNPTranxCompare.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvMfNPTranxCompare.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvMfNPTranxCompare.MasterTableView.ExportToExcel();
        }
        protected void gvMfNPTranxCompare_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtMFNPTransactionHoldingDetails = new DataTable();
            dtMFNPTransactionHoldingDetails = (DataTable)Cache["gvMfNPTranxCompare" + userVo.UserId + userType];
            gvMfNPTranxCompare.DataSource = dtMFNPTransactionHoldingDetails;
            gvMfNPTranxCompare.Visible = true;
            btnMFNPTranxCompare.Visible = true;
       }


    }
}