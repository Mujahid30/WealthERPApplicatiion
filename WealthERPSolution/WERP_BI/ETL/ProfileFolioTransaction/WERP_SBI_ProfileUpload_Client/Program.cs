using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using System.Configuration;
using System.IO;

namespace WERP_SBI_ProfileUpload_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Microsoft.SqlServer.Dts.Runtime.Application App = new Microsoft.SqlServer.Dts.Runtime.Application();
            string packagePath = System.Configuration.ConfigurationSettings.AppSettings["PACKAGE_PATH"].ToString();
            string configPath = System.Configuration.ConfigurationSettings.AppSettings["PACKAGE_CONFIG_PATH"].ToString();
            string logPath = System.Configuration.ConfigurationSettings.AppSettings["LOG_PATH"].ToString();
            string err = "";
            Package stdProPkg1 = App.LoadPackage(packagePath, null);
            stdProPkg1.ImportConfigurationFile(configPath);
            DTSExecResult stdProResult1 = stdProPkg1.Execute();

            if (stdProResult1.ToString() == "Success")
                err = "SUCCESS";
            else if (stdProResult1.ToString() == "Failure")
            {

                foreach (Microsoft.SqlServer.Dts.Runtime.DtsError local_DtsError in stdProPkg1.Errors)
                {
                    string error = local_DtsError.Description.ToString();
                    err = err + error;
                }

            }

            WriteLog(err, logPath);
        }

        private static void WriteLog(string err, string path)
        {
            // Create a stringbuilder and write the new user input to it.
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = =  = = = = = =  = =");
            sb.Append(err);
            sb.AppendLine();
            sb.AppendLine();

            // Open a streamwriter to a new text file named "UserInputFile.txt"and write the contents of 
            // the stringbuilder to it. 
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(sb.ToString());
            }

        }
    }
}
