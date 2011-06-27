using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoCustomerPortfolio;
using VoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace BoCustomerPortfolio
{
    public class SystematicSetupBo
    {
        public bool CreateSystematicSchemeSetup(SystematicSetupVo systematicSetupVo, int userId)
        {
            bool bResult = false;
            SystematicSetupDao systematicSetupDao = new SystematicSetupDao();
            try
            {
                bResult = systematicSetupDao.CreateSystematicSchemeSetup(systematicSetupVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupBo.cs:CreateSystematicSchemeSetup");
                object[] objects = new object[2];
                objects[0] = systematicSetupVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool UpdateSystematicSchemeSetup(SystematicSetupVo systematicSetupVo, int userId)
        {
            bool bResult = false;
            SystematicSetupDao systematicSetupDao = new SystematicSetupDao();
            try
            {
                bResult = systematicSetupDao.UpdateSystematicSchemeSetup(systematicSetupVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupBo.cs:UpdateSystematicSchemeSetup");
                object[] objects = new object[2];
                objects[0] = systematicSetupVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public List<SystematicSetupVo> GetSystematicSchemeSetupList(int portfolioId, int CurrentPage, string sortOrder, out int count)
        {
            List<SystematicSetupVo> systematicSetupList = new List<SystematicSetupVo>();
            SystematicSetupDao systematicSetupDao = new SystematicSetupDao();
            try
            {
                systematicSetupList = systematicSetupDao.GetSystematicSchemeSetupList(portfolioId, CurrentPage, sortOrder, out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupBo.cs:GetSystematicSchemeSetupList()");
                object[] objects = new object[1];
                objects[0] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return systematicSetupList;
        }

        public SystematicSetupVo GetSystematicSchemeSetupDetails(int systematicSetupId)
        {
            SystematicSetupDao systematicSetupDao = new SystematicSetupDao();
            SystematicSetupVo systematicSetupVo = new SystematicSetupVo();
            try
            {
                systematicSetupVo = systematicSetupDao.GetSystematicSchemeSetupDetails(systematicSetupId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupBo.cs:GetSystematicSchemeSetupList()");
                object[] objects = new object[1];
                objects[0] = systematicSetupId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return systematicSetupVo;
        }
    }
}
