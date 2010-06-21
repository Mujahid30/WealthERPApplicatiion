using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoWebContent;
using DaoWebContent;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace BoWebContent
{
    public class DemoRequestBo
    {
        public bool Add(DemoRequestVo demoRequestVo)
        {
            bool result;
            DemoRequestDao demoRequestDao = new DemoRequestDao();
            try
            {
                result = demoRequestDao.Add(demoRequestVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "DemoReuestBo.cs:Add()");


                object[] objects = new object[1];
                objects[0] = demoRequestVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return result;
        }

    }
}
