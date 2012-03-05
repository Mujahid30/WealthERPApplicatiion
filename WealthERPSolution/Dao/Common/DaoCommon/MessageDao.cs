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
            MessageDao messageDao = new MessageDao();
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
            MessageDao messageDao = new MessageDao();
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
            MessageDao messageDao = new MessageDao();
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
            MessageDao messageDao = new MessageDao();
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
            MessageDao messageDao = new MessageDao();
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

    }
}
