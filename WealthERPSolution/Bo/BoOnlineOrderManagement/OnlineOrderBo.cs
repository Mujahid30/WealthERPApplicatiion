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
                onlineOrderDao.UpdateOrderRMSAccountDebitRequestTime(orderId);
                Response = TrigerAPI(rmsAPI);
                if (Response.Contains("TRUE"))
                {
                    onlineOrderDao.UpdateOrderRMSAccountDebitDetails(orderId, 1, string.Empty, "RMSREsponse:-" + Response);
                    result = true;

                }
                else if (Response.Contains("MSG_FAILURE"))
                {
                    onlineOrderDao.UpdateOrderRMSAccountDebitDetails(orderId, 0, string.Empty, "RMSREsponse:-" + Response);

                }
                else if (Response.Contains("FALSE"))
                {
                    onlineOrderDao.UpdateOrderRMSAccountDebitDetails(orderId, 0, string.Empty, "RMSREsponse:-" + Response);

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
            return accountBalance;

        }
    }
}
