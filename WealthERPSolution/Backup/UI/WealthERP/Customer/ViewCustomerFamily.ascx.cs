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
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoAdvisorProfiling;

namespace WealthERP.Customer
{
    public partial class ViewCustomerFamily : System.Web.UI.UserControl
    {
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        CustomerFamilyVo customerFamilyVo;
        CustomerVo customerVo = null;
        CustomerVo customerMemberVo = null;
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = new UserVo();
        DataTable dtAssociations;
        int customerId;
        int memberCustomerId;
        RMVo rmVo = new RMVo();
        int rmId = 0;
        DataTable dtGetAllTheRMList = new DataTable();
        DataSet dsGetAllTheRMList = new DataSet();
        AdvisorVo advisorVo = new AdvisorVo();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                customerVo = (CustomerVo)Session["CustomerVo"];
                userVo = (UserVo)Session["userVo"];
                advisorVo = (AdvisorVo)Session["advisorVo"];
                rmVo = (RMVo)Session["RMVo"];

                if (!IsPostBack)
                {
                    BindRMDropDown();
                    if (Request.QueryString["RMId"] != null)
                    {
                        rmId = int.Parse(Request.QueryString["RMId"].ToString());
                        ddlSelectRMs.SelectedValue = rmId.ToString();

                        hdnadviserId.Value = "0";
                        hdnrmId.Value = rmId.ToString();
                    }
                    else if (((Request.QueryString["AfterDeassociationRMId"] != null) || (Request.QueryString["AfterDeassociationAdviserId"] != null)) && (Request.QueryString["action"] == "Deassociation"))
                    {
                        
                        if (Request.QueryString["AfterDeassociationRMId"] != null)
                        {
                            hdnadviserId.Value = "0";
                            hdnrmId.Value = Request.QueryString["AfterDeassociationRMId"];
                            ddlSelectRMs.SelectedValue = Request.QueryString["AfterDeassociationRMId"];
                        }
                        else
                        {
                            hdnrmId.Value = "0";
                            hdnadviserId.Value = Request.QueryString["AfterDeassociationAdviserId"];
                            ddlSelectRMs.SelectedValue = Request.QueryString["AfterDeassociationAdviserId"];
                        }
                    }
                    else
                    {
                        hdnadviserId.Value = advisorVo.advisorId.ToString();
                        hdnrmId.Value = "0";
                    }


                    BindGrid();
                }
                //lblMessage.Visible = true;
                //gvCustomerFamily.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewCustomerFamily.ascx:Page_Load()");
                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = customerId;
                objects[2] = customerMemberVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        // Created by Vinayak Patil
        // TO GET ALL THE STAFFS WHO IS HAVING ONLY ADMIN AND RM ROLES UNDER THE PERTICULAR ADVISER

