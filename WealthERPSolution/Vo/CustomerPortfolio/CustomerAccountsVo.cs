using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoCustomerPortfolio;
using VoCustomerProfiling;

namespace VoCustomerPortfolio
{
    /// <summary>
    /// added field name to capture original costumer name
    /// </summary>
    public class CustomerAccountsVo : CustomerBankAccountVo
    {
        #region Fields

        private int m_AccountId;
        private int m_PortfolioId;
        private string m_AccountNum;
        private string m_AssetClass;
        private string m_AssetCategory;
        private string m_AssetCategoryName;
        private string m_AssetSubCategory;
        private string m_AssetSubCategoryName;
        private int m_IsJointHolding;
        private string m_AccountSource;
        private int m_CustomerId;
        private string m_CustomerName;
        private string m_ModeOfHolding;
        private string m_ModeOfHoldingCode;
        private string m_BankName;
        private int m_AMCCode;
        private string m_AMCName;
        private string m_Name; // original costumer name
        private string m_PolicyNum;
        private DateTime m_AccountOpeningDate;
        private string m_BrokerCode;
        private string m_TradeNum;
        private double m_BrokerageDeliveryPercentage;
        private double m_BrokerageSpeculativePercentage;
        private double m_OtherCharges;
        private string m_BrokerName;
        private int m_BankId;
        private string m_PanNumber;
        private string m_XCT_CustomerTypeCode;
        private string m_XCST_CustomerSubTypeCode;
        private string m_BankNameInExtFile;


        #endregion Fields


        private string m_CAddress1;
        private string m_CAddress2;
        private string m_CAddress3;
        private string m_CCity;
        private int m_CPinCode;
        private string m_JointName1;
        private string m_JointName2;
        private double m_CPhoneOffice;
        private double m_CPhoneRes;
        private string m_CEmail;
        private DateTime m_CDOB;
        private string m_CMGCXP_BankAddress1;
        private string m_CMGCXP_BankAddress2;
        private string m_CMGCXP_BankAddress3;
        private string m_CMGCXP_BankCity;



        #region Properties

        public string BankNameInExtFile
        {
            get { return m_BankNameInExtFile; }
            set { m_BankNameInExtFile = value; }
        }
        public string PanNumber
        {
            get { return m_PanNumber; }
            set { m_PanNumber = value; }
        }

        public string XCT_CustomerTypeCode
        {
            get { return m_XCT_CustomerTypeCode; }
            set { m_XCT_CustomerTypeCode = value; }
        }


