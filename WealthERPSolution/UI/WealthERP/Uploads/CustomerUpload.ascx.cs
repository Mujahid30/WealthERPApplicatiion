using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.SqlServer.Dts.Runtime;
using System.IO;
using BoUploads;
using VoUploads;
using VoCustomerProfiling;
using VoUser;
using BoCustomerProfiling;
using BoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Collections;
using WealthERP.Base;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Globalization;



namespace WealthERP.Uploads
{
    public partial class CustomerUpload : System.Web.UI.UserControl
    {
        List<KarvyUploadsVo> karvyNewCustomerList = new List<KarvyUploadsVo>();
        List<CamsUploadsVo> camsNewCustomerList = new List<CamsUploadsVo>();
        List<WerpUploadsVo> werpNewCustomerList = new List<WerpUploadsVo>();

        KarvyUploadsVo karvyUploadsVo = new KarvyUploadsVo();
        StandardFolioUploadBo standardFolioUploadBo = new StandardFolioUploadBo();
        WerpMFUploadsBo werpMFUploadsBo = new WerpMFUploadsBo();
        WerpEQUploadsBo werpEQUploadsBo = new WerpEQUploadsBo();
        WerpUploadsBo werpUploadBo = new WerpUploadsBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        MFTransactionVo mfTransactionVo = new MFTransactionVo();
        UserVo userVo = new UserVo();
        UserVo rmUserVo = new UserVo();
        UserVo tempUserVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        UploadProcessLogVo processlogVo = new UploadProcessLogVo();
        RMVo rmVo = new RMVo();

        string ValidationProgress = "";
        CamsUploadsBo camsUploadsBo = new CamsUploadsBo();
        KarvyUploadsBo karvyUploadsBo = new KarvyUploadsBo();
        StandardProfileUploadBo StandardProfileUploadBo = new StandardProfileUploadBo();
        TempletonUploadsBo templetonUploadsBo = new TempletonUploadsBo();
        DeutscheUploadsBo deutscheUploadsBo = new DeutscheUploadsBo();
        CustomerBo customerBo = new CustomerBo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        UploadCommonBo uploadsCommonBo = new UploadCommonBo();
        UploadValidationBo uploadsvalidationBo = new UploadValidationBo();
        UserBo userBo = new UserBo();

        Random id = new Random();
        DataSet getNewFoliosDs = new DataSet();
        DataTable getNewFoliosDt = new DataTable();
        DataSet dsXML = new DataSet();
        DataTable dtInputRejects;
        string message = "";

        int customerId;
        int customerId2;
        int userId;
        int UploadProcessId = 0;
        int portfolioId;
        int countCustCreated = 0;
        int countFolioCreated = 0;
        int adviserId;

        string folioNum;
        string packagePath;
        string reject_reason = "";
        string configPath;
        string xmlPath;
        bool badData;
        bool rejectUpload_Flag = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel1.Style.Add("display", "none");

            lnkbtnpup.Visible = false;
            ModalPopupExtender1.Hide();
            //lblErrorFileType.Visible = false;

            Message_lbl.Visible = false;

            lblFileType.Visible = false;

            string lastUploadDate = "";
            btn_Upload.Attributes.Add("onclick", "setTimeout(\"UpdateImg('Image1','/Images/Wait.gif');\",50);");
            this.Page.Culture = "en-US";
            SessionBo.CheckSession();
            rmVo = (RMVo)Session["rmVo"];
            rmUserVo = (UserVo)Session["userVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["UserVo"];
            DateTime uploadDate;

            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            lastUploadDate = uploadsCommonBo.GetLastUploadDate(adviserVo.advisorId);
            if (lastUploadDate != "")
            {
                //DateTime dt = new DateTime();
                //String.Format("{0:d}", dt);

                lblLastUploadDateText.Visible = true;
                lblLastUploadDate.Visible = true;
                if (lastUploadDate != "01/01/0001 00:00:00")
                {
                    uploadDate = DateTime.Parse(lastUploadDate);
                    lastUploadDate = uploadDate.ToLongDateString();
                }
                else
                {
                    lastUploadDate = "No Uploaded History!";

                }

                lblLastUploadDate.Text = lastUploadDate.ToString();
            }
            if (Session["userVo"] != null)
            {

            }
            else
            {
                Session.Clear();
                Session.Abandon();

                // If User Sessions are empty, load the login control 
                Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscript", "loadcontrol('SessionExpired','');", true);
            }

            if (!IsPostBack)
            {

                lblMessage.Text = "";
                lblError.Text = "";

                //trFileTypeRow.Visible = false;
                trError.Visible = false;
                trMessage.Visible = false;
                rbSkipRowsNo.Checked = true;

                BindListBranch(adviserVo.advisorId, "adviser");

            }
            divInputErrorList.Visible = false;

        }

        private static Dictionary<string, string> GetProfileGenericDictionary()
        {
            Dictionary<string, string> genDictProfile = new Dictionary<string, string>();
            genDictProfile.Add("Standard", "WP");

            return genDictProfile;
        }
        //MF FOLIO DIctonary - Vishal
        private static Dictionary<string, string> GetMFFolioGenericDictionary()
        {
            Dictionary<string, string> genDictProfile = new Dictionary<string, string>();
            genDictProfile.Add("Standard", "MFF");

            return genDictProfile;
        }

        private static Dictionary<string, string> GetFolioGenericDictionary()
        {
            Dictionary<string, string> genDictFolio = new Dictionary<string, string>();
            genDictFolio.Add("Standard", "WP");
            //genDictFolio.Add("CAMS", "CA");
            //genDictFolio.Add("Karvy", "KA");
            //genDictFolio.Add("Templeton", "TN");
            //genDictFolio.Add("Deutsche", "DT");
            return genDictFolio;
        }

        private static Dictionary<string, string> GetMFGenericDictionary()
        {
            Dictionary<string, string> genDictMF = new Dictionary<string, string>();
            genDictMF.Add("CAMS", "CA");
            genDictMF.Add("Karvy", "KA");
            genDictMF.Add("Templeton", "TN");
            genDictMF.Add("Deutsche", "DT");
            genDictMF.Add("Sundaram", "SU");
            genDictMF.Add("Standard", "WPT");
            return genDictMF;
        }

        private static Dictionary<string, string> GetEquityGenericDictionary()
        {
            Dictionary<string, string> genDictEquity = new Dictionary<string, string>();
            //hshEquity.Add("Odin", "Odin");
            genDictEquity.Add("Werp", "WP");
            genDictEquity.Add("OdinNSE", "ODNSE");
            genDictEquity.Add("OdinBSE", "ODBSE");
            return genDictEquity;
        }

        private static Dictionary<string, string> GetCAMSGenericDictionary()
        {
            Dictionary<string, string> genDictCAMS = new Dictionary<string, string>();
            genDictCAMS.Add("Profile", "Profile");
            genDictCAMS.Add("Transaction", "Transaction");
            genDictCAMS.Add("Systematic", "Systematic");
            return genDictCAMS;
        }

        private static Dictionary<string, string> GetKarvyGenericDictionary()
        {
            Dictionary<string, string> genDictKarvy = new Dictionary<string, string>();
            genDictKarvy.Add("Profile", "Profile");
            genDictKarvy.Add("Transaction", "Transaction");
            genDictKarvy.Add("Combination", "Combination");
            return genDictKarvy;
        }

        private static Dictionary<string, string> GetWERPGenericDictionary()
        {
            Dictionary<string, string> genDictWerp = new Dictionary<string, string>();
            genDictWerp.Add("Profile", "Profile");
            genDictWerp.Add("Transaction", "Transaction");
            return genDictWerp;
        }

        private static Dictionary<string, string> GetOdinGenericDictionary()
        {
            Dictionary<string, string> genDictOdin = new Dictionary<string, string>();
            genDictOdin.Add("Transaction", "Transaction");
            return genDictOdin;
        }

        private static Dictionary<string, string> GetEquityExtensions()
        {
            Dictionary<string, string> genDictEqExt = new Dictionary<string, string>();
            genDictEqExt.Add("XLS", "xls");
            genDictEqExt.Add("CSV", "csv");
            return genDictEqExt;
        }

        private static Dictionary<string, string> GetMFExtensions()
        {
            Dictionary<string, string> genDictMFExt = new Dictionary<string, string>();
            genDictMFExt.Add("XLS", "xls");
            genDictMFExt.Add("DBF", "dbf");
            return genDictMFExt;
        }

        private static Dictionary<string, string> GetProfileExtensions()
        {
            Dictionary<string, string> genDictProfExt = new Dictionary<string, string>();
            genDictProfExt.Add("XLS", "xls");
            return genDictProfExt;
        }

        private static Dictionary<string, string> GetFolioExtensions()
        {
            Dictionary<string, string> genDictFolioExt = new Dictionary<string, string>();
            genDictFolioExt.Add("XLS", "xls");
            return genDictFolioExt;
        }

        private static Dictionary<string, string> GetEQTradeAccGenericDictionary()
        {
            Dictionary<string, string> genDictEQTrade = new Dictionary<string, string>();
            genDictEQTrade.Add("Standard", "WP");
            return genDictEQTrade;
        }

        private static Dictionary<string, string> GetEQTradeAccExtensions()
        {
            Dictionary<string, string> genDictEQTradeExt = new Dictionary<string, string>();
            genDictEQTradeExt.Add("XLS", "xls");
            return genDictEQTradeExt;
        }

        private static Dictionary<string, string> GetEQDematAccGenericDictionary()
        {
            Dictionary<string, string> genDictEQDematAcc = new Dictionary<string, string>();
            genDictEQDematAcc.Add("Standard", "WP");
            return genDictEQDematAcc;
        }

        private static Dictionary<string, string> GetEQDematAccExtensions()
        {
            Dictionary<string, string> genDictEQDematAccExt = new Dictionary<string, string>();
            genDictEQDematAccExt.Add("XLS", "xls");
            return genDictEQDematAccExt;
        }

        private static Dictionary<string, string> GetEQTranxExtensions()
        {
            Dictionary<string, string> genDictEQTranxExt = new Dictionary<string, string>();
            genDictEQTranxExt.Add("XLS", "xls");
            return genDictEQTranxExt;
        }

        private static Dictionary<string, string> GetEQTranxGenericDictionary()
        {
            Dictionary<string, string> genDictEQTranx = new Dictionary<string, string>();
            genDictEQTranx.Add("Standard", "WP");
            genDictEQTranx.Add("India Infoline", "IIFL");
            genDictEQTranx.Add("ODIN", "ODIN");
            return genDictEQTranx;
        }

        private static Dictionary<string, string> GetMFSystematicExtensions()
        {
            Dictionary<string, string> genDictMFSystematicExt = new Dictionary<string, string>();
            genDictMFSystematicExt.Add("XLS", "xls");
            return genDictMFSystematicExt;
        }

        private static Dictionary<string, string> GetMFSystematicGenericDictionary()
        {
            Dictionary<string, string> genDictMFSystematic = new Dictionary<string, string>();
            //genDictMFSystematic.Add("Standard", "WP");
            genDictMFSystematic.Add("CAMS", "CA");
            genDictMFSystematic.Add("KARVY", "KA");
            genDictMFSystematic.Add("STANDARD", "WPT");
            return genDictMFSystematic;
        }

