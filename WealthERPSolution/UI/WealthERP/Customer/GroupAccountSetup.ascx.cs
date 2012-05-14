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
using BoAdvisorProfiling;

namespace WealthERP.Customer
{
    public partial class GroupAccountSetup : System.Web.UI.UserControl
    {
        string path;
        int customerId;
        int associateId;
        int associationId;
        DataTable dtAssociateDetails;
        DataTable dtAssociateDetails1;
        DataSet dsAssociateDetails;
        RMVo rmVo = new RMVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        DataTable dtRelationship = new DataTable();
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = new AdvisorVo();
        int selectedRMId = 0;
        DataTable dtGetAllTheRMList = new DataTable();
        DataSet dsGetAllTheRMList = new DataSet();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["UserVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            rmVo=(RMVo)Session[SessionContents.RmVo];

            lblRMsBranch.Text = string.Empty;
            //txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
            //txtMemberCustomer_AutoCompleteExtender.ContextKey = rmVo.RMId.ToString();

            BindRMDropDown();
            if (!IsPostBack)
            {
                BindRelationshipDropDown();
                BindRMDropDown();
                if (Request.QueryString["action"] == "Edit")
                {

                    btnSave.Visible = false;
                    associationId = int.Parse(Session["AssociationId"].ToString());
                    dsAssociateDetails = customerFamilyBo.GetCustomerAssociateDetails(associationId);
                    if (dsAssociateDetails != null)
                    {
                        dtAssociateDetails = dsAssociateDetails.Tables[0];
                        dtAssociateDetails1 = dsAssociateDetails.Tables[1];
                    }


                    txtParentCustomer.Text = dtAssociateDetails.Rows[0]["ParentName"].ToString();
                    txtParentCustomerId.Value = dtAssociateDetails.Rows[0]["ParentId"].ToString();
                    txtAddressParent.Text = dtAssociateDetails.Rows[0]["ParentAddress"].ToString();
                    txtPanParent.Text = dtAssociateDetails.Rows[0]["ParentPan"].ToString();
                    txtParentCustomerType.Value = dtAssociateDetails.Rows[0]["CustomerTypeCode"].ToString();

                    txtMemberCustomer.Text = dtAssociateDetails.Rows[0]["MemberName"].ToString();
                    txtMemberCustomerId.Value = dtAssociateDetails.Rows[0]["MemberId"].ToString();
                    txtAddressMember.Text = dtAssociateDetails.Rows[0]["MemberAddress"].ToString();
                    txtPanMember.Text = dtAssociateDetails.Rows[0]["MemberPan"].ToString();
                    txtParentCustomer.Enabled = false;
                    txtMemberCustomer.Enabled = false;
                    BindRelationshipDropDown();
                    BindRMDropDown();
                    ddlSelectRMs.Enabled = false;
                    ddlSelectRMs.SelectedValue = dtAssociateDetails.Rows[0]["AR_RMId"].ToString();
                    ddlRelationship.SelectedValue = dtAssociateDetails.Rows[0]["Relationship"].ToString();
                    txtStaffCode.Text = !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["StaffCode"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["StaffCode"].ToString().Trim() : string.Empty;

                    if (dtAssociateDetails1.Rows.Count != 0)
                    {
                        int Increment = 0;

                        foreach (DataRow dr in dtAssociateDetails1.Rows)
                        {
                            if (Increment == 0)
                                lblRMsBranch.Text += dr["AB_BranchName"].ToString();
                            else
                                lblRMsBranch.Text += " ," + dr["AB_BranchName"].ToString();

                            Increment++;
                        }
                    }
                    else
                    {
                        lblRMsBranch.Text = string.Empty;
                    }


                }
            }
        }

        // Created by Vinayak Patil
        // TO GET ALL THE STAFFS WHO IS HAVING ONLY ADMIN AND RM ROLES UNDER THE PERTICULAR ADVISER

        private void BindRMDropDown()
        {
            try
            {
                dsGetAllTheRMList = advisorStaffBo.GetAllAdviserRMsHavingOnlyAdminRMRole(advisorVo.advisorId, 0);
                if (dsGetAllTheRMList != null)
                {
                    dtGetAllTheRMList = dsGetAllTheRMList.Tables[0];
                    if (dtGetAllTheRMList.Rows.Count > 0)
                    {
                        ddlSelectRMs.DataSource = dtGetAllTheRMList;
                        ddlSelectRMs.DataValueField = dtGetAllTheRMList.Columns["RMId"].ToString();
                        ddlSelectRMs.DataTextField = dtGetAllTheRMList.Columns["RMName"].ToString();
                        ddlSelectRMs.DataBind();
                    }
                }
                ddlSelectRMs.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select RM", "Select RM"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindRMDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
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
            ddlRelationship.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveAssociateDetails();
            Response.Redirect("ControlHost.aspx?pageid=ViewCustomerFamily&RMId=" + selectedRMId + "", false);
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerFamily','none');", true);
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
                if (txtParentCustomerType.Value == "IND")
                {
                    txtMemberCustomer_AutoCompleteExtender.ContextKey = ddlSelectRMs.SelectedValue.ToString() + "|" + txtParentCustomerId.Value;
                    BindRelationshipDropDown();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Selected Parent Customer is a Non Individua.Hence can not Proceed');", true);
                    txtParentCustomer.Text = "";
                    txtPanParent.Text = "";
                    txtAddressParent.Text = "";
                    return;
                }
                
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

        protected void ddlSelectRMs_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsGetBranchName = new DataSet();
            DataTable dtGetBranchName = new DataTable();
            DataRow[] drRM;

            if (ddlSelectRMs.SelectedIndex != 0)
                selectedRMId = int.Parse(ddlSelectRMs.SelectedValue);
            else
                selectedRMId = 0;

            drRM = dtGetAllTheRMList.Select("RMId=" + selectedRMId);
            txtStaffCode.Text = !string.IsNullOrEmpty(drRM[0][2].ToString().Trim()) ? drRM[0][2].ToString().Trim() : string.Empty;

            txtParentCustomer_autoCompleteExtender.ContextKey = selectedRMId.ToString();
            txtMemberCustomer_AutoCompleteExtender.ContextKey = selectedRMId.ToString();

            dsGetBranchName = advisorStaffBo.GetAllAdviserRMsHavingOnlyAdminRMRole(0, selectedRMId);
            if (dsGetBranchName != null)
            {
                int count = 0;
                dtGetBranchName = dsGetBranchName.Tables[1];

                foreach (DataRow dr in dtGetBranchName.Rows)
                {
                    if (count == 0)
                        lblRMsBranch.Text += dr["AB_BranchName"].ToString();
                    else
                        lblRMsBranch.Text += " ," + dr["AB_BranchName"].ToString();

                    count++;
                }
            }
            else
            {
                lblRMsBranch.Text = string.Empty;
            }

        }

    }
}