using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoHostConfig;
using VoHostConfig;
using System.Data;


namespace BoHostConfig
{
    public class GeneralConfigurationBo
    {
        GeneralConfigurationDao generalConfigurationDao = new GeneralConfigurationDao();
        /// <summary>
        /// Used to insert/update host configuration general details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="generalconfigurationvo"></param>
        /// <returns></returns>
        public bool AddHostGeneralConfiguration(int userId, GeneralConfigurationVo generalconfigurationvo)
        {
            
            bool recordStatus = true;
            try
            {
                recordStatus = generalConfigurationDao.AddHostGeneralConfiguration(userId, generalconfigurationvo);
            }
            catch (Exception ex)
            {
                recordStatus = false;
            }
            return recordStatus;
        }

        /// <summary>
        /// Used to get host configuration general details
        ///  </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public GeneralConfigurationVo GetHostGeneralConfiguration(string xmlPath, int hostId)
        {
            GeneralConfigurationVo generalConfigurationVo = new GeneralConfigurationVo();
            DataSet dsGeneralConfiguration = new DataSet();
            try
            {
                dsGeneralConfiguration = generalConfigurationDao.GetHostGeneralConfiguration(xmlPath);
                DataView dvHostDetails = new DataView(dsGeneralConfiguration.Tables["WERPHostConfiguration"], "HAC_HostId=" + hostId.ToString(), "HAC_HostId", DataViewRowState.CurrentRows);
                DataTable dtHostDetails = dvHostDetails.ToTable("Host");

                if (dtHostDetails != null && dtHostDetails.Rows.Count == 1)
                {
                    generalConfigurationVo.HostLogoPlacement = dtHostDetails.Rows[0]["HAC_HostLogoPlacement"].ToString();
                    generalConfigurationVo.HostLogo = dtHostDetails.Rows[0]["HAC_HostLogo"].ToString();
                    generalConfigurationVo.AdviserLogoPlacement = dtHostDetails.Rows[0]["HAC_AdviserLogoPlacement"].ToString();
                    generalConfigurationVo.DefaultTheme = dtHostDetails.Rows[0]["HAC_DefaultTheme"].ToString();
                    generalConfigurationVo.ContactPersonName = dtHostDetails.Rows[0]["HAC_ContactPersonName"].ToString();
                    generalConfigurationVo.ContactPersonTelephoneNumber = Int64.Parse(dtHostDetails.Rows[0]["HAC_TelephoneNumber"].ToString());
                    generalConfigurationVo.LoginPageContent = dtHostDetails.Rows[0]["HAC_LoginPageContent"].ToString();
                    generalConfigurationVo.ApplicationName = dtHostDetails.Rows[0]["HAC_ApplicationName"].ToString();
                    generalConfigurationVo.Email = dtHostDetails.Rows[0]["HAC_Email"].ToString();
                }

            }
            catch (Exception ex)
            {
            }
            return generalConfigurationVo;

        }

        public DataTable GetAdviserIdFromLookups(string xmlPath)
        {
            DataTable dtGetAdviserIdFromLookups;
            try
            {
                dtGetAdviserIdFromLookups = generalConfigurationDao.GetAdviserIdFromLookups(xmlPath);
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
            return dtGetAdviserIdFromLookups;
        }
    }
}
