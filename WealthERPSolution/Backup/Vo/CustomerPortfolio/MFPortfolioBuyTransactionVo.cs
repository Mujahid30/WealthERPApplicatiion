using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class MFPortfolioBuyTransactionVo
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
        private DateTime m_PurchaseDate;

        public DateTime PurchaseDate
        {
            get { return m_PurchaseDate; }
            set { m_PurchaseDate = value; }
        }
        private double m_PurchasePrice;

        public double PurchasePrice
        {
            get { return m_PurchasePrice; }
            set { m_PurchasePrice = value; }
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
