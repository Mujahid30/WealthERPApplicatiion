using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoOnlineOrderManagemnet
{
    public class OnlineMFOrderVo : OnlineOrderVo
    {
        public int SchemePlanCode { set; get; }
        public double Amount { set; get; }
        public int AccountId { set; get; }
        public string SystematicTypeCode { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public int SystematicDate { set; get; }
        public string SystematicDates { set; get; }
        public string DividendType { set; get; }
        public string TransactionType { set; get; }
        public string FrequencyCode { set; get; }
        public Double Redeemunits { set; get; }
        public Double RedeemAmount { set; get; }
        public string Action { set; get; }
        public string DivFrequencyCode { set; get; }
        public string DivFrequencyName { set; get; }
        public string DivOption { set; get; }
        public string Category { set; get; }
        public string Folio { set; get; }
        public Double UnitsHeld { set; get; }
        public int TotalInstallments { set; get; }
        public int CurrentInstallments { set; get; }
        public int MinDues { set; get; }
        public int MaxDues { set; get; }
        public bool IsAllUnits { set; get; }
        public string SWPRedeemValueType { set; get; }
        public string ModeTypeCode { set; get; }
        public string BSESchemeCode { set; get; }
        public int OrderType { get; set; }
        public int MandateId { get; set; }
        public int SystematicId { set; get; }
       
    }

    public class BSEMFSIPOdererVo
    {
        public string Transactioncode { set; get; }
        public Int32 BSEOrderId { set; get; }
        public string UniqueReferanceNumber { set; get; }
        public string SchemeCode { set; get; }
        public string MemberId { set; get; }
        public string ClientCode { set; get; }
        public string BSEUserId { set; get; }
        public string InternalReferenceNo { set; get; }
        public string TransMode { set; get; }
        public string DPTransactionMode { set; get; }
        public string StartDate { set; get; }
        public string FrequenceType { set; get; }
        public string FrequenceAllowed { set; get; }
        public string InstallmentType { set; get; }
        public string InstallmentAmount { set; get; }
        public string NoOfInstallments { set; get; }
        public string Remarks { set; get; }
        public string FolioNo { set; get; }
        public string FirstOrderFlag { set; get; }
        public string SubBRCode { set; get; }
        public string EUIN { set; get; }
        public string EUINDeclarationFlag { set; get; }
        public string DPC { set; get; }
        public string REGID { set; get; }
        public string IPAddress { set; get; }
        public string Password { set; get; }
        public string PassKey { set; get; }
        public string Param1 { set; get; }
        public string Param2 { set; get; }
        public string Param3 { set; get; }
        public string CreatedBy { set; get; }
        public DateTime CreatedOn { set; get; }
        public int ModifiedBy { set; get; }
        public DateTime ModifiedOn { set; get; }
        public int SystematicSetupId { set; get; }
        public string MandateId { set; get; }

    }
}
