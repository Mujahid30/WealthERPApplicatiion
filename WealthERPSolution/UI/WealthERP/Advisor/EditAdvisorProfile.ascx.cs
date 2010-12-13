using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoAdvisorProfiling;
using BoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;
using System.Data;
using System.Configuration;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;


namespace WealthERP.Advisor
{
    public partial class EditAdvisorProfile : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserBo userBo = new UserBo();
        AdvisorBo advisorBo = new AdvisorBo();

        DataTable dt = new DataTable();
        DataTable dtStates = new DataTable();
        RMVo rmVo = new RMVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();

        System.Drawing.Image thumbnail_image = null;
        System.Drawing.Image original_image = null;
        System.Drawing.Bitmap final_image = null;
        System.Drawing.Graphics graphic = null;
        MemoryStream ms = null;

        string path;
        string UploadImagePath;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();

                userVo = (UserVo)Session["UserVo"];
                advisorVo = advisorBo.GetAdvisorUser(userVo.UserId);
                if (Session["advisorVo"] != null && Session["rmVo"] != null && userVo.UserType == "SuperAdmin")
                {
                    advisorVo = (AdvisorVo)Session["advisorVo"];
                    rmVo = (RMVo)Session["rmVo"];
                }
                else
                {
                    advisorVo = advisorBo.GetAdvisorUser(userVo.UserId);
                    rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                }
                //RegularExpressionValidator3.Controls.Add(
                if (!IsPostBack)
                {
                    lblSTD.CssClass = "FieldName";
                    lblEmail.CssClass = "FieldName";
                    lblISD.CssClass = "FieldName";
                    lblLine1.CssClass = "FieldName";
                    lblOrganizationName.CssClass = "FieldName";
                    lblPhoneNUmber.CssClass = "FieldName";
                    lblPincode.CssClass = "FieldName";
                    lblFax.CssClass = "FieldName";

                    BindDropDowns(path);
                    if (advisorVo.BusinessCode != null)
                        ddlBusinessType.SelectedValue = advisorVo.BusinessCode.ToString();

                    if (advisorVo.Associates == 1)
                    {
                        rbtnAssModelTypeYes.Checked = true;
                        rbtnAssModelTypeNo.Checked = false;
                    }
                    else
                    {
                        rbtnAssModelTypeYes.Checked = false;
                        rbtnAssModelTypeNo.Checked = true;
                    }

                    if (advisorVo.MultiBranch == 1)
                    {
                        rbtnYes.Checked = true;
                        rbtnNo.Checked = false;
                    }
                    else
                    {
                        rbtnNo.Checked = true;
                        rbtnYes.Checked = false;
                    }
                    if (advisorVo.AddressLine1 != null)
                        txtAddressLine1.Text = advisorVo.AddressLine1.ToString();
                    if (advisorVo.AddressLine2 != null)
                        txtAddressLine2.Text = advisorVo.AddressLine2.ToString();
                    if (advisorVo.AddressLine3 != null)
                        txtAddressLine3.Text = advisorVo.AddressLine3.ToString();
                    if (advisorVo.Website != null)
                        txtwebsite.Text = advisorVo.Website.ToString();
                    txtEmail.Text = advisorVo.Email.ToString();

                    if (advisorVo.Fax != 0)
                        txtFax.Text = advisorVo.Fax.ToString();

                    if (advisorVo.FaxIsd != 0)
                        txtFaxISD.Text = advisorVo.FaxIsd.ToString();

                    if (advisorVo.FaxStd != 0)
                        txtFaxSTD.Text = advisorVo.FaxStd.ToString();

                    if (advisorVo.ContactPersonFirstName != null)
                        txtFirstName.Text = advisorVo.ContactPersonFirstName.ToString();

                    if (advisorVo.Phone1Isd != 0)
                        txtISD1.Text = advisorVo.Phone1Isd.ToString();
                    if (advisorVo.Phone2Isd != 0)
                        txtISD2.Text = advisorVo.Phone2Isd.ToString();
                    if (advisorVo.ContactPersonLastName != null)
                        txtLastName.Text = advisorVo.ContactPersonLastName.ToString();
                    if (advisorVo.ContactPersonMiddleName != null)
                        txtMiddleName.Text = advisorVo.ContactPersonMiddleName.ToString();
                    if (advisorVo.MobileNumber != 0)
                        txtMobileNumber.Text = advisorVo.MobileNumber.ToString();
                    if (advisorVo.Phone1Number != 0)
                        txtPhoneNumber1.Text = advisorVo.Phone1Number.ToString();
                    if (advisorVo.Phone2Number != 0)
                        txtPhoneNumber2.Text = advisorVo.Phone2Number.ToString();
                    if (advisorVo.PinCode != 0)
                        txtPinCode.Text = advisorVo.PinCode.ToString();
                    if (advisorVo.Phone1Std != 0)
                        txtSTD1.Text = advisorVo.Phone1Std.ToString();
                    if (advisorVo.Phone2Std != 0)
                        txtSTD2.Text = advisorVo.Phone2Std.ToString();

                    txtOrganizationName.Text = advisorVo.OrganizationName.ToString();

                    txtCity.Text = advisorVo.City.ToString();
                    if (advisorVo.Country != null)
                        txtCountry.Text=advisorVo.Country.ToString();
                    if (advisorVo.State != null)
                        ddlState.SelectedValue = advisorVo.State.ToString();

                    if (!string.IsNullOrEmpty(advisorVo.LogoPath))
                    {
                        lnklogoChange.Text = "Click to change Logo";
                    }
                    else
                    {
                        lnklogoChange.Text = "Click to upload Logo";
 
                    }

                    if (advisorVo.Designation != null)
                        textDesignation.Text = advisorVo.Designation.ToString();
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

                FunctionInfo.Add("Method", "EditAdvisorProfile.ascx:Page_Load()");

                object[] objects = new object[4];

                objects[0] = advisorBo;
                objects[1] = advisorVo;
                objects[2] = userVo;
                objects[3] = userBo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindDropDowns(string path)
        {
            // Bind Business Type Drop Downs
            dt = XMLBo.GetBusinessType(path);
            ddlBusinessType.DataSource = dt;
            ddlBusinessType.DataTextField = "BusinessType";
            ddlBusinessType.DataValueField = "BusinessTypeCode";
            ddlBusinessType.DataBind();
            ddlBusinessType.Items.Insert(0, new ListItem("Select a Business Type", "Select a Business Type"));


            dtStates = XMLBo.GetStates(path);
            ddlState.DataSource = dtStates;
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StateCode";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("Select a State", "Select a State"));

        }

        public bool chkAvailability()
        {
            bool result = false;
            string id;
            try
            {
                id = txtEmail.Text;
                result = userBo.ChkAvailability(id);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditAdvisorProfile.ascx:chkAvailability()");


                object[] objects = new object[1];
                objects[0] = result;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public bool ChkMailId(string email)
        {
            bool bResult = false;
            try
            {
                if (email == null)
                {
                    bResult = false;
                }
                int nFirstAT = email.IndexOf('@');
                int nLastAT = email.LastIndexOf('@');

                if ((nFirstAT > 0) && (nLastAT == nFirstAT) && (nFirstAT < (email.Length - 1)))
                {

                    bResult = true;
                }
                else
                {

                    bResult = false;
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

                FunctionInfo.Add("Method", "EditAdvisorProfile.ascx:ChkMailId()");


                object[] objects = new object[1];
                objects[0] = email;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool Validation()
        {
            bool result = true;
            double res;
            try
            {
                if (txtFax.Text != string.Empty)
                    if (!Double.TryParse(txtFax.Text, out res) || !Double.TryParse(txtFaxSTD.Text, out res) || !Double.TryParse(txtFaxISD.Text, out res))
                    {
                        lblFax.CssClass = "Error";
                        result = false;
                    }
                if (txtPhoneNumber1.Text != string.Empty)
                    if (!Double.TryParse(txtISD1.Text, out res) || !Double.TryParse(txtPhoneNumber1.Text, out res) || !Double.TryParse(txtSTD1.Text, out res))
                    {
                        lblPhone1.CssClass = "Error";
                        result = false;
                    }
                if (txtPhoneNumber2.Text != string.Empty)
                    if (!Double.TryParse(txtSTD2.Text, out res) || !Double.TryParse(txtPhoneNumber2.Text, out res) || !Double.TryParse(txtISD2.Text, out res))
                    {
                        lblPhone2.CssClass = "Error";
                        result = false;
                    }
                if (!ChkMailId(txtEmail.Text.ToString()))
                {
                    result = false;
                    lblEmail.CssClass = "Error";

                }
                else
                {
                    result = true;
                    lblEmail.CssClass = "FieldName";


                }

                if (txtFirstName.Text.ToString() == "")
                {
                    //lblContactName.CssClass = "Error";
                    lblFirst.CssClass = "Error";
                    result = false;

                }

                //if (txtPhoneNumber1.Text == "")
                //{
                //    lblPhone1.CssClass = "Error";
                //    result = false;
                //}

                //if (txtSTD1.Text == " ")
                //{
                //    lblSTD.CssClass = "Error";
                //    result = false;
                //}


                if (txtAddressLine1.Text == "")
                {
                    lblLine1.CssClass = "Error";
                    result = false;
                }

                if (txtOrganizationName.Text == "")
                {
                    lblOrganizationName.CssClass = "Error";
                    result = false;
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

                FunctionInfo.Add("Method", "EditAdvisorProfile.ascx:Validation()");


                object[] objects = new object[1];

                objects[0] = result;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            RMVo newRmVo = new RMVo();
            int isd2 = 0;
            int std2 = 0;
            int phone2 = 0;
            int fisd = 0;
            int fstd = 0;
            int fphone = 0;
            try
            {
                if (Validation())
                {
                    string multibranch = "";
                    if (rbtnNo.Checked)
                    {
                        multibranch = "No";
                    }
                    else { multibranch = "yes"; }
                    advisorVo.UserId = userVo.UserId;
                    newRmVo.UserId = userVo.UserId;
                    newRmVo.FirstName = txtFirstName.Text.Trim().ToString();
                    newRmVo.MiddleName = txtMiddleName.Text.Trim().ToString();
                    newRmVo.LastName = txtLastName.Text.Trim().ToString();
                    newRmVo.RMId = rmVo.RMId;

                    path = Server.MapPath("Images") + "\\";
                    if (logoChange.HasFile)
                    {
                        string[] fileName = logoChange.FileName.Split('.');
                        advisorVo.LogoPath = advisorVo.OrganizationName + "_" + fileName[0] + ".jpg";
                        //advisorBranchVo.LogoPath = advisorVo.advisorId + "_" + txtBranchCode.Text.ToString() + ".jpg";
                        HttpPostedFile myFile = logoChange.PostedFile;
                        UploadImage(path, myFile, advisorVo.LogoPath);
                    }
                    advisorVo.AddressLine1 = txtAddressLine1.Text.Trim().ToString();
                    advisorVo.AddressLine2 = txtAddressLine2.Text.Trim().ToString();
                    advisorVo.AddressLine3 = txtAddressLine3.Text.Trim().ToString();
                    advisorVo.Designation = textDesignation.Text.Trim().ToString();
                    if (ddlBusinessType.SelectedIndex != 0)
                    {
                        advisorVo.BusinessCode = ddlBusinessType.SelectedItem.Value.ToString();
                    }
                    else
                        advisorVo.BusinessCode = null;
                    advisorVo.City = txtCity.Text.Trim();
                    advisorVo.ContactPersonFirstName = txtFirstName.Text.Trim().ToString();
                    advisorVo.ContactPersonLastName = txtLastName.Text.Trim().ToString();
                    advisorVo.ContactPersonMiddleName = txtMiddleName.Text.Trim().ToString();
                    if (!string.IsNullOrEmpty(txtCountry.Text.Trim()))
                        advisorVo.Country = txtCountry.Text.Trim();
                    advisorVo.Email = txtEmail.Text.Trim().ToString();
                    if (!string.IsNullOrEmpty(txtwebsite.Text.Trim().ToString()))
                        advisorVo.Website = txtwebsite.Text.Trim().ToString();
                    if (!string.IsNullOrEmpty(txtMobileNumber.Text.Trim().ToString()))
                    {
                        advisorVo.MobileNumber = long.Parse(txtMobileNumber.Text.Trim().ToString());
                    }
                    else
                    {
                        advisorVo.MobileNumber = 0;
                    }
                    newRmVo.Email = txtEmail.Text.Trim().ToString();

                    advisorVo.Website = txtwebsite.Text.Trim().ToString();
                    if (txtFax.Text.Trim() == "")
                    {
                        advisorVo.Fax = 0;
                        newRmVo.Fax = 0;
                    }
                    else
                    {
                        int.TryParse(txtFax.Text.ToString(), out fphone);
                        advisorVo.Fax = fphone;
                        newRmVo.Fax = fphone;
                    }
                    if (txtFaxISD.Text.Trim() == "")
                    {
                        advisorVo.FaxIsd = 0;
                        newRmVo.FaxIsd = 0;
                    }
                    else
                    {
                        int.TryParse(txtFaxISD.Text.ToString(), out fisd);
                        advisorVo.FaxIsd = fisd;
                        newRmVo.FaxIsd = fisd;
                    }
                    if (txtFaxSTD.Text.Trim() == "")
                    {
                        advisorVo.FaxStd = 0;
                        newRmVo.FaxStd = 0;
                    }
                    else
                    {
                        int.TryParse(txtFaxSTD.Text.ToString(), out fstd);
                        advisorVo.FaxStd = fstd;
                        newRmVo.FaxStd = fstd;
                    }
                    if (txtMobileNumber.Text.Trim() != string.Empty)
                    {
                        advisorVo.MobileNumber = Convert.ToInt64(txtMobileNumber.Text.Trim().ToString());
                        newRmVo.Mobile = Convert.ToInt64(txtMobileNumber.Text.Trim().ToString());
                    }
                    else
                    {
                        advisorVo.MobileNumber = 0;
                        newRmVo.Mobile = 0;
                    }
                    if (rbtnNo.Checked)
                    {
                        advisorVo.MultiBranch = 0;
                    }
                    else
                    {
                        advisorVo.MultiBranch = 1;
                    }

                    if (rbtnAssModelTypeNo.Checked)
                        advisorVo.Associates = 0;
                    else
                        advisorVo.Associates = 1;

                    advisorVo.OrganizationName = txtOrganizationName.Text.Trim().ToString();
                    if (txtISD1.Text.Trim() == "")
                    {
                        advisorVo.Phone1Isd = 0;
                        newRmVo.OfficePhoneDirectIsd = 0;
                    }
                    else
                    {
                        advisorVo.Phone1Isd = int.Parse(txtISD1.Text.Trim().ToString());
                        newRmVo.OfficePhoneDirectIsd = int.Parse(txtISD1.Text.Trim().ToString());
                    }
                    if (txtPhoneNumber1.Text.Trim() != string.Empty)
                    {
                        advisorVo.Phone1Number = int.Parse(txtPhoneNumber1.Text.Trim().ToString());
                        newRmVo.OfficePhoneDirectNumber = int.Parse(txtPhoneNumber1.Text.Trim().ToString());
                    }
                    else
                    {
                        advisorVo.Phone1Number = 0;
                        newRmVo.OfficePhoneDirectNumber = 0;
                    }

                    if (txtSTD1.Text.Trim() != string.Empty)
                    {
                        advisorVo.Phone1Std = int.Parse(txtSTD1.Text.Trim().ToString());
                        newRmVo.OfficePhoneDirectStd = int.Parse(txtSTD1.Text.Trim().ToString());
                    }
                    else
                    {
                        advisorVo.Phone1Std = 0;
                        newRmVo.OfficePhoneDirectStd = 0;
                    }

                    if (txtISD2.Text.Trim() == string.Empty)
                    {
                        advisorVo.Phone2Isd = 0;
                        newRmVo.OfficePhoneExtIsd = 0;
                    }
                    else
                    {
                        int.TryParse(txtISD2.Text.ToString(), out isd2);
                        advisorVo.Phone2Isd = isd2;
                        newRmVo.OfficePhoneExtIsd = isd2;
                    }
                    if (txtPhoneNumber2.Text.Trim() != "")
                    {
                        int.TryParse(txtPhoneNumber2.Text.ToString(), out std2);
                        advisorVo.Phone2Number = std2;
                        newRmVo.OfficePhoneExtNumber = std2;

                    }
                    else
                    {
                        advisorVo.Phone2Number = 0;
                        newRmVo.OfficePhoneExtNumber = 0;
                    }
                    if (txtPinCode.Text.Trim() != string.Empty)
                        advisorVo.PinCode = int.Parse(txtPinCode.Text.Trim().ToString());
                    else
                        advisorVo.PinCode = 0;

                    if (txtSTD2.Text.Trim() == "")
                    {
                        advisorVo.Phone2Std = 0;
                        newRmVo.OfficePhoneExtStd = 0;
                    }
                    else
                    {
                        int.TryParse(txtSTD2.Text.ToString(), out std2);
                        advisorVo.Phone2Std = std2;
                        newRmVo.OfficePhoneExtStd = std2;
                    }
                    if (ddlState.SelectedIndex != 0)
                    {
                        advisorVo.State = ddlState.SelectedValue.ToString();
                    }
                    else
                        advisorVo.State = null;
                    Session["advisorVo"] = (AdvisorVo)advisorVo;

                    //userVo.Email = txtEmail.Text.Trim().ToString();
                    //userVo.FirstName = txtFirstName.Text.Trim().ToString();
                    //userVo.LastName = txtLastName.Text.Trim().ToString();
                    //userVo.MiddleName = txtMiddleName.Text.Trim().ToString();
                    //userVo.UserId = advisorVo.UserId;

                    // Updating Adviser , User and RM
                    advisorBo.UpdateAdvisorUser(advisorVo);
                    //userBo.UpdateUser(userVo);
                    //advisorStaffBo.UpdateStaff(newRmVo);


                    rbtnYes.Enabled = false;
                    rbtnNo.Enabled = false;
                    txtCity.Enabled = false;
                    ddlBusinessType.Enabled = false;

                    ddlState.Enabled = false;
                    txtAddressLine1.Enabled = false;
                    txtAddressLine2.Enabled = false;
                    txtAddressLine3.Enabled = false;
                    txtEmail.Enabled = false;
                    txtFax.Enabled = false;
                    txtFaxISD.Enabled = false;
                    txtFaxSTD.Enabled = false;
                    txtFirstName.Enabled = false;
                    txtISD1.Enabled = false;
                    txtISD2.Enabled = false;
                    txtLastName.Enabled = false;
                    txtMiddleName.Enabled = false;
                    txtMobileNumber.Enabled = false;
                    txtOrganizationName.Enabled = false;
                    txtPhoneNumber1.Enabled = false;
                    txtPhoneNumber2.Enabled = false;
                    txtPinCode.Enabled = false;
                    txtSTD1.Enabled = false;
                    txtSTD2.Enabled = false;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdvisorProfile','none');", true);
                }
                else
                {
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

                FunctionInfo.Add("Method", "EditAdvisorProfile.ascx:btnSubmit_Click()");


                object[] objects = new object[4];

                objects[0] = advisorVo;
                objects[1] = advisorBo;
                objects[2] = userVo;
                objects[3] = userBo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void DisplayLogoControl(object sender, EventArgs e)
        {
            if (!logoChange.Visible)
                logoChange.Visible = true;
            else
                logoChange.Visible = false;
        }

        private void UploadImage(string ImagePath, HttpPostedFile postedFile, string fileName) // Here ImagePath is actually the Image Name
        {
            try
            {
                UploadImagePath = ImagePath;

                // Get the data
                HttpPostedFile jpeg_image_upload = postedFile;

                // Retrieve the uploaded image
                original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
                original_image.Save(UploadImagePath + "O_" + System.IO.Path.GetFileName(jpeg_image_upload.FileName));

                // Calculate the new width and height
                int width = original_image.Width;
                int height = original_image.Height;
                int new_width, new_height;
                //string thumbnail_id;

                int target_width = 70;
                int target_height = 78;
                CreateThumbnail(original_image, ref final_image, ref graphic, ref ms, jpeg_image_upload, width, height, target_width, target_height, "", true, false, out new_width, out new_height, fileName); // , out thumbnail_id

                File.Delete(UploadImagePath + "O_" + System.IO.Path.GetFileName(jpeg_image_upload.FileName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Clean up
                if (final_image != null) final_image.Dispose();
                if (graphic != null) graphic.Dispose();
                if (original_image != null) original_image.Dispose();
                if (thumbnail_image != null) thumbnail_image.Dispose();
                if (ms != null) ms.Close();
            }
        }

        private void CreateThumbnail(System.Drawing.Image original_image, ref System.Drawing.Bitmap final_image, ref System.Drawing.Graphics graphic, ref MemoryStream ms, HttpPostedFile jpeg_image_upload, int width, int height, int target_width, int target_height, string prefix, bool thumb, bool watermark, out int new_width, out int new_height, string fileName) // , out string thumbnail_id
        {
            int indexOfExtension = System.IO.Path.GetFileName(jpeg_image_upload.FileName).LastIndexOf(".");
            string imageFileName = System.IO.Path.GetFileName(jpeg_image_upload.FileName).Substring(0, indexOfExtension);

            float image_ratio = (float)width / (float)height;

            if (width > height)
            {
                if (image_ratio > 1.5)
                {   // If Image width is lot greater than height, then do following
                    if (width > 100 && width < 200)
                    {
                        target_width = width;
                    }
                    else if (width > 200)
                    {
                        target_width = 200;
                    }
                }
            }

            float target_ratio = (float)target_width / (float)target_height;

            if (target_ratio > image_ratio)
            {
                new_height = target_height;
                new_width = (int)Math.Floor(image_ratio * (float)target_height);
            }
            else
            {
                new_height = (int)Math.Floor((float)target_width / image_ratio);
                new_width = target_width;
            }

            new_width = new_width > target_width ? target_width : new_width;
            new_height = new_height > target_height ? target_height : new_height;

            //final_image = new System.Drawing.Bitmap(target_width, target_height);
            final_image = new System.Drawing.Bitmap(new_width, new_height);
            graphic = System.Drawing.Graphics.FromImage(final_image);
            graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), new System.Drawing.Rectangle(0, 0, new_width, new_height));
            //int paste_x = (target_width - new_width) / 2;
            //int paste_y = (target_height - new_height) / 2;
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; /* new way */
            //graphic.DrawImage(original_image, paste_x, paste_y, new_width, new_height);
            graphic.DrawImage(original_image, 0, 0, new_width, new_height);

            ms = new MemoryStream();
            final_image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            saveJpeg(UploadImagePath + fileName, final_image, 100); //jpeg_image_upload.FileName
            ms.Dispose();
        }

        private void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam =
               new EncoderParameter(Encoder.Quality, (long)quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec =
               this.getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }
    }
}
