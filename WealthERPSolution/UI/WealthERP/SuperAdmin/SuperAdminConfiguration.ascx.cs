using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoHostConfig;
using VoHostConfig;
using VoUser;
using System.IO;

namespace WealthERP.SuperAdmin
{
    public partial class SuperAdminConfiguration : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //uplLogoUpload.TargetFolder = Server.MapPath(@"/Images/");
            //uplLogoUpload.TemporaryFolder = Server.MapPath(@"/Images/");
            if (!IsPostBack)
            {
                GeneralConfigurationBo generalconfigurationbo = new GeneralConfigurationBo();
                GeneralConfigurationVo generalconfigurationvo = new GeneralConfigurationVo();
                UserVo uservo = (UserVo)Session["UserVo"];
                generalconfigurationvo = generalconfigurationbo.GetHostGeneralConfiguration(uservo.UserId);
                if (!String.IsNullOrEmpty(generalconfigurationvo.DefaultTheme))
                {
                    ddlApplyTheme.SelectedValue = generalconfigurationvo.DefaultTheme;
                }
                if (!String.IsNullOrEmpty(generalconfigurationvo.AdviserLogoPlacement))
                {
                    ddlPickAdvisorLogoPosition.SelectedValue = generalconfigurationvo.AdviserLogoPlacement;
                }
                if (!String.IsNullOrEmpty(generalconfigurationvo.HostLogoPlacement))
                {
                    ddlPickHostLogoPosition.SelectedValue = generalconfigurationvo.HostLogoPlacement;
                }
                if (!String.IsNullOrEmpty(generalconfigurationvo.ApplicationName))
                {
                    txtApplicationName.Text = generalconfigurationvo.ApplicationName;
                }
                if (!String.IsNullOrEmpty(generalconfigurationvo.ContactPersonName))
                {
                    txtContactPerson.Text = generalconfigurationvo.ContactPersonName;
                }
                if (generalconfigurationvo.ContactPersonTelephoneNumber !=0)
                {
                    txtTelephoneNo.Text = generalconfigurationvo.ContactPersonTelephoneNumber.ToString();
                }
                if (!String.IsNullOrEmpty(generalconfigurationvo.Email))
                {
                    txtEmailId.Text = generalconfigurationvo.Email;
                }
                if (!String.IsNullOrEmpty(generalconfigurationvo.LoginPageContent))
                {
                    txtLoginPageContent.Text = generalconfigurationvo.LoginPageContent;
                }
                //Binary image needed to be here
                if (!String.IsNullOrEmpty(generalconfigurationvo.HostLogo))
                {
                    lblFileUploaded.Visible = true;
                    lblFileUploaded.Text = generalconfigurationvo.HostLogo;
                    hdnUplFileName.Value = generalconfigurationvo.HostLogo;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                GeneralConfigurationVo generalconfigurationvo = new GeneralConfigurationVo();
                GeneralConfigurationBo generalconfigurationbo = new GeneralConfigurationBo();
                bool recordstatus = true;
                UserVo uservo = (UserVo)Session["UserVo"];
                generalconfigurationvo.AdviserLogoPlacement = ddlPickAdvisorLogoPosition.SelectedValue;
                generalconfigurationvo.ApplicationName = txtApplicationName.Text;
                generalconfigurationvo.ContactPersonName = txtContactPerson.Text;
                if (txtTelephoneNo.Text != string.Empty)
                {
                    generalconfigurationvo.ContactPersonTelephoneNumber = Int64.Parse(txtTelephoneNo.Text);
                }
                generalconfigurationvo.DefaultTheme = ddlApplyTheme.SelectedValue;
                generalconfigurationvo.Email = txtEmailId.Text;
                generalconfigurationvo.HostLogoPlacement = ddlPickHostLogoPosition.SelectedValue;
                generalconfigurationvo.LoginPageContent = txtLoginPageContent.Text;
                //if (Session["UploadFileName"] != null && Session["UploadFileName"] != string.Empty)
                //{
                //    generalconfigurationvo.HostLogo = Session["UploadFileName"].ToString();
                //}
                if (hdnUplFileName.Value != "")
                {
                    generalconfigurationvo.HostLogo = hdnUplFileName.Value;
                }
                recordstatus = generalconfigurationbo.AddHostGeneralConfiguration(uservo.UserId, generalconfigurationvo);
                if (recordstatus)
                {
                    msgRecordStatus.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Something Went Wrong \n Record Status: Unsuccessful');", true);
                }
            }

        }

        protected void uplLogoUpload_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            Session["UploadFileName"] = e.File.FileName;
            //Thumbnail.Width = Unit.Pixel(200);
            //Thumbnail.Height = Unit.Pixel(150);
            ////if (File.Exists(Server.MapPath(@"/Images/" + e.File.FileName)))
            ////{
            ////    Thumbnail.ImageUrl = Server.MapPath(@"/Images/" + e.File.FileName);
            ////}

            //using (Stream stream = e.File.InputStream)
            //{
            //    byte[] imageData = new byte[stream.Length];
            //    stream.Read(imageData, 0, (int)stream.Length);
            //    Thumbnail.DataValue = imageData;
            //}
        }
        
    }
}