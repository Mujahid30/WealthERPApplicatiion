using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUser;
using BoValuation;
using BoWerpAdmin;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace WERP_DAILY_MF_TRANSACTION_BALANCE_VALUATION
{
    public class MFTransactionBalanceProcessBo
    {
        AdviserMaintenanceBo adviserMaintenanceBo = new AdviserMaintenanceBo();
        List<AdvisorVo> adviserVoList = new List<AdvisorVo>();
        BoValuation.MFEngineBo.ValuationLabel valuationFor = BoValuation.MFEngineBo.ValuationLabel.Advisor;
        MFEngineBo mfEngineBo = new MFEngineBo();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        
        public void CreateMFTransactionBalanceForAllAdviser()
        {
            adviserVoList = adviserMaintenanceBo.GetAdviserList();
            for (int i = 0; i < adviserVoList.Count; i++)
            {              
                
                try
                {
                    mfEngineBo.MFBalanceCreation(adviserVoList[i].advisorId, 0, valuationFor);
                   
                }
                catch
                {


                }

            }
        }

    }
}
