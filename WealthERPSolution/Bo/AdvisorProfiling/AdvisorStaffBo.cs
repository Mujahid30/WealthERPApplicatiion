using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VoAdvisorProfiling;
using VoUser;
using DaoAdvisorProfiling;
using DaoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;



namespace BoAdvisorProfiling
{
    public class AdvisorStaffBo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmVo"></param>
        /// <param name="advisorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int CreateAdvisorStaff(RMVo rmVo, int advisorId,int userId)
        {
            bool result = false;
            int rmId;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            
            try
            {
                rmId = advisorStaffDao.CreateAdvisorStaff(rmVo, advisorId,userId);
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:CreateAdvisorStaff()");

                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = advisorId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return rmId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CreateRMBranch(int rmId, int branchId,int userId)
        {
            bool result = false;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                advisorStaffDao.CreateRMBranch(rmId, branchId,userId);
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

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:createRMBranch()");

                object[] objects = new object[3];
                objects[0] = rmId;
                objects[1] = branchId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerName"></param>
        /// <param name="rmId"></param>
        /// <param name="currentPage"></param>
        /// <param name="count"></param>
        /// <param name="sortExpression"></param>
        /// <param name="nameFilter"></param>
        /// <param name="areaFilter"></param>
        /// <param name="pincodeFilter"></param>
        /// <param name="parentFilter"></param>
        /// <param name="cityFilter"></param>
        /// <param name="genDictParent"></param>
        /// <param name="genDictCity"></param>
        /// <returns></returns>
        public List<CustomerVo> FindCustomer(string CustomerName, 
                                            int rmId,
                                            int currentPage,
                                            out int count,
                                            string sortExpression, 
                                            string nameFilter, 
                                            string areaFilter, 
                                            string pincodeFilter,
                                            string parentFilter,
                                            string cityFilter,
                                            out Dictionary<string, string> genDictParent,
                                            out Dictionary<string, string> genDictCity
                                            )
        {
            List<CustomerVo> CustomerList = new List<CustomerVo>();
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();

            genDictParent = new Dictionary<string, string>();
            genDictCity = new Dictionary<string, string>();

            try
            {
                CustomerList = advisorStaffDao.FindCustomer(CustomerName, rmId, currentPage, out count, sortExpression, nameFilter, areaFilter, pincodeFilter, parentFilter, cityFilter, out genDictParent, out genDictCity);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:FindCustomer()");
                object[] objects = new object[2];
                objects[0] = CustomerName;
                objects[1] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return CustomerList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteRM(int rmId,int userId)
        {
            bool bResult = false;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                bResult = advisorStaffDao.DeleteRM(rmId, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:DeleteRM()");
                object[] objects = new object[2];
                objects[0] = rmId;
                objects[1] = userId; 
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
            }
            return bResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmVo"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CreateRMUser(RMVo rmVo, int userId, string password)
        {
            bool bResult = false;
            UserVo userVo = new UserVo();
            UserDao userDao = new UserDao();          
            
            
            try
            {
                userVo.Email = rmVo.Email;
                userVo.LoginId = rmVo.Email;                
                userVo.FirstName = rmVo.FirstName;
                userVo.LastName = rmVo.LastName;
                userVo.MiddleName = rmVo.MiddleName;
                userVo.Password = password.ToString();
                userVo.UserId = userId;
                userVo.UserType = "RM";
                 userDao.CreateUser(userVo);
                 bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:CreateRMUser()");


                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = userId;
                objects[2] = password;

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
        /// <param name="userVo"></param>
        /// <param name="rmVo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<int> CreateCompleteRM(UserVo userVo, RMVo rmVo, int userId)
        {
            List<int> rmIds = new List<int>();
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                rmIds = advisorStaffDao.CreateCompleteRM(userVo, rmVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:CreateCompleteRM()");


                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = userVo;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return rmIds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmName"></param>
        /// <param name="adviserId"></param>
        /// <param name="currentpage"></param>
        /// <param name="sortorder"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataSet  FindRM(string rmName,int adviserId,int currentpage,string sortorder,out int count)
        {
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            DataSet ds = new DataSet();
            try
            {
                ds = advisorStaffDao.FindRM(rmName, adviserId, currentpage, sortorder, out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:FindRM()");
                object[] objects = new object[2];
                objects[0] = rmName;
                objects[1] = adviserId; 
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
        /// <param name="branchId"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<RMVo> GetBMRMList(int branchId,int CurrentPage,out int Count)
        {
            List<RMVo> rmList = new List<RMVo>();
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            Count = 0;
            try
            {
                rmList = advisorStaffDao.GetBMRMList(branchId, CurrentPage, out Count);
            }
            catch (Exception e)
            {
                string msg = e.Message.ToString();
            }

            return rmList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public int GetUserId(int rmId)
        {
            int userId;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                userId=advisorStaffDao.GetUserId(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetUserId()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return userId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public RMVo GetAdvisorStaff(int userId)
        {
            RMVo rmVo = new RMVo();
            AdvisorStaffDao advisorStaffDao=new AdvisorStaffDao();
            try
            {
                rmVo = advisorStaffDao.GetAdvisorStaff(userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetAdvisorStaff()");


                object[] objects = new object[1];
                objects[0] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return rmVo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public RMVo GetAdvisorStaffDetails(int rmId)
        {
            RMVo rmVo = new RMVo();
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                rmVo = advisorStaffDao.GetAdvisorStaffDetails(rmId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetAdvisorStaffDetails()");


                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return rmVo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="advisorId"></param>
        /// <returns></returns>
        public List<RMVo> GetRMList(int advisorId)
        {
            List<RMVo> rmList = null;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {

                rmList = advisorStaffDao.GetRMList(advisorId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetRMList()");


                object[] objects = new object[1];
                objects[0] = advisorId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return rmList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="advisorId"></param>
        /// <param name="currentPage"></param>
        /// <param name="sortOrder"></param>
        /// <param name="Count"></param>
        /// <param name="nameSrch"></param>
        /// <returns></returns>
        public List<RMVo> GetRMList(int advisorId, int currentPage, string sortOrder, out int Count,string nameSrch)
        {
            List<RMVo> rmList = null;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {

                rmList = advisorStaffDao.GetRMList(advisorId,currentPage,sortOrder,out Count,nameSrch);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetRMList()");


                object[] objects = new object[1];
                objects[0] = advisorId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return rmList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public List<CustomerVo> GetCustomerList(int rmId)
        {
            List<CustomerVo> customerList = null;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();

            try
            {
                customerList = advisorStaffDao.GetCustomerList(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetCustomerList()");


                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public List<CustomerVo> GetBMCustomerList(int rmId)
        {
            List<CustomerVo> bmCustomerList = null;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();

            try
            {
                bmCustomerList = advisorStaffDao.GetBMCustomerList(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetBMCustomerList()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bmCustomerList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="rmId"></param>
        /// <param name="currentPage"></param>
        /// <param name="sortOrder"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<CustomerVo> GetCustomerForAssociation(int customerId, int rmId, int currentPage, string sortOrder, out int Count)
        {
            List<CustomerVo> customerList = null;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();

            try
            {
                customerList = advisorStaffDao.GetCustomerForAssociation(customerId,rmId,currentPage,sortOrder,out Count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetCustomerForAssociation()");


                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="currentPage"></param>
        /// <param name="count"></param>
        /// <param name="sortExpression"></param>
        /// <param name="nameFilter"></param>
        /// <param name="areaFilter"></param>
        /// <param name="pincodeFilter"></param>
        /// <param name="parentFilter"></param>
        /// <param name="cityFilter"></param>
        /// <param name="genDictParent"></param>
        /// <param name="genDictCity"></param>
        /// <returns></returns>
        public List<CustomerVo> GetCustomerList(int rmId, int currentPage, out int count, string sortExpression, string nameFilter, string areaFilter, string pincodeFilter, string parentFilter, string cityFilter,string active, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictCity)
        {
            List<CustomerVo> customerList = null;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();

            genDictParent = new Dictionary<string, string>();
            genDictCity = new Dictionary<string, string>();

            try
            {
                customerList = advisorStaffDao.GetCustomerList(rmId, currentPage, out count, sortExpression, nameFilter, areaFilter, pincodeFilter, parentFilter, cityFilter, active, out genDictParent, out genDictCity);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetCustomerList()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return customerList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="currentPage"></param>
        /// <param name="count"></param>
        /// <param name="sortExpression"></param>
        /// <param name="nameFilter"></param>
        /// <param name="areaFilter"></param>
        /// <param name="pincodeFilter"></param>
        /// <param name="parentFilter"></param>
        /// <param name="cityFilter"></param>
        /// <param name="RMFilter"></param>
        /// <param name="genDictParent"></param>
        /// <param name="genDictCity"></param>
        /// <param name="genDictRM"></param>
        /// <returns></returns>
        public List<CustomerVo> GetBMCustomerList(int rmId, int currentPage, out int count, string sortExpression, string nameFilter, string areaFilter, string pincodeFilter, string parentFilter, string cityFilter, string RMFilter, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictCity, out Dictionary<string, string> genDictRM)
        {
            List<CustomerVo> customerList = null;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();

            genDictParent = new Dictionary<string, string>();
            genDictCity = new Dictionary<string, string>();
            genDictRM = new Dictionary<string, string>();

            try
            {
                customerList = advisorStaffDao.GetBMCustomerList(rmId, currentPage, out count, sortExpression, nameFilter, areaFilter, pincodeFilter, parentFilter, cityFilter, RMFilter, out genDictParent, out genDictCity, out genDictRM);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetBMCustomerList()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return customerList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public List<CustomerVo> GetCustomerList(string customerName,int rmId)
        {
            List<CustomerVo> customerList = null;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                customerList = advisorStaffDao.GetCustomerList(customerName, rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetCustomerList()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return customerList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        public int GetCustomerList(int rmId, string Flag)
        {
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            int rowCount=0;
            try
            {
                rowCount = advisorStaffDao.GetCustomerList(rmId, Flag);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetCustomerList()");


                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return rowCount;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public DataSet GetRMCustomerList(int rmId)
        {
            DataSet dsCustomerList = new DataSet();
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();

            try
            {
                dsCustomerList = advisorStaffDao.GetRMCustomerListDataSet(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetRMCustomerList()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsCustomerList;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmVo"></param>
        /// <returns></returns>
        public bool UpdateStaff(RMVo rmVo)
        {
            bool bResult = false;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                bResult = advisorStaffDao.UpdateStaff(rmVo);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:updateStaff()");


                object[] objects = new object[3];
                objects[0] = rmVo;

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
        /// <param name="currentPage"></param>
        /// <param name="sortOrder"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public DataSet GetBMStaff(int rmId, int currentPage, string sortOrder, out int Count)
        {
            DataSet dsRMList;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                dsRMList = advisorStaffDao.GetBMStaff(rmId, currentPage, sortOrder, out Count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetBMStaff()");
                object[] objects = new object[4];
                objects[0] = rmId;
                objects[1] = currentPage;
                objects[2] = sortOrder;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsRMList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="advisorId"></param>
        /// <returns></returns>
        public DataTable GetAdvisorAssociatesDropDown(int advisorId)
        {
            DataTable dtAdvisorAssociates;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                dtAdvisorAssociates = advisorStaffDao.GetAdvisorAssociatesDropDown(advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetAdvisorAssociatesDropDown()");
                object[] objects = new object[1];
                objects[0] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAdvisorAssociates;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public DataTable GetExternalRMList(int adviserId,int flag)
        {
            DataTable dt;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                dt = advisorStaffDao.GetExternalRMList(adviserId,flag);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetAdvisorAssociatesDropDown()");
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
        /// <param name="rmId"></param>
        /// <returns></returns>
        public int CheckRMMainBranch(int rmId)
        {
            int cnt;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                cnt = advisorStaffDao.CheckRMMainBranch(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetAdvisorAssociatesDropDown()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return cnt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public int GetRMBranchId(int rmId)
        {
            int branchId = 0;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                branchId = advisorStaffDao.GetRMBranchId(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetRMBranchId()");
                object[] objects = new object[1];
                objects[0] = rmId;
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
        /// <param name="branchId"></param>
        /// <returns></returns>
        public DataTable GetBranchRMList(int branchId)
        {
            DataTable dtBranchRMList;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                dtBranchRMList = advisorStaffDao.GetBranchRMList(branchId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetBranchRMList()");
                object[] objects = new object[1];
                objects[0] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtBranchRMList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataTable GetAdviserRM(int adviserId)
        {
            DataTable dtAdviserRMList;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                dtAdviserRMList = advisorStaffDao.GetAdviserRM(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetAdviserRM()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAdviserRMList;
        }


        /// <summary>
        /// Checks the RM BM Role Have4 any dependency...
        /// </summary>
        /// <param name="RMID"></param>
        /// <returns></returns>
        public Hashtable CheckRMDependency(int RMID)
        {
            Hashtable ht = new Hashtable();
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();

            try
            {
                ht = advisorStaffDao.CheckRMDependency(RMID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:CheckRMDependency()");
                object[] objects = new object[1];
                objects[0] = RMID;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ht;
        }

        /* For Getting Staffs according to Branch selection */

        public List<RMVo> GetBMRMList(int branchId, int branchHeadId, int all, int currentPage, out int Count)
        {
            List<RMVo> rmList = new List<RMVo>();
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            Count = 0;
            try
            {
                rmList = advisorStaffDao.GetBMRMList(branchId, branchHeadId, all, currentPage, out Count);
            }
            catch (Exception e)
            {
                string msg = e.Message.ToString();
            }

            return rmList;
        }

        /* End */


        /* To Get All Customers Associated to the Perticular BM */

        public List<CustomerVo> GetAllBMCustomerList(int branchId, int branchHeadId, int all, int rmId, int currentPage, out int count, string sortExpression, string nameFilter, string areaFilter, string pincodeFilter, string parentFilter, string cityFilter, string RMFilter, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictCity, out Dictionary<string, string> genDictRM)
        {
            List<CustomerVo> customerList = null;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            genDictParent = new Dictionary<string, string>();
            genDictCity = new Dictionary<string, string>();
            genDictRM = new Dictionary<string, string>();

            try
            {
                customerList = advisorStaffDao.GetAllBMCustomerList(branchId, branchHeadId, all, rmId, currentPage, out count, sortExpression, nameFilter, areaFilter, pincodeFilter, parentFilter, cityFilter, RMFilter, out genDictParent, out genDictCity, out genDictRM);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetBMCustomerList()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return customerList;
        }

        /* End */


        /* Branc Dropdown For BM */

        public DataTable GetRMListForBranchDP(int branchId, int branchHeadId, int all)
        {
            DataTable dtBranchRMList;
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            try
            {
                dtBranchRMList = advisorStaffDao.GetRMListForBranchDP(branchId, branchHeadId, all);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetBranchRMList()");
                object[] objects = new object[2];
                objects[0] = branchId;
                objects[1] = branchHeadId;
                objects[2] = all;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtBranchRMList;
        }

        /* ******************** */
    }
}