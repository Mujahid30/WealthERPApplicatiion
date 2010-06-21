using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class MFPortfolioSellTransactionVo
    {
        private string m_TranscationType;

        public string TranscationType
        {
            get { return m_TranscationType; }
            set { m_TranscationType = value; }
        }
        private double m_Units;

        public double Units
        {
            get { return m_Units; }
            set { m_Units = value; }
        }
        private DateTime m_SellDate;

        public DateTime SellDate
        {
            get { return m_SellDate; }
            set { m_SellDate = value; }
        }
        private double m_SellPrice;

        public double SellPrice
        {
            get { return m_SellPrice; }
            set { m_SellPrice = value; }
        }
        private double m_STT;

        public double STT
        {
            get { return m_STT; }
            set { m_STT = value; }
        }
        private string m_TransactionClassificationCode;

        public string TransactionClassificationCode
        {
            get { return m_TransactionClassificationCode; }
            set { m_TransactionClassificationCode = value; }
        }
            
    }
}
