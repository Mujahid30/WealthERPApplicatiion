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
                BindBannerDetails();
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