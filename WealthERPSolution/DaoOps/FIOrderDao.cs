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
    public class FIOrderDao : OrderDao
    {
        public String GetTaxStatus(int customerId)
        {
            DataSet dsOrderNumber;
            DataTable dtTaxStatus;            
            Database db;
            DbCommand getTaxStatuscmd;
            string TaxStatus="";
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getTaxStatuscmd = db.GetStoredProcCommand("Sp_GetTaxStatus");
                db.AddInParameter(getTaxStatuscmd, "@CustID", DbType.Int32, customerId);

                dsOrderNumber = db.ExecuteDataSet(getTaxStatuscmd);
                dtTaxStatus = dsOrderNumber.Tables[0];
                if (dtTaxStatus.Rows.Count > 0)
                    TaxStatus =  dtTaxStatus.Rows[0]["CPS_SubType"].ToString() ;
                 
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FIOrderDao.cs:GetTaxStatus()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return TaxStatus;
        }
        public bool UpdateFITransactionForSynch(int gvOrderId, String gvSchemeCode, int gvaccountId, string gvTrxType, int gvPortfolioId, double gvAmount, out bool status, DateTime gvOrderDate)
        {
            Database db;
            DbCommand updateMFTransactionForSynchCmd;
            int affectedRecords = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateMFTransactionForSynchCmd = db.GetStoredProcCommand("SP_UpdateFITransactionForSync");
                db.AddInParameter(updateMFTransactionForSynchCmd, "@orderId", DbType.Int32, gvOrderId);
                db.AddInParameter(updateMFTransactionForSynchCmd, "@schemeCode", DbType.String, gvSchemeCode);
                db.AddInParameter(updateMFTransactionForSynchCmd, "@accountId", DbType.Int32, gvaccountId);
                db.AddInParameter(updateMFTransactionForSynchCmd, "@trxType", DbType.String, gvTrxType);
                //db.AddInParameter(updateMFTransactionForSynchCmd, "@portfolioId", DbType.Int32, gvPortfolioId);
                db.AddInParameter(updateMFTransactionForSynchCmd, "@amount", DbType.Double, gvAmount);
                db.AddInParameter(updateMFTransactionForSynchCmd, "@orderDate", DbType.DateTime, gvOrderDate);
                db.AddOutParameter(updateMFTransactionForSynchCmd, "@IsSuccess", DbType.Int16, 0);
                if (db.ExecuteNonQuery(updateMFTransactionForSynchCmd) != 0)
                    affectedRecords = int.Parse(db.GetParameterValue(updateMFTransactionForSynchCmd, "@IsSuccess").ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:UpdateMFTransactionForSynch()");
                object[] objects = new object[7];
                objects[0] = gvOrderId;
                objects[1] = gvSchemeCode;
                objects[2] = gvaccountId;
                objects[3] = gvTrxType;
                objects[4] = gvPortfolioId;
                objects[5] = gvAmount;
                objects[6] = gvOrderDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            if (affectedRecords > 0)
                return status = true;
            else
                return status = false;
        }
        public DataSet GetCustomerAssociates(int customerId)
        {
            DataSet dsCustomerAssociates = null;
            DbCommand getCustomerAssociatesCmd;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssociatesCmd = db.GetStoredProcCommand("SP_GetCustomerAssociatesRel");
                db.AddInParameter(getCustomerAssociatesCmd, "@C_CustomerId", DbType.Int32, customerId);
                dsCustomerAssociates = db.ExecuteDataSet(getCustomerAssociatesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "FIOrderDao.cs:GetCustomerAssociatesRel()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


            return dsCustomerAssociates;
        }
        public int GetOrderNumber()
        {
            DataSet dsOrderNumber;
            DataTable dtOrderNumber;
            int orderNumber = 0;
            Database db;
            DbCommand getSchemeSwitchcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeSwitchcmd = db.GetStoredProcCommand("Sp_FIOrderNO");
                dsOrderNumber = db.ExecuteDataSet(getSchemeSwitchcmd);
                dtOrderNumber = dsOrderNumber.Tables[0];
                if (dtOrderNumber.Rows.Count > 0)
                    orderNumber = int.Parse(dtOrderNumber.Rows[0]["CFIOD_DetailsId"].ToString());
                else
                    orderNumber = 999;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetOrderNumber()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return orderNumber;
        }


        public DataSet GetFIModeOfHolding()
        {
            DataSet dsGetFICategory;
            Database db;
            DbCommand getFICategorycmd;
            try
            {
                //  Shantanu Dated :- 18thSept2012
                //Don't Change this scripts As I am using same while MF Folio Add. If you want to change ,
                //then test the folio Add Screen also..

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getFICategorycmd = db.GetStoredProcCommand("SP_XMLModeOfHolding");
                dsGetFICategory = db.ExecuteDataSet(getFICategorycmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFICategory;
        }

        public DataSet GetCustomerBank(int customerId)
        {
            DataSet dsGetCustomerBank;
            Database db;
            DbCommand getCustomerBankcmd;
            try
            {
                //  Shantanu Dated :- 18thSept2012
                //Don't Change this scripts As I am using same while MF Folio Add. If you want to change ,
                //then test the folio Add Screen also..

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerBankcmd = db.GetStoredProcCommand("SP_GetBankAccountDetails");
                db.AddInParameter(getCustomerBankcmd, "@C_CustomerId", DbType.Int32, customerId);
                dsGetCustomerBank = db.ExecuteDataSet(getCustomerBankcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetCustomerBank;
        }

        public DataSet GetFICategory()
        {
            DataSet dsGetFICategory;
            Database db;
            DbCommand getFICategorycmd;
            try
            {
                //  Shantanu Dated :- 18thSept2012
                //Don't Change this scripts As I am using same while MF Folio Add. If you want to change ,
                //then test the folio Add Screen also..

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getFICategorycmd = db.GetStoredProcCommand("Sp_FICategory");
                dsGetFICategory = db.ExecuteDataSet(getFICategorycmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFICategory;
        }





        public DataSet GetFIIssuer(int AdviserID, string CategoryCode)
        {
            DataSet dsGetFIIssuer;
            Database db;
            DbCommand getFIIssuercmd;
            try
            {
                 

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getFIIssuercmd = db.GetStoredProcCommand("Sp_FIIssuer");
                db.AddInParameter(getFIIssuercmd, "@AdviserID", DbType.Int32, AdviserID);
                db.AddInParameter(getFIIssuercmd, "@CategoryCode", DbType.String, CategoryCode);
                dsGetFIIssuer = db.ExecuteDataSet(getFIIssuercmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFIIssuer;
        }


        public DataSet GetFIScheme(int AdviserID, string IssuerID)
        {
            DataSet dsGetFIIssuer;
            Database db;
            DbCommand getFIIssuercmd;
            try
            {
                //  Shantanu Dated :- 18thSept2012
                //Don't Change this scripts As I am using same while MF Folio Add. If you want to change ,
                //then test the folio Add Screen also..

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getFIIssuercmd = db.GetStoredProcCommand("Sp_FIScheme");
                db.AddInParameter(getFIIssuercmd, "@AdviserID", DbType.Int32, AdviserID);
                db.AddInParameter(getFIIssuercmd, "@IssuerID", DbType.String, IssuerID);
                dsGetFIIssuer = db.ExecuteDataSet(getFIIssuercmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFIIssuer;
        }
        public DataTable GetOrderList(int advisorId, string rmId, string branchId, DateTime toDate, DateTime fromDate, string status, string customerId, string orderType, string usertype, int AgentId, string SubBrokerCode, string AgentCode)
        {
            DataSet dsOrder = null;
            DataTable dtOrder;
            Database db;
            DbCommand dbOrder;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbOrder = db.GetStoredProcCommand("SP_GetFIOrderList");
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
                db.AddInParameter(dbOrder, "@Fromdate", DbType.DateTime, fromDate);
                db.AddInParameter(dbOrder, "@Todate", DbType.DateTime, toDate);
                db.AddInParameter(dbOrder, "@UserType", DbType.String, usertype);
                if (SubBrokerCode != "0")
                    db.AddInParameter(dbOrder, "@SubBrokerCode", DbType.String, SubBrokerCode);
                else
                    db.AddInParameter(dbOrder, "@SubBrokerCode", DbType.String, DBNull.Value);
                if (AgentCode != "0")
                    db.AddInParameter(dbOrder, "@AgentCode", DbType.String, AgentCode);
                else
                    db.AddInParameter(dbOrder, "@AgentCode", DbType.String, DBNull.Value);
                //if (SubBrokerName != "0")
                //    db.AddInParameter(dbOrder, "@SubBrokerName", DbType.String, SubBrokerName);
                //else
                //    db.AddInParameter(dbOrder, "@SubBrokerName", DbType.String, DBNull.Value);
                db.AddInParameter(dbOrder, "@status", DbType.String, status);
                //if (customerId != "")
                //    db.AddInParameter(dbOrder, "@C_CustomerId", DbType.String, customerId);
                //else
                    db.AddInParameter(dbOrder, "@C_CustomerId", DbType.String, DBNull.Value);
                if (orderType == "All")
                {
                    db.AddInParameter(dbOrder, "@orderType", DbType.String, null);
                }
                else
                {
                    db.AddInParameter(dbOrder, "@orderType", DbType.String, orderType);
                }
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
        public List<int> CreateOrderFIDetails(OrderVo orderVo, FIOrderVo mforderVo, int userId)
        {
            List<int> orderIds = new List<int>();
            int OrderId;
            Database db;
            DbCommand createMFOrderTrackingCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFOrderTrackingCmd = db.GetStoredProcCommand("[SP_CreateCustomerFIOrderDetails]");
                db.AddOutParameter(createMFOrderTrackingCmd, "@CO_OrderId", DbType.Int32, orderVo.OrderId);
                if (orderVo.OrderDate != DateTime.MinValue)
                    db.AddInParameter(createMFOrderTrackingCmd, "@CO_OrderDate", DbType.DateTime, orderVo.OrderDate);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@CO_OrderDate", DbType.DateTime, DBNull.Value);

                //db.AddInParameter(createMFOrderTrackingCmd, "@CO_OrderDate", DbType.DateTime, orderVo.OrderDate );
                db.AddInParameter(createMFOrderTrackingCmd, "@CustomerId", DbType.Int32, orderVo.CustomerId);
                db.AddInParameter(createMFOrderTrackingCmd, "@WOSR_SourceCode", DbType.String, "");
                db.AddInParameter(createMFOrderTrackingCmd, "@ApplicationNumber", DbType.String, orderVo.ApplicationNumber);
                if (orderVo.ApplicationReceivedDate != DateTime.MinValue)
                    db.AddInParameter(createMFOrderTrackingCmd, "@ApplicationReceivedDate", DbType.DateTime, orderVo.ApplicationReceivedDate);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@ApplicationReceivedDate", DbType.DateTime, DBNull.Value);

             //   db.AddInParameter(createMFOrderTrackingCmd, "@ApplicationReceivedDate", DbType.DateTime, orderVo.ApplicationReceivedDate);
                //db.AddInParameter(createMFOrderTrackingCmd, "@statusCode", DbType.String, orderVo.OrderStatusCode);
                //db.AddInParameter(createMFOrderTrackingCmd, "@StatusReasonCode", DbType.String, orderVo.ReasonCode);
                //if (mforderVo.accountid != 0)
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFA_accountid", DbType.Int32, mforderVo.accountid);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFA_accountid", DbType.Int32, 0);

                db.AddInParameter(createMFOrderTrackingCmd, "@ChequeNumber", DbType.String, orderVo.ChequeNumber);
                if (orderVo.PaymentDate != DateTime.MinValue)
                    db.AddInParameter(createMFOrderTrackingCmd, "@PaymentDate", DbType.DateTime, orderVo.PaymentDate);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@PaymentDate", DbType.DateTime, DBNull.Value);

                
                //db.AddInParameter(createMFOrderTrackingCmd, "@PaymentDate", DbType.DateTime, orderVo.PaymentDate );
                db.AddInParameter(createMFOrderTrackingCmd, "@CustBankAccId", DbType.Int32, orderVo.CustBankAccId);

                db.AddInParameter(createMFOrderTrackingCmd, "@AA_AdviserAssociateId", DbType.Int32, orderVo.AssociationId);
                db.AddInParameter(createMFOrderTrackingCmd, "@AAC_AdviserAgentId", DbType.Int32, orderVo.AgentId);
                db.AddInParameter(createMFOrderTrackingCmd, "@AAC_AgentCode", DbType.String, orderVo.AgentCode);

                db.AddInParameter(createMFOrderTrackingCmd, "@AssetGroupCode", DbType.String, orderVo.AssetGroup);
                db.AddInParameter(createMFOrderTrackingCmd, "@PaymentMode", DbType.String, orderVo.PaymentMode);
                db.AddInParameter(createMFOrderTrackingCmd, "@Isclose", DbType.Int32, 0);

                db.AddInParameter(createMFOrderTrackingCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, mforderVo.AssetInstrumentCategoryCode);
                db.AddInParameter(createMFOrderTrackingCmd, "@PFIIM_IssuerId", DbType.String, mforderVo.IssuerId);
                db.AddInParameter(createMFOrderTrackingCmd, "@PFISM_SchemeId", DbType.Int32, mforderVo.SchemeId);
                db.AddInParameter(createMFOrderTrackingCmd, "@PFISD_SeriesId", DbType.Int32, mforderVo.SeriesId);
                db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_TransactionType", DbType.String, mforderVo.TransactionType);
                db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_OrderNO", DbType.String, orderVo.OrderNumber);
                db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_AmountPayable", DbType.Double, mforderVo.AmountPayable);
                db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_ModeOfHolding", DbType.String, mforderVo.ModeOfHolding);
                db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_SchemeOption", DbType.String, mforderVo.Schemeoption);
                db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_DepositPayableTo", DbType.String, mforderVo.Depositpayableto);
                db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_Frequency", DbType.String, mforderVo.Frequency);
                db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_Privilidge", DbType.String, mforderVo.Privilidge);
                db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_ExisitingDepositreceiptno", DbType.String, mforderVo.ExisitingDepositreceiptno);
                db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_RenewalAmount", DbType.Double, mforderVo.RenewalAmount);
                if (mforderVo.MaturityDate != DateTime.MinValue)
                    db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_MaturityDate", DbType.DateTime, mforderVo.MaturityDate);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_MaturityDate", DbType.DateTime, DBNull.Value);

                //db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_MaturityDate", DbType.DateTime, mforderVo.MaturityDate);
                db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_MaturityAmount", DbType.Double, mforderVo.MaturityAmount);

                db.AddInParameter(createMFOrderTrackingCmd, "@CFIOD_DepCustBankAccId", DbType.Int32, mforderVo.DepCustBankAccId);

                db.AddInParameter(createMFOrderTrackingCmd, "@AgentId", DbType.Int32, mforderVo.AgentId);
                db.AddInParameter(createMFOrderTrackingCmd, "@UserId", DbType.Int32, userId );
                
                //CFIOD_AmountPayable
                //db.AddInParameter(createMFOrderTrackingCmd, "@SourceCode", DbType.String, operationVo.SourceCode);
                //if (!string.IsNullOrEmpty(mforderVo.FutureTriggerCondition.ToString().Trim()))
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_FutureTriggerCondition", DbType.String, mforderVo.FutureTriggerCondition);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_FutureTriggerCondition", DbType.String, DBNull.Value);
                //db.AddInParameter(createMFOrderTrackingCmd, "@ApplicationNumber", DbType.String, orderVo.ApplicationNumber);
                ////db.AddInParameter(createMFOrderTrackingCmd, "@ApplicationReceivedDate", DbType.DateTime, orderVo.ApplicationReceivedDate);
                //if (orderVo.ApplicationReceivedDate != DateTime.MinValue)
                //    db.AddInParameter(createMFOrderTrackingCmd, "@ApplicationReceivedDate", DbType.DateTime, orderVo.ApplicationReceivedDate);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@ApplicationReceivedDate", DbType.DateTime, DBNull.Value);

                //db.AddInParameter(createMFOrderTrackingCmd, "@CP_portfolioId", DbType.Int32, mforderVo.portfolioId);
                //db.AddInParameter(createMFOrderTrackingCmd, "@PaymentMode", DbType.String, orderVo.PaymentMode);
                //if (!string.IsNullOrEmpty(orderVo.ChequeNumber.ToString().Trim()))
                //    db.AddInParameter(createMFOrderTrackingCmd, "@ChequeNumber", DbType.String, orderVo.ChequeNumber);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@ChequeNumber", DbType.String, DBNull.Value);
                //if (orderVo.PaymentDate != DateTime.MinValue)
                //    db.AddInParameter(createMFOrderTrackingCmd, "@PaymentDate", DbType.DateTime, orderVo.PaymentDate);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@PaymentDate", DbType.DateTime, DBNull.Value);
                //if (mforderVo.FutureExecutionDate != DateTime.MinValue)
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_FutureExecutionDate", DbType.DateTime, mforderVo.FutureExecutionDate);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_FutureExecutionDate", DbType.DateTime, DBNull.Value);
                //if (mforderVo.SchemePlanSwitch != 0)
                //    db.AddInParameter(createMFOrderTrackingCmd, "@PASP_SchemePlanSwitch", DbType.Int32, mforderVo.SchemePlanSwitch);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@PASP_SchemePlanSwitch", DbType.Int32, DBNull.Value);
                //if (!string.IsNullOrEmpty(mforderVo.BankName.ToString().Trim()))
                //    db.AddInParameter(createMFOrderTrackingCmd, "@MFOD_BankName", DbType.String, mforderVo.BankName);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@MFOD_BankName", DbType.String, DBNull.Value);
                //if (!string.IsNullOrEmpty(mforderVo.BranchName.ToString().Trim()))
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_BranchName", DbType.String, mforderVo.BranchName);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_BranchName", DbType.String, DBNull.Value);
                //if (!string.IsNullOrEmpty(mforderVo.AddrLine1.ToString().Trim()))
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_AddrLine1", DbType.String, mforderVo.AddrLine1);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_AddrLine1", DbType.String, DBNull.Value);
                //if (mforderVo.AddrLine2 != null)
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_AddrLine2", DbType.String, mforderVo.AddrLine2);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_AddrLine2", DbType.String, DBNull.Value);
                //if (!string.IsNullOrEmpty(mforderVo.AddrLine3.ToString().Trim()))
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_AddrLine3", DbType.String, mforderVo.AddrLine3);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_AddrLine3", DbType.String, DBNull.Value);
                //if (!string.IsNullOrEmpty(mforderVo.City.ToString().Trim()))
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_City", DbType.String, mforderVo.City);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_City", DbType.String, DBNull.Value);
                //if (!string.IsNullOrEmpty(mforderVo.State.ToString().Trim()))
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_State", DbType.String, mforderVo.State);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_State", DbType.String, DBNull.Value);
                //if (!string.IsNullOrEmpty(mforderVo.Country.ToString().Trim()))
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_Country", DbType.String, mforderVo.Country);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_Country", DbType.String, DBNull.Value);
                //if (!string.IsNullOrEmpty(mforderVo.Pincode.ToString().Trim()))
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_PinCode", DbType.String, mforderVo.Pincode);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_PinCode", DbType.String, DBNull.Value);
                //if (mforderVo.LivingSince != DateTime.MinValue)
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_LivingScince", DbType.DateTime, mforderVo.LivingSince);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_LivingScince", DbType.DateTime, DBNull.Value);
                //db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_IsExecuted ", DbType.Int32, mforderVo.IsExecuted);
                //if (mforderVo.FrequencyCode != null && mforderVo.FrequencyCode != "")
                //    db.AddInParameter(createMFOrderTrackingCmd, "@XF_FrequencyCode", DbType.String, mforderVo.FrequencyCode);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@XF_FrequencyCode", DbType.String, DBNull.Value);
                //if (mforderVo.StartDate != DateTime.MinValue)
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_StartDate", DbType.DateTime, mforderVo.StartDate);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_StartDate", DbType.DateTime, DBNull.Value);
                //if (mforderVo.EndDate != DateTime.MinValue)
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_EndDate", DbType.DateTime, mforderVo.EndDate);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_EndDate", DbType.DateTime, DBNull.Value);
                //if (mforderVo.Units != 0)
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_Units", DbType.Double, mforderVo.Units);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_Units", DbType.Double, DBNull.Value);

                //if (!string.IsNullOrEmpty(mforderVo.ARNNo.ToString().Trim()))
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_ARNNo", DbType.String, mforderVo.ARNNo);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_ARNNo", DbType.String, DBNull.Value);

                //db.AddOutParameter(createMFOrderTrackingCmd, "@CO_OrderId", DbType.Int32, 10);

                //if (mforderVo.AgentId != 0)
                //    db.AddInParameter(createMFOrderTrackingCmd, "@AgentId", DbType.Int32, mforderVo.AgentId);
                //else
                //    db.AddInParameter(createMFOrderTrackingCmd, "@AgentId", DbType.Int32, DBNull.Value);

                //db.AddInParameter(createMFOrderTrackingCmd, "@UserId", DbType.Int32, userId);

                if (db.ExecuteNonQuery(createMFOrderTrackingCmd) != 0)
                {
                    OrderId = Convert.ToInt32(db.GetParameterValue(createMFOrderTrackingCmd, "CO_OrderId").ToString());

                    orderIds.Add(OrderId);

                }
                else
                {
                    orderIds = null;
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return orderIds;
        }
        public void CreateCustomerAssociation(int OrderId, String nomineeAssociationIds, string associateType)
        {
             
            Database db;
            DbCommand createMFOrderTrackingCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFOrderTrackingCmd = db.GetStoredProcCommand("SP_FICreateOrderAssociate");
                db.AddInParameter(createMFOrderTrackingCmd, "@CO_OrderId", DbType.Int32,  OrderId);
                db.AddInParameter(createMFOrderTrackingCmd, "@nomineeAssociationIds", DbType.String, nomineeAssociationIds+",");
                db.AddInParameter(createMFOrderTrackingCmd, "@associateType", DbType.String, associateType);
                db.ExecuteNonQuery(createMFOrderTrackingCmd);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "FIOrderDao.cs:GetOrderList()");

                object[] objects = new object[1];
               // objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public DataSet GetFISeries(int SchemeID)
        {
            DataSet dsGetFISeries;
            Database db;
            DbCommand getFISeriescmd;
            try
            {
                //  Shantanu Dated :- 18thSept2012
                //Don't Change this scripts As I am using same while MF Folio Add. If you want to change ,
                //then test the folio Add Screen also..

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getFISeriescmd = db.GetStoredProcCommand("Sp_FISeries");
                db.AddInParameter(getFISeriescmd, "@SchemeID", DbType.Int32, SchemeID);

                dsGetFISeries = db.ExecuteDataSet(getFISeriescmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFISeries;
        }
        public DataSet GetCustomerFIOrderDetails(int orderId)
        {
            DataSet dsGetCustomerMFOrderDetails;
            Database db;
            DbCommand getCustomerMFOrderDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerMFOrderDetailscmd = db.GetStoredProcCommand("SP_GetCustomerFIOrderDetails");
                db.AddInParameter(getCustomerMFOrderDetailscmd, "@orderId", DbType.Int32, orderId);
                dsGetCustomerMFOrderDetails = db.ExecuteDataSet(getCustomerMFOrderDetailscmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FIorderDao.cs:GetOrderNumber()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCustomerMFOrderDetails;
        }
        public DataSet GetCustomerFIOrderMIS(int AdviserId, DateTime dtFrom, DateTime dtTo, string branchId, string rmId, string transactionType, string status, string orderType, string amcCode, string customerId)
        {
            DataSet dsGetCustomerFIOrderMIS = null;
            Database db;
            DbCommand GetCustomerFIOrderMIScmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetCustomerFIOrderMIScmd = db.GetStoredProcCommand("SP_GetCustomerFIOrderMIS");
                db.AddInParameter(GetCustomerFIOrderMIScmd, "@adviserId", DbType.Int64, AdviserId);
                db.AddInParameter(GetCustomerFIOrderMIScmd, "@fromdate", DbType.DateTime, dtFrom);
                db.AddInParameter(GetCustomerFIOrderMIScmd, "@todate", DbType.DateTime, dtTo);
                db.AddInParameter(GetCustomerFIOrderMIScmd, "@branchId", DbType.String, branchId);
                db.AddInParameter(GetCustomerFIOrderMIScmd, "@rmId", DbType.String, rmId);
                if (!string.IsNullOrEmpty(transactionType.ToString().Trim()))
                    db.AddInParameter(GetCustomerFIOrderMIScmd, "@trxType", DbType.String, transactionType);
                else
                    db.AddInParameter(GetCustomerFIOrderMIScmd, "@trxType", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(status.ToString().Trim()))
                    db.AddInParameter(GetCustomerFIOrderMIScmd, "@orderStatus", DbType.String, status);
                else
                    db.AddInParameter(GetCustomerFIOrderMIScmd, "@orderStatus", DbType.String, DBNull.Value);
                db.AddInParameter(GetCustomerFIOrderMIScmd, "@ordertype", DbType.String, orderType);
                //if (!string.IsNullOrEmpty(amcCode.ToString().Trim()))
                //    db.AddInParameter(GetCustomerFIOrderMIScmd, "@amcCode", DbType.String, amcCode);
                //else
                //    db.AddInParameter(GetCustomerFIOrderMIScmd, "@amcCode", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(customerId.ToString().Trim()))
                    db.AddInParameter(GetCustomerFIOrderMIScmd, "@customerId", DbType.String, customerId);
                else
                    db.AddInParameter(GetCustomerFIOrderMIScmd, "@customerId", DbType.String, DBNull.Value);

                dsGetCustomerFIOrderMIS = db.ExecuteDataSet(GetCustomerFIOrderMIScmd);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            return dsGetCustomerFIOrderMIS;
        }
        public DataSet GetFISeriesDetailssDetails(int SeriesID)
        {
            DataSet dsGetFISeriesDetailss;
            Database db;
            DbCommand getFISeriesDetailsscmd;
            try
            {
                //  Shantanu Dated :- 18thSept2012
                //Don't Change this scripts As I am using same while MF Folio Add. If you want to change ,
                //then test the folio Add Screen also..

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getFISeriesDetailsscmd = db.GetStoredProcCommand("Sp_FISeriesDetails");
                db.AddInParameter(getFISeriesDetailsscmd, "@SeriesId", DbType.Int32, SeriesID);

                dsGetFISeriesDetailss = db.ExecuteDataSet(getFISeriesDetailsscmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFISeriesDetailss;
        }

    }
}
