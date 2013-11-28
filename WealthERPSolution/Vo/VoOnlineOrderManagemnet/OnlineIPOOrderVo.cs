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

    }
}
