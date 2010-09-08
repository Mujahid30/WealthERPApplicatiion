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
        public string getId()
        {
            Guid id;
            id = Guid.NewGuid();
            return id.ToString();
        }
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
    }
}