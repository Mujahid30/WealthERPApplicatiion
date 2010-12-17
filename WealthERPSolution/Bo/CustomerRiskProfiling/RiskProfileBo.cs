using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoCustomerRiskProfiling;
using VoUser;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using BoCustomerProfiling;

namespace BoCustomerRiskProfiling
{
    public class RiskProfileBo
    {
        RiskProfileDao riskprofiledao = new RiskProfileDao();
        public DataSet GetRiskProfileQuestion(int advisorId)
        {
            return riskprofiledao.GetRiskProfileQuestion(advisorId);
        }
        public DataSet GetQuestionOption(int questionId,int advisorId)
        {
            return riskprofiledao.GetQuestionOption(questionId, advisorId);
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
        public void AddCustomerResponseToQuestion(int RpId, int questionId, int optionId, RMVo rmvo)
        {
            riskprofiledao.AddCustomerResponseToQuestion(RpId, questionId, optionId, rmvo);

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
        public DataSet GetCustomerRiskProfile(int customerid, int advisorId)
        {
            return riskprofiledao.GetCustomerRiskProfile(customerid,advisorId);
        }
        public DataSet GetAssetAllocationDetails(int riskprofileid)
        {
            return riskprofiledao.GetAssetAllocationDetails(riskprofileid);
        }
        public void UpdateAssetAllocationDetails(int riskprofileid, double cashpercentage, double equitypercentage, double debitpercentage, DateTime clientapprovedon, RMVo rmvo)
        {
            riskprofiledao.UpdateAssetAllocationDetails(riskprofileid, cashpercentage, equitypercentage, debitpercentage, clientapprovedon, rmvo);
        }
        /// <summary>
        /// It returns RiskProfile Description Paragraph
        /// </summary>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public string GetRiskProfileText(string ClassName)
        {
            string RiskTextParagraph = "";
            if (ClassName == "Aggressive")
            {
                RiskTextParagraph = "Your risk behaviour is Aggressive." +
                    " It shows that by nature you are a risk taker." +
                    " You want to assume high risks in anticipation of " +
                    "equally high returns and at the same time you are not " +
                    "too bothered about the downside of high-risk investments.";
            }
            else if (ClassName == "Moderate")
            {
                RiskTextParagraph = "Your risk behaviour is Moderate. It shows that by nature you are a balanced " +
                "risk taker. Before investing anywhere you take in to account all the upsides and downsides " +
                "associated with it and then take a well-reasoned decision. You are bothered about the downside " +
                "of high-risk investments and therefore, want to maintain a balanced portfolio.";

            }
            else if (ClassName == "Conservative")
            {
                RiskTextParagraph = "Your risk behaviour is Conservative." +
                    " It shows that by nature you are a moderate risk taker." +
                    " You don’t want to assume high risks on investments, as " +
                    "you are afraid of booking losses. You are happy with the " +
                    "reasonable return that you may get from medium to low risk investments.";

            }

            return RiskTextParagraph;
        }
        /// <summary>
        /// It Will gives Asset allocation description paragraph of a Customer
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetAssetAllocationText(int CustomerID)
        {
            string AssetAllocationText = "";
            DataSet DSAssets = new DataSet();
            CustomerVo customerVo = new CustomerVo();
            CustomerBo customerBo=new CustomerBo();           
           customerVo = customerBo.GetCustomer(CustomerID);
            string CashLessMore = "";
            DSAssets = riskprofiledao.GetCustomerAssets(CustomerID,customerVo.IsProspect);
            try
            {
                if (DSAssets.Tables[1].Rows.Count > 0)
                {
                    if (DSAssets.Tables[0].Columns["Cash"].ToString() != "" || DSAssets.Tables[1].Columns["Cash"].ToString() != "")
                    {
                        if (double.Parse(DSAssets.Tables[0].Rows[0]["Cash"].ToString()) > double.Parse(DSAssets.Tables[1].Rows[0]["Cash"].ToString()))
                        {
                            CashLessMore = "more";
                        }
                        else
                        {
                            CashLessMore = "less";
                        }
                    }
                    //DSAssets = riskprofiledao.GetCustomerAssets(CustomerID);
                    if (double.Parse(DSAssets.Tables[0].Rows[0]["Equity"].ToString()) == double.Parse(DSAssets.Tables[1].Rows[0]["Equity"].ToString()) && double.Parse(DSAssets.Tables[0].Rows[0]["Debt"].ToString()) == double.Parse(DSAssets.Tables[1].Rows[0]["Debt"].ToString()))
                    {

                        AssetAllocationText = "Based on your current investments we have identified that your asset " +
                        "allocation matches the asset allocation recommended by us. The asset allocation recommended by " +
                        "us is based on your risk profile and other data pulled from your profile information." +
                        "Your current equity allocation is " + Math.Round(double.Parse(DSAssets.Tables[0].Rows[0]["Equity"].ToString()), 2).ToString() + " %" +
                        " and debt allocation is " + Math.Round(double.Parse(DSAssets.Tables[0].Rows[0]["Debt"].ToString()), 2).ToString() + " %" + " Based on our analysis we recommend an equity allocation of "
                        + Math.Round(double.Parse(DSAssets.Tables[1].Rows[0]["Equity"].ToString()), 2).ToString() + " %" + " and debt allocation of " + Math.Round(double.Parse(DSAssets.Tables[1].Rows[0]["Debt"].ToString()), 2) + " %" +
                        ".As per our recommendation you keep " + Math.Round(double.Parse(DSAssets.Tables[1].Rows[0]["Cash"].ToString()), 2).ToString() + " % of your investment portfolio " + " %" +
                        " in cash and cash equivalents to take care of liquidity in your portfolio." +
                        "You have an appropriate asset allocation. Please contact your advisor to help you meet your financial goals";


                    }
                    else
                    {
                        AssetAllocationText = "Based on your current investments we have identified that your asset " +
                       "allocation does not match the asset allocation recommended by us. The asset allocation recommended " +
                       "by us is based on your risk profile and other data pulled from your profile information." +
                       "Your current equity allocation is " + Math.Round(double.Parse(DSAssets.Tables[0].Rows[0]["Equity"].ToString()), 2).ToString() + " %" +
                       " and debt allocation is " + Math.Round(double.Parse(DSAssets.Tables[0].Rows[0]["Debt"].ToString()), 2).ToString() + " %" + ". But based on " +
                       " our analysis we recommend an equity allocation of " + Math.Round(double.Parse(DSAssets.Tables[1].Rows[0]["Equity"].ToString()), 2).ToString() + " %" +
                       " and debt allocation of " + Math.Round(double.Parse(DSAssets.Tables[1].Rows[0]["Debt"].ToString()), 2).ToString() + " %" + ".Please contact your advisor" +
                       " to help you shift closer to the recommended asset allocation. This will keep you in sync with your " +
                       "risk taking capacity and your risk appetite both.Moreover we recommend you keep " +
                       Math.Round(double.Parse(DSAssets.Tables[1].Rows[0]["Cash"].ToString()), 2).ToString() + " %" + " of your investment portfolio in cash and cash equivalents to " +
                       "take care of liquidity in your portfolio. Based on your current asset allocation we have identified that " +
                       "cash allocation is " + CashLessMore + " than recommended.Please contact your advisor to remove the gap.";
                    }

                }
                else
                {
                    AssetAllocationText = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return AssetAllocationText;
        }
        /// <summary>
        /// It Will give customers all Current asset allocation 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public DataSet GetCurrentAssetAllocation(int CustomerID,int isProspect)
        {
            DataSet DSCurrentAssets = new DataSet();
            DSCurrentAssets = riskprofiledao.GetCustomerAssets(CustomerID, isProspect);
            return DSCurrentAssets;

        }

    }
}
