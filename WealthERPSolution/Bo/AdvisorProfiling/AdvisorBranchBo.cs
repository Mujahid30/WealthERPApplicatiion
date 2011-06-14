using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Web;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Data;
using System.Data.Common;

using VoAdvisorProfiling;
using DaoAdvisorProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoUser;

namespace BoAdvisorProfiling
{
    public class AdvisorBranchBo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="advisorBranchVo"></param>
        /// <param name="advisorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int CreateAdvisorBranch(AdvisorBranchVo advisorBranchVo, int advisorId,int userId)
        {
            int branchId;         
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
               branchId = advisorBranchDao.CreateAdvisorBranch(advisorBranchVo, advisorId,userId);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:CreateAdvisorBranch()");


                object[] objects = new object[3];
                objects[0] = advisorBranchVo;
                objects[1] = advisorId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return branchId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmid"></param>
        /// <returns></returns>
        public int GetBranchId(int rmid)
        {
            int branchId=0;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                branchId = advisorBranchDao.GetBranchId(rmid);
            }
            catch (Exception Ex)
            {
                string msg = Ex.Message.ToString();
            }
            return branchId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public bool ChkBranchManagerAvail(int branchId)
        {
            bool bResult = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                bResult = advisorBranchDao.ChkBranchManagerAvail(branchId);
            }
            catch (Exception e)
            {
                string msg = e.Message.ToString();
            }
            return bResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="IsMainBranch"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AssociateBranch(int rmId, int branchId,int IsMainBranch,int userId)
        {
            bool bResult = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                bResult = advisorBranchDao.AssociateBranch(rmId, branchId, IsMainBranch,userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:associateBranch()");


                object[] objects = new object[3];
                objects[0] = rmId;
                objects[1] = branchId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="IsMainBranch"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UpdateAssociateBranch(int rmId, int branchId, int IsMainBranch, int userId)
        {
            bool bResult = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                bResult = advisorBranchDao.UpdateAssociateBranch(rmId, branchId, IsMainBranch, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:UpdateAssociateBranch()");


                object[] objects = new object[3];
                objects[0] = rmId;
                objects[1] = branchId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public bool DeleteBranch(int branchId)
        {
            bool bResult = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                bResult = advisorBranchDao.DeleteBranch(branchId);
            
            }
            catch (BaseApplicationException Ex)
            {
               
                throw Ex;
            }
            catch (Exception Ex)
            {
               
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:DeleteBranch()");
                object[] objects = new object[1];
                objects[0] = branchId;                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="terminalId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AddBranchTerminal(int branchId, int terminalId,int userId)
        {
            bool bResult = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                bResult = advisorBranchDao.AddBranchTerminal( branchId,terminalId,userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:addBranchTerminal()");


                object[] objects = new object[3];
                objects[0] = branchId;
                objects[1] = terminalId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public AdvisorBranchVo GetBranch(int branchId)
        {
            AdvisorBranchVo advisorBranchVo = null;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                advisorBranchVo = advisorBranchDao.GetBranch(branchId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:GetBranch()");


                object[] objects = new object[1];
                objects[0] = branchId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return advisorBranchVo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="advisorBranchVo"></param>
        /// <returns></returns>
        public bool UpdateAdvisorBranch(AdvisorBranchVo advisorBranchVo)
        {
            bool bResult = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                bResult = advisorBranchDao.UpdateAdvisorBranch(advisorBranchVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:updateAdvisorBranch()");


                object[] objects = new object[3];
                objects[0] = advisorBranchVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchName"></param>
        /// <param name="advisorId"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="sortOrder"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<int> FindBranch(string branchName,int advisorId,int CurrentPage, string sortOrder, out int count)
        {
            List<int> branchList = new List<int>();
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                branchList = advisorBranchDao.FindBranch(branchName,advisorId,CurrentPage,sortOrder,out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:FindBranch()");
                object[] objects = new object[2];
                objects[0] = branchName;
                objects[1] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return branchList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="advisorId"></param>
        /// <param name="sortOrder"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<AdvisorBranchVo> GetAdvisorBranches(int advisorId, string sortOrder, int CurrentPage , out int Count)
        {
            List<AdvisorBranchVo> branchList = null;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                branchList = advisorBranchDao.GetAdvisorBranches(advisorId, sortOrder, CurrentPage, out Count);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:GetAdvisorBranches()");


                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = sortOrder;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return branchList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="advisorId"></param>
        /// <param name="IsExternal"></param>
        /// <returns></returns>
        public List<AdvisorBranchVo> GetAdvisorBranches(int advisorId, string IsExternal)
        {
            List<AdvisorBranchVo> branchList = null;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                branchList = advisorBranchDao.GetAdvisorBranches(advisorId, IsExternal);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:GetAdvisorBranches()");

                object[] objects = new object[2];
                objects[0] = advisorId;
                objects[1] = IsExternal;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return branchList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="terminalId"></param>
        /// <returns></returns>
        public bool DeleteBranchTerminal(int terminalId)
        {
            bool bResult = false;
            AdvisorBranchDao advisorBranchDao=new AdvisorBranchDao();
            try
            {
                bResult = advisorBranchDao.DeleteBranchTerminal(terminalId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:DeleteBranchTerminal()");
                object[] objects = new object[1];
                objects[1] = terminalId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public DataSet GetBranchTerminals(int branchId)
        {
            DataSet ds = new DataSet();
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                ds = advisorBranchDao.GetBranchTerminals(branchId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:GetBranchTerminals()");
                object[] objects = new object[1];
                objects[0] = branchId;                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="userid"></param>
        /// <param name="IsMainBranch"></param>
        public void UpdateRMBranchAssociation(int rmId, int branchId, int userid,Int16 IsMainBranch)
        {
            bool bResult = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                bResult = advisorBranchDao.UpdateRMBranchAssociation(rmId, branchId,userid,IsMainBranch);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:UpdateRMBranchAssociation()");
                object[] objects = new object[2];
                objects[0] = rmId;
                objects[1] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        public void DeleteRMBranchAssociation1(int rmId)
        {
            bool bResult = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                bResult = advisorBranchDao.DeleteRMBranchAssociation(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:UpdateRMBranchAssociation()");
                object[] objects = new object[2];
                objects[0] = rmId;
               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="adviserId"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        public DataSet GetRMBranchAssociation(int rmId,int adviserId,string Flag)
        {
            DataSet ds = new DataSet();
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                ds = advisorBranchDao.GetRMBranchAssociation(rmId,adviserId,Flag);
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:GetRMBranchAssociation()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentPage"></param>
        /// <param name="Count"></param>
        /// <param name="BranchFilter"></param>
        /// <param name="RMFilter"></param>
        /// <param name="SortExpression"></param>
        /// <param name="genDictBranch"></param>
        /// <param name="genDictRM"></param>
        /// <returns></returns>
        public DataSet GetBranchAssociation(int userId, int currentPage, out int Count, string BranchFilter, string RMFilter, string SortExpression, out Dictionary<string, string> genDictBranch, out Dictionary<string, string> genDictRM)
        {
            DataSet ds = new DataSet();
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();

            genDictRM = new Dictionary<string, string>();
            genDictBranch = new Dictionary<string, string>();
            Count = 0;

            try
            {
                ds = advisorBranchDao.GetBranchAssociation(userId, currentPage, out Count, BranchFilter, RMFilter, SortExpression, out genDictBranch, out genDictRM);
            }
          
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:GetBranchAssociation()");
                object[] objects = new object[5];
                objects[0] = userId;
                objects[1] = currentPage;
                objects[2] = RMFilter;
                objects[3] = BranchFilter;
                objects[4] = SortExpression;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArrayList GetBranchName(int id)
        {         
          
            ArrayList branchList = null;
            AdvisorBranchDao advisorBranchDao=new AdvisorBranchDao();
              try{
                  branchList=advisorBranchDao.GetBranchName(id);
              }
              catch (BaseApplicationException Ex)
              {
                  throw Ex;
              }
              catch (Exception Ex)
              {
                  BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                  NameValueCollection FunctionInfo = new NameValueCollection();

                  FunctionInfo.Add("Method", "AdvisorBranchBo.cs:GetBranchName()");


                  object[] objects = new object[3];
                  objects[0] = id;

                  FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                  exBase.AdditionalInformation = FunctionInfo;
                  ExceptionManager.Publish(exBase);
                  throw exBase;

              }
              return branchList;
          }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public bool CheckInternalBranchAssociations(int rmId)
        {
            bool blResult = false;

            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                blResult = advisorBranchDao.CheckInternalBranchAssociations(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:CheckInternalBranchAssociations()");
                object[] objects = new object[1];
                objects[1] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public bool CheckBranchMgrRole(int rmId)
        {
            bool blResult = false;

            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                blResult = advisorBranchDao.CheckBranchMgrRole(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:CheckInternalBranchAssociations()");
                object[] objects = new object[1];
                objects[1] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public bool CheckExternalBranchAssociations(int rmId)
        {
            bool blResult = false;

            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                blResult = advisorBranchDao.CheckExternalBranchAssociations(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:CheckExternalBranchAssociations()");
                object[] objects = new object[1];
                objects[1] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="asscCategoryId"></param>
        /// <returns></returns>
        public DataTable GetAsscCommissionDetails(int adviserId, int asscCategoryId) 
        {
            DataTable dt = new DataTable();
            AdvisorBranchDao adviserBranchDao = new AdvisorBranchDao();
            try
            {
                dt = adviserBranchDao.GetAsscCommissionDetails(adviserId, asscCategoryId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetAsscCommissionDetails()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[0] = asscCategoryId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataTable GetAdviserAssetGroups(int adviserId)
        {
            DataTable dt = new DataTable();
            AdvisorBranchDao adviserBranchDao = new AdvisorBranchDao();
            try
            {
                dt = adviserBranchDao.GetAdviserAssetGroups(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetAsscCommissionDetails()");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="advisorAssociateCommissionVo"></param>
        /// <returns></returns>
        public bool AddAssociateCommission(int userid,AdvisorAssociateCommissionVo advisorAssociateCommissionVo)
        {
            bool bResult = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                bResult = advisorBranchDao.AddAssociateCommission(userid, advisorAssociateCommissionVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:AddAssociateCommission()");

                object[] objects = new object[1];
                objects[1] = userid;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="advisorAssociateCommissionVo"></param>
        /// <returns></returns>
        public bool UpdateAssociateCommission(int userid, AdvisorAssociateCommissionVo advisorAssociateCommissionVo)
        {
            bool bResult = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                bResult = advisorBranchDao.UpdateAssociateCommission(userid, advisorAssociateCommissionVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:UpdateAssociateCommission()");

                object[] objects = new object[1];
                objects[1] = userid;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public DataTable GetBranchAssociateCommission(int branchId)
        {
            DataTable dt = new DataTable();
            AdvisorBranchDao adviserBranchDao = new AdvisorBranchDao();
            try
            {
                dt = adviserBranchDao.GetBranchAssociateCommission(branchId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetAsscCommissionDetails()");
                object[] objects = new object[1];
                objects[0] = branchId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }
        /// <summary>
        /// Function to check whether an RM is the branch head (for removing the branch Association) 
        /// </summary>
        /// <param name="rmId">Id of the RM</param>
        /// <param name="branchId">Id of the Branch</param>
        /// <returns></returns>
        public int CheckBranchHead(int rmId, int branchId)
        {
            int count = 0;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                count = advisorBranchDao.CheckBranchHead(rmId, branchId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:CheckBranchHead()");
                object[] objects = new object[2];
                objects[0] = rmId;
                objects[1] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return count;
        }
        /// <summary>
        /// Function to delete the RM-Branch association
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        public bool DeleteBranchAssociation(int rmId,int branchId)
        {
            bool bResult = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                bResult = advisorBranchDao.DeleteBranchAssociation(rmId, branchId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:DeleteBranchAssociation()");
                object[] objects = new object[2];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }
        /// <summary>
        /// Function to check whether an Associate Category is linked to any branch(for deleting the Associate Category) 
        /// </summary>
        /// <param name="categoryId">Id of the Branch</param>
        /// <returns></returns>
        public int CheckAssociateBranchCategory(int categoryId)
        {
            int count = 0;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                count = advisorBranchDao.CheckAssociateBranchCategory(categoryId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:CheckAssociateBranchCategory()");
                object[] objects = new object[1];
                objects[1] = categoryId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return count;
        }

        /// <summary>
        /// Creating RM Branch Association......
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="userid"></param>
        /// <param name="IsMainBranch"></param>
        /// <returns></returns>
        public bool CreateRMBranchAssociation(int rmId, int branchId, int createdBy, int modifiedBy)
        {
            bool bResult = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                bResult = advisorBranchDao.CreateRMBranchAssociation(rmId, branchId, createdBy, modifiedBy);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:CreateRMBranchAssociation()");
                object[] objects = new object[2];
                objects[0] = rmId;
                objects[1] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }


        /// <summary>
        /// Checking RM Branch Association......
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="userid"></param>
        /// <param name="IsMainBranch"></param>
        /// <returns></returns>
        public bool CheckRMBranchDependency(int rmId, int branchId)
        {

            bool isBranchDependency = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                isBranchDependency = advisorBranchDao.CheckRMBranchDependency(rmId, branchId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:CheckRMBranchDependency()");
                object[] objects = new object[2];
                objects[0] = rmId;
                objects[1] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isBranchDependency;
        }


        /// <summary>
        ///Converting One External Branch to Internal and Vice-versa Checking any RM is associated to the branch
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="userid"></param>
        /// <param name="IsMainBranch"></param>
        /// <returns></returns>
        public bool CheckBranchDependency(int branchId)
        {
            bool isBranchRMDependency = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();

            try
            {
                isBranchRMDependency = advisorBranchDao.CheckBranchDependency(branchId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:CheckBranchDependency()");
                object[] objects = new object[1];
                objects[0] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isBranchRMDependency;
        }

        /// <summary>
        ///Before deletion of any branch check the branch depedency with customer
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="userid"></param>
        /// <param name="IsMainBranch"></param>
        /// <returns></returns>
        public bool CheckBranchCustomerDependency(int branchId)
        {
            bool isBranchCustomerDependency = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                isBranchCustomerDependency = advisorBranchDao.CheckBranchCustomerDependency(branchId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:CheckBranchCustomerDependency()");
                object[] objects = new object[1];
                objects[0] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isBranchCustomerDependency;
        }

        /// <summary>
        ///BBefore deleting a associate category checking dependency of category in AdvisorBranch
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="userid"></param>
        /// <param name="IsMainBranch"></param>
        /// <returns></returns>
        public bool CheckAssociateCategoryDependency(int categoryId)
        {
            bool isAssociateCategoryDependent = false;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            try
            {
                isAssociateCategoryDependent = advisorBranchDao.CheckAssociateCategoryDependency(categoryId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:CheckAssociateCategoryDependency()");
                object[] objects = new object[1];
                objects[0] = categoryId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isAssociateCategoryDependent;
        }

        /// <summary>
        /// Get all customer of advisor for bulk assignment
        /// </summary>
        /// <param name="branchIdFilter"></param>
        /// <param name="adviserId"></param>
        /// <param name="currentPage"></param>
        /// <param name="count"></param>
        /// <param name="sortExpression"></param>
        /// <param name="custNameFilter"></param>
        /// <param name="branchNameFilter"></param>
        /// <param name="rmNameFilter"></param>
        /// <param name="areaFilter"></param>
        /// <param name="cityFilter"></param>
        /// <param name="advisorBranchList"></param>
        /// <returns></returns>
        public List<CustomerVo> GetAdviserCustomerListForAssociation(int branchIdFilter, int adviserId, int currentPage, out int count, string sortExpression, string custNameFilter, string branchNameFilter, string rmNameFilter, string areaFilter, string cityFilter, out Dictionary<string, string> advisorBranchList)
        {
            List<CustomerVo> customerList = null;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();


            count = 0;

            try
            {
                customerList = advisorBranchDao.GetAdviserCustomerListForAssociation(branchIdFilter, adviserId, currentPage, out count, sortExpression, custNameFilter, branchNameFilter, rmNameFilter, areaFilter, cityFilter, out advisorBranchList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:GetAdviserCustomerListForAssociation()");

                object[] objects = new object[11];
                objects[0] = adviserId;
                objects[1] = currentPage;
                objects[2] = count;
                objects[3] = sortExpression;
                objects[4] = custNameFilter;
                objects[5] = areaFilter;
                objects[6] = branchNameFilter;
                objects[7] = rmNameFilter;
                objects[8] = branchIdFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return customerList;
        }

        /// <summary>
        /// Checking any of one customer is GroupHead(Customer are associated to him) 
        /// </summary>
        /// <param name="customerIds"></param>
        /// <returns></returns>
        public bool CheckCustomerGroupHead(string customerIds)
        {
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            bool flag = false;
            try
            {
                flag = advisorBranchDao.CheckCustomerGroupHead(customerIds);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:CheckCustomerGroupHead()");
                object[] objects = new object[1];
                objects[0] = customerIds;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return flag;
        }
        /// <summary>
        /// Customer RM Bulk assignment for a branch. 
        /// </summary>
        /// <param name="customerIds"></param>
        /// <param name="branchId"></param>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public bool ReassignCustomersBranchRM(string customerIds, int branchId, int rmId)
        {
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            bool bResult = false;
            try
            {
                bResult = advisorBranchDao.ReassignCustomersBranchRM(customerIds, branchId, rmId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:ReassignCustomersBranchRM()");
                object[] objects = new object[3];
                objects[0] = customerIds;
                objects[1] = branchId;
                objects[2] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }


        /* For Branch Assets */

        public DataSet GetBranchAssets(int advisorBranchId, int branchHeadId, int all)
        {


            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            DataSet ds = new DataSet();
            try
            {
                ds = advisorBranchDao.GetBranchAssets(advisorBranchId, branchHeadId, all);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:GetBranchAssets()");
                object[] objects = new object[3];
                objects[0] = advisorBranchId;
                objects[1] = branchHeadId;
                objects[2] = all;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;

        }

        /* End For Branch Assets */


        /* For Branch Dropdowns */


        public DataSet GetBranchsRMForBMDp(int branchId, int branchHeadId, int all)
        {

            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            DataSet ds = new DataSet();
            try
            {
                ds = advisorBranchDao.GetBranchsRMForBMDp(branchId, branchHeadId, all);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:GetBranchsRMForBMDp()");
                object[] objects = new object[3];
                objects[0] = branchId;
                objects[1] = branchHeadId;
                objects[2] = all;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;


        }

        /* End For Branch Dropdowns */

        public DataSet GetAdviserCustomerFolioMerge(int adviserId, int currentPage, string custNameFilter, out int count)
        {
           
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();

            return advisorBranchDao.GetAdviserCustomerFolioMerge(adviserId, currentPage, custNameFilter, out count);
            
                              
        }

        public DataSet GetCustomerFolioMergeList(int customerId, int amcCode, string fnumber)
        {

            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();

            return advisorBranchDao.GetCustomerFolioMergeList(customerId, amcCode, fnumber);


        }

        public bool CustomerFolioMerged(string ffromfolio, string fnumber ,int customerId)
        {

            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();

            return advisorBranchDao.CustomerFolioMerged(ffromfolio, fnumber,customerId);


        }


        /// <summary>
        /// Getting RM's who are all not having BM role. Added by <<Kirteeshree>>
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="branchHeadId"></param>
        /// <returns></returns>
        public DataSet GetAllRMsWithOutBMRole(int branchId, int branchHeadId)
        {

            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();
            DataSet ds = new DataSet();
            try
            {
                ds = advisorBranchDao.GetAllRMsWithOutBMRole(branchId, branchHeadId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchBo.cs:GetBranchsRMForBMDp()");
                object[] objects = new object[3];
                objects[0] = branchId;
                objects[1] = branchHeadId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;


        }

    }
}
