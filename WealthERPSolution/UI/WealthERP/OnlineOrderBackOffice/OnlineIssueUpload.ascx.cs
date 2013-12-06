using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using BoOnlineOrderManagement;


namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineIssueUpload : System.Web.UI.UserControl
    {
        OnlineCommonBackOfficeBo boComBackOff;

        protected void Page_Load(object sender, EventArgs e)
        {
            //ddlFileType.Enabled = false;

        }
        protected void Readcsvfile()
        {
            //StreamReader StreamReader=new StreamReader(
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            String savePath = @"c:\IssueUpload\";

            if (FileUpload.HasFile)
            {
                String fileName = FileUpload.FileName;

                savePath += fileName;

                FileUpload.SaveAs(savePath);

                ShowMessage(fileName + "Uploaded");
                if (boComBackOff == null) boComBackOff = new OnlineCommonBackOfficeBo();

                boComBackOff.ReadCsvFile(savePath);
            }
            else
            {
                ShowMessage("Error");
            }
        }

        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mykey", "hide();", true);
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSourceData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
     }
}