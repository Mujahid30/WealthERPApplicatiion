using System;
using System.Collections.Generic;
using System.Linq;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoAdvisorProfiling;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;
using BoCalculator;
using Telerik.Web.UI;
using BoUploads;


namespace WealthERP.Customer
{
    public partial class CustomerISARequestList : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        List<LiabilitiesVo> ISAQueueListVo = null;
        LiabilitiesBo liabilitiesBo = new LiabilitiesBo();
        CustomerVo customerVo = null;
        Calculator calculator = new Calculator();
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        RMVo rmVo = new RMVo();
        string currentUserRole = string.Empty;
        string userrole = "adviser";


        protected void Page_Load(object sender, EventArgs e)
        {


            rmVo = (RMVo)Session[SessionContents.RmVo];
            SessionBo.CheckSession();
            userBo = new UserBo();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            currentUserRole = Session[SessionContents.CurrentUserRole].ToString().ToLower();
            if (!IsPostBack)
            {
                if (currentUserRole == "admin" || currentUserRole == "ops")
                {
                    BindBranchDropDown(advisorVo.advisorId);
                    BindGridview(advisorVo.advisorId, true, false, false);
                }
                else if (currentUserRole == "bm")
                {
                    BindBranchDropDown(rmVo.RMId);
                    if (ddlBMBranch.Items.Count > 1)
                    {
                        BindGridview(rmVo.RMId, false, true, false);
                    }
                    else
                    {
                        BindGridview(Convert.ToInt32(ddlBMBranch.SelectedValue.ToString()), false, false, true);
                    }
                }


            }
        }
        protected void gvISArequest_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            gvISArequest.Visible = true;
            DataTable dt = new DataTable();

