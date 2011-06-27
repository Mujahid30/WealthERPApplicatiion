using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoHostConfig
{
    public class GeneralConfigurationVo
    {
        # region Private_Fields
        private string hac_hostLogoPlacement;
        private string hac_hostLogo;
        private string hac_adviserLogoPlacement;
        private string hac_defaultTheme;
        private string hac_contactPersonName;
        private Int64 hac_contactPersonTelephoneNumber;
        private string hac_loginPageContent;
        private string hac_applicationName;
        private string hac_email;
        # endregion

        # region Property
        public string AdviserLogoPlacement
        {
            get { return hac_adviserLogoPlacement; }
            set { hac_adviserLogoPlacement = value; }
        }
        public string HostLogo
        {
            get { return hac_hostLogo; }
            set { hac_hostLogo = value; }
        }
        public string HostLogoPlacement
        {
            get { return hac_hostLogoPlacement; }
            set { hac_hostLogoPlacement = value; }
        }
        public string DefaultTheme
        {
            get { return hac_defaultTheme; }
            set { hac_defaultTheme = value; }
        }
        public string LoginPageContent
        {
            get { return hac_loginPageContent; }
            set { hac_loginPageContent = value; }
        }

        public string ContactPersonName
        {
            get { return hac_contactPersonName; }
            set { hac_contactPersonName = value; }
        }
        public Int64 ContactPersonTelephoneNumber
        {
            get { return hac_contactPersonTelephoneNumber; }
            set { hac_contactPersonTelephoneNumber = value; }
        }
        public string ApplicationName
        {
            get { return hac_applicationName; }
            set { hac_applicationName = value; }
        }
        public string Email
        {
            get { return hac_email; }
            set { hac_email = value; }
        }
        # endregion
    }
}
