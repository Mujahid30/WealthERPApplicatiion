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
        private string m_OtherLenderName;
        private string m_CompoundFrequency;
        private int m_PaymentOptionCode;
        private int m_InstallmentTypeCode;
        private double m_LumpsumRepaymentAmount;
        private double m_OutstandingAmount;
        private string m_Guarantor;
        private DateTime m_LoanStartDate;
        private int m_Tenure;
        private string m_AssetParticular;
        private int m_ISARequestId;
        private DateTime m_RequestDate;
        private string m_Status;
        private string m_Priority;
        private string m_CustomerName;
        private string m_StepCode;
        private string m_StepName;
        private string m_StatusCode;
        private string m_IsaNo;
        private DateTime m_ProcessedDate;

        public DateTime ProcessedDate
        {
            get { return m_ProcessedDate; }
            set { m_ProcessedDate = value; }
        }

        public string StatusCode
        {
            get { return m_StatusCode; }
            set { m_StatusCode = value; }
        }

        public string StepName
        {
            get { return m_StepName; }
            set { m_StepName = value; }
        }
        private string m_BranchName;
        private string m_CurrentStatus;
        #endregion Fields

        #region Properties

        public DateTime RequestDate
        {
            get { return m_RequestDate; }
            set { m_RequestDate = value; }
        }

        public string StepCode
        {
            get { return m_StepCode; }
            set { m_StepCode = value; }
        }
        public string IsaNo
        {
            get { return m_IsaNo; }
            set { m_IsaNo = value; }
        }  
        public int ISARequestId
        {
            get { return m_ISARequestId; }
            set { m_ISARequestId = value; }
        }
        public string BranchName
        {
            get { return m_BranchName; }
            set { m_BranchName = value; }
        }
        public string CustomerName
        {
            get { return m_CustomerName; }
            set { m_CustomerName = value; }
        }
        public string CurrentStatus
        {
            get { return m_CurrentStatus; }
            set { m_CurrentStatus = value; }
        }
        public string Priority
        {
            get { return m_Priority; }
            set { m_Priority = value; }
        }

        public string Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public string AssetParticular
        {
            get { return m_AssetParticular; }
            set { m_AssetParticular = value; }
        }
        
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
        public string OtherLenderName
        {
            get { return m_OtherLenderName; }
            set { m_OtherLenderName = value; }
        }


        public string CompoundFrequency
        {
            get { return m_CompoundFrequency; }
            set { m_CompoundFrequency = value; }
        }

        public int PaymentOptionCode
        {
            get { return m_PaymentOptionCode; }
            set { m_PaymentOptionCode = value; }
        }

        public int InstallmentTypeCode
        {
            get { return m_InstallmentTypeCode; }
            set { m_InstallmentTypeCode = value; }
        }

        public double LumpsumRepaymentAmount
        {
            get { return m_LumpsumRepaymentAmount; }
            set { m_LumpsumRepaymentAmount = value; }
        }

        public double OutstandingAmount
        {
            get { return m_OutstandingAmount; }
            set { m_OutstandingAmount = value; }
        }

        public string Guarantor
        {
            get { return m_Guarantor; }
            set { m_Guarantor = value; }
        }

        public DateTime LoanStartDate
        {
            get { return m_LoanStartDate; }
            set { m_LoanStartDate = value; }
        }

        public int Tenure
        {
            get { return m_Tenure; }
            set { m_Tenure = value; }
        }
        #endregion Properties


    }
}
