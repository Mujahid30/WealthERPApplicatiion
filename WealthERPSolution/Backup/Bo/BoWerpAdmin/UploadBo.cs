using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DaoWerpAdmin;
using VoWerpAdmin;
using System.IO;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
namespace BoWerpAdmin
{
    public class UploadBo
    {
        UploadType uploadType;
        AssetGroupType assetGroupType;
        DateTime startDate;
        DateTime endDate;
        string xmlFileName;
        public int currentUserId;
        public int processId =0;
        string errorMessage = string.Empty;
        int updatedSnapshotCount = 0;
        int updatedHistoryRowsCount = 0;
        public bool isLatestDataAvailable = false;
        int rejectedRecordsCount = 0;
        public StringBuilder StatusMessage = new StringBuilder();
        ProductPriceUploadLogBo productPriceUploadLogBo = new ProductPriceUploadLogBo();
        
        public string Upload_Xml_Folder = @"~\PMT_Uploads_XML\\";

        /// <summary>
        /// Iterate through specific dates and update the snapshot table.
        /// </summary>
        public void Upload(UploadType uploadType, AssetGroupType assetGroupType)
        {
            this.uploadType = uploadType;
            this.assetGroupType = assetGroupType;
            
            
            GetStartDateAndEndDate();

            if (startDate == DateTime.MinValue)
            {
                StatusMessage.Append("Snapshot table contains no data.Please update the snapshot table.");
                return;
            }
            else if (endDate == DateTime.MinValue)
            {
                StatusMessage.Append("Download table does not contain data.");
                return;
            }

            StatusMessage.Append("<table class='tblMaroon'rules='All' style='font-size:12px;border:solid 1px black;'  cellpadding='10'>");
            StatusMessage.Append("<thead><tr style='background-image: url(../CSS/Images/PCGGridViewHeaderGlass2.jpg);color:White;'><th>Date</th><th>Xml Created</th><th>Snapshot Updated</th><th>History Upload</th><th>Rejected Records</th></tr></thead>");
            if (endDate.CompareTo(startDate) < 0)
            {
                isLatestDataAvailable = true;
                return;
            }

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                errorMessage = string.Empty;
                Upload(date);
            }
            StatusMessage.Append("</table>");
        }

        /// <summary>
        /// Upload the data for a specific day.
        /// </summary>
        /// <param name="date"></param>
        private void Upload(DateTime date)
        {
            AdminUploadProcessLogVo processLogVo = new AdminUploadProcessLogVo();
            bool isSnapshotUpdated = false;
            bool isHistoryUpdated = false;
            bool isXmlCreated = false;

            processLogVo.CreatedBy = this.currentUserId;
            processLogVo.AssetClass = assetGroupType.ToString();
            processLogVo.ProcessId = processId  = productPriceUploadLogBo.CreateProcessLog(processLogVo);
            
            isXmlCreated = this.GetDataAndCreateXML(date);
            
            if (isXmlCreated)
            {
                processLogVo.IsXMLCreated = true;
                //productPriceUploadLogBo.UpdateProcessLog(processLogVo);
                
                FillXmlValuesToTempTable();
                UpdateTempTableWithWERPCode();
                rejectedRecordsCount = MoveInvalidRecords();
                updatedSnapshotCount = UpdateSnapshotTable();


                if (updatedSnapshotCount >= 0)
                {
                    isSnapshotUpdated = true;
                    processLogVo.IsInsertedToSnapshot = true;
                    processLogVo.NoOfRecordsRejected = rejectedRecordsCount;
                    processLogVo.NoOfSnapshotsUpdated = updatedSnapshotCount;
                    //productPriceUploadLogBo.UpdateProcessLog(processLogVo);
                }
                if (isSnapshotUpdated)
                {
                    isHistoryUpdated = false;
                    updatedHistoryRowsCount = UpdateHistoryTable();
                    if (updatedHistoryRowsCount > 0)
                    {
                        isHistoryUpdated = true;
                        processLogVo.IsInsertedToHistory = true;
                        processLogVo.NoOfRecordsToHistory = updatedHistoryRowsCount;
                        processLogVo.EndTime = DateTime.Now;
                    }
                }
            }
            productPriceUploadLogBo.UpdateProcessLog(processLogVo);
            UpdateStatusMessage(date, isXmlCreated, isSnapshotUpdated, isHistoryUpdated);
        }

        /// <summary>
        /// Update the status message for displaying in UI.
        /// </summary>
        private void UpdateStatusMessage(DateTime date, bool isXmlCreated, bool isSnapshotUpdated, bool isUploadedToHistory)
        {
            if (errorMessage != string.Empty)
            {
                StatusMessage.Append("<tr>");
                StatusMessage.Append("<td>" + date.ToString("d-MMM-yyyy") + "</td>");
                StatusMessage.Append("<td colspan='4' style='color:red;'>" + errorMessage.ToString() + "</td>");
                StatusMessage.Append("</tr>");
            }
            else
            {
                StatusMessage.Append("<tr>");
                StatusMessage.Append("<td>" + date.ToString("d-MMM-yyyy") + "</td>");
                StatusMessage.Append("<td>" + isXmlCreated.ToString() + "</td>");
                if (isSnapshotUpdated)
                    StatusMessage.Append("<td>Yes (" + updatedSnapshotCount + ") " + "</td>");
                else
                    StatusMessage.Append("<td>No</td>");
                if (updatedHistoryRowsCount > 0)
                    StatusMessage.Append("<td>Yes (" + updatedHistoryRowsCount + ") " + "</td>");
                else
                    StatusMessage.Append("<td>No</td>");

                StatusMessage.Append("<td>" + rejectedRecordsCount +  "</td>");

                StatusMessage.Append("</tr>");
            }
        }

