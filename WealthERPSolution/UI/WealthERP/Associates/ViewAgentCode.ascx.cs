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
        AssociatesVO associatesVo = new AssociatesVO();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        UserVo userVo = new UserVo();
        string userType;
        string Agentcode;

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["userVo"];
            associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
            associatesVo = (AssociatesVO)Session["associatesVo"];
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
            {
                userType = "associates";

            }
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                if (Request.QueryString["AgentCode"] != null)
                {
                    Agentcode = Request.QueryString["AgentCode"].ToString();
                }
                else
                {
                    Agentcode = string.Empty;
                }

            }
            else
            {
                Agentcode = associateuserheirarchyVo.AgentCode;
            }

            //  Agentcode = associateuserheirarchyVo.AgentCode;

            BindAgentCodeList();

        }

        private void BindAgentCodeList()
        {
            DataSet dsGetAgentCodeAndType;
            DataTable dtGetAgentCodeAndType;
            dsGetAgentCodeAndType = associatesBo.GetAgentCodeAndType(advisorVo.advisorId, userVo.UserType, Agentcode);
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
            ExcelToExport();

        }

        private void ExcelToExport()
        {

            CommonProgrammingBo commonProgrammingBo = new CommonProgrammingBo();
            DataTable dt = new DataTable();
            Dictionary<string, string> dHeaderText = new Dictionary<string, string>();
            dt = (DataTable)Cache["gvAgentCodeView" + userVo.UserId + userType];
            for (int i = 0; i < gvAgentCodeView.MasterTableView.Columns.Count; i++)
            {
                if (gvAgentCodeView.Columns[i].Visible == true)
                    dHeaderText.Add(gvAgentCodeView.Columns[i].UniqueName, gvAgentCodeView.MasterTableView.Columns[i].HeaderText);
            }
            dt = commonProgrammingBo.getHeaderNameNValue(dt, dHeaderText);
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "View Code Master.xls"));
            Response.ContentType = "application/ms-excel";
            string str = string.Empty;
            foreach (DataColumn dtcol in dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in dt.Rows)
            {
                str = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
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