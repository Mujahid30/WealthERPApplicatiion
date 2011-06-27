using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class RealEstateVo
    {
        #region fields
        private string m_TransactionId;
        private string m_Name;
        private string m_InvestmentCode;
        private DateTime m_TransactionDate;
        private float m_Quantity;
        private float m_TransactionRate;
        private string m_MeasureCode;
        private string m_AccountId;
        private float m_CurrentRate;
        private string m_BuySell;
        #endregion fields

        #region properties

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
       

        public string InvestmentCode
        {
            get { return m_InvestmentCode; }
            set { m_InvestmentCode = value; }
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
       

        public float TransactionRate
        {
            get { return m_TransactionRate; }
            set { m_TransactionRate = value; }
        }

        public string AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }

        public string MeasureCode
        {
            get { return m_MeasureCode; }
            set { m_MeasureCode = value; }
        }
      


        public float CurrentRate
        {
            get { return m_CurrentRate; }
            set { m_CurrentRate = value; }
        }
       
        public string BuySell
        {
            get { return m_BuySell; }
            set { m_BuySell = value; }
        }

        #endregion properties


    }
}
