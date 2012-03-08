using System;
using System.Web.UI.WebControls;
using VoUser;
using BoCustomerProfiling;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;
using Telerik.Web.UI;
using System.Drawing.Imaging;
using System.IO;
using BoAdvisorProfiling;
using PCGMailLib;
using VoAdvisorProfiling;
using System.Net.Mail;
using System.Drawing.Printing;
using System.Drawing;
using System.Web.UI;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Web;


namespace WealthERP.Customer
{
    public partial class ViewCustomerProofs : System.Web.UI.UserControl
    {
        string path = "";
        string TargetPath = "";
        string imageUploadPath = "";
        CustomerProofUploadsVO CPUVo = new CustomerProofUploadsVO();

        System.Drawing.Image thumbnail_image = null;
        System.Drawing.Image original_image = null;
        System.Drawing.Bitmap final_image = null;
        System.Drawing.Graphics graphic = null;
        MemoryStream ms = null;
        CustomerVo customerVo = new CustomerVo();
        RMVo rmVo = new RMVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        DataSet dsCustomerProof = new DataSet();
        CustomerBo customerBo = new CustomerBo();
        int custBankAccId;
        int customerId;
        AdvisorVo adviserVo = new AdvisorVo();
        System.Guid guid = System.Guid.NewGuid();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session["CustomerVo"];
            Session["rmVo"] = advisorStaffBo.GetAdvisorStaffDetails(customerVo.RmId);
            rmVo = (RMVo)Session["rmVo"];
            Session["Button"] = "";
            //DeleteAllTempFiles();
            adviserVo = (AdvisorVo)Session["AdvisorVo"];
            if (!IsPostBack)
            {
                BindProofTypeDP();
                //BindProofPurposeList();
                BindProofCopy();

                LoadImages();
                btnDelete.Visible = false;
                lblFileUploaded.Visible = false;
            }
        }

        private void DeleteAllTempFiles()
        {
            string Filepath = Server.MapPath("TempCustomerProof") + "\\" + customerVo.CustomerId.ToString() + "\\";
            if (Directory.Exists(Filepath))
            {
                string[] filePaths = Directory.GetFiles(Filepath);
                foreach (string filePath in filePaths)
                {
                    File.Delete(filePath);
                }
                Directory.Delete(Filepath);
            }
        }

        private void BindProofCopy()
        {
            DataTable dtProofCopy = new DataTable();
            dtProofCopy = customerBo.GetCustomerProofCopy();

            if (dtProofCopy.Rows.Count > 0)
            {
                ddlProofCopyType.DataSource = dtProofCopy;
                ddlProofCopyType.DataValueField = dtProofCopy.Columns["XPCT_ProofCopyTypeCode"].ToString();
                ddlProofCopyType.DataTextField = dtProofCopy.Columns["XPCT_ProofCopyType"].ToString();
                ddlProofCopyType.DataBind();
            }
            if (!IsPostBack)
            {
                ddlProofCopyType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }
        }

        //private void BindProofPurposeList()
        //{
        //    DataTable dtProofPurpose = new DataTable();
        //    dtProofPurpose = customerBo.GetCustomerProofPurpose();

        //    if (dtProofPurpose.Rows.Count > 0)
        //    {
        //        lstProofPurpose.DataSource = dtProofPurpose;
        //        lstProofPurpose.DataValueField = dtProofPurpose.Columns["WPP_ProofPurposeCode"].ToString();
        //        lstProofPurpose.DataTextField = dtProofPurpose.Columns["WPP_ProofPurpose"].ToString();
        //        lstProofPurpose.DataBind();
        //    }
        //}

