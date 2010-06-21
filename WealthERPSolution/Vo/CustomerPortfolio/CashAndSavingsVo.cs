using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class CashAndSavingsVo
    {
        #region Fields

        private int m_CashSavingsPortfolioId;
        private int m_CustomerId;
        private string m_AssetInstrumentCategoryCode;
        private string m_AssetInstrumentCategoryName;

      
        private string m_AssetGroupCode;
        private long m_AccountId;
        private string m_DebtIssuerCode;
        private string m_InterestBasisCode;
        private string m_CompoundInterestFrequencyCode;
        private string m_InterestPayoutFrequencyCode;
        private string m_Name;
        private double m_DepositAmount;
        private DateTime m_DepositDate;
        private double m_CurrentValue;
        private float m_InterestRate;
        private float? m_InterestAmntPaidOut;
        private Int16 m_IsInterestAccumalated;
        private float? m_InterestAmntAccumulated;
        private string m_AssetInstrumentCategory;
        private string m_AccountNumber;
        private string m_DebtIssuer;
        private string m_InterestBasis;
        private string m_Remarks;

       


        #endregion

        #region Properties

        public string AssetInstrumentCategoryName
        {
            get { return m_AssetInstrumentCategoryName; }
            set { m_AssetInstrumentCategoryName = value; }
        }
        public string Remarks
        {
            get { return m_Remarks; }
            set { m_Remarks = value; }

        }
        public int CashSavingsPortfolioId
        {
            get { return m_CashSavingsPortfolioId; }
            set { m_CashSavingsPortfolioId = value; }
        }

        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
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

        public long AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }

        public string DebtIssuerCode
        {
            get { return m_DebtIssuerCode; }
            set { m_DebtIssuerCode = value; }
        }

        public string InterestBasisCode
        {
            get { return m_InterestBasisCode; }
            set { m_InterestBasisCode = value; }
        }

        public string CompoundInterestFrequencyCode
        {
            get { return m_CompoundInterestFrequencyCode; }
            set { m_CompoundInterestFrequencyCode = value; }
        }

        public string InterestPayoutFrequencyCode
        {
            get { return m_InterestPayoutFrequencyCode; }
            set { m_InterestPayoutFrequencyCode = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public double DepositAmount
        {
            get { return m_DepositAmount; }
            set { m_DepositAmount = value; }
        }

        public DateTime DepositDate
        {
            get { return m_DepositDate; }
            set { m_DepositDate = value; }
        }

        public double CurrentValue
        {
            get { return m_CurrentValue; }
            set { m_CurrentValue = value; }
        }

        public float InterestRate
        {
            get { return m_InterestRate; }
            set { m_InterestRate = value; }
        }

        public float? InterestAmntPaidOut
        {
            get { return m_InterestAmntPaidOut; }
            set { m_InterestAmntPaidOut = value; }
        }

        public Int16 IsInterestAccumalated
        {
            get { return m_IsInterestAccumalated; }
            set { m_IsInterestAccumalated = value; }
        }

        public float? InterestAmntAccumulated
        {
            get { return m_InterestAmntAccumulated; }
            set { m_InterestAmntAccumulated = value; }
        }

        public string InstrumentCategory
        {
            get { return m_AssetInstrumentCategory; }
            set { m_AssetInstrumentCategory = value; }
        }

        public string AccountNumber
        {
            get { return m_AccountNumber; }
            set { m_AccountNumber = value; }
        }

        public string DebtIssuer
        {
            get { return m_DebtIssuer; }
            set { m_DebtIssuer = value; }
        }

        public string InterestBasis
        {
            get { return m_InterestBasis; }
            set { m_InterestBasis = value; }
        }

        #endregion

    }
}
