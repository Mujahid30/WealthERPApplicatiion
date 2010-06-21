using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoAdvisorProfiling
{
    public class AssociateCategoryVo
    {
        #region Fields
        private int m_AdviserId;
        private string m_AssociateCategoryCode;
        private string m_AssociateCategoryName;
        private int m_AssociateCategoryId;
        private int m_CreatedBy;
        private DateTime m_CreatedOn;
        private int m_ModifiedBy;
        private DateTime m_Modifiedon;

	#endregion
        #region Properties
         public int AdviserId
        {
            get { return m_AdviserId; }
            set { m_AdviserId = value; }
        }
        

        public string AssociateCategoryCode
        {
            get { return m_AssociateCategoryCode; }
            set { m_AssociateCategoryCode = value; }
        }
      

        public string AssociateCategoryName
        {
            get { return m_AssociateCategoryName; }
            set { m_AssociateCategoryName = value; }
        }

        public int AssociateCategoryId
        {
            get { return m_AssociateCategoryId; }
            set { m_AssociateCategoryId = value; }
        }

        public int CreatedBy
        {
            get { return m_CreatedBy; }
            set { m_CreatedBy = value; }
        }

        public DateTime CreatedOn
        {
            get { return m_CreatedOn; }
            set { m_CreatedOn = value; }
        }

        public int ModifiedBy
        {
            get { return m_ModifiedBy; }
            set { m_ModifiedBy = value; }
        }

        public DateTime Modifiedon
        {
            get { return m_Modifiedon; }
            set { m_Modifiedon = value; }
        }
	#endregion
       

    }
}
