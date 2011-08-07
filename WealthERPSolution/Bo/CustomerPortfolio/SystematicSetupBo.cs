using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoCustomerPortfolio;
using VoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;

namespace BoCustomerPortfolio
{
    public class SystematicSetupBo
    {
        SystematicSetupDao systematicSetupDao = new SystematicSetupDao();

        public bool CreateSystematicSchemeSetup(SystematicSetupVo systematicSetupVo, int userId)
        {
            bool bResult = false;
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
        public DataSet GetAllDropdownBinding(string strAmcCode)
        {
            DataSet dsGetAllDropdownBinding=new DataSet();
           
            try
            {
                dsGetAllDropdownBinding = systematicSetupDao.GetAllDropdownBinding(strAmcCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch(Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupBo.cs:GetAllDropdownBinding()");
                object[] objects = new object[1];
                 FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAllDropdownBinding;

        }


        public DataSet GetSystematicMIS()
        {
            DataSet dsBindGvSystematicMIS=new DataSet();
            try
            {
                dsBindGvSystematicMIS = systematicSetupDao.GetSystematicMIS();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupBo.cs:GetSystematicMIS()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsBindGvSystematicMIS;
        }

        public DataSet GetCalenderDetailView()
        {
            DataSet dsBindgvCalenderDetailView = new DataSet();
            try
            {
                dsBindgvCalenderDetailView = systematicSetupDao.GetCalenderDetailView();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupBo.cs:GetCalenderDetailView()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
           return dsBindgvCalenderDetailView;
        }

        /// <summary>
        /// To Get Systematic MIS Data  <<Kirteeshree>>
        /// </summary>
        /// <param name="UserType"></param>
        /// <param name="AdviserId"></param>
        /// <param name="RmId"></param>
        /// <param name="CustomerId"></param>
        /// <param name="BranchHeadId"></param>
        /// <param name="BranchId"></param>
        /// <param name="All"></param>
        /// <param name="Category"></param>
        /// <param name="SysType"></param>
        /// <param name="AmcCode"></param>
        /// <param name="SchemePlanCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
        public DataSet GetAllSystematicMISData(string UserType, int AdviserId, int RmId, int CustomerId, int BranchHeadId, int BranchId, int All, string Category, string SysType, string AmcCode, string SchemePlanCode, string StartDate, string EndDate, DateTime dtFrom, DateTime dtTo, int isIndividualOrGroup)
        {
            DataSet dsGetSystematicMIS = new DataSet();
            try
            {
                dsGetSystematicMIS = systematicSetupDao.GetAllSystematicMISData(UserType, AdviserId, RmId, CustomerId, BranchHeadId, BranchId, All, Category, SysType, AmcCode, SchemePlanCode, StartDate, EndDate, dtFrom, dtTo, isIndividualOrGroup);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupBo.cs:GetAllSystematicMISData()");
                object[] objects = new object[16];
                objects[0] = UserType;
                objects[1] = AdviserId;
                objects[2] = RmId;
                objects[3] = CustomerId;
                objects[4] = BranchHeadId;
                objects[5] = BranchId;
                objects[6] = All;
                objects[7] = Category;
                objects[8] = SysType;
                objects[9] = AmcCode;
                objects[10] = SchemePlanCode;
                objects[11] = StartDate;
                objects[12] = EndDate;
                objects[13] = dtFrom;
                objects[14] = dtTo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetSystematicMIS;
        }

    }
}
