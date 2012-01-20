using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoFPSuperlite
{
    public class CustomerGoalPlanningVo
    {
        
        private int G_CustomerGoalId;
        private int G_CustId;
        private string G_Goalcode;
        private double G_CostOfGoalToday;
        private int G_GoalYear;
        private DateTime G_GoalDate;
        private DateTime G_GoalProfileDate;
        private double G_MonthlySavingsReq;
        private double G_RetirementCorpus;
        private int G_AssociateId;
        private double G_ROIEarned;
        private double G_CurrInvestementForGoal;
        private double G_ExpectedROI;
        private int G_IsActice;
        private double G_InflationPercent;
        private DateTime G_CustomerApprovedOn;
        private string G_Comments;

        private string G_GoalDescription;
        private double G_RateofInterestOnFture;
        private int G_CreatedBy;
        private double G_lumpsumInvestRequired;
        private double G_FutureValueOnCurrentInvest;
        private double G_FutureValueOfCostToday;
        private string G_Frequency;        
        private string G_Priority;

      
        private int CA_AssociationId;
        private int CA_CustomerId;
       
        private string CA_RelationshipCode;

        private string G_GoalName;
        private string G_ChildName;
        private int G_GoalId;
        private bool G_IsOnetimeOccurence;
        private double G_CorpusLeftBehind;
        private bool G_CorpusToBeLeftBehind;
        private string G_FPCalculationBasis;
        private int G_CustomerAge;

        private bool G_IsFundFromAsset;

        public bool IsFundFromAsset
        {
            get { return G_IsFundFromAsset; }
            set { G_IsFundFromAsset = value; }
        }
        public int CustomerAge
        {
            get { return G_CustomerAge; }
            set { G_CustomerAge = value; }
        }

        public string FPCalculationBasis
        {
            get { return G_FPCalculationBasis; }
            set { G_FPCalculationBasis = value; }
        }

        public bool CorpusToBeLeftBehind
        {
            get { return G_CorpusToBeLeftBehind; }
            set { G_CorpusToBeLeftBehind = value; }
        }
       

        public double CorpusLeftBehind
        {
            get { return G_CorpusLeftBehind; }
            set { G_CorpusLeftBehind = value; }
        }
        
        public bool IsOnetimeOccurence
        {
            get { return G_IsOnetimeOccurence; }
            set { G_IsOnetimeOccurence = value; }
        }


        public string Priority
        {
            get { return G_Priority; }
            set { G_Priority = value; }
        }
        public string Frequency
        {
            get { return G_Frequency; }
            set { G_Frequency = value; }
        }
        
        public int CustomerGoalId
        {
            get { return G_CustomerGoalId; }
            set { G_CustomerGoalId = value; }
        }
        public int CustId
        {
            get { return G_CustId; }
            set { G_CustId = value; }
        }
        public string Goalcode
        {
            get { return G_Goalcode; }
            set { G_Goalcode = value; }
        }
        public double CostOfGoalToday
        {
            get { return G_CostOfGoalToday; }
            set { G_CostOfGoalToday = value; }
        }
        public int GoalYear
        {
            get { return G_GoalYear; }
            set { G_GoalYear = value; }
        }
        public DateTime GoalDate
        {
            get { return G_GoalDate; }
            set { G_GoalDate = value; }
        }

        public DateTime GoalProfileDate
        {
            get { return G_GoalProfileDate; }
            set { G_GoalProfileDate = value; }
        }
        public double MonthlySavingsReq
        {
            get { return G_MonthlySavingsReq; }
            set { G_MonthlySavingsReq = value; }
        }
        public double RetirementCorpus
        {
            get { return G_RetirementCorpus; }
            set { G_RetirementCorpus = value; }
        }

        public int AssociateId
        {
            get { return G_AssociateId; }
            set { G_AssociateId = value; }
        }
        public double ROIEarned
        {
            get { return G_ROIEarned; }
            set { G_ROIEarned = value; }
        }
        public double CurrInvestementForGoal
        {
            get { return G_CurrInvestementForGoal; }
            set { G_CurrInvestementForGoal = value; }
        }
        public double ExpectedROI
        {
            get { return G_ExpectedROI; }
            set { G_ExpectedROI = value; }
        }
        public int IsActice
        {
            get { return G_IsActice; }
            set { G_IsActice = value; }
        }
        public double InflationPercent
        {
            get { return G_InflationPercent; }
            set { G_InflationPercent = value; }
        }
        public DateTime CustomerApprovedOn
        {
            get { return G_CustomerApprovedOn; }
            set { G_CustomerApprovedOn = value; }
        }
        public string Comments
        {
            get { return G_Comments; }
            set { G_Comments = value; }
        }
        public string GoalDescription
        {
            get { return G_GoalDescription; }
            set { G_GoalDescription = value; }
        }
        public double RateofInterestOnFture
        {
            get { return G_RateofInterestOnFture; }
            set { G_RateofInterestOnFture = value; }
        }
        public int CreatedBy
        {
            get { return G_CreatedBy; }
            set { G_CreatedBy = value; }
        }






        public int AssociationId
        {
            get { return CA_AssociationId; }
            set { CA_AssociationId = value; }
        }
        public int CustomerId
        {
            get { return CA_CustomerId; }
            set { CA_CustomerId = value; }
        }
        public string AssociateCustomerId
        {
            get { return CA_RelationshipCode; }
            set { CA_RelationshipCode = value; }
        }

        public string GoalName
        {
            get { return G_GoalName; }
            set { G_GoalName = value; }
        }
        public string ChildName
        {
            get { return G_ChildName; }
            set { G_ChildName = value; }
        }
        public int GoalId
        {
            get { return G_GoalId; }
            set { G_GoalId = value; }
        }

        public double LumpsumInvestRequired
        {
            get { return G_lumpsumInvestRequired; }
            set { G_lumpsumInvestRequired = value; }
        }

        public double FutureValueOnCurrentInvest
        {
            get { return G_FutureValueOnCurrentInvest; }
            set { G_FutureValueOnCurrentInvest = value; }
        }

        public double FutureValueOfCostToday
        {
            get { return G_FutureValueOfCostToday; }
            set { G_FutureValueOfCostToday = value; }
        }


        
    }

    public class CustomerAssumptionVo
    {
        private double A_InflationPercent;
        private double A_ReturnOnEquity;

        public double ReturnOnEquity
        {
            get { return A_ReturnOnEquity; }
            set { A_ReturnOnEquity= value; }
        }
        private double A_ReturnOnDebt;

        public double ReturnOnDebt
        {
            get { return A_ReturnOnDebt; }
            set { A_ReturnOnDebt = value; }
        }

        private double A_ReturnOnCash;

        public double ReturnOnCash
        {
            get { return A_ReturnOnCash; }
            set { A_ReturnOnCash = value; }
        }

        private double A_ReturnOnAlternate;

        public double ReturnOnAlternate
        {
            get { return A_ReturnOnAlternate; }
            set { A_ReturnOnAlternate = value; }
        } 

        public double InflationPercent
        {
            get { return A_InflationPercent; }
            set { A_InflationPercent = value; }
        }
        private int A_CustomerAge;

        public int CustomerAge
        {
            get { return A_CustomerAge; }
            set { A_CustomerAge = value; }
        }

        private int A_SpouseAge;

        public int SpouseAge
        {
            get { return A_SpouseAge; }
            set { A_SpouseAge = value; }
        }

        private int A_RetirementAge;

        public int RetirementAge
        {
            get { return A_RetirementAge; }
            set { A_RetirementAge = value; }
        }

        private double A_PostRetirementReturn;

        public double PostRetirementReturn
        {
            get { return A_PostRetirementReturn; }
            set { A_PostRetirementReturn = value; }
        }

        private double A_ReturnOnNewInvestment;

        public double ReturnOnNewInvestment
        {
            get { return A_ReturnOnNewInvestment; }
            set { A_ReturnOnNewInvestment = value; }
        }

        private double A_WeightedReturn;

        public double WeightedReturn
        {
            get { return A_WeightedReturn; }
            set { A_WeightedReturn = value; }
        }

        private int A_CustomerEOL;

        public int CustomerEOL
        {
            get { return A_CustomerEOL; }
            set { A_CustomerEOL = value; }
        }

        private int A_SpouseEOL;

        public int SpouseEOL
        {
            get { return A_SpouseEOL; }
            set { A_SpouseEOL = value; }
        }

        private double A_CorpusToBeLeftBehind;

        public double CorpusToBeLeftBehind
        {
            get { return A_CorpusToBeLeftBehind; }
            set { A_CorpusToBeLeftBehind = value; }
        }

        private int A_RTGoalYear;

        public int RTGoalYear
        {
            get { return A_RTGoalYear; }
            set { A_RTGoalYear = value; }
        }

        private bool C_IsModelPortfolio;

        public bool IsModelPortfolio
        {
            get { return C_IsModelPortfolio; }
            set { C_IsModelPortfolio = value; }
        }

        private bool C_IsGoalFundingFromInvestMapping;

        public bool IsGoalFundingFromInvestMapping
        {
            get { return C_IsGoalFundingFromInvestMapping; }
            set { C_IsGoalFundingFromInvestMapping = value; }
        }

        private bool C_IsCorpusToBeLeftBehind;

        public bool IsCorpusToBeLeftBehind
        {
            get { return C_IsCorpusToBeLeftBehind; }
            set { C_IsCorpusToBeLeftBehind = value; }
        }

        private bool C_IsRiskProfileComplete;

        public bool IsRiskProfileComplete
        {
            get { return C_IsRiskProfileComplete; }
            set { C_IsRiskProfileComplete = value; }
        }

    }


    public class CustomerGoalFundingProgressVo
    {
        private double GFP_MonthlyContribution;
        private double GFP_AmountInvestedTillDate;
        private double GFP_GoalCurrentValue;
        private decimal GFP_ReturnsXIRR;
        private string GFP_EstimatedTimeToAchiveGoal;
        private double GFP_ProjectedValue;
        private double GFP_ProjectedGapValue;
        private double GFP_AdditionalMonthlyRequirement;
        private double GFP_AdditionalYearlyRequirement;
        private double GFP_WeightedReturn;
        private double GFP_ProjectedEndYear;

        public double ProjectedEndYear
        {
            get { return GFP_ProjectedEndYear; }
            set { GFP_ProjectedEndYear = value; }
        }

        public double WeightedReturn
        {
            get { return GFP_WeightedReturn; }
            set { GFP_WeightedReturn = value; }
        }

        public double AdditionalMonthlyRequirement
        {
            get { return GFP_AdditionalMonthlyRequirement; }
            set { GFP_AdditionalMonthlyRequirement = value; }
        }

        public double AdditionalYearlyRequirement
        {
            get { return GFP_AdditionalYearlyRequirement; }
            set { GFP_AdditionalYearlyRequirement = value; }
        }
        public double ProjectedGapValue
        {
            get { return GFP_ProjectedGapValue; }
            set { GFP_ProjectedGapValue = value; }
        }

        public double ProjectedValue
        {
            get { return GFP_ProjectedValue; }
            set { GFP_ProjectedValue = value; }
        }

        public string GEstimatedTimeToAchiveGoal
        {
            get { return GFP_EstimatedTimeToAchiveGoal; }
            set { GFP_EstimatedTimeToAchiveGoal = value; }
        }


        public decimal ReturnsXIRR
        {
            get { return GFP_ReturnsXIRR; }
            set { GFP_ReturnsXIRR = value; }
        }


        public double GoalCurrentValue
        {
            get { return GFP_GoalCurrentValue; }
            set { GFP_GoalCurrentValue = value; }
        }
        
        public double AmountInvestedTillDate
        {
            get { return GFP_AmountInvestedTillDate; }
            set { GFP_AmountInvestedTillDate = value; }
        }

        
        public double MonthlyContribution
        {
            get { return GFP_MonthlyContribution; }
            set { GFP_MonthlyContribution = value; }
        }
 
    }


  
}
