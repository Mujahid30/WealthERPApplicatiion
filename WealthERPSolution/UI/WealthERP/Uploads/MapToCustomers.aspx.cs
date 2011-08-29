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


namespace WealthERP.Uploads
{
    public partial class MapToCustomers : System.Web.UI.Page
    {
        
        AdvisorVo advisorVo = new AdvisorVo();
        UploadProcessLogVo processlogVo;
        UploadCommonBo uploadsCommonBo;
        WerpUploadsBo werpUploadBo;

        int MFTransactionStagingId =0;
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
            lblRefine.Visible = false;
            lblMessage.Text = "";
            Stagingtableid = (ArrayList)Session["Stagingtableid"];
            DistinctProcessId = (ArrayList)Session["distincProcessIds"];
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            
            //folionumbers = (String[])Request.Params["id"];
            //MFTransactionStagingId = Convert.ToInt32(Request.Params["id"]);

            if (!IsPostBack)
            {
                rdbtnMapFolio.Checked = true;
                
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
                btnSearch.Enabled = false;
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

        #region Mapfolio code
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            if (advisorVo != null && advisorVo.advisorId > 0)
            {
                CustomerBo customerBo = new CustomerBo();
                DataSet dsCustomers = customerBo.SearchCustomers(advisorVo.advisorId, txtCustomerName.Text);
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
            int userId = 0;
            int customerId = 0;
            //int testVar = 0;
            customerId = Convert.ToInt32(e.CommandArgument);
            UserVo userVo = (UserVo)Session["userVo"];
            //testVar = (int)Session["varTest"];
            userId = userVo.UserId;
            bool insertioncomplete = true;
            CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
            CustomerAccountBo customerBo = new CustomerAccountBo();
            GridView gv = (GridView)sender;
            
            RejectedTransactionsBo rejectedTransactionsBo = new RejectedTransactionsBo();
              try
                {
                    //if (testVar == 1)
                    //{
                    //    insertioncomplete = MapRejectedFoliosToCustomer(customerId);
                    //    Session.Remove("varTest");
                    //}
                    //else
                        insertioncomplete = MapfoliotoCustomer(customerId);
                  
                }
                catch (Exception ex)
                {
                    insertioncomplete = false;
                }
           
            if (insertioncomplete)
            {
                gvCustomers.Visible = false;
                lblMessage.Visible = true;
                lblMessage.Text = "Customer is mapped";
                lblMessage.CssClass = "SuccessMsg";
                tblSearch.Visible = false;
                reprocess();
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "An error occurred while mapping.";
            }
        }

        private bool MapfoliotoCustomer(int customerId)
        {
            bool result = true;
            UserVo userVo = (UserVo)Session["userVo"];
            RejectedTransactionsBo rejectedTransactionsBo = new RejectedTransactionsBo();

            int userId = userVo.UserId;
            for (int i = 0; i < Stagingtableid.Count; i++)
            {
                try
                {
                    MFTransactionStagingId = Convert.ToInt32(Stagingtableid[i]);
                    result = rejectedTransactionsBo.MapFolioToCustomer(MFTransactionStagingId, customerId, userId);
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }
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
            int adviserId = (int)Session["adviserId"];
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
                        customerVo.RmId = rmVo.RMId;
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
                        customerVo.RmId = rmVo.RMId;
                        customerVo.Type = "NIND";
                        customerVo.CompanyName = txtCompanyName.Text.ToString();
                        customerVo.LastName = txtCompanyName.Text.ToString();
                        userVo.LastName = txtCompanyName.Text.ToString();
                    }
                    customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue);
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
                            reprocess();
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
            BindListBranch(rmVo.RMId, "rm");
            BindSubTypeDropDown();
        }


    }
}
