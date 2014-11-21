using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoOps
{
    public class MFOrderVo:OrderVo
    {
        #region Fields

        private int m_OrderDetailsId;
        private string m_CustomerName;
        private int m_SchemePlanCode;
        private int m_OrderNumber;
        private double m_Amount;
        private string m_StatusCode;
        private string m_StatusReasonCode;
        private string m_TransactionCode;
        private int m_accountid;
        private string m_FolioNumber;
        private int m_IsImmediate;
        private string m_SourceCode;
        private string m_FutureTriggerCondition;
        private int m_portfolioId;
        private DateTime m_FutureExecutionDate;
        private int m_SchemePlanSwitch;
        private string m_BankName;
        private string m_BranchName;
        private string m_AddrLine1;
        private string m_AddrLine2;
        private string m_AddrLine3;
        private string m_City;
        private string m_State;
        private string m_Country;
        private string m_Pincode;
        private DateTime m_LivingSince;
        private int m_IsExecuted;
        private int m_amcCode;
        private string m_categorycode;
        private string m_rmName;
        private string m_BMName;
        private double m_Units;
        private string m_FrequencyCode;
        private DateTime m_StartDate;
        private DateTime m_EndDate;
        private string m_PanNo;
        private string m_ARNNo;
        private string m_DivOption;
        private string m_Remarks;
        public int AssociateId { get; set; }
        public int BankBranchId { get; set; }
        #endregion

        #region Properties

        public int OrderId
        {
            get { return m_OrderDetailsId; }
            set { m_OrderDetailsId = value; }
        }
        public string CustomerName
        {
            get { return m_CustomerName; }
            set { m_CustomerName = value; }
        }
        public int SchemePlanCode
        {
            get { return m_SchemePlanCode; }
            set { m_SchemePlanCode = value; }
        }
        public int OrderNumber
        {
            get { return m_OrderNumber; }
            set { m_OrderNumber = value; }
        }
        public double Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }
        public string StatusCode
        {
            get { return m_StatusCode; }
            set { m_StatusCode = value; }
        }
        public string StatusReasonCode
        {
            get { return m_StatusReasonCode; }
            set { m_StatusReasonCode = value; }
        }
        public string TransactionCode
        {
            get { return m_TransactionCode; }
            set { m_TransactionCode = value; }
        }
        public string FolioNumber
        {
            get { return m_FolioNumber; }
            set { m_FolioNumber = value; }
        }
        public int accountid
        {
            get { return m_accountid; }
            set { m_accountid = value; }
        }

        public int IsImmediate
        {
            get { return m_IsImmediate; }
            set { m_IsImmediate = value; }
        }
        public string SourceCode
        {
            get { return m_SourceCode; }
            set { m_SourceCode = value; }
        }
        public string FutureTriggerCondition
        {
            get { return m_FutureTriggerCondition; }
            set { m_FutureTriggerCondition = value; }
        }
        public int portfolioId
        {
            get { return m_portfolioId; }
            set { m_portfolioId = value; }
        }
        public DateTime FutureExecutionDate
        {
            get { return m_FutureExecutionDate; }
            set { m_FutureExecutionDate = value; }
        }
        public int SchemePlanSwitch
        {
            get { return m_SchemePlanSwitch; }
            set { m_SchemePlanSwitch = value; }
        }
        public string BankName
        {
            get { return m_BankName; }
            set { m_BankName = value; }
        }
        public string BranchName
        {
            get { return m_BranchName; }
            set { m_BranchName = value; }
        }
        public string AddrLine1
        {
            get { return m_AddrLine1; }
            set { m_AddrLine1 = value; }
        }
        public string AddrLine2
        {
            get { return m_AddrLine2; }
            set { m_AddrLine2 = value; }
        }
        public string AddrLine3
        {
            get { return m_AddrLine3; }
            set { m_AddrLine3 = value; }
        }
        public string City
        {
            get { return m_City; }
            set { m_City = value; }
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
        public string Remarks
        {
            get { return m_Remarks; }
            set { m_Remarks = value; }
        }
        public string Pincode
        {
            get { return m_Pincode; }
            set { m_Pincode = value; }
        }
        public DateTime LivingSince
        {
            get { return m_LivingSince; }
            set { m_LivingSince = value; }
        }
        public int IsExecuted
        {
            get { return m_IsExecuted; }
            set { m_IsExecuted = value; }
        }
        public int Amccode
        {
            get { return m_amcCode; }
            set { m_amcCode = value; }
        }
        public string category
        {
            get { return m_categorycode; }
            set { m_categorycode = value; }
        }

        public string RMName
        {
            get { return m_rmName; }
            set { m_rmName = value; }
        }
        public string BMName
        {
            get { return m_BMName; }
            set { m_BMName = value; }
        }
        public double Units
        {
            get { return m_Units; }
            set { m_Units = value; }
        }

        public string FrequencyCode
        {
            get { return m_FrequencyCode; }
            set { m_FrequencyCode = value; }
        }
        public DateTime StartDate
        {
            get { return m_StartDate; }
            set { m_StartDate = value; }
        }
        public DateTime EndDate
        {
            get { return m_EndDate; }
            set { m_EndDate = value; }
        }
        public string PanNo
        {
            get { return m_PanNo; }
            set { m_PanNo = value; }
        }
        public string ARNNo
        {
            get { return m_ARNNo; }
            set { m_ARNNo = value; }
        }

        public string DivOption
        {
            get { return m_DivOption; }
            set { m_DivOption = value; }
        }
        
        #endregion

    }
}
