using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoFPSuperlite
{
    public class CustomerProspectAssetDetailsVo
    {
        private int FP_InstrumentDetailsId;
        private string FP_AssetGroupCode;
        private string FP_AssetInstrumentCategoryCode;
        private double FP_AdjustedValue;       
        private double FP_Value;
        private double FP_SurrMktVal;
        private double? FP_Premium;
        private double FP_AdjustedPremium;
        private double FP_TotalPremiumValue;
        private DateTime? FP_MaturityDate;
        private double FP_TotalSurrMkt;
        private double FP_AdjustedSurrMkt;

        public double AdjustedSurrMkt
        {
            get { return FP_AdjustedSurrMkt; }
            set { FP_AdjustedSurrMkt = value; }
        }

        public double TotalSurrMkt
        {
            get { return FP_TotalSurrMkt; }
            set { FP_TotalSurrMkt = value; }
        }      

        public int InstrumentDetailsId
        {
            get { return FP_InstrumentDetailsId; }
            set { FP_InstrumentDetailsId = value; }
        }
        public string AssetGroupCode
        {
            get { return FP_AssetGroupCode; }
            set { FP_AssetGroupCode = value; }
        }
        public string AssetInstrumentCategoryCode
        {
            get { return FP_AssetInstrumentCategoryCode; }
            set { FP_AssetInstrumentCategoryCode = value; }
        }
        public double Value
        {
            get { return FP_Value; }
            set { FP_Value = value; }
        }
        public DateTime? MaturityDate
        {
            get { return FP_MaturityDate; }
            set { FP_MaturityDate = value; }
        }
        public double? Premium
        {
            get { return FP_Premium; }
            set { FP_Premium = value; }
        }
        public double AdjustedPremium
        {
            get { return FP_AdjustedPremium; }
            set { FP_AdjustedPremium = value; }
        }
        public double AdjustedValue
        {
            get { return FP_AdjustedValue; }
            set { FP_AdjustedValue = value; }
        }

        public double TotalPremiumValue
        {
            get { return FP_TotalPremiumValue; }
            set { FP_TotalPremiumValue = value; }
        }

        public double SurrMktVal
        {
            get { return FP_SurrMktVal; }
            set { FP_SurrMktVal = value; }
        }

        
    }
}
