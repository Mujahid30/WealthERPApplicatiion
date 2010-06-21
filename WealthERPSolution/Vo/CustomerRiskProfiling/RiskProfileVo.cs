using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerRiskProfiling
{
    class RiskProfileVo
    {
        public int _customerId;

        public int CustomerId { get; set; }


        public int _riskScore;

        public int RiskScore { get; set; }


        public string _riskClassCode;

        public string RiskClassCode { get; set; }


        public DateTime _riskDate;

        public DateTime RiskDate { get; set; }
    }
}
