using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class OtherInvestmentVo
    {
        #region Fields
        private string m_TransactionId;
        private string m_Name;
        private DateTime m_TransactionDate;
        private float m_Quantity;
        private string m_MeasureCode;
        private float m_TransactionRate;
        private string m_InvestmentCode;
        private char m_BuySell;
        
        
        private string m_CustomerId;

       
        #endregion Fields

        #region Properties
        public string CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }
        public string TransactionId
        {
            get { return m_TransactionId; }
            set { m_TransactionId = value; }
        }
        
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        
        public DateTime TransactionDate
        {
            get { return m_TransactionDate; }
            set { m_TransactionDate = value; }
        }
        
        public float Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }
       
        public string MeasureCode
        {
            get { return m_MeasureCode; }
            set { m_MeasureCode = value; }
        }
       
        public float TransactionRate
        {
            get { return m_TransactionRate; }
            set { m_TransactionRate = value; }
        }
       
        public string InvestmentCode
        {
            get { return m_InvestmentCode; }
            set { m_InvestmentCode = value; }
        }
        
        public char BuySell
        {
            get { return m_BuySell; }
            set { m_BuySell = value; }
        }
        

       
       
       

        #endregion Properties

    }
}
