using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoOnlineOrderManagemnet
{
  
        public class OnlineMFSchemeDetailsVo
        {
            public int mornigStar { get; set; }
            public string schemeName { get; set; }
            public string amcName { get; set; }
            public string category { get; set; }
            public string fundManager { get; set; }
            public string schemeBanchMark { get; set; }
            public int fundReturn3rdYear { get; set; }
            public int fundReturn5thtYear { get; set; }
            public int fundReturn10thYear { get; set; }
            public string benchmarkReturn1stYear { get; set; }
            public string benchmark3rhYear { get; set; }
            public string benchmark5thdYear { get; set; }
            public DateTime navDate { get; set; }
            public decimal NAV { get; set; }
            public int minmumInvestmentAmount { get; set; }
            public int multipleOf { get; set; }
            public int minSIPInvestment { get; set; }
            public int SIPmultipleOf { get; set; }
            public int exitLoad { get; set; }
            public int schemePlanCode { get; set; }
            public int SchemeRating3Year { get; set; }
            public int SchemeRating5Year { get; set; }
            public int SchemeRating10Year { get; set; }
            public string SchemeRisk3Year { get; set; }
            public string SchemeRisk5Year { get; set; }
            public string SchemeRisk10Year { get; set; }
            public string SchemeReturn3Year { get; set; }
            public string SchemeReturn5Year { get; set; }
            public string SchemeReturn10Year { get; set; }
            public int schemeBox { get; set; }
            public int isSIPAvaliable { get; set; }
            public int isRedeemAvaliable { get; set; }
            public int isPurchaseAvaliable { get; set; }

       
    }
}
