using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VOAssociates
{
    public class AssociatesUserHeirarchyVo
    {
        #region Fields
        
        public int UserId { get; set; }
        public int RMId { get; set; }
        public int AdviserAgentId { get; set; }
        public string AgentCode { get; set; }
        public string UserTitle { get; set; }
       // public int AssociateParentId { get; private set; }
        public int UserTitleId { get; set; }
        public short IsBranchOps { get; set; }

        #endregion

    }
}
