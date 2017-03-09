using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using VoReports;
using BoCommon;
using VoUser;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using VoCustomerPortfolio;

namespace WERP_REQUEST_PROCESSOR
{
    public class RequestCreatorBo
    {
        string daemonCode = Environment.MachineName + "REQUESTPROCESS";
        int userId = 0;
        bool isGroupHead = false;
        CustomerVo custVo = new CustomerVo();
        AdvisorStaffBo adviserStaffBo = new AdvisorStaffBo();
        RMVo customerRMVo = new RMVo();
        CustomerBo customerBo = new CustomerBo();
        string reportTypeAsOn = string.Empty;
        string reportTypeRange = string.Empty;
        MFReportVo mfReport = new MFReportVo();
        int adviserId = 0;
        DateTime dtFrom;
        DateTime dtTo;
        DateTime dtAsOn;
        int requestRecorderId = 0;

        public void CreateRequestFromWERPRequestRecorder()
        {
            DataTable dtRequestRecorderList = GetListOfRequest();
            try
            {
                foreach (DataRow dr in dtRequestRecorderList.Rows)
                {
                    try
                    {
                        string customerIds = dr["WRR_CustomerIds"].ToString();
                        userId = Convert.ToInt32(dr["WRR_CreatedBy"].ToString());
                        //groupCustomerId = Convert.ToInt32(dr["WRR_GroupCustomerId"].ToString());
                        isGroupHead = Convert.ToBoolean(Convert.ToInt16(dr["ASWRR_IsGroupHead"].ToString()));
                        reportTypeAsOn = dr["WRR_ReportTypeAsON"].ToString();
                        reportTypeRange = dr["WRR_ReportTypeRange"].ToString();
                        adviserId = Convert.ToInt32(dr["WRR_AdviserId"].ToString());
                        dtFrom = Convert.ToDateTime(dr["WRR_StartDate"].ToString());
                        dtTo = Convert.ToDateTime(dr["WRR_EndDate"].ToString());
                        dtAsOn = Convert.ToDateTime(dr["WRR_AsOnReportDate"].ToString());
                        requestRecorderId = Convert.ToInt32(dr["WRR_Id"].ToString());
                        ProcessAllCustomerRequest(customerIds);
                    }
                    catch (BaseApplicationException Ex)
                    {
                        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                        NameValueCollection FunctionInfo = new NameValueCollection();
                        FunctionInfo.Add("Method", "RequestCreatorBo:CreateRequestFromWERPRequestRecorder()");
                        object[] objects = new object[2];
                        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                        exBase.AdditionalInformation = FunctionInfo;
                        ExceptionManager.Publish(exBase);
                        throw exBase;
                    }
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
                FunctionInfo.Add("Method", "RequestCreatorBo:CreateRequestFromWERPRequestRecorder()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void ProcessAllCustomerRequest(string customerIds)
        {
            string[] customerids = customerIds.Split('~');
            List<MFReportVo> mfReportVoList = null;
            MFReportEmailVo mfReportEmailVo = new MFReportEmailVo();
            int customerId = 0;
            WERPTaskRequestManagementBo taskRequestManagementBo = new WERPTaskRequestManagementBo();
            int parentrequestId = 0;
            string[] reportTRange = reportTypeRange.Split('~');
            string[] reportTAsON = reportTypeAsOn.Split('~');

            try
            {
                foreach (string arrStr in customerids)
                {
                    mfReportVoList = new List<MFReportVo>();
                    mfReportEmailVo = new MFReportEmailVo();
                    customerId = int.Parse(arrStr);
                    int groupCustomerId = 0;
                    if (isGroupHead)
                        groupCustomerId = customerId;
                    taskRequestManagementBo.CreateTaskRequest(1, userId, out parentrequestId);
                    custVo = customerBo.GetCustomer(customerId);
                    customerRMVo = adviserStaffBo.GetAdvisorStaffDetails(custVo.RmId);
                    foreach (string str in reportTAsON)
                    {
                        mfReportVoList.Add(GetReportInputData(str, customerId, groupCustomerId, ref dtFrom, ref dtTo, ref dtAsOn, "ASON"));
                    }

                    foreach (string str in reportTRange)
                    {
                        mfReportVoList.Add(GetReportInputData(str, customerId, groupCustomerId, ref dtFrom, ref dtTo, ref dtAsOn, "RANGE"));
                    }

                    mfReportEmailVo.AdviserId = adviserId;
                    mfReportEmailVo.CustomerId = custVo.CustomerId;
                    mfReportEmailVo.CustomerEmail = custVo.Email;
                    mfReportEmailVo.RMEmail = customerRMVo.Email;
                    mfReportEmailVo.ReportTypeName = "Mutual Fund Portfolio Statement";

                    taskRequestManagementBo.CreateBulkReportRequest(mfReportVoList, mfReportEmailVo, parentrequestId, 1, userId);

                }

                UpdateTaskRequestRecorder(requestRecorderId, "SUCCESS");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                UpdateTaskRequestRecorder(requestRecorderId, Ex.Message);
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RequestCreatorBo:ProcessAllCustomerRequest(string customerIds)");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private MFReportVo GetReportInputData(string reportName, int customerId, int groupCustomerId, ref DateTime dtFrom, ref DateTime dtTo, ref DateTime dtAsOn, string reportDateType)
        {

            MFReportVo mfReportVo = new MFReportVo();
            string customerPortfolioids = string.Empty;
            mfReport.ReportName = reportName;
            if (reportDateType == "ASON")
            {
                mfReport.FromDate = dtAsOn;
                mfReport.ToDate = dtAsOn;
            }
            else if (reportDateType == "RANGE")
            {
                mfReport.FromDate = dtFrom;
                mfReport.ToDate = dtTo;
            }
            mfReport.SubType = "MF";
            mfReport.AdviserId = adviserId;
            mfReport.CustomerId = customerId;
            mfReport.GroupHeadId = groupCustomerId;
            if (groupCustomerId != 0)
            {
                customerPortfolioids = GetGroupCustomerAllPortfolio(groupCustomerId);
                
            }
            else
            {
                customerPortfolioids = GetCustomerAllPortfolio(customerId);

                if (!string.IsNullOrEmpty(customerPortfolioids.ToString()))
                    customerPortfolioids = customerPortfolioids.Remove(customerPortfolioids.Length - 1, 1);
            }

            mfReport.PortfolioIds = customerPortfolioids;

            return mfReport;
        }

        /// <summary>
        /// This Returns all portfolio Id of all customers of One Group Head Author:Pramod
        /// </summary>
        /// <returns></returns>

        private string GetGroupCustomerAllPortfolio(int groupCustomerId)
        {
            string AllFolioIds = "";
            CustomerBo customerBo = new CustomerBo();
            CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();

            DataTable dt = customerFamilyBo.GetAllCustomerAssociates(groupCustomerId);
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    AllFolioIds = AllFolioIds + GetCustomerAllPortfolio(Convert.ToInt32(dr["C_AssociateCustomerId"]));

                }
            }
            if (!string.IsNullOrEmpty(AllFolioIds.Trim()))
                AllFolioIds = AllFolioIds.Substring(0, AllFolioIds.Length - 1);

            return AllFolioIds;
        }

        /// <summary>
        /// This Returns all portfolio Id of a particular customer. Author:Pramod
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private string GetCustomerAllPortfolio(int customerId)
        {
            string portfolioIDs = "";
            PortfolioBo portfolioBo = new PortfolioBo();
            if (!String.IsNullOrEmpty(customerId.ToString())) //Note : customer Id assigned to txtCustomerId(hidden field) when the user selects customer from customer name suggestion text box
            {
                //int customerId = Convert.ToInt32(txtParentCustomerId.Value);
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); //Get all the portfolios of the selected customer.
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) //One or more folios available for selected customer
                {

                    foreach (CustomerPortfolioVo custPortfolio in customerPortfolioVos)
                    {
                        if (custPortfolio.PortfolioName == "MyPortfolio" || custPortfolio.PortfolioName == "MyPortfolioProspect")
                        {
                            portfolioIDs = portfolioIDs + custPortfolio.PortfolioId;
                            portfolioIDs = portfolioIDs + ",";
                        }
                        //checkbox.Append("<input type='checkbox' checked name='chk--" + custPortfolio.PortfolioId + "' id='chk--" + custPortfolio.PortfolioId + "'>" + custPortfolio.PortfolioName);
                        //checkboxList.Items.Add(new ListItem(custPortfolio.PortfolioName, custPortfolio.PortfolioId.ToString()));
                    }

                }

            }

            return portfolioIDs;
        }




        private DataTable GetListOfRequest()
        {
            DataSet DS = null;
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@TaskId", 1);
                Params[0].DbType = DbType.Int32;
                Params[1] = new SqlParameter("@DaemonCode", daemonCode);
                Params[1].DbType = DbType.String;
                DS = Utils.ExecuteDataSet("SPROC_GetWERPRequestRecorder", Params);
                //DS = Utils.ExecuteDataSet("SPROC_GetWERPRequestList_Test", Params);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RequestCreatorBo:GetListOfRequest()");
                object[] objects = new object[1];
                objects[0] = daemonCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return DS.Tables[0];
        }

        public void UpdateTaskRequestRecorder(int recorderId, string message)
        {

            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@RequestRecordId", recorderId);
                Params[0].DbType = DbType.Int32;
                Params[1] = new SqlParameter("@Message", message);
                Params[1].DbType = DbType.String;
                Utils.ExecuteNonQuery("SPROC_UpdateRequestRecorder", Params);


            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RequestCreatorBo:UpdateTaskRequestRecorder(int recorderId, string message)");
                object[] objects = new object[2];
                objects[0] = recorderId;
                objects[1] = message;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }



        }
    }
}
