using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using System.Configuration;
using System.Data;
using WealthERP.Base;
using VoUser;
using BoCustomerProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace WealthERP.Customer
{
    public partial class GroupAccountSetup : System.Web.UI.UserControl
    {
        string path;
        int customerId;
        int associateId;
        int associationId;
        DataTable dtAssociateDetails;
        RMVo rmVo = new RMVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        DataTable dtRelationship = new DataTable();
        UserVo userVo = new UserVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["UserVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            rmVo=(RMVo)Session[SessionContents.RmVo];
            
            txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
            txtMemberCustomer_AutoCompleteExtender.ContextKey = rmVo.RMId.ToString();
            if(!IsPostBack)
            {
                BindRelationshipDropDown();
                if (Request.QueryString["action"] == "Edit")
                {

                    btnSave.Visible = false;
                    associationId=int.Parse(Session["AssociationId"].ToString());
                    dtAssociateDetails=customerFamilyBo.GetCustomerAssociateDetails(associationId);

                    txtParentCustomer.Text = dtAssociateDetails.Rows[0]["ParentName"].ToString();
                    txtParentCustomerId.Value = dtAssociateDetails.Rows[0]["ParentId"].ToString();
                    txtAddressParent.Text = dtAssociateDetails.Rows[0]["ParentAddress"].ToString();
                    txtPanParent.Text = dtAssociateDetails.Rows[0]["ParentPan"].ToString();
                    txtParentCustomerType.Value = dtAssociateDetails.Rows[0]["CustomerTypeCode"].ToString();

                    txtMemberCustomer.Text = dtAssociateDetails.Rows[0]["MemberName"].ToString();
                    txtMemberCustomerId.Value = dtAssociateDetails.Rows[0]["MemberId"].ToString();
                    txtAddressMember.Text = dtAssociateDetails.Rows[0]["MemberAddress"].ToString();
                    txtPanMember.Text = dtAssociateDetails.Rows[0]["MemberPan"].ToString();

                    BindRelationshipDropDown();

                    ddlRelationship.SelectedValue = dtAssociateDetails.Rows[0]["Relationship"].ToString();

                }
            }
        }

        private void BindRelationshipDropDown()
        {
            if (txtParentCustomerType.Value != string.Empty)
            {
                if(ddlRelationship.Items.Count > 0)
                    ddlRelationship.Items.RemoveAt(0);
                dtRelationship = XMLBo.GetRelationship(path, txtParentCustomerType.Value);
                ddlRelationship.DataSource = dtRelationship;
                ddlRelationship.DataTextField = "Relationship";
                ddlRelationship.DataValueField = "RelationshipCode";
                ddlRelationship.DataBind();
            }
            ddlRelationship.Items.Insert(0, new ListItem("Select Relationship", "Select Relationship"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveAssociateDetails();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerFamily','none');", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveAssociateDetails();
            ClearAll();
            BindRelationshipDropDown();
        }

        protected void txtParentCustomer_TextChanged(object sender, EventArgs e)
        {
            if (txtParentCustomerId.Value != string.Empty)
            {
                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtParentCustomerId.Value));
                DataRow dr = dt.Rows[0];
                txtPanParent.Text = dr["C_PANNum"].ToString();
                txtAddressParent.Text = dr["C_Adr1Line1"].ToString();
                txtParentCustomerType.Value = dr["XCT_CustomerTypeCode"].ToString();
                BindRelationshipDropDown();
            }
        }

        protected void txtMemberCustomer_TextChanged(object sender, EventArgs e)
        {
            if (txtMemberCustomerId.Value != string.Empty)
            {
                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtMemberCustomerId.Value));
                DataRow dr = dt.Rows[0];
                txtPanMember.Text = dr["C_PANNum"].ToString();
                txtAddressMember.Text = dr["C_Adr1Line1"].ToString();
            }
        }

        private void SaveAssociateDetails()
        {
            string relCode;
            try
            {
                if (txtParentCustomerId.Value != string.Empty)
                {
                    customerId = int.Parse(txtParentCustomerId.Value);
                }
                if (txtMemberCustomerId.Value != string.Empty)
                {
                    associateId = int.Parse(txtMemberCustomerId.Value);
                }

                relCode = ddlRelationship.SelectedItem.Value;
                customerFamilyBo.CustomerAssociateUpdate(customerId, associateId, relCode, userVo.UserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GroupAccountSetup.ascx:SaveAssociateDetails()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void ClearAll()
        {
            txtParentCustomerType.Value = "";
            txtParentCustomerId.Value = "";
            txtMemberCustomerId.Value = "";
            txtParentCustomer.Text = "";
            txtPanParent.Text = "";
            txtAddressParent.Text = "";
            txtMemberCustomer.Text = "";
            txtPanMember.Text = "";
            txtAddressMember.Text = "";
            ddlRelationship.Items.Clear();
        }
    }
}