        private void BindProofTypeDP()
        {
            DataTable dtDpProofTypes = new DataTable();
            dtDpProofTypes = customerBo.GetCustomerProofTypes();

            if (dtDpProofTypes.Rows.Count > 0)
            {
                ddlProofType.DataSource = dtDpProofTypes;
                ddlProofType.DataValueField = dtDpProofTypes.Columns["XPRT_ProofTypeCode"].ToString();
                ddlProofType.DataTextField = dtDpProofTypes.Columns["XPRT_ProofType"].ToString();
                ddlProofType.DataBind();
            }
            if (Session["Button"] == "Submit & Add More")
            {
                ddlProofType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                ddlProof.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }
            if (!IsPostBack)
            {
                ddlProofType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                ddlProof.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

            }
        }

        protected void ddlProofType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtDpProofsForTypes = new DataTable();
            if (ddlProofType.SelectedIndex != 0)
                dtDpProofsForTypes = customerBo.GetCustomerProofsForTypes(Convert.ToInt32(ddlProofType.SelectedValue));

            if (dtDpProofsForTypes.Rows.Count > 0)
            {
                ddlProof.DataSource = dtDpProofsForTypes;
                ddlProof.DataValueField = dtDpProofsForTypes.Columns["XP_ProofCode"].ToString();
                ddlProof.DataTextField = dtDpProofsForTypes.Columns["XP_ProofName"].ToString();
                ddlProof.DataBind();
            }
            ddlProof.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            FileStream outputStream = null;
            Session["Button"] = "Submit";
            customerVo = (CustomerVo)Session["CustomerVo"];
            //H:/CustomerProofImages/advisor_1088/38186_Icard.jpg
            string Temppath = System.Configuration.ConfigurationManager.AppSettings["UploadCustomerProofImages"];

            if (Directory.Exists(Temppath))
            {
                path = Temppath + "\\advisor_" + rmVo.AdviserId + "" + "\\";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            else
            {
                path = Server.MapPath("TempCustomerProof") + "\\advisor_" + rmVo.AdviserId + "" + "\\";
            }

            UploadedFile file = radUploadProof.UploadedFiles[0];

            FileIOPermission fp = new FileIOPermission(FileIOPermissionAccess.AllAccess, path);
            PermissionSet ps = new PermissionSet(PermissionState.None);
            ps.AddPermission(fp);

            string extension = file.GetExtension();

            float fileSize = float.Parse(file.ContentLength.ToString()) / 1048576;

            float DirSize = 0;
            DirectoryInfo[] DI = new DirectoryInfo(path).GetDirectories("*.*", SearchOption.AllDirectories);
            FileInfo[] FI = new DirectoryInfo(path).GetFiles("*.*", SearchOption.AllDirectories);
            foreach (FileInfo F1 in FI)
            {
                DirSize += F1.Length;
            }
            //Converting in Mega bytes
            DirSize = DirSize / 1048576;
            if ((fileSize < adviserVo.VaultSize) && (DirSize < adviserVo.VaultSize))
            {
                foreach (UploadedFile f in radUploadProof.UploadedFiles)
                {
                    int l = (int)f.InputStream.Length;
                    byte[] bytes = new byte[l];
                    f.InputStream.Read(bytes, 0, l);

                    imageUploadPath = customerVo.CustomerId + "_" + guid + "_" + f.GetName();
                    if (extension != ".pdf")
                        UploadImage(path, f, imageUploadPath);
                    else
                    {
                        f.SaveAs(path + "\\" + imageUploadPath);
                    }
                }
                
                CPUVo.CustomerId = customerVo.CustomerId;
                CPUVo.ProofTypeCode = Convert.ToInt32(ddlProofType.SelectedValue);
                CPUVo.ProofCode = Convert.ToInt32(ddlProof.SelectedValue);
                CPUVo.ProofCopyTypeCode = ddlProofCopyType.SelectedValue;
                CPUVo.ProofImage = path + "\\" + imageUploadPath;
                CreateDBReferrenceForProofUploads(CPUVo);

                LoadImages();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Sorry your proof attachment size exceeds the allowable limit..!');", true);
            }
        }

