using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoOps;
using VoUser;
using System.Data;
using VoOps;

namespace WealthERP.OPS
{
    public partial class CustomerOrderList : System.Web.UI.UserControl
    {
        OperationBo operationBo = new OperationBo();
        OperationVo operationVo = new OperationVo();
        CustomerVo customerVo = null;
        int customerId;
        int status;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnApprove.Visible = false;
            if (!IsPostBack)
            {
                BindCustomerList();

            }
          
        }

        private void BindCustomerList()
        {
            DataSet dsCustomerApprovalList = new DataSet();
            DataTable dtCustomerApprovallist = new DataTable();
            status = 1;
            try
            {
                SessionBo.CheckSession();
                customerVo = (CustomerVo)Session["CustomerVo"];
                customerId = customerVo.CustomerId;
                status = int.Parse(ddlOrderType.SelectedValue);
                dsCustomerApprovalList = operationBo.GetCustomerApprovalList(customerId,status);
                dtCustomerApprovallist = dsCustomerApprovalList.Tables[0];
                if (dtCustomerApprovallist.Rows.Count > 0)
                {
                    gvOrderList.DataSource = dtCustomerApprovallist;
                    gvOrderList.DataBind();
                    gvOrderList.Visible = true;
                    ErrorMessage.Visible = false;
                    tblMessage.Visible = false;
                    //btnApprove.Visible = false;
                }
                else
                {
                    gvOrderList.Visible = false;
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "No Records Found...!";
                    btnApprove.Visible = false;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            int i = 0;
            int gvOrderId = 0;
            bool result = false;
            foreach (GridViewRow gvRow in gvOrderList.Rows)
            {

                CheckBox chk = (CheckBox)gvRow.FindControl("cbRecons");
                if (chk.Checked)
                {
                    i++;
                }

            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record!');", true);
                BindCustomerList();
            }
            else
            {
                foreach (GridViewRow gvRow in gvOrderList.Rows)
                {
                    if (((CheckBox)gvRow.FindControl("cbRecons")).Checked == true)
                    {
                        gvOrderId = Convert.ToInt32(gvOrderList.DataKeys[gvRow.RowIndex].Values["CMOT_MFOrderId"].ToString());
                        operationBo.UpdateCustomerApprovalList(gvOrderId);
                       
                    }
                }
                BindCustomerList();
            }
        }

        protected void gvOrderList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
           
            if (e.CommandName == "ViewOrderDetails")
            {
                try
                {
                    index = Convert.ToInt32(e.CommandArgument);
                     

                    int orderId = Convert.ToInt32(gvOrderList.DataKeys[index].Value.ToString());
                    //if(int.Parse(ddlOrderType.SelectedValue)==1)
                    //    e.Row.Cells[index].Visible = false;

                    operationVo = operationBo.GetCustomerOrderTrackingDetails(orderId);
                    Session["operationVo"] = operationVo;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderEntry", "loadcontrol('OrderEntry','?action=" +"View"+ "&FromPage="+"OrderList"+ "');", true);

                }
                catch (BaseApplicationException Ex)
                {
                    throw Ex;
                }
            
            }
               
        }
        protected void gvOrderList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TemplateField b = gvOrderList.Columns[0] as TemplateField;

                if (int.Parse(ddlOrderType.SelectedValue) == 1)
                    b.Visible = false;
                else
                    b.Visible = true;
                
            }
        }
        protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(ddlOrderType.SelectedValue) == 0)
            {
                BindCustomerList();
                btnApprove.Visible = true;
            }
            else
            {
                BindCustomerList();
                btnApprove.Visible = false;
            }
        }
    }
}