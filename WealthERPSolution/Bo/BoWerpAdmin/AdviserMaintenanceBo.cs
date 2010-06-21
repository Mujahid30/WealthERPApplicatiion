using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUser;

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
    }
}
