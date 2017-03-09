using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoAdvisorProfiling
{
    public class AdvisorAssociateCommissionVo
    {
        public int Id { set; get; }
        public int BranchId { set; get; }
        public float CommissionFee { set; get; }
        public double RevenueUpperlimit { set; get; }
        public double RevenueLowerlimit { set; get; }
        public string LOBAssetGroupsCode { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public int CreatedBy { set; get; }
        public int ModifiedBy { set; get; }
    }
}
