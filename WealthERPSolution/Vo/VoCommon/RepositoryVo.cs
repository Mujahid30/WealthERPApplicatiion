using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VoCommon
{
    public class RepositoryVo
    {
        public int RepositoryId { get; set; }
        public int AdviserId { get; set; }
        public string CategoryCode { get; set; }
        public string HeadingText { get; set; }
        public bool IsFile { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
