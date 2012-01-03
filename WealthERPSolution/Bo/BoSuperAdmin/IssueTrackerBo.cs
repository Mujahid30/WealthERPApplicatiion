using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using DaoSuperAdmin;
using VoSuperAdmin;
using System.Web;
using System.Collections;

namespace BoSuperAdmin
{
    public class IssueTrackerBo
    {       
        /// <summary>
        ///  
        /// </summary>
        /// <param name="productGoldPriceVO"></param>
        /// <returns></returns>
        IssueTrackerDao superAdmincsissueTrackerDao = new IssueTrackerDao();

      public int InsertcsissueTrackerDetails(IssueTrackerVo csIssueTrackerVO)
       {

           try
           {
               return superAdmincsissueTrackerDao.InsertcsissueTrackerDetails(csIssueTrackerVO);
           }
           catch(Exception ex)
           {
               throw ex;
           }
       }

      public int InsertIntoCSIssueLevel1ToLevel1(IssueTrackerVo csIssueTrackerVO)
       {

           try
           {
               return superAdmincsissueTrackerDao.InsertIntoCSIssueLevel1ToLevel1(csIssueTrackerVO);
           }
           catch(Exception ex)
           {
               throw ex;
           }
       }

      public int InsertIntoCSIssueLevel2ToAnyLevel(IssueTrackerVo csIssueTrackerVO)
       {

           try
           {
               return superAdmincsissueTrackerDao.InsertIntoCSIssueLevel2ToAnyLevel(csIssueTrackerVO);
           }
           catch(Exception ex)
           {
               throw ex;
           }
       }

      public int InsertIntoCSIssueLevel3ToAnyLevel(IssueTrackerVo csIssueTrackerVO)
       {

           try
           {
               return superAdmincsissueTrackerDao.InsertIntoCSIssueLevel3ToAnyLevel(csIssueTrackerVO);
           }
           catch(Exception ex)
           {
               throw ex;
           }
       }

      public int InsertcsissueTrackerDetailsLevel3(IssueTrackerVo csIssueTrackerVO)
       {

           try
           {
               return superAdmincsissueTrackerDao.InsertcsissueTrackerDetailsLevel3(csIssueTrackerVO);
           }
           catch(Exception ex)
           {
               throw ex;
           }
       }
        