        private void BindRMDropDown()
        {
            try
            {
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
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
                ddlSelectRMs.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", advisorVo.advisorId.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ViewCustomerFamily.ascx.cs:BindRMDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindGrid()
        {
            try
            {
                dtAssociations = customerFamilyBo.GetCustomerAssociations(int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), hdnNameFilter.Value);
                if (dtAssociations.Rows.Count == 0)
                {
                    lblMessage.Visible = true;
                    gvCustomerFamily.Visible = false;
                }
                else
                {
                    lblMessage.Visible = false;
                    gvCustomerFamily.DataSource = dtAssociations;
                    gvCustomerFamily.DataBind();
                    gvCustomerFamily.Visible = true;
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
                FunctionInfo.Add("Method", "ViewCustomerFamily.ascx:BindGrid()");
                object[] objects = new object[6];
                objects[0] = customerVo;
                objects[1] = memberCustomerId;
                objects[2] = customerMemberVo;
                objects[3] = customerFamilyVo;
                objects[5] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }

        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "ViewCustomerFamily.ascx:btnDeleteSelected_Click()");
        //        object[] objects = new object[0];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //protected void hiddenDelete_Click(object sender, EventArgs e)
        //{
        //    string val = Convert.ToString(hdnMsgValue.Value);
        //    if (val == "1")
        //    {
        //        DeleteGroupAccount();
        //    }
        //    else
        //    {
        //        ClearCheckBox();
        //    }
        //}
        //protected void ClearCheckBox()
        //{
        //    foreach (GridViewRow dr in gvCustomerFamily.Rows)
        //    {
        //        CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
        //        checkBox.Checked = false;
        //    }
        //}
        //protected void DeleteGroupAccount()
        //{
        //    int CustomerAssociationID = 0;
        //    int Asso_customerId = 0;
        //    CustomerFamilyVo familyVo = new CustomerFamilyVo();
        //    try
        //    {
        //        foreach (GridViewRow dr in gvCustomerFamily.Rows)
        //        {
        //            customerVo = (CustomerVo)Session["CustomerVo"];
        //            customerId = customerVo.CustomerId;

        //            CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
        //            if (checkBox.Checked)
        //            {
        //                CustomerAssociationID = Convert.ToInt32(gvCustomerFamily.DataKeys[dr.RowIndex].Values["AssociationId"].ToString());
        //                familyVo = customerFamilyBo.GetCustomerFamilyAssociateDetails(CustomerAssociationID);
        //                Asso_customerId = Convert.ToInt32(gvCustomerFamily.DataKeys[dr.RowIndex].Values["CustomerId"].ToString());
        //                familyVo.Relationship = "SELF";
        //                familyVo.CustomerId = familyVo.AssociateCustomerId;
        //               // familyVo.AssociateCustomerId = Asso_customerId;
        //                customerFamilyBo.UpdateCustomerAssociate(familyVo, Asso_customerId, userVo.UserId);

        //            }

        //        }
        //        BindGrid(customerId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }

        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "ViewCustomerFamily.ascx:DeleteGroupAccount()");
        //        object[] objects = new object[3];
        //        objects[0] = customerVo;
        //        objects[1] = customerId;
        //        objects[2] = CustomerAssociationID;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        protected void gvCustomerFamily_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditDetails")
            {
                Session["AssociationId"] = e.CommandArgument.ToString();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('GroupAccountSetup','?action=Edit');", true);

            }
        }
        protected void gvCustomerFamily_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            String str;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.FindControl("LinkButton1") != null)
                {
                    if (e.Row.Cells[3].Text.ToString() == "Self")
                    {
                        str = ((LinkButton)e.Row.FindControl("LinkButton1")).Text.ToString();
                        e.Row.Cells[1].Controls.Remove(e.Row.FindControl("LinkButton1"));
                        e.Row.Cells[1].Text = str;
                    }
                }
            }

        }
        protected void btnNameSearch_Click1(object sender, EventArgs e)
        {
            TextBox txtName = GetCustNameTextBox();

            if (txtName != null)
            {
                hdnNameFilter.Value = txtName.Text.Trim();
                this.BindGrid();
            }
        }

        private TextBox GetCustNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvCustomerFamily.HeaderRow != null)
            {
                if ((TextBox)gvCustomerFamily.HeaderRow.FindControl("txtCustNameSearch") != null)
                {
                    txt = (TextBox)gvCustomerFamily.HeaderRow.FindControl("txtCustNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        protected void gvCustomerFamily_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomerFamily.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void Deactive_Click(object sender, EventArgs e)
        {
            //ddlSelectRMs.EnableViewState = true;

            int i = 0;

            foreach (GridViewRow gvr in this.gvCustomerFamily.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                {
                    i = i + 1;
                }
            }

            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select Customer!');", true);
            }
            else
            {
                string GoalIds = GetSelectedGoalIDString();
                int folioDs = customerFamilyBo.CustomerFamilyDissociation(GoalIds);
                if (folioDs > 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showassocation();", true);
                }
                else
                {
                    Dissociatecustomer();
                }

            }
        }
         protected void hiddenassociationfound_Click(object sender, EventArgs e)
         {
            
                 Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerFamily','none');", true);
             
         }

         protected void Dissociatecustomer()
         {
             string GoalIds = GetSelectedGoalIDString();
             int folioDs = customerFamilyBo.CustomerDissociate(GoalIds, userVo.UserId);

             //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerFamily','none');", true);
             if(ddlSelectRMs.SelectedIndex != 0)
                Response.Redirect("ControlHost.aspx?pageid=ViewCustomerFamily&AfterDeassociationRMId=" + ddlSelectRMs.SelectedValue + "&action=Deassociation", false);
             else
                 Response.Redirect("ControlHost.aspx?pageid=ViewCustomerFamily&AfterDeassociationAdviserId=" + advisorVo.advisorId + "&action=Deassociation", false);
            
         }
         private string GetSelectedGoalIDString()
         {
             string gvGoalIds = "";

             //'Navigate through each row in the GridView for checkbox items
             foreach (GridViewRow gvRow in gvCustomerFamily.Rows)
             {
                 CheckBox ChkBxItem = (CheckBox)gvRow.FindControl("chkId");
                 if (ChkBxItem.Checked)
                 {
                     gvGoalIds += Convert.ToString(gvCustomerFamily.DataKeys[gvRow.RowIndex].Value) + "~";
                 }
             }
             return gvGoalIds;

         }

         protected void ddlSelectRMs_SelectedIndexChanged(object sender, EventArgs e)
         {
             if (ddlSelectRMs.SelectedIndex != 0)
             {
                 hdnadviserId.Value = "0";
                 hdnrmId.Value = ddlSelectRMs.SelectedValue;
             }
             else
             {
                 hdnadviserId.Value = advisorVo.advisorId.ToString();
                 hdnrmId.Value = "0";

             }
             BindGrid();
         }
    }
}
