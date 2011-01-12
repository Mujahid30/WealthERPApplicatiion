using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUser;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using System.Data;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System.Xml;
using System.Text;
using iTextSharp.text.html.simpleparser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCommon;



namespace WealthERP.Advisor
{
    public partial class CustomerFolioMerge : System.Web.UI.UserControl
    {
        AdvisorVo adviserVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = new UserVo();

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
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:OnInit()");
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

                this.BindCustomer(mypager.CurrentPage);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[2];
                objects[0] = mypager.CurrentPage;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckBox rdbGVRow = new CheckBox();
            rdbGVRow = GetGvRadioButton();
            rdbGVRow.Attributes.Add("onClick", "javascript:CheckOtherIsCheckedByGVID(value);");
            adviserVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                this.BindCustomer(1);

            }

        }
        private CheckBox GetGvRadioButton()
        {
            CheckBox rbRow = new CheckBox();
            if (gvCustomerFolioMerge.TemplateControl != null)
            {
                if ((CheckBox)gvCustomerFolioMerge.TemplateControl.FindControl("rdbGVRow") != null)
                {
                    rbRow = (CheckBox)gvCustomerFolioMerge.TemplateControl.FindControl("rdbGVRow");
                }
            }
            else
                rbRow = null;

            return rbRow;
        }

        private LinkButton GetGvLinkButton()
        {
            LinkButton lnkFolioNo = new LinkButton();
            if (gvCustomerFolioMerge.TemplateControl != null)
            {
                if ((LinkButton)gvCustomerFolioMerge.TemplateControl.FindControl("hypFolioNo") != null)
                {
                    lnkFolioNo = (LinkButton)gvCustomerFolioMerge.TemplateControl.FindControl("hypFolioNo");
                }
            }
            else
                lnkFolioNo = null;

            return lnkFolioNo;
        }
        protected void BindGrid(int CurrentPage)
        {

            if (hdnCurrentPage.Value.ToString() != "")
            {
                mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                hdnCurrentPage.Value = "";
            }

            int Count = 0;


            lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();


        }
        private void BindCustomer(int CurrentPage)
        {

            int count;
            DataSet dsCustomerFolio;
            AdvisorBranchBo adviserBranchBo = new AdvisorBranchBo();
            DataTable dtCustomerFolio = new DataTable();
            try
            {

                if (hdnCurrentPage.Value.ToString() != "")
                {
                    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                    hdnCurrentPage.Value = "";
                }


                dsCustomerFolio = adviserBranchBo.GetAdviserCustomerFolioMerge(adviserVo.advisorId, mypager.CurrentPage, hdnNameFilter.Value.ToString(), out count);
                lblTotalRows.Text = hdnRecordCount.Value = count.ToString();

                dtCustomerFolio.Columns.Add("CustomerId");
                dtCustomerFolio.Columns.Add("CustomerName");
                dtCustomerFolio.Columns.Add("AMCName");
                dtCustomerFolio.Columns.Add("AMCCode");
                dtCustomerFolio.Columns.Add("Count");
                dtCustomerFolio.Columns.Add("portfilionumber");
                dtCustomerFolio.Columns.Add("mergerstatus");

                if (dsCustomerFolio.Tables[0].Rows.Count == 0)
                {
                    //hdnRecordCount.Value = "0";

                    ErrorMessage.Visible = true;
                    trPager.Visible = false;
                    lblTotalRows.Visible = false;
                    lblCurrentPage.Visible = false;

                }
                else
                {

                    //trPager.Visible = true;
                    //lblTotalRows.Visible = true;
                    //lblCurrentPage.Visible = true;

                    DataRow drCustomerFolio;
                    DataTable dtCustomer = dsCustomerFolio.Tables[0];
                    ErrorMessage.Visible = false;
                    trPager.Visible = true;
                    lblTotalRows.Visible = true;
                    lblCurrentPage.Visible = true;
                    for (int i = 0; i < dtCustomer.Rows.Count; i++)
                    {
                        drCustomerFolio = dtCustomerFolio.NewRow();

                        drCustomerFolio["CustomerId"] = dtCustomer.Rows[i]["customerid"];
                        drCustomerFolio["CustomerName"] = dtCustomer.Rows[i]["name"];
                        drCustomerFolio["AMCName"] = dtCustomer.Rows[i]["amcname"];
                        drCustomerFolio["AMCCode"] = dtCustomer.Rows[i]["amccode"];
                        drCustomerFolio["Count"] = dtCustomer.Rows[i]["number"];
                        drCustomerFolio["portfilionumber"] = dtCustomer.Rows[i]["portfilionumber"];
                        drCustomerFolio["mergerstatus"] = dtCustomer.Rows[i]["mergerstatus"];
                        dtCustomerFolio.Rows.Add(drCustomerFolio);

                    }

                    gvCustomerFolioMerge.DataSource = dtCustomerFolio;
                    gvCustomerFolioMerge.DataBind();

                    //Customer search
                    //TextBox txtBranch = GetBranchTextBox();
                    //if (txtBranch != null)
                    //{
                    //    if (hdnBranchFilter.Value != "")
                    //    {
                    //        txtBranch.Text = hdnBranchFilter.Value.ToString();
                    //    }
                    //}    

                    this.GetPageCount();
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
                FunctionInfo.Add("Method", "CustomerFolioMerge.ascx.cs:BindCustomer()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void GetPageCount()
        {
            string upperlimit = null;
            int rowCount = 0;
            int ratio = 0;
            string lowerlimit = null;
            string PageRecords = null;
            try
            {
                if (hdnRecordCount.Value.ToString() != "")
                    rowCount = Convert.ToInt32(hdnRecordCount.Value);
                if (rowCount > 0)
                {
                    ratio = rowCount / 20;
                    mypager.PageCount = rowCount % 20 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    if (((mypager.CurrentPage - 1) * 20) != 0)
                        lowerlimit = (((mypager.CurrentPage - 1) * 20) + 1).ToString();
                    else
                        lowerlimit = "1";
                    upperlimit = (mypager.CurrentPage * 20).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnRecordCount.Value;
                    PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
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

                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:GetPageCount()");

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

        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetCustNameTextBox();

            if (txtName != null)
            {
                hdnNameFilter.Value = txtName.Text.Trim();
                this.BindCustomer(mypager.CurrentPage);
            }

        }

        private TextBox GetCustNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvCustomerFolioMerge.HeaderRow != null)
            {
                if ((TextBox)gvCustomerFolioMerge.HeaderRow.FindControl("txtCustNameSearch") != null)
                {
                    txt = (TextBox)gvCustomerFolioMerge.HeaderRow.FindControl("txtCustNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        protected void lnkCustomerName_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
            int rowIndex = gvRow.RowIndex;
            DataKey dk = gvCustomerFolioMerge.DataKeys[rowIndex];
            int customerId = Convert.ToInt32(dk.Value);
            gvCustomerFolioMerge.Visible = false;
            trPager.Visible = false;
            lblTotalRows.Visible = false;
            lblCurrentPage.Visible = false;

        }



        protected void rdbGVRow_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox rdbGVRow = new CheckBox();
            rdbGVRow = GetGvRadioButton();
            ddlAdvisorBranchList.Items.Clear();


            foreach (GridViewRow dr in gvCustomerFolioMerge.Rows)
            {
                int rowIndex = dr.RowIndex;
                DataKey dKey = gvCustomerFolioMerge.DataKeys[rowIndex];

                int customerId = int.Parse(dKey.Values["CustomerId"].ToString());
                int amcCode = int.Parse(dKey.Values["AMCCode"].ToString());
                string fnumber = dKey.Values["Count"].ToString();

                if (((CheckBox)dr.FindControl("rdbGVRow")).Checked == true)
                {
                    bindFolioDropDown(customerId, amcCode, fnumber);

                }



            }

        }


        protected void bindFolioDropDown(int customerId, int amcCode, string fnumber)
        {
            lblerror.Visible = false;
            DataSet folioDs;
            AdvisorBranchBo adviserBranchBo = new AdvisorBranchBo();
            folioDs = new DataSet();
            folioDs = adviserBranchBo.GetCustomerFolioMergeList(customerId, amcCode, fnumber);
            if (folioDs.Tables[0].Rows.Count == 0)
            {

                lblerror.Visible = true;

            }
            ddlAdvisorBranchList.DataSource = folioDs;
            ddlAdvisorBranchList.DataValueField = folioDs.Tables[0].Columns["CMFA_FolioNum"].ToString();
            ddlAdvisorBranchList.DataBind();

        }

        protected void gvCustomerFolioMerge_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvCustomerFolioMerge.SelectedRow;




        }


        private void GetLatestValuationDate()
        {
            DateTime EQValuationDate = new DateTime();
            DateTime MFValuationDate = new DateTime();
            PortfolioBo portfolioBo = null;
            Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
            AdvisorVo advisorVo = new AdvisorVo();
            int adviserId = 0;
            advisorVo = (AdvisorVo)Session["advisorVo"];
            adviserId = advisorVo.advisorId;

            try
            {
                portfolioBo = new PortfolioBo();
                if (userVo.UserType == "Advisor")
                {

                    advisorVo = (AdvisorVo)Session["advisorVo"];
                    adviserId = advisorVo.advisorId;
                }
                else if (userVo.UserType == "RM")
                {
                    adviserId = int.Parse(Session["adviserId"].ToString());

                }

                if (portfolioBo.GetLatestValuationDate(adviserId, "EQ") != null)
                {
                    EQValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, "EQ").ToString());
                }
                if (portfolioBo.GetLatestValuationDate(adviserId, "MF") != null)
                {
                    MFValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, "MF").ToString());
                }
                genDict.Add("EQDate", EQValuationDate);
                genDict.Add("MFDate", MFValuationDate);
                Session["ValuationDate"] = genDict;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioDashboard.ascx.cs:GetLatestValuationDate()");
                object[] objects = new object[3];
                objects[0] = EQValuationDate;
                objects[1] = adviserId;
                objects[2] = MFValuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void hypFolioNo_Click(object sender, EventArgs e)
        {


            GridViewRow gvFoliomerge = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
            int rowIndex = gvFoliomerge.RowIndex;

            string folioNumber = gvCustomerFolioMerge.DataKeys[rowIndex].Values["Count"].ToString();
            //int accountId = int.Parse(gvCustomerFolioMerge.DataKeys[row.RowIndex].Values[""].ToString());
            int customerId = int.Parse(gvCustomerFolioMerge.DataKeys[rowIndex].Values["CustomerId"].ToString());
            int portfolioId = int.Parse(gvCustomerFolioMerge.DataKeys[rowIndex].Values["portfilionumber"].ToString());

            customerVo = customerBo.GetCustomer(customerId);

            Session["customerVo"] = customerVo;
            Session[SessionContents.PortfolioId] = portfolioId;
            Session["folioNum"] = folioNumber;

            GetLatestValuationDate();

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewMutualFundPortfolio','none');", true);

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            CheckBox rdbGVRow = new CheckBox();
            rdbGVRow = GetGvRadioButton();
            foreach (GridViewRow dr in gvCustomerFolioMerge.Rows)
            {
                int rowIndex = dr.RowIndex;
                DataKey dKey = gvCustomerFolioMerge.DataKeys[rowIndex];

                int customerId = int.Parse(dKey.Values["CustomerId"].ToString());
                int amcCode = int.Parse(dKey.Values["AMCCode"].ToString());
                string fnumber = dKey.Values["Count"].ToString();

                if (((CheckBox)dr.FindControl("rdbGVRow")).Checked == true)
                {
                    AdvisorBranchBo adviserBranchBo = new AdvisorBranchBo();
                    string ffromfolio = ddlAdvisorBranchList.SelectedValue.ToString();
                    bool folioDs = adviserBranchBo.CustomerFolioMerged(ffromfolio, fnumber, customerId);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdvisorCustomerAccounts','none');", true);
                }

            }


        }
    }

}