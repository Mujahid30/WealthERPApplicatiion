using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoAdvisorProfiling;
using System.Data.SqlClient;

namespace DaoAdvisorProfiling
{
    public class AdviserStaffSMTPDao
    {

        public bool InsertAdviserStaffSMTP(AdviserStaffSMTPVo adviserStaffSMTPvo)
        {
            bool result = false;

            Database db;
            DbCommand cmdInsert;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsert = db.GetStoredProcCommand("SP_InsertAdviserStaffSMTP");

                db.AddInParameter(cmdInsert, "@AR_RMId", DbType.Int32, adviserStaffSMTPvo.RMId);
                db.AddInParameter(cmdInsert, "@ASS_Email", DbType.String, adviserStaffSMTPvo.Email);
                db.AddInParameter(cmdInsert, "@ASS_HostServer", DbType.String, adviserStaffSMTPvo.HostServer);
                db.AddInParameter(cmdInsert, "@ASS_IsAuthenticationRequired", DbType.Int16, adviserStaffSMTPvo.IsAuthenticationRequired);
                if (adviserStaffSMTPvo.ModifiedBy > 0)
                    db.AddInParameter(cmdInsert, "@CurrentUser", DbType.Int32, adviserStaffSMTPvo.ModifiedBy);
                else if (adviserStaffSMTPvo.CreatedBy > 0)
                    db.AddInParameter(cmdInsert, "@CurrentUser", DbType.Int32, adviserStaffSMTPvo.CreatedBy);
                //db.AddInParameter(cmdInsert, "@ASS_ModifiedOn", DbType.DateTime, adviserStaffSMTPvo.ModifiedOn);
                db.AddInParameter(cmdInsert, "@ASS_Password", DbType.String, adviserStaffSMTPvo.Password);
                db.AddInParameter(cmdInsert, "@ASS_Port", DbType.String, adviserStaffSMTPvo.Port);

                db.ExecuteNonQuery(cmdInsert);

                result = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserStaffSMTPDao.cs:InsertAdviserStaffSMTP()");
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
            Database db;
            DbCommand cmd;
            AdviserStaffSMTPVo adviserStaffSMTPVo = new AdviserStaffSMTPVo();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_AdviserStaffGetSMTPSettings");
                db.AddInParameter(cmd, "@RMId", DbType.Int32, RMId);

                SqlDataReader sdr = (SqlDataReader)db.ExecuteReader(cmd);
                while (sdr.Read())
                {
                    adviserStaffSMTPVo.HostServer = sdr["ASS_HostServer"].ToString();
                    adviserStaffSMTPVo.Email = sdr["ASS_Email"].ToString();
                    adviserStaffSMTPVo.Password = sdr["ASS_Password"].ToString();
                    adviserStaffSMTPVo.IsAuthenticationRequired = Convert.ToInt16(sdr["ASS_IsAuthenticationRequired"]);
                    adviserStaffSMTPVo.Port = sdr["ASS_Port"].ToString();
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

                FunctionInfo.Add("Method", "AdviserStaffSMTPDao.cs:GetSMTPCredentials()");

                object[] objects = new object[1];
                objects[0] = RMId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return adviserStaffSMTPVo;


        }

    }
}
