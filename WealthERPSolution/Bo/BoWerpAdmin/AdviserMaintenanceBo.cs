using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUser;
using System.Data;
using System.Data.OleDb;
using DaoWerpAdmin;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoWerpAdmin
{
    public class AdviserMaintenanceBo
    {

        public List<AdvisorVo> GetAdviserList()
        {

            List<AdvisorVo> adviserVoList = new List<AdvisorVo>();
            AdviserMaintenanceDao adviserMaintenanceDao = new AdviserMaintenanceDao();
            try
            {
                adviserVoList = adviserMaintenanceDao.GetAdviserList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserList()");
                object[] objects = new object[1];
                objects[0] = adviserVoList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return adviserVoList;
        }


        public DataSet GetMessageBroadcast()
        {
            AdviserMaintenanceDao advisormaintanencedao = new AdviserMaintenanceDao();
            DataSet dsGetMessageBroadcast = advisormaintanencedao.GetMessageBroadcast();
            return dsGetMessageBroadcast;
        }
        public void UpdateMessageBroadcast(int BroadcastMessageId, Int16 IsActive)
        {
            AdviserMaintenanceDao advisormaintanencedao = new AdviserMaintenanceDao();
            advisormaintanencedao.UpdateMessageBroadcast(BroadcastMessageId, IsActive);
        }
        public void MessageBroadcastSendMessage(string BroadcastMessage, DateTime Broadcasttime, DateTime ExpiryDate)
        {
            AdviserMaintenanceDao advisormaintanencedao = new AdviserMaintenanceDao();
            advisormaintanencedao.MessageBroadcastSendMessage(BroadcastMessage, Broadcasttime, ExpiryDate);
        }

        public List<AdvisorVo> GetAdviserListWithPager(int CurrentPage, out int Count, string SortExpression, string filterExpression)
        {

            List<AdvisorVo> adviserVoList = new List<AdvisorVo>();
            AdviserMaintenanceDao adviserMaintenanceDao = new AdviserMaintenanceDao();
            try
            {
                adviserVoList = adviserMaintenanceDao.GetAdviserListWithPager(CurrentPage, out Count, SortExpression, filterExpression);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserListWithPager()");
                object[] objects = new object[1];
                objects[0] = adviserVoList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return adviserVoList;
        }
    }
}
