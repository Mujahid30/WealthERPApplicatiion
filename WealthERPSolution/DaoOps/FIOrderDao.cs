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
                    TaxStatus =  dtTaxStatus.Rows[0]["WCMV_Name"].ToString() ;
                 
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

        public DataSet GetTaxStatus1(int customerId)
        {
            DataSet dsOrderNumber;
            DataTable dtTaxStatus;
            Database db;
            DbCommand getTaxStatuscmd;
            string TaxStatus = "";
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getTaxStatuscmd = db.GetStoredProcCommand("Sp_GetTaxStatus");
                db.AddInParameter(getTaxStatuscmd, "@CustID", DbType.Int32, customerId);

                dsOrderNumber = db.ExecuteDataSet(getTaxStatuscmd);
                

            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsOrderNumber;

        }
        public Int64  GetFaceValue(int issueId)
        {
            DataSet dsOrderNumber;
            DataTable dtTaxStatus;
            Database db;
            DbCommand getTaxStatuscmd;
            Int64 Facevalue = 0;
            string Fv="0";
            double f1 = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getTaxStatuscmd = db.GetStoredProcCommand("SPROC_GetFaceValue");
                db.AddInParameter(getTaxStatuscmd, "@issueId", DbType.Int32, issueId);

                dsOrderNumber = db.ExecuteDataSet(getTaxStatuscmd);
                dtTaxStatus = dsOrderNumber.Tables[0];
                if (dtTaxStatus.Rows.Count > 0)                    
                f1 = Convert.ToDouble(dtTaxStatus.Rows[0]["AIM_Facevalue"].ToString());
                Facevalue = Convert.ToInt64(f1);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }

            return Facevalue;
        }

        public bool UpdateFITransactionForSynch(int gvOrderId, String gvSchemeCode, int gvaccountId, string gvTrxType, int gvPortfolioId, double gvAmount, out bool status, DateTime gvOrderDate)
        {
            Database db;
            DbCommand updateFITransactionForSynchCmd;
            int affectedRecords = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateFITransactionForSynchCmd = db.GetStoredProcCommand("SP_UpdateFITransactionForSync");
                db.AddInParameter(updateFITransactionForSynchCmd, "@orderId", DbType.Int32, gvOrderId);
                db.AddInParameter(updateFITransactionForSynchCmd, "@schemeCode", DbType.String, gvSchemeCode);
                db.AddInParameter(updateFITransactionForSynchCmd, "@accountId", DbType.Int32, gvaccountId);
                db.AddInParameter(updateFITransactionForSynchCmd, "@trxType", DbType.String, gvTrxType);
                //db.AddInParameter(updateFITransactionForSynchCmd, "@portfolioId", DbType.Int32, gvPortfolioId);
                db.AddInParameter(updateFITransactionForSynchCmd, "@amount", DbType.Double, gvAmount);
                db.AddInParameter(updateFITransactionForSynchCmd, "@orderDate", DbType.DateTime, gvOrderDate);
                db.AddOutParameter(updateFITransactionForSynchCmd, "@IsSuccess", DbType.Int16, 0);
                if (db.ExecuteNonQuery(updateFITransactionForSynchCmd) != 0)
                    affectedRecords = int.Parse(db.GetParameterValue(updateFITransactionForSynchCmd, "@IsSuccess").ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:UpdateFITransactionForSynch()");
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
        //public int GetOrderNumber()
        //{
        //    DataSet dsOrderNumber;
        //    DataTable dtOrderNumber;
        //    int orderNumber = 0;
        //    Database db;
        //    DbCommand getSchemeSwitchcmd;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getSchemeSwitchcmd = db.GetStoredProcCommand("Sp_FIOrderNO");
        //        dsOrderNumber = db.ExecuteDataSet(getSchemeSwitchcmd);
        //        dtOrderNumber = dsOrderNumber.Tables[0];
        //        if (dtOrderNumber.Rows.Count > 0)
        //            orderNumber = int.Parse(dtOrderNumber.Rows[0]["CFIOD_DetailsId"].ToString());
        //        else
        //            orderNumber = 999;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw (Ex);
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "OperationDao.cs:GetOrderNumber()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return orderNumber;
        //}


        public DataSet GetFIModeOfHolding()
        {
            DataSet dsGetFICategory;
            Database db;
            DbCommand getFICategorycmd;
            try
            {
                //  Shantanu Dated :- 18thSept2012
                //Don't Change this scripts As I am using same while FI Folio Add. If you want to change ,
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
                //Don't Change this scripts As I am using same while FI Folio Add. If you want to change ,
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
                //Don't Change this scripts As I am using same while FI Folio Add. If you want to change ,
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


        public void GetTenure(int seriesID,out int minTenure  , out int maxtenure)
        {
            DataSet dsGetFITenure;
            Database db;
            DbCommand getFITenurecmd;
            minTenure = 0;
            maxtenure = 0;
            try
            {
                //  Shantanu Dated :- 18thSept2012
                //Don't Change this scripts As I am using same while FI Folio Add. If you want to change ,
                //then test the folio Add Screen also..

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getFITenurecmd = db.GetStoredProcCommand("Sp_GetMinMaxTenure");
                db.AddInParameter(getFITenurecmd, "@SeriesID", DbType.Int32, seriesID);
                dsGetFITenure = db.ExecuteDataSet(getFITenurecmd);

                if (dsGetFITenure.Tables[0].Rows.Count > 0)
                {
                    if (dsGetFITenure.Tables[0].Rows[0]["MinTenure"].ToString() != "")
                        minTenure = Convert.ToInt32(dsGetFITenure.Tables[0].Rows[0]["MinTenure"].ToString());
                    if (dsGetFITenure.Tables[0].Rows[0]["MaxTenure"].ToString()!="")
                    maxtenure = Convert.ToInt32(dsGetFITenure.Tables[0].Rows[0]["MaxTenure"].ToString());
                }
               
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
             
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
                //Don't Change this scripts As I am using same while FI Folio Add. If you want to change ,
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
        public bool DeleteFIOrder(int orderId)
        {
            bool bResult = false;
            Database db;
            DbCommand DeleteMFOrderCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DeleteMFOrderCmd = db.GetStoredProcCommand("[SP_DeleteCustomerFIOrder]");
                db.AddInParameter(DeleteMFOrderCmd, "@OrderId", DbType.Int32, orderId);
                db.ExecuteNonQuery(DeleteMFOrderCmd);
                bResult = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public List<int> CreateOrderFIDetails(OrderVo orderVo, FIOrderVo FIorderVo, int userId, string Mode)
        {
            List<int> orderIds = new List<int>();
            int OrderId;
            Database db;
            DbCommand createFIOrderTrackingCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                if (Mode == "Submit")
                {
                    createFIOrderTrackingCmd = db.GetStoredProcCommand("[SP_CreateCustomerFIOrderDetails]");
                    db.AddOutParameter(createFIOrderTrackingCmd, "@CO_OrderId", DbType.Int32, orderVo.OrderId);

                }
                else
                {
                    createFIOrderTrackingCmd = db.GetStoredProcCommand("SP_UpdateCustomerFIOrderDetails");
                    db.AddInParameter(createFIOrderTrackingCmd, "@CO_OrderId", DbType.Int32, orderVo.OrderId);

                }

                if (orderVo.OrderDate != DateTime.MinValue)
                    db.AddInParameter(createFIOrderTrackingCmd, "@CO_OrderDate", DbType.DateTime, orderVo.OrderDate);
                else
                    db.AddInParameter(createFIOrderTrackingCmd, "@CO_OrderDate", DbType.DateTime, DBNull.Value);

                //db.AddInParameter(createFIOrderTrackingCmd, "@CO_OrderDate", DbType.DateTime, orderVo.OrderDate );
                db.AddInParameter(createFIOrderTrackingCmd, "@CustomerId", DbType.Int32, orderVo.CustomerId);
                db.AddInParameter(createFIOrderTrackingCmd, "@WOSR_SourceCode", DbType.String, "");
                db.AddInParameter(createFIOrderTrackingCmd, "@ApplicationNumber", DbType.String, orderVo.ApplicationNumber);
                if (orderVo.ApplicationReceivedDate != DateTime.MinValue)
                    db.AddInParameter(createFIOrderTrackingCmd, "@ApplicationReceivedDate", DbType.DateTime, orderVo.ApplicationReceivedDate);
                else
                    db.AddInParameter(createFIOrderTrackingCmd, "@ApplicationReceivedDate", DbType.DateTime, DBNull.Value);

             
                db.AddInParameter(createFIOrderTrackingCmd, "@ChequeNumber", DbType.String, orderVo.ChequeNumber);
                if (orderVo.PaymentDate != DateTime.MinValue)
                    db.AddInParameter(createFIOrderTrackingCmd, "@PaymentDate", DbType.DateTime, orderVo.PaymentDate);
                else
                    db.AddInParameter(createFIOrderTrackingCmd, "@PaymentDate", DbType.DateTime, DBNull.Value);

                
                db.AddInParameter(createFIOrderTrackingCmd, "@CustBankAccId", DbType.Int32, orderVo.CustBankAccId);

                db.AddInParameter(createFIOrderTrackingCmd, "@AA_AdviserAssociateId", DbType.Int32, orderVo.AssociationId);
                db.AddInParameter(createFIOrderTrackingCmd, "@AAC_AdviserAgentId", DbType.Int32, orderVo.AgentId);
                db.AddInParameter(createFIOrderTrackingCmd, "@AAC_AgentCode", DbType.String, orderVo.AgentCode);

                db.AddInParameter(createFIOrderTrackingCmd, "@AssetGroupCode", DbType.String, orderVo.AssetGroup);
                db.AddInParameter(createFIOrderTrackingCmd, "@PaymentMode", DbType.String, orderVo.PaymentMode);
                db.AddInParameter(createFIOrderTrackingCmd, "@Isclose", DbType.Int32, 0);

                db.AddInParameter(createFIOrderTrackingCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, FIorderVo.AssetInstrumentCategoryCode);
                db.AddInParameter(createFIOrderTrackingCmd, "@PFIIM_IssuerId", DbType.String, FIorderVo.IssuerId);
                db.AddInParameter(createFIOrderTrackingCmd, "@PFISM_SchemeId", DbType.Int32, FIorderVo.SchemeId);
                db.AddInParameter(createFIOrderTrackingCmd, "@PFISD_SeriesId", DbType.Int32, FIorderVo.SeriesId);
                db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_TransactionType", DbType.String, FIorderVo.TransactionType);
                db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_OrderNO", DbType.String, orderVo.OrderNumber);
                db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_AmountPayable", DbType.Double, FIorderVo.AmountPayable);
                db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_ModeOfHolding", DbType.String, FIorderVo.ModeOfHolding);
                db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_SchemeOption", DbType.String, FIorderVo.Schemeoption);
                db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_DepositPayableTo", DbType.String, FIorderVo.Depositpayableto);
                db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_Frequency", DbType.String, FIorderVo.Frequency);
                db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_Privilidge", DbType.String, FIorderVo.Privilidge);
                db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_ExisitingDepositreceiptno", DbType.String, FIorderVo.ExisitingDepositreceiptno);
                db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_RenewalAmount", DbType.Double, FIorderVo.RenewalAmount);
                if (FIorderVo.MaturityDate != DateTime.MinValue)
                    db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_MaturityDate", DbType.DateTime, FIorderVo.MaturityDate);
                else
                    db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_MaturityDate", DbType.DateTime, DBNull.Value);

                //db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_MaturityDate", DbType.DateTime, FIorderVo.MaturityDate);
                db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_MaturityAmount", DbType.Double, FIorderVo.MaturityAmount);

                db.AddInParameter(createFIOrderTrackingCmd, "@CFIOD_DepCustBankAccId", DbType.Int32, FIorderVo.DepCustBankAccId);

                db.AddInParameter(createFIOrderTrackingCmd, "@AgentId", DbType.Int32, FIorderVo.AgentId);
                db.AddInParameter(createFIOrderTrackingCmd, "@UserId", DbType.Int32, userId );

                db.AddInParameter(createFIOrderTrackingCmd, "@DematAcntId", DbType.Int32, FIorderVo.DematAccountId);
                db.AddInParameter(createFIOrderTrackingCmd, "@Qty", DbType.Double, FIorderVo.Qty);
                db.AddInParameter(createFIOrderTrackingCmd, "@BranchName", DbType.String, orderVo.BankBranchName);
                

                if (db.ExecuteNonQuery(createFIOrderTrackingCmd) != 0)
                {
                    if (Mode == "Submit")
                    {
                        OrderId = Convert.ToInt32(db.GetParameterValue(createFIOrderTrackingCmd, "CO_OrderId").ToString());

                        orderIds.Add(OrderId);
                    }

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
            DbCommand createFIOrderTrackingCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createFIOrderTrackingCmd = db.GetStoredProcCommand("SP_FICreateOrderAssociate");
                db.AddInParameter(createFIOrderTrackingCmd, "@CO_OrderId", DbType.Int32,  OrderId);
                db.AddInParameter(createFIOrderTrackingCmd, "@nomineeAssociationIds", DbType.String, nomineeAssociationIds+",");
                db.AddInParameter(createFIOrderTrackingCmd, "@associateType", DbType.String, associateType);
                db.ExecuteNonQuery(createFIOrderTrackingCmd);
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
                //Don't Change this scripts As I am using same while FI Folio Add. If you want to change ,
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
        public DataSet GetFIProof( )
        {
            DataSet dsGetCustomerFIOrderDetails;
            Database db;
            DbCommand getCustomerFIOrderDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerFIOrderDetailscmd = db.GetStoredProcCommand("Sp_GetFIProof");
               
                dsGetCustomerFIOrderDetails = db.ExecuteDataSet(getCustomerFIOrderDetailscmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FIorderDao.cs:GetFIProof()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCustomerFIOrderDetails;
        }
        public DataSet GetCustomerFIOrderDetails(int orderId)
        {
            DataSet dsGetCustomerFIOrderDetails;
            Database db;
            DbCommand getCustomerFIOrderDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerFIOrderDetailscmd = db.GetStoredProcCommand("SP_GetCustomerFIOrderDetails");
                db.AddInParameter(getCustomerFIOrderDetailscmd, "@orderId", DbType.Int32, orderId);
                dsGetCustomerFIOrderDetails = db.ExecuteDataSet(getCustomerFIOrderDetailscmd);
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
            return dsGetCustomerFIOrderDetails;
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
                //Don't Change this scripts As I am using same while FI Folio Add. If you want to change ,
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
        public DataTable Get54ECOrderDetails(int orderId)
        {
            DataSet dsGet54ECOrderDetails;
            Database db;
            DbCommand getGet54ECOrderDetails;
            DataTable dtGet54ECOrderDetails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGet54ECOrderDetails = db.GetStoredProcCommand("SPROC_View54ECDetails");
                db.AddInParameter(getGet54ECOrderDetails, "@orderId", DbType.Int32, orderId);
                dsGet54ECOrderDetails = db.ExecuteDataSet(getGet54ECOrderDetails);
                dtGet54ECOrderDetails = dsGet54ECOrderDetails.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dtGet54ECOrderDetails;
        }
    }
}
