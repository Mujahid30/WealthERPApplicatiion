using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class EquityBrokerageVo
    {
#region Fields
        private int m_CEB_BrokerageId;
        private char m_CEB_BuySell;
        private float m_CEB_Brokerage;
        private float m_CEB_ServiceTax;
        private float m_CEB_Clearing;
        private float m_CEB_STT;
        private float m_CEB_TransactionType;
        private char m_CEB_Class;
        private char m_CEB_CalculationBasis;
        private int m_CEB_CreatedBy;
        private int m_CEB_ModifiedBy;
        private int m_AdviserId;
        private int m_CustomerId;

      
     

#endregion Fields

        #region Properties

        public int CEB_BrokerageId
        {
            get { return m_CEB_BrokerageId; }
            set { m_CEB_BrokerageId = value; }
        }
        

        public char CEB_BuySell
        {
            get { return m_CEB_BuySell; }
            set { m_CEB_BuySell = value; }
        }
       
        public float CEB_Brokerage
        {
            get { return m_CEB_Brokerage; }
            set { m_CEB_Brokerage = value; }
        }

        public float CEB_ServiceTax
        {
            get { return m_CEB_ServiceTax; }
            set { m_CEB_ServiceTax = value; }
        }

        public float CEB_Clearing
        {
            get { return m_CEB_Clearing; }
            set { m_CEB_Clearing = value; }
        }

        public float CEB_STT
        {
            get { return m_CEB_STT; }
            set { m_CEB_STT = value; }
        }

        public float CEB_TransactionType
        {
            get { return m_CEB_TransactionType; }
            set { m_CEB_TransactionType = value; }
        }

        public char CEB_Class
        {
            get { return m_CEB_Class; }
            set { m_CEB_Class = value; }
        }

        public char CEB_CalculationBasis
        {
            get { return m_CEB_CalculationBasis; }
            set { m_CEB_CalculationBasis = value; }
        }

        public int CEB_CreatedBy
        {
            get { return m_CEB_CreatedBy; }
            set { m_CEB_CreatedBy = value; }
        }

        public int CEB_ModifiedBy
        {
            get { return m_CEB_ModifiedBy; }
            set { m_CEB_ModifiedBy = value; }
        }
        public int AdviserId
        {
            get { return m_AdviserId; }
            set { m_AdviserId = value; }
        }
        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }

        #endregion Properties


    }
}
