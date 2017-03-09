namespace WERP_NAVLIB
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using NLog;
    using System.Net;

    public class HttpDownLoad //: IHttpDownLoad
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string NAVFile = string.Empty;

        public void DownLoadDataFile()
        {

        }

        public bool ExecuteDownLoadTask(DownloadTask dtask, DownloadLogVo downloadLogVo)
        {
            try
            {
                WebClient webclient = new WebClient();
                webclient.Credentials = WerpCredentials.GetDefaultCredentials();
                NAVFile = GetLocalStoragePath() + "NAV-" + DateTime.Now.ToString("yyyy-MM-dd") + "---" + DateTime.Now.Ticks + ".txt";
                logger.Info("Download file name : " + NAVFile);
                webclient.DownloadFile(dtask.DownLoadURL, NAVFile);

                downloadLogVo.Is_Downloaded = true;
                DownloadLog.Update(downloadLogVo);

                //NAVFile = "C:\\PCGNAVData\\NAV-2010-04-23---634076155947031250.txt";
            }
            catch (Exception ex)
            {
                logger.Fatal("Error occurred while downloading NAV. ExecuteDownLoadTask()" + ex.ToString());
                return false;
            }

            List<WerpMutualFund> listMF = new NavFormatter().LoadMutualData(new NavFormatter().FormatDownloadedFile(NAVFile));


            DBUpdate dbupdate = new DBUpdate();
            int totalUpdateSchemes = dbupdate.DBupload(listMF,downloadLogVo);
            if (totalUpdateSchemes > 0)
            {
                downloadLogVo.Is_AF_Updated = true;
                DownloadLog.Update(downloadLogVo);
                return true;
            }
            else
                return false;
        }

        private string GetLocalStoragePath()
        {
            if (!System.IO.Directory.Exists(ConfigurationSettings.AppSettings["LocalStoragePath"].ToString()))
            { 
                
              System.IO.Directory.CreateDirectory(ConfigurationSettings.AppSettings["LocalStoragePath"].ToString());
            
            }
                return ConfigurationSettings.AppSettings["LocalStoragePath"].ToString()+@"\";
        }

        private string GetRemoteUrl()
        {
            AppSettingsReader APPREADER = new AppSettingsReader();
            return ConfigurationSettings.AppSettings["RemoteUrl"].ToString();
        }
    }
}

