namespace WERP_NAVLIB
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using NLog;

    public class DownloadTask
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DownloadTask(TaskType TaskTypeName)
        {
            logger.Info("Reading configuration");
            try
            {

                string remoteUrl = this.GetRemoteUrl();
                this.FileName = ConfigurationSettings.AppSettings["MFFileName"];
                this.DateBasedDirName = this.GetDateBasedDirName();
                this.TaskName = TaskTypeName;
                if (TaskTypeName == TaskType.MutualFund)
                {
                    remoteUrl = remoteUrl + ConfigurationSettings.AppSettings["MFDir"];
                    this.FileName = ConfigurationSettings.AppSettings["MFFileName"];
                    remoteUrl = remoteUrl + this.GetDateBasedDirName() + this.FileName;
                }
                this.DownLoadURL = remoteUrl;
            }
            catch (Exception ex)
            {
                logger.Fatal("Error while reading configuration" + ex.ToString());
                return;
            }
        }

        private string GetDateBasedDirName()
        {
            int datefiff = Convert.ToInt32(ConfigurationSettings.AppSettings["DateDiff"]);
            DateTime d1 = DateTime.Now;
            TimeSpan t1 = new TimeSpan(datefiff, 0, 0, 0);
            d1 = d1.Subtract(t1);
            return ("/" + string.Format("{0:ddMMyyyy}", d1).ToString());
        }

        private string GetRemoteUrl()
        {
            AppSettingsReader APPREADER = new AppSettingsReader();
            return ConfigurationSettings.AppSettings["RemoteUrl"];
        }

        //public string DateBasedDirName
        //{
        //    [CompilerGenerated]
        //    get
        //    {
        //        return this.<DateBasedDirName>k__BackingField;
        //    }
        //    [CompilerGenerated]
        //    set
        //    {
        //        this.<DateBasedDirName>k__BackingField = value;
        //    }
        //}

        //public List<DownloadTask> DownloadTaskList
        //{
        //    [CompilerGenerated]
        //    get
        //    {
        //        return this.<DownloadTaskList>k__BackingField;
        //    }
        //    [CompilerGenerated]
        //    set
        //    {
        //        this.<DownloadTaskList>k__BackingField = value;
        //    }
        //}

        //public string DownLoadURL
        //{
        //    [CompilerGenerated]
        //    get
        //    {
        //        return this.<DownLoadURL>k__BackingField;
        //    }
        //    private [CompilerGenerated]
        //    set
        //    {
        //        this.<DownLoadURL>k__BackingField = value;
        //    }
        //}

        //public DateTime FileDateToDownLoad
        //{
        //    [CompilerGenerated]
        //    get
        //    {
        //        return this.<FileDateToDownLoad>k__BackingField;
        //    }
        //    [CompilerGenerated]
        //    set
        //    {
        //        this.<FileDateToDownLoad>k__BackingField = value;
        //    }
        //}

        //public string FileName
        //{
        //    [CompilerGenerated]
        //    get
        //    {
        //        return this.<FileName>k__BackingField;
        //    }
        //    [CompilerGenerated]
        //    set
        //    {
        //        this.<FileName>k__BackingField = value;
        //    }
        //}

        //public TaskType TaskName
        //{
        //    [CompilerGenerated]
        //    get
        //    {
        //        return this.<TaskName>k__BackingField;
        //    }
        //    private [CompilerGenerated]
        //    set
        //    {
        //        this.<TaskName>k__BackingField = value;
        //    }
        //}
        //[CompilerGenerated]
        //private string <DateBasedDirName>k__BackingField;
        //[CompilerGenerated]
        //private List<DownloadTask> <DownloadTaskList>k__BackingField;
        //[CompilerGenerated]
        //private string <DownLoadURL>k__BackingField;
        //[CompilerGenerated]
        //private DateTime <FileDateToDownLoad>k__BackingField;
        //[CompilerGenerated]
        //private string <FileName>k__BackingField;
        //[CompilerGenerated]
        //private TaskType <TaskName>k__BackingField;

        public TaskType TaskName { get; private set; }
        //name of the file to download
        public string FileName { get; set; }
        //This will tell which date file need to be downloaded 
        public DateTime FileDateToDownLoad { get; set; }
        //This prpperty may be used to execute multiple DownLoad Tasks.
        public List<DownloadTask> DownloadTaskList { get; set; }
        /// <summary>
        /// This properity 
        /// </summary>
        public string DateBasedDirName { get; set; }
        public string DownLoadURL { get; private set; }
        //This will tell which date file need to be downloaded 
      //  public List<DownloadTask> DownloadTaskList { get; set; }
    }
}

