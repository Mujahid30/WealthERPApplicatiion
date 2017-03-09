using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoUser;
using DaoUser;
using System.Collections.Specialized;

namespace BoUser
{
    public class DashboardBo
    {
        public DashboardVo GetUserDashboard(UserVo userVo, string KeyName)
        {
            DashboardVo dashboardVo = null;
            DashboardDao dashboardDao = new DashboardDao();

            try
            {
                dashboardVo = dashboardDao.GetUserDashboard(userVo, KeyName);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "DashboardBo.cs:GetUserDashboard()");


                object[] objects = new object[2];
                objects[0] = userVo;
                objects[1] = KeyName;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dashboardVo;
        }

        public bool UpdateUserDashboard(DashboardVo dashboardVo)
        {
            bool Result = false;
            DashboardDao dashboardDao = new DashboardDao();

            try
            {
                Result = dashboardDao.UpdateUserDashboard(dashboardVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "DashboardBo.cs:UpdateUserDashboard()");

                object[] objects = new object[2];
                objects[0] = dashboardVo.KeyName;
                objects[1] = dashboardVo.UserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return Result;
        }
    }
}
