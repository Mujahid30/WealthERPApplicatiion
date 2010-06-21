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

namespace BoAdvisorProfiling
{
    public class AdvisorBranchBo
    {
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
    }
}
