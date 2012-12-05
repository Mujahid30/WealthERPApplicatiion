using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoAdvisorProfiling;


namespace DaoAdvisorProfiling
{
    public class AdvisorPreferenceDao
    {

        public AdvisorPreferenceVo GetAdviserPreference(int adviserId)
        {
            Database db;
            DbCommand cmdAdviserPreference;
            DataSet dsAdviserPreference = null;
            DataTable dtAdviserPreference = new DataTable();
            AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdAdviserPreference = db.GetStoredProcCommand("SPROC_GetAdviserPreference");
                db.AddInParameter(cmdAdviserPreference, "@AdviserId", DbType.Int32, adviserId);
                dsAdviserPreference = db.ExecuteDataSet(cmdAdviserPreference);
                dtAdviserPreference = dsAdviserPreference.Tables[0];
                if (dtAdviserPreference.Rows.Count > 0)
                {
                    advisorPreferenceVo.ValtPath = dtAdviserPreference.Rows[0]["AP_ValtPath"].ToString();
                    advisorPreferenceVo.IsLoginWidgetActive =Convert.ToBoolean(dtAdviserPreference.Rows[0]["AP_IsLoginWidgetActive"]);
                    if (!string.IsNullOrEmpty(dtAdviserPreference.Rows[0]["AP_LoginWidgetLogOutPageURL"].ToString()))
                    advisorPreferenceVo.LoginWidgetLogOutPageURL = dtAdviserPreference.Rows[0]["AP_LoginWidgetLogOutPageURL"].ToString();
                    advisorPreferenceVo.BrowserTitleBarName = dtAdviserPreference.Rows[0]["AP_BrowserTitleBarName"].ToString();                 
                    advisorPreferenceVo.BrowserTitleBarIconImageName = dtAdviserPreference.Rows[0]["AP_BrowserTitleBarIconImageName"].ToString();

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
                FunctionInfo.Add("Method", "AdvisorPreferenceDao.cs:GetAdviserPreference()");
                object[] objects = new object[2];
                objects[0] = adviserId;               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return advisorPreferenceVo;
        }

    }
}
