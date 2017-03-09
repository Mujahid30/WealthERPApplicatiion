using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmpsysJobDaemon
{
    class JobTest : Job
    {
        public override JobStatus Start(JobParams JP, out string ErrorMsg)
        {
            ErrorMsg = "";

            return JobStatus.SuccessFull;
        }
    }
}
