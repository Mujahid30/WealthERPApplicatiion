using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerGoalProfiling
{
    public class GoalProfileSetupVo
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


        private int CA_AssociationId;
        private int CA_CustomerId;
        private int CA_AssociateCustomerId;
        private string CA_RelationshipCode;

        private string G_GoalName;
        private string G_ChildName;
        private int G_GoalId;

        private double G_CurrentGoalValue;
        private double G_GoalCompletionPercent;
        private bool G_IsFundFromAsset;
        private double G_CorpsToBeLeftBehind;

        public double CorpsToBeLeftBehind
        {
            get { return G_CorpsToBeLeftBehind; }
            set { G_CorpsToBeLeftBehind = value; }
        }

        public bool IsFundFromAsset
        {
            get { return G_IsFundFromAsset; }
            set { G_IsFundFromAsset = value; }
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

        public double GoalCompletionPercent
        {
            get { return G_GoalCompletionPercent; }
            set { G_GoalCompletionPercent = value; }
        }

        public double CurrentGoalValue
        {
            get { return G_CurrentGoalValue; }
            set { G_CurrentGoalValue = value; }
        }
        
       
    }
}
