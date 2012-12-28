﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoUser;
using VoUploads;
using BoUploads;
using WealthERP.Base;
using System.Configuration;
using BoCommon;
using Telerik.Web.UI;
using BoSuperAdmin;

namespace WealthERP.Uploads
{
    public partial class ViewUploadProcessLog : System.Web.UI.UserControl
    {
        AdvisorVo adviserVo = new AdvisorVo();
        RMVo rmVo;
        UserVo userVo;
        UploadProcessLogVo processlogVo;

        CamsUploadsBo camsUploadsBo;
        KarvyUploadsBo karvyUploadsBo;
        UploadCommonBo uploadsCommonBo;
        WerpUploadsBo werpUploadBo;
        WerpMFUploadsBo werpMFUploadsBo;
        WerpEQUploadsBo werpEQUploadsBo;
        TempletonUploadsBo templetonUploadsBo;
        StandardFolioUploadBo standardFolioUploadBo;
        StandardProfileUploadBo standardProfileUploadBo;
        DeutscheUploadsBo deutscheUploadsBo;
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();

        DataSet getProcessLogDs;
        DataTable dtgvProcessLog = new DataTable();

        int adviserId;
        int processID;
        int filetypeId;

        string XMLConversionStatus = string.Empty;
        string InputInsertionStatus = string.Empty;
        string FirstStagingStatus = string.Empty;
        string SecondStagingStatus = string.Empty;
        string WERPInsertionStatus = string.Empty;
        string ExternalInsertionStatus = string.Empty;
        string packagePath = string.Empty;
        string xmlPath;
        string xmlFileName = string.Empty;
        string configPath;
        int rmId;



        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();

            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session[SessionContents.UserVo];


            if (adviserVo.advisorId == 1000)
            {
                if (ddlAdviser.SelectedValue != "Select" && ddlAdviser.SelectedValue != "")
                {
                    adviserId = Convert.ToInt32(ddlAdviser.SelectedValue.ToString());
                    if (hfRmId.Value != "")
                    {
                        rmId = Convert.ToInt32(hfRmId.Value);
                    }
                    trGridView.Visible = true;
                    //Panel2.ScrollBars = ScrollBars.Horizontal; 
                }
                else
                {
                    trGridView.Visible = false;
                    Panel2.ScrollBars = ScrollBars.None; 
                   // Panel2.Visible = false;
                    adviserId = 1000;
                }
            }
            else
            {
                trGridView.Visible = true;
                Panel2.ScrollBars = ScrollBars.Horizontal; 
                //Panel2.Visible = true;
                trAdviserSelection.Visible = false;
                adviserId = adviserVo.advisorId;
                rmId =rmVo.RMId;
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
                tblReprocess.Visible = false;
                if (adviserId != 1000)
                {
                    BindProcessHistoryGrid();
                }
                else
                {
                    BindAdviserDropDownList();    
                }


            }

        }
        protected void BindAdviserDropDownList()
        {
            DataTable dtAdviserList = new DataTable();
            dtAdviserList = superAdminOpsBo.BindAdviserForUpload();

            if (dtAdviserList.Rows.Count > 0)
            {
                ddlAdviser.DataSource = dtAdviserList;
                ddlAdviser.DataTextField = "A_OrgName";
                ddlAdviser.DataValueField = "A_AdviserId";
                ddlAdviser.DataBind();
            }
            ddlAdviser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }


        private void BindProcessHistoryGrid()
        {            
            DataTable dtForInsertedOrNotInserted = new DataTable();
            UploadCommonBo uploadProcessLogBo = new UploadCommonBo();
            GridColumn gcStatus;

            try
            {
                getProcessLogDs = uploadProcessLogBo.GetUploadProcessLogAdmin(adviserId, hdnSort.Value);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ViewUploadProcessLog.ascx:BindProcessHistoryGrid()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            if (getProcessLogDs.Tables[0].Rows.Count > 0)
            {
                trTransactionMessage.Visible = false;

                getProcessLogDs.Tables[0].Columns.Add("Status");


                foreach (DataRow dr in getProcessLogDs.Tables[0].Rows)
                {
                    if (dr["ADUL_IsXMLConvesionComplete"].ToString() == "Y" &&
                        dr["ADUL_IsInsertionToInputComplete"].ToString() == "Y" &&
                        dr["ADUL_IsInsertionToFirstStagingComplete"].ToString() == "Y" &&
                        dr["ADUL_IsInsertionToSecondStagingComplete"].ToString() == "Y" &&
                        dr["ADUL_IsInsertionToWerpComplete"].ToString() == "Y" &&
                        dr["ADUL_IsInsertionToXtrnlComplete"].ToString() == "Y"
                        )
                    {
                        //gvProcessLog.Rows[i].Cells[1].Text = "Inserted";//gobinda

                        dr["Status"] = "Inserted";


                    }
                    else
                    {
                        dr["Status"] = "Not Inserted";
                    }

                }
               


                if (Cache["ProcessLogDetails" + adviserId.ToString()] == null)
                {
                    Cache.Insert("ProcessLogDetails" + adviserId.ToString(), getProcessLogDs);
                }
                else
                {
                    Cache.Remove("ProcessLogDetails" + adviserId.ToString());
                    Cache.Insert("ProcessLogDetails" + adviserId.ToString(), getProcessLogDs);
                }
                Panel2.ScrollBars = ScrollBars.Horizontal;
                trGridView.Visible = true;
                gvProcessLog.DataSource = getProcessLogDs;
                gvProcessLog.DataBind();

            }
            else
            {
                Panel2.ScrollBars = ScrollBars.None;
                trGridView.Visible = false;
                trTransactionMessage.Visible = true;
                //gvProcessLog.DataSource = null;
                //gvProcessLog.DataBind();

                //if (Cache["ProcessLogDetails" + adviserId.ToString()] == null)
                //{
                //    Cache.Insert("ProcessLogDetails" + adviserId.ToString(), getProcessLogDs);
                //}
                //else
                //{
                //    Cache.Remove("ProcessLogDetails" + adviserId.ToString());
                //    Cache.Insert("ProcessLogDetails" + adviserId.ToString(), getProcessLogDs);
                //}
                //gvProcessLog.DataSource = getProcessLogDs.Tables[0];
                //gvProcessLog.DataBind();
            }


            // this.GetPageCount();
        }

        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        {
            try
            {

                RadComboBox ddlAction = (RadComboBox)sender;
                uploadsCommonBo = new UploadCommonBo();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "Page_ClientValidate();Loading(true);", true); 
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;
                processID = int.Parse(gvProcessLog.MasterTableView.DataKeyValues[selectedRow - 1]["ADUL_ProcessId"].ToString());
                filetypeId = Int32.Parse(gvProcessLog.MasterTableView.DataKeyValues[selectedRow - 1]["WUXFT_XMLFileTypeId"].ToString());
                string extracttype = gvProcessLog.MasterTableView.DataKeyValues[selectedRow - 1]["XUET_ExtractTypeCode"].ToString();
                //XMLConversionStatus = gvProcessLog.Rows[selectedRow].Cells[18].Text.Trim();
                //InputInsertionStatus = gvProcessLog.Rows[selectedRow].Cells[19].Text.Trim();
                //FirstStagingStatus = gvProcessLog.Rows[selectedRow].Cells[20].Text.Trim();
                //SecondStagingStatus = gvProcessLog.Rows[selectedRow].Cells[21].Text.Trim();
                //WERPInsertionStatus = gvProcessLog.Rows[selectedRow].Cells[22].Text.Trim();
                UploadProcessLogVo processlogVo1 = new UploadProcessLogVo();
                processlogVo1 = uploadsCommonBo.GetProcessLogInfo(processID);
                if (processlogVo1.IsInsertionToInputComplete == 1)
                    InputInsertionStatus = "Y";
                else
                    InputInsertionStatus = "N";
                if (processlogVo1.IsInsertionToFirstStagingComplete == 1)
                    FirstStagingStatus = "Y";
                else
                    FirstStagingStatus = "N";
                if (processlogVo1.IsInsertionToSecondStagingComplete == 1)
                    SecondStagingStatus = "Y";
                else
                    SecondStagingStatus = "N";
                if (processlogVo1.IsInsertionToWERPComplete == 1)
                    WERPInsertionStatus = "Y";
                else
                    WERPInsertionStatus = "N";

                if (ddlAction.SelectedItem.Value.ToString() == Contants.Reprocess)
                {
                    //if (XMLConversionStatus == "N")
                    //{
                    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerUpload','login');", true);
                    //}
                    //else
                    //{
                    Reprocess(processID, filetypeId, InputInsertionStatus, FirstStagingStatus, SecondStagingStatus, WERPInsertionStatus);
                    //}
                }
                else if (ddlAction.SelectedItem.Value.ToString() == Contants.RollBack)
                {
                    //if (XMLConversionStatus == "N")
                    //{
                    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerUpload','login');", true);
                    //}
                    //else
                    //{
                    RollBack(processID, filetypeId, InputInsertionStatus, FirstStagingStatus, SecondStagingStatus, WERPInsertionStatus);
                    //}
                }
                else if (ddlAction.SelectedItem.Value.ToString() == Contants.ManageRejects)
                {
                    if ((filetypeId == 28) || (filetypeId == 29) || (filetypeId == 30))
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TrailCommisionTransactionRejects','processId=" + processID + "');", true);
                    }

                    else if ((filetypeId == (int)Contants.UploadTypes.CAMSProfile || filetypeId == (int)Contants.UploadTypes.KarvyProfile || filetypeId == (int)Contants.UploadTypes.TempletonProfile ||
                        filetypeId == (int)Contants.UploadTypes.DeutscheProfile || filetypeId == (int)Contants.UploadTypes.StandardProfile || filetypeId == 21)
                        && (extracttype == "PO" || extracttype == "PAF"))
                    {
                       // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedWERPProfile','?processId=" + processID + "&filetypeid=" + filetypeId + "');", true);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedMFFolio','?processId=" + processID + "&filetypeid=" + filetypeId + "');", true);

                        
                        
                    
                    }

                    else if ((filetypeId == (int)Contants.UploadTypes.CAMSProfile || filetypeId == (int)Contants.UploadTypes.KarvyProfile || filetypeId == (int)Contants.UploadTypes.TempletonProfile ||
                       filetypeId == (int)Contants.UploadTypes.DeutscheProfile || filetypeId == (int)Contants.UploadTypes.StandardProfile)
                       && (extracttype == "FO"))
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedMFFolio','processId=" + processID + "&filetypeid=" + filetypeId + "');", true);
                    }

                    else if ((filetypeId == (int)Contants.UploadTypes.CAMSTransaction || filetypeId == (int)Contants.UploadTypes.KarvyTransaction || filetypeId == (int)Contants.UploadTypes.TempletonTransaction ||
                       filetypeId == (int)Contants.UploadTypes.DeutscheTransaction || filetypeId == 25 || filetypeId == 6)
                       && extracttype == "MFT")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedMFTransactionStaging','processId=" + processID + "&filetypeid=" + filetypeId + "');", true);
                    }


