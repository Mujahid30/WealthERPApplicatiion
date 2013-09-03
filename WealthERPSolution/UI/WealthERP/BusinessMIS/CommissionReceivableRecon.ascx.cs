using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoWerpAdmin;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoAdvisorProfiling;
using VoUser;
using BoUploads;
using BoCustomerGoalProfiling;
using Telerik.Web.UI;
using BoCommon;
using BoAdvisorProfiling;
using System.Configuration;
using BOAssociates;

namespace WealthERP.BusinessMIS
{
    public partial class CommissionReceivableRecon : System.Web.UI.UserControl
    {
        PriceBo priceBo = new PriceBo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        AssociatesBo associatesBo = new AssociatesBo();


        string categoryCode = string.Empty;
        int amcCode = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                BindMutualFundDropDowns();
                BindNAVCategory();
                int day = 1;
                gvCommissionReceiveRecon.Visible = false;
                txtFrom.SelectedDate = DateTime.Parse(day.ToString() + '/' + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString());
                txtTo.SelectedDate = DateTime.Now;
            }
        }
        private void BindNAVCategory()
        {
            DataSet dsNavCategory;
            DataTable dtNavCategory;
            dsNavCategory = priceBo.GetNavOverAllCategoryList();
            dtNavCategory = dsNavCategory.Tables[0];
            if (dtNavCategory.Rows.Count > 0)
            {

                ddlCategory.DataSource = dtNavCategory;
                ddlCategory.DataValueField = dtNavCategory.Columns["Category_Code"].ToString();
                ddlCategory.DataTextField = dtNavCategory.Columns["Category_Name"].ToString();
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("All", "All"));

            }
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssuer.SelectedIndex != 0)
            {
                if (ddlCategory.SelectedIndex != 0)
                {
                    int amcCode = int.Parse(ddlIssuer.SelectedValue);
                    LoadAllSchemeList(amcCode);
                    //GdBind_Click(sender,e);
                }

            }

        }
        private void SetParameters()
        {
            //if (userVo.UserType=="advisor")
            //{
            if (string.IsNullOrEmpty(txtFrom.SelectedDate.ToString()) != true)
                hdnFromDate.Value = txtFrom.SelectedDate.ToString();
            if (string.IsNullOrEmpty(txtTo.SelectedDate.ToString()) != true)
                hdnToDate.Value = txtTo.SelectedDate.ToString();
            if (string.IsNullOrEmpty(ddlScheme.SelectedItem.Value.ToString()) != true)
                hdnschemeId.Value = ddlScheme.SelectedItem.Value.ToString();
            if (string.IsNullOrEmpty(ddlCategory.SelectedItem.Value.ToString()) != true)
                hdnCategory.Value = ddlCategory.SelectedItem.Value.ToString();
            if (string.IsNullOrEmpty(ddlSelectType.SelectedItem.Value.ToString()) != true)
                hdnSBbrokercode.Value = ddlSelectType.SelectedItem.Value.ToString();

            //}


        }
        protected void GdBind_Click(Object sender, EventArgs e)
        {
            SetParameters();
            DataSet ds = new DataSet();
            //ds.ReadXml(Server.MapPath(@"\Sample.xml"));

            ds = adviserMFMIS.GetCommissionReceivableRecon(advisorVo.advisorId, int.Parse(hdnschemeId.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), hdnCategory.Value, int.Parse(hdnSBbrokercode.Value),ddlCommType.SelectedValue);
            if (ds.Tables[0] != null)
            {
                gvCommissionReceiveRecon.Visible = true;
                gvCommissionReceiveRecon.DataSource = ds.Tables[0];
                DataTable dtGetAMCTransactionDeatails = new DataTable();
                gvCommissionReceiveRecon.DataBind();
                if (Cache["gvCommissionReceiveRecon" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("gvCommissionReceiveRecon" + userVo.UserId.ToString(), ds.Tables[0]);
                }
                else
                {
                    Cache.Remove("gvCommissionReceiveRecon" + userVo.UserId.ToString());
                    Cache.Insert("gvCommissionReceiveRecon" + userVo.UserId.ToString(), ds.Tables[0]);
                }
            }
            else
            {
                gvCommissionReceiveRecon.Visible = false;

            }
        }
        public void BindMutualFundDropDowns()
        {
            PriceBo priceBo = new PriceBo();
            DataTable dtGetMutualFundList = new DataTable();
            dtGetMutualFundList = priceBo.GetMutualFundList();
            ddlIssuer.DataSource = dtGetMutualFundList;
            ddlIssuer.DataTextField = dtGetMutualFundList.Columns["PA_AMCName"].ToString();
            ddlIssuer.DataValueField = dtGetMutualFundList.Columns["PA_AMCCode"].ToString();
            ddlIssuer.DataBind();
            ddlIssuer.Items.Insert(0, new ListItem("Select AMC", "Select AMC Code"));

        }
        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserType.SelectedIndex != 0)
            {
                if (ddlUserType.SelectedValue == "BM")
                {
                    BindBranchSubBrokerCode();
                }
                else if (ddlUserType.SelectedValue == "RM")
                {
                    BindSalesSubBrokerCode();
                }
                else if (ddlUserType.SelectedValue == "Associates")
                {
                    BindAssociatesSubBrokerCode();
                }
            }
        }
        private void BindAssociatesSubBrokerCode()
        {
            DataSet ds;
            DataTable dt;
            dt = associatesBo.GetAssociatesSubBrokerCodeList(advisorVo.advisorId);
            if (dt.Rows.Count > 0)
            {
                ddlSelectType.DataSource = dt;
                ddlSelectType.DataValueField = dt.Columns["AAC_AdviserAgentId"].ToString();
                ddlSelectType.DataTextField = dt.Columns["AAC_AgentCode"].ToString();
                ddlSelectType.DataBind();
            }
            ddlSelectType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }

        private void BindSalesSubBrokerCode()
        {
            DataSet ds;
            DataTable dt;
            dt = associatesBo.GetSalesSubBrokerCodeList(advisorVo.advisorId);
            if (dt.Rows.Count > 0)
            {
                ddlSelectType.DataSource = dt;
                ddlSelectType.DataValueField = dt.Columns["AAC_AdviserAgentId"].ToString();
                ddlSelectType.DataTextField = dt.Columns["AAC_AgentCode"].ToString();
                ddlSelectType.DataBind();
            }
            ddlSelectType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
        }

        private void BindBranchSubBrokerCode()
        {
            DataSet ds;
            DataTable dt;
            dt = associatesBo.GetBranchSubBrokerCodeList(advisorVo.advisorId);
            if (dt.Rows.Count > 0)
            {
                ddlSelectType.DataSource = dt;
                ddlSelectType.DataValueField = dt.Columns["AAC_AdviserAgentId"].ToString();
                ddlSelectType.DataTextField = dt.Columns["AAC_AgentCode"].ToString();
                ddlSelectType.DataBind();
            }
            ddlSelectType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
        }
        protected void ddlIssuer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssuer.SelectedIndex != 0)
            {
                int amcCode = int.Parse(ddlIssuer.SelectedValue);
                ddlCategory.SelectedIndex = 0;
                LoadAllSchemeList(amcCode);


            }
        }
        private void LoadAllSchemeList(int amcCode)
        {
            DataSet dsLoadAllScheme = new DataSet();
            DataTable dtLoadAllScheme = new DataTable();
            if (ddlIssuer.SelectedIndex != 0)
            {
                amcCode = int.Parse(ddlIssuer.SelectedValue.ToString());
                categoryCode = ddlCategory.SelectedValue;
                //dtLoadAllScheme = priceBo.GetAllScehmeList(amcCode);
                dsLoadAllScheme = priceBo.GetSchemeListCategoryConcatenation(amcCode, categoryCode);
                dtLoadAllScheme = dsLoadAllScheme.Tables[0];
            }

            if (dtLoadAllScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtLoadAllScheme;
                ddlScheme.DataTextField = dtLoadAllScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataValueField = dtLoadAllScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlScheme.DataSource = null;
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("All", "0"));
            }

        }
        protected void gvCommissionReceiveRecon_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["gvCommissionReceiveRecon" + userVo.UserId];
            gvCommissionReceiveRecon.DataSource = dt;
            gvCommissionReceiveRecon.Visible = true;
        }
    }
}