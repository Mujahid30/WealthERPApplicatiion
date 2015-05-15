using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.Sql;
using VoOnlineOrderManagemnet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using DaoOnlineOrderManagement;
using System.IO;
using System.Net;
using System.Configuration;

namespace BoOnlineOrderManagement
{
    public class OnlineOrderBo
    {
        public bool DebitRMSUserAccountBalance(string userAccountId, double amount, int orderId)
        {
            bool result = false; 
            string Response = string.Empty;
            OnlineOrderDao onlineOrderDao = new OnlineOrderDao();
            string rmsAPI = ConfigurationSettings.AppSettings["RMS_USER_ACCOUNT_BALANCE_API"];

            try
            {
                rmsAPI = rmsAPI.Replace("#UserAccountId#", userAccountId);
                rmsAPI = rmsAPI.Replace("#Amount#", amount.ToString());
                onlineOrderDao.UpdateOrderRMSAccountDebitRequestTime(orderId, Convert.ToDecimal(amount));
                Response = TrigerAPI(rmsAPI);
                if (Response.Contains("TRUE"))
                {
                    onlineOrderDao.UpdateOrderRMSAccountDebitDetails(orderId, 1, string.Empty, "RMSREsponse:-" + Response);
                    result = true;

                }               
                else if (Response.Contains("FALSE"))
                {
                    onlineOrderDao.UpdateOrderRMSAccountDebitDetails(orderId, 0, string.Empty, "RMSREsponse:-" + Response);

                }
                else if (!Response.Contains("TRUE") || !Response.Contains("FALSE"))
                {
                    onlineOrderDao.UpdateOrderRMSAccountDebitDetails(orderId, 2, string.Empty, "RMSREsponse:-" + Response);

                }

            }
            catch (Exception Ex)
            {
                onlineOrderDao.UpdateOrderRMSAccountDebitDetails(orderId, 0, string.Empty, ("RMSREsponse:-" + Response + "ERROR:-" + Ex.Message));

            }
            return result;

        }

        public static string TrigerAPI(string URL)
        {
            string Response = "";
            WebRequest WR = (WebRequest)HttpWebRequest.Create(URL);
            Response = new StreamReader(WR.GetResponse().GetResponseStream()).ReadToEnd();
            return Response;
        }

        public decimal GetUserRMSAccountBalance(string userAccountId)
        {
            decimal accountBalance = 0;
            string response = string.Empty;
            string rmsAPI = ConfigurationSettings.AppSettings["RMS_USER_ACCOUNT_BALANCE_CHECK_API"];
            string[] resultAPI = new string[5];

            try
            {
                rmsAPI = rmsAPI.Replace("#UserAccountId#", userAccountId);
                response = TrigerAPI(rmsAPI);
                if (!string.IsNullOrEmpty(response))
                {
                    resultAPI = response.Split('|');
                    accountBalance = Convert.ToDecimal(resultAPI[1].ToString());
                }

            }
            catch (Exception Ex)
            {


            }
            return Math.Round(accountBalance, 2);

        }

        //public string CreateClientMFAccessMessage(string accessCode)
        //{
        //    string message = string.Empty;
        //    switch (accessCode)
        //    {
        //        case "NA":
        //            message = "KRA not completed / updated. Hence cannot invest in mutual fund. Please contact SSL customer care";
        //            break;
        //        case "PA":
        //            message = "KRA not completed / updated. Hence cannot invest in mutual fund. Please contact SSL customer care";
        //            break;
        //    }
        //    return message;
        //}

        public string GetClientMFAccessStatus(int customerId)
        {
            string strClientAccess = "NA";
            DataTable dtClientKYCStatus = new DataTable();
            OnlineOrderDao onlineOrderDao = new OnlineOrderDao();
            try
            {
                dtClientKYCStatus = onlineOrderDao.GetClientKYCStatus(customerId);

                DataRow[] drKYCYes = dtClientKYCStatus.Select("C_IsKYCAvailable=1", "C_IsKYCAvailable");
                DataRow[] drKYCNo = dtClientKYCStatus.Select("C_IsKYCAvailable=0", "C_IsKYCAvailable");
                if (drKYCYes.Count() == dtClientKYCStatus.Rows.Count)
                    strClientAccess = "FA";
                else if (drKYCNo.Count() >= 1)
                    strClientAccess = "PA";
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OnlineOrderBo.cs:GetClientMFAccessStatus()");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return strClientAccess;
        }
        public string GetOnlineOrderUserMessage(string messageType)
        {
            string message = string.Empty;
            switch (messageType)
            {
                case "EUIN":
                    message = "I/We here by confirm that this is an execution-only transaction without any interaction or advice by the employee/relationship manager/sales person of the above distributor or notwithstanding the advice of in-appropriateness, if any, provided by the employee/relationship manager/sales person of the distributor and the distributor has not charged any advisory fees on this transaction";
                    break;
                case "PA":
                    message = "KRA not completed / updated. Hence cannot invest in mutual fund. Please contact SSL customer care";
                    break;
                case "NA":
                    message = "KRA not completed / updated. Hence cannot invest in mutual fund. Please contact SSL customer care";
                    break;
            }
            return message;
        }
        public DataTable GetImageListForBanner(string assetGroupCode)
        {


            OnlineOrderDao onlineOrderDao = new OnlineOrderDao();
            DataTable dt;
            try
            {
                dt = onlineOrderDao.GetImageListForBanner(assetGroupCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
    }
}
