using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoCommon;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;


namespace BoCommon
{
    public class CommonLookupBo
    {

        public DataTable GetMFInstrumentSubCategory(string categoryCode)
        {
            CommonLookupDao commonLookupDao=new CommonLookupDao();
            DataTable dtSubCategory;

            try
            {
                dtSubCategory = commonLookupDao.GetMFInstrumentSubCategory(categoryCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetMFInstrumentSubCategory(string categoryCode)");
                object[] objects = new object[1];
                objects[0] = categoryCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSubCategory;
        }
    }
}
