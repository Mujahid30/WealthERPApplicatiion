using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VOFPUtilityUser
{
  public  class FPUserVo
    {
        public string UserName { get; set; }
        public string Pan { get; set; }
        public string EMail { get; set; }
        public long MobileNo { get; set; }
        public Int32 UserId { get; set; }
        public Int32 C_CustomerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsClientExists { get; set; }
        public bool IsProspectmarked { get; set; }
        public string RiskClassCode { get; set; }
        public DateTime DOB { get; set; }
    }
}
