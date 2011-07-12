using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoFPSuperlite
{
    public class CustomerProspectAssetGroupDetails
    {
        private int FP_AssetGroupId;
        private string FP_AssetGroupCode;
        private double FP_AdjustedValue;       
        private double FP_Value;
        private double FP_AdjustedPremiumValue;
        private double FP_TotalPremiumValue;
               

        public int AssetGroupId
        {
            get { return FP_AssetGroupId; }
            set { FP_AssetGroupId = value; }
        }

        public string AssetGroupCode
        {
            get { return FP_AssetGroupCode; }
            set { FP_AssetGroupCode = value; }
        }
        public double Value
        {
            get { return FP_Value; }
            set { FP_Value = value; }
        }
        public double AdjustedValue
        {
            get { return FP_AdjustedValue; }
            set { FP_AdjustedValue = value; }
        }

        public double AdjustedPremiumValue
        {
            get { return FP_AdjustedPremiumValue; }
            set { FP_AdjustedPremiumValue = value; }
        }

        public double TotalPremiumValue
        {
            get { return FP_TotalPremiumValue; }
            set { FP_TotalPremiumValue = value; }
        }
    }
}
