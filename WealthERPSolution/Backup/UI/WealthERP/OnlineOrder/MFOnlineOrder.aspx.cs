using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Configuration;
using BoCommon;
using VoReports;
using VoUser;
using WealthERP.Base;

namespace WealthERP.OnlineOrder
{
    public partial class MFOnlineOrder : System.Web.UI.Page
    {
        string path;
        ReportServerConfigVo reportServerConfigVo;
        ReportServerType reportType;
        AdvisorVo advisorVo;
        CustomerVo Customervo;
        DateTime fromDate;
        DateTime todate;
        bool IsDummyAgent;
        string agentCode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            DataSet dsReportServerConfig;
            //path = Server.MapPath(ConfigurationManager.AppSettings["xmlReportServer"].ToString());
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            Customervo = (CustomerVo)Session[SessionContents.CustomerVo];
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ReportCode"] != null)
                {
                    fromDate = Convert.ToDateTime(Request.QueryString["fromDate"]);
                    todate = Convert.ToDateTime(Request.QueryString["toDate"]);
                    IsDummyAgent = Convert.ToBoolean(Request.QueryString["IsDummyAgent"]);
                    agentCode = Request.QueryString["agentCode"];
                    if (agentCode == "")
                        agentCode = "0";
                    SetReportType(Request.QueryString["ReportCode"]);
                }
                dsReportServerConfig = XMLBo.GetReportServerConfiguration();
                reportServerConfigVo = GetReportServerVo(dsReportServerConfig, reportType);
                ShowReport(reportServerConfigVo, reportType);
            }


        }

        private void SetReportType(string rptCode)
        {
            switch (rptCode)
            {
                case "100":
                    reportType = ReportServerType.AssociateBrokerageComissionReport;
                    break;
              
            }
        }

        private void ShowReport(ReportServerConfigVo reportServerConfigVo, ReportServerType reportType)
        {
            try
            {

                IReportServerCredentials irsc = new CustomReportCredentials(reportServerConfigVo.UserName, reportServerConfigVo.Password, reportServerConfigVo.Domain);
                rptViewer.ServerReport.ReportServerCredentials = irsc;
                rptViewer.ProcessingMode = ProcessingMode.Remote; // ProcessingMode will be Either Remote or Local
                rptViewer.ServerReport.ReportServerUrl = new Uri(reportServerConfigVo.ReportServerURL); //Set the ReportServer Url

                switch (reportType)
                {
                    case ReportServerType.AssociateBrokerageComissionReport:
                        rptViewer.ServerReport.ReportPath = reportServerConfigVo.ReportPath; //Passing the Report Path
                        rptViewer.ServerReport.DisplayName = "Associate Consolidate Payout " + DateTime.Now.ToShortDateString();
                        break;
                   
                }


                //Creating an ArrayList for combine the Parameters which will be passed into SSRS Report
                ArrayList reportParam = new ArrayList();
                reportParam = ReportDefaultPatam();

                ReportParameter[] param = new ReportParameter[reportParam.Count];
                for (int k = 0; k < reportParam.Count; k++)
                {
                    param[k] = (ReportParameter)reportParam[k];
                }


                //pass parmeters to report
                rptViewer.ServerReport.SetParameters(CreateReportParameter(Request.QueryString["ReportCode"])); //Set Report Parameters
                rptViewer.ShowParameterPrompts = false;
                rptViewer.ServerReport.Refresh();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ArrayList ReportDefaultPatam()
        {
            ArrayList arrLstDefaultParam = new ArrayList();
            //arrLstDefaultParam.Add(CreateReportParameter("AdviserId", advisorVo.advisorId.ToString()));
            //arrLstDefaultParam.Add(CreateReportParameter("ReportSubTitle", "Sub Title of Report"));
            return arrLstDefaultParam;
        }

        private ReportServerConfigVo GetReportServerVo(DataSet dsReportServerDetails, ReportServerType reportType)
        {
            DataTable dtServerConfig = dsReportServerDetails.Tables[0];
            DataTable dtReportDetails = dsReportServerDetails.Tables[1];
            reportServerConfigVo = new ReportServerConfigVo();
            foreach (DataRow dr in dtServerConfig.Rows)
            {
                switch (dr["ServerConfigTypeCode"].ToString())
                {
                    case "RSURL":
                        reportServerConfigVo.ReportServerURL = dr["ServerConfigTypeValue"].ToString();
                        break;
                    case "RSUN":
                        reportServerConfigVo.UserName = dr["ServerConfigTypeValue"].ToString();
                        break;
                    case "RSPWD":
                        reportServerConfigVo.Password = dr["ServerConfigTypeValue"].ToString();
                        break;
                    case "RSDOM":
                        reportServerConfigVo.Domain = dr["ServerConfigTypeValue"].ToString();
                        break;
                }


            }

            DataView dvReportDetails = new DataView(dtReportDetails, "ReportTypeCode=" + reportType.ToString("d").ToString(), "ReportTypeCode", DataViewRowState.CurrentRows);

            reportServerConfigVo.ReportPath = dvReportDetails.ToTable().Rows[0]["ReportPath"].ToString();


            return reportServerConfigVo;
        }
        private List<ReportParameter> CreateReportParameter(String rptCode)
        {
            List<ReportParameter> myParams = new List<ReportParameter>();
            if (rptCode == "100")
            {

                ReportParameter p = new ReportParameter("AdviserId");
                p.Values.Add(advisorVo.advisorId.ToString());
                myParams.Add(p);
                p = new ReportParameter("AgentCode");
                p.Values.Add(agentCode);
                myParams.Add(p);
                p = new ReportParameter("FromDate");
                p.Values.Add(fromDate.ToString());
                myParams.Add(p);
                p = new ReportParameter("ToDate");
                p.Values.Add(todate.ToString());
                myParams.Add(p);
                p = new ReportParameter("IsDummyAgent");
                p.Values.Add(IsDummyAgent.ToString());
                myParams.Add(p);
            }
           
            return myParams;

        }

        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;

            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }

            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user,
             out string password, out string authority)
            {
                authCookie = null;
                user = password = authority = null;
                return false;
            }
        }
    }
}
