using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoUser;
using BoUser;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Drawing.Printing;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Text;
using BoCommon;
using WealthERP.Base; 
using BoWerpAdmin;
using BoCustomerProfiling;
using VoAdvisorProfiling;
using BoCustomerPortfolio;

namespace WealthERP.SuperAdmin
{
    public partial class IFF : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();        
        RMVo rmVo=new RMVo();
        BoWerpAdmin.AdviserMaintenanceBo advisormaintanancebo = new AdviserMaintenanceBo();
        UserBo userBo = new UserBo();
        UserVo userVo = new UserVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        List<string> roleList = new List<string>();
        string sourcePath = "";
        string branchLogoSourcePath = "";
        int rmId;
        int userId;
        int branchId;
        int index;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        List<int> rmList = new List<int>();
        int count;
        Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
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
                this.BindGrid();
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
                    
                    ratio = rowCount / 20;
                    mypager.PageCount = rowCount % 20 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    lowerlimit = (((mypager.CurrentPage - 1) * 20) + 1).ToString();
                    upperlimit = (mypager.CurrentPage * 20).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnCount.Value;
                    PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;
                    //ratio = rowCount / 10;
                    //mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                    //mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    //lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
                    //upperlimit = (mypager.CurrentPage * 10).ToString();
                    //if (mypager.CurrentPage == mypager.PageCount)
                    //    upperlimit = hdnCount.Value;
                    //PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    //lblCurrentPage.Text = PageRecords;
                    //hdnCurrentPage.Value = mypager.CurrentPage.ToString();
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

