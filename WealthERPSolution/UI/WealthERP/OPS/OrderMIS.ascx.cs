using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerPortfolio;
using VoUser;
using BoCustomerProfiling;
using WealthERP.Base;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoAdvisorProfiling;
using BoUploads;
using BoOps;
using VoOps;
using BoProductMaster;
using System.IO;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;

namespace WealthERP.OPS
{
    public partial class OrderMIS : System.Web.UI.UserControl
    {
        ProductMFBo productMFBo = new ProductMFBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo;
        RMVo rmVo = new RMVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        OperationBo operationBo = new OperationBo();
        OperationVo operationVo = new OperationVo();
        MFOrderBo mforderBo = new MFOrderBo();
        MFOrderVo mforderVo = new MFOrderVo();
        OrderVo orderVo = new OrderVo();

        UserVo userVo = new UserVo();

        int portfolioId=0;
        int schemePlanCode;
        int customerId;
        bool GridViewCultureFlag = true;
        int count;
        String userType;
        int bmID = 0;
        string customerType = string.Empty;


        protected override void OnInit(EventArgs e)
        {
           try
            {
                ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
                mypager.EnableViewState = true;
                base.OnInit(e);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioSystematicView.ascx.cs:OnInit()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
       }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            try
            {
                GetPageCount();
                this.BindMISGridView();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioSystematicView.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void GetPageCount()
        {
            string upperlimit = null;
            int rowCount = 0;
            int ratio = 0;
            string lowerlimit = null;
            string PageRecords = null;
            try
            {
                if (hdnRecordCount.Value.ToString() != "")
                    rowCount = Convert.ToInt32(hdnRecordCount.Value);
                if (rowCount > 0)
                {
                    ratio = rowCount / 30;
                    mypager.PageCount = rowCount % 30 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    if (((mypager.CurrentPage - 1) * 30) != 0)
                        lowerlimit = (((mypager.CurrentPage - 1) * 30) + 1).ToString();
                    else
                        lowerlimit = "1";
                    upperlimit = (mypager.CurrentPage * 30).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnRecordCount.Value;
                    PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;
                    hdnRecordCount.Value = mypager.CurrentPage.ToString();
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
                FunctionInfo.Add("Method", "PortfolioSystematicView.ascx.cs:GetPageCount()");
                object[] objects = new object[5];
                objects[0] = upperlimit;
                objects[1] = rowCount;
                objects[2] = ratio;
                objects[3] = lowerlimit;
                objects[4] = PageRecords;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();

            userVo = (UserVo)Session["userVo"];

            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            rmVo = (RMVo)Session[SessionContents.RmVo];
            int RMId = rmVo.RMId;
            bmID = rmVo.RMId;
            gvCustomerOrderMIS.Visible = false;
            btnMannualMatch.Visible = false;
            btnSync.Visible = false;
            if (Request.QueryString["result"] != null)
            {
                gvCustomerOrderMIS.Visible = true;
                BindMISGridView();
            }
            if (!IsPostBack)
            {

                if (userType == "advisor")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                }
                if (userType == "bm")
                {
                    BindBranchForBMDropDown();
                    BindRMforBranchDropdown(0, bmID);
                }
                BindAMC();
                //BindPortfolioDropdown();
                //BindFolionumberDropdown(portfolioId);
                //BindTransactionType();
                BindOrderStatus();
                //BindAssetType();
                lblselectCustomer.Visible = false;
                txtIndividualCustomer.Visible = false;
            }
            //btnSync.Visible = false;
            //btnMannualMatch.Visible = false;
            txtFrom.Text = DateTime.Now.ToShortDateString();
            txtTo.Text = DateTime.Now.ToShortDateString();

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
        private void BindAMC()
        {
            DataSet dsProductAmc;
            DataTable dtProductAMC;

            try
            {
              dsProductAmc = productMFBo.GetProductAmc();
              if (dsProductAmc.Tables.Count > 0)
                {
                    dtProductAMC = dsProductAmc.Tables[0];
                    ddlAMC.DataSource = dtProductAMC;
                    ddlAMC.DataTextField = dtProductAMC.Columns["PA_AMCName"].ToString();
                    ddlAMC.DataValueField = dtProductAMC.Columns["PA_AMCCode"].ToString();
                    ddlAMC.DataBind();
                }
              ddlAMC.Items.Insert(0, new ListItem("All", "All"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        private void BindOrderStatus()
        {
            DataSet dsOrderStaus;
            DataTable dtOrderStatus;
            dsOrderStaus = operationBo.GetOrderStatus();
            dtOrderStatus = dsOrderStaus.Tables[0];
            if (dtOrderStatus.Rows.Count > 0)
            {
                ddlMISOrderStatus.DataSource = dtOrderStatus;
                ddlMISOrderStatus.DataValueField = dtOrderStatus.Columns["XS_StatusCode"].ToString();
                ddlMISOrderStatus.DataTextField = dtOrderStatus.Columns["XS_Status"].ToString();
                ddlMISOrderStatus.DataBind();
            }
        }

        private void BindTransactionType()
        {
            DataSet dsTrxType;
            DataTable dtTrxType;
            dsTrxType = operationBo.GetTransactionType();
            dtTrxType = dsTrxType.Tables[0];
            if (dtTrxType.Rows.Count > 0)
            {
                ddlTrxType.DataSource = dtTrxType;
                ddlTrxType.DataValueField = dtTrxType.Columns["WMTT_TransactionClassificationCode"].ToString();
                ddlTrxType.DataTextField = dtTrxType.Columns["WMTT_TransactionClassificationName"].ToString();
                ddlTrxType.DataBind();
            }
            ddlTrxType.Items.Insert(0, new ListItem("All", "All"));
        }

        //private void BindAssetType()
        //{
        //    DataSet dsAssetType;
        //    DataTable dtAssetType;
        //    dsAssetType = operationBo.GetAssetType();
        //    dtAssetType = dsAssetType.Tables[0];
        //    if (dtAssetType.Rows.Count > 0)
        //    {
        //        ddlAssetType.DataSource = dtAssetType;
        //        ddlAssetType.DataValueField = dtAssetType.Columns["PAG_AssetGroupCode"].ToString();
        //        ddlAssetType.DataTextField = dtAssetType.Columns["PAG_AssetGroupName"].ToString();
        //        ddlAssetType.DataBind();
        //    }
        //    ddlTrxType.Items.Insert(0, new ListItem("Select", "Select"));
        //}
        //protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        //{
        //    customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
        //    Session["customerVo"] = customerVo;
        //    customerId = int.Parse(txtCustomerId.Value);
        //}
        //private void BindFolionumberDropdown(int portfolioId)
        //{
        //    DataSet dsCustomerAccounts = new DataSet();
        //    DataTable dtCustomerAccounts;

        //    if (txtSchemeCode.Value != null && ddlPortfolio.SelectedValue != null)
        //    {
        //        portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
        //        schemePlanCode = int.Parse(txtSchemeCode.Value);
        //        dsCustomerAccounts = customerAccountBo.GetCustomerMFAccounts(portfolioId, "MF", schemePlanCode);
                
        //    }

        //    if (dsCustomerAccounts.Tables.Count > 0)
        //    {
        //        dtCustomerAccounts = dsCustomerAccounts.Tables[0];
        //        if (dtCustomerAccounts.Rows.Count > 0)
        //        {
        //            ddlFolioNumber.DataSource = dtCustomerAccounts;
        //            ddlFolioNumber.DataTextField = "CMFA_FolioNum";
        //            ddlFolioNumber.DataValueField = "CMFA_AccountId";
        //            ddlFolioNumber.DataBind();
        //        }
        //        ddlFolioNumber.Items.Insert(0, new ListItem("Select", "Select"));
        //    }
            
        //}

        //private void BindPortfolioDropdown()
        //{
        //    DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
        //    ddlPortfolio.DataSource = ds;
        //    ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
        //    ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
        //    ddlPortfolio.DataBind();
        //    //ddlPortfolio.SelectedValue = portfolioId.ToString();
        //}


        private void BindMISGridView()
        {
            //DataTable dtBindGridView = new DataTable();
            DataSet dsOrderMIS;
            DataTable dtOrderMIS;
            //dsOrderMIS = operationBo.GetOrderMIS(advisorVo.advisorId,hdnBranchId.Value,hdnRMId.Value,hdnTransactionType.Value,hdnOrdStatus.Value,hdnOrderType.Value,hdnamcCode.Value,DateTime.Parse(hdnFromdate.Value),DateTime.Parse(hdnTodate.Value), mypager.CurrentPage, out  count);
            dsOrderMIS = mforderBo.GetCustomerMFOrderMIS(advisorVo.advisorId, DateTime.Parse(hdnFromdate.Value), DateTime.Parse(hdnTodate.Value), hdnBranchId.Value, hdnRMId.Value, hdnTransactionType.Value, hdnOrdStatus.Value, hdnOrderType.Value, hdnamcCode.Value, hdnCustomerId.Value);
            dtOrderMIS = dsOrderMIS.Tables[0];
            if (dtOrderMIS.Rows.Count > 0)
            {
                lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                gvCustomerOrderMIS.DataSource = dtOrderMIS;
                gvCustomerOrderMIS.DataBind();
                gvCustomerOrderMIS.Visible = true;
                this.GetPageCount();
                if (ddlMISOrderStatus.SelectedValue == "OMIP")
                {
                    btnSync.Visible = true;
                    btnMannualMatch.Visible = true;
                }
                else
                {
                    btnSync.Visible = false;
                    btnMannualMatch.Visible = false;
                }
                if (Cache["OrderMIS" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("OrderMIS" + userVo.UserId, dtOrderMIS);
                }
                else
                {
                    Cache.Remove("OrderMIS" + userVo.UserId);
                    Cache.Insert("OrderMIS" + userVo.UserId, dtOrderMIS);
                }
                //btnSubmit.Visible = true;
                ErrorMessage.Visible = false;
                tblMessage.Visible = false;
                Session["GridView"] = dtOrderMIS;
                tblPager.Visible = true;
                trPager.Visible = true;
                lblCurrentPage.Visible = true;
                lblTotalRows.Visible = true;
                btnMForderRecon.Visible = true;
                //imgBtnExport.Visible = true;
            }
            else
            {
                gvCustomerOrderMIS.Visible = false;
                tblMessage.Visible = true;
                //btnSubmit.Visible = false;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
                btnSync.Visible = false;
                btnMannualMatch.Visible = false;
                tblPager.Visible = false;
                trPager.Visible = false;
                btnMForderRecon.Visible = false;
                //imgBtnExport.Visible = false;
            }
            


        }

        private void BindRMDropDown()
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
        private void BindBranchDropDown()
        {

            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;

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

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bmID = rmVo.RMId;
            if (ddlBranch.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0);
            }
        }
        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
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

        protected void btnGo_Click(object sender, EventArgs e)
        {

            gvCustomerOrderMIS.Visible = true; 
            SetParameters();
            BindMISGridView();
        }

        private void SetParameters()
        {
            if (ddlBranch.SelectedIndex != 0)
                hdnBranchId.Value = ddlBranch.SelectedValue;
            else
                hdnBranchId.Value = "";

            if (ddlRM.SelectedIndex != 0)
                hdnRMId.Value = ddlRM.SelectedValue;
            else
                hdnRMId.Value = "";


            //if (ddlPortfolio.SelectedIndex != -1)
            //    hdnPortFolio.Value = ddlPortfolio.SelectedValue;
            //else
            //    hdnPortFolio.Value = "";


            if (ddlTrxType.SelectedIndex != 0)
                hdnTransactionType.Value = ddlTrxType.SelectedValue;
            else
                hdnTransactionType.Value = "";

            //if (txtReceivedDate.Text != "")
            //    hdnARDate.Value = DateTime.Parse(txtReceivedDate.Text).ToString();
            //else
            //    hdnARDate.Value = DateTime.MinValue.ToString();

            if (ddlMISOrderStatus.SelectedIndex != 0)
                hdnOrdStatus.Value = ddlMISOrderStatus.SelectedValue ;
            else
                hdnOrdStatus.Value = "OMIP";

             
            if (ddlOrderType.SelectedIndex != -1)
                hdnOrderType.Value = ddlOrderType.SelectedValue;
            else
                hdnOrderType.Value = "0";

            if (ddlAMC.SelectedIndex != 0)
                hdnamcCode.Value = ddlAMC.SelectedValue;
            else
                hdnamcCode.Value = "";
    
            //if (txtOrderDate.Text != "")
            //    hdnOrderDate.Value = DateTime.Parse(txtOrderDate.Text).ToString();
            //else
            //    hdnOrderDate.Value = DateTime.MinValue.ToString();
            if (txtFrom.Text != "")
                hdnFromdate.Value = DateTime.Parse(txtFrom.Text).ToString();
            else
                hdnFromdate.Value = DateTime.MinValue.ToString();

            if (txtTo.Text != "")
                hdnTodate.Value = DateTime.Parse(txtTo.Text).ToString();
            else
                hdnTodate.Value = DateTime.MinValue.ToString();

        

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (GridDataItem gvRow in gvCustomerOrderMIS.Items)
           {

               CheckBox chk = (CheckBox)gvRow.FindControl("cbRecons");
               if (chk.Checked)
               {
                   i++;
               }
               
           }
             if (i == 0)
             {
                 ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record!');", true);
                 BindMISGridView();
             }
             else
             {
                 string ids = GetSelectedIdString();
                 ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalFundPage", "loadcontrol('OrderRecon','?result=" + ids + "');", true);
             }
           
        }
        private string GetSelectedIdString()
        {
            string gvIds = "";

            //'Navigate through each row in the GridView for checkbox items
            foreach (GridDataItem gvRow in gvCustomerOrderMIS.Items)
            {
                CheckBox ChkBxItem = (CheckBox)gvRow.FindControl("cbRecons");
                if (ChkBxItem.Checked)
                {
                    gvIds += Convert.ToString(gvCustomerOrderMIS.MasterTableView.DataKeyNames[gvRow.RowIndex]) + "~";
                }
            }
            

            return gvIds;
 
        }

        protected void lnkOrderId_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
                LinkButton lnkOrderId = (LinkButton)sender;
                GridDataItem gdi;
                gdi = (GridDataItem)lnkOrderId.NamingContainer;
                int selectedRow = gdi.ItemIndex + 1;
                
                int orderId = int.Parse((gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CMFOD_OrderDetailsId"].ToString()));
           
                    operationVo = operationBo.GetCustomerOrderTrackingDetails(orderId);
                    Session["operationVo"] = operationVo;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderEntry", "loadcontrol('OrderEntry','action=View');", true);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }

        //protected void txtCustomerName_TextChanged(object sender, EventArgs e)
        //{
        //    BindPortfolioDropdown();
        //}

        //protected void txtSchemeName_TextChanged(object sender, EventArgs e)
        //{
        //    if(ddlPortfolio.SelectedValue != "")
        //         BindFolionumberDropdown(portfolioId);
        //}

        protected void gvMIS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblordertype = e.Row.FindControl("lblOrderType") as Label;
                //CheckBox chkApprove = e.Row.FindControl("cbRecons") as CheckBox;
                string ordertype = null;
                ordertype = lblordertype.Text;
                if (ordertype == "1")
                    lblordertype.Text = "Immediate";
                else
                    lblordertype.Text = "Future";
                //Label lblisApproved = e.Row.FindControl("lblIsApproved") as Label;
                //string status = null;
                //status = lblisApproved.Text;
                //if (status == "1")
                //{
                //    lblisApproved.Text = "Yes";
                //}
                //else
                //{
                //    lblisApproved.Text = "No";
                //    chkApprove.Enabled = false;
                //    chkApprove.Checked = false;
                //}

            }
        }

        protected void btnSync_Click(object sender, EventArgs e)
        {
           // int i=0;
           // int gvOrderId = 0;
           // //int gvCustomerId = 0;
           // int gvPortfolioId = 0;
           // int gvSchemeCode = 0;
           // int gvaccountId = 0;
           // string gvTrxType = "";
           // double gvAmount = 0.0;
           // DateTime gvOrderDate = DateTime.MinValue;
           // bool result = false;
           // foreach (GridViewRow gvRow in gvCustomerOrderMIS.rows)
           //{

           //    CheckBox chk = (CheckBox)gvRow.FindControl("cbRecons");
           //    if (chk.Checked)
           //    {
           //        i++;
           //    }
               
           //}
           // if (i == 0)
           // {
           //     ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record!');", true);
           //     BindMISGridView();
           // }
           // else
           // {
           //     foreach (GridViewRow gvRow in gvCustomerOrderMIS.Rows)
           //     {
           //         if (((CheckBox)gvRow.FindControl("cbRecons")).Checked == true)
           //         {
           //             gvOrderId = Convert.ToInt32(gvCustomerOrderMIS.DataKeys[gvRow.RowIndex].Values["CMFOD_OrderDetailsId"].ToString());
           //             //gvCustomerId = Convert.ToInt32(gvMIS.DataKeys[gvRow.RowIndex].Values["C_CustomerId"].ToString());
           //             gvPortfolioId = Convert.ToInt32(gvCustomerOrderMIS.DataKeys[gvRow.RowIndex].Values["CP_portfolioId"].ToString());
           //             gvSchemeCode = Convert.ToInt32(gvCustomerOrderMIS.DataKeys[gvRow.RowIndex].Values["PASP_SchemePlanCode"].ToString());
           //             if (!string.IsNullOrEmpty(gvCustomerOrderMIS.DataKeys[gvRow.RowIndex].Values["CMFA_AccountId"].ToString().Trim()))
           //                 gvaccountId = Convert.ToInt32(gvCustomerOrderMIS.DataKeys[gvRow.RowIndex].Values["CMFA_AccountId"].ToString());
           //             else
           //                 gvaccountId = 0;
           //             gvTrxType = gvCustomerOrderMIS.DataKeys[gvRow.RowIndex].Values["WMTT_TransactionClassificationCode"].ToString();
           //             gvAmount = Convert.ToDouble(gvCustomerOrderMIS.DataKeys[gvRow.RowIndex].Values["CMFOD_Amount"].ToString());
           //             gvOrderDate = Convert.ToDateTime(gvCustomerOrderMIS.DataKeys[gvRow.RowIndex].Values["CO_OrderDate"].ToString());
           //             result = operationBo.UpdateMFTransaction(gvOrderId, gvSchemeCode, gvaccountId, gvTrxType, gvPortfolioId, gvAmount, gvOrderDate);
           //             if (result == true)
           //                 ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Match is done');", true);
           //             else
           //                 ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Not able to match');", true);
           //         }
           //     }
           //     BindMISGridView();
           // }
        }

        protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtender1.TargetControlID = "imgBtnExport";
            ModalPopupExtender1.Show();
        }

        //protected void btnExportExcel_Click(object sender, EventArgs e)
        //{

        //    gvCustomerOrderMIS.Columns[0].Visible = false;

        //    //gvCustomerOrderMIS.HeaderRow.Visible = true;

        //    if (hdnDownloadPageType.Value.ToString() == "multiple")
        //    {
        //        GridViewCultureFlag = false;
        //        BindMISGridView();
        //        GridViewCultureFlag = true;
        //    }
        //    //else
        //    //{
        //    //    GridViewCultureFlag = false;
        //    //    //1 for All record to be export to excel...
        //    //    BindMISGridView();
        //    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "AferExportAll('ctrl_RMMultipleTransactionView_btnPrintGrid');", true);
        //    //    GridViewCultureFlag = true;
        //    //}


        //    ExportGridView(hdnDownloadFormat.Value.ToString());

        //}
        //private void ExportGridView(string Filetype)
        //{
        //    // float ReportTextSize = 7;
        //    {
        //        HtmlForm frm = new HtmlForm();
        //        System.Web.UI.WebControls.Table tbl = new System.Web.UI.WebControls.Table();
        //        frm.Controls.Clear();
        //        frm.Attributes["runat"] = "server";
        //        if (Filetype.ToLower() == "print")
        //        {
        //            GridView_Print();
        //        }

        //        else if (Filetype.ToLower() == "excel")
        //        {
        //            // gvCustomer.Columns.Remove(this.gvCustomer.Columns[0]);
        //            string temp = rmVo.FirstName + rmVo.LastName + "Customer's MFTransactionList.xls";
        //            string attachment = "attachment; filename=" + temp;
        //            Response.ClearContent();
        //            Response.AddHeader("content-disposition", attachment);
        //            Response.ContentType = "application/ms-excel";
        //            StringWriter sw = new StringWriter();
        //            HtmlTextWriter htw = new HtmlTextWriter(sw);

        //            Response.Output.Write("<table border=\"0\"><tbody ><caption align=\"left\"><FONT FACE=\"ARIAL\"  SIZE=\"4\">");
        //            //Response.Output.Write("Order MIS for " + convertedFromDate.ToString("MMM-dd-yyyy") + " to " + convertedToDate.ToString("MMM-dd-yyyy") + "</FONT></caption>");
        //            Response.Output.Write("<tr><td>");
        //            Response.Output.Write("Report Generated on  : ");
        //            Response.Output.Write(DateTime.Now.ToString("MMM-dd-yyyy hh:ss tt"));
        //            Response.Output.Write("</td></tr>");
        //            Response.Output.Write("</tbody></table>");


        //            PrepareGridViewForExport(gvCustomerOrderMIS);

        //            if (gvCustomerOrderMIS.HeaderRow != null)
        //            {
        //                PrepareControlForExport(gvCustomerOrderMIS.HeaderRow);

        //            }
        //            foreach (GridViewRow row in gvCustomerOrderMIS.Rows)
        //            {

        //                PrepareControlForExport(row);

        //            }
        //            if (gvCustomerOrderMIS.FooterRow != null)
        //            {
        //                PrepareControlForExport(gvCustomerOrderMIS.FooterRow);

        //            }


        //            gvCustomerOrderMIS.Parent.Controls.Add(frm);
        //            frm.Controls.Add(gvCustomerOrderMIS);
        //            frm.RenderControl(htw);

        //            Response.Write(sw.ToString());
        //            Response.End();


        //        }

                
        //    }

        //}
        //private void GridView_Print()
        //{
        //    gvCustomerOrderMIS.Columns[0].Visible = false;
        //    //gvMFTransactions.Columns[1].Visible = false;
        //    if (hdnDownloadPageType.Value.ToString() == "single")
        //    {
        //        BindMISGridView();
        //    }
        //    else
        //    {
        //        BindMISGridView();
        //    }

        //    PrepareGridViewForExport(gvCustomerOrderMIS);
        //    if (gvCustomerOrderMIS.HeaderRow != null)
        //    {
        //        PrepareControlForExport(gvCustomerOrderMIS.HeaderRow);
        //    }
        //    foreach (GridViewRow row in gvCustomerOrderMIS.Rows)
        //    {
        //        PrepareControlForExport(row);
        //    }
        //    if (gvCustomerOrderMIS.FooterRow != null)
        //    {
        //        PrepareControlForExport(gvCustomerOrderMIS.FooterRow);
        //    }



        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_RMMultipleTransactionView_tbl','ctrl_RMMultipleTransactionView_btnPrintGrid');", true);

        //}
        //private static void PrepareControlForExport(Control control)
        //{
        //    for (int i = 0; i < control.Controls.Count; i++)
        //    {
        //        Control current = control.Controls[i];
        //        if (current is LinkButton)
        //        {
        //            control.Controls.Remove(current);
        //            control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
        //        }
        //        else if (current is ImageButton)
        //        {
        //            control.Controls.Remove(current);
        //            control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
        //        }
        //        else if (current is HyperLink)
        //        {
        //            control.Controls.Remove(current);
        //            control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
        //        }
        //        else if (current is DropDownList)
        //        {
        //            control.Controls.Remove(current);
        //            control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedValue.ToString()));
        //        }
        //        else if (current is CheckBox)
        //        {
        //            control.Controls.Remove(current);
        //            control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
        //        }

        //        if (current.HasControls())
        //        {
        //            PrepareControlForExport(current);
        //        }
        //    }
        //}

        //private void PrepareGridViewForExport(Control gv)
        //{
        //    LinkButton lb = new LinkButton();
        //    Literal l = new Literal();
        //    string name = String.Empty;
        //    for (int i = 0; i < gv.Controls.Count; i++)
        //    {
        //        if (gv.Controls[i].GetType() == typeof(LinkButton))
        //        {
        //            l.Text = (gv.Controls[i] as LinkButton).Text;
        //            gv.Controls.Remove(gv.Controls[i]);
        //        }
        //        else if (gv.Controls[i].GetType() == typeof(DropDownList))
        //        {
        //            l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
        //            gv.Controls.Remove(gv.Controls[i]);
        //        }
        //        else if (gv.Controls[i].GetType() == typeof(CheckBox))
        //        {
        //            l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
        //            gv.Controls.Remove(gv.Controls[i]);
        //        }
        //        else if (gv.Controls[i].GetType() == typeof(TextBox))
        //        {
        //            l.Text = (gv.Controls[i] as TextBox).Text;
        //            gv.Controls.Remove(gv.Controls[i]);
        //        }
        //        if (gv.Controls[i].HasControls())
        //        {
        //            PrepareGridViewForExport(gv.Controls[i]);
        //        }

        //    }

        //}

        protected void btnMannualMatch_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (GridDataItem gvRow in gvCustomerOrderMIS.Items)
            {
                CheckBox chk = (CheckBox)gvRow.FindControl("cbRecons");
                if (chk.Checked)
                {
                    count++;
                }
                if (count > 1)
                    chk.Checked = false;

            }
            if (count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record!');", true);
                BindMISGridView();
            }
            if (count == 1)
            {
                int OrderId = 0;
                int PortfolioId = 0;
                int SchemeCode = 0;
                int accountId = 0;
                int CustomerId = 0;
                double Amount = 0.0;
                int schemeCodeSwitch = 0;
                string TrxType = string.Empty;
                DateTime OrderDate = DateTime.MinValue;
               
                foreach (GridDataItem gvRow1 in gvCustomerOrderMIS.Items)
                {
                    if (((CheckBox)gvRow1.FindControl("cbRecons")).Checked == true)
                    {

                        LinkButton lnkOrderId = (LinkButton)sender;
                        GridDataItem gdi;
                        gdi = (GridDataItem)lnkOrderId.NamingContainer;
                        int selectedRow = gdi.ItemIndex + 1;
                        OrderId = int.Parse(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CMFOD_OrderDetailsId"].ToString());
                        //OrderId = Convert.ToInt32(gvCustomerOrderMIS.DataKeys[gvRow1.RowIndex].Values["CMFOD_OrderDetailsId"].ToString());
                        CustomerId = int.Parse(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString());
                        //PortfolioId = Convert.ToInt32(gvMIS.DataKeys[gvRow1.RowIndex].Values["CP_portfolioId"].ToString());
                        SchemeCode = int.Parse(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["PASP_SchemePlanCode"].ToString());
                        if (!string.IsNullOrEmpty(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CMFA_AccountId"].ToString().Trim()))
                            accountId = int.Parse(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CMFA_AccountId"].ToString());
                        else
                            accountId = 0;
                        TrxType = gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["WMTT_TransactionClassificationCode"].ToString();
                        Amount = double.Parse(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CMFOD_Amount"].ToString());
                        OrderDate = DateTime.Parse(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderDate"].ToString());
                        if (!string.IsNullOrEmpty(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["PASP_SchemePlanSwitch"].ToString()))
                            schemeCodeSwitch = int.Parse(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["PASP_SchemePlanSwitch"].ToString());
                        else
                            schemeCodeSwitch = 0;
                        Response.Write("<script type='text/javascript'>detailedresults=window.open('OPS/ManualOrderMapping.aspx?result=" + OrderId + "&SchemeCode=" + SchemeCode + "&AccountId=" + accountId + "&Type=" + TrxType + "&Amount=" + Amount + "&OrderDate=" + OrderDate + "&Customerid=" + CustomerId + "&SchemeSwitch=" + schemeCodeSwitch + "','mywindow', 'width=1000,height=450,scrollbars=yes,location=center');</script>");
                    }

                }
                BindMISGridView();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You can select only one record at a time.');", true);
                BindMISGridView();
            }
           
        }

        protected void ddlMISOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlMISOrderStatus.SelectedValue=="0")
            {
                btnMannualMatch.Visible=true;
                btnSync.Visible=true;
            }
        }

        //protected void gvMIS_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int index = 0;
        //    DataSet dsGetMFOrderDetails;
        //    if (e.CommandName == "ViewOrder")
        //    {
        //        try
        //        {
        //            index = Convert.ToInt32(e.CommandArgument);
        //            //GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
        //            int orderId = Convert.ToInt32(gvMIS.DataKeys[index].Value.ToString());
        //            dsGetMFOrderDetails = mforderBo.GetCustomerMFOrderDetails(orderId);
        //            if (dsGetMFOrderDetails.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in dsGetMFOrderDetails.Tables[0].Rows)
        //                {
        //                    orderVo.OrderId = int.Parse(dr["CO_OrderId"].ToString());
        //                    orderVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
        //                    mforderVo.CustomerName = dr["Customer_Name"].ToString();
        //                    mforderVo.RMName = dr["RM_Name"].ToString();
        //                    mforderVo.BMName = dr["AB_BranchName"].ToString();
        //                    mforderVo.PanNo = dr["C_PANNum"].ToString();
        //                    if (!string.IsNullOrEmpty(dr["PA_AMCCode"].ToString().Trim()))
        //                        mforderVo.Amccode = int.Parse(dr["PA_AMCCode"].ToString());
        //                    else
        //                        mforderVo.Amccode = 0;
        //                    if (!string.IsNullOrEmpty(dr["PAIC_AssetInstrumentCategoryCode"].ToString().Trim()))
        //                        mforderVo.category = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
        //                    if (!string.IsNullOrEmpty(dr["PASP_SchemePlanCode"].ToString().Trim()))
        //                        mforderVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
        //                    mforderVo.OrderNumber = int.Parse(dr["CMFOD_OrderNumber"].ToString());
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_Amount"].ToString().Trim()))
        //                        mforderVo.Amount = double.Parse(dr["CMFOD_Amount"].ToString());
        //                    else
        //                        mforderVo.Amount = 0;

        //                    if (int.Parse(dr["CMFA_accountid"].ToString()) != 0)
        //                        mforderVo.accountid = int.Parse(dr["CMFA_accountid"].ToString());
        //                    else
        //                        mforderVo.accountid = 0;
        //                    mforderVo.TransactionCode = dr["WMTT_TransactionClassificationCode"].ToString();
        //                    orderVo.OrderDate = DateTime.Parse(dr["CO_OrderDate"].ToString());
        //                    mforderVo.IsImmediate = int.Parse(dr["CMFOD_IsImmediate"].ToString());
        //                    orderVo.ApplicationNumber = dr["CO_ApplicationNumber"].ToString();
        //                    orderVo.ApplicationReceivedDate = DateTime.Parse(dr["CO_ApplicationReceivedDate"].ToString());
        //                    mforderVo.portfolioId = int.Parse(dr["CP_portfolioId"].ToString());
        //                    orderVo.PaymentMode = dr["XPM_PaymentModeCode"].ToString();
        //                    if (!string.IsNullOrEmpty(dr["CO_ChequeNumber"].ToString()))
        //                        orderVo.ChequeNumber = dr["CO_ChequeNumber"].ToString();
        //                    else
        //                        orderVo.ChequeNumber = "";
        //                    if (!string.IsNullOrEmpty(dr["CO_PaymentDate"].ToString()))
        //                        orderVo.PaymentDate = DateTime.Parse(dr["CO_PaymentDate"].ToString());
        //                    else
        //                        orderVo.PaymentDate = DateTime.MinValue;
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_FutureTriggerCondition"].ToString()))
        //                        mforderVo.FutureTriggerCondition = dr["CMFOD_FutureTriggerCondition"].ToString();
        //                    else
        //                        mforderVo.FutureTriggerCondition = "";
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_FutureExecutionDate"].ToString()))
        //                        mforderVo.FutureExecutionDate = DateTime.Parse(dr["CMFOD_FutureExecutionDate"].ToString());
        //                    else
        //                        mforderVo.FutureExecutionDate = DateTime.MinValue;
        //                    if (!string.IsNullOrEmpty(dr["PASP_SchemePlanSwitch"].ToString()))
        //                        mforderVo.SchemePlanSwitch = int.Parse(dr["PASP_SchemePlanSwitch"].ToString());
        //                    else
        //                        mforderVo.SchemePlanSwitch = 0;
        //                    if (!string.IsNullOrEmpty(dr["CB_CustBankAccId"].ToString()))
        //                        orderVo.CustBankAccId = int.Parse(dr["CB_CustBankAccId"].ToString());
        //                    else
        //                        orderVo.CustBankAccId = 0;
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_BankName"].ToString()))
        //                        mforderVo.BankName = dr["CMFOD_BankName"].ToString();
        //                    else
        //                        mforderVo.BankName = "";
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_BranchName"].ToString()))
        //                        mforderVo.BranchName = dr["CMFOD_BranchName"].ToString();
        //                    else
        //                        mforderVo.BranchName = "";
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine1"].ToString()))
        //                        mforderVo.AddrLine1 = dr["CMFOD_AddrLine1"].ToString();
        //                    else
        //                        mforderVo.AddrLine1 = "";
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine2"].ToString()))
        //                        mforderVo.AddrLine2 = dr["CMFOD_AddrLine2"].ToString();
        //                    else
        //                        mforderVo.AddrLine2 = "";
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine3"].ToString()))
        //                        mforderVo.AddrLine3 = dr["CMFOD_AddrLine3"].ToString();
        //                    else
        //                        mforderVo.AddrLine3 = "";
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_City"].ToString()))
        //                        mforderVo.City = dr["CMFOD_City"].ToString();
        //                    else
        //                        mforderVo.City = "";
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_State"].ToString()))
        //                        mforderVo.State = dr["CMFOD_State"].ToString();
        //                    else
        //                        mforderVo.State = "";
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_Country"].ToString()))
        //                        mforderVo.Country = dr["CMFOD_Country"].ToString();
        //                    else
        //                        mforderVo.Country = "";
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_PinCode"].ToString()))
        //                        mforderVo.Pincode = dr["CMFOD_PinCode"].ToString();
        //                    else
        //                        mforderVo.Pincode = "";
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_LivingScince"].ToString()))
        //                        mforderVo.LivingSince = DateTime.Parse(dr["CMFOD_LivingScince"].ToString());
        //                    else
        //                        mforderVo.LivingSince = DateTime.MinValue;

        //                    if (!string.IsNullOrEmpty(dr["XF_FrequencyCode"].ToString()))
        //                        mforderVo.FrequencyCode = dr["XF_FrequencyCode"].ToString();
        //                    else
        //                        mforderVo.FrequencyCode = "";
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_StartDate"].ToString()))
        //                        mforderVo.StartDate = DateTime.Parse(dr["CMFOD_StartDate"].ToString());
        //                    else
        //                        mforderVo.StartDate = DateTime.MinValue;
        //                    if (!string.IsNullOrEmpty(dr["CMFOD_EndDate"].ToString()))
        //                        mforderVo.EndDate = DateTime.Parse(dr["CMFOD_EndDate"].ToString());
        //                    else
        //                        mforderVo.EndDate = DateTime.MinValue;

        //                    if (!string.IsNullOrEmpty(dr["CMFOD_Units"].ToString()))
        //                        mforderVo.Units = double.Parse(dr["CMFOD_Units"].ToString());
        //                    else
        //                        mforderVo.Units = 0;
                            
        //                }
        //                Session["orderVo"] = orderVo;
        //                Session["mforderVo"]=mforderVo;
        //            }
        //            //****************Old Vo,Bo****************************************
        //            //operationVo = operationBo.GetCustomerOrderTrackingDetails(orderId);
        //            //Session["operationVo"] = operationVo;
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderEntry", "loadcontrol('MFOrderEntry','action=View');", true);

        //        }
        //        catch (BaseApplicationException Ex)
        //        {
        //            throw Ex;
        //        }
        //    }
        //}
        protected void ddlCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIndividualCustomer.Text = string.Empty;
            txtIndividualCustomer.Enabled = true;
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
                lblselectCustomer.Visible = true;
                txtIndividualCustomer.Visible = true;
            }
            else if (ddlCustomerType.SelectedItem.Value == "1")
            {
                lblselectCustomer.Visible = true;
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
            else
            {
                txtIndividualCustomer.Enabled = false;
                hdnCustomerId.Value = null;
                lblselectCustomer.Visible = false;
                txtIndividualCustomer.Visible = false;
            }
        }
        protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnCustomerId.Value.ToString().Trim()))
            {
                customerId = int.Parse(hdnCustomerId.Value);

                //customerVo = customerBo.GetCustomer(int.Parse(txtIndividualCustomer_autoCompleteExtender.ContextKey));
            }
        }


        protected void gvCustomerOrderMIS_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblordertype = e.Row.FindControl("lblOrderType") as Label;
            //    //CheckBox chkApprove = e.Row.FindControl("cbRecons") as CheckBox;
            //    string ordertype = null;
            //    ordertype = lblordertype.Text;
            //    if (ordertype == "1")
            //        lblordertype.Text = "Immediate";
            //    else
            //        lblordertype.Text = "Future";
                

            //}
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;

                Label lblordertype = dataItem.FindControl("lblOrderType") as Label;
                string ordertype = null;
                ordertype = lblordertype.Text;
                if (ordertype == "1")
                    lblordertype.Text = "Immediate";
                else
                    lblordertype.Text = "Future";
            }

        }
        protected void lnkOrderNo_Click(object sender, EventArgs e)
        {
            int index = 0;
            DataSet dsGetMFOrderDetails;
            LinkButton lnkOrderNo = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkOrderNo.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            int orderId = int.Parse((gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString()));
            dsGetMFOrderDetails = mforderBo.GetCustomerMFOrderDetails(orderId);

            if (dsGetMFOrderDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsGetMFOrderDetails.Tables[0].Rows)
                {
                    orderVo.OrderId = int.Parse(dr["CO_OrderId"].ToString());
                    orderVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                    mforderVo.CustomerName = dr["Customer_Name"].ToString();
                    mforderVo.RMName = dr["RM_Name"].ToString();
                    mforderVo.BMName = dr["AB_BranchName"].ToString();
                    mforderVo.PanNo = dr["C_PANNum"].ToString();
                    if (!string.IsNullOrEmpty(dr["PA_AMCCode"].ToString().Trim()))
                        mforderVo.Amccode = int.Parse(dr["PA_AMCCode"].ToString());
                    else
                        mforderVo.Amccode = 0;
                    if (!string.IsNullOrEmpty(dr["PAIC_AssetInstrumentCategoryCode"].ToString().Trim()))
                        mforderVo.category = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["PASP_SchemePlanCode"].ToString().Trim()))
                        mforderVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                    mforderVo.OrderNumber = int.Parse(dr["CMFOD_OrderNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["CMFOD_Amount"].ToString().Trim()))
                        mforderVo.Amount = double.Parse(dr["CMFOD_Amount"].ToString());
                    else
                        mforderVo.Amount = 0;

                    if (int.Parse(dr["CMFA_accountid"].ToString()) != 0)
                        mforderVo.accountid = int.Parse(dr["CMFA_accountid"].ToString());
                    else
                        mforderVo.accountid = 0;
                    mforderVo.TransactionCode = dr["WMTT_TransactionClassificationCode"].ToString();
                    orderVo.OrderDate = DateTime.Parse(dr["CO_OrderDate"].ToString());
                    mforderVo.IsImmediate = int.Parse(dr["CMFOD_IsImmediate"].ToString());
                    orderVo.ApplicationNumber = dr["CO_ApplicationNumber"].ToString();
                    orderVo.ApplicationReceivedDate = DateTime.Parse(dr["CO_ApplicationReceivedDate"].ToString());
                    mforderVo.portfolioId = int.Parse(dr["CP_portfolioId"].ToString());
                    orderVo.PaymentMode = dr["XPM_PaymentModeCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["CO_ChequeNumber"].ToString()))
                        orderVo.ChequeNumber = dr["CO_ChequeNumber"].ToString();
                    else
                        orderVo.ChequeNumber = "";
                    if (!string.IsNullOrEmpty(dr["CO_PaymentDate"].ToString()))
                        orderVo.PaymentDate = DateTime.Parse(dr["CO_PaymentDate"].ToString());
                    else
                        orderVo.PaymentDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(dr["CMFOD_FutureTriggerCondition"].ToString()))
                        mforderVo.FutureTriggerCondition = dr["CMFOD_FutureTriggerCondition"].ToString();
                    else
                        mforderVo.FutureTriggerCondition = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_FutureExecutionDate"].ToString()))
                        mforderVo.FutureExecutionDate = DateTime.Parse(dr["CMFOD_FutureExecutionDate"].ToString());
                    else
                        mforderVo.FutureExecutionDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(dr["PASP_SchemePlanSwitch"].ToString()))
                        mforderVo.SchemePlanSwitch = int.Parse(dr["PASP_SchemePlanSwitch"].ToString());
                    else
                        mforderVo.SchemePlanSwitch = 0;
                    if (!string.IsNullOrEmpty(dr["CB_CustBankAccId"].ToString()))
                        orderVo.CustBankAccId = int.Parse(dr["CB_CustBankAccId"].ToString());
                    else
                        orderVo.CustBankAccId = 0;
                    if (!string.IsNullOrEmpty(dr["CMFOD_BankName"].ToString()))
                        mforderVo.BankName = dr["CMFOD_BankName"].ToString();
                    else
                        mforderVo.BankName = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_BranchName"].ToString()))
                        mforderVo.BranchName = dr["CMFOD_BranchName"].ToString();
                    else
                        mforderVo.BranchName = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine1"].ToString()))
                        mforderVo.AddrLine1 = dr["CMFOD_AddrLine1"].ToString();
                    else
                        mforderVo.AddrLine1 = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine2"].ToString()))
                        mforderVo.AddrLine2 = dr["CMFOD_AddrLine2"].ToString();
                    else
                        mforderVo.AddrLine2 = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine3"].ToString()))
                        mforderVo.AddrLine3 = dr["CMFOD_AddrLine3"].ToString();
                    else
                        mforderVo.AddrLine3 = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_City"].ToString()))
                        mforderVo.City = dr["CMFOD_City"].ToString();
                    else
                        mforderVo.City = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_State"].ToString()))
                        mforderVo.State = dr["CMFOD_State"].ToString();
                    else
                        mforderVo.State = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_Country"].ToString()))
                        mforderVo.Country = dr["CMFOD_Country"].ToString();
                    else
                        mforderVo.Country = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_PinCode"].ToString()))
                        mforderVo.Pincode = dr["CMFOD_PinCode"].ToString();
                    else
                        mforderVo.Pincode = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_LivingScince"].ToString()))
                        mforderVo.LivingSince = DateTime.Parse(dr["CMFOD_LivingScince"].ToString());
                    else
                        mforderVo.LivingSince = DateTime.MinValue;

                    if (!string.IsNullOrEmpty(dr["XF_FrequencyCode"].ToString()))
                        mforderVo.FrequencyCode = dr["XF_FrequencyCode"].ToString();
                    else
                        mforderVo.FrequencyCode = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_StartDate"].ToString()))
                        mforderVo.StartDate = DateTime.Parse(dr["CMFOD_StartDate"].ToString());
                    else
                        mforderVo.StartDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(dr["CMFOD_EndDate"].ToString()))
                        mforderVo.EndDate = DateTime.Parse(dr["CMFOD_EndDate"].ToString());
                    else
                        mforderVo.EndDate = DateTime.MinValue;

                    if (!string.IsNullOrEmpty(dr["CMFOD_Units"].ToString()))
                        mforderVo.Units = double.Parse(dr["CMFOD_Units"].ToString());
                    else
                        mforderVo.Units = 0;

                }
                Session["orderVo"] = orderVo;
                Session["mforderVo"] = mforderVo;
            }
            //****************Old Vo,Bo****************************************
            //operationVo = operationBo.GetCustomerOrderTrackingDetails(orderId);
            //Session["operationVo"] = operationVo;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderEntry", "loadcontrol('MFOrderEntry','action=View');", true);

        }

        protected void gvCustomerOrderMIS_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtOrderMIS = new DataTable();
            dtOrderMIS = (DataTable)Cache["OrderMIS" + userVo.UserId];
            gvCustomerOrderMIS.DataSource = dtOrderMIS;
            gvCustomerOrderMIS.Visible = true;
        }

        protected void btnMForderRecon_Click(object sender, ImageClickEventArgs e)
        {
            gvCustomerOrderMIS.ExportSettings.OpenInNewWindow = true;
            gvCustomerOrderMIS.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvCustomerOrderMIS.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvCustomerOrderMIS.MasterTableView.ExportToExcel();
        }
    }
}
