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
    public class GoldBo
    {
        public bool CreateGoldNetPosition(GoldVo goldVo, int userId)
        {
            GoldDao goldDao = new GoldDao();
            bool bResult = false;
            try
            {
                bResult = goldDao.CreateGoldNetPosition(goldVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "GoldBo.cs:CreateGoldNetPosition()");


                object[] objects = new object[2];
                objects[0] = goldVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public List<GoldVo> GetGoldNetPosition(int portfolioId, int CurrentPage, string SortOrder, out int Count)
        {
            GoldDao goldDao = new GoldDao();

            List<GoldVo> goldList = new List<GoldVo>();
            try
            {
                goldList = goldDao.GetGoldNetPosition(portfolioId,CurrentPage,SortOrder,out Count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "GoldBo.cs:GetGoldNetPosition()");


                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return goldList;
        }

        public GoldVo GetGoldAsset(int GoldNPId)
        {
            GoldDao goldDao = new GoldDao();

            GoldVo goldVo = new GoldVo();
            try
            {
                goldVo = goldDao.GetGoldAsset(GoldNPId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "GoldBo.cs:GetGoldAsset()");


                object[] objects = new object[1];
                objects[0] = GoldNPId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return goldVo;
        }

        public bool UpdateGoldNetPosition(GoldVo goldVo, int userId)
        {
            GoldDao goldDao = new GoldDao();
            bool bResult = false;
            try
            {
                bResult = goldDao.UpdateGoldNetPosition(goldVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "GoldBo.cs:UpdateGoldNetPosition()");


                object[] objects = new object[2];
                objects[0] = goldVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }


        public bool DeleteGoldPortfolio(int goldId)
        {
            bool bResult = false;
            GoldDao goldDao = new GoldDao();

            try
            {
                bResult = goldDao.DeleteGoldPortfolio(goldId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GoldBo.cs:DeleteGoldPortfolio()");
                object[] objects = new object[1];
                objects[0] = goldId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

    }
}
