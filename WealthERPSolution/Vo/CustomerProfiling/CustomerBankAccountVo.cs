using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerProfiling
{
    /// <summary>
    /// Class Containing Customer Bank Account Information.
    /// </summary>
    public class CustomerBankAccountVo
    {
        #region Fields

        private int m_CustBankAccId;
        private string m_BankName;
        private string m_AccountType;
        private string m_BankAccountNum;
        private int m_PortfolioId;
        private string m_WERPBMBankName;
        private string m_ModeOfOperationCode;
        private string m_AccountTypeCode;
        private string m_BankCity;
        private int m_IsJointHolding;
        private string m_ModeOfOperation;
        private string m_BranchName;
        private string m_BranchAdrLine1;
        private string m_BranchAdrLine2;
        private string m_BranchAdrLine3;
        private int m_BranchAdrPinCode;
        private string m_BranchAdrCity;
        private string m_BranchAdrState;
        private string m_BranchAdrCountry;
        private string m_MICR;
        private string m_IFSC;
        private float m_Balance;
        private float m_InterestRate;
        private bool m_IsCurrent;
        private string m_BankBranchCode;
        public int BankId { get; set; }
        public int BankAccTypeId { get; set; }
        public string NeftCode { get; set; }
        public string RTGSCode { get; set; }

        public int BranchAddCityId { get; set; }
        public int BranchAddStateId { get; set; }
        public int BranchAddCountryId { get; set; }

      



        #endregion Fields

        #region Properties

        public string ModeOfOperationCode
        {
            get { return m_ModeOfOperationCode; }
            set { m_ModeOfOperationCode = value; }
        }
        public int PortfolioId
        {
            get { return m_PortfolioId; }
            set { m_PortfolioId = value; }
        }
        public string BankCity
        {
            get { return m_BankCity; }
            set { m_BankCity = value; }
        }
        public int IsJointHolding
        {
            get { return m_IsJointHolding; }
            set { m_IsJointHolding = value; }
        }

        public string AccountTypeCode
        {
            get { return m_AccountTypeCode; }
            set { m_AccountTypeCode = value; }
        }


        public string BankAccountNum
        {
            get { return m_BankAccountNum; }
            set { m_BankAccountNum = value; }
        }
        public int CustBankAccId
        {
            get { return m_CustBankAccId; }
            set { m_CustBankAccId = value; }
        }
        public string WERPBMBankName
        {
            get { return m_WERPBMBankName; }
            set { m_WERPBMBankName = value; }


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

        public string ModeOfOperation
        {
            get { return m_ModeOfOperation; }
            set { m_ModeOfOperation = value; }
        }
        public string BranchName
        {
            get { return m_BranchName; }
            set { m_BranchName = value; }
        }
        public string BranchAdrLine1
        {
            get { return m_BranchAdrLine1; }
            set { m_BranchAdrLine1 = value; }
        }
        public string BranchAdrLine2
        {
            get { return m_BranchAdrLine2; }
            set { m_BranchAdrLine2 = value; }
        }
        public string BranchAdrLine3
        {
            get { return m_BranchAdrLine3; }
            set { m_BranchAdrLine3 = value; }
        }
        public int BranchAdrPinCode
        {
            get { return m_BranchAdrPinCode; }
            set { m_BranchAdrPinCode = value; }
        }


        public string BranchAdrCity
        {
            get { return m_BranchAdrCity; }
            set { m_BranchAdrCity = value; }
        }
        public string BranchAdrState
        {
            get { return m_BranchAdrState; }
            set { m_BranchAdrState = value; }
        }
        public string BranchAdrCountry
        {
            get { return m_BranchAdrCountry; }
            set { m_BranchAdrCountry = value; }
        }
        public string MICR
        {
            get { return m_MICR; }
            set { m_MICR = value; }
        }
        public string IFSC
        {
            get { return m_IFSC; }
            set { m_IFSC = value; }
        }

        public float Balance
        {
            get { return m_Balance; }
            set { m_Balance = value; }
        }

        public float InterestRate
        {
            get { return m_InterestRate; }
            set { m_InterestRate = value; }
        }
        public bool IsCurrent
        {
            get { return m_IsCurrent; }
            set { m_IsCurrent = value; }
        }
        public string BankBranchCode
        {
            get { return m_BankBranchCode; }
            set { m_BankBranchCode = value; }
        }

        //public string BankName
        //{
        //    get { return m_BankName; }
        //    set { m_BankName = value; }
        //}

        #endregion Properties
    }
}