        /// 
        /// </summary>
        /// <returns></returns>
      public int autoIncrementcsiSSUECode()
      {
          try
          {
              return superAdmincsissueTrackerDao.autoIncrementcsiSSUECode();
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
      public DataSet GetTreeNodeList(int roleId)
        {  
            DataSet ds;
            try
            {

                ds = superAdmincsissueTrackerDao.GetTreeNodeList(roleId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="treeNodeId"></param>
        /// <returns></returns>
      public DataSet GetTreeSubNodeList(int roleId, int treeNodeId)
        {
            DataSet ds;
            try
            {

                ds = superAdmincsissueTrackerDao.GetTreeSubNodeList(roleId, treeNodeId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="treeSubNodeId"></param>
        /// <param name="treeSubSubNodeId"></param>
        /// <returns></returns>
      public DataSet GetTreeSubSubNodeList(int roleID, int treeSubNodeId, int treeSubSubNodeId)
      {
          DataSet ds;
          try
          {

              ds = superAdmincsissueTrackerDao.GetTreeSubSubNodeList(roleID, treeSubNodeId, treeSubSubNodeId);
          }
          catch (BaseApplicationException Ex)
          {
              throw Ex;
          }
          return ds;
      } 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
      public DataTable GetPriorityList()
        {
            DataTable dt;
            try
            {

                dt = superAdmincsissueTrackerDao.GetPriorityList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
      public DataTable GetCustomerPriorityList()
        {
            DataTable dt;
            try
            {

                dt = superAdmincsissueTrackerDao.GetCustomerPriorityList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
            return dt;
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetLevelList()
        {
            DataTable dt;
            try
            {

                dt = superAdmincsissueTrackerDao.GetLevelList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetTypesList()
        {          
            DataTable dt;
            try
            {

                dt = superAdmincsissueTrackerDao.GetTypesList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetStatusList()
        {
            DataTable dt;
            try
            {

                dt = superAdmincsissueTrackerDao.GetStatusList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAdviserList()
        {
            DataTable dt;
            try
            {

                dt = superAdmincsissueTrackerDao.GetAdviserList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
            return dt;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetRoleList()
        { 
            DataTable dt;
            try
            {

                dt = superAdmincsissueTrackerDao.GetRoleList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
            return dt;
        }

        //public DataTable GetTreeNodeDetails()
        //{ 
        //    DataTable dt;
        //    try
        //    {

        //        dt = superAdmincsissueTrackerDao.GetTreeNodeDetails();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }            
        //    return dt;
        //}


        //public DataTable GetTreeSubNodeDetails()
        //{
        //    DataTable dt;
        //    try
        //    {

        //        dt = superAdmincsissueTrackerDao.GetTreeSubNodeDetails();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    return dt;
        //}

        //public DataTable GetTreeSubSubNodeDetails()
        //{
        //    DataTable dt;
        //    try
        //    {

        //        dt = superAdmincsissueTrackerDao.GetTreeSubSubNodeDetails();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    return dt;
        //} 
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet getCSIssueDetails()
        { 
            try
            {
                return superAdmincsissueTrackerDao.getCSIssueDetails();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csId"></param>
        /// <param name="strLevelName"></param>
        /// <returns></returns>
        public DataSet getCSIssueDataAccordingToCSId(int csId)
        {   
            try
            {
                return superAdmincsissueTrackerDao.getCSIssueDataAccordingToCSId(csId );
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strOrgName"></param>
        /// <returns></returns>
        public DataSet GetAdviserPhoneNOandEmailidAccordingToAdviserName(string strOrgName)
        {
            try
            {
                return superAdmincsissueTrackerDao.GetAdviserPhoneNOandEmailidAccordingToAdviserName(strOrgName);
            }
            catch
            {
                throw;
            }
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csId"></param>
        /// <returns></returns>
        public DataSet GetQACSIssueDataAccordingToCSId(int csId)
        {
            try
            {
                return superAdmincsissueTrackerDao.GetQACSIssueDataAccordingToCSId(csId );
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csId"></param>
        /// <param name="levelId"></param>
        /// <returns></returns>
        public DataSet GetDEVCSIssueDataAccordingToCSId(int csId)
        {
            try
            {
                return superAdmincsissueTrackerDao.GetDEVCSIssueDataAccordingToCSId(csId);
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetSearchDetails(string strSearch)
        {
            try
            {
                return superAdmincsissueTrackerDao.GetSearchDetails(strSearch);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="superAdminCSIssueTrackerVo"></param>
        /// <returns></returns>
        public int InsertQAData(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            try
            {
                return superAdmincsissueTrackerDao.InsertQAData(superAdminCSIssueTrackerVo);
            }
            catch
            {
                throw;
            }
        }


        public int InsertQADataLevel2(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            try
            {
                return superAdmincsissueTrackerDao.InsertQADataLevel2(superAdminCSIssueTrackerVo);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="superAdminCSIssueTrackerVo"></param>
        /// <returns></returns>
        /// 


        public int InsertQADataLevel3(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            try
            {
                return superAdmincsissueTrackerDao.InsertQADataLevel3(superAdminCSIssueTrackerVo);
            }
            catch
            {
                throw;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="superAdminCSIssueTrackerVo"></param>
        /// <returns></returns>
        public int InsertDEVData(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            try
            {
                return superAdmincsissueTrackerDao.InsertDEVData(superAdminCSIssueTrackerVo);
            }
            catch
            {
                throw;
            }
        }

        public int InsertDEVDataLevel2Send(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            try
            {
                return superAdmincsissueTrackerDao.InsertDEVDataLevel2Send(superAdminCSIssueTrackerVo);
            }
            catch
            {
                throw;
            }
        }
        

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="superAdminCSIssueTrackerVo"></param>
        ///// <returns></returns>
        //public int insertQACsissueLevelAssociation(IssueTrackerVo superAdminCSIssueTrackerVo)
        //{ 
        //    try
        //    {
        //        return superAdmincsissueTrackerDao.insertQACsissueLevelAssociation(superAdminCSIssueTrackerVo);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="superAdminCSIssueTrackerVo"></param>
        /// <returns></returns>
        public int UpdateCSIssueLevelAssociationDEVDetails(IssueTrackerVo superAdminCSIssueTrackerVo)
        { 
            try
            {
                return superAdmincsissueTrackerDao.UpdateCSIssueLevelAssociationDEVDetails(superAdminCSIssueTrackerVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="superAdminCSIssueTrackerVo"></param>
        /// <returns></returns>
        public int UpdateCSIssueLevelAssociationCSDetails(IssueTrackerVo superAdminCSIssueTrackerVo)
        { 
            try
            {
                return superAdmincsissueTrackerDao.UpdateCSIssueLevelAssociationCSDetails(superAdminCSIssueTrackerVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
        }

        public int UpdateCSIssueLevelAssociationLevel1ToAnyLevel(IssueTrackerVo superAdminCSIssueTrackerVo)
        { 
            try
            {
                return superAdmincsissueTrackerDao.UpdateCSIssueLevelAssociationLevel1ToAnyLevel(superAdminCSIssueTrackerVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
        }

        public int UpdateCSIssueLevelAssociationLevel2ToAnyLevel(IssueTrackerVo superAdminCSIssueTrackerVo)
        { 
            try
            {
                return superAdmincsissueTrackerDao.UpdateCSIssueLevelAssociationLevel2ToAnyLevel(superAdminCSIssueTrackerVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
        }

        public int UpdateCSIssueLevelAssociationLevel3ToAnyLevel(IssueTrackerVo superAdminCSIssueTrackerVo)
        { 
            try
            {
                return superAdmincsissueTrackerDao.UpdateCSIssueLevelAssociationLevel3ToAnyLevel(superAdminCSIssueTrackerVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
        }

        public int UpdateCSIssueLevelAssociationDEVDetailsLevel3(IssueTrackerVo superAdminCSIssueTrackerVo)
        { 
            try
            {
                return superAdmincsissueTrackerDao.UpdateCSIssueLevelAssociationDEVDetailsLevel3(superAdminCSIssueTrackerVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
        }

        public int UpdateCSIssueLevelAssociationDEVDetailsLevel2(IssueTrackerVo superAdminCSIssueTrackerVo)
        { 
            try
            {
                return superAdmincsissueTrackerDao.UpdateCSIssueLevelAssociationDEVDetailsLevel2(superAdminCSIssueTrackerVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
        }
         

        public int UpdateCSIssueLevelAssociationCSDetailsLevel2(IssueTrackerVo superAdminCSIssueTrackerVo)
        { 
            try
            {
                return superAdmincsissueTrackerDao.UpdateCSIssueLevelAssociationCSDetailsLevel2(superAdminCSIssueTrackerVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="superAdminCSIssueTrackerVo"></param>
        /// <returns></returns>
        public int UpdateCSIssueLevelAssociationQADetails(IssueTrackerVo superAdminCSIssueTrackerVo)
        { 
            try
            {
                return superAdmincsissueTrackerDao.UpdateCSIssueLevelAssociationQADetails(superAdminCSIssueTrackerVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="superAdminCSIssueTrackerVo"></param>
        /// <returns></returns>
        public int CloseIssue(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            try
            {
                return superAdmincsissueTrackerDao.CloseIssue(superAdminCSIssueTrackerVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
    }
}
