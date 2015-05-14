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
        OnlineMFSchemeDetailsDao OnlineMFSchemeDetailsDao = new OnlineMFSchemeDetailsDao();
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
    }
}
