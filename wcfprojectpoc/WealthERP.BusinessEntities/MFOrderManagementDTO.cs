using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WealthERP.BusinessEntities
{
    /// <summary>
    ///   MF Order Management DTO
    /// </summary>
    /// <remarks>
    /// </remarks>
    [DataContract]
    [Serializable]
    public class MFOrderManagementDTO : OrderManagementDTO
    {
        [DataMember(Order = 0)]
        public int OrderDetailsId { get; set; }

        [DataMember(Order = 1)]
        public string CustomerName;

        [DataMember(Order = 2)]
        public int SchemePlanCode { get; set; }

        [DataMember(Order = 3)]
        public int OrderNumber { get; set; }

        [DataMember(Order = 4)]
        public double Amount { get; set; }

        [DataMember(Order = 5)]
        public string StatusCode { get; set; }

        [DataMember(Order = 6)]
        public string StatusReasonCode { get; set; }

        [DataMember(Order = 7)]
        public string TransactionCode { get; set; }

        [DataMember(Order = 8)]
        public int Accountid { get; set; }

        [DataMember(Order = 9)]
        public int IsImmediate { get; set; }

        [DataMember(Order = 10)]
        public string SourceCode { get; set; }

        [DataMember(Order = 11)]
        public string FutureTriggerCondition { get; set; }

        [DataMember(Order = 12)]
        public int PortfolioId { get; set; }

        [DataMember(Order = 13)]
        public DateTime FutureExecutionDate { get; set; }

        [DataMember(Order = 14)]
        public int SchemePlanSwitch { get; set; }

        [DataMember(Order = 15)]
        public string BankName { get; set; }

        [DataMember(Order = 16)]
        public string BranchName { get; set; }

        [DataMember(Order = 17)]
        public string AddrLine1 { get; set; }

        [DataMember(Order = 18)]
        public string m_AddrLine2 { get; set; }

        [DataMember(Order = 19)]
        public string AddrLine3 { get; set; }

        [DataMember(Order = 20)]
        public string City { get; set; }

        [DataMember(Order = 21)]
        public string State { get; set; }

        [DataMember(Order = 22)]
        public string Country { get; set; }

        [DataMember(Order = 23)]
        public string Pincode { get; set; }

        [DataMember(Order = 24)]
        public DateTime LivingSince { get; set; }

        [DataMember(Order = 25)]
        public int IsExecuted { get; set; }

        [DataMember(Order = 26)]
        public int AmcCode { get; set; }

        [DataMember(Order = 27)]
        public string CategoryCode { get; set; }

        [DataMember(Order = 28)]
        public string RmName { get; set; }

        [DataMember(Order = 29)]
        public string BMName { get; set; }

        [DataMember(Order = 30)]
        public double Units { get; set; }

        [DataMember(Order = 31)]
        public string FrequencyCode { get; set; }

        [DataMember(Order = 32)]
        public DateTime StartDate { get; set; }

        [DataMember(Order = 33)]
        public DateTime EndDate { get; set; }

        [DataMember(Order = 34)]
        public string PanNo { get; set; }

        [DataMember(Order = 35)]
        public string ARNNo { get; set; }

        [DataMember(Order = 36)]
        public int AssociateId { get; set; }

    }
}
