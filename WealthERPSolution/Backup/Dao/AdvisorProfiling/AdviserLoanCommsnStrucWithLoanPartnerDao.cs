using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoAdvisorProfiling;


namespace DaoAdvisorProfiling
{
    public class AdviserLoanCommsnStrucWithLoanPartnerDao
    {
        public DataSet GetAdvisorLoanPartnerCommissionForAdviser(int AdviserId)
        {
            DataSet ds = null;

            Database db;
            DbCommand getAdvLnPartnerCommsn;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvLnPartnerCommsn = db.GetStoredProcCommand("SP_GetAdvisorLoanPartnerCommissionForAdviser");
                db.AddInParameter(getAdvLnPartnerCommsn, "@A_AdviserId", DbType.Int32, AdviserId);

                ds = db.ExecuteDataSet(getAdvLnPartnerCommsn);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserLoanCommsnStrucWithLoanPartnerDao.cs:GetAdvisorLoanPartnerCommissionForAdviser()");
                object[] objects = new object[1];
                objects[0] = AdviserId;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return ds;


        }

        public DataSet GetAdviserLoanSchemeNameForLnPtnrLnType(int LoanType, int LoanPartner, int AdviserId)
        {
            DataSet ds;

            Database db;
            DbCommand getLoanSchemeNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanSchemeNames = db.GetStoredProcCommand("SP_GetAdviserLoanSchemeNameForLnPtnrLnType");
                db.AddInParameter(getLoanSchemeNames, "@XLT_LoanTypeCode", DbType.Int32, LoanType);
                db.AddInParameter(getLoanSchemeNames, "@XLP_LoanPartnerCode", DbType.Int32, LoanPartner);
                db.AddInParameter(getLoanSchemeNames, "@A_AdviserId", DbType.Int32, AdviserId);

                ds = db.ExecuteDataSet(getLoanSchemeNames);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserLoanCommsnStrucWithLoanPartnerDao.cs:GetAdviserLoanSchemeNameForLnPtnrLnType()");
                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = LoanType;
                objects[2] = LoanPartner;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return ds;
        }

        public bool InsertAdviserLoanSchemeNameForLnPtnrLnType(AdviserLoanCommsnStrucWithLoanPartnerVo AdviserLoanCommsnStrucWithLoanPartner)
        {
            bool result = false;

            Database db;
            DbCommand cmdInsert;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsert = db.GetStoredProcCommand("SP_InsertAdvisorLoanPartnerCommissionForAdviser");
                db.AddInParameter(cmdInsert, "@ALS_LoanSchemeId", DbType.Int32, AdviserLoanCommsnStrucWithLoanPartner.LoanSchemeId );
                db.AddInParameter(cmdInsert, "@XLP_LoanPartnerCode", DbType.Int32, AdviserLoanCommsnStrucWithLoanPartner.LoanPartnerCode );
                db.AddInParameter(cmdInsert, "@XLT_LoanTypeCode", DbType.Int32, AdviserLoanCommsnStrucWithLoanPartner.LoanTypeCode );
                db.AddInParameter(cmdInsert, "@A_AdviserId", DbType.Int32, AdviserLoanCommsnStrucWithLoanPartner.AdviserId );
                db.AddInParameter(cmdInsert, "@ALPC_SlabUpperLimit", DbType.Decimal, AdviserLoanCommsnStrucWithLoanPartner.SlabUpperLimit );
                db.AddInParameter(cmdInsert, "@ALPC_SlabLowerLimit", DbType.Decimal, AdviserLoanCommsnStrucWithLoanPartner.SlabLowerLimit );
                db.AddInParameter(cmdInsert, "@ALPC_StartDate", DbType.DateTime, AdviserLoanCommsnStrucWithLoanPartner.StartDate );
                db.AddInParameter(cmdInsert, "@ALPC_EndDate", DbType.DateTime, AdviserLoanCommsnStrucWithLoanPartner.EndDate);
                db.AddInParameter(cmdInsert, "@ALPC_CommissionFee", DbType.Decimal,AdviserLoanCommsnStrucWithLoanPartner.CommissionFee );
                db.AddInParameter(cmdInsert, "@ALPC_CreatedOn", DbType.DateTime, AdviserLoanCommsnStrucWithLoanPartner.CreatedOn );
                db.AddInParameter(cmdInsert, "@ALPC_CreatedBy", DbType.Int32, AdviserLoanCommsnStrucWithLoanPartner.CreatedBy );
                db.AddInParameter(cmdInsert, "@ALPC_ModifiedOn", DbType.DateTime, AdviserLoanCommsnStrucWithLoanPartner.ModifiedOn);
                db.AddInParameter(cmdInsert, "@ALPC_ModifiedBy", DbType.Int32, AdviserLoanCommsnStrucWithLoanPartner.ModifiedBy );
                
           
           
                db.ExecuteNonQuery(cmdInsert);

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
                FunctionInfo.Add("Method", "AdviserLoanCommsnStrucWithLoanPartnerDao.cs:InsertAdviserLoanSchemeNameForLnPtnrLnType()");
                object[] objects = new object[1];
                objects[0] = AdviserLoanCommsnStrucWithLoanPartner;
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            return result;
        }

