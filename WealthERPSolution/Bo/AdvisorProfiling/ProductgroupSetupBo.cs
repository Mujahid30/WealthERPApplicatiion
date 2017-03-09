using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoAdvisorProfiling;
using VoUser;
using DaoAdvisorProfiling;
using DaoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;


namespace BoAdvisorProfiling
{
   public class ProductgroupSetupBo
    {
        public DataSet GetHierarchyDetails(int adviserId)
        {
            DataSet dsPlanOpsStaffAddStatus;
            ProductgroupSetupDao ProdGrSetupDao = new ProductgroupSetupDao();
            try
            {
                dsPlanOpsStaffAddStatus = ProdGrSetupDao.GetHierarchyDetails(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupBo.cs:GetHierarchyDetails()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsPlanOpsStaffAddStatus;
        }
        public DataSet GetTeam( )
        {
            DataSet dsPlanOpsStaffAddStatus;
            ProductgroupSetupDao ProdGrSetupDao = new ProductgroupSetupDao();
            try
            {
                dsPlanOpsStaffAddStatus = ProdGrSetupDao.GetTeam();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupBo.cs:GetTeam()");
                object[] objects = new object[1];
                //objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsPlanOpsStaffAddStatus;
        }
        public DataSet GetChanel()
        {
            DataSet dsPlanOpsStaffAddStatus;
            ProductgroupSetupDao ProdGrSetupDao = new ProductgroupSetupDao();
            try
            {
                dsPlanOpsStaffAddStatus = ProdGrSetupDao.GetChanel();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupBo.cs:GetChanel()");
                object[] objects = new object[1];
                //objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsPlanOpsStaffAddStatus;
        }


        public int GetTopSeqNo(int Adviserid)
        {
            int SeqId;
            ProductgroupSetupDao PgDao = new ProductgroupSetupDao();

            try
            {
                SeqId = PgDao.GetTopSeqNo(Adviserid);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupBo.cs:GetTopSeqNo()");
                object[] objects = new object[1];                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return SeqId;
        }
         public int GetSeqNoinChanel(int ChanelsId,  int Adviserid)
         
        {
            int ChanelId;
            ProductgroupSetupDao PgDao = new ProductgroupSetupDao();

            try
            {
                ChanelId = PgDao.GetSeqNoinChanel(ChanelsId, Adviserid);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupBo.cs:GetSeqNoinChanel()");
                object[] objects = new object[1];
                objects[0] = ChanelsId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ChanelId;
        }
        public DataSet GetReportsTo(int chanelid,  int adviserId)
        {
            DataSet dsPlanOpsStaffAddStatus;
            ProductgroupSetupDao ProdGrSetupDao = new ProductgroupSetupDao();
            try
            {
                dsPlanOpsStaffAddStatus = ProdGrSetupDao.GetReportsTo(chanelid, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupBo.cs:GetReportsTo()");
                object[] objects = new object[1];
                //objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsPlanOpsStaffAddStatus;
        }
        public DataSet GetHirarchy(int Chanelid,int adviserId) 
        {
            DataSet dsPlanOpsStaffAddStatus;
            ProductgroupSetupDao ProdGrSetupDao = new ProductgroupSetupDao();
            try
            {
                dsPlanOpsStaffAddStatus = ProdGrSetupDao.GetHirarchy(Chanelid,adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupBo.cs:GetReportsTo()");
                object[] objects = new object[1];
                //objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsPlanOpsStaffAddStatus;
        }

        public DataSet GetSeq(int ChanelId, int adviserId)
        {


            DataSet dsPlanOpsStaffAddStatus;
            ProductgroupSetupDao ProdGrSetupDao = new ProductgroupSetupDao();
            try
            {
                dsPlanOpsStaffAddStatus = ProdGrSetupDao.GetSeq(ChanelId, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupBo.cs:GetReportsTo()");
                object[] objects = new object[1];
                //objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsPlanOpsStaffAddStatus;
        }

        public bool HierarchyDetailsDetailsAddEditDelete(int adviserId, int Hid, String HierarchyName,
           int TitleId, String Teamname, int TeamId, int ReportsToId,
           string ReportsTo, string ChannelName, int ChannelId, int Sequence,
           string HierarchyType, string CommandName)
        {


            ProductgroupSetupDao PgDao = new ProductgroupSetupDao();
            bool inserted = false;
            try
            {
                inserted = PgDao.HierarchyDetailsDetailsAddEditDelete(adviserId, Hid, HierarchyName,
                    TitleId, Teamname, TeamId, ReportsToId, ReportsTo, ChannelName,
                    ChannelId, Sequence,HierarchyType, CommandName);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupBo.cs:HierarchyDetailsDetailsAddEditDelete(int adviserId, int Hid, String HierarchyName,int TitleId, String Teamname, int TeamId, int ReportsToId,string ReportsTo, string ChannelName, int ChannelId, int Sequence,string HierarchyType, string CommandName)");
                object[] objects = new object[2];
                objects[0] = adviserId;                 
                objects[1] = CommandName;
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


            ProductgroupSetupDao PgDao = new ProductgroupSetupDao();
            bool inserted = false;
            try
            {
                inserted = PgDao.HierarchyDetailsAddEditDelete(adviserId, Hid, HierarchyName,
                    TitleId, Teamname, TeamId, ReportsToId, ReportsTo, ChannelName,
                    ChannelId, Sequence, HierarchyType,ChangeATExistingSeq, CommandName);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupBo.cs:HierarchyDetailsDetailsAddEditDelete(int adviserId, int Hid, String HierarchyName,int TitleId, String Teamname, int TeamId, int ReportsToId,string ReportsTo, string ChannelName, int ChannelId, int Sequence,string HierarchyType, string CommandName)");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = CommandName;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return inserted;
        }
        public DataSet AddChannel(string Channel)
        {


            DataSet dsAddChannel;
            ProductgroupSetupDao ProdGrSetupDao = new ProductgroupSetupDao();
            try
            {
                dsAddChannel = ProdGrSetupDao.AddChannel(Channel);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupBo.cs:GetReportsTo()");
                object[] objects = new object[1];
                //objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAddChannel;
        }
        public DataSet GetMinMaxSeqNo(int AdviserId, int ChannelId)
        {


            DataSet dsGetMinMaxSeqNo;
            ProductgroupSetupDao ProdGrSetupDao = new ProductgroupSetupDao();
            try
            {
                dsGetMinMaxSeqNo = ProdGrSetupDao.GetMinMaxSeqNo(AdviserId, ChannelId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductgroupSetupBo.cs:GetReportsTo()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetMinMaxSeqNo;
        }
    }
        
    
}
