using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using BoUser;
using VoUser;
using WealthERP.Base;
using System.Drawing.Printing;
using BoCommon;
using BoOnlineOrderManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class MFAccountValuation : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        int UserId;
        int advisorId = 0;
        AdvisorVo advisorVo = new AdvisorVo();
        OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            SessionBo.CheckSession();
            userBo = new UserBo();
            userVo = (UserVo)Session[SessionContents.UserVo];
            UserId = userVo.UserId;
            advisorId = advisorVo.advisorId;
            if (!IsPostBack)
            {
                trPnlValuation.Visible = false;
                trReProcess.Visible = false;
            }
        }
        private void ValuationGridBind( int IsValued)
        {
            
            DataSet dsGetValuation = new DataSet();
            try
            {
                dsGetValuation = OnlineOrderBackOfficeBo.GetAdviserCustomersAllMFAccounts(IsValued, advisorId);
                //if (dsGetValuation.Tables[0].Rows.Count > 0)
                //{
                              
                    gvMFAccounts.DataSource = dsGetValuation;
                    gvMFAccounts.DataBind();
                    if (Cache["MFAccounts" + userVo.UserId] == null)
                    {
                        Cache.Insert("MFAccounts" + userVo.UserId, dsGetValuation);
                    }
                    else
                    {
                        Cache.Remove("MFAccounts" + userVo.UserId);
                        Cache.Insert("MFAccounts" + userVo.UserId, dsGetValuation);
                    }
                   
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MFAccountValuation.ascx.cs:ValuationGridBind()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void gvMFAccounts_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dsGetValuation = new DataSet();
            dsGetValuation = (DataSet)Cache["MFAccounts" + userVo.UserId];
            this.gvMFAccounts.DataSource = dsGetValuation;
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (ddlSelect.SelectedValue == "S")
            {
                return;
            }
            else if (ddlSelect.SelectedValue == "0" || ddlSelect.SelectedValue == "1")
            {
                ValuationGridBind(Convert.ToInt32(ddlSelect.SelectedValue));
                trPnlValuation.Visible = true;
                trReProcess.Visible = false;
                gvMFAccounts.MasterTableView.GetColumn("chkBoxColumn").Display = false;
            }

            else
            {
                ValuationGridBind(Convert.ToInt32(ddlSelect.SelectedValue));
                trPnlValuation.Visible = true;
                trReProcess.Visible = true;
                gvMFAccounts.MasterTableView.GetColumn("chkBoxColumn").Display = true;
                btnUpdate.Visible = true;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int i=0;
                foreach (GridDataItem dataItem in gvMFAccounts.MasterTableView.Items)
                {
                    if ((dataItem.FindControl("chkItem") as CheckBox).Checked)
                    {
                        i = i + 1;
                    }
                }
                if (i == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select item for ReProcess!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "ConfirmValuation();", true);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "ConfirmValuation();", true);
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
                FunctionInfo.Add("Method", "MFAccountValuation.ascx.cs:btnUpdate_Click()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private string GetSelectedMFAIdString()
        {
            string gvMFAId = "";
            foreach (GridDataItem dataItem in gvMFAccounts.MasterTableView.Items)
            {
                if ((dataItem.FindControl("chkItem") as CheckBox).Checked)
                {
                    gvMFAId += dataItem.GetDataKeyValue("MFAIV_Id").ToString() + ",";
                }
            }
            return gvMFAId;
        }
        protected void hiddenUpdate_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                UpdateValuation();
            }
            else
            {
                foreach (GridDataItem dataItem in gvMFAccounts.MasterTableView.Items)
                {
                    CheckBox checkBox = (CheckBox)dataItem.FindControl("chkItem");
                    checkBox.Checked = false;
                }
            }
        }

        private void UpdateValuation()
        {
            int Processed = 2;
            string gvMFAId = GetSelectedMFAIdString();
            OnlineOrderBackOfficeBo.UpdateAdviserCustomersAllMFAccounts(gvMFAId, UserId);
            ValuationGridBind(Processed);
        }
    }
}