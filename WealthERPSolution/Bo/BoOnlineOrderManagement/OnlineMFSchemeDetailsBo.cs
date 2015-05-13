using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoOnlineOrderManagemnet;
using DaoOnlineOrderManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
namespace BoOnlineOrderManagement
{
    public class OnlineMFSchemeDetailsBo
    {
        public OnlineMFSchemeDetailsVo GetSchemeDetails(int amcCode, int schemeCode, string category)
        {
            OnlineMFSchemeDetailsVo OnlineMFSchemeDetailsVo = new OnlineMFSchemeDetailsVo();
            OnlineMFSchemeDetailsDao OnlineMFSchemeDetailsDao = new OnlineMFSchemeDetailsDao();
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
