using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using VoUser;
using Telerik.Web.UI;
using BoCommon;
using System.Configuration;
using VOAssociates;
using BOAssociates;
using BoUploads;

namespace WealthERP.Associates
{
    public partial class ViewAssociates : System.Web.UI.UserControl
    {
        List<AssociatesVO> associateVoList = null;
        AdvisorVo advisorVo = new AdvisorVo();
        AssociatesBo associatesBo = new AssociatesBo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        string userType;
        string currentUserRole;

        protected void Page_Load(object sender, EventArgs e)
        {
            rmVo = (RMVo)Session[SessionContents.RmVo];
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            currentUserRole = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            if (!IsPostBack)
            {
                if (currentUserRole == "admin" || currentUserRole == "ops")
                {
                    BindBranchDropDown(advisorVo.advisorId);
                    BindAssociatesGrid(advisorVo.advisorId, true, false, false);
                }
                else if (currentUserRole == "bm")
                {
                    BindBranchDropDown(rmVo.RMId);
                    if (ddlBMBranch.Items.Count > 1)
                    {
                        BindAssociatesGrid(rmVo.RMId, false, true, false);
                    }
                    else
                    {
                        BindAssociatesGrid(Convert.ToInt32(ddlBMBranch.SelectedValue.ToString()), false, false, true);
                    }
                }


            }
            
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

        private void BindAssociatesGrid(int id, bool isAdviser, bool isBranchHead, bool isBranchId)
        {
            associateVoList = new List<AssociatesVO>();
            AssociatesVO associateVo = new AssociatesVO();
            DataSet dsViewAssociates;
            DataTable dtViewAssociates = new DataTable(); 
            DataRow dr;
            //dsViewAssociates = associatesBo.GetViewAssociates(advisorVo.advisorId);
            associateVoList = associatesBo.GetViewAssociates(id, isAdviser, isBranchHead, isBranchId, currentUserRole);
            string format = "dd/MM/yyyy hh:mm tt";
            if (associateVoList != null)
            {
                dtViewAssociates.Columns.Add("AA_AdviserAssociateId");
                dtViewAssociates.Columns.Add("AA_RequestDate");
                dtViewAssociates.Columns.Add("AA_ContactPersonName");
                dtViewAssociates.Columns.Add("AAC_AgentCode");
                dtViewAssociates.Columns.Add("XS_Status");
                dtViewAssociates.Columns.Add("AA_StepStatus");
                dtViewAssociates.Columns.Add("WWFSM_StepCode");
                dtViewAssociates.Columns.Add("WWFSM_StepName");
                dtViewAssociates.Columns.Add("AB_BranchName");
                dtViewAssociates.Columns.Add("RM");
                dtViewAssociates.Columns.Add("AA_Email");
                dtViewAssociates.Columns.Add("AA_Mobile");
                for (int i = 0; i < associateVoList.Count; i++)
                {
                    dr = dtViewAssociates.NewRow();
                    associateVo = associateVoList[i];
                    dr["AA_AdviserAssociateId"] = associateVo.AdviserAssociateId;
                    if (associateVo.RequestDate != DateTime.MinValue)
                        dr["AA_RequestDate"] = associateVo.RequestDate.ToString(format);
                    if (associateVo.ContactPersonName != null)
                        dr["AA_ContactPersonName"] = associateVo.ContactPersonName;
                    if (associateVo.AAC_AgentCode!= null)
                        dr["AAC_AgentCode"] = associateVo.AAC_AgentCode;
                    if (associateVo.CurrentStatus != null)
                        dr["AA_StepStatus"] = associateVo.CurrentStatus;
                    if (associateVo.StepName != null)
                        dr["WWFSM_StepCode"] = associateVo.StepCode;
                    if (associateVo.StepName != null)
                        dr["WWFSM_StepName"] = associateVo.StepName;
                    if (associateVo.Status != null)
                        dr["XS_Status"] = associateVo.Status;
                    if (associateVo.BranchName != null)
                        dr["AB_BranchName"] = associateVo.BranchName;
                    if (associateVo.RMNAme != null)
                        dr["RM"] = associateVo.RMNAme;
                    if (associateVo.Email != null)
                        dr["AA_Email"] = associateVo.Email;
                    if (associateVo.Mobile != 0)
                        dr["AA_Mobile"] = associateVo.Mobile;
                    dtViewAssociates.Rows.Add(dr);
                }
                pnlAssociatesView.Visible = true;
                gvViewAssociates.DataSource = dtViewAssociates;
                gvViewAssociates.DataBind();
                gvViewAssociates.Visible = true;
                imgExportAssociates.Visible = true;
                trErrorMessage.Visible = false;
                if (Cache["gvViewAssociates + '" + advisorVo.advisorId + "'"] == null)
                {
                    Cache.Insert("gvViewAssociates + '" + advisorVo.advisorId + "'", dtViewAssociates);
                }
                else
                {
                    Cache.Remove("gvViewAssociates + '" + advisorVo.advisorId + "'");
                    Cache.Insert("gvViewAssociates + '" + advisorVo.advisorId + "'", dtViewAssociates);
                }

            }

            else
            {
                trErrorMessage.Visible = true;
                gvViewAssociates.Visible = false;
                imgExportAssociates.Visible = false;
                pnlAssociatesView.Visible = false;
            }
            //if (dtViewAssociates == null)
            //{
            //    gvViewAssociates.DataSource = null;
            //    gvViewAssociates.DataBind();
            //}
            //else
            //{
            //    gvViewAssociates.DataSource = dtViewAssociates;
            //    gvViewAssociates.DataBind();
            //    imgViewAssociates.Visible = true;
            //    if (Cache["gvViewAssociates" + userVo.UserId + userType] == null)
            //    {
            //        Cache.Insert("gvViewAssociates" + userVo.UserId + userType, dtViewAssociates);
            //    }
            //    else
            //    {
            //        Cache.Remove("gvViewAssociates" + userVo.UserId + userType);
            //        Cache.Insert("gvViewAssociates" + userVo.UserId + userType, dtViewAssociates);
            //    }
            //}
        }

        protected void gvAssociates_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtViewAssociates = new DataTable();
            dtViewAssociates = (DataTable)Cache["gvViewAssociates + '" + advisorVo.advisorId + "'"];
            gvViewAssociates.DataSource = dtViewAssociates;
            gvViewAssociates.Visible = true;

            pnlAssociatesView.Visible = true;
            //gvViewAssociates.Visible = false;
        }

      
        protected void imgExportAssociates_Click(object sender, ImageClickEventArgs e)
        {
            //gvViewAssociates.ExportSettings.OpenInNewWindow = true;
            //gvViewAssociates.ExportSettings.IgnorePaging = true;
            //foreach (GridFilteringItem filter in gvViewAssociates.MasterTableView.GetItems(GridItemType.FilteringItem))
            //{
            //    filter.Visible = false;
            //}
            //gvViewAssociates.MasterTableView.ExportToExcel();

            DataTable dtGvSchemeDetails = new DataTable();
            dtGvSchemeDetails = (DataTable)Cache["gvViewAssociates + '" + advisorVo.advisorId + "'"];
            gvViewAssociates.DataSource = dtGvSchemeDetails;

            gvViewAssociates.ExportSettings.OpenInNewWindow = true;
            gvViewAssociates.ExportSettings.IgnorePaging = true;
            gvViewAssociates.ExportSettings.HideStructureColumns = true;
            gvViewAssociates.ExportSettings.ExportOnlyData = true;
            //gvViewAssociates.ExportSettings.FileName = "Scheme Mapping Details";
            gvViewAssociates.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvViewAssociates.MasterTableView.ExportToExcel();
        }
      
