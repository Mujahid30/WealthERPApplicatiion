using System;

namespace VoCustomerPortfolio
{
    public class AdviserDailyLOGVo
    {
        #region Fields
        
       
        private int m_EODLogId;
        private DateTime m_ProcessDate;
        private DateTime m_StartTime;
        private int m_IsValuationComplete;
        private int m_IsEquityCleanUpComplete;
        private DateTime m_EndTime;
        private int m_CreatedBy;
        private int m_ModifiedBy;
        private int m_AdviserId;
        private string m_AssetGroup;

       

        #endregion





        #region Properties
        public int EODLogId
        {
            get { return m_EODLogId; }
            set { m_EODLogId = value; }
        }

        public string AssetGroup
        {
            get { return m_AssetGroup; }
            set { m_AssetGroup = value; }
        }
        public DateTime ProcessDate
        {
            get { return m_ProcessDate; }
            set { m_ProcessDate = value; }
        }


        public DateTime StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }


        public int IsValuationComplete
        {
            get { return m_IsValuationComplete; }
            set { m_IsValuationComplete = value; }
        }


        public int IsEquityCleanUpComplete
        {
            get { return m_IsEquityCleanUpComplete; }
            set { m_IsEquityCleanUpComplete = value; }
        }


        public DateTime EndTime
        {
            get { return m_EndTime; }
            set { m_EndTime = value; }
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


        public int AdviserId
        {
            get { return m_AdviserId; }
            set { m_AdviserId = value; }
        }



        #endregion
	

    }
}
