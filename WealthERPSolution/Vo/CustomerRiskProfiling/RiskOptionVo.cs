using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoCustomerRiskProfiling
{
    [Serializable]
    public class RiskOptionVo
    {
        private string m_Option;

        public string Option
        {
            get { return m_Option; }
            set { m_Option = value; }
        }
        private int m_Value;

        public int Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
            

    }

    /// <summary>
    /// Adviser dynamic risk profile Questions and options Encapsulation vo..
    /// Added by Vinayak Patil..
    /// </summary>
    
    public class AdviserDynamicRiskQuestionsVo
    {
        #region Fields
        // Questions Vo 
        private int m_QuestionId;
        private string m_Question;
        private string m_Purpose;
        private int m_AdviserId;

        // Option Vo
        private int m_OptionId;
        private string m_Option;
        private int m_Weightage;

        #endregion Fields


        #region Properties
        public int QuestionId
        {
            get { return m_QuestionId; }
            set { m_QuestionId = value; }
        }
        public int AdviserId
        {
            get { return m_AdviserId; }
            set { m_AdviserId = value; }
        }
        public int OptionId
        {
            get { return m_OptionId; }
            set { m_OptionId = value; }
        }
        public int Weightage
        {
            get { return m_Weightage; }
            set { m_Weightage = value; }
        }

        public string Question
        {
            get { return m_Question; }
            set { m_Question = value; }
        }
        public string Purpose
        {
            get { return m_Purpose; }
            set { m_Purpose = value; }
        }
        public string Option
        {
            get { return m_Option; }
            set { m_Option = value; }
        }
        #endregion Properties
    }
}
