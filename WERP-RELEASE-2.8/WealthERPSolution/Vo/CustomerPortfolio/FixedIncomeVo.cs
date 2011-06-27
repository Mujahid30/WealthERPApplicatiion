using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class FixedIncomeVo
    {
        #region Fields

        private int m_FITransactionId;
        private int m_CustomerId;
        private int m_AccountId;
        private string m_AssetInstrumentCategoryCode;
        private string m_AssetInstrumentCategoryName;
        private string m_AssetGroupCode;
        private string m_DebtIssuerCode;
        private string m_InterestBasisCode;
        private string m_CompoundInterestFrequencyCode;
        private string m_InterestPayableFrequencyCode;
        private string m_Name;
        private int m_IssueDate;
        private double m_PrinciaplAmount;
        private double m_InterestAmtPaidOut;
        private double m_InterestAmtAccumulated;
        private float m_InterestRate;
        private double m_FaceValue;
        private float m_MaturityFaceValue;
        private double m_PurchasePrice;
        private double m_SubsequentDepositAmount;
        private string m_DepositFrequencyCode;
        private int m_DebentureNum;
        private float m_PurchaseValue;
        private DateTime m_PurchaseDate;
        private DateTime m_MaturityDate;
        private float m_MaturityValue;
        private int m_IsInterestAccumulated;
        private double m_CurrentPrice;
        private double m_CurrentValue;
        private float m_Quantity;
        private string m_Remark;

                
        #endregion Fields


        #region Properties

        public int FITransactionId
        {
            get { return m_FITransactionId; }
            set { m_FITransactionId = value; }
        }
        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
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
        public string InterestPayableFrequencyCode
        {
            get { return m_InterestPayableFrequencyCode; }
            set { m_InterestPayableFrequencyCode = value; }
        }
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public int IssueDate
        {
            get { return m_IssueDate; }
            set { m_IssueDate = value; }
        }
        public double PrinciaplAmount
        {
            get { return m_PrinciaplAmount; }
            set { m_PrinciaplAmount = value; }
        }
        public double InterestAmtPaidOut
        {
            get { return m_InterestAmtPaidOut; }
            set { m_InterestAmtPaidOut = value; }
        }
        public double InterestAmtAccumulated
        {
            get { return m_InterestAmtAccumulated; }
            set { m_InterestAmtAccumulated = value; }
        }
        public float InterestRate
        {
            get { return m_InterestRate; }
            set { m_InterestRate = value; }
        }

        public double FaceValue
        {
            get { return m_FaceValue; }
            set { m_FaceValue = value; }
        }

        public float MaturityFaceValue
        {
            get { return m_MaturityFaceValue; }
            set { m_MaturityFaceValue = value; }
        }
        public double PurchasePrice
        {
            get { return m_PurchasePrice; }
            set { m_PurchasePrice = value; }
        }

        public double SubsequentDepositAmount
        {
            get { return m_SubsequentDepositAmount; }
            set { m_SubsequentDepositAmount = value; }
        }

        public string DepositFrequencyCode
        {
            get { return m_DepositFrequencyCode; }
            set { m_DepositFrequencyCode = value; }
        }

        public int DebentureNum
        {
            get { return m_DebentureNum; }
            set { m_DebentureNum = value; }
        }
        public float PurchaseValue
        {
            get { return m_PurchaseValue; }
            set { m_PurchaseValue = value; }
        }
        public DateTime PurchaseDate
        {
            get { return m_PurchaseDate; }
            set { m_PurchaseDate = value; }
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
        public int IsInterestAccumulated
        {
            get { return m_IsInterestAccumulated; }
            set { m_IsInterestAccumulated = value; }
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
        public float Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }

        public string Remark
        {
            get { return m_Remark; }
            set { m_Remark = value; }
        }
        #endregion Properties
    }
}
