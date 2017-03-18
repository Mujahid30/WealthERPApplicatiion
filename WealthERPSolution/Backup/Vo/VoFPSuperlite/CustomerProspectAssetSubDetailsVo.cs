﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoFPSuperlite
{
    public class CustomerProspectAssetSubDetailsVo
    {
        private int FP_SubInstrumentDetailsId;        
        private string FP_AssetGroupCode;       
        private string FP_AssetInstrumentCategoryCode;       
        private string FP_AssetInstrumentSubCategoryCode;
        private double FP_AdjustedValue;
        private double FP_Value;
        private double? FP_Premium;
        private double FP_AdjustedPremium;
        private double FP_TotalPremiumValue;
        private DateTime? FP_MaturityDate;

        public int SubInstrumentDetailsId
        {
            get { return FP_SubInstrumentDetailsId; }
            set { FP_SubInstrumentDetailsId = value; }
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
        public string AssetInstrumentSubCategoryCode
        {
            get { return FP_AssetInstrumentSubCategoryCode; }
            set { FP_AssetInstrumentSubCategoryCode = value; }
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
        public double AdjustedPremium
        {
            get { return FP_AdjustedPremium; }
            set { FP_AdjustedPremium = value; }
        }
        public double TotalPremiumValue
        {
            get { return FP_TotalPremiumValue; }
            set { FP_TotalPremiumValue = value; }
        }
    }
}