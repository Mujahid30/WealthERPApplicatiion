using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using VoWerpAdmin;
using BoWerpAdmin;
using WealthERP.Base;
using WealthERP;
using VoUser;
using BoCommon;

namespace WealthERP.Admin
{
    public partial class Upload : System.Web.UI.UserControl
    {
        
        protected void OnClick_Upload(object sender, EventArgs e)
        {
            UploadBo uploadBo = new UploadBo();
            UserVo userVo = (UserVo)Session["UserVo"];
            
            UploadType uploadType;
            AssetGroupType assetGroupType;

            uploadType = (UploadType)(Convert.ToInt16(ddlUploadType.SelectedValue));
            assetGroupType = (AssetGroupType)(Convert.ToInt16(ddlAssetGroup.SelectedValue));

            uploadBo.Upload_Xml_Folder =  Server.MapPath(uploadBo.Upload_Xml_Folder);
            uploadBo.currentUserId = userVo.UserId;
            uploadBo.Upload(uploadType, assetGroupType);

            divUploadReport.Visible = true;

            if(uploadBo.isLatestDataAvailable)
                divUploadReport.InnerHtml = "<span style='color:red'><b>The snapshot table contains latest data from Download</b></span>";
            else
                divUploadReport.InnerHtml = uploadBo.StatusMessage.ToString();
           
        }

        protected void btnRejectedRecords_Click(object sender, EventArgs e)
        {

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('','?type=" + ddl + "');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdminUploadReject','?processId=" + ddlAssetGroup.SelectedValue + "');", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdminUploadProcessLog','?processId=" + ddlAssetGroup.SelectedValue + "');", true); 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
        }

    }
}