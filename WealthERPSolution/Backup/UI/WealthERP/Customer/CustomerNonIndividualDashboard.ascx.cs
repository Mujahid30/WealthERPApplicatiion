using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using VoCustomerProfiling;
using BoUser;
using BoCustomerProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;


namespace WealthERP.Customer
{
    public partial class CustomerNonIndividualDashboard : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        RMVo rmVo = new RMVo();
        CustomerBo customerBo = new CustomerBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                customerVo = (CustomerVo)Session["CustomerVo"];
                rmVo = (RMVo)Session["RmVo"];
                Session["RmVo"] = rmVo;
                Session["CustomerVo"] = customerVo;
                lblName.Text = customerVo.CompanyName.ToString();                
                lblPhone.Text = customerVo.ResISDCode.ToString() + " - " + customerVo.ResSTDCode.ToString() + " - " + customerVo.ResPhoneNum.ToString();
                lblAddress.Text = customerVo.Adr1Line1.ToString() + "<br/>" + customerVo.Adr1Line2.ToString() + "<br/>" + customerVo.Adr1Line3.ToString() + "<br/>" + customerVo.Adr1PinCode.ToString() + "<br/>" + customerVo.Adr1State.ToString() + "<br/>" + customerVo.Adr1City.ToString() + "<br/>" + customerVo.Adr1Country.ToString();
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerNonIndividualDashboard.ascx:Page_Load()");
                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = customerVo;
                objects[2] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }
    }
}