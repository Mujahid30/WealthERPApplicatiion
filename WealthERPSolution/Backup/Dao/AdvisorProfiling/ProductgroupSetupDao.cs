using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Data;
using System.Data.Common;
using VoAdvisorProfiling;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
namespace DaoAdvisorProfiling
{
    public class ProductgroupSetupDao
    {
        public int GetTopSeqNo(int Adviserid)
        {
            int SeqId = 0;
            Database db;
            DbCommand DbCmd;
            DataSet dsResult;
            DataTable dt = new DataTable();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //DbCmd = db.GetStoredProcCommand("Sp_GetSeqNoinChanel");

                DbCmd = db.GetSqlStringCommand("select  count(AH_Id) From AdviserHierarchy where A_AdviserId='" + Adviserid + "'");
                dsResult = db.ExecuteDataSet(DbCmd);
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0][0] != System.DBNull.Value)
                    { 
                                SeqId = Convert.ToInt32(dsResult.Tables[0].Rows[0][0])  ;
                                
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
                FunctionInfo.Add("Method", "ProductgroupSetupDao.cs:GetSeqNoinChanel()");
                object[] objects = new object[1];
                objects[0] = SeqId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return SeqId ;
        }
        public int GetSeqNoinChanel(int ChanelsId, int Adviserid)
        {
            int ChanelId=1;
            Database db;
            DbCommand DbCmd;
            DataSet dsResult;
            DataTable dt = new DataTable();
           
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
              //  DbCmd = db.GetStoredProcCommand("Sp_GetSeqNoinChanel");

                DbCmd = db.GetSqlStringCommand("select  MAX(AH_Sequence) From AdviserHierarchy where  AH_ChannelId =" + ChanelsId + " and   A_AdviserId='" + Adviserid + "'");
                dsResult = db.ExecuteDataSet(DbCmd);
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0][0] != System.DBNull.Value)
                    {                     
                                ChanelId = Convert.ToInt32(dsResult.Tables[0].Rows[0][0]) + 1;
                                           
                            
                         
                    }
                   
                }

                //db.AddInParameter(DbCmd, "@ChanelId", DbType.Int32, ChanelsId);

                //dsResult = db.ExecuteDataSet(DbCmd);
                //if (dsResult.Tables[0].Rows.Count > 0)
                //{
                //    if (dsResult.Tables[0].Rows[0][0]!=System.DBNull.Value)
                //    {
                //        ChanelId = Convert.ToInt32(dsResult.Tables[0].Rows[0][0]);
                //    }
                //}
                //else
                //    ChanelId = 0;


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupDao.cs:GetSeqNoinChanel()");
                object[] objects = new object[1];
                objects[0] = ChanelsId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ChanelId;
        }
        

        public DataSet GetHierarchyDetails(int adviserId)
        {
            Database db;
            DbCommand DbCmd;
            DataSet dsResult;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DbCmd = db.GetStoredProcCommand("Sp_HierarchyDetails");
                if (adviserId != 0)
                    db.AddInParameter(DbCmd, "@AdviserId", DbType.Int32, adviserId);
                else
                    db.AddInParameter(DbCmd, "@AdviserId", DbType.Int32, DBNull.Value);
                dsResult = db.ExecuteDataSet(DbCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupDao.cs:GetHierarchyDetails()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsResult;
        }

        public DataSet GetTeam( )
        {
            Database db;
            DbCommand DbCmd;
            DataSet dsResult;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DbCmd = db.GetStoredProcCommand("Sp_GetTeam");
                
                dsResult = db.ExecuteDataSet(DbCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupDao.cs:GetTeam()");
                object[] objects = new object[1];
              //  objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsResult;
        }
        public DataSet GetChanel()
        {
            Database db;
            DbCommand DbCmd;
            DataSet dsResult;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DbCmd = db.GetStoredProcCommand("Sp_GetChanel");

                dsResult = db.ExecuteDataSet(DbCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupDao.cs:GetChanel()");
                object[] objects = new object[1];
                //  objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsResult;
        }
        public DataSet GetReportsTo(int chanelid, int adviserId)
        {
            Database db;
            DbCommand DbCmd;
            DataSet dsResult;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DbCmd = db.GetStoredProcCommand("Sp_GetReportsTo");
                db.AddInParameter(DbCmd, "@ChanelId", DbType.Int32, chanelid);
                db.AddInParameter(DbCmd, "@AdviserId", DbType.Int32, adviserId);
                dsResult = db.ExecuteDataSet(DbCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupDao.cs:GetReportsTo()");
                object[] objects = new object[1];
                //  objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsResult;
        }

        public DataSet GetSeq(int ChanelId, int adviserId)
        {
            Database db;
            DbCommand DbCmd;
            DataSet dsResult;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DbCmd = db.GetStoredProcCommand("Sp_GetSeq");
                db.AddInParameter(DbCmd, "@ChanelId", DbType.Int32, ChanelId);
                db.AddInParameter(DbCmd, "@AdviserId", DbType.Int32, adviserId);

                dsResult = db.ExecuteDataSet(DbCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupDao.cs:GetReportsTo()");
                object[] objects = new object[1];
                //  objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsResult;
        }

        public DataSet GetHirarchy(int Chanelid, int adviserId)
        {
            Database db;
            DbCommand DbCmd;
            DataSet dsResult;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DbCmd = db.GetStoredProcCommand("Sp_GetHirarchy");
                if (Chanelid != 0)

                    db.AddInParameter(DbCmd, "@Chanelid", DbType.Int32, Chanelid);
                else
                    db.AddInParameter(DbCmd, "@Chanelid", DbType.Int32, DBNull.Value);
                db.AddInParameter(DbCmd, "@AdviserId", DbType.Int32, adviserId);

                dsResult = db.ExecuteDataSet(DbCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupDao.cs:Sp_GetHirarchy()");
                object[] objects = new object[1];
                //  objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsResult;
        }
        public bool HierarchyDetailsDetailsAddEditDelete(int adviserId,int Hid ,String  HierarchyName, 
            int TitleId, String Teamname, int TeamId, int ReportsToId, 
            string ReportsTo, string ChannelName, int ChannelId, int Sequence,
            string HierarchyType, string CommandName)
        {
            bool inserted = false;
            Database db;
            DbCommand InsertHierarchyDetailsCmd;
            int Result;
             
    
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                InsertHierarchyDetailsCmd = db.GetStoredProcCommand("SPROC_HierarchyDetailsAddEditDelete");

                db.AddInParameter(InsertHierarchyDetailsCmd, "@adviserId", DbType.Int32, adviserId);

                if (Hid  != 0)
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@HId", DbType.Int32, Hid);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@HId", DbType.Int32, DBNull.Value);

                if (HierarchyName  != "")
                db.AddInParameter(InsertHierarchyDetailsCmd, "@HierarchyName", DbType.String, HierarchyName);
                 else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@HierarchyName", DbType.String, DBNull.Value);

                if (TitleId != 0)

                    db.AddInParameter(InsertHierarchyDetailsCmd, "@TitleId", DbType.Int32, TitleId);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@TitleId", DbType.Int32, DBNull.Value);
               
                if (Teamname != "")
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@Teamname", DbType.String, Teamname);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@Teamname", DbType.String, DBNull.Value);

                if (TeamId != 0)
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@TeamId", DbType.Int32, TeamId);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@TeamId", DbType.Int32, DBNull.Value);

                  if (ReportsToId != 0)
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ReportsToId", DbType.Int32, ReportsToId);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ReportsToId", DbType.Int32, DBNull.Value);

                 if (ReportsTo != "")
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ReportsTo", DbType.String , ReportsTo);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ReportsTo", DbType.String, DBNull.Value);

                 if (ChannelName != "")
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ChannelName", DbType.String , ChannelName);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ChannelName", DbType.String, DBNull.Value);
               
              

                      if (ChannelId != 0)
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ChannelId", DbType.Int32 , ChannelId);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ChannelId", DbType.Int32, DBNull.Value);
               
         
                      if (Sequence != 0)
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@Sequence", DbType.Int32 , Sequence);
                    else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@Sequence", DbType.Int32, DBNull.Value);


                      if (HierarchyType !="")
                          db.AddInParameter(InsertHierarchyDetailsCmd, "@HierarchyType", DbType.String, HierarchyType);
                      else
                          db.AddInParameter(InsertHierarchyDetailsCmd, "@HierarchyType", DbType.String, DBNull.Value);

                      db.AddInParameter(InsertHierarchyDetailsCmd, "@CommandName", DbType.String, CommandName);
             //   db.AddOutParameter(InsertHierarchyDetailsCmd, "@deleted", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(InsertHierarchyDetailsCmd) != 0)
                    inserted = true;

                //if (CommandName == "Delete")
                //{

                //    Result = int.Parse(db.GetParameterValue(InsertHierarchyDetailsCmd, "deleted").ToString());
                //    if (Result == 0)
                //        inserted = false;
                //}



            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductGroupSetupDao.cs: HierarchyDetailsDetailsAddEditDelete(int adviserId,int Hid ,String  HierarchyName, int TitleId, String Teamname, int TeamId, int ReportsToId,string ReportsTo, string ChannelName, int ChannelId, int Sequence,string HierarchyType, string CommandName)");
                object[] objects = new object[2];
                objects[0] = adviserId;               
                objects[9] = CommandName;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return inserted;
        }

        public bool HierarchyDetailsAddEditDelete(int adviserId, int Hid, String HierarchyName,
            int TitleId, String Teamname, int TeamId, int ReportsToId,
            string ReportsTo, string ChannelName, int ChannelId, int Sequence,
            string HierarchyType,int ChangeATExistingSeq, string CommandName)
        {
            bool inserted = false;
            Database db;
            DbCommand InsertHierarchyDetailsCmd;
            int Result;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                InsertHierarchyDetailsCmd = db.GetStoredProcCommand("SPROC_HierarchyDetailsAddEditDelete");

                db.AddInParameter(InsertHierarchyDetailsCmd, "@adviserId", DbType.Int32, adviserId);

                if (Hid != 0)
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@HId", DbType.Int32, Hid);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@HId", DbType.Int32, DBNull.Value);

                if (HierarchyName != "")
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@HierarchyName", DbType.String, HierarchyName);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@HierarchyName", DbType.String, DBNull.Value);

                if (TitleId != 0)

                    db.AddInParameter(InsertHierarchyDetailsCmd, "@TitleId", DbType.Int32, TitleId);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@TitleId", DbType.Int32, DBNull.Value);

                if (Teamname != "")
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@Teamname", DbType.String, Teamname);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@Teamname", DbType.String, DBNull.Value);

                if (TeamId != 0)
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@TeamId", DbType.Int32, TeamId);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@TeamId", DbType.Int32, DBNull.Value);

                if (ReportsToId != 0)
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ReportsToId", DbType.Int32, ReportsToId);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ReportsToId", DbType.Int32, DBNull.Value);

                if (ReportsTo != "")
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ReportsTo", DbType.String, ReportsTo);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ReportsTo", DbType.String, DBNull.Value);

                if (ChannelName != "")
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ChannelName", DbType.String, ChannelName);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ChannelName", DbType.String, DBNull.Value);



                if (ChannelId != 0)
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ChannelId", DbType.Int32, ChannelId);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@ChannelId", DbType.Int32, DBNull.Value);


                if (Sequence != 0)
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@Sequence", DbType.Int32, Sequence);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@Sequence", DbType.Int32, DBNull.Value);


                if (HierarchyType != "")
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@HierarchyType", DbType.String, HierarchyType);
                else
                    db.AddInParameter(InsertHierarchyDetailsCmd, "@HierarchyType", DbType.String, DBNull.Value);

                db.AddInParameter(InsertHierarchyDetailsCmd, "@ChangeATExistingSeq", DbType.String, ChangeATExistingSeq);

                db.AddInParameter(InsertHierarchyDetailsCmd, "@CommandName", DbType.String, CommandName);
                //   db.AddOutParameter(InsertHierarchyDetailsCmd, "@deleted", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(InsertHierarchyDetailsCmd) != 0)
                    inserted = true;

                //if (CommandName == "Delete")
                //{

                //    Result = int.Parse(db.GetParameterValue(InsertHierarchyDetailsCmd, "deleted").ToString());
                //    if (Result == 0)
                //        inserted = false;
                //}



            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductGroupSetupDao.cs: HierarchyDetailsDetailsAddEditDelete(int adviserId,int Hid ,String  HierarchyName, int TitleId, String Teamname, int TeamId, int ReportsToId,string ReportsTo, string ChannelName, int ChannelId, int Sequence,string HierarchyType, string CommandName)");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[9] = CommandName;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return inserted;
        }

        public DataSet AddChannel(string Channel)
        {
            Database db;
            DbCommand DbCmd;
            DataSet dsResult;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DbCmd = db.GetStoredProcCommand("Sp_AddChannel");
                if (Channel!=null)
                   db.AddInParameter(DbCmd, "@AddChannel", DbType.String, Channel);
                 else
                    db.AddInParameter(DbCmd, "@AddChannel", DbType.String, DBNull.Value);
                dsResult = db.ExecuteDataSet(DbCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupDao.cs:GetHierarchyDetails()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsResult;
        }
        public DataSet GetMinMaxSeqNo(int AdviserId, int ChannelId)
        {
            Database db;
            DbCommand DbCmd;
            DataSet dsResult;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DbCmd = db.GetStoredProcCommand("SP_GetChannelMinMaxValue");
                if (AdviserId != 0)
                    db.AddInParameter(DbCmd, "@A_AdviserId", DbType.Int32, AdviserId);
                else
                    db.AddInParameter(DbCmd, "@A_AdviserId", DbType.Int32, DBNull.Value);
                if (ChannelId != 0)
                    db.AddInParameter(DbCmd, "@AH_ChannelId", DbType.Int32, ChannelId);
                else
                    db.AddInParameter(DbCmd, "@AH_ChannelId", DbType.Int32, DBNull.Value);
                dsResult = db.ExecuteDataSet(DbCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupDao.cs:GetHierarchyDetails()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsResult;
        }

    }
}
