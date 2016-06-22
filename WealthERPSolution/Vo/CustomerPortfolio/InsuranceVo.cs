using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class InsuranceVo
    {
        #region Fields
        //private int m_PortfolioId;
        private int m_CustInsInvId;
        private string m_AssetInstrumentCategoryCode;
        private string m_AssetInstrumentCategoryName;      
        private string m_AssetGroupCode;
        private int m_AccountId;
        private int m_ModifiedBy;
        private int m_CreatedBy;
        private float m_InsuranceCharges;
        private string m_ULIPPlans;
        private DateTime m_EndDate;
        private double m_MaturityValue;
        private double m_SurrenderValue;
        private double m_BonusAccumalated;
        private double m_PremiumAccumalated;
        private float m_PeriodLapsed;
        private DateTime m_StartDate;
        private Int16 m_PremiumPaymentDate;        
        private double m_SumAssured;
        private float m_PremiumDuration;
        private double m_PremiumAmount;
        private string m_PolicyNumber;
        private string m_Name;
        private string m_PremiumFrequencyCode;
        private string m_InsuranceIssuerCode;
        private float m_GracePeriod;
        private string m_Remarks;
        private float m_PolicyEpisode;               
        private float m_PolicyPeriod;
        private string m_ApplicationNumber;       
        private DateTime m_ApplicationDate;
        private DateTime m_FirstPremiumDate;
        private DateTime m_LastPremiumDate;
        private string m_Frequency;

        private int m_SchemeId;
        private DateTime m_PurchaseDate;

        private float m_NAV;
        private float m_OtherULIPCharges;
        private float m_MortalityCharges;

        private int m_PolicyTerms;
        private string m_PolicyTermsDuration;
        private string m_InsuranceIssuerName;

        public string BankBranch { get; set; }
        public DateTime PaymentInstrumentDate { get; set; }
        public string BankName { get; set; }
        public string PaymentInstrumentNumber { get; set; }
        public string ModeOfPayment { get; set; }
        public float Amount { get; set; }
        public int bankcode { get; set; }
        #endregion Fields


        #region Properties
        public string AssetInstrumentCategoryName
        {
            get { return m_AssetInstrumentCategoryName; }
            set { m_AssetInstrumentCategoryName = value; }
        }

        public string Frequency
        {
            get { return m_Frequency; }
            set { m_Frequency = value; }
        }

        public DateTime ApplicationDate
        {
            get { return m_ApplicationDate; }
            set { m_ApplicationDate = value; }
        }
        public string ApplicationNumber
        {
            get { return m_ApplicationNumber; }
            set { m_ApplicationNumber = value; }
        }

        public float PolicyEpisode
        {
            get { return m_PolicyEpisode; }
            set { m_PolicyEpisode = value; }
        }
        public float PolicyPeriod
        {
            get { return m_PolicyPeriod; }
            set { m_PolicyPeriod = value; }
        }
        
        public string Remarks
        {
            get { return m_Remarks; }
            set { m_Remarks = value; }
        }
        public float GracePeriod
        {
            get { return m_GracePeriod; }
            set { m_GracePeriod = value; }
        }
        public int CustInsInvId
        {
            get { return m_CustInsInvId; }
            set { m_CustInsInvId = value; }
        }
       

        public string AssetInstrumentCategoryCode
        {
            get { return m_AssetInstrumentCategoryCode; }
            set { m_AssetInstrumentCategoryCode = value; }
        }
       
        public string AssetGroupCode
        {
            get { return m_AssetGroupCode; }
            set { m_AssetGroupCode = value; }
        }

        public Int16 PremiumPaymentDate
        {
            get { return m_PremiumPaymentDate; }
            set { m_PremiumPaymentDate = value; }
        }
        public int AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }
     

        //public int PortfolioId
        //{
        //    get { return m_PortfolioId; }
        //    set { m_PortfolioId = value; }
        //}
       

        public string InsuranceIssuerCode
        {
            get { return m_InsuranceIssuerCode; }
            set { m_InsuranceIssuerCode = value; }
        }
    

        public string PremiumFrequencyCode
        {
            get { return m_PremiumFrequencyCode; }
            set { m_PremiumFrequencyCode = value; }
        }
      
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
       


        public string PolicyNumber
        {
            get { return m_PolicyNumber; }
            set { m_PolicyNumber = value; }
        }


        public double PremiumAmount
        {
            get { return m_PremiumAmount; }
            set { m_PremiumAmount = value; }
        }
    

        public float PremiumDuration
        {
            get { return m_PremiumDuration; }
            set { m_PremiumDuration = value; }
        }
        

        public double SumAssured
        {
            get { return m_SumAssured; }
            set { m_SumAssured = value; }
        }
   

        public DateTime StartDate
        {
            get { return m_StartDate; }
            set { m_StartDate = value; }
        }
       

        public float PeriodLapsed
        {
            get { return m_PeriodLapsed; }
            set { m_PeriodLapsed = value; }
        }


        public double PremiumAccumalated
        {
            get { return m_PremiumAccumalated; }
            set { m_PremiumAccumalated = value; }
        }


        public double BonusAccumalated
        {
            get { return m_BonusAccumalated; }
            set { m_BonusAccumalated = value; }
        }


        public double SurrenderValue
        {
            get { return m_SurrenderValue; }
            set { m_SurrenderValue = value; }
        }


        public double MaturityValue
        {
            get { return m_MaturityValue; }
            set { m_MaturityValue = value; }
        }
       

        public DateTime EndDate
        {
            get { return m_EndDate; }
            set { m_EndDate = value; }
        }
      

        public string ULIPPlans
        {
            get { return m_ULIPPlans; }
            set { m_ULIPPlans = value; }
        }


        public float InsuranceCharges
        {
            get { return m_InsuranceCharges; }
            set { m_InsuranceCharges = value; }
        }
    

        public int CreatedBy
        {
            get { return m_CreatedBy; }
            set { m_CreatedBy = value; }
        }
     
        public int ModifiedBy
        {
            get { return m_ModifiedBy; }
            set { m_ModifiedBy = value; }
        }

        public DateTime FirstPremiumDate
        {
            get { return m_FirstPremiumDate; }
            set { m_FirstPremiumDate = value; }
        }


        public DateTime LastPremiumDate
        {
            get { return m_LastPremiumDate; }
            set { m_LastPremiumDate = value; }
        }

        public int SchemeId
        {
            get { return m_SchemeId; }
            set { m_SchemeId = value; }
        }

        public DateTime PurchaseDate
        {
            get { return m_PurchaseDate; }
            set { m_PurchaseDate = value; }
        }

        public float NAV
        {
            get { return m_NAV; }
            set { m_NAV = value; }
        }

        public float OtherULIPCharges
        {
            get { return m_OtherULIPCharges; }
            set { m_OtherULIPCharges = value; }
        }

        public float MortalityCharges
        {
            get { return m_MortalityCharges; }
            set { m_MortalityCharges = value; }
        }

        public int PolicyTerms
        {
            get { return m_PolicyTerms; }
            set { m_PolicyTerms = value; }
        }

        public string PolicyTermsDuration
        {
            get { return m_PolicyTermsDuration; }
            set { m_PolicyTermsDuration = value; }
        }

        public string InsuranceIssuerName
        {
            get { return m_InsuranceIssuerName; }
            set { m_InsuranceIssuerName = value; }
        }
        
        #endregion Properties
    }
    public class InsuranceIssueVO
    {
      public  string insuranceType { get; set; }
      public string issureCode { get; set; }
      public string category { get; set; }
      public string subCategory { get; set; }
      public string policyName { get; set; }
      public string remarks { get; set; }
      public DateTime launchDate { get; set; }
      public DateTime closeDate { get; set; }
      public int active { get; set; }

    }
}
