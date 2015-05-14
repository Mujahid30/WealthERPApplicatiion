using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoOnlineOrderManagemnet;
using DaoOnlineOrderManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
namespace BoOnlineOrderManagement
{
    public class OnlineMFSchemeDetailsBo
    {
        OnlineMFSchemeDetailsDao OnlineMFSchemeDetailsDao = new OnlineMFSchemeDetailsDao();

        public bool CustomerAddMFSchemeToWatch(int customerId, int schemeCode, string assetGroup, int userId)
        {
            bool bResult = false;
            bResult=OnlineMFSchemeDetailsDao.CustomerAddMFSchemeToWatch(customerId, schemeCode, assetGroup, userId);
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





    }
}
