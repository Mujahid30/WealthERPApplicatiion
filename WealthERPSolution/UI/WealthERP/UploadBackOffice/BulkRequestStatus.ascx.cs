using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoOps;
using Telerik.Web.UI;
using VoUser;
using BoCommon;
using BoUploads;
using WealthERP.Base;
using BoAdvisorProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VOAssociates;
using BOAssociates;
using VoOps;
using System.Collections;
using BoCommisionManagement;

namespace WealthERP.UploadBackOffice
{
    public partial class BulkRequestStatus : System.Web.UI.UserControl
    {
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();
        AssociatesVO associatesVo = new AssociatesVO();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        AdvisorVo advisorVo;
        UserVo userVo;
        string userType;
        string AgentCode = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            associatesVo = (AssociatesVO)Session["associatesVo"];
            userVo = (UserVo)Session["userVo"];

        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            BindAssociatePayoutGrid(advisorVo.advisorId, txtAgentCode.Text, DateTime.Parse(txtFromDate.Text.ToString()), DateTime.Parse(txtToDate.Text.ToString()));
        }
        private void BindAssociatePayoutGrid(int adviserId, String agentCode, DateTime fromDate, DateTime toDate)
        {
            DataTable dtAssociatePayout = new DataTable();
            try
            {
                dtAssociatePayout = commisionReceivableBo.GetAssociateCommissionPayout(adviserId, agentCode, fromDate, toDate);
                rdAssociatePayout.DataSource = dtAssociatePayout;
                rdAssociatePayout.DataBind();
                pnlOrderList.Visible = true;
                rdAssociatePayout.Visible = true;
                btnExportFilteredDupData.Visible = true;
                if (Cache["AssociatePayout" + userVo.UserId] != null)
                {

                    Cache.Remove("AssociatePayout" + userVo.UserId);
                }
                Cache.Insert("AssociatePayout" + userVo.UserId, dtAssociatePayout);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BindAssociatePayoutGrid(int adviserId,String agentCode,DateTime fromDate,DateTime toDate)");
                object[] objects = new object[4];
                objects[0] = adviserId;
                objects[1] = agentCode;
                objects[2] = toDate;
                objects[3] = fromDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void rdAssociatePayout_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGIDetails = new DataTable();
            dtGIDetails = (DataTable)Cache["AssociatePayout" + userVo.UserId];
            rdAssociatePayout.Visible = true;
            rdAssociatePayout.DataSource = dtGIDetails;
        }
        protected void rbAssocicatieAll_AssociationSelection(object sender, EventArgs e)
        {
            tdlblAgentCode.Visible = false;
            tdtxtAgentCode.Visible = false;
            if (rdAssociateInd.Checked == true)
            {
                tdlblAgentCode.Visible = true;
                tdtxtAgentCode.Visible = true;
            }
        }
        protected void btnExportFilteredDupData_OnClick(object sender, ImageClickEventArgs e)
        {

            rdAssociatePayout.ExportSettings.OpenInNewWindow = true;
            rdAssociatePayout.ExportSettings.IgnorePaging = true;
            rdAssociatePayout.ExportSettings.HideStructureColumns = true;
            rdAssociatePayout.ExportSettings.ExportOnlyData = true;
      
            rdAssociatePayout.ExportSettings.Excel.Format = GridExcelExportFormat.Html;
            rdAssociatePayout.MasterTableView.ExportToExcel();

        }
        protected void btnExpand_Click(object sender, EventArgs e)
        {
            LinkButton button1 = (LinkButton)sender;
            if (button1.Text == "+")
            {
                foreach (GridDataItem gvr in this.rdAssociatePayout.Items)
                {

                    DataTable dtIssueDetail;
                    int issueId = 0;
                    string PAG_AssetGroupCode, PAISC_AssetInstrumentSubCategoryCode, AAC_AgentCode;
                    LinkButton button = (LinkButton)gvr.FindControl("lbDetails");
                    RadGrid gvChildDetails = (RadGrid)gvr.FindControl("rgNCDIPOMIS");
                    Panel PnlChild = (Panel)gvr.FindControl("pnlchild");
                    issueId = int.Parse(rdAssociatePayout.MasterTableView.DataKeyValues[gvr.ItemIndex]["AIM_IssueId"].ToString());
                    AAC_AgentCode = rdAssociatePayout.MasterTableView.DataKeyValues[gvr.ItemIndex]["AgentCode"].ToString();
                    PAG_AssetGroupCode =rdAssociatePayout.MasterTableView.DataKeyValues[gvr.ItemIndex]["PAG_AssetGroupCode"].ToString();
                    PAISC_AssetInstrumentSubCategoryCode =rdAssociatePayout.MasterTableView.DataKeyValues[gvr.ItemIndex]["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                     dtIssueDetail = commisionReceivableBo.GetAgentProductWiseCommissionDetails(AAC_AgentCode, PAG_AssetGroupCode, PAISC_AssetInstrumentSubCategoryCode, issueId, advisorVo.advisorId, DateTime.Parse(txtFromDate.Text.ToString()), DateTime.Parse(txtToDate.Text.ToString()));
                     gvChildDetails.DataSource = dtIssueDetail;
                     gvChildDetails.DataBind();
                    if (PnlChild.Visible == false)
                    {
                        PnlChild.Visible = true;
                        button.Text = "-";
                    }

                }
                button1.Text = "-";
            }
            else
            {
                foreach (GridDataItem gvr in this.rdAssociatePayout.Items)
                {
                    LinkButton button = (LinkButton)gvr.FindControl("lbDetails");
                    Panel PnlChild = (Panel)gvr.FindControl("pnlchild");
                    if (PnlChild.Visible == true)
                        PnlChild.Visible = false;
                    button.Text = "+";
                }
                button1.Text = "+";
            }

        }
        protected void btnExpandAll_Click(object sender, EventArgs e)
        {

            int count = rdAssociatePayout.MasterTableView.Items.Count;
            DataTable dtIssueDetail;
            int strIssuerId = 0;
            LinkButton buttonlink = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)buttonlink.NamingContainer;
            int issueId = 0;
            string PAG_AssetGroupCode, PAISC_AssetInstrumentSubCategoryCode, AAC_AgentCode;
            issueId = int.Parse(rdAssociatePayout.MasterTableView.DataKeyValues[gdi.ItemIndex]["AIM_IssueId"].ToString());
            AAC_AgentCode = rdAssociatePayout.MasterTableView.DataKeyValues[gdi.ItemIndex]["AgentCode"].ToString();
            PAG_AssetGroupCode = rdAssociatePayout.MasterTableView.DataKeyValues[gdi.ItemIndex]["PAG_AssetGroupCode"].ToString();
            PAISC_AssetInstrumentSubCategoryCode = rdAssociatePayout.MasterTableView.DataKeyValues[gdi.ItemIndex]["PAISC_AssetInstrumentSubCategoryCode"].ToString();
            dtIssueDetail = commisionReceivableBo.GetAgentProductWiseCommissionDetails(AAC_AgentCode, PAG_AssetGroupCode, PAISC_AssetInstrumentSubCategoryCode, issueId, advisorVo.advisorId, DateTime.Parse(txtFromDate.Text.ToString()), DateTime.Parse(txtToDate.Text.ToString()));
            RadGrid gvChildDetails = (RadGrid)gdi.FindControl("rgNCDIPOMIS");
            Panel PnlChild = (Panel)gdi.FindControl("pnlchild");
            if (PnlChild.Visible == false)
            {
                PnlChild.Visible = true;
                buttonlink.Text = "-";
            }
            else if (PnlChild.Visible == true)
            {
                PnlChild.Visible = false;
                buttonlink.Text = "+";
            }
            dtIssueDetail = commisionReceivableBo.GetAgentProductWiseCommissionDetails(AAC_AgentCode, PAG_AssetGroupCode, PAISC_AssetInstrumentSubCategoryCode, issueId, advisorVo.advisorId, DateTime.Parse(txtFromDate.Text.ToString()), DateTime.Parse(txtToDate.Text.ToString()));
            gvChildDetails.DataSource = dtIssueDetail;
            gvChildDetails.DataBind();
        }
    }
}