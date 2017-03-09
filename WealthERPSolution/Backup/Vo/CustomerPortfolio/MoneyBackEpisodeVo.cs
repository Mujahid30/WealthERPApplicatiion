using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class MoneyBackEpisodeVo
    {
        #region Fields

        private DateTime m_CIMBE_RepaymentDate;       
        private float m_CIMBE_RepaidPer;       
        private int m_CIMBE_ModifiedBy;        
        private int m_CIMBE_CreatedBy;
        private int m_CIP_CustInsInvId;
        private int m_CIMBE_Id;

        #endregion Fields

        #region Properties

        public int CIMBE_ModifiedBy
        {
            get { return m_CIMBE_ModifiedBy; }
            set { m_CIMBE_ModifiedBy = value; }
        }
        public float CIMBE_RepaidPer
        {
            get { return m_CIMBE_RepaidPer; }
            set { m_CIMBE_RepaidPer = value; }
        }
        public DateTime CIMBE_RepaymentDate
        {
            get { return m_CIMBE_RepaymentDate; }
            set { m_CIMBE_RepaymentDate = value; }
        }
        public int CIMBE_CreatedBy
        {
            get { return m_CIMBE_CreatedBy; }
            set { m_CIMBE_CreatedBy = value; }
        }
        public int CustInsInvId
        {
            get { return m_CIP_CustInsInvId; }
            set { m_CIP_CustInsInvId = value; }
        }
        public int MoneyBackId
        {
            get { return m_CIMBE_Id; }
            set { m_CIMBE_Id = value; }
        }

        #endregion Properties

    }
}
