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

        /// <summary>
        /// This will add the emails to the emailtable ... which will help to send the email to the email IDs added to the table
        /// This method can be used as a global method to access from any page within the project
        /// In this case we just need to provide the emailIds to the table and the email would be sent automatically with the help of the 
        /// emailprocessing daemon
        /// </summary>
        /// <param name="emailVo"></param>
        /// <returns>It will return a bool value which would be true or false</returns>

        public bool AddToEmailQueue(EmailVo emailVo)
        {
           
            bool isComplete = false;
            Database db;
            DbCommand addToEmailQueueCmd;
            Int16 noOfRecord;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                addToEmailQueueCmd = db.GetStoredProcCommand("SP_AddEmailToQueueComplete");

                //inserting data into the queue table and the attachment table

                if (emailVo.To != null)
                    db.AddInParameter(addToEmailQueueCmd, "@To", DbType.String, emailVo.To);
                else
                    db.AddInParameter(addToEmailQueueCmd, "@To", DbType.String, DBNull.Value);

                if (emailVo.Cc != null)
                    db.AddInParameter(addToEmailQueueCmd, "@Cc", DbType.String, emailVo.Cc);
                else
                    db.AddInParameter(addToEmailQueueCmd, "@Cc", DbType.String, DBNull.Value);

                if (emailVo.Bcc != null)
                    db.AddInParameter(addToEmailQueueCmd, "@Bcc", DbType.String, emailVo.Bcc);
                else
                    db.AddInParameter(addToEmailQueueCmd, "@Bcc", DbType.String, DBNull.Value);

                if (emailVo.Subject != null)
                    db.AddInParameter(addToEmailQueueCmd, "@Subject", DbType.String, emailVo.Subject);
                else
                    db.AddInParameter(addToEmailQueueCmd, "@Subject", DbType.String, DBNull.Value);

                if (emailVo.Body != null)
                    db.AddInParameter(addToEmailQueueCmd, "@Body", DbType.String, emailVo.Body);
                else
                    db.AddInParameter(addToEmailQueueCmd, "@Body", DbType.String, DBNull.Value);

                if (emailVo.HasAttachment != null)
                    db.AddInParameter(addToEmailQueueCmd, "@HasAttachment", DbType.Int32, emailVo.HasAttachment);
                else
                    db.AddInParameter(addToEmailQueueCmd, "@HasAttachment", DbType.Int32, DBNull.Value);

                if (emailVo.AttachmentPath != null)
                    db.AddInParameter(addToEmailQueueCmd, "@Path", DbType.String, emailVo.AttachmentPath);
                else
                    db.AddInParameter(addToEmailQueueCmd, "@Path", DbType.String, DBNull.Value);

                //db.AddOutParameter(addToEmailQueueCmd, "@EmailQId", DbType.Int32, 10000);
                noOfRecord=Convert.ToInt16(db.ExecuteNonQuery(addToEmailQueueCmd));

                if (noOfRecord>0)
                    isComplete = true;
                //if (db.ExecuteNonQuery(addToEmailQueueCmd) != 0)
                //{
                //    emailQueueId = int.Parse(db.GetParameterValue(addToEmailQueueCmd, "EmailQId").ToString());
                //}

                //if (emailVo.EmailQueueId != null)
                //    db.AddInParameter(addToEmailQueueCmd, "@EmailQId", DbType.Int32, emailQueueId);
                //else
                //    db.AddInParameter(addToEmailQueueCmd, "@EmailQId", DbType.Int32, DBNull.Value);

                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:AddToEmailQueue(EmailVo emailVo)");
                object[] objects = new object[1];
                objects[0] = emailVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return isComplete;
        }
    }
}