        protected void BindGrid()
        {
            try
            {

                advisorVo = (AdvisorVo)Session["advisorVo"];
                userVo = (UserVo)Session["UserVo"];
                if (hdnCurrentPage.Value.ToString() != "")
                {
                    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                    hdnCurrentPage.Value = "";
                }
                //if (Session["RM"] != null)
                //{

                    ShowAdvisor("");
                //    Session.Remove("RM");

                //}
                //else
                //{
                //    showAdvisorList();
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
                FunctionInfo.Add("Method", "ViewRMD.ascx.cs:BindGrid()");
                object[] objects = new object[2];
                objects[0] = advisorVo;
                objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //string rm = "";
            SessionBo.CheckSession();
            try
            {
                if (!IsPostBack)
                {
                    mypager.CurrentPage = 1;
                    this.BindGrid();
                }
                //if (Session["RM"] != null)
                //{
                //    ShowRM();
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
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:Page_Load()");
                object[] objects = new object[1];
                objects[0] = rmVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

       
        private void ShowAdvisor(string filterexpression)
        {
            string rm = "";
            DataTable dtAdvisor = new DataTable();
            DataRow drAdvisor;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List<AdvisorVo> advisorvolist = new List<AdvisorVo>();
            AdvisorLOBVo advisorlobvo = new AdvisorLOBVo();            
            int count = 0;
            try
            {

                advisorvolist = advisormaintanancebo.GetAdviserListWithPager(mypager.CurrentPage, out count, hdnSort.Value, filterexpression);
                Session["IFFAdvisorVoList"] = advisorvolist;
                lblTotalRows.Text = hdnCount.Value = count.ToString();
                if (advisorvolist.Count != 0)
                {                    
                    ErrorMessage.Visible = false;
                    dtAdvisor.Columns.Add("AdviserId");
                    dtAdvisor.Columns.Add("UserId");
                    dtAdvisor.Columns.Add("IFFName");
                    //dtAdvisorStaff.Columns.Add("RM Main Branch");
                    dtAdvisor.Columns.Add("Category");
                    dtAdvisor.Columns.Add("IFFAddress");
                    dtAdvisor.Columns.Add("IFFCity");
                    dtAdvisor.Columns.Add("IFFContactPerson");
                    dtAdvisor.Columns.Add("IFFMobileNumber");
                    dtAdvisor.Columns.Add("IFFEmailId");
                    dtAdvisor.Columns.Add("imgIFFComodities");
                    dtAdvisor.Columns.Add("imgIFFLiabilities");
                    dtAdvisor.Columns.Add("imgIFFEquity");
                    dtAdvisor.Columns.Add("imgIFFFixedIncome");
                    dtAdvisor.Columns.Add("imgIFFInsurance");
                    dtAdvisor.Columns.Add("imgIFFMutualfund");
                    dtAdvisor.Columns.Add("imgIFFPMS");
                    dtAdvisor.Columns.Add("imgIFFPostalSavings");
                    dtAdvisor.Columns.Add("imgIFFRealEstate");
                    dtAdvisor.Columns.Add("imgIFFIsActive");
                    DataRow dr;
                    for (int i = 0; i < advisorvolist.Count; i++)
                    {
                        advisorVo = advisorvolist[i];
                        drAdvisor = dtAdvisor.NewRow();
                        //dr = dt.Rows[i];
                        rmVo = new RMVo();

                        drAdvisor[0] = advisorVo.advisorId;
                        drAdvisor[1] = advisorVo.UserId;
                        drAdvisor[2] = advisorVo.OrganizationName;
                        drAdvisor[3] = advisorVo.Category;
                        drAdvisor[4] = advisorVo.AddressLine3;
                        drAdvisor[5] = advisorVo.City;
                        drAdvisor[6] = advisorVo.ContactPersonFirstName;
                        drAdvisor[7] = advisorVo.MobileNumber;
                        drAdvisor[8] = advisorVo.Email;
                        drAdvisor[9] = "";
                        drAdvisor[10] = "";
                        drAdvisor[11] = "";
                        drAdvisor[12] = "";
                        drAdvisor[13] = "";
                        drAdvisor[14] = "";
                        drAdvisor[15] = "";
                        drAdvisor[16] = "";
                        drAdvisor[17] = "";
                        drAdvisor[18] = "";
                        for (int j = 0; j < advisorVo.AdvisorLOBVoList.Count; j++)
                        {
                            advisorlobvo = advisorVo.AdvisorLOBVoList[j];

                            if (advisorlobvo.LOBClassificationType == "Commodities")
                            {
                                if (advisorlobvo.IsDependent == 1)
                                {

                                    drAdvisor[9] = "DE";
                                }
                                else
                                {
                                    drAdvisor[9] = "IN";
                                }
                            }
                            else if (advisorlobvo.LOBClassificationType == "Liabilities:DirectSaleProducts" )
                            {
                                if (advisorlobvo.IsDependent == 1)
                                {

                                    drAdvisor[10] = "DE";
                                }
                                else
                                {
                                    drAdvisor[10] = "IN";
                                }                                
                            }
                            else if (advisorlobvo.LOBClassificationType == "Equity")
                            {
                                if (advisorlobvo.IsDependent == 1)
                                {

                                    drAdvisor[11] = "DE";
                                }
                                else
                                {
                                    drAdvisor[11] = "IN";
                                }
                                
                            }
                            else if (advisorlobvo.LOBClassificationType == "Fixed Income")
                            {
                                if (advisorlobvo.IsDependent == 1)
                                {

                                    drAdvisor[12] = "DE";
                                }
                                else
                                {
                                    drAdvisor[12] = "IN";
                                }
                               
                            }
                            else if (advisorlobvo.LOBClassificationType == "Insurance")
                            {
                                if (advisorlobvo.IsDependent == 1)
                                {

                                    drAdvisor[13] = "DE";
                                }
                                else
                                {
                                    drAdvisor[13] = "IN";
                                }
                                
                            }
                            else if (advisorlobvo.LOBClassificationType == "Mutual Fund")
                            {
                                if (advisorlobvo.IsDependent == 1)
                                {

                                    drAdvisor[14] = "DE";
                                }
                                else
                                {
                                    drAdvisor[14] = "IN";
                                }
                                
                            }
                            else if (advisorlobvo.LOBClassificationType == "PMS")
                            {
                                if (advisorlobvo.IsDependent == 1)
                                {

                                    drAdvisor[15] = "DE";
                                }
                                else
                                {
                                    drAdvisor[15] = "IN";
                                }
                               
                            }
                            else if (advisorlobvo.LOBClassificationType == "Postal Savings")
                            {
                                if (advisorlobvo.IsDependent == 1)
                                {

                                    drAdvisor[16] = "DE";
                                }
                                else
                                {
                                    drAdvisor[16] = "IN";
                                }
                                
                            }
                            else if (advisorlobvo.LOBClassificationType == "Real Estate")
                            {
                                if (advisorlobvo.IsDependent == 1)
                                {

                                    drAdvisor[17] = "DE";
                                }
                                else
                                {
                                    drAdvisor[17] = "IN";
                                }
                                
                            }
                            
                        }
                        if (advisorVo.IsActive == 1)
                        {
                            drAdvisor[18] = "Y";
                        }
                        else
                        {
                            drAdvisor[18] = "N";
                        }
                        
                       dtAdvisor.Rows.Add(drAdvisor);
                    }
                    
                    gvAdvisorList.DataSource = dtAdvisor;
                    gvAdvisorList.DataBind();
                    this.GetPageCount();
                }
                else
                {
                    gvAdvisorList.DataSource = null;
                    gvAdvisorList.DataBind();
                    DivPager.Visible = false;
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
                    ErrorMessage.Visible = true;
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
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:ShowRM()");
                object[] objects = new object[3];
                objects[0] = advisorVo;
                objects[1] = rm;
                objects[2] = rmList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        
       /* public void showAdvisorList()
        {
            rmVo = new RMVo();
            int Count = 0;
            List<RMVo> rmList = new List<RMVo>();
            List<string> roleList = new List<string>();
            //roleList = userBo.GetUserRoles(userVo.UserId);
            try
            {
                //if (roleList.Contains("BM") && Session["CurrentUserRole"] == "BM")
                if (Session[SessionContents.CurrentUserRole] == "BM")
                {
                    rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);

                    branchId = advisorBranchBo.GetBranchId(rmVo.RMId);
                    if (branchId != 0)
                    {
                        //lblMsg.Visible = true;
                        //lblMsg.Text = "You dont have RM.. !";

                        rmList = advisorStaffBo.GetBMRMList(branchId, mypager.CurrentPage, out Count);

                        if (rmList.Count != 0)
                        {
                            lblTotalRows.Text = hdnCount.Value = Count.ToString();
                            DataTable dtAdvisorStaff = new DataTable();

                            dtAdvisorStaff.Columns.Add("UserId");
                            dtAdvisorStaff.Columns.Add("WealthERP Id");
                            dtAdvisorStaff.Columns.Add("RMName");
                            dtAdvisorStaff.Columns.Add("StaffType");
                            dtAdvisorStaff.Columns.Add("StaffRole");
                            dtAdvisorStaff.Columns.Add("Email");
                            dtAdvisorStaff.Columns.Add("Mobile Number");
                            DataRow drAdvisorStaff;

                            for (int i = 0; i < rmList.Count; i++)
                            {
                                drAdvisorStaff = dtAdvisorStaff.NewRow();
                                rmVo = new RMVo();
                                rmVo = rmList[i];
                                drAdvisorStaff[0] = rmVo.UserId.ToString();
                                drAdvisorStaff[1] = rmVo.RMId.ToString();
                                drAdvisorStaff[2] = rmVo.FirstName.ToString() + " " + rmVo.MiddleName.ToString() + " " + rmVo.LastName.ToString();
                                //drAdvisorStaff[3] = rmVo.MainBranch.ToString();
                                if (rmVo.IsExternal == 1)
                                    drAdvisorStaff[3] = "External";
                                else
                                    drAdvisorStaff[3] = "Internal";
                                drAdvisorStaff[4] = rmVo.RMRole.ToString();
                                drAdvisorStaff[5] = rmVo.Email.ToString();
                                drAdvisorStaff[6] = rmVo.Mobile.ToString();
                                dtAdvisorStaff.Rows.Add(drAdvisorStaff);
                            }

                            gvAdvisorList.DataSource = dtAdvisorStaff;
                            gvAdvisorList.DataBind();

                            this.GetPageCount();
                        }
                        else
                        {
                            gvAdvisorList.DataSource = null;
                            gvAdvisorList.DataBind();
                            DivPager.Visible = false;
                            lblCurrentPage.Visible = false;
                            lblTotalRows.Visible = false;
                            ErrorMessage.Visible = true;
                        }
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                    }
                    Session[SessionContents.CurrentUserRole] = null;
                }
                else
                {
                    ErrorMessage.Visible = false;
                    advisorStaffBo = new AdvisorStaffBo();
                    List<RMVo> advisorStaffList = null;
                    advisorStaffList = advisorStaffBo.GetRMList(advisorVo.advisorId, mypager.CurrentPage, hdnSort.Value, out Count, string.Empty);
                    if (advisorStaffList != null)
                    {
                        lblTotalRows.Text = hdnCount.Value = Count.ToString();
                        DataTable dtAdvisorStaff = new DataTable();

                        dtAdvisorStaff.Columns.Add("UserId");
                        dtAdvisorStaff.Columns.Add("WealthERP Id");
                        dtAdvisorStaff.Columns.Add("RMName");
                        dtAdvisorStaff.Columns.Add("StaffType");
                        dtAdvisorStaff.Columns.Add("StaffRole");
                        dtAdvisorStaff.Columns.Add("Email");
                        dtAdvisorStaff.Columns.Add("Mobile Number");
                        DataRow drAdvisorStaff;

                        for (int i = 0; i < advisorStaffList.Count; i++)
                        {
                            drAdvisorStaff = dtAdvisorStaff.NewRow();
                            rmVo = new RMVo();
                            rmVo = advisorStaffList[i];
                            drAdvisorStaff[0] = rmVo.UserId.ToString();
                            drAdvisorStaff[1] = rmVo.RMId.ToString();
                            drAdvisorStaff[2] = rmVo.FirstName.ToString() + " " + rmVo.MiddleName.ToString() + " " + rmVo.LastName.ToString();
                            //drAdvisorStaff[3] = rmVo.MainBranch.ToString();
                            if (rmVo.IsExternal == 1)
                                drAdvisorStaff[3] = "External";
                            else
                                drAdvisorStaff[3] = "Internal";
                            drAdvisorStaff[4] = rmVo.RMRole.ToString();
                            drAdvisorStaff[5] = rmVo.Email.ToString();
                            drAdvisorStaff[6] = rmVo.Mobile.ToString();
                            dtAdvisorStaff.Rows.Add(drAdvisorStaff);
                        }

                        gvAdvisorList.DataSource = dtAdvisorStaff;
                        gvAdvisorList.DataBind();

                        this.GetPageCount();
                    }
                    else
                    {
                        gvAdvisorList.DataSource = null;
                        gvAdvisorList.DataBind();
                        DivPager.Visible = false;
                        lblCurrentPage.Visible = false;
                        lblTotalRows.Visible = false;
                        ErrorMessage.Visible = true;
                    }
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

                FunctionInfo.Add("Method", "ViewRM.ascx:showRMList()");


                object[] objects = new object[4];
                objects[0] = advisorVo;
                objects[1] = rmVo;
                objects[2] = rmId;
                objects[3] = userId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }*/
        protected void gvAdvisorList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                BindCategory(ddlCategory);
                ddlCategory.SelectedValue = hdnCategory.Value;
                
            }
        }
        protected void BindCategory(DropDownList ddlCategory)
        {
            ddlCategory.DataSource = advisorBo.GetXMLAdvisorCategory();
            ddlCategory.DataTextField = "AdviserCategory";
            ddlCategory.DataValueField = "AdviserCategoryCode";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, "Select");
            ListItem li=new ListItem("All","All");
            ddlCategory.Items.Insert(1, li);
        }
        private DropDownList GetCategoryDDL()
        {            
            DropDownList ddl = new DropDownList();
            if (gvAdvisorList.HeaderRow != null)
            {
                if ((DropDownList)gvAdvisorList.HeaderRow.FindControl("ddlCategory") != null)
                {
                    ddl = (DropDownList)gvAdvisorList.HeaderRow.FindControl("ddlCategory");
                }
            }
            else
                ddl = null;

            return ddl;
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCategory = GetCategoryDDL();
            List<AdvisorVo> advisorvolist = new List<AdvisorVo>();
            string rm = "";
            DataTable dtAdvisor = new DataTable();
            DataRow drAdvisor;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            
            AdvisorLOBVo advisorlobvo = new AdvisorLOBVo();   
            if (ddlCategory != null)
            {
                if (ddlCategory.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    advisorvolist = advisormaintanancebo.GetAdviserList();
                    lblTotalRows.Text = hdnCount.Value = count.ToString();
                    if (ddlCategory.SelectedIndex == 1)
                    {
                        ShowAdvisor(""); 
                    }
                    else
                    {
                        hdnCategory.Value = ddlCategory.SelectedItem.Text.ToString();
                        ShowAdvisor(hdnCategory.Value); 
                    }
                                       
                    
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnCategory.Value = "";
                }

                //BindGridView(customerId, mypager.CurrentPage, 0, DateTime.Parse(txtFromTran.Text), DateTime.Parse(txtToTran.Text));
            }
        }
        
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

            string menu;
            try
            {
                DropDownList MyDropDownList = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)MyDropDownList.NamingContainer;
                int selectedRow = gvr.RowIndex;
                userId = int.Parse(gvAdvisorList.DataKeys[selectedRow].Value.ToString());
                //Session["userId"] = userId;
                rmVo = advisorStaffBo.GetAdvisorStaff(userId);
                Session["rmVo"] = rmVo;
                menu = MyDropDownList.SelectedItem.Value.ToString();
                if (menu == "View Dashboard")
                {
                    Session["advisorVo"] = advisorBo.GetAdvisorUser(userId);                    
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrolonly('IFAAdminMainDashboard','none');", true);
                    //AdvisorDashboardValidation(userId);                    
                }
                if (menu == "Edit profile")
                {
                    Session["advisorVo"] = advisorBo.GetAdvisorUser(userId);
                    Session["iffUserVo"] = userBo.GetUserDetails(userId);
                    Session["IFFAdd"] = "Edit";
                    Session.Remove("IDs");
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('IFFAdd','none');", true);
                }
                //if (menu == "User Management")
                //{
                //    Session["UserManagement"] = "IFF";
                //    Session["UserManagementAdvisorVo"] = advisorBo.GetAdvisorUser(userId);
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomerUserDetails','none');", true);
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

                FunctionInfo.Add("Method", "ViewRM.ascx:ddlMenu_SelectedIndexChanged()");


                object[] objects = new object[3];
                objects[0] = advisorStaffBo;
                objects[1] = rmVo;
                objects[2] = userId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }
        protected void AdvisorDashboardValidation(int userId)
        {
            try
            {
                //Session["userVo"] = userBo.GetUserDetails(userId);
                //userVo.UserId = userId;
                Session["advisorVo"] = advisorBo.GetAdvisorUser(userId);
                Session["rmVo"] = advisorStaffBo.GetAdvisorStaff(userId);
                advisorVo = (AdvisorVo)Session["advisorVo"];
                rmVo = (RMVo)Session["rmVo"];
                Session["adviserId"] = advisorBo.GetRMAdviserId(rmVo.RMId);
                if (advisorVo != null)
                {
                    if (advisorVo.LogoPath == null || advisorVo.LogoPath == "")
                    {
                        advisorVo.LogoPath = "";
                    }

                    sourcePath = "Images/" + advisorVo.LogoPath.ToString();

                    Session[SessionContents.LogoPath] = sourcePath;
                }
                roleList = userBo.GetUserRoles(userVo.UserId);
                count = roleList.Count;
                string UserName = userVo.FirstName + " " + userVo.LastName;
                if (count == 3)
                {
                    advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                    Session["advisorBranchVo"] = advisorBranchVo;
                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                    Session["refreshTheme"] = true;
                    Session["SuperAdmin_Status_Check"] = "1";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMBMDashBoard','login','" + UserName + "','" + sourcePath + "');", true);
                    //login user role Type
                    Session["S_CurrentUserRole"] = "Admin";
                }
                if (count == 2)
                {
                    if (roleList.Contains("RM") && roleList.Contains("BM"))
                    {
                        advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                        Session["advisorBranchVo"] = advisorBranchVo;
                        //login user role Type
                        Session["S_CurrentUserRole"] = "RM";
                        branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                        Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                        Session["refreshTheme"] = true;
                        Session["SuperAdmin_Status_Check"] = "1";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('BMRMDashBoard','login','" + UserName + "','" + sourcePath + "','" + branchLogoSourcePath + "');", true);
                    }
                    else if (roleList.Contains("RM") && roleList.Contains("Admin"))
                    {
                        advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                        Session["advisorBranchVo"] = advisorBranchVo;
                        //login user role Type
                        Session["S_CurrentUserRole"] = "Admin";
                        branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                        Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                        Session["refreshTheme"] = true;
                        Session["SuperAdmin_Status_Check"] = "1";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMDashBoard','login','" + UserName + "','" + sourcePath + "');", true);
                    }
                    else if (roleList.Contains("BM") && roleList.Contains("Admin"))
                    {
                        advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                        Session["advisorBranchVo"] = advisorBranchVo;
                        branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                        Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                        //login user role Type
                        Session["S_CurrentUserRole"] = "Admin";
                    }
                }

                //for (int i = 0; i < 2; i++)
                //{
                //    if (roleList[i] == "RM")
                //    {

                //        rmVo = (RMVo)Session["rmVo"];
                //        
                //    }
                //    if (roleList[i] == "BM")
                //    {
                //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorBMDashBoard','login','" + UserName + "','" + sourcePath + "');", true);
                //    }

                //}




                if (count == 1)
                {
                    if (roleList.Contains("RM"))
                    {
                        Session["adviserId"] = advisorBo.GetRMAdviserId(rmVo.RMId);
                        //Session["advisorVo"]=advisorBo.GetAdvisor(
                        branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                        sourcePath = "Images/" + userBo.GetRMLogo(rmVo.RMId);
                        Session[SessionContents.LogoPath] = sourcePath;
                        Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('RMDashBoard','login','" + UserName + "','" + sourcePath + "','" + branchLogoSourcePath + "');", true);

                    }
                    else if (roleList.Contains("BM"))
                    {
                        advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                        Session["advisorBranchVo"] = advisorBranchVo;
                        branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                        Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('BMDashBoard','login','" + UserName + "','" + sourcePath + "','" + branchLogoSourcePath + "');", true);

                    }
                    else
                    {

                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorDashBoard','login','" + UserName + "','" + sourcePath + "');", true);
                    }
                }
                GetLatestValuationDate();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetLatestValuationDate()
        {
            DateTime EQValuationDate = new DateTime();
            DateTime MFValuationDate = new DateTime();
            PortfolioBo portfolioBo = null;
            genDict = new Dictionary<string, DateTime>();
            AdvisorVo advisorVo = new AdvisorVo();
            int adviserId = 0;
            try
            {
                portfolioBo = new PortfolioBo();
                advisorVo = (AdvisorVo)Session["advisorVo"];
                adviserId = advisorVo.advisorId;


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

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void gvAdvisorList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                hdnSort.Value = sortExpression + " DESC";
                this.BindGrid();

            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                hdnSort.Value = sortExpression + " ASC";
                this.BindGrid();

            }

        }

        private void sortGridViewRM(string sortExpression, string direction)
        {
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            List<RMVo> advisorStaffList = null;
            RMVo rmVo = null;
            DataTable dtAdvisorStaff = new DataTable();
            DataRow drAdvisorStaff;
            try
            {

                advisorStaffList = advisorStaffBo.GetRMList(advisorVo.advisorId);

                dtAdvisorStaff.Columns.Add("Sl.No.");
                dtAdvisorStaff.Columns.Add("UserId");
                dtAdvisorStaff.Columns.Add("RM Name");
                dtAdvisorStaff.Columns.Add("Email");

                for (int i = 0; i < advisorStaffList.Count; i++)
                {
                    drAdvisorStaff = dtAdvisorStaff.NewRow();
                    rmVo = new RMVo();

                    rmVo = advisorStaffList[i];
                    drAdvisorStaff[0] = (i + 1).ToString();
                    drAdvisorStaff[1] = rmVo.UserId.ToString();
                    drAdvisorStaff[2] = rmVo.FirstName.ToString() + rmVo.MiddleName.ToString() + rmVo.LastName.ToString();
                    drAdvisorStaff[3] = rmVo.Email.ToString();
                    dtAdvisorStaff.Rows.Add(drAdvisorStaff);
                }
                DataView dv = new DataView(dtAdvisorStaff);
                dv.Sort = sortExpression + direction;
                //dv.Sort = (string)ViewState["sortExpression"];
                gvAdvisorList.DataSource = dv;
                gvAdvisorList.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:sortGridViewRM()");
                object[] objects = new object[2];
                objects[1] = rmVo;
                objects[2] = advisorStaffList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


    }
}