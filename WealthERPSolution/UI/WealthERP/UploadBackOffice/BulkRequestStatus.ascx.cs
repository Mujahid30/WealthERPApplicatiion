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
            BindAssociatePayoutGrid(advisorVo.advisorId, AgentCode, DateTime.Parse(txtFromDate.Text.ToString()), DateTime.Parse(txtToDate.Text.ToString()));
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
    }
}