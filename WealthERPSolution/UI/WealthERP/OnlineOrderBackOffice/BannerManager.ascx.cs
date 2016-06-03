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
using BoCommon;
using BoProductMaster;
using System.Web.UI.HtmlControls;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class BannerManager : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        OnlineOrderBackOfficeBo onlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        OnlineCommonBackOfficeBo OnlineCommonBackOfficeBo = new OnlineCommonBackOfficeBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                multipageAdsUpload.SelectedIndex = 0;
                BindBannerDetails();
                BindScrollerDetails();
                BindDemoVideoDetails();
                BindFAQDetails();
                BindNotificationSetup();
                BindSchemeRankDetails(adviserVo.advisorId);
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
        private void BindNotificationSetup()
        {
            DataSet ds = onlineOrderBackOfficeBo.BindNotificationSetup(adviserVo.advisorId);
            if (ds.Tables[0].Rows.Count >= 0)
            {
                if (Cache["NotificationSetup" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("NotificationSetup" + userVo.UserId.ToString(), ds.Tables[0]);
                }
                else
                {
                    Cache.Remove("NotificationSetup" + userVo.UserId.ToString());
                    Cache.Insert("NotificationSetup" + userVo.UserId.ToString(), ds.Tables[0]);
                }
                rgNotification.DataSource = ds.Tables[0];
                rgNotification.DataBind();

            }
            //if (ds.Tables[1].Rows.Count >= 0)
            //{
            //    if (Cache["NotificationType" + userVo.UserId.ToString()] == null)
            //    {
            //        Cache.Insert("NotificationType" + userVo.UserId.ToString(), ds.Tables[1]);
            //    }
            //    else
            //    {
            //        Cache.Remove("NotificationType" + userVo.UserId.ToString());
            //        Cache.Insert("NotificationType" + userVo.UserId.ToString(), ds.Tables[1]);
            //    }
            //}
            if (ds.Tables[2].Rows.Count >= 0)
            {
                if (Cache["transTypes" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("transTypes" + userVo.UserId.ToString(), ds.Tables[2]);
                }
                else
                {
                    Cache.Remove("transTypes" + userVo.UserId.ToString());
                    Cache.Insert("transTypes" + userVo.UserId.ToString(), ds.Tables[2]);
                }
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
            string headingText = string.Empty;
            headingText = ((TextBox)insertItem.FindControl("txtFAQHeading")).Text;
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
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(0, assetGroupCode, userVo.UserId, fileName, DateTime.MinValue, 0, isActive, "FAQ", headingText, "FAQ");
            BindFAQDetails();
        }
        protected void RadGrid4_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PUHD_Id"].ToString());
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(id, "", 0, "", DateTime.Now, 1, 0, "FAQ", string.Empty, string.Empty);
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
            string headingText = string.Empty;
            string assetGroupCode = ((DropDownList)insertItem.FindControl("ddlAssetGroupName1")).SelectedValue;
            string formatType = ((DropDownList)insertItem.FindControl("ddlFormatType")).SelectedValue;
            int isActive = Convert.ToInt16(((CheckBox)insertItem.FindControl("CheckBox")).Checked);
            if (formatType == "PDF")
            {
                FileUpload fileUpload = (FileUpload)insertItem.FindControl("VideoFileUpload");
                string uploadFilePath = ConfigurationManager.AppSettings["BANNER_IMAGE_PATH"].ToString();
                uploadFilePath = Server.MapPath(uploadFilePath);
                if (fileUpload.HasFile)
                {
                    if (!Directory.Exists(uploadFilePath))
                    {
                        Directory.CreateDirectory(uploadFilePath);
                    }
                    scrollerText = fileUpload.FileName.ToString();
                    fileUpload.SaveAs(uploadFilePath + scrollerText);

                }
            }
            else if (formatType == "YTL")
            {
                scrollerText = ((TextBox)insertItem.FindControl("TextBox1")).Text;
            }
            headingText = ((TextBox)insertItem.FindControl("txtDemoHeading")).Text;
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(0, assetGroupCode, userVo.UserId, scrollerText, DateTime.MinValue, 0, isActive, "Demo", headingText, formatType);
            BindDemoVideoDetails();
        }
        protected void RadGrid3_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PUHD_Id"].ToString());
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(id, "", 0, "", DateTime.Now, 1, 0, "Demo", string.Empty, "");
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
                string headingText = (RadGrid3.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PUHD_Heading"]).ToString();
                string assetGroupCode = RadGrid3.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PAG_AssetGroupCode"].ToString();
                string formatType = RadGrid3.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PUHD_HelpFormatType"].ToString();
                int isActive = Convert.ToInt16(RadGrid3.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PUHD_IsActive"]);
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList dropDownList = (DropDownList)editedItem.FindControl("ddlAssetGroupName1");
                DropDownList ddlFormatType = (DropDownList)editedItem.FindControl("ddlFormatType");
                dropDownList.SelectedValue = assetGroupCode;
                ddlFormatType.SelectedValue = formatType;
                ((TextBox)editedItem.FindControl("TextBox1")).Text = scrolltext;
                ((TextBox)editedItem.FindControl("txtDemoHeading")).Text = headingText;
                ((CheckBox)editedItem.FindControl("CheckBox")).Checked = isActive == 1;
                HtmlTableRow tr6;
                HtmlTableRow tr2;
                tr6 = (HtmlTableRow)editedItem.FindControl("tr6");
                tr2 = (HtmlTableRow)editedItem.FindControl("tr2");
                if (formatType == "YTL")
                {
                    tr2.Visible = true;

                }
                else if (formatType == "PDF")
                {
                    tr6.Visible = true;

                }
            }
        }
        protected void RadGrid3_UpdateCommand(object source, GridCommandEventArgs e)
        {


            GridEditableItem editedItem = e.Item as GridEditableItem;
            string scrollerText = string.Empty;
            string headingText = ((TextBox)e.Item.FindControl("txtDemoHeading")).Text;
            int id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["PUHD_Id"].ToString());
            string assetGroupCode = ((DropDownList)e.Item.FindControl("ddlAssetGroupName1")).SelectedValue;
            int isActive = Convert.ToInt16(((CheckBox)e.Item.FindControl("CheckBox")).Checked);
            string formatType = ((DropDownList)e.Item.FindControl("ddlFormatType")).SelectedValue;
            if (formatType == "PDF")
            {
                FileUpload fileUpload = (FileUpload)e.Item.FindControl("VideoFileUpload");
                string uploadFilePath = ConfigurationManager.AppSettings["BANNER_IMAGE_PATH"].ToString();
                uploadFilePath = Server.MapPath(uploadFilePath);
                if (fileUpload.HasFile)
                {
                    if (!Directory.Exists(uploadFilePath))
                    {
                        Directory.CreateDirectory(uploadFilePath);
                    }
                    scrollerText = fileUpload.FileName.ToString();
                    fileUpload.SaveAs(uploadFilePath + scrollerText);

                }
            }
            else if (formatType == "YTL")
            {
                scrollerText = ((TextBox)e.Item.FindControl("TextBox1")).Text;
            }
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(id, assetGroupCode, userVo.UserId, scrollerText, DateTime.MinValue, 0, isActive, "Demo", headingText, formatType);

            BindDemoVideoDetails();

        }

        protected void ddlFormatType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlFormatType = (DropDownList)sender;
            HtmlTableRow tr6;
            HtmlTableRow tr2;
            if (ddlFormatType.NamingContainer is Telerik.Web.UI.GridEditFormItem)
            {
                GridEditFormItem gdi;

                gdi = (GridEditFormItem)ddlFormatType.NamingContainer;
                tr6 = (HtmlTableRow)gdi.FindControl("tr6");
                tr2 = (HtmlTableRow)gdi.FindControl("tr2");
                if (ddlFormatType.SelectedValue == "YTL")
                {
                    tr2.Visible = true;
                    tr6.Visible = false;
                }
                else if (ddlFormatType.SelectedValue == "PDF")
                {
                    tr6.Visible = true;
                    tr2.Visible = false;
                }
                else
                {
                    tr6.Visible = false;
                    tr2.Visible = false;
                }
            }
            else if (ddlFormatType.NamingContainer is Telerik.Web.UI.GridEditFormInsertItem)
            {
                GridEditFormInsertItem gdi;
                gdi = (GridEditFormInsertItem)ddlFormatType.NamingContainer;
                tr6 = (HtmlTableRow)gdi.FindControl("tr6");
                tr2 = (HtmlTableRow)gdi.FindControl("tr2");
                if (ddlFormatType.SelectedValue == "YTL")
                {
                    tr2.Visible = true;
                    tr6.Visible = false;
                }
                else if (ddlFormatType.SelectedValue == "PDF")
                {
                    tr6.Visible = true;
                    tr2.Visible = false;
                }
                else
                {
                    tr6.Visible = false;
                    tr2.Visible = false;
                }
            }

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
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(0, assetGroupCode, userVo.UserId, scrollerText, DateTime.MinValue, 0, isActive, "Scroll", string.Empty, "TEXT");
            BindScrollerDetails();
        }
        protected void RadGrid2_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PUHD_Id"].ToString());
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(id, "", 0, "", DateTime.Now, 1, 0, "Scroll", string.Empty, string.Empty);
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
                int isActive = Convert.ToInt16(RadGrid2.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PUHD_IsActive"]);
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList dropDownList = (DropDownList)editedItem.FindControl("ddlAssetGroupName1");
                dropDownList.SelectedValue = assetGroupCode;
                ((TextBox)editedItem.FindControl("TextBox1")).Text = scrolltext;
                ((CheckBox)editedItem.FindControl("CheckBox")).Checked = isActive == 1;

            }
        }
        protected void RadGrid2_UpdateCommand(object source, GridCommandEventArgs e)
        {


            GridEditableItem editedItem = e.Item as GridEditableItem;
            string scrollerText = ((TextBox)e.Item.FindControl("TextBox1")).Text;
            int id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["PUHD_Id"].ToString());
            string assetGroupCode = ((DropDownList)e.Item.FindControl("ddlAssetGroupName1")).SelectedValue;
            int isActive = Convert.ToInt16(((CheckBox)e.Item.FindControl("CheckBox")).Checked);
            onlineOrderBackOfficeBo.InsertUpdateDeleteOnAdvertisementDetails(id, assetGroupCode, userVo.UserId, scrollerText, DateTime.MinValue, 0, isActive, "scroll", string.Empty, "TEXT");

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
        private void BindNotificationType(DropDownList dl, string assetgroup)
        {
            DataTable dt = new DataTable();
            DataSet ds = onlineOrderBackOfficeBo.GetNotificationTypes(assetgroup);
            dl.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Cache["NotificationType" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("NotificationType" + userVo.UserId.ToString(), ds.Tables[0]);
                }
                else
                {
                    Cache.Remove("NotificationType" + userVo.UserId.ToString());
                    Cache.Insert("NotificationType" + userVo.UserId.ToString(), ds.Tables[0]);
                }
                dl.DataSource = ds;
                dl.DataTextField = ds.Tables[0].Columns["CNT_NotificationType"].ToString();
                dl.DataValueField = ds.Tables[0].Columns["CNT_ID"].ToString();
                dl.DataBind();

            }
            dl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }
        private void BindtransactionTypes(CheckBoxList rlb, string NotificationType)
        {
            DataRow[] dt;
            DataTable dttransTypes = new DataTable();
            dttransTypes = (DataTable)Cache["transTypes" + userVo.UserId.ToString()];
            dt = dttransTypes.Select("CNT_ID=" + NotificationType);
            DataTable dt_Temp = dttransTypes.Clone();
            foreach (DataRow dr in dt) { dt_Temp.ImportRow(dr); }
            rlb.Items.Clear();
            rlb.DataSource = dt_Temp;
            rlb.DataTextField = "TransTypeName";
            rlb.DataValueField = "TransTypeCode";
            rlb.DataBind();
        }
        protected void NotificationType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList NotificationType = (DropDownList)sender;
            CheckBoxList rlb = new CheckBoxList();
            if (NotificationType.NamingContainer is Telerik.Web.UI.GridEditFormItem)
            {
                GridEditFormItem gdi;
                gdi = (GridEditFormItem)NotificationType.NamingContainer;
                rlb = (CheckBoxList)gdi.FindControl("chkbltranstype");
                NotificationType = (DropDownList)gdi.FindControl("DropDownList1");
                DropDownList assetgroup = new DropDownList();
                assetgroup = (DropDownList)gdi.FindControl("ddlAssetGroupName1");
                Label Label7 = new Label();
                TextBox txtPriorDays = new TextBox();
                RequiredFieldValidator Requiredfieldvalidator9 = new RequiredFieldValidator();
                Label7 = (Label)gdi.FindControl("Label7");
                txtPriorDays = (TextBox)gdi.FindControl("txtPriorDays");
                Requiredfieldvalidator9 = (RequiredFieldValidator)gdi.FindControl("Requiredfieldvalidator9");
                if (assetgroup.SelectedValue == "MF")
                {
                    if (gdi.IsInEditMode == true)
                    {
                        BindtransactionTypes(rlb, NotificationType.SelectedValue);
                    }
                    else
                    {

                    }
                }
                DataTable dt = new DataTable();
                dt = (DataTable)Cache["NotificationType" + userVo.UserId.ToString()];
                 DataRow[] foundRows=dt.Select("CNT_ID="+   NotificationType.SelectedValue);
                 if (foundRows.Length >0&&foundRows[0]["CNT_Code"].ToString().ToUpper() == "REMINDER")
                {
                    txtPriorDays.Visible = true;
                    Label7.Visible = true;
                    Requiredfieldvalidator9.Enabled = true;
                }
                else
                {
                    txtPriorDays.Visible = false;
                    Label7.Visible = false;
                    Requiredfieldvalidator9.Enabled = false;
                }
            }

        }
        protected void ddlAssetGroupName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList assetgroup = (DropDownList)sender;
            CheckBoxList rlb = new CheckBoxList();
            if (assetgroup.NamingContainer is Telerik.Web.UI.GridEditFormItem)
            {
                GridEditFormItem gdi;
                gdi = (GridEditFormItem)assetgroup.NamingContainer;

                assetgroup = (DropDownList)gdi.FindControl("ddlAssetGroupName1");
                DropDownList NotificationType = new DropDownList();
                NotificationType = (DropDownList)gdi.FindControl("DropDownList1");
                HtmlTableRow trTransType = (HtmlTableRow)gdi.FindControl("trTransType");
                BindNotificationType(NotificationType, assetgroup.SelectedValue);
                if (assetgroup.SelectedValue == "MF")
                {
                    trTransType.Visible = true;
                }
                else
                {
                    trTransType.Visible = false;
                }
            }
        }
        protected void rgNotification_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt;
            dt = (DataTable)Cache["NotificationSetup" + userVo.UserId.ToString()];
            if (dt != null)
            {
                rgNotification.DataSource = dt;
            }
        }
        protected void rgNotification_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditFormInsertItem insertItem = e.Item as GridEditFormInsertItem;

            string headingText = string.Empty;
            int notificationType;
            int PriorDays = 0;
            bool isSMSEnabled = false;
            bool isEMailEnabled = false;
            headingText = ((TextBox)insertItem.FindControl("txtNotificationHeading")).Text;
            string assetGroupCode = ((DropDownList)insertItem.FindControl("ddlAssetGroupName1")).SelectedValue;
            notificationType = Convert.ToInt32(((DropDownList)insertItem.FindControl("DropDownList1")).SelectedValue);
            PriorDays = Convert.ToInt32(string.IsNullOrEmpty(((TextBox)insertItem.FindControl("txtPriorDays")).Text)?"0":((TextBox)insertItem.FindControl("txtPriorDays")).Text);

            string transtypes = string.Empty;
            foreach (ListItem li in ((CheckBoxList)e.Item.FindControl("chkbltranstype")).Items)
            {
                if (li.Selected == true)
                    transtypes += li.Value + ",";
            }
            isSMSEnabled = ((CheckBox)insertItem.FindControl("chkSMS")).Checked;
            isEMailEnabled = ((CheckBox)insertItem.FindControl("chkEmail")).Checked;


            onlineOrderBackOfficeBo.InsertUpdateDeleteNotificationSetupDetails(0, userVo.UserId, adviserVo.advisorId, assetGroupCode, notificationType, transtypes, headingText, PriorDays, isSMSEnabled, isEMailEnabled, false);
            BindNotificationSetup();
        }
        protected void rgNotification_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["CTNS_Id"].ToString());
            onlineOrderBackOfficeBo.InsertUpdateDeleteNotificationSetupDetails(id, userVo.UserId, adviserVo.advisorId, string.Empty, 0, string.Empty, string.Empty, 0, false, false, false);
            //retrive entity form the Db
            BindNotificationSetup();
        }
        protected void rgNotification_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.EditCommandName)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "SetEditMode", "isEditMode = true;", true);
                this.rgNotification.MasterTableView.Items[0].Edit = true;
            }
        }
        protected void rgNotification_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {

                string assetGroupCode = rgNotification.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PAG_AssetGroupCode"].ToString();
                string NotificationHeading = rgNotification.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CTNS_NotificationHeader"].ToString();
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList dropDownList1 = (DropDownList)editedItem.FindControl("DropDownList1");
                DropDownList ddlAssetGroupName1 = (DropDownList)editedItem.FindControl("ddlAssetGroupName1");
                TextBox txtNotificationHeading = (TextBox)editedItem.FindControl("txtNotificationHeading");
                CheckBoxList chkbltranstype = (CheckBoxList)editedItem.FindControl("chkbltranstype");
                CheckBox chkSMS = (CheckBox)editedItem.FindControl("chkSMS");
                CheckBox chkEmail = (CheckBox)editedItem.FindControl("chkEmail");
                chkSMS.Checked = rgNotification.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CTNS_IsSMSEnabled"].ToString() == "True";
                chkEmail.Checked = rgNotification.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CTNS_ISEmailEnabled"].ToString() == "True";
                ddlAssetGroupName1.SelectedValue = assetGroupCode;
                txtNotificationHeading.Text = NotificationHeading;
                BindNotificationType(dropDownList1, assetGroupCode);
                dropDownList1.SelectedValue = rgNotification.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CNT_ID"].ToString();
                BindtransactionTypes(chkbltranstype, dropDownList1.SelectedValue);
                string transtypes = rgNotification.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CTNS_TransactionTypes"].ToString();
                string[] transtype;
                if (string.IsNullOrEmpty(transtypes))
                    return;
                else
                    transtype = transtypes.Split(',');

                for (int i = 0; i < transtype.Length; i++)
                {
                    foreach (ListItem li in chkbltranstype.Items)
                    {
                        if (li.Value == transtype[i])
                        {
                            li.Selected = true;
                        }


                    }
                }
                Label Label7 = new Label();
                TextBox txtPriorDays = new TextBox();
                RequiredFieldValidator Requiredfieldvalidator9 = new RequiredFieldValidator();
                Label7 = (Label)editedItem.FindControl("Label7");
                txtPriorDays = (TextBox)editedItem.FindControl("txtPriorDays");
                Requiredfieldvalidator9 = (RequiredFieldValidator)editedItem.FindControl("Requiredfieldvalidator9");
                DataTable dt = new DataTable();
                dt = (DataTable)Cache["NotificationType" + userVo.UserId.ToString()];
                DataRow[] foundRows = dt.Select("CNT_ID=" + dropDownList1.SelectedValue);
                if (foundRows.Length > 0 && foundRows[0]["CNT_Code"].ToString().ToUpper() == "REMINDER")
                {
                    txtPriorDays.Visible = true;
                    Label7.Visible = true;
                    Requiredfieldvalidator9.Enabled = true;
                }
                else
                {
                    txtPriorDays.Visible = false;
                    Label7.Visible = false;
                    Requiredfieldvalidator9.Enabled = false;
                }
            }
            if ((e.Item is GridEditFormInsertItem) && (e.Item.OwnerTableView.IsItemInserted))
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList DropDownList1 = (DropDownList)gefi.FindControl("DropDownList1");

                //BindNotificationType(DropDownList1);


            }
        }
        protected void rgNotification_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string scrollerText = string.Empty;
            string headingText = ((TextBox)e.Item.FindControl("txtNotificationHeading")).Text;
            int id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["CTNS_Id"].ToString());
            string assetGroupCode = ((DropDownList)e.Item.FindControl("ddlAssetGroupName1")).SelectedValue;

            int notificationType = Convert.ToInt32(((DropDownList)e.Item.FindControl("DropDownList1")).SelectedValue);
            int priorDays = Convert.ToInt32(((TextBox)e.Item.FindControl("txtPriorDays")).Text);
            string transtypes = string.Empty;
            foreach (ListItem li in ((CheckBoxList)e.Item.FindControl("chkbltranstype")).Items)
            {
                if (li.Selected == true)
                    transtypes += li.Value + ",";
            }
            bool isSMSEnabled = ((CheckBox)e.Item.FindControl("chkSMS")).Checked;
            bool isEmailEnabled = ((CheckBox)e.Item.FindControl("chkEmail")).Checked;
            onlineOrderBackOfficeBo.InsertUpdateDeleteNotificationSetupDetails(id, userVo.UserId, adviserVo.advisorId, assetGroupCode, notificationType, transtypes, headingText, priorDays, isSMSEnabled, isEmailEnabled, true);
            BindNotificationSetup();
        }
        protected void rgNotification_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                LinkButton EditLinkEmail = (LinkButton)e.Item.FindControl("EditLinkEmail");
                EditLinkEmail.OnClientClick = String.Format("return ShowEditForm('{0}','{1}');", "?setupId=" + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CTNS_Id"] + "&FormatType=Email", e.Item.ItemIndex);
                LinkButton EditLinkSMS = (LinkButton)e.Item.FindControl("EditLinkSMS");
                EditLinkSMS.OnClientClick = String.Format("return ShowEditForm('{0}','{1}');", "?setupId=" + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CTNS_Id"] + "&FormatType=SMS", e.Item.ItemIndex);
            }
        }
        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                RadGrid1.MasterTableView.SortExpressions.Clear();
                RadGrid1.MasterTableView.GroupByExpressions.Clear();
                RadGrid1.Rebind();
            }
            else if (e.Argument == "RebindAndNavigate")
            {
                RadGrid1.MasterTableView.SortExpressions.Clear();
                RadGrid1.MasterTableView.GroupByExpressions.Clear();
                RadGrid1.MasterTableView.CurrentPageIndex = RadGrid1.MasterTableView.PageCount - 1;
                RadGrid1.Rebind();
            }
        }


        protected void rgSchemeRanking_ItemDataBound(object sender, GridItemEventArgs e)
        {


            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode) && e.Item.ItemIndex != -1)
            {
                GridEditFormItem editform = (GridEditFormItem)e.Item;
                DropDownList ddlAMC = (DropDownList)editform.FindControl("ddlAMC");
                DropDownList ddlCategory = (DropDownList)editform.FindControl("ddlCategory");
                DropDownList ddlSchemeRank = (DropDownList)editform.FindControl("ddlSchemeRank");
                DropDownList ddlScheme = (DropDownList)editform.FindControl("ddlScheme");
                BindAMC(ddlAMC);
                BindCategory(ddlCategory);
                int schemePlanCode = Convert.ToInt32(rgSchemeRanking.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PASP_SchemePlanCode"].ToString());
                string Category = rgSchemeRanking.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PAIC_AssetInstrumentCategoryCode"].ToString();
                int AmcCode = Convert.ToInt32(rgSchemeRanking.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PA_AMCCode"].ToString());
                int schemeRank = Convert.ToInt32(rgSchemeRanking.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AMFSR_SchemeRank"].ToString());
                BindScheme(AmcCode, Category, ddlScheme, true);
                ddlScheme.SelectedValue = schemePlanCode.ToString();
                ddlAMC.SelectedValue = AmcCode.ToString();
                ddlCategory.SelectedValue = Category.ToString();
                BindSchemeRank(ddlSchemeRank, Category, false);
                ddlSchemeRank.SelectedValue = schemeRank.ToString();
                ddlScheme.SelectedValue = schemePlanCode.ToString();


            }
            if ((e.Item is GridEditFormInsertItem) && (e.Item.OwnerTableView.IsItemInserted))
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlAMC = (DropDownList)gefi.FindControl("ddlAMC");
                DropDownList ddlCategory = (DropDownList)gefi.FindControl("ddlCategory");
                DropDownList ddlSchemeRank = (DropDownList)gefi.FindControl("ddlSchemeRank");
                DropDownList ddlScheme = (DropDownList)gefi.FindControl("ddlScheme");
                BindAMC(ddlAMC);
                BindCategory(ddlCategory);

            }



        }
        protected void BindSchemeRankDetails(int adviserId)
        {
            OnlineCommonBackOfficeBo OnlineCommonBackOfficeBo = new OnlineCommonBackOfficeBo();
            DataTable dtBindSchemeRelatedDetails = OnlineCommonBackOfficeBo.GetMFSchemeRanking(adviserId);
            if (Cache["BindSchemeRankDetails" + userVo.UserId] != null)
            {
                Cache.Remove("BindSchemeRankDetails" + userVo.UserId);
            }
            Cache.Insert("BindSchemeRankDetails" + userVo.UserId, dtBindSchemeRelatedDetails);
            rgSchemeRanking.DataSource = dtBindSchemeRelatedDetails;
            rgSchemeRanking.DataBind();
            rgSchemeRanking.Visible = true;

        }


        private void BindScheme(int amcCode, string category, DropDownList ddlScheme, Boolean IsEdit)
        {
            DataTable dt;
            OnlineCommonBackOfficeBo OnlineCommonBackOfficeBo = new OnlineCommonBackOfficeBo();
            dt = OnlineCommonBackOfficeBo.GetSchemeForRank(adviserVo.advisorId, amcCode, category, IsEdit);
            ddlScheme.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                ddlScheme.DataSource = dt;
                ddlScheme.DataValueField = "PASP_SchemePlanCode";
                ddlScheme.DataTextField = "PASP_SchemePlanName";
                ddlScheme.DataBind();

            }
            ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }
        private void BindCategory(DropDownList ddlCategory)
        {
            DataSet dsProductAssetCategory;
            ProductMFBo productMFBo = new ProductMFBo();
            dsProductAssetCategory = productMFBo.GetProductAssetCategory();
            DataTable dtCategory = dsProductAssetCategory.Tables[0];
            ddlCategory.DataSource = dtCategory;
            ddlCategory.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Category", "0"));
        }

        private void BindAMC(DropDownList ddlAMC)
        {

            DataTable dtGetAMCList = new DataTable();
            CommonLookupBo commonLookupBo = new CommonLookupBo();
            dtGetAMCList = commonLookupBo.GetProdAmc(0, true);
            ddlAMC.DataSource = dtGetAMCList;
            ddlAMC.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
            ddlAMC.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
            ddlAMC.DataBind();
            ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select AMC", "0"));
        }
        protected void rgSchemeRanking_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataTable dtMap = new DataTable();
            if (Cache["BindSchemeRankDetails" + userVo.UserId.ToString()] != null)
            {
                dtMap = (DataTable)Cache["BindSchemeRankDetails" + userVo.UserId.ToString()];
                rgSchemeRanking.DataSource = dtMap;
            }




        }

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCategory = (DropDownList)sender;
            DropDownList ddlAmcCode = new DropDownList();
            DropDownList ddlScheme = new DropDownList();
            DropDownList ddlSchemeRank = new DropDownList();
            if (ddlCategory.NamingContainer is Telerik.Web.UI.GridEditFormItem)
            {
                GridEditFormItem gdi;
                gdi = (GridEditFormItem)ddlCategory.NamingContainer;
                ddlAmcCode = (DropDownList)gdi.FindControl("ddlAMC");
                ddlScheme = (DropDownList)gdi.FindControl("ddlScheme");
                ddlSchemeRank = (DropDownList)gdi.FindControl("ddlSchemeRank");
                ddlCategory = (DropDownList)gdi.FindControl("ddlCategory");

                if (gdi.IsInEditMode == true)
                {
                    BindSchemeRank(ddlSchemeRank, ddlCategory.SelectedValue, true);
                    BindScheme(int.Parse(ddlAmcCode.SelectedValue), ddlCategory.SelectedValue, ddlScheme, true);
                }
                else
                {
                    BindSchemeRank(ddlSchemeRank, ddlCategory.SelectedValue, false);
                    BindScheme(int.Parse(ddlAmcCode.SelectedValue), ddlCategory.SelectedValue, ddlScheme, false);
                }
                //(e.Item is GridEditFormItem) && (e.Item.IsInEditMode)

            }

        }
        private void BindSchemeRank(DropDownList ddlSchemeRank, string ddlCategory, Boolean IsInsert)
        {
            DataTable dtRank = new DataTable();
            if (IsInsert)
            {
                dtRank = OnlineCommonBackOfficeBo.GetCategorySchemeRank(ddlCategory, adviserVo.advisorId);
                ddlSchemeRank.DataSource = dtRank;
                ddlSchemeRank.DataTextField = dtRank.Columns["RowNumber"].ToString();
                ddlSchemeRank.DataValueField = dtRank.Columns["RowNumber"].ToString();
                ddlSchemeRank.DataBind();
                ddlSchemeRank.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Rank", "0"));
            }
            else
            {
                dtRank.Columns.Add("RowNumber");

                for (int i = 1; i <= 10; i++)
                {
                    DataRow dr = dtRank.NewRow();
                    dr["RowNumber"] = i.ToString();
                    dtRank.Rows.Add(dr);
                }
                ddlSchemeRank.DataSource = dtRank;
                ddlSchemeRank.DataTextField = dtRank.Columns["RowNumber"].ToString();
                ddlSchemeRank.DataValueField = dtRank.Columns["RowNumber"].ToString();
                ddlSchemeRank.DataBind();
                ddlSchemeRank.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Rank", "0"));


            }


        }
        protected void rgSchemeRanking_OnDeleteCommand(object source, GridCommandEventArgs e)
        {
            int AMFSR_Id = Convert.ToInt32(rgSchemeRanking.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AMFSR_Id"].ToString());
            OnlineCommonBackOfficeBo.CUDSchemeRanking(adviserVo.advisorId, 0, 0, null, 0, 3, AMFSR_Id);
            BindSchemeRankDetails(adviserVo.advisorId);
        }
        protected void rgSchemeRanking_OnUpdateCommand(object source, GridCommandEventArgs e)
        {
            DropDownList ddlAMC = (DropDownList)e.Item.FindControl("ddlAMC");
            DropDownList ddlCategory = (DropDownList)e.Item.FindControl("ddlCategory");
            DropDownList ddlScheme = (DropDownList)e.Item.FindControl("ddlScheme");
            DropDownList ddlSchemeRank = (DropDownList)e.Item.FindControl("ddlSchemeRank");
            int AMFSR_Id = Convert.ToInt32(rgSchemeRanking.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AMFSR_Id"].ToString());
            OnlineCommonBackOfficeBo.CUDSchemeRanking(adviserVo.advisorId, int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), ddlCategory.SelectedValue, int.Parse(ddlSchemeRank.SelectedValue), 2, AMFSR_Id);
            BindSchemeRankDetails(adviserVo.advisorId);

        }
        protected void rgSchemeRanking_OnInsertCommand(object source, GridCommandEventArgs e)
        {
            DropDownList ddlAMC = (DropDownList)e.Item.FindControl("ddlAMC");
            DropDownList ddlCategory = (DropDownList)e.Item.FindControl("ddlCategory");
            DropDownList ddlScheme = (DropDownList)e.Item.FindControl("ddlScheme");
            DropDownList ddlSchemeRank = (DropDownList)e.Item.FindControl("ddlSchemeRank");

            OnlineCommonBackOfficeBo.CUDSchemeRanking(adviserVo.advisorId, int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), ddlCategory.SelectedValue, int.Parse(ddlSchemeRank.SelectedValue), 1, 0);
            BindSchemeRankDetails(adviserVo.advisorId);
        }


    }
}