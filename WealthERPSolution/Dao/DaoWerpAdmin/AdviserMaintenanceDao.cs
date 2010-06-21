using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUser;
using System.Data.Common;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace DaoWerpAdmin
{
    public class AdviserMaintenanceDao
    {
        public List<AdvisorVo> GetAdviserList()
        {
            List<AdvisorVo> adviserVoList = new List<AdvisorVo>();
            AdvisorVo adviserVo=new AdvisorVo();
            DataSet getAdvisorDs;
            DataTable dtAdvisers;
            Database db;
            DbCommand Cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                Cmd = db.GetStoredProcCommand("SP_GetAllAdvisers");
                
                 getAdvisorDs = db.ExecuteDataSet(Cmd);
                 dtAdvisers=getAdvisorDs.Tables[0];
                 foreach (DataRow dr in dtAdvisers.Rows)
                 {
                     adviserVo = new AdvisorVo();

                     adviserVo.advisorId = int.Parse(dr["A_AdviserId"].ToString());
                     adviserVo.BusinessCode = dr["XABT_BusinessTypeCode"].ToString();
                     adviserVo.OrganizationName = dr["A_OrgName"].ToString();
                     adviserVo.AddressLine1 = dr["A_AddressLine1"].ToString();
                     adviserVo.AddressLine2 = dr["A_AddressLine2"].ToString();
                     adviserVo.AddressLine3 = dr["A_AddressLine3"].ToString();
                     //advisorVo.BusinessCode = dr["BT_BusinessCode"].ToString();
                     adviserVo.City = dr["A_City"].ToString();
                     adviserVo.ContactPersonFirstName = dr["A_ContactPersonFirstName"].ToString();
                     adviserVo.ContactPersonLastName = dr["A_ContactPersonLastName"].ToString();
                     adviserVo.ContactPersonMiddleName = dr["A_ContactPersonMiddleName"].ToString();
                     adviserVo.Country = dr["A_Country"].ToString();
                     adviserVo.Email = dr["A_Email"].ToString();
                     adviserVo.Website = dr["A_Website"].ToString();
                     if (dr["A_Fax"] != null && dr["A_Fax"].ToString() != "")
                         adviserVo.Fax = int.Parse(dr["A_Fax"].ToString());
                     if (dr["A_FaxISD"] != null && dr["A_FaxISD"].ToString() != "")
                         adviserVo.FaxIsd = int.Parse(dr["A_FaxISD"].ToString());
                     if (dr["A_FaxSTD"] != null && dr["A_FaxSTD"].ToString() != "")
                         adviserVo.FaxStd = int.Parse(dr["A_FaxSTD"].ToString());
                     if (dr["A_ContactPersonMobile"] != null && dr["A_ContactPersonMobile"].ToString() != "")
                         adviserVo.MobileNumber = Convert.ToInt64(dr["A_ContactPersonMobile"].ToString());
                     if (dr["A_IsMultiBranch"].ToString() != "" && dr["A_IsMultiBranch"].ToString() != null)
                         adviserVo.MultiBranch = int.Parse(dr["A_IsMultiBranch"].ToString());
                     if (dr["A_IsAssociateModel"].ToString() != "" && dr["A_IsAssociateModel"].ToString() != null)
                         adviserVo.Associates = int.Parse(dr["A_IsAssociateModel"].ToString());
                     if (dr["A_Phone1STD"] != null && dr["A_Phone1STD"].ToString() != "")
                         adviserVo.Phone1Std = int.Parse(dr["A_Phone1STD"].ToString());
                     if (dr["A_Phone2STD"] != null && dr["A_Phone2STD"].ToString() != "")
                         adviserVo.Phone2Std = int.Parse(dr["A_Phone2STD"].ToString());
                     if (dr["A_Phone1ISD"] != null && dr["A_Phone1ISD"].ToString() != "")
                         adviserVo.Phone1Isd = int.Parse(dr["A_Phone1ISD"].ToString());
                     if (dr["A_Phone2ISD"] != null && dr["A_Phone2ISD"].ToString() != "")
                         adviserVo.Phone2Isd = int.Parse(dr["A_Phone2ISD"].ToString());
                     if (dr["A_Phone1Number"] != null && dr["A_Phone1Number"].ToString() != "")
                         adviserVo.Phone1Number = int.Parse(dr["A_Phone1Number"].ToString());
                     if (dr["A_Phone2Number"] != null && dr["A_Phone2Number"].ToString() != "")
                         adviserVo.Phone2Number = int.Parse(dr["A_Phone2Number"].ToString());
                     if (dr["A_PinCode"] != null && dr["A_PinCode"].ToString() != "")
                         adviserVo.PinCode = int.Parse(dr["A_PinCode"].ToString());

                     adviserVoList.Add(adviserVo);
                 }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdvisor()");
                object[] objects = new object[1];
                objects[0] = adviserVoList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return adviserVoList;
        }
    }
}
