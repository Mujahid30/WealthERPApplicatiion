using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoWerpAdmin
{
    public class AdminUploadProcessLogVo
    {
        #region Fields

        public int ProcessId { get; set; }
        public string AssetClass { get; set; }

        public int NoOfSnapshotsUpdated { get; set; }
        public int NoOfRecordsRejected { get; set; }
        public int NoOfRecordsToHistory { get; set; }

        //public bool IsInsertionToStagingComplete { get; set; }
        public bool IsXMLCreated { get; set; }
        public bool IsInsertedToSnapshot { get; set; }
       // public bool IsInsertedToQEquitySnapshot { get; set; }
        public bool IsInsertedToHistory { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }


        #endregion Fields

       
    }

    //TODO: Move the following code to a different page.

    public enum UploadType
    {
        Price = 1,
        CorpAction = 2
    }

    public enum AssetGroupType
    {
        Equity = 1,
        MF = 2
    }

}
