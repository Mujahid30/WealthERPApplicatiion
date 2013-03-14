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
                    advisorPreferenceVo.IsLoginWidgetEnable =Convert.ToBoolean(dtAdviserPreference.Rows[0]["AP_IsLoginWidgetActive"]);
                    if (!string.IsNullOrEmpty(dtAdviserPreference.Rows[0]["AP_LoginWidgetLogOutPageURL"].ToString()))
                    advisorPreferenceVo.LoginWidgetLogOutPageURL = dtAdviserPreference.Rows[0]["AP_LoginWidgetLogOutPageURL"].ToString();
                    advisorPreferenceVo.BrowserTitleBarName = dtAdviserPreference.Rows[0]["AP_BrowserTitleBarName"].ToString();                 
                    advisorPreferenceVo.BrowserTitleBarIconImageName = dtAdviserPreference.Rows[0]["AP_BrowserTitleBarIconImageName"].ToString();
                    advisorPreferenceVo.WebSiteDomainName = dtAdviserPreference.Rows[0]["AP_WebSiteDomainName"].ToString();
                    advisorPreferenceVo.GridPageSize = Convert.ToInt32(dtAdviserPreference.Rows[0]["AP_GridPageSize"].ToString());
                    if (dtAdviserPreference.Rows[0]["AP_IsBannerEnable"].ToString()=="1")
                      advisorPreferenceVo.IsBannerEnabled =true;
                    advisorPreferenceVo.BannerImageName = dtAdviserPreference.Rows[0]["AP_BannerImageName"].ToString();

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

        public bool AdviserPreferenceSetUp(AdvisorPreferenceVo advisorPreferenceVo,int adviserId, int UserId)
        {
            Database db;
            DbCommand cmdAdviserPreferenceSetUp;           
            DataTable dtAdviserPreference = new DataTable();
           // string LoginWidgetLogOutPageURL=null;
            bool isSuccess = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdAdviserPreferenceSetUp = db.GetStoredProcCommand("SPROC_UpdateAdviserPreference");
                db.AddInParameter(cmdAdviserPreferenceSetUp, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdAdviserPreferenceSetUp, "@IsLoginWidgetEnable", DbType.Int16, advisorPreferenceVo.IsLoginWidgetEnable);
                //if ()
                db.AddInParameter(cmdAdviserPreferenceSetUp, "@LoginWidgetLogOutPageURL", DbType.String, advisorPreferenceVo.LoginWidgetLogOutPageURL);
                //else
                //db.AddInParameter(cmdAdviserPreferenceSetUp, "@LoginWidgetLogOutPageURL", DbType.Int32, DBNull.Value);
                db.AddInParameter(cmdAdviserPreferenceSetUp, "@BrowserTitleBarName", DbType.String, advisorPreferenceVo.BrowserTitleBarName);
                db.AddInParameter(cmdAdviserPreferenceSetUp, "@WebSiteDomainName", DbType.String, advisorPreferenceVo.WebSiteDomainName);
                db.AddInParameter(cmdAdviserPreferenceSetUp, "@GridPageSize", DbType.Int32, advisorPreferenceVo.GridPageSize);
                db.AddInParameter(cmdAdviserPreferenceSetUp, "@UserId", DbType.String, UserId);
                if (db.ExecuteNonQuery(cmdAdviserPreferenceSetUp) != 0)
                    isSuccess = true;
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorPreferenceDao.cs:AdviserPreferenceSetUp()");
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
