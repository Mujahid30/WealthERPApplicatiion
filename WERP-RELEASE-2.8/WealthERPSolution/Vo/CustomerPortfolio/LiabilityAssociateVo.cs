using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class LiabilityAssociateVo
    {

        #region Fields

        private int m_LiabilitiesAssociationId;
        private int m_LiabilitiesId;
        private string m_LoanAssociateCode;
        private float m_LiabilitySharePer; 
        private float m_MarginPer;
        private int m_AssociationId;
        private int m_ModifiedBy;
        private int m_CreatedBy;
        #endregion Fields

        #region Properties

        public int LiabilitiesAssociationId
        {
            get { return m_LiabilitiesAssociationId; }
            set { m_LiabilitiesAssociationId = value; }
        }


        public int LiabilitiesId
        {
            get { return m_LiabilitiesId; }
            set { m_LiabilitiesId = value; }
        }


        public string LoanAssociateCode
        {
            get { return m_LoanAssociateCode; }
            set { m_LoanAssociateCode = value; }
        }


        public float LiabilitySharePer
        {
            get { return m_LiabilitySharePer; }
            set { m_LiabilitySharePer = value; }
        }


        public float MarginPer
        {
            get { return m_MarginPer; }
            set { m_MarginPer = value; }
        }


        public int AssociationId
        {
            get { return m_AssociationId; }
            set { m_AssociationId = value; }
        }


        public int CreatedBy
        {
            get { return m_CreatedBy; }
            set { m_CreatedBy = value; }
        }
        public int ModifiedBy
        {
            get { return m_ModifiedBy; }
            set { m_ModifiedBy = value; }
        }

        #endregion Properties





    }
}
