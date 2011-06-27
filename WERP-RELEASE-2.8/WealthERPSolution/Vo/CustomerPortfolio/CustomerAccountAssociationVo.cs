using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class CustomerAccountAssociationVo
    {
        #region fields

        private int m_AccountAssociationId;
        private int m_AssociationId;        
        private string m_AssociationType;
        private int m_AccountId;
        private int m_CustomerId;
        private int m_NomineeShare;     
        

        #endregion fields

        #region properties

        public int AssociationId
        {
            get { return m_AssociationId; }
            set { m_AssociationId = value; }
        }
        public string AssociationType
        {
            get { return m_AssociationType; }
            set { m_AssociationType = value; }
        }
        public int AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }
        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }
        public int AccountAssociationId
        {
            get { return m_AccountAssociationId; }
            set { m_AccountAssociationId = value; }
        }
        public int NomineeShare
        {
            get { return m_NomineeShare; }
            set { m_NomineeShare = value; }
        }

        #endregion properties

    }
}
