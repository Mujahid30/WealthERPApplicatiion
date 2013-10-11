using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoOnlineOrderManagemnet
{
    public class OnlineOrderVo
    {
        public int OrderId { set; get; }
        public string AssetGroup { set; get; }
        public DateTime OrderDate { set; get; }
        public int OrderNumber { set; get; }
        public int CustomerId { set; get; }
        public string SourceCode { set; get; }
        public string ApplicationNumber { set; get; }
        public DateTime ApplicationReceivedDate { set; get; }
        public string PaymentMode { set; get; }
        public string ChequeNumber { set; get; }
        public DateTime PaymentDate { set; get; }
        public int CustBankAccId { set; get; }
        public string BankBranchName { set; get; }

        public string OrderStepCode { set; get; }
        public string OrderStatusCode { set; get; }
        public string ReasonCode { set; get; }
        public int ApprovedBy { set; get; }
        public int AssociationId { set; get; }
        public string AssociationType { set; get; }
        public int IsCustomerApprovalApplicable { set; get; }
        public int AgentId { get; set; }
        public string AgentCode { get; set; }


    }
}
