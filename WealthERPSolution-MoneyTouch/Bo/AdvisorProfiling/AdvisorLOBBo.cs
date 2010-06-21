using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoAdvisorProfiling;
using DaoAdvisorProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
namespace BoAdvisorProfiling
{
    public class AdvisorLOBBo
    {

        public bool CreateAdvisorLOB(AdvisorLOBVo advisorLOBVo, int advisorId,int userId)
        {
            bool result = false;

            AdvisorLOBDao advisorLOBDao = new AdvisorLOBDao();
            try
            {

                result = advisorLOBDao.CreateAdvisorLOB(advisorLOBVo, advisorId,userId);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorLOBBo.cs:CreateAdvisorLOB()");


                object[] objects = new object[3];
                objects[0] = advisorLOBVo;
                objects[1] = advisorId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }
        public bool UpdateLOB(AdvisorLOBVo advisorLOBVo,int adviserId,int userId)
        {
            bool bResult = false;
            AdvisorLOBDao advisorLOBDao = new AdvisorLOBDao();
            try
            {
                bResult = advisorLOBDao.UpdateLOB(advisorLOBVo,adviserId,userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorLOBBo.cs:updateLOB()");


                object[] objects = new object[1];
                objects[0] = advisorLOBVo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool DeleteLOB(int lobId)
        {
            AdvisorLOBDao advisorLOBDao = new AdvisorLOBDao();
            bool bResult = false;
            try
            {
                bResult = advisorLOBDao.DeleteLOB(lobId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorLOBBo.cs:DeleteLOB()");
                object[] objects = new object[1];
                objects[0] = lobId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }
        public AdvisorLOBVo GetLOB(int LOBId)
        {
            AdvisorLOBDao advisorLOBDao = new AdvisorLOBDao();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            try
            {
                advisorLOBVo=advisorLOBDao.GetLOB(LOBId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorLOBBo.cs:getLOB()");


                object[] objects = new object[1];
                objects[0] = LOBId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return advisorLOBVo;
        }
        public DataSet GetAdvisorLOBs(int advisorId, string NameFilter, string BusinessTypeFilter)
        {
            DataSet dsAdvisorLOBList;
            AdvisorLOBDao advisorLOBDao = new AdvisorLOBDao();
            try
            {
                dsAdvisorLOBList = advisorLOBDao.GetAdvisorLOBs(advisorId, NameFilter, BusinessTypeFilter);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorLOBBo.cs:GetAdvisorLOBs()");

                object[] objects = new object[1];
                objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsAdvisorLOBList;
        }

        public string GetLOBCode(string path,string assetClass, string category, string segment)
        {

            string LOBCode = "";
            AdvisorLOBDao advisorLOBDao = new AdvisorLOBDao();
            try
            {
                LOBCode = advisorLOBDao.GetLOBCode(path,assetClass, category, segment);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorLOBBo.cs:GetLOBCode()");


                object[] objects = new object[4];
                objects[0] = path;
                objects[1] = assetClass;
                objects[2] = category;
                objects[3] = segment;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return LOBCode;

        }
        public string GetBusinessType(string path,string LOBCode)
        {

            string businessType = "";
            AdvisorLOBDao advisorLOBDao;
            try
            {
                advisorLOBDao = new AdvisorLOBDao();
                businessType = advisorLOBDao.GetBusinessType(path, LOBCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorLOBBo.cs:getBusinessType()");


                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = LOBCode;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return businessType;
        }

        public bool CheckLOBExistence(int advisorId, string classificationCode)
        {
            bool result = false;

            AdvisorLOBDao advisorLOBDao = new AdvisorLOBDao();
            try
            {

                result = advisorLOBDao.CheckLOBExistence(advisorId, classificationCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorLOBBo.cs:CheckLOBExistence()");


                object[] objects = new object[2];
                objects[0] = classificationCode;
                objects[1] = advisorId;
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }
        public void UpdateAdvisorLOB(int lobId,int IsDependent)
        {
            AdvisorLOBDao advisorlobdao=new AdvisorLOBDao();
            advisorlobdao.UpdateAdvisorLOB(lobId, IsDependent);

        }
    }
}