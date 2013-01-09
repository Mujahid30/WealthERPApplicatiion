using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VoAdvisorProfiling;
using DaoAdvisorProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections;

namespace BoAdvisorProfiling
{
    public class AdviserPreferenceBo
    {
        public AdvisorPreferenceVo GetAdviserPreference(int adviserId)
        {
            AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();
            try
            {                              
                AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
                advisorPreferenceVo = advisorPreferenceDao.GetAdviserPreference(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserPreferenceBo.cs:GetAdviserPreference()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return advisorPreferenceVo;

        }

        public bool AdviserPreferenceSetUp(AdvisorPreferenceVo advisorPreferenceVo, int adviserId, int UserId)
        {
            bool isSuccess = false;
            try
            {
                AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
                isSuccess = advisorPreferenceDao.AdviserPreferenceSetUp(advisorPreferenceVo, adviserId, UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserPreferenceBo.cs:AdviserPreferenceSetUp()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return isSuccess;

        }

    }
}
