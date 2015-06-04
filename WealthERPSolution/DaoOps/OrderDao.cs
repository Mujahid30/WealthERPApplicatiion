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

        public DateTime  GetServerTime( )
        {
            DateTime Dt = DateTime.MinValue;
            Database db;
            DataSet getDs = new DataSet();
            DbCommand getCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCmd = db.GetStoredProcCommand("Sp_GetServerDate");                
                getDs = db.ExecuteDataSet(getCmd);
                  if (getDs.Tables [0].Rows.Count > 0)
                    Dt =  Convert.ToDateTime(getDs.Tables [0].Rows[0]["ServDate"].ToString()) ;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:Get GetServerTime()");

                object[] objects = new object[1];                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return Dt;

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

        public void InsertIntoProductGIInsuranceScheme(string assetText, string InsuranceIssuerCode, string schemePlanName)
        {
            Database db;
            DbCommand cmdInsertAssetParticular;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertAssetParticular = db.GetStoredProcCommand("SP_InsertIntoProductGISchemePLan");
                db.AddInParameter(cmdInsertAssetParticular, "@XGII_GIIssuerCode ", DbType.String, assetText);
                db.AddInParameter(cmdInsertAssetParticular, "@PGISP_SchemePlanName", DbType.String, schemePlanName);
                db.AddInParameter(cmdInsertAssetParticular, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, InsuranceIssuerCode);
                db.ExecuteNonQuery(cmdInsertAssetParticular);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void InsertAssetParticularScheme(string assetText, string InsuranceIssuerCode, string assetCategory)
        {
            Database db;
            DbCommand cmdInsertAssetParticular;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertAssetParticular = db.GetStoredProcCommand("SP_InsertAssetParticularScheme");
                db.AddInParameter(cmdInsertAssetParticular, "@assetText", DbType.String, assetText);
                db.AddInParameter(cmdInsertAssetParticular, "@assetCategory", DbType.String, assetCategory);
                db.AddInParameter(cmdInsertAssetParticular, "@InsuranceIssuerCode", DbType.String, InsuranceIssuerCode);
                db.ExecuteNonQuery(cmdInsertAssetParticular);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public bool AddLifeInsuranceOrder(LifeInsuranceOrderVo lifeInsuranceOrdervo, string nomineeAssociationIds, out int orderId)
        {
            Database db;
            DbCommand LifeInsuranceOrderCmd;
            orderId = 0;
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
                if (lifeInsuranceOrdervo.HoldingMode != null && lifeInsuranceOrdervo.HoldingMode != "" && lifeInsuranceOrdervo.HoldingMode != "Select")
                    db.AddInParameter(LifeInsuranceOrderCmd, "@ModeOfHolding", DbType.String, lifeInsuranceOrdervo.HoldingMode);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@ModeOfHolding", DbType.String, DBNull.Value);
                db.AddInParameter(LifeInsuranceOrderCmd, "@IsJointlyHeld", DbType.String, lifeInsuranceOrdervo.IsJointlyHeld);
                db.AddInParameter(LifeInsuranceOrderCmd, "@InsuranceSchemeid", DbType.Int32, lifeInsuranceOrdervo.InsuranceSchemeId);
                db.AddInParameter(LifeInsuranceOrderCmd, "@AssetCategory", DbType.String, lifeInsuranceOrdervo.AssetCategory);
                db.AddInParameter(LifeInsuranceOrderCmd, "@AssetGroup", DbType.String, lifeInsuranceOrdervo.AssetGroup);

                db.AddInParameter(LifeInsuranceOrderCmd, "@SourceCode", DbType.String, lifeInsuranceOrdervo.SourceCode);

                if (lifeInsuranceOrdervo.OrderStepCode != "")
                    db.AddInParameter(LifeInsuranceOrderCmd, "@WOS_OrderStepCode", DbType.String, lifeInsuranceOrdervo.OrderStepCode);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@WOS_OrderStepCode", DbType.String, DBNull.Value);

                if (lifeInsuranceOrdervo.InsuranceIssuerCode != "")
                    db.AddInParameter(LifeInsuranceOrderCmd, "@XII_InsuranceIssuerCode", DbType.String, lifeInsuranceOrdervo.InsuranceIssuerCode);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@XII_InsuranceIssuerCode", DbType.String, DBNull.Value);

                if (lifeInsuranceOrdervo.GIIssuerCode != null)
                    db.AddInParameter(LifeInsuranceOrderCmd, "@XGII_GIIssuerCode", DbType.String, lifeInsuranceOrdervo.GIIssuerCode);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@XGII_GIIssuerCode", DbType.String, DBNull.Value);

                //if (lifeInsuranceOrdervo.OrderStatusCode != "")
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@XS_StatusCode", DbType.String, lifeInsuranceOrdervo.OrderStatusCode);
                //else
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@XS_StatusCode", DbType.String, DBNull.Value);

                //if (lifeInsuranceOrdervo.ReasonCode != "")
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@XSR_StatusReasonCode", DbType.String, lifeInsuranceOrdervo.ReasonCode);
                //else
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@XSR_StatusReasonCode", DbType.String, DBNull.Value);

                if (lifeInsuranceOrdervo.ApprovedBy != null)
                    db.AddInParameter(LifeInsuranceOrderCmd, "@ApprovedByID", DbType.Int32, lifeInsuranceOrdervo.ApprovedBy);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@ApprovedByID", DbType.String, DBNull.Value);

                if (nomineeAssociationIds != "")
                    db.AddInParameter(LifeInsuranceOrderCmd, "@nomineeAssociationIds", DbType.String, nomineeAssociationIds);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@nomineeAssociationIds", DbType.String, DBNull.Value);

                //if (jointHoldingAssociationIds != "")
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@jointHoldingAssociationIds", DbType.String, jointHoldingAssociationIds);
                //else
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@jointHoldingAssociationIds", DbType.String, DBNull.Value);

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

                //db.AddInParameter(LifeInsuranceOrderCmd, "@IsCustomerApprovalApplicable", DbType.Int32, lifeInsuranceOrdervo.IsCustomerApprovalApplicable);
                db.AddOutParameter(LifeInsuranceOrderCmd, "@orderId", DbType.Int32, 5000);
                if (db.ExecuteNonQuery(LifeInsuranceOrderCmd) != 0)
                {
                    orderId = int.Parse(db.GetParameterValue(LifeInsuranceOrderCmd, "@orderId").ToString());
                    bResult = true;
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
                FunctionInfo.Add("Method", "OrderDao.cs:AddLifeInsuranceOrder()");
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

        public bool UpdateLifeInsuranceOrder(LifeInsuranceOrderVo lifeInsuranceOrdervo, string nomineeAssociationIds)
        {
            Database db;
            DbCommand LifeInsuranceOrderCmd;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                LifeInsuranceOrderCmd = db.GetStoredProcCommand("SP_UpdateLifeInsuranceOrder");

                db.AddInParameter(LifeInsuranceOrderCmd, "@CO_OrderId", DbType.Int32, lifeInsuranceOrdervo.OrderId);
                db.AddInParameter(LifeInsuranceOrderCmd, "@ApplicationNumber", DbType.String, lifeInsuranceOrdervo.ApplicationNumber);
                db.AddInParameter(LifeInsuranceOrderCmd, "@CustomerId", DbType.Int32, lifeInsuranceOrdervo.CustomerId);
                db.AddInParameter(LifeInsuranceOrderCmd, "@PaymentMode", DbType.String, lifeInsuranceOrdervo.PaymentMode);
                db.AddInParameter(LifeInsuranceOrderCmd, "@ChequeNumber", DbType.String, lifeInsuranceOrdervo.ChequeNumber);
                db.AddInParameter(LifeInsuranceOrderCmd, "@CustBankAccId", DbType.String, lifeInsuranceOrdervo.CustBankAccId);
                db.AddInParameter(LifeInsuranceOrderCmd, "@SumAssured", DbType.Double, lifeInsuranceOrdervo.SumAssured);
                db.AddInParameter(LifeInsuranceOrderCmd, "@FrequencyCode", DbType.String, lifeInsuranceOrdervo.FrequencyCode);
                if (lifeInsuranceOrdervo.HoldingMode != null && lifeInsuranceOrdervo.HoldingMode != "" && lifeInsuranceOrdervo.HoldingMode != "Select")
                    db.AddInParameter(LifeInsuranceOrderCmd, "@ModeOfHolding", DbType.String, lifeInsuranceOrdervo.HoldingMode);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@ModeOfHolding", DbType.String, DBNull.Value);
                db.AddInParameter(LifeInsuranceOrderCmd, "@IsJointlyHeld", DbType.String, lifeInsuranceOrdervo.IsJointlyHeld);
                db.AddInParameter(LifeInsuranceOrderCmd, "@InsuranceSchemeid", DbType.Int32, lifeInsuranceOrdervo.InsuranceSchemeId);
                db.AddInParameter(LifeInsuranceOrderCmd, "@AssetCategory", DbType.String, lifeInsuranceOrdervo.AssetCategory);
                db.AddInParameter(LifeInsuranceOrderCmd, "@AssetGroup", DbType.String, lifeInsuranceOrdervo.AssetGroup);

                //db.AddInParameter(LifeInsuranceOrderCmd, "@SourceCode", DbType.String, lifeInsuranceOrdervo.SourceCode);

                //if (lifeInsuranceOrdervo.OrderStepCode != "")
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@WOS_OrderStepCode", DbType.String, lifeInsuranceOrdervo.OrderStepCode);
                //else
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@WOS_OrderStepCode", DbType.String, DBNull.Value);

                if (lifeInsuranceOrdervo.InsuranceIssuerCode != "")
                    db.AddInParameter(LifeInsuranceOrderCmd, "@XII_InsuranceIssuerCode", DbType.String, lifeInsuranceOrdervo.InsuranceIssuerCode);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@XII_InsuranceIssuerCode", DbType.String, DBNull.Value);

                //if (lifeInsuranceOrdervo.GIIssuerCode != null)
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@XGII_GIIssuerCode", DbType.String, lifeInsuranceOrdervo.GIIssuerCode);
                //else
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@XGII_GIIssuerCode", DbType.String, DBNull.Value);

                //if (lifeInsuranceOrdervo.OrderStatusCode != "")
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@XS_StatusCode", DbType.String, lifeInsuranceOrdervo.OrderStatusCode);
                //else
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@XS_StatusCode", DbType.String, DBNull.Value);

                //if (lifeInsuranceOrdervo.ReasonCode != "")
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@XSR_StatusReasonCode", DbType.String, lifeInsuranceOrdervo.ReasonCode);
                //else
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@XSR_StatusReasonCode", DbType.String, DBNull.Value);

                if (lifeInsuranceOrdervo.ApprovedBy != null)
                    db.AddInParameter(LifeInsuranceOrderCmd, "@ApprovedByID", DbType.Int32, lifeInsuranceOrdervo.ApprovedBy);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@ApprovedByID", DbType.String, DBNull.Value);

                if (nomineeAssociationIds != "")
                    db.AddInParameter(LifeInsuranceOrderCmd, "@nomineeAssociationIds", DbType.String, nomineeAssociationIds);
                else
                    db.AddInParameter(LifeInsuranceOrderCmd, "@nomineeAssociationIds", DbType.String, DBNull.Value);

                //if (jointHoldingAssociationIds != "")
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@jointHoldingAssociationIds", DbType.String, jointHoldingAssociationIds);
                //else
                //    db.AddInParameter(LifeInsuranceOrderCmd, "@jointHoldingAssociationIds", DbType.String, DBNull.Value);

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
                FunctionInfo.Add("Method", "OrderDao.cs:UpdateInsurancePortfolio()");
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

        public bool DeleteLifeInsuranceOrder(int orderId)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteInsurancePortfolioCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteInsurancePortfolioCmd = db.GetStoredProcCommand("SP_DeleteLifeInsuranceOrder");
                db.AddInParameter(deleteInsurancePortfolioCmd, "@CO_OrderId", DbType.Int32, orderId);

                if (db.ExecuteNonQuery(deleteInsurancePortfolioCmd) != 0)
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
                FunctionInfo.Add("Method", "InsuranceDao.cs:DeleteLifeInsuranceOrder()");
                object[] objects = new object[1];
                objects[0] = orderId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public DataTable GetCustomerOrderDetails(int customerId, DateTime orderDate, string AssetCategory, string applicationNumber)
        {
            DataSet dsOrderDetails = null;
            DataTable dtOrderDetails;
            Database db;
            DbCommand dbOrderDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbOrderDetailsCmd = db.GetStoredProcCommand("SP_GetCustomerOrderDetails");
                db.AddInParameter(dbOrderDetailsCmd, "@C_CustomerId", DbType.Int64, customerId);
                db.AddInParameter(dbOrderDetailsCmd, "@CO_OrderDate", DbType.DateTime, orderDate);
                db.AddInParameter(dbOrderDetailsCmd, "@PAS_AssetCategory", DbType.String, AssetCategory);
                db.AddInParameter(dbOrderDetailsCmd, "@CO_ApplicationNumber", DbType.String, applicationNumber);
                dsOrderDetails = db.ExecuteDataSet(dbOrderDetailsCmd);
                dtOrderDetails = dsOrderDetails.Tables[0];
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:GetCustomerOrderDetails()");

                object[] objects = new object[3];
                objects[0] = customerId;
                objects[1] = orderDate;
                objects[2] = AssetCategory;
                objects[3] = applicationNumber;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtOrderDetails;
        }

        public LifeInsuranceOrderVo GetLifeInsuranceOrderDetails(int orderId)
        {
            LifeInsuranceOrderVo lifeInsuranceOrderVo = new LifeInsuranceOrderVo();
            DataSet dsLIOrder = null;
            DataTable dtLIOrder = null;
            DataTable dtOrderStep = null;
            DataTable dtOrderAssociate = null;
            Database db;
            DbCommand dbLIOrdercmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbLIOrdercmd = db.GetStoredProcCommand("SP_GetLifeInsuranceOrderDetails");
                db.AddInParameter(dbLIOrdercmd, "@CO_OrderId", DbType.Int32, orderId);
                dsLIOrder = db.ExecuteDataSet(dbLIOrdercmd);
                dtLIOrder = dsLIOrder.Tables[0];
                dtOrderStep = dsLIOrder.Tables[1];
                dtOrderAssociate = dsLIOrder.Tables[2];
                DataRow dr;
                if (dtLIOrder.Rows.Count > 0)
                {
                    dr = dtLIOrder.Rows[0];

                    lifeInsuranceOrderVo.ApplicationNumber = dr["CO_ApplicationNumber"].ToString();
                    lifeInsuranceOrderVo.ApplicationReceivedDate = Convert.ToDateTime(dr["CO_ApplicationReceivedDate"].ToString());
                    //lifeInsuranceOrderVo.ApprovedBy = Convert.ToInt32(dr["CO_ApplicationNumber"].ToString());
                    lifeInsuranceOrderVo.AssetGroup = dr["PAG_AssetGroupCode"].ToString();
                    lifeInsuranceOrderVo.AssetCategory = dr["CIOD_AssetCategory"].ToString();

                    lifeInsuranceOrderVo.ChequeNumber = dr["CO_ChequeNumber"].ToString();
                    lifeInsuranceOrderVo.CustBankAccId = Convert.ToInt32(dr["CB_CustBankAccId"].ToString());
                    lifeInsuranceOrderVo.BankBranchName = dr["CB_BranchName"].ToString();
                    lifeInsuranceOrderVo.CustomerId = Convert.ToInt32(dr["C_CustomerId"].ToString());
                    lifeInsuranceOrderVo.FrequencyCode = dr["XF_FrequencyCode"].ToString();
                    lifeInsuranceOrderVo.GIIssuerCode = dr["XGII_GIIssuerCode"].ToString();
                    lifeInsuranceOrderVo.HoldingMode = dr["CIOD_ModeOfHolding"].ToString();
                    lifeInsuranceOrderVo.InsuranceIssuerCode = dr["XII_InsuranceIssuerCode"].ToString();
                    lifeInsuranceOrderVo.InsuranceSchemeId = Convert.ToInt32(dr["IS_SchemeId"].ToString());
                    //lifeInsuranceOrderVo.IsCustomerApprovalApplicable = Convert.ToInt32(dr["CO_IsCustomerApprovalNeeded"].ToString());
                    lifeInsuranceOrderVo.IsJointlyHeld = Convert.ToInt16(dr["CIOD_IsJointlyHeld"].ToString());
                    lifeInsuranceOrderVo.MaturityDate = Convert.ToDateTime(dr["CIOD_PolicyMaturityDate"].ToString());
                    lifeInsuranceOrderVo.OrderDate = Convert.ToDateTime(dr["CO_OrderDate"].ToString());
                    lifeInsuranceOrderVo.OrderId = Convert.ToInt32(dr["CO_OrderId"].ToString());
                    //lifeInsuranceOrderVo.OrderNumber = Convert.ToInt32(dr["CO_ApplicationNumber"].ToString());

                    lifeInsuranceOrderVo.PaymentDate = Convert.ToDateTime(dr["CO_PaymentDate"].ToString());
                    lifeInsuranceOrderVo.PaymentMode = dr["XPM_PaymentModeCode"].ToString();
                    lifeInsuranceOrderVo.SumAssured = Convert.ToDouble(dr["CIOD_SumAssured"].ToString());
                    lifeInsuranceOrderVo.SourceCode = dr["WOSR_SourceCode"].ToString();
                }
                if (dtOrderStep.Rows.Count > 0)
                {
                    dr = dtOrderStep.Rows[0];
                    lifeInsuranceOrderVo.OrderStatusCode = dr["XS_StatusCode"].ToString();
                    lifeInsuranceOrderVo.OrderStepCode = dr["WOS_OrderStepCode"].ToString();
                    lifeInsuranceOrderVo.ReasonCode = dr["XSR_StatusReasonCode"].ToString();
                }
                if (dtOrderAssociate.Rows.Count > 0)
                {
                    dr = dtOrderAssociate.Rows[0];
                    lifeInsuranceOrderVo.AssociationId = Convert.ToInt32(dr["CA_AssociationId"].ToString());
                    lifeInsuranceOrderVo.AssociationType = dr["COA_AssociationType"].ToString();
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

                FunctionInfo.Add("Method", "OrderDao.cs:GetLifeInsuranceOrderDetails()");

                object[] objects = new object[1];
                objects[0] = orderId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return lifeInsuranceOrderVo;
        }

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

        public DataSet GetOrderStepsDetails(int oredrId)
        {
            DataSet dsStepsDetails = null;
            Database db;
            DbCommand dbStepsDetails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbStepsDetails = db.GetStoredProcCommand("SP_GetOrderStepsDetails");
                db.AddInParameter(dbStepsDetails, "@CO_OrderId", DbType.Int64, oredrId);

                dsStepsDetails = db.ExecuteDataSet(dbStepsDetails);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OrderDao.cs:GetOrderStepsDetails()");
                object[] objects = new object[0];
                objects[0] = oredrId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsStepsDetails;
        }

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

                db.AddInParameter(bondOrderVoCmd, "@AssetGroup", DbType.String, bondOrderVo.AssetGroup);
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
                //db.AddInParameter(bondOrderVoCmd, "@IsCustomerApprovalApplicable", DbType.Int16, bondOrderVo.IsCustomerApprovalApplicable);

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

        public DataTable GetOrderList(int systematicId, int advisorId, string rmId, string branchId, DateTime toDate, DateTime fromDate, string status, string customerId, string orderType, string usertype, int AgentId, string SubBrokerCode, string AgentCode, int orderId, string transactionType, string folioNo, int userId)
        {
            DataSet dsOrder = null;
            DataTable dtOrder;
            Database db;
            DbCommand dbOrder;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbOrder = db.GetStoredProcCommand("SP_GetOrderList");
                if (advisorId != 0)
                    db.AddInParameter(dbOrder, "@A_AdviserId", DbType.Int64, advisorId);
                else
                    db.AddInParameter(dbOrder, "@A_AdviserId", DbType.Int64, DBNull.Value);
                if (AgentId != 0)
                    db.AddInParameter(dbOrder, "@AAC_AdviserAgentId", DbType.Int64, AgentId);
                else
                    db.AddInParameter(dbOrder, "@AAC_AdviserAgentId", DbType.Int64, DBNull.Value);
                if (rmId != "")
                    db.AddInParameter(dbOrder, "@RmId", DbType.String, rmId);
                else
                    db.AddInParameter(dbOrder, "@RmId", DbType.String, DBNull.Value);
                if (branchId != "")
                    db.AddInParameter(dbOrder, "@BranchId", DbType.String, branchId);
                else
                    db.AddInParameter(dbOrder, "@BranchId", DbType.String, DBNull.Value);
                if (fromDate != DateTime.MinValue)
                {
                    db.AddInParameter(dbOrder, "@Fromdate", DbType.DateTime, fromDate);
                }
                else
                {
                    db.AddInParameter(dbOrder, "@Fromdate", DbType.DateTime, DBNull.Value);

                }
                if (toDate != DateTime.MinValue)

                db.AddInParameter(dbOrder, "@Todate", DbType.DateTime, toDate);
                else
                    db.AddInParameter(dbOrder, "@Todate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(dbOrder, "@UserType", DbType.String, usertype);
                if (SubBrokerCode != "0")
                    db.AddInParameter(dbOrder, "@SubBrokerCode", DbType.String, SubBrokerCode);
                else
                    db.AddInParameter(dbOrder, "@SubBrokerCode", DbType.String, DBNull.Value);
                if (AgentCode != "0")
                    db.AddInParameter(dbOrder, "@AgentCode", DbType.String, AgentCode);
                else
                  db.AddInParameter(dbOrder, "@AgentCode", DbType.String, DBNull.Value);
                
                db.AddInParameter(dbOrder, "@status", DbType.String, status);
                if (customerId != "")
                    db.AddInParameter(dbOrder, "@C_CustomerId", DbType.String, customerId);
                else
                    db.AddInParameter(dbOrder, "@C_CustomerId", DbType.String, DBNull.Value);
                if (orderType == "All")
                {
                    db.AddInParameter(dbOrder, "@orderType", DbType.String, null);
                }
                else
                {
                    db.AddInParameter(dbOrder, "@orderType", DbType.String, orderType);
                }

                db.AddInParameter(dbOrder, "@sytematicId", DbType.Int32, systematicId);
                if (orderId != 0)
                    db.AddInParameter(dbOrder, "@OrderId", DbType.Int32, orderId);
                else
                    db.AddInParameter(dbOrder, "@OrderId", DbType.Int32, 0);
                if (transactionType == "ALL")
                    db.AddInParameter(dbOrder, "@transactionType", DbType.String, null );
                else
                    db.AddInParameter(dbOrder, "@transactionType", DbType.String, transactionType);
                db.AddInParameter(dbOrder, "@folioNo", DbType.String, folioNo);

                db.AddInParameter(dbOrder, "@userId", DbType.Int32, userId);
                dsOrder = db.ExecuteDataSet(dbOrder);
                dtOrder = dsOrder.Tables[0];

            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:GetOrderList()");

                object[] objects = new object[1];
                objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtOrder;
        }

        public DataTable GetOrderStatus(string orderStepCode, int orderId)
        {
            DataSet ds = null;
            DataTable dt;
            Database db;
            DbCommand dbCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCmd = db.GetStoredProcCommand("SP_GetOrderStepStatus");
                db.AddInParameter(dbCmd, "@OrderStepCode", DbType.String, orderStepCode);
                db.AddInParameter(dbCmd, "@CO_OrderId", DbType.Int64, orderId);
                ds = db.ExecuteDataSet(dbCmd);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:GetOrderStatus()");

                object[] objects = new object[1];
                objects[0] = orderStepCode;
                objects[1] = orderId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public DataTable GetOrderStatusPendingReason(string orderStatusCode)
        {
            DataSet ds = null;
            DataTable dt;
            Database db;
            DbCommand dbCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCmd = db.GetStoredProcCommand("SP_GetOrderStatusPendingReason");
                //db.AddInParameter(dbCmd, "@OrderStepCode", DbType.String, orderStepCode);
                db.AddInParameter(dbCmd, "@OrderStatusCode", DbType.String, orderStatusCode);
                //db.AddInParameter(dbCmd, "@CO_OrderId", DbType.Int64, orderId);
                ds = db.ExecuteDataSet(dbCmd);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:GetOrderStatusPendingReason()");

                object[] objects = new object[2];
                objects[0] = orderStatusCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public bool UpdateOrderStep(string updatedStatus, string updatedReason, int orderId, string orderStepCode)
        {
            Database db;
            DbCommand OrderCmd;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                OrderCmd = db.GetStoredProcCommand("SP_UpdateOrderStep");
                db.AddInParameter(OrderCmd, "@updatedStatusCode", DbType.String, updatedStatus);
                db.AddInParameter(OrderCmd, "@updatedReasonCode", DbType.String, updatedReason);
                db.AddInParameter(OrderCmd, "@CO_OrderId", DbType.Int64, orderId);
                db.AddInParameter(OrderCmd, "@OrderStepCode", DbType.String, orderStepCode);
                if (db.ExecuteNonQuery(OrderCmd) != 0)
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
                FunctionInfo.Add("Method", "OrderDao.cs:UpdateOrderStep()");
                object[] objects = new object[3];
                objects[0] = updatedStatus;
                objects[1] = updatedReason;
                objects[2] = orderId;
                objects[3] = orderStepCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public DataTable GetCustomerOrderStepStatus(string orderstepCode)
        {
            DataSet ds = null;
            DataTable dt;
            Database db;
            DbCommand dbCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCmd = db.GetStoredProcCommand("SP_GetCustomerOrderStepStatus");
                db.AddInParameter(dbCmd, "@WOS_OrderStepCode", DbType.String, orderstepCode);
                ds = db.ExecuteDataSet(dbCmd);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetCustomerOrderStepStatusRejectReason(string orderstepCode)
        {
            DataSet ds = null;
            DataTable dt;
            Database db;
            DbCommand dbCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCmd = db.GetStoredProcCommand("SP_GetCustomerOrderStepStatusRejectReason");
                db.AddInParameter(dbCmd, "@WOS_OrderStepCode", DbType.String, orderstepCode);
                ds = db.ExecuteDataSet(dbCmd);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetCustomerOrderAssociates(int orderId)
        {
            DataSet ds = null;
            DataTable dt;
            Database db;
            DbCommand dbCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCmd = db.GetStoredProcCommand("SP_GetCustomerOrderAssociates");
                db.AddInParameter(dbCmd, "@CO_OrderId", DbType.String, orderId);
                ds = db.ExecuteDataSet(dbCmd);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable GetSubBrokerCode(int advisorId, int rmId, int branchId, string usertype)
        {
            DataSet dsOrder = null;
            DataTable dtOrder;
            Database db;
            DbCommand dbOrder;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbOrder = db.GetStoredProcCommand("SP_BindSubBrokerCode");
                if (advisorId != 0)
                    db.AddInParameter(dbOrder, "@AdviserId", DbType.Int64, advisorId);
                else
                    db.AddInParameter(dbOrder, "@AdviserId", DbType.Int64, DBNull.Value);
                db.AddInParameter(dbOrder, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(dbOrder, "@BranchId ", DbType.Int32, branchId);
                db.AddInParameter(dbOrder, "@UserType", DbType.String, usertype);
                dsOrder = db.ExecuteDataSet(dbOrder);
                dtOrder = dsOrder.Tables[0];

            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:GetSubBrokerCode()");

                object[] objects = new object[1];
                objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtOrder;   
        }
        public DataTable GetSubBrokerAgentCode(int adviserId,string AgentCode)
        {
            DataSet dsOrder = null;
            DataTable dtOrder;
            Database db;
            DbCommand dbOrder;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbOrder = db.GetStoredProcCommand("SP_BindBrokerAgentCode");
                if (adviserId !=0)
                    db.AddInParameter(dbOrder, "@AdviserID", DbType.Int32, adviserId);
                else
                    db.AddInParameter(dbOrder, "@AdviserID", DbType.String, DBNull.Value);
                if (AgentCode!=null)
                db.AddInParameter(dbOrder, "@AgentCode", DbType.String ,AgentCode);
                else
                db.AddInParameter(dbOrder, "@AgentCode", DbType.String, DBNull.Value);
                dsOrder = db.ExecuteDataSet(dbOrder);               
                dtOrder = dsOrder.Tables[0];
        

            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderDao.cs:GetSubBrokerAgentCode()");

                object[] objects = new object[1];
                objects[0] = AgentCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtOrder;
        }
       

        public DataTable GetAllAgentListForOrder(int id, string UserRole)
        {
            DataSet dsAgentList = null;
            Database db;
            DbCommand cmdAgentList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdAgentList = db.GetStoredProcCommand("SPROC_GetAllAgentListForOrder");
                db.AddInParameter(cmdAgentList, "@id", DbType.Int64, id);
                db.AddInParameter(cmdAgentList, "@UserRole", DbType.String, UserRole);
                dsAgentList = db.ExecuteDataSet(cmdAgentList);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OrderDao.cs:GetAllAgentListForOrder()");
                object[] objects = new object[1];
                objects[0] = id;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAgentList.Tables[0];
        }

        public DataTable GetTradeDateListForOrder(DateTime date, int isPastDateList, int noOfDaysReq)
        {
            DataSet dsTradeDate = null;
            Database db;
            DbCommand cmdTradeDate;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdTradeDate = db.GetStoredProcCommand("SPROC_GetTradeDateListForOrder");
                db.AddInParameter(cmdTradeDate, "@Date", DbType.Date, date);
                db.AddInParameter(cmdTradeDate, "@IsPastDateList", DbType.Int16, isPastDateList);
                db.AddInParameter(cmdTradeDate, "@NoOfDaysReq", DbType.Int16, noOfDaysReq);
                dsTradeDate = db.ExecuteDataSet(cmdTradeDate);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OrderDao.cs:GetTradeDateListForOrder(DateTime date, int isPastDateList, int noOfDaysReq))");
                object[] objects = new object[3];
                objects[0] = date;
                objects[1] = isPastDateList;
                objects[2] = noOfDaysReq;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsTradeDate.Tables[0];
        }
        public DataTable GetTradeDateList(DateTime date)
        {
            DataSet dsTradeDate = null;
            Database db;
            DbCommand cmdTradeDate;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdTradeDate = db.GetStoredProcCommand("SPROC_GetTradeDateList");
                db.AddInParameter(cmdTradeDate, "@Date", DbType.Date, date);
                dsTradeDate = db.ExecuteDataSet(cmdTradeDate);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OrderDao.cs:GetTradeDateListForOrder(DateTime date, int isPastDateList, int noOfDaysReq))");
                object[] objects = new object[3];
                objects[0] = date;
                exBase.AdditionalInformation = FunctionInfo;
            
                throw exBase;
            }
            return dsTradeDate.Tables[0];
        }



    }
}
