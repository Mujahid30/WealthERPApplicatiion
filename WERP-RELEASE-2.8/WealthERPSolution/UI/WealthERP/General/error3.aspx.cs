using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoHostConfig;
using WealthERP.Base;
using System.Text;

namespace WealthERP.General
{
    public partial class error3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GeneralConfigurationVo generalconfigurationvo = new GeneralConfigurationVo();
            StringBuilder sbText = new StringBuilder();
            if (!IsPostBack)
            {
                if (Session[SessionContents.SAC_HostGeneralDetails] != null)
                {
                    generalconfigurationvo = (GeneralConfigurationVo)Session[SessionContents.SAC_HostGeneralDetails];
                    sbText.Append("3. Contact ");
                    if (!string.IsNullOrEmpty(generalconfigurationvo.ApplicationName))
                    {
                        sbText.Append(generalconfigurationvo.ApplicationName);
                    }
                    else
                    {
                        sbText.Append("<Application Name not Filled>");
                    }
                    sbText.Append(" team (");
                    if (!string.IsNullOrEmpty(generalconfigurationvo.ContactPersonName))
                    {
                        sbText.Append(generalconfigurationvo.ContactPersonName);
                    }
                    else
                    {
                        sbText.Append("<Contact Peson Name not Filled>");
                    }
                    sbText.Append(") at ");
                    if (generalconfigurationvo.ContactPersonTelephoneNumber != 0)
                    {
                        sbText.Append(generalconfigurationvo.ContactPersonTelephoneNumber);
                    }
                    else
                    {
                        sbText.Append("<Telephone Number not Filled>");
                    }
                    lblContactDetails.Text = sbText.ToString();
                }
            }
        }
    }
}