        public bool UpdateAdvisorLoanPartnerCommissionForAdviser(AdviserLoanCommsnStrucWithLoanPartnerVo AdviserLoanCommsnStrucWithLoanPartner)
        {
            bool result = false;

            Database db;
            DbCommand cmdUpdate;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdate = db.GetStoredProcCommand("SP_UpdateAdvisorLoanPartnerCommissionForAdviser");
                db.AddInParameter(cmdUpdate, "@ALPC_Id", DbType.Int32, AdviserLoanCommsnStrucWithLoanPartner.Id);
                db.AddInParameter(cmdUpdate, "@ALS_LoanSchemeId", DbType.Int32, AdviserLoanCommsnStrucWithLoanPartner.LoanSchemeId);
                db.AddInParameter(cmdUpdate, "@XLP_LoanPartnerCode", DbType.Int32, AdviserLoanCommsnStrucWithLoanPartner.LoanPartnerCode);
                db.AddInParameter(cmdUpdate, "@XLT_LoanTypeCode", DbType.Int32, AdviserLoanCommsnStrucWithLoanPartner.LoanTypeCode);
                db.AddInParameter(cmdUpdate, "@A_AdviserId", DbType.Int32, AdviserLoanCommsnStrucWithLoanPartner.AdviserId);
                db.AddInParameter(cmdUpdate, "@ALPC_SlabUpperLimit", DbType.Decimal, AdviserLoanCommsnStrucWithLoanPartner.SlabUpperLimit);
                db.AddInParameter(cmdUpdate, "@ALPC_SlabLowerLimit", DbType.Decimal, AdviserLoanCommsnStrucWithLoanPartner.SlabLowerLimit);
                db.AddInParameter(cmdUpdate, "@ALPC_StartDate", DbType.DateTime, AdviserLoanCommsnStrucWithLoanPartner.StartDate);
                db.AddInParameter(cmdUpdate, "@ALPC_EndDate", DbType.DateTime, AdviserLoanCommsnStrucWithLoanPartner.EndDate);
                db.AddInParameter(cmdUpdate, "@ALPC_CommissionFee", DbType.Decimal, AdviserLoanCommsnStrucWithLoanPartner.CommissionFee);
                db.AddInParameter(cmdUpdate, "@ALPC_ModifiedOn", DbType.DateTime, AdviserLoanCommsnStrucWithLoanPartner.ModifiedOn);
                db.AddInParameter(cmdUpdate, "@ALPC_ModifiedBy", DbType.Int32, AdviserLoanCommsnStrucWithLoanPartner.ModifiedBy);



                db.ExecuteNonQuery(cmdUpdate);

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
                FunctionInfo.Add("Method", "AdviserLoanCommsnStrucWithLoanPartnerDao.cs:UpdateAdvisorLoanPartnerCommissionForAdviser()");
                object[] objects = new object[1];
                objects[0] = AdviserLoanCommsnStrucWithLoanPartner;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            return result;
        }


        public bool DeleteAdvisorLoanPartnerCommission(int AdvisorLoanPartnerCommissionId)
        {
            bool result = false;

            Database db;
            DbCommand cmdDelete;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdDelete = db.GetStoredProcCommand("SP_DeleteAdvisorLoanPartnerCommission");
                db.AddInParameter(cmdDelete, "@ALPC_Id", DbType.Int32, AdvisorLoanPartnerCommissionId);



                db.ExecuteNonQuery(cmdDelete);

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
                FunctionInfo.Add("Method", "AdviserLoanCommsnStrucWithLoanPartnerDao.cs:DeleteAdvisorLoanPartnerCommission()");
                object[] objects = new object[1];
                objects[0] = AdvisorLoanPartnerCommissionId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            return result;
        }
    }
}
