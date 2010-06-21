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
using System.Data;
using WealthERP.Base;
using BoCommon;
using System.Configuration;
using System.Text;
namespace WealthERP.Advisor
{
    public partial class RMCustomerDashboard : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        RMVo rmVo = new RMVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        string path = "";
        List<CustomerFamilyVo> customerFamilyList=null;
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            try
            {
              //  customerFamilyList = new List<CustomerFamilyVo>();

                customerVo = (CustomerVo)Session["CustomerVo"];
                rmVo = (RMVo)Session["RmVo"];
                StringBuilder sbAddress = new StringBuilder();
                
                lblName.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                lblPhone.Text = customerVo.ResISDCode.ToString() + " - " + customerVo.ResSTDCode.ToString() + " - " + customerVo.ResPhoneNum.ToString();

                sbAddress.Append(customerVo.Adr1Line1.ToString());
                sbAddress.Append("<br />");
                sbAddress.Append(customerVo.Adr1Line2.ToString());
                sbAddress.Append("<br />");
                sbAddress.Append(customerVo.Adr1Line3.ToString());
                sbAddress.Append("<br />");
                sbAddress.Append(customerVo.Adr1PinCode.ToString());
                sbAddress.Append("<br />");

                if (customerVo.Adr1State.ToString() != "")
                {
                    sbAddress.Append(XMLBo.GetStateName(path, customerVo.Adr1State.ToString()));
                    sbAddress.Append("<br />");
                }
                
                sbAddress.Append(customerVo.Adr1City.ToString());
                sbAddress.Append("<br />");
                sbAddress.Append(customerVo.Adr1Country.ToString());

                lblAddress.Text = sbAddress.ToString();

                Session["RmVo"] = rmVo;
                // Session["CustomerVo"] = customerVo;
                Session["Check"] = "Dashboard";

                customerFamilyList = customerFamilyBo.GetCustomerFamily(customerVo.CustomerId);
                if (customerFamilyList != null)
                {
                    trFamilyMembers.Visible = true;
                    DataTable dtCustomerFamilyList = new DataTable();
                    dtCustomerFamilyList.Columns.Add("AssociationId");
                    dtCustomerFamilyList.Columns.Add("Name");
                    dtCustomerFamilyList.Columns.Add("Relationship");

                    DataRow drCustomerFamily;
                    for (int i = 0; i < customerFamilyList.Count; i++)
                    {
                        drCustomerFamily = dtCustomerFamilyList.NewRow();
                        CustomerFamilyVo customerFamilyVo = new CustomerFamilyVo();
                        customerFamilyVo = customerFamilyList[i];
                        drCustomerFamily[0] = customerFamilyVo.AssociationId.ToString();
                        drCustomerFamily[1] = customerFamilyVo.AssociateCustomerName.ToString();
                        drCustomerFamily[2] = customerFamilyVo.Relationship;
                        dtCustomerFamilyList.Rows.Add(drCustomerFamily);

                    }
                    if (dtCustomerFamilyList.Rows.Count > 0)
                    {
                        gvFamilyMembers.DataSource = dtCustomerFamilyList;
                        gvFamilyMembers.DataBind();
                        gvFamilyMembers.Visible = true;
                        trFamilyMembers.Visible = true;
                    }
                    else
                    {
                        gvFamilyMembers.DataSource = null;
                        gvFamilyMembers.DataBind();
                        trFamilyMembers.Visible = false;
                    }
                    
                }
                else
                {
                    gvFamilyMembers.DataSource = null;
                    gvFamilyMembers.DataBind();
                    trFamilyMembers.Visible = false;
                    
                }
                //lblMobile.Text = customerVo.Mobile1.ToString();
                //lblArea.Text = customerVo.Adr1Line3.ToString();
                //lblEmail.Text = customerVo.Email.ToString();
                //lblPanNum.Text = customerVo.PANNum.ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RMCustomerDashboard.ascx:Page_Load()");
                object[] objects = new object[4];
                objects[0] = customerFamilyList;
                objects[1] = rmVo;
                objects[2] = customerVo;
                objects[3] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvFamilyMembers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFamilyMembers.PageIndex = e.NewPageIndex;
            gvFamilyMembers.DataBind();
        }

        
    }
}