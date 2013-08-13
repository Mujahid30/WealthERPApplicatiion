using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using VOAssociates;
using DAOAssociates;


namespace BOAssociates
{
    public class AssociatesBo
    {
        AssociatesDAO associatesDao = new AssociatesDAO();
        public List<int> CreateCompleteAssociates(UserVo userVo, AssociatesVO associatesVo, int userId)
        {
            List<int> associatesIds = new List<int>();
            try
            {
                associatesIds = associatesDao.CreateCompleteAssociates(userVo, associatesVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:CreateAssociates()");


                object[] objects = new object[3];
                objects[0] = associatesVo;
                objects[1] = userVo;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return associatesIds;
        }
        public List<AssociatesVO> GetViewAssociates(int id, bool isAdviser, bool isBranchHead, bool isBranchId, string currentUserRole)
        {
            List<AssociatesVO> associatesVOList = null;
            try
            {
                associatesVOList = associatesDao.GetViewAssociates(id, isAdviser, isBranchHead, isBranchId, currentUserRole);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetViewAssociates()");


                object[] objects = new object[1];
                objects[0] = id;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return associatesVOList;
        }
        public DataSet GetAdviserAssociatesDetails(int associateId)
        {
            DataSet dsAssociatesDetails; 
            try
            {
                dsAssociatesDetails = associatesDao.GetAdviserAssociatesDetails(associateId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociatesBo.cs:GetAdviserAssociatesDetails()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAssociatesDetails;
        }
        public bool UpdateStatusStep1(string statusStep1, int associateId)
        {
            bool bResult = false;
            try
            {
                bResult = associatesDao.UpdateStatusStep1(statusStep1, associateId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return bResult;
        }
        public bool UpdateAdviserAssociates(AssociatesVO associatesVo)
        {
            bool bResult = false;
            try
            {
                bResult=associatesDao.UpdateAdviserAssociates(associatesVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return bResult;
        }
        public int GetAgentId()
        {
            int agentId;
            try
            {
                agentId = associatesDao.GetAgentId();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetAgentId()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return agentId;
        }

        public DataTable GetAssociatesList(int adviserId)
        {
            DataTable dtGetAgentlist;
            try
            {
                dtGetAgentlist = associatesDao.GetAssociatesList(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetAssociatesList()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetAgentlist;
        }
        public bool CreateAdviserAgentCode(AssociatesVO associatesVo, int agentId)
        {
            bool result = false;
            try
            {
                result = associatesDao.CreateAdviserAgentCode(associatesVo,agentId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        public DataSet GetAgentCodeAndType(int adviserId,string usertype,string agentcode)
        {
            DataSet ds;
            try
            {
                ds = associatesDao.GetAgentCodeAndType(adviserId, usertype, agentcode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetAgentCodeAndType()");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
        public void UpdateAssociatesWorkFlowStatusDetails(int AssociateId, string Status, string StepCode, string StatusReason,string comments)
        {
            try
            {
                associatesDao.UpdateAssociatesWorkFlowStatusDetails(AssociateId, Status, StepCode, StatusReason, comments);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public DataTable GetProductAssetGroup()
        {
            DataTable dt;
            try
            {
                dt = associatesDao.GetProductAssetGroup();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
        public DataSet GetAssociatesStepDetails(int requestId)
        {
            DataSet ds;
            try
            {
                ds = associatesDao.GetAssociatesStepDetails(requestId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }
        public DataSet AssociateUserMangemnetList(int adviserId)
        {
            DataSet dsGetAssociateList;
            try
            {
                dsGetAssociateList = associatesDao.AssociateUserMangemnetList(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetAssociateList;
        }
        public DataSet BindAssociateCodeList(int adviserId)
        {
            DataSet dsGetAssociateCodeList;
            try
            {
                dsGetAssociateCodeList = associatesDao.BindAssociateCodeList(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetAssociateCodeList;
        }
        public AssociatesVO GetAssociateUser(int UserId)
        {
            AssociatesVO associatesVo = new AssociatesVO();
            try
            {
                associatesVo = associatesDao.GetAssociateUser(UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return associatesVo;
        }
        public DataSet GetAdviserAssociateList(int adviserId,string Usertype,string agentcode)
        {
            DataSet dsGetAssociateCodeList;
            try
            {
                dsGetAssociateCodeList = associatesDao.GetAdviserAssociateList(adviserId, Usertype, agentcode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetAssociateCodeList;
        }
        public AssociatesVO GetAssociateVoList(int assiciateId)
        {
            AssociatesVO associatesVo = new AssociatesVO();
            try
            {
                associatesVo = associatesDao.GetAssociateVoList(assiciateId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return associatesVo;
        }

        public DataTable GetRMAssociatesList(int rmId)
        {
            DataTable dtGetAgentlist;
            try
            {
                dtGetAgentlist = associatesDao.GetRMAssociatesList(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetRMAssociatesList()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetAgentlist;
        }
        public bool CodeduplicateCheck(int adviserId, string agentCode)
        {
            bool bResult = false;
            try
            {
                bResult = associatesDao.CodeduplicateCheck(adviserId, agentCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public DataTable GetAgentCodeFromAgentPaaingAssociateId(int assiciateId)
        {
            DataTable dtGetAgentdetails;
            //string code;
            try
            {
                dtGetAgentdetails = associatesDao.GetAgentCodeFromAgentPaaingAssociateId(assiciateId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateBo.cs:GetAgentCodeFromAgentPaaingAssociateId(assiciateId)");
                object[] objects = new object[1];
                objects[0] = assiciateId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetAgentdetails;
        }
        public bool AddAgentChildCode(AssociatesVO associatesVo, string code)
        {
            bool bResult = false;
            try
            {
                bResult = associatesDao.AddAgentChildCode(associatesVo, code);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public DataTable GetAgentChildCodeList(int PagentId)
        {
            DataTable dtChildCodeList;
            //string code;
            try
            {
                dtChildCodeList = associatesDao.GetAgentChildCodeList(PagentId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateBo.cs:GetAgentChildCodeList(PagentId)");
                object[] objects = new object[1];
                objects[0] = PagentId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtChildCodeList;
        }
        public bool EditAddChildAgentCodeList(AssociatesVO associatesVo, string ChildCode, int PagentId, char flag)
        {
            bool result = false; ;
            result = associatesDao.EditAddChildAgentCodeList(associatesVo, ChildCode, PagentId, flag);
            return result;
        }
        public bool DeleteChildAgentCode(int childAgentId)
        {
            bool result = false; ;
            result = associatesDao.DeleteChildAgentCode(childAgentId);
            return result;
        }


        public AssociatesUserHeirarchyVo GetAssociateUserHeirarchy(int userId,int adviserId)
        {
            AssociatesUserHeirarchyVo associatesUserHeirarchyVo = new AssociatesUserHeirarchyVo();
            try
            {
                associatesUserHeirarchyVo = associatesDao.GetAssociateUserHeirarchy(userId, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return associatesUserHeirarchyVo;
        }

        public DataSet GetAdviserHierarchyStaffList(int branchId, int hierarchyRoleId)
        {
            AssociatesDAO associatesDao = new AssociatesDAO();
            DataSet dsAdviserHierarchyStaffList = new DataSet();
            try
            {
                dsAdviserHierarchyStaffList = associatesDao.GetAdviserHierarchyStaffList(branchId, hierarchyRoleId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsAdviserHierarchyStaffList;

        }
        
    }
}
