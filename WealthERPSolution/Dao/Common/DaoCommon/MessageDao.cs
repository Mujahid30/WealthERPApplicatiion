using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using VoCommon;

namespace DaoCommon
{
    public class MessageDao
    {
        /// <summary>
        /// Gets the user list for the selected role in the compose screen
        /// </summary>
        /// <param name="strCurrentUserRole">string value specifying the current user role</param>
        /// <param name="intUserId">logged in user id</param>
        /// <param name="strRoleList">list of selected roles for which user ids need to be retrieved</param>
        /// <returns></returns>
        public DataSet GetUserListSpecificToRole(string strCurrentUserRole, int intUserId, string strRoleList)
        {
            Database db;
            DbCommand cmdGetUserListSpecificToRole;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetUserListSpecificToRole = db.GetStoredProcCommand("sproc_Message_GetUserListSpecificToRole");
                db.AddInParameter(cmdGetUserListSpecificToRole, "@currentUserRole", DbType.String, strCurrentUserRole);
                db.AddInParameter(cmdGetUserListSpecificToRole, "@userId", DbType.Int32, intUserId);
                db.AddInParameter(cmdGetUserListSpecificToRole, "@roleList", DbType.String, strRoleList);
                ds = db.ExecuteDataSet(cmdGetUserListSpecificToRole);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageDao.cs:GetUserListSpecificToRole(string strUserType, int intUserId, string strRoleList)");
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

        /// <summary>
        /// Inserts the composed message for the logged in user
        /// </summary>
        /// <param name="messageVo"></param>
        /// <returns></returns>
        public bool InsertComposedMessage(MessageVo messageVo)
        {
            Database db;
            DbCommand cmdAddMessage;
            bool blResult = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdAddMessage = db.GetStoredProcCommand("sproc_Message_Compose");
                db.AddInParameter(cmdAddMessage, "@messageSubject", DbType.String, messageVo.Subject);
                db.AddInParameter(cmdAddMessage, "@messageBody", DbType.String, messageVo.Message);
                db.AddInParameter(cmdAddMessage, "@userId", DbType.Int32, messageVo.UserId);
                db.AddInParameter(cmdAddMessage, "@xmlUserIds", DbType.Xml, messageVo.strXMLRecipientIds);
                db.ExecuteNonQuery(cmdAddMessage);
                blResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageDao.cs:InsertComposedMessage(MessageVo messageVo)");
                object[] objects = new object[1];
                objects[0] = messageVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        /// <summary>
        /// Gets the Inbox for the logged in user id
        /// </summary>
        /// <param name="intUserId">logged in user id</param>
        /// <returns></returns>
        public DataSet GetInboxRecords(int intUserId)
        {
            Database db;
            DbCommand cmdGetInbox;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetInbox = db.GetStoredProcCommand("sproc_Message_GetInboxRecords");
                db.AddInParameter(cmdGetInbox, "@userId", DbType.Int32, intUserId);
                ds = db.ExecuteDataSet(cmdGetInbox);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageDao.cs:GetInboxRecords(int intUserId)");
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
        /// 
        /// </summary>
        /// <param name="intRecipientId"></param>
        /// <returns></returns>
        public bool UpdateMessageReadFlag(long intRecipientId)
        {
            Database db;
            DbCommand cmdAddMessage;
            bool blResult = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdAddMessage = db.GetStoredProcCommand("sproc_Message_UpdateMessageReadFlag");
                db.AddInParameter(cmdAddMessage, "@RecipientId", DbType.Int64, intRecipientId);
                db.ExecuteNonQuery(cmdAddMessage);
                blResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageDao.cs:UpdateMessageReadFlag(long intRecipientId)");
                object[] objects = new object[1];
                objects[0] = intRecipientId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        /// <summary>
        /// Gets the Outbox for the logged in user id
        /// </summary>
        /// <param name="intUserId">logged in user id</param>
        /// <returns></returns>
        public DataSet GetOutboxRecords(int intUserId)
        {
            Database db;
            DbCommand cmdGetOutbox;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetOutbox = db.GetStoredProcCommand("sproc_Message_GetOutboxRecords");
                db.AddInParameter(cmdGetOutbox, "@userId", DbType.Int32, intUserId);
                ds = db.ExecuteDataSet(cmdGetOutbox);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageDao.cs:GetOutboxRecords(int intUserId)");
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
            Database db;
            DbCommand cmdDeleteInboxMessages;
            bool blResult = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdDeleteInboxMessages = db.GetStoredProcCommand("sproc_Message_DeleteMessages");
                db.AddInParameter(cmdDeleteInboxMessages, "@strDeletedMessages", DbType.String, strDeletedMessages);
                db.AddInParameter(cmdDeleteInboxMessages, "@IsInbox", DbType.Int16, intIsInbox);
                db.ExecuteNonQuery(cmdDeleteInboxMessages);
                blResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageDao.cs:DeleteMessages(string strDeletedMessages, Int16 intIsInbox)");
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
            Database db;
            DbCommand cmdGetUnreadMessageCount;
            int Id;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetUnreadMessageCount = db.GetStoredProcCommand("sproc_Message_GetUnreadMessageCount");
                db.AddInParameter(cmdGetUnreadMessageCount, "@userId", DbType.Int32, intId);
                Id = Int32.Parse(db.ExecuteScalar(cmdGetUnreadMessageCount).ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageDao.cs:GetUnreadMessageCount(int intId)");
                object[] objects = new object[1];
                objects[0] = intId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return Id;
        }

        public string GetMessage(Int64 intMessageId)
        {
            Database db;
            DbCommand cmdGetMessage;
            string strResult;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetMessage = db.GetStoredProcCommand("sproc_Message_GetMessage");
                db.AddInParameter(cmdGetMessage, "@messageId", DbType.Int64, intMessageId);
                strResult = db.ExecuteScalar(cmdGetMessage).ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MessageDao.cs:GetMessage(Int64 intMessageId)");
                object[] objects = new object[1];
                objects[0] = intMessageId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return strResult;
        }
    }
}