        protected void LnkRQ_Click(object sender, EventArgs e)
        {
            GridDataItem gvRow = ((GridDataItem)(((LinkButton)sender).Parent.Parent));

            int selectedRow = gvRow.ItemIndex + 1;
            int requestId = int.Parse(gvViewAssociates.MasterTableView.DataKeyValues[selectedRow - 1]["AA_AdviserAssociateId"].ToString());
            string statusCode = gvViewAssociates.MasterTableView.DataKeyValues[selectedRow - 1]["AA_StepStatus"].ToString();
            string stepCode = gvViewAssociates.MasterTableView.DataKeyValues[selectedRow - 1]["WWFSM_StepCode"].ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GenerateAssociates", "loadcontrol('AddAssociates','?RequestId=" + requestId + "&stepCode=" + stepCode + "&statusCode=" + statusCode + "');", true);
        }

        protected void ddlBMBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBMBranch.SelectedIndex == 0)
            {
                if (currentUserRole == "admin" || currentUserRole == "ops")
                {
                    if (ddlBMBranch.Items.Count > 1)
                       BindAssociatesGrid(advisorVo.advisorId, true, false, false);
                        //BindAssociatesGrid();
                    else
                        BindAssociatesGrid(Convert.ToInt32(ddlBMBranch.SelectedValue.ToString()), false, false, true);
                        //BindAssociatesGrid();
                }
                else if (currentUserRole == "bm")
                {

                    if (ddlBMBranch.Items.Count > 1)
                    {
                        BindAssociatesGrid(rmVo.RMId, false, true, false);
                        //BindAssociatesGrid();
                    }
                    else
                    {
                        BindAssociatesGrid(Convert.ToInt32(ddlBMBranch.SelectedValue.ToString()), false, false, true);
                        //BindAssociatesGrid();
                    }
                }

            }
            else
            {
                BindAssociatesGrid(Convert.ToInt32(ddlBMBranch.SelectedValue.ToString()), false, false, true);
                //BindAssociatesGrid();

            }
        }
    }
}