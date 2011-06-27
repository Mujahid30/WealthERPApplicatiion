using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class EQSpeculativeVo
    {
        #region Fields

        private int m_CustomerId;
        private int m_AccountId;
        private int m_ScripCode;
        private string m_BrokerCode;
        private string m_Date;
        private List<EQTransactionVo> m_EQTransactionVoList;
        private string m_Exchange;

        

        #endregion Fields

        #region Properties

        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }

        public int AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }

        public int ScripCode
        {
            get { return m_ScripCode; }
            set { m_ScripCode = value; }
        }

        public string BrokerCode
        {
            get { return m_BrokerCode; }
            set { m_BrokerCode = value; }
        }


        public string Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        public List<EQTransactionVo> EQTransactionVoList
        {
            get { return m_EQTransactionVoList; }
            set { m_EQTransactionVoList = value; }
        }

        public string Exchange
        {
            get { return m_Exchange; }
            set { m_Exchange = value; }
        }

        #endregion Properties
    }
}
