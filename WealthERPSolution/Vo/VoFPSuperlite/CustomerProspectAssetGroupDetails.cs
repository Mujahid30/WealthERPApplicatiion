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
        private double FP_Value;
               

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
    }
}
