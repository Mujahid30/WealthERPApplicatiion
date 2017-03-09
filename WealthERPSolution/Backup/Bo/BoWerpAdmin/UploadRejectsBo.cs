using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using VoWerpAdmin;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using DaoWerpAdmin;

namespace BoWerpAdmin
{
    public class UploadRejectsBo
    {
        public int MoveRejectedRecordsFromTemp(int processId, UploadType uploadType, AssetGroupType assetGroupType, int createdBy)
        {
            //UploadDao uploadDao = new UploadDao();

            UploadRejectsDao uploadRejectsDao = new UploadRejectsDao();
            try
            {
                return uploadRejectsDao.MoveRejectedRecordsFromTemp(processId, uploadType, assetGroupType, createdBy);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("MoveRejectedRecordsFromTemp", "UploadRejectsBo.cs:MoveRejectedRecordsFromTemp()");

                object[] objects = new object[4];
                objects[0] = processId;
                objects[1] = uploadType;
                objects[2] = assetGroupType;
                objects[3] = createdBy;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public DataSet GetRejectedRecords(int processId,int page, out int count, UploadType uploadType, AssetGroupType assetGroupType)
        {
            UploadRejectsDao uploadRejectsDao = new UploadRejectsDao();
            return uploadRejectsDao.GetRejectedRecords(processId, page,out count, uploadType, assetGroupType);
        }

        public bool Reprocess(int processId, UploadType uploadType, AssetGroupType assetGroupType, int currentUser,out int updatedSnapshots,out int updatedHistory)
        {
            try
            {
                UploadRejectsDao rejectsDao = new UploadRejectsDao();
                return rejectsDao.Reprocess(processId, uploadType, assetGroupType, currentUser,out updatedSnapshots,out updatedHistory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Reprocess()", "UploadRejectsDao.cs:Reprocess()");

                object[] objects = new object[1];
                objects[0] = processId;
                objects[1] = uploadType;
                objects[2] = assetGroupType;
                objects[3] = currentUser;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            
        }
    }



}