        protected void btn_Upload_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(5000);
            //Create XML for the file
            if (Page.IsValid)
            {
                #region Uploading Content
                string pathxml = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                bool XmlCreated = GetInputXML();
                bool stdProFirstStagingResult = false;
                bool stdProCommonStagingResult = false;
                bool stdProCreateBankAccountResult = false;
                bool stdProCreateCustomerResult = false;
                bool camsProStagingResult = false;
                bool camsProStagingCheckResult = false;
                bool camsProCommonStagingResult = false;
                bool camsProCommonChecksResult = false;
                bool camsProCreateCustomerResult = false;
                bool camsFolioCommonStagingResult = false;
                bool camsFolioWerpInsertionResult = false;
                bool templetonProStagingResult = false;
                bool templetonProStagingCheckResult = false;
                bool templetonProCommonStagingResult = false;
                bool templetonProCommonChecksResult = false;
                bool templetonProCreateCustomerResult = false;
                bool templetonFolioCommonStagingResult = false;
                bool templetonFolioWerpInsertionResult = false;
                bool deutscheProStagingResult = false;
                bool deutscheProStagingCheckResult = false;
                bool deutscheProCommonStagingResult = false;
                bool deutscheProCommonChecksResult = false;
                bool deutscheProCreateCustomerResult = false;
                bool deutscheFolioCommonStagingResult = false;
                bool deutscheFolioWerpInsertionResult = false;
                bool stdProCommonDeleteResult = false;
                bool stdFolioCommonDeleteResult = false;
                bool stdFolToWERPTable = false;
                string InputInsertionProgress = "";
                string XtrnlInsertionProgress = "";
                string XMLProgress = "";
                string SecondStagingInsertionProgress = "";
                string FirstStagingInsertionProgress = "";
                string WERPInsertionProgress = "";


                trError.Visible = false;
                xmlPath = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
                if (ValidationProgress.ToLower() != "failure")
                {
                    string fileName = Server.MapPath("\\UploadFiles\\" + UploadProcessId + ".xml");


                    if (XmlCreated == true && rejectUpload_Flag == false)
                    {
                        trError.Visible = false;

                        #region Standard Folio Upload VISHAL
                        //*****************************************************************************************************************************
                        //Standard Folio Upload
                        if (ddlUploadType.SelectedValue == Contants.ExtractTypeFolio && ddlListCompany.SelectedValue == Contants.UploadExternalTypeStandard)
                        {
                            packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioXMLtoInput.dtsx");
                            bool stdProInputResult = StandardProfileUploadBo.StdInsertToInputProfile(packagePath, fileName, configPath);
                            if (stdProInputResult)
                            {
                                processlogVo.IsInsertionToInputComplete = 1;
                                processlogVo.IsInsertionToXtrnlComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog1)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                    //stdProFirstStagingResult = StandardProfileUploadBo.StdInsertToFirstStaging(UploadProcessId, packagePath, configPath);
                                    bool stdProCommonChecksResult = StandardProfileUploadBo.StdCommonProfileChecks(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadFolioUploadCount(UploadProcessId, "RE");

                                    if (stdProCommonChecksResult)
                                    {
                                        processlogVo.IsInsertionToFirstStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                        //stdProFirstStagingResult = StandardProfileUploadBo.StdInsertToFirstStaging(UploadProcessId, packagePath, configPath);
                                        stdFolToWERPTable = StandardProfileUploadBo.StdFolioStaggingToWERP(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                        if (stdFolToWERPTable)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfCustomerInserted = uploadsCommonBo.GetUploadFolioUploadCount(UploadProcessId, "UP");

                                        }
                                    }
                                }

                            }

                            if (XmlCreated)
                                XMLProgress = "Done";
                            else
                                XMLProgress = "Failure";

                            if (stdProInputResult)
                            {
                                XtrnlInsertionProgress = "NA";
                                InputInsertionProgress = "Done";
                            }
                            else
                            {
                                InputInsertionProgress = "Failure";
                                XtrnlInsertionProgress = "1";
                            }

                            if (stdProInputResult)
                                FirstStagingInsertionProgress = "Done";
                            else
                                FirstStagingInsertionProgress = "Failure";



                            if (stdFolToWERPTable)
                                WERPInsertionProgress = "Done";
                            else
                                WERPInsertionProgress = "Failure";

                            // Update Process Summary Text Boxes
                            txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                            txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                            txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                            txtUploadedRecords.Text = processlogVo.NoOfCustomerInserted.ToString();
                            txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords - processlogVo.NoOfInputRejects;
                            Session[SessionContents.ProcessLogVo] = processlogVo;
                            bool updateProcessLog4 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (XMLProgress == "Done" && InputInsertionProgress == "Done" && FirstStagingInsertionProgress == "Done" && SecondStagingInsertionProgress == "Done" && WERPInsertionProgress == "Done")
                            {
                                msgUploadComplete.Visible = true;
                            }

                        }

                        #endregion Standard Folio Upload


                        #region Standard Profile Upload
                        //*****************************************************************************************************************************
                        //Standard Profile Upload
                        if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfile && ddlListCompany.SelectedValue == Contants.UploadExternalTypeStandard)
                        {
                            // Std Insert To Input Profile
                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileDataFromFileToInput.dtsx");
                            bool stdProInputResult = StandardProfileUploadBo.StdInsertToInputProfile(packagePath, fileName, configPath);
                            if (stdProInputResult)
                            {
                                processlogVo.IsInsertionToInputComplete = 1;
                                processlogVo.IsInsertionToXtrnlComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog1)
                                {
                                    // Std Insert To First Staging Profile
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileInputDataToFirstStaging.dtsx");
                                    stdProFirstStagingResult = StandardProfileUploadBo.StdInsertToFirstStaging(UploadProcessId, packagePath, configPath);
                                    if (stdProFirstStagingResult)
                                    {
                                        processlogVo.IsInsertionToFirstStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog2)
                                        {
                                            // Data translation checks
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsDataTranslationChecksInStdProfileMainStaging.dtsx");
                                            bool stdProTranslationCheckStagingResult = StandardProfileUploadBo.StdDataTranslationCheckInFirstStaging(UploadProcessId, packagePath, configPath);
                                            if (stdProTranslationCheckStagingResult)
                                            {
                                                // Insertion to common staging
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileFirstStagingToCommonStaging.dtsx");
                                                stdProCommonStagingResult = StandardProfileUploadBo.StdInsertToCommonStaging(UploadProcessId, packagePath, configPath);
                                                if (stdProCommonStagingResult)
                                                {
                                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                    processlogVo.EndTime = DateTime.Now;
                                                    bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    if (updateProcessLog3)
                                                    {
                                                        //common profile checks
                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                        bool stdProCommonChecksResult = StandardProfileUploadBo.StdCommonProfileChecks(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                                        if (stdProCommonChecksResult)
                                                        {
                                                            // Insert Customer Details into WERP Tables
                                                            stdProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserVo.advisorId, UploadProcessId, rmVo.RMId, int.Parse(ddlListBranch.SelectedValue.ToString()), xmlPath, out countCustCreated);
                                                            if (stdProCreateCustomerResult)
                                                            {
                                                                //Create new Bank Accounts
                                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                                stdProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(UploadProcessId, packagePath, configPath);
                                                                if (stdProCreateBankAccountResult)
                                                                {

                                                                    processlogVo.NoOfCustomerInserted = countCustCreated;
                                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                                    processlogVo.EndTime = DateTime.Now;
                                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(UploadProcessId, "WP");
                                                                    bool updateProcessLog4 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                    if (updateProcessLog4)
                                                                    {
                                                                        stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(UploadProcessId);
                                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(UploadProcessId, "WP");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            // Update Process Progress Monitoring Text Boxes
                            //txtProcessID.Text = processlogVo.ProcessId.ToString();

                            if (XmlCreated)
                                XMLProgress = "Done";
                            else
                                XMLProgress = "Failure";

                            if (stdProInputResult)
                            {
                                XtrnlInsertionProgress = "NA";
                                InputInsertionProgress = "Done";
                            }
                            else
                            {
                                InputInsertionProgress = "Failure";
                                XtrnlInsertionProgress = "1";
                            }

                            if (stdProFirstStagingResult)
                                FirstStagingInsertionProgress = "Done";
                            else
                                FirstStagingInsertionProgress = "Failure";

                            if (stdProCommonStagingResult)
                                SecondStagingInsertionProgress = "Done";
                            else
                                SecondStagingInsertionProgress = "Failure";

                            if (stdProCreateCustomerResult)
                                WERPInsertionProgress = "Done";
                            else
                                WERPInsertionProgress = "Failure";

                            // Update Process Summary Text Boxes
                            txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                            txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                            txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                            txtUploadedRecords.Text = processlogVo.NoOfCustomerInserted.ToString();
                            txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords - processlogVo.NoOfInputRejects;
                            Session[SessionContents.ProcessLogVo] = processlogVo;
                            if (XMLProgress == "Done" && InputInsertionProgress == "Done" && FirstStagingInsertionProgress == "Done" && SecondStagingInsertionProgress == "Done" && WERPInsertionProgress == "Done")
                            {
                                msgUploadComplete.Visible = true;
                            }
                        }
                        #endregion Standard Profile Upload

                        #region MF CAMS Profile Upload
                        //*****************************************************************************************************************************
                        //MF CAMS Profile Upload
                        else if ((ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio) && ddlListCompany.SelectedValue == Contants.UploadExternalTypeCAMS)
                        {
                            // CAMS Insert To Input Profile
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSFileToXtrnlProfileInput.dtsx");
                            bool camsProInputResult = camsUploadsBo.CAMSInsertToInputProfile(UploadProcessId, packagePath, fileName, configPath);
                            if (camsProInputResult)
                            {
                                processlogVo.IsInsertionToInputComplete = 1;
                                processlogVo.IsInsertionToXtrnlComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog1)
                                {
                                    // CAMS Insert To Staging Profile
                                    packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                                    camsProStagingResult = camsUploadsBo.CAMSInsertToStagingProfile(UploadProcessId, packagePath, configPath);
                                    if (camsProStagingResult)
                                    {
                                        processlogVo.IsInsertionToFirstStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog2)
                                        {
                                            // Doing a check on data translation
                                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadDataTranslationChecksFirstStaging.dtsx");
                                            camsProStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingProfile(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                            if (camsProStagingCheckResult)
                                            {
                                                if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile)
                                                {
                                                    // Insertion to common staging
                                                    packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                                    camsProCommonStagingResult = camsUploadsBo.CAMSInsertToCommonStaging(UploadProcessId, packagePath, configPath);
                                                    if (camsProCommonStagingResult)
                                                    {
                                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (updateProcessLog3)
                                                        {
                                                            //common profile checks
                                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                            camsProCommonChecksResult = StandardProfileUploadBo.StdCommonProfileChecks(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                                            // Insert Customer Details into WERP Tables
                                                            if (camsProCommonChecksResult)
                                                                camsProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserVo.advisorId, UploadProcessId, rmVo.RMId, int.Parse(ddlListBranch.SelectedValue.ToString()), xmlPath, out countCustCreated);
                                                            if (camsProCreateCustomerResult)
                                                            {
                                                                //Create new Bank Accounts
                                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                                bool camsProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(UploadProcessId, packagePath, configPath);
                                                                if (camsProCreateBankAccountResult)
                                                                {
                                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                                    processlogVo.EndTime = DateTime.Now;
                                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(UploadProcessId, "CA");
                                                                    processlogVo.NoOfCustomerInserted = countCustCreated;
                                                                    txtUploadedRecords.Text = processlogVo.NoOfCustomerInserted.ToString();
                                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(UploadProcessId, "CA");
                                                                    processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords - processlogVo.NoOfInputRejects;

                                                                    //processlogVo.NoOfAccountsInserted = countFolioCreated;
                                                                    bool updateProcessLog4 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                    if (updateProcessLog4)
                                                                        stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(UploadProcessId);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio)
                                    {
                                        if (camsProStagingResult)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadFolioDataFromCAMSStagingToCommonStaging.dtsx");
                                            camsFolioCommonStagingResult = camsUploadsBo.CAMSInsertFolioDataToFolioCommonStaging(UploadProcessId, packagePath, configPath);
                                            if (camsFolioCommonStagingResult)
                                            {
                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                                bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserVo.advisorId, UploadProcessId, configPath);
                                                if (camsFolioStagingChkResult)
                                                {
                                                    //Move Folio data to WERP table
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                    camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserVo.advisorId, UploadProcessId, configPath);
                                                    if (camsFolioWerpInsertionResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(UploadProcessId, "CA");
                                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(UploadProcessId, "WPMF");
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(UploadProcessId, "CA");
                                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                        txtUploadedRecords.Text = processlogVo.NoOfAccountsInserted.ToString();

                                                        bool updateProcessLog4 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (updateProcessLog4)
                                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(UploadProcessId);
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }


                            // Update Process Progress Monitoring Text Boxes
                            // Commented for Removing Process Progress Monitoring 
                            //txtProcessID.Text = processlogVo.ProcessId.ToString();

                            if (XmlCreated)
                                XMLProgress = "Done";
                            else
                                XMLProgress = "Failure";

                            if (camsProInputResult)
                            {
                                XtrnlInsertionProgress = "Done";
                                InputInsertionProgress = "Done";
                            }
                            else
                            {
                                XtrnlInsertionProgress = "Failure";
                                InputInsertionProgress = "Failure";
                            }

                            if (camsProStagingResult)
                                FirstStagingInsertionProgress = "Done";
                            else
                                FirstStagingInsertionProgress = "Failure";

                            if (camsProCommonStagingResult == true || camsFolioCommonStagingResult == true)
                                SecondStagingInsertionProgress = "Done";
                            else
                                SecondStagingInsertionProgress = "Failure";

                            if (camsProCreateCustomerResult == true || camsFolioWerpInsertionResult == true)
                                WERPInsertionProgress = "Done";
                            else
                                WERPInsertionProgress = "Failure";
                            // Up to here 

                            // Update Process Summary Text Boxes
                            txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                            txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                            txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                            //txtUploadedRecords.Text = processlogVo.NoOfAccountsInserted.ToString();
                            txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                            Session[SessionContents.ProcessLogVo] = processlogVo;
                        }
                        #endregion MF CAMS Profile Upload





                        #region MF Deutsche Transaction Upload

                        else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeDeutsche)
                        {
                            bool updateProcessLog = false;
                            bool deutscheTranWerpResult = false;
                            bool deutscheTranStagingCheckResult = false;
                            bool deutscheTranStagingResult = false;
                            bool deutscheTranInputResult = false;
                            bool deutscheTansSecondStaginresult = false;
                            bool CommonTransChecks = false;

                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionXMLFileToInputTable.dtsx");
                            deutscheTranInputResult = deutscheUploadsBo.DeutscheInsertToInputTrans(UploadProcessId, packagePath, fileName, configPath);

                            if (deutscheTranInputResult)
                            {
                                processlogVo.IsInsertionToInputComplete = 1;
                                processlogVo.IsInsertionToXtrnlComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                if (updateProcessLog)
                                {


                                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionInputToFirstStaging.dtsx");
                                    deutscheTranStagingResult = deutscheUploadsBo.DeutscheInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                    if (deutscheTranStagingResult)
                                    {
                                        processlogVo.IsInsertionToFirstStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadsDeutscheTransactionChecksFirstStaging.dtsx");
                                            deutscheTranStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingTrans(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                            if (deutscheTranStagingCheckResult)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionFirstStagingtoSecondStaging.dtsx");
                                                deutscheTansSecondStaginresult = deutscheUploadsBo.DeutscheTransInsertToCommonTransStaging(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                                if (deutscheTansSecondStaginresult)
                                                {
                                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                    processlogVo.EndTime = DateTime.Now;
                                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    if (updateProcessLog)
                                                    {
                                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                                        CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserVo.advisorId, UploadProcessId, packagePath, configPath, "DT", "Deutsche");
                                                        if (CommonTransChecks)
                                                        {
                                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                            deutscheTranWerpResult = uploadsCommonBo.InsertTransToWERP(UploadProcessId, packagePath, configPath);
                                                            if (deutscheTranWerpResult)
                                                            {
                                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(UploadProcessId, "WPMF");
                                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(UploadProcessId, Contants.UploadExternalTypeCAMS);
                                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadTransactionInputRejectCount(UploadProcessId, "DT");
                                                                processlogVo.NoOfTransactionDuplicates = 0;
                                                                processlogVo.EndTime = DateTime.Now;
                                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            // Update Process Progress Monitoring Text Boxes
                            //txtProcessID.Text = processlogVo.ProcessId.ToString();

                            if (XmlCreated)
                                XMLProgress = "Done";
                            else
                                XMLProgress = "Failure";

                            if (deutscheTranInputResult)
                            {
                                XtrnlInsertionProgress = "Done";
                                InputInsertionProgress = "Done";
                            }
                            else
                            {
                                InputInsertionProgress = "Failure";
                                XtrnlInsertionProgress = "Failure";
                            }

                            if (deutscheTranStagingResult)
                                FirstStagingInsertionProgress = "Done";
                            else
                                FirstStagingInsertionProgress = "Failure";

                            if (deutscheTranStagingCheckResult)
                                SecondStagingInsertionProgress = "Done";
                            else
                                SecondStagingInsertionProgress = "Failure";

                            if (CommonTransChecks && deutscheTranWerpResult)
                            {
                                WERPInsertionProgress = "Done";

                            }
                            else
                                WERPInsertionProgress = "Failure";

                            if (deutscheTranWerpResult)
                                XtrnlInsertionProgress = "Done";
                            else
                                XtrnlInsertionProgress = "Failure";

                            // Update Process Summary Text Boxes
                            txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                            txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                            txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                            txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();

                            txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                            Session[SessionContents.ProcessLogVo] = processlogVo;
                        }



                        #endregion MF Deutsche Transaction Upload

                        #region MF Templeton Profile Upload
                        //*****************************************************************************************************************************
                        //MF Templeton Profile Upload
                        else if ((ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio) && ddlListCompany.SelectedValue == Contants.UploadExternalTypeTemp)
                        {
                            // Templeton Insert To Input Profile
                            packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadTempletonProfileDataFromFileToInput.dtsx");
                            bool templetonProInputResult = templetonUploadsBo.TempInsertToInputProfile(UploadProcessId, packagePath, fileName, configPath);
                            if (templetonProInputResult)
                            {
                                processlogVo.IsInsertionToInputComplete = 1;
                                processlogVo.IsInsertionToXtrnlComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog1)
                                {
                                    // Templeton Insert To Staging Profile
                                    packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadTempXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                                    templetonProStagingResult = templetonUploadsBo.TempInsertToStagingProfile(UploadProcessId, packagePath, configPath);
                                    if (templetonProStagingResult)
                                    {
                                        processlogVo.IsInsertionToFirstStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog2)
                                        {
                                            // Doing a check on data translation
                                            packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadTempDataTranslationChecksFirstStaging.dtsx");
                                            templetonProStagingCheckResult = templetonUploadsBo.TempProcessDataInStagingProfile(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                            if (templetonProStagingCheckResult)
                                            {
                                                if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile)
                                                {
                                                    // Insertion to common staging
                                                    packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadProfileDataFromTempStagingToCommonStaging.dtsx");
                                                    templetonProCommonStagingResult = templetonUploadsBo.TempInsertToCommonStaging(UploadProcessId, packagePath, configPath);
                                                    if (templetonProCommonStagingResult)
                                                    {
                                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (updateProcessLog3)
                                                        {
                                                            //common profile checks
                                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                            templetonProCommonChecksResult = StandardProfileUploadBo.StdCommonProfileChecks(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                                            if (templetonProCommonChecksResult)
                                                            {
                                                                // Insert Customer Details into WERP Tables
                                                                templetonProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserVo.advisorId, UploadProcessId, rmVo.RMId, int.Parse(ddlListBranch.SelectedValue.ToString()), xmlPath, out countCustCreated);
                                                                if (templetonProCreateCustomerResult)
                                                                {
                                                                    //Create new Bank Accounts
                                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                                    bool templetonProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(UploadProcessId, packagePath, configPath);
                                                                    if (templetonProCreateBankAccountResult)
                                                                    {
                                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                                        processlogVo.EndTime = DateTime.Now;
                                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(UploadProcessId, "TN");
                                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                                        txtUploadedRecords.Text = processlogVo.NoOfCustomerInserted.ToString();
                                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(UploadProcessId, "TN");
                                                                        processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords - processlogVo.NoOfInputRejects;
                                                                        bool updateProcessLog4 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                        if (updateProcessLog4)
                                                                            stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(UploadProcessId);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio)
                                    {
                                        if (templetonProStagingResult)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadFolioDataFromTempStagingToCommonStaging.dtsx");
                                            templetonFolioCommonStagingResult = templetonUploadsBo.TempInsertFolioDataToFolioCommonStaging(UploadProcessId, packagePath, configPath);
                                            if (templetonFolioCommonStagingResult)
                                            {
                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                                bool templetonFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserVo.advisorId, UploadProcessId, configPath);
                                                if (templetonFolioStagingChkResult)
                                                {
                                                    //Folio Chks in Std Folio Staging 
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                    templetonFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserVo.advisorId, UploadProcessId, configPath);
                                                    if (templetonFolioWerpInsertionResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(UploadProcessId, "TN");
                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(UploadProcessId, "WPMF");
                                                        txtUploadedRecords.Text = processlogVo.NoOfAccountsInserted.ToString();
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(UploadProcessId, "TN");
                                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;

                                                        bool updateProcessLog4 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (updateProcessLog4)
                                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(UploadProcessId);
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }


                            // Update Process Progress Monitoring Text Boxes
                            //txtProcessID.Text = processlogVo.ProcessId.ToString();

                            if (XmlCreated)
                                XMLProgress = "Done";
                            else
                                XMLProgress = "Failure";

                            if (templetonProInputResult)
                            {
                                XtrnlInsertionProgress = "Done";
                                InputInsertionProgress = "Done";
                            }
                            else
                            {
                                XtrnlInsertionProgress = "Failure";
                                InputInsertionProgress = "Failure";
                            }

                            if (templetonProStagingResult)
                                FirstStagingInsertionProgress = "Done";
                            else
                                FirstStagingInsertionProgress = "Failure";

                            if (templetonProCommonStagingResult == true || templetonFolioCommonStagingResult == true)
                                SecondStagingInsertionProgress = "Done";
                            else
                                SecondStagingInsertionProgress = "Failure";

                            if (templetonProCreateCustomerResult == true || templetonFolioWerpInsertionResult == true)
                                WERPInsertionProgress = "Done";
                            else
                                WERPInsertionProgress = "Failure";

                            // Update Process Summary Text Boxes
                            txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                            txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                            txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                            //txtUploadedRecords.Text = processlogVo.NoOfAccountsInserted.ToString();
                            txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                            Session[SessionContents.ProcessLogVo] = processlogVo;
                        }
                        #endregion MF Templeton Profile Upload

                        #region MF Deutsche Profile Upload
                        //*****************************************************************************************************************************
                        //MF Deutsche Profile Upload
                        else if ((ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio) && ddlListCompany.SelectedValue == Contants.UploadExternalTypeDeutsche)
                        {
                            // Deutsche Insert To Input Profile
                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheProfileDataFromFileToInput.dtsx");
                            bool deutscheProInputResult = deutscheUploadsBo.DeutscheInsertToInputProfile(UploadProcessId, packagePath, fileName, configPath);
                            if (deutscheProInputResult)
                            {
                                processlogVo.IsInsertionToInputComplete = 1;
                                processlogVo.IsInsertionToXtrnlComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog1)
                                {
                                    // Deutsche Insert To Staging Profile
                                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                                    deutscheProStagingResult = deutscheUploadsBo.DeutscheInsertToStagingProfile(UploadProcessId, packagePath, configPath);
                                    if (deutscheProStagingResult)
                                    {
                                        processlogVo.IsInsertionToFirstStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog2)
                                        {
                                            // Doing a check on data translation
                                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheDataTranslationChecksFirstStaging.dtsx");
                                            deutscheProStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingProfile(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                            if (deutscheProStagingCheckResult)
                                            {
                                                if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile)
                                                {
                                                    // Insertion to common staging
                                                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadProfileDataFromDeutscheStagingToCommonStaging.dtsx");
                                                    deutscheProCommonStagingResult = deutscheUploadsBo.DeutscheInsertToCommonStaging(UploadProcessId, packagePath, configPath);
                                                    if (deutscheProCommonStagingResult)
                                                    {
                                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (updateProcessLog3)
                                                        {
                                                            //common profile checks
                                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                            deutscheProCommonChecksResult = StandardProfileUploadBo.StdCommonProfileChecks(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                                            if (deutscheProCommonChecksResult)
                                                            {
                                                                // Insert Customer Details into WERP Tables
                                                                deutscheProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserVo.advisorId, UploadProcessId, rmVo.RMId, int.Parse(ddlListBranch.SelectedValue.ToString()), xmlPath, out countCustCreated);
                                                                if (deutscheProCreateCustomerResult)
                                                                {
                                                                    //Create new Bank Accounts
                                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                                    bool deutscheProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(UploadProcessId, packagePath, configPath);
                                                                    if (deutscheProCreateBankAccountResult)
                                                                    {
                                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                                        processlogVo.EndTime = DateTime.Now;
                                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(UploadProcessId, "DT");
                                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                                        txtUploadedRecords.Text = processlogVo.NoOfCustomerInserted.ToString();
                                                                        //processlogVo.NoOfAccountsInserted = countFolioCreated;
                                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(UploadProcessId, "DT");
                                                                        processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords - processlogVo.NoOfInputRejects;
                                                                        bool updateProcessLog4 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                        if (updateProcessLog4)
                                                                            stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(UploadProcessId);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio)
                                    {
                                        if (deutscheProStagingResult)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadFolioDataFromDeutscheStagingToCommonStaging.dtsx");
                                            deutscheFolioCommonStagingResult = deutscheUploadsBo.DeutscheInsertFolioDataToFolioCommonStaging(UploadProcessId, packagePath, configPath);
                                            if (deutscheFolioCommonStagingResult)
                                            {
                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                                bool deutscheFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserVo.advisorId, UploadProcessId, configPath);
                                                if (deutscheFolioStagingChkResult)
                                                {
                                                    //Folio Chks in Std Folio Staging 
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                    deutscheFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserVo.advisorId, UploadProcessId, configPath);
                                                    if (deutscheFolioWerpInsertionResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(UploadProcessId, "DT");
                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(UploadProcessId, "WPMF");
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(UploadProcessId, "DT");
                                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                        txtUploadedRecords.Text = processlogVo.NoOfAccountsInserted.ToString();
                                                        bool updateProcessLog4 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (updateProcessLog4)
                                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(UploadProcessId);
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }


                            // Update Process Progress Monitoring Text Boxes
                            //txtProcessID.Text = processlogVo.ProcessId.ToString();

                            if (XmlCreated)
                                XMLProgress = "Done";
                            else
                                XMLProgress = "Failure";

                            if (deutscheProInputResult)
                            {
                                XtrnlInsertionProgress = "Done";
                                InputInsertionProgress = "Done";
                            }
                            else
                            {
                                XtrnlInsertionProgress = "Failure";
                                InputInsertionProgress = "Failure";
                            }

                            if (deutscheProStagingResult)
                                FirstStagingInsertionProgress = "Done";
                            else
                                FirstStagingInsertionProgress = "Failure";

                            if (deutscheProCommonStagingResult == true || deutscheFolioCommonStagingResult == true)
                                SecondStagingInsertionProgress = "Done";
                            else
                                SecondStagingInsertionProgress = "Failure";

                            if (deutscheProCreateCustomerResult == true || deutscheFolioWerpInsertionResult == true)
                                WERPInsertionProgress = "Done";
                            else
                                WERPInsertionProgress = "Failure";

                            // Update Process Summary Text Boxes
                            txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                            txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                            txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                            //txtUploadedRecords.Text = processlogVo.NoOfAccountsInserted.ToString();
                            txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                            Session[SessionContents.ProcessLogVo] = processlogVo;
                        }
                        #endregion MF Deutsche Profile Upload

                        #region MF CAMS Transaction Upload
                        //*****************************************************************************************************************************
                        //MF CAMS Transaction Upload
                        else
                            if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeCAMS)
                            {
                                bool updateProcessLog = false;
                                bool camsTranWerpResult = false;
                                bool CommonTransChecks = false;
                                bool camsTranStagingCheckResult = false;
                                bool camsTranStagingResult = false;
                                bool camsTranInputResult = false;


                                packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadTransactionDataFromCAMSFileToXtrnlTransactionInput.dtsx");
                                camsTranInputResult = camsUploadsBo.CAMSInsertToInputTrans(UploadProcessId, packagePath, fileName, configPath);
                                if (camsTranInputResult)
                                {
                                    processlogVo.IsInsertionToInputComplete = 1;
                                    processlogVo.IsInsertionToXtrnlComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    processlogVo.IsInsertionToXtrnlComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);


                                    packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadXtrnlTransactionInputToXtrnlTransactionStaging.dtsx");
                                    camsTranStagingResult = camsUploadsBo.CAMSInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                    if (camsTranStagingResult)
                                    {
                                        processlogVo.IsInsertionToFirstStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadChecksCAMSTransactionStaging.dtsx");
                                        camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                        if (camsTranStagingCheckResult)
                                        {

                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                            CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserVo.advisorId, UploadProcessId, packagePath, configPath, "CA", "CAMS");


                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                            camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(UploadProcessId, packagePath, configPath);
                                            if (camsTranWerpResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(UploadProcessId, "WPMF");
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(UploadProcessId, Contants.UploadExternalTypeCAMS);
                                                processlogVo.EndTime = DateTime.Now;
                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadTransactionInputRejectCount(UploadProcessId, "CA");
                                                processlogVo.NoOfTransactionDuplicates = 0;
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            }
                                        }
                                    }


                                }

                                // Update Process Progress Monitoring Text Boxes
                                //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                if (XmlCreated)
                                    XMLProgress = "Done";
                                else
                                    XMLProgress = "Failure";

                                if (camsTranInputResult)
                                {
                                    XtrnlInsertionProgress = "Done";
                                    InputInsertionProgress = "Done";
                                }
                                else
                                {
                                    InputInsertionProgress = "Failure";
                                    XtrnlInsertionProgress = "Failure";
                                }

                                if (camsTranStagingResult)
                                    FirstStagingInsertionProgress = "Done";
                                else
                                    FirstStagingInsertionProgress = "Failure";

                                if (camsTranStagingCheckResult)
                                    SecondStagingInsertionProgress = "Done";
                                else
                                    SecondStagingInsertionProgress = "Failure";

                                if (CommonTransChecks && camsTranWerpResult)
                                {
                                    WERPInsertionProgress = "Done";

                                }
                                else
                                    WERPInsertionProgress = "Failure";

                                if (camsTranWerpResult)
                                    XtrnlInsertionProgress = "Done";
                                else
                                    XtrnlInsertionProgress = "Failure";

                                // Update Process Summary Text Boxes
                                txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();

                                txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                Session[SessionContents.ProcessLogVo] = processlogVo;
                            }


                        #endregion MF CAMS Transaction Upload


                            #region MF Standard Transaction Upload
                            //*****************************************************************************************************************************
                            //MF standard Transaction Upload gobinda
                            else
                                if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction && ddlListCompany.SelectedValue == "WPT")
                                {
                                    bool updateProcessLog = false;
                                    bool standardTranWerpResult = false;
                                    bool CommonTransChecks = false;
                                    bool standardTranStagingCheckResult = false;
                                    bool standardTranStagingResult = false;
                                    bool standardTranInputResult = false;


                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStandardTransactionFolioDataToStandardTransactionFolioInput.dtsx");
                                    standardTranInputResult = camsUploadsBo.StandardFolioAndTrxInsertToInputTrans(UploadProcessId, packagePath, fileName, configPath);
                                    if (standardTranInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);


                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadXtrnlTransactionFolioInputToXtrnlTransactionFolioStaging.dtsx");
                                        standardTranStagingResult = camsUploadsBo.StandardInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                        if (standardTranStagingResult)
                                        {
                                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadChecksStandardTransactionStaging.dtsx");
                                            standardTranStagingCheckResult = camsUploadsBo.StandardProcessDataInStagingTrans(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                            if (standardTranStagingCheckResult)
                                            {

                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsStandardMFTrxnStagingChk.dtsx");
                                                CommonTransChecks = uploadsCommonBo.InsertMFStandardTransToWERP(UploadProcessId, adviserVo.advisorId, packagePath, configPath);

                                                if (CommonTransChecks)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(UploadProcessId, "WP");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(UploadProcessId, Contants.UploadExternalTypeStandard);
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadTransactionInputRejectCount(UploadProcessId, "WP");
                                                    processlogVo.NoOfTransactionDuplicates = 0;
                                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }


                                    }

                                    // Update Process Progress Monitoring Text Boxes
                                    //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                    if (XmlCreated)
                                        XMLProgress = "Done";
                                    else
                                        XMLProgress = "Failure";

                                    if (standardTranInputResult)
                                    {
                                        XtrnlInsertionProgress = "Done";
                                        InputInsertionProgress = "Done";
                                    }
                                    else
                                    {
                                        InputInsertionProgress = "Failure";
                                        XtrnlInsertionProgress = "Failure";
                                    }

                                    if (standardTranStagingResult)
                                        FirstStagingInsertionProgress = "Done";
                                    else
                                        FirstStagingInsertionProgress = "Failure";

                                    if (standardTranStagingCheckResult)
                                        SecondStagingInsertionProgress = "Done";
                                    else
                                        SecondStagingInsertionProgress = "Failure";

                                    if (CommonTransChecks)
                                    {
                                        WERPInsertionProgress = "Done";

                                    }
                                    else
                                        WERPInsertionProgress = "Failure";

                                    //if (standardTranWerpResult)
                                    //    XtrnlInsertionProgress = "Done";
                                    //else
                                    //    XtrnlInsertionProgress = "Failure";

                                    // Update Process Summary Text Boxes
                                    txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                    txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                    txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                    txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();

                                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                    Session[SessionContents.ProcessLogVo] = processlogVo;
                                }


                            #endregion MF Standard Transaction Upload



                                //****************Systematic Uploads KARVY**************\\
                                //*********gobinda Uploads KARVY**************\\

                                #region MF Systematic KARVY Upload
                                //MF KARVY Systematic Upload
                                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFSystematic && ddlListCompany.SelectedValue == "KA")
                                {
                                    bool updateProcessLog = false;
                                    //bool camsSIPWerpResult = false;
                                    //bool CommonTransChecks = false;
                                    bool karvySIPCommonStagingChk = false;
                                    bool karvySIPStagingToCommonStaging = false;
                                    bool karvySIPInputResult = false;
                                    bool karvySIPCommonStaging = false;
                                    bool karvySIPStagingCheckResult = false;
                                    bool karvySIPStagingResult = false;
                                    bool karvySIPCommonStagingToWERP = false;


                                    packagePath = Server.MapPath("\\UploadPackages\\SipKarvyUploads\\SipKarvyUploads\\SipKarvyUploads\\UploadSystematicTransationXMLToInput.dtsx");
                                    karvySIPInputResult = camsUploadsBo.KarvySIPInsertToInputTrans(UploadProcessId, packagePath, fileName, configPath);
                                    if (karvySIPInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);


                                        packagePath = Server.MapPath("\\UploadPackages\\SipKarvyUploads\\SipKarvyUploads\\SipKarvyUploads\\UploadSystematicTransationInputToStaging.dtsx");
                                        karvySIPStagingResult = camsUploadsBo.KarvySIPInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                        if (karvySIPStagingResult)
                                        {
                                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            packagePath = Server.MapPath("\\UploadPackages\\SipKarvyUploads\\SipKarvyUploads\\SipKarvyUploads\\UploadStandardTransactionStagingtoCommonStaging.dtsx");
                                            karvySIPStagingCheckResult = camsUploadsBo.KarvySIPProcessDataInStagingTrans(adviserVo.advisorId, UploadProcessId, packagePath, configPath);
                                            if (karvySIPStagingCheckResult)
                                            {



                                                //packagePath = Server.MapPath("\\UploadPackages\\SipKarvyUploads\\SipKarvyUploads\\SipKarvyUploads\\UploadStandardTransactionStagingCheck.dtsx");
                                                //karvySIPStagingToCommonStaging = camsUploadsBo.KarvySIPStagingToCommonStaging(UploadProcessId, packagePath, configPath);
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingCheck.dtsx");
                                                karvySIPCommonStagingChk = camsUploadsBo.KarvySIPCommonStagingChk(UploadProcessId, packagePath, configPath, "KA");
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(UploadProcessId, "KA");
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (karvySIPCommonStagingChk)
                                                {
                                                    packagePath = Server.MapPath("\\UploadPackages\\SipKarvyUploads\\SipKarvyUploads\\SipKarvyUploads\\UploadStandardTransactionCommonStagingToWERP.dtsx");
                                                    karvySIPCommonStagingToWERP = camsUploadsBo.CamsSIPCommonStagingToWERP(UploadProcessId, packagePath, configPath);

                                                    if (karvySIPCommonStagingToWERP)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(UploadProcessId, "KA");
                                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    }
                                                }
                                            }
                                        }

                                        // Update Process Progress Monitoring Text Boxes
                                        //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                        if (XmlCreated)
                                            XMLProgress = "Done";
                                        else
                                            XMLProgress = "Failure";

                                        if (karvySIPInputResult)
                                        {
                                            XtrnlInsertionProgress = "Done";
                                            InputInsertionProgress = "Done";
                                        }
                                        else
                                        {
                                            InputInsertionProgress = "Failure";
                                            XtrnlInsertionProgress = "Failure";
                                        }

                                        if (karvySIPStagingResult)
                                            FirstStagingInsertionProgress = "Done";
                                        else
                                            FirstStagingInsertionProgress = "Failure";

                                        if (karvySIPCommonStagingChk)
                                            SecondStagingInsertionProgress = "Done";
                                        else
                                            SecondStagingInsertionProgress = "Failure";

                                        if (karvySIPCommonStagingChk && karvySIPCommonStagingToWERP)
                                        {
                                            WERPInsertionProgress = "Done";

                                        }
                                        else
                                            WERPInsertionProgress = "Failure";

                                        if (karvySIPCommonStagingToWERP)
                                            XtrnlInsertionProgress = "Done";
                                        else
                                            XtrnlInsertionProgress = "Failure";

                                        // Update Process Summary Text Boxes
                                        txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                        txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                        txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                        txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();

                                        txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                        Session[SessionContents.ProcessLogVo] = processlogVo;
                                    }
                                }
                                #endregion MF Standard Systematic Upload





                                //****************Templeton Trail Commission Uploads**************\\
                                //*********gobinda Uploads Templeton Trail Commission**************\\

                                #region MF Templeton Trail Commission Upload
                                //MF Templeton Trail Commission Upload
                                else if (ddlUploadType.SelectedValue == "TRAIL" && ddlListCompany.SelectedValue == "Templeton")
                                {
                                    bool updateProcessLog = false;
                                    bool TempletonTrailCommonStagingChk = false;
                                    bool TempletonTrailStagingToCommonStaging = false;
                                    bool TempletonTrailInputResult = false;
                                    bool TempletonTrailCommonStaging = false;
                                    bool TempletonTrailStagingCheckResult = false;
                                    bool TempletonTrailStagingResult = false;
                                    bool TempletonTrailCommonStagingToWERP = false;

                                    packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\xmltotempletoninput.dtsx");
                                    TempletonTrailInputResult = camsUploadsBo.TempletonTrailCommissionInsertToInputTrans(UploadProcessId, packagePath, fileName, configPath);
                                    if (TempletonTrailInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);


                                        packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\xtrnlToStaging.dtsx");
                                        TempletonTrailStagingResult = camsUploadsBo.TempletonTrailCommissionInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                        if (TempletonTrailStagingResult)
                                        {
                                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\stagingToCommonStaging.dtsx");
                                            TempletonTrailStagingCheckResult = camsUploadsBo.TempletonTrailCommissionProcessDataInStagingTrans(adviserVo.advisorId, UploadProcessId, packagePath, configPath);
                                            if (TempletonTrailStagingCheckResult)
                                            {
                                                //packagePath = Server.MapPath("\\UploadPackages\\SipKarvyUploads\\SipKarvyUploads\\SipKarvyUploads\\UploadStandardTransactionStagingCheck.dtsx");
                                                //karvySIPStagingToCommonStaging = camsUploadsBo.KarvySIPStagingToCommonStaging(UploadProcessId, packagePath, configPath);
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                //packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                                                //if (TempletonTrailStagingCheckResult)
                                                //{
                                                packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                                                TempletonTrailCommonStagingChk = camsUploadsBo.TempletonTrailCommissionCommonStagingChk(UploadProcessId, packagePath, configPath, "TN");
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(UploadProcessId, "TN");
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                //TempletonTrailCommonStagingToWERP = camsUploadsBo.TempletonTrailCommissionCommonStagingToWERP(UploadProcessId, packagePath, configPath);

                                                if (TempletonTrailCommonStagingChk)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(UploadProcessId, "TN");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCountForTrail(UploadProcessId, "TN");
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadTransactionInputRejectCount(UploadProcessId, "TN");
                                                    processlogVo.NoOfTransactionDuplicates = 0;
                                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                    //processlogVo.IsInsertionToWERPComplete = 1;
                                                    //processlogVo.EndTime = DateTime.Now;
                                                    //processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(UploadProcessId, "TN");
                                                    //updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                                //}
                                            }
                                        }

                                        // Update Process Progress Monitoring Text Boxes
                                        //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                        if (XmlCreated)
                                            XMLProgress = "Done";
                                        else
                                            XMLProgress = "Failure";

                                        if (TempletonTrailInputResult)
                                        {
                                            XtrnlInsertionProgress = "Done";
                                            InputInsertionProgress = "Done";
                                        }
                                        else
                                        {
                                            InputInsertionProgress = "Failure";
                                            XtrnlInsertionProgress = "Failure";
                                        }

                                        if (TempletonTrailStagingResult)
                                            FirstStagingInsertionProgress = "Done";
                                        else
                                            FirstStagingInsertionProgress = "Failure";

                                        if (TempletonTrailCommonStagingChk)
                                            SecondStagingInsertionProgress = "Done";
                                        else
                                            SecondStagingInsertionProgress = "Failure";

                                        if (TempletonTrailCommonStagingChk && TempletonTrailCommonStagingToWERP)
                                        {
                                            WERPInsertionProgress = "Done";

                                        }
                                        else
                                            WERPInsertionProgress = "Failure";

                                        if (TempletonTrailCommonStagingToWERP)
                                            XtrnlInsertionProgress = "Done";
                                        else
                                            XtrnlInsertionProgress = "Failure";

                                        // Update Process Summary Text Boxes
                                        txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                        txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                        txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                        txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();

                                        txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                        Session[SessionContents.ProcessLogVo] = processlogVo;
                                    }
                                }
                                #endregion MF Templeton Trail Commission Upload



                                //****************CAMS Trail Commission Uploads**************\\
                                //*********gobinda Uploads CAMS Trail Commission**************\\

                                #region MF CAMS Trail Commission Upload
                                //MF CAMS Trail Commission Upload
                                else if (ddlUploadType.SelectedValue == "TRAIL" && ddlListCompany.SelectedValue == "CAMS")
                                {
                                    bool updateProcessLog = false;
                                    bool CAMSTrailCommonStagingChk = false;
                                    bool CAMSTrailStagingToCommonStaging = false;
                                    bool CAMSTrailInputResult = false;
                                    bool CAMSTrailCommonStaging = false;
                                    bool CAMSTrailStagingCheckResult = false;
                                    bool CAMSTrailStagingResult = false;
                                    bool CAMSTrailCommonStagingToWERP = false;

                                    packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\xmltoCamsinput.dtsx");
                                    CAMSTrailInputResult = camsUploadsBo.CAMSTrailCommissionInsertToInputTrans(UploadProcessId, packagePath, fileName, configPath);
                                    if (CAMSTrailInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);


                                        packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\XtrnlToCamsStaging.dtsx");
                                        CAMSTrailStagingResult = camsUploadsBo.CAMSTrailCommissionInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                        if (CAMSTrailStagingResult)
                                        {
                                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\stagingToCamsCommonStaging.dtsx");
                                            CAMSTrailStagingCheckResult = camsUploadsBo.CAMSTrailCommissionProcessDataInStagingTrans(adviserVo.advisorId, UploadProcessId, packagePath, configPath);
                                            if (CAMSTrailStagingCheckResult)
                                            {
                                                //packagePath = Server.MapPath("\\UploadPackages\\SipKarvyUploads\\SipKarvyUploads\\SipKarvyUploads\\UploadStandardTransactionStagingCheck.dtsx");
                                                //karvySIPStagingToCommonStaging = camsUploadsBo.KarvySIPStagingToCommonStaging(UploadProcessId, packagePath, configPath);
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                //packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                                                //if (TempletonTrailStagingCheckResult)
                                                //{
                                                packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                                                CAMSTrailCommonStagingChk = camsUploadsBo.CAMSTrailCommissionCommonStagingChk(UploadProcessId, packagePath, configPath, "CA");
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(UploadProcessId, "CA");
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                //TempletonTrailCommonStagingToWERP = camsUploadsBo.TempletonTrailCommissionCommonStagingToWERP(UploadProcessId, packagePath, configPath);

                                                if (CAMSTrailCommonStagingChk)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(UploadProcessId, "CA");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCountForTrail(UploadProcessId, "CA");
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadTransactionInputRejectCount(UploadProcessId, "CA");
                                                    processlogVo.NoOfTransactionDuplicates = 0;
                                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                    //processlogVo.IsInsertionToWERPComplete = 1;
                                                    //processlogVo.EndTime = DateTime.Now;
                                                    //processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(UploadProcessId, "TN");
                                                    //updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                                //}
                                            }
                                        }

                                        // Update Process Progress Monitoring Text Boxes
                                        //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                        if (XmlCreated)
                                            XMLProgress = "Done";
                                        else
                                            XMLProgress = "Failure";

                                        if (CAMSTrailInputResult)
                                        {
                                            XtrnlInsertionProgress = "Done";
                                            InputInsertionProgress = "Done";
                                        }
                                        else
                                        {
                                            InputInsertionProgress = "Failure";
                                            XtrnlInsertionProgress = "Failure";
                                        }

                                        if (CAMSTrailStagingResult)
                                            FirstStagingInsertionProgress = "Done";
                                        else
                                            FirstStagingInsertionProgress = "Failure";

                                        if (CAMSTrailCommonStagingChk)
                                            SecondStagingInsertionProgress = "Done";
                                        else
                                            SecondStagingInsertionProgress = "Failure";

                                        if (CAMSTrailCommonStagingChk && CAMSTrailCommonStagingToWERP)
                                        {
                                            WERPInsertionProgress = "Done";

                                        }
                                        else
                                            WERPInsertionProgress = "Failure";

                                        if (CAMSTrailCommonStagingToWERP)
                                            XtrnlInsertionProgress = "Done";
                                        else
                                            XtrnlInsertionProgress = "Failure";

                                        // Update Process Summary Text Boxes
                                        txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                        txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                        txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                        txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();

                                        txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                        Session[SessionContents.ProcessLogVo] = processlogVo;
                                    }
                                }
                                #endregion MF CAMS Trail Commission Upload



                                //****************KARVY Trail Commission Uploads**************\\
                                //*********gobinda Uploads KARVY Trail Commission**************\\

                                #region MF KARVY Trail Commission Upload
                                //MF KARVY Trail Commission Upload
                                else if (ddlUploadType.SelectedValue == "TRAIL" && ddlListCompany.SelectedValue == "KARVY")
                                {
                                    bool updateProcessLog = false;
                                    bool KARVYTrailCommonStagingChk = false;
                                    bool KARVYTrailStagingToCommonStaging = false;
                                    bool KARVYTrailInputResult = false;
                                    bool KARVYTrailCommonStaging = false;
                                    bool KARVYTrailStagingCheckResult = false;
                                    bool KARVYTrailStagingResult = false;
                                    bool KARVYTrailCommonStagingToWERP = false;

                                    packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\xmlToKarvyInput.dtsx");
                                    KARVYTrailInputResult = camsUploadsBo.KARVYTrailCommissionInsertToInputTrans(UploadProcessId, packagePath, fileName, configPath);
                                    if (KARVYTrailInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);


                                        packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\xtrnlToKarvyStaging.dtsx");
                                        KARVYTrailStagingResult = camsUploadsBo.KARVYTrailCommissionInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                        if (KARVYTrailStagingResult)
                                        {
                                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\stagingToKarvyCommonStaging.dtsx");
                                            KARVYTrailStagingCheckResult = camsUploadsBo.KARVYTrailCommissionProcessDataInStagingTrans(adviserVo.advisorId, UploadProcessId, packagePath, configPath);
                                            if (KARVYTrailStagingCheckResult)
                                            {
                                                //packagePath = Server.MapPath("\\UploadPackages\\SipKarvyUploads\\SipKarvyUploads\\SipKarvyUploads\\UploadStandardTransactionStagingCheck.dtsx");
                                                //karvySIPStagingToCommonStaging = camsUploadsBo.KarvySIPStagingToCommonStaging(UploadProcessId, packagePath, configPath);
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                //packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                                                //if (TempletonTrailStagingCheckResult)
                                                //{
                                                packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                                                KARVYTrailCommonStagingChk = camsUploadsBo.KARVYTrailCommissionCommonStagingChk(UploadProcessId, packagePath, configPath, "KA");
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(UploadProcessId, "KA");
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                //TempletonTrailCommonStagingToWERP = camsUploadsBo.TempletonTrailCommissionCommonStagingToWERP(UploadProcessId, packagePath, configPath);

                                                if (KARVYTrailCommonStagingChk)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(UploadProcessId, "KA");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCountForTrail(UploadProcessId, "KA");
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadTransactionInputRejectCount(UploadProcessId, "KA");
                                                    processlogVo.NoOfTransactionDuplicates = 0;
                                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                    //processlogVo.IsInsertionToWERPComplete = 1;
                                                    //processlogVo.EndTime = DateTime.Now;
                                                    //processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(UploadProcessId, "TN");
                                                    //updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                                //}
                                            }
                                        }

                                        // Update Process Progress Monitoring Text Boxes
                                        //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                        if (XmlCreated)
                                            XMLProgress = "Done";
                                        else
                                            XMLProgress = "Failure";

                                        if (KARVYTrailInputResult)
                                        {
                                            XtrnlInsertionProgress = "Done";
                                            InputInsertionProgress = "Done";
                                        }
                                        else
                                        {
                                            InputInsertionProgress = "Failure";
                                            XtrnlInsertionProgress = "Failure";
                                        }

                                        if (KARVYTrailStagingResult)
                                            FirstStagingInsertionProgress = "Done";
                                        else
                                            FirstStagingInsertionProgress = "Failure";

                                        if (KARVYTrailCommonStagingChk)
                                            SecondStagingInsertionProgress = "Done";
                                        else
                                            SecondStagingInsertionProgress = "Failure";

                                        if (KARVYTrailCommonStagingChk && KARVYTrailCommonStagingToWERP)
                                        {
                                            WERPInsertionProgress = "Done";

                                        }
                                        else
                                            WERPInsertionProgress = "Failure";

                                        if (KARVYTrailCommonStagingToWERP)
                                            XtrnlInsertionProgress = "Done";
                                        else
                                            XtrnlInsertionProgress = "Failure";

                                        // Update Process Summary Text Boxes
                                        txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                        txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                        txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                        txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();

                                        txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                        Session[SessionContents.ProcessLogVo] = processlogVo;
                                    }
                                }
                                #endregion MF KARVY Trail Commission Upload



                                //****************Deutsche Trail Commission Uploads**************\\
                                //*********gobinda Uploads Deutsche Trail Commission**************\\

                                #region MF Deutsche Trail Commission Upload
                                //MF Deutsche Trail Commission Upload
                                else if (ddlUploadType.SelectedValue == "TRAIL" && ddlListCompany.SelectedValue == "Deutsche")
                                {
                                    bool updateProcessLog = false;
                                    bool DeutscheTrailCommonStagingChk = false;
                                    bool DeutscheTrailStagingToCommonStaging = false;
                                    bool DeutscheTrailInputResult = false;
                                    bool DeutscheTrailCommonStaging = false;
                                    bool DeutscheTrailStagingCheckResult = false;
                                    bool DeutscheTrailStagingResult = false;
                                    bool DeutscheTrailCommonStagingToWERP = false;

                                    packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\xmlToDetscheInput.dtsx");
                                    DeutscheTrailInputResult = camsUploadsBo.DeutscheTrailCommissionInsertToInputTrans(UploadProcessId, packagePath, fileName, configPath);
                                    if (DeutscheTrailInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);


                                        packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\xtrnlToDeutscheStaging.dtsx");
                                        DeutscheTrailStagingResult = camsUploadsBo.DeutscheTrailCommissionInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                        if (DeutscheTrailStagingResult)
                                        {
                                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\stagingToDeutscheCommonStaging.dtsx");
                                            DeutscheTrailStagingCheckResult = camsUploadsBo.DeutscheTrailCommissionProcessDataInStagingTrans(adviserVo.advisorId, UploadProcessId, packagePath, configPath);
                                            if (DeutscheTrailStagingCheckResult)
                                            {
                                                //packagePath = Server.MapPath("\\UploadPackages\\SipKarvyUploads\\SipKarvyUploads\\SipKarvyUploads\\UploadStandardTransactionStagingCheck.dtsx");
                                                //karvySIPStagingToCommonStaging = camsUploadsBo.KarvySIPStagingToCommonStaging(UploadProcessId, packagePath, configPath);
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                //packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                                                //if (TempletonTrailStagingCheckResult)
                                                //{
                                                packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                                                DeutscheTrailCommonStagingChk = camsUploadsBo.DeutscheTrailCommissionCommonStagingChk(UploadProcessId, packagePath, configPath, "DT");
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(UploadProcessId, "DT");
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                //TempletonTrailCommonStagingToWERP = camsUploadsBo.TempletonTrailCommissionCommonStagingToWERP(UploadProcessId, packagePath, configPath);

                                                if (DeutscheTrailCommonStagingChk)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(UploadProcessId, "DT");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCountForTrail(UploadProcessId, "DT");
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadTransactionInputRejectCount(UploadProcessId, "DT");
                                                    processlogVo.NoOfTransactionDuplicates = 0;
                                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                    //processlogVo.IsInsertionToWERPComplete = 1;
                                                    //processlogVo.EndTime = DateTime.Now;
                                                    //processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(UploadProcessId, "TN");
                                                    //updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                                //}
                                            }
                                        }

                                        // Update Process Progress Monitoring Text Boxes
                                        //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                        if (XmlCreated)
                                            XMLProgress = "Done";
                                        else
                                            XMLProgress = "Failure";

                                        if (DeutscheTrailInputResult)
                                        {
                                            XtrnlInsertionProgress = "Done";
                                            InputInsertionProgress = "Done";
                                        }
                                        else
                                        {
                                            InputInsertionProgress = "Failure";
                                            XtrnlInsertionProgress = "Failure";
                                        }

                                        if (DeutscheTrailStagingResult)
                                            FirstStagingInsertionProgress = "Done";
                                        else
                                            FirstStagingInsertionProgress = "Failure";

                                        if (DeutscheTrailCommonStagingChk)
                                            SecondStagingInsertionProgress = "Done";
                                        else
                                            SecondStagingInsertionProgress = "Failure";

                                        if (DeutscheTrailCommonStagingChk && DeutscheTrailCommonStagingToWERP)
                                        {
                                            WERPInsertionProgress = "Done";

                                        }
                                        else
                                            WERPInsertionProgress = "Failure";

                                        if (DeutscheTrailCommonStagingToWERP)
                                            XtrnlInsertionProgress = "Done";
                                        else
                                            XtrnlInsertionProgress = "Failure";

                                        // Update Process Summary Text Boxes
                                        txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                        txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                        txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                        txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();

                                        txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                        Session[SessionContents.ProcessLogVo] = processlogVo;
                                    }
                                }
                                #endregion MF Deutsche Trail Commission Upload



                                    //****************Systematic Uploads**************\\
                                //********************Shantanu**********************\\

                                #region MF CAMS Systematic Upload
                                //MF CAMS Systematic Upload
                                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFSystematic && ddlListCompany.SelectedValue == Contants.UploadExternalTypeCAMS)
                                {
                                    bool updateProcessLog = false;
                                    //bool camsSIPWerpResult = false;
                                    //bool CommonTransChecks = false;
                                    bool camsSIPCommonStagingChk = false;
                                    bool camsSIPStagingToCommonStaging = false;
                                    bool camsSIPInputResult = false;
                                    bool camsSIPCommonStaging = false;
                                    bool camsSIPStagingCheckResult = false;
                                    bool camsSIPStagingResult = false;
                                    bool camsSIPCommonStagingToWERP = false;


                                    packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPFileToInput.dtsx");
                                    camsSIPInputResult = camsUploadsBo.CAMSSIPInsertToInputTrans(UploadProcessId, packagePath, fileName, configPath);
                                    if (camsSIPInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);


                                        packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPInputToStaging.dtsx");
                                        camsSIPStagingResult = camsUploadsBo.CAMSSIPInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                        if (camsSIPStagingResult)
                                        {
                                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPStagingCheck.dtsx");
                                            camsSIPStagingCheckResult = camsUploadsBo.CAMSSIPProcessDataInStagingTrans(UploadProcessId, packagePath, configPath);
                                            if (camsSIPStagingCheckResult)
                                            {



                                                packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPStagingtoCommonStaging.dtsx");
                                                camsSIPStagingToCommonStaging = camsUploadsBo.CamsSIPStagingToCommonStaging(UploadProcessId, packagePath, configPath);
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingCheck.dtsx");
                                                camsSIPCommonStagingChk = camsUploadsBo.CamsSIPCommonStagingChk(UploadProcessId, packagePath, configPath, "CA");
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(UploadProcessId, "CA");
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (camsSIPCommonStagingChk)
                                                {
                                                    packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingToWERP.dtsx");
                                                    camsSIPCommonStagingToWERP = camsUploadsBo.CamsSIPCommonStagingToWERP(UploadProcessId, packagePath, configPath);

                                                    if (camsSIPCommonStagingToWERP)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(UploadProcessId, "CA");



                                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    }
                                                }


                                            }


                                        }

                                        // Update Process Progress Monitoring Text Boxes
                                        //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                        if (XmlCreated)
                                            XMLProgress = "Done";
                                        else
                                            XMLProgress = "Failure";

                                        if (camsSIPInputResult)
                                        {
                                            XtrnlInsertionProgress = "Done";
                                            InputInsertionProgress = "Done";
                                        }
                                        else
                                        {
                                            InputInsertionProgress = "Failure";
                                            XtrnlInsertionProgress = "Failure";
                                        }

                                        if (camsSIPStagingResult)
                                            FirstStagingInsertionProgress = "Done";
                                        else
                                            FirstStagingInsertionProgress = "Failure";

                                        if (camsSIPCommonStagingChk)
                                            SecondStagingInsertionProgress = "Done";
                                        else
                                            SecondStagingInsertionProgress = "Failure";

                                        if (camsSIPCommonStagingChk && camsSIPCommonStagingToWERP)
                                        {
                                            WERPInsertionProgress = "Done";

                                        }
                                        else
                                            WERPInsertionProgress = "Failure";

                                        if (camsSIPCommonStagingToWERP)
                                            XtrnlInsertionProgress = "Done";
                                        else
                                            XtrnlInsertionProgress = "Failure";

                                        // Update Process Summary Text Boxes
                                        txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                        txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                        txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                        txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();

                                        txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                        Session[SessionContents.ProcessLogVo] = processlogVo;
                                    }
                                }
                                #endregion MF CAMS Systematic Upload


                                //****************Systematic Uploads**************\\
                                //*********gobinda Uploads**************\\

                                #region MF Systematic Standard Upload
                                //MF CAMS Systematic Upload
                                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFSystematic && ddlListCompany.SelectedValue == "WPT")
                                {
                                    bool updateProcessLog = false;
                                    //bool camsSIPWerpResult = false;
                                    //bool CommonTransChecks = false;
                                    bool standardSIPCommonStagingChk = false;
                                    bool standardSIPStagingToCommonStaging = false;
                                    bool standardSIPInputResult = false;
                                    bool standardSIPCommonStaging = false;
                                    bool standardSIPStagingCheckResult = false;
                                    bool standardSIPStagingResult = false;
                                    bool standardSIPCommonStagingToWERP = false;


                                    packagePath = Server.MapPath("\\UploadPackages\\StandardSIPTransactionsUpload\\StandardSIPTransactionsUpload\\UploadSystematicTransationXMLToInput.dtsx");
                                    standardSIPInputResult = camsUploadsBo.StandardSIPInsertToInputTrans(UploadProcessId, packagePath, fileName, configPath);
                                    if (standardSIPInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);


                                        packagePath = Server.MapPath("\\UploadPackages\\StandardSIPTransactionsUpload\\StandardSIPTransactionsUpload\\UploadSystematicTransationInputToStaging.dtsx");
                                        standardSIPStagingResult = camsUploadsBo.StandardSIPInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                        if (standardSIPStagingResult)
                                        {
                                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            packagePath = Server.MapPath("\\UploadPackages\\StandardSIPTransactionsUpload\\StandardSIPTransactionsUpload\\UploadStandardTransactionStagingCheck.dtsx");
                                            standardSIPStagingCheckResult = camsUploadsBo.StandardSIPProcessDataInStagingTrans(UploadProcessId, packagePath, configPath);
                                            if (standardSIPStagingCheckResult)
                                            {



                                                packagePath = Server.MapPath("\\UploadPackages\\StandardSIPTransactionsUpload\\StandardSIPTransactionsUpload\\UploadStandardTransactionStagingtoCommonStaging.dtsx");
                                                standardSIPStagingToCommonStaging = camsUploadsBo.StandardSIPStagingToCommonStaging(UploadProcessId, packagePath, configPath);
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                packagePath = Server.MapPath("\\UploadPackages\\StandardSIPTransactionsUpload\\StandardSIPTransactionsUpload\\UploadStandardTransactionCommonStagingCheck.dtsx");
                                                standardSIPCommonStagingChk = camsUploadsBo.StandardSIPCommonStagingChk(UploadProcessId, packagePath, configPath, "WPT");
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(UploadProcessId, "WPT");
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (standardSIPCommonStagingChk)
                                                {
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardSIPTransactionsUpload\\StandardSIPTransactionsUpload\\UploadStandardTransactionCommonStagingToWERP.dtsx");
                                                    standardSIPCommonStagingToWERP = camsUploadsBo.CamsSIPCommonStagingToWERP(UploadProcessId, packagePath, configPath);

                                                    if (standardSIPCommonStagingToWERP)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(UploadProcessId, "WPT");
                                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    }
                                                }
                                            }
                                        }

                                        // Update Process Progress Monitoring Text Boxes
                                        //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                        if (XmlCreated)
                                            XMLProgress = "Done";
                                        else
                                            XMLProgress = "Failure";

                                        if (standardSIPInputResult)
                                        {
                                            XtrnlInsertionProgress = "Done";
                                            InputInsertionProgress = "Done";
                                        }
                                        else
                                        {
                                            InputInsertionProgress = "Failure";
                                            XtrnlInsertionProgress = "Failure";
                                        }

                                        if (standardSIPStagingResult)
                                            FirstStagingInsertionProgress = "Done";
                                        else
                                            FirstStagingInsertionProgress = "Failure";

                                        if (standardSIPCommonStagingChk)
                                            SecondStagingInsertionProgress = "Done";
                                        else
                                            SecondStagingInsertionProgress = "Failure";

                                        if (standardSIPCommonStagingChk && standardSIPCommonStagingToWERP)
                                        {
                                            WERPInsertionProgress = "Done";

                                        }
                                        else
                                            WERPInsertionProgress = "Failure";

                                        if (standardSIPCommonStagingToWERP)
                                            XtrnlInsertionProgress = "Done";
                                        else
                                            XtrnlInsertionProgress = "Failure";

                                        // Update Process Summary Text Boxes
                                        txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                        txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                        txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                        txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();

                                        txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                        Session[SessionContents.ProcessLogVo] = processlogVo;
                                    }
                                }
                                #endregion MF Standard Systematic Upload




                                #region MF Sundaram Profile Upload
                                //*****************************************************************************************************************************
                                //MF Sundaram Profile Upload
                                else if ((ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio) && ddlListCompany.SelectedValue == "SU")
                                {
                                    // Sundaram Insert To Input Profile
                                    packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\SundramFileToInput.dtsx");
                                    bool camsProInputResult = camsUploadsBo.SundramInsertToInputProfile(UploadProcessId, packagePath, fileName, configPath);
                                    if (camsProInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog1)
                                        {
                                            // Sundaram Insert To Staging Profile
                                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                                            camsProStagingResult = camsUploadsBo.CAMSInsertToStagingProfile(UploadProcessId, packagePath, configPath);
                                            if (camsProStagingResult)
                                            {
                                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (updateProcessLog2)
                                                {
                                                    // Doing a check on data translation
                                                    packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadDataTranslationChecksFirstStaging.dtsx");
                                                    camsProStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingProfile(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                                    if (camsProStagingCheckResult)
                                                    {
                                                        if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile)
                                                        {
                                                            // Insertion to common staging
                                                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                                            camsProCommonStagingResult = camsUploadsBo.SundaramInsertToCommonStaging(UploadProcessId, packagePath, configPath);
                                                            if (camsProCommonStagingResult)
                                                            {
                                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                                processlogVo.EndTime = DateTime.Now;
                                                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                if (updateProcessLog3)
                                                                {
                                                                    //common profile checks
                                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                                    camsProCommonChecksResult = StandardProfileUploadBo.StdCommonProfileChecks(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                                                    // Insert Customer Details into WERP Tables
                                                                    if (camsProCommonChecksResult)
                                                                        camsProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserVo.advisorId, UploadProcessId, rmVo.RMId, int.Parse(ddlListBranch.SelectedValue.ToString()), xmlPath, out countCustCreated);
                                                                    if (camsProCreateCustomerResult)
                                                                    {
                                                                        //Create new Bank Accounts
                                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                                        bool camsProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(UploadProcessId, packagePath, configPath);
                                                                        if (camsProCreateBankAccountResult)
                                                                        {
                                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                                            processlogVo.EndTime = DateTime.Now;
                                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(UploadProcessId, "SU");
                                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                                            txtUploadedRecords.Text = processlogVo.NoOfCustomerInserted.ToString();
                                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(UploadProcessId, "SU");
                                                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords - processlogVo.NoOfInputRejects;

                                                                            //processlogVo.NoOfAccountsInserted = countFolioCreated;
                                                                            bool updateProcessLog4 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                            if (updateProcessLog4)
                                                                                stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(UploadProcessId);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio)
                                            {
                                                if (camsProStagingResult)
                                                {
                                                    packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadFolioDataFromSundaramStagingToCommonStaging.dtsx");
                                                    camsFolioCommonStagingResult = camsUploadsBo.SundaramInsertFolioDataToFolioCommonStaging(UploadProcessId, packagePath, configPath);
                                                    if (camsFolioCommonStagingResult)
                                                    {
                                                        //Folio Chks in Std Folio Staging 
                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                                        bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserVo.advisorId, UploadProcessId, configPath);
                                                        if (camsFolioStagingChkResult)
                                                        {
                                                            //Move Folio data to WERP table
                                                            packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                            camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserVo.advisorId, UploadProcessId, configPath);
                                                            if (camsFolioWerpInsertionResult)
                                                            {
                                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                                processlogVo.EndTime = DateTime.Now;
                                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(UploadProcessId, "SU");
                                                                processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(UploadProcessId, "WPMF");
                                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(UploadProcessId, "SU");
                                                                processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                                txtUploadedRecords.Text = processlogVo.NoOfAccountsInserted.ToString();

                                                                bool updateProcessLog4 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                if (updateProcessLog4)
                                                                    stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(UploadProcessId);
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }


                                    // Update Process Progress Monitoring Text Boxes
                                    // Commented for Removing Process Progress Monitoring 
                                    //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                    if (XmlCreated)
                                        XMLProgress = "Done";
                                    else
                                        XMLProgress = "Failure";

                                    if (camsProInputResult)
                                    {
                                        XtrnlInsertionProgress = "Done";
                                        InputInsertionProgress = "Done";
                                    }
                                    else
                                    {
                                        XtrnlInsertionProgress = "Failure";
                                        InputInsertionProgress = "Failure";
                                    }

                                    if (camsProStagingResult)
                                        FirstStagingInsertionProgress = "Done";
                                    else
                                        FirstStagingInsertionProgress = "Failure";

                                    if (camsProCommonStagingResult == true || camsFolioCommonStagingResult == true)
                                        SecondStagingInsertionProgress = "Done";
                                    else
                                        SecondStagingInsertionProgress = "Failure";

                                    if (camsProCreateCustomerResult == true || camsFolioWerpInsertionResult == true)
                                        WERPInsertionProgress = "Done";
                                    else
                                        WERPInsertionProgress = "Failure";
                                    // Up to here 

                                    // Update Process Summary Text Boxes
                                    txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                    txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                    txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                    //txtUploadedRecords.Text = processlogVo.NoOfAccountsInserted.ToString();
                                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                                    Session[SessionContents.ProcessLogVo] = processlogVo;
                                }
                                #endregion MF Sundaram Profile Upload



                                #region MF Karvy Profile Upload
                                //*******************************************************************************************************************
                                //MF Karvy Profile Upload
                                else if ((ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio) && ddlListCompany.SelectedValue == Contants.UploadExternalTypeKarvy)
                                {
                                    bool karvyProInputResult = false;
                                    bool karvyProStagingResult = false;
                                    bool karvyProStagingCheckResult = false;
                                    bool karvyStagingToProfileStagingResult = false;
                                    bool karvyProCommonChecksResult = false;
                                    bool karvyProCreateCustomerResult = false;
                                    bool karvyStagingToFolioStagingResult = false;
                                    bool karvyFolioWerpInsertionResult = false;
                                    // Karvy Insert To Input Profile
                                    packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadProfileDataFromKarvyProfileFileToXtrnlProfileInput.dtsx");
                                    karvyProInputResult = karvyUploadsBo.KARVYInsertToInputProfile(packagePath, UploadProcessId, fileName, configPath);
                                    if (karvyProInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        if (updateProcessLog1)
                                        {

                                            // Karvy Insert To Staging Profile
                                            packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileInputToKarvyXtrnlProfileStaging .dtsx");
                                            karvyProStagingResult = karvyUploadsBo.KARVYInsertToStagingProfile(UploadProcessId, packagePath, configPath);
                                            if (karvyProStagingResult)
                                            {
                                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (updateProcessLog2)
                                                {
                                                    // Data Translation checks in Karvy Staging
                                                    packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadDataTranslationKarvyProfileStaging.dtsx");
                                                    karvyProStagingCheckResult = karvyUploadsBo.KARVYStagingDataTranslationCheck(UploadProcessId, packagePath, configPath);
                                                    if (karvyProStagingCheckResult)
                                                    {

                                                        if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile)
                                                        {
                                                            // Inserting Karvy Staging Data to Profile Staging
                                                            packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToProfileStaging.dtsx");
                                                            karvyStagingToProfileStagingResult = karvyUploadsBo.KARVYStagingInsertToProfileStaging(UploadProcessId, packagePath, configPath);
                                                            if (karvyStagingToProfileStagingResult)
                                                            {
                                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                                processlogVo.EndTime = DateTime.Now;
                                                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                if (updateProcessLog3)
                                                                {
                                                                    //Making Chks in Profile Staging
                                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                                    karvyProCommonChecksResult = StandardProfileUploadBo.StdCommonProfileChecks(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                                                    if (karvyProCommonChecksResult)
                                                                    {
                                                                        // Insert Customer Details into WERP Tables
                                                                        karvyProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserVo.advisorId, UploadProcessId, rmVo.RMId, int.Parse(ddlListBranch.SelectedValue.ToString()), xmlPath, out countCustCreated);
                                                                        if (karvyProCreateCustomerResult)
                                                                        {
                                                                            //Create new Bank Accounts
                                                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                                            bool karvyProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(UploadProcessId, packagePath, configPath);
                                                                            if (karvyProCreateBankAccountResult)
                                                                            {
                                                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                                                processlogVo.EndTime = DateTime.Now;
                                                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(UploadProcessId, "KA");
                                                                                processlogVo.NoOfCustomerInserted = countCustCreated;
                                                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(UploadProcessId, "KA");
                                                                                processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords - processlogVo.NoOfInputRejects;
                                                                                txtUploadedRecords.Text = processlogVo.NoOfCustomerInserted.ToString();
                                                                                bool updateProcessLog4 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                                if (updateProcessLog4)
                                                                                    stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(UploadProcessId);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio)
                                                        {
                                                            if (karvyProStagingCheckResult)
                                                            {
                                                                // Inserting Karvy Staging Data to Folio Staging
                                                                packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToFolioStaging.dtsx");
                                                                karvyStagingToFolioStagingResult = karvyUploadsBo.KARVYStagingInsertToFolioStaging(UploadProcessId, packagePath, configPath);
                                                                if (karvyStagingToFolioStagingResult)
                                                                {

                                                                    //Folio Chks in Std Folio Staging 
                                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                                                    bool karvyFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserVo.advisorId, UploadProcessId, configPath);
                                                                    if (karvyFolioStagingChkResult)
                                                                    {
                                                                        //Folio Chks in Std Folio Staging 
                                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                                        karvyFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserVo.advisorId, UploadProcessId, configPath);
                                                                        if (karvyFolioWerpInsertionResult)
                                                                        {
                                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                                            processlogVo.EndTime = DateTime.Now;
                                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(UploadProcessId, "KA");
                                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(UploadProcessId, "WPMF");
                                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(UploadProcessId, "KA");
                                                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                                            txtUploadedRecords.Text = processlogVo.NoOfAccountsInserted.ToString();
                                                                            bool updateProcessLog4 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                            if (updateProcessLog4)
                                                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(UploadProcessId);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }


                                    // Update Process Progress Monitoring Text Boxes
                                    //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                    if (XmlCreated)
                                        XMLProgress = "Done";
                                    else
                                        XMLProgress = "Failure";

                                    if (karvyProInputResult)
                                    {
                                        InputInsertionProgress = "Done";
                                        XtrnlInsertionProgress = "Done";
                                    }
                                    else
                                    {
                                        InputInsertionProgress = "Failure";
                                        XtrnlInsertionProgress = "Failure";
                                    }

                                    if (karvyProStagingResult)
                                        FirstStagingInsertionProgress = "Done";
                                    else
                                        FirstStagingInsertionProgress = "Failure";

                                    if (karvyStagingToProfileStagingResult == true || karvyStagingToFolioStagingResult == true)
                                        SecondStagingInsertionProgress = "Done";
                                    else
                                        SecondStagingInsertionProgress = "Failure";

                                    if (karvyProCreateCustomerResult == true || karvyFolioWerpInsertionResult == true)
                                        WERPInsertionProgress = "Done";
                                    else
                                        WERPInsertionProgress = "Failure";

                                    // Update Process Summary Text Boxes
                                    txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                    txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                    txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                    //if (processlogVo.NoOfFolioInserted > processlogVo.NoOfCustomerInserted)
                                    //{
                                    txtUploadedRecords.Text = processlogVo.NoOfAccountsInserted.ToString();
                                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                    Session[SessionContents.ProcessLogVo] = processlogVo;
                                }
                                #endregion MF Karvy Profile Upload

                                #region MF Karvy Transaction Upload
                                //*****************************************************************************************************************
                                //MF Karvy Transaction Upload
                                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeKarvy)
                                {
                                    bool updateProcessLog;
                                    bool karvyTranWerpResult = false;
                                    bool CommonTransChecks = false;
                                    bool karvyTranStagingCheckResult = false;
                                    bool karvyTranStagingResult = false;
                                    bool karvyTranInputResult = false;

                                    // Input Insertion
                                    packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadTransactionDataFromKarvyFileToXtrnlTransactionInput.dtsx");
                                    karvyTranInputResult = karvyUploadsBo.KarvyInsertToInputTrans(processlogVo.ProcessId, packagePath, fileName, configPath);
                                    if (karvyTranInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        // Staging Insertion
                                        packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadKarvyXtrnlTransactionInputToKarvyXtrnlTransactionStaging.dtsx");
                                        karvyTranStagingResult = karvyUploadsBo.KarvyInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                        if (karvyTranStagingResult)
                                        {
                                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            // Transalation check in first staging and Move to second staging
                                            packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadChecksKarvyTransactionStaging.dtsx");
                                            karvyTranStagingCheckResult = karvyUploadsBo.KarvyProcessDataInStagingTrans(UploadProcessId, packagePath, configPath);
                                            if (karvyTranStagingCheckResult)
                                            {
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                                CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserVo.advisorId, UploadProcessId, packagePath, configPath, "KA", "Karvy");

                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                karvyTranWerpResult = uploadsCommonBo.InsertTransToWERP(UploadProcessId, packagePath, configPath);
                                                if (karvyTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(UploadProcessId, "WPMF");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(UploadProcessId, Contants.UploadExternalTypeKarvy);
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadTransactionInputRejectCount(UploadProcessId, "KA");
                                                    processlogVo.NoOfTransactionDuplicates = 0;
                                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }

                                    }

                                    // Update Process Progress Monitoring Text Boxes
                                    //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                    if (XmlCreated)
                                        XMLProgress = "Done";
                                    else
                                        XMLProgress = "Failure";

                                    if (karvyTranInputResult)
                                    {
                                        XtrnlInsertionProgress = "Done";
                                        InputInsertionProgress = "Done";
                                    }
                                    else
                                    {
                                        InputInsertionProgress = "Failure";
                                        XtrnlInsertionProgress = "Failure";
                                    }

                                    if (karvyTranStagingResult)
                                        FirstStagingInsertionProgress = "Done";
                                    else
                                        FirstStagingInsertionProgress = "Failure";

                                    if (karvyTranStagingCheckResult)
                                        SecondStagingInsertionProgress = "Done";
                                    else
                                        SecondStagingInsertionProgress = "Failure";


                                    if (CommonTransChecks && karvyTranWerpResult)
                                    {
                                        WERPInsertionProgress = "Done";
                                    }
                                    else
                                        WERPInsertionProgress = "Failure";

                                    if (karvyTranInputResult)
                                        XtrnlInsertionProgress = "Done";
                                    else
                                        XtrnlInsertionProgress = "Failure";

                                    // Update Process Summary Text Boxes
                                    txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                    txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                    txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                    //if (processlogVo.NoOfFolioInserted > processlogVo.NoOfCustomerInserted)
                                    //{
                                    txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();
                                    //}
                                    //else if (processlogVo.NoOfFolioInserted < processlogVo.NoOfCustomerInserted)
                                    //{

                                    //}
                                    //txtUploadedRecords.Text = (processlogVo.);//processlogVo.NoOfTotalRecords - processlogVo.NoOfRejectedRecords).ToString();
                                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                    Session[SessionContents.ProcessLogVo] = processlogVo;
                                }
                                #endregion MF Karvy Transaction Upload

                                #region MF Karvy Combination Upload
                                //*****************************************************************************************************************************
                                //MF Karvy Combination Upload
                                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio && ddlListCompany.SelectedValue == Contants.UploadExternalTypeKarvy)
                                {

                                }
                                //****************************************************************************************************************
                                ////MF WERP Profile
                                //else if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio && ddlListCompany.SelectedValue == Contants.UploadExternalTypeStandard)
                                //{
                                //    // WERP MF Insert To Input Profile
                                //    packagePath = Server.MapPath("\\UploadPackages\\WerpMFProfileUploadPackageNew\\WerpMFProfileUploadPackageNew\\UploadProfileDataFromWerpMFProfileFileToWerpMFXtrnlProfileInput.dtsx");
                                //    bool werpMFProInputResult = werpMFUploadsBo.WerpMFInsertToInputProfile(packagePath, fileName, configPath);
                                //    if (werpMFProInputResult)
                                //    {
                                //        processlogVo.IsInsertionToInputComplete = 1;
                                //        processlogVo.EndTime = DateTime.Now;
                                //        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                //        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                //    }

                                //    // WERP MF Insert To Staging Profile
                                //    packagePath = Server.MapPath("\\UploadPackages\\WerpMFProfileUploadPackageNew\\WerpMFProfileUploadPackageNew\\UploadWerpMFXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                                //    bool werpMFProStagingResult = werpMFUploadsBo.WerpMFInsertToStagingProfile(UploadProcessId, packagePath, configPath);
                                //    if (werpMFProStagingResult)
                                //    {
                                //        processlogVo.IsInsertionToFirstStagingComplete = 1;
                                //        processlogVo.EndTime = DateTime.Now;
                                //        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                //    }

                                //    // Doing a check on data in Staging and marking IsRejected flag
                                //    packagePath = Server.MapPath("\\UploadPackages\\WerpMFProfileUploadPackageNew\\WerpMFProfileUploadPackageNew\\UploadChecksWerpMFProfileStaging.dtsx");
                                //    bool werpMFProStagingCheckResult = werpMFUploadsBo.WerpMFProcessDataInStagingProfile(UploadProcessId, adviserVo.advisorId, packagePath, configPath);

                                //    // Insert Customer Details into Customer Tables
                                //    bool werpMFProCreateCustomerResult = werpMFUploadsBo.WerpMFInsertCustomerDetails(adviserVo.advisorId, UploadProcessId, rmVo.RMId, out countCustCreated, out countFolioCreated);
                                //    bool werpMFProCreateBankAccountResult = false;
                                //    if (werpMFProCreateCustomerResult)
                                //    {
                                //        // Insert Bank Account Details
                                //        packagePath = Server.MapPath("\\UploadPackages\\WerpMFProfileUploadPackageNew\\WerpMFProfileUploadPackageNew\\UploadWerpMFProfileNewBankAccountCreation.dtsx");
                                //        werpMFProCreateBankAccountResult = werpMFUploadsBo.WerpMFCreationOfNewBankAccounts(UploadProcessId, packagePath, configPath);
                                //        if (werpMFProCreateBankAccountResult)
                                //        {

                                //            processlogVo.IsInsertionToWERPComplete = 1;
                                //            processlogVo.IsInsertionToXtrnlComplete = 2;
                                //            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(UploadProcessId, Contants.UploadExternalTypeStandard);
                                //            processlogVo.NoOfCustomerInserted = countCustCreated;
                                //            processlogVo.NoOfAccountsInserted = countFolioCreated;
                                //            processlogVo.EndTime = DateTime.Now;
                                //            bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                //        }
                                //    }



                                //    // Insert uploaded records from Staging Table into External Tables
                                //    //packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToKarvyXtrnlProfile.dtsx");
                                //    //bool werpMFProXtrnlResult = werpMFUploadsBo.WerpMFInsertExternalProfile(UploadProcessId, packagePath);
                                //    //if (werpMFProXtrnlResult)
                                //    //{
                                //    //    processlogVo.IsInsertionToXtrnlComplete = 1;
                                //    //    processlogVo.EndTime = DateTime.Now;
                                //    //    bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                //    //}
                                //    //



                                //    // Update Process Progress Monitoring Text Boxes
                                //    txtProcessID.Text = processlogVo.ProcessId.ToString();

                                //    if (XmlCreated)
                                //        XMLProgress = "Done";
                                //    else
                                //        XMLProgress = "Failure";

                                //    if (werpMFProInputResult)
                                //        InputInsertionProgress = "Done";
                                //    else
                                //        InputInsertionProgress = "Failure";

                                //    if (werpMFProStagingResult)
                                //        txtStagingInsertionProgress.Text = "Done";
                                //    else
                                //        txtStagingInsertionProgress.Text = "Failure";

                                //    if (werpMFProStagingCheckResult && werpMFProCreateCustomerResult && werpMFProCreateBankAccountResult)
                                //    {
                                //        WERPInsertionProgress = "Done";
                                //    }
                                //    else
                                //        WERPInsertionProgress = "Failure";

                                //    // Update Process Summary Text Boxes
                                //    txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                //    txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                //    txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                //    txtUploadedRecords.Text = processlogVo.NoOfAccountsInserted.ToString();
                                //    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                //    Session[SessionContents.ProcessLogVo] = processlogVo;
                                //}
                                #endregion MF Karvy Combination Upload

                                #region   MF WERP Transaction
                                //*****************************************************************************************************************************
                                //MF WERP Transaction
                                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio && ddlListCompany.SelectedValue == Contants.UploadExternalTypeStandard)
                                {
                                    // Input Insertion
                                    packagePath = Server.MapPath("\\UploadPackages\\WERPMFUploadTransactionNew\\WERPMFUploadTransactionNew\\UploadTransactionDataFromWERPMFFileToXtrnlTransactionInput.dtsx");
                                    bool WERPMFTranInputResult = werpUploadBo.WERPMFInsertToInputTrans(packagePath, fileName, configPath);
                                    if (WERPMFTranInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }

                                    // Staging Insertion
                                    packagePath = Server.MapPath("\\UploadPackages\\WERPMFUploadTransactionNew\\WERPMFUploadTransactionNew\\UploadTransactionDataFromWERPMFInputToStagingTransactionInput.dtsx");
                                    bool WERPMFTranStagingResult = werpUploadBo.WERPMFInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                    if (WERPMFTranStagingResult)
                                    {
                                        processlogVo.IsInsertionToFirstStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }

                                    // WERP Insertion
                                    packagePath = Server.MapPath("\\UploadPackages\\WERPMFUploadTransactionNew\\WERPMFUploadTransactionNew\\CheckTransactionDataFromWERPMFStaging.dtsx");
                                    bool WERPMFTranStagingCheckResult = werpUploadBo.WERPMFProcessDataInStagingTrans(UploadProcessId, packagePath, configPath);

                                    packagePath = Server.MapPath("\\UploadPackages\\WERPMFUploadTransactionNew\\WERPMFUploadTransactionNew\\UploadTransactionDataFromWERPMFStagingToWERPTable.dtsx");
                                    bool WERPMFTranWerpResult = werpUploadBo.WERPMFInsertTransDetails(UploadProcessId, packagePath, configPath);
                                    if (WERPMFTranWerpResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 2;
                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(UploadProcessId, Contants.UploadExternalTypeStandard);
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(UploadProcessId, Contants.UploadExternalTypeStandard);
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }

                                    // Update Process Progress Monitoring Text Boxes
                                    //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                    if (XmlCreated)
                                        XMLProgress = "Done";
                                    else
                                        XMLProgress = "Failure";

                                    if (WERPMFTranInputResult)
                                        InputInsertionProgress = "Done";
                                    else
                                        InputInsertionProgress = "Failure";

                                    if (WERPMFTranStagingResult)
                                        FirstStagingInsertionProgress = "Done";
                                    else
                                        FirstStagingInsertionProgress = "Failure";

                                    if (WERPMFTranStagingCheckResult && WERPMFTranWerpResult)
                                    {
                                        WERPInsertionProgress = "Done";
                                    }
                                    else
                                        WERPInsertionProgress = "Failure";


                                    // Update Process Summary Text Boxes
                                    txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                    txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                    txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                    //if (processlogVo.NoOfFolioInserted > processlogVo.NoOfCustomerInserted)
                                    //{
                                    txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();
                                    //}
                                    //else if (processlogVo.NoOfFolioInserted < processlogVo.NoOfCustomerInserted)
                                    //{

                                    //}
                                    //txtUploadedRecords.Text = (processlogVo.);//processlogVo.NoOfTotalRecords - processlogVo.NoOfRejectedRecords).ToString();
                                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                    Session[SessionContents.ProcessLogVo] = processlogVo;
                                }
                                #endregion MF WERP Transaction


                                //Gobinda ODIN Standard Equity Transaction Upload with BSE and NSE files .. 
                                #region Standard Equity Transaction Upload with ODIN
                                //*****************************************************************************************************************************                                
                                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTransaction && (ddlListCompany.SelectedValue == Contants.UploadExternalTypeStandard || ddlListCompany.SelectedValue == Contants.UploadExternalTypeIIFL || ddlListCompany.SelectedValue == Contants.UploadExternalTypeODIN))
                                {
                                    bool werpEQTranInputResult = false;
                                    bool werpEQFirstStagingResult = false;
                                    bool werpEQFirstStagingCheckResult = false;
                                    bool werpEQSecondStagingResult = false;
                                    bool WERPEQSecondStagingCheckResult = false;
                                    bool WERPEQTranWerpResult = false;

                                    // WERP Equity Insert To Input Profile
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadTransactionDataFromEQStdFileToEQStdTranInput.dtsx");
                                    werpEQTranInputResult = werpEQUploadsBo.WerpEQInsertToInputTransaction(packagePath, fileName, configPath);
                                    if (werpEQTranInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        if (updateProcessLog1)
                                        {
                                            // WERP Equity Insert To 1st Staging Transaction
                                            packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTransactionInputToEQStdTranStaging.dtsx");
                                            werpEQFirstStagingResult = werpEQUploadsBo.WerpEQInsertToFirstStagingTransaction(UploadProcessId, packagePath, configPath);
                                            if (werpEQFirstStagingResult)
                                            {
                                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                if (updateProcessLog2)
                                                {
                                                    // Doing a check on data in First Staging and marking IsRejected flag

                                                    packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQStdTranStaging.dtsx");

                                                    if (ddlListCompany.SelectedValue != Contants.UploadExternalTypeODIN)
                                                        werpEQFirstStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTransForODINUploads(UploadProcessId, packagePath, configPath, adviserVo.advisorId,"WP");
                                                    else if (ddlListCompany.SelectedValue == Contants.UploadExternalTypeODIN)
                                                    {
                                                        if (ddlAction.SelectedValue == "NSE")
                                                            werpEQFirstStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTransForODINUploads(UploadProcessId, packagePath, configPath, adviserVo.advisorId, ddlAction.SelectedValue);
                                                        else if (ddlAction.SelectedValue == "BSE")
                                                            werpEQFirstStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTransForODINUploads(UploadProcessId, packagePath, configPath, adviserVo.advisorId, ddlAction.SelectedValue);
                                                    }

                                                    //werpEQFirstStagingCheckResult = werpEQUploadsBo.WerpEQProcessDataInFirstStagingTrans(UploadProcessId, packagePath, configPath, adviserVo.advisorId);

                                                    if (werpEQFirstStagingCheckResult)
                                                    {
                                                        // WERP Equity Insert To 2nd Staging Transaction
                                                        //int filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "EQ", Contants.UploadExternalTypeIIFL, Contants.UploadFileTypeTransaction);
                                                        packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQStdTranStagingToEQTranStaging.dtsx");
                                                        if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeIIFL)
                                                        {
                                                            werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTransaction(UploadProcessId, packagePath, configPath, 19); // EQ Trans XML File Type Id = 8
                                                        }
                                                        else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTransaction && ddlListCompany.SelectedValue == "WP")
                                                        {
                                                            werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTransaction(UploadProcessId, packagePath, configPath, 8); // EQ Trans XML File Type Id = 8
                                                        }
                                                        else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeODIN)
                                                        {
                                                            if (ddlAction.SelectedValue == "NSE")
                                                                werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTransaction(UploadProcessId, packagePath, configPath, 10); // EQ Trans XML File Type Id = 8
                                                            else if (ddlAction.SelectedValue == "BSE")
                                                                werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTransaction(UploadProcessId, packagePath, configPath, 11); // EQ Trans XML File Type Id = 8
                                                        }


                                                        if (werpEQSecondStagingResult)
                                                        {
                                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                            if (updateProcessLog3)
                                                            {
                                                                // WERP Insertion
                                                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");

                                                                WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(UploadProcessId, packagePath, configPath, adviserVo.advisorId);

                                                                if (WERPEQSecondStagingCheckResult)
                                                                {
                                                                    packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                                                                    WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(UploadProcessId, packagePath, configPath);
                                                                    if (WERPEQTranWerpResult)
                                                                    {
                                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                                    }
                                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadTransactionInputRejectCount(UploadProcessId, "EQT");
                                                                    if (ddlAction.SelectedValue == "NSE" || ddlAction.SelectedValue == "BSE")
                                                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(UploadProcessId, "ODIN");
                                                                    else
                                                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(UploadProcessId, "WPEQ");
                                                                    if (ddlAction.SelectedValue == "NSE" || ddlAction.SelectedValue == "BSE")
                                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(UploadProcessId, "ODIN");
                                                                    else
                                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(UploadProcessId, "WPEQ");
                                                                    processlogVo.NoOfTransactionDuplicates = 0;
                                                                    processlogVo.EndTime = DateTime.Now;
                                                                    bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    // Update Process Progress Monitoring Text Boxes
                                    //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                    if (XmlCreated)
                                        XMLProgress = "Done";
                                    else
                                        XMLProgress = "Failure";

                                    if (werpEQTranInputResult)
                                        InputInsertionProgress = "Done";
                                    else
                                        InputInsertionProgress = "Failure";

                                    if (werpEQFirstStagingResult)
                                        FirstStagingInsertionProgress = "Done";
                                    else
                                        FirstStagingInsertionProgress = "Failure";

                                    if (werpEQSecondStagingResult)
                                        SecondStagingInsertionProgress = "Done";
                                    else
                                        SecondStagingInsertionProgress = "Failure";

                                    if (WERPEQTranWerpResult)
                                        WERPInsertionProgress = "Done";
                                    else
                                        WERPInsertionProgress = "Failure";

                                    XtrnlInsertionProgress = "1";

                                    // Update Process Summary Text Boxes
                                    txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                    txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                    if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeIIFL)
                                    {
                                        int count = processlogVo.NoOfTotalRecords;
                                        count = count - 2;
                                        txtExternalTotalRecords.Text = count.ToString();
                                    }
                                    else
                                        txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();

                                    txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();
                                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                    Session[SessionContents.ProcessLogVo] = processlogVo;
                                    if (XMLProgress == "Done" && InputInsertionProgress == "Done" && FirstStagingInsertionProgress == "Done" && SecondStagingInsertionProgress == "Done" && WERPInsertionProgress == "Done")
                                    {
                                        msgUploadComplete.Visible = true;
                                    }
                                }
                                #endregion Standard Equity Transaction Upload

                                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeODIN)
                                {
                                    bool werpEQTranInputResult = false;
                                    bool werpEQFirstStagingResult = false;
                                    bool werpEQFirstStagingCheckResult = false;
                                    bool werpEQSecondStagingResult = false;
                                    bool WERPEQSecondStagingCheckResult = false;
                                    bool WERPEQTranWerpResult = false;
                                }

                                #region Standard Equity Trade Account Upload

                                //************************************************************************************************************************************

                                //Standard Equity Trade Account Upload
                                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTradeAccount && ddlListCompany.SelectedValue == Contants.UploadExternalTypeStandard)
                                {
                                    bool werpEQTradeInputResult = false;
                                    bool werpEQFirstStagingResult = false;
                                    bool werpEQFirstStagingCheckResult = false;
                                    bool werpEQSecondStagingResult = false;
                                    bool WERPEQSecondStagingCheckResult = false;
                                    bool WERPEQTradeWerpResult = false;

                                    // WERP Equity Insert To Input Profile
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadTradeAccDataFromEQStdFileToEQStdTradeAccInput.dtsx");
                                    werpEQTradeInputResult = werpEQUploadsBo.WerpEQInsertToInputTradeAccount(packagePath, fileName, configPath);
                                    if (werpEQTradeInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        if (updateProcessLog1)
                                        {
                                            // WERP Equity Insert To 1st Staging Trade Account
                                            packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeAccInputToEQStdTradeAccStaging.dtsx");
                                            werpEQFirstStagingResult = werpEQUploadsBo.WerpEQInsertToFirstStagingTradeAccount(UploadProcessId, packagePath, configPath);
                                            if (werpEQFirstStagingResult)
                                            {
                                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                if (updateProcessLog2)
                                                {
                                                    // Doing a check on data in First Staging and marking IsRejected flag
                                                    packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQStdTradeAccStaging.dtsx");
                                                    werpEQFirstStagingCheckResult = werpEQUploadsBo.WerpEQProcessDataInFirstStagingTradeAccount(UploadProcessId, packagePath, configPath, adviserVo.advisorId);

                                                    if (werpEQFirstStagingCheckResult)
                                                    {
                                                        // WERP Equity Insert To 2nd Staging Trade Account
                                                        packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQStdTradeStagingToEQTradeStaging.dtsx");
                                                        werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTradeAccount(UploadProcessId, packagePath, configPath, 13); // EQ Trade Account XML File Type Id = 13

                                                        if (werpEQSecondStagingResult)
                                                        {
                                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                            if (updateProcessLog3)
                                                            {
                                                                // WERP Insertion
                                                                packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQTradeStaging.dtsx");
                                                                WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTradeAccount(UploadProcessId, packagePath, configPath, adviserVo.advisorId);

                                                                if (WERPEQSecondStagingCheckResult)
                                                                {
                                                                    packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeStagingToWerp.dtsx");
                                                                    WERPEQTradeWerpResult = werpEQUploadsBo.WERPEQInsertTradeAccountDetails(UploadProcessId, packagePath, configPath);
                                                                    if (WERPEQTradeWerpResult)
                                                                    {
                                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(UploadProcessId, "WPEQ");
                                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetAccountsUploadRejectCount(UploadProcessId, "WPEQ");
                                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadTradeAccountInputRejectCount(UploadProcessId, "EQTA");
                                                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                                        processlogVo.EndTime = DateTime.Now;
                                                                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    // Update Process Progress Monitoring Text Boxes
                                    //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                    if (XmlCreated)
                                        XMLProgress = "Done";
                                    else
                                        XMLProgress = "Failure";

                                    if (werpEQTradeInputResult)
                                        InputInsertionProgress = "Done";
                                    else
                                        InputInsertionProgress = "Failure";

                                    if (werpEQFirstStagingResult)
                                        FirstStagingInsertionProgress = "Done";
                                    else
                                        FirstStagingInsertionProgress = "Failure";

                                    if (werpEQSecondStagingResult)
                                        SecondStagingInsertionProgress = "Done";
                                    else
                                        SecondStagingInsertionProgress = "Failure";

                                    if (WERPEQTradeWerpResult)
                                        WERPInsertionProgress = "Done";
                                    else
                                        WERPInsertionProgress = "Failure";

                                    XtrnlInsertionProgress = "1";

                                    // Update Process Summary Text Boxes
                                    txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                    txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                    txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                    txtUploadedRecords.Text = processlogVo.NoOfAccountsInserted.ToString();
                                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                    Session[SessionContents.ProcessLogVo] = processlogVo;
                                    if (XMLProgress == "Done" && InputInsertionProgress == "Done" && FirstStagingInsertionProgress == "Done" && SecondStagingInsertionProgress == "Done" && WERPInsertionProgress == "Done")
                                    {
                                        msgUploadComplete.Visible = true;
                                    }

                                }
                                #endregion Standard Equity Trade Account Upload

                                #region MF Templeton Transaction Upload
                                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeTemp)
                                {
                                    bool updateProcessLog = false;
                                    bool templeTranWerpResult = false;
                                    bool CommonTransChecks = false;
                                    bool templeTranSecondStagingResult = false;
                                    bool templeTranStagingCheckResult = false;
                                    bool templeTranStagingResult = false;
                                    bool templeTranInputResult = false;

                                    packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTransactionFromXmlFileToInputTable.dtsx");
                                    templeTranInputResult = templetonUploadsBo.TempletonInsertToInputTrans(UploadProcessId, packagePath, fileName, configPath);
                                    if (templeTranInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        if (updateProcessLog)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTempletonTransToStagingTable.dtsx");
                                            templeTranStagingResult = templetonUploadsBo.TempletonInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                            if (templeTranStagingResult)
                                            {
                                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                if (updateProcessLog)
                                                {
                                                    packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadChecksOnTempletonStaging.dtsx");
                                                    templeTranStagingCheckResult = templetonUploadsBo.TempletonProcessDataInStagingTrans(UploadProcessId, packagePath, configPath);
                                                    if (templeTranStagingCheckResult)
                                                    {
                                                        packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTempletonTransStagingToTransStaging.dtsx");
                                                        templeTranSecondStagingResult = templetonUploadsBo.TempletonInsertFromTempStagingTransToCommonStaging(UploadProcessId, packagePath, configPath);
                                                        if (templeTranSecondStagingResult)
                                                        {
                                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                            if (updateProcessLog)
                                                            {
                                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                                                CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserVo.advisorId, UploadProcessId, packagePath, configPath, "TN", "Templeton");

                                                                if (CommonTransChecks)
                                                                {
                                                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                                    templeTranWerpResult = uploadsCommonBo.InsertTransToWERP(UploadProcessId, packagePath, configPath);
                                                                    if (templeTranWerpResult)
                                                                    {
                                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(UploadProcessId, "WPMF");
                                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(UploadProcessId, Contants.UploadExternalTypeTemp);
                                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadTransactionInputRejectCount(UploadProcessId, "TN");
                                                                        processlogVo.NoOfTransactionDuplicates = 0;
                                                                        processlogVo.EndTime = DateTime.Now;
                                                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    // Update Process Progress Monitoring Text Boxes
                                    //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                    if (XmlCreated)
                                        XMLProgress = "Done";
                                    else
                                        XMLProgress = "Failure";

                                    if (templeTranInputResult)
                                    {
                                        XtrnlInsertionProgress = "Done";
                                        InputInsertionProgress = "Done";
                                    }
                                    else
                                    {
                                        InputInsertionProgress = "Failure";
                                        XtrnlInsertionProgress = "Failure";
                                    }

                                    if (templeTranStagingResult)
                                        FirstStagingInsertionProgress = "Done";
                                    else
                                        FirstStagingInsertionProgress = "Failure";

                                    if (templeTranStagingCheckResult)
                                        SecondStagingInsertionProgress = "Done";
                                    else
                                        SecondStagingInsertionProgress = "Failure";

                                    if (CommonTransChecks && templeTranWerpResult)
                                    {
                                        WERPInsertionProgress = "Done";

                                    }
                                    else
                                        WERPInsertionProgress = "Failure";

                                    if (templeTranWerpResult)
                                        XtrnlInsertionProgress = "Done";
                                    else
                                        XtrnlInsertionProgress = "Failure";

                                    // Update Process Summary Text Boxes
                                    txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                    txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                    txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                    txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();

                                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                    Session[SessionContents.ProcessLogVo] = processlogVo;
                                }
                                #endregion

                                #region MF Sundaram Transaction Upload
                                //*****************************************************************************************************************************
                                //MF Sundaram Transaction Upload
                                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction && ddlListCompany.SelectedValue == "SU")
                                {
                                    bool updateProcessLog = false;
                                    bool camsTranWerpResult = false;
                                    bool CommonTransChecks = false;
                                    bool camsTranStagingCheckResult = false;
                                    bool camsTranStagingResult = false;
                                    bool camsTranInputResult = false;
                                    packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadTransactionDataFromCAMSFileToXtrnlTransactionInput.dtsx");
                                    camsTranInputResult = camsUploadsBo.CAMSInsertToInputTrans(UploadProcessId, packagePath, fileName, configPath);

                                    if (camsTranInputResult)
                                    {
                                        processlogVo.IsInsertionToInputComplete = 1;
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        processlogVo.IsInsertionToXtrnlComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);


                                        packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadXtrnlTransactionInputToXtrnlTransactionStaging.dtsx");
                                        camsTranStagingResult = camsUploadsBo.CAMSInsertToStagingTrans(UploadProcessId, packagePath, configPath);
                                        if (camsTranStagingResult)
                                        {
                                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadChkSundaramTransactionStaging.dtsx");
                                            camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(UploadProcessId, adviserVo.advisorId, packagePath, configPath);
                                            if (camsTranStagingCheckResult)
                                            {

                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                                CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserVo.advisorId, UploadProcessId, packagePath, configPath, "SU", "Sundaram");


                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(UploadProcessId, packagePath, configPath);
                                                if (camsTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(UploadProcessId, "WPMF");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(UploadProcessId, "SU");
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadTransactionInputRejectCount(UploadProcessId, "SU");
                                                    processlogVo.NoOfTransactionDuplicates = 0;
                                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }


                                    }

                                    // Update Process Progress Monitoring Text Boxes
                                    //txtProcessID.Text = processlogVo.ProcessId.ToString();

                                    if (XmlCreated)
                                        XMLProgress = "Done";
                                    else
                                        XMLProgress = "Failure";

                                    if (camsTranInputResult)
                                    {
                                        XtrnlInsertionProgress = "Done";
                                        InputInsertionProgress = "Done";
                                    }
                                    else
                                    {
                                        InputInsertionProgress = "Failure";
                                        XtrnlInsertionProgress = "Failure";
                                    }

                                    if (camsTranStagingResult)
                                        FirstStagingInsertionProgress = "Done";
                                    else
                                        FirstStagingInsertionProgress = "Failure";

                                    if (camsTranStagingCheckResult)
                                        SecondStagingInsertionProgress = "Done";
                                    else
                                        SecondStagingInsertionProgress = "Failure";

                                    if (CommonTransChecks && camsTranWerpResult)
                                    {
                                        WERPInsertionProgress = "Done";

                                    }
                                    else
                                        WERPInsertionProgress = "Failure";

                                    if (camsTranWerpResult)
                                        XtrnlInsertionProgress = "Done";
                                    else
                                        XtrnlInsertionProgress = "Failure";

                                    // Update Process Summary Text Boxes
                                    txtUploadStartTime.Text = processlogVo.StartTime.ToShortTimeString();
                                    txtUploadEndTime.Text = processlogVo.EndTime.ToShortTimeString();
                                    txtExternalTotalRecords.Text = processlogVo.NoOfTotalRecords.ToString();
                                    txtUploadedRecords.Text = processlogVo.NoOfTransactionInserted.ToString();

                                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                                    Session[SessionContents.ProcessLogVo] = processlogVo;
                                }


                                #endregion MF CAMS Transaction Upload


                    }
                    else
                    {
                        string rejectmessage = reject_reason;
                        trError.Visible = true;
                        lblError.Text = rejectmessage;
                    }
                }
                else
                {
                    // Display Incorrect File Format
                    trError.Visible = true;
                    lblError.Text = "The file format does not match the selection made!";
                }
                # endregion
                if (processlogVo.NoOfRejectedRecords == 0)
                {
                    btn_ViewRjects.Visible = false;
                }
                else
                {
                    btn_ViewRjects.Visible = true;
                }
                if (XMLProgress == "Done" && XtrnlInsertionProgress == "Done" && InputInsertionProgress == "Done" && FirstStagingInsertionProgress == "Done" && SecondStagingInsertionProgress == "Done" && WERPInsertionProgress == "Done" && XtrnlInsertionProgress == "Done")
                {
                    msgUploadComplete.Visible = true;
                }
            }


        }

        protected void ddlUploadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Label6.Visible = true;
            //rbSkipRowsYes.Visible = true;
            SkiprowsVisible.Visible = true;
            upload.Visible = false;
            datevisible.Visible = false;
            if (ddlUploadType.SelectedValue == "P" || ddlUploadType.SelectedValue == "PMFF")
            {
                trListBranch.Visible = true;
            }
            else
            {
                trListBranch.Visible = false;
            }
            //MF FOLIO UPLOADS DROPDOWN POPULATE -VISHAL 
            if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFFolio)
            {   // MF FOLIO  Only
                ddlListCompany.DataSource = GetMFFolioGenericDictionary();
                ddlListCompany.DataTextField = "Key";
                ddlListCompany.DataValueField = "Value";
                ddlListCompany.DataBind();
                ddlListCompany.Items.Insert(0, new ListItem("Select Source Type", "Select Source Type"));

            }
            if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfile)
            {   // Profile Only

                //fill External Type dropdownbox
                ddlListCompany.DataSource = GetProfileGenericDictionary();
                ddlListCompany.DataTextField = "Key";
                ddlListCompany.DataValueField = "Value";
                ddlListCompany.DataBind();
                ddlListCompany.Items.Insert(0, new ListItem("Select Source Type", "Select Source Type"));

                //Fill Extension types for Selected Asset
                //ddlListExtensionType.DataSource = GetMFExtensions();
                //ddlListExtensionType.DataTextField = "Key";
                //ddlListExtensionType.DataValueField = "Value";
                //ddlListExtensionType.DataBind();
                //ddlListExtensionType.Items.Insert(0, new ListItem("Select File Extension", "Select File Extension"));
            }
            else if (ddlUploadType.SelectedValue == Contants.ExtractTypeFolio)
            {   // Folio Only
                //fill External Type dropdownbox
                ddlListCompany.DataSource = GetFolioGenericDictionary();
                ddlListCompany.DataTextField = "Key";
                ddlListCompany.DataValueField = "Value";
                ddlListCompany.DataBind();
                ddlListCompany.Items.Insert(0, new ListItem("Select Source Type", "Select Source Type"));

                //Fill Extension types for Selected Asset
                //ddlListExtensionType.DataSource = GetMFExtensions();
                //ddlListExtensionType.DataTextField = "Key";
                //ddlListExtensionType.DataValueField = "Value";
                //ddlListExtensionType.DataBind();
                //ddlListExtensionType.Items.Insert(0, new ListItem("Select File Extension", "Select File Extension"));
            }
            else if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio)
            {   // Profile & Folio Only

                //fill External Type dropdownbox
                ddlListCompany.DataSource = GetMFGenericDictionary();
                ddlListCompany.DataTextField = "Key";
                ddlListCompany.DataValueField = "Value";
                ddlListCompany.DataBind();
                ddlListCompany.Items.Insert(0, new ListItem("Select Source Type", "Select Source Type"));

                //Fill Extension types for Selected Asset
                //ddlListExtensionType.DataSource = GetMFExtensions();
                //ddlListExtensionType.DataTextField = "Key";
                //ddlListExtensionType.DataValueField = "Value";
                //ddlListExtensionType.DataBind();
                //ddlListExtensionType.Items.Insert(0, new ListItem("Select File Extension", "Select File Extension"));
            }
            else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction)
            {   // MF Trnx

                //fill External Type dropdownbox
                ddlListCompany.DataSource = GetMFGenericDictionary();
                ddlListCompany.DataTextField = "Key";
                ddlListCompany.DataValueField = "Value";
                ddlListCompany.DataBind();
                ddlListCompany.Items.Insert(0, new ListItem("Select Source Type", "Select Source Type"));

                //Fill Extension types for Selected Asset
                //ddlListExtensionType.DataSource = GetMFExtensions();
                //ddlListExtensionType.DataTextField = "Key";
                //ddlListExtensionType.DataValueField = "Value";
                //ddlListExtensionType.DataBind();
                //ddlListExtensionType.Items.Insert(0, new ListItem("Select File Extension", "Select File Extension"));
            }
            else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTradeAccount)
            {   // EQ Trade Account Only

                //fill External Type dropdownbox
                ddlListCompany.DataSource = GetEQTradeAccGenericDictionary();
                //Label6.Visible = false;
                //rbSkipRowsYes.Visible = false;
                SkiprowsVisible.Visible = false;
                ddlListCompany.DataTextField = "Key";
                ddlListCompany.DataValueField = "Value";
                ddlListCompany.DataBind();
                ddlListCompany.Items.Insert(0, new ListItem("Select Source Type", "Select Source Type"));

                //Fill Extension types for Selected Asset
                //ddlListExtensionType.DataSource = GetEQTradeAccExtensions();
                //ddlListExtensionType.DataTextField = "Key";
                //ddlListExtensionType.DataValueField = "Value";
                //ddlListExtensionType.DataBind();
                //ddlListExtensionType.Items.Insert(0, new ListItem("Select File Extension", "Select File Extension"));
            }
            else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQDematAccount)
            {   // EQ Demat Account Only

                //fill External Type dropdownbox
                Label6.Visible = false;
                rbSkipRowsYes.Visible = false;
                rbSkipRowsNo.Visible = false;
                ddlListCompany.DataSource = GetEQDematAccGenericDictionary();
                ddlListCompany.DataTextField = "Key";
                ddlListCompany.DataValueField = "Value";
                ddlListCompany.DataBind();
                ddlListCompany.Items.Insert(0, new ListItem("Select Source Type", "Select Source Type"));

                //Fill Extension types for Selected Asset
                //ddlListExtensionType.DataSource = GetEQDematAccExtensions();
                //ddlListExtensionType.DataTextField = "Key";
                //ddlListExtensionType.DataValueField = "Value";
                //ddlListExtensionType.DataBind();
                //ddlListExtensionType.Items.Insert(0, new ListItem("Select File Extension", "Select File Extension"));
            }
            else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTransaction)
            {   // EQ Transaction

                //fill External Type dropdownbox
                ddlListCompany.DataSource = GetEQTranxGenericDictionary();
                ddlListCompany.DataTextField = "Key";
                ddlListCompany.DataValueField = "Value";
                ddlListCompany.DataBind();
                ddlListCompany.Items.Insert(0, new ListItem("Select Source Type", "Select Source Type"));
                //Label6.Visible = false;
                //rbSkipRowsYes.Visible = false;
                SkiprowsVisible.Visible = false;

                //Fill Extension types for Selected Asset
                //ddlListExtensionType.DataSource = GetEQTranxExtensions();
                //ddlListExtensionType.DataTextField = "Key";
                //ddlListExtensionType.DataValueField = "Value";
                //ddlListExtensionType.DataBind();
                //ddlListExtensionType.Items.Insert(0, new ListItem("Select File Extension", "Select File Extension"));
            }
            else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFSystematic)
            {   // MF Systematic Setup

                //fill External Type dropdownbox
                ddlListCompany.DataSource = GetMFSystematicGenericDictionary();
                ddlListCompany.DataTextField = "Key";
                ddlListCompany.DataValueField = "Value";
                ddlListCompany.DataBind();
                ddlListCompany.Items.Insert(0, new ListItem("Select Source Type", "Select Source Type"));

                //Fill Extension types for Selected Asset
                //ddlListExtensionType.DataSource = GetMFSystematicExtensions();
                //ddlListExtensionType.DataTextField = "Key";
                //ddlListExtensionType.DataValueField = "Value";
                //ddlListExtensionType.DataBind();
                //ddlListExtensionType.Items.Insert(0, new ListItem("Select File Extension", "Select File Extension"));
            }
            //List Added For Trail Commission
            else if (ddlUploadType.SelectedValue == "TRAIL")
            {
                ddlListCompany.Items.Clear();
                ddlListCompany.Items.Add("CAMS");
                ddlListCompany.Items.Add("KARVY");
                ddlListCompany.Items.Add("Templeton");
                ddlListCompany.Items.Add("Deutsche");
                ddlListCompany.Items.Add("Sundaram");
                ddlListCompany.Items.Insert(0, new ListItem("Select Source Type", "Select Source Type"));
            }
            //List Added For ODIN 
            else if (ddlUploadType.SelectedValue == "ODIN")
            {
                List<object> ListA = new List<object>();
                ListA.Add(new
                {
                    oDid = "ODNSE",
                    oDName = "NSE"
                }
                );

                ListA.Add(new
                {
                    oDid = "ODBSE",
                    oDName = "BSE"
                }
                );

                ddlListCompany.DataSource = ListA;
                ddlListCompany.DataTextField = "oDName";
                ddlListCompany.DataValueField = "oDid";
                ddlListCompany.DataBind();


                ddlListCompany.Items.Insert(0, new ListItem("Select Source Type", "Select Source Type"));
            }
            else
            {
                ddlListCompany.Items.Clear();
                //ddlListExtensionType.Items.Clear();
            }
        }

        /*  Code For Downloading standerd file formats */

        //protected void LnkBtnPup_Click(object sender, EventArgs e)
        //{
        //    ModalPopupExtender1.TargetControlID = "LnkBtnPup";
        //    ModalPopupExtender1.Show();
        //}


        /*  ***************************************** */
        /*  Coding for Download Standerd upload files */


        //protected void lnkbtnpup_Click1(object sender, EventArgs e)
        //{
        //    ModalPopupExtender1.TargetControlID = "lnkbtnpup";
        //    ModalPopupExtender1.Show();

        //}

        protected void lnkbtnpup_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.TargetControlID = "btnPopUp";
            ModalPopupExtender1.Show();

        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (File1.Checked)
            {
                Response.Redirect("Standard Upload Files/EquityTradeAccount.xlsx");
            }
            else if (File2.Checked)
            {
                Response.Redirect("Standard Upload Files/EquityTransaction.xlsx");
            }
            else if (File3.Checked)
            {
                Response.Redirect("Standard Upload Files/MFFolio.xlsx");
            }
            else if (File4.Checked)
            {
                Response.Redirect("Standard Upload Files/MFTransaction.xlsx");
            }
            else if (File5.Checked)
            {
                Response.Redirect("Standard Upload Files/CustomerProfile.xlsx");
            }
            else if (File6.Checked)
            {
                Response.Redirect("Standard Upload Files/SystematicStandard.xlsx");
            }
            else if (File7.Checked)
            {
                Response.Redirect("Standard Upload Files/BSEForOdin.xlsx");
            }
            else if (File8.Checked)
            {
                Response.Redirect("Standard Upload Files/NSEForOdin");
            }
            else if (AllFiles.Checked)
            {
                Response.Redirect("Standard Upload Files/All Standard Upload Files.zip");
            }
        }


        /* *** */
        /* End */


        protected void ddlListCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            datevisible.Visible = false;
            txtUploadDate.Visible = false;
            lbldate.Visible = false;
            upload.Visible = false;
            lbluploadtype.Visible = false;
            ddlAction.Visible = false;
            Label6.Visible = true;
            rbSkipRowsYes.Visible = true;
            rbSkipRowsNo.Visible = true;
            SkiprowsVisible.Visible = true;
            //Span4.visible = false;
            if (ddlListCompany.SelectedIndex != 0)
            {
                //trFileTypeRow.Visible = true;
            }
            else
            {
                //trFileTypeRow.Visible = false;
            }
            //***Dropdown screen info coding***

            if (ddlUploadType.SelectedValue == "PMFF" || ddlUploadType.SelectedValue == "MFT" || ddlUploadType.SelectedValue == "TRAIL" || ddlUploadType.SelectedValue == "ODIN" || ddlUploadType.SelectedValue == "EQT")
            {
                lblFileType.Visible = true;
                message = Show_Message(ddlUploadType.SelectedValue, ddlListCompany.SelectedValue);
                lblFileType.Text = "Please use the &nbsp;" + message + "&nbsp; File provided by the R&T to Upload.";

            }

            else
            {
                lblFileType.Visible = false;

            }
            //Hide Skip Rows for MF Folo Upload - vishal
            if (ddlUploadType.SelectedValue == "MFFO" || ddlUploadType.SelectedValue == "MFF")
            {
                Label6.Visible = false;
                rbSkipRowsYes.Visible = false;
                rbSkipRowsNo.Visible = false;
                SkiprowsVisible.Visible = false;


            }
            if (ddlUploadType.SelectedValue == "P" && ddlListCompany.SelectedValue == "WP")
            {
                //lnkbtnpup.Style.Add("display", "block");
                //Panel1.Style.Add("display", "block");

                lnkbtnpup.Visible = true;

            }
            else if (ddlUploadType.SelectedValue == "MFF" && ddlListCompany.SelectedValue == "WP")
            {
                lnkbtnpup.Visible = true;
            }
            else
            {
                //lnkbtnpup.Style.Add("display", "none");
                //Panel1.Style.Add("display", "none");

                lnkbtnpup.Visible = false;

            }
            if (ddlUploadType.SelectedValue == "EQT" && ddlListCompany.SelectedValue == "IIFL")
            {
                datevisible.Visible = true;
                txtUploadDate.Visible = true;
                lbldate.Visible = true;
            }
            if (ddlUploadType.SelectedValue == "EQT" && ddlListCompany.SelectedValue == "ODIN")
            {
                lnkbtnpup.Visible = true;
                upload.Visible = true;
                lbluploadtype.Visible = true;
                ddlAction.Visible = true;
            }
            if (ddlUploadType.SelectedValue == "MFSS" && ddlListCompany.SelectedValue == "WPT")
            {
                lnkbtnpup.Visible = true;
            }

        }

        protected void lnkbtnpup_Click1(object sender, EventArgs e)
        {
            ModalPopupExtender1.TargetControlID = "lnkbtnpup";
            ModalPopupExtender1.Show();

        }

        protected string Show_Message(string ddlUploadType, string ddlCompanyType)
        {
            string fileName = ddlListCompany.SelectedValue;
            string msg = "";
            if (ddlUploadType == "PMFF" && ddlCompanyType == "CA")
            {
                msg = "WBR-9";
            }
            else if (ddlUploadType == "PMFF" && ddlCompanyType == "KA")
            {
                msg = "MFSD-221 Combo";
            }
            else if (ddlUploadType == "MFT" && ddlCompanyType == "CA")
            {
                msg = "WBR-2";
            }
            else if (ddlUploadType == "MFT" && ddlCompanyType == "KA")
            {
                msg = "MFSD-221 Combo";
            }
            else if ((ddlUploadType == "MFT" && ddlCompanyType == "TN") || (ddlUploadType == "PMFF" && ddlCompanyType == "TN"))
            {
                msg = "Standard Combo";
            }
            else if ((ddlUploadType == "PMFF" && ddlCompanyType == "DT"))
            {
                msg = "Deautsche Client Master";
            }

            else if ((ddlUploadType == "MFT" && ddlCompanyType == "DT"))
            {
                msg = "Deautsche Transaction file";
            }
            else if (ddlUploadType == "TRAIL")
            {
                msg = "Trail Commission" + " " + fileName;
            }
            else if ((ddlUploadType == "MFT" && ddlCompanyType == "SU"))
            {
                msg = "ER-02";
            }
            else if ((ddlUploadType == "PMFF" && ddlCompanyType == "SU"))
            {
                msg = "ER - 04";
            }
            else if ((ddlUploadType == "ODIN" && ddlCompanyType == "NSE"))
            {
                msg = "0330TRD";
            }
            else if ((ddlUploadType == "ODIN" && ddlCompanyType == "BSE"))
            {
                msg = "TR10022012";
            }
            else if ((ddlUploadType == "EQT" && ddlCompanyType == "ODIN"))
            {
                if (ddlAction.SelectedValue == "NSE")
                {
                    msg = "NSE TRD";
                    lnkbtnpup.Visible = true;
                }
                else if (ddlAction.SelectedValue == "BSE")
                {
                    msg = "TR BSE";
                    lnkbtnpup.Visible = true;
                }
            }
            return msg;
        }

        protected void rbSkipRowsNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSkipRowsNo.Checked == true)
                trNoOfRows.Visible = false;
        }

        protected void rbSkipRowsYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSkipRowsYes.Checked == true)
                trNoOfRows.Visible = true;
        }

        protected bool GetInputXML()
        {
            bool XmlCreated = false;
            int skiprowsval = 1;

            ReadExternalFile readFile = new ReadExternalFile();

            UploadCommonBo uploadcommonBo = new UploadCommonBo();
            int filetypeid = 0; // holds filtype id for the type of file which is to be ulpoaded
            DataSet ds = new DataSet(); //holds data from the file

            string pathxml = "";

            // This dataset stores the values of actual colum names for the file type with Mandatory flags
            DataSet dsColumnNames = new DataSet();
            DataSet dsWerpColumnNames = new DataSet();

            //This dataset is used as data for the final XML to be created.
            //DataSet dsXML = new DataSet();

            int indexOfExtension = FileUpload.FileName.LastIndexOf('.');
            int length = FileUpload.FileName.Length;
            string UploadFileName = FileUpload.FileName.ToString();
            string extension = (UploadFileName.Substring(indexOfExtension + 1)).ToLower();
            string strFileReadError = string.Empty;

            pathxml = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            try
            {
                bool filereadflag = true;

                #region Mf Deutsche Transaction  upload
                //Read File for Mf Deutsche Transaction  upload
                if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeDeutsche)
                {
                    if (extension == "dbf")
                    {
                        string filename = "DTD.dbf";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadDBFFile(filepath, filename, out strFileReadError);

                        if (strFileReadError != "")
                        {
                            filereadflag = false;
                            rejectUpload_Flag = true;
                            reject_reason = strFileReadError;
                        }
                    }
                    else if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\DeutscheTransXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);
                    }

                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                    if (filereadflag == true)
                    {
                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);

                            //for getting line number of error data in the file when validating
                            skiprowsval = Convert.ToInt16(txtNoOfRows.Text) + 1;
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.DeutscheTransaction);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.DeutscheTransaction);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", Contants.UploadExternalTypeDeutsche, Contants.UploadFileTypeTransaction);

                        //Reject upload if there are any data error validations
                        if (dsXML.Tables.Count > 0)
                            ValidateInputfile(Contants.UploadExternalTypeDeutsche, Contants.ExtractTypeMFTransaction, pathxml, skiprowsval);



                    }
                }
                #endregion

                //***********gobinda systematic uploads for transaction and profile***************\\
                #region Mf Systematic Transaction  upload
                //Read File for Mf Systematic Transaction  upload
                if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction && ddlListCompany.SelectedValue == "WPT")
                {
                    if (extension == "dbf")
                    {
                        string filename = "DTD.dbf";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadDBFFile(filepath, filename, out strFileReadError);

                        if (strFileReadError != "")
                        {
                            filereadflag = false;
                            rejectUpload_Flag = true;
                            reject_reason = strFileReadError;
                        }
                    }
                    else if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\StandardTransFolioXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);
                    }

                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                    if (filereadflag == true)
                    {
                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);

                            //for getting line number of error data in the file when validating
                            skiprowsval = Convert.ToInt16(txtNoOfRows.Text) + 1;
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.MFStandardTransaction);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.MFStandardTransaction);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", Contants.UploadExternalTypeStandard, Contants.UploadFileTypeTransaction);

                        //Reject upload if there are any data error validations
                        if (dsXML.Tables.Count > 0)
                            ValidateInputfile(Contants.UploadExternalTypeStandard, Contants.ExtractTypeMFTransaction, pathxml, skiprowsval);



                    }
                }
                #endregion


                #region CAMS PRofile and Folio or Folio only
                //CAMS Profile and Folio or profile only
                else if ((ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio) && ddlListCompany.SelectedValue == Contants.UploadExternalTypeCAMS)
                {
                    if (extension == "dbf")
                    {
                        string filename = "CPD.dbf";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadDBFFile(filepath, filename, out strFileReadError);
                        if (strFileReadError == "")
                        {

                            //ds.Tables[0].Columns[15].ColumnName = "JOINT_NAME2";
                            //ds.Tables[0].Columns[14].ColumnName = "JOINT_NAME1";
                            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                            {
                                if (ds.Tables[0].Columns[i].ColumnName == "CPD#dbf.JOINT_NAME")
                                    ds.Tables[0].Columns[i].ColumnName = "JOINT_NAME1";

                                if (ds.Tables[0].Columns[i].ColumnName == "CPD#dbf.JOINT_NAME1")
                                    ds.Tables[0].Columns[i].ColumnName = "JOINT_NAME2";
                            }


                        }
                        else
                        {
                            filereadflag = false;
                            rejectUpload_Flag = true;
                            reject_reason = strFileReadError;
                        }
                    }

                    else if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\CAMSProfileXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        //ds.Tables[0].Columns[15].ColumnName = "JOINT_NAME2";
                        //ds.Tables[0].Columns[14].ColumnName = "JOINT_NAME1";


                    }

                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                    if (filereadflag == true)
                    {
                        //Skip rows if adiver wants to
                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.CAMSProfile);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.CAMSProfile);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", Contants.UploadExternalTypeCAMS, Contants.UploadFileTypeProfile);
                    }

                }
                #endregion

                #region CAMS Transaction
                //Read File for Mf CAMS Transaction DBF Upload
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeCAMS)
                {
                    if (extension == "dbf")
                    {
                        string filename = "CTD.dbf";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadDBFFile(filepath, filename, out strFileReadError);
                        if (strFileReadError == "")
                        {
                            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                            {
                                if (ds.Tables[0].Columns[i].ColumnName == "CTD#dbf.TAX_STATUS")
                                    ds.Tables[0].Columns[i].ColumnName = "TAX_STATUS";

                                if (ds.Tables[0].Columns[i].ColumnName == "CTD#dbf.TAX_STATUS1")
                                    ds.Tables[0].Columns[i].ColumnName = "TAX_STATUS1";
                            }

                        }
                        else
                        {
                            filereadflag = false;
                            rejectUpload_Flag = true;
                            reject_reason = strFileReadError;
                        }
                    }

                    else if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\CAMSTransactionXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);
                        //ds.Tables[0].Columns[36].ColumnName = "TAX_STATUS1";
                        //ds.Tables[0].Columns[26].ColumnName = "TAX_STATUS";
                    }

                    else
                    {
                        ValidationProgress = "Failure";
                    }
                    if (filereadflag == true)
                    {

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);

                            //for getting line number of error data in the file when validating
                            skiprowsval = Convert.ToInt16(txtNoOfRows.Text) + 1;
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.CAMSTransaction);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.CAMSTransaction);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", Contants.UploadExternalTypeCAMS, Contants.UploadFileTypeTransaction);

                        //Reject upload if there are any data error validations
                        if (dsXML.Tables.Count > 0)
                            ValidateInputfile(Contants.UploadExternalTypeCAMS, Contants.ExtractTypeMFTransaction, pathxml, skiprowsval);


                    }
                }
                #endregion

                #region CAMS Systamatic
                //Read File for Mf CAMS Systematic XLS Upload
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio && ddlListCompany.SelectedValue == Contants.UploadExternalTypeCAMS)
                {
                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\CAMSSystamaticXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames(0);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames(0);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);


                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, Contants.ExtractTypeProfileFolio, Contants.UploadExternalTypeCAMS, Contants.UploadFileTypeSystematic);
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                }
                #endregion


                //**************Gobinda***********************
                #region Systematic Standard
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFSystematic && ddlListCompany.SelectedValue == "WPT")
                {
                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\CAMSSystamaticXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames(26);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames(26);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);


                        //foreach(DataRow dr in dsXML.Tables[0].Rows)
                        //{
                        //    if(dr["PERIODICIT"].ToString()=="SM")
                        //    {
                        //        string[] toPERIOD = (dr["PERIOD_DAY"].ToString()).Split(new char[] { ',' });
                        //    }

                        //}


                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", "WPT", Contants.UploadFileTypeSystematic);
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                }
                #endregion



                //**************Gobinda Trail Comission For Templeton***********************
                #region Trail Commision for Templeton
                else if (ddlUploadType.SelectedValue == "TRAIL" && ddlListCompany.SelectedValue == "Templeton")
                {
                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\TempletonTrailCommission.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames(28);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames(28);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);


                        //foreach(DataRow dr in dsXML.Tables[0].Rows)
                        //{
                        //    if(dr["PERIODICIT"].ToString()=="SM")
                        //    {
                        //        string[] toPERIOD = (dr["PERIOD_DAY"].ToString()).Split(new char[] { ',' });
                        //    }

                        //}


                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", "TN", "TrailCommission");
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                }
                #endregion


                //**************Gobinda Trail Comission For CAMS***********************
                #region Trail Commision for Cams
                else if (ddlUploadType.SelectedValue == "TRAIL" && ddlListCompany.SelectedValue == "CAMS")
                {
                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\CAMSTrailCommission.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames(29);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames(29);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);


                        //foreach(DataRow dr in dsXML.Tables[0].Rows)
                        //{
                        //    if(dr["PERIODICIT"].ToString()=="SM")
                        //    {
                        //        string[] toPERIOD = (dr["PERIOD_DAY"].ToString()).Split(new char[] { ',' });
                        //    }

                        //}


                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", "CA", "TrailCommission");
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                }
                #endregion


                //**************Gobinda Trail Comission For KARVY***********************
                #region Trail Commision for KARVY
                else if (ddlUploadType.SelectedValue == "TRAIL" && ddlListCompany.SelectedValue == "KARVY")
                {
                    if (extension == "xls" || extension == "xlsx" || extension == "dbf")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\KARVYTrailCommission.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames(30);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames(30);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);


                        //foreach(DataRow dr in dsXML.Tables[0].Rows)
                        //{
                        //    if(dr["PERIODICIT"].ToString()=="SM")
                        //    {
                        //        string[] toPERIOD = (dr["PERIOD_DAY"].ToString()).Split(new char[] { ',' });
                        //    }

                        //}


                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", "KA", "TrailCommission");
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                }
                #endregion


                //**************Gobinda Trail Comission For DEUTSCHE***********************
                #region Trail Commision for Deutsche
                else if (ddlUploadType.SelectedValue == "TRAIL" && ddlListCompany.SelectedValue == "Deutsche")
                {
                    if (extension == "xls" || extension == "xlsx" || extension == "dbf")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\DeutscheTrailCommission.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames(31);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames(31);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);


                        //foreach(DataRow dr in dsXML.Tables[0].Rows)
                        //{
                        //    if(dr["PERIODICIT"].ToString()=="SM")
                        //    {
                        //        string[] toPERIOD = (dr["PERIOD_DAY"].ToString()).Split(new char[] { ',' });
                        //    }

                        //}


                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", "DT", "TrailCommission");
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                }
                #endregion


                //--------Gobinda KRAVY Uploads---------\\

                #region Systematic KARVY
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFSystematic && ddlListCompany.SelectedValue == "KA")
                {
                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\CAMSSystamaticXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames(27);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames(27);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);


                        //foreach(DataRow dr in dsXML.Tables[0].Rows)
                        //{
                        //    if(dr["PERIODICIT"].ToString()=="SM")
                        //    {
                        //        string[] toPERIOD = (dr["PERIOD_DAY"].ToString()).Split(new char[] { ',' });
                        //    }

                        //}


                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", "KA", Contants.UploadFileTypeSystematic);
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                }
                #endregion




                //------------Shantanu--------------------------------


                #region CAMS Systematic
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFSystematic && ddlListCompany.SelectedValue == Contants.UploadExternalTypeCAMS)
                {
                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\CAMSSystamaticXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames(20);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames(20);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);


                        //foreach(DataRow dr in dsXML.Tables[0].Rows)
                        //{
                        //    if(dr["PERIODICIT"].ToString()=="SM")
                        //    {
                        //        string[] toPERIOD = (dr["PERIOD_DAY"].ToString()).Split(new char[] { ',' });
                        //    }

                        //}


                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", "CA", Contants.UploadFileTypeSystematic);
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                }
                #endregion

                #region Mf Karvy Profile
                //Read File for Mf Karvy Profile 
                else if ((ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio) && ddlListCompany.SelectedValue == Contants.UploadExternalTypeKarvy)
                {
                    if (extension == "dbf")
                    {
                        string filename = "KPD.dbf";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadDBFFile(filepath, filename, out strFileReadError);
                        if (strFileReadError == "")
                        {


                        }
                        else
                        {
                            filereadflag = false;
                            rejectUpload_Flag = true;
                            reject_reason = strFileReadError;
                        }
                    }
                    else if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\KarvyProfileXls.xls";

                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                    if (filereadflag == true)
                    {
                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.KarvyProfile);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.KarvyProfile);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", Contants.UploadExternalTypeKarvy, Contants.UploadFileTypeProfile);
                    }
                }
                #endregion

                #region Karvy Transaction
                //Read File for Mf Karvy Transaction 
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeKarvy)
                {
                    if (extension == "dbf")
                    {

                        string filename = "KTD.dbf";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadDBFFile(filepath, filename, out strFileReadError);
                        if (strFileReadError == "")
                        {


                        }
                        else
                        {
                            filereadflag = false;
                            rejectUpload_Flag = true;
                            reject_reason = strFileReadError;
                        }
                    }
                    else if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\KarvyTransactionXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                    if (filereadflag == true)
                    {
                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);

                            //for getting line number of error data in the file when validating
                            skiprowsval = Convert.ToInt16(txtNoOfRows.Text) + 1;
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.KarvyTransaction);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.KarvyTransaction);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", Contants.UploadExternalTypeKarvy, Contants.UploadFileTypeTransaction);

                        //Reject upload if there are any data error validations
                        if (dsXML.Tables.Count > 0)
                            ValidateInputfile(Contants.UploadExternalTypeKarvy, Contants.ExtractTypeMFTransaction, pathxml, skiprowsval);

                    }
                }
                #endregion

                #region Mf Karvy Combination
                //Read File for Mf Karvy Combination DBF Upload
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio && ddlListCompany.SelectedValue == Contants.UploadExternalTypeKarvy)
                {
                    if (extension == "dbf")
                    {
                        string filename = "KCD.dbf";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadDBFFile(filepath, filename, out strFileReadError);
                        if (strFileReadError == "")
                        {


                        }
                        else
                        {
                            filereadflag = false;
                            rejectUpload_Flag = true;
                            reject_reason = strFileReadError;
                        }
                    }
                    else if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\KarvyCombinationXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                    if (filereadflag == true)
                    {
                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.KarvyCombination);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.KarvyCombination);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, Contants.ExtractTypeProfileFolio, Contants.UploadExternalTypeKarvy, Contants.UploadFileTypeCombination);
                    }
                }
                #endregion

