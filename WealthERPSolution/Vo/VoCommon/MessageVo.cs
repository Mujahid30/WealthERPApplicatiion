using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCommon
{
    public class MessageVo
    {
        public int UserId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string strXMLRecipientIds { get; set; }
    }
}
