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

namespace BoCustomerPortfolio
{
    public class RealEstateBo
    {
        public bool CreateRealEstateTransaction(RealEstateVo realEstateVo, string userId)
        {
            RealEstateDao realEstateDao = new RealEstateDao();
            bool bResult = false;
            try
            {
                bResult = realEstateDao.CreateRealEstateTransaction(realEstateVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RealEstateBo.cs:CreateRealEstateTransaction()");


                object[] objects = new object[2];
                objects[0] = realEstateVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public List<RealEstateVo> GetRealEstateTransactions(string accountId)
        {
            RealEstateDao realEstateDao = new RealEstateDao();

            List<RealEstateVo> realEstateList = new List<RealEstateVo>();
            try
            {
                realEstateList = realEstateDao.GetRealEstateTransactions(accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RealEstateBo.cs:GetRealEstateTransactions()");


                object[] objects = new object[1];
                objects[0] = accountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return realEstateList;
        }

    }
}
