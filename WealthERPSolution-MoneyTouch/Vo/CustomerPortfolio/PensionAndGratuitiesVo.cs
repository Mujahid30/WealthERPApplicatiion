using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class PensionAndGratuitiesVo
    {
        #region Fields
        private int m_PensionGratuitiesPortfolioId;    
        private int m_AccountId;
        private string m_AssetInstrumentCategoryCode;
        private string m_AssetInstrumentCategoryName;
        private string m_AssetGroupCode;
        private string m_DebtIssuerCode;
        private string m_FiscalYearCode;
        private string m_InterestPayableFrequencyCode;
        private string m_CompoundInterestFrequencyCode;
        private float m_InterestRate;
        private string m_OrganizationName;
        private DateTime m_PurchaseDate;
        private float m_DepositAmount;
        private DateTime m_MaturityDate;
        private float m_MaturityValue;
        private double m_CurrentValue;
        private string m_InterestBasis;
        private int m_IsInterestAccumalated;
        private float m_InterestAmtAccumalated;
        private float m_InterestAmtPaidOut;
        private DateTime m_LoanStartDate;
        private DateTime m_LoanEndDate;
        private double m_LoanOutstandingAmount;
        private string m_LoanDescription;
        private string m_Remarks;
        private float m_EmployeeContri;
        private float m_EmployerContri;

        #endregion Fields

        #region Properties

        public int PensionGratuitiesPortfolioId
        {
            get { return m_PensionGratuitiesPortfolioId; }
            set { m_PensionGratuitiesPortfolioId = value; }
        }

        public int AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }

        public string AssetInstrumentCategoryCode
        {
            get { return m_AssetInstrumentCategoryCode; }
            set { m_AssetInstrumentCategoryCode = value; }
        }

        public string AssetInstrumentCategoryName
        {
            get { return m_AssetInstrumentCategoryName; }
            set { m_AssetInstrumentCategoryName = value; }
        }

        public string AssetGroupCode
        {
            get { return m_AssetGroupCode; }
            set { m_AssetGroupCode = value; }
        }

        public string DebtIssuerCode
        {
            get { return m_DebtIssuerCode; }
            set { m_DebtIssuerCode = value; }
        }
     
        public string FiscalYearCode
        {
            get { return m_FiscalYearCode; }
            set { m_FiscalYearCode = value; }
        }

        public string InterestPayableFrequencyCode
        {
            get { return m_InterestPayableFrequencyCode; }
            set { m_InterestPayableFrequencyCode = value; }
        }

        public string CompoundInterestFrequencyCode
        {
            get { return m_CompoundInterestFrequencyCode; }
            set { m_CompoundInterestFrequencyCode = value; }
        }

        public float InterestRate
        {
            get { return m_InterestRate; }
            set { m_InterestRate = value; }
        }
       
        public string OrganizationName
        {
            get { return m_OrganizationName; }
            set { m_OrganizationName = value; }
        }
       
        public DateTime PurchaseDate
        {
            get { return m_PurchaseDate; }
            set { m_PurchaseDate = value; }
        }
       
        public float DepositAmount
        {
            get { return m_DepositAmount; }
            set { m_DepositAmount = value; }
        }
       
        public DateTime MaturityDate
        {
            get { return m_MaturityDate; }
            set { m_MaturityDate = value; }
        }
     
        public float MaturityValue
        {
            get { return m_MaturityValue; }
            set { m_MaturityValue = value; }
        }

        public double CurrentValue
        {
            get { return m_CurrentValue; }
            set { m_CurrentValue = value; }
        }

        public string InterestBasis
        {
            get { return m_InterestBasis; }
            set { m_InterestBasis = value; }
        }

        public int IsInterestAccumalated
        {
            get { return m_IsInterestAccumalated; }
            set { m_IsInterestAccumalated = value; }
        }

        public float InterestAmtAccumalated
        {
            get { return m_InterestAmtAccumalated; }
            set { m_InterestAmtAccumalated = value; }
        }
       
        public float InterestAmtPaidOut
        {
            get { return m_InterestAmtPaidOut; }
            set { m_InterestAmtPaidOut = value; }
        }
       
        public DateTime LoanStartDate
        {
            get { return m_LoanStartDate; }
            set { m_LoanStartDate = value; }
        }

        public DateTime LoanEndDate
        {
            get { return m_LoanEndDate; }
            set { m_LoanEndDate = value; }
        }

        public double LoanOutstandingAmount
        {
            get { return m_LoanOutstandingAmount; }
            set { m_LoanOutstandingAmount = value; }
        }
   
        public string LoanDescription
        {
            get { return m_LoanDescription; }
            set { m_LoanDescription = value; }
        }

        public string Remarks
        {
            get { return m_Remarks; }
            set { m_Remarks = value; }
        }

        public float EmployeeContribution
        {
            get { return m_EmployeeContri; }
            set { m_EmployeeContri = value; }
        }

        public float EmployerContribution
        {
            get { return m_EmployerContri; }
            set { m_EmployerContri = value; }
        }

        #endregion Properties

    }
}
