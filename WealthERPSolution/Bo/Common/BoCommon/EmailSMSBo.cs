using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoEmailSMS;
using DaoCommon;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoCommon
{
    public class EmailSMSBo
    {
        public int AddToSMSQueue(SMSVo smsVo)
        {
            EmailSMSDao emailSMSDao = new EmailSMSDao();
            int smsId = 0;
            try
            {
                smsId = emailSMSDao.AddToSMSQueue(smsVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EmailSMSBo.cs:AddToSMSQueue(SMSVo smsVo)");
                object[] objects = new object[1];
                objects[0] = smsVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return smsId;
        }
        public List<SMSVo> AddToSMSQueue(List<SMSVo> smsVoList)
        {
            int smsId=0;
            
            try
            {
                for(int i=0;i<smsVoList.Count;i++)
                {
                    smsId = AddToSMSQueue(smsVoList[i]);
                    smsVoList[i].SMSId = smsId;
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
                FunctionInfo.Add("Method", "EmailSMSBo.cs:AddToSMSQueue(SMSVo smsVo)");
                object[] objects = new object[1];
                objects[0] = smsVoList;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return smsVoList;
        }
        /// <summary>
        /// Method to Add Email Log Infor to the Table
        /// </summary>
        /// <param name="emailVo">EmailVo Object with the Details of the Email Sent</param>
        /// <returns name="EmailLogId">The Unique Id created for the email Log Entry</returns>
        /// 
        public int AddToEmailLog(EmailVo emailVo)
        {
            int emailLogId = 0;
            EmailSMSDao emailSMSDao = new EmailSMSDao();
        
            try
            {
                emailLogId = emailSMSDao.AddToEmailLog(emailVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EmailSMSBo.cs:AddToEmailLog(EmailVo emailVo)");
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
        /// </summary>
        /// <param name="emailVo"></param>
        /// <returns>This will return true or false</returns>

        public bool AddToEmailQueue(EmailVo emailVo)
        {
            bool isCompleted= false;
            EmailSMSDao emailSMSDao = new EmailSMSDao();
            try
            {
                isCompleted = bool.Parse(emailSMSDao.AddToEmailQueue(emailVo).ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EmailSMSBo.cs:AddToEmailQueue(EmailVo emailVo)");
                object[] objects = new object[1];
                objects[0] = emailVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return isCompleted;
        }
    }
}
