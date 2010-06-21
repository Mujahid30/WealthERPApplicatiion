using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WERP_NAVLIB;
using WERP_NAV_UPLOAD;
using NLog;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace AmpsysJobDaemon
{
    class MutualFundNAVDownload:Job
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public override JobStatus Start(JobParams JP, out string ErrorMsg)
        {
            ErrorMsg = "";
            logger.Info("-------------Starting NAV Download-------------");

            DownloadLogVo downloadLogVo = new DownloadLogVo();
            downloadLogVo.ID = DownloadLog.Add();

            if (downloadLogVo.ID > 0)
            {
                HttpDownLoad dn = new HttpDownLoad();
                DownloadTask dt = new DownloadTask(TaskType.MutualFund);
                if (dn.ExecuteDownLoadTask(dt, downloadLogVo))
                {

                    NAVUpload navUpload = new NAVUpload();
                    navUpload.CopyDataToWERPTempTable();
                    navUpload.UpdateWERPSchemeCode();
                    downloadLogVo.RejectedSchemesCount = navUpload.MoveRejectedRecords(downloadLogVo.ID);
                    DownloadLog.Update(downloadLogVo);
                    downloadLogVo.UpdatedSchemesCount = navUpload.UpdateSnapshot();
                    downloadLogVo.Is_WERP_Snapshot_Updated = true;
                    DownloadLog.Update(downloadLogVo);
                    //navUpload.UpdateHistoryTable();
                }
            }
            logger.Info("-------------NAV Update Ends-------------");

            return JobStatus.SuccessFull;
        }
    }
}
