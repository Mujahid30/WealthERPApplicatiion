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
            BindAdviserClientKYCStatusList();
        }
        protected void BindAdviserClientKYCStatusList()
        {
            try
            {
                DataTable dtAdviserClientKYCStatusList = new DataTable();
                dtAdviserClientKYCStatusList = onlineOrderBackOfficeBo.GetAdviserClientKYCStatusList(adviserVo.advisorId);
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