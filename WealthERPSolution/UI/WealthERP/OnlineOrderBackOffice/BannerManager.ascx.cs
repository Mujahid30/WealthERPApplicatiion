using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using WealthERP.Base;
using System.Data;
using Telerik.Web.UI;
using BoOnlineOrderManagement;
using System.Configuration;
using System.IO;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class BannerManager : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        OnlineOrderBackOfficeBo onlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            userVo = (UserVo)Session[SessionContents.UserVo];
            if (!IsPostBack)
            {
                multipageAdsUpload.SelectedIndex = 0;
                BindBannerDetails();
                BindScrollerDetails();
                BindDemoVideoDetails();
                BindFAQDetails();
                //hdnButtonText.Value = ConfigurationManager.AppSettings["ADVISOR_UPLOAD_PATH"].ToString() + "\\";
            }
        }


        private void BindBannerDetails()
        {

            DataTable dt = onlineOrderBackOfficeBo.GetBannerDetailsWithAssetGroup();
            if (dt.Rows.Count >= 0)
            {
                if (Cache["BannerDetailsList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("BannerDetailsList" + userVo.UserId.ToString(), dt);
                }
                else
                {
                    Cache.Remove("BannerDetailsList" + userVo.UserId.ToString());
                    Cache.Insert("BannerDetailsList" + userVo.UserId.ToString(), dt);
                }
                RadGrid1.DataSource = dt;
                RadGrid1.DataBind();

            }
        }
        private void BindScrollerDetails()
        {

            DataTable dt = onlineOrderBackOfficeBo.GetAdvertisementDetailsWithAssetGroup("Scroll");
            if (dt.Rows.Count >= 0)
            {
                if (Cache["ScrollerDetailsList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("ScrollerDetailsList" + userVo.UserId.ToString(), dt);
                }
                else
                {
                    Cache.Remove("ScrollerDetailsList" + userVo.UserId.ToString());
                    Cache.Insert("ScrollerDetailsList" + userVo.UserId.ToString(), dt);
                }
                RadGrid2.DataSource = dt;
                RadGrid2.DataBind();

            }
        }
        private void BindDemoVideoDetails()
        {

            DataTable dt = onlineOrderBackOfficeBo.GetAdvertisementDetailsWithAssetGroup("Demo");
            if (dt.Rows.Count >= 0)
            {
                if (Cache["DemoVideoDetailsList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("DemoVideoDetailsList" + userVo.UserId.ToString(), dt);
                }
                else
                {
                    Cache.Remove("DemoVideoDetailsList" + userVo.UserId.ToString());
                    Cache.Insert("DemoVideoDetailsList" + userVo.UserId.ToString(), dt);
                }
                RadGrid3.DataSource = dt;
                RadGrid3.DataBind();

            }
        }
        private void BindFAQDetails()
        {

            DataTable dt = onlineOrderBackOfficeBo.GetAdvertisementDetailsWithAssetGroup("FAQ");
            if (dt.Rows.Count >= 0)
            {
                if (Cache["FAQDetailsList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("FAQDetailsList" + userVo.UserId.ToString(), dt);
                }
                else
                {
                    Cache.Remove("FAQDetailsList" + userVo.UserId.ToString());
                    Cache.Insert("FAQDetailsList" + userVo.UserId.ToString(), dt);
                }
                RadGrid4.DataSource = dt;
                RadGrid4.DataBind();

            }
        }
        protected void RadGrid4_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt;
            dt = (DataTable)Cache["FAQDetailsList" + userVo.UserId.ToString()];
            if (dt != null)
            {
                RadGrid4.DataSource = dt;
            }
        }
        protected void RadGrid4_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditFormInsertItem insertItem = e.Item as GridEditFormInsertItem;
            string fileName = string.Empty;
            string assetGroupCode = ((DropDownList)insertItem.FindControl("ddlAssetGroupName1")).SelectedValue;
            int isActive = Convert.ToInt16(((CheckBox)insertItem.FindControl("CheckBox")).Checked);
            FileUpload fileUpload = (FileUpload)insertItem.FindControl("FileUpload");
            string uploadFilePath = ConfigurationManager.AppSettings["BANNER_IMAGE_PATH"].ToString();
            uploadFilePath = Server.MapPath(uploadFilePath);
            if (fileUpload.HasFile)
            {
                if (!Directory.Exists(uploadFilePath))
                {
                    Directory.CreateDirectory(uploadFilePath);
                }
                fileName = fileUpload.FileName.ToString();
                fileUpload.SaveAs(uploadFilePath + fileName);

            }
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(0, assetGroupCode, userVo.UserId, fileName, DateTime.MinValue, 0, isActive, "FAQ");
            BindFAQDetails();
        }
        protected void RadGrid4_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PUHD_Id"].ToString());
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(id, "", 0, "", DateTime.Now, 1, 0, "FAQ");
            BindFAQDetails();
        }

        protected void RadGrid3_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt;
            dt = (DataTable)Cache["DemoVideoDetailsList" + userVo.UserId.ToString()];
            if (dt != null)
            {
                RadGrid3.DataSource = dt;
            }
        }
        protected void RadGrid3_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditFormInsertItem insertItem = e.Item as GridEditFormInsertItem;
            string scrollerText = string.Empty;
            string assetGroupCode = ((DropDownList)insertItem.FindControl("ddlAssetGroupName1")).SelectedValue;
            int isActive = Convert.ToInt16(((CheckBox)insertItem.FindControl("CheckBox")).Checked);
            scrollerText = ((TextBox)insertItem.FindControl("TextBox1")).Text;
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(0, assetGroupCode, userVo.UserId, scrollerText, DateTime.MinValue, 0, isActive, "Demo");
            BindDemoVideoDetails();
        }
        protected void RadGrid3_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PUHD_Id"].ToString());
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(id, "", 0, "", DateTime.Now, 1, 0, "Demo");
            BindDemoVideoDetails();
        }
        protected void RadGrid3_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.EditCommandName)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "SetEditMode", "isEditMode = true;", true);
                this.RadGrid3.MasterTableView.Items[0].Edit = true;
            }

        }
        protected void RadGrid3_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                string scrolltext = (RadGrid3.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PUHD_HelpDetails"]).ToString();
                string assetGroupCode = RadGrid3.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PAG_AssetGroupCode"].ToString();
                int isActive = Convert.ToInt16(RadGrid3.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PUHD_IsActive"]);
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList dropDownList = (DropDownList)editedItem.FindControl("ddlAssetGroupName1");
                dropDownList.SelectedValue = assetGroupCode;
                ((TextBox)editedItem.FindControl("TextBox1")).Text = scrolltext;
                ((CheckBox)editedItem.FindControl("CheckBox")).Checked = isActive == 1;

            }
        }
        protected void RadGrid3_UpdateCommand(object source, GridCommandEventArgs e)
        {


            GridEditableItem editedItem = e.Item as GridEditableItem;
            string scrollerText = ((TextBox)e.Item.FindControl("TextBox1")).Text;
            int id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["PUHD_Id"].ToString());
            string assetGroupCode = ((DropDownList)e.Item.FindControl("ddlAssetGroupName1")).SelectedValue;
            int isActive = Convert.ToInt16(((CheckBox)e.Item.FindControl("CheckBox")).Checked);
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(id, assetGroupCode, userVo.UserId, scrollerText, DateTime.MinValue, 0, isActive, "Demo");

            BindDemoVideoDetails();

        }




        protected void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt;
            dt = (DataTable)Cache["ScrollerDetailsList" + userVo.UserId.ToString()];
            if (dt != null)
            {
                RadGrid2.DataSource = dt;
            }
        }
        protected void RadGrid2_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditFormInsertItem insertItem = e.Item as GridEditFormInsertItem;
            string scrollerText = string.Empty;
            string assetGroupCode = ((DropDownList)insertItem.FindControl("ddlAssetGroupName1")).SelectedValue;
            int isActive = Convert.ToInt16(((CheckBox)insertItem.FindControl("CheckBox")).Checked);
            scrollerText = ((TextBox)insertItem.FindControl("TextBox1")).Text;
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(0, assetGroupCode, userVo.UserId, scrollerText, DateTime.MinValue, 0, isActive, "Scroll");
            BindScrollerDetails();
        }
        protected void RadGrid2_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PUHD_Id"].ToString());
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(id, "", 0, "", DateTime.Now, 1, 0, "Scroll");
            BindScrollerDetails();
        }
        protected void RadGrid2_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.EditCommandName)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "SetEditMode", "isEditMode = true;", true);
                this.RadGrid2.MasterTableView.Items[0].Edit = true;
            }

        }
        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                string scrolltext = (RadGrid2.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PUHD_HelpDetails"]).ToString();
                string assetGroupCode = RadGrid2.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PAG_AssetGroupCode"].ToString();
                int isActive =Convert.ToInt16( RadGrid2.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PUHD_IsActive"]);
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList dropDownList = (DropDownList)editedItem.FindControl("ddlAssetGroupName1");
                dropDownList.SelectedValue = assetGroupCode;
                ((TextBox)editedItem.FindControl("TextBox1")).Text = scrolltext;
                ((CheckBox)editedItem.FindControl("CheckBox")).Checked = isActive==1;
               
            }
        }
        protected void RadGrid2_UpdateCommand(object source, GridCommandEventArgs e)
        {


            GridEditableItem editedItem = e.Item as GridEditableItem;
            string scrollerText = ((TextBox)e.Item.FindControl("TextBox1")).Text;
            int id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["PUHD_Id"].ToString());
            string assetGroupCode = ((DropDownList)e.Item.FindControl("ddlAssetGroupName1")).SelectedValue;
           int isActive=Convert.ToInt16(((CheckBox)e.Item.FindControl("CheckBox")).Checked);
           onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(id, assetGroupCode, userVo.UserId, scrollerText, DateTime.MinValue, 0, isActive,"scroll");

            BindScrollerDetails();

        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                FileUpload upload = (FileUpload)e.Item.FindControl("FileUpload");

            }
        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt;
            dt = (DataTable)Cache["BannerDetailsList" + userVo.UserId.ToString()];
            if (dt != null)
            {
                RadGrid1.DataSource = dt;
            }
        }
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {


            GridEditFormInsertItem insertItem = e.Item as GridEditFormInsertItem;
            string fileName = string.Empty;
            string assetGroupCode = ((DropDownList)insertItem.FindControl("ddlAssetGroupName")).SelectedValue;
            DateTime expireDate = Convert.ToDateTime(((RadDateTimePicker)insertItem.FindControl("dtpExpireDate")).SelectedDate);
            FileUpload fileUpload = (FileUpload)insertItem.FindControl("FileUpload");
            string uploadFilePath = ConfigurationManager.AppSettings["BANNER_IMAGE_PATH"].ToString();
            uploadFilePath = Server.MapPath(uploadFilePath);
            if (fileUpload.HasFile)
            {
                if (!Directory.Exists(uploadFilePath))
                {
                    Directory.CreateDirectory(uploadFilePath);
                }
                fileName = fileUpload.FileName.ToString();
                fileUpload.SaveAs(uploadFilePath + fileName);

            }
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnBannerDetails(0, assetGroupCode, userVo.UserId, fileName, expireDate, 0);
            BindBannerDetails();


        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {


            GridEditableItem editedItem = e.Item as GridEditableItem;
            string fileName = string.Empty;
            int id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["PBD_Id"].ToString());
            string assetGroupCode = ((DropDownList)e.Item.FindControl("ddlAssetGroupName")).SelectedValue;
            DateTime expireDate = Convert.ToDateTime(((RadDateTimePicker)e.Item.FindControl("dtpExpireDate")).SelectedDate);
            FileUpload fileUpload = (FileUpload)e.Item.FindControl("FileUpload");
            string uploadFilePath = ConfigurationManager.AppSettings["BANNER_IMAGE_PATH"].ToString();
            uploadFilePath = Server.MapPath(uploadFilePath);
            if (fileUpload.HasFile)
            {
                if (!Directory.Exists(uploadFilePath))
                {
                    Directory.CreateDirectory(uploadFilePath);
                }
                fileName = fileUpload.FileName.ToString();
                fileUpload.SaveAs(uploadFilePath + fileName);

            }
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnBannerDetails(id, assetGroupCode, userVo.UserId, fileName, expireDate, 0);

            BindBannerDetails();

        }
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PBD_Id"].ToString());
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnBannerDetails(id, "", 0, "", DateTime.Now, 1);
            //retrive entity form the Db
            BindBannerDetails();
        }
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.EditCommandName)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "SetEditMode", "isEditMode = true;", true);
                this.RadGrid1.MasterTableView.Items[0].Edit = true;
            }

        }
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                DateTime expirydate = Convert.ToDateTime(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PDB_ExpiryDate"]);
                string assetGroupCode = RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PAG_AssetGroupCode"].ToString();
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList dropDownList = (DropDownList)editedItem.FindControl("ddlAssetGroupName");
                dropDownList.SelectedValue = assetGroupCode;
                RadDateTimePicker radDateTimePicker = (RadDateTimePicker)editedItem.FindControl("dtpExpireDate");
                radDateTimePicker.SelectedDate = expirydate;
            }
        }

    }
}