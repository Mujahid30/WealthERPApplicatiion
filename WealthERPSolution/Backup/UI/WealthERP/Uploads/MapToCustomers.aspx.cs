using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCustomerProfiling;
using System.Data;
using VoUser;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using VoCustomerProfiling;
using BoUploads;
using BoCommon;
using VoUploads;
using System.Collections;
using WealthERP.Base;
using System.Configuration;
using BoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoAdvisorProfiling;


namespace WealthERP.Uploads
{
    public partial class MapToCustomers : System.Web.UI.Page
    {

        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        UploadProcessLogVo processlogVo;
        UploadCommonBo uploadsCommonBo;
        WerpUploadsBo werpUploadBo;

        //int testVar = 0;
        int MFTransactionStagingId = 0;
        int MFFolioStagingId = 0;
        int MFSIPFolioStagingId = 0;
        int MFTrailFolioStagingid = 0;
        ArrayList Stagingtableid = new ArrayList();
        ArrayList DistinctProcessId = new ArrayList();
        string configPath;

        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        RMVo rmVo = new RMVo();
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();

        UserVo tempUserVo = new UserVo();
        DataTable dtCustomerSubType = new DataTable();
        string assetInterest;
        string path;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Theme"] == null || Session["Theme"].ToString() == string.Empty)
            {
                Session["Theme"] = "PCG";
            }

            Page.Theme = Session["Theme"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["userVo"];
            lblRefine.Visible = false;
            lblMessage.Text = "";
            if (advisorVo.IsISASubscribed == true)
            {

                rdbtnCreateNewCust.Visible = false;
            }

            string folioId = Convert.ToString(Request.Params["Folioid"]);
            if (folioId != null)
            {
                string[] testArrayFolio = folioId.Split('~');
            }
            string transactionId = Convert.ToString(Request.Params["id"]);
            if (transactionId != null)
            {
                string[] testArray = transactionId.Split('~');
            }
            string SIPFolioid = Convert.ToString(Request.Params["SIPFolioid"]);
            if (SIPFolioid != null)
            {
                string[] testArraySIPFolio = SIPFolioid.Split('~');
            }
            string TrailFolioid = Convert.ToString(Request.Params["TrailFolioid"]);
            if (TrailFolioid != null)
            {
                string[] testArrayTrailFolio = TrailFolioid.Split('~');
            }
            //MFTransactionStagingId = Convert.ToInt32(Request.Params["id"]);
            Stagingtableid = (ArrayList)Session["Stagingtableid"];
            DistinctProcessId = (ArrayList)Session["distincProcessIds"];
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            //Session["ProcessIdMaptoCustomers"] = 1;

            //testVar = (int)Session["varTest"];

            //folionumbers = (String[])Request.Params["id"];
            //MFTransactionStagingId = Convert.ToInt32(Request.Params["id"]);

            if (!IsPostBack)
            {
                rdbtnMapFolio.Checked = true;

            }


            if (userVo.UserType == "Advisor" || userVo.UserType.ToLower() == "ops")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";

                }
            else if (userVo.UserType.ToLower() == "superadmin")
                {

                    int advisorId = Convert.ToInt32(Session["adviserId_Upload"]);
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                }
           

           
            if (rdbtnMapFolio.Checked)
            {
                divMapToCustomer.Visible = true;
                divCreateNewCustomer.Visible = false;
            }
            else if (rdbtnCreateNewCust.Checked)
            {
                divCreateNewCustomer.Visible = true;
                divMapToCustomer.Visible = false;

                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                rmVo = (RMVo)Session["rmVo"];

                if (!IsPostBack)
                {
                    lblPanDuplicate.Visible = false;
                    rbtnIndividual.Checked = true;
                    trIndividualName.Visible = false;
                    trNonIndividualName.Visible = false;
                    BindListBranch(rmVo.RMId, "rm");
                    BindSubTypeDropDown();
                }

            }

