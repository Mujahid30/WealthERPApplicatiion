using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoAdvisorProfiling;
using System.Configuration;
using System.Collections.Specialized;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace WealthERP.Advisor
{
    public partial class ViewLOBDetails : System.Web.UI.UserControl
    {
        int LOBId;
        AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
        AdvisorLOBVo advisorLOBVo;
        
        string path;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

                LOBId = int.Parse(Session["LOBId"].ToString());
                advisorLOBVo = advisorLOBBo.GetLOB(LOBId);
                lblBtype.Text = XMLBo.GetLOBType(path, advisorLOBVo.LOBClassificationCode.ToString());
                lblIdentifier.Text = advisorLOBVo.Identifier.ToString();
                lblLicenseNumber.Text = advisorLOBVo.LicenseNumber.ToString();
                lblOrgname.Text = advisorLOBVo.OrganizationName.ToString();
                lblValiditydate.Text = advisorLOBVo.ValidityDate.ToShortDateString().ToString();

                if (advisorLOBVo.LOBClassificationCode == "LMIT" || advisorLOBVo.LOBClassificationCode == "LESC" || advisorLOBVo.LOBClassificationCode == "LERC" || advisorLOBVo.LOBClassificationCode == "LEBC")
                {
                    lblLicenseNo.Visible = false;
                    lblLicenseNumber.Visible = false;
                }
                if (advisorLOBVo.LOBClassificationCode == "LMIT")
                {
                    lblValidity.Visible = true;
                    lblValidity.Visible = true;
                }
                else
                {

                    lblValidity.Visible = false;
                    lblValiditydate.Visible = false;
                    DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);

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

                FunctionInfo.Add("Method", "ViewLOBDetails.ascx:Page_Load()");


                object[] objects = new object[3];
                
                objects[1] = advisorLOBVo;
                objects[2] = LOBId;
                objects[3] = path;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

       
   

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                LOBId = int.Parse(Session["LOBId"].ToString());
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('EditLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewLOBDetails.ascx.cs:LinkButton1_Click()");
                object[] objects = new object[1];
                objects[0] = LOBId;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewLOB','none');", true);
        }
    }
}