using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoAdvisorProfiling
{
    /// <summary>
    /// Class Containing Advisors LOB Details.
    /// </summary>
    public class AdvisorLOBVo
    {
#region Fields

        private int m_LOBId;
        private Int32 m_AdviserId;

        private string m_LOBClassificationCode;
        private string m_LOBClassificationType;
        private string m_IdentifierTypeCode;             
        private DateTime m_ValidityDate;
        private string m_LicenseNumber;
        private string m_OrganizationName;
        private string m_LOBSegment;
        private string m_BuisnessType;
        private string m_AssetClass;
        private string m_Identifier;
        private double m_TargetAmount;
        private float m_TargetAccount;
        private double m_TargetPremiumAmount;
        private string m_AgentType;
        private string m_AgentNum;
        private string m_BrokerCode;
        private Int16 m_IsDependent;
      
        
#endregion Fields

#region Properties
        public string LOBClassificationType
        {
            get { return m_LOBClassificationType; }
            set { m_LOBClassificationType = value; }
        }
        public Int16 IsDependent
        {
            get { return m_IsDependent; }
            set { m_IsDependent = value; }
        }

      
        public string BrokerCode
        {
            get { return m_BrokerCode; }
            set { m_BrokerCode = value; }
        }
        public double TargetPremiumAmount
        {
            get { return m_TargetPremiumAmount; }
            set { m_TargetPremiumAmount = value; }
        }
        public string AgentNum
        {
            get { return m_AgentNum; }
            set { m_AgentNum = value; }
        }
        public string AgentType
        {
            get { return m_AgentType; }
            set { m_AgentType = value; }
        }
        public float TargetAccount
        {
            get { return m_TargetAccount; }
            set { m_TargetAccount = value; }
        }
        public double TargetAmount
        {
            get { return m_TargetAmount; }
            set { m_TargetAmount = value; }
        }
        public string Identifier
        {
            get { return m_Identifier; }
            set { m_Identifier = value; }
        }

        public Int32 AdviserId
        {
            get { return m_AdviserId; }
            set { m_AdviserId = value; }
        }

        public int LOBId
        {
            get { return m_LOBId; }
            set { m_LOBId = value; }
        }

        public string LOBClassificationCode
        {
            get { return m_LOBClassificationCode; }
            set { m_LOBClassificationCode = value; }
        }
        
        public DateTime ValidityDate
        {
            get { return m_ValidityDate; }
            set { m_ValidityDate = value; }
        }

        public string IdentifierTypeCode
        {
            get { return m_IdentifierTypeCode; }
            set { m_IdentifierTypeCode = value; }
        }
        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public string OrganizationName
        {
            get { return m_OrganizationName; }
            set { m_OrganizationName = value; }
        }


        public string LOBSegment
        {
            get { return m_LOBSegment; }
            set { m_LOBSegment = value; }
        }

        public string BuisnessType
        {
            get { return m_BuisnessType; }
            set { m_BuisnessType = value; }
        }

        public string AssetClass
        {
            get { return m_AssetClass; }
            set { m_AssetClass = value; }
        }


#endregion properties


    }
}