        /// <summary>
        /// Move all the data of snapshot table to history table.
        /// </summary>
        /// <returns></returns>
        private int UpdateHistoryTable()
        {
            UploadDao uploadDao = new UploadDao();
            return uploadDao.UpdateHistoryTable(uploadType, assetGroupType, currentUserId);
        }

        private int MoveInvalidRecords()
        {
            UploadRejectsBo uploadRejectsBo = new UploadRejectsBo();
            return uploadRejectsBo.MoveRejectedRecordsFromTemp(processId, uploadType, assetGroupType, currentUserId);

        }

        /// <summary>
        /// Update each rows of snapshot table using temp table.
        /// </summary>
        /// <returns></returns>
        private int UpdateSnapshotTable()
        {
            UploadDao uploadDao = new UploadDao();
            return uploadDao.UpdateSnapshotTable(uploadType, assetGroupType, currentUserId);
        }
        
        /// <summary>
        /// Update the temp table with WERP code using the mapping table.
        /// </summary>
        /// <returns></returns>
        private bool UpdateTempTableWithWERPCode()
        {
            UploadDao uploadDao = new UploadDao();
            return uploadDao.UpdateTempTableWithWERPCode(uploadType, assetGroupType);
        }

        /// <summary>
        /// Save the values in xml file to a temp table
        /// </summary>
        /// <returns></returns>
        private bool FillXmlValuesToTempTable()
        {
            UploadDao uploadDao = new UploadDao();
            DataSet dsDownloads = new DataSet();
            if (this.xmlFileName.Length > 0)
            {
                dsDownloads.ReadXml(Upload_Xml_Folder + this.xmlFileName);
                return uploadDao.SaveXmlValuesToTempTable(this.uploadType, this.assetGroupType, dsDownloads.Tables[0]);
            }
            else
                return false;
        }

        /// <summary>
        /// Get the data for a specific date from market DB and create XML file using the data.
        /// </summary>
        private bool GetDataAndCreateXML(DateTime date)
        {
            UploadDao uploadDao = new UploadDao();

            DataSet ds;
            try
            {
                //get the data for a specific date.
                ds = uploadDao.GetDownloadsByDate(date, this.uploadType, this.assetGroupType);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //Checking if both NSE and BSE data available.
                    if (assetGroupType == AssetGroupType.Equity && !IsNSEandBSEDataExists(ds))
                        return false;

                    //Check if the folder exists. If not , create a folder
                    CreateFolder(Upload_Xml_Folder);
                    //form xml file name.
                    this.xmlFileName = "Uploads-" + "-" + this.uploadType.ToString() + "-" + assetGroupType.ToString() + "-" + date.ToString("d-MMM-yyyy") + "-" + DateTime.Now.ToFileTime() + ".xml";
                    ds.WriteXml(Upload_Xml_Folder + this.xmlFileName);
                    return true;
                }
                else
                {
                    errorMessage = "Data not available.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred while creating XML file.";
                this.xmlFileName = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// Check if both NSE data and BSE data available for the day. 
        /// If only one data is available do not process.
        /// </summary>
        private bool IsNSEandBSEDataExists(DataSet ds)
        {
            if (ds.Tables[0].Select("Exchange='NSE'").Length < 1)
            {
                errorMessage = "NSE data not available.";
                return false;
            }
            else if (ds.Tables[0].Select("Exchange='BSE'").Length < 1)
            {
                errorMessage = "BSE data not available.";
                return false;
            }
            else
                return true;
        }
       
        /// <summary>
        /// Check if the folder exists. If not create the folder.
        /// </summary>
        /// <param name="Upload_Xml_Folder"></param>
        /// <returns></returns>
        private bool CreateFolder(string Upload_Xml_Folder)
        {
            if (!Upload_Xml_Folder.EndsWith("\\"))
                Upload_Xml_Folder = Upload_Xml_Folder + "\\";
            try
            {
                if (!Directory.Exists(Upload_Xml_Folder))
                {
                    Directory.CreateDirectory(Upload_Xml_Folder);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Get the start date and end date for the upload.
        /// </summary>
        public void GetStartDateAndEndDate()
        {
            UploadDao UploadDao = new UploadDao();
            this.startDate = UploadDao.GetPMTUploadStartDate(uploadType, assetGroupType);
            this.endDate = UploadDao.GetPMTUploadEndDate(uploadType, assetGroupType);

        }

        #region OldCommentedMethods
        //public int UploadToWERP(DateTime FromDate, DateTime ToDate, String AssetGroup, string Exchange)
        //{

        //    UploadDao obj = new UploadDao();
        //    return obj.UploadToWERP(FromDate, ToDate, AssetGroup, Exchange);

        //}

        //public int RejectedRecordCount(DateTime FromDate, DateTime ToDate, int CurrentPage, string Exchange, string AssetGroup, string Flag)
        //{
        //    UploadDao obj = new UploadDao();
        //    return obj.RejectedRecordCount(FromDate, ToDate, CurrentPage, Exchange, AssetGroup, Flag);

        //}

        //public DataSet RejectedRecords(DateTime FromDate, DateTime ToDate, int CurrentPage, string AssetGroup, string Flag)
        //{
        //    UploadDao obj = new UploadDao();
        //    return obj.RejectedRecords(FromDate, ToDate, CurrentPage, AssetGroup, Flag);

        //}

        #endregion OldCommentedMethods
    }


}
