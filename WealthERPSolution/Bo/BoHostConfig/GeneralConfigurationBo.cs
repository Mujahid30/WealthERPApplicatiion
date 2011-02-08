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
        /// <summary>
        /// Used to insert/update host configuration general details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="generalconfigurationvo"></param>
        /// <returns></returns>
        public bool AddHostGeneralConfiguration(int userId, GeneralConfigurationVo generalconfigurationvo)
        {
            GeneralConfigurationDao generalConfigurationDao = new GeneralConfigurationDao();
            bool recordStatus=true;
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
        public GeneralConfigurationVo GetHostGeneralConfiguration(int userId)
        {
            GeneralConfigurationVo generalConfigurationVo = new GeneralConfigurationVo();
            GeneralConfigurationDao generalConfigurationDao = new GeneralConfigurationDao();           
            DataSet dsGeneralConfiguration = new DataSet();
            try
            {
                dsGeneralConfiguration = generalConfigurationDao.GetHostGeneralConfiguration(userId);
                if (dsGeneralConfiguration != null && dsGeneralConfiguration.Tables[0].Rows.Count==1)                
                {
                    generalConfigurationVo.HostLogoPlacement = dsGeneralConfiguration.Tables[0].Rows[0]["HAC_HostLogoPlacement"].ToString();
                    generalConfigurationVo.HostLogo = dsGeneralConfiguration.Tables[0].Rows[0]["HAC_HostLogo"].ToString();
                    generalConfigurationVo.AdviserLogoPlacement = dsGeneralConfiguration.Tables[0].Rows[0]["HAC_AdviserLogoPlacement"].ToString();
                    generalConfigurationVo.DefaultTheme = dsGeneralConfiguration.Tables[0].Rows[0]["HAC_DefaultTheme"].ToString();
                    generalConfigurationVo.ContactPersonName = dsGeneralConfiguration.Tables[0].Rows[0]["HAC_ContactPersonName"].ToString();
                    generalConfigurationVo.ContactPersonTelephoneNumber = Int64.Parse(dsGeneralConfiguration.Tables[0].Rows[0]["HAC_TelephoneNumber"].ToString());
                    generalConfigurationVo.LoginPageContent = dsGeneralConfiguration.Tables[0].Rows[0]["HAC_LoginPageContent"].ToString();
                    generalConfigurationVo.ApplicationName = dsGeneralConfiguration.Tables[0].Rows[0]["HAC_ApplicationName"].ToString();
                    generalConfigurationVo.Email = dsGeneralConfiguration.Tables[0].Rows[0]["HAC_Email"].ToString(); 
                }
                
            }
            catch (Exception ex)
            {                
            }
            return generalConfigurationVo;

        }
    }
}
