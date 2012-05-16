using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoOps;

namespace DaoOps
{
    public class OrderDao
    {
        public DataTable GetBankAccountDetails(int customerId)
        {
            Database db;
            DataSet getCustomerBankDs = new DataSet();
            DbCommand getCustomerBankCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerBankCmd = db.GetStoredProcCommand("SP_GetBankAccountDetails");
                db.AddInParameter(getCustomerBankCmd, "@C_CustomerId", DbType.Int32, customerId);
                getCustomerBankDs = db.ExecuteDataSet(getCustomerBankCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:GetCustomerBankAccountDetails()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getCustomerBankDs.Tables[0];
        }


        public DataTable GetBankBranch(int BankAccId)
        {
            Database db;
            DataSet getCustomerBankDs;
            DataTable dt = new DataTable();
            DbCommand getCustomerBankCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerBankCmd = db.GetStoredProcCommand("SP_GetBankBranchDetails");
                db.AddInParameter(getCustomerBankCmd, "@BankAccId", DbType.Int32, BankAccId);
                getCustomerBankDs = db.ExecuteDataSet(getCustomerBankCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:GetBankBranch()");

                object[] objects = new object[1];
                objects[0] = BankAccId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getCustomerBankDs.Tables[0];
        }

        public DataTable GetPaymentMode()
        {
            DataSet dsPaymentMode = null;
            DataTable dtPaymentMode;
            Database db;
            DbCommand dbcommandTradeAccount;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbcommandTradeAccount = db.GetStoredProcCommand("SP_GetPaymentMode");
                dsPaymentMode = db.ExecuteDataSet(dbcommandTradeAccount);
                dtPaymentMode = dsPaymentMode.Tables[0];
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:GetPaymentMode()");

                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtPaymentMode;
        }

        public DataTable GetAssetParticular(string IssuerCode)
        {
            DataSet dsAssetParticular = null;
            DataTable dtAssetParticular;
            Database db;
            DbCommand dbAssetParticularCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbAssetParticularCmd = db.GetStoredProcCommand("SP_GetAssetParticular");
                db.AddInParameter(dbAssetParticularCmd, "@InsIssuerCode", DbType.String, IssuerCode);
                dsAssetParticular = db.ExecuteDataSet(dbAssetParticularCmd);
                dtAssetParticular = dsAssetParticular.Tables[0];
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:GetAssetParticular()");

                object[] objects = new object[1];
                objects[0] = IssuerCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAssetParticular;
        }

        public void InsertAssetParticularScheme(string assetText, string InsuranceIssuerCode)
        {
            Database db;
            DbCommand cmdInsertAssetParticular;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertAssetParticular = db.GetStoredProcCommand("SP_InsertAssetParticularScheme");
                db.AddInParameter(cmdInsertAssetParticular, "@assetText", DbType.String, assetText);
                db.AddInParameter(cmdInsertAssetParticular, "@InsuranceIssuerCode", DbType.String, InsuranceIssuerCode);
                db.ExecuteNonQuery(cmdInsertAssetParticular);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public bool AddLifeInsuranceOrder(LifeInsuranceOrderVo lifeInsuranceOrdervo)
        {
            Database db;
            DbCommand LifeInsuranceOrderCmd;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                LifeInsuranceOrderCmd = db.GetStoredProcCommand("SP_CreateLifeInsuranceOrder");

                //db.AddInParameter(LifeInsuranceOrderCmd, "@OrderNumber", DbType.Int32, operationVo.OrderNumber);
                db.AddInParameter(LifeInsuranceOrderCmd, "@ApplicationNumber", DbType.String, lifeInsuranceOrdervo.ApplicationNumber);
                db.AddInParameter(LifeInsuranceOrderCmd, "@CustomerId", DbType.Int32, lifeInsuranceOrdervo.CustomerId);
                db.AddInParameter(LifeInsuranceOrderCmd, "@PaymentMode", DbType.String, lifeInsuranceOrdervo.PaymentMode);
                db.AddInParameter(LifeInsuranceOrderCmd, "@ChequeNumber", DbType.String, lifeInsuranceOrdervo.ChequeNumber);
                db.AddInParameter(LifeInsuranceOrderCmd, "@CustBankAccId", DbType.String, lifeInsuranceOrdervo.CustBankAccId);
                db.AddInParameter(LifeInsuranceOrderCmd, "@SumAssured", DbType.Double, lifeInsuranceOrdervo.SumAssured);
                db.AddInParameter(LifeInsuranceOrderCmd, "@FrequencyCode", DbType.String, lifeInsuranceOrdervo.FrequencyCode);
                db.AddInParameter(LifeInsuranceOrderCmd, "@ModeOfHolding", DbType.String, lifeInsuranceOrdervo.HoldingMode);
                db.AddInParameter(LifeInsuranceOrderCmd, "@IsJointlyHeld", DbType.String, lifeInsuranceOrdervo.IsJointlyHeld);
                db.AddInParameter(LifeInsuranceOrderCmd, "@InsuranceSchemeid", DbType.Int32, lifeInsuranceOrdervo.InsuranceSchemeId); 
                db.AddInParameter(LifeInsuranceOrderCmd, "@AssetCategory", DbType.String, lifeInsuranceOrdervo.AssetCategory);

                db.AddInParameter(LifeInsuranceOrderCmd, "@SourceCode", DbType.String, lifeInsuranceOrdervo.SourceCode);
                db.AddInParameter(LifeInsuranceOrderCmd, "@WOS_OrderStepCode", DbType.String, lifeInsuranceOrdervo.OrderStepCode);

                db.AddInParameter(LifeInsuranceOrderCmd, "@XII_InsuranceIssuerCode", DbType.String, lifeInsuranceOrdervo.InsuranceIssuerCode);
                db.AddInParameter(LifeInsuranceOrderCmd, "@XGII_GIIssuerCode", DbType.String, lifeInsuranceOrdervo.GIIssuerCode);
                db.AddInParameter(LifeInsuranceOrderCmd, "@XS_StatusCode", DbType.String, lifeInsuranceOrdervo.OrderStatusCode);
                db.AddInParameter(LifeInsuranceOrderCmd, "@XSR_StatusReasonCode", DbType.String, lifeInsuranceOrdervo.ReasonCode);
                db.AddInParameter(LifeInsuranceOrderCmd, "@ApprovedByID", DbType.Int32, lifeInsuranceOrdervo.ApprovedBy);
                db.AddInParameter(LifeInsuranceOrderCmd, "@CA_AssociationId", DbType.Int32, lifeInsuranceOrdervo.AssociationId);
                db.AddInParameter(LifeInsuranceOrderCmd, "@AssociationType", DbType.String, lifeInsuranceOrdervo.AssociationType);

                if (lifeInsuranceOrdervo.OrderDate != DateTime.MinValue)
                    db.AddInParameter(LifeInsuranceOrderCmd, "@CO_OrderDate", DbType.DateTime, lifeInsuranceOrdervo.OrderDate);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@CO_OrderDate", DbType.DateTime, DBNull.Value);

                if (lifeInsuranceOrdervo.ApplicationReceivedDate != DateTime.MinValue)
                    db.AddInParameter(LifeInsuranceOrderCmd, "@ApplicationReceivedDate", DbType.DateTime, lifeInsuranceOrdervo.ApplicationReceivedDate);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@ApplicationReceivedDate", DbType.DateTime, DBNull.Value);

                if (lifeInsuranceOrdervo.PaymentDate != DateTime.MinValue)
                    db.AddInParameter(LifeInsuranceOrderCmd, "@PaymentDate", DbType.DateTime, lifeInsuranceOrdervo.PaymentDate);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@PaymentDate", DbType.DateTime, DBNull.Value);

                if (lifeInsuranceOrdervo.MaturityDate != DateTime.MinValue)
                    db.AddInParameter(LifeInsuranceOrderCmd, "@MaturityDate", DbType.DateTime, lifeInsuranceOrdervo.MaturityDate);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@MaturityDate", DbType.DateTime, DBNull.Value);

                db.AddOutParameter(LifeInsuranceOrderCmd, "@CO_OrderId", DbType.Int32, lifeInsuranceOrdervo.OrderId);

                if (db.ExecuteNonQuery(LifeInsuranceOrderCmd) != 0)
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
                FunctionInfo.Add("Method", "OrderDao.cs:CreateInsurancePortfolio()");
                object[] objects = new object[1];
                objects[0] = lifeInsuranceOrdervo;
                //objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            //   return bResult;
            return bResult;
        }

        //public DataSet GetLifeInsuranceOrderDetails(LifeInsuranceOrderVo lifeInsuranceOrdervo)
        //{
        //    DataSet dsLIOrder = null;
        //    DataTable dtLIOrder = null; 
        //    Database db;
        //    DbCommand dbLIOrdercmd;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        dbLIOrdercmd = db.GetStoredProcCommand("SP_GetPaymentMode");

        //        //db.AddInParameter(dbLIOrdercmd, "@OrderNumber", DbType.Int32, operationVo.OrderNumber);
        //        db.AddOutParameter(dbLIOrdercmd, "@ApplicationNumber", DbType.String, lifeInsuranceOrdervo.ApplicationNumber);
        //        db.AddOutParameter(dbLIOrdercmd, "@CustomerId", DbType.Int32, lifeInsuranceOrdervo.CustomerId);
        //        db.AddOutParameter(dbLIOrdercmd, "@PaymentMode", DbType.String, lifeInsuranceOrdervo.PaymentMode);
        //        db.AddOutParameter(dbLIOrdercmd, "@ChequeNumber", DbType.String, lifeInsuranceOrdervo.ChequeNumber);
        //        db.AddOutParameter(dbLIOrdercmd, "@CustBankAccId", DbType.String, lifeInsuranceOrdervo.CustBankAccId);
        //        db.AddOutParameter(dbLIOrdercmd, "@SumAssured", DbType.Double, lifeInsuranceOrdervo.SumAssured);
        //        db.AddOutParameter(dbLIOrdercmd, "@FrequencyCode", DbType.String, lifeInsuranceOrdervo.FrequencyCode);
        //        db.AddOutParameter(dbLIOrdercmd, "@ModeOfHolding", DbType.String, lifeInsuranceOrdervo.HoldingMode);
        //        db.AddOutParameter(dbLIOrdercmd, "@IsJointlyHeld", DbType.String, lifeInsuranceOrdervo.IsJointlyHeld);
        //        db.AddOutParameter(dbLIOrdercmd, "@InsuranceSchemeid", DbType.Int32, lifeInsuranceOrdervo.InsuranceSchemeId);
        //        db.AddOutParameter(dbLIOrdercmd, "@AssetCategory", DbType.String, lifeInsuranceOrdervo.AssetCategory);
        //        db.AddOutParameter(dbLIOrdercmd, "@SourceCode", DbType.String, lifeInsuranceOrdervo.SourceCode);
        //        db.AddOutParameter(dbLIOrdercmd, "@XII_InsuranceIssuerCode", DbType.String, lifeInsuranceOrdervo.InsuranceIssuerCode);
        //        db.AddOutParameter(dbLIOrdercmd, "@XGII_GIIssuerCode", DbType.String, lifeInsuranceOrdervo.GIIssuerCode);
        //        db.AddOutParameter(dbLIOrdercmd, "@WOS_OrderStepCode", DbType.String, lifeInsuranceOrdervo.OrderStepCode);
        //        db.AddOutParameter(dbLIOrdercmd, "@XS_StatusCode", DbType.String, lifeInsuranceOrdervo.OrderStatusCode);
        //        db.AddOutParameter(dbLIOrdercmd, "@XSR_StatusReasonCode", DbType.String, lifeInsuranceOrdervo.ReasonCode);
        //        db.AddOutParameter(dbLIOrdercmd, "@ApprovedByID", DbType.Int32, lifeInsuranceOrdervo.ApprovedBy);
        //        db.AddOutParameter(dbLIOrdercmd, "@CA_AssociationId", DbType.Int32, lifeInsuranceOrdervo.AssociationId);
        //        db.AddOutParameter(dbLIOrdercmd, "@AssociationType", DbType.String, lifeInsuranceOrdervo.AssociationType);

        //        if (lifeInsuranceOrdervo.OrderDate != DateTime.MinValue)
        //            db.AddOutParameter(dbLIOrdercmd, "@CO_OrderDate", DbType.DateTime, lifeInsuranceOrdervo.OrderDate);
        //        else
        //            db.AddOutParameter(dbLIOrdercmd, "@CO_OrderDate", DbType.DateTime, DBNull.Value);

        //        if (lifeInsuranceOrdervo.ApplicationReceivedDate != DateTime.MinValue)
        //            db.AddOutParameter(dbLIOrdercmd, "@ApplicationReceivedDate", DbType.DateTime, lifeInsuranceOrdervo.ApplicationReceivedDate);
        //        else
        //            db.AddOutParameter(dbLIOrdercmd, "@ApplicationReceivedDate", DbType.DateTime, DBNull.Value);

        //        if (lifeInsuranceOrdervo.PaymentDate != DateTime.MinValue)
        //            db.AddOutParameter(dbLIOrdercmd, "@PaymentDate", DbType.DateTime, lifeInsuranceOrdervo.PaymentDate);
        //        else
        //            db.AddOutParameter(dbLIOrdercmd, "@PaymentDate", DbType.DateTime, DBNull.Value);

        //        if (lifeInsuranceOrdervo.MaturityDate != DateTime.MinValue)
        //            db.AddOutParameter(dbLIOrdercmd, "@MaturityDate", DbType.DateTime, lifeInsuranceOrdervo.MaturityDate);
        //        else
        //            db.AddOutParameter(dbLIOrdercmd, "@MaturityDate", DbType.DateTime, DBNull.Value);


        //        dsLIOrder = db.ExecuteDataSet(dbLIOrdercmd);
        //        dtLIOrder = dsLIOrder.Tables[0];
        //    }
        //    catch (BaseApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "OrderDao.cs:GetPaymentMode()");

        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return dsLIOrder;
        //}

        //public DataSet EditLifeInsuranceOrderDetails(LifeInsuranceOrderVo lifeInsuranceOrdervo)
        //{
        //    DataSet dsLIOrder = null;
        //    DataTable dtLIOrder = null;
        //    Database db;
        //    DbCommand dbLIOrdercmd;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        dbLIOrdercmd = db.GetStoredProcCommand("SP_EditLifeInsuranceOrderDetails");

        //        //db.AddInParameter(dbLIOrdercmd, "@OrderNumber", DbType.Int32, operationVo.OrderNumber);
        //        db.AddInParameter(dbLIOrdercmd, "@ApplicationNumber", DbType.String, lifeInsuranceOrdervo.ApplicationNumber);
        //        db.AddInParameter(dbLIOrdercmd, "@CustomerId", DbType.Int32, lifeInsuranceOrdervo.CustomerId);
        //        db.AddInParameter(dbLIOrdercmd, "@PaymentMode", DbType.String, lifeInsuranceOrdervo.PaymentMode);
        //        db.AddInParameter(dbLIOrdercmd, "@ChequeNumber", DbType.String, lifeInsuranceOrdervo.ChequeNumber);
        //        db.AddInParameter(dbLIOrdercmd, "@CustBankAccId", DbType.String, lifeInsuranceOrdervo.CustBankAccId);
        //        db.AddInParameter(dbLIOrdercmd, "@SumAssured", DbType.Double, lifeInsuranceOrdervo.SumAssured);
        //        db.AddInParameter(dbLIOrdercmd, "@FrequencyCode", DbType.String, lifeInsuranceOrdervo.FrequencyCode);
        //        db.AddInParameter(dbLIOrdercmd, "@ModeOfHolding", DbType.String, lifeInsuranceOrdervo.HoldingMode);
        //        db.AddInParameter(dbLIOrdercmd, "@IsJointlyHeld", DbType.String, lifeInsuranceOrdervo.IsJointlyHeld);
        //        db.AddInParameter(dbLIOrdercmd, "@InsuranceSchemeid", DbType.Int32, lifeInsuranceOrdervo.InsuranceSchemeId);
        //        db.AddInParameter(dbLIOrdercmd, "@AssetCategory", DbType.String, lifeInsuranceOrdervo.AssetCategory);
        //        db.AddInParameter(dbLIOrdercmd, "@SourceCode", DbType.String, lifeInsuranceOrdervo.SourceCode);
        //        db.AddInParameter(dbLIOrdercmd, "@XII_InsuranceIssuerCode", DbType.String, lifeInsuranceOrdervo.InsuranceIssuerCode);
        //        db.AddInParameter(dbLIOrdercmd, "@XGII_GIIssuerCode", DbType.String, lifeInsuranceOrdervo.GIIssuerCode);
        //        db.AddInParameter(dbLIOrdercmd, "@WOS_OrderStepCode", DbType.String, lifeInsuranceOrdervo.OrderStepCode);
        //        db.AddInParameter(dbLIOrdercmd, "@XS_StatusCode", DbType.String, lifeInsuranceOrdervo.OrderStatusCode);
        //        db.AddInParameter(dbLIOrdercmd, "@XSR_StatusReasonCode", DbType.String, lifeInsuranceOrdervo.ReasonCode);
        //        db.AddInParameter(dbLIOrdercmd, "@ApprovedByID", DbType.Int32, lifeInsuranceOrdervo.ApprovedBy);
        //        db.AddInParameter(dbLIOrdercmd, "@CA_AssociationId", DbType.Int32, lifeInsuranceOrdervo.AssociationId);
        //        db.AddInParameter(dbLIOrdercmd, "@AssociationType", DbType.String, lifeInsuranceOrdervo.AssociationType);

        //        if (lifeInsuranceOrdervo.OrderDate != DateTime.MinValue)
        //            db.AddInParameter(dbLIOrdercmd, "@CO_OrderDate", DbType.DateTime, lifeInsuranceOrdervo.OrderDate);
        //        else
        //            db.AddInParameter(dbLIOrdercmd, "@CO_OrderDate", DbType.DateTime, DBNull.Value);

        //        if (lifeInsuranceOrdervo.ApplicationReceivedDate != DateTime.MinValue)
        //            db.AddInParameter(dbLIOrdercmd, "@ApplicationReceivedDate", DbType.DateTime, lifeInsuranceOrdervo.ApplicationReceivedDate);
        //        else
        //            db.AddInParameter(dbLIOrdercmd, "@ApplicationReceivedDate", DbType.DateTime, DBNull.Value);

        //        if (lifeInsuranceOrdervo.PaymentDate != DateTime.MinValue)
        //            db.AddInParameter(dbLIOrdercmd, "@PaymentDate", DbType.DateTime, lifeInsuranceOrdervo.PaymentDate);
        //        else
        //            db.AddInParameter(dbLIOrdercmd, "@PaymentDate", DbType.DateTime, DBNull.Value);

        //        if (lifeInsuranceOrdervo.MaturityDate != DateTime.MinValue)
        //            db.AddInParameter(dbLIOrdercmd, "@MaturityDate", DbType.DateTime, lifeInsuranceOrdervo.MaturityDate);
        //        else
        //            db.AddInParameter(dbLIOrdercmd, "@MaturityDate", DbType.DateTime, DBNull.Value);


        //        dsLIOrder = db.ExecuteDataSet(dbLIOrdercmd);
        //        dtLIOrder = dsLIOrder.Tables[0];
        //    }
        //    catch (BaseApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "OrderDao.cs:GetPaymentMode()");

        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return dsLIOrder;
        //}

        public DataTable GetAssetParticularForBonds(int bondIssuerId)
        {
            DataSet dsAssetParticular = null;
            DataTable dtAssetParticular;
            Database db;
            DbCommand dbAssetParticularCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbAssetParticularCmd = db.GetStoredProcCommand("SP_GetAssetParticularForBonds");
                db.AddInParameter(dbAssetParticularCmd, "@bondIssuerId", DbType.Int64, bondIssuerId);
                dsAssetParticular = db.ExecuteDataSet(dbAssetParticularCmd);
                dtAssetParticular = dsAssetParticular.Tables[0];
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:GetAssetParticularForBonds()");

                object[] objects = new object[1];
                objects[0] = bondIssuerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAssetParticular;
        }

        public void InsertBondIssuerName(string bondIssuerName)
        {
            Database db;
            DbCommand cmdInsertAssetParticular;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertAssetParticular = db.GetStoredProcCommand("SP_InsertBondIssuerName");
                db.AddInParameter(cmdInsertAssetParticular, "@bondIssuerName", DbType.String, bondIssuerName);               
                db.ExecuteNonQuery(cmdInsertAssetParticular);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void InsertAssetParticularForBonds(string assetParticularText, int InsuranceIssuerId)
        {
            Database db;
            DbCommand cmdInsertAssetParticular;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertAssetParticular = db.GetStoredProcCommand("SP_InsertAssetParticularSchemeForBonds");
                db.AddInParameter(cmdInsertAssetParticular, "@assetParticularText", DbType.String, assetParticularText);
                db.AddInParameter(cmdInsertAssetParticular, "@InsuranceIssuerId", DbType.Int32, InsuranceIssuerId);
                db.ExecuteNonQuery(cmdInsertAssetParticular);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public DataTable GetBondsIssuerName()
        {
            DataSet dsAssetParticular = null;
            DataTable dtAssetParticular;
            Database db;
            DbCommand dbAssetParticularCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbAssetParticularCmd = db.GetStoredProcCommand("SP_GetBondsIssuerName");
                dsAssetParticular = db.ExecuteDataSet(dbAssetParticularCmd);
                dtAssetParticular = dsAssetParticular.Tables[0];
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:GetBondsIssuer()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAssetParticular;
        }

        public bool AddBondOrderDetails(BondOrderVo bondOrderVo)
        {
            Database db;
            DbCommand bondOrderVoCmd;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                bondOrderVoCmd = db.GetStoredProcCommand("SP_CreateBondOrder");

                db.AddInParameter(bondOrderVoCmd, "@IsJointlyHeld", DbType.Int16, bondOrderVo.IsJointlyHeld);
                db.AddInParameter(bondOrderVoCmd, "@IsBuyBackFacility", DbType.Int16, bondOrderVo.IsBuyBackFacility);
                db.AddInParameter(bondOrderVoCmd, "@IsFormOfHoldingPhysical", DbType.Int16, bondOrderVo.IsFormOfHoldingPhysical);
                
                db.AddInParameter(bondOrderVoCmd, "@AssetCategory", DbType.String, bondOrderVo.AssetCategory);
                db.AddInParameter(bondOrderVoCmd, "@CustomerId", DbType.Int32, bondOrderVo.CustomerId);
                db.AddInParameter(bondOrderVoCmd, "@SourceCode", DbType.String, bondOrderVo.SourceCode);
                db.AddInParameter(bondOrderVoCmd, "@ApplicationNumber", DbType.String, bondOrderVo.ApplicationNumber);
                db.AddInParameter(bondOrderVoCmd, "@PaymentMode", DbType.String, bondOrderVo.PaymentMode);
                db.AddInParameter(bondOrderVoCmd, "@ChequeNumber", DbType.String, bondOrderVo.ChequeNumber);
                db.AddInParameter(bondOrderVoCmd, "@CustBankAccId", DbType.String, bondOrderVo.CustBankAccId);
                
                db.AddInParameter(bondOrderVoCmd, "@XS_StatusCode", DbType.String, bondOrderVo.OrderStatusCode);
                db.AddInParameter(bondOrderVoCmd, "@ApprovedByID", DbType.Int32, bondOrderVo.ApprovedBy);


                db.AddInParameter(bondOrderVoCmd, "@AssociationType", DbType.String, bondOrderVo.AssociationType);
                db.AddInParameter(bondOrderVoCmd, "@ModeOfHolding", DbType.String, bondOrderVo.ModeOfHolding);
                db.AddInParameter(bondOrderVoCmd, "@FaceValue", DbType.Double, bondOrderVo.FaceValue);
                db.AddInParameter(bondOrderVoCmd, "@FrequencyCode", DbType.String, bondOrderVo.Frequency);

                db.AddInParameter(bondOrderVoCmd, "@BondSchemeId", DbType.Int32, bondOrderVo.BondSchemeId);
                db.AddInParameter(bondOrderVoCmd, "@BondIssuerid", DbType.Int32, bondOrderVo.BondIssuerid);
                db.AddInParameter(bondOrderVoCmd, "@CA_AssociationId", DbType.Int64, bondOrderVo.AssociationId);
               
                db.AddInParameter(bondOrderVoCmd, "@BuyBackAmount", DbType.Int32, bondOrderVo.BuyBackAmount);
                db.AddInParameter(bondOrderVoCmd, "@Amount", DbType.Int32, bondOrderVo.Amount);
                db.AddInParameter(bondOrderVoCmd, "@CustDemateAccId", DbType.String, bondOrderVo.AccountId);

                db.AddInParameter(bondOrderVoCmd, "@WOS_OrderStepCode", DbType.String, bondOrderVo.OrderStepCode);
                db.AddInParameter(bondOrderVoCmd, "@IsCustomerApprovalApplicable", DbType.Int16, bondOrderVo.IsCustomerApprovalApplicable);

                if (bondOrderVo.ReasonCode == "")
                    db.AddInParameter(bondOrderVoCmd, "@XSR_StatusReasonCode", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(bondOrderVoCmd, "@XSR_StatusReasonCode", DbType.String, bondOrderVo.ReasonCode);

                if (bondOrderVo.OrderDate != DateTime.MinValue)
                    db.AddInParameter(bondOrderVoCmd, "@CO_OrderDate", DbType.DateTime, bondOrderVo.OrderDate);
                else
                    db.AddInParameter(bondOrderVoCmd, "@CO_OrderDate", DbType.DateTime, DBNull.Value);

                if (bondOrderVo.ApplicationReceivedDate != DateTime.MinValue)
                    db.AddInParameter(bondOrderVoCmd, "@ApplicationReceivedDate", DbType.DateTime, bondOrderVo.ApplicationReceivedDate);
                else
                    db.AddInParameter(bondOrderVoCmd, "@ApplicationReceivedDate", DbType.DateTime, DBNull.Value);

                if (bondOrderVo.PaymentDate != DateTime.MinValue)
                    db.AddInParameter(bondOrderVoCmd, "@PaymentDate", DbType.DateTime, bondOrderVo.PaymentDate);
                else
                    db.AddInParameter(bondOrderVoCmd, "@PaymentDate", DbType.DateTime, DBNull.Value);

                if (bondOrderVo.DepositDate != DateTime.MinValue)
                    db.AddInParameter(bondOrderVoCmd, "@DepositDate", DbType.DateTime, bondOrderVo.DepositDate);
                else
                    db.AddInParameter(bondOrderVoCmd, "@DepositDate", DbType.DateTime, DBNull.Value);

                if (bondOrderVo.MaturityDate != DateTime.MinValue)
                    db.AddInParameter(bondOrderVoCmd, "@MaturityDate", DbType.DateTime, bondOrderVo.MaturityDate);
                else
                    db.AddInParameter(bondOrderVoCmd, "@MaturityDate", DbType.DateTime, DBNull.Value);

                if (bondOrderVo.BuyBackDate != DateTime.MinValue)
                    db.AddInParameter(bondOrderVoCmd, "@BuyBackDate", DbType.DateTime, bondOrderVo.BuyBackDate);
                else
                    db.AddInParameter(bondOrderVoCmd, "@BuyBackDate", DbType.DateTime, DBNull.Value);

                db.AddOutParameter(bondOrderVoCmd, "@CO_OrderId", DbType.Int32, bondOrderVo.OrderId);

                if (db.ExecuteNonQuery(bondOrderVoCmd) != 0)
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
                FunctionInfo.Add("Method", "OrderDao.cs:AddBondOrderDetails()");
                object[] objects = new object[1];
                objects[0] = bondOrderVo;
                //objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            //   return bResult;
            return bResult;
        }

        public DataTable GetCustomerDemateAccountDetails(int customerId)
        {
            DataSet dsDemateDetails = null;
            DataTable dtDemateDetails;
            Database db;
            DbCommand dbDemateDetails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbDemateDetails = db.GetStoredProcCommand("SP_GetCustomerDemateAccountDetails");
                db.AddInParameter(dbDemateDetails, "@CustomerId", DbType.Int64, customerId);
                dsDemateDetails = db.ExecuteDataSet(dbDemateDetails);
                dtDemateDetails = dsDemateDetails.Tables[0];
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:GetCustomerDemateAccountDetails()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtDemateDetails;
        }
    }
}
