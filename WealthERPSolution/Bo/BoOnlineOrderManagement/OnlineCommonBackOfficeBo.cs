using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace BoOnlineOrderManagement
{
    public class OnlineCommonBackOfficeBo
    {
        public DataTable ReadCsvFile(string FilePath)
        {
            string[] allLines = File.ReadAllLines(FilePath);

            string[] headers = allLines[0].Split(',');

            DataTable dtUploadFile = new DataTable("Upload");

            foreach (string header in headers) dtUploadFile.Columns.Add(header);

            for (int i = 1; i < allLines.Length; i++)
            {
                string[] row = allLines[i].Split(',');
                dtUploadFile.Rows.Add(row);
            }

            return dtUploadFile;
        }
    }
}
