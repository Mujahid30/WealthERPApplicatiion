using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;

using Telerik.Web.UI;
using System.Data;
using WealthERP.Base;
using System.Collections.Specialized;
using System.Collections;
using Microsoft.ApplicationBlocks.ExceptionManagement;



using BoCommon;
using BoUploads;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class UploadRequestStatus : System.Web.UI.UserControl
    {
        UserBo userBo;
        // ReqRejects
        UserVo userVo;
        AdvisorVo advisorVo;
        UploadCommonBo uploadCommonBo = new UploadCommonBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionBo.CheckSession();
            //userBo = new UserBo();
            //userVo = (UserVo)Session[SessionContents.UserVo];
            //if (Session["Theme"] != null)
            //{
            //    ddlTheme.SelectedValue = Session["Theme"].ToString();
            //}
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];

            if (!IsPostBack)
            {
                GetTypes();
            }
        }


        protected void btnGo_Click(object sender, EventArgs e)
        {
            pnlRequest.Visible = true;
            GetRequests();
           
        }
        private void GetTypes()
        {
            try
            {
                DataTable dtType = new DataTable();
                dtType = uploadCommonBo.GetCMLType().Tables[0];
                ddlType.DataValueField = "WT_TaskId";
                ddlType.DataTextField = "WT_Task";
                ddlType.DataSource = dtType;
                ddlType.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        private void GetRequests()
        {
            try
            {
                DataTable dtType = new DataTable();
                DataSet dsType = new DataSet();
                dsType = uploadCommonBo.GetCMLData(Convert.ToInt32(ddlType.SelectedValue), Convert.ToDateTime(txtReqDate.SelectedDate), advisorVo.advisorId);
                if (dsType.Tables.Count == 0)
                    return;
                if (dsType != null)
                    dtType = dsType.Tables[0];
                rgRequests.DataSource = dtType;
                rgRequests.DataBind();
                if (Cache[userVo.UserId.ToString() + "Requests"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "Requests");
                Cache.Insert(userVo.UserId.ToString() + "Requests", dtType);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }

        //private void GetRequestWiseRejects(int reqId, RadGrid rgReqWiseRejects)
        //{
        //    try
        //    {
        //        DataTable dtReqReje = new DataTable();
        //        DataSet dsRej = new DataSet();
        //        dsRej = uploadCommonBo.RequestWiseRejects(reqId);
        //        if (dsRej.Tables.Count == 0)
        //            return;
        //        dtReqReje = dsRej.Tables[0];
        //        rgReqWiseRejects.DataSource = dtReqReje;
        //        rgReqWiseRejects.DataBind();

        //        if (Cache[userVo.UserId.ToString() + "RequestsWiseRejects"] != null)
        //            Cache.Remove(userVo.UserId.ToString() + "RequestsWiseRejects");
        //        Cache.Insert(userVo.UserId.ToString() + "RequestsWiseRejects", dtReqReje);

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }

        //}
        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            rgRequests.ExportSettings.OpenInNewWindow = true;
            // rgRequests.MasterTableView.Caption = "Adviser:" + adviserVo.OrganizationName+' '+"RM:"+ rmVo.FirstName;            
            rgRequests.ExportSettings.IgnorePaging = true;
            rgRequests.ExportSettings.HideStructureColumns = true;
            rgRequests.ExportSettings.ExportOnlyData = true;
            rgRequests.ExportSettings.FileName = "Upload Request Status";
            rgRequests.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rgRequests.MasterTableView.ExportToExcel();
        }

        protected void rgRequests_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem && e.Item.ItemIndex != -1)
            {
                GridDataItem editform = (GridDataItem)e.Item;
                int inputRejects = Convert.ToInt32(editform["InputRejects"].Text);
                int stagingRejects = Convert.ToInt32(editform["StagingRejects"].Text);
                int Staging = Convert.ToInt32(editform["Staging"].Text);
                LinkButton lbDetails = (LinkButton)editform.FindControl("lbDetails");
                if (inputRejects + stagingRejects + Staging > 0)
                {
                    lbDetails.Visible = true;
                }
                else
                {
                    lbDetails.Visible = false;
                }
            }
        }


        protected void rgRequests_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtRequests = new DataTable();
            dtRequests = (DataTable)Cache[userVo.UserId.ToString() + "Requests"];
            if (dtRequests != null)
            {
                rgRequests.DataSource = dtRequests;
            }

        }
        protected void rgRequestRejects_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid rgRequestRejects = (RadGrid)sender;
            DataTable dtRequestsWiseRejects = new DataTable();
            dtRequestsWiseRejects = (DataTable)Cache[userVo.UserId.ToString() + "RequestsWiseRejects"];
            if (dtRequestsWiseRejects != null)
            {
                rgRequestRejects.DataSource = dtRequestsWiseRejects;
            }

        }
        protected void btnCategoriesExpandAll_Click(object sender, EventArgs e)
        {
            int reqId = 0;
            LinkButton buttonlink = (LinkButton)sender;
            GridDataItem gdi = (GridDataItem)buttonlink.NamingContainer;
            if (!string.IsNullOrEmpty(rgRequests.MasterTableView.DataKeyValues[gdi.ItemIndex]["ReqId"].ToString()))
            {
                reqId = int.Parse(rgRequests.MasterTableView.DataKeyValues[gdi.ItemIndex]["ReqId"].ToString());
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ManageProfileReject','?ReqId=" + reqId + "');", true);
            //else
            //{
            //    reqId = 0;
            //}
            //RadGrid rgReqWiseRejects = (RadGrid)gdi.FindControl("rgRequestRejects");
            //Panel pnlchild = (Panel)gdi.FindControl("pnlchild");
            //GetRequestWiseRejects(reqId, rgReqWiseRejects);
            //if (pnlchild.Visible == false)
            //{
            //    pnlchild.Visible = true;
            //    buttonlink.Text = "-";
            //}
            //else if (pnlchild.Visible == true)
            //{
            //    pnlchild.Visible = false;
            //    buttonlink.Text = "+";
            //}
        }
    }
}