using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoAdvisorProfiling;
using DaoAdvisorProfiling;
using VoUser;

namespace BoAdvisorProfiling
{
    public class AdviserStaffSMTPBo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adviserStaffSMTPvo"></param>
        /// <returns></returns>
        public bool InsertAdviserStaffSMTP(AdviserStaffSMTPVo adviserStaffSMTPvo)
        {
            bool result = false;



            try
            {

                AdviserStaffSMTPDao adviserStaffSMTdao = new AdviserStaffSMTPDao();
                result = adviserStaffSMTdao.InsertAdviserStaffSMTP(adviserStaffSMTPvo);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserStaffSMSTPBo.cs:InsertAdviserStaffSMTP()");
                object[] objects = new object[1];
                objects[0] = adviserStaffSMTPvo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RMId"></param>
        /// <returns></returns>
        public AdviserStaffSMTPVo GetSMTPCredentials(int RMId)
        {
            AdviserStaffSMTPDao adviserStaffSMTdao = new AdviserStaffSMTPDao();
            return adviserStaffSMTdao.GetSMTPCredentials(RMId);
        }

        public DataSet GetSMSProvider()
        {
            DataSet dsSMSProvider;
            try
            {
                AdviserStaffSMTPDao adviserStaffSMTdao = new AdviserStaffSMTPDao();
                dsSMSProvider = adviserStaffSMTdao.GetSMSProvider();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsSMSProvider;
        }
        public bool CreateSMSProviderDetails(AdviserStaffSMTPVo adviserStaffSMTPvo)
        {
            bool bResult = false;
            try
            {
                AdviserStaffSMTPDao adviserStaffSMTdao = new AdviserStaffSMTPDao();
                bResult = adviserStaffSMTdao.CreateSMSProviderDetails(adviserStaffSMTPvo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }




        public DataSet GetAPIProvider()
        {
            DataSet dsAPIProvider;
            try
            {
                AdviserStaffSMTPDao adviserStaffSMTdao = new AdviserStaffSMTPDao();
                dsAPIProvider = adviserStaffSMTdao.GetAPIProvider();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsAPIProvider;
        }

        public bool BSEMandateCreate(string ClientCode, string ClientName, double amount, string bankName, string bankBranch, int userId, out string message, out int mandateId)
        {
            bool bResult = false;
            AdviserStaffSMTPVo adviserStaffSMTPvo = new AdviserStaffSMTPVo();
            RMVo advrm = new RMVo();
            string[] bsePasswordResponse;
            AdvisorStaffBo adviserstaffbo = new AdvisorStaffBo();
            advrm = adviserstaffbo.GetAdvisorStaff(userId);
            UCC.MFUploadServiceClient c = new BoAdvisorProfiling.UCC.MFUploadServiceClient();
            mandateId = 0;
            try
            {
                adviserStaffSMTPvo = GetSMTPCredentials(advrm.RMId);
                AdviserStaffSMTPDao adviserStaffSMTdao = new AdviserStaffSMTPDao();
                
                string password = c.getPassword(adviserStaffSMTPvo.ApiUserName, adviserStaffSMTPvo.ApiMemberId, adviserStaffSMTPvo.Apipassword, "E234586789D12");
                string[] bsePassArray = password.Split('|');
                if (bsePassArray[0].ToString() == "100")
                {
                    string REPSONSE = c.MFAPI("06", adviserStaffSMTPvo.ApiUserName, bsePassArray[1], ClientCode + "|" + ClientName + "|" + amount + "|" + bankName + "|" + bankName + "|" + 'X');
                    bsePasswordResponse = REPSONSE.Split('|');

                    if (bsePasswordResponse[0].ToString() == "100")
                    {
                        
                        bResult = true;
                        message = bsePasswordResponse[1].ToString() + " " + "Mandate ID :" + bsePasswordResponse[2].ToString();
                        mandateId = Convert.ToInt32(bsePasswordResponse[2].ToString());
                    }
                    else
                        message = bsePasswordResponse[1].ToString();

                }

                else
                {
                    message = bsePassArray[1].ToString();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            finally
            {
                c.Close();
            }
            return bResult;
        }






        public bool CreateAPIProviderDetails(AdviserStaffSMTPVo adviserStaffSMTPvo, out string message)
        {
            bool bResult = false;

            string[] bsePasswordResponse;
            message = string.Empty;
            try
            {
                AdviserStaffSMTPDao adviserStaffSMTdao = new AdviserStaffSMTPDao();

                //bResult = adviserStaffSMTdao.CreateAPIProviderDetails(adviserStaffSMTPvo);

                UCC.MFUploadServiceClient c = new BoAdvisorProfiling.UCC.MFUploadServiceClient();

                string password = c.getPassword(adviserStaffSMTPvo.ApiUserName, adviserStaffSMTPvo.ApiMemberId, adviserStaffSMTPvo.Apipassword, "E234586789D12");
                string[] bsePassArray = password.Split('|');

                if (bsePassArray[0].ToString() == "100")
                {
                    string REPSONSE = c.MFAPI("04", adviserStaffSMTPvo.ApiUserName, bsePassArray[1], adviserStaffSMTPvo.Apipassword + "|" + adviserStaffSMTPvo.NewPassword + "|" + adviserStaffSMTPvo.ConfirmPassword);
                    bsePasswordResponse = REPSONSE.Split('|');

                    if (bsePasswordResponse[0].ToString() == "100")
                    {
                        bResult = adviserStaffSMTdao.CreateAPIProviderDetails(adviserStaffSMTPvo);
                        bResult = true;
                        message = bsePasswordResponse[1].ToString();
                    }
                    else
                        message = bsePasswordResponse[1].ToString();

                }

                else
                {
                    message = bsePassArray[1].ToString();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
    }


}
