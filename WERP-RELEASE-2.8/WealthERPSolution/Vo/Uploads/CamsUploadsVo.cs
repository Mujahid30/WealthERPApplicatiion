using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoUploads
{
    public class CamsUploadsVo
    {
        #region Fields

        private string m_FolioChk;
        private string m_InvestorName;
        private string m_Address1;
        private string m_Address2;
        private string m_Address3;
        private string m_City;
        private string m_Pincode;
        private string m_Product;
        private string m_SchemeName;
        private string m_ReportDate;
        private string m_ClosingBalance;
        private string m_RupeeBalance;
        private string m_Type;
        private string m_SubType;
        private string m_Joint1PAN;
        private string m_SubBrok;
        private string m_ReinvestmentFlag;
        private string m_JointName1;
        private string m_TaxStatus1;
        private string m_StatusCode;
        private string m_InvestorIIN;
        private string m_ScanRefNumber; 
        private string m_EntryLoad;
        private string m_JointName2; 
        private string m_UINNumber;
        private string m_PhoneOffice; 
        private string m_PhoneResidence; 
        private string m_Email; 
        private string m_HoldingNA;
        private string m_AlternateFolio; 
        private string m_SequenceNumber; 
        private string m_MultipleBroker; 
        private string m_STT; 
        private string m_SchemeType; 
        private string m_Tax; 
        private string m_OldFolio;
        private string m_AltFolio; 
        private string m_InvIn; 
        private string m_PANNumber; 
        private string m_BrokerCode; 
        private string m_Joint2PAN; 
        private string m_GuardPAN; 
        private string m_TaxStatus;
        private string m_Dummy1; 
        private string m_FeedDate; 
        private string m_Dummy2; 
        private string m_Dummy3; 
        private string m_TransactionNature; 
        private string m_AlternateBroker;
        private string m_FolioNumber; 
        private string m_AMCCode; 
        private string m_CustomerId; 
        private string m_AdviserId; 
        private string m_IsCustomerNew; 
        private string m_IsRejectde; 
        private string m_IsFolioNew; 
        private string m_RejectedRemarks;
        private string m_ProductCode; 
        private string m_Scheme; 
        private string m_TransactionType; 
        private string m_TransactionNumber; 
        private string m_TransactionMode; 
        private string m_TransactionStatus; 
        private string m_UserCode; 
        private string m_UserTransactionNumber;
        private string m_BrokerageAmount; 
        private string m_BrokeragePercentage; 
        private string m_SubBrokerCode; 
        private string m_Amount; 
        private string m_Units; 
        private string m_PostDate; 
        private string m_ValueDate; 
        private string m_Price;
        private string m_ProcessId;

        #endregion Fields

        #region Properties
        public string FolioChk
        {
            get { return m_FolioChk; }
            set { m_FolioChk = value; }
        }
        public string InvestorName
        {
            get { return m_InvestorName; }
            set { m_InvestorName = value; }
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
        public string Product
        {
            get { return m_Product; }
            set { m_Product = value; }
        }
        public string SchemeName
        {
            get { return m_SchemeName; }
            set { m_SchemeName = value; }
        }
        public string ReportDate
        {
            get { return m_ReportDate; }
            set { m_ReportDate = value; }
        }
        public string ClosingBalance
        {
            get { return m_ClosingBalance; }
            set { m_ClosingBalance = value; }
        }
        public string RupeeBalance
        {
            get { return m_RupeeBalance; }
            set { m_RupeeBalance = value; }
        }
        
        public string SubBrok
        {
            get { return m_SubBrok; }
            set { m_SubBrok = value; }
        }
        

        public string ReinvestmentFlag
        {
            get { return m_ReinvestmentFlag; }
            set { m_ReinvestmentFlag = value; }
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
        

        public string PhoneOffice
        {
            get { return m_PhoneOffice; }
            set { m_PhoneOffice = value; }
        }
        

        public string PhoneResidence
        {
            get { return m_PhoneResidence; }
            set { m_PhoneResidence = value; }
        }
        

        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }
        

        public string HoldingNA
        {
            get { return m_HoldingNA; }
            set { m_HoldingNA = value; }
        }
        

        public string UINNumber
        {
            get { return m_UINNumber; }
            set { m_UINNumber = value; }
        }
        

        public string BrokerCode
        {
            get { return m_BrokerCode; }
            set { m_BrokerCode = value; }
        }
        

        public string PANNumber
        {
            get { return m_PANNumber; }
            set { m_PANNumber = value; }
        }
        

        public string Joint1PAN
        {
            get { return m_Joint1PAN; }
            set { m_Joint1PAN = value; }
        }
        

        public string Joint2PAN
        {
            get { return m_Joint2PAN; }
            set { m_Joint2PAN = value; }
        }
        

        public string GuardPAN
        {
            get { return m_GuardPAN; }
            set { m_GuardPAN = value; }
        }
        

        public string TaxStatus
        {
            get { return m_TaxStatus; }
            set { m_TaxStatus = value; }
        }

        

        public string InvIn
        {
            get { return m_InvIn; }
            set { m_InvIn = value; }
        }
        
        public string AltFolio
        {
            get { return m_AltFolio; }
            set { m_AltFolio = value; }
        }
        

        public string IsRejectde
        {
            get { return m_IsRejectde; }
            set { m_IsRejectde = value; }
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
        

        public string RejectedRemarks
        {
            get { return m_RejectedRemarks; }
            set { m_RejectedRemarks = value; }
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

        

        public string AMCCode
        {
            get { return m_AMCCode; }
            set { m_AMCCode = value; }
        }
        

        public string FolioNumber
        {
            get { return m_FolioNumber; }
            set { m_FolioNumber = value; }
        }
        

        public string ProductCode
        {
            get { return m_ProductCode; }
            set { m_ProductCode = value; }
        }


        public string Scheme
        {
            get { return m_Scheme; }
            set { m_Scheme = value; }
        }
        

        public string TransactionType
        {
            get { return m_TransactionType; }
            set { m_TransactionType = value; }
        }
        

        public string TransactionNumber
        {
            get { return m_TransactionNumber; }
            set { m_TransactionNumber = value; }
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
        

        public string UserCode
        {
            get { return m_UserCode; }
            set { m_UserCode = value; }
        }
        

        public string UserTransactionNumber
        {
            get { return m_UserTransactionNumber; }
            set { m_UserTransactionNumber = value; }
        }
        

        public string ValueDate
        {
            get { return m_ValueDate; }
            set { m_ValueDate = value; }
        }
        

        public string PostDate
        {
            get { return m_PostDate; }
            set { m_PostDate = value; }
        }
        

        public string Price
        {
            get { return m_Price; }
            set { m_Price = value; }
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
        

        public string BrokerageAmount
        {
            get { return m_BrokerageAmount; }
            set { m_BrokerageAmount = value; }
        }
        

        public string Dummy1
        {
            get { return m_Dummy1; }
            set { m_Dummy1 = value; }
        }
       

        public string FeedDate
        {
            get { return m_FeedDate; }
            set { m_FeedDate = value; }
        }
        

        public string Dummy2
        {
            get { return m_Dummy2; }
            set { m_Dummy2 = value; }
        }
    

        public string Dummy3
        {
            get { return m_Dummy3; }
            set { m_Dummy3 = value; }
        }
        


        public string TransactionNature
        {
            get { return m_TransactionNature; }
            set { m_TransactionNature = value; }
        }
       
        public string AlternateBroker
        {
            get { return m_AlternateBroker; }
            set { m_AlternateBroker = value; }
        }
       

        public string AlternateFolio
        {
            get { return m_AlternateFolio; }
            set { m_AlternateFolio = value; }
        }
        

        public string OldFolio
        {
            get { return m_OldFolio; }
            set { m_OldFolio = value; }
        }
        

        public string SequenceNumber
        {
            get { return m_SequenceNumber; }
            set { m_SequenceNumber = value; }
        }
        

        public string MultipleBroker
        {
            get { return m_MultipleBroker; }
            set { m_MultipleBroker = value; }
        }
        

        public string Tax
        {
            get { return m_Tax; }
            set { m_Tax = value; }
        }
        

        public string STT
        {
            get { return m_STT; }
            set { m_STT = value; }
        }
        

        public string SchemeType
        {
            get { return m_SchemeType; }
            set { m_SchemeType = value; }
        }
      

        public string EntryLoad
        {
            get { return m_EntryLoad; }
            set { m_EntryLoad = value; }
        }
        

        public string ScanRefNumber
        {
            get { return m_ScanRefNumber; }
            set { m_ScanRefNumber = value; }
        }
        

        public string InvestorIIN
        {
            get { return m_InvestorIIN; }
            set { m_InvestorIIN = value; }
        }
        

        public string TaxStatus1
        {
            get { return m_TaxStatus1; }
            set { m_TaxStatus1 = value; }
        }
        

        public string StatusCode
        {
            get { return m_StatusCode; }
            set { m_StatusCode = value; }
        }
        
        

        public string Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }
        

        public string SubType
        {
            get { return m_SubType; }
            set { m_SubType = value; }
        }

        public string ProcessId
        {
            get { return m_ProcessId; }
            set { m_ProcessId = value; }
        }

        #endregion Properties
    }
}
