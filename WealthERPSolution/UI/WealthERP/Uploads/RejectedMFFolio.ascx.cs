using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoUploads;
using System.Data;
using VoUploads;
using VoUser;
using WealthERP.Base;
using System.Configuration;
using BoCommon;
using System.Collections;

namespace WealthERP.Uploads
{
    public partial class RejectedMFFolio : System.Web.UI.UserControl
    {
        int ProcessId;
        int filetypeId;
        RejectedRecordsBo rejectedRecordsBo;
        DataSet dsRejectedRecords;
        string configPath;
        AdvisorVo adviserVo = new AdvisorVo();
        RMVo rmVo;
        UserVo userVo;
        UploadProcessLogVo processlogVo;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            //ProcessId = 0;
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];            
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session[SessionContents.UserVo];

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            if (Request.QueryString["filetypeid"] != null)
            {
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());
                LinkInputRejects.Visible = true;
            }
            else
                LinkInputRejects.Visible = false;

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
                mypager.CurrentPage = 1;
                hdnProcessIdFilter.Value = ProcessId.ToString();
                //Session["ProcessId"] = "";
                //Session["ProcessIdMaptoCustomers"] = "";
                //if (Session["ProcessIdMaptoCustomers"].ToString() == "1")
                //{
                //    ProcessId = int.Parse(Session["ProcessId"].ToString());
                //}

                ProcessId = int.Parse(hdnProcessIdFilter.Value.ToString());
                BindGrid(ProcessId);
                //Session["ProcessId"] = "";
                //Session["ProcessIdMaptoCustomers"]="";
            }
        }
        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }
        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            ProcessId = 0;
            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());
            try
            {
                GetPageCount();
                this.BindGrid(ProcessId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RejectedMFFolio.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[1];
                objects[0] = ProcessId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void GetPageCount()
        {
            string upperlimit = string.Empty;
            string lowerlimit = string.Empty;
            int rowCount = 0;
            try
            {
                if (hdnRecordCount.Value != "")
                    rowCount = Convert.ToInt32(hdnRecordCount.Value);
                if (rowCount > 0)
                {

                    int ratio = rowCount / 10;
                    mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    lowerlimit = ((mypager.CurrentPage - 1) * 10).ToString();
                    upperlimit = (mypager.CurrentPage * 10).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnRecordCount.Value;
                    string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;

                    hdnCurrentPage.Value = mypager.CurrentPage.ToString();
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
                FunctionInfo.Add("Method", "RejectedMFFolio.ascx.cs:GetPageCount()");
                object[] objects = new object[3];
                objects[0] = upperlimit;
                objects[1] = rowCount;
                objects[2] = lowerlimit;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void ddlRejectReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlReject = GetRejectReasonDDL();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());

            if (ddlReject != null)
            {
                if (ddlReject.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnRejectReasonFilter.Value = ddlReject.SelectedValue;
                    BindGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnProcessIdFilter.Value = "";
                    hdnRejectReasonFilter.Value = "";
                    BindGrid(ProcessId);
                }
            }
        }
        protected void ddlIsRejected_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlRejected = GetIsRejectedDDL();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());

            if (ddlRejected != null)
            {
                if (ddlRejected.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnIsRejectedFilter.Value = ddlRejected.SelectedValue;
                    BindGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnIsRejectedFilter.Value = "";
                    BindGrid(ProcessId);
                }
            }
        }
        protected void ddlCustomerExists_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCustExists = GetCustExistsDDL();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());

            if (ddlCustExists != null)
            {
                if (ddlCustExists.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnIsCustomerExistingFilter.Value = ddlCustExists.SelectedValue;
                    BindGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnIsCustomerExistingFilter.Value = "";
                    BindGrid(ProcessId);
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            trErrorMessage.Visible = false;

            string newPan = string.Empty;
            string newFolio = string.Empty;
            bool blResult = false;
            rejectedRecordsBo = new RejectedRecordsBo();

            // Gets the footer row directly Cool right! 
            GridViewRow footerRow = gvCAMSProfileReject.FooterRow;

            foreach (GridViewRow dr in gvCAMSProfileReject.Rows)
            {
                if (((TextBox)footerRow.FindControl("txtPanMultiple")).Text.Trim() == "" && ((TextBox)footerRow.FindControl("txtFolioMultiple")).Text.Trim() == "")
                {
                    newPan = ((TextBox)dr.FindControl("txtPan")).Text;
                    newFolio = ((TextBox)dr.FindControl("txtFolio")).Text;
                }
                else
                {
                    newPan = ((TextBox)footerRow.FindControl("txtPanMultiple")).Text;
                    newFolio = ((TextBox)footerRow.FindControl("txtFolioMultiple")).Text;
                }

                CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
                if (checkBox.Checked)
                {
                    int StagingId = Convert.ToInt32(gvCAMSProfileReject.DataKeys[dr.RowIndex].Values["MFFolioStagingId"].ToString());
                    int MainStagingId = Convert.ToInt32(gvCAMSProfileReject.DataKeys[dr.RowIndex].Values["MainStagingId"].ToString());
                    blResult = rejectedRecordsBo.UpdateMFFolioStaging(StagingId,MainStagingId, newPan, newFolio);
                }
            }

            if (blResult)
            {
                // Success Message
            }
            else
            {
                // Failure Message
            }

            // BindGrid
            if (Request.QueryString["processId"] != null)
            {
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            }
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());
            BindGrid(ProcessId);
        }

        private void BindGrid(int ProcessId)
        {
            Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();
            Dictionary<string, string> genDictIsCustomerExisting = new Dictionary<string, string>();

            try
            {
                if (hdnCurrentPage.Value.ToString() != "")
                {
                    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                    hdnCurrentPage.Value = "";
                }

                int Count;

                rejectedRecordsBo = new RejectedRecordsBo();

                if (ProcessId == 0)
                {   // Bind All Processes
                    dsRejectedRecords = rejectedRecordsBo.getMFRejectedFolios(adviserVo.advisorId, ProcessId, mypager.CurrentPage, out Count, hdnSortProcessID.Value, hdnIsRejectedFilter.Value, hdnPANFilter.Value.Trim(), hdnRejectReasonFilter.Value, hdnNameFilter.Value.Trim(), hdnFolioFilter.Value.Trim());
                }
                else
                {   // Bind Grid for the specific Process Id
                    dsRejectedRecords = rejectedRecordsBo.getMFRejectedFolios(adviserVo.advisorId, ProcessId, mypager.CurrentPage, out Count, hdnSortProcessID.Value, hdnIsRejectedFilter.Value, hdnPANFilter.Value.Trim(), hdnRejectReasonFilter.Value, hdnNameFilter.Value.Trim(), hdnFolioFilter.Value.Trim());
                }

                lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
                if (Count > 0)
                    DivPager.Style.Add("display", "visible");

                if (dsRejectedRecords.Tables[0].Rows.Count > 0)
                {   // If Records found, then bind them to grid
                    trProfileMessage.Visible = false;
                    trReprocess.Visible = true;

                    gvCAMSProfileReject.DataSource = dsRejectedRecords.Tables[0];
                    gvCAMSProfileReject.DataBind();

                    if (dsRejectedRecords.Tables[2].Rows.Count > 0)
                    {
                        // Get the Reject Reason Codes Available into Generic Dictionary
                        foreach (DataRow dr in dsRejectedRecords.Tables[2].Rows)
                        {
                            genDictRejectReason.Add(dr["WRR_RejectReasonDescription"].ToString(), dr["WRR_RejectReasonCode"].ToString());
                        }

                        DropDownList ddlRejectReason = GetRejectReasonDDL();
                        if (ddlRejectReason != null)
                        {
                            ddlRejectReason.DataSource = genDictRejectReason;
                            ddlRejectReason.DataTextField = "Key";
                            ddlRejectReason.DataValueField = "Value";
                            ddlRejectReason.DataBind();
                            ddlRejectReason.Items.Insert(0, new ListItem("Select Reject Reason", "Select Reject Reason"));
                        }

                        if (hdnRejectReasonFilter.Value != "")
                        {
                            ddlRejectReason.SelectedValue = hdnRejectReasonFilter.Value.ToString().Trim();
                        }
                    }

                    if (dsRejectedRecords.Tables[3].Rows.Count > 0)
                    {
                        // Get Is Reject Flags Available into Generic Dictionary
                        foreach (DataRow dr in dsRejectedRecords.Tables[3].Rows)
                        {
                            genDictIsRejected.Add(dr["IsRejected"].ToString(), dr["IsRejected"].ToString());
                        }

                        DropDownList ddlReject = GetIsRejectedDDL();
                        if (ddlReject != null)
                        {
                            ddlReject.DataSource = genDictIsRejected;
                            ddlReject.DataTextField = "Key";
                            ddlReject.DataValueField = "Value";
                            ddlReject.DataBind();
                            ddlReject.Items.Insert(0, new ListItem("Select Is Reject Flag", "Select Is Reject Flag"));
                        }

                        if (hdnIsRejectedFilter.Value != "")
                        {
                            ddlReject.SelectedValue = hdnIsRejectedFilter.Value.ToString().Trim();
                        }
                    }

                    //if (dsRejectedRecords.Tables[4].Rows.Count > 0)
                    //{
                    //    // Get Is Customer Exist Flag into Generic Dictionary
                    //    foreach (DataRow dr in dsRejectedRecords.Tables[4].Rows)
                    //    {
                    //        genDictIsCustomerExisting.Add(dr["CustomerExists"].ToString(), dr["CustomerExists"].ToString());
                    //    }

                    //    DropDownList ddlCustExists = GetCustExistsDDL();
                    //    if (ddlCustExists != null)
                    //    {
                    //        ddlCustExists.DataSource = genDictIsCustomerExisting;
                    //        ddlCustExists.DataTextField = "Key";
                    //        ddlCustExists.DataValueField = "Value";
                    //        ddlCustExists.DataBind();
                    //        ddlCustExists.Items.Insert(0, new ListItem("Select a Flag", "Select a Flag"));
                    //    }

                    //    if (hdnIsCustomerExistingFilter.Value != "")
                    //    {
                    //        ddlCustExists.SelectedValue = hdnIsCustomerExistingFilter.Value.ToString().Trim();
                    //    }
                    //}

                    TextBox txtName = GetNameTextBox();
                    TextBox txtFolio = GetFolioTextBox();
                    TextBox txtPan = GetPanTextBox();

                    if (txtName != null && txtFolio != null && txtPan != null)
                    {
                        if (hdnNameFilter.Value != "")
                        {
                            txtName.Text = hdnNameFilter.Value.ToString().Trim();
                        }
                        if (hdnFolioFilter.Value != "")
                        {
                            txtFolio.Text = hdnFolioFilter.Value.ToString().Trim();
                        }
                        if (hdnPANFilter.Value != "")
                        {
                            txtPan.Text = hdnPANFilter.Value.ToString().Trim();
                        }
                    }
                    BindProcessId(dsRejectedRecords.Tables[4]);
                }
                else
                {
                    hdnRecordCount.Value = "0";
                    gvCAMSProfileReject.DataSource = null;
                    gvCAMSProfileReject.DataBind();
                    trProfileMessage.Visible = true;
                    trReprocess.Visible = false;
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

                FunctionInfo.Add("Method", "RejectedCAMSProfile.ascx:BindGrid()");

                object[] objects = new object[1];
                objects[0] = ProcessId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            this.GetPageCount();
        }

        //********** Code implented by bhoopendra for adding a dropdown filter of process id.*************//
        //********** Code Starts *************//
        private void BindProcessId(DataTable dtProcessId)
        {
            Dictionary<string, string> genDictPanNum = new Dictionary<string, string>();
            if (dtProcessId.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtProcessId.Rows)
                {
                    genDictPanNum.Add(dr["ProcessId"].ToString(), dr["ProcessId"].ToString());
                }

                DropDownList ddlProcessId = GetProcessIdDDL();
                if (ddlProcessId != null)
                {
                    ddlProcessId.DataSource = genDictPanNum;
                    ddlProcessId.DataTextField = "Key";
                    ddlProcessId.DataValueField = "Value";
                    ddlProcessId.DataBind();
                    ddlProcessId.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnProcessIdFilter.Value != "")
                {
                    ddlProcessId.SelectedValue = hdnProcessIdFilter.Value.ToString().Trim();
                }
            }
        }

        protected void ddlProcessId_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlProcessId = GetProcessIdDDL();

            if (ddlProcessId != null)
            {
                if (ddlProcessId.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnProcessIdFilter.Value = ddlProcessId.SelectedValue;
                    ProcessId = int.Parse(hdnProcessIdFilter.Value);
                    BindGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnProcessIdFilter.Value = "0";
                    //ProcessId = int.Parse(hdnProcessIdFilter.Value);
                    BindGrid(ProcessId);
                }
            }
        }
        private DropDownList GetProcessIdDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvCAMSProfileReject.HeaderRow.FindControl("ddlProcessId") != null)
            {
                ddl = (DropDownList)gvCAMSProfileReject.HeaderRow.FindControl("ddlProcessId");
            }
            return ddl;
        }

        /*************To delete the selected records ****************/

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            string StagingID = string.Empty;

            foreach (GridViewRow gvr in this.gvCAMSProfileReject.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkBx")).Checked == true)
                {
                    i = i + 1;
                    StagingID += Convert.ToString(gvCAMSProfileReject.DataKeys[gvr.RowIndex].Value) + "~";
                   
                }
            }

            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to delete!');", true);
            }
            else
            {
                rejectedRecordsBo = new RejectedRecordsBo();
                rejectedRecordsBo.DeleteMFRejectedFolios(StagingID);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedMFFolio','login');", true);

            }
        }
        #region previouscode unused
        //{
        //    int i = 0;
        //    foreach (GridViewRow gvr in this.gvCAMSProfileReject.Rows)
        //    {
        //        if (((CheckBox)gvr.FindControl("chkBx")).Checked == true)
        //            i = i + 1;
        //    }

        //    if (i == 0)
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to delete!');", true);
        //    else
        //        CustomerTransactionDelete();
        //}
        #endregion

        private void CustomerTransactionDelete()
        {
            #region unused            
            //foreach (GridViewRow gvr in this.gvCAMSProfileReject.Rows)
            //{
            //    if (((CheckBox)gvr.FindControl("chkBx")).Checked == true)
            //    {
            //        rejectedRecordsBo = new RejectedRecordsBo();
            //        int StagingID = int.Parse(gvCAMSProfileReject.DataKeys[gvr.RowIndex].Values["MFFolioStagingId"].ToString());
            //        rejectedRecordsBo.DeleteMFRejectedFolios(StagingID);
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedMFFolio','login');", true);
            //    }
            //}
            #endregion
        }


        //************** Code End  ***********************//


        private DropDownList GetRejectReasonDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvCAMSProfileReject.HeaderRow.FindControl("ddlRejectReason") != null)
            {
                ddl = (DropDownList)gvCAMSProfileReject.HeaderRow.FindControl("ddlRejectReason");
            }
            return ddl;
        }
        private DropDownList GetIsRejectedDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvCAMSProfileReject.HeaderRow.FindControl("ddlIsRejected") != null)
            {
                ddl = (DropDownList)gvCAMSProfileReject.HeaderRow.FindControl("ddlIsRejected");
            }
            return ddl;
        }
        private DropDownList GetCustExistsDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvCAMSProfileReject.HeaderRow.FindControl("ddlCustomerExists") != null)
            {
                ddl = (DropDownList)gvCAMSProfileReject.HeaderRow.FindControl("ddlCustomerExists");
            }
            return ddl;
        }
     

        private TextBox GetPanTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvCAMSProfileReject.HeaderRow.FindControl("txtPanSearch") != null)
            {
                txt = (TextBox)gvCAMSProfileReject.HeaderRow.FindControl("txtPanSearch");
            }
            return txt;
        }

        private TextBox GetFolioTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvCAMSProfileReject.HeaderRow.FindControl("txtFolioSearch") != null)
            {
                txt = (TextBox)gvCAMSProfileReject.HeaderRow.FindControl("txtFolioSearch");
            }
            return txt;
        }

        private TextBox GetNameTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvCAMSProfileReject.HeaderRow.FindControl("txtNameSearch") != null)
            {
                txt = (TextBox)gvCAMSProfileReject.HeaderRow.FindControl("txtNameSearch");
            }
            return txt;
        }

        protected void btnGridSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetNameTextBox();
            TextBox txtFolio = GetFolioTextBox();
            TextBox txtPan = GetPanTextBox();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());

            if (txtName != null && txtFolio != null && txtPan != null)
            {
                hdnNameFilter.Value = txtName.Text.Trim();
                hdnPANFilter.Value = txtPan.Text.Trim();
                hdnFolioFilter.Value = txtFolio.Text.Trim();

                BindGrid(ProcessId);
            }
        }

        protected void btnReprocess_Click(object sender, EventArgs e)
        {
            bool blResult = false;
            UploadCommonBo uploadsCommonBo = new UploadCommonBo();
            UploadProcessLogVo processlogVo = new UploadProcessLogVo();
            UploadProcessLogVo TempprocesslogVo = new UploadProcessLogVo();
            StandardProfileUploadBo standardProfileUploadBo = new StandardProfileUploadBo();
            StandardFolioUploadBo standardFolioUploadBo = new StandardFolioUploadBo();
            CamsUploadsBo camsUploadsBo = new CamsUploadsBo();
            //int countCustCreated = 0;
            //int countFolioCreated = 0;
            //int countRejectedRecords = 0;
            
           // processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);
            

            // BindGrid
            if (Request.QueryString["processId"] != null)
            {
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            }
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());

            processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);
            if (uploadsCommonBo.ResetRejectedFlagByProcess(ProcessId, 9))
            {

               //Folio Chks in Std Folio Staging 
                  string  packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                  bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserVo.advisorId, ProcessId, configPath);
                    if (camsFolioStagingChkResult)
                    {
                        //Folio Chks in Std Folio Staging 
                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                        bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath,adviserVo.advisorId, ProcessId, configPath);
                        if (camsFolioWerpInsertionResult)
                        {
                            processlogVo.IsInsertionToWERPComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            if (processlogVo.FileTypeId == 2 )
                            {
                                processlogVo.IsInsertionToWERPComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(ProcessId, "CA");
                                processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(ProcessId, "WPMF");
                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(ProcessId, "CA");
                                processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                            }
                            else if (processlogVo.FileTypeId == 4)
                            {
                                  processlogVo.IsInsertionToWERPComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(ProcessId, "KA");
                                processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(ProcessId, "WPMF");
                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(ProcessId, "KA");
                                processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                            }
                            else if (processlogVo.FileTypeId == 16)
                            {
                                processlogVo.IsInsertionToWERPComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(ProcessId, "TN");
                                processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(ProcessId, "WPMF");
                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(ProcessId, "TN");
                                processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            }
                            else if (processlogVo.FileTypeId == 18)
                            {
                                processlogVo.IsInsertionToWERPComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(ProcessId, "DT");
                                processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(ProcessId, "WPMF");
                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(ProcessId, "DT");
                                processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            }
                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (blResult)
                            {
                                bool stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(ProcessId);
                            }
                        }
                    }
                
            }

            if (blResult)
            {
                // Success Message
                trErrorMessage.Visible = true;
                lblError.Text = "Reprocess Done Successfully!";
            }
            else
            {
                // Failure Message
                trErrorMessage.Visible = true;
                lblError.Text = "Reprocess Failure!";
            }

            BindGrid(ProcessId);
        }

        //protected void lnkProfile_Click(object sender, EventArgs e)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedMFFolio','processId=" + processlogVo.ProcessId + "');", true);
        //}

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["processId"] != null)
            {
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            }
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RejectedWERPProfile','processId=" + ProcessId + "');", true);
            
         
        }
        protected void LinkInputRejects_Click(object sender, EventArgs e)
        {


            if (filetypeId == (int)Contants.UploadTypes.CAMSProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CAMSProfileFolioInputRejects','processId=" + ProcessId + "');", true);
            else if (filetypeId == (int)Contants.UploadTypes.KarvyProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('KarvyProfileFolioInputRejects','processId=" + ProcessId + "');", true);
            else if (filetypeId == (int)Contants.UploadTypes.TempletonProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TempletonProfileFolioInputRejects','processId=" + ProcessId + "');", true);
            else if (filetypeId == (int)Contants.UploadTypes.DeutscheProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('DeutscheProfileFolioInputRejects','processId=" + ProcessId + "');", true);
            else if (filetypeId == (int)Contants.UploadTypes.StandardProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('StandardProfileInputRejects','processId=" + ProcessId + "');", true);

        }

        //protected void btnMapToCustomer_Click(object sender, EventArgs e)
        //{
        //    ArrayList Stagingtableid = new ArrayList();
        //    ArrayList ProcessId = new ArrayList();
        //    int i = 0;
        //    //int varTest = 1;
        //    //const int FOLIONUM_INDEX = 2;
        //    HiddenField hdnStagingTableid;
        //    HiddenField hdnProcessID;
        //    //string folionum;
        //    foreach (GridViewRow dr in gvCAMSProfileReject.Rows)
        //    {
        //        CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
        //        if (checkBox.Checked)
        //        {
        //            hdnStagingTableid = (HiddenField)dr.FindControl("hdnBxStagingId");
        //            Stagingtableid.Add(hdnStagingTableid.Value);
        //            hdnProcessID = (HiddenField)dr.FindControl("hdnBxProcessID");
        //            ProcessId.Add(hdnProcessID.Value);
        //            i++;
        //            Session["ProcessId"] = hdnProcessID.Value;
        //        }
        //    }
        //    //use a hashtable to create a unique list
        //    Hashtable ht = new Hashtable();

        //    foreach (string item in ProcessId)
        //    {
        //        //set a key in the hashtable for our arraylist value - leaving the hashtable value empty
        //        ht[item] = null;
        //    }
        //    //now grab the keys from that hashtable into another arraylist
        //    ArrayList distincProcessIds = new ArrayList(ht.Keys);

        //    //int selectedcount = ProcessId.Count;
        //    //string[] processids = new string[selectedcount];
        //    //for(i=0; i<selectedcount; i++)
        //    Session["Stagingtableid"] = Stagingtableid;
        //    Session["distincProcessIds"] = distincProcessIds;
        //    //Session["varTest"] = varTest;

        //    Response.Write("<script type='text/javascript'>detailedresults=window.open('Uploads/MapToCustomers.aspx','mywindow', 'width=700,height=450,scrollbars=yes,location=no');</script>");
        //}
    }
}
