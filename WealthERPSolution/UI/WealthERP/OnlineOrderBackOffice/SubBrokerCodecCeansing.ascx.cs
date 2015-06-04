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
                //BindRTA();
            }
        }
        protected void BindAMC()
        {
            DataTable dtBindAMC;
            try
            {
            //    if (ddlRTA.SelectedValue == "")
            //    {
                dtBindAMC = OnlineOrderBackOfficeBo.GetAMCListRNTWise("Select");
                    ddlAMC.DataSource = dtBindAMC;
                    ddlAMC.DataTextField = dtBindAMC.Columns["PA_AMCName"].ToString();
                    ddlAMC.DataValueField = dtBindAMC.Columns["PA_AMCCode"].ToString();
                    ddlAMC.DataBind();
                    ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
                //}
                //else
                //{
                //    dtBindAMC = OnlineOrderBackOfficeBo.GetAMCListRNTWise(ddlRTA.SelectedValue);
                //    ddlAMC.DataSource = dtBindAMC;
                //    ddlAMC.DataTextField = dtBindAMC.Columns["PA_AMCName"].ToString();
                //    ddlAMC.DataValueField = dtBindAMC.Columns["PA_AMCCode"].ToString();
                //    ddlAMC.DataBind();
                //    ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
                //}
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
            if (ddlAMC.SelectedValue == "")
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

                //DataTable dtRTA;
                //DataSet dsRTA;
                //dsRTA = OnlineOrderBackOfficeBo.GetRTALists();
                //dtRTA = dsRTA.Tables[0];
                //ddlRTA.DataSource = dtRTA;
                //ddlRTA.DataValueField = dtRTA.Columns["XES_SourceName"].ToString();
                //ddlRTA.DataTextField = dtRTA.Columns["XES_SourceName"].ToString();
                //ddlRTA.DataBind();
                //ddlRTA.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        //protected void ddlRTA_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindAMC();
        //}
        protected void Go_OnClick(object sender, EventArgs e)
        {
            BindSubBrokerCleansingLisrt();
            imgexportButton.Visible = true;
        }
        protected void BindSubBrokerCleansingLisrt()
        {
            DataTable dtBindSubBrokerCleansingLisrt;
            try
            {
                dtBindSubBrokerCleansingLisrt = OnlineOrderBackOfficeBo.GetSubBrokerCodeCleansing(int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), advisorVo.advisorId, int.Parse(ddlSubbrokerCode.SelectedValue));
                gvSubBrokerCleansing.DataSource = dtBindSubBrokerCleansingLisrt;
                gvSubBrokerCleansing.DataBind();
                pnlgvSubBrokerCleansing.Visible = true;
                btnUpdateSubBroker.Visible = true;
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
            if (dtBindSubBrokerCleansingLisrt != null)
            {
                gvSubBrokerCleansing.DataSource = dtBindSubBrokerCleansingLisrt;
            }
        }
        protected void btnUpdate_Update(object sender, EventArgs e)
        {
            string gvMFAId = "";
            int i = 0;
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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select Item To Apply SubBrokerCode !');", true);
                return;
            }
            else
            {
                foreach (GridDataItem dataItem in gvSubBrokerCleansing.MasterTableView.Items)
                {
                    if ((dataItem.FindControl("chkItem") as CheckBox).Checked)
                    {
                        TextBox txtSubBrokerCode = dataItem.FindControl("txtSubBrokerCode") as TextBox;
                        txtSubBrokerCode.Text = newSubBrokerCode.Text;
                    }
                }
            
            }
        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvSubBrokerCleansing.ExportSettings.OpenInNewWindow = true;
            gvSubBrokerCleansing.ExportSettings.IgnorePaging = true;
            gvSubBrokerCleansing.ExportSettings.HideStructureColumns = true;
            gvSubBrokerCleansing.ExportSettings.ExportOnlyData = true;
            gvSubBrokerCleansing.ExportSettings.FileName = "SubBroker Code Cleansing";
            gvSubBrokerCleansing.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvSubBrokerCleansing.MasterTableView.ExportToExcel();
        }
        protected void btnUpdateSubBroker_OnClick(object sender, EventArgs e)
        {
            int i=0;
            foreach (GridDataItem dataItem in gvSubBrokerCleansing.MasterTableView.Items)
            {
                if ((dataItem.FindControl("txtSubBrokerCode") as TextBox).Text != string.Empty)
                {
                    i = i + 1;
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Fill SubBrokerCode !');", true);
                return;
            }
            else
            {
                DataTable dtSubBrokerCode = new DataTable();
                dtSubBrokerCode.Columns.Add("TransactioId", typeof(Int32));
                dtSubBrokerCode.Columns.Add("subBrokerCode", typeof(String));
                DataRow drSubBrokerCode;
                foreach (GridDataItem radItem in gvSubBrokerCleansing.MasterTableView.Items)
                {
                    drSubBrokerCode = dtSubBrokerCode.NewRow();
                    if ((radItem.FindControl("txtSubBrokerCode") as TextBox).Text != string.Empty)
                    {
                        drSubBrokerCode["TransactioId"] = int.Parse(gvSubBrokerCleansing.MasterTableView.DataKeyValues[radItem.ItemIndex]["CMFT_MFTransId"].ToString());
                        TextBox txtSubBrokerCode = radItem.FindControl("txtSubBrokerCode") as TextBox;
                        drSubBrokerCode["subBrokerCode"] = txtSubBrokerCode.Text;
                        dtSubBrokerCode.Rows.Add(drSubBrokerCode);
                    }
                }
                OnlineOrderBackOfficeBo.UpdateNewSubBrokerCode(dtSubBrokerCode);
                BindSubBrokerCleansingLisrt();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('SubBroker Code Update Successfully !!');", true);
            }
        }
    }
}