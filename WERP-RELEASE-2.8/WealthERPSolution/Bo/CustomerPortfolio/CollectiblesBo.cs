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
    public class CollectiblesBo
    {
        public bool CreateCollectiblesPortfolio(CollectiblesVo collectiblesVo, int userId)
        {
            CollectiblesDao collectiblesDao = new CollectiblesDao();
            bool bResult = false;
            try
            {
                bResult = collectiblesDao.CreateCollectiblesPortfolio(collectiblesVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CollectiblesBo.cs:CreateCollectiblesPortfolio()");


                object[] objects = new object[2];
                objects[0] = collectiblesVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public List<CollectiblesVo> GetCollectiblesPortfolio(int PortfolioId, int CurrentPage, string sortOrder, out int count)
        {
            CollectiblesDao collectiblesDao = new CollectiblesDao();

            List<CollectiblesVo> collectiblesList = new List<CollectiblesVo>();
            try
            {
                collectiblesList = collectiblesDao.GetCollectiblesPortfolio(PortfolioId, CurrentPage, sortOrder, out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CollectiblesBo.cs:GetCollectiblesPortfolio()");


                object[] objects = new object[1];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return collectiblesList;
        }

        public CollectiblesVo GetCollectiblesAsset(int collectibleId)
        {
            CollectiblesDao collectiblesDao = new CollectiblesDao();

            CollectiblesVo collectiblesVo = new CollectiblesVo();
            try
            {
                collectiblesVo = collectiblesDao.GetCollectibleAsset(collectibleId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CollectiblesBo.cs:GetCollectiblesAsset()");


                object[] objects = new object[1];
                objects[0] = collectibleId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return collectiblesVo;
        }


        public bool UpdateCollectiblesPortfolio(CollectiblesVo collectiblesVo, int userId)
        {
            CollectiblesDao collectiblesDao = new CollectiblesDao();
            bool bResult = false;
            try
            {
                bResult = collectiblesDao.UpdateCollectiblesPortfolio(collectiblesVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CollectiblesBo.cs:UpdateCollectiblesPortfolio()");


                object[] objects = new object[2];
                objects[0] = collectiblesVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool DeleteCollectiblesPortfolio(int collectiblesId)
        {
            bool bResult = false;
            CollectiblesDao collectiblesDao = new CollectiblesDao();

            try
            {
                bResult = collectiblesDao.DeleteCollectiblesPortfolio(collectiblesId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CollectiblesBo.cs:DeleteCollectiblesPortfolio()");
                object[] objects = new object[1];
                objects[0] = collectiblesId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

    }
}
