using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUser;
using VoCustomerProfiling;
using BoCustomerProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using BoCommon;

namespace WealthERP.Customer
{
    public partial class CustomerDashboard : System.Web.UI.UserControl
    {
        UserVo userVo=new UserVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        List<CustomerFamilyVo> customerFamilyList = new List<CustomerFamilyVo>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];
                customerVo = customerBo.GetCustomerInfo(userVo.UserId);
                Session["CustomerVo"] = customerVo;
                lblName.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                lblPhone.Text = customerVo.ResISDCode.ToString() + " - " + customerVo.ResSTDCode.ToString() + " - " + customerVo.ResPhoneNum.ToString();
                lblAddress.Text = customerVo.Adr1Line1.ToString() + "<br />" + customerVo.Adr1Line1.ToString() + "<br />" + customerVo.Adr1Line2.ToString() + "<br />" + customerVo.Adr1Line3.ToString() + "<br />" + customerVo.Adr1PinCode.ToString() + "<br />" + customerVo.Adr1State.ToString() + "<br />" + customerVo.Adr1City.ToString() + "<br />" + customerVo.Adr1Country.ToString();
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
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerIndividualDashboard.ascx:Page_Load()");

                object[] objects = new object[2];
                
                objects[1] = customerVo;
                objects[2] = userVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        //protected void gvrPersonal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvFamilyMembers.PageIndex = e.NewPageIndex;
        //    gvFamilyMembers.DataBind();
        //}
    }
}