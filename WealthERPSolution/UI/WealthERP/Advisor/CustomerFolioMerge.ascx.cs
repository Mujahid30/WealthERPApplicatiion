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
using VoCustomerProfiling;
using Telerik.Web.UI;



namespace WealthERP.Advisor
{
    public partial class CustomerFolioMerge : System.Web.UI.UserControl
    {
        AdvisorVo adviserVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = new UserVo();
        DataSet dsCustomerPortfolioList = new DataSet();
        int customerPortfolioID = 0;
        int isBankAssociatedWithOtherTransactions=0;

        //protected override void OnInit(EventArgs e)
        //{
        //    try
        //    {
        //        ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
        //        mypager.EnableViewState = true;
        //        base.OnInit(e);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:OnInit()");
        //        object[] objects = new object[0];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        //GetPageCount();
        //        this.BindCustomer(mypager.CurrentPage);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:HandlePagerEvent()");
        //        object[] objects = new object[2];
        //        objects[0] = mypager.CurrentPage;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
           
            //CheckBox rdbGVRow = new CheckBox();
            //rdbGVRow = GetGvRadioButton();
            //rdbGVRow.Attributes.Add("onClick", "javascript:CheckOtherIsCheckedByGVID(value);");
            adviserVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                this.BindCustomer(1);
                
            }
            lblerror.Visible = false;
            trFolioStatus.Visible = false;
            trMergeFolioStatus.Visible = false;
            txtPickCustomer_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
            txtPickCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";

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
            //if (hdnCurrentPage.Value.ToString() != "")
            //{
            //    //mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
            //    hdnCurrentPage.Value = "";
            //}

