using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using VoCommon;

namespace BoCommon
{
    public class MessageBo
    {
        public DataSet GetUserListSpecificToRole(string strCurrentUserRole, int intUserId, string strRoleList)
        {
            MessageDao messageDao = new MessageDao();
            DataSet ds = new DataSet();

            try
            {
                ds = messageDao.GetUserListSpecificToRole(strCurrentUserRole, intUserId, strRoleList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageBo.cs:GetUserListSpecificToRole(string strUserType, int intUserId, string strRoleList)");
                object[] objects = new object[3];
                objects[0] = strCurrentUserRole;
                objects[1] = intUserId;
                objects[2] = strRoleList;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public bool InsertComposedMessage(MessageVo messageVo)
        {
            MessageDao messageDao = new MessageDao();
            bool blResult = false;

            try
            {
                blResult = messageDao.InsertComposedMessage(messageVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageBo.cs:InsertComposedMessage(MessageVo messageVo)");
                object[] objects = new object[1];
                objects[0] = messageVo;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }

        public DataSet GetInboxRecords(int intUserId)
        {
            MessageDao messageDao = new MessageDao();
            DataSet ds = new DataSet();

            try
            {
                ds = messageDao.GetInboxRecords(intUserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageBo.cs:GetInboxRecords(int intUserId)");
                object[] objects = new object[1];
                objects[0] = intUserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public bool UpdateMessageReadFlag(long intRecipientId)
        {
            MessageDao messageDao = new MessageDao();
            bool blResult = false;

            try
            {
                blResult = messageDao.UpdateMessageReadFlag(intRecipientId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageBo.cs:UpdateMessageReadFlag(long intRecipientId)");
                object[] objects = new object[1];
                objects[0] = intRecipientId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public DataSet GetOutboxRecords(int intUserId)
        {
            MessageDao messageDao = new MessageDao();
            DataSet ds = new DataSet();

            try
            {
                ds = messageDao.GetOutboxRecords(intUserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageBo.cs:GetOutboxRecords(int intUserId)");
                object[] objects = new object[1];
                objects[0] = intUserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        /// <summary>
        /// Deletes the Messages and returns a boolean value indicating the success or failure
        /// </summary>
        /// <param name="strDeletedMessages">Contains the XML Dataset string of the deleted messages</param>
        /// <param name="intIsInbox">If 1 then inbox records will be deleted. If 0 then outbox records will be deleted</param>
        /// <returns>boolean</returns>
        public bool DeleteMessages(string strDeletedMessages, Int16 intIsInbox)
        {
            MessageDao messageDao = new MessageDao();
            bool blResult = false;

            try
            {
                blResult = messageDao.DeleteMessages(strDeletedMessages, intIsInbox);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageBo.cs:DeleteMessages(string strDeletedMessages, Int16 intIsInbox)");
                object[] objects = new object[2];
                objects[0] = strDeletedMessages;
                objects[1] = intIsInbox;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public int GetUnreadMessageCount(int intId)
        {
            MessageDao messageDao = new MessageDao();
            int intUnreadMessageCount;

            try
            {
                intUnreadMessageCount = messageDao.GetUnreadMessageCount(intId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageBo.cs:GetUnreadMessageCount(int intId)");
                object[] objects = new object[1];
                objects[0] = intId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return intUnreadMessageCount;
        }


    }
}
