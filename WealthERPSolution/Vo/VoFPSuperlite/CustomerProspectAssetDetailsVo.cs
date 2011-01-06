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
        private double? FP_Premium;
        private DateTime? FP_MaturityDate;



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
        public double AdjustedValue
        {
            get { return FP_AdjustedValue; }
            set { FP_AdjustedValue = value; }
        }
    }
}
