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
        private string m_AccountNum;

       
        private string m_ModeOfOperation;
        private string m_BranchName;
        private string m_BranchAdrLine1;
        private string m_BranchAdrLine2;
        private string m_BranchAdrLine3;
        private int m_BranchAdrPinCode;
        private string m_BranchAdrCity;
        private string m_BranchAdrState;
        private string m_BranchAdrCountry;
        private long m_MICR;
        private string m_IFSC;
        private float m_Balance;
        private float m_InterestRate;


        #endregion Fields

        #region Properties
        public string AccountNum
        {
            get { return m_AccountNum; }
            set { m_AccountNum = value; }
        }
        public int CustBankAccId
        {
            get { return m_CustBankAccId; }
            set { m_CustBankAccId = value; }
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
        public long MICR
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

        #endregion Properties
    }
}
