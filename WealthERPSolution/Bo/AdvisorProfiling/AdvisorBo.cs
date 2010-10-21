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
        public List<CustomerVo> GetAdviserCustomerList(int adviserId, int CurrentPage, out int Count, string SortExpression, string NameFilter, string AreaFilter, string PincodeFilter, string ParentFilter, string RMFilter, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictRM, out Dictionary<string, string> genDictReassignRM)
        {
            List<CustomerVo> customerList = null;
            AdvisorDao advisorDao = new AdvisorDao();

            genDictParent = new Dictionary<string, string>();
            genDictRM = new Dictionary<string, string>();

            Count = 0;

            try
            {
                customerList = advisorDao.GetAdviserCustomerList(adviserId, CurrentPage, out Count, SortExpression, NameFilter, AreaFilter, PincodeFilter, ParentFilter, RMFilter, out genDictParent, out genDictRM,out genDictReassignRM);
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
        public List<CustomerVo> GetAdviserCustomersForSMS(int adviserId, string namefilter)
        {
            List<CustomerVo> customerList = new List<CustomerVo>();
            
            AdvisorDao adviserDao = new AdvisorDao();
            try
            {
                customerList = adviserDao.GetAdviserCustomersForSMS(adviserId, namefilter);
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


                object[] objects = new object[2];
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
    }
}