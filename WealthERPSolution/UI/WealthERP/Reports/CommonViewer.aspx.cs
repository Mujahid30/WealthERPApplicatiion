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

namespace WealthERP.Reports
{
    public partial class CommonViewer : System.Web.UI.Page
    {
        AdvisorVo advisorVo;
        string product, productCategory, fromdate, todate,IsReceivable;
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

        }
        private void getData()
        {
            RptViewer.Reset();
            DataTable dt = getDataTable(product, productCategory, fromdate, todate, amcCode, schemeId, adviserId, issueid);
            DataTable dt2 = GetAssociateData(product, productCategory, fromdate, todate, amcCode, schemeId, adviserId, issueid);
            DataTable dt3 = GetProductApplicationWiseData(product, productCategory, fromdate, todate, amcCode, schemeId, adviserId, issueid);
            DataTable dt4 = GetAssocaiteApplicationWiseData(product, productCategory, fromdate, todate, amcCode, schemeId, adviserId, issueid);
            ReportDataSource rds = new ReportDataSource("BranchDS_SPROC_GetBranchBrokerageReceivable", dt);
            ReportDataSource rds2 = new ReportDataSource("ProductAssociateBrokarageReceived_SPROC_GetAssociateWiseBrokerageReceived", dt2);
            ReportDataSource rds3 = new ReportDataSource("ProductBrokerageApplicationWise_SPROC_GetProductBrokerageApplicationWise", dt3);
            ReportDataSource rds4 = new ReportDataSource("ProductAssociateBrokarageReceived_SPROC_GetBranchAssociateCommissionPayOuts", dt4);
            RptViewer.LocalReport.DataSources.Add(rds);
            RptViewer.LocalReport.DataSources.Add(rds2);
            RptViewer.LocalReport.DataSources.Add(rds3);
            RptViewer.LocalReport.DataSources.Add(rds4);
            RptViewer.LocalReport.ReportPath = Server.MapPath("BrokerageMIS.rdlc");
            ReportParameter[] RP = new ReportParameter[]{
            new ReportParameter("Product",product),
        new ReportParameter("Category",productCategory),
        new ReportParameter("FromDate", fromdate),
        new ReportParameter("ToDate",todate),
        new ReportParameter("AMCCode",amcCode.ToString()), 
        new ReportParameter("IssueId",issueid.ToString()), 
         new ReportParameter("IsReceivable",IsReceivable), 
        new ReportParameter("CommissionType","Upfront"), 
         new ReportParameter("Channel","true"), 
         
        };
            RptViewer.LocalReport.SetParameters(RP);
            RptViewer.LocalReport.Refresh();

        }
        private DataTable getDataTable(string product, string productCategory, string fromdate, string todate, int amcCode, int schemeId, int adviserId, int issueid)
        {
            DataTable dt = new DataTable();
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("SPROC_GetBranchBrokerageReceivable", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Product", SqlDbType.VarChar).Value = product;
                cmd.Parameters.Add("@ProductCategory", SqlDbType.VarChar).Value = productCategory;
                cmd.Parameters.Add("@IssueId", SqlDbType.Int).Value = issueid;
                cmd.Parameters.Add("@IsReceivable", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromdate;
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = todate;
                cmd.Parameters.Add("@Channel", SqlDbType.Bit).Value = 1;
                cmd.Parameters.Add("@commissionType", SqlDbType.VarChar).Value = "upfront";
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
            }
            return dt;
        }
        private DataTable GetAssociateData(string product, string productCategory, string fromdate, string todate, int amcCode, int schemeId, int adviserId, int issueid)
        {
            DataTable dt = new DataTable();
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("SPROC_GetAssociateWiseBrokerageReceived", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Product", SqlDbType.VarChar).Value = product;
                cmd.Parameters.Add("@ProductCategory", SqlDbType.VarChar).Value = productCategory;
                cmd.Parameters.Add("@IssueId", SqlDbType.Int).Value = issueid;
                cmd.Parameters.Add("@IsReceivable", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value =fromdate;
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = todate;
                cmd.Parameters.Add("@Channel", SqlDbType.Bit).Value = 1;
                cmd.Parameters.Add("@commissionType", SqlDbType.VarChar).Value = "upfront";
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
            }
            return dt;
        }
        private DataTable GetProductApplicationWiseData(string product, string productCategory, string fromdate, string todate, int amcCode, int schemeId, int adviserId, int issueid)
        {
            DataTable dt = new DataTable();
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("SPROC_GetProductBrokerageApplicationWise", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Product", SqlDbType.VarChar).Value = product;
                cmd.Parameters.Add("@ProductCategory", SqlDbType.VarChar).Value = productCategory;
                cmd.Parameters.Add("@IssueId", SqlDbType.Int).Value = issueid;
                cmd.Parameters.Add("@IsReceivable", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = "2015/01/01";
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = "2016/12/01";
                cmd.Parameters.Add("@Channel", SqlDbType.Bit).Value = 1;
                cmd.Parameters.Add("@commissionType", SqlDbType.VarChar).Value = "upfront";
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
            }
            return dt;
        }
        private DataTable GetAssocaiteApplicationWiseData(string product, string productCategory, string fromdate, string todate, int amcCode, int schemeId, int adviserId, int issueid)
        {
            DataTable dt = new DataTable();
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("SPROC_GetBranchAssociateCommissionPayOuts", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Product", SqlDbType.VarChar).Value = product;
                cmd.Parameters.Add("@ProductCategory", SqlDbType.VarChar).Value = productCategory;
                cmd.Parameters.Add("@IssueId", SqlDbType.Int).Value = issueid;
                cmd.Parameters.Add("@IsReceivable", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@Channel", SqlDbType.Bit).Value = 1;
                cmd.Parameters.Add("@commissionType", SqlDbType.VarChar).Value = "upfront";
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = "2015/01/01";
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = "2016/12/01";
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
            }
            return dt;
        }
    }
}

