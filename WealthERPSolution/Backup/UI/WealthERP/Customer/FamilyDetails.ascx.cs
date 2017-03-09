using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerProfiling;
using VoUser;
using BoCustomerProfiling;
using BoUser;
using BoCommon;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace WealthERP.Customer
{
    public partial class FamilyDetails : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        CustomerFamilyVo customerFamilyVo = new CustomerFamilyVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        int userId;
        DataTable dtRelationship = new DataTable();
        string path;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                if (Session["Current_Link"].ToString() == "RMCustomerIndividualLeftPane")
                {
                    dtRelationship = XMLBo.GetRelationship(path, "IND");
                    ddlRelationship.DataSource = dtRelationship;
                    ddlRelationship.DataTextField = "Relationship";
                    ddlRelationship.DataValueField = "RelationshipCode";
                    ddlRelationship.DataBind();
                }
                else if (Session["Current_Link"].ToString() == "RMCustomerNonIndividualLeftPane")
                {
                    dtRelationship = XMLBo.GetRelationship(path, "NIND");
                    ddlRelationship.DataSource = dtRelationship;
                    ddlRelationship.DataTextField = "Relationship";
                    ddlRelationship.DataValueField = "RelationshipCode";
                    ddlRelationship.DataBind();
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
                FunctionInfo.Add("Method", "FamilyDetails.ascx:Page_Load()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {


            try
            {
                userId = rmVo.UserId;

                //string membercustomerId = "";


                //customerFamilyVo.MemberCustomerId = customerBo.GenerateId();
                //customerFamilyVo.Id = customerBo.GenerateId();
                //customerFamilyVo.Relationship = ddlRelationship.SelectedItem.Value;
                //customerFamilyBo.CreateCustomerFamily(customerFamilyVo, customerVo.CustomerId, userId);
                //Session["CustomerId2"] = customerFamilyVo.MemberCustomerId.ToString();
                Session["relationship"] = ddlRelationship.SelectedItem.Value;
                if (Session["Current_Link"].ToString() == "RMCustomerNonIndividualLeftPane")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerNonIndividualAdd','none');", true);
                }
                else if (Session["Current_Link"].ToString() == "RMCustomerIndividualLeftPane")
                {


                    if (ddlRelationship.SelectedItem.Value == "CH")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('FamilyDetailsChild','none');", true);
                    }
                    else if (ddlRelationship.SelectedItem.Value == "SP")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('FamilyDetailsSpouse','none');", true);

                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('FamilyDetailsOther','none');", true);

                    }
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
                FunctionInfo.Add("Method", "FamilyDetails.ascx:btnAdd_Click()");
                object[] objects = new object[2];
                objects[0] = userId;
                objects[1] = rmVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
    }
}