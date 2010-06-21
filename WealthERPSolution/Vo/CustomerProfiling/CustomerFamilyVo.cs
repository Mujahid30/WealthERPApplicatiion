using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerProfiling
{
    public class CustomerFamilyVo
    {
        #region Fields

        private int m_AssociationId;
        private int m_AssociateCustomerId;
        private string m_AssociateCustomerName;
        private int m_CustomerId;
        private string m_Relationship;

        #endregion Fields
         


        #region Properties

        public int AssociationId
        {
            get { return m_AssociationId; }
            set { m_AssociationId = value; }
        }
        public int AssociateCustomerId
        {
            get { return m_AssociateCustomerId; }
            set { m_AssociateCustomerId = value; }
        }
        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }
        public string AssociateCustomerName
        {
            get { return m_AssociateCustomerName; }
            set { m_AssociateCustomerName = value; }
        }
        public string Relationship
        {
            get { return m_Relationship; }
            set { m_Relationship = value; }
        }


        #endregion Properties
    }
}