            if (advisorVo == null || advisorVo.advisorId < 1)
            {
                lblMessage.Visible = true;
                lblMessage.CssClass = "Error";
                lblMessage.Text = "your session has expired.Please close this window and login again.";
                //btnSearch.Enabled = false;
                gvCustomers.Visible = false;
            }
        }

        protected void rdbtnMapFolio_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnMapFolio.Checked)
            {
                divMapToCustomer.Visible = true;
                divCreateNewCustomer.Visible = false;
            }
            else if (rdbtnCreateNewCust.Checked)
            {
                divCreateNewCustomer.Visible = true;
                divMapToCustomer.Visible = false;
            }
        }


        protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {
                ////trJointHoldersList.Visible = false;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
                ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
              
                //customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));               

                int adviserId = 0;
                if (advisorVo != null && advisorVo.advisorId > 0)
                {
                    if (userVo.UserType != "SuperAdmin")
                    {
                        adviserId = advisorVo.advisorId;
                    }
                    else
                    {
                        if (Session["adviserId_Upload"] != null)
                            adviserId = (int)Session["adviserId_Upload"];
                    }
                    CustomerBo customerBo = new CustomerBo();
                    DataSet dsCustomers = customerBo.SearchCustomers(adviserId, txtCustomerName.Text);
                    gvCustomers.DataSource = dsCustomers;
                    gvCustomers.DataBind();
                    gvCustomers.Visible = true;
                    if (dsCustomers.Tables[0].Rows.Count >= 100)
                        lblRefine.Visible = true;
                    else
                        lblRefine.Visible = false;
                }

            }
        }

        #region Mapfolio code
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int adviserId = 0;
            if (advisorVo != null && advisorVo.advisorId > 0)
            {
                if (userVo.UserType != "SuperAdmin")
                {
                    adviserId = advisorVo.advisorId;
                }
                else
                {
                    if (Session["adviserId_Upload"] != null)
                        adviserId = (int)Session["adviserId_Upload"];
                }
                CustomerBo customerBo = new CustomerBo();
                DataSet dsCustomers = customerBo.SearchCustomers(adviserId, txtCustomerName.Text);
                gvCustomers.DataSource = dsCustomers;
                gvCustomers.DataBind();
                gvCustomers.Visible = true;
                if (dsCustomers.Tables[0].Rows.Count >= 100)
                    lblRefine.Visible = true;
                else
                    lblRefine.Visible = false;
            }

        }

        protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int i;
            int userId = 0;
            int customerId = 0;
            string[] testArrayFolio = new string[0];
            string[] testArray = new string[0];
            string[] testArraySIPFolio = new string[0];
            string[] testArrayTrailFolio = new string[0];
            
            customerId = Convert.ToInt32(e.CommandArgument);
            UserVo userVo = (UserVo)Session["userVo"];
            userId = userVo.UserId;
            bool insertioncomplete = true;
            CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
            CustomerAccountBo customerBo = new CustomerAccountBo();
            GridView gv = (GridView)sender;
            //string transactionId = Convert.ToString(Request.Params["id"]);
            //string folioId = Convert.ToString(Request.Params["Folioid"]);
            //string[] testArrayFolio = folioId.Split('~');
            //string[] testArray = transactionId.Split('~');
            string folioId = Convert.ToString(Request.Params["Folioid"]);
            if (folioId != null)
            {
                testArrayFolio = folioId.Split('~');
            }
            string transactionId = Convert.ToString(Request.Params["id"]);
            if (transactionId != null)
            {
                testArray = transactionId.Split('~');
            }
             string SIPFolioid = Convert.ToString(Request.Params["SIPFolioid"]);
            if (SIPFolioid != null)
            {
                testArraySIPFolio = SIPFolioid.Split('~');
            }
            string TrailFolioid = Convert.ToString(Request.Params["TrailFolioid"]);
            if (TrailFolioid != null)
            {
                testArrayTrailFolio = TrailFolioid.Split('~');
            }

            RejectedTransactionsBo rejectedTransactionsBo = new RejectedTransactionsBo();
            try
            {
                if (transactionId != null)
                {
                    for (i = 0; i < testArray.Length; i++)
                    {
                        MFTransactionStagingId = int.Parse(testArray[i]);
                        insertioncomplete = rejectedTransactionsBo.MapFolioToCustomer(MFTransactionStagingId, customerId, userId);
                    }
                }
                 if (SIPFolioid != null)
                {
                    for (i = 0; i < testArraySIPFolio.Length; i++)
                    {
                        MFSIPFolioStagingId= int.Parse(testArraySIPFolio[i]);
                        insertioncomplete = rejectedTransactionsBo.MapRejectedSIPFoliosToCustomer(MFSIPFolioStagingId, customerId, userId);
                    }
                }
                if (folioId != null)
                {
                    for (i = 0; i < testArrayFolio.Length; i++)
                    {
                        MFFolioStagingId = int.Parse(testArrayFolio[i]);
                        insertioncomplete = rejectedTransactionsBo.MapRejectedFoliosToCustomer(MFFolioStagingId, customerId, userId);
                    }
                }
                if (TrailFolioid != null)
                {
                    for (i = 0; i < testArrayTrailFolio.Length; i++)
                    {
                        MFTrailFolioStagingid = int.Parse(testArrayTrailFolio[i]);
                        insertioncomplete = rejectedTransactionsBo.MapRejectedTrailFoliosToCustomer(MFTrailFolioStagingid, customerId, userId);
                    }
                }
            }
            catch (Exception ex)
            {
                insertioncomplete = false;
            }

            if (insertioncomplete)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mykey", "ClosePopUp();", true);
                divMapToCustomer.Visible = false;
                lblMessage.Visible = true;
                lblMessage.Text = "Customer is mapped";
                lblMessage.CssClass = "SuccessMsg";
                tblSearch.Visible = false;
                //reprocess();
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "An error occurred while mapping.";
            }
        }

        private bool MapfoliotoCustomer(int customerId)
        {
            int i;
            string[] testArrayFolio = new string[0];
            string[] testArray = new string[0];
            string[] testArraySIPFolio = new string[0];
            string[] testArrayTrailFolio = new string[0];
            bool result = true;
            UserVo userVo = (UserVo)Session["userVo"];
            RejectedTransactionsBo rejectedTransactionsBo = new RejectedTransactionsBo();
            string folioId = Convert.ToString(Request.Params["Folioid"]);
            if (folioId != null)
                testArrayFolio = folioId.Split('~');
            string transactionId = Convert.ToString(Request.Params["id"]);
            if (transactionId != null)
                testArray = transactionId.Split('~');


            string SIPFolioid = Convert.ToString(Request.Params["SIPFolioid"]);
            if (SIPFolioid != null)
            {
                testArraySIPFolio = SIPFolioid.Split('~');
            }
            string TrailFolioid = Convert.ToString(Request.Params["TrailFolioid"]);
            if (TrailFolioid != null)
            {
                testArrayTrailFolio = TrailFolioid.Split('~');
            }

            int userId = userVo.UserId;

            if (transactionId != null)
            {
                for (i = 0; i < testArray.Length; i++)
                {
                    MFTransactionStagingId = int.Parse(testArray[i]);
                    result = rejectedTransactionsBo.MapFolioToCustomer(MFTransactionStagingId, customerId, userId);
                }
            }
            if (folioId != null)
            {
                for (i = 0; i < testArrayFolio.Length; i++)
                {
                    MFFolioStagingId = int.Parse(testArrayFolio[i]);
                    result = rejectedTransactionsBo.MapRejectedFoliosToCustomer(MFFolioStagingId, customerId, userId);
                }
            }
            if (TrailFolioid != null)
            {
                for (i = 0; i < testArrayTrailFolio.Length; i++)
                {
                    MFTrailFolioStagingid = int.Parse(testArrayTrailFolio[i]);
                    result = rejectedTransactionsBo.MapRejectedTrailFoliosToCustomer(MFTrailFolioStagingid, customerId, userId);
                }
            }
            if (SIPFolioid != null)
            {
                for (i = 0; i < testArray.Length; i++)
                {
                    MFSIPFolioStagingId = int.Parse(testArraySIPFolio[i]);
                    result = rejectedTransactionsBo.MapRejectedSIPFoliosToCustomer(MFSIPFolioStagingId, customerId, userId);
                }
            }

            //MFTransactionStagingId = Convert.ToInt32(Request.Params["id"]);
            //result = rejectedTransactionsBo.MapFolioToCustomer(MFTransactionStagingId, customerId, userId);
            //for (int i = 0; i < Stagingtableid.Count; i++)
            //{
            //    try
            //    {
            //        MFTransactionStagingId = Convert.ToInt32(Stagingtableid[i]);
            //        result = rejectedTransactionsBo.MapFolioToCustomer(MFTransactionStagingId, customerId, userId);
            //    }
            //    catch (Exception ex)
            //    {
            //        result = false;
            //    }
            //}
            return result;
        }

        //private bool MapRejectedFoliosToCustomer(int customerId)
        //{
        //    bool result = true;
        //    int mfFolioStagingId = 0;
        //    UserVo userVo = (UserVo)Session["userVo"];
        //    RejectedTransactionsBo rejectedTransactionsBo = new RejectedTransactionsBo();

        //    int userId = userVo.UserId;
        //    for (int i = 0; i < Stagingtableid.Count; i++)
        //    {
        //        try
        //        {
        //            mfFolioStagingId = Convert.ToInt32(Stagingtableid[i]);
        //            result = rejectedTransactionsBo.MapRejectedFoliosToCustomer(mfFolioStagingId, customerId, userId);
        //        }
        //        catch (Exception ex)
        //        {
        //            result = false;
        //        }
        //    }
        //    return result;
        //}

        #endregion

        #region Reporcess Code
        protected void reprocess()
        {
            bool blResult = false;
            uploadsCommonBo = new UploadCommonBo();

            int countTransactionsInserted = 0;
            int countRejectedRecords = 0;
            int filetypeId = 0;

            // BindGrid

            for (int i = 0; i < DistinctProcessId.Count; i++)
            {
                int ProcessId = Convert.ToInt32(DistinctProcessId[i].ToString());

                UploadProcessLogVo processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);
                filetypeId = processlogVo.FileTypeId;
                if (processlogVo.FileTypeId == 1 || processlogVo.FileTypeId == 3 || processlogVo.FileTypeId == 15 || processlogVo.FileTypeId == 17)
                {

                    blResult = MFWERPTransactionWERPInsertion(ProcessId, out countTransactionsInserted, out countRejectedRecords, processlogVo.FileTypeId);
                    // }

                    if (blResult)
                    {
                        // Success Message
                        lblMessage.Text = "Mapping and Reprocess Done Successfully!";
                    }
                    else
                    {
                        // Failure Message

                        lblMessage.Text = "Reprocess Failure!";
                    }


                }
                else
                {
                    //Karvy Reprocess

                }
            }

        }

        private bool MFWERPTransactionWERPInsertion(int ProcessId, out int countTransactionsInserted, out int countRejectedRecords, int fileTypeId)
        {
            bool blResult = false;
            processlogVo = new UploadProcessLogVo();
            uploadsCommonBo = new UploadCommonBo();
            werpUploadBo = new WerpUploadsBo();

            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];

            countTransactionsInserted = 0;
            countRejectedRecords = 0;

            processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);

            //CAMS and KARVY Reprocess 
            string packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
            bool CommonTransChecks = false;
            if (fileTypeId == 1)
            {

                bool camsDatatranslationCheckResult = uploadsCommonBo.UploadsCAMSDataTranslationForReprocess(ProcessId);
                if (camsDatatranslationCheckResult)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(advisorVo.advisorId, ProcessId, packagePath, configPath, "CA", "CAMS");
                }
            }
            else if (fileTypeId == 3)
            {

                bool karvyDataTranslationCheck = uploadsCommonBo.UploadsKarvyDataTranslationForReprocess(ProcessId);
                if (karvyDataTranslationCheck)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(advisorVo.advisorId, ProcessId, packagePath, configPath, "KA", "Karvy");
                }
            }
            else if (fileTypeId == 15)
            {
                bool TempletonDataTranslationCheck = uploadsCommonBo.UploadsTempletonDataTranslationForReprocess(ProcessId);
                if (TempletonDataTranslationCheck)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(advisorVo.advisorId, ProcessId, packagePath, configPath, "TN", "Templeton");
                }
            }
            else if (fileTypeId == 17)
            {
                bool DeutscheDataTranslationCheck = uploadsCommonBo.UploadsDeutscheDataTranslationForReprocess(ProcessId);
                if (DeutscheDataTranslationCheck)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(advisorVo.advisorId, ProcessId, packagePath, configPath, "DT", "Deutsche");
                }
            }


            if (CommonTransChecks)
            {

                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                bool insertTransWERP = uploadsCommonBo.InsertTransToWERP(ProcessId, packagePath, configPath);
                if (insertTransWERP)
                {
                    processlogVo.IsInsertionToWERPComplete = 1;
                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(ProcessId, "WPMF");

                    processlogVo.EndTime = DateTime.Now;

                    if (fileTypeId == 1)
                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, Contants.UploadExternalTypeCAMS);
                    else if (fileTypeId == 3)
                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, Contants.UploadExternalTypeKarvy);
                    else if (fileTypeId == 15)
                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, Contants.UploadExternalTypeTemp);
                    else if (fileTypeId == 17)
                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, Contants.UploadExternalTypeDeutsche);
                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                }
            }
            return blResult;
        }


        #endregion


        #region CreateNewCustomerCode
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

                FunctionInfo.Add("Method", "CustomerType.ascx:chkAvailability()");


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

                FunctionInfo.Add("Method", "CustomerType.ascx:ChkMailId()");


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
            int adviserId = 0;
            if(userVo.UserType == "SuperAdmin")
            {
                adviserId = (int)Session["adviserId_Upload"];
            }
            else if (userVo.UserType != "")
            {
                adviserId = (int)Session["adviserId"];
             }
            try
            {
                if (customerBo.PANNumberDuplicateCheck(adviserId, txtPanNumber.Text.ToString(), customerVo.CustomerId))
                {
                    result = false;
                    lblPanDuplicate.Visible = true;
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
                FunctionInfo.Add("Method", "CustomerType.ascx:Validation()");
                object[] objects = new object[1];
                objects[0] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            BindSubTypeDropDown();
        }

        private void BindSubTypeDropDown()
        {
            try
            {
                dtCustomerSubType = XMLBo.GetCustomerSubType(path, "IND");
                ddlCustomerSubType.DataSource = dtCustomerSubType;
                ddlCustomerSubType.DataTextField = "CustomerTypeName";
                ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
                ddlCustomerSubType.DataBind();
                ddlCustomerSubType.Items.Insert(0, new ListItem("Select a Sub-Type", "Select a Sub-Type"));

                trIndividualName.Visible = true;
                trNonIndividualName.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerType.ascx:rbtnIndividual_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void rbtnNonIndividual_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dtCustomerSubType = XMLBo.GetCustomerSubType(path, "NIND");
                ddlCustomerSubType.DataSource = dtCustomerSubType;
                ddlCustomerSubType.DataTextField = "CustomerTypeName";
                ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
                ddlCustomerSubType.DataBind();
                ddlCustomerSubType.Items.Insert(0, new ListItem("Select a Sub-Type", "Select a Sub-Type"));
                trIndividualName.Visible = false;
                trNonIndividualName.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerType.ascx:rbtnNonIndividual_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<int> customerIds = null;
            try
            {
                Nullable<DateTime> dt = new DateTime();
                customerIds = new List<int>();
                lblPanDuplicate.Visible = false;
                if (Validation())
                {
                    userVo = new UserVo();
                    if (rbtnIndividual.Checked)
                    {
                        rmVo = (RMVo)Session["rmVo"];
                        tempUserVo = (UserVo)Session["userVo"];
                        //customerVo.RmId = rmVo.RMId;
                        customerVo.Type = "IND";
                        customerVo.FirstName = txtFirstNameCreation.Text.ToString();
                        customerVo.MiddleName = txtMiddleName.Text.ToString();
                        customerVo.LastName = txtLastNameCreation.Text.ToString();

                        userVo.FirstName = txtFirstNameCreation.Text.ToString();
                        userVo.MiddleName = txtMiddleName.Text.ToString();
                        userVo.LastName = txtLastNameCreation.Text.ToString();
                    }
                    else if (rbtnNonIndividual.Checked)
                    {
                        rmVo = (RMVo)Session["rmVo"];
                        tempUserVo = (UserVo)Session["userVo"];
                        //customerVo.RmId = rmVo.RMId;
                        customerVo.Type = "NIND";
                        customerVo.CompanyName = txtCompanyName.Text.ToString();
                        customerVo.LastName = txtCompanyName.Text.ToString();
                        userVo.LastName = txtCompanyName.Text.ToString();
                    }
                    customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue);
                    customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue);
                    customerVo.SubType = ddlCustomerSubType.SelectedItem.Value;
                    customerVo.Email = txtEmail.Text.ToString();
                    customerVo.PANNum = txtPanNumber.Text.ToString();
                    customerVo.Dob = DateTime.MinValue;
                    customerVo.RBIApprovalDate = DateTime.MinValue;
                    customerVo.CommencementDate = DateTime.MinValue;
                    customerVo.RegistrationDate = DateTime.MinValue;
                    customerVo.Adr1State = null;
                    customerVo.Adr2State = null;
                    customerVo.ProfilingDate = DateTime.Today;
                    customerVo.UserId = userVo.UserId;
                    customerVo.ViaSMS = 1;
                    userVo.Email = txtEmail.Text.ToString();
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PortfolioName = "MyPortfolio";
                    customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, tempUserVo.UserId);
                    Session["Customer"] = "Customer";
                    if (customerIds != null)
                    {
                        CustomerFamilyVo familyVo = new CustomerFamilyVo();
                        CustomerFamilyBo familyBo = new CustomerFamilyBo();
                        familyVo.AssociateCustomerId = customerIds[1];
                        familyVo.CustomerId = customerIds[1];
                        familyVo.Relationship = "SELF";
                        familyBo.CreateCustomerFamily(familyVo, customerIds[1], userVo.UserId);

                        //Map folios to the new customer created
                        bool Mapfolio = MapfoliotoCustomer(customerIds[1]);

                        if (Mapfolio)
                        {
                            divCreateNewCustomer.Visible = false;
                            lblMessage.Visible = true;
                            lblMessage.Text = "Customer is mapped";
                            lblMessage.CssClass = "SuccessMsg";
                            tblSearch.Visible = false;
                            //reprocess();
                        }
                        else
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "An error occurred while mapping.";
                        }

                    }

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
                FunctionInfo.Add("Method", "CustomerType.ascx:btnSubmit_Click()");
                object[] objects = new object[5];
                objects[0] = customerIds;
                objects[1] = customerVo;
                objects[2] = rmVo;
                objects[3] = userVo;
                objects[4] = customerPortfolioVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindListBranch(int rmId, string userType)
        {
            UploadCommonBo uploadCommonBo = new UploadCommonBo();
            DataSet ds = uploadCommonBo.GetAdviserBranchList(rmId, userType);

            ddlAdviserBranchList.DataSource = ds.Tables[0];
            ddlAdviserBranchList.DataTextField = "AB_BranchName";
            ddlAdviserBranchList.DataValueField = "AB_BranchId";
            ddlAdviserBranchList.DataBind();
            ddlAdviserBranchList.Items.Insert(0, new ListItem("Select a Branch", "Select a Branch"));
        }

        #endregion

        protected void rdbtnCreateNewCust_CheckedChanged(object sender, EventArgs e)
        {
            lblPanDuplicate.Visible = false;
            rbtnIndividual.Checked = true;
            trIndividualName.Visible = false;
            trNonIndividualName.Visible = false;
            if (userVo.UserType != "SuperAdmin")
            {
                BindListBranch(advisorVo.advisorId, "adviser");
            }
            else
            {
                int adviserId=0;
                if(Session["adviserId_Upload"] != null)
                adviserId = (int)Session["adviserId_Upload"];
                BindListBranch(adviserId, "adviser");
            }
            
            BindSubTypeDropDown();
            //if (userVo.UserType == "SuperAdmin")
            //{
            //    advisorVo.advisorId = 1000;
            //}

        }

        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {

            try
            {

                DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlAdviseRMList.DataSource = ds.Tables[0];
                    ddlAdviseRMList.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlAdviseRMList.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                    ddlAdviseRMList.DataBind();
                    ddlAdviseRMList.Items.Remove("No RM Available");
                    ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                    CompareValidator2.ValueToCompare = "Select";
                    CompareValidator2.ErrorMessage = "Please select a RM";
                }
                else
                {
                    if (!IsPostBack)
                    {
                        ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                        CompareValidator2.ValueToCompare = "Select";
                        CompareValidator2.ErrorMessage = "Please select a RM";

                    }
                    else
                    {
                        if (rbtnNonIndividual.Checked == true)
                        {
                            if ((IsPostBack) && (ddlAdviserBranchList.SelectedIndex == 0))
                            {
                                ddlAdviseRMList.Items.Clear();
                                ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                                CompareValidator2.ValueToCompare = "Select";
                                CompareValidator2.ErrorMessage = "Please select a RM";
                            }
                            else
                            {
                                ddlAdviseRMList.Items.Clear();
                                ddlAdviseRMList.Items.Remove("Select");
                                ddlAdviseRMList.Items.Insert(0, new ListItem("No RM Available", "No RM Available"));
                                CompareValidator2.ValueToCompare = "No RM Available";
                                CompareValidator2.ErrorMessage = "Cannot Add Customer Without a RM";


                            }
                        }
                        else
                        {
                            if ((IsPostBack) && (ddlAdviserBranchList.SelectedIndex == 0))
                            {
                                ddlAdviseRMList.Items.Clear();
                                ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                                CompareValidator2.ValueToCompare = "Select";
                                CompareValidator2.ErrorMessage = "Please select a RM";
                            }
                            else
                            {
                                ddlAdviseRMList.Items.Clear();
                                ddlAdviseRMList.Items.Remove("Select");
                                ddlAdviseRMList.Items.Insert(0, new ListItem("No RM Available", "No RM Available"));
                                CompareValidator2.ValueToCompare = "No RM Available";
                                CompareValidator2.ErrorMessage = "Cannot Add Customer Without a RM";
                            }
                        }
                    }
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

                FunctionInfo.Add("Method", "MapToCustomers.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlAdviserBranchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAdviserBranchList.SelectedIndex == 0)
            {
                //BindRMforBranchDropdown(0, bmID);
                BindRMforBranchDropdown(0, 0);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlAdviserBranchList.SelectedValue.ToString()), 0);

            }
        }

        protected void chkUseDummyPan_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbtnNo.Checked == true)
            //{
            //    txtPanNumber.Enabled = true;
            //    txtPanNumber.Text = string.Empty;
            //}
            //else if (rbtnYes.Checked == true)
            //{
            if (chkUseDummyPan.Checked == true)
            {
                int adviserId = advisorVo.advisorId;
                int createdBy = advisorVo.advisorId;
                int modifiedBy = advisorVo.advisorId;
                int dummyPan = 0;
                //DateTime modifiedOn = DateTime.Now;
                //DateTime createdOn = DateTime.Now;

                RejectedTransactionsBo rejectedTransactionsBo = new RejectedTransactionsBo();
                dummyPan = rejectedTransactionsBo.GetNewDummyPan(adviserId, createdBy, modifiedBy, out dummyPan);

                txtPanNumber.Text = "WERPAN" + dummyPan.ToString();
                txtPanNumber.Enabled = false;
            }
            else
            {
                txtPanNumber.Enabled = true;
                txtPanNumber.Text = string.Empty;
            }
        }
    }
}
