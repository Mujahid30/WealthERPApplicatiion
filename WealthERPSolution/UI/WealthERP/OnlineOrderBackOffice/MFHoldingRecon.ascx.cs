using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.ExceptionManagement;


using BoCommon;
using BoOnlineOrderManagement;
using Telerik.Web.UI;
using VoUser;
using VoOnlineOrderManagemnet;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class MFHoldingRecon : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        OnlineOrderMISBo OnlineOrderMISBo = new OnlineOrderMISBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            AdvisorVo adviserVo = new AdvisorVo();

            BindRequestId();
            if (!Page.IsPostBack)
            {
                Label1.Visible = false;
                txtTo.Visible = false;
                btnSynch.Visible = false;

            }

        }
        protected void BindRequestId()
        {
            DataTable dtGetIssueName = new DataTable();

            dtGetIssueName = OnlineOrderMISBo.GetMFHolding();
            ddlIssue.DataSource = dtGetIssueName;
            ddlIssue.DataValueField = dtGetIssueName.Columns["WR_RequestId"].ToString();
            ddlIssue.DataTextField = dtGetIssueName.Columns["WRD_InputParameterValue"].ToString();
            ddlIssue.DataBind();
            ddlIssue.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }
        protected void BindAMC(int requestId)
        {
            ddlAMC.Items.Clear();
            if (ddlAMC.SelectedIndex == 0) return;

            DataTable dtAmc = OnlineOrderMISBo.GetAMCList(requestId);
            if (dtAmc == null) return;

            if (dtAmc.Rows.Count > 0)
            {
                ddlAMC.DataSource = dtAmc;
                ddlAMC.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAMC.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAMC.DataBind();
            }
            ddlAMC.Items.Insert(0, new ListItem("Select", "0"));
            
        }
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            try
            {
                trFliters.Visible = false;
                DataTable dtMFHoldingRecon = new DataTable();
                dtMFHoldingRecon = OnlineOrderMISBo.GetMFHoldingRecon(int.Parse(ddlIssue.SelectedValue));
                if (dtMFHoldingRecon.Rows.Count > 0)
                {
                    BindMFHoldingRecon();

                    Label1.Visible = true;
                    txtTo.Visible = true;
                    btnSynch.Visible = true;
                    trNoRecords.Visible = false;
                    divNoRecords.Visible = false;
                }
                else
                {
                    BindMFHoldingRecon();
                    Label1.Visible = false;
                    txtTo.Visible = false;
                    btnSynch.Visible = false;
                    trNoRecords.Visible = true;
                    divNoRecords.Visible = true;

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
                FunctionInfo.Add("Method", "MFHoldingRecon.ascx.cs:btngo_Click()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindMFHoldingReconAfterSync(false);
        }
        protected void ddlDifference_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindMFHoldingReconAfterSync(false);
        }
        protected void btnSync_OnClick(object sender, EventArgs e)
        {
            MFHoldingRecons.Visible = false;
            pnlMFHoldingRecons.Visible = false;
            if (OnlineOrderMISBo.updateSystemMFHoldingRecon(int.Parse(ddlIssue.SelectedValue), Convert.ToDateTime(txtTo.SelectedDate)))
            {
                BindAMC(int.Parse(ddlIssue.SelectedValue));
                trFliters.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Synchronization done Successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Synchronization Unsuccsessfully');", true);
            }




        }
        protected void BindMFHoldingRecon()
        {
            try
            {

                DataTable dtMFHoldingRecon = new DataTable();
                dtMFHoldingRecon = OnlineOrderMISBo.GetMFHoldingRecon(int.Parse(ddlIssue.SelectedValue));
                if (dtMFHoldingRecon.Rows.Count > 0)
                {
                    if (Cache["MFHoldingMIS" + userVo.UserId] == null)
                    {
                        Cache.Insert("MFHoldingMIS" + userVo.UserId, dtMFHoldingRecon);
                    }
                    else
                    {
                        Cache.Remove("MFHoldingMIS" + userVo.UserId);
                        Cache.Insert("MFHoldingMIS" + userVo.UserId, dtMFHoldingRecon);
                    }
                    gvMFHoldinfRecon.DataSource = dtMFHoldingRecon;
                    gvMFHoldinfRecon.DataBind();
                    MFHoldingRecons.Visible = true;
                    pnlMFHoldingRecons.Visible = true;

                    gvMFHoldinfRecon.MasterTableView.GetColumn("SchemePlanName").Display = false;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("Diff").Display = false;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemUnits").Display = false;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemNAV").Display = false;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemNAVDate").Display = false;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemAUM").Display = false;

                    //this.gvMFHoldinfRecon.ClientSettings.Scrolling.FrozenColumnsCount = 1;
                    //this.gvMFHoldinfRecon.ClientSettings.Scrolling.UseStaticHeaders = true;
                    //this.gvMFHoldinfRecon.ClientSettings.Scrolling.SaveScrollPosition = true;
                }
                else
                {
                    gvMFHoldinfRecon.DataSource = dtMFHoldingRecon;
                    gvMFHoldinfRecon.DataBind();
                    MFHoldingRecons.Visible = true;
                    pnlMFHoldingRecons.Visible = true;

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
                FunctionInfo.Add("Method", "MFHoldingRecon.ascx.cs:MFHoldingRecon()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void BindMFHoldingReconAfterSync(bool isSync)
        {
            try
            {
                MFHoldingRecons.Visible = false;
                pnlMFHoldingRecons.Visible = false;
                DataTable dtMFHoldingReconSync = new DataTable();
                dtMFHoldingReconSync = OnlineOrderMISBo.GetMFHoldingReconAfterSync(int.Parse(ddlIssue.SelectedValue), Convert.ToDateTime(txtTo.SelectedDate), int.Parse(ddlType.SelectedValue), int.Parse(ddlDifference.SelectedValue), int.Parse(ddlAMC.SelectedValue), isSync);
               
               
               
                if (dtMFHoldingReconSync.Rows.Count > 0)
                {
                    if (Cache["MFHoldingMIS" + userVo.UserId] == null)
                    {
                        Cache.Insert("MFHoldingMIS" + userVo.UserId, dtMFHoldingReconSync);
                    }
                    else
                    {
                        Cache.Remove("MFHoldingMIS" + userVo.UserId);
                        Cache.Insert("MFHoldingMIS" + userVo.UserId, dtMFHoldingReconSync);
                    }
                    gvMFHoldinfRecon.DataSource = dtMFHoldingReconSync;
                    gvMFHoldinfRecon.DataBind();
                    MFHoldingRecons.Visible = true;
                    pnlMFHoldingRecons.Visible = true;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SchemePlanName").Display = true;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemUnits").Display = true;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemNAV").Display = true;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemNAVDate").Display = true;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemAUM").Display = true;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("Diff").Display = true;
                   

                   
                    
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
                FunctionInfo.Add("Method", "MFHoldingRecon.ascx.cs:MFHoldingRecon()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
       
        protected void gvMFHoldinfRecon_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataTable dtMFHoldingRecon = new DataTable();
            dtMFHoldingRecon = (DataTable)Cache["MFHoldingMIS" + userVo.UserId];
            if (dtMFHoldingRecon != null)
            {
                gvMFHoldinfRecon.DataSource = dtMFHoldingRecon;
            }
        }
        protected void btnExportData_OnClick(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "MFHoldingMIS.xls"));
            Response.ContentType = "application/ms-excel";
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["MFHoldingMIS" + userVo.UserId];
            string str = string.Empty;
            foreach (DataColumn dtcol in dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in dt.Rows)
            {
                str = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        protected void ddlAMC_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindMFHoldingReconAfterSync(false);
        }
        protected void btnFilter_OnClick(object sender, EventArgs e)
        {
            BindMFHoldingReconAfterSync(false);
            gvMFHoldinfRecon.MasterTableView.GetColumn("SystemInvestorName").Visible = true;
            gvMFHoldinfRecon.MasterTableView.GetColumn("SystemPan").Visible = true;
        }


    }
}
