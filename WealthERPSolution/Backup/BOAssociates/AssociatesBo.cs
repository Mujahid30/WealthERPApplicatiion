﻿using System;
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
        public bool CheckPanNumberDuplicatesForAssociates(string Pan, int AdviserAssociateId, int adviserId)
        {
            bool bResult = false;
            try
            {
                bResult = associatesDao.CheckPanNumberDuplicatesForAssociates(Pan, AdviserAssociateId, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return bResult;
        }
        public bool UpdateAdviserAssociates(AssociatesVO associatesVo, int associateId, int userId, string command)
        {
            bool bResult = false;
            try
            {
                bResult = associatesDao.UpdateAdviserAssociates(associatesVo, associateId, userId, command);
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
        public bool CreateAdviserAgentCode(AssociatesVO associatesVo, int agentId, int adviserId)
        {
            bool result = false;
            try
            {
                result = associatesDao.CreateAdviserAgentCode(associatesVo, agentId, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        public DataSet GetAgentCodeAndType(int adviserId, string usertype, string agentcode)
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
        public void UpdateAssociatesWorkFlowStatusDetails(int AssociateId, string Status, string StepCode, string StatusReason, string comments)
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
        public DataSet GetAdviserAssociateList(int Id, string Usertype, string agentcode)
        {
            DataSet dsGetAssociateCodeList;
            try
            {
                dsGetAssociateCodeList = associatesDao.GetAdviserAssociateList(Id, Usertype, agentcode);
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

        public int SynchronizeCustomerAssociation(int AdviserId, string Type, int UId)
        {
            int result = 0;
            try
            {
                result = associatesDao.SynchronizeCustomerAssociation(AdviserId, Type, UId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;

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
        public DataTable GetAgentCodeFromAgentPaaingAssociateId(int assiciateId, string type)
        {
            DataTable dtGetAgentdetails;
            //string code;
            try
            {
                dtGetAgentdetails = associatesDao.GetAgentCodeFromAgentPaaingAssociateId(assiciateId, type);
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
        public bool EditAddChildAgentCodeList(AssociatesVO associatesVo, string ChildCode, int PagentId, char flag, string childName, string childEmailId, int userId, string roleIds)
        {
            bool result = false; ;
            result = associatesDao.EditAddChildAgentCodeList(associatesVo, ChildCode, PagentId, flag, childName, childEmailId, userId, roleIds);
            return result;
        }
        public bool DeleteChildAgentCode(int childAgentId)
        {
            bool result = false; ;
            result = associatesDao.DeleteChildAgentCode(childAgentId);
            return result;
        }


        public AssociatesUserHeirarchyVo GetAssociateUserHeirarchy(int userId, int adviserId)
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

        public DataSet GetAdviserHierarchyStaffList(int hierarchyRoleId)
        {
            AssociatesDAO associatesDao = new AssociatesDAO();
            DataSet dsAdviserHierarchyStaffList = new DataSet();
            try
            {
                dsAdviserHierarchyStaffList = associatesDao.GetAdviserHierarchyStaffList(hierarchyRoleId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsAdviserHierarchyStaffList;

        }

        public DataTable GetSalesListToAddCode(int AdviserId)
        {
            DataTable dtChildCodeList;
            try
            {
                dtChildCodeList = associatesDao.GetSalesListToAddCode(AdviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateBo.cs:GetSalesListToAddCode(AdviserId)");
                object[] objects = new object[1];
                objects[0] = AdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtChildCodeList;
        }
        public DataTable GetAssociatesSubBrokerCodeList(int adviserId)
        {
            DataTable dtAssociatesSubBrokerCodeList;
            try
            {
                dtAssociatesSubBrokerCodeList = associatesDao.GetAssociatesSubBrokerCodeList(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetAssociatesSubBrokerCodeList()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAssociatesSubBrokerCodeList;
        }
        public DataTable GetSalesSubBrokerCodeList(int adviserId)
        {
            DataTable dtSalesSubBrokerCodeList;
            try
            {
                dtSalesSubBrokerCodeList = associatesDao.GetSalesSubBrokerCodeList(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetSalesSubBrokerCodeList()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSalesSubBrokerCodeList;
        }
        public DataTable GetBranchSubBrokerCodeList(int adviserId)
        {
            DataTable dtBranchSubBrokerCodeList;
            try
            {
                dtBranchSubBrokerCodeList = associatesDao.GetBranchSubBrokerCodeList(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetBranchSubBrokerCodeList()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtBranchSubBrokerCodeList;
        }
        public DataSet GetProductDetailsFromMFTransaction(string agentcode, string userType, int AdviserId, int rmId, int branchId, int branchHeadId, DateTime FromDate, DateTime Todate, int All, int IsOnline)
        {
            DataSet dsGetProductDetailFromMFOrder;
            try
            {
                dsGetProductDetailFromMFOrder = associatesDao.GetProductDetailsFromMFTransaction(agentcode, userType, AdviserId, rmId, branchId, branchHeadId, FromDate, Todate, All, IsOnline);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetProductDetailFromMFOrder()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetProductDetailFromMFOrder;
        }
        public DataSet GetOrganizationDetailFromTransaction(string agentcode, string userType, int AdviserId, int rmId, int branchId, int branchHeadId, DateTime FromDate, DateTime Todate, int All)
        {
            DataSet dsGetOrganizationFromMFOrder;
            try
            {
                dsGetOrganizationFromMFOrder = associatesDao.GetOrganizationDetailFromTransaction(agentcode, userType, AdviserId, rmId, branchId, branchHeadId, FromDate, Todate, All);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetProductDetailFromMFOrder()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetOrganizationFromMFOrder;
        }
        public DataSet GetMemberDetailFromTransaction(string agentcode, string userType, int AdviserId, int branchHeadId, DateTime FromDate, DateTime Todate, int IsOnline)
        {
            DataSet dsGetMemberDetailFromTransaction;
            try
            {
                dsGetMemberDetailFromTransaction = associatesDao.GetMemberDetailFromTransaction(agentcode, userType, AdviserId, branchHeadId, FromDate, Todate, IsOnline);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetProductDetailFromMFOrder()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetMemberDetailFromTransaction;
        }


        public DataSet BindChannelList(int AdviserId)
        {
            DataSet dsBindChannelList;

            dsBindChannelList = associatesDao.BindChannelList(AdviserId);
            return dsBindChannelList;
        }

        public DataSet BindTitlesList(int channelId, int AdviserId)
        {
            DataSet dsBindTitleList;

            dsBindTitleList = associatesDao.BindTitleList(channelId, AdviserId);
            return dsBindTitleList;
        }
        public DataSet BindTitlesList(int AdviserId)
        {
            DataSet dsBindTitleList;

            dsBindTitleList = associatesDao.BindTitleList(0, AdviserId);
            return dsBindTitleList;
        }
        public DataSet BindSubBrokerList(int searchId, int AdviserId, string searchType)
        {
            DataSet dsBindSubBrokerList;

            dsBindSubBrokerList = associatesDao.BindSubBrokerList(searchId, AdviserId, searchType);
            return dsBindSubBrokerList;
        }

        public DataTable GetStateList()
        {
            DataTable dtStateList;
            try
            {
                dtStateList = associatesDao.GetStateList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetStateList()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtStateList;
        }

        public DataTable GetCityList(string stateId, int flag)
        {
            DataTable dtCityList;
            try
            {
                dtCityList = associatesDao.GetCityList(stateId, flag);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetCityList()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtCityList;
        }

        public DataTable GetAdviserHierarchyTitleList(int adviserId)
        {
            DataTable dtAdviserHierarchyTitleList;
            try
            {
                dtAdviserHierarchyTitleList = associatesDao.GetAdviserHierarchyTitleList(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateBo.cs:GetAdviserHierarchyTitleList()");
                object[] objects = new object[0];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAdviserHierarchyTitleList;
        }

        public DataTable GetAdviserStaffBranchList(int staffId)
        {

            DataTable dtAdviserStaffBranchList;

            try
            {
                dtAdviserStaffBranchList = associatesDao.GetAdviserStaffBranchList(staffId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociatesBo.cs:GetAdviserStaffBranchList()");
                object[] objects = new object[1];
                objects[0] = staffId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAdviserStaffBranchList;

        }
        public bool UpdateUserrole(int DepartmentId, string rollid)
        {
            bool bResult = false;
            bResult = associatesDao.UpdateUserrole(DepartmentId, rollid);
            return bResult;
        }
        public DataSet GetDepartment(int adviserId)
        {
            DataSet dsGetUserRole;
            try
            {

                dsGetUserRole = associatesDao.GetDepartment(adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:CheckForBusinessDate(DateTime date)");
                object[] objParams = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetUserRole;

        }
        public string GetAgentCode(int agentId, int adviserId)
        {
            string agentCode = string.Empty;
            try
            {
                agentCode = associatesDao.GetAgentCode(agentId, adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return agentCode;
        }
        public string GetPANNo(int agentId)
        {
            string PANNo = string.Empty;
            try
            {
                PANNo = associatesDao.GetPANNo(agentId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return PANNo;
        }
        public bool UpdateAssociate(AssociatesVO associatesVo, int userId, int associateId, int agentId)
        {
            bool bResult = false;
            try
            {
                bResult = associatesDao.UpdateAssociate(associatesVo, userId, associateId, agentId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool UpdateAssociateWelcomeNotePath(int userId, long associateId, string welcomeNotePath)
        {
            bool bResult = false;

            try
            {
                bResult = associatesDao.UpdateAssociateWelcomeNotePath(userId, associateId, welcomeNotePath);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool UpdateAssociateDetails(AssociatesVO associatesVo, int userId, int associateid, int agentcode)
        {
            bool bResult = false;
            try
            {
                bResult = associatesDao.UpdateAssociateDetails(associatesVo, userId, associateid, agentcode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public DataTable AssetsGroup()
        {
            DataTable dtAssetsGroup;
            try
            {
                dtAssetsGroup = associatesDao.AssetsGroup();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtAssetsGroup;

        }
        public DataTable GetAssetsRegistration(int associateId)
        {
            DataTable dtGetAssetsRegistration;
            try
            {
                dtGetAssetsRegistration = associatesDao.GetAssetsRegistration(associateId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetAssetsRegistration;
        }
        public bool AssociateRegistration(int associateId, DateTime registrationExp, int RegistrationNo, string assetsGroup)
        {
            bool bResult = false;
            try
            {
                bResult = associatesDao.AssociateRegistration(associateId, registrationExp, RegistrationNo, assetsGroup);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool AssociateFieldValidation(string text, string Type, int adviserId, int AdviserAssociateId)
        {
            bool bResult = false;
            try
            {
                bResult = associatesDao.AssociateFieldValidation(text, Type, adviserId,AdviserAssociateId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public DataSet GetAdviserIndividualAssociateList(int associateId, int adviserId)
        
        {
            DataSet dsGetAssociateCodeList;
            try
            {
                dsGetAssociateCodeList = associatesDao.GetAdviserIndividualAssociateList(associateId,adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetAssociateCodeList;
        }
        public DataTable GetAssociateNameDetails(string searchType, string prefixText, int Adviserid)
        {

            DataTable dtAssociatesNames = new DataTable();
            try
            {
                dtAssociatesNames = associatesDao.GetAssociateNameDetails(searchType,prefixText, Adviserid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAssociateNameDetails()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtAssociatesNames;
        }
        public string GetSampleAssociateCode(int adviserId, int branchId,string Type)
        {
            string associateCode = string.Empty;
            try
            {
                associateCode = associatesDao.GetSampleAssociateCode(adviserId, branchId, Type);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return associateCode;
        }

        public string CreateBulkChildCode(int adviserId, string Type, int startingchildcode, int noofchaildcode, string parentcode)
        {
            string mssage = string.Empty;
            try
            {
                mssage = associatesDao.CreateBulkChildCode(adviserId, Type, startingchildcode, noofchaildcode, parentcode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
             return mssage;;
        }

        public DataTable GetAssociatetype()
        {
            DataTable dtGetAssociatetype = new DataTable();
            try
            {
                dtGetAssociatetype = associatesDao.GetAssociatetype();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetAssociatetype;
        }
    }
   }
