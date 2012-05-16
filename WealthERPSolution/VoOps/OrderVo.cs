using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoOps
{
    public class OrderVo
    {
        #region Fields

        private int m_OrderId;
        private string m_AssetCategory;
        private DateTime m_OrderDate;
        private int m_OrderNumber;
        private int m_CustomerId;
        private string m_SourceCode;
        private string m_ApplicationNumber;
        private DateTime m_ApplicationReceivedDate;
        private string m_PaymentMode;
        private string m_ChequeNumber;
        private DateTime m_PaymentDate;
        private int m_CustBankAccId;

        private string m_OrderStepCode;
        private string m_OrderStatusCode;
        private string m_ReasonCode;
        private int m_ApprovedBy;
        private int m_AssociationId;
        private string m_AssociationType;
        private int m_IsCustomerApprovalApplicable;

        #endregion

        #region Properties

        public int OrderId
        {
            get { return m_OrderId; }
            set { m_OrderId = value; }
        }
        public string AssetCategory
        {
            get { return m_AssetCategory; }
            set { m_AssetCategory = value; }
        }
        public DateTime OrderDate
        {
            get { return m_OrderDate; }
            set { m_OrderDate = value; }
        }
        public int OrderNumber
        {
            get { return m_OrderNumber; }
            set { m_OrderNumber = value; }
        }
        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }
        public string SourceCode
        {
            get { return m_SourceCode; }
            set { m_SourceCode = value; }
        }
        public string ApplicationNumber
        {
            get { return m_ApplicationNumber; }
            set { m_ApplicationNumber = value; }
        }
        public DateTime ApplicationReceivedDate
        {
            get { return m_ApplicationReceivedDate; }
            set { m_ApplicationReceivedDate = value; }
        }
        public string PaymentMode
        {
            get { return m_PaymentMode; }
            set { m_PaymentMode = value; }
        }
        public string ChequeNumber
        {
            get { return m_ChequeNumber; }
            set { m_ChequeNumber = value; }
        }
        public DateTime PaymentDate
        {
            get { return m_PaymentDate; }
            set { m_PaymentDate = value; }
        }
        public int CustBankAccId
        {
            get { return m_CustBankAccId; }
            set { m_CustBankAccId = value; }
        }

        public string OrderStepCode
        {
            get { return m_OrderStepCode; }
            set { m_OrderStepCode = value; }
        }

        public string OrderStatusCode
        {
            get { return m_OrderStatusCode; }
            set { m_OrderStatusCode = value; }
        }

        public string ReasonCode
        {
            get { return m_ReasonCode; }
            set { m_ReasonCode = value; }
        }

        public int ApprovedBy
        {
            get { return m_ApprovedBy; }
            set { m_ApprovedBy = value; }
        }
        public int AssociationId
        {
            get { return m_AssociationId; }
            set { m_AssociationId = value; }
        }

        public string AssociationType
        {
            get { return m_AssociationType; }
            set { m_AssociationType = value; }
        }

        public int IsCustomerApprovalApplicable
        {
            get { return m_IsCustomerApprovalApplicable; }
            set { m_IsCustomerApprovalApplicable = value; }
        }

        #endregion
    }
}