                    else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTradeAccount && extracttype == "TAO")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedTradeAccountStaging','processId=" + processID + "&filetypeid=" + filetypeId + "');", true);
                    }

                    else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTransaction && extracttype == "ET")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedEquityTransactionStaging','processId=" + processID + "&filetypeid=" + filetypeId + "');", true);
                    }
                    else if (filetypeId == (int)Contants.UploadTypes.IIFLTransaction && extracttype == "ET")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedEquityTransactionStaging','processId=" + processID + "&filetypeid=" + filetypeId + "');", true);
                    }
                    else if (filetypeId == (int)Contants.UploadTypes.ODINTransaction && extracttype == "ET" || filetypeId == 10 || filetypeId == 11)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedEquityTransactionStaging','processId=" + processID + "&filetypeid=" + filetypeId + "');", true);
                    }

                    else if (filetypeId == 20 || filetypeId == 26 || filetypeId == 27)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedSystematicTransactionStaging','processId=" + processID + "');", true);
                    }                 

                }
                else
                {
                    // Display Invalid Selection
                }

            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ViewUploadProcessLog.ascx:ddlAction_OnSelectedIndexChange()");

                object[] objects = new object[1];
                objects[0] = processID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void Reprocess(int processID, int filetypeId, string InputInsertionStatus, string FirstStagingStatus, string SecondStagingStatus, string WERPInsertionStatus)
        {

            camsUploadsBo = new CamsUploadsBo();
            karvyUploadsBo = new KarvyUploadsBo();
            uploadsCommonBo = new UploadCommonBo();
            processlogVo = new UploadProcessLogVo();
            werpEQUploadsBo = new WerpEQUploadsBo();
            templetonUploadsBo = new TempletonUploadsBo();
            standardFolioUploadBo = new StandardFolioUploadBo();
            standardProfileUploadBo = new StandardProfileUploadBo();
            deutscheUploadsBo = new DeutscheUploadsBo();

            int NoOfCustomersUploaded = 0;
            int NoOfFoliosUploaded = 0;
            int NoOfTransactionsUploaded = 0;
            int NoOfRejectedRecords = 0;
            int countCustCreated = 0;
            int countFolioCreated = 0;

            bool werpEQTradeInputResult = false;
            bool werpEQFirstStagingResult = false;
            bool werpEQFirstStagingCheckResult = false;
            bool werpEQSecondStagingResult = false;
            bool WERPEQSecondStagingCheckResult = false;
            bool WERPEQTradeWerpResult = false;
            bool stdFolioCommonDeleteResult = false;
            bool stdProCommonDeleteResult = false;

            xmlPath = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            processlogVo = uploadsCommonBo.GetProcessLogInfo(processID);
            string extracttype = processlogVo.ExtractTypeCode;

            bool blResult = false;

            tblReprocess.Visible = true;

            if (InputInsertionStatus == "Y")
            {
                if (FirstStagingStatus == "Y")
                {
                    if (SecondStagingStatus == "Y")
                    {
                        // WERP Insertion Logic

                        #region CAMS Profile
                        if (filetypeId == (int)Contants.UploadTypes.CAMSProfile)
                        {
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                bool blCamsAMCUpdated = uploadsCommonBo.UpdateAMCForFolioReprocess(processID, "CAMS");
                            }
                            // MF CAMS Profile Upload
                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);  //StandardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                if (stdProCommonChecksResult)
                                {
                                    // Insert Customer Details into WERP Tables
                                    bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                    if (camsProCreateCustomerResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "CA");
                                        processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                        processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "CA");
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (blResult)
                                            stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                //Folio Chks in Std Folio Staging 
                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                if (camsFolioStagingChkResult)
                                {
                                    //Folio Staging to WERP Tables
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                    bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                    if (camsFolioWerpInsertionResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "CA");
                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "CA");
                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (blResult)
                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                    }
                                }
                            }
                        }
                        #endregion CAMS Profile

                        #region Sundaram Profile
                        if (filetypeId == 21)
                        {
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                bool blCamsAMCUpdated = uploadsCommonBo.UpdateAMCForFolioReprocess(processID, "Sundaram");
                            }
                            // MF Sundaram Profile Upload
                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);  //StandardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                if (stdProCommonChecksResult)
                                {
                                    // Insert Customer Details into WERP Tables
                                    bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                    if (camsProCreateCustomerResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "SU");
                                        processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                        processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "SU");
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (blResult)
                                            stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                //Folio Chks in Std Folio Staging 
                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                if (camsFolioStagingChkResult)
                                {
                                    //Folio Staging to WERP Tables
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                    bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                    if (camsFolioWerpInsertionResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "SU");
                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "SU");
                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (blResult)
                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                    }
                                }
                            }
                        }
                        #endregion Sundaram Profile

                        #region Standard Equity Trade Account WERP Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTradeAccount)
                        {
                            // Standard Equity Trade Account WERP Insertion
                            packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQTradeStaging.dtsx");
                            WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTradeAccount(processID, packagePath, configPath, adviserId);

                            if (WERPEQSecondStagingCheckResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeStagingToWerp.dtsx");
                                WERPEQTradeWerpResult = werpEQUploadsBo.WERPEQInsertTradeAccountDetails(processID, packagePath, configPath);
                                if (WERPEQTradeWerpResult)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPEQ");
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetAccountsUploadRejectCount(processID, "WPEQ");
                                    processlogVo.EndTime = DateTime.Now;
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }
                        }
                        #endregion Standard Equity Trade Account WERP Insertion

                        #region CAMS MF TRansaction WERP Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.CAMSTransaction)
                        {

                            bool camsDatatranslationCheckResult = uploadsCommonBo.UploadsCAMSDataTranslationForReprocess(processID);
                            if (camsDatatranslationCheckResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "CA", "CAMS");
                                if (CommonTransChecks)
                                {

                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                    bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                    if (camsTranWerpResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeCAMS);
                                        processlogVo.EndTime = DateTime.Now;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }
                                }
                            }
                        }
                        #endregion CAMS MF TRansaction WERP Insertion

                        #region Sundaram MF TRansaction WERP Insertion
                        else if (filetypeId == 25)
                        {

                            bool camsDatatranslationCheckResult = uploadsCommonBo.UploadsCAMSDataTranslationForReprocess(processID);
                            if (camsDatatranslationCheckResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "SU", "Sundaram");
                                if (CommonTransChecks)
                                {

                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                    bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                    if (camsTranWerpResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "SU");
                                        processlogVo.EndTime = DateTime.Now;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }
                                }
                            }
                        }
                        #endregion Sundaram MF TRansaction WERP Insertion
                        #region WERP Equity Transation Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTransaction)
                        {
                            // WERP Equity Transation Insertion
                            werpEQUploadsBo = new WerpEQUploadsBo();
                            packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
                            WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(processID, packagePath, configPath, adviserId);

                            if (WERPEQSecondStagingCheckResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                                bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(processID, packagePath, configPath); // EQ Trans XML File Type Id = 8);
                                if (WERPEQTranWerpResult)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPEQ");
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "WPEQ");
                                    processlogVo.EndTime = DateTime.Now;
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }
                        }
                        else if (filetypeId == (int)Contants.UploadTypes.IIFLTransaction)
                        {
                            // WERP Equity Transation Insertion
                            werpEQUploadsBo = new WerpEQUploadsBo();
                            packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
                            WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(processID, packagePath, configPath, adviserId);

                            if (WERPEQSecondStagingCheckResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                                bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(processID, packagePath, configPath); // EQ Trans XML File Type Id = 19);
                                if (WERPEQTranWerpResult)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPEQ");
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "WPEQ");
                                    processlogVo.EndTime = DateTime.Now;
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }
                        }
                        #endregion WERP Equity Transation Insertion

                        #region KARVY MF Transaction WERP Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.KarvyTransaction)
                        {


                            bool karvyDataTranslationCheck = uploadsCommonBo.UploadsKarvyDataTranslationForReprocess(processID);
                            if (karvyDataTranslationCheck)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "KA", "Karvy");
                                if (CommonTransChecks)
                                {

                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                    bool karvyTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                    if (karvyTranWerpResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeKarvy);
                                        processlogVo.EndTime = DateTime.Now;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }
                                }
                            }
                        }
                        #endregion KARVY MF Transaction WERP Insertion

                        #region KARVY Profile + Folio WERP Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.KarvyProfile)
                        {
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                bool blAMCUpdated = uploadsCommonBo.UpdateAMCForFolioReprocess(processID, "Karvy");
                            }
                            //KARVY Profile + Folio WERP Insertion
                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                //Checks in Profile Staging
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                bool karvyProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                if (karvyProCommonChecksResult)
                                {
                                    // Insert Customer Details into WERP Tables
                                    bool karvyProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                    if (karvyProCreateCustomerResult)
                                    {
                                        //Create new Bank Accounts
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                        bool karvyProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                        if (karvyProCreateBankAccountResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "KA");
                                            processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }

                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                //Folio Chks in Std Folio Staging                            
                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                bool karvyFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                if (karvyFolioStagingChkResult)
                                {
                                    //Inserting Folio into WERP Tables
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                    blResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                    if (blResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "KA");
                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (blResult)
                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                    }
                                }
                            }

                        }
                        #endregion KARVY Profile + Folio WERP Insertion

                        #region Standard Profile WERP INsertion
                        else if (filetypeId == (int)Contants.UploadTypes.StandardProfile)
                        {
                            //Standard Profile WERP INsertion
                            StandardProfileUploadBo StandardProfileUploadBo = new StandardProfileUploadBo();

                            bool stdProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out  countCustCreated);
                            if (stdProCreateCustomerResult)
                            {
                                //Create new Bank Accounts
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                bool stdProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                if (stdProCreateBankAccountResult)
                                {

                                    processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (blResult)
                                    {
                                        stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(processID);
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "WP");
                                    }
                                }
                            }
                        }
                        #endregion Standard Profile WERP INsertion

                        #region Templeton Transaction WERP Insertion

                        else if (filetypeId == (int)Contants.UploadTypes.TempletonTransaction)
                        {
                            // Update AMC - Sujith's Query
                            bool blTempAMCUpdated = uploadsCommonBo.UpdateAMCForTransactionReprocess(processID, "Templeton");

                            if (blTempAMCUpdated)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "TN", "Templeton");
                                if (CommonTransChecks)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                    bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                    if (camsTranWerpResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeTemp);
                                        processlogVo.EndTime = DateTime.Now;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }
                                }
                            }
                        }

                        #endregion

                        #region Templeton Profile + Folio WERP Insertion

                        else if (filetypeId == (int)Contants.UploadTypes.TempletonProfile)
                        {
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                bool blTempletonAMCUpdated = uploadsCommonBo.UpdateAMCForFolioReprocess(processID, "Templeton");
                            }

                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                //common profile checks
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                bool templetonProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                if (templetonProCommonChecksResult)
                                {
                                    // Insert Customer Details into WERP Tables
                                    bool templetonProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                    if (templetonProCreateCustomerResult)
                                    {
                                        //Create new Bank Accounts
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                        bool templetonProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                        if (templetonProCreateBankAccountResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "TN");
                                            processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                //Folio Chks in Std Folio Staging 
                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                bool templetonFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                if (templetonFolioStagingChkResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                    bool templetonFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                    if (templetonFolioWerpInsertionResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "TN");
                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (blResult)
                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                    }
                                }
                            }
                            else if (filetypeId == (int)Contants.UploadTypes.ODINTransaction)
                            {
                                // WERP Equity Transation Insertion
                                werpEQUploadsBo = new WerpEQUploadsBo();
                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
                                WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(processID, packagePath, configPath, adviserId);

                                if (WERPEQSecondStagingCheckResult)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                                    bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(processID, packagePath, configPath); // EQ Trans XML File Type Id = 19);
                                    if (WERPEQTranWerpResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPEQ");
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "WPEQ");
                                        processlogVo.EndTime = DateTime.Now;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }
                                }
                            }
                        }

                        #endregion

                        #region Deutsche Transaction WERP Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.DeutscheTransaction)
                        {
                            // Update AMC - Sujith's Query
                            bool blDeutAMCUpdated = uploadsCommonBo.UpdateAMCForTransactionReprocess(processID, "Deutsche");

                            if (blDeutAMCUpdated)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "DT", "Deutsche");

                                if (CommonTransChecks)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                    bool deutscheTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                    if (deutscheTranWerpResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeDeutsche);
                                        processlogVo.EndTime = DateTime.Now;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }
                                }
                            }


                        }
                        #endregion

                        #region Deutsche Profile + Folio WERP Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.DeutscheProfile)
                        {
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                bool blDeutcheAMCUpdated = uploadsCommonBo.UpdateAMCForFolioReprocess(processID, "Deutsche");
                            }

                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                //common profile checks
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                bool deutscheProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                if (deutscheProCommonChecksResult)
                                {
                                    // Insert Customer Details into WERP Tables
                                    bool deutscheProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                    if (deutscheProCreateCustomerResult)
                                    {
                                        //Create new Bank Accounts
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                        bool deutscheProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                        if (deutscheProCreateBankAccountResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "DT");
                                            processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                //Folio Chks in Std Folio Staging 
                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                bool deutscheFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                if (deutscheFolioStagingChkResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                    bool deutscheFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                    if (deutscheFolioWerpInsertionResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "DT");
                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (blResult)
                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                    }
                                }
                            }
                        }
                        #endregion

                        //****gobinda's MF STANDARD TRNX AND FOLIO****\\

                        #region Standard Transaction + Folio Insertion
                        else if (filetypeId == 6)
                        {

                            bool CommonStdTransChecks;
                            standardProfileUploadBo = new StandardProfileUploadBo();
                            string stdPackagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsStandardMFTrxnStagingChk.dtsx");
                            CommonStdTransChecks = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, stdPackagePath, configPath);

                            if (CommonStdTransChecks)
                            {
                                processlogVo.IsInsertionToWERPComplete = 1;
                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WP");
                                processlogVo.EndTime = DateTime.Now;

                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeStandard);

                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            }
                        }

                        #endregion

                        //-----------gobinda's SYSTEMATIC STANDARD-------------\\

                        #region STANDARD SYSTEMATIC
                        else if (filetypeId == 26)
                        {
                            bool standardSIPCommonStagingChk = false;
                            bool standardSIPCommonStagingToWERP = false;
                            bool updateProcessLog = false;
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingCheck.dtsx");
                            standardSIPCommonStagingChk = camsUploadsBo.CamsSIPCommonStagingChk(processID, packagePath, configPath, "WPT");
                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "WPT");
                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (standardSIPCommonStagingChk)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingToWERP.dtsx");
                                standardSIPCommonStagingToWERP = camsUploadsBo.CamsSIPCommonStagingToWERP(processID, packagePath, configPath);

                                if (standardSIPCommonStagingToWERP)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(processID, "WPT");
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }
                        }

                        #endregion



                        #region CAMS SYSTEMATIC
                        //CAMS SYSTEMATIC 
                        if (filetypeId == 20)
                        {
                            bool camsSIPCommonStagingChk = false;
                            bool camsSIPCommonStagingToWERP = false;
                            bool updateProcessLog = false;
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingCheck.dtsx");
                            camsSIPCommonStagingChk = camsUploadsBo.CamsSIPCommonStagingChk(processID, packagePath, configPath, "CA");
                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "CA");
                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (camsSIPCommonStagingChk)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingToWERP.dtsx");
                                camsSIPCommonStagingToWERP = camsUploadsBo.CamsSIPCommonStagingToWERP(processID, packagePath, configPath);

                                if (camsSIPCommonStagingToWERP)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(processID, "CA");


                                    // processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "CA");
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                }
                            }
                        }
                        #endregion

                        //-----------gobinda's KARVY STANDARD-------------\\
                        #region KARVY SYSTEMATIC
                        else if (filetypeId == 27)
                        {
                            bool karvySIPCommonStagingChk = false;
                            bool karvySIPCommonStagingToWERP = false;
                            bool updateProcessLog = false;
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingCheck.dtsx");
                            karvySIPCommonStagingChk = camsUploadsBo.KarvySIPCommonStagingChk(processID, packagePath, configPath, "KA");
                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "KA");
                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (karvySIPCommonStagingChk)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\SipKarvyUploads\\SipKarvyUploads\\SipKarvyUploads\\UploadStandardTransactionCommonStagingToWERP.dtsx");
                                karvySIPCommonStagingToWERP = camsUploadsBo.CamsSIPCommonStagingToWERP(processID, packagePath, configPath);

                                if (karvySIPCommonStagingToWERP)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(processID, "KA");
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }

                        }

                        #endregion

                        //-----------gobinda's Trail Commission Templeton-------------\\
                        #region Trail Commission
                        else if (filetypeId == 28)
                        {
                            AdvisorVo advisorVo = new AdvisorVo();
                            uploadsCommonBo = new UploadCommonBo();
                            //CamsUploadsBo camsUploadsBo = new CamsUploadsBo();
                            bool TempletonTrailCommonStagingChk = false;
                            bool TempletonTrailCommonStagingToSetUp = false;
                            bool updateProcessLog = false;
                            string packagePath;


                            processlogVo = uploadsCommonBo.GetProcessLogInfo(processID);

                            packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\callOnReprocess.dtsx");

                            TempletonTrailCommonStagingChk = uploadsCommonBo.TrailCommissionCommonStagingCheck(adviserId, processID, packagePath, configPath);
                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "TN");

                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (TempletonTrailCommonStagingChk)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                                TempletonTrailCommonStagingToSetUp = camsUploadsBo.TempletonTrailCommissionCommonStagingChk(processID, packagePath, configPath, "TN");

                                if (TempletonTrailCommonStagingToSetUp)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(processID, "TN");
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }

                        }

                        #endregion

                        //-----------gobinda's Trail Commission CAMS-------------\\
                        #region Trail Commission
                        else if (filetypeId == 29)
                        {
                            AdvisorVo advisorVo = new AdvisorVo();
                            uploadsCommonBo = new UploadCommonBo();
                            //CamsUploadsBo camsUploadsBo = new CamsUploadsBo();
                            bool TempletonTrailCommonStagingChk = false;
                            bool TempletonTrailCommonStagingToSetUp = false;
                            bool updateProcessLog = false;
                            string packagePath;


                            processlogVo = uploadsCommonBo.GetProcessLogInfo(processID);

                            packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\callOnReprocess.dtsx");

                            TempletonTrailCommonStagingChk = uploadsCommonBo.TrailCommissionCommonStagingCheck(adviserId, processID, packagePath, configPath);
                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "TN");

                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (TempletonTrailCommonStagingChk)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                                TempletonTrailCommonStagingToSetUp = camsUploadsBo.TempletonTrailCommissionCommonStagingChk(processID, packagePath, configPath, "CA");

                                if (TempletonTrailCommonStagingToSetUp)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(processID, "CA");
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }

                        }

                        #endregion

                    }


                    else
                    {
                        #region CAMS Profile
                        // Call WERP Insertion Logic
                        if (filetypeId == (int)Contants.UploadTypes.CAMSProfile)
                        {
                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                bool camsProCommonStagingResult = camsUploadsBo.CAMSInsertToCommonStaging(processID, packagePath, configPath);
                                if (camsProCommonStagingResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog3)
                                    {

                                        //common profile checks

                                        // Final step
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                        bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                        if (stdProCommonChecksResult)
                                        {
                                            // Insert Customer Details into WERP Tables
                                            bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                            if (camsProCreateCustomerResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "CA");
                                                processlogVo.NoOfCustomerInserted = countCustCreated;
                                                processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (blResult)
                                                    stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                            }
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadFolioDataFromCAMSStagingToCommonStaging.dtsx");
                                bool camsFolioCommonStagingResult = camsUploadsBo.CAMSInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                if (camsFolioCommonStagingResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                    bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                    if (camsFolioStagingChkResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                        bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                        if (camsFolioWerpInsertionResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "CA");
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "CA");
                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                        }
                                    }
                                }
                            }


                        }
                        #endregion

                        //-----------gobinda's KARVY STANDARD-------------\\
                        #region KARVY SYSTEMATIC
                        else if (filetypeId == 27)
                        {
                            bool karvySIPCommonStagingChk = false;
                            bool karvySIPCommonStagingToWERP = false;
                            bool updateProcessLog = false;
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingCheck.dtsx");
                            karvySIPCommonStagingChk = camsUploadsBo.KarvySIPCommonStagingChk(processID, packagePath, configPath, "KA");
                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "KA");
                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (karvySIPCommonStagingChk)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\SipKarvyUploads\\SipKarvyUploads\\SipKarvyUploads\\UploadStandardTransactionCommonStagingToWERP.dtsx");
                                karvySIPCommonStagingToWERP = camsUploadsBo.CamsSIPCommonStagingToWERP(processID, packagePath, configPath);

                                if (karvySIPCommonStagingToWERP)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(processID, "KA");
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }

                        }

                        #endregion


                        #region Sundaram Profile
                        // Call WERP Insertion Logic
                        if (filetypeId == 21)
                        {
                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                bool camsProCommonStagingResult = camsUploadsBo.SundaramInsertToCommonStaging(processID, packagePath, configPath);
                                if (camsProCommonStagingResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog3)
                                    {

                                        //common profile checks

                                        // Final step
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                        bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                        if (stdProCommonChecksResult)
                                        {
                                            // Insert Customer Details into WERP Tables
                                            bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                            if (camsProCreateCustomerResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "SU");
                                                processlogVo.NoOfCustomerInserted = countCustCreated;
                                                processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (blResult)
                                                    stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                            }
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadFolioDataFromSundaramStagingToCommonStaging.dtsx");
                                bool camsFolioCommonStagingResult = camsUploadsBo.SundaramInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                if (camsFolioCommonStagingResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                    bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                    if (camsFolioStagingChkResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                        bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                        if (camsFolioWerpInsertionResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "SU");
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "SU");
                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                        }
                                    }
                                }
                            }


                        }
                        #endregion



                        #region Standard Equity Trade Account Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTradeAccount)
                        {
                            // WERP Equity Insert To 2nd Staging Trade Account
                            packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQStdTradeStagingToEQTradeStaging.dtsx");
                            werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTradeAccount(processID, packagePath, configPath, 8); // EQ Trans File Type Id = 8

                            if (werpEQSecondStagingResult)
                            {
                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                if (updateProcessLog3)
                                {

                                    // WERP Insertion
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQTradeStaging.dtsx");
                                    WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTradeAccount(processID, packagePath, configPath, adviserId);

                                    if (WERPEQSecondStagingCheckResult)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeStagingToWerp.dtsx");
                                        WERPEQTradeWerpResult = werpEQUploadsBo.WERPEQInsertTradeAccountDetails(processID, packagePath, configPath);
                                        if (WERPEQTradeWerpResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPEQ");
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetAccountsUploadRejectCount(processID, "WPEQ");
                                            processlogVo.EndTime = DateTime.Now;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region CAMS MF TRansaction Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.CAMSTransaction)
                        {

                            packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadChecksCAMSTransactionStaging.dtsx");
                            bool camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                            if (camsTranStagingCheckResult)
                            {
                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                    bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "CA", "CAMS");

                                    if (CommonTransChecks)
                                    {

                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                        bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                        if (camsTranWerpResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeCAMS);
                                            processlogVo.EndTime = DateTime.Now;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Sundaram MF TRansaction Common Staging Insertion
                        else if (filetypeId == 25)
                        {

                            packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadChkSundaramTransactionStaging.dtsx");
                            bool camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                            if (camsTranStagingCheckResult)
                            {
                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                    bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "SU", "Sundaram");

                                    if (CommonTransChecks)
                                    {

                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                        bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                        if (camsTranWerpResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "Su");
                                            processlogVo.EndTime = DateTime.Now;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region WERP Equity Transation Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTransaction)
                        {
                            werpEQUploadsBo = new WerpEQUploadsBo();
                            packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQStdTranStaging.dtsx");
                            werpEQFirstStagingCheckResult = werpEQUploadsBo.WerpEQProcessDataInFirstStagingTrans(processID, packagePath, configPath, adviserId);

                            if (werpEQFirstStagingCheckResult)
                            {
                                // WERP Equity Insert To 2nd Staging Transaction
                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQStdTranStagingToEQTranStaging.dtsx");
                                werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTransaction(processID, packagePath, configPath, 8); // EQ Trans File Type Id = 8

                                if (werpEQSecondStagingResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                    if (updateProcessLog3)
                                    {

                                        // Fourth step
                                        // WERP Insertion
                                        packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
                                        WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(processID, packagePath, configPath, adviserId);

                                        if (WERPEQSecondStagingCheckResult)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                                            bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(processID, packagePath, configPath); // EQ Trans XML File Type Id = 8
                                            if (WERPEQTranWerpResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPEQ");
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "WPEQ");
                                                processlogVo.EndTime = DateTime.Now;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region KARVY MF Transaction Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.KarvyTransaction)
                        {
                            KarvyUploadsBo karvyUploadBo = new KarvyUploadsBo();
                            packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadChecksKarvyTransactionStaging.dtsx");
                            bool karvyTranStagingCheckResult = karvyUploadBo.KarvyProcessDataInStagingTrans(processID, packagePath, configPath);
                            if (karvyTranStagingCheckResult)
                            {
                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                    bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "KA", "Karvy");
                                    if (CommonTransChecks)
                                    {

                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                        bool karvyTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                        if (karvyTranWerpResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeKarvy);
                                            processlogVo.EndTime = DateTime.Now;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region KARVY Profile + Folio Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.KarvyProfile)
                        {   // MF Karvy Profile Upload

                            // Data Translation checks in Karvy Staging
                            packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadDataTranslationKarvyProfileStaging.dtsx");
                            bool karvyProStagingCheckResult = karvyUploadsBo.KARVYStagingDataTranslationCheck(processID, packagePath, configPath);
                            if (karvyProStagingCheckResult)
                            {
                                if (extracttype == "PO" || extracttype == "PAF")
                                {
                                    //Inserting Data from Karvy Staging to Profile Staging
                                    packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToProfileStaging.dtsx");
                                    bool karvyStagingToProfileStagingResult = karvyUploadsBo.KARVYStagingInsertToProfileStaging(processID, packagePath, configPath);
                                    if (karvyStagingToProfileStagingResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog3)
                                        {
                                            //Making Chks in Profile Staging
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                            bool karvyProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                            if (karvyProCommonChecksResult)
                                            {
                                                // Insert Customer Details into WERP Tables
                                                bool karvyProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                if (karvyProCreateCustomerResult)
                                                {
                                                    //Create new Bank Accounts
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                    bool karvyProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                    if (karvyProCreateBankAccountResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "KA");
                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                        processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (extracttype == "FO" || extracttype == "PAF")
                                {
                                    if (karvyProStagingCheckResult)
                                    {
                                        // Inserting Karvy Staging Data to Folio Staging
                                        packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToFolioStaging.dtsx");
                                        bool karvyStagingToFolioStagingResult = karvyUploadsBo.KARVYStagingInsertToFolioStaging(processID, packagePath, configPath);
                                        if (karvyStagingToFolioStagingResult)
                                        {
                                            //Folio Chks in Std Folio Staging 
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                            bool karvyFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                            if (karvyFolioStagingChkResult)
                                            {
                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                blResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                if (blResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "KA");
                                                    processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                                    processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    if (blResult)
                                                        stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        #endregion

                        #region Standard Profile Common Staging INsertion
                        else if (filetypeId == (int)Contants.UploadTypes.StandardProfile)
                        {
                            StandardProfileUploadBo StandardProfileUploadBo = new StandardProfileUploadBo();
                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileFirstStagingToCommonStaging.dtsx");
                            bool stdProCommonStagingResult = StandardProfileUploadBo.StdInsertToCommonStaging(processID, packagePath, configPath);
                            if (stdProCommonStagingResult)
                            {
                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog3)
                                {
                                    //common profile checks
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                    bool stdProCommonChecksResult = StandardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                    if (stdProCommonChecksResult)
                                    {
                                        // Insert Customer Details into WERP Tables
                                        bool stdProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out  countCustCreated);
                                        if (stdProCreateCustomerResult)
                                        {
                                            //Create new Bank Accounts
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                            bool stdProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                            if (stdProCreateBankAccountResult)
                                            {

                                                processlogVo.NoOfCustomerInserted = countCustCreated;
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (blResult)
                                                {
                                                    stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "WP");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Templeton Transaction Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.TempletonTransaction)
                        {
                            packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadChecksOnTempletonStaging.dtsx");
                            bool templeTranStagingCheckResult = templetonUploadsBo.TempletonProcessDataInStagingTrans(processID, packagePath, configPath);
                            if (templeTranStagingCheckResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTempletonTransStagingToTransStaging.dtsx");
                                bool templeTranSecondStagingResult = templetonUploadsBo.TempletonInsertFromTempStagingTransToCommonStaging(processID, packagePath, configPath);
                                if (templeTranSecondStagingResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                        bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "TN", "Templeton");

                                        if (CommonTransChecks)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                            bool templeTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                            if (templeTranWerpResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeTemp);
                                                processlogVo.EndTime = DateTime.Now;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                //updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Templeton Profile + Folio Common Staging Insertion

                        else if (filetypeId == (int)Contants.UploadTypes.TempletonProfile)
                        {
                            // Insertion to common staging
                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadProfileDataFromTempStagingToCommonStaging.dtsx");
                                bool templetonProCommonStagingResult = templetonUploadsBo.TempInsertToCommonStaging(processID, packagePath, configPath);
                                if (templetonProCommonStagingResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog3)
                                    {
                                        //common profile checks
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                        bool templetonProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                        if (templetonProCommonChecksResult)
                                        {
                                            // Insert Customer Details into WERP Tables
                                            bool templetonProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                            if (templetonProCreateCustomerResult)
                                            {
                                                //Create new Bank Accounts
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                bool templetonProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                if (templetonProCreateBankAccountResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "TN");
                                                    processlogVo.NoOfCustomerInserted = countCustCreated;
                                                    processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    if (blResult)
                                                        stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadFolioDataFromTempStagingToCommonStaging.dtsx");
                                bool templetonFolioCommonStagingResult = templetonUploadsBo.TempInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                if (templetonFolioCommonStagingResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                    bool templetonFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                    if (templetonFolioStagingChkResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                        bool templetonFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                        if (templetonFolioWerpInsertionResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "TN");
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                        }

                        #endregion

                        #region Deutsche Transaction Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.DeutscheTransaction)
                        {
                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionFirstStagingtoSecondStaging.dtsx");
                            bool deutscheTranStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                            if (deutscheTranStagingCheckResult)
                            {
                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                if (updateProcessLog)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                    bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "DT", "Deutsche");

                                    if (CommonTransChecks)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                        bool deutscheTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                        if (deutscheTranWerpResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeDeutsche);
                                            processlogVo.EndTime = DateTime.Now;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Deutsche Profile + Folio Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.DeutscheProfile)
                        {
                            // Deutsche Insert To Staging Profile

                            // Doing a check on data translation
                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheDataTranslationChecksFirstStaging.dtsx");
                            bool deutscheProStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                            if (deutscheProStagingCheckResult)
                            {
                                if (extracttype == "PO" || extracttype == "PAF")
                                {
                                    // Insertion to common staging
                                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadProfileDataFromDeutscheStagingToCommonStaging.dtsx");
                                    bool deutscheProCommonStagingResult = deutscheUploadsBo.DeutscheInsertToCommonStaging(processID, packagePath, configPath);
                                    if (deutscheProCommonStagingResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog3)
                                        {
                                            //common profile checks
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                            bool deutscheProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                            if (deutscheProCommonChecksResult)
                                            {
                                                // Insert Customer Details into WERP Tables
                                                bool deutscheProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                if (deutscheProCreateCustomerResult)
                                                {
                                                    //Create new Bank Accounts
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                    bool deutscheProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                    if (deutscheProCreateBankAccountResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "DT");
                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                        processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {

                                packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadFolioDataFromDeutscheStagingToCommonStaging.dtsx");
                                bool deutscheFolioCommonStagingResult = deutscheUploadsBo.DeutscheInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                if (deutscheFolioCommonStagingResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                    bool deutscheFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                    if (deutscheFolioStagingChkResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                        bool deutscheFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                        if (deutscheFolioWerpInsertionResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "DT");
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                    }
                }
                else if (FirstStagingStatus == "N")
                {
                    #region CAMS Profile

                    if (filetypeId == (int)Contants.UploadTypes.CAMSProfile)
                    {   // MF CAMS Profile Upload
                        packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                        bool camsProStagingResult = camsUploadsBo.CAMSInsertToStagingProfile(processID, packagePath, configPath);
                        if (camsProStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog2)
                            {
                                // Doing a check on data translation
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadDataTranslationChecksFirstStaging.dtsx");
                                bool camsProStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                if (camsProStagingCheckResult)
                                {
                                    if (extracttype == "PO" || extracttype == "PAF")
                                    {
                                        // Insertion to common staging

                                        // Thirs step fails
                                        packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                        bool camsProCommonStagingResult = camsUploadsBo.CAMSInsertToCommonStaging(processID, packagePath, configPath);
                                        if (camsProCommonStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (updateProcessLog3)
                                            {
                                                //common profile checks

                                                // Final step

                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                if (stdProCommonChecksResult)
                                                {

                                                    // Insert Customer Details into WERP Tables
                                                    bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                    if (camsProCreateCustomerResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "CA");
                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                        processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (extracttype == "FO" || extracttype == "PAF")
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadFolioDataFromCAMSStagingToCommonStaging.dtsx");
                                        bool camsFolioCommonStagingResult = camsUploadsBo.CAMSInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                        if (camsFolioCommonStagingResult)
                                        {
                                            //Folio Chks in Std Folio Staging 
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                            bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                            if (camsFolioStagingChkResult)
                                            {
                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                if (camsFolioWerpInsertionResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "CA");
                                                    processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "CA");
                                                    processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    if (blResult)
                                                        stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                                }
                                            }

                                        }
                                    }
                                }

                            }
                        }

                    }


                    #endregion

                    #region Sundaram Profile

                    if (filetypeId == 21)
                    {   // MF Sundaram Profile Upload
                        packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                        bool camsProStagingResult = camsUploadsBo.CAMSInsertToStagingProfile(processID, packagePath, configPath);
                        if (camsProStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog2)
                            {
                                // Doing a check on data translation
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadDataTranslationChecksFirstStaging.dtsx");
                                bool camsProStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                if (camsProStagingCheckResult)
                                {
                                    if (extracttype == "PO" || extracttype == "PAF")
                                    {
                                        // Insertion to common staging

                                        // Thirs step fails
                                        packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                        bool camsProCommonStagingResult = camsUploadsBo.SundaramInsertToCommonStaging(processID, packagePath, configPath);
                                        if (camsProCommonStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (updateProcessLog3)
                                            {
                                                //common profile checks

                                                // Final step

                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                if (stdProCommonChecksResult)
                                                {

                                                    // Insert Customer Details into WERP Tables
                                                    bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                    if (camsProCreateCustomerResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "SU");
                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                        processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (extracttype == "FO" || extracttype == "PAF")
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadFolioDataFromSundaramStagingToCommonStaging.dtsx");
                                        bool camsFolioCommonStagingResult = camsUploadsBo.SundaramInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                        if (camsFolioCommonStagingResult)
                                        {
                                            //Folio Chks in Std Folio Staging 
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                            bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                            if (camsFolioStagingChkResult)
                                            {
                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                if (camsFolioWerpInsertionResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "SU");
                                                    processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "SU");
                                                    processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    if (blResult)
                                                        stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                                }
                                            }

                                        }
                                    }
                                }

                            }
                        }

                    }


                    #endregion


                    #region Standard Equity Trade Account First Staging Insertion

                    else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTradeAccount)
                    {
                        // WERP Equity Insert To 1st Staging Trade Account
                        packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeAccInputToEQStdTradeAccStaging.dtsx");
                        werpEQFirstStagingResult = werpEQUploadsBo.WerpEQInsertToFirstStagingTradeAccount(processID, packagePath, configPath);
                        if (werpEQFirstStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                            if (updateProcessLog2)
                            {
                                // Doing a check on data in First Staging and marking IsRejected flag
                                packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQStdTradeAccStaging.dtsx");
                                werpEQFirstStagingCheckResult = werpEQUploadsBo.WerpEQProcessDataInFirstStagingTradeAccount(processID, packagePath, configPath, adviserId);

                                if (werpEQFirstStagingCheckResult)
                                {
                                    //Step 3:
                                    // WERP Equity Insert To 2nd Staging Trade Account
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQStdTradeStagingToEQTradeStaging.dtsx");
                                    werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTradeAccount(processID, packagePath, configPath, 8); // EQ Trans File Type Id = 8

                                    if (werpEQSecondStagingResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        if (updateProcessLog3)
                                        {
                                            //Step 4:
                                            // WERP Insertion
                                            packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQTradeStaging.dtsx");
                                            WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTradeAccount(processID, packagePath, configPath, adviserId);

                                            if (WERPEQSecondStagingCheckResult)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeStagingToWerp.dtsx");
                                                WERPEQTradeWerpResult = werpEQUploadsBo.WERPEQInsertTradeAccountDetails(processID, packagePath, configPath);
                                                if (WERPEQTradeWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPEQ");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetAccountsUploadRejectCount(processID, "WPEQ");
                                                    processlogVo.EndTime = DateTime.Now;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }


                    #endregion

                    #region CAMS MF TRansaction First Staging Insertion

                    else if (filetypeId == (int)Contants.UploadTypes.CAMSTransaction)
                    {
                        CamsUploadsBo camsUploadBo = new CamsUploadsBo();
                        packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadXtrnlTransactionInputToXtrnlTransactionStaging.dtsx");
                        bool camsTranStagingResult = camsUploadsBo.CAMSInsertToStagingTrans(processID, packagePath, configPath);

                        if (camsTranStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadChecksCAMSTransactionStaging.dtsx");
                                bool camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                                if (camsTranStagingCheckResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                        bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "CA", "CAMS");
                                        if (CommonTransChecks)
                                        {

                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                            bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                            if (camsTranWerpResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeCAMS);
                                                processlogVo.EndTime = DateTime.Now;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region Sundaram MF TRansaction First Staging Insertion

                    else if (filetypeId == 25)
                    {
                        CamsUploadsBo camsUploadBo = new CamsUploadsBo();
                        packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadXtrnlTransactionInputToXtrnlTransactionStaging.dtsx");
                        bool camsTranStagingResult = camsUploadsBo.CAMSInsertToStagingTrans(processID, packagePath, configPath);

                        if (camsTranStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadChkSundaramTransactionStaging.dtsx");
                                bool camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                                if (camsTranStagingCheckResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                        bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "SU", "Sundaram");
                                        if (CommonTransChecks)
                                        {

                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                            bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                            if (camsTranWerpResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "SU");
                                                processlogVo.EndTime = DateTime.Now;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region WERP Equity Transation First Staging Insertion

                    else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTransaction)
                    {
                        werpEQUploadsBo = new WerpEQUploadsBo();
                        packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTransactionInputToEQStdTranStaging.dtsx");
                        werpEQFirstStagingResult = werpEQUploadsBo.WerpEQInsertToFirstStagingTransaction(processID, packagePath, configPath);
                        if (werpEQFirstStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                            if (updateProcessLog2)
                            {
                                // Doing a check on data in First Staging and marking IsRejected flag
                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQStdTranStaging.dtsx");
                                werpEQFirstStagingCheckResult = werpEQUploadsBo.WerpEQProcessDataInFirstStagingTrans(processID, packagePath, configPath, adviserId);

                                if (werpEQFirstStagingCheckResult)
                                {
                                    // WERP Equity Insert To 2nd Staging Transaction
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQStdTranStagingToEQTranStaging.dtsx");
                                    werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTransaction(processID, packagePath, configPath, 8); // EQ Trans File Type Id = 8

                                    if (werpEQSecondStagingResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        if (updateProcessLog3)
                                        {
                                            // WERP Insertion
                                            packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
                                            WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(processID, packagePath, configPath, adviserId);

                                            if (WERPEQSecondStagingCheckResult)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                                                bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(processID, packagePath, configPath);
                                                if (WERPEQTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPEQ");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "WPEQ");
                                                    processlogVo.EndTime = DateTime.Now;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region KARVY MF Transaction First Staging Insertion

                    else if (filetypeId == (int)Contants.UploadTypes.KarvyTransaction)
                    {
                        KarvyUploadsBo karvyUploadBo = new KarvyUploadsBo();
                        packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadKarvyXtrnlTransactionInputToKarvyXtrnlTransactionStaging.dtsx");
                        bool karvyTranStagingResult = karvyUploadBo.KarvyInsertToStagingTrans(processID, packagePath, configPath);
                        if (karvyTranStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog)
                            {

                                packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadChecksKarvyTransactionStaging.dtsx");
                                bool karvyTranStagingCheckResult = karvyUploadBo.KarvyProcessDataInStagingTrans(processID, packagePath, configPath);
                                if (karvyTranStagingCheckResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                        bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "KA", "Karvy");
                                        if (CommonTransChecks)
                                        {

                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                            bool karvyTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                            if (karvyTranWerpResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeKarvy);
                                                processlogVo.EndTime = DateTime.Now;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region KARVY Profile + Folio First Staging Insertion

                    else if (filetypeId == (int)Contants.UploadTypes.KarvyProfile)
                    {   // MF Karvy Profile Upload

                        packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileInputToKarvyXtrnlProfileStaging .dtsx");
                        bool karvyProStagingResult = karvyUploadsBo.KARVYInsertToStagingProfile(processID, packagePath, configPath);
                        if (karvyProStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog2)
                            {
                                // Data Translation checks in Karvy Staging
                                packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadDataTranslationKarvyProfileStaging.dtsx");
                                bool karvyProStagingCheckResult = karvyUploadsBo.KARVYStagingDataTranslationCheck(processID, packagePath, configPath);
                                if (karvyProStagingCheckResult)
                                {
                                    if (extracttype == "PO" || extracttype == "PAF")
                                    {
                                        // Inserting Karvy Staging Data to Profile Staging
                                        packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToProfileStaging.dtsx");
                                        bool karvyStagingToProfileStagingResult = karvyUploadsBo.KARVYStagingInsertToProfileStaging(processID, packagePath, configPath);
                                        if (karvyStagingToProfileStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (updateProcessLog3)
                                            {
                                                //Making Chks in Profile Staging
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                bool karvyProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                if (karvyProCommonChecksResult)
                                                {
                                                    // Insert Customer Details into WERP Tables
                                                    bool karvyProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                    if (karvyProCreateCustomerResult)
                                                    {
                                                        //Create new Bank Accounts
                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                        bool karvyProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                        if (karvyProCreateBankAccountResult)
                                                        {
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "KA");
                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (extracttype == "PO" || extracttype == "PAF")
                                    {
                                        if (karvyProStagingCheckResult)
                                        {
                                            // Inserting Karvy Staging Data to Folio Staging
                                            packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToFolioStaging.dtsx");
                                            bool karvyStagingToFolioStagingResult = karvyUploadsBo.KARVYStagingInsertToFolioStaging(processID, packagePath, configPath);
                                            if (karvyStagingToFolioStagingResult)
                                            {

                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                                bool stdFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                                if (stdFolioStagingChkResult)
                                                {
                                                    //Folio Chks in Std Folio Staging 
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                    blResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                    if (blResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "KA");
                                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region Standard Profile First Staging INsertion

                    else if (filetypeId == (int)Contants.UploadTypes.StandardProfile)
                    {
                        StandardProfileUploadBo StandardProfileUploadBo = new StandardProfileUploadBo();
                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileInputDataToFirstStaging.dtsx");
                        bool stdProFirstStagingResult = StandardProfileUploadBo.StdInsertToFirstStaging(processID, packagePath, configPath);
                        if (stdProFirstStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog2)
                            {
                                // Data translation checks
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsDataTranslationChecksInStdProfileMainStaging.dtsx");
                                bool stdProTranslationCheckStagingResult = StandardProfileUploadBo.StdDataTranslationCheckInFirstStaging(processID, packagePath, configPath);
                                if (stdProTranslationCheckStagingResult)
                                {
                                    // Insertion to common staging
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileFirstStagingToCommonStaging.dtsx");
                                    bool stdProCommonStagingResult = StandardProfileUploadBo.StdInsertToCommonStaging(processID, packagePath, configPath);
                                    if (stdProCommonStagingResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog3)
                                        {
                                            //common profile checks
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                            bool stdProCommonChecksResult = StandardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                            if (stdProCommonChecksResult)
                                            {
                                                // Insert Customer Details into WERP Tables
                                                bool stdProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out  countCustCreated);
                                                if (stdProCreateCustomerResult)
                                                {
                                                    //Create new Bank Accounts
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                    bool stdProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                    if (stdProCreateBankAccountResult)
                                                    {

                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                        {
                                                            stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "WP");
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

                    #endregion

                    #region Templeton Transaction First Staging Insertion
                    else if (filetypeId == (int)Contants.UploadTypes.TempletonTransaction)
                    {
                        packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTempletonTransToStagingTable.dtsx");
                        bool templeTranStagingResult = templetonUploadsBo.TempletonInsertToStagingTrans(processID, packagePath, configPath);
                        if (templeTranStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                            if (updateProcessLog)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadChecksOnTempletonStaging.dtsx");
                                bool templeTranStagingCheckResult = templetonUploadsBo.TempletonProcessDataInStagingTrans(processID, packagePath, configPath);
                                if (templeTranStagingCheckResult)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTempletonTransStagingToTransStaging.dtsx");
                                    bool templeTranSecondStagingResult = templetonUploadsBo.TempletonInsertFromTempStagingTransToCommonStaging(processID, packagePath, configPath);
                                    if (templeTranSecondStagingResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        if (updateProcessLog)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                            bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "TN", "Templeton");

                                            if (CommonTransChecks)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                bool templeTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                                if (templeTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeTemp);
                                                    processlogVo.EndTime = DateTime.Now;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region Templeton Profile + Folio First Staging Insertion

                    else if (filetypeId == (int)Contants.UploadTypes.TempletonProfile)
                    {
                        // Templeton Insert To Staging Profile
                        packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadTempXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                        bool templetonProStagingResult = templetonUploadsBo.TempInsertToStagingProfile(processID, packagePath, configPath);
                        if (templetonProStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog2)
                            {
                                // Doing a check on data translation
                                packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadTempDataTranslationChecksFirstStaging.dtsx");
                                bool templetonProStagingCheckResult = templetonUploadsBo.TempProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                if (templetonProStagingCheckResult)
                                {
                                    if (extracttype == "PO" || extracttype == "PAF")
                                    {
                                        // Insertion to common staging
                                        packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadProfileDataFromTempStagingToCommonStaging.dtsx");
                                        bool templetonProCommonStagingResult = templetonUploadsBo.TempInsertToCommonStaging(processID, packagePath, configPath);
                                        if (templetonProCommonStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (updateProcessLog3)
                                            {
                                                //common profile checks
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                bool templetonProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                if (templetonProCommonChecksResult)
                                                {
                                                    // Insert Customer Details into WERP Tables
                                                    bool templetonProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                    if (templetonProCreateCustomerResult)
                                                    {
                                                        //Create new Bank Accounts
                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                        bool templetonProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                        if (templetonProCreateBankAccountResult)
                                                        {
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "TN");
                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (extracttype == "FO" || extracttype == "PAF")
                        {
                            if (templetonProStagingResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadFolioDataFromTempStagingToCommonStaging.dtsx");
                                bool templetonFolioCommonStagingResult = templetonUploadsBo.TempInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                if (templetonFolioCommonStagingResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                    bool templetonFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                    if (templetonFolioStagingChkResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                        bool templetonFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                        if (templetonFolioWerpInsertionResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "TN");
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region Deutsche Transaction First Staging Insertion
                    else if (filetypeId == (int)Contants.UploadTypes.DeutscheTransaction)
                    {
                        packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionInputToFirstStaging.dtsx");
                        bool deutscheTranStagingResult = deutscheUploadsBo.DeutscheInsertToStagingTrans(processID, packagePath, configPath);
                        if (deutscheTranStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                            if (updateProcessLog)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionFirstStagingtoSecondStaging.dtsx");
                                bool deutscheTranStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                                if (deutscheTranStagingCheckResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                    if (updateProcessLog)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                        bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "DT", "Deutsche");

                                        if (CommonTransChecks)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                            bool deutscheTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                            if (deutscheTranWerpResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeDeutsche);
                                                processlogVo.EndTime = DateTime.Now;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region Deutsche Profile + Folio First Staging Insertion
                    else if (filetypeId == (int)Contants.UploadTypes.DeutscheProfile)
                    {
                        // Deutsche Insert To Staging Profile
                        packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                        bool deutscheProStagingResult = deutscheUploadsBo.DeutscheInsertToStagingProfile(processID, packagePath, configPath);
                        if (deutscheProStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog2)
                            {
                                // Doing a check on data translation
                                packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheDataTranslationChecksFirstStaging.dtsx");
                                bool deutscheProStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                if (deutscheProStagingCheckResult)
                                {
                                    if (extracttype == "PO" || extracttype == "PAF")
                                    {
                                        // Insertion to common staging
                                        packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadProfileDataFromDeutscheStagingToCommonStaging.dtsx");
                                        bool deutscheProCommonStagingResult = deutscheUploadsBo.DeutscheInsertToCommonStaging(processID, packagePath, configPath);
                                        if (deutscheProCommonStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (updateProcessLog3)
                                            {
                                                //common profile checks
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                bool deutscheProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                if (deutscheProCommonChecksResult)
                                                {
                                                    // Insert Customer Details into WERP Tables
                                                    bool deutscheProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                    if (deutscheProCreateCustomerResult)
                                                    {
                                                        //Create new Bank Accounts
                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                        bool deutscheProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                        if (deutscheProCreateBankAccountResult)
                                                        {
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "DT");
                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (extracttype == "PO" || extracttype == "PAF")
                        {
                            if (deutscheProStagingResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadFolioDataFromDeutscheStagingToCommonStaging.dtsx");
                                bool deutscheFolioCommonStagingResult = deutscheUploadsBo.DeutscheInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                if (deutscheFolioCommonStagingResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                    bool deutscheFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                    if (deutscheFolioStagingChkResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                        bool deutscheFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                        if (deutscheFolioWerpInsertionResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "DT");
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    else
                    {
                        // Invalid Number
                    }
                }
            }
            else if (InputInsertionStatus == "N")
            {
                xmlFileName = Server.MapPath("\\UploadFiles\\" + processID + ".xml");

                #region CAMS Profile

                if (filetypeId == (int)Contants.UploadTypes.CAMSProfile)
                {   // MF CAMS Profile Upload
                    // CAMS Insert To Input Profile
                    // First step fails
                    packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSFileToXtrnlProfileInput.dtsx");
                    bool camsProInputResult = camsUploadsBo.CAMSInsertToInputProfile(processID, packagePath, xmlFileName, configPath);
                    if (camsProInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (updateProcessLog1)
                        {
                            // CAMS Insert To Staging Profile

                            // Second step fails
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                            bool camsProStagingResult = camsUploadsBo.CAMSInsertToStagingProfile(processID, packagePath, configPath);
                            if (camsProStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog2)
                                {
                                    // Doing a check on data translation
                                    packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadDataTranslationChecksFirstStaging.dtsx");
                                    bool camsProStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                    if (camsProStagingCheckResult)
                                    {
                                        if (extracttype == "PO" || extracttype == "PAF")
                                        {
                                            // Insertion to common staging

                                            // Thirs step fails
                                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                            bool camsProCommonStagingResult = camsUploadsBo.CAMSInsertToCommonStaging(processID, packagePath, configPath);
                                            if (camsProCommonStagingResult)
                                            {
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (updateProcessLog3)
                                                {
                                                    //common profile checks

                                                    // Final step

                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                    bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                    if (stdProCommonChecksResult)
                                                    {

                                                        // Insert Customer Details into WERP Tables
                                                        bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                        if (camsProCreateCustomerResult)
                                                        {
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "CA");
                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (extracttype == "FO" || extracttype == "PAF")
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadFolioDataFromCAMSStagingToCommonStaging.dtsx");
                                            bool camsFolioCommonStagingResult = camsUploadsBo.CAMSInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                            if (camsFolioCommonStagingResult)
                                            {
                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                                bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                                if (camsFolioStagingChkResult)
                                                {
                                                    //Folio Chks in Std Folio Staging 
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                    bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                    if (camsFolioWerpInsertionResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "CA");
                                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "CA");
                                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
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

                #endregion


                #region Sundaram Profile

                if (filetypeId == 21)
                {   // MF CAMS Profile Upload
                    // CAMS Insert To Input Profile
                    // First step fails
                    // Sundaram Insert To Input Profile
                    packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\SundramFileToInput.dtsx");
                    bool camsProInputResult = camsUploadsBo.SundramInsertToInputProfile(processID, packagePath, xmlFileName, configPath);
                    if (camsProInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (updateProcessLog1)
                        {
                            // CAMS Insert To Staging Profile

                            // Second step fails
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                            bool camsProStagingResult = camsUploadsBo.CAMSInsertToStagingProfile(processID, packagePath, configPath);
                            if (camsProStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog2)
                                {
                                    // Doing a check on data translation
                                    packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadDataTranslationChecksFirstStaging.dtsx");
                                    bool camsProStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                    if (camsProStagingCheckResult)
                                    {
                                        if (extracttype == "PO" || extracttype == "PAF")
                                        {
                                            // Insertion to common staging

                                            // Thirs step fails
                                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                            bool camsProCommonStagingResult = camsUploadsBo.SundaramInsertToCommonStaging(processID, packagePath, configPath);
                                            if (camsProCommonStagingResult)
                                            {
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (updateProcessLog3)
                                                {
                                                    //common profile checks

                                                    // Final step

                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                    bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                    if (stdProCommonChecksResult)
                                                    {

                                                        // Insert Customer Details into WERP Tables
                                                        bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                        if (camsProCreateCustomerResult)
                                                        {
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "SU");
                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (extracttype == "FO" || extracttype == "PAF")
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadFolioDataFromSundaramStagingToCommonStaging.dtsx");
                                            bool camsFolioCommonStagingResult = camsUploadsBo.SundaramInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                            if (camsFolioCommonStagingResult)
                                            {
                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                                bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                                if (camsFolioStagingChkResult)
                                                {
                                                    //Folio Chks in Std Folio Staging 
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                    bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                    if (camsFolioWerpInsertionResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "SU");
                                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "SU");
                                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
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

                #endregion
                #region Standard Equity Trade Account Input Insertion

                else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTradeAccount)
                {
                    // WERP Equity Insert To Input Profile
                    //Step 1:
                    packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadTradeAccDataFromEQStdFileToEQStdTradeAccInput.dtsx");
                    werpEQTradeInputResult = werpEQUploadsBo.WerpEQInsertToInputTradeAccount(packagePath, xmlFileName, configPath);
                    if (werpEQTradeInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.IsInsertionToXtrnlComplete = 2;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                        if (updateProcessLog1)
                        {
                            //Step2:
                            // WERP Equity Insert To 1st Staging Trade Account
                            packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeAccInputToEQStdTradeAccStaging.dtsx");
                            werpEQFirstStagingResult = werpEQUploadsBo.WerpEQInsertToFirstStagingTradeAccount(processID, packagePath, configPath);
                            if (werpEQFirstStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                if (updateProcessLog2)
                                {
                                    // Doing a check on data in First Staging and marking IsRejected flag
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQStdTradeAccStaging.dtsx");
                                    werpEQFirstStagingCheckResult = werpEQUploadsBo.WerpEQProcessDataInFirstStagingTradeAccount(processID, packagePath, configPath, adviserId);

                                    if (werpEQFirstStagingCheckResult)
                                    {
                                        //Step 3:
                                        // WERP Equity Insert To 2nd Staging Trade Account
                                        packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQStdTradeStagingToEQTradeStaging.dtsx");
                                        werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTradeAccount(processID, packagePath, configPath, 8); // EQ Trans File Type Id = 8

                                        if (werpEQSecondStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            if (updateProcessLog3)
                                            {
                                                //Step 4:
                                                // WERP Insertion
                                                packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQTradeStaging.dtsx");
                                                WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTradeAccount(processID, packagePath, configPath, adviserId);

                                                if (WERPEQSecondStagingCheckResult)
                                                {
                                                    packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeStagingToWerp.dtsx");
                                                    WERPEQTradeWerpResult = werpEQUploadsBo.WERPEQInsertTradeAccountDetails(processID, packagePath, configPath);
                                                    if (WERPEQTradeWerpResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPEQ");
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetAccountsUploadRejectCount(processID, "WPEQ");
                                                        processlogVo.EndTime = DateTime.Now;
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
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

                #endregion

                #region CAMS MF TRansaction Input Insertion

                else if (filetypeId == (int)Contants.UploadTypes.CAMSTransaction)
                {
                    CamsUploadsBo camsUploadBo = new CamsUploadsBo();

                    packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadTransactionDataFromCAMSFileToXtrnlTransactionInput.dtsx");
                    bool camsTranInputResult = camsUploadsBo.CAMSInsertToInputTrans(processID, packagePath, xmlFileName, configPath);
                    if (camsTranInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        processlogVo.IsInsertionToXtrnlComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (updateProcessLog)
                        {
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadXtrnlTransactionInputToXtrnlTransactionStaging.dtsx");
                            bool camsTranStagingResult = camsUploadsBo.CAMSInsertToStagingTrans(processID, packagePath, configPath);
                            if (camsTranStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog)
                                {

                                    packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadChecksCAMSTransactionStaging.dtsx");
                                    bool camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                                    if (camsTranStagingCheckResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                            bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "CA", "CAMS");
                                            if (CommonTransChecks)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                                if (camsTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeCAMS);
                                                    processlogVo.EndTime = DateTime.Now;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion


                #region Sundaram MF TRansaction Input Insertion

                else if (filetypeId == 25)
                {
                    CamsUploadsBo camsUploadBo = new CamsUploadsBo();

                    packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadTransactionDataFromCAMSFileToXtrnlTransactionInput.dtsx");
                    bool camsTranInputResult = camsUploadsBo.CAMSInsertToInputTrans(processID, packagePath, xmlFileName, configPath);
                    if (camsTranInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        processlogVo.IsInsertionToXtrnlComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (updateProcessLog)
                        {
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadXtrnlTransactionInputToXtrnlTransactionStaging.dtsx");
                            bool camsTranStagingResult = camsUploadsBo.CAMSInsertToStagingTrans(processID, packagePath, configPath);
                            if (camsTranStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog)
                                {

                                    packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadChkSundaramTransactionStaging.dtsx");
                                    bool camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                                    if (camsTranStagingCheckResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                            bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "SU", "Sundaram");
                                            if (CommonTransChecks)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                                if (camsTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "");
                                                    processlogVo.EndTime = DateTime.Now;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region WERP Equity Transation Input Insertion

                else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTransaction)
                {
                    werpEQUploadsBo = new WerpEQUploadsBo();
                    packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadTransactionDataFromEQStdFileToEQStdTranInput.dtsx");
                    bool werpEQTranInputResult = werpEQUploadsBo.WerpEQInsertToInputTransaction(packagePath, xmlFileName, configPath);
                    if (werpEQTranInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.IsInsertionToXtrnlComplete = 2;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                        if (updateProcessLog1)
                        {
                            // WERP Equity Insert To 1st Staging Transaction
                            packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTransactionInputToEQStdTranStaging.dtsx");
                            werpEQFirstStagingResult = werpEQUploadsBo.WerpEQInsertToFirstStagingTransaction(processID, packagePath, configPath);
                            if (werpEQFirstStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                if (updateProcessLog2)
                                {
                                    // Doing a check on data in First Staging and marking IsRejected flag
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQStdTranStaging.dtsx");
                                    werpEQFirstStagingCheckResult = werpEQUploadsBo.WerpEQProcessDataInFirstStagingTrans(processID, packagePath, configPath, adviserId);

                                    if (werpEQFirstStagingCheckResult)
                                    {
                                        // WERP Equity Insert To 2nd Staging Transaction
                                        packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQStdTranStagingToEQTranStaging.dtsx");
                                        werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTransaction(processID, packagePath, configPath, 8); // EQ Trans File Type Id = 8

                                        if (werpEQSecondStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            if (updateProcessLog3)
                                            {
                                                // WERP Insertion
                                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
                                                WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(processID, packagePath, configPath, adviserId);

                                                if (WERPEQSecondStagingCheckResult)
                                                {
                                                    packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                                                    bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(processID, packagePath, configPath);
                                                    if (WERPEQTranWerpResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPEQ");
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "WPEQ");
                                                        processlogVo.EndTime = DateTime.Now;
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
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

                #endregion

                #region KARVY MF Transaction Input Insertion

                else if (filetypeId == (int)Contants.UploadTypes.KarvyTransaction)
                {
                    KarvyUploadsBo karvyUploadBo = new KarvyUploadsBo();

                    packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadTransactionDataFromKarvyFileToXtrnlTransactionInput.dtsx");
                    bool karvyTranInputResult = karvyUploadBo.KarvyInsertToInputTrans(processID, packagePath, xmlFileName, configPath);
                    if (karvyTranInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        processlogVo.IsInsertionToXtrnlComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (updateProcessLog)
                        {

                            packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadKarvyXtrnlTransactionInputToKarvyXtrnlTransactionStaging.dtsx");
                            bool karvyTranStagingResult = karvyUploadBo.KarvyInsertToStagingTrans(processID, packagePath, configPath);
                            if (karvyTranStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog)
                                {

                                    packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadChecksKarvyTransactionStaging.dtsx");
                                    bool karvyTranStagingCheckResult = karvyUploadBo.KarvyProcessDataInStagingTrans(processID, packagePath, configPath);
                                    if (karvyTranStagingCheckResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                            bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "KA", "Karvy");
                                            if (CommonTransChecks)
                                            {

                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                bool karvyTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                                if (karvyTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeKarvy);
                                                    processlogVo.EndTime = DateTime.Now;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region KARVY Profile + Folio Input Insertion

                else if (filetypeId == (int)Contants.UploadTypes.KarvyProfile)
                {   // MF Karvy Profile Upload

                    packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadProfileDataFromKarvyProfileFileToXtrnlProfileInput.dtsx");
                    bool karvyProInputResult = karvyUploadsBo.KARVYInsertToInputProfile(packagePath, processID, xmlFileName, configPath);
                    if (karvyProInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                        if (updateProcessLog1)
                        {
                            // Karvy Insert To Staging Profile
                            packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileInputToKarvyXtrnlProfileStaging .dtsx");
                            bool karvyProStagingResult = karvyUploadsBo.KARVYInsertToStagingProfile(processID, packagePath, configPath);
                            if (karvyProStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog2)
                                {
                                    // Data Translation checks in Karvy Staging
                                    packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadDataTranslationKarvyProfileStaging.dtsx");
                                    bool karvyProStagingCheckResult = karvyUploadsBo.KARVYStagingDataTranslationCheck(processID, packagePath, configPath);
                                    if (karvyProStagingCheckResult)
                                    {
                                        if (extracttype == "PO" || extracttype == "PAF")
                                        {
                                            // Inserting Karvy Staging Data to Profile Staging
                                            packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToProfileStaging.dtsx");
                                            bool karvyStagingToProfileStagingResult = karvyUploadsBo.KARVYStagingInsertToProfileStaging(processID, packagePath, configPath);
                                            if (karvyStagingToProfileStagingResult)
                                            {
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (updateProcessLog3)
                                                {
                                                    //Making Chks in Profile Staging
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                    bool karvyProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                    if (karvyProCommonChecksResult)
                                                    {
                                                        // Insert Customer Details into WERP Tables
                                                        bool karvyProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                        if (karvyProCreateCustomerResult)
                                                        {
                                                            //Create new Bank Accounts
                                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                            bool karvyProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                            if (karvyProCreateBankAccountResult)
                                                            {
                                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                                processlogVo.EndTime = DateTime.Now;
                                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "KA");
                                                                processlogVo.NoOfCustomerInserted = countCustCreated;
                                                                processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                if (blResult)
                                                                    stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (extracttype == "FO" || extracttype == "PAF")
                                        {
                                            if (karvyProStagingCheckResult)
                                            {
                                                // Inserting Karvy Staging Data to Folio Staging
                                                packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToFolioStaging.dtsx");
                                                bool karvyStagingToFolioStagingResult = karvyUploadsBo.KARVYStagingInsertToFolioStaging(processID, packagePath, configPath);
                                                if (karvyStagingToFolioStagingResult)
                                                {

                                                    //Folio Chks in Std Folio Staging 
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                                    bool stdFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                                    if (stdFolioStagingChkResult)
                                                    {
                                                        //Folio Chks in Std Folio Staging 
                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                        blResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                        if (blResult)
                                                        {
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "KA");
                                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
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

                #endregion

                #region Standard Profile Input INsertion

                else if (filetypeId == (int)Contants.UploadTypes.StandardProfile)
                {  // Std Insert To Input Profile
                    StandardProfileUploadBo StandardProfileUploadBo = new StandardProfileUploadBo();
                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileDataFromFileToInput.dtsx");
                    bool stdProInputResult = StandardProfileUploadBo.StdInsertToInputProfile(packagePath, xmlFileName, configPath);
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
                            bool stdProFirstStagingResult = StandardProfileUploadBo.StdInsertToFirstStaging(processID, packagePath, configPath);
                            if (stdProFirstStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog2)
                                {
                                    // Data translation checks
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsDataTranslationChecksInStdProfileMainStaging.dtsx");
                                    bool stdProTranslationCheckStagingResult = StandardProfileUploadBo.StdDataTranslationCheckInFirstStaging(processID, packagePath, configPath);
                                    if (stdProTranslationCheckStagingResult)
                                    {
                                        // Insertion to common staging
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileFirstStagingToCommonStaging.dtsx");
                                        bool stdProCommonStagingResult = StandardProfileUploadBo.StdInsertToCommonStaging(processID, packagePath, configPath);
                                        if (stdProCommonStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (updateProcessLog3)
                                            {
                                                //common profile checks
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                bool stdProCommonChecksResult = StandardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                if (stdProCommonChecksResult)
                                                {
                                                    // Insert Customer Details into WERP Tables
                                                    bool stdProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out  countCustCreated);
                                                    if (stdProCreateCustomerResult)
                                                    {
                                                        //Create new Bank Accounts
                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                        bool stdProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                        if (stdProCreateBankAccountResult)
                                                        {

                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                            {
                                                                stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "WP");
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
                }

                #endregion

                #region Templeton Transaction Input Insertion
                else if (filetypeId == (int)Contants.UploadTypes.TempletonTransaction)
                {
                    packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTransactionFromXmlFileToInputTable.dtsx");
                    bool templeTranInputResult = templetonUploadsBo.TempletonInsertToInputTrans(processID, packagePath, xmlFileName, configPath);
                    if (templeTranInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.IsInsertionToXtrnlComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                        if (updateProcessLog)
                        {
                            packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTempletonTransToStagingTable.dtsx");
                            bool templeTranStagingResult = templetonUploadsBo.TempletonInsertToStagingTrans(processID, packagePath, configPath);
                            if (templeTranStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                if (updateProcessLog)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadChecksOnTempletonStaging.dtsx");
                                    bool templeTranStagingCheckResult = templetonUploadsBo.TempletonProcessDataInStagingTrans(processID, packagePath, configPath);
                                    if (templeTranStagingCheckResult)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTempletonTransStagingToTransStaging.dtsx");
                                        bool templeTranSecondStagingResult = templetonUploadsBo.TempletonInsertFromTempStagingTransToCommonStaging(processID, packagePath, configPath);
                                        if (templeTranSecondStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            if (updateProcessLog)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                                bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "TN", "Templeton");

                                                if (CommonTransChecks)
                                                {
                                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                    bool templeTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                                    if (templeTranWerpResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeTemp);
                                                        processlogVo.EndTime = DateTime.Now;
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
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
                #endregion

                #region Templeton Profile + Folio Input Insertion
                else if (filetypeId == (int)Contants.UploadTypes.TempletonProfile)
                {
                    // Templeton Insert To Input Profile
                    packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadTempletonProfileDataFromFileToInput.dtsx");
                    bool templetonProInputResult = templetonUploadsBo.TempInsertToInputProfile(processID, packagePath, xmlFileName, configPath);
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
                            bool templetonProStagingResult = templetonUploadsBo.TempInsertToStagingProfile(processID, packagePath, configPath);
                            if (templetonProStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog2)
                                {
                                    // Doing a check on data translation
                                    packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadTempDataTranslationChecksFirstStaging.dtsx");
                                    bool templetonProStagingCheckResult = templetonUploadsBo.TempProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                    if (templetonProStagingCheckResult)
                                    {
                                        if (extracttype == "PO" || extracttype == "PAF")
                                        {
                                            // Insertion to common staging
                                            packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadProfileDataFromTempStagingToCommonStaging.dtsx");
                                            bool templetonProCommonStagingResult = templetonUploadsBo.TempInsertToCommonStaging(processID, packagePath, configPath);
                                            if (templetonProCommonStagingResult)
                                            {
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (updateProcessLog3)
                                                {
                                                    //common profile checks
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                    bool templetonProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                    if (templetonProCommonChecksResult)
                                                    {
                                                        // Insert Customer Details into WERP Tables
                                                        bool templetonProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                        if (templetonProCreateCustomerResult)
                                                        {
                                                            //Create new Bank Accounts
                                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                            bool templetonProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                            if (templetonProCreateBankAccountResult)
                                                            {
                                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                                processlogVo.EndTime = DateTime.Now;
                                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "TN");
                                                                processlogVo.NoOfCustomerInserted = countCustCreated;
                                                                processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                if (blResult)
                                                                    stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                if (templetonProStagingResult)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadFolioDataFromTempStagingToCommonStaging.dtsx");
                                    bool templetonFolioCommonStagingResult = templetonUploadsBo.TempInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                    if (templetonFolioCommonStagingResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                        bool templetonFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                        if (templetonFolioStagingChkResult)
                                        {
                                            //Folio Chks in Std Folio Staging 
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                            bool templetonFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                            if (templetonFolioWerpInsertionResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "TN");
                                                processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                                processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (blResult)
                                                    stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region Deutsche Transaction Input Insertion
                else if (filetypeId == (int)Contants.UploadTypes.DeutscheTransaction)
                {
                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionXMLFileToInputTable.dtsx");
                    bool deutscheTranInputResult = deutscheUploadsBo.DeutscheInsertToInputTrans(processID, packagePath, xmlFileName, configPath);

                    if (deutscheTranInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.IsInsertionToXtrnlComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                        if (updateProcessLog)
                        {
                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionInputToFirstStaging.dtsx");
                            bool deutscheTranStagingResult = deutscheUploadsBo.DeutscheInsertToStagingTrans(processID, packagePath, configPath);
                            if (deutscheTranStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                if (updateProcessLog)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionFirstStagingtoSecondStaging.dtsx");
                                    bool deutscheTranStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                                    if (deutscheTranStagingCheckResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        if (updateProcessLog)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                            bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "DT", "Deutsche");

                                            if (CommonTransChecks)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                bool deutscheTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                                if (deutscheTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeDeutsche);
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
                #endregion

                #region Deutsche Profile + Folio Input Insertion
                else if (filetypeId == (int)Contants.UploadTypes.DeutscheProfile)
                {
                    // Deutsche Insert To Input Profile
                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheProfileDataFromFileToInput.dtsx");
                    bool deutscheProInputResult = deutscheUploadsBo.DeutscheInsertToInputProfile(processID, packagePath, xmlFileName, configPath);
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
                            bool deutscheProStagingResult = deutscheUploadsBo.DeutscheInsertToStagingProfile(processID, packagePath, configPath);
                            if (deutscheProStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog2)
                                {
                                    // Doing a check on data translation
                                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheDataTranslationChecksFirstStaging.dtsx");
                                    bool deutscheProStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                    if (deutscheProStagingCheckResult)
                                    {
                                        if (extracttype == "PO" || extracttype == "PAF")
                                        {
                                            // Insertion to common staging
                                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadProfileDataFromDeutscheStagingToCommonStaging.dtsx");
                                            bool deutscheProCommonStagingResult = deutscheUploadsBo.DeutscheInsertToCommonStaging(processID, packagePath, configPath);
                                            if (deutscheProCommonStagingResult)
                                            {
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (updateProcessLog3)
                                                {
                                                    //common profile checks
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                    bool deutscheProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                    if (deutscheProCommonChecksResult)
                                                    {
                                                        // Insert Customer Details into WERP Tables
                                                        bool deutscheProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID,rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                        if (deutscheProCreateCustomerResult)
                                                        {
                                                            //Create new Bank Accounts
                                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                            bool deutscheProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                            if (deutscheProCreateBankAccountResult)
                                                            {
                                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                                processlogVo.EndTime = DateTime.Now;
                                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "DT");
                                                                processlogVo.NoOfCustomerInserted = countCustCreated;
                                                                processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                if (blResult)
                                                                    stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                if (deutscheProStagingResult)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadFolioDataFromDeutscheStagingToCommonStaging.dtsx");
                                    bool deutscheFolioCommonStagingResult = deutscheUploadsBo.DeutscheInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                    if (deutscheFolioCommonStagingResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                        bool deutscheFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                        if (deutscheFolioStagingChkResult)
                                        {
                                            //Folio Chks in Std Folio Staging 
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                            bool deutscheFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                            if (deutscheFolioWerpInsertionResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "DT");
                                                processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                                processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (blResult)
                                                    stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

            }
            else
            {
                // Invalid Number
            }

            if (blResult)
            {
                // Display Success Message
                msgReprocessComplete.Visible = true;

                if (extracttype == "PO" || extracttype == "PAF")
                {   // Check if Profile Upload

                    trUploadedCustomers.Visible = true;
                    trUploadedFolios.Visible = true;
                    trUploadedTransactions.Visible = false;
                    trRejectedRecords.Visible = true;

                    txtUploadedCustomers.Text = processlogVo.NoOfCustomerInserted.ToString();
                    txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                }
                else if (extracttype == "FO")
                {   // Check if Profile Upload

                    trUploadedCustomers.Visible = false;
                    trUploadedFolios.Visible = true;
                    trUploadedTransactions.Visible = false;
                    trRejectedRecords.Visible = true;

                    //txtUploadedCustomers.Text = NoOfCustomersUploaded.ToString();
                    txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                }
                else if (extracttype == "MFT" || extracttype == "ET")
                {   // Check if Transaction Upload

                    trUploadedCustomers.Visible = false;
                    trUploadedFolios.Visible = false;
                    trUploadedTransactions.Visible = true;
                    trRejectedRecords.Visible = true;

                    txtUploadedTransactions.Text = processlogVo.NoOfTransactionInserted.ToString();
                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                }
                else if (extracttype == "")
                {   // Check if Combination Upload

                    trUploadedCustomers.Visible = true;
                    trUploadedFolios.Visible = true;
                    trUploadedTransactions.Visible = true;
                    trRejectedRecords.Visible = true;

                    txtUploadedCustomers.Text = processlogVo.NoOfCustomerInserted.ToString();
                    txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    txtUploadedTransactions.Text = processlogVo.NoOfTransactionInserted.ToString();
                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                }

                else if (extracttype == "SS")
                {   // Check if Combination Upload

                    trUploadedCustomers.Visible = true;
                    trUploadedFolios.Visible = true;
                    trUploadedTransactions.Visible = true;
                    trRejectedRecords.Visible = true;

                    txtUploadedCustomers.Text = processlogVo.NoOfCustomerInserted.ToString();
                    txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    txtUploadedTransactions.Text = processlogVo.NoOfTransactionInserted.ToString();
                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                }

                else if (extracttype == "TRAIL")
                {
                    trUploadedCustomers.Visible = true;
                    trUploadedFolios.Visible = true;
                    trUploadedTransactions.Visible = true;
                    trRejectedRecords.Visible = true;

                    txtUploadedCustomers.Text = processlogVo.NoOfCustomerInserted.ToString();
                    txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    txtUploadedTransactions.Text = processlogVo.NoOfTransactionInserted.ToString();
                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                }

                BindProcessHistoryGrid();
            }
            else
            {
                // Display Failure
                msgReprocessincomplete.Visible = true;

                if (extracttype == "PO" || extracttype == "PAF")
                {   // Check if Profile Upload

                    trUploadedCustomers.Visible = true;
                    trUploadedFolios.Visible = true;
                    trUploadedTransactions.Visible = false;
                    trRejectedRecords.Visible = true;

                    txtUploadedCustomers.Text = processlogVo.NoOfCustomerInserted.ToString();
                    txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                }
                if (extracttype == "FO")
                {   // Check if Profile Upload

                    trUploadedCustomers.Visible = false;
                    trUploadedFolios.Visible = true;
                    trUploadedTransactions.Visible = false;
                    trRejectedRecords.Visible = true;

                    //txtUploadedCustomers.Text = NoOfCustomersUploaded.ToString();
                    txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                }
                else if (extracttype == "MFT" || extracttype == "ET")
                {   // Check if Transaction Upload

                    trUploadedCustomers.Visible = false;
                    trUploadedFolios.Visible = false;
                    trUploadedTransactions.Visible = true;
                    trRejectedRecords.Visible = true;

                    txtUploadedTransactions.Text = processlogVo.NoOfTransactionInserted.ToString();
                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                }
                else if (extracttype == "")
                {   // Check if Combination Upload

                    trUploadedCustomers.Visible = true;
                    trUploadedFolios.Visible = true;
                    trUploadedTransactions.Visible = true;
                    trRejectedRecords.Visible = true;

                    txtUploadedCustomers.Text = processlogVo.NoOfCustomerInserted.ToString();
                    txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    txtUploadedTransactions.Text = processlogVo.NoOfTransactionInserted.ToString();
                    txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                }
                else
                {
                    trUploadedCustomers.Visible = false;
                    trUploadedFolios.Visible = false;
                    trUploadedTransactions.Visible = false;
                    trRejectedRecords.Visible = false;
                }

                BindProcessHistoryGrid();
            }
        }

        private void RollBack(int processID, int filetypeId, string InputInsertionStatus, string StagingStatus, string WERPInsertionStatus, string ExternalInsertionStatus)
        {
            camsUploadsBo = new CamsUploadsBo();
            uploadsCommonBo = new UploadCommonBo();
            processlogVo = new UploadProcessLogVo();
            bool blResult = false;

            bool blCustAssociateExists = false;
            bool blCustBankExists = false;
            bool blCustAssetExists = false;
            bool blCustEQTranNetPositionUpdated = false;
            bool blCustMFTranNetPositionUpdated = false;
            bool blCustEQTranExist = false;

            processlogVo = uploadsCommonBo.GetProcessLogInfo(processID);

            /*****/
            //RollBack Back Logic Goes Here
            blResult = uploadsCommonBo.Rollback(processID, filetypeId, InputInsertionStatus, StagingStatus, WERPInsertionStatus, ExternalInsertionStatus, out blCustAssociateExists, out blCustBankExists, out blCustAssetExists, out blCustEQTranNetPositionUpdated, out blCustMFTranNetPositionUpdated, out blCustEQTranExist);
            /*****/

            if (blResult)
            {
                // Display Success Message
                msgRollbackSuccessfull.Visible = true;
                BindProcessHistoryGrid();
            }
            else
            {   // Display relevant failure messages

                if (!blCustAssociateExists)
                {
                    msgStatus.Visible = true;
                    lblError.Text = "Cannot rollback as few customers have other customers associated!";
                }
                else if (!blCustBankExists)
                {
                    msgStatus.Visible = true;
                    lblError.Text = "Cannot rollback as few customers have Banks associated!";
                }
                else if (!blCustAssetExists)
                {
                    msgStatus.Visible = true;
                    lblError.Text = "Cannot rollback as few customers have asset details entered!";
                }

                else if (!blCustEQTranNetPositionUpdated)
                {
                    msgStatus.Visible = true;
                    lblError.Text = "Cannot rollback as few transactions have equity net positions updated!";
                }
                else if (!blCustMFTranNetPositionUpdated)
                {
                    msgStatus.Visible = true;
                    lblError.Text = "Cannot rollback as few customers mutual fund accounts / MF net positions updated!";
                }
                else if (!blCustEQTranExist)
                {
                    msgStatus.Visible = true;
                    lblError.Text = "Cannot rollback as Trade Accounts have Transactions!";
                }
            }
        }

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void gvProcessLog_Sort(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = null;
            try
            {
                sortExpression = e.SortExpression;
                ViewState["sortExpression"] = sortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    hdnSort.Value = sortExpression + " DESC";
                    this.BindProcessHistoryGrid();
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    hdnSort.Value = sortExpression + " ASC";
                    this.BindProcessHistoryGrid();
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

                FunctionInfo.Add("Method", "ViewUploadProcessLog.ascx.cs:gvProcessLog_Sort()");

                object[] objects = new object[1];
                objects[0] = sortExpression;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void gvProcessLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlAction");
                ddl.Attributes.Add("onChange", "Loading(true);setTimeout(\"UpdateImg('Image1','/Images/Wait.gif');\",50);");
            }
            //        btn_Upload.Attributes.Add("onclick",
            //"setTimeout(\"UpdateImg('Image1','/Images/Wait.gif');\",50);");
        }

        protected void gvProcessLog_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string rcbType = string.Empty;
            DataSet dtProcessLogDetails = new DataSet();
            DataTable dtrr = new DataTable();
            dtProcessLogDetails = (DataSet)Cache["ProcessLogDetails" + adviserId.ToString()];
            if (dtProcessLogDetails == null)
            {
                dtrr = dtProcessLogDetails.Tables[0];
                if (ViewState["XUET_ExtractType"] != null)
                    rcbType = ViewState["XUET_ExtractType"].ToString();
                if (!string.IsNullOrEmpty(rcbType))
                {
                    DataView dvStaffList = new DataView(dtrr, "XUET_ExtractType = '" + rcbType + "'", "ADUL_FileName,WUXFT_XMLFileName,ADUL_XMLFileName", DataViewRowState.CurrentRows);
                    // DataView dvStaffList = dtMIS.DefaultView;
                    gvProcessLog.DataSource = dvStaffList.ToTable();

                }
                else
                {
                    gvProcessLog.DataSource = dtProcessLogDetails;

                }
            }

            //DataSet dtProcessLogDetails = new DataSet();
            //dtProcessLogDetails = (DataSet)Cache["ProcessLogDetails" + adviserId.ToString()];
            //gvProcessLog.DataSource = dtProcessLogDetails;
        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvProcessLog.ExportSettings.OpenInNewWindow = true;
            gvProcessLog.ExportSettings.IgnorePaging = true;
            gvProcessLog.ExportSettings.HideStructureColumns = true;
            gvProcessLog.ExportSettings.ExportOnlyData = true;
            gvProcessLog.ExportSettings.FileName = "Upload History Details";
            gvProcessLog.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvProcessLog.MasterTableView.ExportToExcel();
        }

        public void gvProcessLog_ItemDataBound(object sender, GridItemEventArgs e)
        {


            if (e.Item is GridFilteringItem && e.Item.ItemIndex ==-1)
            {   
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                RadComboBox RadComboBoxET = (RadComboBox)filterItem.FindControl("RadComboBoxET");
              //  RadComboBox RadComboBoxIN = (RadComboBox)filterItem.FindControl("RadComboBoxRR");

               // DataSet dtProcessLogDetails = new DataSet();
                getProcessLogDs = (DataSet)Cache["ProcessLogDetails" + adviserId.ToString()];
                dtgvProcessLog = getProcessLogDs.Tables[0];
                Session["dt"] = dtgvProcessLog;
                DataTable dtcustMIS = new DataTable();
                dtcustMIS.Columns.Add("XUET_ExtractType");
                //dtcustMIS.Columns.Add("RejectReason");
                // dtcustMIS.Columns.Add("SystematicTransactionType");
                DataRow drcustMIS;
                foreach (DataRow dr in dtgvProcessLog.Rows)
                {
                    drcustMIS = dtcustMIS.NewRow();
                    drcustMIS["XUET_ExtractType"] = dr["XUET_ExtractType"].ToString();
                    //drcustMIS["RejectReason"] = dr["RejectReason"].ToString();
                    //drcustMIS["SystematicTransactionType"] = dr["TypeCode"].ToString();
                    dtcustMIS.Rows.Add(drcustMIS);
                }
               
                DataView view = new DataView(dtgvProcessLog);
                DataTable distinctValues = view.ToTable(true, "XUET_ExtractType");
                RadComboBoxET.DataSource = distinctValues;
                RadComboBoxET.DataValueField = dtcustMIS.Columns["XUET_ExtractType"].ToString();
                RadComboBoxET.DataTextField = dtcustMIS.Columns["XUET_ExtractType"].ToString();
                //RadComboBoxIN.ClearSelection();
                RadComboBoxET.DataBind();
               
            }
              
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (e.Item as GridDataItem);
                RadComboBox rcb = new RadComboBox();
                TemplateColumn tm = new TemplateColumn();
                Label lbl = new Label();
                lbl = (Label)e.Item.FindControl("lblFiletypeId");
                if (lbl.Text == "8" || lbl.Text == "19" || lbl.Text == "10" || lbl.Text == "11")
                {

                    rcb = (RadComboBox)e.Item.FindControl("ddlAction");
                    if (rcb != null)
                    {
                        rcb.Items.FindItemByValue("RollBack").Remove();
                    }
                }


            }

        }
        protected void gvProcessLog_PreRender(object sender, EventArgs e)
        {
            if (gvProcessLog.MasterTableView.FilterExpression != string.Empty)
            {
                RefreshCombos();
            }
        }

        protected void RefreshCombos()
        {
            getProcessLogDs = (DataSet)Cache["ProcessLogDetails" + adviserId.ToString()];
            dtgvProcessLog = getProcessLogDs.Tables[0];
            DataView view = new DataView(dtgvProcessLog);
            DataTable distinctValues = view.ToTable();
            DataRow[] rows = distinctValues.Select(gvProcessLog.MasterTableView.FilterExpression.ToString());
            gvProcessLog.MasterTableView.Rebind();
        }

        protected void RadComboBoxET_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

            RadComboBox dropdown = o as RadComboBox;
            ViewState["XUET_ExtractType"] = dropdown.SelectedValue.ToString();
            if (ViewState["XUET_ExtractType"] != "")
            {
                //    gvWERPTrans.MasterTableView.FilterExpression = "([RejectReason]= '" + dropdown.SelectedValue + "')";
                GridColumn column = gvProcessLog.MasterTableView.GetColumnSafe("XUET_ExtractType");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvProcessLog.MasterTableView.Rebind();
                //   // column.CurrentFilterValue = dropdown.SelectedValue.ToString();



                //    //+ Combo.SelectedValue +
            }
            else
            {
                //gvProcessLog.MasterTableView.FilterExpression = "";
                GridColumn column = gvProcessLog.MasterTableView.GetColumnSafe("XUET_ExtractType");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvProcessLog.MasterTableView.Rebind();
                //  //  column.CurrentFilterValue = dropdown.SelectedValue.ToString();
                //    // dropdown.SelectedValue = ViewState["RejectReason"].ToString();

            }

        }
        protected void rcbContinents1_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            ////persist the combo selected value  
            if (ViewState["XUET_ExtractType"] != null)
            {

                Combo.SelectedValue = ViewState["XUET_ExtractType"].ToString();
            }

        } 

        protected void ddlAdviser_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAdviser.SelectedValue != "Select" && ddlAdviser.SelectedValue != "")
            {
                adviserId = int.Parse(ddlAdviser.SelectedValue);
                DataSet dsAdviserRmDetails = superAdminOpsBo.GetAdviserRmDetails(adviserId);

                if (dsAdviserRmDetails.Tables[0].Rows.Count > 0)
                {
                    rmId = int.Parse(dsAdviserRmDetails.Tables[0].Rows[0]["ar_rmid"].ToString());
                    hfRmId.Value = rmId.ToString();
                }
                BindProcessHistoryGrid();
            }
            else
            {
                trGridView.Visible = true;
            }

        }
    }
}
