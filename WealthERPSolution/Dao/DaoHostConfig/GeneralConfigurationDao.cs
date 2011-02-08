using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoHostConfig;
namespace DaoHostConfig
{
    public class GeneralConfigurationDao
    {
        /// <summary>
        /// Used to insert/update host configuration general details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="generalconfigurationvo"></param>
        /// <returns></returns>
        public bool AddHostGeneralConfiguration(int userId,GeneralConfigurationVo generalconfigurationvo)
        {
            Database db;
            DbCommand cmdAddHostGeneralConfiguration;
            bool bRecordStatus = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdAddHostGeneralConfiguration = db.GetStoredProcCommand("SP_AddHostGeneralConfiguration");
                db.AddInParameter(cmdAddHostGeneralConfiguration, "@HAC_HostLogoPlacement", DbType.String, generalconfigurationvo.HostLogoPlacement);
                db.AddInParameter(cmdAddHostGeneralConfiguration, "@HAC_HostLogo", DbType.String, generalconfigurationvo.HostLogo);
                db.AddInParameter(cmdAddHostGeneralConfiguration, "@HAC_AdviserLogoPlacement", DbType.String, generalconfigurationvo.AdviserLogoPlacement);
                db.AddInParameter(cmdAddHostGeneralConfiguration, "@HAC_DefaultTheme", DbType.String, generalconfigurationvo.DefaultTheme);
                db.AddInParameter(cmdAddHostGeneralConfiguration, "@HAC_ContactPersonName", DbType.String, generalconfigurationvo.ContactPersonName);
                db.AddInParameter(cmdAddHostGeneralConfiguration, "@HAC_TelephoneNumber", DbType.Int64, generalconfigurationvo.ContactPersonTelephoneNumber);
                db.AddInParameter(cmdAddHostGeneralConfiguration, "@HAC_LoginPageContent", DbType.String, generalconfigurationvo.LoginPageContent);
                db.AddInParameter(cmdAddHostGeneralConfiguration, "@HAC_ApplicationName", DbType.String, generalconfigurationvo.ApplicationName);
                db.AddInParameter(cmdAddHostGeneralConfiguration, "@HAC_Email", DbType.String, generalconfigurationvo.Email);
                db.AddInParameter(cmdAddHostGeneralConfiguration, "@U_UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdAddHostGeneralConfiguration) != 0)
                    bRecordStatus = true;
            }
            catch (BaseApplicationException Ex)
            {
                bRecordStatus=false;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GeneralConfigurationDao.cs:AddHostGeneralConfiguration(int customerId, int userId, CustomerProspectVo customerprospectvo)");
                object[] objects = new object[1];                
                objects[0] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                bRecordStatus = false;
                throw exBase;

            }
            return bRecordStatus;
        }

        /// <summary>
        /// Used to get host configuration general details
        ///  </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataSet GetHostGeneralConfiguration(int userId)
        {
            Database db;
            DbCommand cmdGetHostGeneralConfiguration;
            DataSet dsGetHostGeneralConfiguration = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetHostGeneralConfiguration = db.GetStoredProcCommand("SP_GetHostGeneralConfiguration");
                db.AddInParameter(cmdGetHostGeneralConfiguration, "@U_UserId", DbType.Int32, userId);
                dsGetHostGeneralConfiguration = db.ExecuteDataSet(cmdGetHostGeneralConfiguration);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GeneralConfigurationDao.cs:GetHostGeneralConfiguration(int userId)");
                object[] objects = new object[1];
                objects[0] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetHostGeneralConfiguration;


        }
    }
}
