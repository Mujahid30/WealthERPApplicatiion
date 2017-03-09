using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmpsysJobDaemon
{
    public enum JobStatus { NotProcessed = 0, InProcess = 1, SuccessFull = 2, SuccessPartial = 3, Failed = 4 };
    public enum JobPartStatus { NotProcessed = 0, Success = 1, Failed = 2 };

    class JobMgr
    {
        public static Job GetJobByType(int JobTypeId)
        {
            Job J = null;
            
            switch (JobTypeId)
            {
                case 1:
                    J = new EquityFlaggingJob();
                    break;
                case 2:
                    J = new DailyAssetValuation();
                    break;
                case 3:
                    J = new MFTransactionCancellationJob();
                    break;
                case 4:
                    J = new AlertServicesTriggerJob();
                    break;
                case 5:
                    J = new MutualFundNAVDownload();
                    break;
                case 6:
                    J = new JobAccordProductMaster();
                    break;
                default: J = new JobTest();
                    break;
            }

            return J;
        }

    }
}
