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
        private int m_OrderStepId;
        private string m_ApplicationNumber;
        private int m_ApplicationReceivedDate;
        private int m_PaymentMode;
        private string m_ChequeNumber;
        private DateTime m_PaymentDate;
        private int m_CustBankAccId;

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
        public int OrderStepId
        {
            get { return m_OrderStepId; }
            set { m_OrderStepId = value; }
        }
        public string ApplicationNumber
        {
            get { return m_ApplicationNumber; }
            set { m_ApplicationNumber = value; }
        }
        public int ApplicationReceivedDate
        {
            get { return m_ApplicationReceivedDate; }
            set { m_ApplicationReceivedDate = value; }
        }
        public int PaymentMode
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

        #endregion
    }
}
