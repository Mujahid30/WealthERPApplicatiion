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

namespace WealthERP.OPS
{
    public partial class OrderMIS : System.Web.UI.UserControl
    {

        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo;
        RMVo rmVo = new RMVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();

        int portfolioId;
        int schemePlanCode;
        int customerId;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();

            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];

            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            gvMIS.Visible = false;
            btnSubmit.Visible = false;
            if (Request.QueryString["result"] != null)
            {
                gvMIS.Visible = true;
                BindMISGridView();
            }
            if (!IsPostBack)
            {

                if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";

                }
                BindBranchDropDown();
                BindRMDropDown();
                //BindPortfolioDropdown();
                //BindFolionumberDropdown(portfolioId);
            }
            

        }
        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
            Session["customerVo"] = customerVo;
            customerId = int.Parse(txtCustomerId.Value);
        }
        private void BindFolionumberDropdown(int portfolioId)
        {
            DataSet dsCustomerAccounts = new DataSet();
            DataTable dtCustomerAccounts;

            if (schemePlanCode != 0)
            {
                portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                dsCustomerAccounts = customerAccountBo.GetCustomerMFAccounts(portfolioId, "MF", schemePlanCode);
            }

            if (dsCustomerAccounts.Tables.Count > 0)
            {
                dtCustomerAccounts = dsCustomerAccounts.Tables[0];

                ddlFolioNumber.DataSource = dtCustomerAccounts;
                ddlFolioNumber.DataTextField = "CMFA_FolioNum";
                ddlFolioNumber.DataValueField = "CMFA_AccountId";
                ddlFolioNumber.DataBind();
            }
            ddlFolioNumber.Items.Insert(0, "Select a Folio");
        }

        //private void BindPortfolioDropdown()
        //{
        //    DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
        //    ddlPortfolio.DataSource = ds;
        //    ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
        //    ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
        //    ddlPortfolio.DataBind();
        //}


        private void BindMISGridView()
        {
            DataTable dtBindGridView = new DataTable();

            dtBindGridView.Columns.Add("Id");
            dtBindGridView.Columns.Add("Assetclass");
            dtBindGridView.Columns.Add("Branch");
            dtBindGridView.Columns.Add("RM");
            dtBindGridView.Columns.Add("Customer");
            dtBindGridView.Columns.Add("FolioNo");
            dtBindGridView.Columns.Add("Scheme");
            dtBindGridView.Columns.Add("Type");
            dtBindGridView.Columns.Add("TransactionDate");
            dtBindGridView.Columns.Add("Price");
            dtBindGridView.Columns.Add("Units");
            dtBindGridView.Columns.Add("Amount");
            dtBindGridView.Columns.Add("OrderNumber");
            dtBindGridView.Columns.Add("TransactionNumber");
            dtBindGridView.Columns.Add("OrderType");
            dtBindGridView.Columns.Add("OrderStatus");
            dtBindGridView.Columns.Add("Applicateionreceivedate");
            dtBindGridView.Columns.Add("Orderdate");
            dtBindGridView.Columns.Add("Pending/Reject");
            dtBindGridView.Columns.Add("ProcessID");
            dtBindGridView.Columns.Add("ApplicationNumber");

            DataRow drdtBindGridView;
            drdtBindGridView = dtBindGridView.NewRow();
            drdtBindGridView[0] = "1";
            drdtBindGridView[1] = "MF";
            drdtBindGridView[2] = "Kalina";
            drdtBindGridView[3] = "Walk-in";
            drdtBindGridView[4] = "GK JAIN ";
            drdtBindGridView[5] = "5360043/46";
            drdtBindGridView[6] = "ICICI Prudential Focused Bluechip Equity Fund - Retail Growth Plan";
            drdtBindGridView[7] = "BUY";
            drdtBindGridView[8] = "3/8/2011";
            drdtBindGridView[9] = "16.36";
            drdtBindGridView[10] = "5077.15";
            drdtBindGridView[11] = "83062.26";
            drdtBindGridView[12] = "10001";
            drdtBindGridView[13] = "3156789";
            drdtBindGridView[14] = "Immediate";
            drdtBindGridView[15] = "Executed";
            drdtBindGridView[16] = "3/8/2011";
            drdtBindGridView[17] = "3/8/2011";
            drdtBindGridView[18] = "";
            drdtBindGridView[19] = "101";
            drdtBindGridView[20] = "1989002";
            dtBindGridView.Rows.Add(drdtBindGridView);

            drdtBindGridView = dtBindGridView.NewRow();
            drdtBindGridView[0] = "2";
            drdtBindGridView[1] = "MF";
            drdtBindGridView[2] = "Kalina";
            drdtBindGridView[3] = "Walk-in";
            drdtBindGridView[4] = "GK JAIN ";
            drdtBindGridView[5] = "1831166/97";
            drdtBindGridView[6] = "Kotak-Mid-Cap-Growth";
            drdtBindGridView[7] = "BUY";
            drdtBindGridView[8] = "3/8/2011";
            drdtBindGridView[9] = "25.16";
            drdtBindGridView[10] = "3339.82";
            drdtBindGridView[11] = "84043.29";
            drdtBindGridView[12] = "10002";
            drdtBindGridView[13] = "3156790";
            drdtBindGridView[14] = "Immediate";
            drdtBindGridView[15] = "Executed";
            drdtBindGridView[16] = "3/8/2011";
            drdtBindGridView[17] = "3/8/2011";
            drdtBindGridView[18] = "";
            drdtBindGridView[19] = "102";
            drdtBindGridView[20] = "1989003";
            dtBindGridView.Rows.Add(drdtBindGridView);

            drdtBindGridView = dtBindGridView.NewRow();
            drdtBindGridView[0] = "3";
            drdtBindGridView[1] = "MF";
            drdtBindGridView[2] = "Kalina";
            drdtBindGridView[3] = "Walk-in";
            drdtBindGridView[4] = "GK JAIN ";
            drdtBindGridView[5] = "7034832/85";
            drdtBindGridView[6] = "HDFC Floating Rate Income Fund-Short Term Plan - Retail Option - Dividend - Daily";
            drdtBindGridView[7] = "BUY";
            drdtBindGridView[8] = "3/8/2011";
            drdtBindGridView[9] = "46.41";
            drdtBindGridView[10] = "1811.29";
            drdtBindGridView[11] = "84070.56";
            drdtBindGridView[12] = "10003";
            drdtBindGridView[13] = "3156791";
            drdtBindGridView[14] = "Immediate";
            drdtBindGridView[15] = "Executed";
            drdtBindGridView[16] = "3/8/2011";
            drdtBindGridView[17] = "3/8/2011";
            drdtBindGridView[18] = "";
            drdtBindGridView[19] = "103";
            drdtBindGridView[20] = "1989004";
            dtBindGridView.Rows.Add(drdtBindGridView);

            drdtBindGridView = dtBindGridView.NewRow();
            drdtBindGridView[0] = "4";
            drdtBindGridView[1] = "MF";
            drdtBindGridView[2] = "Kalina";
            drdtBindGridView[3] = "Walk-in";
            drdtBindGridView[4] = "GK JAIN ";
            drdtBindGridView[5] = "1831166/97";
            drdtBindGridView[6] = "Kotak Floater Long-Term-Daily Dividend";
            drdtBindGridView[7] = "BUY";
            drdtBindGridView[8] = "3/8/2011";
            drdtBindGridView[9] = "10.07";
            drdtBindGridView[10] = "8337.78";
            drdtBindGridView[11] = "84043.29";
            drdtBindGridView[12] = "10004";
            drdtBindGridView[13] = "3156792";
            drdtBindGridView[14] = "Immediate";
            drdtBindGridView[15] = "Executed";
            drdtBindGridView[16] = "3/8/2011";
            drdtBindGridView[17] = "3/8/2011";
            drdtBindGridView[18] = "";
            drdtBindGridView[19] = "104";
            drdtBindGridView[20] = "1989005";
            dtBindGridView.Rows.Add(drdtBindGridView);

            drdtBindGridView = dtBindGridView.NewRow();
            drdtBindGridView[0] = "5";
            drdtBindGridView[1] = "MF";
            drdtBindGridView[2] = "Kalina";
            drdtBindGridView[3] = "Walk-in";
            drdtBindGridView[4] = "GK JAIN ";
            drdtBindGridView[5] = "5360043/46";
            drdtBindGridView[6] = "ICICI Prudential Focused Bluechip Equity Fund - Retail Growth Plan";
            drdtBindGridView[7] = "BUY";
            drdtBindGridView[8] = "3/8/2011";
            drdtBindGridView[9] = "100.2842";
            drdtBindGridView[10] = "828.269";
            drdtBindGridView[11] = "83062.26";
            drdtBindGridView[12] = "10005";
            drdtBindGridView[13] = "3156793";
            drdtBindGridView[14] = "Immediate";
            drdtBindGridView[15] = "Executed";
            drdtBindGridView[16] = "3/8/2011";
            drdtBindGridView[17] = "3/8/2011";
            drdtBindGridView[18] = "";
            drdtBindGridView[19] = "105";
            drdtBindGridView[20] = "1989006";
            dtBindGridView.Rows.Add(drdtBindGridView);

            drdtBindGridView = dtBindGridView.NewRow();
            drdtBindGridView[0] = "6";
            drdtBindGridView[1] = "MF";
            drdtBindGridView[2] = "Kalina";
            drdtBindGridView[3] = "Walk-in";
            drdtBindGridView[4] = "GK JAIN ";
            drdtBindGridView[5] = "7034832/85";
            drdtBindGridView[6] = "HDFC Floating Rate Income Fund-Short Term Plan - Retail Option - Dividend - Daily";
            drdtBindGridView[7] = "SELL";
            drdtBindGridView[8] = "5/8/2011";
            drdtBindGridView[9] = "50.4146";
            drdtBindGridView[10] = "1811.295";
            drdtBindGridView[11] = "84070.56";
            drdtBindGridView[12] = "10007";
            drdtBindGridView[13] = "3156795";
            drdtBindGridView[14] = "Immediate";
            drdtBindGridView[15] = "Pending";
            drdtBindGridView[16] = "3/8/2011";
            drdtBindGridView[17] = "5/8/2011";
            drdtBindGridView[18] = "Kyc not done";
            drdtBindGridView[19] = "107";
            drdtBindGridView[20] = "1989008";
            dtBindGridView.Rows.Add(drdtBindGridView);

            drdtBindGridView = dtBindGridView.NewRow();
            drdtBindGridView[0] = "7";
            drdtBindGridView[1] = "MF";
            drdtBindGridView[2] = "Kalina";
            drdtBindGridView[3] = "Walk-in";
            drdtBindGridView[4] = "GK JAIN ";
            drdtBindGridView[5] = "1831166/97";
            drdtBindGridView[6] = "Kotak Floater Long-Term-Daily Dividend";
            drdtBindGridView[7] = "SELL";
            drdtBindGridView[8] = "5/8/2011";
            drdtBindGridView[9] = "13.0798";
            drdtBindGridView[10] = "8337.7852";
            drdtBindGridView[11] = "84043.29";
            drdtBindGridView[12] = "10008";
            drdtBindGridView[13] = "3156796";
            drdtBindGridView[14] = "Immediate";
            drdtBindGridView[15] = "Rejected";
            drdtBindGridView[16] = "3/8/2011";
            drdtBindGridView[17] = "3/8/2011";
            drdtBindGridView[18] = "Signature not matching";
            drdtBindGridView[19] = "108";
            drdtBindGridView[20] = "1989009";
            dtBindGridView.Rows.Add(drdtBindGridView);

            drdtBindGridView = dtBindGridView.NewRow();
            drdtBindGridView[0] = "8";
            drdtBindGridView[1] = "MF";
            drdtBindGridView[2] = "Kalina";
            drdtBindGridView[3] = "Walk-in";
            drdtBindGridView[4] = "GK JAIN ";
            drdtBindGridView[5] = "5360043/46";
            drdtBindGridView[6] = "ICICI Prudential Flexible Income Plan Regular- Daily Dividend Plan";
            drdtBindGridView[7] = "SELL";
            drdtBindGridView[8] = "18/8/2011";
            drdtBindGridView[9] = "103.2842";
            drdtBindGridView[10] = "828.269";
            drdtBindGridView[11] = "83062.26";
            drdtBindGridView[12] = "10009";
            drdtBindGridView[13] = "3156797";
            drdtBindGridView[14] = "Future";
            drdtBindGridView[15] = "In Process";
            drdtBindGridView[16] = "3/8/2011";
            drdtBindGridView[17] = "18/8/2011";
            drdtBindGridView[18] = "";
            drdtBindGridView[19] = "109";
            drdtBindGridView[20] = "1989010";
            dtBindGridView.Rows.Add(drdtBindGridView);

            gvMIS.DataSource = dtBindGridView;
            gvMIS.DataBind();
            btnSubmit.Visible = true;
            Session["GridView"]=dtBindGridView;


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
            
            gvMIS.Visible=true;
            BindMISGridView();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string ids = GetSelectedIdString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalFundPage", "loadcontrol('OrderRecon','?result=" + ids + "');", true);
           
        }
        private string GetSelectedIdString()
        {
            string gvIds = "";

            //'Navigate through each row in the GridView for checkbox items
            foreach (GridViewRow gvRow in gvMIS.Rows)
            {
                CheckBox ChkBxItem = (CheckBox)gvRow.FindControl("cbRecons");
                if (ChkBxItem.Checked)
                {
                    gvIds += Convert.ToString(gvMIS.DataKeys[gvRow.RowIndex].Value) + "~";
                }
            }
            

            return gvIds;
 
        }

        protected void lnkOrderId_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderEntry", "loadcontrol('OrderEntry','login');", true);
            
        }

    }
}