            //int Count = 0;
            //lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
        }

        private void BindCustomer(int CurrentPage)
        {
            int count=0;
            DataSet dsCustomerFolio=new DataSet();
            AdvisorBranchBo adviserBranchBo = new AdvisorBranchBo();
            DataTable dtCustomerFolio = new DataTable();
            try
            {
                //if (hdnCurrentPage.Value.ToString() != "")
                //{
                //    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                //    hdnCurrentPage.Value = "";
                //}
                
                    dsCustomerFolio = adviserBranchBo.GetAdviserCustomerFolioMerge(adviserVo.advisorId);

                //if (hdnFolioFilter.Value != "")
                //    dsCustomerFolio = adviserBranchBo.FilterFolioNumber(adviserVo.advisorId, mypager.CurrentPage, hdnFolioFilter.Value.ToString(), out count);
                
                //lblTotalRows.Text = hdnRecordCount.Value = count.ToString();

                dtCustomerFolio.Columns.Add("CustomerId");
                dtCustomerFolio.Columns.Add("GroupHead");
                dtCustomerFolio.Columns.Add("processId");
                dtCustomerFolio.Columns.Add("CustomerName");
                dtCustomerFolio.Columns.Add("AMCName");
                dtCustomerFolio.Columns.Add("AMCCode");
                dtCustomerFolio.Columns.Add("Count");
                dtCustomerFolio.Columns.Add("FolioName");
                dtCustomerFolio.Columns.Add("portfilionumber");
                dtCustomerFolio.Columns.Add("mergerstatus");

                if (dsCustomerFolio.Tables[0].Rows.Count == 0)
                {
                    //hdnRecordCount.Value = "0";
                    ErrorMessage.Visible = true;
                    //trPager.Visible = false;
                    //lblTotalRows.Visible = false;
                    //lblCurrentPage.Visible = false;
                    btnExportFilteredData.Visible = false;
                }
                else
                {
                    btnExportFilteredData.Visible = true;
                    //trPager.Visible = true;
                    //lblTotalRows.Visible = true;
                    //lblCurrentPage.Visible = true;

                    DataRow drCustomerFolio;
                    DataTable dtCustomer = dsCustomerFolio.Tables[0];
                    ErrorMessage.Visible = false;
                    //trPager.Visible = true;
                    //lblTotalRows.Visible = true;
                    //lblCurrentPage.Visible = true;
                    for (int i = 0; i < dtCustomer.Rows.Count; i++)
                    {
                        drCustomerFolio = dtCustomerFolio.NewRow();

                        drCustomerFolio["CustomerId"] = dtCustomer.Rows[i]["customerid"];
                        drCustomerFolio["processId"]=dtCustomer.Rows[i]["processid"];
                        drCustomerFolio["GroupHead"] = dtCustomer.Rows[i]["Parent"];
                        drCustomerFolio["CustomerName"] = dtCustomer.Rows[i]["name"];
                        drCustomerFolio["AMCName"] = dtCustomer.Rows[i]["amcname"];
                        drCustomerFolio["AMCCode"] = dtCustomer.Rows[i]["amccode"];
                        drCustomerFolio["Count"] = dtCustomer.Rows[i]["number"];
                        if (!string.IsNullOrEmpty(dtCustomer.Rows[i]["FolioName"].ToString().Trim()))
                            drCustomerFolio["FolioName"] = dtCustomer.Rows[i]["FolioName"];
                        else
                            drCustomerFolio["FolioName"] = "";
                        drCustomerFolio["portfilionumber"] = dtCustomer.Rows[i]["portfilionumber"];
                        drCustomerFolio["mergerstatus"] = dtCustomer.Rows[i]["mergerstatus"];
                        dtCustomerFolio.Rows.Add(drCustomerFolio);
                    }
                    gvCustomerFolioMerge.DataSource = dtCustomerFolio;
                    gvCustomerFolioMerge.DataBind();

                    if (Cache["gvCustomerFolioMerge" + adviserVo.advisorId] == null)
                    {
                        Cache.Insert("gvCustomerFolioMerge" + adviserVo.advisorId, dtCustomerFolio);
                    }
                    else
                    {
                        Cache.Remove("gvCustomerFolioMerge" + adviserVo.advisorId);
                        Cache.Insert("gvCustomerFolioMerge" + adviserVo.advisorId, dtCustomerFolio);
                    }

                    //Customer search
                    //TextBox txtBranch = GetBranchTextBox();
                    //if (txtBranch != null)
                    //{
                    //    if (hdnBranchFilter.Value != "")
                    //    {
                    //        txtBranch.Text = hdnBranchFilter.Value.ToString();
                    //    }
                    //}    
                    //this.GetPageCount();
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

        //private void GetPageCount()
        //{
        //    string upperlimit = null;
        //    int rowCount = 0;
        //    int ratio = 0;
        //    string lowerlimit = null;
        //    string PageRecords = null;
        //    try
        //    {
        //        if (hdnRecordCount.Value.ToString() != "")
        //            rowCount = Convert.ToInt32(hdnRecordCount.Value);
        //        if (rowCount > 0)
        //        {
        //            ratio = rowCount / 20;
        //            mypager.PageCount = rowCount % 20 == 0 ? ratio : ratio + 1;
        //            mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
        //            if (((mypager.CurrentPage - 1) * 20) != 0)
        //                lowerlimit = (((mypager.CurrentPage - 1) * 20) + 1).ToString();
        //            else
        //                lowerlimit = "1";
        //            upperlimit = (mypager.CurrentPage * 20).ToString();
        //            if (mypager.CurrentPage == mypager.PageCount)
        //                upperlimit = hdnRecordCount.Value;
        //            PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
        //            lblCurrentPage.Text = PageRecords;
        //            hdnCurrentPage.Value = mypager.CurrentPage.ToString();
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:GetPageCount()");

        //        object[] objects = new object[5];
        //        objects[0] = upperlimit;
        //        objects[1] = rowCount;
        //        objects[2] = ratio;
        //        objects[3] = lowerlimit;
        //        objects[4] = PageRecords;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //protected void btnCustomerSearch_Click(object sender, EventArgs e)
        //{
        //    TextBox txtName = GetCustNameTextBox();
        //    if (txtName != null)
        //    {
        //        hdnNameFilter.Value = txtName.Text.Trim();
        //        this.BindCustomer(mypager.CurrentPage);
        //    }
        //}

        //private TextBox GetCustNameTextBox()
        //{
        //    TextBox txt = new TextBox();
        //    if (gvCustomerFolioMerge.HeaderRow != null)
        //    {
        //        if ((TextBox)gvCustomerFolioMerge.HeaderRow.FindControl("txtCustNameSearch") != null)
        //        {
        //            txt = (TextBox)gvCustomerFolioMerge.HeaderRow.FindControl("txtCustNameSearch");
        //        }
        //    }
        //    else
        //        txt = null;
        //    return txt;
        //}

        //protected void btnFolioNumberSearch_Click(object sender, EventArgs e)
        //{
        //    TextBox txtFolioNo = GetFolioNumberTextBox();
        //    if (txtFolioNo != null)
        //    {
        //        hdnFolioFilter.Value = txtFolioNo.Text.Trim();
        //        this.BindCustomer(mypager.CurrentPage);
        //    }
        //}

        //private TextBox GetFolioNumberTextBox()
        //{
        //    TextBox txtFolio = new TextBox();
        //    if (gvCustomerFolioMerge.MasterTableView. != null)
        //    {
        //        if ((TextBox)gvCustomerFolioMerge.HeaderRow.FindControl("txtFolioSearch") != null)
        //        {
        //            txtFolio = (TextBox)gvCustomerFolioMerge.HeaderRow.FindControl("txtFolioSearch");
        //        }
        //    }
        //    else
        //        txtFolio = null;
        //    return txtFolio;
        //}

        protected void lnkCustomerName_Click(object sender, EventArgs e)
        {
            ////GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
            //GridDataItem gvRow = ((GridDataItem)(((LinkButton)sender).Parent.Parent));

            int rowIndex =0;
            LinkButton lnkIssueCode = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkIssueCode.NamingContainer;
            rowIndex = gdi.ItemIndex + 1;

            //DataKey dk = gvCustomerFolioMerge.DataKeys[rowIndex];
            //int customerId = Convert.ToInt32(dk.Value);
            int customerId = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex-1]["CustomerId"].ToString());
            gvCustomerFolioMerge.Visible = false;
            //trPager.Visible = false;
            //lblTotalRows.Visible = false;
            //lblCurrentPage.Visible = false;
        }



        protected void rdbGVRow_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox rdbGVRow = new CheckBox();
            rdbGVRow = GetGvRadioButton();
            ddlAdvisorBranchList.Items.Clear();
            if (ddlMovePortfolio.SelectedValue != "MtoAC")
            {
                txtPickCustomer.Text = string.Empty;            
                ddlPortfolio.Items.Clear();
            }
            ReadCustomerGridDetails();

            int customerId = 0;

            foreach (GridDataItem dr in gvCustomerFolioMerge.Items)
            {
                int rowIndex = 0;
                CheckBox chk = (CheckBox)dr.FindControl("rdbGVRow");
                GridDataItem gdi;
                gdi = (GridDataItem)chk.NamingContainer;
                rowIndex = gdi.ItemIndex + 1;

                //int rowIndex = dr.RowIndex;
                //DataKey dKey = gvCustomerFolioMerge.DataKeys[rowIndex];
                if (chk.Checked == true)
                {
                    //customerId = int.Parse(dKey.Values["CustomerId"].ToString());
                    customerId = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex-1]["CustomerId"].ToString());
                    if (ddlMovePortfolio.SelectedValue == "MtoAC")
                    {
                        if (string.IsNullOrEmpty(txtPickCustomer.Text.Trim()))
                        {
                            hdnCustomerId.Value = "0";
                        }
                        if (hdnCustomerId.Value.ToString() != "0")
                        {
                            bindDropdownPortfolio(int.Parse(hdnCustomerId.Value.ToString()));
                        }
            
                    }
                    else
                    bindDropdownPortfolio(customerId);
                    return;
                }
            }

        }
        protected void ReadCustomerGridDetails()
        {
            int gvrcustomerId = 0;
            string gvrfnumber = "";
            int gvramcCode = 0;
            foreach (GridDataItem dr in gvCustomerFolioMerge.Items)
            {
                //int rowIndex;
                //rowIndex = dr.RowIndex;
                //DataKey dKey = gvCustomerFolioMerge.DataKeys[rowIndex];

                //gvrcustomerId = int.Parse(dKey.Values["CustomerId"].ToString());
                //gvramcCode = int.Parse(dKey.Values["AMCCode"].ToString());
                //gvrfnumber = dKey.Values["Count"].ToString();
            
                int rowIndex = 0;
                CheckBox chk = (CheckBox)dr.FindControl("rdbGVRow");
                GridDataItem gdi;
                gdi = (GridDataItem)chk.NamingContainer;
                rowIndex = gdi.ItemIndex + 1;

                gvrcustomerId = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex-1]["CustomerId"].ToString());
                gvramcCode = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex-1]["AMCCode"].ToString());
                gvrfnumber = gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex-1]["Count"].ToString();   


                if (((CheckBox)dr.FindControl("rdbGVRow")).Checked == true)
                {
                    bindFolioDropDown(gvrcustomerId, gvramcCode, gvrfnumber);
                    return;
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
            if (ddlMovePortfolio.SelectedValue == "Merge")
            {
                if (folioDs.Tables[0].Rows.Count == 0)
                {
                    lblerror.Visible = true;
                }
            }
            ddlAdvisorBranchList.DataSource = folioDs;
            ddlAdvisorBranchList.DataValueField = folioDs.Tables[0].Columns["CMFA_FolioNum"].ToString();
            ddlAdvisorBranchList.DataBind();
        }



        protected void gvCustomerFolioMerge_SelectedIndexChanged(object sender, EventArgs e)
        {
            

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
            int rowIndex = 0;
            LinkButton lnkIssueCode = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkIssueCode.NamingContainer;
            rowIndex = gdi.ItemIndex + 1;
            //csissueId = int.Parse((gvCSIssueTracker.MasterTableView.DataKeyValues[selectedRow - 1]["CSI_id"].ToString()));

            GridDataItem gvFoliomerge = ((GridDataItem)(((LinkButton)sender).Parent.Parent));

            //GridViewRow gvFoliomerge = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
            //int rowIndex = gvFoliomerge.ItemIndex;

            //string folioNumber = gvCustomerFolioMerge.DataKeys[rowIndex].Values["Count"].ToString();
            ////int accountId = int.Parse(gvCustomerFolioMerge.DataKeys[row.RowIndex].Values[""].ToString());
            //int customerId = int.Parse(gvCustomerFolioMerge.DataKeys[rowIndex].Values["CustomerId"].ToString());
            //int portfolioId = int.Parse(gvCustomerFolioMerge.DataKeys[rowIndex].Values["portfilionumber"].ToString());



            int customerId = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex - 1]["CustomerId"].ToString());
            int portfolioId = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex - 1]["portfilionumber"].ToString());
            string folioNumber = gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex - 1]["Count"].ToString();

            customerVo = customerBo.GetCustomer(customerId);

            Session["customerVo"] = customerVo;
            Session[SessionContents.PortfolioId] = portfolioId;
            Session["folioNum"] = folioNumber;

            GetLatestValuationDate();

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerMFFolioView','none');", true);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            CheckBox rdbGVRow = new CheckBox();
            rdbGVRow = GetGvRadioButton();
            foreach (GridDataItem dr in gvCustomerFolioMerge.Items)
            {
                int rowIndex = dr.RowIndex;
                //DataKey dKey = gvCustomerFolioMerge.DataKeys[rowIndex];

                //int customerId = int.Parse(dKey.Values["CustomerId"].ToString());
                //int amcCode = int.Parse(dKey.Values["AMCCode"].ToString());
                //string fnumber = dKey.Values["Count"].ToString();

                int customerId = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[dr.ItemIndex]["CustomerId"].ToString());
                int amcCode = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[dr.ItemIndex]["AMCCode"].ToString());
                string fnumber = gvCustomerFolioMerge.MasterTableView.DataKeyValues[dr.ItemIndex]["Count"].ToString();        

                if (((CheckBox)dr.FindControl("rdbGVRow")).Checked == true)
                {
                    bool folioDs = false;
                    AdvisorBranchBo adviserBranchBo = new AdvisorBranchBo();
                    string ffromfolio = ddlAdvisorBranchList.SelectedValue.ToString();
                    if (ffromfolio != "")
                    {
                        folioDs = adviserBranchBo.CustomerFolioMerged(ffromfolio, fnumber, customerId);                        
                    }
                    else
                      ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('No Folio To Merge');", true);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdvisorCustomerAccounts','none');", true);
                    if (folioDs == true)
                        trMergeFolioStatus.Visible = true;
                }
            }
            //this.BindCustomer(mypager.CurrentPage);            
            showHideControls(0);
            ddlMovePortfolio.SelectedIndex = 0;
        }

        protected void txtPickCustomer_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPickCustomer.Text.Trim()))
            {
                hdnCustomerId.Value = "0";
                ddlPortfolio.Items.Clear();
            }
            if (hdnCustomerId.Value.ToString() != "0")
            {
                bindDropdownPortfolio(int.Parse(hdnCustomerId.Value.ToString()));
            }
        }

        private void bindDropdownPortfolio(int customerId)
        {
            PortfolioBo portfolioBo = new PortfolioBo();
            try
                {
                    if (hdnCustomerId.Value != null)
                    {
                        dsCustomerPortfolioList = portfolioBo.GetCustomerPortfolio(customerId);
                        ddlPortfolio.DataSource = dsCustomerPortfolioList;
                        ddlPortfolio.DataValueField = dsCustomerPortfolioList.Tables[0].Columns["CP_PortfolioId"].ToString();
                        ddlPortfolio.DataTextField = dsCustomerPortfolioList.Tables[0].Columns["CP_PortfolioName"].ToString();
                        ddlPortfolio.DataBind();
                    }
                }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        // To Move the folio from one customer to another customer's portfolio.

        protected void btnSubmitPortfolio_Click(object sender, EventArgs e)
        {
            try
            {                
                if (hdnCustomerId.Value == "0" && ddlMovePortfolio.SelectedIndex==2)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Select a customer first!');", true);
                }
                else
                {
                    CheckBox rdbGVRow = new CheckBox();
                    rdbGVRow = GetGvRadioButton();
                    int fromPortfolioId;
                    string folioNumber;
                    int amcCode;
                    customerPortfolioID = Convert.ToInt32(ddlPortfolio.SelectedValue);
                    DataSet dsPortFolioUpdate = new DataSet();

                    foreach (GridDataItem dr in gvCustomerFolioMerge.Items)
                    {
                        //int rowIndex;
                        //rowIndex = dr.ItemIndex;
                        //rowIndex = dr.ItemIndex + 1;

                        int rowIndex = 0;
                        CheckBox chk = (CheckBox)dr.FindControl("rdbGVRow");
                        GridDataItem gdi;
                        gdi = (GridDataItem)chk.NamingContainer;
                        rowIndex = gdi.ItemIndex + 1;

                        //DataKey dKey = gvCustomerFolioMerge.DataKeys[rowIndex];
                        if (chk.Checked == true)
                        {
                            AdvisorBranchBo adviserBranchBo = new AdvisorBranchBo();
                            //amcCode = int.Parse(dKey.Values["AMCCode"].ToString());
                            //folioNumber = dKey.Values["Count"].ToString();
                            //fromPortfolioId = Convert.ToInt32(dKey.Values["portfilionumber"].ToString());


                            amcCode = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex-1]["AMCCode"].ToString());
                            folioNumber = gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex-1]["Count"].ToString();
                            fromPortfolioId = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex-1]["portfilionumber"].ToString());


                            isBankAssociatedWithOtherTransactions = adviserBranchBo.CustomerFolioMoveToCustomer(amcCode, folioNumber, fromPortfolioId, customerPortfolioID, isBankAssociatedWithOtherTransactions);
                            if(isBankAssociatedWithOtherTransactions>0)
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Cannot transfer this folio the bank is associate with some other transactions');", true);

                            //dsPortFolioUpdate = adviserBranchBo.CustomerFolioMoveToCustomer(amcCode, folioNumber, fromPortfolioId, customerPortfolioID, isBankAssociatedWithOtherTransactions);
                           
                            break;
                        }
                    }
                    if(isBankAssociatedWithOtherTransactions>0)
                        trFolioStatus.Visible = false;
                    else
                        trFolioStatus.Visible = true;
                    
                    //this.BindCustomer(mypager.CurrentPage);
                    showHideControls(0);
                    ddlMovePortfolio.SelectedIndex = 0;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        protected void showHideControls(int flag)
        {
            if (flag == 0)
            {
                txtPickCustomer.Text = string.Empty;
                ddlAdvisorBranchList.Items.Clear();
                ddlPortfolio.Items.Clear(); 

                trMergeToAnotherAMC.Visible = false;
                trPickCustomer.Visible = false;
                trPickPortfolio.Visible = false;
                trBtnSubmit.Visible = false;
                lblerror.Visible = false;
            }
            else if (flag == 1)
            {
                txtPickCustomer.Text = string.Empty;
                ddlPortfolio.Items.Clear();

                trMergeToAnotherAMC.Visible = true;
                trPickCustomer.Visible = false;
                trPickPortfolio.Visible = false;
                trBtnSubmit.Visible = false;
            }
            else if (flag == 2)
            {
                ddlAdvisorBranchList.Items.Clear();

                lblerror.Visible = false;
                trMergeToAnotherAMC.Visible = false;
                trPickCustomer.Visible = true;
                trPickPortfolio.Visible = true;
                trBtnSubmit.Visible = true;
            }
            else if (flag == 3)
            {
                txtPickCustomer.Text = string.Empty;
                ddlAdvisorBranchList.Items.Clear();

                trMergeToAnotherAMC.Visible = false;
                trPickCustomer.Visible = false;
                trPickPortfolio.Visible = true;
                trBtnSubmit.Visible = true;
                lblerror.Visible = false;
            }            
        }

        protected void gvCustomerFolioMerge_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtFolioDetails = new DataTable();
            dtFolioDetails = (DataTable)Cache["gvCustomerFolioMerge" + adviserVo.advisorId];
            gvCustomerFolioMerge.DataSource = dtFolioDetails;
        }


        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            DataSet dtGvSchemeDetails = new DataSet();
            dtGvSchemeDetails = (DataSet)Cache["gvSchemeDetailsForMappinginSuperAdmin"];
            gvCustomerFolioMerge.DataSource = dtGvSchemeDetails;

            gvCustomerFolioMerge.ExportSettings.OpenInNewWindow = true;
            gvCustomerFolioMerge.ExportSettings.IgnorePaging = true;
            gvCustomerFolioMerge.ExportSettings.HideStructureColumns = true;
            gvCustomerFolioMerge.ExportSettings.ExportOnlyData = true;
            gvCustomerFolioMerge.ExportSettings.FileName = "Accounts Details";
            gvCustomerFolioMerge.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvCustomerFolioMerge.MasterTableView.ExportToExcel();
        }

        protected void ddlMovePortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnCustomerId.Value = "0";
            if (ddlMovePortfolio.SelectedValue == "Merge")
            {
                showHideControls(1);
                ReadCustomerGridDetails();
            }
            else if (ddlMovePortfolio.SelectedValue == "MtoAC")
            {                
                showHideControls(2);
                if (string.IsNullOrEmpty(txtPickCustomer.Text.Trim()))
                {
                    ddlPortfolio.Items.Clear();
                }
            }
            else if (ddlMovePortfolio.SelectedValue == "MtoAP")
            {
                showHideControls(3);

                int customerId = 0;

                foreach (GridDataItem dr in gvCustomerFolioMerge.Items)
                {
                    int rowIndex = dr.RowIndex;
                    //DataKey dKey = gvCustomerFolioMerge.DataKeys[rowIndex];
                    string dKey = gvCustomerFolioMerge.MasterTableView.DataKeyValues[dr.ItemIndex]["CustomerId"].ToString();

                    //string strExtType = gvSchemeDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PASC_AMC_ExternalType"].ToString();
                    if (((CheckBox)dr.FindControl("rdbGVRow")).Checked == true)
                    {
                        //customerId = int.Parse(dKey.Values["CustomerId"].ToString());
                        customerId =Convert.ToInt32(dKey);
                        bindDropdownPortfolio(customerId);
                        return;
                    }
                }
            }
            else if (ddlMovePortfolio.SelectedValue == "S")
            {
                showHideControls(0);
            }
        }
    }
}

            
