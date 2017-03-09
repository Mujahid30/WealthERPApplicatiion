using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WERP_REQUEST_PROCESSOR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RequestCreatorBo requestCreatorBo = new RequestCreatorBo();
            requestCreatorBo.CreateRequestFromWERPRequestRecorder();

        }
    }
}
