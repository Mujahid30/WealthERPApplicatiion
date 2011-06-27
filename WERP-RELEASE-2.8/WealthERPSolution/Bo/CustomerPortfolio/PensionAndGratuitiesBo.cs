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
    public class PensionAndGratuitiesBo
    {
        public bool CreatePensionAndGratuities(PensionAndGratuitiesVo pensionAndGratuitiesVo,int userId)
        {
            bool bResult = false;
            PensionAndGratuitiesDao pensionAndGratuitiesDao = new PensionAndGratuitiesDao();
            try
            {
                bResult = pensionAndGratuitiesDao.CreatePensionAndGratuities(pensionAndGratuitiesVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PensionAndGratuitiesBo.cs:CreatePensionAndGratuities()");
                object[] objects = new object[2];
                objects[0] = pensionAndGratuitiesVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool UpdatePensionAndGratuities(PensionAndGratuitiesVo pensionAndGratuitiesVo, int userId)
        {
            bool bResult = false;
            PensionAndGratuitiesDao pensionAndGratuitiesDao = new PensionAndGratuitiesDao();
            try
            {
                bResult = pensionAndGratuitiesDao.UpdatePensionAndGratuities(pensionAndGratuitiesVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PensionAndGratuitiesBo.cs:UpdatePensionAndGratuities()");
                object[] objects = new object[2];
                objects[0] = pensionAndGratuitiesVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public bool DeletePensionAndGratuitiesPortfolio(int personalId, int accountId)
        {
            bool bResult = false;
            PensionAndGratuitiesDao pensionDao = new PensionAndGratuitiesDao();

            try
            {
                bResult = pensionDao.DeletePensionAndGratuitiesPortfolio(personalId, accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PensionAndGratuitiesBo.cs:DeletePensionAndGratuitiesPortfolio()");
                object[] objects = new object[1];
                objects[0] = personalId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public List<PensionAndGratuitiesVo> GetPensionAndGratuitiesList(int portfolioId, int CurrentPage, string SortOrder, out int count)
        {
            List<PensionAndGratuitiesVo> pensionAndGratuitiesList = new List<PensionAndGratuitiesVo>();
            PensionAndGratuitiesDao pensionAndGratuitiesDao = new PensionAndGratuitiesDao();
            try
            {
                pensionAndGratuitiesList = pensionAndGratuitiesDao.GetPensionAndGratuitiesList(portfolioId,CurrentPage,SortOrder,out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PensionAndGratuitiesBo.cs:GetPensionAndGratuitiesList()");


                object[] objects = new object[1];
                objects[0] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return pensionAndGratuitiesList;
        }

        public PensionAndGratuitiesVo GetPensionAndGratuities(int portfolioId)
        {
            PensionAndGratuitiesDao pensionAndGratuitiesDao = new PensionAndGratuitiesDao();
            PensionAndGratuitiesVo pensionAndGratuitiesVo = new PensionAndGratuitiesVo();
            try
            {
                pensionAndGratuitiesVo = pensionAndGratuitiesDao.GetPensionAndGratuities(portfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PensionAndGratuitiesBo.cs:GetPensionAndGratuities()");


                object[] objects = new object[1];
                objects[0] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return pensionAndGratuitiesVo;
        }
    }
}
