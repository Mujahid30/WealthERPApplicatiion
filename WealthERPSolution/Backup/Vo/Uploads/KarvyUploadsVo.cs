using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoUploads
{
    public class KarvyUploadsVo
    {
        #region Fields

        private string m_filepath;

        private string m_ProductCode;
        private string m_Fund;
        private string m_FolioNumber;
        private string m_SchemeCode;
        private string m_DividendOption;
        private string m_FundDescription;
        private string m_TransactionHead;
        private string m_TransactionNumber;
        private string m_Switch_RefNo;
        private string m_InstrumentNumber;
        private string m_InvestorName;
        private string m_JointName1;
        private string m_JointName2;
        private string m_Address1;
        private string m_Address2;
        private string m_Address3;
        private string m_City;
        private string m_Pincode;
        private string m_State;
        private string m_Country;
        private string m_DateofBirth;
        private string m_PhoneResidence;
        private string m_PhoneRes1;
        private string m_PhoneRes2;
        private string m_Mobile;
        private string m_PhoneOffice;
        private string m_PhoneOff1;
        private string m_PhoneOff2;
        private string m_FaxResidence;
        private string m_FaxOffice;
        private string m_TaxStatus;
        private string m_OccCode;
        private string m_Email;
        private string m_BankAccno;
        private string m_BankName;
        private string m_AccountType;
        private string m_Branch;
        private string m_BankAddress1;
        private string m_BankAddress2;
        private string m_BankAddress3;
        private string m_BankCity;
        private string m_BankPhone;
        private string m_PANNumber;
        private string m_TransactionMode;
        private string m_TransactionStatus;
        private string m_BranchName;
        private string m_BranchTransactionNo;
        private string m_TransactionDate;
        private string m_ProcessDate;
        private string m_Price;
        private string m_LoadPercentage;
        private string m_Units;
        private string m_Amount;
        private string m_LoadAmount;
        private string m_AgentCode;
        private string m_SubBrokerCode;
        private string m_BrokeragePercentage;
        private string m_Commission;
        private string m_InvestorID;
        private string m_ReportDate;
        private string m_ReportTime;
        private string m_TransactionSub;
        private string m_ApplicationNumber;
        private string m_TransactionID;
        private string m_TransactionDescription;
        private string m_TransactionType;
        private string m_OrgPurchaseDate;
        private string m_OrgPurchaseAmount;
        private string m_OrgPurchaseUnits;
        private string m_TrTypeFlag;
        private string m_SwitchFundDate;
        private string m_InstrumentDate;
        private string m_InstrumentBank;
        private string m_Remarks;
        private string m_Scheme;
        private string m_Plan;
        private string m_NAV;
        private string m_AnnualizedPercentage;
        private string m_AnnualizedCommision;
        private string m_OrginalPurchaseTrnxNo;
        private string m_OrginalPurchaseBranch;
        private string m_OldAcno;
        private string m_IHNo;
        private string m_IsRejected;
        private string m_IsFolioNew;
        private string m_IsCustomerNew;
        private string m_RejectedRemark;
        private string m_AdviserId;
        private string m_CustomerId;
        private string m_AccountId;
        private string m_BuySell;
        private string m_FinacialFlag;
        private string m_TransactionType1;
        private string m_TransactionTrigger;
        private string m_OccupationDescription;
        private string m_TypeCode;
        private string m_SubTypeCode;
        private string m_TPIN;
        private string m_FName;
        private string m_MName;
        private string m_ModeofHolding;
        private string m_ModeofHoldingDesc;
        private string m_MapinId;
        private string m_BankState;
        private string m_BankCountry;
        private string m_ProcessId;

        
        
        #endregion Fields

        #region Properties

        public string TypeCode
        {
            get { return m_TypeCode; }
            set { m_TypeCode = value; }
        }

        public string SubTypeCode
        {
            get { return m_SubTypeCode; }
            set { m_SubTypeCode = value; }
        }

        public string OccupationDescription
        {
            get { return m_OccupationDescription; }
            set { m_OccupationDescription = value; }
        }
        public string Filepath
        {
            get { return m_filepath; }
            set { m_filepath = value; }
        }
        public string ProductCode
        {
            get { return m_ProductCode; }
            set { m_ProductCode = value; }
        }
        public string Fund
        {
            get { return m_Fund; }
            set { m_Fund = value; }
        }
        public string FolioNumber
        {
            get { return m_FolioNumber; }
            set { m_FolioNumber = value; }
        }
        public string SchemeCode
        {
            get { return m_SchemeCode; }
            set { m_SchemeCode = value; }
        }
        public string DividendOption
        {
            get { return m_DividendOption; }
            set { m_DividendOption = value; }
        }
        public string FundDescription
        {
            get { return m_FundDescription; }
            set { m_FundDescription = value; }
        }
        public string TransactionHead
        {
            get { return m_TransactionHead; }
            set { m_TransactionHead = value; }
        }
        public string TransactionNumber
        {
            get { return m_TransactionNumber; }
            set { m_TransactionNumber = value; }
        }
        public string Switch_RefNo
        {
            get { return m_Switch_RefNo; }
            set { m_Switch_RefNo = value; }
        }
        public string InstrumentNumber
        {
            get { return m_InstrumentNumber; }
            set { m_InstrumentNumber = value; }
        }
        public string InvestorName
        {
            get { return m_InvestorName; }
            set { m_InvestorName = value; }
        }
        public string JointName1
        {
            get { return m_JointName1; }
            set { m_JointName1 = value; }
        }
        public string JointName2
        {
            get { return m_JointName2; }
            set { m_JointName2 = value; }
        }
        public string Address1
        {
            get { return m_Address1; }
            set { m_Address1 = value; }
        }
        public string Address2
        {
            get { return m_Address2; }
            set { m_Address2 = value; }
        }
        public string Address3
        {
            get { return m_Address3; }
            set { m_Address3 = value; }
        }
        public string City
        {
            get { return m_City; }
            set { m_City = value; }
        }
        public string Pincode
        {
            get { return m_Pincode; }
            set { m_Pincode = value; }
        }
        public string State
        {
            get { return m_State; }
            set { m_State = value; }
        }
        public string Country
        {
            get { return m_Country; }
            set { m_Country = value; }
        }
        public string DateofBirth
        {
            get { return m_DateofBirth; }
            set { m_DateofBirth = value; }
        }
        public string PhoneResidence
        {
            get { return m_PhoneResidence; }
            set { m_PhoneResidence = value; }
        }
        public string PhoneRes1
        {
            get { return m_PhoneRes1; }
            set { m_PhoneRes1 = value; }
        }
        public string PhoneRes2
        {
            get { return m_PhoneRes2; }
            set { m_PhoneRes2 = value; }
        }
        public string Mobile
        {
            get { return m_Mobile; }
            set { m_Mobile = value; }
        }
        public string PhoneOffice
        {
            get { return m_PhoneOffice; }
            set { m_PhoneOffice = value; }
        }
        public string PhoneOff1
        {
            get { return m_PhoneOff1; }
            set { m_PhoneOff1 = value; }
        }
        public string PhoneOff2
        {
            get { return m_PhoneOff2; }
            set { m_PhoneOff2 = value; }
        }
        public string FaxResidence
        {
            get { return m_FaxResidence; }
            set { m_FaxResidence = value; }
        }
        public string FaxOffice
        {
            get { return m_FaxOffice; }
            set { m_FaxOffice = value; }
        }
        public string TaxStatus
        {
            get { return m_TaxStatus; }
            set { m_TaxStatus = value; }
        }
        public string OccCode
        {
            get { return m_OccCode; }
            set { m_OccCode = value; }
        }
        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }
        public string BankAccno
        {
            get { return m_BankAccno; }
            set { m_BankAccno = value; }
        }
        public string BankName
        {
            get { return m_BankName; }
            set { m_BankName = value; }
        }
        public string AccountType
        {
            get { return m_AccountType; }
            set { m_AccountType = value; }
        }
        public string Branch
        {
            get { return m_Branch; }
            set { m_Branch = value; }
        }
        public string BankAddress1
        {
            get { return m_BankAddress1; }
            set { m_BankAddress1 = value; }
        }
        public string BankAddress2
        {
            get { return m_BankAddress2; }
            set { m_BankAddress2 = value; }
        }
        public string BankAddress3
        {
            get { return m_BankAddress3; }
            set { m_BankAddress3 = value; }
        }
        public string BankCity
        {
            get { return m_BankCity; }
            set { m_BankCity = value; }
        }
        public string BankPhone
        {
            get { return m_BankPhone; }
            set { m_BankPhone = value; }
        }
        public string PANNumber
        {
            get { return m_PANNumber; }
            set { m_PANNumber = value; }
        }
        public string TransactionMode
        {
            get { return m_TransactionMode; }
            set { m_TransactionMode = value; }
        }
        public string TransactionStatus
        {
            get { return m_TransactionStatus; }
            set { m_TransactionStatus = value; }
        }
        public string BranchName
        {
            get { return m_BranchName; }
            set { m_BranchName = value; }
        }
        public string BranchTransactionNo
        {
            get { return m_BranchTransactionNo; }
            set { m_BranchTransactionNo = value; }
        }
        public string TransactionDate
        {
            get { return m_TransactionDate; }
            set { m_TransactionDate = value; }
        }
        public string ProcessDate
        {
            get { return m_ProcessDate; }
            set { m_ProcessDate = value; }
        }
        public string Price
        {
            get { return m_Price; }
            set { m_Price = value; }
        }
        public string LoadPercentage
        {
            get { return m_LoadPercentage; }
            set { m_LoadPercentage = value; }
        }
        public string Units
        {
            get { return m_Units; }
            set { m_Units = value; }
        }
        public string Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }
        public string LoadAmount
        {
            get { return m_LoadAmount; }
            set { m_LoadAmount = value; }
        }
        public string AgentCode
        {
            get { return m_AgentCode; }
            set { m_AgentCode = value; }
        }
        public string SubBrokerCode
        {
            get { return m_SubBrokerCode; }
            set { m_SubBrokerCode = value; }
        }
        public string BrokeragePercentage
        {
            get { return m_BrokeragePercentage; }
            set { m_BrokeragePercentage = value; }
        }
        public string Commission
        {
            get { return m_Commission; }
            set { m_Commission = value; }
        }
        public string InvestorID
        {
            get { return m_InvestorID; }
            set { m_InvestorID = value; }
        }
        public string ReportDate
        {
            get { return m_ReportDate; }
            set { m_ReportDate = value; }
        }
        public string ReportTime
        {
            get { return m_ReportTime; }
            set { m_ReportTime = value; }
        }
        public string TransactionSub
        {
            get { return m_TransactionSub; }
            set { m_TransactionSub = value; }
        }
        public string ApplicationNumber
        {
            get { return m_ApplicationNumber; }
            set { m_ApplicationNumber = value; }
        }
        public string TransactionID
        {
            get { return m_TransactionID; }
            set { m_TransactionID = value; }
        }
        public string TransactionDescription
        {
            get { return m_TransactionDescription; }
            set { m_TransactionDescription = value; }
        }
        public string TransactionType
        {
            get { return m_TransactionType; }
            set { m_TransactionType = value; }
        }
        public string OrgPurchaseDate
        {
            get { return m_OrgPurchaseDate; }
            set { m_OrgPurchaseDate = value; }
        }
        public string OrgPurchaseAmount
        {
            get { return m_OrgPurchaseAmount; }
            set { m_OrgPurchaseAmount = value; }
        }
        public string OrgPurchaseUnits
        {
            get { return m_OrgPurchaseUnits; }
            set { m_OrgPurchaseUnits = value; }
        }
        public string TrTypeFlag
        {
            get { return m_TrTypeFlag; }
            set { m_TrTypeFlag = value; }
        }
        public string SwitchFundDate
        {
            get { return m_SwitchFundDate; }
            set { m_SwitchFundDate = value; }
        }
        public string InstrumentDate
        {
            get { return m_InstrumentDate; }
            set { m_InstrumentDate = value; }
        }
        public string InstrumentBank
        {
            get { return m_InstrumentBank; }
            set { m_InstrumentBank = value; }
        }
        public string Remarks
        {
            get { return m_Remarks; }
            set { m_Remarks = value; }
        }
        public string Scheme
        {
            get { return m_Scheme; }
            set { m_Scheme = value; }
        }
        public string Plan
        {
            get { return m_Plan; }
            set { m_Plan = value; }
        }
        public string NAV
        {
            get { return m_NAV; }
            set { m_NAV = value; }
        }
        public string AnnualizedPercentage
        {
            get { return m_AnnualizedPercentage; }
            set { m_AnnualizedPercentage = value; }
        }
        public string AnnualizedCommision
        {
            get { return m_AnnualizedCommision; }
            set { m_AnnualizedCommision = value; }
        }
        public string OrginalPurchaseTrnxNo
        {
            get { return m_OrginalPurchaseTrnxNo; }
            set { m_OrginalPurchaseTrnxNo = value; }
        }
        public string OrginalPurchaseBranch
        {
            get { return m_OrginalPurchaseBranch; }
            set { m_OrginalPurchaseBranch = value; }
        }
        public string OldAcno
        {
            get { return m_OldAcno; }
            set { m_OldAcno = value; }
        }
        public string IHNo
        {
            get { return m_IHNo; }
            set { m_IHNo = value; }
        }
        public string IsRejected
        {
            get { return m_IsRejected; }
            set { m_IsRejected = value; }
        }
        public string IsFolioNew
        {
            get { return m_IsFolioNew; }
            set { m_IsFolioNew = value; }
        }
        public string IsCustomerNew
        {
            get { return m_IsCustomerNew; }
            set { m_IsCustomerNew = value; }
        }
        public string RejectedRemark
        {
            get { return m_RejectedRemark; }
            set { m_RejectedRemark = value; }
        }
        public string AdviserId
        {
            get { return m_AdviserId; }
            set { m_AdviserId = value; }
        }
        public string CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }
        public string AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }
        public string BuySell
        {
            get { return m_BuySell; }
            set { m_BuySell = value; }
        }
        public string FinacialFlag
        {
            get { return m_FinacialFlag; }
            set { m_FinacialFlag = value; }
        }
        public string TransactionType1
        {
            get { return m_TransactionType1; }
            set { m_TransactionType1 = value; }
        }
        public string TransactionTrigger
        {
            get { return m_TransactionTrigger; }
            set { m_TransactionTrigger = value; }
        }

        public string TPIN
        {
            get { return m_TPIN; }
            set { m_TPIN = value; }
        }

        public string FirstName
        {
            get { return m_FName; }
            set { m_FName = value; }
        }

        public string MiddleName
        {
            get { return m_MName; }
            set { m_MName = value; }
        }

        public string ModeOfHolding
        {
            get { return m_ModeofHolding; }
            set { m_ModeofHolding = value; }
        }

        public string ModeOfHoldingDesc
        {
            get { return m_ModeofHoldingDesc; }
            set { m_ModeofHoldingDesc = value; }
        }

        public string MapinId
        {
            get { return m_MapinId; }
            set { m_MapinId = value; }
        }

        public string BankState
        {
            get { return m_BankState; }
            set { m_BankState = value; }
        }

        public string BankCountry
        {
            get { return m_BankCountry; }
            set { m_BankCountry = value; }
        }

        public string ProcessId
        {
            get { return m_ProcessId; }
            set { m_ProcessId = value; }
        }

        #endregion properties
    }
}
