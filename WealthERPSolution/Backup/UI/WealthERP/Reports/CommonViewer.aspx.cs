using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using WealthERP.Base;
using VoUser;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using BoCommisionManagement;

namespace WealthERP.Reports
{
    public partial class CommonViewer : System.Web.UI.Page
    {
        AdvisorVo advisorVo;
        string product, productCategory, fromdate, todate,IsReceivable,CommissionType;
        int amcCode, schemeId, adviserId, issueid;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ReportCode"] != null)
                {
                    if (Request.QueryString["ReportCode"] == "25")
                    {
                        getParameter();
                        getData();
                    }
                }
            }
        }
        private void getParameter()
        {
            product = Request.QueryString["Product"];
            productCategory = Request.QueryString["productCategory"];
            fromdate = Request.QueryString["fromdate"];
            todate = Request.QueryString["todate"];
            amcCode = int.Parse(Request.QueryString["AmcCode"]);
            schemeId = int.Parse(Request.QueryString["SchemeId"]);
            adviserId = int.Parse(Request.QueryString["AdviserId"]);
            issueid = int.Parse(Request.QueryString["issueid"]);
            IsReceivable = Request.QueryString["IsReceivable"];
            CommissionType = Request.QueryString["CommissionType"];

        }
        private void getData()
        {
            RptViewer.Reset();
            CommisionReceivableBo CommisionReceivableBo = new CommisionReceivableBo();
            DataTable dt = CommisionReceivableBo.GetBranchBrokerage(product, productCategory, fromdate, todate, amcCode, schemeId, adviserId, issueid, CommissionType);
            DataTable dt2 = CommisionReceivableBo.GetAssociateBrokerage(product, productCategory, fromdate, todate, amcCode, schemeId, adviserId, issueid,CommissionType);
            DataTable dt3 = CommisionReceivableBo.GetProductApplicationWiseData(product, productCategory, fromdate, todate, amcCode, schemeId, adviserId, issueid,CommissionType);
            DataTable dt4 = CommisionReceivableBo.GetAssocaiteApplicationWiseData(product, productCategory, fromdate, todate, amcCode, schemeId, adviserId, issueid,CommissionType);
            ReportDataSource rds = new ReportDataSource("BranchDS_SPROC_GetBranchBrokerageReceivable", dt);
            ReportDataSource rds2 = new ReportDataSource("ProductAssociateBrokarageReceived_SPROC_GetAssociateWiseBrokerageReceived", dt2);
            ReportDataSource rds3 = new ReportDataSource("ProductBrokerageApplicationWise_SPROC_GetProductBrokerageApplicationWise", dt3);
            ReportDataSource rds4 = new ReportDataSource("ProductAssociateBrokarageReceived_SPROC_GetBranchAssociateCommissionPayOuts", dt4);
            RptViewer.LocalReport.DataSources.Add(rds);
            RptViewer.LocalReport.DataSources.Add(rds2);
            RptViewer.LocalReport.DataSources.Add(rds3);
            RptViewer.LocalReport.DataSources.Add(rds4);
            RptViewer.LocalReport.ReportPath = @"Reports\BrokerageMIS.rdlc";
            ReportParameter[] RP = new ReportParameter[]{
            new ReportParameter("Product",product),
        new ReportParameter("Category",productCategory),
        new ReportParameter("FromDate", fromdate),
        new ReportParameter("ToDate",todate),
        new ReportParameter("AMCCode",amcCode.ToString()), 
        new ReportParameter("IssueId",issueid.ToString()), 
         new ReportParameter("IsReceivable",IsReceivable), 
        new ReportParameter("CommissionType",CommissionType), 
         new ReportParameter("Channel","true"), 
         
        };
            RptViewer.LocalReport.SetParameters(RP);
            RptViewer.LocalReport.Refresh();

        }
       
    }
}

