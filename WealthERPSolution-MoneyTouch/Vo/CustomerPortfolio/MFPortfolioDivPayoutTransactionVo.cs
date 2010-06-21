using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class MFPortfolioDivPayoutTransactionVo
    {
        private DateTime m_TradeDate;

        public DateTime TradeDate
        {
            get { return m_TradeDate; }
            set { m_TradeDate = value; }
        }
        private double m_Amount;

        public double Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }
    }
}
