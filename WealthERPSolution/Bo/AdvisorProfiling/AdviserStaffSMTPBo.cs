using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoAdvisorProfiling;
using DaoAdvisorProfiling;

namespace BoAdvisorProfiling
{
    public class AdviserStaffSMTPBo
    {
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

        public AdviserStaffSMTPVo GetSMTPCredentials(int RMId)
        {
            AdviserStaffSMTPDao adviserStaffSMTdao = new AdviserStaffSMTPDao();
            return adviserStaffSMTdao.GetSMTPCredentials(RMId);
        }
    }
}