                #region Sundaram Transaction
                //Read File for Mf CAMS Transaction DBF Upload
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction && ddlListCompany.SelectedValue == "SU")
                {
                    if (extension == "dbf")
                    {
                        string filename = "CTD.dbf";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadDBFFile(filepath, filename, out strFileReadError);
                        if (strFileReadError == "")
                        {
                            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                            {
                                if (ds.Tables[0].Columns[i].ColumnName == "CTD#dbf.TAX_STATUS")
                                    ds.Tables[0].Columns[i].ColumnName = "TAX_STATUS";

                                if (ds.Tables[0].Columns[i].ColumnName == "CTD#dbf.TAX_STATUS1")
                                    ds.Tables[0].Columns[i].ColumnName = "TAX_STATUS1";
                            }

                        }
                        else
                        {
                            filereadflag = false;
                            rejectUpload_Flag = true;
                            reject_reason = strFileReadError;
                        }
                    }

                    else if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\CAMSTransactionXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);
                        //ds.Tables[0].Columns[36].ColumnName = "TAX_STATUS1";
                        //ds.Tables[0].Columns[26].ColumnName = "TAX_STATUS";
                    }

                    else
                    {
                        ValidationProgress = "Failure";
                    }
                    if (filereadflag == true)
                    {

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);

                            //for getting line number of error data in the file when validating
                            skiprowsval = Convert.ToInt16(txtNoOfRows.Text) + 1;
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.CAMSTransaction);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.CAMSTransaction);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", "SU", Contants.UploadFileTypeTransaction);

                        //Reject upload if there are any data error validations
                        if (dsXML.Tables.Count > 0)
                            ValidateInputfile(Contants.UploadExternalTypeCAMS, Contants.ExtractTypeMFTransaction, pathxml, skiprowsval);


                    }
                }
                #endregion

                #region Templeton Profile
                //Read File for Mf Templeton Profile DBF Upload
                else if ((ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio) && ddlListCompany.SelectedValue == Contants.UploadExternalTypeTemp)
                {
                    if (extension == "dbf")
                    {
                        string filename = "TPD.dbf";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadDBFFile(filepath, filename, out strFileReadError);
                        if (strFileReadError == "")
                        {

                        }
                        else
                        {
                            filereadflag = false;
                            rejectUpload_Flag = true;
                            reject_reason = strFileReadError;
                        }
                    }
                    else if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\TPX.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                    if (filereadflag == true)
                    {
                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.TempletonProfile);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.TempletonProfile);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", Contants.UploadExternalTypeTemp, Contants.UploadFileTypeProfile);
                    }
                }
                #endregion

                #region Mf Deutsche Profile
                //Read File for Mf Deutsche Profile 
                else if ((ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio) && ddlListCompany.SelectedValue == Contants.UploadExternalTypeDeutsche)
                {
                    if (extension == "dbf")
                    {
                        string filename = "DPD.dbf";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadDBFFile(filepath, filename, out strFileReadError);
                        if (strFileReadError == "")
                        {


                        }

                        else
                        {
                            filereadflag = false;
                            rejectUpload_Flag = true;
                            reject_reason = strFileReadError;
                        }
                    }
                    else if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\DPX.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                    if (filereadflag == true)
                    {
                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.DeutscheProfile);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.DeutscheProfile);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", Contants.UploadExternalTypeDeutsche, Contants.UploadFileTypeProfile);
                    }
                }
                #endregion

                #region NSE Odin
                //Equity NSE Odin XLS
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTradeAccount && ddlListCompany.SelectedValue == Contants.UploadExternalTypeOdinNSE)
                {
                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\OdinNSEXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);


                    }
                    else if (extension == "csv")
                    {
                        string filename = "ONC.csv";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadCSVFile(filepath, filename);
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                        filereadflag = false;
                    }
                    if (filereadflag == true)
                    {
                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.OdinNSETransaction);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.OdinNSETransaction);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, Contants.ExtractTypeEQTradeAccount, Contants.UploadExternalTypeOdinNSE, Contants.UploadFileTypeTransaction);

                    }

                }
                #endregion

                #region BSE Odin
                //Equity BSE Odin XLS
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTradeAccount && ddlListCompany.SelectedValue == Contants.UploadExternalTypeOdinBSE)
                {
                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\OdinBSEXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);


                    }
                    else if (extension == "csv")
                    {
                        string filename = "ONC.csv";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadCSVFile(filepath, filename);
                    }

                    else
                    {
                        //ValidationProgress = "Failure";
                        filereadflag = false;
                    }
                    if (filereadflag == true)
                    {
                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.OdinNSETransaction);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.OdinNSETransaction);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, Contants.ExtractTypeEQTradeAccount, Contants.UploadExternalTypeOdinBSE, Contants.UploadFileTypeTransaction);

                    }

                }
                #endregion

                #region Standard Equity Trade Account
                // Standard Equity Trade Account
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTradeAccount && ddlListCompany.SelectedValue == Contants.UploadExternalTypeStandard)
                {
                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\WERPEqProf.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.EquityStandardTradeAccount);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.EquityStandardTradeAccount);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "EQ", Contants.UploadExternalTypeStandard, Contants.UploadFileTypeTradeAccount);
                    }
                    else
                    {
                        XmlCreated = false;
                        ValidationProgress = "Failure";
                    }
                }
                #endregion

                #region Standard MF Folio Upload Vishal
                // Standard Equity Trade Account
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeFolio && ddlListCompany.SelectedValue == Contants.UploadExternalTypeStandard)
                {
                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\WERPEqProf.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.MFstandardFolio);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.MFstandardFolio);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);
                        if (dsXML.Tables.Count > 0)
                        {
                            int count = dsXML.Tables[0].Rows.Count;
                            txtExternalTotalRecords.Text = dsXML.Tables[0].Rows.Count.ToString();


                            //Get filetypeid from XML
                            filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", Contants.UploadExternalTypeStandard, Contants.UploadFileTypeStdFolio);
                        }
                        else
                        {
                            XmlCreated = false;
                            ValidationProgress = "Failure";
                        }
                    }
                    else
                    {
                        XmlCreated = false;
                        ValidationProgress = "Failure";
                    }
                }
                #endregion


                #region Standard Equity Transaction
                // Standard Equity Transaction
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeStandard)
                {
                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\WERPEqTrans.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }
                        ds.Tables[0].Columns.Add("column25");
                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.EquityStandardTransaction);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.EquityStandardTransaction);


                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);
                        if (dsXML.Tables.Count > 0)

                            ValidateInputfile(Contants.ExtractTypeEQTransaction, Contants.UploadExternalTypeStandard, pathxml, skiprowsval);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "EQ", Contants.UploadExternalTypeStandard, Contants.UploadFileTypeTransaction);
                    }
                    else
                    {
                        XmlCreated = false;
                        ValidationProgress = "Failure";
                    }
                }
                #endregion

                #region IIFL Equity Transaction

                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeIIFL)
                {
                    DataSet dsIIFLTemp = new DataSet();
                    DataSet dsStdColNames = new DataSet();
                    DataTable dtIIFL = new DataTable();
                    int ifl = 0;

                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\IIFLEqTrans.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile1(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }
                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.IIFLTransaction);

                        //get all column nams for the standard eq transaction type
                        dsStdColNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.EquityStandardTransaction);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.IIFLTransaction);

                        DataSet ds1 = new DataSet();
                        //Get XML after mapping, checking for columns
                        dsIIFLTemp = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);
                        //foreach (DataRow de in dsIIFLTemp.Tables[0].Rows)
                        //{

                        //    if (de[0].ToString() != "NSE" || de[0].ToString() != "BSE")
                        //    {
                        //        de[0] = 1;
                        //    }
                        //    ds1.Tables[0].Rows.Add(de[0]);
                        //}

                        //DataSet dsXMLtemp = new DataSet();
                        dsXML = dsIIFLTemp;

                        if (dsXML.Tables.Count > 0)

                            ValidateInputfile(Contants.UploadExternalTypeIIFL, Contants.UploadFileTypeTransaction, pathxml, skiprowsval);

                        if (badData == false)
                        {
                            DataRow dr1;

                            foreach (DataRow dr in dsStdColNames.Tables[0].Rows)
                            {
                                dtIIFL.Columns.Add(dr["XMLHeaderName"].ToString());
                            }

                            foreach (DataRow dr in dsIIFLTemp.Tables[0].Rows)
                            {

                                if (dr.ItemArray[0].ToString() == "NSE" || dr.ItemArray[0].ToString() == "BSE")
                                {
                                    if (dr.ItemArray[0].ToString() == "NSE")
                                    {
                                        if (double.Parse(dr.ItemArray[5].ToString()) > 0)
                                        {
                                            dr1 = dtIIFL.NewRow();

                                            dr1["Trade_Account_Number"] = dr["Client Code"].ToString();
                                            //dt.Rows[i]["Trade_Account_Number"] = dr["Client Code"].ToString();
                                            dr1["PESM_Identifier"] = dr["Scrip Name"].ToString();
                                            dr1["WETT_TransactionCode"] = 1;
                                            dr1["CET_TradeDate"] = txtUploadDate.Text.ToString();
                                            dr1["CET_Rate"] = float.Parse(dr["Avg Buy Rate"].ToString()).ToString();
                                            dr1["CET_Quantity"] = dr["Buy Qty"].ToString();
                                            dr1["CET_BuySell"] = "B";
                                            dr1["XE_ExchangeCode"] = "NSE";
                                            dr1["CET_Brokerage"] = 0.00;
                                            dr1["CET_ServiceTax"] = 0.00;
                                            dr1["CET_EducationCess"] = 0.00;
                                            dr1["CET_STT"] = 0.00;
                                            dr1["CET_OtherCharges"] = 0.00;
                                            dr1["CET_RateInclBrokerage"] = 0.00;
                                            dr1["CET_TradeTotal"] = 0.00;
                                            dr1["CET_TradeNum"] = 0;
                                            dr1["CET_OrderNum"] = 0;
                                            dr1["CET_IsSpeculative"] = 0;
                                           

                                            ifl++;
                                            dtIIFL.Rows.Add(dr1);
                                        }

                                        if (dr.ItemArray[0].ToString() == "NSE" && double.Parse(dr.ItemArray[8].ToString()) > 0)
                                        {
                                            dr1 = dtIIFL.NewRow();
                                            dr1["Trade_Account_Number"] = dr["Client Code"].ToString();
                                            dr1["PESM_Identifier"] = dr["Scrip Name"].ToString();
                                            dr1["WETT_TransactionCode"] = 2;
                                            dr1["CET_TradeDate"] = txtUploadDate.Text.ToString();
                                            dr1["CET_Rate"] = dr["Avg Sell Rate"].ToString();
                                            dr1["CET_Quantity"] = dr["Sell Qty"].ToString();
                                            dr1["CET_BuySell"] = "S";
                                            dr1["XE_ExchangeCode"] = "NSE";
                                            dr1["CET_Brokerage"] = 0;
                                            dr1["CET_ServiceTax"] = 0;
                                            dr1["CET_EducationCess"] = 0;
                                            dr1["CET_STT"] = 0;
                                            dr1["CET_OtherCharges"] = 0;
                                            dr1["CET_RateInclBrokerage"] = 0;
                                            dr1["CET_TradeTotal"] = 0;
                                            dr1["CET_TradeNum"] = 0;
                                            dr1["CET_OrderNum"] = 0;
                                            dr1["CET_IsSpeculative"] = 0;
                                          

                                            ifl++;
                                            dtIIFL.Rows.Add(dr1);

                                        }
                                    }

                                    else if (dr.ItemArray[0].ToString() == "BSE")
                                    {
                                        if (double.Parse(dr.ItemArray[5].ToString()) > 0)
                                        {
                                            dr1 = dtIIFL.NewRow();
                                            dr1["Trade_Account_Number"] = dr["Client Code"].ToString();
                                            dr1["PESM_Identifier"] = dr["Scrip Code"].ToString();
                                            dr1["WETT_TransactionCode"] = 1;
                                            dr1["CET_TradeDate"] = txtUploadDate.Text.ToString();
                                            dr1["CET_Rate"] = dr["Avg Buy Rate"].ToString();
                                            dr1["CET_Quantity"] = dr["Buy Qty"].ToString();
                                            dr1["CET_BuySell"] = "B";
                                            dr1["XE_ExchangeCode"] = "BSE";
                                            dr1["CET_Brokerage"] = 0;
                                            dr1["CET_ServiceTax"] = 0;
                                            dr1["CET_EducationCess"] = 0;
                                            dr1["CET_STT"] = 0;
                                            dr1["CET_OtherCharges"] = 0;
                                            dr1["CET_RateInclBrokerage"] = 0;
                                            dr1["CET_TradeTotal"] = 0;
                                            dr1["CET_TradeNum"] = 0;
                                            dr1["CET_OrderNum"] = 0;
                                            dr1["CET_IsSpeculative"] = 0;

                                            ifl++;
                                            dtIIFL.Rows.Add(dr1);
                                        }

                                        if (double.Parse(dr.ItemArray[8].ToString()) > 0)
                                        {
                                            dr1 = dtIIFL.NewRow();
                                            dr1["Trade_Account_Number"] = dr["Client Code"].ToString();
                                            dr1["PESM_Identifier"] = dr["Scrip Code"].ToString();
                                            dr1["WETT_TransactionCode"] = 2;
                                            dr1["CET_TradeDate"] = txtUploadDate.Text.ToString();
                                            dr1["CET_Rate"] = dr["Avg Sell Rate"].ToString();
                                            dr1["CET_Quantity"] = dr["Sell Qty"].ToString();
                                            dr1["CET_BuySell"] = "S";
                                            dr1["XE_ExchangeCode"] = "BSE";
                                            dr1["CET_Brokerage"] = 0;
                                            dr1["CET_ServiceTax"] = 0;
                                            dr1["CET_EducationCess"] = 0;
                                            dr1["CET_STT"] = 0;
                                            dr1["CET_OtherCharges"] = 0;
                                            dr1["CET_RateInclBrokerage"] = 0;
                                            dr1["CET_TradeTotal"] = 0;
                                            dr1["CET_TradeNum"] = 0;
                                            dr1["CET_OrderNum"] = 0;
                                            dr1["CET_IsSpeculative"] = 0;

                                            ifl++;
                                            dtIIFL.Rows.Add(dr1);
                                        }



                                    }

                                }

                            }

                            //DataSet dsXML = new DataSet();


                            dsXML.Tables.Remove("Table1");
                            dsXML.Tables.Add(dtIIFL);

                        }


                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "EQ", Contants.UploadExternalTypeIIFL, Contants.UploadFileTypeTransaction);

                        //dsXML.Tables.Add(dtIIFL);



                    }
                    else
                    {
                        XmlCreated = false;
                        ValidationProgress = "Failure";
                        //lblErrorFileType.Visible = true;
                    }
                }

                #endregion


                #region ODIN Equity BSE Transaction
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeODIN && (ddlAction.SelectedValue == "BSE" || ddlAction.SelectedValue == "NSE"))
                {
                    if (extension == "txt")
                    {
                        string odinFileName = (System.Guid.NewGuid()).ToString();

                        string Filepath = Server.MapPath("UploadFiles") + "\\" + odinFileName + ".xls";
                        FileUpload.SaveAs(Filepath);
                        string filetype = ddlAction.SelectedValue.ToString();
                        ds = readFile.ReadTxtFile(Filepath, filetype);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        dsXML = ds;
                        string uploadtype = ddlAction.SelectedValue;
                        if (dsXML.Tables.Count > 0)

                            ValidateInputfile1(Contants.UploadExternalTypeODIN, Contants.UploadFileTypeTransaction, pathxml, skiprowsval, uploadtype);

                        if (badData == false)
                        {

                            if (ds.Tables[0].Columns.Count == 26 || ds.Tables[0].Columns.Count == 16)
                            {
                                DataSet dsStdColNames = new DataSet();
                                DataTable dt = new DataTable();
                                DataRow dr1;
                                int i = 0;
                                DataTable dtOdin = new DataTable();
                                dtOdin.Columns.Add("Trade_Account_Number");
                                dtOdin.Columns.Add("PESM_Identifier");
                                dtOdin.Columns.Add("WETT_TransactionCode");
                                dtOdin.Columns.Add("CET_TradeDate");
                                dtOdin.Columns.Add("CET_Rate");
                                dtOdin.Columns.Add("CET_Quantity");
                                dtOdin.Columns.Add("CET_BuySell");
                                dtOdin.Columns.Add("XE_ExchangeCode");
                                dtOdin.Columns.Add("CET_Brokerage");
                                dtOdin.Columns.Add("CET_ServiceTax");
                                dtOdin.Columns.Add("CET_EducationCess");
                                dtOdin.Columns.Add("CET_STT");
                                dtOdin.Columns.Add("CET_OtherCharges");
                                dtOdin.Columns.Add("CET_RateInclBrokerage");
                                dtOdin.Columns.Add("CET_TradeTotal");
                                dtOdin.Columns.Add("CET_TradeNum");
                                dtOdin.Columns.Add("CET_OrderNum");
                                dtOdin.Columns.Add("CET_IsSpeculative");
                                dtOdin.Columns.Add("column25");
                                
                                //foreach (DataRow dr in dtOdin.Rows)
                                //{
                                //    dt.Columns.Add(dr[""].ToString());

                                //}

                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    if (ddlAction.SelectedValue == "NSE")
                                    {
                                        dr1 = dtOdin.NewRow();
                                        dr1["Trade_Account_Number"] = dr["Col15"].ToString();
                                        dr1["PESM_Identifier"] = dr["Col3"].ToString();
                                        if (dr["Col11"].ToString() == "1")
                                            dr1["WETT_TransactionCode"] = 1;
                                        else
                                            dr1["WETT_TransactionCode"] = 2;
                                        dr1["CET_TradeDate"] = dr["Col20"].ToString();
                                        dr1["CET_Rate"] = dr["Col13"].ToString();
                                        dr1["CET_Quantity"] = dr["Col12"].ToString();
                                        if (dr["Col11"].ToString() == "1")
                                            dr1["CET_BuySell"] = "B";
                                        else
                                            dr1["CET_BuySell"] = "S";
                                        dr1["XE_ExchangeCode"] = "NSE";
                                        dr1["CET_Brokerage"] = 0;
                                        dr1["CET_ServiceTax"] = 0;
                                        dr1["CET_EducationCess"] = 0;
                                        dr1["CET_STT"] = 0;
                                        dr1["CET_OtherCharges"] = 0;
                                        dr1["CET_RateInclBrokerage"] = 0;
                                        dr1["CET_TradeTotal"] = 0;
                                        dr1["CET_TradeNum"] = dr["Col22"].ToString();
                                        dr1["CET_OrderNum"] = dr["Col1"].ToString();
                                        dr1["CET_IsSpeculative"] = 0;
                                        dr1["column25"] = dr["Col25"].ToString(); 

                                        i++;
                                        dtOdin.Rows.Add(dr1);
                                    }
                                    else if (ddlAction.SelectedValue == "BSE")
                                    {
                                        dr1 = dtOdin.NewRow();
                                        dr1["Trade_Account_Number"] = dr["Col10"].ToString();
                                        dr1["PESM_Identifier"] = dr["Col1"].ToString();
                                        if (dr["Col11"].ToString() == "B")
                                            dr1["WETT_TransactionCode"] = 1;
                                        else
                                            dr1["WETT_TransactionCode"] = 2;
                                        dr1["CET_TradeDate"] = dr["Col9"].ToString();
                                        dr1["CET_Rate"] = dr["Col4"].ToString();
                                        dr1["CET_Quantity"] = dr["Col5"].ToString();
                                        if (dr["Col11"].ToString() == "B")
                                            dr1["CET_BuySell"] = "B";
                                        else
                                            dr1["CET_BuySell"] = "S";
                                        dr1["XE_ExchangeCode"] = "BSE";
                                        dr1["CET_Brokerage"] = 0;
                                        dr1["CET_ServiceTax"] = 0;
                                        dr1["CET_EducationCess"] = 0;
                                        dr1["CET_STT"] = 0;
                                        dr1["CET_OtherCharges"] = 0;
                                        dr1["CET_RateInclBrokerage"] = 0;
                                        dr1["CET_TradeTotal"] = 0;
                                        dr1["CET_TradeNum"] = dr["Col3"].ToString();
                                        dr1["CET_OrderNum"] = dr["Col13"].ToString();
                                        dr1["CET_IsSpeculative"] = 0;                                        
                                        i++;
                                        dtOdin.Rows.Add(dr1);

                                    }


                                }




                                dsXML.Tables.Remove("Table1");
                                dsXML.Tables.Add(dtOdin);




                            }
                        }
                        if (ddlAction.SelectedValue == "NSE")
                            filetypeid = 10;
                        else if (ddlAction.SelectedValue == "BSE")
                            filetypeid = 11;
                        else if (ddlAction.SelectedValue == "IIFL")
                            filetypeid = 19;
                    }
                    else
                    {
                        XmlCreated = false;
                        ValidationProgress = "Failure";
                        //lblErrorFileType.Visible = true;
                    }

                }
                #endregion

                #region Standard Profile
                //Standard Profile
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfile && ddlListCompany.SelectedValue == Contants.UploadExternalTypeStandard)
                {
                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\StdProf.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.StandardProfile);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.StandardProfile);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, null, Contants.UploadExternalTypeStandard, Contants.UploadFileTypeProfile);

                    }
                    else
                    {
                        XmlCreated = false;
                        ValidationProgress = "Failure";
                    }

                }
                #endregion

                #region WERP MF WERP Transaction
                //WERP MF WERP Transaction
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio && ddlListCompany.SelectedValue == Contants.UploadExternalTypeStandard)
                {
                    if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\WERPMFTans.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);

                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames(0);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames(0);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, Contants.ExtractTypeProfileFolio, Contants.UploadExternalTypeStandard, Contants.UploadFileTypeTransaction);
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                }
                #endregion

                #region Templeton Transaction
                else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeTemp)
                {
                    if (extension == "dbf")
                    {
                        string filename = "TN.dbf";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadDBFFile(filepath, filename, out strFileReadError);
                        if (strFileReadError == "")
                        {

                        }
                        else
                        {
                            filereadflag = false;
                            rejectUpload_Flag = true;
                            reject_reason = strFileReadError;
                        }
                    }
                    else if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\WERPMFTans.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);
                    }

                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                    if (filereadflag == true)
                    {
                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);

                            //for getting line number of error data in the file when validating
                            skiprowsval = Convert.ToInt16(txtNoOfRows.Text) + 1;
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.TempletonTransaction);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.TempletonTransaction);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", Contants.UploadExternalTypeTemp, Contants.UploadFileTypeTransaction);

                        //Reject upload if there are any data error validations
                        if (dsXML.Tables.Count > 0)
                            ValidateInputfile(Contants.UploadExternalTypeTemp, Contants.ExtractTypeMFTransaction, pathxml, skiprowsval);


                    }
                }
                #endregion

                #region Mf Sundaram Profile
                //Read File for Mf Deutsche Profile 
                else if ((ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile || ddlUploadType.SelectedValue == Contants.ExtractTypeFolio) && ddlListCompany.SelectedValue == "SU")
                {
                    if (extension == "dbf")
                    {
                        string filename = "DPD.dbf";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadDBFFile(filepath, filename, out strFileReadError);
                        if (strFileReadError == "")
                        {


                        }

                        else
                        {
                            filereadflag = false;
                            rejectUpload_Flag = true;
                            reject_reason = strFileReadError;
                        }
                    }
                    else if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\DPX.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);
                    }
                    else
                    {
                        //ValidationProgress = "Failure";
                    }
                    if (filereadflag == true)
                    {
                        if (rbSkipRowsYes.Checked)
                        {
                            ds = SkipRows(ds);
                        }

                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames(21);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames(21);

                        //Get XML after mapping, checking for columns
                        dsXML = getXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, "MF", "SU", Contants.UploadFileTypeProfile);
                    }
                }
                #endregion

                // Update Success Failures
                if (ValidationProgress != "Failure")
                {
                    //Fill details for Upload process log
                    if (rejectUpload_Flag == false)
                    {
                        processlogVo.AdviserId = adviserVo.advisorId;
                        if (ddlListBranch.SelectedValue.ToString() != "Select a Branch")
                            processlogVo.BranchId = int.Parse(ddlListBranch.SelectedValue.ToString());
                        processlogVo.CreatedBy = userVo.UserId;
                        processlogVo.StartTime = DateTime.Now;
                        processlogVo.FileName = FileUpload.FileName;
                        processlogVo.FileTypeId = filetypeid;
                        processlogVo.IsExternalConversionComplete = 1;
                        processlogVo.ModifiedBy = userVo.UserId;
                        if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTransaction && ddlListCompany.SelectedValue == Contants.UploadExternalTypeIIFL)
                        {
                            int count = ds.Tables[0].Rows.Count;
                            count = count - 2;
                            processlogVo.NoOfTotalRecords = count;
                        }
                        else
                            processlogVo.NoOfTotalRecords = ds.Tables[0].Rows.Count;
                        processlogVo.UserId = userVo.UserId;

                        if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfile)
                            processlogVo.ExtractTypeCode = "PO";

                        else if (ddlUploadType.SelectedValue == Contants.ExtractTypeFolio)
                            processlogVo.ExtractTypeCode = "FO";

                        else if (ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio)
                            processlogVo.ExtractTypeCode = "PAF";

                        else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction)
                            processlogVo.ExtractTypeCode = "MFT";

                        else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTradeAccount)
                            processlogVo.ExtractTypeCode = "TAO";

                        else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQDematAccount)
                            processlogVo.ExtractTypeCode = "DAO";

                        else if (ddlUploadType.SelectedValue == Contants.ExtractTypeEQTransaction)
                            processlogVo.ExtractTypeCode = "ET";



                        else if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFSystematic)
                            processlogVo.ExtractTypeCode = "SS";

                        else if (ddlUploadType.SelectedValue == "TRAIL")
                            processlogVo.ExtractTypeCode = "TRAIL";

                        processlogVo.ProcessId = uploadcommonBo.CreateUploadProcess(processlogVo);
                        dsXML.Tables[0].Columns.Add("ProcessId");
                        dsXML.Tables[0].Columns.Add("AdviserId");
                        dsXML.Tables[0].Columns.Add("CreatedBy");
                        dsXML.Tables[0].Columns.Add("CreatedOn");
                        dsXML.Tables[0].Columns.Add("ModifiedBy");
                        dsXML.Tables[0].Columns.Add("ModifiedOn");

                        foreach (DataRow dr in dsXML.Tables[0].Rows)
                        {
                            dr["ProcessId"] = processlogVo.ProcessId;
                            dr["AdviserId"] = adviserVo.advisorId;
                            dr["CreatedBy"] = userVo.UserId;
                            dr["CreatedOn"] = DateTime.Now;
                            dr["ModifiedBy"] = userVo.UserId;
                            dr["ModifiedOn"] = DateTime.Now;
                        }

                        dsXML.WriteXml(Server.MapPath("UploadFiles") + "\\" + processlogVo.ProcessId + ".xml", XmlWriteMode.WriteSchema);
                        XmlCreated = true;

                        Session[SessionContents.UploadProcessId] = processlogVo.ProcessId;
                        Session[SessionContents.UploadFileTypeId] = processlogVo.FileTypeId;
                        UploadProcessId = processlogVo.ProcessId;

                        //Show result division
                        divresult.Visible = true;
                    }
                }
            }
            catch (BaseApplicationException ex)
            {
                rejectUpload_Flag = true;
                reject_reason = reject_reason + ex.Message;
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerUpload.ascx:GetInputXML()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return XmlCreated;
        }

        //Skip rows in the dataset
        private DataSet SkipRows(DataSet ds)
        {
            int noRowsSkip = Convert.ToInt32(txtNoOfRows.Text);
            DataRow dr = ds.Tables[0].Rows[noRowsSkip - 1];
            int count = ds.Tables[0].Columns.Count;
            int i = 0;

            try
            {
                //Replace all column names with arbitrary column names
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    dc.ColumnName = "DataColumn" + i;
                    i++;
                }

                //Replace arbitrary names with columnanmes of the rows skipped
                i = 0;
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    if (!string.IsNullOrEmpty(dr[i].ToString().Trim()))
                    {
                        if (ddlUploadType.SelectedValue == Contants.ExtractTypeMFSystematic && ddlListCompany.SelectedValue == Contants.UploadExternalTypeCAMS)
                        {
                            if (dr[i].ToString().Trim() == "AUTO_TRXN_")
                            {
                                dc.ColumnName = dr[i].ToString() + i.ToString();
                            }
                            else
                            {
                                dc.ColumnName = dr[i].ToString();
                            }
                        }
                        else
                        {
                            dc.ColumnName = dr[i].ToString();
                        }
                    }
                    i++;
                }
                ds.Tables[0].Rows.Remove(dr);

                for (int j = 0; j < noRowsSkip - 1; j++)
                {
                    dr = ds.Tables[0].Rows[0];
                    ds.Tables[0].Rows.Remove(dr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //This method maps columns of the file with column names from the database for the file typle selected and
        //then returns a dataset after mapping and inserting data into for the XML to be created.
        private DataSet getXMLDs(DataSet dsFile, DataSet dsActual, DataSet dsColumnNames)
        {
            DataSet dsXML = new DataSet();
            DataTable dt = new DataTable();
            UploadCommonBo uploadcommonBo = new UploadCommonBo();

            try
            {


                int dsfileCount = dsFile.Tables[0].Rows.Count;


                //Add headers to the datatable which will be used for creating xml
                foreach (DataRow dr in dsActual.Tables[0].Rows)
                {
                    dt.Columns.Add(dr["XMLHeaderName"].ToString());
                }


                //get all column names for the DB to match the headers with the WERP headers
                DataSet WERPColumnnames = dsColumnNames;


                foreach (DataColumn dcFile in dsFile.Tables[0].Columns)
                {
                    foreach (DataRow drwerpclmns in WERPColumnnames.Tables[0].Rows)
                    {
                        if (drwerpclmns["ExternalHeaderName"].ToString().ToUpper().TrimEnd() == dcFile.ColumnName.ToString().ToUpper().TrimEnd())
                        {
                            dcFile.ColumnName = drwerpclmns["XMLHeaderName"].ToString();
                        }
                    }
                }


                //Check for mandatory 

                foreach (DataRow dr in dsActual.Tables[0].Rows)
                {
                    bool isExistingFlag = false;
                    foreach (DataColumn dcFile in dsFile.Tables[0].Columns)
                    {
                        if (dr["XMLHeaderName"].ToString().ToUpper().TrimEnd() == dcFile.ColumnName.ToString().ToUpper().TrimEnd())
                        {
                            isExistingFlag = true;
                            string WERPcolumnname = dr["XMLHeaderName"].ToString();

                            DataRow drXML;
                            for (int i = 0; i < dsfileCount; i++)
                            {

                                if (dt.Rows.Count < dsfileCount)
                                {
                                    drXML = dt.NewRow();
                                    drXML[WERPcolumnname] = dsFile.Tables[0].Rows[i][dcFile.ColumnName].ToString();
                                    dt.Rows.Add(drXML);
                                }
                                else
                                {
                                    dt.Rows[i][WERPcolumnname] = dsFile.Tables[0].Rows[i][dcFile.ColumnName];
                                }

                            }

                            break;
                        }

                    }
                    if (isExistingFlag == false)
                    {
                        if ((ddlUploadType.SelectedValue == Contants.ExtractTypeProfileFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeProfile) && dr["IsProfileMandatory"].ToString() == "1")
                        {
                            rejectUpload_Flag = true;
                            reject_reason = reject_reason + "The mandatory Column '" + dr["XMLHeaderName"].ToString() + "' does not exist; <br />";
                        }

                        if ((ddlUploadType.SelectedValue == Contants.ExtractTypeMFTransaction || ddlUploadType.SelectedValue == Contants.ExtractTypeEQTransaction) && dr["IsTransactionMandatory"].ToString() == "1")
                        {
                            rejectUpload_Flag = true;
                            reject_reason = reject_reason + "The mandatory Column '" + dr["XMLHeaderName"].ToString() + "' does not exist; <br />";
                        }

                        if ((ddlUploadType.SelectedValue == Contants.ExtractTypeFolio || ddlUploadType.SelectedValue == Contants.ExtractTypeEQTradeAccount) && dr["IsAccountMandatory"].ToString() == "1")
                        {
                            rejectUpload_Flag = true;
                            reject_reason = reject_reason + "The mandatory Column '" + dr["XMLHeaderName"].ToString() + "' does not exist; <br />";
                        }

                        if ((ddlUploadType.SelectedValue == Contants.ExtractTypeMFSystematic) && dr["IsMFSystematicMandatory"].ToString() == "1")
                        {
                            rejectUpload_Flag = true;
                            reject_reason = reject_reason + "The mandatory Column '" + dr["XMLHeaderName"].ToString() + "' does not exist; <br />";
                        }

                    }
                }

                //For inserting the values into table used for creating xml if all mandatory is there

                if (rejectUpload_Flag == false)
                    dsXML.Tables.Add(dt);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerUpload.ascx:getXMLDs()");

                object[] objects = new object[2];
                objects[0] = dsFile;
                objects[1] = dsActual;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsXML;
        }

        protected void btn_ViewRjects_Click(object sender, EventArgs e)
        {
            processlogVo = new UploadProcessLogVo();
            processlogVo = (UploadProcessLogVo)Session[SessionContents.ProcessLogVo];

            int processid = processlogVo.ProcessId;
            int filetype = processlogVo.FileTypeId;
            string extracttype = processlogVo.ExtractTypeCode;



            if ((filetype == (int)Contants.UploadTypes.CAMSProfile || filetype == (int)Contants.UploadTypes.KarvyProfile || filetype == (int)Contants.UploadTypes.TempletonProfile ||
                filetype == (int)Contants.UploadTypes.DeutscheProfile || filetype == (int)Contants.UploadTypes.StandardProfile || filetype == 21)
                && (extracttype == "PO" || extracttype == "PAF"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedWERPProfile','?processId=" + processid + "&filetypeid=" + filetype + "');", true);
            }

            else if ((filetype == (int)Contants.UploadTypes.CAMSProfile || filetype == (int)Contants.UploadTypes.KarvyProfile || filetype == (int)Contants.UploadTypes.TempletonProfile ||
               filetype == (int)Contants.UploadTypes.DeutscheProfile || filetype == (int)Contants.UploadTypes.StandardProfile)
               && (extracttype == "FO"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedMFFolio','?processId=" + processid + "&filetypeid=" + filetype + "');", true);
            }

            else if ((filetype == (int)Contants.UploadTypes.CAMSTransaction || filetype == (int)Contants.UploadTypes.KarvyTransaction || filetype == (int)Contants.UploadTypes.TempletonTransaction ||
               filetype == (int)Contants.UploadTypes.DeutscheTransaction || filetype == 25 || filetype == 6)
               && extracttype == "MFT")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedMFTransactionStaging','?processId=" + processid + "&filetypeid=" + filetype + "');", true);
            }


            else if (filetype == (int)Contants.UploadTypes.EquityStandardTradeAccount && extracttype == "TAO")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedTradeAccountStaging','?processId=" + processid + "&filetypeid=" + filetype + "');", true);
            }

            else if (filetype == (int)Contants.UploadTypes.EquityStandardTransaction && extracttype == "ET")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedEquityTransactionStaging','?processId=" + processid + "&filetypeid=" + filetype + "');", true);
            }
            else if (filetype == (int)Contants.UploadTypes.IIFLTransaction && extracttype == "ET")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedEquityTransactionStaging','processId=" + processid + "&filetypeid=" + filetype + "');", true);
            }
            else if (filetype == 10 || filetype == 11 || filetype == 19 && extracttype == "ET")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedEquityTransactionStaging','processId=" + processid + "&filetypeid=" + filetype + "');", true);
            }
            else if (filetype == 20 || filetype == 26 || filetype == 27)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedSystematicTransactionStaging','processId=" + processid + "');", true);
            }
            //else if(filetype==28)
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedSystematicTransactionStaging','processId=" + processid + "');", true);
            //}
            else if (filetype == 28 || filetype == 29 || filetype == 30 || filetype == 31 || filetype == 32)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TrailCommisionTransactionRejects','processId=" + processid + "');", true);
            }
        }

        protected void btnRollback_Click(object sender, EventArgs e)
        {
            bool blResult = false;
            bool blCustAssociateExists = false;
            bool blCustBankExists = false;
            bool blCustAssetExists = false;
            bool blCustEQTranNetPositionUpdated = false;
            bool blCustMFTranNetPositionUpdated = false;
            bool blCustEQTranExist = false;

            string InputInsertionComplete = "N";
            string StagingInsertionComplete = "N";
            string WerpInsertionComplete = "N";
            string ExternalInsertionComplete = "N";

            processlogVo = new UploadProcessLogVo();

            processlogVo = (UploadProcessLogVo)Session[SessionContents.ProcessLogVo];

            if (processlogVo.IsInsertionToInputComplete == 1)
                InputInsertionComplete = "Y";

            if (processlogVo.IsInsertionToFirstStagingComplete == 1)
                StagingInsertionComplete = "Y";

            if (processlogVo.IsInsertionToWERPComplete == 1)
                WerpInsertionComplete = "Y";

            if (processlogVo.IsInsertionToXtrnlComplete == 1)
                ExternalInsertionComplete = "Y";

            blResult = uploadsCommonBo.Rollback(processlogVo.ProcessId, processlogVo.FileTypeId, InputInsertionComplete, StagingInsertionComplete, WerpInsertionComplete, ExternalInsertionComplete, out blCustAssociateExists, out blCustBankExists, out blCustAssetExists, out blCustEQTranNetPositionUpdated, out blCustMFTranNetPositionUpdated, out blCustEQTranExist);

            if (blResult)
            {
                trMessage.Visible = true;
                lblMessage.Text = "Process Rolled Back Successfully!";
            }
            else
            {   // Display relevant failure messages

                if (!blCustAssociateExists)
                {
                    trError.Visible = true;
                    lblMessage.Text = "Cannot rollback as few customers have other customers associated!";
                }
                else if (!blCustBankExists)
                {
                    trError.Visible = true;
                    lblMessage.Text = "Cannot rollback as few customers have Banks associated!";
                }
                else if (!blCustAssetExists)
                {
                    trError.Visible = true;
                    lblMessage.Text = "Cannot rollback as few customers have asset details entered!";
                }

                if (!blCustEQTranNetPositionUpdated)
                {
                    trError.Visible = true;
                    lblMessage.Text = "Cannot rollback as few transactions have equity net positions updated!";
                }
                else if (!blCustMFTranNetPositionUpdated)
                {
                    trError.Visible = true;
                    lblMessage.Text = "Cannot rollback as few customers mutual fund accounts / MF net positions updated!";
                }
                else if (!blCustEQTranExist)
                {
                    trError.Visible = true;
                    lblMessage.Text = "Cannot rollback as Trade Accounts have Transactions!";
                }
            }
        }

        private void BindListBranch(int advisorId, string Id)
        {
            UploadCommonBo uploadCommonBo = new UploadCommonBo();
            DataSet ds = uploadCommonBo.GetAdviserBranchList(advisorId, Id);

            ddlListBranch.DataSource = ds.Tables[0];
            ddlListBranch.DataTextField = "AB_BranchName";
            ddlListBranch.DataValueField = "AB_BranchId";
            ddlListBranch.DataBind();
            ddlListBranch.Items.Insert(0, new ListItem("Select a Branch", "Select a Branch"));
        }

        protected void gvInputError_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            DataTable dtdtinputvalidationerror = new DataTable();
            dtdtinputvalidationerror = (DataTable)ViewState["dtinputvalidationerror"];
            gvInputError.PageIndex = e.NewPageIndex;
            gvInputError.DataSource = dtdtinputvalidationerror;
            gvInputError.DataBind();
            divInputErrorList.Visible = true;
            divresult.Visible = false;
        }

        private void ValidateInputfile(string Externaltype, string Extracttype, string pathxml, int skiprowsval)
        {
            dtInputRejects = uploadsvalidationBo.InputValidation(dsXML.Tables[0], Externaltype, Extracttype, pathxml, skiprowsval);
            if (dtInputRejects.Rows.Count != 0)
            {
                rejectUpload_Flag = true;
                DataTable dtdtinputvalidationerror = new DataTable();
                dtdtinputvalidationerror = dtInputRejects;

                gvInputError.DataSource = dtdtinputvalidationerror;
                gvInputError.DataBind();
                divInputErrorList.Visible = true;
                divresult.Visible = false;
                badData = true;
            }
            else
                badData = false;

        }

        private void ValidateInputfile1(string Externaltype, string Extracttype, string pathxml, int skiprowsval, string uploadtype)
        {
            dtInputRejects = uploadsvalidationBo.InputValidation1(dsXML.Tables[0], Externaltype, Extracttype, pathxml, skiprowsval, uploadtype);
            if (dtInputRejects.Rows.Count != 0)
            {
                rejectUpload_Flag = true;
                DataTable dtdtinputvalidationerror = new DataTable();
                dtdtinputvalidationerror = dtInputRejects;

                gvInputError.DataSource = dtdtinputvalidationerror;
                gvInputError.DataBind();
                divInputErrorList.Visible = true;
                divresult.Visible = false;
                badData = true;
            }
            else
                badData = false;

        }
        protected void imgBtnExport_Click(object sender, EventArgs e)
        {

            divInputErrorList.Visible = true;
            gvInputError.DataSource = dtInputRejects;
            gvInputError.DataBind();
            divresult.Visible = false;
            ViewState["dtinputvalidationerror"] = dtInputRejects;
        }

        protected void ddlAction_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string ddlCompanyType="ODIN";
            string ddlUploadType="EQT";
            message = Show_Message(ddlUploadType, ddlCompanyType);
            lblFileType.Visible = true;
            lblFileType.Text = "Please use the &nbsp;" + message + "&nbsp; File provided by the R&T to Upload.";
        }
    }
}

