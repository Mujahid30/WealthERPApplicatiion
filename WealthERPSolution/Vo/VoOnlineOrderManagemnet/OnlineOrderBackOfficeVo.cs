using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VoOnlineOrderManagemnet
{
    public class OnlineOrderBackOfficeVo
    {
        public string HeaderName { get; set; }
        public int HeaderSequence { get; set; }
        public string WerpColumnName { get; set; }
        public string DataType { get; set; }
        public int MaxLength { get; set; }
        public bool IsNullable { get; set; }
    }
}
