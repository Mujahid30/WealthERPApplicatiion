namespace WERP_NAVLIB
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using FileHelpers;
    using NLog;
    internal class NavFormatter
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        public XmlDocument FormatDownloadedFile(string DownLoadedFileFullPath)
        {
            string DownloadedFile = string.Empty;
            XmlDocument doc = new XmlDocument();
            logger.Debug("Creating XMLDocument from downloaded file");
            try
            {

                using (StreamReader rdr = File.OpenText(DownLoadedFileFullPath))
                {
                    DownloadedFile = rdr.ReadToEnd().Replace("<<row>>", "<row>").Replace("<</row>>", "</row>").Replace("<<eof>>", "");
                    Dictionary<string, string> keyvalues = new Dictionary<string, string>();
                    keyvalues.Add("##$Value$##", DownloadedFile);
                    string formattedFile = ProcessTokens(LoadTemplate("WERP_Template1.txt"), keyvalues);
                    doc.LoadXml(formattedFile);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred while creating XMLDocument from downloaded file:" + DownLoadedFileFullPath + ex.ToString());
                
            }
            return doc;
        }

        public List<WerpMutualFund> LoadMutualData(XmlDocument dataXml)
        {
            logger.Debug("Looping through each nodes in XMLDocument and creating WerpMutualFund list");
            FileHelperEngine engine = new FileHelperEngine(typeof(WerpMutualFund));
            List<WerpMutualFund> Wmf = new List<WerpMutualFund>();
            try
            {
                foreach (XmlNode xn in dataXml.ChildNodes[0].ChildNodes)
                {
                    WerpMutualFund[] WMFList = (WerpMutualFund[])engine.ReadString(xn.InnerText);
                    WerpMutualFund vm = WMFList[0];
                    Wmf.Add(vm);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred while creating WerpMutualFund list from XMLDocument "+ ex.ToString());

            }
            logger.Info("Total count =" + Wmf.Count);
            return Wmf;
        }

        public static string LoadTemplate(string templateName)
        {
            string template = " <Root> ##$Value$## </Root>";


            //try
            //{
            //    if (string.IsNullOrEmpty(templateName))
            //    {
            //        throw new ArgumentNullException("Invalid  Template");
            //    }
            //    string filePath = AppDomain.CurrentDomain.BaseDirectory;
            //    if (!filePath.EndsWith(@"\"))
            //    {
            //        filePath = filePath + @"\";
            //    }
            //    filePath = (filePath + @"bin\") + @"Templates\" + templateName;
            //    using (StreamReader rdr = File.OpenText(@"C:\PCG-NAVCURRENT\LatestPCG\LatestPCG\FileHelpers_Source\WERP_NAVLIB\Templates\WERP_Template1.txt"))
            //    {
            //        template = rdr.ReadToEnd();
            //    }
            //}
            //catch (Exception)
            //{
            //}
            return template;
        }

        public static string ProcessTokens(string body, Dictionary<string, string> tokens)
        {
            foreach (KeyValuePair<string, string> pair in tokens)
            {
                body = body.Replace(pair.Key, pair.Value);
            }
            return body;
        }
    }
}

