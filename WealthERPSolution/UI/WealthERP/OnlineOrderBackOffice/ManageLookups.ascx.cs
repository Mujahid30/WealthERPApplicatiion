using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using WealthERP.Base;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using VoUser;
using BoCommon;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class ManageLookups : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        OnlineOrderBackOfficeBo onlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        OnlineOrderBackOfficeVo onlineOrderBackOfficeVo;
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
            BindRtA();

        }
        private void BindCategory()
        {
            DataTable dtCategory = new DataTable();
            dtCategory = onlineOrderBackOfficeBo.GetLookupCategory().Tables[0];
            ddlCategory.DataSource = dtCategory;
            ddlCategory.DataValueField = dtCategory.Columns["WCM_Id"].ToString();
            ddlCategory.DataTextField = dtCategory.Columns["WCM_Name"].ToString();
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));

        }
        private void BindRtA()
        {
            DataTable dtRTA = new DataTable();
            dtRTA = onlineOrderBackOfficeBo.GetRTA().Tables[0];
            ddlRTA.DataSource = dtRTA;
            ddlRTA.DataValueField = dtRTA.Columns["XES_SourceCode"].ToString();
            ddlRTA.DataTextField = dtRTA.Columns["XES_SourceName"].ToString();
            ddlRTA.DataBind();
            ddlRTA.Items.Insert(0, new ListItem("Select", "Select"));
        }
        private void BindWerpValues(int categoryID, DropDownList ddlWerp)
        {
            DataTable dtWerp = new DataTable();
            dtWerp = onlineOrderBackOfficeBo.GetWERPValues(categoryID).Tables[0];
            ddlWerp.DataSource = dtWerp;
            ddlWerp.DataValueField = dtWerp.Columns["WCMV_LookupId"].ToString();
            ddlWerp.DataTextField = dtWerp.Columns["ValueIds"].ToString();
            ddlWerp.DataBind();
            ddlWerp.Items.Insert(0, new ListItem("Select", "Select"));

        }
        private void BindWerpGrid(int categoryID)
        {
            DataTable dtWerp = new DataTable();
            dtWerp = onlineOrderBackOfficeBo.GetWERPValues(categoryID).Tables[0];
            rgWerp.DataSource = dtWerp;
            rgWerp.DataBind();
            if (Cache[userVo.UserId.ToString() + "ManageLookups"] != null)
                Cache.Remove(userVo.UserId.ToString() + "ManageLookups");
            Cache.Insert(userVo.UserId.ToString() + "ManageLookups", dtWerp);
        }
        private void BindMapingGrid(string sourceCode, int categoryID)
        {
            DataTable dtMap = new DataTable();
            dtMap = onlineOrderBackOfficeBo.GetRtaWiseMapings(sourceCode, categoryID).Tables[0];
            rgMaping.DataSource = dtMap;
            rgMaping.DataBind();
            if (Cache[userVo.UserId.ToString() + "ManageLookupsMapping"] != null)
                Cache.Remove(userVo.UserId.ToString() + "ManageLookupsMapping");
            Cache.Insert(userVo.UserId.ToString() + "ManageLookupsMapping", dtMap);
        }
        private void CreateNewWerpName(string newWerpName)
        {
            bool result;
            onlineOrderBackOfficeVo = new OnlineOrderBackOfficeVo();
            onlineOrderBackOfficeVo.WerpName = newWerpName;
            onlineOrderBackOfficeVo.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);

            result = onlineOrderBackOfficeBo.CreateNewWerpName(onlineOrderBackOfficeVo, userVo.UserId);
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
        private void RemoveMaping()
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

                        onlineOrderBackOfficeVo = new OnlineOrderBackOfficeVo();
                        onlineOrderBackOfficeVo.MapID = mapingID;

                        result = onlineOrderBackOfficeBo.RemoveMapingWIthRTA(onlineOrderBackOfficeVo);
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

        public void Button3_OnClick(object sender, EventArgs e)
        {
            RemoveMaping();
        }
       
        private void CreateMap(DropDownList ddlWerp, TextBox txtExternalCode, TextBox txtNewEXtName)
        {
            bool result;
            onlineOrderBackOfficeVo = new OnlineOrderBackOfficeVo();
            onlineOrderBackOfficeVo.SourceCode = ddlRTA.SelectedValue;
            onlineOrderBackOfficeVo.LookupID = Convert.ToInt32(ddlWerp.SelectedValue);
            onlineOrderBackOfficeVo.ExternalCode = txtExternalCode.Text;
            onlineOrderBackOfficeVo.ExternalName = txtNewEXtName.Text;

            result = onlineOrderBackOfficeBo.CreateMapwithRTA(onlineOrderBackOfficeVo, userVo.UserId);
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

        protected void ddlView_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlCategory.SelectedValue == "Select")
            {
                ddlView.SelectedValue = "Select";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Select Category.');", true);
                return;
            }

            ShowControlsBasedonView(ddlView.SelectedValue);

        }
        private void ShowControlsBasedonView(string viewType)
        {
            // 1 for lookups grid 2 for maping grid

            tblwerpGrd.Visible = false;
            tblExtMapGrd.Visible = false;
            tblMap.Visible = false;
            if (viewType == "1")
            {
                tblwerpGrd.Visible = true;
            }
            else if (viewType == "2")
            {
                tblMap.Visible = true;
                // BindWerpValues(Convert.ToInt32(ddlCategory.SelectedValue));
            }

        }
        protected void ddlRTA_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlRTA.SelectedValue == "Select" & ddlCategory.SelectedValue == "Select")
                return;
            tblExtMapGrd.Visible = true;
            BindMapingGrid(ddlRTA.SelectedValue, Convert.ToInt32(ddlCategory.SelectedValue));
        }
        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue == "Select")
                return;
            //  BindWerpValues(Convert.ToInt32(ddlCategory.SelectedValue));

            BindWerpGrid(Convert.ToInt32(ddlCategory.SelectedValue));
        }
        protected void rgWerp_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                //DataSet dsCommissionLookup;
                //dsCommissionLookup = (DataSet)Session["CommissionLookUpData"];

                //GridEditFormItem editform = (GridEditFormItem)e.Item;
                //DropDownList ddlCommissionType = (DropDownList)editform.FindControl("ddlCommissionType");
                //DropDownList ddlInvestorType = (DropDownList)editform.FindControl("ddlInvestorType");

            }
        }

        protected void rgMaping_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                //DataSet dsCommissionLookup;
                //dsCommissionLookup = (DataSet)Session["CommissionLookUpData"];

                GridEditFormItem editform = (GridEditFormItem)e.Item;
                DropDownList ddlWerp = (DropDownList)editform.FindControl("ddlWerp");
                //DropDownList ddlInvestorType = (DropDownList)editform.FindControl("ddlInvestorType");
                BindWerpValues(Convert.ToInt32(ddlCategory.SelectedValue), ddlWerp);
            }
        }
        protected void rgWerp_UpdateCommand(object source, GridCommandEventArgs e)
        {

            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                TextBox txtNewWERPName = (TextBox)e.Item.FindControl("txtNewWERPName");
                CreateNewWerpName(txtNewWERPName.Text.Trim());
            }
        }
        protected void rgWerp_ItemCommand(object source, GridCommandEventArgs e)
        {


        }
        protected void rgWerp_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtWerp = new DataTable();
            if (Cache[userVo.UserId.ToString() + "ManageLookups"] != null)
            {
                dtWerp = (DataTable)Cache[userVo.UserId.ToString() + "ManageLookups"];
                rgWerp.DataSource = dtWerp;
            }
        }
        protected void rgMaping_UpdateCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                DropDownList ddlWerp = (DropDownList)e.Item.FindControl("ddlWerp");
                TextBox txtExternalCode = (TextBox)e.Item.FindControl("txtExternalCode");
                TextBox txtNewEXtName = (TextBox)e.Item.FindControl("txtNewEXtName");
                CreateMap(ddlWerp, txtExternalCode, txtNewEXtName);
            }
        }
        protected void rgMaping_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataTable dtMap = new DataTable();
            if (Cache[userVo.UserId.ToString() + "ManageLookupsMapping"] != null)
            {
                dtMap = (DataTable)Cache[userVo.UserId.ToString() + "ManageLookupsMapping"];
                rgMaping.DataSource = dtMap;
            }


        }


    }

}
