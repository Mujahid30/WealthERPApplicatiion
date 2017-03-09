using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoCustomerPortfolio;
using VoUser;
using DaoCustomerPortfolio;
using BoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;

namespace BoCustomerPortfolio
{
    public class PersonalBo
    {
        public int CreatePersonalPortfolio(PersonalVo personalVo, int userId)
        {
            PersonalDao personalDao = new PersonalDao();
            int  personalId = 0;
            try
            {
                personalId = personalDao.CreatePersonalNetPosition(personalVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PersonalBo.cs:CreatePersonalPortfolio()");


                object[] objects = new object[2];
                objects[0] = personalVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return personalId;
        }

        public List<PersonalVo> GetPersonalPortfolio(int portfolioId, int CurrentPage, string sortOrder, out int count)
        {
            PersonalDao personalDao = new PersonalDao();

            List<PersonalVo> personalList = new List<PersonalVo>();
            try
            {
                personalList = personalDao.GetPersonalNetPosition(portfolioId,CurrentPage,sortOrder,out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PersonalBo.cs:GetPersonalPortfolio()");


                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return personalList;
        }

        public PersonalVo GetPersonalAsset(int personalPortfolioId)
        {
            PersonalDao personalDao = new PersonalDao();

            PersonalVo personalVo = new PersonalVo();
            try
            {
                personalVo = personalDao.GetPersonalAsset(personalPortfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PersonalBo.cs:GetPersonalAsset()");


                object[] objects = new object[1];
                objects[0] = personalPortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return personalVo;
        }

        public bool UpdatePersonalPortfolio(PersonalVo personalVo, int userId)
        {
            PersonalDao personalDao = new PersonalDao();
            bool bResult = false;
            try
            {
                bResult = personalDao.UpdatePersonalNetPosition(personalVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PersonalBo.cs:UpdatePersonalPortfolio()");


                object[] objects = new object[2];
                objects[0] = personalVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool DeletePersonalPortfolio(int personalId)
        {
            bool bResult = false;
            PersonalDao personalDao = new PersonalDao();

            try
            {
                bResult = personalDao.DeletePersonalPortfolio(personalId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PersonalBo.cs:DeletePersonalPortfolio()");
                object[] objects = new object[1];
                objects[0] = personalId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public DataSet GetPersonalDropDown(string customerId)
        {
            DataSet ds = null;
            PersonalDao personalDao = new PersonalDao();

            try
            {
                ds = personalDao.GetPersonalDropDown(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PersonalBo.cs:GetPersonalDropDown()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return ds;
        }

    }
}
