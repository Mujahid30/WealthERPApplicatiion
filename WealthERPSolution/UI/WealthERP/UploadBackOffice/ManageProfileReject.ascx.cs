 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using Telerik.Web.UI;
using System.Data;
using System.Collections.Specialized;
using System.Collections;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;
using BoUploads;
namespace WealthERP.UploadBackOffice
{
    public partial class ManageProfileReject : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        AdvisorVo advisorVo;
        UploadCommonBo uploadCommonBo = new UploadCommonBo();
        int reqId;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            userVo = (UserVo)Session[SessionContents.UserVo];
            if (!IsPostBack)
            {
                msgReprocessComplete.Visible = false;
                reqId = Convert.ToInt32(Request.QueryString["ReqId"]);
                if (reqId != null)
                {
                    GetProfileIncreamentRejection(reqId);
                }
            }
        }

        private void GetProfileIncreamentRejection(int reqId)
        {
            try
            {

                DataTable dtReqReje = new DataTable();
                DataSet dsRej = new DataSet();
                dsRej = uploadCommonBo.RequestWiseRejects(reqId);
                dtReqReje = dsRej.Tables[0];
                gvProfileIncreamenetReject.DataSource = dtReqReje;
                gvProfileIncreamenetReject.DataBind();
                gvProfileIncreamenetReject.Visible = true;


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string city = string.Empty;
            string state = string.Empty;
            string pincode = string.Empty;
            string mobileno = string.Empty;
            string occupation = string.Empty;
            string accounttype = string.Empty;
            string bankname = string.Empty;
            string personalstatus = string.Empty;
            bool blResult = false;
            int Id = 0;
            int tableNo=0;
            string clientCode = string.Empty;
            uploadCommonBo = new UploadCommonBo();
            GridFooterItem footerRow = (GridFooterItem)gvProfileIncreamenetReject.MasterTableView.GetItems(GridItemType.Footer)[0];
            foreach (GridDataItem dr in gvProfileIncreamenetReject.Items)
            {
                if (((TextBox)footerRow.FindControl("txtClientCodeFooter")).Text.Trim() == "")
                {
                    clientCode = ((TextBox)dr.FindControl("txtClientCode")).Text;
                }
                else
                {
                    clientCode = ((TextBox)footerRow.FindControl("txtClientCodeFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtCityFooter")).Text.Trim() == "")
                {
                    city = ((TextBox)dr.FindControl("txtCity")).Text;
                }
                else
                {
                    city = ((TextBox)footerRow.FindControl("txtCityFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtStateFooter")).Text.Trim() == "")
                {
                    state = ((TextBox)dr.FindControl("txtState")).Text;
                }
                else
                {
                    state = ((TextBox)footerRow.FindControl("txtStateFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtPinCodeFooter")).Text.Trim() == "")
                {
                    pincode = ((TextBox)dr.FindControl("txtPinCode")).Text;
                }
                else
                {
                    pincode = ((TextBox)footerRow.FindControl("txtPinCodeFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtMobileNoFooter")).Text.Trim() == "")
                {
                    mobileno =((TextBox)dr.FindControl("txtMobileNo")).Text;
                }
                else
                {
                    mobileno =((TextBox)footerRow.FindControl("txtMobileNoFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtOccupationFooter")).Text.Trim() == "")
                {
                    occupation = ((TextBox)dr.FindControl("txtOccupation")).Text;
                }
                else
                {
                    occupation = ((TextBox)footerRow.FindControl("txtOccupationFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtAccountTypeFooter")).Text.Trim() == "")
                {
                    accounttype = ((TextBox)dr.FindControl("txtAccountType")).Text;
                }
                else
                {
                    accounttype = ((TextBox)footerRow.FindControl("txtAccountTypeFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtBankNameFooter")).Text.Trim() == "")
                {
                    bankname = ((TextBox)dr.FindControl("txtBankName")).Text;
                }
                else
                {
                    bankname = ((TextBox)footerRow.FindControl("txtBankNameFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtPersonalStatusFooter")).Text.Trim() == "")
                {
                    personalstatus = ((TextBox)dr.FindControl("txtPersonalStatus")).Text;
                }
                else
                {
                    personalstatus = ((TextBox)footerRow.FindControl("txtPersonalStatusFooter")).Text;
                }
                CheckBox checkBox = (CheckBox)dr.FindControl("chkId");
                if (checkBox.Checked==true)
                {
                    int selectedRow = 0;
                    GridDataItem gdi;
                    gdi = (GridDataItem)checkBox.NamingContainer;
                    selectedRow = gdi.ItemIndex + 1;
                    Id = int.Parse((gvProfileIncreamenetReject.MasterTableView.DataKeyValues[selectedRow - 1]["ID"].ToString()));
                     tableNo = int.Parse((gvProfileIncreamenetReject.MasterTableView.DataKeyValues[selectedRow - 1]["TableNo"].ToString()));
                     blResult = uploadCommonBo.UpdateRequestRejects(clientCode,Id, tableNo, city, state, pincode, mobileno, occupation, accounttype, bankname, personalstatus);
                    
                }
                
            }

            if (Request.QueryString["ReqId"] != null)
                reqId = Int32.Parse(Request.QueryString["ReqId"].ToString());
            GetProfileIncreamentRejection(reqId);
        }
        protected void gvProfileIncreamenetReject_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtRequests = new DataTable();
            DataSet dtProcessLogDetails = new DataSet();
            dtRequests = (DataTable)Cache[userVo.UserId.ToString() + "ReqId"];
            if (dtRequests == null)
            {
                gvProfileIncreamenetReject.DataSource = dtRequests;
            }
            
        }
        protected void btnReProcess_Click(object sender, EventArgs e)
        {
            int reprocess;
            if (Request.QueryString["ReqId"] != null)
            {
                reqId = Int32.Parse(Request.QueryString["ReqId"].ToString());
                reprocess=uploadCommonBo.SetRequestParentreqId(reqId, userVo.UserId);
                if (reprocess > 0)
                {
                    msgReprocessincomplete.Visible = true;
                    msgReprocessincomplete.InnerText = "Request already exists";
                }
                else 
                {
                    msgReprocessComplete.Visible = true;
                    msgReprocessComplete.InnerText = "ReProcess SuccessFully Done";
                }
            }

        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (GridDataItem gvr in this.gvProfileIncreamenetReject.Items)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                {
                    i = i + 1;
                }
            }

            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to delete!');", true);
            }
            else
            {
                RejectedRequestDelete();
                //NeedSource();
                gvProfileIncreamenetReject.MasterTableView.Rebind();
                msgReprocessComplete.Visible = false;
                msgReprocessincomplete.Visible = false;
                //    rejectedRecordsBo = new RejectedRecordsBo();
                //    rejectedRecordsBo.DeleteMFTransactionStaging(StagingID);
                //    if (hdnProcessIdFilter.Value != "")
                //    {
                //        ProcessId = int.Parse(hdnProcessIdFilter.Value);
                //    }
                //    BindEquityTransactionGrid(ProcessId);
            }
        }
        private void RejectedRequestDelete()
        {
            int Id = 0;
            int tableNo = 0;
            foreach (GridDataItem gvr in this.gvProfileIncreamenetReject.Items)
            {
                CheckBox checkBox = (CheckBox)gvr.FindControl("chkId");
                if (checkBox.Checked)
                {
                    int selectedRow = 0;
                    GridDataItem gdi;
                    gdi = (GridDataItem)checkBox.NamingContainer;
                    selectedRow = gdi.ItemIndex + 1;
                    Id = int.Parse((gvProfileIncreamenetReject.MasterTableView.DataKeyValues[selectedRow - 1]["ID"].ToString()));
                    tableNo = int.Parse((gvProfileIncreamenetReject.MasterTableView.DataKeyValues[selectedRow - 1]["TableNo"].ToString()));
                    uploadCommonBo.DeleteRequestRejected(Id, tableNo);
                }

            }
            if (Request.QueryString["ReqId"] != null)
                reqId = Int32.Parse(Request.QueryString["ReqId"].ToString());
            GetProfileIncreamentRejection(reqId);
        }
        protected void btnDataTranslationMapping_Click(object sender, EventArgs e)
        {
            Response.Redirect("ControlHost.aspx?pageid=ManageLookups", false);

        }
    }
    }
