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
    public partial class OnlineSchemeMIS : System.Web.UI.UserControl
    {
        OnlineOrderMISBo OnlineOrderMISBo = new OnlineOrderMISBo();
        OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        int SchemePlanCode = 0;
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

                if (ddlTosee.SelectedIndex == 0 || ddlTosee.SelectedIndex == 1)
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
        protected void Onselectedindex_select(object sender, EventArgs e)
        {
            if (ddlProduct.SelectedValue == "BO")
            {
                tdtosee.Visible = false;
                llbtosee.Visible = false;
            }
            else
            {
                tdtosee.Visible = true;
                llbtosee.Visible = true;
            }
        }
        protected void BindSchemeMIS()
        {
            try
            {
                DataSet dsSchemeMIs = new DataSet();
                DataTable dtschememis = new DataTable();
                dsSchemeMIs = OnlineOrderMISBo.GetSchemeMIS(hdnAssettype.Value,int.Parse(ddlTosee.SelectedItem.Value));
                dtschememis = dsSchemeMIs.Tables[0];
                if (dtschememis.Rows.Count > 0)
                {
                    if (Cache["SchemeMIS" + adviserVo.advisorId] == null)
                    {
                        Cache.Insert("SchemeMIS" + adviserVo.advisorId, dtschememis);
                    }
                    else
                    {
                        Cache.Remove("SchemeMIS" + adviserVo.advisorId);
                        Cache.Insert("SchemeMIS" + adviserVo.advisorId, dtschememis);
                    }
                    gvonlineschememis.DataSource = dtschememis;
                    gvonlineschememis.DataBind();
                    SchemeMIS.Visible = true;
                    pnlSchemeMIS.Visible = true;
                }
                else
                {
                    tdtosee.Visible=false;
                    gvonlineschememis.DataSource = dtschememis;
                    gvonlineschememis.DataBind();
                    SchemeMIS.Visible = true;
                    pnlSchemeMIS.Visible = true;
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
            DropDownList ddlAction = (DropDownList)sender;
            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            string action = "";
            SchemePlanCode = int.Parse(gvonlineschememis.MasterTableView.DataKeyValues[gvr.ItemIndex]["PASP_SchemePlanCode"].ToString());
            // string Status = (gvonlineschememis.MasterTableView.DataKeyValues[gvr.ItemIndex]["PASP_Status"].ToString());
            Session["SchemeList"] = OnlineOrderBackOfficeBo.GetOnlineSchemeSetUp(SchemePlanCode);
            if (ddlAction.SelectedItem.Value.ToString() == "View")
            {
                if (ddlProduct.SelectedItem.Value.ToString() == "MF")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineSchemeSetUp", "loadcontrol('OnlineSchemeSetUp','?SchemePlanCode=" + SchemePlanCode + "&strAction=" + ddlAction.SelectedItem.Value.ToString() + " ');", true);
                }
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Is InActive Scheme !!');", true);
                //}
            }
            if (ddlAction.SelectedItem.Value.ToString() == "Edit")
            {
                if (ddlProduct.SelectedItem.Value.ToString() == "MF")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineSchemeSetUp", "loadcontrol('OnlineSchemeSetUp','?SchemePlanCode=" + SchemePlanCode + "&strAction=" + ddlAction.SelectedItem.Value.ToString() + " ');", true);

                }
            }
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
