using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using VoEmailSMS;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoCommon
{
    public class EmailSMSDao
    {
        public int AddToSMSQueue(SMSVo smsVo)
        {
            int smsId = 0;
            Database db;
            DbCommand addToSMSQueueCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                addToSMSQueueCmd = db.GetStoredProcCommand("AddOutgoingSmsToQueue");
                db.AddInParameter(addToSMSQueueCmd, "@number", DbType.String,smsVo.Mobile.ToString());
                db.AddInParameter(addToSMSQueueCmd, "@message", DbType.String,smsVo.Message);
                db.AddInParameter(addToSMSQueueCmd, "@sent", DbType.Int16, smsVo.IsSent);
                
                db.AddOutParameter(addToSMSQueueCmd, "@Id", DbType.Int32, 10000);
                if (db.ExecuteNonQuery(addToSMSQueueCmd) != 0)
                {
                    smsId = int.Parse(db.GetParameterValue(addToSMSQueueCmd, "Id").ToString());
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
                FunctionInfo.Add("Method", "AdvisorDao.cs:AddToSMSQueue(SMSVo smsVo)");
                object[] objects = new object[1];
                objects[0] = smsVo;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return smsId;
        }
    }
}
