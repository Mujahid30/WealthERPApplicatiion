using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoAdvisorProfiling
{
    public class AdviserLoanCommsnStrucWithLoanPartnerVo
    {
        private int m_Id;
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private int m_LoanSchemeId;
        public int LoanSchemeId
        {
            get { return m_LoanSchemeId; }
            set { m_LoanSchemeId = value; }
        }

        private int m_LoanPartnerCode;
        public int LoanPartnerCode
        {
            get { return m_LoanPartnerCode; }
            set { m_LoanPartnerCode = value; }
        }

        private int m_LoanTypeCode;
        public int LoanTypeCode
        {
            get { return m_LoanTypeCode; }
            set { m_LoanTypeCode = value; }
        }

        private int m_AdviserId;
        public int AdviserId
        {
            get { return m_AdviserId; }
            set { m_AdviserId = value; }
        }

        private float m_SlabUpperLimit;
        public float SlabUpperLimit
        {
            get { return m_SlabUpperLimit; }
            set { m_SlabUpperLimit = value; }
        }

        private float m_SlabLowerLimit;
        public float SlabLowerLimit
        {
            get { return m_SlabLowerLimit; }
            set { m_SlabLowerLimit = value; }
        }

        private DateTime m_StartDate;
        public DateTime StartDate
        {
            get { return m_StartDate; }
            set { m_StartDate = value; }
        }

        private DateTime m_EndDate;
        public DateTime EndDate
        {
            get { return m_EndDate; }
            set { m_EndDate = value; }
        }

        private float m_CommissionFee;

        public float CommissionFee
        {
            get { return m_CommissionFee; }
            set { m_CommissionFee = value; }
        }

        private DateTime m_CreatedOn;
        public DateTime CreatedOn
        {
            get { return m_CreatedOn; }
            set { m_CreatedOn = value; }
        }

        private int m_CreatedBy;
        public int CreatedBy
        {
            get { return m_CreatedBy; }
            set { m_CreatedBy = value; }
        }

        private DateTime m_ModifiedOn;
        public DateTime ModifiedOn
        {
            get { return m_ModifiedOn; }
            set { m_ModifiedOn = value; }
        }

        private int m_ModifiedBy;
        public int ModifiedBy
        {
            get { return m_ModifiedBy; }
            set { m_ModifiedBy = value; }
        }

    }
}
