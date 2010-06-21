using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

using System.Threading;
using System.Globalization;

using System.Data;
using System.Data.Sql;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.ApplicationBlocks.ExceptionManagement;

using VoCustomerGoalProfiling;
using VoUser;




namespace DaoCustomerGoalProfiling
{
    public class CustomerGoalSetupDao
    {

        public void CreateCustomerGoalProfile(GoalProfileSetupVo GoalProfileVo, int UserId)
        {
            Database db;
            DbCommand createCustomerGoalProfileCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerGoalProfileCmd = db.GetStoredProcCommand("SP_SaveCustomerGoalProfile");
                db.AddInParameter(createCustomerGoalProfileCmd, "@CustomerId", DbType.Int32, GoalProfileVo.CustomerId);
                db.AddInParameter(createCustomerGoalProfileCmd, "@GoalCode", DbType.String, GoalProfileVo.Goalcode);
                db.AddInParameter(createCustomerGoalProfileCmd, "@CostToday", DbType.Double, GoalProfileVo.CostOfGoalToday);
                db.AddInParameter(createCustomerGoalProfileCmd, "@GoalYear", DbType.Int32, GoalProfileVo.GoalYear);
                db.AddInParameter(createCustomerGoalProfileCmd, "@GoalProfileDate", DbType.DateTime, GoalProfileVo.GoalDate);
                db.AddInParameter(createCustomerGoalProfileCmd, "@CorpusRequired", DbType.Double, GoalProfileVo.RetirementCorpus);
                db.AddInParameter(createCustomerGoalProfileCmd, "@MonthlySavingsRequired", DbType.Double,GoalProfileVo.MonthlySavingsReq);
                if (GoalProfileVo.AssociateId != 0)
                {
                    db.AddInParameter(createCustomerGoalProfileCmd, "@AssociateId", DbType.Int32, GoalProfileVo.AssociateId);
                }
                db.AddInParameter(createCustomerGoalProfileCmd, "@ROIEarned", DbType.Double, GoalProfileVo.ROIEarned);
                db.AddInParameter(createCustomerGoalProfileCmd, "@CurrentInvestment", DbType.Double, GoalProfileVo.CurrInvestementForGoal);
                db.AddInParameter(createCustomerGoalProfileCmd, "@ExpectedROI", DbType.Double, GoalProfileVo.ExpectedROI);
                db.AddInParameter(createCustomerGoalProfileCmd, "@IsActive", DbType.Int16, GoalProfileVo.IsActice);
                db.AddInParameter(createCustomerGoalProfileCmd, "@InflationPer", DbType.Double, GoalProfileVo.InflationPercent);
                if (GoalProfileVo.CustomerApprovedOn !=DateTime.Parse("01/01/0001 00:00:00"))
                {
                    db.AddInParameter(createCustomerGoalProfileCmd, "@CustomerApprovedOn", DbType.DateTime, GoalProfileVo.CustomerApprovedOn);
                }
                db.AddInParameter(createCustomerGoalProfileCmd, "@Comments", DbType.String, GoalProfileVo.Comments);
                db.AddInParameter(createCustomerGoalProfileCmd, "@GoalDescription", DbType.String, GoalProfileVo.GoalDescription);
                db.AddInParameter(createCustomerGoalProfileCmd, "@ROIOnFuture", DbType.Double, GoalProfileVo.RateofInterestOnFture);
                db.AddInParameter(createCustomerGoalProfileCmd, "@CreatedBy", DbType.Int32, GoalProfileVo.CreatedBy);

                db.ExecuteNonQuery(createCustomerGoalProfileCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "GoalProfileSetupDao.cs:CreateCustomerGoalProfile()");


                object[] objects = new object[1];
                objects[0] = GoalProfileVo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }


