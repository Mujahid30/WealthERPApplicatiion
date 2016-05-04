using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoOnlineOrderManagemnet;
using DaoOnlineOrderManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using System.Net;
using System.IO;
namespace BoOnlineOrderManagement
{
    public class OnlineMFSchemeDetailsBo
    {
        OnlineMFSchemeDetailsDao OnlineMFSchemeDetailsDao = new OnlineMFSchemeDetailsDao();

        public bool CustomerAddMFSchemeToWatch(int customerId, int schemeCode, string assetGroup, int userId)
        {
            bool bResult = false;
            bResult = OnlineMFSchemeDetailsDao.CustomerAddMFSchemeToWatch(customerId, schemeCode, assetGroup, userId);
            return bResult;
        }
        public bool DeleteSchemeFromCustomerWatch(int schemeCode, int customerId, string productType)
        {
            try
            {
                return OnlineMFSchemeDetailsDao.DeleteSchemeFromCustomerWatch(schemeCode, customerId, productType);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DeleteSchemeFromCustomerWatch(int schemeCode, int customerId,string productType)");
                object[] objects = new object[3];
                objects[0] = customerId;
                objects[1] = schemeCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


        public OnlineMFSchemeDetailsVo GetSchemeDetails(int amcCode, int schemeCode, string category, out DataTable dtNavDetails)
        {
            OnlineMFSchemeDetailsVo OnlineMFSchemeDetailsVo = new OnlineMFSchemeDetailsVo();

            try
            {
                OnlineMFSchemeDetailsVo = OnlineMFSchemeDetailsDao.GetSchemeDetails(amcCode, schemeCode, category, out dtNavDetails);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return OnlineMFSchemeDetailsVo;
        }

        public string GetCmotCode(int schemeplanCode)
        {
            string cmotCode = string.Empty;
            try
            {
                cmotCode = OnlineMFSchemeDetailsDao.GetCmotCode(schemeplanCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return cmotCode;
        }


        public List<OnlineMFSchemeDetailsVo> GetCompareMFSchemeDetails(string schemeCompareList)
        {
            List<OnlineMFSchemeDetailsVo> OnlineMFSchemeDetailsList = new List<OnlineMFSchemeDetailsVo>();
            try
            {
                OnlineMFSchemeDetailsList = OnlineMFSchemeDetailsDao.GetCompareMFSchemeDetails(schemeCompareList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return OnlineMFSchemeDetailsList;
        }
        public DataSet GetSIPCustomeSchemePlan(int customerId, int AMCCode, int exchangeType)
        {
            DataSet ds;
            try
            {
                ds = OnlineMFSchemeDetailsDao.GetSIPCustomeSchemePlan(customerId, AMCCode, exchangeType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }
        public DataTable GetAMCandCategoryWiseScheme(int AMCCode, string category)
        {
            DataTable dt;
            try
            {
                dt = OnlineMFSchemeDetailsDao.GetAMCandCategoryWiseScheme(AMCCode, category);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }

        public DataTable GetSchemeNavHistory(int schemePlanCode, DateTime? fromDate, DateTime? toDate)
        {
            DataTable dt;
            try
            {
                dt = OnlineMFSchemeDetailsDao.GetSchemeNavHistory(schemePlanCode, fromDate, toDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }

        public DataTable GetschemedetailsNAV(int schemePlanCode)
        {
            DataTable dt;
            try
            {
                dt = OnlineMFSchemeDetailsDao.GetschemedetailsNAV(schemePlanCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
        public Dictionary<string, int> GetSortList(String ddlName)
        {
            Dictionary<string, int> sortlist = new Dictionary<string, int>();
            try
            {
                sortlist.Add("Rating- Low to high", 1);
                sortlist.Add("Rating - High to Low", 2);
                 sortlist.Add("Returns-1yr", 5);
                sortlist.Add("Returns-3yr", 6);
                sortlist.Add("Returns-5yr", 7);
                if (ddlName != "ddlMarketDataSort")
                {

                    sortlist.Add("Ranking- Low to High", 3);
                    sortlist.Add("Ranking- High to Low", 4);
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return sortlist;
        }
        public DataSet GetAPIData(string APIFormate)
        {
            DataSet theDataSet = new DataSet();
            try
            {
                WebResponse response;
                string result;
                WebRequest request = HttpWebRequest.Create(APIFormate);
                response = request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                    reader.Close();
                }
                StringReader theReader = new StringReader(result);
                theDataSet.ReadXml(theReader);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GetAPIData(string APIFormate)");
                object[] objects = new object[1];
                objects[0] = APIFormate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return theDataSet;
        }
    }
}
