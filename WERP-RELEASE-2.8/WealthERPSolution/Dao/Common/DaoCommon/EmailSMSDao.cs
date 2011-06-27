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
                db.AddInParameter(addToSMSQueueCmd, "@number", DbType.String, smsVo.Mobile.ToString());
                db.AddInParameter(addToSMSQueueCmd, "@message", DbType.String, smsVo.Message);
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
        /// <summary>
        /// Method to Add Email Log Infor to the Table
        /// </summary>
        /// <param name="emailVo">EmailVo Object with the Details of the Email Sent</param>
        /// <returns name="EmailLogId">The Unique Id created for the email Log Entry</returns>
        public int AddToEmailLog(EmailVo emailVo)
        {
            int emailLogId = 0;
            Database db;
            DbCommand addToEmailLogCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                addToEmailLogCmd = db.GetStoredProcCommand("SP_AddOutGoingEmailLog");
                if (emailVo.EmailQueueId != null)
                    db.AddInParameter(addToEmailLogCmd, "@AEML_EmailQueueId", DbType.Int32, emailVo.EmailQueueId);
                else
                    db.AddInParameter(addToEmailLogCmd, "@AEML_EmailQueueId", DbType.Int32, DBNull.Value);

                if (emailVo.AdviserId != null)
                    db.AddInParameter(addToEmailLogCmd, "@A_AdviserId", DbType.Int32, emailVo.AdviserId);
                else
                    db.AddInParameter(addToEmailLogCmd, "@A_AdviserId", DbType.Int32, DBNull.Value);

                if (emailVo.CustomerId != null)
                    db.AddInParameter(addToEmailLogCmd, "@C_CustomerId", DbType.Int32, emailVo.CustomerId);
                else
                    db.AddInParameter(addToEmailLogCmd, "@C_CustomerId", DbType.Int32, DBNull.Value);

                if (emailVo.EmailType != null && emailVo.EmailType != "")
                    db.AddInParameter(addToEmailLogCmd, "@AEML_EmailType", DbType.String, emailVo.EmailType);
                else
                    db.AddInParameter(addToEmailLogCmd, "@AEML_EmailType", DbType.String, DBNull.Value);
                if (emailVo.HasAttachment != null)
                    db.AddInParameter(addToEmailLogCmd, "@AEML_Attachment", DbType.Int16, emailVo.HasAttachment);
                else
                    db.AddInParameter(addToEmailLogCmd, "@AEML_Attachment", DbType.Int16, DBNull.Value);
                if (emailVo.AttachmentPath != null && emailVo.AttachmentPath != "")
                    db.AddInParameter(addToEmailLogCmd, "@AEML_AttachmentPath", DbType.String, emailVo.AttachmentPath);
                else
                    db.AddInParameter(addToEmailLogCmd, "@AEML_AttachmentPath", DbType.String, DBNull.Value);
                if (emailVo.SentDate != DateTime.MinValue && emailVo.SentDate != null)
                    db.AddInParameter(addToEmailLogCmd, "@AEML_DateSent", DbType.DateTime, emailVo.SentDate);
                else
                    db.AddInParameter(addToEmailLogCmd, "@AEML_DateSent", DbType.DateTime, DBNull.Value);
                if (emailVo.FileName != null && emailVo.FileName != "")
                    db.AddInParameter(addToEmailLogCmd, "@AEML_FileName", DbType.String, emailVo.FileName);
                else
                    db.AddInParameter(addToEmailLogCmd, "@AEML_FileName", DbType.String, DBNull.Value);
                if (emailVo.ReportCode != null)
                    db.AddInParameter(addToEmailLogCmd, "@AEML_ReportCode", DbType.Int16, emailVo.ReportCode);
                else
                    db.AddInParameter(addToEmailLogCmd, "@AEML_ReportCode", DbType.Int16, DBNull.Value);
                if (emailVo.Status != null)
                    db.AddInParameter(addToEmailLogCmd, "@AEML_Status", DbType.Int16, emailVo.Status);
                else
                    db.AddInParameter(addToEmailLogCmd, "@AEML_Status", DbType.Int16, DBNull.Value);

                db.AddOutParameter(addToEmailLogCmd, "@Id", DbType.Int32, 10000);
                if (db.ExecuteNonQuery(addToEmailLogCmd) != 0)
                {
                    emailLogId = int.Parse(db.GetParameterValue(addToEmailLogCmd, "Id").ToString());
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
                FunctionInfo.Add("Method", "AdvisorDao.cs:AddToEmailLog(EmailVo emailVo)");
                object[] objects = new object[1];
                objects[0] = emailVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return emailLogId;
        }
    }
}