        public void UpdateCustomerGoalProfile(GoalProfileSetupVo GoalProfileVo, int GolaId)
        {
            Database db;
            DbCommand updateCustomerGoalProfileCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCustomerGoalProfileCmd = db.GetStoredProcCommand("SP_UpdateCustomerGoalProfile");
                db.AddInParameter(updateCustomerGoalProfileCmd, "@GoalId", DbType.Int32, GolaId);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@CustomerId", DbType.Int32, GoalProfileVo.CustomerId);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@GoalCode", DbType.String, GoalProfileVo.Goalcode);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@CostToday", DbType.Double, GoalProfileVo.CostOfGoalToday);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@GoalYear", DbType.Int32, GoalProfileVo.GoalYear);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@GoalProfileDate", DbType.DateTime, GoalProfileVo.GoalDate);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@MonthlySavingsRequired", DbType.Double, GoalProfileVo.MonthlySavingsReq);
                if (GoalProfileVo.AssociateId != 0)
                {
                    db.AddInParameter(updateCustomerGoalProfileCmd, "@AssociateId", DbType.Int32, GoalProfileVo.AssociateId);
                }
                db.AddInParameter(updateCustomerGoalProfileCmd, "@ROIEarned", DbType.Double, GoalProfileVo.ROIEarned);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@CurrentInvestment", DbType.Double, GoalProfileVo.CurrInvestementForGoal);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@ExpectedROI", DbType.Double, GoalProfileVo.ExpectedROI);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@IsActive", DbType.Int16, GoalProfileVo.IsActice);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@InflationPer", DbType.Double, GoalProfileVo.InflationPercent);
                if (GoalProfileVo.CustomerApprovedOn != DateTime.Parse("01/01/0001 00:00:00"))
                {
                    db.AddInParameter(updateCustomerGoalProfileCmd, "@CustomerApprovedOn", DbType.DateTime, GoalProfileVo.CustomerApprovedOn);
                }
                db.AddInParameter(updateCustomerGoalProfileCmd, "@Comments", DbType.String, GoalProfileVo.Comments);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@GoalDescription", DbType.String, GoalProfileVo.GoalDescription);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@ROIOnFuture", DbType.Double, GoalProfileVo.RateofInterestOnFture);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@CreatedBy", DbType.Int32, GoalProfileVo.CreatedBy);

                db.ExecuteNonQuery(updateCustomerGoalProfileCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "GoalProfileSetupDao.cs:UpdateCustomerGoalProfile()");


                object[] objects = new object[2];
                objects[0] = GoalProfileVo;
                objects[1] = GolaId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }
        public DataSet GetCustomerGoalProfiling()
        {
            Database db;
            DbCommand getGoalObjTypeCmd;
            DataSet getGoalObjTypeDs;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGoalObjTypeCmd = db.GetStoredProcCommand("SP_GetGoalName");
                getGoalObjTypeDs = db.ExecuteDataSet(getGoalObjTypeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupDao:GetGoalObjectiveType()");


                object[] objects = new object[1];
                objects[0] = "SP_GetGoalName";

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getGoalObjTypeDs;
        }

        public DataSet GetCustomerAssociationDetails(int CustomerID)
        {
            Database db;
            DbCommand getAssociationCmd;
            DataSet getAssociationDs;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssociationCmd = db.GetStoredProcCommand("SP_GetCustomerAssociationDetails");
                db.AddInParameter(getAssociationCmd, "@Customer_ID", DbType.Int32, CustomerID);
                getAssociationDs = db.ExecuteDataSet(getAssociationCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupDao:GetCustomerAssociationDetails()");


                object[] objects = new object[1];
                objects[0] = CustomerID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getAssociationDs;
        }
        public Decimal GetInflationPercent()
        {
            Database db;
            DbCommand getInflationCmd;
            Decimal InflationRate = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getInflationCmd = db.GetStoredProcCommand("SP_GetInflationRate");
                db.AddOutParameter(getInflationCmd, "@InflationRate", DbType.Decimal,0);
                db.ExecuteNonQuery(getInflationCmd);
                Object objInflationRate = db.GetParameterValue(getInflationCmd, "@InflationRate");
                if(objInflationRate != DBNull.Value )
                    InflationRate = (decimal)db.GetParameterValue(getInflationCmd, "@InflationRate");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupDao:GetInflationPercent()");


                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return InflationRate;

        }


        public Decimal GetExpectedROI(int CustomerID)
        {
            Database db;
            DbCommand getExpROICmd;
            Decimal ExpectedROI = 0;
            try
            {
                 db = DatabaseFactory.CreateDatabase("wealtherp");
                 getExpROICmd = db.GetStoredProcCommand("SP_GetExpectedROI");
                 db.AddInParameter(getExpROICmd, "@C_CustomerId", DbType.Int32, CustomerID);

                 IDbDataParameter ExpROI = getExpROICmd.CreateParameter();
                 ExpROI.ParameterName = "@ExpectedROI";
                 ExpROI.DbType = DbType.Decimal;
                 ExpROI.Scale = 3;
                 //ExpROI.Precision = 3;
                 ExpROI.Direction = ParameterDirection.Output;
                 getExpROICmd.Parameters.Add(ExpROI);

                 //IDbDataParameter DecimalOut = comm.CreateParameter();
                 //DecimalOut.Direction = ParameterDirection.Output;
                 // DecimalOut.ParameterName = "@Price";
                 // DecimalOut.DbType = DbType.Decimal;
                 //DecimalOut.Scale = 4;
                 //comm.Parameters.Add(DecimalOut); 


                 //db.AddOutParameter(getExpROICmd, "@ExpectedROI", DbType.Decimal, 0);

                 db.ExecuteNonQuery(getExpROICmd);
                 ExpectedROI = (Decimal)db.GetParameterValue(getExpROICmd, "@ExpectedROI");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupDao:GetExpectedROI()");


                object[] objects = new object[1];
                objects[0] = CustomerID;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ExpectedROI;
            
        }

        public List<GoalProfileSetupVo> GetCustomerGoalProfile(int CustomerId, int ActiveFlag)
        {
            DataSet ds = null;
            Database db;
            DbCommand getCustomerGoalProfileCmd;
            List<GoalProfileSetupVo> GoalProfileList = null;
            GoalProfileSetupVo GoalProfileVo = new GoalProfileSetupVo();
            DataTable dtGoalProfile = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerGoalProfileCmd = db.GetStoredProcCommand("SP_GetCustomerGoalProfileDetails");
                db.AddInParameter(getCustomerGoalProfileCmd, "@CustomerId", DbType.Int32, CustomerId);
                db.AddInParameter(getCustomerGoalProfileCmd, "@ActiveFlag", DbType.Int32, ActiveFlag);

                ds = db.ExecuteDataSet(getCustomerGoalProfileCmd);
               

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtGoalProfile = ds.Tables[0];
                    GoalProfileList = new List<GoalProfileSetupVo>();
                    foreach (DataRow dr in dtGoalProfile.Rows)
                    {
                        GoalProfileVo = new GoalProfileSetupVo();
                        GoalProfileVo.GoalId=int.Parse(dr["CG_GoalId"].ToString());
                        GoalProfileVo.Goalcode=dr["XG_GoalCode"].ToString();

                        if (dr["XG_GoalName"].ToString() != string.Empty)
                        {
                            GoalProfileVo.GoalName = dr["XG_GoalName"].ToString();

                        }
                        else
                            GoalProfileVo.GoalName = string.Empty;

                        GoalProfileVo.ChildName = dr["ChildName"].ToString();
                        GoalProfileVo.CostOfGoalToday = double.Parse(dr["CG_CostToday"].ToString());
                        GoalProfileVo.MonthlySavingsReq = double.Parse(dr["CG_MonthlySavingsRequired"].ToString());
                        GoalProfileVo.GoalProfileDate = DateTime.Parse(dr["CG_GoalProfileDate"].ToString());
                        GoalProfileVo.GoalYear = int.Parse(dr["CG_GoalYear"].ToString());
                        GoalProfileVo.IsActice = int.Parse(dr["CG_IsActive"].ToString());

                        if ((dr["CG_CustomerApprovedOn"].ToString()) != string.Empty)
                        {
                            GoalProfileVo.CustomerApprovedOn = DateTime.Parse(dr["CG_CustomerApprovedOn"].ToString());
 
                        }
                       
                         
                          GoalProfileList.Add(GoalProfileVo);
                                              

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
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetCustomerGoalProfile()");
                object[] objects = new object[1];
                objects[0] = CustomerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return GoalProfileList;
        }
        public DataSet GetCustomerRTDetails(int CustomerID)
        {
            Database db;
            DbCommand getRTCmd;
            DataSet getRTDs;
           
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRTCmd = db.GetStoredProcCommand("SP_GetCustomerRetGoal");
                db.AddInParameter(getRTCmd, "@CustomerId", DbType.Int32, CustomerID);
                getRTDs = db.ExecuteDataSet(getRTCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupDao:GetCustomerRTDetails()");


                object[] objects = new object[1];
                objects[0] = CustomerID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getRTDs;
        }
        public void SetCustomerlGoalIsActive(String GoalIDs, int UserId)
        {
            Database db;
            DbCommand SetCustomerGoalActiveCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                SetCustomerGoalActiveCmd = db.GetStoredProcCommand("SP_SetCustomerGoalActive");
                db.AddInParameter(SetCustomerGoalActiveCmd, "@CustomerId", DbType.Int32, UserId);
                db.AddInParameter(SetCustomerGoalActiveCmd, "@GaolIds", DbType.String, GoalIDs);
                db.ExecuteNonQuery(SetCustomerGoalActiveCmd);


            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "GoalProfileSetupDao.cs:SetCustomerlGoalIsActive()");


                object[] objects = new object[2];
                objects[0] = GoalIDs;
                objects[1] = UserId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }


        public void SetCustomerlGoalDeActive(String GoalIDs, int UserId)
        {
            Database db;
            DbCommand SetCustomerGoalDeActiveCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                SetCustomerGoalDeActiveCmd = db.GetStoredProcCommand("SP_SetCustomerGoalDeActive");
                db.AddInParameter(SetCustomerGoalDeActiveCmd, "@CustomerId", DbType.Int32, UserId);
                db.AddInParameter(SetCustomerGoalDeActiveCmd, "@GaolIds", DbType.String, GoalIDs);
                db.ExecuteNonQuery(SetCustomerGoalDeActiveCmd);


            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "GoalProfileSetupDao.cs:SetCustomerlGoalDeActive()");


                object[] objects = new object[2];
                objects[0] = GoalIDs;
                objects[1] = UserId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }


        public void DeleteCustomerlGoal(String GoalIDs, int UserId)
        {
            Database db;
            DbCommand DeleteGoalCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DeleteGoalCmd = db.GetStoredProcCommand("SP_DeleteCustomerGoal");
                db.AddInParameter(DeleteGoalCmd, "@CustomerId", DbType.Int32, UserId);
                db.AddInParameter(DeleteGoalCmd, "@GaolIds", DbType.String, GoalIDs);
                db.ExecuteNonQuery(DeleteGoalCmd);


            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupDao.cs:DeleteCustomerlGoal()");


                object[] objects = new object[2];
                objects[0] = GoalIDs;
                objects[1] = UserId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }




        public GoalProfileSetupVo GetCustomerGoal(int CustomerId, int GoalId)
        {
            GoalProfileSetupVo GoalProfileVo = new GoalProfileSetupVo();
            Database db;
            DbCommand getCustomerGoalCmd;
            DataSet getCustomerGoalDs;
            DataRow dr;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerGoalCmd = db.GetStoredProcCommand("SP_GetCustomerGoalProfile");
                db.AddInParameter(getCustomerGoalCmd, "@CustomerId", DbType.Int32, CustomerId);
                db.AddInParameter(getCustomerGoalCmd, "@GoalId", DbType.Int32, GoalId);
                getCustomerGoalDs = db.ExecuteDataSet(getCustomerGoalCmd);



                if (getCustomerGoalDs.Tables[0].Rows.Count == 1)
                {
                    dr = getCustomerGoalDs.Tables[0].Rows[0];

                    GoalProfileVo.GoalId = int.Parse(dr["CG_GoalId"].ToString());
                    GoalProfileVo.GoalDate = DateTime.Parse(dr["CG_GoalProfileDate"].ToString());
                    GoalProfileVo.GoalName = dr["XG_GoalName"].ToString();
                    GoalProfileVo.Goalcode = dr["XG_GoalCode"].ToString();
                    GoalProfileVo.GoalDescription = dr["CG_GoalDescription"].ToString();
                    GoalProfileVo.ChildName = dr["ChildName"].ToString();
                    GoalProfileVo.CostOfGoalToday = double.Parse(dr["CG_CostToday"].ToString());
                    GoalProfileVo.GoalYear = int.Parse(dr["CG_GoalYear"].ToString());
                    GoalProfileVo.CurrInvestementForGoal = double.Parse(dr["CG_CurrentInvestment"].ToString());
                    GoalProfileVo.ROIEarned = double.Parse(dr["CG_ROIEarned"].ToString());
                    GoalProfileVo.ExpectedROI = double.Parse(dr["CG_ExpectedROI"].ToString());
                    if (dr["CA_AssociateId"].ToString()!=string.Empty)
                    GoalProfileVo.AssociateId = int.Parse(dr["CA_AssociateId"].ToString());

                    if (dr["CG_ROIOnFuture"].ToString() != string.Empty)
                    {
                        GoalProfileVo.RateofInterestOnFture = double.Parse(dr["CG_ROIOnFuture"].ToString());
                       
                    }
                    else
                        GoalProfileVo.RateofInterestOnFture = 0;
                    
                    GoalProfileVo.Comments = dr["CG_Comments"].ToString();

                    if (dr["CG_CustomerApprovedOn"].ToString() != string.Empty)
                        GoalProfileVo.CustomerApprovedOn = DateTime.Parse(dr["CG_CustomerApprovedOn"].ToString());
                    else
                        GoalProfileVo.CustomerApprovedOn = DateTime.MinValue;

                   
                                        
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

                FunctionInfo.Add("Method", "CustomerGoalSetupDao:GetCustomerGoal()");


                object[] objects = new object[2];
                objects[0] = CustomerId;
                objects[1] = GoalId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return GoalProfileVo;
        }
        /// <summary>
        /// Checking Goals Exists for the customer, before risk profilling saving to DB.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int CheckGoalProfile(int UserId)
        {
            Database db;
            DbCommand CheckGPCmd;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CheckGPCmd = db.GetStoredProcCommand("SP_CheckForGoals");
                db.AddInParameter(CheckGPCmd, "@CustomerId", DbType.Int32, UserId);
                db.AddOutParameter(CheckGPCmd, "@Flag", DbType.Int32, count);
                db.ExecuteNonQuery(CheckGPCmd);
                count = (int)db.GetParameterValue(CheckGPCmd, "@Flag");
                return count;
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException baseEx = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RiskProfileDao.cs:CheckGoalProfile()");
                object[] objects = new object[1];
                objects[0] = UserId;
                FunctionInfo = baseEx.AddObject(FunctionInfo, objects);
                baseEx.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(baseEx);
                throw baseEx;
            }

        }

        public void SetCustomerAllGoalDeActive(int UserId)
        {
            Database db;
            DbCommand SetCustomerAllGoalDeActiveCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                SetCustomerAllGoalDeActiveCmd = db.GetStoredProcCommand("SP_CustomerAllGoalDeActive");
                db.AddInParameter(SetCustomerAllGoalDeActiveCmd, "@CustomerId", DbType.Int32, UserId);
                db.ExecuteNonQuery(SetCustomerAllGoalDeActiveCmd);


            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "GoalProfileSetupDao.cs:SetCustomerAllGoalDeActive()");


                object[] objects = new object[1];
                objects[1] = UserId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }

    }
}
