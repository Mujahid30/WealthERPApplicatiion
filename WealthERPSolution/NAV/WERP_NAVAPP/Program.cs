using NLog;
using WERP_NAV_UPLOAD;
using WERP_NAVLIB;
namespace WERP_NAVAPP
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
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
        }
    }
}
