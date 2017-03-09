using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoAdvisorProfiling
{
    public class AdvisorPreferenceVo
    {
        public string ValtPath { get; set; }
        public bool IsLoginWidgetEnable { get; set; }
        public string LoginWidgetLogOutPageURL { get; set; }
        public string BrowserTitleBarName { get; set; }
        public string BrowserTitleBarIconImageName { get; set; }
        public string WebSiteDomainName { get; set; }
        public int  GridPageSize { get; set; }
        public bool IsBannerEnabled { get; set; }
        public string BannerImageName { get; set; }
        
    }
}
