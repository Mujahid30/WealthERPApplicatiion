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
        private DateTime m_DOB;
        private string m_RelationshipCode;
        private string m_FirstName;
        private string m_MiddleName;
        private string m_LastName;
        private string m_EmailId;
        private string m_PanNo;



        #endregion Fields



        #region Properties
        public string EmailId
        {
            get { return m_EmailId; }
            set { m_EmailId = value; }
        }
        public DateTime DOB
        {
            get { return m_DOB; }
            set { m_DOB = value; }
        }
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
        public string RelationshipCode
        {
            get { return m_RelationshipCode; }
            set { m_RelationshipCode = value; }
        }
        public string FirstName
        {
            get { return m_FirstName; }
            set { m_FirstName = value; }
        }
        public string MiddleName
        {
            get { return m_MiddleName; }
            set { m_MiddleName = value; }
        }
        public string LastName
        {
            get { return m_LastName; }
            set { m_LastName = value; }
        }
        public string PanNo
        {
            get { return m_PanNo; }
            set { m_PanNo = value; }
        }
        #endregion Properties
    }
}