        public string XCST_CustomerSubTypeCode
        {
            get { return m_XCST_CustomerSubTypeCode; }
            set { m_XCST_CustomerSubTypeCode = value; }
        }
        public string CAddress1
        {
            get { return m_CAddress1; }
            set { m_CAddress1 = value; }
        }
        public string CAddress2
        {
            get { return m_CAddress2; }
            set { m_CAddress2 = value; }
        }
        public string CAddress3
        {
            get { return m_CAddress3; }
            set { m_CAddress3 = value; }
        }
        public string CCity
        {
            get { return m_CCity; }
            set { m_CCity = value; }
        }
        public int CPinCode
        {
            get { return m_CPinCode; }
            set { m_CPinCode = value; }
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
        public double CPhoneOffice
        {
            get { return m_CPhoneOffice; }
            set { m_CPhoneOffice = value; }
        }
        public double CPhoneRes
        {
            get { return m_CPhoneRes; }
            set { m_CPhoneRes = value; }
        }
        public string CEmail
        {
            get { return m_CEmail; }
            set { m_CEmail = value; }
        }
        public DateTime CDOB
        {
            get { return m_CDOB; }
            set { m_CDOB = value; }
        }
        public string CMGCXP_BankAddress1
        {
            get { return m_CMGCXP_BankAddress1; }
            set { m_CMGCXP_BankAddress1 = value; }
        }
        public string CMGCXP_BankAddress2
        {
            get { return m_CMGCXP_BankAddress2; }
            set { m_CMGCXP_BankAddress2 = value; }
        }
        public string CMGCXP_BankAddress3
        {
            get { return m_CMGCXP_BankAddress3; }
            set { m_CMGCXP_BankAddress3 = value; }
        }
        public string CMGCXP_BankCity
        {
            get { return m_CMGCXP_BankCity; }
            set { m_CMGCXP_BankCity = value; }
        }



        public string ModeOfHoldingCode
        {
            get { return m_ModeOfHoldingCode; }
            set { m_ModeOfHoldingCode = value; }
        }
        public string PolicyNum
        {
            get { return m_PolicyNum; }
            set { m_PolicyNum = value; }
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
        public string AccountNum
        {
            get { return m_AccountNum; }
            set { m_AccountNum = value; }
        }
        public string AssetClass
        {
            get { return m_AssetClass; }
            set { m_AssetClass = value; }
        }
        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }

        public string CustomerName
        {
            get { return m_CustomerName; }
            set { m_CustomerName = value; }
        }
        public string ModeOfHolding
        {
            get { return m_ModeOfHolding; }
            set { m_ModeOfHolding = value; }
        }
        public string AssetCategory
        {
            get { return m_AssetCategory; }
            set { m_AssetCategory = value; }
        }
        public string AssetCategoryName
        {
            get { return m_AssetCategoryName; }
            set { m_AssetCategoryName = value; }
        }
        public string AssetSubCategory
        {
            get { return m_AssetSubCategory; }
            set { m_AssetSubCategory = value; }
        }
        public string AssetSubCategoryName
        {
            get { return m_AssetSubCategoryName; }
            set { m_AssetSubCategoryName = value; }
        }
        public int IsJointHolding
        {
            get { return m_IsJointHolding; }
            set { m_IsJointHolding = value; }
        }
        public string AccountSource
        {
            get { return m_AccountSource; }
            set { m_AccountSource = value; }
        }

        public string BankName
        {
            get { return m_BankName; }
            set { m_BankName = value; }
        }
        public DateTime AccountOpeningDate
        {
            get { return m_AccountOpeningDate; }
            set { m_AccountOpeningDate = value; }
        }
        public int AMCCode
        {
            get { return m_AMCCode; }
            set { m_AMCCode = value; }
        }

        public string AMCName
        {
            get { return m_AMCName; }
            set { m_AMCName = value; }
        }
        public string Name //original costumer name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public string BrokerCode
        {
            get { return m_BrokerCode; }
            set { m_BrokerCode = value; }
        }
        public string TradeNum
        {
            get { return m_TradeNum; }
            set { m_TradeNum = value; }
        }
        public double BrokerageDeliveryPercentage
        {
            get { return m_BrokerageDeliveryPercentage; }
            set { m_BrokerageDeliveryPercentage = value; }
        }
        public double BrokerageSpeculativePercentage
        {
            get { return m_BrokerageSpeculativePercentage; }
            set { m_BrokerageSpeculativePercentage = value; }
        }
        public double OtherCharges
        {
            get { return m_OtherCharges; }
            set { m_OtherCharges = value; }
        }
        public string BrokerName
        {
            get { return m_BrokerName; }
            set { m_BrokerName = value; }
        }
        public int BankId
        {
            get { return m_BankId; }
            set { m_BankId = value; }
        }
        #endregion Properties



    }

    public class CustomerISAAccountsVo
    {
        public bool IsJointHolding { get; set; }
        public string ModeOfHolding { get; set; }
        public int ISAAccountId { get; set; }
        public bool IsOperatedByPOA { get; set; }
        public string ISAAccountNumber { get; set; }
        public int AssociationId { get; set; }
        public string AssociationTypeCode { get; set; }
        public string JointHoldersIds { get; set; }
        public string NomineeIds { get; set; }

    }
}