        private void CreateDBReferrenceForProofUploads(CustomerProofUploadsVO CPUVo)
        {
            int proofUploadID = 0;
            bool bStatus = false;

            if (btnSubmit.Text == "Submit")
            {
                if (CPUVo != null)
                {
                    bStatus = customerBo.CreateCustomersProofUploads(CPUVo, proofUploadID);
                }
            }
            else if (btnSubmit.Text == "Update")
            {
                proofUploadID = Convert.ToInt32(Session["ProofID"].ToString());
                if (CPUVo != null)
                {
                    bStatus = customerBo.CreateCustomersProofUploads(CPUVo, proofUploadID);
                }
            }
            
        }

        private void UploadImage(string path, UploadedFile f, string imageUploadPath)
        {
            TargetPath = path;

            UploadedFile jpeg_image_upload = f;

            // Retrieve the uploaded image
            original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
            original_image.Save(TargetPath + "O_" + System.IO.Path.GetFileName(jpeg_image_upload.FileName));

            int width = original_image.Width;
            int height = original_image.Height;
            int new_width, new_height;
            //string thumbnail_id;

            int target_width = 140;
            int target_height = 100;


            CreateThumbnail(original_image, ref final_image, ref graphic, ref ms, jpeg_image_upload, width, height, target_width, target_height, "", true, false, out new_width, out new_height, imageUploadPath); // , out thumbnail_id

            File.Delete(TargetPath + "O_" + System.IO.Path.GetFileName(jpeg_image_upload.FileName));
        }


