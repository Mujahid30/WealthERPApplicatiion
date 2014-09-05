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
using VoAdvisorProfiling;
using BoUploads;
using VOAssociates;
using BOAssociates;
using BoOps;
using VoOps;
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
        int isBankAssociatedWithOtherTransactions = 0;
        AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        OrderBo orderbo = new OrderBo();
        RMVo rmVo = new RMVo();
        AssociatesVO associatesVo = new AssociatesVO();

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
        int advisorId = 0;
        String userType;
        string AgentCode;
        string UserTitle;
        int IsAgentCodeBased;
        int rmId = 0;
        int bmID = 0;
        int all = 0;
        int branchId = 0;
        int branchHeadId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            //CheckBox rdbGVRow = new CheckBox();
            //rdbGVRow = GetGvRadioButton();
            //rdbGVRow.Attributes.Add("onClick", "javascript:CheckOtherIsCheckedByGVID(value);");
            advisorPreferenceVo = (AdvisorPreferenceVo)(Session["AdvisorPreferenceVo"]);
            adviserVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            associatesVo = (AssociatesVO)Session["associatesVo"];
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();

            if (adviserVo.A_AgentCodeBased == 0)
            {
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                    userType = "advisor";
                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                    userType = "rm";
                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                    userType = "bm";
                else
                    userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();
            }
            else if (adviserVo.A_AgentCodeBased == 1)
            {
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    userType = "advisor";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                {
                    userType = "rm";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                {
                    userType = "bm";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
                {
                    userType = "associates";
                    associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                    if (associateuserheirarchyVo.UserTitle == "SubBroker")
                    {
                        if (associateuserheirarchyVo.AgentCode != null)
                        {
                            AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                        }
                        else
                        {
                            AgentCode = "0";
                        }
                    }
                    else
                    {
                        if (associateuserheirarchyVo.AgentCode != null)
                        {
                            AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                        }
                        else
                        {
                            AgentCode = "0";
                        }
                    }
                }
                else
                {
                    userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();
                }
            }
            advisorId = adviserVo.advisorId;
            IsAgentCodeBased = adviserVo.A_AgentCodeBased;
            //int RMId = rmVo.RMId;
            rmId = rmVo.RMId;
            bmID = rmVo.RMId;

            if (!IsPostBack)
            {
                if (IsAgentCodeBased == 0)
                {
                    if (userType == "advisor" || userType == "rm")
                    {
                        BindBranchDropDown();
                        BindRMDropDown();
                        if (userType == "rm")
                        {
                            ddlBranch.Enabled = false;
                            ddlRM.SelectedValue = rmVo.RMId.ToString();
                            ddlRM.Enabled = false;
                            trAction.Visible = false;
                            Label1.Visible = false;
                        }
                    }
                    if (userType == "bm")
                    {
                        BindBranchForBMDropDown();
                        BindRMforBranchDropdown(0, bmID);
                        trAction.Visible = false;
                        Label1.Visible = false;
                    }
                }
                else if (IsAgentCodeBased==1)
                {
                    if (userType == "advisor" || userType == "rm")
                    {
                        //BindCustomer("All");
                        BindBranchDropDown();
                        BindRMDropDown();
                        trSelect.Visible = true;
                        //Label1.Visible = true;
                        trBranchRM.Visible = false;
                        tdGoBtn.Visible = false;
                        if (userType == "rm")
                        {
                            ddlBranch.Enabled = false;
                            ddlRM.SelectedValue = rmVo.RMId.ToString();
                            ddlRM.Enabled = false;
                            trAction.Visible = false;
                            Label1.Visible = false;
                        }
                    }
                    if (userType == "bm")
                    {
                        //BindCustomer();
                        BindBranchForBMDropDown();
                        BindRMforBranchDropdown(0, bmID);
                        trAction.Visible = false;
                        Label1.Visible = false;
                    }
                }
                if (userType == "associates")
                {
                    trBranchRM.Visible = false;
                    ddlRM.Visible=false;
                    trSelect.Visible = true;
                   // Label1.Visible = true;
                    ddlBranch.Visible = false;
                    tdGoBtn.Visible = false;
                    //btnGo.Visible = false;
                    BindSubBrokerAgentCode(AgentCode);
                    //BindCustomer("All");
                    //BindBranchDropDown();
                    //BindRMDropDown();
                    //ddlRM.SelectedValue = rmVo.RMId.ToString();
                    //ddlRM.Enabled = false;
                }
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
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0);
            }

        }
        protected void BindSubBrokerAgentCode(string AgentCode)
        {
            DataTable dtSubbrokerCode = new DataTable();

            dtSubbrokerCode = orderbo.GetSubBrokerAgentCode(adviserVo.advisorId, AgentCode);

            if (dtSubbrokerCode.Rows.Count > 0)
            {
                ddlBrokerCode.DataSource = dtSubbrokerCode;
                ddlBrokerCode.DataValueField = dtSubbrokerCode.Columns["ACC_AgentId"].ToString();
                ddlBrokerCode.DataTextField = dtSubbrokerCode.Columns["AAC_AgentCode"].ToString();
                ddlBrokerCode.DataBind();
            }
            ddlBrokerCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));

        }
        private void BindRMDropDown()
        {
            try
            {
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                DataTable dt = advisorStaffBo.GetAdviserRM(adviserVo.advisorId);
                if (dt.Rows.Count > 0)
                {
                    ddlRM.DataSource = dt;
                    ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
                    ddlRM.DataTextField = dt.Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "2"));
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
        private void BindBranchForBMDropDown()
        {
            try
            {
                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, bmID, 0);
                if (ds != null)
                {
                    ddlBranch.DataSource = ds.Tables[1]; ;
                    ddlBranch.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {

            try
            {
                DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
                if (ds != null)
                {
                    ddlRM.DataSource = ds.Tables[0]; ;
                    ddlRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlRM.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserEQMIS.ascx:BindRMforBranchDropdown()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindBranchDropDown()
        {
            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(adviserVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlBranch.DataSource = ds;
                    ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
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
        protected void btnGo_Click(object sender, EventArgs e)
        {
            SetParameters();
            //if(IsAgentCodeBased==0)
            //{
            if (IsAgentCodeBased == 0)
            {
                if (userType == "rm" || userType == "bm")
                {
                    BindCustomer();
                    trAction.Visible = false;
                    Label1.Visible = false;
                }
                else
                {
                    BindCustomer();
                    trAction.Visible = true;
                    //Label1.Visible = true;
                }
            }
            else if (IsAgentCodeBased == 1)
            {
                if (userType == "bm")
                {
                    BindCustomer();
                    trAction.Visible = false;
                    Label1.Visible = false;
                }
            }
           
             //else if(IsAgentCodeBased==1)
             //{ if (userType == "advisor"||userType == "rm"||userType=="bm")
             //{  
             //    btnGo.Visible
             //}
             //}
            //Label1.Visible = true;
            //trAction.Visible = true;

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
          
            CustomerTransactionBo CustomerTransactionBo=new CustomerTransactionBo();
            LinkButton lnkOrderNo = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkOrderNo.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            int folioId = int.Parse((gvCustomerFolioMerge.MasterTableView.DataKeyValues[selectedRow - 1]["CMFA_AccountId"].ToString()));
           // Session["FolioId"] = CustomerTransactionBo.GetCustomerMFFolioDetails(folioId);
           
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd','?Folioaction=viewFolioDts&FolioId=" + folioId + "');", true);

            // ScriptManager.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd','FolioId=" + value + "');", true);

        }
        //protected void rgvMultiProductMIS_ItemCommand(object source, GridCommandEventArgs e)
        //{
        ////    if (e.CommandName == "Redirect")
        ////    {
        ////        GridDataItem item = (GridDataItem)e.Item;
        ////        string value = ""; // item.GetDataKeyValue("CMFA_BROKERCODE").ToString();
        ////        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('CustomerMFAccountAdd','strCustomreId=" + value + " ');", true);

        ////    }
        //}
        private void SetParameters()
        {
            if (IsAgentCodeBased == 0)
            {
                gvCustomerFolioMerge.Columns[10].Visible = false;
                gvCustomerFolioMerge.Columns[11].Visible = false;
                gvCustomerFolioMerge.Columns[12].Visible = false;
                gvCustomerFolioMerge.Columns[13].Visible = false;
                gvCustomerFolioMerge.Columns[14].Visible=  false;
                gvCustomerFolioMerge.Columns[15].Visible = false;
                gvCustomerFolioMerge.Columns[16].Visible = false;
                gvCustomerFolioMerge.Columns[17].Visible = false;
                gvCustomerFolioMerge.Columns[18].Visible = false;
                gvCustomerFolioMerge.Columns[19].Visible = false;
                DivCustomerFolio.Style.Add("width","950px");
                if (userType == "advisor")
                {

                    hdnIsassociate.Value = "0";
                    if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                    {
                        hdnadviserId.Value = advisorId.ToString();
                        hdnAll.Value = "0";
                        hdnAgentId.Value = "0";
                        hdnbranchId.Value = "0";
                        hdnrmId.Value = "0";

                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        hdnadviserId.Value = advisorId.ToString();
                        hdnbranchId.Value = ddlBranch.SelectedValue;
                        hdnAgentId.Value = "0";
                        hdnAll.Value = "1";
                        hdnrmId.Value = "0";
                    }
                    else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                    {
                        hdnadviserId.Value = advisorId.ToString();
                        hdnbranchId.Value = "0";
                        hdnAgentId.Value = "0";
                        hdnAll.Value = "2";
                        hdnrmId.Value = ddlRM.SelectedValue; ;
                    }
                    else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                    {
                        hdnadviserId.Value = advisorId.ToString();
                        hdnbranchId.Value = ddlBranch.SelectedValue;
                        hdnrmId.Value = ddlRM.SelectedValue;
                        hdnAgentId.Value = "0";
                        hdnAll.Value = "3";
                    }
                }
                else if (userType == "rm")
                {
                    hdnIsassociate.Value = "0";
                    hdnrmId.Value = rmVo.RMId.ToString();
                    hdnAgentId.Value = "0";
                    hdnAll.Value = "0";
                }
                else if (userType == "bm")
                {
                    hdnIsassociate.Value = "0";
                    if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                    {

                        hdnbranchHeadId.Value = bmID.ToString();
                        hdnbranchId.Value = "0";
                        hdnAgentId.Value = "0";
                        hdnAll.Value = "0";
                        hdnrmId.Value = "0";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        hdnbranchHeadId.Value = bmID.ToString();
                        hdnbranchId.Value = ddlBranch.SelectedValue;
                        hdnAgentId.Value = "0";
                        hdnAll.Value = "1";
                        hdnrmId.Value = "0";
                    }
                    else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                    {
                        hdnbranchHeadId.Value = bmID.ToString();
                        hdnbranchId.Value = "0";
                        hdnAgentId.Value = "0";
                        hdnAll.Value = "2";
                        hdnrmId.Value = ddlRM.SelectedValue; ;
                    }
                    else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                    {
                        hdnbranchHeadId.Value = bmID.ToString();
                        hdnbranchId.Value = ddlBranch.SelectedValue;
                        hdnrmId.Value = ddlRM.SelectedValue;
                        hdnAgentId.Value = "0";
                        hdnAll.Value = "3";
                    }
                }

                if (hdnbranchHeadId.Value == "")
                    hdnbranchHeadId.Value = "0";

                if (hdnbranchId.Value == "")
                    hdnbranchId.Value = "0";

                if (hdnadviserId.Value == "")
                    hdnadviserId.Value = "0";

                if (hdnrmId.Value == "")
                    hdnrmId.Value = "0";
            }
                else if (IsAgentCodeBased == 1)
                {
                    DivCustomerFolio.Attributes.Add("width", "50%");
                    if (userType == "associates")
                    {
                        if (ddlBrokerCode.SelectedIndex != 0)
                        {
                            hdnAgentCode.Value = ddlBrokerCode.SelectedItem.ToString();
                          

                        }
                        else
                        {
                            hdnAgentCode.Value =AgentCode;                           
                        }
                        hdnIsassociate.Value = "1";
                        hdnadviserId.Value = advisorId.ToString();
                        hdnAgentId.Value = "0";
                        hdnbranchHeadId.Value = "0";
                        hdnbranchId.Value = "0";
                        hdnrmId.Value = "0";
                        hdnAll.Value = "0";
                    }
                    if (userType == "advisor")
                    {
                        //if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                        //{
                            hdnadviserId.Value = advisorId.ToString();
                            hdnAll.Value = "0";
                            hdnAgentId.Value = "0";
                            hdnbranchId.Value = "0";
                            hdnAgentCode.Value = "0";
                            hdnrmId.Value = "0";

                        //}
                        //else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                        //{
                        //    hdnadviserId.Value = advisorId.ToString();
                        //    hdnbranchId.Value = ddlBranch.SelectedValue;
                        //    hdnAgentId.Value = "0";
                        //    hdnAll.Value = "1";
                        //    hdnAgentCode.Value = "0";
                        //    hdnrmId.Value = "0";
                        //}
                        //else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                        //{
                        //    hdnadviserId.Value = advisorId.ToString();
                        //    hdnbranchId.Value = "0";
                        //    hdnAgentId.Value = "0";
                        //    hdnAll.Value = "2";
                        //    hdnAgentCode.Value = "0";
                        //    hdnrmId.Value = ddlRM.SelectedValue; ;
                        //}
                        //else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                        //{
                        //    hdnadviserId.Value = advisorId.ToString();
                        //    hdnAll.Value = "0";
                        //    hdnAgentId.Value = "0";
                        //    hdnbranchId.Value = "0";
                        //    hdnAgentCode.Value = "0";
                        //    hdnrmId.Value = "0";
                        //}
                        
                    }
                    //else
                    //{
                    //    if (ddlBranch.SelectedIndex <= 0 && ddlRM.SelectedIndex <= 0)
                    //    hdnadviserId.Value = advisorId.ToString();
                    //    hdnAll.Value = "0";
                    //    hdnAgentId.Value = "0";
                    //    hdnbranchId.Value = "0";
                    //    hdnAgentCode.Value = "0";
                    //    hdnrmId.Value = "0";
                    //}
                if(userType == "rm")
                {
                    hdnrmId.Value = rmVo.RMId.ToString();
                    hdnadviserId.Value = "0";
                    hdnbranchId.Value = "0";
                    hdnAgentCode.Value = "0";
                    hdnbranchHeadId.Value = "0";
                    hdnAll.Value = "0";
                    hdnAgentId.Value = "0";
                }
                if (userType == "bm")
                {
                    if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                    {
                        hdnadviserId.Value = advisorId.ToString();
                        hdnbranchHeadId.Value = bmID.ToString();
                        hdnbranchId.Value = "0";
                        hdnAgentId.Value = "0";
                        hdnAll.Value = "0";
                        hdnrmId.Value = "0";
                        hdnAgentCode.Value = "0";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        hdnadviserId.Value = advisorId.ToString();
                        hdnbranchHeadId.Value = bmID.ToString();
                        hdnbranchId.Value = ddlBranch.SelectedValue;
                        hdnAgentId.Value = "0";
                        hdnAll.Value = "1";
                        hdnrmId.Value = "0";
                        hdnAgentCode.Value = "0";
                    }
                    else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                    {
                        hdnadviserId.Value = advisorId.ToString();
                        hdnbranchHeadId.Value = bmID.ToString();
                        hdnbranchId.Value = "0";
                        hdnAgentId.Value = "0";
                        hdnAll.Value = "2";
                        hdnAgentCode.Value = "0";
                        hdnrmId.Value = ddlRM.SelectedValue; ;
                    }
                    else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                    {
                        hdnadviserId.Value = advisorId.ToString();
                        hdnbranchHeadId.Value = bmID.ToString();
                        hdnbranchId.Value = ddlBranch.SelectedValue;
                        hdnrmId.Value = ddlRM.SelectedValue;
                        hdnAgentId.Value = "0";
                        hdnAgentCode.Value = "0";
                        //hdnadviserId.Value = "0";
                        hdnAll.Value = "3";
                    }
                }
                }

            if (hdnbranchHeadId.Value == "")
                hdnbranchHeadId.Value = "0";

            if (hdnbranchId.Value == "")
                hdnbranchId.Value = "0";

            if (hdnadviserId.Value == "")
                hdnadviserId.Value = "0";

            if (hdnrmId.Value == "")
                hdnrmId.Value = "0";
            }
        private void BindCustomer()
        {
           
            DataSet dsCustomerFolio = new DataSet();
            AdvisorBranchBo adviserBranchBo = new AdvisorBranchBo();
            DataTable dtCustomerFolio = new DataTable();
            try
            {
                SetParameters();
                //if (hdnCurrentPage.Value.ToString() != "")
                //{
                //    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                //    hdnCurrentPage.Value = "";
                //}

                dsCustomerFolio = adviserBranchBo.GetAdviserCustomerFolioMerge(int.Parse(hdnadviserId.Value), int.Parse(hdnAgentId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), userType, IsAgentCodeBased, hdnAgentCode.Value, int.Parse(ddlSelect.SelectedValue));

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
                dtCustomerFolio.Columns.Add("CMFA_AccountId");
                dtCustomerFolio.Columns.Add("IsOnline");
                dtCustomerFolio.Columns.Add("FolioName");
                dtCustomerFolio.Columns.Add("portfilionumber");
                dtCustomerFolio.Columns.Add("mergerstatus");
                dtCustomerFolio.Columns.Add("Nominee");
                dtCustomerFolio.Columns.Add("ModeOfHolding");
                dtCustomerFolio.Columns.Add("CMFA_BROKERCODE");
                dtCustomerFolio.Columns.Add("CMFA_SubBrokerCode");
                dtCustomerFolio.Columns.Add("ZonalManagerName");
                dtCustomerFolio.Columns.Add("AreaManager");
                dtCustomerFolio.Columns.Add("AssociatesName");
                dtCustomerFolio.Columns.Add("ChannelName");
                dtCustomerFolio.Columns.Add("Titles");
                dtCustomerFolio.Columns.Add("ClusterManager");
                dtCustomerFolio.Columns.Add("ReportingManagerName");
                dtCustomerFolio.Columns.Add("UserType");
                dtCustomerFolio.Columns.Add("DeuptyHead");
                    
                //dtCustomerFolio.Columns.Add("DeuptyHead");
               if(dsCustomerFolio.Tables[0].Rows.Count >0)
                {
                    btnExportFilteredData.Visible = true;
                    //trPager.Visible = true;
                    //lblTotalRows.Visible = true;
                    //lblCurrentPage.Visible = true;
                    //trAction.Visible = true;
                    //Label1.Visible = true;

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
                        drCustomerFolio["processId"] = dtCustomer.Rows[i]["processid"];
                        drCustomerFolio["GroupHead"] = dtCustomer.Rows[i]["Parent"];
                        drCustomerFolio["CustomerName"] = dtCustomer.Rows[i]["name"];
                        drCustomerFolio["AMCName"] = dtCustomer.Rows[i]["amcname"];
                        drCustomerFolio["AMCCode"] = dtCustomer.Rows[i]["amccode"];
                        drCustomerFolio["Count"] = dtCustomer.Rows[i]["number"];
                        drCustomerFolio["CMFA_AccountId"] = dtCustomer.Rows[i]["CMFA_AccountId"];
                        drCustomerFolio["IsOnline"] = dtCustomer.Rows[i]["IsOnline"];
                        if (!string.IsNullOrEmpty(dtCustomer.Rows[i]["FolioName"].ToString().Trim()))
                            drCustomerFolio["FolioName"] = dtCustomer.Rows[i]["FolioName"];
                        else
                            drCustomerFolio["FolioName"] = "";
                        drCustomerFolio["CMFA_BROKERCODE"] = dtCustomer.Rows[i]["CMFA_BROKERCODE"];
                        drCustomerFolio["CMFA_SubBrokerCode"] = dtCustomer.Rows[i]["CMFA_SubBrokerCode"];
                        drCustomerFolio["portfilionumber"] = dtCustomer.Rows[i]["portfilionumber"];
                        drCustomerFolio["mergerstatus"] = dtCustomer.Rows[i]["mergerstatus"];
                        drCustomerFolio["Nominee"] = dtCustomer.Rows[i]["Nominee"];
                        drCustomerFolio["ModeOfHolding"] = dtCustomer.Rows[i]["ModeOfHolding"];
                        if (IsAgentCodeBased == 1)
                        {
                            drCustomerFolio["ZonalManagerName"] = dtCustomer.Rows[i]["ZonalManagerName"];
                            drCustomerFolio["AreaManager"] = dtCustomer.Rows[i]["AreaManager"];
                            drCustomerFolio["AssociatesName"] = dtCustomer.Rows[i]["AssociatesName"];
                            drCustomerFolio["ChannelName"] = dtCustomer.Rows[i]["ChannelName"];
                            drCustomerFolio["Titles"] = dtCustomer.Rows[i]["Titles"];
                            drCustomerFolio["ClusterManager"] = dtCustomer.Rows[i]["ClusterManager"];
                            drCustomerFolio["ReportingManagerName"] = dtCustomer.Rows[i]["ReportingManagerName"];
                            drCustomerFolio["UserType"] = dtCustomer.Rows[i]["UserType"];
                            drCustomerFolio["DeuptyHead"] = dtCustomer.Rows[i]["DeputyHead"];
                        }
                        dtCustomerFolio.Rows.Add(drCustomerFolio);
                    }
                       if (Cache["gvCustomerFolioMerge" + adviserVo.advisorId] == null)
                    {
                        Cache.Insert("gvCustomerFolioMerge" + adviserVo.advisorId, dtCustomerFolio);
                    }
                    else
                    {
                        Cache.Remove("gvCustomerFolioMerge" + adviserVo.advisorId);
                        Cache.Insert("gvCustomerFolioMerge" + adviserVo.advisorId, dtCustomerFolio);
                    }
                    DivCustomerFolio.Visible = true;
                    gvCustomerFolioMerge.DataSource = dtCustomerFolio;
                    gvCustomerFolioMerge.PageSize = advisorPreferenceVo.GridPageSize;
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
                    //this.GetPageCount();
                }
               else
               {
                   //hdnRecordCount.Value = "0";
                   ErrorMessage.Visible = true;
                   //trPager.Visible = false;
                   //lblTotalRows.Visible = false;
                   //lblCurrentPage.Visible = false;
                   trAction.Visible = false;
                   Label1.Visible = false;
                   DivCustomerFolio.Visible = false;
                   btnExportFilteredData.Visible = false;                   
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

            int rowIndex = 0;
            LinkButton lnkIssueCode = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkIssueCode.NamingContainer;
            rowIndex = gdi.ItemIndex + 1;

            //DataKey dk = gvCustomerFolioMerge.DataKeys[rowIndex];
            //int customerId = Convert.ToInt32(dk.Value);
            int customerId = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex - 1]["CustomerId"].ToString());
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
                    customerId = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex - 1]["CustomerId"].ToString());
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

                gvrcustomerId = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex - 1]["CustomerId"].ToString());
                gvramcCode = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex - 1]["AMCCode"].ToString());
                gvrfnumber = gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex - 1]["Count"].ToString();


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
                if (hdnCustomerId.Value == "0" && ddlMovePortfolio.SelectedIndex == 2)
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


                            amcCode = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex - 1]["AMCCode"].ToString());
                            folioNumber = gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex - 1]["Count"].ToString();
                            fromPortfolioId = Convert.ToInt32(gvCustomerFolioMerge.MasterTableView.DataKeyValues[rowIndex - 1]["portfilionumber"].ToString());


                            isBankAssociatedWithOtherTransactions = adviserBranchBo.CustomerFolioMoveToCustomer(amcCode, folioNumber, fromPortfolioId, customerPortfolioID, isBankAssociatedWithOtherTransactions);
                            if (isBankAssociatedWithOtherTransactions > 0)
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Cannot transfer this folio the bank is associate with some other transactions');", true);

                            //dsPortFolioUpdate = adviserBranchBo.CustomerFolioMoveToCustomer(amcCode, folioNumber, fromPortfolioId, customerPortfolioID, isBankAssociatedWithOtherTransactions);

                            break;
                        }
                    }
                    if (isBankAssociatedWithOtherTransactions > 0)
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
            else if (flag == 4)
            {
                trAction.Visible = true;
                Label1.Visible = true;
            }
            else if (flag == 5)
            {
                trAction.Visible = true;
                Label1.Visible = true;
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
            DataTable dtGvFolioDetails = new DataTable();
            dtGvFolioDetails = (DataTable)Cache["gvCustomerFolioMerge" + adviserVo.advisorId];
            gvCustomerFolioMerge.DataSource = dtGvFolioDetails;

            gvCustomerFolioMerge.ExportSettings.OpenInNewWindow = true;
            gvCustomerFolioMerge.ExportSettings.IgnorePaging = true;
            gvCustomerFolioMerge.ExportSettings.HideStructureColumns = true;
            gvCustomerFolioMerge.ExportSettings.ExportOnlyData = true;
            gvCustomerFolioMerge.ExportSettings.FileName = "Accounts Details";
            gvCustomerFolioMerge.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvCustomerFolioMerge.MasterTableView.ExportToExcel();
        }
        protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelect.SelectedValue == "S")
            {
                showHideControls(4);
            }
            else if (ddlSelect.SelectedValue == "0")
            {
                BindCustomer();
                showHideControls(5);
            }
            else if (ddlSelect.SelectedValue == "1")
            {
                BindCustomer();
                showHideControls(4);
            }
            else if (ddlSelect.SelectedValue == "2")
            {
                BindCustomer();
                showHideControls(4);
            }
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
                        customerId = Convert.ToInt32(dKey);
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


