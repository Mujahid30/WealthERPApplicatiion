using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUploads;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoUploads
{
    public class WerpEQUploadsDao
    {

        public List<WerpEQTranUploadsVo> GetWerpEQNewTransactions(int processId)
        {
            List<WerpEQTranUploadsVo> uploadsEQTransactionsList = new List<WerpEQTranUploadsVo>();
            WerpEQTranUploadsVo WerpEQTranVo;
            Database db;
            DbCommand getNewTransactionsCmd;
            DataSet getNewTransactionsDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNewTransactionsCmd = db.GetStoredProcCommand("SP_UploadsGetNewEQStandardTransactions");
                db.AddInParameter(getNewTransactionsCmd, "@ProcessId", DbType.Int32, processId);
                getNewTransactionsDs = db.ExecuteDataSet(getNewTransactionsCmd);
                //dr = getNewCustomersDs.Tables[0].Rows[0];
                foreach (DataRow dr in getNewTransactionsDs.Tables[0].Rows)
                {
                    WerpEQTranVo = new WerpEQTranUploadsVo();
                    WerpEQTranVo.ProcessID = int.Parse(dr["ADUL_ProcessId"].ToString());
                    WerpEQTranVo.XMLFileTypeId = int.Parse(dr["WUXFT_XMLFileTypeId"].ToString());
                    WerpEQTranVo.TradeNumber = long.Parse(dr["CETS_TradeNum"].ToString());
                    WerpEQTranVo.TradeAccountNumber = dr["CETS_TradeAccountNumber"].ToString();
                    if (dr["CETS_TradeDate"].ToString() != "")
                        WerpEQTranVo.TradeDate = DateTime.Parse(dr["CETS_TradeDate"].ToString());
                    WerpEQTranVo.IsSpeculative = short.Parse (dr["CETS_IsSpeculative"].ToString());
                    WerpEQTranVo.ScripCode = int.Parse(dr["CETS_ScripCode"].ToString());
                    WerpEQTranVo.Rate = double.Parse(dr["CETS_Rate"].ToString());
                    WerpEQTranVo.Quantity = dr["CETS_Quantity"].ToString();
                    WerpEQTranVo.BrokerCode = dr["CETS_BrokerCode"].ToString();
                    WerpEQTranVo.Exchange = dr["CETS_Exchange"].ToString();
                    WerpEQTranVo.Brokerage = dr["CETS_Brokerage"].ToString();
                    WerpEQTranVo.ServiceTax = dr["CETS_ServiceTax"].ToString();
                    WerpEQTranVo.EducationCess = dr["CETS_EducationCess"].ToString();
                    WerpEQTranVo.STT = dr["CETS_STT"].ToString();
                    WerpEQTranVo.OtherCharges = dr["CETS_OtherCharges"].ToString();
                    WerpEQTranVo.RateInclBrokerage = dr["CETS_RateInclBrokerage"].ToString();
                    WerpEQTranVo.TradeTotal = dr["CETS_TradeTotal"].ToString();
                    WerpEQTranVo.BuySell = dr["CETS_BuySell"].ToString();
                    WerpEQTranVo.OrderNum = dr["CETS_OrderNum"].ToString();
                    WerpEQTranVo.AccountId = dr["CETA_AccountId"].ToString();
                    WerpEQTranVo.PortfolioId = dr["CP_PortfolioId"].ToString();
                    WerpEQTranVo.CustomerId = dr["C_CustomerId"].ToString();
                    WerpEQTranVo.TransactionTypeCode = dr["CETS_TransactionTypeCode"].ToString();
                    WerpEQTranVo.PANNumber = dr["CETS_PANNum"].ToString();
             
                    uploadsEQTransactionsList.Add(WerpEQTranVo);
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "WerpMFUploadsDao.cs:GetWerpMFProfNewCustomers()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return uploadsEQTransactionsList;
        }

        public bool UpdateWerpMFProfileStagingIsCustomerNew(int adviserId, int processId)
        {
            Database db;
            DbCommand updateStagingIsFolioNew;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateStagingIsFolioNew = db.GetStoredProcCommand("SP_UpdateWerpMFProfileStagingIsCustomerNew");
                db.AddInParameter(updateStagingIsFolioNew, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(updateStagingIsFolioNew, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(updateStagingIsFolioNew);
                result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "WerpMFUploadDao.cs:UpdateWerpMFProfileStagingIsCustomerNew()");

                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public bool UpdateWerpMFProfileStagingIsFolioNew(int processId)
        {
            Database db;
            DbCommand updateStagingIsFolioNew;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateStagingIsFolioNew = db.GetStoredProcCommand("SP_UpdateWerpMFProfileStagingIsFolioNew");
                db.AddInParameter(updateStagingIsFolioNew, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(updateStagingIsFolioNew);
                result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "WerpMFUploadDao.cs:UpdateWerpMFProfileStagingIsFolioNew()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public DataSet GetWerpMFProfileNewFolios(int processId)
        {
            DataSet uploadsFolioList = new DataSet();
            //WerpMFUploadsVo WerpMFUploadsVo;
            Database db;
            DbCommand getNewFoliosCmd;
            DataSet getNewFoliosDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNewFoliosCmd = db.GetStoredProcCommand("SP_UploadGetWerpMFProfileNewFolios");
                db.AddInParameter(getNewFoliosCmd, "@processId", DbType.Int32, processId);
                getNewFoliosDs = db.ExecuteDataSet(getNewFoliosCmd);
                //dr = getNewCustomersDs.Tables[0].Rows[0];
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "WerpMFUploadDao.cs:GetWerpMFProfileNewFolios()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getNewFoliosDs;
        }


    }
}
