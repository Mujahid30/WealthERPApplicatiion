using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace AmpsysJobDaemon
{
    class JobAccordProductMaster : Job
    {
        private static string _AccordFTPServer = ConfigurationSettings.AppSettings["AccordFTPServer"];
        private static string _AccordFTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["AccordFTPUsername"]);
        private static string _AccordFTPPassword = Encryption.Decrypt(ConfigurationSettings.AppSettings["AccordFTPPassword"]);

        public override JobStatus Start(JobParams JP, out string ErrorMsg)
        {
            ErrorMsg = "";

            string AccordImportDate = ConfigurationManager.AppSettings["AccordImportDate"];
            DateTime ImportDate = DateTime.Now;

            if (AccordImportDate != "#RUNDATE#")
                ImportDate = DateTime.Parse(AccordImportDate);

            ProcessAccordProductMasterData(ImportDate);

            return JobStatus.SuccessFull;
        }

        public void ProcessAccordProductMasterData(DateTime ImportDate)
        {
            string XMLPath = Utils.GetDataStore() + @"\AccordProductMaster.xml";

            XmlDocument XD = new XmlDocument();
            XD.Load(XMLPath);

            foreach (XmlNode XN in XD.SelectNodes("filelist/file"))
            {
                string FilePath = XN.Attributes["name"].Value;
                FilePath = FilePath.Replace("DDMMYYYY", ImportDate.Day.ToString("00") + ImportDate.Month.ToString("00") + ImportDate.Year.ToString("0000"));

                string LocalPath = Utils.GetDataStore() + FilePath.Replace("/", @"\");

                string[] TempPath = LocalPath.Split(@"\".ToCharArray());
                Directory.CreateDirectory(String.Join(@"\", TempPath, 0, TempPath.Length - 1));

                CreateDatabaseTable(XN);

                if (!CheckIfFileExists(LocalPath, ImportDate))
                    DownloadFTPFile(FilePath, LocalPath);

                if (File.Exists(LocalPath))
                {
                    ImportAccordFile(XN, LocalPath, ImportDate);
                }
            }

            ProcessData(ImportDate);
        }

        public bool CheckIfFileExists(string LocalPath, DateTime ImportDate)
        {
            bool FileExists = false;

            if (File.Exists(LocalPath))
            {
                DateTime CreationTime = File.GetCreationTime(LocalPath);
                if (ImportDate.Year == CreationTime.Year && ImportDate.Month == CreationTime.Month && ImportDate.Day == CreationTime.Day)
                    FileExists = true;
            }

            return FileExists;
        }

        public void DownloadFTPFile(string FilePath, string LocalPath)
        {
            try
            {
                FtpWebRequest FWR = (FtpWebRequest)WebRequest.Create("ftp://" + _AccordFTPServer + FilePath);
                FWR.UsePassive = false;
                FWR.KeepAlive = true;

                NetworkCredential NC = new NetworkCredential();
                NC.UserName = _AccordFTPUsername;
                NC.Password = _AccordFTPPassword;

                FWR.Credentials = NC;

                FtpWebResponse FWResp = (FtpWebResponse)FWR.GetResponse();
                StreamReader SR = new StreamReader(FWResp.GetResponseStream());

                StreamWriter SW = new StreamWriter(LocalPath);
                SW.Write(SR.ReadToEnd());
                SW.Flush();
                SW.Close();
                SR.Close();

            }
            catch (Exception Ex)
            {
                if (Ex.ToString().IndexOf("550") == -1)
                    throw new Exception("Failed to download file: " + FilePath, Ex);
            }
        }

        public void CreateDatabaseTable(XmlNode XN)
        {
            try
            {
                string TableName = XN.Attributes["table"].Value;
                string TableScript = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]') AND type in (N'U')) BEGIN TRUNCATE TABLE [dbo].[" + TableName + "] END ELSE BEGIN ";

                TableScript += "CREATE TABLE [dbo].[" + TableName + "]( ";

                string PrimaryKey = "CONSTRAINT [PK_" + TableName + "] PRIMARY KEY CLUSTERED ( ";
                foreach (XmlNode XNCol in XN.SelectNodes("field"))
                {
                    string Name = XNCol.Attributes["name"].Value;
                    string Type = XNCol.Attributes["type"].Value;
                    bool IsPrimary = (XNCol.Attributes["primary"] != null && XNCol.Attributes["primary"].Value == "1") ? true : false;

                    if (IsPrimary)
                        PrimaryKey += Name + " ASC,";

                    if (IsPrimary)
                        TableScript += Name + " " + Type + " NOT NULL,";
                    else
                        TableScript += Name + " " + Type + " NULL,";
                }
                if (PrimaryKey.LastIndexOf(",") == PrimaryKey.Length - 1)
                    PrimaryKey = PrimaryKey.Substring(0, PrimaryKey.Length - 1);

                if (TableScript.LastIndexOf(",") == TableScript.Length - 1)
                    TableScript = TableScript.Substring(0, TableScript.Length - 1);

                PrimaryKey += " ) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]";

                TableScript += ", " + PrimaryKey;
                TableScript += " ) ON [PRIMARY]";

                TableScript += " END";

                Utils.ExecuteNonQueryAccord(TableScript);
            }
            catch (Exception Ex)
            {
                throw new Exception("Failed to create table for " + XN.Attributes["name"].Value, Ex);
            }
        }

        public Type GetTypeFromDBType(string DBType)
        {
            Type T = null;

            switch (DBType.Split('(')[0])
            {
                case "int": T = Type.GetType("System.Int32");
                    break;
                case "float": T = Type.GetType("System.Double");
                    break;
                case "datetime": T = Type.GetType("System.DateTime");
                    break;
                case "nvarchar": T = Type.GetType("System.String");
                    break;
                case "numeric": T = Type.GetType("System.Decimal");
                    break;
                case "decimal": T = Type.GetType("System.Decimal");
                    break;
                case "ntext": T = Type.GetType("System.String");
                    break;
                default: T = null;
                    break;
            }

            return T;
        }

        public void ImportAccordFile(XmlNode XN, string Path, DateTime ImportDate)
        {
            try
            {
                int Added = 0;
                int Deleted = 0;
                int Updated = 0;

                StreamReader SR = new StreamReader(Path);
                DataTable DT = new DataTable();

                foreach (XmlNode XNCol in XN.SelectNodes("field"))
                {
                    string Name = XNCol.Attributes["name"].Value;
                    string ColType = XNCol.Attributes["type"].Value;

                    DataColumn DC = new DataColumn(Name, GetTypeFromDBType(ColType));
                    if (ColType.IndexOf("varchar") != -1)
                    {
                        DC.MaxLength = int.Parse(ColType.Substring(ColType.IndexOf("(")+1, ColType.IndexOf(")") - ColType.IndexOf("(") - 1));
                    }

                    DC.AllowDBNull = true;
                    DT.Columns.Add(DC);
                }

                while (!SR.EndOfStream)
                {
                    string Data = SR.ReadLine();
                    if (Data.IndexOf("<<row>>") != -1)
                    {
                        // convert datetime to db format
                        Regex R = new Regex("([0-9]{1,2})/([0-9]{1,2})/([0-9]{4})", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        Data = R.Replace(Data, "$3/$1/$2");

                        object[] DataArray = Data.Replace("<<row>>", "").Replace("<</row>>", "").Split('|');

                        if (DataArray.Length == DT.Columns.Count)
                        {
                            // handle nulls
                            for (int i = 0; i < DataArray.Length; i++)
                            {
                                if ((DT.Columns[i].DataType == Type.GetType("System.Int32") || DT.Columns[i].DataType == Type.GetType("System.Double") || DT.Columns[i].DataType == Type.GetType("System.Decimal")) && (DataArray[i].ToString() == "" || DataArray[i].ToString() == "NULL"))
                                    DataArray[i] = "0";
                                else if ((DT.Columns[i].DataType == Type.GetType("System.DateTime")) && (DataArray[i].ToString() == "" || DataArray[i].ToString() == "NULL"))
                                    DataArray[i] = "1990/01/01";

                                if (DT.Columns[i].MaxLength > -1 && DataArray[i].ToString().Length > DT.Columns[i].MaxLength)
                                    DataArray[i] = DataArray[i].ToString().Substring(0, DT.Columns[i].MaxLength);
                            }

                            switch (DataArray[DataArray.Length - 1].ToString())
                            {
                                case "A": Added++; break;
                                case "D": Deleted++; break;
                                default: Updated++; break;
                            }

                            DT.Rows.Add(DataArray);
                        }
                    }
                }

                SR.Close();

                string TableName = XN.Attributes["table"].Value;
                string FileName = XN.Attributes["name"].Value;

                SqlBulkCopy SBC = new SqlBulkCopy(Utils.GetAccordConnectionString());
                SBC.DestinationTableName = TableName;

                SBC.BulkCopyTimeout = 30 * 60;
                SBC.WriteToServer(DT);

                SBC.Close();

                SqlParameter[] Params = new SqlParameter[5];
                Params[0] = new SqlParameter("@TableName", TableName);
                Params[1] = new SqlParameter("@Date", ImportDate);
                Params[2] = new SqlParameter("@Added", Added);
                Params[3] = new SqlParameter("@Deleted", Deleted);
                Params[4] = new SqlParameter("@Updated", Updated);

                Utils.ExecuteNonQueryAccord("sproc_AddImportLog", Params);
            }
            catch (Exception Ex)
            {
                throw new Exception("Failed to import to database " + XN.Attributes["name"].Value, Ex);
            }
        }

        public void ProcessData(DateTime ImportDate)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@ImportDate", ImportDate);

                Utils.ExecuteNonQueryAccord("sproc_ProcessAccordData", Params);
            }
            catch (Exception Ex)
            {
                throw new Exception("Failed to process accord data", Ex);
            }
        }
    }
}
