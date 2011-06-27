using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class EditLogVo
    {
        #region Fields

        private int m_LiabilitiesEditLogId;
        private int m_LiabilitiesId;
        private int m_EditTypeCode;
        private DateTime m_EditOccurenceDate;
        private double m_EditAmount;
        private string m_ReferenceNum;
        private string m_Remark;

      
        private int m_CreatedBy;
        private int m_ModifiedBy;

        #endregion Fields

        #region Properties

        public int LiabilitiesEditLogId
        {
            get { return m_LiabilitiesEditLogId; }
            set { m_LiabilitiesEditLogId = value; }
        }
        public string Remark
        {
            get { return m_Remark; }
            set { m_Remark = value; }
        }
        public int LiabilitiesId
        {
            get { return m_LiabilitiesId; }
            set { m_LiabilitiesId = value; }
        }
       
        public int EditTypeCode
        {
            get { return m_EditTypeCode; }
            set { m_EditTypeCode = value; }
        }
       
        public DateTime EditOccurenceDate
        {
            get { return m_EditOccurenceDate; }
            set { m_EditOccurenceDate = value; }
        }
     
        public double EditAmount
        {
            get { return m_EditAmount; }
            set { m_EditAmount = value; }
        }
      
        public string ReferenceNum
        {
            get { return m_ReferenceNum; }
            set { m_ReferenceNum = value; }
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
