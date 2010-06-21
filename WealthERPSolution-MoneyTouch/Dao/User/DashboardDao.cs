using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoUser;
using System.Data.SqlClient;
using System.Xml;
using System.IO;

namespace DaoUser
{
    public class DashboardDao
    {
        public DashboardVo GetUserDashboard(UserVo userVo, string KeyName)
        {
            DashboardVo dashboardVo = null;
            Database db;
            DbCommand getCmd;
            DataSet getDs;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCmd = db.GetStoredProcCommand("SP_GetUserDashboard");
                db.AddInParameter(getCmd, "@D_KeyName", DbType.String, KeyName.ToString());
                db.AddInParameter(getCmd, "@U_UserId", DbType.Int32, userVo.UserId);
                getDs = db.ExecuteDataSet(getCmd);
                dashboardVo = new DashboardVo();

                if (getDs.Tables.Count == 0 || getDs.Tables[0].Rows.Count == 0)
                    throw new Exception("Unable to load dashboard for user");

                int i = 0;
                foreach (DataRow dr in getDs.Tables[0].Rows)
                {
                    if (i == 0)
                    {
                        dashboardVo.DashboardId = int.Parse(dr["D_DashboardId"].ToString());
                        dashboardVo.UserId = userVo.UserId;
                        dashboardVo.Name = dr["D_Name"].ToString();
                        dashboardVo.KeyName = KeyName;
                        dashboardVo.Columns = int.Parse(dr["D_Columns"].ToString());

                        i++;
                    }

                    DashboardPartVo dashboardPartVo = new DashboardPartVo();

                    dashboardPartVo.DashboardId = int.Parse(dr["D_DashboardId"].ToString());
                    dashboardPartVo.DashboardPartId = int.Parse(dr["DP_DashboardPartId"].ToString());
                    dashboardPartVo.Name = dr["DP_Name"].ToString();
                    dashboardPartVo.ControlName = dr["DP_ControlName"].ToString();
                    dashboardPartVo.Params = dr["DP_Params"].ToString();
                    dashboardPartVo.UserOrder = int.Parse(dr["UD_Order"].ToString());
                    dashboardPartVo.UserParams = dr["UD_Params"].ToString();
                    dashboardPartVo.Visible = (dr["Visible"].ToString() == "1")? true: false;
                    dashboardPartVo.Columns = int.Parse(dr["DP_Columns"].ToString());

                    dashboardVo.PartList.Add(dashboardPartVo);
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

                FunctionInfo.Add("Method", "DashboardDao.cs:GetUserDashboard()");

                object[] objects = new object[2];
                objects[0] = KeyName;
                objects[1] = userVo.UserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dashboardVo;
        }

        public bool UpdateUserDashboard(DashboardVo dashboardVo)
        {
            Database db;
            DbCommand getCmd;
            bool bResult = false;

            try
            {
                XmlWriterSettings wSettings = new XmlWriterSettings();
                wSettings.Indent = true;
                wSettings.OmitXmlDeclaration = true;
                MemoryStream ms = new MemoryStream();
                XmlWriter xw = XmlWriter.Create(ms, wSettings);// Write Declaration
                xw.WriteStartDocument();
                xw.WriteStartElement("Root");

                foreach (DashboardPartVo dashboardPartVo in dashboardVo.PartList)
                {
                    xw.WriteStartElement("Item");
                    xw.WriteAttributeString("PartId", dashboardPartVo.DashboardPartId.ToString());
                    xw.WriteAttributeString("Order", dashboardPartVo.UserOrder.ToString());
                    xw.WriteAttributeString("Params", dashboardPartVo.UserParams.ToString());
                    xw.WriteAttributeString("Visible", (dashboardPartVo.Visible)? "1": "0");
                    xw.WriteEndElement();
                }

                xw.WriteEndElement();
                xw.WriteEndDocument();
                xw.Flush();
                
                Byte[] buffer = new Byte[ms.Length];
                buffer = ms.ToArray();
                string xmlOutput = System.Text.Encoding.UTF8.GetString(buffer);
                
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCmd = db.GetStoredProcCommand("SP_UpdateUserDashboard");
                db.AddInParameter(getCmd, "@D_DashboardId", DbType.Int32, dashboardVo.DashboardId);
                db.AddInParameter(getCmd, "@U_UserId", DbType.Int32, dashboardVo.UserId);
                db.AddInParameter(getCmd, "@XML", DbType.String, xmlOutput);

                db.ExecuteNonQuery(getCmd);

                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "DashboardDao.cs:UpdateUserDashboard()");

                object[] objects = new object[2];
                objects[0] = dashboardVo.UserId;
                objects[1] = dashboardVo.KeyName;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
    }
}
