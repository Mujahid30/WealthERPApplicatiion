using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class GovtSavingsVo
    {
        #region Fields

        private int m_GovtSavingsPortfolioId;
        private int m_PortfolioId;
        private int m_AccountId;
        private string m_AssetInstrumentCategoryCode;

        private string m_AssetGroupCode;
        private string m_DebtIssuerCode;
        private string m_InterestBasisCode;
        private string m_InterestPayableFrequencyCode;
        private string m_CompoundInterestFrequencyCode;
        private float m_InterestRate;
        private string m_Name;
        private float m_Quantity;
        private DateTime m_PurchaseDate;
        private double m_CurrentPrice;
        private double m_CurrentValue;
        private DateTime m_MaturityDate;
        private double m_DepositAmt;
        private float m_MaturityValue;
        private int m_IsInterestAccumalated;
        private float m_InterestAmtAccumalated;
        private double m_InterestAmtPaidOut;
        private string m_Remarks;
        private string m_AssetInstrumentCategoryName;

        public double SubsqntDepositAmount{set;get;}
        public DateTime SubsqntDepositDate{set;get;}
        public string DepositFrequencyCode { set; get; }


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

        public int GoveSavingsPortfolioId
        {
            get { return m_GovtSavingsPortfolioId; }
            set { m_GovtSavingsPortfolioId = value; }
        }
        public int PortfolioId
        {
            get { return m_PortfolioId; }
            set { m_PortfolioId = value; }
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

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public float Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }
        public DateTime PurchaseDate
        {
            get { return m_PurchaseDate; }
            set { m_PurchaseDate = value; }
        }
        public double CurrentPrice
        {
            get { return m_CurrentPrice; }
            set { m_CurrentPrice = value; }
        }
        public double CurrentValue
        {
            get { return m_CurrentValue; }
            set { m_CurrentValue = value; }
        }
        public DateTime MaturityDate
        {
            get { return m_MaturityDate; }
            set { m_MaturityDate = value; }
        }
        public double DepositAmt
        {
            get { return m_DepositAmt; }
            set { m_DepositAmt = value; }
        }
        public float MaturityValue
        {
            get { return m_MaturityValue; }
            set { m_MaturityValue = value; }
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
        public double InterestAmtPaidOut
        {
            get { return m_InterestAmtPaidOut; }
            set { m_InterestAmtPaidOut = value; }
        }

        public string InterestBasisCode
        {
            get { return m_InterestBasisCode; }
            set { m_InterestBasisCode = value; }
        }
        #endregion Properties


    }
}