            btnExportFilteredData.Visible = true;
            dt = (DataTable)Cache["IsaRequestDetails  + '" + advisorVo.advisorId + "'"];
            gvISArequest.DataSource = dt;
        }
        protected void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvISArequest.ExportSettings.OpenInNewWindow = true;
            gvISArequest.ExportSettings.IgnorePaging = true;
            gvISArequest.ExportSettings.HideStructureColumns = true;
            gvISArequest.ExportSettings.ExportOnlyData = true;
            gvISArequest.ExportSettings.FileName = "ISA status Details";
            gvISArequest.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvISArequest.MasterTableView.ExportToExcel();
        }
        private void BindBranchDropDown(int id)
        {
            try
            {


                UploadCommonBo uploadCommonBo = new UploadCommonBo();
                DataSet ds = uploadCommonBo.GetAdviserBranchList(id, currentUserRole);

                ddlBMBranch.DataSource = ds.Tables[0];
                ddlBMBranch.DataTextField = "AB_BranchName";
                ddlBMBranch.DataValueField = "AB_BranchId";
                ddlBMBranch.DataBind();
                //ddlBMBranch.Items.Insert(0, new ListItem("Select a Branch", "Select a Branch"));
                if (ds.Tables[0].Rows.Count > 1)
                {
                    ddlBMBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", id.ToString()));
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

                FunctionInfo.Add("Method", "BMDashBoard.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }



        protected void ddlBMBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBMBranch.SelectedIndex == 0)
            {
                if (currentUserRole == "admin" || currentUserRole == "ops")
                {
                    if (ddlBMBranch.Items.Count > 1)
                        BindGridview(advisorVo.advisorId, true, false, false);
                    else
                        BindGridview(Convert.ToInt32(ddlBMBranch.SelectedValue.ToString()), false, false, true);
                }
                else if (currentUserRole == "bm")
                {

                    if (ddlBMBranch.Items.Count > 1)
                    {
                        BindGridview(rmVo.RMId, false, true, false);
                    }
                    else
                    {
                        BindGridview(Convert.ToInt32(ddlBMBranch.SelectedValue.ToString()), false, false, true);
                    }
                }

            }
            else
            {
                BindGridview(Convert.ToInt32(ddlBMBranch.SelectedValue.ToString()), false, false, true);

            }
        }
        protected void BindGridview(int id, bool isAdviser, bool isBranchHead, bool isBranchId)
        {
            ISAQueueListVo = new List<LiabilitiesVo>();
            LiabilitiesVo liabilityVo = new LiabilitiesVo();
            ISAQueueListVo = liabilitiesBo.GetISAQueueList(id, isAdviser, isBranchHead, isBranchId, currentUserRole);
            DataTable dt = new DataTable();
            DataRow dr;
            Double loanOutStanding = 0;
            DateTime nextInsDate = new DateTime();
            string format = "dd/MM/yyyy hh:mm tt";
            if (ISAQueueListVo != null)
            {
                btnExportFilteredData.Visible = true;
                trErrorMessage.Visible = false;
                dt.Columns.Add("AISAQ_RequestQueueid");
                dt.Columns.Add("AISAQ_date");
                dt.Columns.Add("AISAQ_Status");
                dt.Columns.Add("AISAQ_Priority");
                dt.Columns.Add("CustomerName");
                dt.Columns.Add("WWFSM_StepCode");
                dt.Columns.Add("AISAQD_Status");
                dt.Columns.Add("BranchName");
                dt.Columns.Add("StepName");
                dt.Columns.Add("StatusCode");
                dt.Columns.Add("CISAA_AccountNumber");
                dt.Columns.Add("AISAQ_ProcessedDate");
                

                for (int i = 0; i < ISAQueueListVo.Count; i++)
                {
                    dr = dt.NewRow();
                    liabilityVo = ISAQueueListVo[i];
                    dr[0] = liabilityVo.ISARequestId;
                    if (liabilityVo.RequestDate != DateTime.MinValue)
                        dr[1] = liabilityVo.RequestDate.ToString(format);
                    if (liabilityVo.CurrentStatus != null)
                        dr[2] = liabilityVo.CurrentStatus;
                    if (liabilityVo.Priority != null)
                        dr[3] = liabilityVo.Priority;
                    if (liabilityVo.CustomerName != null)
                        dr[4] = liabilityVo.CustomerName;
                    if (liabilityVo.StepCode != null)
                        dr[5] = liabilityVo.StepCode;
                    if (liabilityVo.Status != null)
                        dr[6] = liabilityVo.Status;
                    if (liabilityVo.BranchName != null)
                        dr[7] = liabilityVo.BranchName;
                    if (liabilityVo.StepName != null)
                        dr[8] = liabilityVo.StepName;
                    if (liabilityVo.StatusCode != null)
                        dr[9] = liabilityVo.StatusCode;
                    if (liabilityVo.IsaNo != null)
                        dr[10] = liabilityVo.IsaNo;
                    if (liabilityVo.ProcessedDate != DateTime.MinValue)
                        dr[11] = liabilityVo.ProcessedDate.ToString(format);
                    dt.Rows.Add(dr);
                }
                gvISArequest.DataSource = dt;
                gvISArequest.DataBind();
                gvISArequest.Visible = true;

                if (Cache["IsaRequestDetails + '" + advisorVo.advisorId + "'"] == null)
                {
                    Cache.Insert("IsaRequestDetails  + '" + advisorVo.advisorId + "'", dt);
                }
                else
                {
                    Cache.Remove("IsaRequestDetails  + '" + advisorVo.advisorId + "'");
                    Cache.Insert("IsaRequestDetails  + '" + advisorVo.advisorId + "'", dt);
                }

            }

            else
            {
                trErrorMessage.Visible = true;
                gvISArequest.Visible = false;
                btnExportFilteredData.Visible = false;
            }
        }

        protected void LnkRQ_Click(object sender, EventArgs e)
        {
            GridDataItem gvRow = ((GridDataItem)(((LinkButton)sender).Parent.Parent));

            int selectedRow = gvRow.ItemIndex + 1;
            int requestId = int.Parse(gvISArequest.MasterTableView.DataKeyValues[selectedRow - 1]["AISAQ_RequestQueueid"].ToString());
            string statusCode = gvISArequest.MasterTableView.DataKeyValues[selectedRow - 1]["StatusCode"].ToString();
            string stepCode = gvISArequest.MasterTableView.DataKeyValues[selectedRow - 1]["WWFSM_StepCode"].ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GenerateISA", "loadcontrol('CustomerISARequest','?RequestId=" + requestId + "&stepCode=" + stepCode + "&statusCode=" + statusCode + "');", true);
        }
        //protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        //{
        //    RadComboBox ddlAction = (RadComboBox)sender;
        //    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "Page_ClientValidate();Loading(true);", true); 
        //    GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
        //    int selectedRow = gvr.ItemIndex + 1;
        //    int requestId = int.Parse(gvISArequest.MasterTableView.DataKeyValues[selectedRow - 1]["AISAQ_RequestQueueid"].ToString());
        //    string statusCode = gvISArequest.MasterTableView.DataKeyValues[selectedRow - 1]["StatusCode"].ToString();
        //    string stepCode = gvISArequest.MasterTableView.DataKeyValues[selectedRow - 1]["WWFSM_StepCode"].ToString();
        //    if (ddlAction.SelectedValue == "View")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GenerateISA", "loadcontrol('CustomerISARequest','?RequestId=" + requestId + "&stepCode=" + stepCode + "&statusCode=" + statusCode + "');", true);
        //    }

        //}

    }
}
