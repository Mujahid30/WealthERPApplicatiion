﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VoOnlineOrderManagemnet
{
    public class OnlineOrderBackOfficeVo
    {
        public string HeaderName { get; set; }
        public int HeaderSequence { get; set; }
        public string WerpColumnName { get; set; }
        public string DataType { get; set; }
        public int MaxLength { get; set; }
        public bool IsNullable { get; set; }

        public int LookupID { get; set; }        
        public string ExternalName { get; set; }
        public int CategoryID { get; set; }
        public string WerpCode { get; set; }
        public string WerpName { get; set; }
        public int MapID { get; set; }


       public int AMCCode { get; set; }
       public int SchemePlanCode { get; set; }
	   public string SchemePlanName { get; set; }
	   public string AssetSubSubCategory { get; set; }
	   public string AssetSubCategoryCode { get; set; }
	   public string AssetCategoryCode { get; set; }
	   public string Product  { get; set; }
	   public string Status { get; set; }
	   public int   IsOnline { get; set; }
	   public int   IsDirect { get; set; }
	   public double FaceValue { get; set; }
       public string SchemeType { get; set; }
       public string SchemeOption { get; set; }
       public string DividendFrequency { get; set; }
       public string  BankName { get; set; }
       public string AccountNumber  { get; set; }
       public string    Branch { get; set; }
       public int  IsNFO { get; set; }
       public DateTime  NFOStartDate  { get; set; }
       public DateTime    NFOEndDate { get; set; }
       public int  LockInPeriod { get; set; }
       public TimeSpan CutOffTime { get; set; }
       public double  EntryLoadPercentag { get; set; }
       public string   EntryLoadRemark { get; set; }
       public double   ExitLoadPercentage { get; set; }
       public string ExitLoadRemark { get; set; }
       public int    IsPurchaseAvailable { get; set; }
       public int   IsRedeemAvailable { get; set; }
       public int   IsSIPAvailable { get; set; }
       public int  IsSWPAvailable { get; set; }
       public int IsSwitchAvailable { get; set; }
       public int  IsSTPAvailable { get; set; }
       public double InitialPurchaseAmount { get; set; }
       public double InitialMultipleAmount  { get; set; }
       public double AdditionalPruchaseAmount { get; set; } 
       public double AdditionalMultipleAmount { get; set; }
       public double MinRedemptionAmount { get; set; }
       public double RedemptionMultipleAmount  { get; set; }
       public int   MinRedemptionUnits { get; set; }
       public int   RedemptionMultiplesUnits { get; set; }
       public double MinSwitchAmount { get; set; }
       public double SwitchMultipleAmount { get; set; }
       public int    MinSwitchUnits { get; set; }
       public int    SwitchMultiplesUnits { get; set; }
       public string  GenerationFrequency { get; set; }
       public string  SourceCode  { get; set; }
       public string  CustomerSubTypeCode { get; set; }
       public string  SecurityCode { get; set; }
       public double  PASPD_MaxInvestment { get; set; }
       public string    WERPBM_BankCode  { get; set; }
       public string    ExternalCode { get; set; }
       public string ExternalType { get; set; }
       public string Dividendfreq { get; set; }
      



    }
}
