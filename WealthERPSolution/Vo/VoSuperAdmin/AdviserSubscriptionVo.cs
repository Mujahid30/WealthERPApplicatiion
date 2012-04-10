using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoSuperAdmin
{
    public class AdviserSubscriptionVo
    {

        public int AdviserId { get; set; }
        public int SubscriptionId { get; set; }
        public int PlanId { get; set; }
        public string SubscriptionType { get; set; }
        public DateTime? TrialStartDate { get; set; }
        public DateTime? TrialEndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int NoOfBranches { get; set; }
        public int NoOfStaffLogins { get; set; }
        public int NoOfCustomerLogins { get; set; }
        public int SmsBought { get; set; }
        public int SmsSent { get; set; }
        public int SmsRemaining { get; set; }
        public string Comments { get; set; }
        public string CustomPlanSelection { get; set; }
        public int IsDeActivated { get; set; }
        public DateTime DeActivationDate { get; set; }
        public int IsActive { get; set; }
        public string FlavourCategory { get; set; }

    }
}
