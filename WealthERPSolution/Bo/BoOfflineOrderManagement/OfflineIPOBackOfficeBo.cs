using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using DaoOfflineOrderManagement;
namespace BoOfflineOrderManagement
{
    public class OfflineIPOBackOfficeBo
    {
        public DataTable GetOfflineIPOOrderBook(int adviserId, int issueNo, string status, DateTime dtFrom, DateTime dtTo, int orderId)
        {
            DataTable dtIPOOrder;
            OfflineIPOBackOfficeDao OfflineIPOBackOfficeDao = new OfflineIPOBackOfficeDao();
            try
            {
                dtIPOOrder = OfflineIPOBackOfficeDao.GetOfflineIPOOrderBook(adviserId, issueNo, status, dtFrom, dtTo, orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OfflineIPOBackOfficeBo.cs:GetOfflineIPOOrderBook()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOOrder;
        }
        public DataTable GetOfflineIPOOrderSubBook(int adviserId, int IssuerId, int orderid)
        {
            DataTable dtIPOOrderBook;
            OfflineIPOBackOfficeDao OfflineIPOBackOfficeDao = new OfflineIPOBackOfficeDao();
            try
            {
                dtIPOOrderBook = OfflineIPOBackOfficeDao.GetOfflineIPOOrderSubBook(adviserId, IssuerId, orderid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OfflineIPOBackOfficeBo.cs:GetOfflineIPOOrderSubBook()");
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
            OfflineIPOBackOfficeDao OfflineIPOBackOfficeDao = new OfflineIPOBackOfficeDao();
            dtGetIPOHoldings = OfflineIPOBackOfficeDao.GetIPOHoldings(AdviserId, AIMIssueId, PageSize, CurrentPage, CustomerNamefilter, out RowCount);
            return dtGetIPOHoldings;
        }
        
    }
}
