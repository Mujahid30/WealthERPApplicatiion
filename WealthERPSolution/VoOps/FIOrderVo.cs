using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoOps
{
    public class FIOrderVo : OrderVo
    {

        //private int    m_DetailsId;
        //private int m_OrderId;
        private string m_AssetInstrumentCategoryCode;

        public string AssetInstrumentCategoryCode
        {
            get { return m_AssetInstrumentCategoryCode; }
            set { m_AssetInstrumentCategoryCode = value; }
        }
        private string m_IssuerId;

        public string IssuerId
        {
            get { return m_IssuerId; }
            set { m_IssuerId = value; }
        }
        private string m_ExisitingDepositreceiptno;

        public string ExisitingDepositreceiptno
        {
            get { return m_ExisitingDepositreceiptno; }
            set { m_ExisitingDepositreceiptno = value; }
        }
        private double m_RenewalAmount;

        public double RenewalAmount
        {
            get { return m_RenewalAmount; }
            set { m_RenewalAmount = value; }
        }
        private DateTime m_MaturityDate;

        public DateTime MaturityDate
        {
            get { return m_MaturityDate; }
            set { m_MaturityDate = value; }
        }
        private double m_MaturityAmount;

        public double MaturityAmount
        {
            get { return m_MaturityAmount; }
            set { m_MaturityAmount = value; }
        }

        private string m_TransactionType;

        public string TransactionType
        {
            get { return m_TransactionType; }
            set { m_TransactionType = value; }
        }
        private string m_SeriesDetails;

        public string SeriesDetails
        {
            get { return m_SeriesDetails; }
            set { m_SeriesDetails = value; }
        }

        //private string m_ModeOfHolding;


        private int m_SchemeId;

        public int SchemeId
        {
            get { return m_SchemeId; }
            set { m_SchemeId = value; }
        }

        private int m_SeriesId;

        public int SeriesId
        {
            get { return m_SeriesId; }
            set { m_SeriesId = value; }
        }

        private int m_DepCustBankAccId;

        public int DepCustBankAccId
        {
            get { return m_DepCustBankAccId; }
            set { m_DepCustBankAccId = value; }
        }
        //private int m_NoOfBonds;

        //public int NoOfBonds
        //{
        //    get { return m_NoOfBonds; }
        //    set { m_NoOfBonds = value; }
        //}
        private double m_AmountPayable;

        public double AmountPayable
        {
            get { return m_AmountPayable; }
            set { m_AmountPayable = value; }
        }
        private string m_ModeOfHolding;

        public string ModeOfHolding
        {
            get { return m_ModeOfHolding; }
            set { m_ModeOfHolding = value; }
        }
        private string m_Schemeoption;

        public string Schemeoption
        {
            get { return m_Schemeoption; }
            set { m_Schemeoption = value; }
        }
        private string m_Depositpayableto;

        public string Depositpayableto
        {
            get { return m_Depositpayableto; }
            set { m_Depositpayableto = value; }
        }
        private string m_Frequency;

        public string Frequency
        {
            get { return m_Frequency; }
            set { m_Frequency = value; }
        }
        private string m_Privilidge;

        public string Privilidge
        {
            get { return m_Privilidge; }
            set { m_Privilidge = value; }
        }
        private string m_CODimage;

        public string CODimage
        {
            get { return m_CODimage; }
            set { m_CODimage = value; }
        }

        public Int64 DematAccountId { get; set; }
        public double Qty { get; set; }
        public string BranchName { get; set; }
        public string AssetInstrumentCategory { get; set; }
        public int authenticatedbyId { get; set; }
        public int authenticId { get; set; }
        //private int m_OrderId;

        ////public int OrderId
        ////{
        ////    get { return m_OrderId; }
        ////    set { m_OrderId = value; }
        ////}
        //private int m_OrderDetailsId;

        //public int OrderDetailsId
        //{
        //    get { return m_OrderDetailsId; }
        //    set { m_OrderDetailsId = value; }
        //}
        //private int  m_SchemePlanCode;

        //public int  SchemePlanCode 
        //{
        //    get { return m_SchemePlanCode; }
        //    set { m_SchemePlanCode = value; }
        //}
        //private int m_OrderNumber;

        //public int OrderNumber
        //{
        //    get { return m_OrderNumber; }
        //    set { m_OrderNumber = value; }
        //}
        //private double m_Amount;

        //public double Amount
        //{
        //    get { return m_Amount; }
        //    set { m_Amount = value; }
        //}
        //private int m_accountid;

        //public int Accountid
        //{
        //    get { return m_accountid; }
        //    set { m_accountid = value; }
        //}
        //private string m_TransactionClassificationCode;

        //public string TransactionClassificationCode
        //{
        //    get { return m_TransactionClassificationCode; }
        //    set { m_TransactionClassificationCode = value; }
        //}
        //private string m_IsImmediate;

        //public string IsImmediate
        //{
        //    get { return m_IsImmediate; }
        //    set { m_IsImmediate = value; }
        //}
        //private string m_FutureTriggerCondition;

        //public string FutureTriggerCondition
        //{
        //    get { return m_FutureTriggerCondition; }
        //    set { m_FutureTriggerCondition = value; }
        //}
        //private int m_portfolioId;

        //public int PortfolioId
        //{
        //    get { return m_portfolioId; }
        //    set { m_portfolioId = value; }
        //}
        //private DateTime m_FutureExecutionDate;

        //public DateTime FutureExecutionDate
        //{
        //    get { return m_FutureExecutionDate; }
        //    set { m_FutureExecutionDate = value; }
        //}
        //private int m_SchemePlanSwitch;

        //public int SchemePlanSwitch
        //{
        //    get { return m_SchemePlanSwitch; }
        //    set { m_SchemePlanSwitch = value; }
        //}
        //private string m_IsExecuted;

        //public string IsExecuted
        //{
        //    get { return m_IsExecuted; }
        //    set { m_IsExecuted = value; }
        //}
        //private string m_FrequencyCode;

        //public string FrequencyCode
        //{
        //    get { return m_FrequencyCode; }
        //    set { m_FrequencyCode = value; }
        //}
        //private DateTime m_StartDate;

        //public DateTime StartDate
        //{
        //    get { return m_StartDate; }
        //    set { m_StartDate = value; }
        //}
        //private DateTime m_EndDate;

        //public DateTime EndDate
        //{
        //    get { return m_EndDate; }
        //    set { m_EndDate = value; }
        //}
        //private double m_Units;

        //public double Units
        //{
        //    get { return m_Units; }
        //    set { m_Units = value; }
        //}
        //private DateTime m_CreatedOn;

        //public DateTime CreatedOn
        //{
        //    get { return m_CreatedOn; }
        //    set { m_CreatedOn = value; }
        //}
        //private int m_ModifiedBy;

        //public int ModifiedBy
        //{
        //    get { return m_ModifiedBy; }
        //    set { m_ModifiedBy = value; }
        //}
        //private DateTime m_ModifiedOn;

        //public DateTime ModifiedOn
        //{
        //    get { return m_ModifiedOn; }
        //    set { m_ModifiedOn = value; }
        //}
        //private int m_CreatedBy;

        //public int CreatedBy
        //{
        //    get { return m_CreatedBy; }
        //    set { m_CreatedBy = value; }
        //}
        //private string m_BankName;

        //public string BankName
        //{
        //    get { return m_BankName; }
        //    set { m_BankName = value; }
        //}
        //private string m_BranchName;

        //public string BranchName
        //{
        //    get { return m_BranchName; }
        //    set { m_BranchName = value; }
        //}
        //private string m_AddrLine1;

        //public string AddrLine1
        //{
        //    get { return m_AddrLine1; }
        //    set { m_AddrLine1 = value; }
        //}
        //private string m_AddrLine2;

        //public string AddrLine2
        //{
        //    get { return m_AddrLine2; }
        //    set { m_AddrLine2 = value; }
        //}
        //private string m_AddrLine3;

        //public string AddrLine3
        //{
        //    get { return m_AddrLine3; }
        //    set { m_AddrLine3 = value; }
        //}
        //private string m_City;

        //public string City
        //{
        //    get { return m_City; }
        //    set { m_City = value; }
        //}
        //private string m_State;

        //public string State
        //{
        //    get { return m_State; }
        //    set { m_State = value; }
        //}
        //private string m_Country;

        //public string Country
        //{
        //    get { return m_Country; }
        //    set { m_Country = value; }
        //}
        //private double m_PinCode;

        //public double PinCode
        //{
        //    get { return m_PinCode; }
        //    set { m_PinCode = value; }
        //}
        //private DateTime m_LivingScince;

        //public DateTime LivingScince
        //{
        //    get { return m_LivingScince; }
        //    set { m_LivingScince = value; }
        //}
        //private DateTime m_ApprovalDate;

        //public DateTime ApprovalDate
        //{
        //    get { return m_ApprovalDate; }
        //    set { m_ApprovalDate = value; }
        //}
        //private string m_ARNNo;

        //public string ARNNo
        //{
        //    get { return m_ARNNo; }
        //    set { m_ARNNo = value; }
        //}






    }
}
