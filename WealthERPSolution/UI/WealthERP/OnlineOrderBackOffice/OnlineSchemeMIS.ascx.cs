using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using BoOnlineOrderManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Telerik.Web.UI;
using VoUser;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineSchemeMIS : System.Web.UI.UserControl
    {
        OnlineOrderMISBo OnlineOrderMISBo = new OnlineOrderMISBo();
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
        }


        protected void SetParameter()
        {
            try
            {
                if (ddlProduct.SelectedIndex != 0)
                {
                    hdnAssettype.Value = ddlProduct.SelectedValue;
                    ViewState["Assettype"] = hdnAssettype.Value;
                }
                else
                {
                    hdnAssettype.Value = "0";
                }

                if (ddlTosee.SelectedIndex != 0)
                {
                    hdnIsonline.Value = ddlTosee.SelectedValue;
                    ViewState["Isonline"] = hdnIsonline.Value;
                }
                else
                {
                    hdnIsonline.Value = "0";
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
                FunctionInfo.Add("Method", "OnlineSchemeMIS.ascx.cs:SetParameter()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        protected void BindSchemeMIS()
        {
            try
            {
                DataSet dsSchemeMIs = new DataSet();
                DataTable dtschememis = new DataTable();
                dsSchemeMIs = OnlineOrderMISBo.GetSchemeMIS(hdnAssettype.Value,int.Parse(hdnIsonline.Value));
                dtschememis = dsSchemeMIs.Tables[0];
                gvonlineschememis.DataSource = dtschememis;
                gvonlineschememis.DataBind();
                if (Cache["SchemeMIS" + adviserVo.advisorId ] == null)
                    {
                        Cache.Insert("SchemeMIS" + adviserVo.advisorId ,dtschememis);
                    }
                    else
                    {
                        Cache.Remove("SchemeMIS" + adviserVo.advisorId);
                        Cache.Insert("SchemeMIS" + adviserVo.advisorId,dtschememis);
                    }
                SchemeMIS.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeMIS.ascx.cs:SetParameter()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            try
            {
                gvonlineschememis.ExportSettings.OpenInNewWindow = true;
                gvonlineschememis.ExportSettings.IgnorePaging = true;
                gvonlineschememis.ExportSettings.HideStructureColumns = true;
                gvonlineschememis.ExportSettings.ExportOnlyData = true;
                gvonlineschememis.ExportSettings.FileName = "Scheme MIS";
                gvonlineschememis.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvonlineschememis.MasterTableView.ExportToExcel();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeMIS.ascx.cs:btnExportData_OnClick()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void ddlAction_OnSelectedIndexChanged(object sender, EventArgs e)
        {

                // DropDownList ddlAction = (DropDownList)sender;
                // GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                ////int selectedRow = gvr.ItemIndex + 1;
                // string action = "";
                // int SchemePlanCode = int.Parse(gvonlineschememis.MasterTableView.DataKeyValues[gvr.ItemIndex]["PASP_SchemePlanCode"].ToString());

                // if (ddlAction.SelectedItem.Value.ToString() == "View")
                // {
                //     action = "View";
                //     if (ddlProduct.SelectedItem.Value.ToString() == "MF")
                //     {
                //         ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineSchemeSetUP", "loadcontrol('OnlineSchemeSetUP',,'action=View');", true);   
                //     }
                // }
            }

        protected void gvonlineschememis_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dsSchemeMIS = new DataSet();
            DataTable dtschememis = new DataTable();
            //dsSchemeMIS = (DataSet)Cache["SchemeMIS" + adviserVo.advisorId.ToString()];
            //DataTable dtCustomer = new DataTable();
            dtschememis = (DataTable)Cache["SchemeMIS" + adviserVo.advisorId];

            if(dtschememis!=null)
                {
                gvonlineschememis.DataSource=dtschememis;
                }
        }
    
        protected void btngo_Click(object sender, EventArgs e)
        {
            try
            {
                SetParameter();
                BindSchemeMIS();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeMIS.ascx.cs:btngo_Click()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

    }
}