using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Data;
using System.Data.Common;
using VoCustomerPortfolio;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoCustomerPortfolio
{
    public class OtherInvestmentTransactionDao
    {
        public bool CreateOtherInvestmentTransaction(OtherInvestmentVo otherInvestmentVo, string userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createOtherInvestmentCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createOtherInvestmentCmd = db.GetStoredProcCommand("CreateOtherInvestment");
                db.AddInParameter(createOtherInvestmentCmd, "@CIOT_OtherTransactionId", DbType.String, otherInvestmentVo.TransactionId);
                db.AddInParameter(createOtherInvestmentCmd, "@CIOT_Name", DbType.String, otherInvestmentVo.Name);
                db.AddInParameter(createOtherInvestmentCmd, "@CIOT_TransactionDate", DbType.String, otherInvestmentVo.TransactionDate);
                db.AddInParameter(createOtherInvestmentCmd, "@CIOT_Quantity", DbType.String, otherInvestmentVo.Quantity);
                db.AddInParameter(createOtherInvestmentCmd, "@CIOT_MeasureCode", DbType.String, otherInvestmentVo.MeasureCode);
                db.AddInParameter(createOtherInvestmentCmd, "@CIOT_TransactionRate", DbType.String, otherInvestmentVo.TransactionRate);
                db.AddInParameter(createOtherInvestmentCmd, "@IC_InvestmentCode", DbType.String, otherInvestmentVo.InvestmentCode);
                db.AddInParameter(createOtherInvestmentCmd, "@CIOT_BuySell", DbType.String, otherInvestmentVo.BuySell);
                db.AddInParameter(createOtherInvestmentCmd, "@CIOT_CreatedBy", DbType.String, userId);
                db.AddInParameter(createOtherInvestmentCmd, "@CIOT_ModifiedBy", DbType.String, userId);
                if (db.ExecuteNonQuery(createOtherInvestmentCmd) != 0)
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

                FunctionInfo.Add("Method", "OtherInvestmentTransactionDao.cs:CreateOtherInvestmentTransaction()");


                object[] objects = new object[2];
                objects[0] = otherInvestmentVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;

        }

        public List<OtherInvestmentVo> GetOtherInvestmentTransaction(string customerId)
        {
            List<OtherInvestmentVo> otherInvestmentTransactionList = null;
            OtherInvestmentVo otherInvestmentVo = new OtherInvestmentVo();
            Database db;
            DbCommand getOtherInvestmentTransactionsCmd;
            DataSet dsGetOtherInvestmentTransactions;
            DataTable dtGetOtherInvestmentTransactions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getOtherInvestmentTransactionsCmd = db.GetStoredProcCommand("GetOtherInvestmentTransactions");
                db.AddInParameter(getOtherInvestmentTransactionsCmd, "@CM_CustomerId", DbType.String, customerId.ToString());
                dsGetOtherInvestmentTransactions = db.ExecuteDataSet(getOtherInvestmentTransactionsCmd);
                if (dsGetOtherInvestmentTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetOtherInvestmentTransactions = dsGetOtherInvestmentTransactions.Tables[0];
                    otherInvestmentTransactionList = new List<OtherInvestmentVo>();
                    foreach (DataRow dr in dtGetOtherInvestmentTransactions.Rows)
                    {
                        otherInvestmentVo = new OtherInvestmentVo();
                        otherInvestmentVo.BuySell = char.Parse(dr["CIOT_BuySell"].ToString());
                        otherInvestmentVo.InvestmentCode = dr["IC_InvestmentCode"].ToString();
                        otherInvestmentVo.MeasureCode = dr["CIOT_MeasureCode"].ToString();
                        otherInvestmentVo.Name = dr["CIOT_Name"].ToString();
                        otherInvestmentVo.Quantity = float.Parse(dr["CIOT_Quantity"].ToString());
                        otherInvestmentVo.TransactionDate = DateTime.Parse(dr["CIOT_TransactionDate"].ToString());
                        otherInvestmentVo.TransactionId = dr["CIOT_OtherTransactionId"].ToString();
                        otherInvestmentVo.TransactionRate = float.Parse(dr["CIOT_TransactionRate"].ToString());
                        otherInvestmentTransactionList.Add(otherInvestmentVo);

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

                FunctionInfo.Add("Method", "OtherInvestementTransactionDao.cs:GetOtherInvestmentTransaction()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return otherInvestmentTransactionList;
        }

    }
}
