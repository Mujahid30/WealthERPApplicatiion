using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoOnlineOrderManagemnet
{
    public class OnlineBondOrderVo : OnlineOrderVo
    {

        public int PFISM_SchemeId { get; set; }
        public int PFISD_SeriesId { get; set; }

        public string PFIIM_IssuerId { get; set; }
        public int PFISD_Tenure { get; set; }
        public double PFISD_CouponRate { get; set; }
        public string PFISD_CouponFreq { get; set; }
        public string PFISD_RenewCouponRate { get; set; }

        public double AIM_FaceValue { get; set; }
        public string PFISD_DefaultInterestRate { get; set; }
        public string PFISD_YieldUpto { get; set; }
        public string PFISD_YieldatBuyBack { get; set; }
        public string PFISD_LockingPeriod { get; set; }
        public string PFISD_CallOption { get; set; }
        public string PFISD_BuyBackFacility { get; set; }
        public int Qty { get; set; }
        public string BrokerCode { get; set; }
        public double Amount { get; set; }
        public int BankAccid { get; set; }
        public int applicationNo { get; set; }
        public string Remarks { get; set; }
        public string PanNo { get; set; }
        public string AgentNo { get; set; }

    }
}
