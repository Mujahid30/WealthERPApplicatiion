using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VoOnlineOrderManagemnet
{
    public class RTAExtractHeadeInfoVo
    {
        public string HeaderName { get; set; }
        public int HeaderSequence { get; set; }
        public string WerpColumnName { get; set; }
        public string DataType { get; set; }
        public int MaxLength { get; set; }
        public bool IsNullable { get; set; }
    }

    public class WERPlookupCodeValueManagementVo
    {
        public int LookupID { get; set; }
        public string ExternalName { get; set; }
        public int CategoryID { get; set; }
        public string WerpCode { get; set; }
        public string WerpName { get; set; }
        public int MapID { get; set; }
        public string SourceCode { get; set; }
        public string ExternalCode { get; set; }

    }

    public class MFProductAMCSchemePlanDetailsVo
    {

        public int AMCCode { get; set; }
        public int SchemePlanCode { get; set; }
        public string SchemePlanName { get; set; }
        public string AssetSubSubCategory { get; set; }
        public string AssetSubCategoryCode { get; set; }
        public string AssetCategoryCode { get; set; }
        public string Product { get; set; }
        public string Status { get; set; }
        public int IsOnline { get; set; }
        public int IsDirect { get; set; }
        public double FaceValue { get; set; }
        public string SchemeType { get; set; }
        public string SchemeOption { get; set; }
        public string DividendFrequency { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Branch { get; set; }
        public int IsNFO { get; set; }
        public DateTime NFOStartDate { get; set; }
        public DateTime NFOEndDate { get; set; }
        public int LockInPeriod { get; set; }
        public TimeSpan CutOffTime { get; set; }
        public double EntryLoadPercentag { get; set; }
        public string EntryLoadRemark { get; set; }
        public double ExitLoadPercentage { get; set; }
        public string ExitLoadRemark { get; set; }
        public int IsPurchaseAvailable { get; set; }
        public int IsRedeemAvailable { get; set; }
        public int IsSIPAvailable { get; set; }
        public int IsSWPAvailable { get; set; }
        public int IsSwitchAvailable { get; set; }
        public int IsSTPAvailable { get; set; }
        public double InitialPurchaseAmount { get; set; }
        public double InitialMultipleAmount { get; set; }
        public double AdditionalPruchaseAmount { get; set; }
        public double AdditionalMultipleAmount { get; set; }
        public double MinRedemptionAmount { get; set; }
        public double RedemptionMultipleAmount { get; set; }
        public double MinRedemptionUnits { get; set; }
        public double RedemptionMultiplesUnits { get; set; }
        public double MinSwitchAmount { get; set; }
        public double SwitchMultipleAmount { get; set; }
        public int MinSwitchUnits { get; set; }
        public int SwitchMultiplesUnits { get; set; }
        public string GenerationFrequency { get; set; }
        public string SourceCode { get; set; }
        public string CustomerSubTypeCode { get; set; }
        public string SecurityCode { get; set; }
        public double PASPD_MaxInvestment { get; set; }
        public int WCMV_Lookup_BankId { get; set; }
        public string ExternalCode { get; set; }
        public string ExternalType { get; set; }
        public string Dividendfreq { get; set; }
        public string StartDate { get; set; }
        public string Frequency { get; set; }
        public int MinDues { get; set; }
        public int MaxDues { get; set; }
        public double MinAmount { get; set; }
        public double MultipleAmount { get; set; }
        public string SystematicCode { get; set; }
        public int Bankcode { get; set; }
        public string productcode { get; set; }
        public int Mergecode { get; set; }
        public string Allproductcode { get; set; }
        public string AMFIcode { get; set; }
        public DateTime SchemeStartDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public int IsOnlineEnablement { get; set; }
        public string ISINNo { get; set; }
    }

    public class TradeBusinessDateVo
    {
        public int TradeBusinessId { get; set; }
        public DateTime TradeBusinessDate { get; set; }
        public DateTime TradeBusinessExecutionDate { get; set; }
        public int IsTradeBusinessDateHoliday { get; set; }
        public int IsTradeBusinessDateWeekend { get; set; }
        public int year { get; set; }
        public DateTime date { get; set; }
        public string HolidayName { get; set; }
    }
}
