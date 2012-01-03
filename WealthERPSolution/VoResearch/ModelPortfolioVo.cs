using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoResearch
{
    public class ModelPortfolioVo
    {
        #region Fields

        private string m_PortfolioName;
        private double m_MinAUM;
        private double m_MaxAUM;
        private int m_MinAge;
        private int m_MaxAge;
        private int m_MinTimeHorizon;
        private int m_MaxTimeHorizon;
        private string m_VariantDescription;
        private decimal m_DebtAllocation;
        private decimal m_CashAllocation;
        private decimal m_EquityAllocation;
        private decimal m_AlternateAllocation;
        private decimal m_ROR;
        private decimal m_RiskPercentage;


        private int m_LowerScore;
        private int m_UpperScore;
        private int m_AvilableScoreRange;
        private string m_Description;
        private string m_BasisId;
        private int m_ModelPortfolioCode;
        private int m_AMPBU_Id;
        private string m_RiskClassCode;

        private int m_SchemeCode;
        private decimal m_Weightage;
        private int m_ArchiveReason;
        private DateTime m_StartDate;
        private DateTime m_EndDate;
        private Int16 s_IsActiveScheme;
        private string m_SchemeDescription;

        #endregion Fields


        #region Properties

        public string PortfolioName
        {
            get { return m_PortfolioName; }
            set { m_PortfolioName = value; }
        }

        public double MinAUM
        {
            get { return m_MinAUM; }
            set { m_MinAUM = value; }
        }

        public double MaxAUM
        {
            get { return m_MaxAUM; }
            set { m_MaxAUM = value; }
        }

        public int MinAge
        {
            get { return m_MinAge; }
            set { m_MinAge = value; }
        }

        public int MaxAge
        {
            get { return m_MaxAge; }
            set { m_MaxAge = value; }
        }

        public int MinTimeHorizon
        {
            get { return m_MinTimeHorizon; }
            set { m_MinTimeHorizon = value; }
        }

        public int MaxTimeHorizon
        {
            get { return m_MaxTimeHorizon; }
            set { m_MaxTimeHorizon = value; }
        }

        public decimal DebtAllocation
        {
            get { return m_DebtAllocation; }
            set { m_DebtAllocation = value; }
        }
        public decimal CashAllocation
        {
            get { return m_CashAllocation; }
            set { m_CashAllocation = value; }
        }
        public decimal EquityAllocation
        {
            get { return m_EquityAllocation; }
            set { m_EquityAllocation = value; }
        }
        public decimal AlternateAllocation
        {
            get { return m_AlternateAllocation; }
            set { m_AlternateAllocation = value; }
        }

        public decimal RiskPercentage
        {
            get { return m_RiskPercentage; }
            set { m_RiskPercentage = value; }
        }

        public decimal ROR
        {
            get { return m_ROR; }
            set { m_ROR = value; }
        } 

        public string VariantDescription
        {
            get { return m_VariantDescription; }
            set { m_VariantDescription = value; }
        }

        public int LowerScore
        {
            get { return m_LowerScore; }
            set { m_LowerScore = value; }
        }
        public int UpperScore
        {
            get { return m_UpperScore; }
            set { m_UpperScore = value; }
        }
        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }
        public int AvilableScoreRange
        {
            get { return m_AvilableScoreRange; }
            set { m_AvilableScoreRange = value; }
        }

        public string BasisId
        {
            get { return m_BasisId; }
            set { m_BasisId = value; }
        }

        public int ModelPortfolioCode
        {
            get { return m_ModelPortfolioCode; }
            set { m_ModelPortfolioCode = value; }
        }
        public int AMPBU_Id
        {
            get { return m_AMPBU_Id; }
            set { m_AMPBU_Id = value; }
        }
        public string RiskClassCode
        {
            get { return m_RiskClassCode; }
            set { m_RiskClassCode = value; }
        }
        public int SchemeCode
        {
            get { return m_SchemeCode; }
            set { m_SchemeCode = value; }
        }
        public int ArchiveReason
        {
            get { return m_ArchiveReason; }
            set { m_ArchiveReason = value; }
        }
        public decimal Weightage
        {
            get { return m_Weightage; }
            set { m_Weightage = value; }
        }
        public DateTime StartDate
        {
            get { return m_StartDate; }
            set { m_StartDate = value; }
        }
        public DateTime EndDate
        {
            get { return m_EndDate; }
            set { m_EndDate = value; }
        }
        public Int16 IsActiveScheme
        {
            get { return s_IsActiveScheme; }
            set { s_IsActiveScheme = value; }
        }
        public string SchemeDescription
        {
            get { return m_SchemeDescription; }
            set { m_SchemeDescription = value; }
        }
        #endregion Properties

    }
}
