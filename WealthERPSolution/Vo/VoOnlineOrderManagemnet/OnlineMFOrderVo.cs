﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoOnlineOrderManagemnet
{
    public class OnlineMFOrderVo:OnlineOrderVo
    {   
        public  int SchemePlanCode{set;get;}
        public double Amount { set; get; }
        public  int AccountId{set;get;}
        public  string SystematicTypeCode{set;get;}
        public  DateTime StartDate{set;get;}
        public  DateTime EndDate{set;get;}
        public  int SystematicDate{set;get;}            
        public  string SystematicDates{set;get;}
        public string DividendType { set; get; }
        public string TransactionType { set; get; }
        public string FrequencyCode { set; get; }
        public Double Redeemunits { set; get; }
        public Double RedeemAmount { set; get; }

    }
}