        private void CreateThumbnail(System.Drawing.Image original_image, ref System.Drawing.Bitmap final_image, ref System.Drawing.Graphics graphic, ref MemoryStream ms, UploadedFile jpeg_image_upload, int width, int height, int target_width, int target_height, string p, bool p_11, bool p_12, out int new_width, out int new_height, string imageUploadPath)
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
            graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Transparent), new System.Drawing.Rectangle(0, 0, new_width, new_height));
            //int paste_x = (target_width - new_width) / 2;
            //int paste_y = (target_height - new_height) / 2;
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
            //graphic.DrawImage(original_image, paste_x, paste_y, new_width, new_height);
            graphic.DrawImage(original_image, 0, 0, new_width, new_height);

            ms = new MemoryStream();
            final_image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            saveJpeg(TargetPath + imageUploadPath, final_image, 100); //jpeg_image_upload.FileName
            ms.Dispose();
        }

        private void saveJpeg(string p, System.Drawing.Bitmap final_image, int p_3)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam =
               new EncoderParameter(Encoder.Quality, (long)p_3);

            // Jpeg image codec
            ImageCodecInfo jpegCodec =
               this.getEncoderInfo("image/png");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            final_image.Save(p, jpegCodec, encoderParams);
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

        private void LoadImages()
        {
            //string Temppath = "";
            DataTable dtImages = new DataTable();
            DataTable dtBindImages = new DataTable();
            DataTable dtProofPurposes = new DataTable();
            dtBindImages.Columns.Add("ProofUploadId");
            dtBindImages.Columns.Add("ProofType");
            dtBindImages.Columns.Add("ProofName");
            dtBindImages.Columns.Add("ProofCopyType");
            //dtBindImages.Columns.Add("ProofPurposes");
            dtBindImages.Columns.Add("ProofImage");
            dtBindImages.Columns.Add("ProofExtensions");
            dtBindImages.Columns.Add("ProofFileName");

            DataRow drBindImages = null;
            dtImages = GetUploadedImagePaths(0);
            System.Web.UI.WebControls.Image imageProof = new System.Web.UI.WebControls.Image();
            System.Web.UI.WebControls.HyperLink hypPdf = new HyperLink();
            string fileExt = "";

            int i = 0;
            //int fileCount = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Length;
            string sourceDir = "";
            if (dtImages.Rows.Count > 0)
            {
                foreach (DataRow drUploadImage in dtImages.Rows)
                {
                    drBindImages = dtBindImages.NewRow();
                    drBindImages["ProofUploadId"] = drUploadImage["CPU_ProofUploadId"];
                    drBindImages["ProofType"] = drUploadImage["XPRT_ProofType"];
                    drBindImages["ProofName"] = drUploadImage["XP_ProofName"];
                    drBindImages["ProofCopyType"] = drUploadImage["XPCT_ProofCopyType"];
                    drBindImages["ProofImage"] = drUploadImage["CPU_Image"];
                    drBindImages["ProofExtensions"] = Path.GetExtension(drUploadImage["CPU_Image"].ToString());
                    drBindImages["ProofFileName"] = Path.GetFileName(drUploadImage["CPU_Image"].ToString());

                    dtBindImages.Rows.Add(drBindImages);
                }
                if (dtBindImages.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtBindImages.Rows)
                    {
                        string fileTempPath = dr["ProofImage"].ToString();

                        string extension = Path.GetExtension(fileTempPath);
                        string fileName = Path.GetFileName(fileTempPath);
                        Session["FileExtension"] = extension;
                    }
                }
                repProofImages.DataSource = dtBindImages;
                repProofImages.DataBind();

                if (Session["Button"] != "")
                {
                    if (Session["Button"] == "Submit")
                    {
                        radPOCProof.SelectedIndex = 1;
                        multiPageView.SelectedIndex = 1;
                    }
                    else if (Session["Button"] == "Submit & Add More")
                    {
                        radPOCProof.TabIndex = 0;
                        multiPageView.TabIndex = 0;

                        BindProofTypeDP();
                        ddlProofType.SelectedIndex = 0;
                        BindProofCopy();
                        ddlProofCopyType.SelectedIndex = 0;
                        ddlProof.SelectedIndex = 0;
                        btnDelete.Visible = false;
                    }
                }

            }
            else
            {
                repProofImages.DataSource = null;
                radPOCProof.SelectedIndex = 0;
                multiPageView.SelectedIndex = 0;
            }
        }


        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            Session["Button"] = "Submit & Add More";
            customerVo = (CustomerVo)Session["CustomerVo"];

            //string Temppath = System.Configuration.ConfigurationManager.AppSettings["UploadCustomerProofImages"];
            //if (Directory.Exists(Temppath))
            //{
            //    path = Temppath + "\\";
            //    //path = path.Replace("\\", "
            //}
            //else
            //{
            //    path = Server.MapPath("ProofImages") + "\\";
            //}

            if (!System.IO.Directory.Exists(Server.MapPath("~/CustomerProofImages/advisor_" + rmVo.AdviserId + "")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/CustomerProofImages/advisor_" + rmVo.AdviserId + ""));
            }


            string Temppath = "~/CustomerProofImages/advisor_" + rmVo.AdviserId + "";
            path = Server.MapPath("~/CustomerProofImages/advisor_" + rmVo.AdviserId + "") + "\\";

            UploadedFile file = radUploadProof.UploadedFiles[0];

            float fileSize = float.Parse(file.ContentLength.ToString()) / 1048576;

            float DirSize = 0;
            DirectoryInfo[] DI = new DirectoryInfo(path).GetDirectories("*.*", SearchOption.AllDirectories);
            FileInfo[] FI = new DirectoryInfo(path).GetFiles("*.*", SearchOption.AllDirectories);
            foreach (FileInfo F1 in FI)
            {
                DirSize += F1.Length;
            }
            //Converting in Mega bytes
            DirSize = DirSize / 1048576;
            if ((fileSize < adviserVo.VaultSize) && (DirSize < adviserVo.VaultSize))
            {
                foreach (UploadedFile f in radUploadProof.UploadedFiles)
                {
                    int l = (int)f.InputStream.Length;
                    byte[] bytes = new byte[l];
                    f.InputStream.Read(bytes, 0, l);

                    imageUploadPath = customerVo.CustomerId + "_" + f.GetName();

                    UploadImage(path, f, imageUploadPath);
                }
                CPUVo.CustomerId = customerVo.CustomerId;
                CPUVo.ProofTypeCode = Convert.ToInt32(ddlProofType.SelectedValue);
                CPUVo.ProofCode = Convert.ToInt32(ddlProof.SelectedValue);
                CPUVo.ProofCopyTypeCode = ddlProofCopyType.SelectedValue;
                CPUVo.ProofImage = Temppath + "/" + imageUploadPath;
                CreateDBReferrenceForProofUploads(CPUVo);

                LoadImages();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Sorry your proof attachment size exceeds the allowable limit..!');", true);
            }

        }

        protected void repProofImages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            DataTable dtGetPerticularProofs = new DataTable();
            int ProofUploadId = int.Parse(e.CommandArgument.ToString());
            Session["ProofID"] = ProofUploadId;
            dtGetPerticularProofs = GetUploadedImagePaths(ProofUploadId);
            DataTable dtPurposes = new DataTable();
            string imageAttachmentPath = dtGetPerticularProofs.Rows[0]["CPU_Image"].ToString();
            Session["ImagePath"] = imageAttachmentPath;
            lblFileUploaded.Visible = true;
            lblFileUploaded.Text = Path.GetFileName(imageAttachmentPath);

            if (e.CommandName == "Send Mail")
            {
                SendMail(imageAttachmentPath);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Proof Image sent successfully..!');", true);
            }
            if (e.CommandName == "Print proof")
            {
                int printerCount = PrinterSettings.InstalledPrinters.Count;
                if (printerCount != 0)
                {
                    PrintProof(imageAttachmentPath);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Proof Image printing is done successfully..!');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('No printers available..!');", true);
                }
            }
            if (e.CommandName == "Edit proof")
            {
                DataTable dtTempProofBind = new DataTable();
                radPOCProof.SelectedIndex = 0;
                multiPageView.SelectedIndex = 0;
                btnDelete.Visible = true;
                btnSubmit.Text = "Update";
                btnSubmitAdd.Visible = false;

                BindProofTypeDP();
                BindProofCopy();

                ddlProofType.SelectedValue = dtGetPerticularProofs.Rows[0]["XPRT_ProofTypeCode"].ToString();

                if (ddlProofType.SelectedValue != "")
                    dtTempProofBind = customerBo.GetCustomerProofsForTypes(Convert.ToInt32(ddlProofType.SelectedValue));

                if (dtTempProofBind.Rows.Count > 0)
                {
                    ddlProof.DataSource = dtTempProofBind;
                    ddlProof.DataValueField = dtTempProofBind.Columns["XP_ProofCode"].ToString();
                    ddlProof.DataTextField = dtTempProofBind.Columns["XP_ProofName"].ToString();
                    ddlProof.DataBind();
                }

                ddlProof.SelectedValue = dtGetPerticularProofs.Rows[0]["XP_ProofCode"].ToString();
                ddlProofCopyType.SelectedValue = dtGetPerticularProofs.Rows[0]["XPCT_ProofCopyTypeCode"].ToString();
            }

            string path = "";
            if (e.CommandName == "NavigateToPdf")
            {
                path = e.CommandArgument.ToString();
                Response.Redirect(path);
            }
        }

        private void PrintProof(string imageAttachmentPath)
        {
            string extension = Path.GetExtension(imageAttachmentPath);
            if (extension != ".pdf")
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(printPage);
                pd.Print();
            }
            else
            {
                PrintDocument pdf = new PrintDocument();

            }
        }

        private void printPage(object o, PrintPageEventArgs e)
        {
            string imagePath = "";
            if (Session["ImagePath"] != null)
            {
                imagePath = Session["ImagePath"].ToString();
                //imagePath = Server.MapPath(imagePath);
            }

            System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath);
            Point loc = new Point(100, 100);
            e.Graphics.DrawImage(img, 100, 300, 800, 700);
        }

        private void SendMail(string imageAttachmentPath)
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();
            //imageAttachmentPath = Server.MapPath(imageAttachmentPath);

            Attachment inline = new Attachment(imageAttachmentPath);
            inline.ContentDisposition.Inline = true;
            email.Attachments.Add(inline);
            email.Subject = "Customer proof uploads";
            email.Body = "Dear " + customerVo.FirstName + customerVo.MiddleName + customerVo.LastName + ""
                          + "<br />"
                          + "<br />"
                          + "<br />"
                          + "Please find attached Proof.";
            email.IsBodyHtml = true;
            email.To.Add(customerVo.Email);

            AdviserStaffSMTPBo adviserStaffSMTPBo = new AdviserStaffSMTPBo();
            AdviserStaffSMTPVo adviserStaffSMTPVo = adviserStaffSMTPBo.GetSMTPCredentials(rmVo.AdviserId);
            if (adviserStaffSMTPVo.HostServer != null && adviserStaffSMTPVo.HostServer != string.Empty)
            {
                emailer.isDefaultCredentials = !Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired);

                if (!String.IsNullOrEmpty(adviserStaffSMTPVo.Password))
                    emailer.smtpPassword = Encryption.Decrypt(adviserStaffSMTPVo.Password);
                emailer.smtpPort = int.Parse(adviserStaffSMTPVo.Port);
                emailer.smtpServer = adviserStaffSMTPVo.HostServer;
                emailer.smtpUserName = adviserStaffSMTPVo.Email;

                if (Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired))
                {
                    email.From = new MailAddress(emailer.smtpUserName, rmVo.FirstName + rmVo.MiddleName + rmVo.LastName);
                }
            }
            bool isMailSent = emailer.SendMail(email);
        }


        private DataTable GetUploadedImagePaths(int ProofUploadId)
        {
            DataTable dtImages = new DataTable();
            try
            {
                dtImages = customerBo.GetCustomerUploadedProofs(customerVo.CustomerId, ProofUploadId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetUploadedImagePaths()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dtImages;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showDeleteConfirmAlert();", true);
        }

        protected void hdnbtnDelete_Click(object sender, EventArgs e)
        {
            DataTable dtGetPerticularProofs = new DataTable();
            if (hdnMsgValue.Value == "1")
            {
                if (Session["ProofID"] != null)
                {
                    int ProofIdToDelete = Convert.ToInt32(Session["ProofID"].ToString());
                    dtGetPerticularProofs = GetUploadedImagePaths(ProofIdToDelete);
                    string imageAttachmentPath = dtGetPerticularProofs.Rows[0]["CPU_Image"].ToString();
                    //imageAttachmentPath = Server.MapPath(imageAttachmentPath);
                    if (customerBo.DeleteCustomerUploadedProofs(customerVo.CustomerId, ProofIdToDelete))
                    {
                        File.Delete(imageAttachmentPath);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Proof deleted successfully..!');", true);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "loadcontrol('ViewCustomerProofs','login');", true);
                    }

                }
            }
        }

        protected void repProofImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        protected string LoadControls(string extension, string proofPath, string fileName)
        {
            string control = "";

            if (extension != ".pdf")
            {
                control = "<ul class=\"quickZoom\">"
                           + "<li style=\"text-align: center; float: left;\">"
                           + "<img src=\"../General/ImageServe.aspx?Path=" + proofPath + "\" style=\"float: left;\" alt=\"Pdf file\" />"
                           + "</li>"
                           + "</ul>"
                           + "<br />"
                           + "<br />";
            }
            else
            {
                string Filepath = Server.MapPath("TempCustomerProof") + "\\" + customerVo.CustomerId.ToString() + "\\";
                Directory.CreateDirectory(Filepath);
                //Filepath = Server.MapPath("TempCustomerProof") + "\\" + customerVo.CustomerId.ToString() + "\\";
                Filepath = Filepath + fileName;
                if (!File.Exists(Filepath))
                {
                    File.Copy(proofPath, Filepath);
                }
                control = "<a href=\"../TempCustomerProof/" + customerVo.CustomerId.ToString() + "/" + fileName + "\" target=\"_blank\" class=\"LinkButtons\" >" + fileName + "</a>";
            }
            return control;
        }

        private void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs e)
        {
            e.IsValid = (radUploadProof.InvalidFiles.Count == 0);
        }
    }
}