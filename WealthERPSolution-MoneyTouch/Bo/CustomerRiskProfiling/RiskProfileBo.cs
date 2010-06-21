using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoCustomerRiskProfiling;
using VoUser;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace BoCustomerRiskProfiling
{
    public class RiskProfileBo
    {
        RiskProfileDao riskprofiledao = new RiskProfileDao();       
        public DataSet GetRiskProfileQuestion()
        {
            return riskprofiledao.GetRiskProfileQuestion();
        }
        public DataSet GetQuestionOption(int questionId)
        {
            return riskprofiledao.GetQuestionOption(questionId);
        }
        public DataSet GetRiskProfileRules()
        {
            return riskprofiledao.GetRiskProfileRules();
        }
        public DataSet GetCustomerDOBById(int customerId)
        {
            return riskprofiledao.GetCustomerDOBById(customerId);
        }
        public void AddCustomerRiskProfileDetails(int customerId, int crpscore, DateTime riskdate, string riskclasscode, RMVo rmvo)
        {
            riskprofiledao.AddCustomerRiskProfileDetails(customerId, crpscore, riskdate, riskclasscode, rmvo);
        }
        public void AddCustomerResponseToQuestion(int RpId,int questionId, int optionId, RMVo rmvo)
        {
            riskprofiledao.AddCustomerResponseToQuestion(RpId,questionId, optionId, rmvo);

        }
        public DataSet GetRiskClass(string RiskClassCode)
        {
            return riskprofiledao.GetRiskClass(RiskClassCode);        
        }
        public DataSet GetRpId(int customerId)
        {
            return riskprofiledao.GetRpId(customerId);
        }
        public DataSet GetAssetAllocationRules(string riskclasscode)
        {
            return riskprofiledao.GetAssetAllocationRules(riskclasscode);
        }
        public void AddAssetAllocationDetails(int riskprofileid, double cashpercentage, double equitypercentage, double debitpercentage, DateTime clientapprovedon, RMVo rmvo)
        {
           riskprofiledao.AddAssetAllocationDetails(riskprofileid, cashpercentage, equitypercentage, debitpercentage, clientapprovedon, rmvo);
        }
        public DataSet GetRiskClassForRisk(string riskclasscode)
        {
            return riskprofiledao.GetRiskClassForRisk(riskclasscode);
        }
        public DataSet GetCustomerRiskProfile(int customerid)
        {
            return riskprofiledao.GetCustomerRiskProfile(customerid);
        }
        public DataSet GetAssetAllocationDetails(int riskprofileid)
        {
            return riskprofiledao.GetAssetAllocationDetails(riskprofileid);
        }
        public void UpdateAssetAllocationDetails(int riskprofileid, double cashpercentage, double equitypercentage, double debitpercentage, DateTime clientapprovedon, RMVo rmvo)
        {
            riskprofiledao.UpdateAssetAllocationDetails(riskprofileid, cashpercentage, equitypercentage, debitpercentage, clientapprovedon, rmvo);
        }

    }
}
