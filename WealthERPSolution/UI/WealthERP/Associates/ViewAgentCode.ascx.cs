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

namespace WealthERP.Associates
{
    public partial class ViewAgentCode : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        AssociatesBo associatesBo = new AssociatesBo();
        UserVo userVo = new UserVo();
        string userType;

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["userVo"];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            BindAgentCodeList();

        }

        private void BindAgentCodeList()
        {
            DataSet dsGetAgentCodeAndType;
            DataTable dtGetAgentCodeAndType;
            dsGetAgentCodeAndType = associatesBo.GetAgentCodeAndType(advisorVo.advisorId);
            dtGetAgentCodeAndType = dsGetAgentCodeAndType.Tables[0];
            if (dtGetAgentCodeAndType == null)
            {
                imgexportButton.Visible = false;
                gvAgentCodeView.DataSource = null;
                gvAgentCodeView.DataBind();
            }
            else
            {
                imgexportButton.Visible = true;
                gvAgentCodeView.DataSource = dtGetAgentCodeAndType;
                gvAgentCodeView.DataBind();
                if (Cache["gvAgentCodeView" + userVo.UserId + userType] == null)
                {
                    Cache.Insert("gvAgentCodeView" + userVo.UserId + userType, dtGetAgentCodeAndType);
                }
                else
                {
                    Cache.Remove("gvAgentCodeView" + userVo.UserId + userType);
                    Cache.Insert("gvAgentCodeView" + userVo.UserId + userType, dtGetAgentCodeAndType);
                }
            }
        }
        protected void gvAgentCodeView_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetAgentCodeAndType = new DataTable();
            dtGetAgentCodeAndType = (DataTable)Cache["gvAgentCodeView" + userVo.UserId + userType];
            gvAgentCodeView.DataSource = dtGetAgentCodeAndType;
            gvAgentCodeView.Visible = true;

            pnlAgentCodeView.Visible = true;
            gvAgentCodeView.Visible = false;
        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvAgentCodeView.ExportSettings.OpenInNewWindow = true;
            gvAgentCodeView.ExportSettings.IgnorePaging = true;
            gvAgentCodeView.ExportSettings.HideStructureColumns = true;
            gvAgentCodeView.ExportSettings.ExportOnlyData = true;
            gvAgentCodeView.ExportSettings.FileName = "AgentCode View";
            gvAgentCodeView.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvAgentCodeView.MasterTableView.ExportToExcel();
        }
        protected void LnkRQ_Click(object sender, EventArgs e)
        {
            GridDataItem gvRow = ((GridDataItem)(((LinkButton)sender).Parent.Parent));

            int selectedRow = gvRow.ItemIndex + 1;
            int AgentId = int.Parse(gvAgentCodeView.MasterTableView.DataKeyValues[selectedRow - 1]["AAC_AdviserAgentId"].ToString());
            string SubBrokerCode = gvAgentCodeView.MasterTableView.DataKeyValues[selectedRow - 1]["AAC_AgentCode"].ToString();
            int Id = int.Parse(gvAgentCodeView.MasterTableView.DataKeyValues[selectedRow - 1]["Id"].ToString());
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddBranchRMAgentAssociation", "loadcontrol('AddBranchRMAgentAssociation','?AgentId=" + AgentId + "&stepCode=" + SubBrokerCode + "&statusCode=" + Id + "');", true);
        }
    }
}