using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class LiabilitiesVo
    {
        #region Fields

        private int m_LiabilitiesId;
        private int m_LoanPartnerCode;
        private string m_LoanPartner;
        private int m_LoanTypeCode;
        private string m_LoanType;
        private int m_IsFloatingRateInterest;
        private int m_LoanProposalId;
        private double m_LoanAmount;
        private float m_RateOfInterest;
        private double m_EMIAmount;
        private int m_EMIDate;
        private int m_NoOfInstallments;
        private double m_AmountPrepaid;
        private string m_RepaymentTypeCode;
        private string m_FrequencyCodeEMI;
        private DateTime m_InstallmentStartDate;
        private DateTime m_InstallmentEndDate;
        private int m_IsInProcess;
        private int m_CreatedBy;
        private int m_ModifiedBy;
        private double m_CommissionAmount;
        private float m_CommissionPer;
        private string m_Guarantor;
        private int m_Tenure;


        #endregion Fields

        #region Properties
        public string LoanPartner
        {
            get { return m_LoanPartner; }
            set { m_LoanPartner = value; }
        }
        public string LoanType
        {
            get { return m_LoanType; }
            set { m_LoanType = value; }
        }
        public int LiabilitiesId
        {
            get { return m_LiabilitiesId; }
            set { m_LiabilitiesId = value; }
        }

        public int LoanPartnerCode
        {
            get { return m_LoanPartnerCode; }
            set { m_LoanPartnerCode = value; }
        }


        public int LoanTypeCode
        {
            get { return m_LoanTypeCode; }
            set { m_LoanTypeCode = value; }
        }

        public int IsFloatingRateInterest
        {
            get { return m_IsFloatingRateInterest; }
            set { m_IsFloatingRateInterest = value; }
        }


        public int LoanProposalId
        {
            get { return m_LoanProposalId; }
            set { m_LoanProposalId = value; }
        }


        public double LoanAmount
        {
            get { return m_LoanAmount; }
            set { m_LoanAmount = value; }
        }

        public float RateOfInterest
        {
            get { return m_RateOfInterest; }
            set { m_RateOfInterest = value; }
        }


        public double EMIAmount
        {
            get { return m_EMIAmount; }
            set { m_EMIAmount = value; }
        }

        public int EMIDate
        {
            get { return m_EMIDate; }
            set { m_EMIDate = value; }
        }


        public int NoOfInstallments
        {
            get { return m_NoOfInstallments; }
            set { m_NoOfInstallments = value; }
        }


        public double AmountPrepaid
        {
            get { return m_AmountPrepaid; }
            set { m_AmountPrepaid = value; }
        }


        public string RepaymentTypeCode
        {
            get { return m_RepaymentTypeCode; }
            set { m_RepaymentTypeCode = value; }
        }


        public string FrequencyCodeEMI
        {
            get { return m_FrequencyCodeEMI; }
            set { m_FrequencyCodeEMI = value; }
        }


        public DateTime InstallmentStartDate
        {
            get { return m_InstallmentStartDate; }
            set { m_InstallmentStartDate = value; }
        }


        public DateTime InstallmentEndDate
        {
            get { return m_InstallmentEndDate; }
            set { m_InstallmentEndDate = value; }
        }


        public int IsInProcess
        {
            get { return m_IsInProcess; }
            set { m_IsInProcess = value; }
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


        public double CommissionAmount
        {
            get { return m_CommissionAmount; }
            set { m_CommissionAmount = value; }
        }


        public float CommissionPer
        {
            get { return m_CommissionPer; }
            set { m_CommissionPer = value; }
        }
        public string Guarantor
        {
            get { return m_Guarantor; }
            set { m_Guarantor = value; }
        }
        public int Tenure
        {
            get { return m_Tenure; }
            set { m_Tenure = value; }
        }
        #endregion Properties


    }
}
