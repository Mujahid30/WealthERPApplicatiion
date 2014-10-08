using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using System.Data;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using Telerik.Web.UI;
using BoProductMaster;
using BoOnlineOrderManagement;
namespace WealthERP.OnlineOrderBackOffice
{
    public partial class SubBrokerCodecCeansing : System.Web.UI.UserControl
    {
        OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        ProductMFBo productMFBo = new ProductMFBo();
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["UserVo"];
            rmVo = (RMVo)Session["RMVo"];
            if (!IsPostBack)
            {
                BindAMC();
                BindScheme();
                BindRTA();
            }
        }
        protected void BindAMC()
        {
            DataTable dtBindAMC;
            try
            {
                if (ddlRTA.SelectedValue == "")
                {
                    dtBindAMC = OnlineOrderBackOfficeBo.GetAMCListRNTWise(ddlRTA.SelectedValue);
                    ddlAMC.DataSource = dtBindAMC;
                    ddlAMC.DataTextField = dtBindAMC.Columns["PA_AMCName"].ToString();
                    ddlAMC.DataValueField = dtBindAMC.Columns["PA_AMCCode"].ToString();
                    ddlAMC.DataBind();
                    ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
                }
                else
                {
                    dtBindAMC = OnlineOrderBackOfficeBo.GetAMCListRNTWise(ddlRTA.SelectedValue);
                    ddlAMC.DataSource = dtBindAMC;
                    ddlAMC.DataTextField = dtBindAMC.Columns["PA_AMCName"].ToString();
                    ddlAMC.DataValueField = dtBindAMC.Columns["PA_AMCCode"].ToString();
                    ddlAMC.DataBind();
                    ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        protected void ddlAMC_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindScheme();
        }
        protected void BindScheme()
        {
            DataTable dt;
            if (ddlAMC.SelectedValue == "0")
            {
                dt = productMFBo.GetSchemePlanName(int.Parse(ddlAMC.SelectedValue));
                ddlScheme.DataSource = dt;
                ddlScheme.DataValueField = "PASP_SchemePlanCode";
                ddlScheme.DataTextField = "PASP_SchemePlanName";
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            else
            {
                dt = productMFBo.GetSchemePlanName(int.Parse(ddlAMC.SelectedValue));
                ddlScheme.DataSource = dt;
                ddlScheme.DataValueField = "PASP_SchemePlanCode";
                ddlScheme.DataTextField = "PASP_SchemePlanName";
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));

            }
        }
        protected void BindRTA()
        {
            try
            {

                DataTable dtRTA;
                DataSet dsRTA;
                dsRTA = OnlineOrderBackOfficeBo.GetRTALists();
                dtRTA = dsRTA.Tables[0];
                ddlRTA.DataSource = dtRTA;
                ddlRTA.DataValueField = dtRTA.Columns["XES_SourceName"].ToString();
                ddlRTA.DataTextField = dtRTA.Columns["XES_SourceName"].ToString();
                ddlRTA.DataBind();
                ddlRTA.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        protected void ddlRTA_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindAMC();
        }
        protected void Go_OnClick(object sender, EventArgs e)
        {
            BindSubBrokerCleansingLisrt();
        }
        protected void BindSubBrokerCleansingLisrt()
        {
            DataTable dtBindSubBrokerCleansingLisrt;
            try
            {
                dtBindSubBrokerCleansingLisrt = OnlineOrderBackOfficeBo.GetSubBrokerCodeCleansing(ddlRTA.SelectedValue, int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), advisorVo.advisorId, int.Parse(ddlSubbrokerCode.SelectedValue));
                gvSubBrokerCleansing.DataSource = dtBindSubBrokerCleansingLisrt;
                gvSubBrokerCleansing.DataBind();
                if (Cache["SubBrokerCleansing" + userVo.UserId] == null)
                {
                    Cache.Insert("SubBrokerCleansing" + userVo.UserId, dtBindSubBrokerCleansingLisrt);
                }
                else
                {
                    Cache.Remove("SubBrokerCleansing" + userVo.UserId);
                    Cache.Insert("SubBrokerCleansing" + userVo.UserId, dtBindSubBrokerCleansingLisrt);
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        protected void gvSubBrokerCleansing_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindSubBrokerCleansingLisrt = new DataTable();
            dtBindSubBrokerCleansingLisrt = (DataTable)Cache["SubBrokerCleansing" + userVo.UserId];
            this.gvSubBrokerCleansing.DataSource = dtBindSubBrokerCleansingLisrt;
        }
        protected void btnUpdate_Update(object sender, EventArgs e)
        {
            string gvMFAId = "";
            int i = 0;
            // GridDataItem item = (GridDataItem)(sender);
            GridFooterItem footerItem = (GridFooterItem)gvSubBrokerCleansing.MasterTableView.GetItems(GridItemType.Footer)[0];
            TextBox newSubBrokerCode = (TextBox)footerItem.FindControl("newSubBrokerCode");
            foreach (GridDataItem dataItem in gvSubBrokerCleansing.MasterTableView.Items)
            {
                if ((dataItem.FindControl("chkItem") as CheckBox).Checked)
                {
                    i = i + 1;
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select Item To Update SubBrokerCode !');", true);
                return;
            }
            else
            {
                foreach (GridDataItem dataItem in gvSubBrokerCleansing.MasterTableView.Items)
                {
                    if ((dataItem.FindControl("chkItem") as CheckBox).Checked)
                    {
                        gvMFAId += dataItem.GetDataKeyValue("CMFT_MFTransId").ToString() + ",";
                    }
                }
                OnlineOrderBackOfficeBo.UpdateSubBrokerCode(gvMFAId.TrimEnd(','), newSubBrokerCode.Text);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('SubBrokerCode Update successfully !!');", true);
            }
        }
    }
}