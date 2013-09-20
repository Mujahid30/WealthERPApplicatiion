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
            //int isForAllAdviser = int.Parse(ConfigurationSettings.AppSettings["IS_FOR_ALL_Adviser"].ToString());
            //int adviserId = int.Parse(ConfigurationSettings.AppSettings["Adviser_Id"].ToString());
            int commonId1 = int.Parse(ConfigurationSettings.AppSettings["CommonId1"].ToString());
            int commonId2 = int.Parse(ConfigurationSettings.AppSettings["CommonId2"].ToString());
            int valuationLevelId = int.Parse(ConfigurationSettings.AppSettings["ValuationLevelId"].ToString());
            TrigerBalanceValuationFunction(commonId1,commonId2, valuationLevelId);
        }

        public static void TrigerBalanceValuationFunction(int commonId1,int commonId2, int valuationLevelId)
        {
            MFTransactionBalanceProcessBo balanceProcessBo=new MFTransactionBalanceProcessBo();
            switch (valuationLevelId)
            {
                case 1:
                    {
                        
                        balanceProcessBo.ProcessMFTransactionBalance(commonId1, commonId2, MFTransactionBalanceProcessBo.ValuationLevel.AllAdviser);
                        break;
                    }
                case 2:
                    {
                        balanceProcessBo.ProcessMFTransactionBalance(commonId1, commonId2, MFTransactionBalanceProcessBo.ValuationLevel.Adviser);
                        break;
                    }
                case 3:
                    {
                        balanceProcessBo.ProcessMFTransactionBalance(commonId1, commonId2, MFTransactionBalanceProcessBo.ValuationLevel.Customer);
                        break;
                    }
                case 4:
                    {
                        balanceProcessBo.ProcessMFTransactionBalance(commonId1, commonId2, MFTransactionBalanceProcessBo.ValuationLevel.Account);
                        break;
                    }

            }

        }
       

    }
}
