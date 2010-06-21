using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using VoCustomerPortfolio;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;


namespace DaoCustomerPortfolio 
{
    public class RealEstateDao
    {
        public bool CreateRealEstateTransaction(RealEstateVo realEstateVo, string userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createRealEstateTransaction;
            try 
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                createRealEstateTransaction=db.GetStoredProcCommand("createRealEstateTransaction");                
                db.AddInParameter(createRealEstateTransaction, "@CIRET_TransactionId", DbType.String, realEstateVo.TransactionId);
                db.AddInParameter(createRealEstateTransaction, "@CIRET_Name", DbType.String, realEstateVo.Name);
                db.AddInParameter(createRealEstateTransaction, "@IC_InvestmentCode", DbType.String, realEstateVo.InvestmentCode);
                db.AddInParameter(createRealEstateTransaction, "@CIRET_TransactionDate", DbType.DateTime, realEstateVo.TransactionDate);
                db.AddInParameter(createRealEstateTransaction, "@CIRET_Quantity", DbType.Decimal, realEstateVo.Quantity);
                db.AddInParameter(createRealEstateTransaction, "@CIRET_TransactionRate", DbType.Decimal, realEstateVo.TransactionRate);
                db.AddInParameter(createRealEstateTransaction, "@CA_AccountId", DbType.String, realEstateVo.AccountId);
                db.AddInParameter(createRealEstateTransaction, "@IC_MeasurCode", DbType.String, realEstateVo.MeasureCode);
                db.AddInParameter(createRealEstateTransaction, "@CIRET_BuySell", DbType.String, realEstateVo.BuySell);
                db.AddInParameter(createRealEstateTransaction, "@CIRET_CreatedBy", DbType.String, userId);
                db.AddInParameter(createRealEstateTransaction, "@CIRET_ModifiedBy", DbType.String, userId);

               if( db.ExecuteNonQuery(createRealEstateTransaction)!=0)
                bResult = true;


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RealEstateDao.cs:CreateRealEstateTransaction()");


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
            List<RealEstateVo> realEstateTransactionsList = null;
            RealEstateVo realEstateVo = new RealEstateVo();
            Database db;
            DbCommand getRealEstateTransactionsCmd;
            DataSet dsGetRealEstateTransactions;
            DataTable dtGetRealEstateTransactions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRealEstateTransactionsCmd = db.GetStoredProcCommand("getCustomerRealEstateTransactions");
                db.AddInParameter(getRealEstateTransactionsCmd, "@CA_AccountId", DbType.String, accountId.ToString());

                dsGetRealEstateTransactions = db.ExecuteDataSet(getRealEstateTransactionsCmd);
                if (dsGetRealEstateTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetRealEstateTransactions = dsGetRealEstateTransactions.Tables[0];
                    realEstateTransactionsList = new List<RealEstateVo>();
                    foreach (DataRow dr in dtGetRealEstateTransactions.Rows)
                    {
                        realEstateVo = new RealEstateVo();
                        realEstateVo.TransactionId = dr["CIRET_TransactionId"].ToString();
                        realEstateVo.Name = dr["CIRET_Name"].ToString();
                        realEstateVo.InvestmentCode = dr["IC_InvestmentCode"].ToString();
                        realEstateVo.BuySell = dr["CIRET_BuySell"].ToString();
                        realEstateVo.TransactionDate = DateTime.Parse(dr["CIRET_TransactionDate"].ToString());
                        realEstateVo.Quantity = float.Parse(dr["CIRET_Quantity"].ToString());
                        realEstateVo.TransactionRate = float.Parse(dr["CIRET_TransactionRate"].ToString());
                        realEstateVo.MeasureCode = dr["IC_MeasurCode"].ToString();

                        realEstateTransactionsList.Add(realEstateVo);
                    }
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

                FunctionInfo.Add("Method", "RealEstateDao.cs:GetRealEstateTransactions()");


                object[] objects = new object[1];
                objects[0] = accountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return realEstateTransactionsList;
        }

        
    }
}
