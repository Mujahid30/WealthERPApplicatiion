using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using DaoOnlineOrderManagement;

namespace BoOnlineOrderManagement
{
    public class OnlineIPOBackOfficeBo
    {
        public DataTable GetAdviserIPOOrderBook(int adviserId,int issueNo, string status, DateTime dtFrom, DateTime dtTo,int orderId,string bidStatus)
        {
            DataTable dtIPOOrder;
            OnlineIPOBackOfficeDao OnlineIPOBackOfficeDao = new OnlineIPOBackOfficeDao();
            try
            {
                dtIPOOrder = OnlineIPOBackOfficeDao.GetAdviserIPOOrderBook(adviserId, issueNo, status, dtFrom, dtTo, orderId, bidStatus);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetAdviserNCDOrderBook()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOOrder;
        }
        public DataTable GetAdviserIPOOrderSubBook(int adviserId, int IssuerId, int orderid)
        {
            DataTable dtIPOOrderBook;
            OnlineIPOBackOfficeDao OnlineIPOBackOfficeDao = new OnlineIPOBackOfficeDao();
            try
            {
                dtIPOOrderBook = OnlineIPOBackOfficeDao.GetAdviserIPOOrderSubBook(adviserId, IssuerId, orderid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetAdviserIPOOrderSubBook()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOOrderBook;
        }
        public DataTable GetIPOHoldings(int AdviserId, int AIMIssueId, int PageSize, int CurrentPage, string CustomerNamefilter, out int RowCount)
        {
            DataTable dtGetIPOHoldings;
            OnlineIPOBackOfficeDao OnlineIPOBackOfficeDao = new OnlineIPOBackOfficeDao();
            dtGetIPOHoldings = OnlineIPOBackOfficeDao.GetIPOHoldings(AdviserId, AIMIssueId, PageSize, CurrentPage, CustomerNamefilter, out RowCount);
            return dtGetIPOHoldings;
        }
    }
}
