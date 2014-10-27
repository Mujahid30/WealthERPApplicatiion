using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DaoUser;
using VoUser;
using VoAdvisorProfiling;
using DaoAdvisorProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoEmailSMS;
using System.Collections;


namespace BoAdvisorProfiling
{
    public class AdvisorBo
    {
        /// <summary>
        /// Registers a New Adviser
        /// </summary>
        /// <param name="userVo"></param>
        /// <param name="advisorVo"></param>
        /// <param name="rmVo"></param>
        /// <returns>List<int></returns>
        public List<int> RegisterAdviser(UserVo userVo, AdvisorVo advisorVo, RMVo rmVo)
        {
            List<int> Ids = new List<int>();
            AdvisorDao advisorDao = new AdvisorDao();
            try
            {
                Ids = advisorDao.RegisterAdviser(userVo, advisorVo, rmVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:CreateCompleteAdviser()");
                object[] objects = new object[3];
                objects[0] = advisorVo;
                objects[1] = rmVo;
                objects[2] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return Ids;
        }
        /// <summary>
        /// To Create Complete Adviser (UserAccount, AdviserAccount and AdviserStaff Account)
        /// </summary>
        /// <param name="userVo"></param>
        /// <param name="adviserVo"></param>
        /// <param name="rmVo"></param>
        /// <returns>List<int></returns>
        public List<int> CreateCompleteAdviser(UserVo userVo, AdvisorVo adviserVo, RMVo rmVo)
        {
            List<int> Ids = new List<int>();
            AdvisorDao advisorDao = new AdvisorDao();
            try
            {
                Ids = advisorDao.CreateCompleteAdviser(userVo, adviserVo, rmVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:CreateCompleteAdviser()");
                object[] objects = new object[3];
                objects[0] = adviserVo;
                objects[1] = rmVo;
                objects[2] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return Ids;
        }
        /// <summary>
        /// Create Single Adviser
        /// </summary>
        /// <param name="advisorVo"></param>
        /// <returns>advisorId</returns>
        public int CreateAdvisor(AdvisorVo advisorVo)
        {

            int advisorId;

            AdvisorDao advisorDao = new AdvisorDao();

            try
            {
                advisorId = advisorDao.CreateAdvisor(advisorVo);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:CreateAdvisor()");


                object[] objects = new object[1];
                objects[0] = advisorVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return advisorId;

        }
        /// <summary>
        /// Returns list of Adviser's Customers based on the Adviser Id with Paging
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="currentPage"></param>
        /// <param name="sortOrder"></param>
        /// <returns>List of CustomerIds</returns>
        public List<int> GetAdviserCustomer(int adviserId, int currentPage, string sortOrder)
        {
            List<int> customerList = new List<int>();
            AdvisorDao advisorDao = new AdvisorDao();
            try
            {
                customerList = advisorDao.GetAdviserCustomer(adviserId, currentPage, sortOrder);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserCustomer()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return customerList;
        }
        /// <summary>
        /// Creates User Account for an Adviser
        /// </summary>
        /// <param name="advisorVo"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns>bool result</returns>
        public bool CreateAdvisorUser(AdvisorVo advisorVo, int userId, string password)
        {
            bool bResult = false;

            UserVo userVo = new UserVo();
            UserDao userDao = new UserDao();
            AdvisorDao advisorDao = new AdvisorDao();
            Random id = new Random();

            try
            {
                userVo.Email = advisorVo.Email;
                userVo.FirstName = advisorVo.ContactPersonFirstName.ToString();
                userVo.MiddleName = advisorVo.ContactPersonMiddleName.ToString();
                userVo.LastName = advisorVo.ContactPersonMiddleName.ToString();
                userVo.Password = id.Next(10000, 99999).ToString();
                userVo.UserType = "Advisor";
                userVo.UserId = userId;
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

                FunctionInfo.Add("Method", "AdvisorBo.cs:CreateAdvisorUser()");

                object[] objects = new object[1];
                objects[0] = advisorVo;
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
        /// Returns Adviser's Customer Object List based on Adviser Id and the filters
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="Count"></param>
        /// <param name="SortExpression"></param>
        /// <param name="NameFilter"></param>
        /// <param name="AreaFilter"></param>
        /// <param name="PincodeFilter"></param>
        /// <param name="ParentFilter"></param>
        /// <param name="RMFilter"></param>
        /// <param name="genDictParent"></param>
        /// <param name="genDictRM"></param>
        /// <param name="genDictReassignRM"></param>
        /// <returns>List of CustomerVo</returns>
        public List<CustomerVo> GetAdviserCustomerList(int adviserId, int CurrentPage, out int Count, string SortExpression, string panFilter, string NameFilter, string AreaFilter, string PincodeFilter, string ParentFilter, string RMFilter, string Active, string IsProspect, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictRM, out Dictionary<string, string> genDictReassignRM)
        {
            List<CustomerVo> customerList = null;
            AdvisorDao advisorDao = new AdvisorDao();

            genDictParent = new Dictionary<string, string>();
            genDictRM = new Dictionary<string, string>();

            Count = 0;

            try
            {
                customerList = advisorDao.GetAdviserCustomerList(adviserId, CurrentPage, out Count, SortExpression, panFilter, NameFilter, AreaFilter, PincodeFilter, ParentFilter, RMFilter, Active, IsProspect, out genDictParent, out genDictRM, out genDictReassignRM);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserCustomerList()");

                object[] objects = new object[11];
                objects[0] = adviserId;
                objects[1] = CurrentPage;
                objects[2] = Count;
                objects[3] = SortExpression;
                objects[4] = NameFilter;
                objects[5] = AreaFilter;
                objects[6] = PincodeFilter;
                objects[7] = ParentFilter;
                objects[8] = RMFilter;
                objects[9] = genDictParent;
                objects[10] = genDictRM;

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
        /// <param name="adviserId"></param>
        /// <returns>List of CustomerVo</returns>
        public List<CustomerVo> GetAdviserAllCustomerList(int adviserId)
        {
            List<CustomerVo> customerList = null;
            AdvisorDao advisorDao = new AdvisorDao();
            try
            {
                customerList = advisorDao.GetAdviserAllCustomerList(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserAllCustomerList()");


                object[] objects = new object[4];
                objects[0] = adviserId;


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
        /// <param name="smsVoList"></param>
        /// <param name="adviserId"></param>
        /// <param name="smsType"></param>
        /// <returns>bool Result</returns>
        public bool AddToAdviserSMSLog(List<SMSVo> smsVoList, int adviserId, string smsType)
        {
            bool bResult = false;
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                for (int i = 0; i < smsVoList.Count; i++)
                {
                    bResult = AddToAdviserSMSLog(smsVoList[i], adviserId, smsType);
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

                FunctionInfo.Add("Method", "AdvisorBo.cs:AddToAdviserSMSLog(List<SMSVo> smsVoList, int adviserId, string smsType)");


                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = smsVoList;

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
        /// <param name="smsVo"></param>
        /// <param name="adviserId"></param>
        /// <param name="smsType"></param>
        /// <returns>bool Result</returns>
        public bool AddToAdviserSMSLog(SMSVo smsVo, int adviserId, string smsType)
        {
            bool bResult = false;
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                bResult = adviserDao.AddToAdviserSMSLog(smsVo, adviserId, smsType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:UpdateAdviserSMSLicence(int adviserId, int smsLincence)");


                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = smsVo;

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
        /// <param name="adviserId"></param>
        /// <param name="namefilter"></param>
        /// <returns>List of CustomerVo</returns>
        public List<CustomerVo> GetAdviserCustomersForSMS(int adviserId, int rmId, string namefilter)
        {
            List<CustomerVo> customerList = new List<CustomerVo>();

            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                customerList = adviserDao.GetAdviserCustomersForSMS(adviserId, rmId, namefilter);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserCustomersForSMS(int adviserId, string namefilter)");


                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = rmId;


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
        /// <param name="adviserId"></param>
        /// <param name="smsLincence"></param>
        /// <returns>bool Result</returns>
        public bool UpdateAdviserSMSLicence(int adviserId, int smsLincence)
        {
            bool bResult = false;
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                bResult = adviserDao.UpdateAdviserSMSLicence(adviserId, smsLincence);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:UpdateAdviserSMSLicence(int adviserId, int smsLincence)");


                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = smsLincence;

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
        /// <param name="adviserId"></param>
        /// <returns>Dataset of AdviserSubscription Details</returns>
        public DataSet GetAdviserSubscriptionDetails(int adviserId)
        {
            DataSet dsAdviserSubscriptionDetails;
            AdvisorDao advisorDao = new AdvisorDao();
            try
            {
                dsAdviserSubscriptionDetails = advisorDao.GetAdviserSubscriptionDetails(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserSubscriptionDetails(int adviserId)");


                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsAdviserSubscriptionDetails;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="advisorId"></param>
        /// <returns>AdviserVo</returns>
        public AdvisorVo GetAdvisor(int advisorId)
        {
            AdvisorVo advisorVo = null;
            AdvisorDao advisorDao = new AdvisorDao();
            try
            {
                advisorVo = advisorDao.GetAdvisor(advisorId);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdvisor()");


                object[] objects = new object[1];
                objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return advisorVo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmId"></param>
        /// <returns>AdviserId</returns>
        public int GetRMAdviserId(int rmId)
        {
            int adviserId;
            AdvisorDao adviserDao = new AdvisorDao();

            try
            {
                adviserId = adviserDao.GetRMAdviserId(rmId);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetRMAdviserId()");
                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return adviserId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="Flag"></param>
        /// <returns>int count</returns>
        public int GetAdviserCustomerList(int adviserId, string Flag)
        {
            AdvisorDao advisorDao = new AdvisorDao();
            int count;
            try
            {
                count = advisorDao.GetAdviserCustomerList(adviserId, Flag);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserCustomerList()");


                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = Flag;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns>Advisers Customer Dataset</returns>
        public DataSet GetAdviserCustomerListDataSet(int adviserId)
        {
            DataSet ds = new DataSet();
            AdvisorDao advisorDao = new AdvisorDao();
            try
            {
                ds = advisorDao.GetAdviserCustomerListDataSet(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserCustomerListDataSet()");


                object[] objects = new object[1];
                objects[0] = adviserId;

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
        /// <param name="advisorId"></param>
        /// <returns>UserVo</returns>
        public UserVo GetAdvisorUserInfo(int advisorId)
        {
            UserVo userVo = null;
            UserDao userDao = new UserDao();
            AdvisorDao advisorDao = new AdvisorDao();
            string username = "";
            try
            {
                username = advisorDao.GetAdvisorUsername(advisorId);
                userVo = userDao.GetUser(username);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdvisorUserInfo()");


                object[] objects = new object[1];
                objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return userVo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerName"></param>
        /// <param name="advisorId"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="Count"></param>
        /// <param name="SortExpression"></param>
        /// <param name="NameFilter"></param>
        /// <param name="AreaFilter"></param>
        /// <param name="PincodeFilter"></param>
        /// <param name="ParentFilter"></param>
        /// <param name="RMFilter"></param>
        /// <param name="genDictParent"></param>
        /// <param name="genDictRM"></param>
        /// <param name="genDictReassignRM"></param>
        /// <returns></returns>
        public List<CustomerVo> FindCustomer(string CustomerName, int advisorId, int CurrentPage, out int Count, string SortExpression, string NameFilter, string AreaFilter, string PincodeFilter, string ParentFilter, string RMFilter, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictRM, out Dictionary<string, string> genDictReassignRM)
        {
            List<CustomerVo> CustomerList = new List<CustomerVo>();
            AdvisorDao advisorDao = new AdvisorDao();

            genDictParent = new Dictionary<string, string>();
            genDictRM = new Dictionary<string, string>();
            genDictReassignRM = new Dictionary<string, string>();

            Count = 0;

            try
            {
                CustomerList = advisorDao.FindCustomer(CustomerName, advisorId, CurrentPage, out Count, SortExpression, NameFilter, AreaFilter, PincodeFilter, ParentFilter, RMFilter, out genDictParent, out genDictRM, out genDictReassignRM);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:FindCustomer()");
                object[] objects = new object[2];
                objects[0] = CustomerName;
                objects[1] = advisorId;
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
        /// <param name="userId"></param>
        /// <returns></returns>
        public AdvisorVo GetAdvisorUser(int userId)
        {


            AdvisorVo advisorVo = null;
            AdvisorDao advisorDao = new AdvisorDao();
            try
            {
                advisorVo = advisorDao.GetAdvisorUser(userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdvisorUser()");


                object[] objects = new object[1];
                objects[0] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return advisorVo;
        }
        //public string GetBusinessCode(string businessType,string path)
        //{
        //    string bCode = " ";
        //    AdvisorDao advisorDao;
        //    try
        //    {
        //        advisorDao = new AdvisorDao();
        //        bCode = advisorDao.GetBusinessCode(businessType, path);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AdvisorBo.cs:getBusinessCode()");


        //        object[] objects = new object[2];
        //        objects[0] = businessType;
        //        objects[1] = path;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return bCode;  

        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="advisorVo"></param>
        /// <returns></returns>
        public bool UpdateAdvisorUser(AdvisorVo advisorVo)
        {
            bool bResult = false;
            AdvisorDao advisorDao = new AdvisorDao();
            try
            {
                bResult = advisorDao.UpdateAdvisorUser(advisorVo);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:updateAdvisorUser()");


                object[] objects = new object[1];
                objects[0] = advisorVo;

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
        /// <returns></returns>
        public string getId()
        {
            Guid id;
            id = Guid.NewGuid();
            return id.ToString();
        }
        /// <summary>
        /// Get all Classification List of the advisor
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataSet GetAdviserClassification(int adviserId)
        {
            AdvisorDao advisorDao = new AdvisorDao();
            DataSet dsAdviserClassification;

            try
            {
                dsAdviserClassification = advisorDao.GetAdviserClassification(adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserClassification(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserClassification;

        }

        /// <summary>
        /// Function to retrieve the tree nodes based on the user role
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataSet GetTreeNodesBasedOnUserRoles(string userRole, string treeType, int adviserId)
        {
            AdvisorDao advisorDao = new AdvisorDao();
            DataSet dsAdviserTreeNodes;

            try
            {
                dsAdviserTreeNodes = advisorDao.GetTreeNodesBasedOnUserRoles(userRole, treeType, adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetTreeNodesBasedOnUserRoles(string userRole)");
                object[] objects = new object[1];
                objects[0] = userRole;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserTreeNodes;

        }

        public DataSet GetTreeNodesBasedOnUserRoles(string treeType, int adviserId)
        {
            AdvisorDao advisorDao = new AdvisorDao();
            DataSet dsAdviserTreeNodes;

            try
            {
                dsAdviserTreeNodes = advisorDao.GetTreeNodesBasedOnUserRoles(treeType, adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetTreeNodesBasedOnUserRoles(string treeType)");
                object[] objects = new object[1];
                objects[0] = treeType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserTreeNodes;

        }

        /// <summary>
        /// Function to retrieve the tree nodes based on the plans subscribed
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataSet GetTreeNodesBasedOnPlans(int adviserId, string userRole, string treeType)
        {
            AdvisorDao advisorDao = new AdvisorDao();
            DataSet dsAdviserTreeNodes;

            try
            {
                dsAdviserTreeNodes = advisorDao.GetTreeNodesBasedOnPlans(adviserId, userRole, treeType);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetTreeNodesBasedOnPlans()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = userRole;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserTreeNodes;

        }

        /// <summary>
        /// Function to retrieve the potential home page fot a user
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataSet GetUserPotentialHomepages(int adviserId, string userRole)
        {
            AdvisorDao advisorDao = new AdvisorDao();
            DataSet dsUserPotentialHomepage;

            try
            {
                dsUserPotentialHomepage = advisorDao.GetUserPotentialHomepages(adviserId, userRole);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetUserPotentialHomepages()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = userRole;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsUserPotentialHomepage;

        }

        /// <summary>
        /// Get domain name for the adviser for login widget
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public string GetAdviserDomainName(int adviserId)
        {

            string domain = "";
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {

                domain = adviserDao.GetAdviserDomainName(adviserId);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserDomainName(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return domain;

        }

        /// <summary>
        /// Gets category that adviser belongs to
        /// </summary>
        /// <returns></returns>
        public DataSet GetXMLAdvisorCategory()
        {
            DataSet dsGetXMLAdvisorCategory;
            AdvisorDao advisorDao = new AdvisorDao();
            try
            {
                dsGetXMLAdvisorCategory = advisorDao.GetXMLAdvisorCategory();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserSubscriptionDetails(int adviserId)");


                object[] objects = new object[1];
                objects[0] = "GetXMLAdvisorCategory";

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetXMLAdvisorCategory;
        }

        /// <summary>
        /// Updates Complete Advisor which includes AdviserRM,Adviser and Adviser Table
        /// Caution:Please use this only if it is needed.
        /// </summary>
        /// <param name="userVo"></param>
        /// <param name="adviserVo"></param>
        /// <param name="rmVo"></param>
        public void UpdateCompleteAdviser(UserVo userVo, AdvisorVo adviserVo, RMVo rmVo)
        {

            AdvisorDao advisorDao = new AdvisorDao();


            try
            {
                advisorDao.UpdateCompleteAdviser(userVo, adviserVo, rmVo);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:CreateCompleteAdviser()");
                object[] objects = new object[3];
                objects[0] = adviserVo;
                objects[1] = rmVo;
                objects[2] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        /// <summary>
        /// To Create AdviserIPsPool Information for Adviser 
        /// Added by Vinayak Patil
        /// </summary>
        /// <param name="adviserIPvo"></param>
        /// <param name="createdBy"></param>
        /// <returns>adviserIPPoolstatus</returns>

        public bool CreateAdviserIPPools(AdviserIPVo adviserIPvo, int createdBy)
        {

            bool bStatus = false;
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                bStatus = adviserDao.CreateAdviserIPPools(adviserIPvo, createdBy);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:CreateAdviserIPPools(AdviserIPVo adviserIPvo, int createdBy)");
                object[] objects = new object[3];
                objects[0] = adviserIPvo;
                objects[1] = createdBy;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bStatus;

        }

        /// <summary>
        /// To Get all AdviserIPPool Informations
        /// Added by Vinayak Patil
        /// </summary>
        /// <param name="AdviserId"></param>
        /// <returns></returns>

        public DataSet GetAdviserIPPoolsInformation(int AdviserId)
        {
            DataSet dsGetAdviserIP = new DataSet();
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                dsGetAdviserIP = adviserDao.GetAdviserIPPoolsInformation(AdviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFamilyBo.cs:GetAdviserIPPoolsInformation()");


                object[] objects = new object[1];
                objects[0] = AdviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetAdviserIP;
        }

        /// <summary>
        /// To Delete last IP Pool from Adviser IP Pool 
        /// Vinayak Patil
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>

        public bool DeleteAdviserIPPool(int adviserIPPoolId, int adviserId, bool isSingleIP, string Flag)
        {
            bool bResult = false;
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                bResult = adviserDao.DeleteAdviserIPPools(adviserIPPoolId, adviserId, isSingleIP, Flag);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:DeleteAdviserIPPools(adviserIPPoolId, Flag)");

                object[] objects = new object[1];
                objects[0] = adviserIPPoolId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        /// <summary>
        /// To Delete IP Pool from Adviser IP Pool 
        /// Vinayak Patil 
        /// </summary>
        /// <param name="adviserIPPoolId"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>

        public bool DeleteAdviserIPPools(int adviserIPPoolId, int adviserId, bool isSingleIP, string Flag)
        {
            bool bResult = false;
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                bResult = adviserDao.DeleteAdviserIPPools(adviserIPPoolId, adviserId, isSingleIP, Flag);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:DeleteChildCustomer(int adviserIPPoolId, string Flag)");

                object[] objects = new object[1];
                objects[0] = adviserIPPoolId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public DataSet GetAdvisersAlreadyLoggedIPs(int AdviserId)
        {
            DataSet dsAdviserAlreadyLoggedIps = new DataSet();
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                dsAdviserAlreadyLoggedIps = adviserDao.GetAdvisersAlreadyLoggedIPs(AdviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserIPPoolsInformation()");


                object[] objects = new object[1];
                objects[0] = AdviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsAdviserAlreadyLoggedIps;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adviserIPvo"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public bool UpdateAdviserIPPools(AdviserIPVo adviserIPvo, int createdBy, string Flag)
        {
            bool bResult = false;
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                bResult = adviserDao.UpdateAdviserIPPools(adviserIPvo, createdBy, Flag);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:CreateAdviserIPPools(AdviserIPVo adviserIPvo, int createdBy)");
                object[] objects = new object[3];
                objects[0] = adviserIPvo;
                objects[1] = createdBy;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        /// <summary>
        /// Get All the Ops Staffs for a perticular advisor...
        /// Added by Vinayak Patil on 20th Sep 2011
        /// </summary>
        /// <param name="AdviserId"></param>
        /// <param name="UserRole"></param>
        /// <returns></returns>

        public DataSet GetAllOpsStaffsForAdviser(int AdviserId, string UserRole)
        {
            DataSet dsAllOpsStaffsForAdviser = new DataSet();
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                dsAllOpsStaffsForAdviser = adviserDao.GetAllOpsStaffsForAdviser(AdviserId, UserRole);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAllOpsStaffsForAdviser()");


                object[] objects = new object[1];
                objects[0] = AdviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsAllOpsStaffsForAdviser;
        }

        /// <summary>
        /// To Update the perticular RM Status..
        /// Added by Vinayak Patil on 20th Sep 2011
        /// </summary>
        /// <param name="RMId"></param>
        /// <param name="RMLoginStatus"></param>
        /// <returns></returns>

        public bool UpdateOpsStaffLoginStatus(int RMId, int RMLoginStatus)
        {
            bool bResult = false;
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                bResult = adviserDao.UpdateOpsStaffLoginStatus(RMId, RMLoginStatus);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:CreateAdviserIPPools(AdviserIPVo adviserIPvo, int createdBy)");
                object[] objects = new object[3];
                objects[0] = RMId;
                objects[1] = RMLoginStatus;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        /// <summary>
        /// Get all the Advisers Online Transaction AMC Links..
        /// </summary>
        /// <param name="aotalVo"></param>
        /// <returns></returns>

        public List<AdviserOnlineTransactionAMCLinksVo> GetAdviserOnlineTransactionAMCLinks(AdviserOnlineTransactionAMCLinksVo aotalVo)
        {
            DataSet dsGetAdviserOnlineTransactionAMCLinks = new DataSet();
            AdvisorDao adviserDao = new AdvisorDao();
            List<AdviserOnlineTransactionAMCLinksVo> adviserOTALink = null;
            try
            {
                adviserOTALink = adviserDao.GetAdviserOnlineTransactionAMCLinks(aotalVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserOnlineTransactionAMCLinks(AdviserOnlineTransactionAMCLinksVo aotalVo)");


                object[] objects = new object[1];
                objects[0] = aotalVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return adviserOTALink;
        }

        /// <summary>
        /// Get Theme name for the adviser for login widget
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetAdviserLogInWidGetDetails(int adviserId)
        {

            Dictionary<string, string> advisorLoginWidgetDetails = new Dictionary<string, string>();
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                advisorLoginWidgetDetails = adviserDao.GetAdviserLogInWidgetDetails(adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserLogInWidGetDetails(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return advisorLoginWidgetDetails;

        }

        public bool UpdateAdviserFPBatch(string customerIds, int adviserId)
        {
            AdvisorDao adviserDao = new AdvisorDao();
            bool result = true;
            try
            {
                result = adviserDao.UpdateAdviserFPBatch(customerIds, adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:UpdateAdviserFPBatch(string customerIds)");
                object[] objects = new object[1];
                objects[0] = customerIds;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;

        }

        public bool UpdateAdviserStorageBalance(int intAdviserId, float fStorageBal)
        {
            AdvisorDao adviserDao = new AdvisorDao();
            bool result = true;
            try
            {
                result = adviserDao.UpdateAdviserStorageBalance(intAdviserId, fStorageBal);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:UpdateAdviserStorageBalance(int intAdviserId, float fStorageBal)");
                object[] objects = new object[2];
                objects[0] = intAdviserId;
                objects[1] = fStorageBal;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public void UpdateAdviserLoginWidgetSetting(int adviserId, string webSiteName, bool isLoginWidgetEnable)
        {
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                adviserDao.UpdateAdviserLoginWidgetSetting(adviserId, webSiteName, isLoginWidgetEnable);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:UpdateAdviserLoginWidgetSetting(int adviserId, string webSiteName, bool isLoginWidgetEnable)");
                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = webSiteName;
                objects[1] = isLoginWidgetEnable;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        /// <summary>
        /// This method would perform the operation for add edit and delete
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="rmId"></param>
        /// <param name="ZoneId"></param>
        /// <param name="Description"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="createdBy"></param>
        /// <param name="modifiedBy"></param>
        /// <param name="createdDate"></param>
        /// <param name="modifiedDate">modified date of the zone/cluster</param>
        /// <param name="CommandName">operation filtered by commandname if edit delete or add</param>
        /// <returns>rows true or false</returns>
        public bool ZoneClusterDetailsAddEditDelete(int adviserId, int rmId, int ZoneClusterId, int ZoneId, string Description, string name, string type, int createdBy, int modifiedBy, DateTime createdDate, DateTime modifiedDate, string CommandName)
        {
            AdvisorDao adviserDao = new AdvisorDao();
            bool inserted = false;
            try
            {
                inserted = adviserDao.ZoneClusterDetailsAddEditDelete(adviserId, rmId, ZoneClusterId, ZoneId, Description, name, type, createdBy, modifiedBy, createdDate, modifiedDate, CommandName);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:ZoneClusterDetailsAddEditDelete(int adviserId,int rmId,int ZoneId, string Description, string name, string type, int createdBy,int modifiedBy,DateTime createdDate, string CommandName)");
                object[] objects = new object[10];
                objects[0] = adviserId;
                objects[1] = rmId;
                objects[2] = ZoneId;
                objects[3] = Description;
                objects[4] = name;
                objects[5] = type;
                objects[6] = createdBy;
                objects[7] = modifiedBy;
                objects[8] = createdDate;
                objects[9] = CommandName;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return inserted;
        }


        /// <summary>
        /// Get the details of the zone and the RM to bind the RM and the zone drop down 
        /// </summary>
        /// <param name="AdviserId"></param>
        /// <returns>dataset with two tables consisting of the RM and the Zone details</returns>
        public DataSet GetRMDetailsAdviserwiseAndZoneDetails(int AdviserId)
        {
            AdvisorDao adviserDao = new AdvisorDao();
            DataSet dsGetRMDetailsAdviserwiseAndZoneDetails;
            try
            {
                dsGetRMDetailsAdviserwiseAndZoneDetails = adviserDao.GetRMDetailsAdviserwiseAndZoneDetails(AdviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetRMDetailsAdviserwiseAndZoneDetails(int AdviserId)");
                object[] objects = new object[3];
                objects[0] = AdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetRMDetailsAdviserwiseAndZoneDetails;
        }

        /// <summary>
        /// Getting all the details of the zone/cluster of the adviser
        /// </summary>
        /// <param name="AdviserId"></param>
        /// <param name="type"></param>
        /// <returns>dataset of the zone/cluster adviserwise</returns>
        public DataSet GetZoneClusterDetailsAdviserwise(int AdviserId, int type)
        {
            AdvisorDao adviserDao = new AdvisorDao();
            DataSet dsGetZoneClusterDetails;
            try
            {
                dsGetZoneClusterDetails = adviserDao.GetZoneClusterDetailsAdviserwise(AdviserId, type);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetZoneClusterDetailsAdviserwise(int AdviserId,int type)");
                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = type;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetZoneClusterDetails;
        }

        public DataSet GetAdviserCategory(int AdviserId)
        {
            AdvisorDao adviserDao = new AdvisorDao();
            DataSet dsGetAdviserCustomerCategory;
            try
            {
                dsGetAdviserCustomerCategory = adviserDao.GetAdviserCategory(AdviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetAdviserCustomerCategory;
        }


        public DataSet GetAdviserCustomerCategory(int AdviserId)
        {
            AdvisorDao adviserDao = new AdvisorDao();
            DataSet dsGetAdviserCustomerCategory;
            try
            {
                dsGetAdviserCustomerCategory = adviserDao.GetAdviserCustomerCategory(AdviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetAdviserCustomerCategory;
        }

        public bool DeleteAdviserCustomerCategory(int CategoryCode)
        {
            AdvisorDao adviserDao = new AdvisorDao();
            bool isDeleted = false;
            try
            {
                isDeleted = adviserDao.DeleteAdviserCustomerCategory(CategoryCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isDeleted;
        }

        public bool InsertAdviserCustomerCategory(string CategoryName, int AdviserId)
        {
            AdvisorDao adviserDao = new AdvisorDao();
            bool isInserted = false;
            try
            {
                isInserted = adviserDao.InsertAdviserCustomerCategory(CategoryName, AdviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isInserted;
        }

        public bool EditAdviserCustomerCategory(int CategoryCode, string CategoryName, int AdviserId)
        {
            AdvisorDao adviserDao = new AdvisorDao();
            bool isUpdate = false;
            try
            {
                isUpdate = adviserDao.EditAdviserCustomerCategory(CategoryCode, CategoryName, AdviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isUpdate;
        }

        public void CheckIfValuationDateAlreadyInQueue(DateTime valuationDate, int adviserId, out int Count, out int totalCountGivenToday, out int CountforPendingRecords)
        {
            AdvisorDao adviserDao = new AdvisorDao();
            //DataSet dsValuationDetails = new DataSet();
            try
            {
                //dsValuationDetails = 
                adviserDao.CheckIfValuationDateAlreadyInQueue(valuationDate, adviserId, out Count, out totalCountGivenToday, out CountforPendingRecords);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            //return dsValuationDetails;
        }

        public void InsertHistoricalValuationInQueue(DateTime valuationDate, int adviserId, int userId, int isCurrent)
        {
            AdvisorDao adviserDao = new AdvisorDao();
            //DataSet dsValuationDetails = new DataSet();
            try
            {
                //dsValuationDetails = 
                adviserDao.InsertHistoricalValuationInQueue(valuationDate, adviserId, userId, isCurrent);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            //return dsValuationDetails;
        }

        /// <summary>
        /// Get StaffUser Customer List
        /// </summary>
        /// <param name="adviserId"> </param>
        /// <param name="rmId"></param>
        /// <param name="UserRole"></param>
        /// <param name="branchHeadId"></param>
        /// <param name="PageSize"></param>
        /// <param name="genDictParent"></param>
        /// <param name="genDictRM"></param>
        /// <param name="genDictReassignRM"></param>
        /// <returns>will return the list of the customers from the data base accroding to the parameters assigned</returns>
        public List<CustomerVo> GetStaffUserCustomerList(int adviserId, int rmId, int AgentId, string UserRole, int branchHeadId, string agentCode, string filterOn, int customerId, string customerCategoryFilter, string customerFilter, string custcodeFilter, string nameFilter, string parentFilter, string panFilter, string BranchFilter, string Rmfilter, string areaFilter, string cityFilter, string pincodeFilter, string IsProspectFilter, string isActiveFilter, string iskycavailableFilter, string processFilter, int pageSize, int pageindex, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictRM, out Dictionary<string, string> genDictReassignRM, out int RowCount)
        {
            List<CustomerVo> customerList = null;
            AdvisorDao advisorDao = new AdvisorDao();

            genDictParent = new Dictionary<string, string>();
            genDictRM = new Dictionary<string, string>();
            genDictReassignRM = new Dictionary<string, string>();
            
            try
            {
                customerList = advisorDao.GetStaffUserCustomerList(adviserId, rmId, AgentId, UserRole, branchHeadId, agentCode,filterOn, customerId,customerCategoryFilter,customerFilter,custcodeFilter,nameFilter,parentFilter,panFilter, BranchFilter, Rmfilter,areaFilter,cityFilter,pincodeFilter,IsProspectFilter,isActiveFilter,iskycavailableFilter,processFilter,pageSize,pageindex, out genDictParent, out genDictRM, out genDictReassignRM, out RowCount);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetStaffUserCustomerList()");
                object[] objects = new object[8];
                objects[0] = adviserId;
                objects[1] = genDictParent;
                objects[2] = genDictRM;
                objects[3] = genDictReassignRM;
                objects[4] = rmId;
                objects[5] = UserRole;
                objects[6] = branchHeadId;
                objects[7] = agentCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return customerList;
        }

        /// <summary>
        /// Get StaffUser Customer List
        /// </summary>
        /// <param name="adviserId"> </param>
        /// <param name="rmId"></param>
        /// <param name="UserRole"></param>
        /// <param name="branchHeadId"></param>
        /// <param name="PageSize"></param>
        /// <param name="genDictParent"></param>
        /// <param name="genDictRM"></param>
        /// <param name="genDictReassignRM"></param>
        /// <returns>will return the list of the customers from the data base accroding to the parameters assigned</returns>
        public DataSet GetAssociateCustomerList(int adviserId, string UserRole, string agentCode)
        {
            DataSet dsCustList = null;
            AdvisorDao advisorDao = new AdvisorDao();

            try
            {
                dsCustList = advisorDao.GetAssociateCustomerList(adviserId, UserRole, agentCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAssociateCustomerList(int adviserId, string UserRole, string agentCode)");
                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = UserRole;
                objects[2] = agentCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCustList;
        }

        public AdvisorVo GetAssociateAdviserUser(int userId)
        {
            AdvisorVo advisorVo = null;
            AdvisorDao advisorDao = new AdvisorDao();
            try
            {
                advisorVo = advisorDao.GetAssociateAdviserUser(userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAssociateAdviserUser()");
                object[] objects = new object[1];
                objects[0] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return advisorVo;
        }

        public DataSet GetAdviserRoleTreeNodes(int adviserId)
        {
            AdvisorDao advisorDao = new AdvisorDao();
            DataSet dsAdviserTreeNodes;

            try
            {
                dsAdviserTreeNodes = advisorDao.GetAdviserRoleTreeNodes(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GetAdviserRoleTreeNodes(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserTreeNodes;
        }
        public DataSet BindArea(int adviserId)
        {
            AdvisorDao advisorDao = new AdvisorDao();
            DataSet dsArea;

            try
            {
                dsArea = advisorDao.BindArea(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BindArea(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsArea;
        }
    }
}
