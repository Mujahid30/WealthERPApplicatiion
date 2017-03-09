using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoWerpAdmin;
using VoWerpAdmin;
using System.Data;

namespace BoWerpAdmin
{
    public class ProductPriceUploadLogBo
    {
        public int CreateProcessLog(AdminUploadProcessLogVo processLog)
        {
            ProductPriceUploadLogDao productPriceUploadLogDao = new ProductPriceUploadLogDao();
            return productPriceUploadLogDao.CreateProcessLog(processLog);
        }
        public bool UpdateProcessLog(AdminUploadProcessLogVo processLog)
        {
            ProductPriceUploadLogDao productPriceUploadLogDao = new ProductPriceUploadLogDao();
            return productPriceUploadLogDao.UpdateProcessLog(processLog);
        }
        public DataSet GetProcessLog(int currentPage,out int count)
        {
            ProductPriceUploadLogDao productPriceUploadLogDao = new ProductPriceUploadLogDao();
            return productPriceUploadLogDao.GetProcessLog(currentPage,out count);
        }
    }
}
