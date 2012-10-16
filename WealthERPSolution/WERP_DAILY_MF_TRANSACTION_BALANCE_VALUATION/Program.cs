using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WERP_DAILY_MF_TRANSACTION_BALANCE_VALUATION
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int isForAllAdviser = int.Parse(ConfigurationSettings.AppSettings["IS_FOR_ALL_Adviser"].ToString());
            int adviserId = int.Parse(ConfigurationSettings.AppSettings["Adviser_Id"].ToString());

            ProcessMFTransactionBalance(isForAllAdviser, adviserId);

        }
        public static void ProcessMFTransactionBalance(int isForAllAdviser,int adviserId)
        {
            MFTransactionBalanceProcessBo MFBalance = new MFTransactionBalanceProcessBo();
            MFBalance.CreateMFTransactionBalanceForAllAdviser(isForAllAdviser, adviserId);
        }

    }
}
