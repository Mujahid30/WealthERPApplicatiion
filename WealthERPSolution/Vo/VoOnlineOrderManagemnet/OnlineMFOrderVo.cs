using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoOnlineOrderManagemnet
{
    public class OnlineMFOrderVo:OnlineOrderVo
    {   
        public  int SchemePlanCode{set;get;}
        public double Amount { set; get; }

    }
}
