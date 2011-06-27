using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class BorrowerOwnershipVo
    {
        public int Id { get; set; }

        public string BorrowerName { get; set; }

        public string AssetOwnership { get; set; }

        public string Liability { get; set; }

        public string Margin { get; set; }
        
    }
}
