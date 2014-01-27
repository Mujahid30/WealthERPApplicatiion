using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using WealthERP.Base;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;


using VoUser;
using BoCommon;

using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;


namespace WealthERP.OnlineOrderBackOffice
{
    public partial class ManageLookups : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        OnlineOrderBackOfficeBo onlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        WERPlookupCodeValueManagementVo werplookupCodeValueManagementVo;
        int count;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                BindLookupdropdowns();
                ShowControlsBasedonView("Select");
            }
        }
        private void BindLookupdropdowns()
        {
            BindCategory();
        }
        private void BindCategory()
        {
            try
            {

                DataTable dtCategory = new DataTable();



                dtCategory = onlineOrderBackOfficeBo.GetLookupCategory().Tables[0];
                ddlCategory.DataSource = dtCategory;
                ddlCategory.DataValueField = dtCategory.Columns["WCM_Id"].ToString();
                ddlCategory.DataTextField = dtCategory.Columns["WCM_Name"].ToString();
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));


            }



            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:BindCategory()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                
                ShowControlsBasedonView(ddlView.SelectedValue);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:BindRtA()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindRtA()
        {
            try
            {
                DataTable dtRTA = new DataTable();
                dtRTA = onlineOrderBackOfficeBo.GetRTA().Tables[0];
                ddlRTA.DataSource = dtRTA;
                ddlRTA.DataValueField = dtRTA.Columns["XES_SourceCode"].ToString();
                ddlRTA.DataTextField = dtRTA.Columns["XES_SourceName"].ToString();
                ddlRTA.DataBind();
                ddlRTA.Items.Insert(0, new ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:BindRtA()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindWerpValues(int categoryID, DropDownList ddlWerp)
        {
            try
            {
                DataTable dtWerp = new DataTable();
                dtWerp = onlineOrderBackOfficeBo.GetWERPValues(categoryID).Tables[0];
                ddlWerp.DataSource = dtWerp;
                ddlWerp.DataValueField = dtWerp.Columns["WCMV_LookupId"].ToString();
                ddlWerp.DataTextField = dtWerp.Columns["ValueIds"].ToString();
                ddlWerp.DataBind();
                ddlWerp.Items.Insert(0, new ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:BindWerpValues()");
                object[] objects = new object[1];
                objects[1] = categoryID;
                objects[2] = ddlWerp;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindWerpGrid(int categoryID)
        {
            try
            {
                DataTable dtWerp = new DataTable();
                dtWerp = onlineOrderBackOfficeBo.GetWERPValues(categoryID).Tables[0];
                rgWerp.DataSource = dtWerp;
                rgWerp.DataBind();
                if (Cache[userVo.UserId.ToString() + "ManageLookups"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "ManageLookups");
                Cache.Insert(userVo.UserId.ToString() + "ManageLookups", dtWerp);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:BindWerpGrid()");
                object[] objects = new object[1];
                objects[1] = categoryID;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindMapingGrid(string sourceCode, int categoryID)
        {
            try
            {
                DataTable dtMap = new DataTable();
                dtMap = onlineOrderBackOfficeBo.GetRtaWiseMapings(sourceCode, categoryID).Tables[0];
                rgMaping.DataSource = dtMap;
                rgMaping.DataBind();

                if (dtMap.Rows.Count > 0)
                    btnRemoveMaping.Visible = true;
                else
                    btnRemoveMaping.Visible = false;


                if (Cache[userVo.UserId.ToString() + "ManageLookupsMapping"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "ManageLookupsMapping");
                Cache.Insert(userVo.UserId.ToString() + "ManageLookupsMapping", dtMap);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:BindMapingGrid()");
                object[] objects = new object[2];
                objects[1] = sourceCode;
                objects[2] = categoryID;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        private void CreateNewWerpName(string newWerpName)
        {
            try
            {
                bool result;
                werplookupCodeValueManagementVo = new WERPlookupCodeValueManagementVo();
                werplookupCodeValueManagementVo.WerpName = newWerpName;
                werplookupCodeValueManagementVo.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);

                result = onlineOrderBackOfficeBo.CreateNewWerpName(werplookupCodeValueManagementVo, userVo.UserId);
                if (result)
                {
                    BindWerpGrid(Convert.ToInt32(ddlCategory.SelectedValue));
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Internal Name Created Successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Already Exist.');", true);
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:CreateNewWerpName()");
                object[] objects = new object[1];
                objects[1] = newWerpName;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void DeleteWerpName(int lookupID)
        {
            try
            {
                bool result;
                werplookupCodeValueManagementVo = new WERPlookupCodeValueManagementVo();
                werplookupCodeValueManagementVo.LookupID = lookupID;

                result = onlineOrderBackOfficeBo.DeleteWerpName(werplookupCodeValueManagementVo);
                if (result)
                {
                    BindWerpGrid(Convert.ToInt32(ddlCategory.SelectedValue));
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Internal Name Deleted Successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Maping Exist.You cant delete.');", true);
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:DeleteWerpName()");
                object[] objects = new object[1];
                objects[0] = lookupID;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void UpdateWerpName(int lookupID, string werpName)
        {
            try
            {
                bool result;
                werplookupCodeValueManagementVo = new WERPlookupCodeValueManagementVo();
                werplookupCodeValueManagementVo.LookupID = lookupID;
                werplookupCodeValueManagementVo.WerpName = werpName;

                result = onlineOrderBackOfficeBo.UpdateWerpName(werplookupCodeValueManagementVo, userVo.UserId);
                if (result)
                {
                    BindWerpGrid(Convert.ToInt32(ddlCategory.SelectedValue));
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Internal Name Updated Successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Error in Updating');", true);
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:UpdateWerpName()");
                object[] objects = new object[1];
                objects[1] = lookupID;
                objects[2] = werpName;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void RemoveMaping()
        {
            try
            {
                int i = 0;
                bool result;
                int mapingID;
                foreach (GridDataItem gvRow in rgMaping.Items)
                {
                    CheckBox chk = (CheckBox)gvRow.FindControl("cbMap");
                    if (chk.Checked)
                    {
                        count++;
                    }
                    if (count > 1)
                        chk.Checked = false;

                }
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record!');", true);
                }
                if (count == 1)
                {
                    foreach (GridDataItem gdi in rgMaping.Items)
                    {
                        if (((CheckBox)gdi.FindControl("cbMap")).Checked == true)
                        {

                            int selectedRow = gdi.ItemIndex + 1;
                            mapingID = int.Parse(rgMaping.MasterTableView.DataKeyValues[selectedRow - 1]["WCMVXM_Id"].ToString());

                            werplookupCodeValueManagementVo = new WERPlookupCodeValueManagementVo();
                            werplookupCodeValueManagementVo.MapID = mapingID;

                            result = onlineOrderBackOfficeBo.RemoveMapingWIthRTA(werplookupCodeValueManagementVo);
                            if (result)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Maping Removed Successfully.');", true);
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You can select only one record at a time.');", true);
                }
                BindMapingGrid(ddlRTA.SelectedValue, Int32.Parse(ddlCategory.SelectedValue));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:RemoveMaping()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


        public void btnRemoveMaping_OnClick(object sender, EventArgs e)
        {
            RemoveMaping();
        }

        private void CreateMap(DropDownList ddlWerp, TextBox txtExternalCode, TextBox txtNewEXtName)
        {
            try
            {
                bool result;
                werplookupCodeValueManagementVo = new WERPlookupCodeValueManagementVo();
                werplookupCodeValueManagementVo.SourceCode = ddlRTA.SelectedValue;
                werplookupCodeValueManagementVo.LookupID = Convert.ToInt32(ddlWerp.SelectedValue);
                werplookupCodeValueManagementVo.ExternalCode = txtExternalCode.Text;
                werplookupCodeValueManagementVo.ExternalName = txtNewEXtName.Text;

                result = onlineOrderBackOfficeBo.CreateMapwithRTA(werplookupCodeValueManagementVo, userVo.UserId);
                if (result)
                {
                    BindMapingGrid(ddlRTA.SelectedValue, Convert.ToInt32(ddlCategory.SelectedValue));
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Maping Created Successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Maping Already Exist.');", true);
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:CreateMap()");
                object[] objects = new object[3];
                objects[1] = ddlWerp;
                objects[2] = txtExternalCode;
                objects[3] = txtNewEXtName;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void ddlView_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlView.SelectedValue == "Select")
                    return;

                ShowControlsBasedonView("");
                if (ddlView.SelectedValue == "Mapping")
                {
                    trSourceType.Visible = true;
                    BindRtA();
                }
                else if (ddlView.SelectedValue == "Lookup")
                {
                    trSourceType.Visible = false;
                    
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:ddlView_OnSelectedIndexChanged()");
                object[] objects = new object[2];
                objects[1] = sender;
                objects[2] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        private void ShowControlsBasedonView(string viewType)
        {
            // 1 for lookups grid 2 for maping grid
            try
            {
                
                tblwerpGrd.Visible = false;
                tblExtMapGrd.Visible = false;
               // trSourceType.Visible = false;
                btnWERPExport.Visible = false;
                btnMapingExport.Visible = false;

                if (viewType == "Lookup")
                {
                    tblwerpGrd.Visible = true;
                    btnWERPExport.Visible = true;
                    BindWerpGrid(Convert.ToInt32(ddlCategory.SelectedValue));
                }
                else if (viewType == "Mapping")
                {
                    
                    tblExtMapGrd.Visible = true;
                    btnMapingExport.Visible = true;
                    BindMapingGrid(ddlRTA.SelectedValue, Convert.ToInt32(ddlCategory.SelectedValue));
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:ShowControlsBasedonView()");
                object[] objects = new object[1];
                objects[1] = viewType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        protected void ddlRTA_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void rgMaping_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    GridEditFormItem editform = (GridEditFormItem)e.Item;
                    DropDownList ddlWerp = (DropDownList)editform.FindControl("ddlWerp");
                    BindWerpValues(Convert.ToInt32(ddlCategory.SelectedValue), ddlWerp);
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:rgMaping_ItemDataBound()");
                object[] objects = new object[2];
                objects[1] = sender;
                objects[2] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void rgWerp_ItemCommand(object source, GridCommandEventArgs e)
        {
            string description = string.Empty;
            string name = string.Empty;
            string insertType = string.Empty;

            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                int EditingLookupID = Convert.ToInt32(rgWerp.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WCMV_LookupId"].ToString());
                TextBox txtNewWERPName = (TextBox)e.Item.FindControl("txtNewWERPName");
                UpdateWerpName(EditingLookupID, txtNewWERPName.Text.ToString());

            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                TextBox txtNewWERPName = (TextBox)e.Item.FindControl("txtNewWERPName");
                CreateNewWerpName(txtNewWERPName.Text.Trim());
            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                int DeletingLookupID = Convert.ToInt32(rgWerp.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WCMV_LookupId"].ToString());
                DeleteWerpName(DeletingLookupID);
            }
            //bind the grid to get the edit form mode
            BindWerpGrid(Convert.ToInt32(ddlCategory.SelectedValue));
        }


        protected void rgWerp_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DataTable dtWerp = new DataTable();
                if (Cache[userVo.UserId.ToString() + "ManageLookups"] != null)
                {
                    dtWerp = (DataTable)Cache[userVo.UserId.ToString() + "ManageLookups"];
                    rgWerp.DataSource = dtWerp;
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:rgWerp_NeedDataSource()");
                object[] objects = new object[2];
                objects[1] = source;
                objects[2] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void rgMaping_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == RadGrid.UpdateCommandName)
                {
                    DropDownList ddlWerp = (DropDownList)e.Item.FindControl("ddlWerp");
                    TextBox txtExternalCode = (TextBox)e.Item.FindControl("txtExternalCode");
                    TextBox txtNewEXtName = (TextBox)e.Item.FindControl("txtNewEXtName");
                    CreateMap(ddlWerp, txtExternalCode, txtNewEXtName);
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:rgMaping_UpdateCommand()");
                object[] objects = new object[2];
                objects[1] = source;
                objects[2] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void btnWERPExport_Click(object sender, ImageClickEventArgs e)
        {
            rgWerp.ExportSettings.OpenInNewWindow = true;
            rgWerp.ExportSettings.IgnorePaging = true;
            rgWerp.ExportSettings.HideStructureColumns = true;
            rgWerp.ExportSettings.ExportOnlyData = true;
            rgWerp.ExportSettings.FileName = "Internal Lookups";
            rgWerp.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rgWerp.MasterTableView.ExportToExcel();

        }
        protected void btnMapingExport_Click(object sender, ImageClickEventArgs e)
        {
            rgMaping.ExportSettings.OpenInNewWindow = true;
            rgMaping.ExportSettings.IgnorePaging = true;
            rgMaping.ExportSettings.HideStructureColumns = true;
            rgMaping.ExportSettings.ExportOnlyData = true;
            rgMaping.ExportSettings.FileName = "External Lookups";
            rgMaping.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rgMaping.MasterTableView.ExportToExcel();

        }
        protected void rgMaping_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DataTable dtMap = new DataTable();
                if (Cache[userVo.UserId.ToString() + "ManageLookupsMapping"] != null)
                {
                    dtMap = (DataTable)Cache[userVo.UserId.ToString() + "ManageLookupsMapping"];
                    rgMaping.DataSource = dtMap;
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:rgMaping_NeedDataSource()");
                object[] objects = new object[2];
                objects[1] = source;
                objects[2] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }


    }

}
