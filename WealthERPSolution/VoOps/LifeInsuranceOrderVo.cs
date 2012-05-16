using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoOps
{
    public class LifeInsuranceOrderVo : OrderVo
    {
        #region Fields
        
        private string m_GIIssuerCode;
        private double m_SumAssured;
        private string m_FrequencyCode;
        private DateTime m_MaturityDate;
        private string m_HoldingMode;
        private int m_IsJointlyHeld;
        private int m_InsuranceSchemeId;
        private string m_InsuranceIssuerCode;
      
        #endregion

        #region Properties

        public string GIIssuerCode
        {
            get { return m_GIIssuerCode; }
            set { m_GIIssuerCode = value; }
        }

        public double SumAssured
        {
            get { return m_SumAssured; }
            set { m_SumAssured = value; }
        }

        public string FrequencyCode
        {
            get { return m_FrequencyCode; }
            set { m_FrequencyCode = value; }
        }

        public DateTime MaturityDate
        {
            get { return m_MaturityDate; }
            set { m_MaturityDate = value; }
        }
        public int InsuranceSchemeId
        {
            get { return m_InsuranceSchemeId; }
            set { m_InsuranceSchemeId = value; }
        }
       
        public string HoldingMode
        {
            get { return m_HoldingMode; }
            set { m_HoldingMode = value; }
        }

        public int IsJointlyHeld
        {
            get { return m_IsJointlyHeld; }
            set { m_IsJointlyHeld = value; }
        }
               
        public string InsuranceIssuerCode
        {
            get { return m_InsuranceIssuerCode; }
            set { m_InsuranceIssuerCode = value; }
        }

        #endregion
    }
}
