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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                customerVo = (CustomerVo)Session["CustomerVo"];
                userVo = (UserVo)Session["userVo"];

                rmVo = (RMVo)Session["RMVo"];

                BindGrid();
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

        private void BindGrid()
        {
            try
            {
                dtAssociations = customerFamilyBo.GetCustomerAssociations(rmVo.RMId, hdnNameFilter.Value);
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
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('GroupAccountSetup','action=Edit');", true);

            }
        }
        protected void gvCustomerFamily_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            String str;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.FindControl("LinkButton1") != null)
                {
                    if (e.Row.Cells[2].Text.ToString()=="Self")
                    {
                        str = ((LinkButton)e.Row.FindControl("LinkButton1")).Text.ToString();
                        e.Row.Cells[0].Controls.Remove(e.Row.FindControl("LinkButton1"));
                        e.Row.Cells[0].Text = str;
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
    }
}
