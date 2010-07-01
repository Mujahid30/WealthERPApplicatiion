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
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;

namespace WealthERP.Customer
{
    public partial class ViewCustomerProofs : System.Web.UI.UserControl
    {

        DataSet dsCustomerProof = new DataSet();
        CustomerVo customerVo = null;
        CustomerBo customerBo = new CustomerBo();
        int custBankAccId;
        int customerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session["CustomerVo"];
            if (!IsPostBack)
            {
                Session.Remove("FlagProof");
                BindProofGrid();
            }
        }
        protected override void OnInit(EventArgs e)
        {
            try
            {

                ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
                mypager.EnableViewState = true;
                base.OnInit(e);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:OnInit()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            try
            {
                GetPageCount();
                this.BindProofGrid();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void GetPageCount()
        {
            string upperlimit = "";
            int rowCount = 0;
            int ratio = 0;
            string lowerlimit = "";
            string PageRecords = "";
            try
            {
                if (hdnCount.Value != "")
                    rowCount = Convert.ToInt32(hdnCount.Value);
                if (rowCount > 0)
                {
                    ratio = rowCount / 10;
                    mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    lowerlimit = (((mypager.CurrentPage - 1) * 10)+1).ToString();
                    upperlimit = (mypager.CurrentPage * 10).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnCount.Value;
                    PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;
                    hdnCurrentPage.Value = mypager.CurrentPage.ToString();
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
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:GetPageCount()");
                object[] objects = new object[5];
                objects[0] = upperlimit;
                objects[1] = rowCount;
                objects[2] = ratio;
                objects[3] = lowerlimit;
                objects[4] = PageRecords;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindProofGrid()
        {
            int Count = 0;
            try
            {
               
                customerId = int.Parse(customerVo.CustomerId.ToString());

                dsCustomerProof = customerBo.GetCustomerProofs(customerId,mypager.CurrentPage,out Count);
                lblTotalRows.Text = hdnCount.Value = Count.ToString();
                if (dsCustomerProof.Tables[0].Rows.Count >0)
                {
                    lblMsg.Visible = false;
                    trPager.Visible = true;
                    lblCurrentPage.Visible = true;
                    lblTotalRows.Visible = true;
                    DataTable dtCustomerProofs = new DataTable();
                    dtCustomerProofs.Columns.Add("ProofCode");
                    dtCustomerProofs.Columns.Add("Proof Name");
                    dtCustomerProofs.Columns.Add("Proof Category");

                    DataRow drCustomerProof;

                    for (int i = 0; i < dsCustomerProof.Tables[0].Rows.Count; i++)
                    {
                        drCustomerProof = dtCustomerProofs.NewRow();
                        drCustomerProof[0] = dsCustomerProof.Tables[0].Rows[i]["ProofCode"].ToString();
                        drCustomerProof[1] = dsCustomerProof.Tables[0].Rows[i]["ProofName"].ToString();
                        drCustomerProof[2] = dsCustomerProof.Tables[0].Rows[i]["ProofCategory"].ToString();

                        dtCustomerProofs.Rows.Add(drCustomerProof);


                    }
                    gvCustomerProofs.DataSource = dtCustomerProofs;
                    gvCustomerProofs.DataBind();
                    gvCustomerProofs.Visible = true;
                    this.GetPageCount();
                }
                else
                {
                    gvCustomerProofs.DataSource = null;
                    gvCustomerProofs.DataBind();
                    lblMsg.Visible = true;
                    trPager.Visible = false;
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
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
                FunctionInfo.Add("Method", "ViewCustomerProofs.ascx:BindProofGrid()");
                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = customerId;
                objects[2] = dsCustomerProof;
                objects[3] = custBankAccId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                foreach (GridViewRow dr in gvCustomerProofs.Rows)
                {

                    if (((CheckBox)dr.FindControl("chkBx")).Checked == true)
                    {
                        i = i + 1;
                    }
                }
                if (i == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select Proof Id..!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
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
                FunctionInfo.Add("Method", "ViewBranchDetails.ascx.cs:btnDeleteSelected_Click()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        protected void hiddenDelete_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                DeleteProof();
            }
            else
            {
                ClearCheckBox();
            }
        }

        private void DeleteProof()
        {
            
            bool result = false;
            int proofCode = 0;
            try
            {
                foreach (GridViewRow dr in gvCustomerProofs.Rows)
                {
                    CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
                    if (checkBox.Checked)
                    {

                        proofCode = Convert.ToInt32(gvCustomerProofs.DataKeys[dr.RowIndex].Values["ProofCode"].ToString());
                        result = customerBo.DeleteCustomerProof(customerVo.CustomerId, proofCode);
                        if (result)
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Deleted Successfully..');", true);

                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Sorry..');", true);
                        }
                    }
                }
                BindProofGrid();
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewBranchDetails.ascx.cs:DeleteTerminalId()");
                object[] objects = new object[2];
                objects[0] = proofCode;
                objects[1] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ClearCheckBox()
        {
            foreach (GridViewRow dr in gvCustomerProofs.Rows)
            {
                CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
                checkBox.Checked = false;
            }
        }
    }
}