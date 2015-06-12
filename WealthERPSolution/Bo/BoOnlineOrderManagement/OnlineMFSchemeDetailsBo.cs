using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoOnlineOrderManagemnet;
using DaoOnlineOrderManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
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



        public OnlineMFSchemeDetailsVo GetSchemeDetails(int amcCode, int schemeCode, string category)
        {
            OnlineMFSchemeDetailsVo OnlineMFSchemeDetailsVo = new OnlineMFSchemeDetailsVo();
            try
            {
                OnlineMFSchemeDetailsVo = OnlineMFSchemeDetailsDao.GetSchemeDetails(amcCode, schemeCode, category);
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
        public DataSet GetSIPCustomeSchemePlan(int customerId, int AMCCode)
        {
            DataSet ds;
            try
            {
                ds = OnlineMFSchemeDetailsDao.GetSIPCustomeSchemePlan(customerId, AMCCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }
    }
}
