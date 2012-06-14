using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;


using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using VoValuation;




namespace DaoValuation
{
    public class MFEngineDao
    {
        public List<int> GetAdviserCustomerList_MF(int adviserId)
        {
            List<int> customerList = null;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet ds;

            try
            {
                customerList = new List<int>();
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_AdviserDailyValuationMFCustomerList");
                db.AddInParameter(getCustomerListCmd, "@A_AdviserId", DbType.Int32, adviserId);


                ds = db.ExecuteDataSet(getCustomerListCmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    customerList = new List<int>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int customerId = int.Parse(dr["CustomerId"].ToString());

                        customerList.Add(customerId);
                    }
                }
                else
                {
                    customerList = null;
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
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerList_MF()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return customerList;
        }

     

        public DataSet GetCustomerDeatilsForValuation(int customerId, int portfolioid, int mfAccountId, int schemePlanCode)
        {
            Database db;
            DbCommand getCustomerDetailsCmd;
            DataSet dsGetCustomerDetails;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerDetailsCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolio");
                db.AddInParameter(getCustomerDetailsCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerDetailsCmd, "@Portfolioid", DbType.Int32, portfolioid);
                db.AddInParameter(getCustomerDetailsCmd, "@AccountId", DbType.Int32, mfAccountId);
                db.AddInParameter(getCustomerDetailsCmd, "@SchemePlanCode", DbType.Int32, schemePlanCode);
                dsGetCustomerDetails = db.ExecuteDataSet(getCustomerDetailsCmd);
                
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioDao.cs:GetCustomerPortfolios()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsGetCustomerDetails;

        }

        public List<CustomerPortfolioVo> GetCustomerPortfolios(int customerId)
        {
            List<CustomerPortfolioVo> customerPortfolioVoList = null;

            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            Database db;
            DbCommand getCustomerPortfolioCmd;
            DataSet dsGetCustomerPortfolio;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerPortfolioCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolio");
                db.AddInParameter(getCustomerPortfolioCmd, "C_CustomerId", DbType.Int32, customerId);

                dsGetCustomerPortfolio = db.ExecuteDataSet(getCustomerPortfolioCmd);
                if (dsGetCustomerPortfolio.Tables[0].Rows.Count > 0)
                {
                    customerPortfolioVoList = new List<CustomerPortfolioVo>();

                    foreach (DataRow dr in dsGetCustomerPortfolio.Tables[0].Rows)
                    {
                        customerPortfolioVo = new CustomerPortfolioVo();
                        customerPortfolioVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        customerPortfolioVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        customerPortfolioVo.IsMainPortfolio = int.Parse(dr["CP_IsMainPortfolio"].ToString());
                        customerPortfolioVo.PortfolioTypeCode = dr["XPT_PortfolioTypeCode"].ToString();
                        customerPortfolioVo.PMSIdentifier = dr["CP_PMSIdentifier"].ToString();
                        customerPortfolioVo.PortfolioName = dr["CP_PortfolioName"].ToString();

                        customerPortfolioVoList.Add(customerPortfolioVo);
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

                FunctionInfo.Add("Method", "PortfolioDao.cs:GetCustomerPortfolios()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerPortfolioVoList;

        }

        public DataSet GetCustomerTransactionsForBalanceCreation(int customerId)
        {
            Database db;
            DbCommand getCustomerTransactionDetailsCmd;
            DataSet dsGetCustomerTransactionDetails;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerTransactionDetailsCmd = db.GetStoredProcCommand("SPROC_GetCustomerTransactionsForBalanceCreation");
                db.AddInParameter(getCustomerTransactionDetailsCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerTransactionDetailsCmd, "@ValuationDate", DbType.DateTime, DateTime.Now);
                dsGetCustomerTransactionDetails = db.ExecuteDataSet(getCustomerTransactionDetailsCmd);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFEngineDao.cs:GetCustomerTransactionsForBalanceCreation()");


                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsGetCustomerTransactionDetails;

        }
        public DataSet GetCustomerTransactionsForBalanceCreation(int customerId,DateTime dtValuationDate)
        {
            Database db;
            DbCommand getCustomerTransactionDetailsCmd;
            DataSet dsGetCustomerTransactionDetails;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerTransactionDetailsCmd = db.GetStoredProcCommand("SPROC_GetCustomerTransactionsForBalanceCreation");
                db.AddInParameter(getCustomerTransactionDetailsCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerTransactionDetailsCmd, "@ValuationDate", DbType.DateTime, dtValuationDate);
                dsGetCustomerTransactionDetails = db.ExecuteDataSet(getCustomerTransactionDetailsCmd);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFEngineDao.cs:GetCustomerTransactionsForBalanceCreation()");


                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsGetCustomerTransactionDetails;

        }

        public void CreateCustomerMFTransactionBalance(DataSet dsCustomerMFTransBalanceSellPair)
        {

            string conString;
            conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(conString);
            DataTable dtCustomerMFTransactionBalance = new DataTable();
            DataTable dtCustomerMFTransactionSellPair = new DataTable();

            
            try
            {
                if (dsCustomerMFTransBalanceSellPair.Tables.Count > 0)
                {
                    dtCustomerMFTransactionBalance = dsCustomerMFTransBalanceSellPair.Tables["TransactionBalance"];
                    dtCustomerMFTransactionSellPair = dsCustomerMFTransBalanceSellPair.Tables["TransactionSellPair"];
 
                }
                SqlCommand cmd = new SqlCommand("SPROC_CreateCustomerMFTransactionBalance", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                //SqlParameter param = new SqlParameter("@BalanceTable", SqlDbType.Structured);
                //cmd.Parameters.Add(param);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);


                // Add the input parameter and set its properties.
                SqlParameter sqlParameterBalanceTable = new SqlParameter();
                sqlParameterBalanceTable.ParameterName = "@BalanceTableTemp";
                sqlParameterBalanceTable.SqlDbType = SqlDbType.Structured;
                //parameter.Direction = ParameterDirection.Input;
                sqlParameterBalanceTable.Value = dtCustomerMFTransactionBalance;

                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(sqlParameterBalanceTable);

                // Add the input parameter and set its properties.
                SqlParameter sqlParameterSellPair = new SqlParameter();
                sqlParameterSellPair.ParameterName = "@TransactionSellPair";
                sqlParameterSellPair.SqlDbType = SqlDbType.Structured;
                //parameter.Direction = ParameterDirection.Input;
                sqlParameterSellPair.Value = dtCustomerMFTransactionSellPair;

                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(sqlParameterSellPair);



                sqlCon.Open();
                cmd.ExecuteNonQuery();
               
                //SqlParameter parameter = new SqlParameter();
                ////The parameter for the SP must be of SqlDbType.Structured 
                //parameter.ParameterName = "@Sample";
                //parameter.SqlDbType = System.Data.SqlDbType.Structured;
                //parameter.Value = dtCustomerMFTransactionBalance;
                //command.Parameters.Add(parameter);

                //db = DatabaseFactory.CreateDatabase("wealtherp");

                //getdtCustomerMFTransactionBalanceCmd = db.GetStoredProcCommand("SPROC_GetCustomerTransactionsForBalanceCreation");
                //db.AddInParameter(getCustomerTransactionDetailsCmd, "@CustomerId", DbType.Int32, customerId);
                //dsGetCustomerTransactionDetails = db.ExecuteDataSet(getCustomerTransactionDetailsCmd);


            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                //FunctionInfo.Add("Method", "MFEngineDao.cs:GetCustomerTransactionsForBalanceCreation()");


                //object[] objects = new object[1];
                //objects[0] = customerId;
                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                //exBase.AdditionalInformation = FunctionInfo;
                //ExceptionManager.Publish(exBase);
                //throw exBase;

            }
            finally
            {
                sqlCon.Close();
            }

            
        }

        public DataSet GetCustomerAllMFPortfolioAccountForValution(int customerId)
        {
            Database db;
            DbCommand getCustomerPortfolioAccountCmd;
            DataSet dsGetCustomerPortfolioAccount;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerPortfolioAccountCmd = db.GetStoredProcCommand("SPROC_GetCustomerMFPortfolioAccountForValuation");
                db.AddInParameter(getCustomerPortfolioAccountCmd, "@CustomerId", DbType.Int32, customerId);
                dsGetCustomerPortfolioAccount = db.ExecuteDataSet(getCustomerPortfolioAccountCmd);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFEngineDao.cs:GetCustomerAllMFPortfolioAccountForValution()");


                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsGetCustomerPortfolioAccount;

        }

        public DataSet GetMFTransactionBalanceAndSellPairAccountSchemeWise(int accountId, int schemePlanCode, DateTime valuationDate)
        {
            Database db;
            DbCommand getMFTransactionBalanceAndSellPairCmd;
            DataSet dsMFTransactionBalanceAndSellPair;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFTransactionBalanceAndSellPairCmd = db.GetStoredProcCommand("SPROC_GetMFTransactionBalanceAndSellPairAccountSchemeWise");
                db.AddInParameter(getMFTransactionBalanceAndSellPairCmd, "@AccountId", DbType.Int32, accountId);
                db.AddInParameter(getMFTransactionBalanceAndSellPairCmd, "@SchemePlanCode", DbType.Int32, schemePlanCode);
                db.AddInParameter(getMFTransactionBalanceAndSellPairCmd, "@ValuationDate", DbType.DateTime, valuationDate);
                dsMFTransactionBalanceAndSellPair = db.ExecuteDataSet(getMFTransactionBalanceAndSellPairCmd);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFEngineDao.cs:GetMFTransactionBalanceAndSellPairAccountSchemeWise()");


                object[] objects = new object[3];
                objects[0] = accountId;
                objects[1] = schemePlanCode;
                objects[2] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsMFTransactionBalanceAndSellPair;

        }

        public void CreateCustomerMFNetPosition(int customerId,DateTime valuationDate,DataTable dtCustomerMFNetPosition)
        {

            string conString;
            conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(conString);
          
            try
            {
                SqlCommand cmd = new SqlCommand("SPROC_CreateCustomerMutualFundNetPositionData", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                //SqlParameter param = new SqlParameter("@BalanceTable", SqlDbType.Structured);
                //cmd.Parameters.Add(param);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);

                // Add the input parameter and set its properties.
                SqlParameter sqlParameterCustomerId = new SqlParameter();
                sqlParameterCustomerId.ParameterName = "@CustomerId";
                sqlParameterCustomerId.SqlDbType = SqlDbType.BigInt;
                sqlParameterCustomerId.Direction = ParameterDirection.Input;
                sqlParameterCustomerId.Value = customerId;

                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(sqlParameterCustomerId);


                // Add the input parameter and set its properties.
                SqlParameter sqlParameterValuationDate = new SqlParameter();
                sqlParameterValuationDate.ParameterName = "@ValuationDate";
                sqlParameterValuationDate.SqlDbType = SqlDbType.DateTime;
                sqlParameterValuationDate.Direction = ParameterDirection.Input;
                sqlParameterValuationDate.Value = valuationDate;

                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(sqlParameterValuationDate);

                // Add the input parameter and set its properties.
                SqlParameter sqlParameterMFNetPositionTable = new SqlParameter();
                sqlParameterMFNetPositionTable.ParameterName = "@CustomerMFNetPosition";
                sqlParameterMFNetPositionTable.SqlDbType = SqlDbType.Structured;
                //parameter.Direction = ParameterDirection.Input;
                sqlParameterMFNetPositionTable.Value = dtCustomerMFNetPosition;

                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(sqlParameterMFNetPositionTable);


            
                sqlCon.Open();
                cmd.ExecuteNonQuery();

                //SqlParameter parameter = new SqlParameter();
                ////The parameter for the SP must be of SqlDbType.Structured 
                //parameter.ParameterName = "@Sample";
                //parameter.SqlDbType = System.Data.SqlDbType.Structured;
                //parameter.Value = dtCustomerMFTransactionBalance;
                //command.Parameters.Add(parameter);

                //db = DatabaseFactory.CreateDatabase("wealtherp");

                //getdtCustomerMFTransactionBalanceCmd = db.GetStoredProcCommand("SPROC_GetCustomerTransactionsForBalanceCreation");
                //db.AddInParameter(getCustomerTransactionDetailsCmd, "@CustomerId", DbType.Int32, customerId);
                //dsGetCustomerTransactionDetails = db.ExecuteDataSet(getCustomerTransactionDetailsCmd);


            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                //FunctionInfo.Add("Method", "MFEngineDao.cs:GetCustomerTransactionsForBalanceCreation()");


                //object[] objects = new object[1];
                //objects[0] = customerId;
                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                //exBase.AdditionalInformation = FunctionInfo;
                //ExceptionManager.Publish(exBase);
                //throw exBase;

            }
            finally
            {
                sqlCon.Close();
            }


        }

        public DataSet GetCustomerMFTransactionBalanceAndSellPair(int customerId, DateTime valuationDate)
        {
            Database db;
            DbCommand getCustomerMFTransactionBalanceAndSellPairCmd;
            DataSet dsCustomerMFTransactionBalanceAndSellPair;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerMFTransactionBalanceAndSellPairCmd = db.GetStoredProcCommand("SP_GetCustomerTransactionBalanceForValuation");
                db.AddInParameter(getCustomerMFTransactionBalanceAndSellPairCmd, "@C_CustomerId", DbType.Int32, customerId);                
                db.AddInParameter(getCustomerMFTransactionBalanceAndSellPairCmd, "@ValuationDate", DbType.DateTime, valuationDate);
                dsCustomerMFTransactionBalanceAndSellPair = db.ExecuteDataSet(getCustomerMFTransactionBalanceAndSellPairCmd);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFEngineDao.cs:GetMFTransactionBalanceAndSellPairAccountSchemeWise()");


                object[] objects = new object[3];
                objects[0] = customerId;                
                objects[1] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsCustomerMFTransactionBalanceAndSellPair;

        }
        //private SqlConnection SqlConnection(string con)
        //{
        //    throw new NotImplementedException();
        //}

        public void CreateAdviserMFNetPosition(int adviserId, DateTime valuationDate, DataTable dtAdviserMFNetPosition)
        {

            string conString;
            conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(conString);

            try
            {
                SqlCommand cmd = new SqlCommand("SPROC_BulkInsertAdviserMutualFundNetPosition", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                //SqlParameter param = new SqlParameter("@BalanceTable", SqlDbType.Structured);
                //cmd.Parameters.Add(param);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);

                // Add the input parameter and set its properties.
                SqlParameter sqlParameterCustomerId = new SqlParameter();
                sqlParameterCustomerId.ParameterName = "@AdviserId";
                sqlParameterCustomerId.SqlDbType = SqlDbType.BigInt;
                sqlParameterCustomerId.Direction = ParameterDirection.Input;
                sqlParameterCustomerId.Value = adviserId;

                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(sqlParameterCustomerId);


                // Add the input parameter and set its properties.
                SqlParameter sqlParameterValuationDate = new SqlParameter();
                sqlParameterValuationDate.ParameterName = "@ValuationDate";
                sqlParameterValuationDate.SqlDbType = SqlDbType.DateTime;
                sqlParameterValuationDate.Direction = ParameterDirection.Input;
                sqlParameterValuationDate.Value = valuationDate;

                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(sqlParameterValuationDate);

                // Add the input parameter and set its properties.
                SqlParameter sqlParameterMFNetPositionTable = new SqlParameter();
                sqlParameterMFNetPositionTable.ParameterName = "@AdviserMFNetPosition";
                sqlParameterMFNetPositionTable.SqlDbType = SqlDbType.Structured;
                //parameter.Direction = ParameterDirection.Input;
                sqlParameterMFNetPositionTable.Value = dtAdviserMFNetPosition;

                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(sqlParameterMFNetPositionTable);



                sqlCon.Open();
                cmd.ExecuteNonQuery();

                //SqlParameter parameter = new SqlParameter();
                ////The parameter for the SP must be of SqlDbType.Structured 
                //parameter.ParameterName = "@Sample";
                //parameter.SqlDbType = System.Data.SqlDbType.Structured;
                //parameter.Value = dtCustomerMFTransactionBalance;
                //command.Parameters.Add(parameter);

                //db = DatabaseFactory.CreateDatabase("wealtherp");

                //getdtCustomerMFTransactionBalanceCmd = db.GetStoredProcCommand("SPROC_GetCustomerTransactionsForBalanceCreation");
                //db.AddInParameter(getCustomerTransactionDetailsCmd, "@CustomerId", DbType.Int32, customerId);
                //dsGetCustomerTransactionDetails = db.ExecuteDataSet(getCustomerTransactionDetailsCmd);


            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                //FunctionInfo.Add("Method", "MFEngineDao.cs:GetCustomerTransactionsForBalanceCreation()");


                //object[] objects = new object[1];
                //objects[0] = customerId;
                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                //exBase.AdditionalInformation = FunctionInfo;
                //ExceptionManager.Publish(exBase);
                //throw exBase;

            }
            finally
            {
                sqlCon.Close();
            }


        }


        public DataSet GetMFTransactionsForBalanceCreation(int accountId,int schemePlaneCode)
        {
            Database db;
            DbCommand getMFTransactionDetailsCmd;
            DataSet dsGetMFTransactionDetails;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFTransactionDetailsCmd = db.GetStoredProcCommand("SPROC_GetMFTransactionsForBalanceCreation");
                db.AddInParameter(getMFTransactionDetailsCmd, "@AccountId", DbType.Int32, accountId);
                db.AddInParameter(getMFTransactionDetailsCmd, "@SchemePlanCode", DbType.Int32, schemePlaneCode);
                dsGetMFTransactionDetails = db.ExecuteDataSet(getMFTransactionDetailsCmd);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFEngineDao.cs:GetCustomerTransactionsForBalanceCreation()");


                object[] objects = new object[2];
                objects[0] = accountId;
                objects[1] = schemePlaneCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsGetMFTransactionDetails;

        }
        public DataSet GetSchemeNavDetail(int schemePlanCode, DateTime valuationDate)
        {
            Database db;
            DbCommand getSchemeNavCmd;
            DataSet dsSchemeNavDetail;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeNavCmd = db.GetStoredProcCommand("SPROC_GetSchemeNavDetail");
                db.AddInParameter(getSchemeNavCmd, "@SchemePlanCode", DbType.Int32, schemePlanCode);
                db.AddInParameter(getSchemeNavCmd, "@ValuationDate", DbType.DateTime, valuationDate);
                dsSchemeNavDetail = db.ExecuteDataSet(getSchemeNavCmd);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFEngineDao.cs:GetSchemeNavDetail()");


                object[] objects = new object[2];

                objects[0] = schemePlanCode;
                objects[1] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsSchemeNavDetail;
        }

    }
}
