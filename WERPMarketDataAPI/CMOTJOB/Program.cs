using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using System.Data;

namespace CMOTJOB
{
    class Program
    {
        static void Main(string[] args)
        {
            CMOTJOBBo CMOTBo = new CMOTJOBBo();
            DataSet ds= CMOTBo.GetDataFromAPI();
            //DataTable dtCompleteSchemeDetails= CMOTBo.CombineGetSchemeDetails(ds);
            int result = CMOTBo.updateSchemeDetails(ds.Tables[0]);
          
        }
        
    }
}
