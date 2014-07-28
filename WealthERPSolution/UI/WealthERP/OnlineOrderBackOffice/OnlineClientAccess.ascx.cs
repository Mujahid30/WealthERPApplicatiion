using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

using BoCommon;
using BoOnlineOrderManagement;
using VoUser;
using VoOnlineOrderManagemnet;
using WealthERP.Base;


namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineClientAccess : System.Web.UI.UserControl
    {
        OnlineOrderBackOfficeBo onlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            //if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            //{
                txtClientCode_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtClientCode_autoCompleteExtender.ServiceMethod = "GetCustCode";
            //}
          //  BindAdviserClientKYCStatusList();
        }
        protected void ddlCOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearControls();
            if (ddlCOption.SelectedValue == "Clientcode")
            {
                tdtxtClientCode.Visible = true;
            }
            else
            {
                tdtxtClientCode.Visible = false;
            }
        }
        protected void clearControls()
        {
            txtClientCode.Text = "";
            //txtCustomerId.Value = string.Empty;
        }
        private string GetSelectedFilterValue()
        {

            string FilterOn;
            if (ddlCOption.SelectedValue == "Clientcode")
            {
                FilterOn = "customer";
            }
            else
            {
                FilterOn = ddlCOption.SelectedValue;
            }


            return FilterOn;
        }
        protected void click_Go(object sender, EventArgs e)
        {
            BindAdviserClientKYCStatusList();
            KYClist.Visible = true;
        }
        protected void BindAdviserClientKYCStatusList()
        {
            try
            {
                DataTable dtAdviserClientKYCStatusList = new DataTable();
                dtAdviserClientKYCStatusList = onlineOrderBackOfficeBo.GetAdviserClientKYCStatusList(adviserVo.advisorId, GetSelectedFilterValue(),(string.IsNullOrEmpty(txtClientCode.Text))?string.Empty:txtClientCode.Text);
                if (dtAdviserClientKYCStatusList.Rows.Count > 0)
                {
                    if (Cache["KYCList" + adviserVo.advisorId] == null)
                    {
                        Cache.Insert("KYCList" + adviserVo.advisorId, dtAdviserClientKYCStatusList);
                    }
                    else
                    {
                        Cache.Remove("KYCList" + adviserVo.advisorId);
                        Cache.Insert("KYCList" + adviserVo.advisorId, dtAdviserClientKYCStatusList);
                    }
                    gvKYCStatusList.DataSource = dtAdviserClientKYCStatusList;
                    gvKYCStatusList.DataBind();
                }
                else
                {
                    gvKYCStatusList.DataSource = dtAdviserClientKYCStatusList;
                    gvKYCStatusList.DataBind();
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
                FunctionInfo.Add("Method", "OnlineClientAccess.ascx.cs:BindAdviserClientKYCStatusList()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        protected void gvKYCStatusList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dsAdviserClientKYCStatusList = new DataSet();
            DataTable dtAdviserClientKYCStatusList = new DataTable();
            //dsSchemeMIS = (DataSet)Cache["SchemeMIS" + adviserVo.advisorId.ToString()];
            //DataTable dtCustomer = new DataTable();
            dtAdviserClientKYCStatusList = (DataTable)Cache["KYCList" + adviserVo.advisorId];

            if (dtAdviserClientKYCStatusList != null)
            {
                gvKYCStatusList.DataSource = dtAdviserClientKYCStatusList;
            }
        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            try
            {
                gvKYCStatusList.ExportSettings.OpenInNewWindow = true;
                gvKYCStatusList.ExportSettings.IgnorePaging = true;
                gvKYCStatusList.ExportSettings.HideStructureColumns = true;
                gvKYCStatusList.ExportSettings.ExportOnlyData = true;
                gvKYCStatusList.ExportSettings.FileName = "Client Access";
                gvKYCStatusList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvKYCStatusList.MasterTableView.ExportToExcel();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineClientAccess.ascx.cs:btnExportData_OnClick()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}