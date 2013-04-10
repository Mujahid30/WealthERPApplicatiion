using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace WERP_BULK_MAIL_REQUEST_RENERATION
{
    class Program
    {
        static void Main(string[] args)
        {
            string reportRepositoryLocation = ConfigurationSettings.AppSettings["REPORT_REPOSITORY_LOCATION"].ToString();
            BulkMailRequestGenerationBo bulkMailRequestGenerationBo = new BulkMailRequestGenerationBo(reportRepositoryLocation);
            bulkMailRequestGenerationBo.BulkMailRequestProcessor();
        }
    }
}
