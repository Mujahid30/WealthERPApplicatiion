using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoOnlineOrderManagemnet
{
    public class OnlineIPOOrderVo : OnlineOrderVo
    {
        public bool IsCutOffApplicable { set; get; }
        public int IPOIssueBidQuantity { set; get; }
        public double IPOIssueBidPrice { set; get; }
        public double IPOIssueBidAmount { set; get; }

        public string CustomerName { set; get; }
        public string CustomerPAN { set; get; }
        public string CustomerType { set; get; }
        public int CustomerSubTypeId { set; get; }
        public string DematBeneficiaryAccountNum { set; get; }
        public string DematDepositoryName { set; get; }
        public string DematDPId { set; get; }
        public string AgentNo { set; get; }
        public int AgentId { set; get; }
        
        


    }
}
