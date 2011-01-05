using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using BoWerpAdmin;
using BoMarketDataPull;
using System.IO;
using VoWerpAdmin;

namespace EquityPriceDownloadUpload
{
    class Program
    {
       
        static void Main(string[] args)
        {
            DataTable dtNSE = new DataTable();
            DataTable dtBSE = new DataTable();
            GetDownloadData objDownload = new GetDownloadData();
            DateTime priceDate = new DateTime();
            UploadBo uploadBo = new UploadBo();
            string xmlPath = ConfigurationManager.AppSettings["xmlFilePath"].ToString();
            priceDate = DateTime.Today;
            if (DateTime.Now.Hour < 23)
                priceDate = priceDate.AddDays(-1);
            dtNSE = objDownload.downloadNSEEquityData(100, priceDate, priceDate);
            Console.WriteLine("NSE Download Date:" + dtNSE.Rows[0][0].ToString() + " Result: " + dtNSE.Rows[0][1].ToString() + " No Of Records: " + dtNSE.Rows[0][2].ToString());
            dtBSE = objDownload.downloadBSEEquityData(100, priceDate, priceDate);
            Console.WriteLine("BSE Download Date:" + dtBSE.Rows[0][0].ToString() + " Result: " + dtBSE.Rows[0][1].ToString() + " No Of Records: " + dtBSE.Rows[0][2].ToString());


            UploadType uploadType;
            AssetGroupType assetGroupType;

            uploadType = (UploadType)(1);
            assetGroupType = (AssetGroupType)(1);

            uploadBo.Upload_Xml_Folder = xmlPath;
            uploadBo.currentUserId = 100;
            uploadBo.Upload(uploadType, assetGroupType);
            Console.WriteLine("Completed Equity Price Uploads");
        }
    }
}
