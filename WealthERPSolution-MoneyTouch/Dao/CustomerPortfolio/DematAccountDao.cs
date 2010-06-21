using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data.Common;
using VoUser;
using VoCustomerPortfolio;
using System.Collections;



namespace DaoCustomerPortfolio
{
    public class DematAccountDao
    {
        
        public DataSet GetCustomerAccociation(CustomerVo customervo)
        {
            DataSet datasetCustomerAssociationList=null;
            Database db;
            DbCommand dbCustomerAssociationList;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCustomerAssociationList = db.GetStoredProcCommand("SP_GetCustomerAssociatesRel");
                db.AddInParameter(dbCustomerAssociationList, "@C_CustomerId", DbType.Int32, customervo.CustomerId);
                datasetCustomerAssociationList = db.ExecuteDataSet(dbCustomerAssociationList);
            }            
            catch(BaseApplicationException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message , ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerListDataSet()");

                object[] objects = new object[1];
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return datasetCustomerAssociationList;

            
        }

        public DataSet GetTradeAccountNumber(CustomerVo customervo)
        {
            DataSet dsTradeAccount=null;
            Database db;
            DbCommand dbcommandTradeAccount;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbcommandTradeAccount = db.GetStoredProcCommand("SP_GetTradeAccountNumbersByCustomer");
                db.AddInParameter(dbcommandTradeAccount, "@CustomerId", DbType.Int32, customervo.CustomerId);
                dsTradeAccount = db.ExecuteDataSet(dbcommandTradeAccount);

            }            
            catch(BaseApplicationException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerListDataSet()");

                object[] objects = new object[1];
                objects[0]=customervo.CustomerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsTradeAccount;
        }
        public DataSet GetAvailableTrades(int customerId,int dematAccountId)
        {
            DataSet dsGetAvailableTrades = null;
            Database db;
            DbCommand dbcommandGetAvailableTrades;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbcommandGetAvailableTrades = db.GetStoredProcCommand("SP_GetAvailableTrades");
                db.AddInParameter(dbcommandGetAvailableTrades, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(dbcommandGetAvailableTrades, "@DematAccountId", DbType.Int32, dematAccountId);
                dsGetAvailableTrades = db.ExecuteDataSet(dbcommandGetAvailableTrades);

            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAvailableTrades()");

                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = dematAccountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAvailableTrades;
        }
        public DataSet GetXmlModeOfHolding()
        {
            DataSet dsTradeAccount = null;
            Database db;
            DbCommand dbcommandTradeAccount;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbcommandTradeAccount = db.GetStoredProcCommand("SP_GetModeOfHolding");
                dsTradeAccount = db.ExecuteDataSet(dbcommandTradeAccount);

            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerListDataSet()");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsTradeAccount;
        }
        public void AddDematDetails(int customerId, int portfolioId, DematAccountVo demataccountvo, RMVo rmvo, ArrayList associationIdJH, ArrayList associationIdN, ArrayList lstassociatedtradeaccount)
        {
            //DataSet dsDematDetails = null;
            Database db;
            DbCommand dbDematDetails;
            DbCommand dbAssociationTypes=null;
            DbCommand dbTradeAccount=null;
            int dematAccountId=0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbDematDetails = db.GetStoredProcCommand("SP_AddDematDetails");               

                db.AddInParameter(dbDematDetails,"@CP_PortfolioId",DbType.Int32,portfolioId);
                db.AddInParameter(dbDematDetails,"@CEDA_DPClientId",DbType.String,demataccountvo.DpclientId);
                db.AddInParameter(dbDematDetails,"@CEDA_DPId",DbType.String,demataccountvo.DpId);
                db.AddInParameter(dbDematDetails,"@CEDA_DPName",DbType.String,demataccountvo.DpName);
                db.AddInParameter(dbDematDetails,"@XMOH_ModeOfHoldingCode",DbType.String,demataccountvo.ModeOfHolding);
                db.AddInParameter(dbDematDetails,"@CEDA_IsJointlyHeld",DbType.Int16,demataccountvo.IsHeldJointly);
                db.AddInParameter(dbDematDetails,"@CEDA_AccountOpeningDate",DbType.DateTime,demataccountvo.AccountOpeningDate);
                db.AddInParameter(dbDematDetails,"@CEDA_BeneficiaryAccountNum",DbType.String,demataccountvo.BeneficiaryAccountNbr);
                db.AddInParameter(dbDematDetails,"@CEDA_CreatedBy",DbType.Int32,rmvo.RMId);
                db.AddInParameter(dbDematDetails,"@CEDA_ModifiedBy",DbType.Int32,rmvo.RMId);
                db.AddInParameter(dbDematDetails,"@C_CustomerId",DbType.Int32,customerId);
                db.AddOutParameter(dbDematDetails,"@CEDA_DematAccountId",DbType.Int32,10000);
                db.ExecuteNonQuery(dbDematDetails);
                dematAccountId = int.Parse(db.GetParameterValue(dbDematDetails, "@CEDA_DematAccountId").ToString());
                
                
                IEnumerator enumassociationIdJH = associationIdJH.GetEnumerator();
                while (enumassociationIdJH.MoveNext())
                {
                   
                    Object obj = enumassociationIdJH.Current;
                    dbAssociationTypes = db.GetStoredProcCommand("SP_AddAssociationTypesForDemat");
                    db.AddInParameter(dbAssociationTypes, "@CEDA_DematAccountId", DbType.Int32, dematAccountId);
                    db.AddInParameter(dbAssociationTypes, "@CAS_AssociationId", DbType.Int32, int.Parse(obj.ToString()));                    
                    db.AddInParameter(dbAssociationTypes, "@CEDAA_AssociationType", DbType.String, "JH");
                    db.AddInParameter(dbAssociationTypes, "@CEDAA_CreatedBy", DbType.String, rmvo.RMId);
                    db.AddInParameter(dbAssociationTypes, "@CEDAA_ModifiedBy", DbType.String, rmvo.RMId);
                    db.ExecuteNonQuery(dbAssociationTypes);
                }

                
                IEnumerator enumassociationIdN = associationIdN.GetEnumerator();

                while (enumassociationIdN.MoveNext())
                    {
                        dbAssociationTypes = db.GetStoredProcCommand("SP_AddAssociationTypesForDemat");
                        Object obj = enumassociationIdN.Current;
                        db.AddInParameter(dbAssociationTypes, "@CEDA_DematAccountId", DbType.Int32, dematAccountId);
                        db.AddInParameter(dbAssociationTypes, "@CAS_AssociationId", DbType.Int32, int.Parse(obj.ToString()));                        
                        db.AddInParameter(dbAssociationTypes, "@CEDAA_AssociationType", DbType.String, "N");
                        db.AddInParameter(dbAssociationTypes, "@CEDAA_CreatedBy", DbType.String, rmvo.RMId);
                        db.AddInParameter(dbAssociationTypes, "@CEDAA_ModifiedBy", DbType.String, rmvo.RMId);
                        db.ExecuteNonQuery(dbAssociationTypes);
                    }
                


               
                IEnumerator enumlstassociatedtradeaccount = lstassociatedtradeaccount.GetEnumerator();
                while (enumlstassociatedtradeaccount.MoveNext())
                {
                    dbTradeAccount = db.GetStoredProcCommand("SP_AddTradeAccountForDemat");
                
                    Object obj = enumlstassociatedtradeaccount.Current;
                    db.AddInParameter(dbTradeAccount, "@CEDA_DematAccountId", DbType.Int32, dematAccountId);
                    db.AddInParameter(dbTradeAccount, "@CETA_AccountId", DbType.Int32, int.Parse(obj.ToString()));
                    db.AddInParameter(dbTradeAccount, "@CETDAA_IsDefault", DbType.Int16, 1);
                    db.AddInParameter(dbTradeAccount, "@CEDA_CreatedBy", DbType.Int32, rmvo.RMId);
                    db.AddInParameter(dbTradeAccount, "@CEDA_ModifiedBy", DbType.Int32, rmvo.RMId);
                    db.ExecuteNonQuery(dbTradeAccount);
                }

            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:AddDematDetails()");

                object[] objects = new object[2];
                objects[0] = customerId;
                objects[0] = dematAccountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        # region Dont need
        //public void AddAssociationTypesForDemat(int associationId, string associationtype)
        //{
        //    //DataSet dsDematDetails = null;
        //    Database db;
        //    DbCommand dbDematDetails;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        dbDematDetails = db.GetStoredProcCommand("SP_AddAssociationTypesForDemat");
        //        db.AddInParameter(dbDematDetails, "@CAS_AssociationId", DbType.Int32, associationId);
        //        db.AddInParameter(dbDematDetails, "@CEDAA_AssociationType", DbType.String,associationtype);
        //        db.ExecuteNonQuery(dbDematDetails);

        //    }
        //    catch (BaseApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerListDataSet()");

        //        object[] objects = new object[1];
        //        objects[0] = associationId;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}
        //public void AddTradeAccountForDemat(int accountId)
        //{
        //    //DataSet dsDematDetails = null;
        //    Database db;
        //    DbCommand dbDematDetails;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        dbDematDetails = db.GetStoredProcCommand("SP_AddTradeAccountForDemat");
        //        db.AddInParameter(dbDematDetails, "@CETA_AccountId", DbType.Int32, accountId);
        //        db.AddInParameter(dbDematDetails, "@CETDAA_IsDefault", DbType.Int16, 1);
        //        db.ExecuteNonQuery(dbDematDetails);

        //    }
        //    catch (BaseApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerListDataSet()");

        //        object[] objects = new object[1];
        //        objects[0] = accountId;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}
        //
        // We are getting details for Viewing Demat details in Grid which is in DematAccountDetails.ascx
        //
        //
        //
        //
# endregion
        public DataSet GetDematDetails(int customerId,int dematId)
        {
            DataSet datasetDematDetails = null;
            Database db;
            DbCommand dbDematDetails;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbDematDetails = db.GetStoredProcCommand("SP_GetDematDetails");
                db.AddInParameter(dbDematDetails, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(dbDematDetails, "@CEDA_DematAccountId", DbType.Int32, dematId);
                datasetDematDetails = db.ExecuteDataSet(dbDematDetails);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerListDataSet()");

                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = dematId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return datasetDematDetails;


        }


        public DataSet GetDematAccountHolderDetails(int customerId)
        {
            DataSet datasetDematDetails = null;
            
            Database db;
            DbCommand dbDematDetails;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbDematDetails = db.GetStoredProcCommand("SP_GetDematAccountHolderDetails");
                db.AddInParameter(dbDematDetails, "@C_CustomerId", DbType.Int32, customerId);
                
                datasetDematDetails = db.ExecuteDataSet(dbDematDetails);
                
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:GetDematAccountHolderDetails()");

                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return datasetDematDetails;


        }


        //
        // We are getting details for Viewing Demat details in Seperate Page
        //
        //
        //
        //
        public DataSet GetDematAccountDetails(int demataccountId)
        {
            DataSet datasetDematAccountDetails = null;
            Database db;
            DbCommand dbDematAccountDetails;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbDematAccountDetails = db.GetStoredProcCommand("SP_GetDematAccountDetails");
                db.AddInParameter(dbDematAccountDetails, "@CEDA_DematAccountId", DbType.Int32, demataccountId);
                datasetDematAccountDetails = db.ExecuteDataSet(dbDematAccountDetails);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerListDataSet()");

                object[] objects = new object[1];
                objects[0] = demataccountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return datasetDematAccountDetails;


        }
        public DataSet GetJointHoldersAndNominees(int demataccountId)
        {
            DataSet datasetJointHoldersAndNominees = null;
            Database db;
            DbCommand dbJointHoldersAndNominees;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbJointHoldersAndNominees = db.GetStoredProcCommand("SP_GetJointHoldersAndNominees ");
                db.AddInParameter(dbJointHoldersAndNominees, "@CEDA_DematAccountId", DbType.Int32, demataccountId);
                datasetJointHoldersAndNominees = db.ExecuteDataSet(dbJointHoldersAndNominees);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerListDataSet()");

                object[] objects = new object[1];
                objects[0] = demataccountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return datasetJointHoldersAndNominees;


        }
        # region Dont need
        //public void UpdateDematDetails(int customerId,int portfolioId, DematAccountVo demataccountvo, RMVo rmvo)
        //{
        //    //DataSet dsDematDetails = null;
        //    Database db;
        //    DbCommand dbDematDetails;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        dbDematDetails = db.GetStoredProcCommand("SP_AddDematDetails");
        //        db.AddInParameter(dbDematDetails, "@CP_PortfolioId", DbType.Int32, portfolioId);
        //        db.AddInParameter(dbDematDetails, "@CEDA_DPClientId", DbType.String, demataccountvo.DpclientId);
        //        db.AddInParameter(dbDematDetails, "@CEDA_DPId", DbType.String, demataccountvo.DpId);
        //        db.AddInParameter(dbDematDetails, "@CEDA_DPName", DbType.String, demataccountvo.DpName);
        //        db.AddInParameter(dbDematDetails, "@XMOH_ModeOfHoldingCode", DbType.String, demataccountvo.ModeOfHolding);
        //        db.AddInParameter(dbDematDetails, "@CEDA_IsJointlyHeld", DbType.Int16, demataccountvo.IsHeldJointly);
        //        db.AddInParameter(dbDematDetails, "@CEDA_AccountOpeningDate", DbType.DateTime, demataccountvo.AccountOpeningDate);
        //        db.AddInParameter(dbDematDetails, "@CEDA_BeneficiaryAccountNum", DbType.String, demataccountvo.BeneficiaryAccountNbr);
        //        db.AddInParameter(dbDematDetails, "@CEDA_CreatedBy", DbType.Int32, rmvo.RMId);
        //        db.AddInParameter(dbDematDetails, "@CEDA_ModifiedBy", DbType.Int32, rmvo.RMId);
        //        db.AddOutParameter(dbDematDetails, "@CEDA_DematAccountId", DbType.Int32, 20000);
        //        db.ExecuteNonQuery(dbDematDetails);

        //    }
        //    catch (BaseApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerListDataSet()");

        //        object[] objects = new object[1];
        //        objects[0] = portfolioId;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}
        //public void UpdateAssociationTypesForDemat(int associationId, string associationtype)
        //{
        //    //DataSet dsDematDetails = null;
        //    Database db;
        //    DbCommand dbDematDetails;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        dbDematDetails = db.GetStoredProcCommand("SP_AddAssociationTypesForDemat");
        //        db.AddInParameter(dbDematDetails, "@CAS_AssociationId", DbType.Int32, associationId);
        //        db.AddInParameter(dbDematDetails, "@CEDAA_AssociationType", DbType.String, associationtype);
        //        db.ExecuteNonQuery(dbDematDetails);

        //    }
        //    catch (BaseApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerListDataSet()");

        //        object[] objects = new object[1];
        //        objects[0] = associationId;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}
        //public void UpdateTradeAccountForDemat(int accountId)
        //{
        //    //DataSet dsDematDetails = null;
        //    Database db;
        //    DbCommand dbDematDetails;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        dbDematDetails = db.GetStoredProcCommand("SP_AddTradeAccountForDemat");
        //        db.AddInParameter(dbDematDetails, "@CETA_AccountId", DbType.Int32, accountId);
        //        db.AddInParameter(dbDematDetails, "@CETDAA_IsDefault", DbType.Int16, 1);
        //        db.ExecuteNonQuery(dbDematDetails);

        //    }
        //    catch (BaseApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerListDataSet()");

        //        object[] objects = new object[1];
        //        objects[0] = accountId;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

# endregion
        public void UpdateDematDetails(int customerId, int portfolioId,int dematId, DematAccountVo demataccountvo, RMVo rmvo, ArrayList associationIdJH, ArrayList associationIdN, ArrayList lstassociatedtradeaccount)
        {
            //DataSet dsDematDetails = null;
            Database db;
            DbCommand dbDematDetails;
            DbCommand dbAssociationTypes = null;
            DbCommand dbTradeAccount = null;
            DbCommand dbDeleteAssociates = null;
            int dematAccountId = dematId;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbDematDetails = db.GetStoredProcCommand("SP_UpdateDematDetails");

                db.AddInParameter(dbDematDetails, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(dbDematDetails, "@CEDA_DematAccountId", DbType.Int32, dematId);
                db.AddInParameter(dbDematDetails, "@CEDA_DPClientId", DbType.String, demataccountvo.DpclientId);
                db.AddInParameter(dbDematDetails, "@CEDA_DPId", DbType.String, demataccountvo.DpId);
                db.AddInParameter(dbDematDetails, "@CEDA_DPName", DbType.String, demataccountvo.DpName);
                db.AddInParameter(dbDematDetails, "@XMOH_ModeOfHoldingCode", DbType.String, demataccountvo.ModeOfHolding);
                db.AddInParameter(dbDematDetails, "@CEDA_IsJointlyHeld", DbType.Int16, demataccountvo.IsHeldJointly);
                db.AddInParameter(dbDematDetails, "@CEDA_AccountOpeningDate", DbType.DateTime, demataccountvo.AccountOpeningDate);
                db.AddInParameter(dbDematDetails, "@CEDA_BeneficiaryAccountNum", DbType.String, demataccountvo.BeneficiaryAccountNbr);              
                db.AddInParameter(dbDematDetails, "@C_CustomerId", DbType.Int32, customerId);                
                db.ExecuteNonQuery(dbDematDetails);

                dbDeleteAssociates = db.GetStoredProcCommand("SP_DeleteDematAndTradeAssociates");
                db.AddInParameter(dbDeleteAssociates, "@DematAccountId", DbType.Int32, dematAccountId);
                db.ExecuteNonQuery(dbDeleteAssociates);

                if (associationIdJH.Count != 0)
                {
                    IEnumerator enumassociationIdJH = associationIdJH.GetEnumerator();
                    while (enumassociationIdJH.MoveNext())
                    {

                        Object obj = enumassociationIdJH.Current;
                        dbAssociationTypes = db.GetStoredProcCommand("SP_AddAssociationTypesForDemat");
                        db.AddInParameter(dbAssociationTypes, "@CEDA_DematAccountId", DbType.Int32, dematAccountId);
                        db.AddInParameter(dbAssociationTypes, "@CAS_AssociationId", DbType.Int32, int.Parse(obj.ToString()));
                        db.AddInParameter(dbAssociationTypes, "@CEDAA_AssociationType", DbType.String, "JH");
                        db.AddInParameter(dbAssociationTypes, "@CEDAA_CreatedBy", DbType.String, rmvo.RMId);
                        db.AddInParameter(dbAssociationTypes, "@CEDAA_ModifiedBy", DbType.String, rmvo.RMId);
                        db.ExecuteNonQuery(dbAssociationTypes);
                    }
                }

                if (associationIdN.Count != 0)
                {

                    IEnumerator enumassociationIdN = associationIdN.GetEnumerator();

                    while (enumassociationIdN.MoveNext())
                    {
                        dbAssociationTypes = db.GetStoredProcCommand("SP_AddAssociationTypesForDemat");
                        Object obj = enumassociationIdN.Current;
                        db.AddInParameter(dbAssociationTypes, "@CEDA_DematAccountId", DbType.Int32, dematAccountId);
                        db.AddInParameter(dbAssociationTypes, "@CAS_AssociationId", DbType.Int32, int.Parse(obj.ToString()));
                        db.AddInParameter(dbAssociationTypes, "@CEDAA_AssociationType", DbType.String, "N");
                        db.AddInParameter(dbAssociationTypes, "@CEDAA_CreatedBy", DbType.String, rmvo.RMId);
                        db.AddInParameter(dbAssociationTypes, "@CEDAA_ModifiedBy", DbType.String, rmvo.RMId);
                        db.ExecuteNonQuery(dbAssociationTypes);
                    }


                }
                if (lstassociatedtradeaccount.Count != 0)
                {

                    IEnumerator enumlstassociatedtradeaccount = lstassociatedtradeaccount.GetEnumerator();
                    while (enumlstassociatedtradeaccount.MoveNext())
                    {
                        dbTradeAccount = db.GetStoredProcCommand("SP_AddTradeAccountForDemat");

                        Object obj = enumlstassociatedtradeaccount.Current;
                        db.AddInParameter(dbTradeAccount, "@CEDA_DematAccountId", DbType.Int32, dematAccountId);
                        db.AddInParameter(dbTradeAccount, "@CETA_AccountId", DbType.Int32, int.Parse(obj.ToString()));
                        db.AddInParameter(dbTradeAccount, "@CETDAA_IsDefault", DbType.Int16, 1);
                        db.AddInParameter(dbTradeAccount, "@CEDA_CreatedBy", DbType.Int32, rmvo.RMId);
                        db.AddInParameter(dbTradeAccount, "@CEDA_ModifiedBy", DbType.Int32, rmvo.RMId);
                        db.ExecuteNonQuery(dbTradeAccount);
                    }
                }
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:UpdateDEmatDEtails()");

                object[] objects = new object[2];
                objects[0] = customerId;
                objects[0] = dematAccountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}
