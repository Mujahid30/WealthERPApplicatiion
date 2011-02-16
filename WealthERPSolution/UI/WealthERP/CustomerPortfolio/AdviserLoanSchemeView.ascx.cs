using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCustomerPortfolio;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;

namespace WealthERP.Loans
{
    public partial class AdviserLoanSchemeView : System.Web.UI.UserControl
    {
        UserVo userVo = null;
        AdvisorVo advisorVo = null;


        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {

            GetPageCount();
            BindLoanSchemes();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["UserVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                BindLoanSchemes();



            }

        }

        private void BindLoanSchemes()
        {
            if (hdnCurrentPage.Value.ToString() != "")
            {
                mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                hdnCurrentPage.Value = "";
            }

            int count;
            gvAdviserLoanSchemeView.DataSource = LiabilitiesBo.GetLoanSchemes(mypager.CurrentPage, out count);
            gvAdviserLoanSchemeView.DataBind();
            hdnRecordCount.Value = count.ToString();

            if (count > 0)
            {
                DivPager.Style.Add("display", "visible");
                lblTotalRows.Text = hdnRecordCount.Value;
            }

            this.GetPageCount();
        }
        private void GetPageCount()
        {
            string upperlimit = string.Empty;
            string lowerlimit = string.Empty;
            int rowCount = 0;
            try
            {
                if (hdnRecordCount.Value != "")
                    rowCount = Convert.ToInt32(hdnRecordCount.Value);
                if (rowCount > 0)
                {

                    int ratio = rowCount / 10;
                    mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
                    upperlimit = (mypager.CurrentPage * 10).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnRecordCount.Value;
                    string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
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
                FunctionInfo.Add("Method", "RejectedMFFolio.ascx.cs:GetPageCount()");
                object[] objects = new object[3];
                objects[0] = upperlimit;
                objects[1] = rowCount;
                objects[2] = lowerlimit;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        {
            int schemeId;

            try
            {
                DropDownList ddlAction = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
                int selectedRow = gvr.RowIndex;
                schemeId = int.Parse(gvAdviserLoanSchemeView.DataKeys[selectedRow].Values["SchemeId"].ToString());
                if (ddlAction.SelectedValue == "Edit")
                {
                    if (Session["LoanSchemeView"]!=null && Session["LoanSchemeView"].ToString() == "SuperAdmin")
                    {
                        string url = "?schemeId=" + schemeId + "&mode=Edit";
                        Session["LoanSchemeId"] = schemeId;
                        Session["LoanSchemeViewStatus"] = "Edit";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanScheme','" + url + "');", true);
                    }
                    else
                    {
                        EditDisabledMessage.Visible = true;
                        lblEditMessageDisabled.Text = "Sorry, you dont have Sufficient Permission to Edit Scheme";
                    }
                }
                else if (ddlAction.SelectedValue == "View")
                {
                    string url = "?schemeId=" + schemeId + "&mode=View";
                    Session["LoanSchemeId"] = schemeId;
                    Session["LoanSchemeViewStatus"] = "View";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanScheme','" + url + "');", true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            //string url = "?schemeId=" + schemeId + "&mode=View";
            string url = "?mode=Add";
            Session["LoanSchemeViewStatus"] = "Add";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanScheme','" + url + "');", true);
        }
    }
}