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
using System.Threading;
using System.Globalization;

namespace DaoCustomerPortfolio
{
    public class PortfolioDao
    {
        public DateTime? GetLatestValuationDate(int adviserID, string assetGroup)
        {
            DateTime? valuationDate = new DateTime?();
            Database db;
            DbCommand GetLatestValuationDateCmd;
            DataSet ds;


            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetLatestValuationDateCmd = db.GetStoredProcCommand("SP_GetLatestValuationDate");
                db.AddInParameter(GetLatestValuationDateCmd, "@A_AdviserId", DbType.Int32, adviserID);
                db.AddInParameter(GetLatestValuationDateCmd, "@AssetGroup", DbType.String, assetGroup);
                ds = db.ExecuteDataSet(GetLatestValuationDateCmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //string dt = ds.Tables[0].Rows[0]["ProcessDate"].ToString(); // "dd/MM/yyyy"
                    valuationDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ProcessDate"]);
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
                FunctionInfo.Add("Method", "PortfolioDao.cs:GetLatestValuationDate()");
                object[] objects = new object[2];
                objects[0] = adviserID;
                objects[1] = assetGroup;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return valuationDate;
        }

        public bool UpdateCustomerPortfolio(CustomerPortfolioVo customerPortfolioVo, int userId)
        {

            bool bResult = false;
            Database db;
            DbCommand updatePortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updatePortfolioCmd = db.GetStoredProcCommand("SP_UpdateCustomerPortfolio");

                db.AddInParameter(updatePortfolioCmd, "CP_IsMainPortfolio", DbType.Int16, customerPortfolioVo.IsMainPortfolio);
                db.AddInParameter(updatePortfolioCmd, "XPT_PortfolioTypeCode", DbType.String, customerPortfolioVo.PortfolioTypeCode);
                db.AddInParameter(updatePortfolioCmd, "CP_PortfolioName", DbType.String, customerPortfolioVo.PortfolioName);
                db.AddInParameter(updatePortfolioCmd, "CP_PMSIdentifier", DbType.String, customerPortfolioVo.PMSIdentifier);
                db.AddInParameter(updatePortfolioCmd, "CP_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(updatePortfolioCmd, "CP_PortfolioId", DbType.Int32, customerPortfolioVo.PortfolioId);

                if (db.ExecuteNonQuery(updatePortfolioCmd) != 0)
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
                FunctionInfo.Add("Method", "PortfolioDao.cs:UpdateCustomerPortfolio()");
                object[] objects = new object[2];
                objects[0] = customerPortfolioVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool CreateCustomerPortfolio(CustomerPortfolioVo customerPortfolioVo, int userId)
        {
            bool bResult = false;
            int customerPortfolioId;
            Database db;
            DbCommand createPortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createPortfolioCmd = db.GetStoredProcCommand("SP_CreateCustomerPortfolio");
                db.AddInParameter(createPortfolioCmd, "C_CustomerId", DbType.Int32, customerPortfolioVo.CustomerId);
                db.AddInParameter(createPortfolioCmd, "CP_IsMainPortfolio", DbType.Int16, customerPortfolioVo.IsMainPortfolio);
                db.AddInParameter(createPortfolioCmd, "XPT_PortfolioTypeCode", DbType.String, customerPortfolioVo.PortfolioTypeCode);
                db.AddInParameter(createPortfolioCmd, "CP_PMSIdentifier", DbType.String, customerPortfolioVo.PMSIdentifier);
                db.AddInParameter(createPortfolioCmd, "CP_PortfolioName", DbType.String, customerPortfolioVo.PortfolioName);
                db.AddInParameter(createPortfolioCmd, "CP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createPortfolioCmd, "CP_ModifiedBy", DbType.Int32, userId);
                db.AddOutParameter(createPortfolioCmd, "CP_PortfolioId", DbType.Int32, 10);

                if (db.ExecuteNonQuery(createPortfolioCmd) != 0)
                    bResult = true;
                customerPortfolioId = int.Parse(db.GetParameterValue(createPortfolioCmd, "CP_PortfolioId").ToString());


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioDao.cs:CreateCustomerPortfolio()");


                object[] objects = new object[2];
                objects[0] = customerPortfolioVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public DataSet GetCustomerPortfolio(int customerId)
        {
            Database db;
            DbCommand getCustomerPortfolioCmd;
            DataSet dsGetCustomerPortfolio;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerPortfolioCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolio");
                db.AddInParameter(getCustomerPortfolioCmd, "C_CustomerId", DbType.Int32, customerId);

                dsGetCustomerPortfolio = db.ExecuteDataSet(getCustomerPortfolioCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioDao.cs:GetCustomerPortfolio()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerPortfolio;
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

        public CustomerPortfolioVo GetCustomerDefaultPortfolio(int customerId)
        {
            CustomerPortfolioVo customerPortfolioVo = null;
            Database db;
            DbCommand getCustomerPortfolioCmd;
            DataSet dsGetCustomerPortfolio;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerPortfolioCmd = db.GetStoredProcCommand("SP_GetCustomerDefaultPortfolio");
                db.AddInParameter(getCustomerPortfolioCmd, "C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerPortfolioCmd, "@portfolioFlag", DbType.String,"D");

                dsGetCustomerPortfolio = db.ExecuteDataSet(getCustomerPortfolioCmd);

                if (dsGetCustomerPortfolio.Tables[0] != null && dsGetCustomerPortfolio.Tables[0].Rows.Count > 0)
                {
                    customerPortfolioVo = new CustomerPortfolioVo();
                    dr = dsGetCustomerPortfolio.Tables[0].Rows[0];

                    customerPortfolioVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                    customerPortfolioVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                    customerPortfolioVo.IsMainPortfolio = int.Parse(dr["CP_IsMainPortfolio"].ToString());
                    customerPortfolioVo.PortfolioTypeCode = dr["XPT_PortfolioTypeCode"].ToString();
                    customerPortfolioVo.PMSIdentifier = dr["CP_PMSIdentifier"].ToString();
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

                FunctionInfo.Add("Method", "PortfolioDao.cs:GetCustomerDefaultPortfolio()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerPortfolioVo;

        }

        public CustomerPortfolioVo GetCustomerDefaultPortfolio1(int customerId, String portfolio)
        {
            CustomerPortfolioVo customerPortfolioVo = null;
            Database db;
            DbCommand getCustomerPortfolioCmd;
            DataSet dsGetCustomerPortfolio;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerPortfolioCmd = db.GetStoredProcCommand("SP_GetCustomerDefaultPortfolio");
                db.AddInParameter(getCustomerPortfolioCmd, "C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerPortfolioCmd, "@portfolio", DbType.String, portfolio);
                db.AddInParameter(getCustomerPortfolioCmd, "@portfolioFlag", DbType.String, "M");

                dsGetCustomerPortfolio = db.ExecuteDataSet(getCustomerPortfolioCmd);

                if (dsGetCustomerPortfolio.Tables[0] != null && dsGetCustomerPortfolio.Tables[0].Rows.Count > 0)
                {
                    customerPortfolioVo = new CustomerPortfolioVo();
                    dr = dsGetCustomerPortfolio.Tables[0].Rows[0];

                    customerPortfolioVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                    customerPortfolioVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                    customerPortfolioVo.IsMainPortfolio = int.Parse(dr["CP_IsMainPortfolio"].ToString());
                    customerPortfolioVo.PortfolioTypeCode = dr["XPT_PortfolioTypeCode"].ToString();
                    customerPortfolioVo.PMSIdentifier = dr["CP_PMSIdentifier"].ToString();
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

                FunctionInfo.Add("Method", "PortfolioDao.cs:GetCustomerDefaultPortfolio()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerPortfolioVo;

        }

        public DataSet GetCustomerPortfolioDetails(int portfolioId)
        {
            Database db;
            DbCommand getCustomerPortfolioCmd;
            DataSet dsGetCustomerPortfolio;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerPortfolioCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioDetails");
                db.AddInParameter(getCustomerPortfolioCmd, "@PortfolioId", DbType.Int32, portfolioId);

                dsGetCustomerPortfolio = db.ExecuteDataSet(getCustomerPortfolioCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioDao.cs:GetCustomerPortfolio()");


                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerPortfolio;
        }

        public DataTable GetRMCustomerPortfolios(int rmId, int currentPage, out int count, string nameSrchValue)
        {
            Database db;
            DbCommand getCustomerPortfoliosCmd;
            DataSet dsGetCustomerPortfolios;
            DataTable dtGetCustomerPortfolios;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerPortfoliosCmd = db.GetStoredProcCommand("SP_GetRMCustomerPortfolios");
                db.AddInParameter(getCustomerPortfoliosCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(getCustomerPortfoliosCmd, "@CurrentPage", DbType.Int32, currentPage);
                db.AddInParameter(getCustomerPortfoliosCmd, "@nameFilter", DbType.String, nameSrchValue);
                getCustomerPortfoliosCmd.CommandTimeout = 60 * 60;
                dsGetCustomerPortfolios = db.ExecuteDataSet(getCustomerPortfoliosCmd);
                dtGetCustomerPortfolios = dsGetCustomerPortfolios.Tables[0];
                count = int.Parse(dsGetCustomerPortfolios.Tables[1].Rows[0][0].ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioDao.cs:GetRMCustomerPortfolios()");


                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetCustomerPortfolios;
        }

        public static bool TransferFolio(int MFAccountId, int newPortfolioId)
        {
            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_Folios_Move");
                db.AddInParameter(updateCmd, "@AccountId", DbType.Int32, MFAccountId);
                db.AddInParameter(updateCmd, "@NewPortfolioId", DbType.Int32, newPortfolioId);

                affectedRows = db.ExecuteNonQuery(updateCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioDao.cs:TransferFolio()");

                object[] objects = new object[1];
                objects[0] = MFAccountId;
                objects[1] = newPortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            if (affectedRows > 0)
                return true;
            else
                return false;




        }

        public int CustomerPortfolioCheck(string association, string Flag)
        {

            Database db;
            DbCommand getCustomerListCmd;
            int associationcount = 0;



            db = DatabaseFactory.CreateDatabase("wealtherp");
            getCustomerListCmd = db.GetStoredProcCommand("SP_CustomerPortfolioCheck");
            db.AddInParameter(getCustomerListCmd, "@GaolIds", DbType.String, association);
            db.AddInParameter(getCustomerListCmd, "@Flag", DbType.String, Flag);
            db.AddOutParameter(getCustomerListCmd, "@CountFlag", DbType.Int32, 0);
            associationcount = db.ExecuteNonQuery(getCustomerListCmd);
            associationcount = (int)db.GetParameterValue(getCustomerListCmd, "@CountFlag");
            if (associationcount != 1)
                return 2;
            else return 1;

        }

        public int CustomerPortfolioMultiple(string association, string Flag)
        {

            Database db;
            DbCommand getCustomerListCmd;
            int associationcount = 0;



            db = DatabaseFactory.CreateDatabase("wealtherp");
            getCustomerListCmd = db.GetStoredProcCommand("SP_CustomerPortfolioCheck");
            db.AddInParameter(getCustomerListCmd, "@GaolIds", DbType.String, association);
            db.AddInParameter(getCustomerListCmd, "@Flag", DbType.String, Flag);
            db.AddOutParameter(getCustomerListCmd, "@CountFlag", DbType.Int32, 0);
            associationcount = db.ExecuteNonQuery(getCustomerListCmd);
            associationcount = (int)db.GetParameterValue(getCustomerListCmd, "@CountFlag");
            if (associationcount != 1)
                return 2;
            else return 1;

        }
        public DataSet CustomerPortfolioNumber(string association, string Flag)
        {

            Database db;
            DbCommand getCustomerListCmd;
            DataSet getCustomerDs;
            



            db = DatabaseFactory.CreateDatabase("wealtherp");
            getCustomerListCmd = db.GetStoredProcCommand("SP_CustomerPortfolioCheck");
            db.AddInParameter(getCustomerListCmd, "@GaolIds", DbType.String, association);
            db.AddInParameter(getCustomerListCmd, "@Flag", DbType.String, Flag);
            getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
            return getCustomerDs;
            

        }

        public int PortfolioDissociate(string association, string toPortfolio,string Flag)
        {

            Database db;
            DbCommand getCustomerListCmd;
            int associationcount = 0;



            db = DatabaseFactory.CreateDatabase("wealtherp");
            getCustomerListCmd = db.GetStoredProcCommand("SP_CustomerPortfolioDelete");
            db.AddInParameter(getCustomerListCmd, "@GaolIds", DbType.String, association);
            db.AddInParameter(getCustomerListCmd, "@Portfolio", DbType.String, toPortfolio);
            db.AddInParameter(getCustomerListCmd, "@Flag", DbType.String, Flag);
            db.AddOutParameter(getCustomerListCmd, "@CountFlag", DbType.Int32, 0);
            associationcount = db.ExecuteNonQuery(getCustomerListCmd);
            associationcount = (int)db.GetParameterValue(getCustomerListCmd, "@CountFlag");
            return 1;

        }


        public int PortfolioDissociateUnmanaged(string association, string Flag)
        {

            Database db;
            DbCommand getCustomerListCmd;
            int associationcount = 0;



            db = DatabaseFactory.CreateDatabase("wealtherp");
            getCustomerListCmd = db.GetStoredProcCommand("SP_CustomerPortfolioDelete");
            db.AddInParameter(getCustomerListCmd, "@GaolIds", DbType.String, association);
            db.AddInParameter(getCustomerListCmd, "@Flag", DbType.String, Flag);
            db.AddInParameter(getCustomerListCmd, "@Portfolio", DbType.String, " ");
            db.AddOutParameter(getCustomerListCmd, "@CountFlag", DbType.Int32, 0);
            associationcount = db.ExecuteNonQuery(getCustomerListCmd);
            associationcount = (int)db.GetParameterValue(getCustomerListCmd, "@CountFlag");
            return 1;

        }


        public int CustomerPortfolioDefault(string association, string Flag)
        {

            Database db;
            DbCommand getCustomerListCmd;
            int associationcount = 0;



            db = DatabaseFactory.CreateDatabase("wealtherp");
            getCustomerListCmd = db.GetStoredProcCommand("SP_CustomerPortfolioCheck");
            db.AddInParameter(getCustomerListCmd, "@GaolIds", DbType.String, association);
            db.AddInParameter(getCustomerListCmd, "@Flag", DbType.String, Flag);
            db.AddOutParameter(getCustomerListCmd, "@CountFlag", DbType.Int32, 0);
            associationcount = db.ExecuteNonQuery(getCustomerListCmd);
            associationcount = (int)db.GetParameterValue(getCustomerListCmd, "@CountFlag");
            if (associationcount != 1)
                return 2;
            else return 1;

        }
    }
}
