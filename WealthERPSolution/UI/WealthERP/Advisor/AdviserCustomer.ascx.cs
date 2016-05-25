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
using System.Xml;
using System.Text;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCommon;
using VoAdvisorProfiling;
using BoSuperAdmin;
using VOAssociates;
using BOAssociates;
using Telerik.Web.UI;
using System.Text.RegularExpressions;

namespace WealthERP.Advisor
{
    public partial class AdviserCustomer : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        int CustomerId;
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();
        static string user = "";
        string UserRole;
        static Dictionary<string, string> genDictParent;
        static Dictionary<string, string> genDictRM;
        static Dictionary<string, string> genDictReassignRM;
        int RowCount = 0;
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        AssociatesVO associatesVo = new AssociatesVO();
        int rmId;
        int branchHeadId;
        AdvisorPreferenceVo advisorPrefernceVo = new AdvisorPreferenceVo();
        string customer = "";
        int ParentId;
        int adviserId;
        int AgentId;
        string AgentCode;
        int rbtnReg;
        AssociatesUserHeirarchyVo assocUsrHeirVo;
        string custCode = null;
        string pan = null;
        int producttype = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            advisorPrefernceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];
            CreationSuccessMessage.Visible = false;
            rmVo = (RMVo)Session["rmVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            associatesVo = (AssociatesVO)Session["associatesVo"];
            ddlCOption.Items[5].Enabled = false;
            assocUsrHeirVo = (AssociatesUserHeirarchyVo)Session["associatesUserHeirarchyVo"];
              if (rbtnRegister.Checked)
            {
                rbtnReg = 0;
            }
            if (rbtnNonRegister.Checked)
            {
                rbtnReg = 1;
            }
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops" || userVo.AdviserRole.ContainsValue("CNT"))
            {
                txtCustomerName_autoCompleteExtender.ContextKey = rbtnReg + "/" + adviserVo.advisorId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserAllCustomerName";
                txtPansearch_autoCompleteExtender.ContextKey = rbtnReg + "/" + adviserVo.advisorId.ToString()+"/"+"Admin"+"/"+0;
                txtPansearch_autoCompleteExtender.ServiceMethod = "GetAdviserAllCustomerPan";
                txtClientCode_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtClientCode_autoCompleteExtender.ServiceMethod = "GetCustCode";


            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";

            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = rbtnReg + "/" + adviserVo.advisorId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserAllCustomerName";
                txtPansearch_autoCompleteExtender.ContextKey = rbtnReg + "/" + adviserVo.advisorId.ToString() + "/" + "Associates" + "/" + associatesVo.AAC_AdviserAgentId;
                txtPansearch_autoCompleteExtender.ServiceMethod = "GetAdviserAllCustomerPan";
                txtClientCode_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtClientCode_autoCompleteExtender.ServiceMethod = "GetCustCode";
            }
         
            if (userVo.UserType == "SuperAdmin")
            {
                UserRole = "advisor";
            }
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "research")
            {
                UserRole = "advisor";
                rmId = rmVo.RMId;
                branchHeadId = rmVo.RMId;
            }
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
            {
                UserRole = "rm";
                rmId = rmVo.RMId;
                branchHeadId = rmVo.RMId;
            }
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
            {
                UserRole = "bm";
                rmId = rmVo.RMId;
                branchHeadId = rmVo.RMId;
            }
          
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates" && !userVo.AdviserRole.ContainsValue("CNT"))
            {
                UserRole = "associates";
                AgentId = associatesVo.AAC_AdviserAgentId;
                branchHeadId = rmVo.RMId;

                rbtnRegister.Visible = false;
                //rbtnNonRegister.Checked = true;

                // assocUsrHeirVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                AgentCode = assocUsrHeirVo.AgentCode;
                ddlCOption.Items[3].Enabled = false;
                ddlCOption.Items[0].Enabled = true;
                //ddlCOption.Items[1].Enabled = false;
                //ddlCOption.Items[2].Enabled = false;
                ddlCOption.Items[4].Enabled = false;
                ddlCOption.Items[5].Enabled = false;

            }
            if (userVo.AdviserRole.ContainsValue("CNT"))
            {
                rmId = 4682;
                UserRole = "advisor";
            }
            if (userVo.UserType == "SuperAdmin")
            {
                if (ddlAdviser.SelectedValue != "Select" && ddlAdviser.SelectedValue != "")
                {
                    adviserId = int.Parse(ddlAdviser.SelectedValue);
                }
                else
                {
                    adviserId = 1000;
                }
            }
            else
            {
                tdLblAdviser.Visible = false;
                tdDdlAdviser.Visible = false;
                ddlAdviser.Visible = false;
                rmId = rmVo.RMId;
                adviserId = adviserVo.advisorId;
                branchHeadId = rmVo.RMId;
            }

            #region
            //RadRotatorImage.RotatorType = RotatorType.AutomaticAdvance;
            #endregion
            if (!IsPostBack)
            {

                trSearchtype.Visible = false;
                if (Request.QueryString["CustomerDeleteStatus"] != null)
                {
                    CreationSuccessMessage.Visible = true;
                }
                if (Session["Current_Link"].ToString() == "AdvisorLeftPane" || Session["Current_Link"].ToString() == "RMCustomerIndividualLeftPane" || Session["Current_Link"].ToString() == "LeftPanel_Links" || Session["Current_Link"].ToString() == "RMLeftPane")
                {
                    if (Session["Customer"] != null)
                    {
                        if (Session["Customer"].ToString() == "Customer")
                        {
                            //BindCustomerGrid();
                        }
                        else
                        {
                            //BindGrid();
                        }
                    }
                    else
                    {
                        //BindGrid();
                    }
                }
                if (userVo.UserType == "SuperAdmin")
                {
                    BindAdviserDropDownList();
                    gvCustomerList.Visible = false;
                }
                if (Request.QueryString["CustCode"] != null)
                {
                    if (Request.QueryString["customerId"] == "CC")
                        custCode = Request.QueryString["CustCode"].ToString();
                    else
                        pan = Request.QueryString["CustCode"].ToString();

                    BindCustomerGrid();
                }
                if (Request.QueryString["reqId"] != null)
                {
                    BindCustomerGrid();
                }
                if (Request.QueryString["action"] != null)
                {
                    rbtnRegister.Visible = false;
                    rbtnNonRegister.Visible = false;
                  
                    tdcustomerlist.Visible = true;
                    lbltype.Visible = false;
                    trfpcustomer.Visible = true;
                }
                else
                {
                    trfpcustomer.Visible = false;
                }
            }
        }
        /// <summary>
        /// This function use to bind Adviser in superadmin
        /// </summary>
        /// 

        protected void ddlCOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearControls();
            if (ddlCOption.SelectedValue == "Name")
            {
                tdtxtCustomerName.Visible = true;
                tdtxtClientCode.Visible = false;
                tdtxtPansearch.Visible = false;
                tdRequestId.Visible = false;
            }
            else if (ddlCOption.SelectedValue == "Panno")
            {
                tdtxtPansearch.Visible = true;
                tdtxtCustomerName.Visible = false;
                tdtxtClientCode.Visible = false;
                tdRequestId.Visible = false;
            }
            else if (ddlCOption.SelectedValue == "Clientcode")
            {
                tdtxtClientCode.Visible = true;
                tdtxtPansearch.Visible = false;
                tdtxtCustomerName.Visible = false;
                tdRequestId.Visible = false;
            }
            else if (ddlCOption.SelectedValue == "ReqId")
            {
                tdRequestId.Visible = true;
                tdtxtClientCode.Visible = false;
                tdtxtPansearch.Visible = false;
                tdtxtCustomerName.Visible = false;
            }
        }
        protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        {

            //if (string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            //{
            //    txtCustomerId.Value = "0";
            //}

        }

        protected void click_Go(object sender, EventArgs e)
        {
            gvCustomerList.DataSource = null;
            BindCustomerGrid();
            tdcustomerlist.Visible = true;
            txtCustomerId.Value = string.Empty;
        }
        private string GetSelectedFilterValue()
        {

            string FilterOn;
            if (Request.QueryString["CustCode"] != null || Request.QueryString["action"] != null)
            {
                FilterOn = "customer";
            }
            else
            {
                if (ddlCOption.SelectedValue == "Name" || ddlCOption.SelectedValue == "Panno" || ddlCOption.SelectedValue == "Clientcode")
                {
                    FilterOn = "customer";

                }
                else if (Request.QueryString["reqId"] != null)
                {
                    FilterOn = "ReqId";
                }
                else
                {
                    FilterOn = ddlCOption.SelectedValue;
                    txtCustomerId.Value = string.Empty;
                }
            }

            return FilterOn;
        }
        protected void BindAdviserDropDownList()
        {
            DataTable dtAdviserList = new DataTable();
            dtAdviserList = superAdminOpsBo.BindAdviserForUpload();
            if (dtAdviserList.Rows.Count > 0)
            {
                ddlAdviser.DataSource = dtAdviserList;
                ddlAdviser.DataTextField = "A_OrgName";
                ddlAdviser.DataValueField = "A_AdviserId";
                ddlAdviser.DataBind();
            }
            ddlAdviser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }
        protected void ddlAdviser_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAdviser.SelectedValue != "Select" && ddlAdviser.SelectedValue != "")
            {
                adviserId = int.Parse(ddlAdviser.SelectedValue);
                DataSet dsAdviserRmDetails = superAdminOpsBo.GetAdviserRmDetails(adviserId);
                if (dsAdviserRmDetails.Tables[0].Rows.Count > 0)
                {
                    rmId = int.Parse(dsAdviserRmDetails.Tables[0].Rows[0]["ar_rmid"].ToString());
                }
                DivCustomerList.Visible = true;
                BindCustomerGrid();
                gvCustomerList.Columns[0].Visible = false;
                gvCustomerList.Visible = true;

            }
            else
            {
                DivCustomerList.Visible = false;
                gvCustomerList.Columns[0].Visible = true;
                gvCustomerList.Visible = false;

            }
        }
        /// <summary>
        /// This Function use to create DataTable 
        /// </summary>
        /// <param name="UserRole"></param>
        /// <returns> Return Column on UserRole condition</returns>

        public DataTable CreateCustomeListTable(string UserRole)
        {
            DataTable dtCustomer = new DataTable();
            dtCustomer.Columns.Add("CustomerId");
            dtCustomer.Columns.Add("ParentId");
            dtCustomer.Columns.Add("ADUL_ProcessId");
            dtCustomer.Columns.Add("ACC_customerCategoryName");
            dtCustomer.Columns.Add("UserId");
            dtCustomer.Columns.Add("RMId");
            dtCustomer.Columns.Add("Group");
            dtCustomer.Columns.Add("Cust_Comp_Name");
            dtCustomer.Columns.Add("PANNumber");
            dtCustomer.Columns.Add("MobileNumber");
            dtCustomer.Columns.Add("PhoneNumber");
            dtCustomer.Columns.Add("Email");
            dtCustomer.Columns.Add("Address");
            dtCustomer.Columns.Add("Area");
            dtCustomer.Columns.Add("City");
            dtCustomer.Columns.Add("Pincode");
            dtCustomer.Columns.Add("custcode");
            if (UserRole != "rm")
            {
                dtCustomer.Columns.Add("AssignedRM");
            }
            dtCustomer.Columns.Add("IsActive");
            dtCustomer.Columns.Add("IsProspect");
            dtCustomer.Columns.Add("IsFPClient");
            dtCustomer.Columns.Add("IsMFKYC");
            dtCustomer.Columns.Add("CreatedOn");
            dtCustomer.Columns.Add("C_ModifiedOn");
            dtCustomer.Columns.Add("C_ModifiedBy");
            dtCustomer.Columns.Add("C_CreatedBy");
            dtCustomer.Columns.Add("PAG_AssetGroupCode");
            if (UserRole != "rm")
            {
                dtCustomer.Columns.Add("BranchName");
            }
            return dtCustomer;
        }

        /// <summary>
        /// This Function use to hide column 
        /// </summary>
        /// <param name="UserRole"></param>

        protected void HideCustomerGridColumn(string UserRole)
        {
            if (UserRole == "rm")
            {
                gvCustomerList.Columns[6].Visible = false;
                gvCustomerList.Columns[7].Visible = false;
            }
            if (UserRole == "associates")
            {
                gvCustomerList.Columns[0].Visible = false;
            }
        }

        /// <summary>
        ///  Binding Customer List at diffrent user role
        /// </summary>

        protected void BindCustomerGrid()
        {

            AdvisorBo adviserBo = new AdvisorBo();
            //List<CustomerVo> customerList = new List<CustomerVo>();
            DataTable dtcustomerList = new DataTable();
            RMVo customerRMVo = new RMVo();
            if (rbtnRegister.Checked == true)
            {
                rbtnReg = 0;

            }
            if (rbtnNonRegister.Checked == true)
            {
                rbtnReg = 1;

            }
            if (Request.QueryString["reqId"] != null)
            {
                string isOnln = Request.QueryString["IsOnl"].ToString();
                if (isOnln == "Online")
                {
                    rbtnReg = 0;
                }
                else if (isOnln == "Offline")
                {
                    rbtnReg = 1;
                }
                    
                txtRequestId.Text = int.Parse(Request.QueryString["reqId"]).ToString();
                dtcustomerList = BindOnDemamd(adviserId, rmId, AgentId, UserRole, branchHeadId, AgentCode, rbtnReg, GetSelectedFilterValue(), (!string.IsNullOrEmpty(txtCustomerId.Value)) ? int.Parse(txtCustomerId.Value) : 0, null, null, (!string.IsNullOrEmpty(custCode)) ? custCode : null, null, null, (!string.IsNullOrEmpty(pan)) ? pan : null, null, null, null, null, null, null, null, null, null, gvCustomerList.PageSize, gvCustomerList.CurrentPageIndex + 1, out genDictParent, out genDictRM, out genDictReassignRM, out RowCount, (!string.IsNullOrEmpty(txtRequestId.Text)) ? int.Parse(txtRequestId.Text) : 0, producttype);
                trRegister.Visible = false;
            }
            else
            {
                dtcustomerList = BindOnDemamd(adviserId, rmId, AgentId, UserRole, branchHeadId, AgentCode, rbtnReg, GetSelectedFilterValue(), (!string.IsNullOrEmpty(txtCustomerId.Value)) ? int.Parse(txtCustomerId.Value) : 0, null, null, (!string.IsNullOrEmpty(custCode)) ? custCode : null, null, null, (!string.IsNullOrEmpty(pan)) ? pan : null, null, null, null, null, null, null, null, null, null, gvCustomerList.PageSize, gvCustomerList.CurrentPageIndex + 1, out genDictParent, out genDictRM, out genDictReassignRM, out RowCount, (!string.IsNullOrEmpty(txtRequestId.Text)) ? int.Parse(txtRequestId.Text) : 0, producttype);
            }
            gvCustomerList.DataSource = dtcustomerList;
            gvCustomerList.VirtualItemCount = RowCount;
            gvCustomerList.DataBind();
            DivCustomerList.Visible = true;
            gvCustomerList.Visible = true;
            pnlCustomerList.Visible = true;
            tdcustomerlist.Visible = true;
            imgexportButton.Visible = true;
            ErrorMessage.Visible = false;

        }

        /// <summary>
        /// Binding Customer List when find customer
        /// </summary>

        protected void BindGrid()
        {
            AdvisorBo adviserBo = new AdvisorBo();
            List<CustomerVo> customerList = new List<CustomerVo>();
            RMVo customerRMVo = new RMVo();
            try
            {
                if (Session["Customer"] != null)
                {
                    customer = Session["Customer"].ToString();
                    if (customer.ToLower().Trim() == "find customer" || customer.ToLower().Trim() == "")
                        customer = string.Empty;
                }

                //customerList = adviserBo.GetStaffUserCustomerList(adviserVo.advisorId, rmId, AgentId, UserRole, branchHeadId, AgentCode, GetSelectedFilterValue(), (!string.IsNullOrEmpty(txtCustomerId.Value)) ? int.Parse(txtCustomerId.Value) : 0, out genDictParent, out genDictRM, out genDictReassignRM);

                if (customerList == null)
                {
                    DivCustomerList.Visible = false;
                    gvCustomerList.Visible = false;
                    // pnlCustomerList.Visible = true; 
                    imgexportButton.Visible = false;
                    ErrorMessage.Visible = true;

                }
                else
                {
                    DataTable dtCustomerList = CreateCustomeListTable(UserRole);
                    HideCustomerGridColumn(UserRole);
                    DataRow drCustomer;
                    for (int i = 0; i < customerList.Count; i++)
                    {
                        drCustomer = dtCustomerList.NewRow();
                        customerVo = new CustomerVo();
                        customerVo = customerList[i];
                        drCustomer["CustomerId"] = customerVo.CustomerId.ToString();
                        drCustomer["ParentId"] = customerVo.ParentId.ToString();
                        if (customerVo.ProcessId == 0)
                        {
                            drCustomer["ADUL_ProcessId"] = "N/A";
                        }
                        else
                            drCustomer["ADUL_ProcessId"] = customerVo.ProcessId.ToString();
                        if (customerVo.ACC_CustomerCategoryName == null)
                        {
                            drCustomer["ACC_CustomerCategoryName"] = "N/A";
                        }
                        else
                            drCustomer["ACC_CustomerCategoryName"] = customerVo.ACC_CustomerCategoryName.ToString();
                        drCustomer["UserId"] = customerVo.UserId.ToString();
                        drCustomer["RMId"] = customerVo.RmId.ToString();
                        if (customerVo.ParentCustomer != null)
                        {
                            drCustomer["Group"] = customerVo.ParentCustomer.ToString();
                        }
                        drCustomer["Cust_Comp_Name"] = cleanCustomerName(customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString());
                        if (customerVo.PANNum != null)
                            drCustomer["PANNumber"] = customerVo.PANNum.ToString();
                        else
                            drCustomer["PANNumber"] = "";
                        drCustomer["MobileNumber"] = customerVo.Mobile1.ToString();
                        drCustomer["PhoneNumber"] = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                        drCustomer["Email"] = customerVo.Email.ToString();
                        if (customerVo.Adr1Line1 == null)
                            customerVo.Adr1Line1 = "";
                        if (customerVo.Adr1Line2 == null)
                            customerVo.Adr1Line2 = "";
                        if (customerVo.Adr1Line3 == null)
                            customerVo.Adr1Line3 = "";
                        if (customerVo.Adr1City == null)
                            customerVo.Adr1City = "";
                        if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() == "")
                            drCustomer["Address"] = "-";
                        else if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() != "")
                            drCustomer["Address"] = customerVo.Adr1Line2.ToString();
                        else if (customerVo.Adr1Line1.ToString() != "" && customerVo.Adr1Line2.ToString() == "")
                            drCustomer["Address"] = customerVo.Adr1Line1.ToString();
                        else
                            drCustomer["Address"] = customerVo.Adr1Line1.ToString() + "," + customerVo.Adr1Line2.ToString();
                        drCustomer["Area"] = customerVo.Adr1Line3.ToString();
                        drCustomer["City"] = customerVo.Adr1City.ToString();
                        drCustomer["Pincode"] = customerVo.Adr1PinCode.ToString();
                        if (UserRole != "rm")
                        {
                            if (customerVo.AssignedRM != null)
                                drCustomer["AssignedRM"] = customerVo.AssignedRM.ToString();
                            else
                                drCustomer["AssignedRM"] = "";
                        }

                        if (customerVo.IsActive == 1)
                        {
                            drCustomer["IsActive"] = "Active";
                        }
                        else
                        {
                            drCustomer["IsActive"] = "In Active";

                        }
                        if (customerVo.IsProspect == 1)
                        {
                            drCustomer["IsProspect"] = "Yes";
                        }
                        else
                        {
                            drCustomer["IsProspect"] = "No";
                        }
                        if (customerVo.IsFPClient == 1)
                        {
                            drCustomer["IsFPClient"] = "Yes";
                        }
                        else
                        {
                            drCustomer["IsFPClient"] = "No";
                        }
                        if (customerVo.MfKYC == 1)
                        {
                            drCustomer["IsMFKYC"] = "Y";
                        }
                        else
                        {
                            drCustomer["IsMFKYC"] = "N";
                        }

                        if (customerVo.Createdon != null)

                            drCustomer["CreatedOn"] = customerVo.Createdon;
                        if (customerVo.Modifiefon != null)
                            drCustomer["C_ModifiedOn"] = customerVo.Modifiefon;
                        if (drCustomer["C_ModifiedBy"] != null)
                            drCustomer["C_ModifiedBy"] = customerVo.Modifiedby.ToString();
                        if (drCustomer["C_CreatedBy"] != null)
                            drCustomer["C_CreatedBy"] = customerVo.CreatedBy.ToString();
                        if (UserRole != "rm")
                        {
                            drCustomer["BranchName"] = customerVo.BranchName;
                        }
                        if (customerVo.CustCode != null)
                        {
                            drCustomer["custcode"] = customerVo.CustCode.ToString();
                        }
                        else
                            drCustomer["custcode"] = "";
                        dtCustomerList.Rows.Add(drCustomer);

                    }

                    if (Cache["CustomerList+UserRole" + adviserVo.advisorId + UserRole] == null)
                    {
                        Cache.Insert("CustomerList+UserRole" + adviserVo.advisorId + UserRole, dtCustomerList);
                    }
                    else
                    {
                        Cache.Remove("CustomerList+UserRole" + adviserVo.advisorId + UserRole);
                        Cache.Insert("CustomerList+UserRole" + adviserVo.advisorId + UserRole, dtCustomerList);
                    }

                    gvCustomerList.DataSource = dtCustomerList;
                    if (advisorPrefernceVo != null)
                    {
                        gvCustomerList.PageSize = advisorPrefernceVo.GridPageSize;
                    }
                    else
                    { gvCustomerList.PageSize = 40; }
                    gvCustomerList.DataBind();
                    DivCustomerList.Visible = true;
                    gvCustomerList.Visible = true;
                    //pnlCustomerList.Style.Add("Height", "410px");
                    //  pnlCustomerList.Visible = true;                    
                    imgexportButton.Visible = true;
                    ErrorMessage.Visible = false;
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
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:BindGrid()");
                object[] objects = new object[4];
                objects[0] = user;
                objects[1] = rmVo;
                objects[2] = customerVo;
                objects[3] = customerList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


        protected void gvCustomerList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (userVo.UserType == "Advisor") {
                if (producttype == 2)
                {
                    gvCustomerList.MasterTableView.GetColumn("Action").Visible = false;
                    gvCustomerList.MasterTableView.GetColumn("MarkFPClient").Visible = false;
                    gvCustomerList.MasterTableView.GetColumn("ActionForProspect").Visible = true;
                }
                else
                {
                    gvCustomerList.MasterTableView.GetColumn("Action").Visible = true;
                    gvCustomerList.MasterTableView.GetColumn("MarkFPClient").Visible = true;
                    gvCustomerList.MasterTableView.GetColumn("ActionForProspect").Visible = false;
                }
                return; 
            }
            if (userVo.UserType == "Associates")
            {
                gvCustomerList.MasterTableView.GetColumn("Action").Visible = false;
                gvCustomerList.MasterTableView.GetColumn("Remove").Visible = false;
                gvCustomerList.MasterTableView.GetColumn("Mark").Visible = false;
            }
            if (userVo.AdviserRole.ContainsValue("CNT")) 
            {
                gvCustomerList.MasterTableView.GetColumn("Action").Visible = true;
                gvCustomerList.MasterTableView.GetColumn("Remove").Visible = false;
                gvCustomerList.MasterTableView.GetColumn("Mark").Visible = false;
            }
           
        }

        protected void gvCustomerList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            AdvisorBo adviserBo = new AdvisorBo();
            List<CustomerVo> customerList = new List<CustomerVo>();
            RMVo customerRMVo = new RMVo();
            DataTable dtcustomerList = new DataTable();
            string prospectType = string.Empty;
            string statustype = string.Empty;
            string rmType = string.Empty;
            if (Request.QueryString["action"] != null)
            {
                rbtnRegister.Visible = false;
                rbtnNonRegister.Visible = false;
                producttype = 1;
                tdcustomerlist.Visible = true;
                lbltype.Visible = false;
            }
            try
            {
                GetGridFilters();
                dtcustomerList = BindOnDemamd(adviserId, rmId, AgentId, UserRole, branchHeadId, AgentCode, rbtnReg, GetSelectedFilterValue(), (!string.IsNullOrEmpty(txtCustomerId.Value) && txtCustomerId.Value != "") ? int.Parse(txtCustomerId.Value) : 0, hdncustomerCategoryFilter.Value, hdnSystemId.Value, hdnClientId.Value, hdnName.Value, hdnGroup.Value, hdnPAN.Value, hdnBranch.Value, rmType, hdnArea.Value, hdnCity.Value, hdnPincode.Value, hdnIsProspect.Value, hdnIsActive.Value, hdnIsMFKYC.Value, hdnProcessId.Value, gvCustomerList.PageSize, gvCustomerList.CurrentPageIndex + 1, out genDictParent, out genDictRM, out genDictReassignRM, out RowCount, (!string.IsNullOrEmpty(txtRequestId.Text) && txtRequestId.Text != "") ? int.Parse(txtRequestId.Text) : 0, producttype);
                gvCustomerList.DataSource = dtcustomerList;
                gvCustomerList.VirtualItemCount = RowCount;


            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:BindCustomerGrid()");
                object[] objects = new object[4];
                objects[0] = user;
                objects[1] = rmId;
                objects[2] = customerVo;
                objects[3] = customerList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }



        }
        /// <summary>
        /// this is use to export customer list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvCustomerList.ExportSettings.OpenInNewWindow = true;
            gvCustomerList.ExportSettings.IgnorePaging = false;
            gvCustomerList.ExportSettings.HideStructureColumns = true;
            gvCustomerList.ExportSettings.ExportOnlyData = true;
            gvCustomerList.ExportSettings.FileName = "Customer List";
            gvCustomerList.ExportSettings.Excel.Format = GridExcelExportFormat.Html;
            gvCustomerList.MasterTableView.ExportToExcel();
        }
        #region no need to apply Drop down filter on group

        protected void RadComboGroup_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //RadComboBox dropdown = sender as RadComboBox;
            //ViewState["ParentId"] = dropdown.SelectedValue;
            //if (ViewState["ParentId"] != "")
            //{
            //    GridColumn column = gvCustomerList.MasterTableView.GetColumnSafe("ParentId");
            //    column.CurrentFilterFunction = GridKnownFunction.EqualTo;
            //    gvCustomerList.CurrentPageIndex = 0;
            //    gvCustomerList.MasterTableView.Rebind();
            //}
            //else
            //{
            //    GridColumn column = gvCustomerList.MasterTableView.GetColumnSafe("ParentId");
            //    column.CurrentFilterFunction = GridKnownFunction.EqualTo;
            //    gvCustomerList.CurrentPageIndex = 0;
            //    gvCustomerList.MasterTableView.Rebind();

            //}
        }
        #endregion
        protected void RadComboRM_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox dropdown = sender as RadComboBox;
            ViewState["RMId"] = dropdown.SelectedValue;
            if (ViewState["RMId"] != "")
            {
                GridColumn column = gvCustomerList.MasterTableView.GetColumnSafe("RMId");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvCustomerList.CurrentPageIndex = 0;
                gvCustomerList.MasterTableView.Rebind();
            }
            else
            {
                GridColumn column = gvCustomerList.MasterTableView.GetColumnSafe("RMId");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvCustomerList.CurrentPageIndex = 0;
                gvCustomerList.MasterTableView.Rebind();
            }
        }
        protected void IsProspect_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox dropdown = sender as RadComboBox;
            ViewState["IsProspect"] = dropdown.SelectedValue.ToString();
            if (ViewState["IsProspect"] != "")
            {
                GridColumn column = gvCustomerList.MasterTableView.GetColumnSafe("IsProspect");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvCustomerList.CurrentPageIndex = 0;
                gvCustomerList.MasterTableView.Rebind();
            }
            else
            {
                GridColumn column = gvCustomerList.MasterTableView.GetColumnSafe("IsProspect");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvCustomerList.CurrentPageIndex = 0;
                gvCustomerList.MasterTableView.Rebind();
            }
        }
        protected void Status_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox dropdown = sender as RadComboBox;
            ViewState["IsActive"] = dropdown.SelectedValue.ToString();
            if (ViewState["IsActive"] != "")
            {
                GridColumn column = gvCustomerList.MasterTableView.GetColumnSafe("IsActive");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvCustomerList.CurrentPageIndex = 0;
                gvCustomerList.MasterTableView.Rebind();
            }
            else
            {
                GridColumn column = gvCustomerList.MasterTableView.GetColumnSafe("IsActive");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvCustomerList.CurrentPageIndex = 0;
                gvCustomerList.MasterTableView.Rebind();
            }
        }
        protected void gvCustomerList_ItemCreated(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridFilteringItem)
            //{
            ////    GridFilteringItem fItem = (GridFilteringItem)e.Item;
            ////    foreach (GridColumn col in gvCustomerList.MasterTableView.Columns)
            ////    {

            ////        (fItem[col.UniqueName].Controls[0] as TextBox).Attributes.Add("onkeyup", "semicolon(this, event)");
            ////    }

            //}
        }
        protected void gvCustomerList_PreRender(object sender, EventArgs e)
        {
            if (UserRole == "associates")
            {
                foreach (GridColumn column in gvCustomerList.Columns)
                {
                    if (column.UniqueName == "CustomerId" | column.UniqueName == "Cust_Comp_Name" | column.UniqueName == "PANNumber" | column.UniqueName == "Action")
                    {
                        column.Visible = true;

                    }
                    else
                    {
                        column.Visible = false;
                    }
                }
                gvCustomerList.Rebind();
            }
            if (!IsPostBack)
            {
                gvCustomerList.MasterTableView.FilterExpression = "([Cust_Comp_Name] LIKE \'%" + customer.Trim() + "%\') ";
                GridColumn column = gvCustomerList.MasterTableView.GetColumnSafe("Cust_Comp_Name");
                column.CurrentFilterFunction = GridKnownFunction.Contains;
                column.CurrentFilterValue = customer;
                gvCustomerList.MasterTableView.Rebind();
            }
            if (gvCustomerList.MasterTableView.FilterExpression != string.Empty)
            {
                RefreshCombos();
            }


        }

        /// <summary>
        /// This function call when textbox filter dependency with dropdown filter
        /// </summary>

        protected void RefreshCombos()
        {
            DataTable dtCustomerList = new DataTable();
            dtCustomerList = (DataTable)Cache["CustomerList+UserRole" + adviserVo.advisorId + UserRole];
            if (dtCustomerList != null)
            {
                DataView view = new DataView(dtCustomerList);
                DataTable distinctValues = view.ToTable();
                DataRow[] rows = distinctValues.Select(gvCustomerList.MasterTableView.FilterExpression.ToString());

                //if(rows.Length>10)
                //    pnlCustomerList.Style.Add("Height", "410px");
                //else
                //    pnlCustomerList.Style.Remove("Height");

                gvCustomerList.MasterTableView.Rebind();
            }
        }
        #region Group filter prerender code
        protected void rcbgroup_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            if (ViewState["ParentId"] != null)
            {
                Combo.SelectedValue = ViewState["ParentId"].ToString();
            }
        }
        #endregion
        protected void rcbRM_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            if (ViewState["RMId"] != null)
            {
                Combo.SelectedValue = ViewState["RMId"].ToString();
            }
        }

        protected void Isprospect_Prerender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            if (ViewState["IsProspect"] != null)
            {
                Combo.SelectedValue = ViewState["IsProspect"].ToString();
            }
        }

        protected void Status_Prerender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            if (ViewState["IsActive"] != null)
            {
                Combo.SelectedValue = ViewState["IsActive"].ToString();
            }
        }
        protected void ddlActionForProspect_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int userId = 0;
            UserBo userBo = new UserBo();
            bool isGrpHead = false;
            if (Session[SessionContents.PortfolioId] != null)
            {
                Session.Remove(SessionContents.PortfolioId);
            }
            try
            {
                DropDownList ddlAction = (DropDownList)sender;
                //RadComboBox ddlAction = (RadComboBox)sender;
                GridDataItem item = (GridDataItem)ddlAction.NamingContainer;
                ParentId = int.Parse(gvCustomerList.MasterTableView.DataKeyValues[item.ItemIndex]["CustomerId"].ToString());
                userId = int.Parse(gvCustomerList.MasterTableView.DataKeyValues[item.ItemIndex]["UserId"].ToString());
                Session["ParentIdForDelete"] = ParentId;
                customerVo = customerBo.GetCustomer(ParentId);
                Session["CustomerVo"] = customerVo;
                isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                if (ddlAction.SelectedItem.Value.ToString() != "Profile")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
                }
                //to set portfolio Id and its details
                customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                Session["customerPortfolioVo"] = customerPortfolioVo;
                if (ddlAction.SelectedItem.Value.ToString() == "Profile")
                {
                    Session["IsDashboard"] = "false";
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                    if (customerVo.IsProspect == 0)
                    {
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                        Session["customerPortfolioVo"] = customerPortfolioVo;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMCustomerIndividualDashboard", "loadcontrol('RMCustomerIndividualDashboard','login');", true);
                    }
                    else
                    {
                        isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                        if (isGrpHead == false)
                        {
                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
                        }
                        else
                        {
                            ParentId = customerVo.CustomerId;
                        }
                        Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                        Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
                        //Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                    }
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "FinancialPlanning")
                {
                    Session["IsDashboard"] = "FP";
                    if (ParentId != 0)
                    {
                        if (customerVo.IsProspect == 0)
                        {
                            Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                        }
                        else
                        {
                            isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                            if (isGrpHead == false)
                            {
                                ParentId = customerBo.GetCustomerGroupHead(ParentId);
                            }
                            else
                            {
                                CustomerId = customerVo.CustomerId;
                            }
                            Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                            Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                            customerVo = customerBo.GetCustomer(ParentId);
                            Session["CustomerVo"] = customerVo;
                        }
                    }
                    Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                    Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "View";
                    Session[SessionContents.FPS_AddProspectListActionStatus] = "FPDashBoard";
                  
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFPDashBoard','login');", true);
                    
                }
            }
            catch(Exception ex)
            {

            }
        }
        protected void ddlAction_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int userId = 0;
            UserBo userBo = new UserBo();
            bool isGrpHead = false;
            if (Session[SessionContents.PortfolioId] != null)
            {
                Session.Remove(SessionContents.PortfolioId);
            }
            try
            {
                DropDownList ddlAction = (DropDownList)sender;
                //RadComboBox ddlAction = (RadComboBox)sender;
                GridDataItem item = (GridDataItem)ddlAction.NamingContainer;
                ParentId = int.Parse(gvCustomerList.MasterTableView.DataKeyValues[item.ItemIndex]["CustomerId"].ToString());
                userId = int.Parse(gvCustomerList.MasterTableView.DataKeyValues[item.ItemIndex]["UserId"].ToString());
                Session["ParentIdForDelete"] = ParentId;
                customerVo = customerBo.GetCustomer(ParentId);
                Session["CustomerVo"] = customerVo;

                if (ddlAction.SelectedItem.Value.ToString() != "Delete Profile")
                {
                    if (ddlAction.SelectedItem.Value.ToString() != "Profile")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
                    }
                }
                if (ddlAction.SelectedItem.Value.ToString() == "Profile")
                {
                    if (customerVo.IsProspect == 0)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
                    }
                    else
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);

                    }

                }
                else
                {
                }
                //to check whether he is group head or not
                isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                //to set portfolio Id and its details
                customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                Session["customerPortfolioVo"] = customerPortfolioVo;
                if (ddlAction.SelectedItem.Value.ToString() == "Dashboard")
                {
                    Session["IsDashboard"] = "true";
                    isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                    if (customerVo.IsProspect == 0)
                    {
                        if (isGrpHead == true)
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorRMCustGroupDashboard", "loadcontrol('AdvisorRMCustGroupDashboard','login');", true);
                        else
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorRMCustIndiDashboard", "loadcontrol('AdvisorRMCustIndiDashboard','login');", true);
                    }
                    else
                    {
                        if (isGrpHead == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorRMCustGroupDashboard", "loadcontrol('AdvisorRMCustGroupDashboard','login');", true);
                        }
                        else
                        {
                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
                            customerVo = customerBo.GetCustomer(ParentId);
                            Session["CustomerVo"] = customerVo;

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorRMCustGroupDashboard", "loadcontrol('AdvisorRMCustIndiDashboard','login');", true);
                        }

                    }
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "Profile")
                {
                    Session["IsDashboard"] = "false";
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                    if (customerVo.IsProspect == 0)
                    {
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                        Session["customerPortfolioVo"] = customerPortfolioVo;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMCustomerIndividualDashboard", "loadcontrol('RMCustomerIndividualDashboard','login');", true);
                    }
                    else
                    {
                        isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                        if (isGrpHead == false)
                        {
                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
                        }
                        else
                        {
                            ParentId = customerVo.CustomerId;
                        }
                        Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                        Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
                        //Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                    }
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "UnitHoldings")
                {
                    Session["IsDashboard"] = "UnitHoldingss";
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                    if (customerVo.IsProspect == 0)
                    {
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                        Session["customerPortfolioVo"] = customerPortfolioVo;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerMFUnitHoldingList','login');", true);
                    }
                    else
                    {
                        isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                        if (isGrpHead == false)
                        {
                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
                        }
                        else
                        {
                            ParentId = customerVo.CustomerId;
                        }
                        Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                        Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerMFUnitHoldingList','login');", true);
                        //Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                    }
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "TransactionBook")
                {
                    Session["IsDashboard"] = "TransactionBooks";
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                    if (customerVo.IsProspect == 0)
                    {
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                        Session["customerPortfolioVo"] = customerPortfolioVo;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineCustomerOrderandTransactionBook','login');", true);
                    }
                    else
                    {
                        isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                        if (isGrpHead == false)
                        {
                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
                        }
                        else
                        {
                            ParentId = customerVo.CustomerId;
                        }
                        Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                        Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineCustomerOrderandTransactionBook','login');", true);
                        //Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                    }
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "SIPBook")
                {
                    Session["IsDashboard"] = "SIPBooks";
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                    if (customerVo.IsProspect == 0)
                    {
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                        Session["customerPortfolioVo"] = customerPortfolioVo;
                        // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SIPBookSummmaryList','login');", true);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "SIPBookSummmaryList", "loadcontrol('SIPBookSummmaryList','?systematicType=" + "SIP" + "');", true);

                    }
                    else
                    {
                        isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                        if (isGrpHead == false)
                        {
                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
                        }
                        else
                        {
                            ParentId = customerVo.CustomerId;
                        }
                        Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                        Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SIPBookSummmaryList','login');", true);
                        //Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "SIPBookSummmaryList", "loadcontrol('SIPBookSummmaryList','?systematicType=" + "SIP" + "');", true);

                    }
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "OrderBook")
                {
                    Session["IsDashboard"] = "OrderBooks";
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                    if (customerVo.IsProspect == 0)
                    {
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                        Session["customerPortfolioVo"] = customerPortfolioVo;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineCustomerOrderandTransactionBook','login');", true);
                    }
                    else
                    {
                        isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                        if (isGrpHead == false)
                        {
                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
                        }
                        else
                        {
                            ParentId = customerVo.CustomerId;
                        }
                        Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                        Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineCustomerOrderandTransactionBook','login');", true);
                        //Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                    }
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "NCDIssueList")
                {
                    Session["IsDashboard"] = "NCDIssueLists";
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                    if (customerVo.IsProspect == 0)
                    {
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                        Session["customerPortfolioVo"] = customerPortfolioVo;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('NCDIssueList','?BondType=" + "FISDSD" + "');", true);
                    }
                    else
                    {
                        isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                        if (isGrpHead == false)
                        {
                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
                        }
                        else
                        {
                            ParentId = customerVo.CustomerId;
                        }
                        Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                        Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('NCDIssueList','?BondType=" + "FISDSD" + "');", true);
                        //Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                    }
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "NCDHoldings")
                {
                    Session["IsDashboard"] = "NCDHoldingss";
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                    if (customerVo.IsProspect == 0)
                    {
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                        Session["customerPortfolioVo"] = customerPortfolioVo;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('NCDIssueHoldings','?BondType=" + "FISDSD" + "');", true);
                    }
                    else
                    {
                        isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                        if (isGrpHead == false)
                        {
                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
                        }
                        else
                        {
                            ParentId = customerVo.CustomerId;
                        }
                        Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                        Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('NCDIssueHoldings','?BondType=" + "FISDSD" + "');", true);
                        //Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                    }
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "IPOIssueList")
                {
                    Session["IsDashboard"] = "IPOIssueLists";
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                    if (customerVo.IsProspect == 0)
                    {
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                        Session["customerPortfolioVo"] = customerPortfolioVo;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IPOIssueList','login');", true);
                    }
                    else
                    {
                        isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                        if (isGrpHead == false)
                        {
                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
                        }
                        else
                        {
                            ParentId = customerVo.CustomerId;
                        }
                        Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                        Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IPOIssueList','login');", true);
                        //Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                    }
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "IPOHoldings")
                {
                    Session["IsDashboard"] = "IPOHoldingss";
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                    if (customerVo.IsProspect == 0)
                    {
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                        Session["customerPortfolioVo"] = customerPortfolioVo;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerIPOHolding','login');", true);
                    }
                    else
                    {
                        isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                        if (isGrpHead == false)
                        {
                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
                        }
                        else
                        {
                            ParentId = customerVo.CustomerId;
                        }
                        Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                        Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerIPOHolding','login');", true);
                        //Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                    }
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "Portfolio")
                {
                    Session["IsDashboard"] = "portfolio";
                    if (customerVo.IsProspect == 0)
                    {
                        customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioDashboard", "loadcontrol('PortfolioDashboard','login');", true);
                    }
                    else
                    {
                        isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                        if (isGrpHead == false)
                        {
                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
                        }
                        else
                        {
                            ParentId = customerVo.CustomerId;
                        }
                        customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                        customerVo = customerBo.GetCustomer(ParentId);
                        Session["CustomerVo"] = customerVo;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioDashboard", "loadcontrol('PortfolioDashboard','login');", true);
                    }
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "Alerts")
                {
                    Session["IsDashboard"] = "alerts";
                    isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                    if (isGrpHead == false)
                    {
                        if (customerVo.IsProspect == 1)
                        {
                            CustomerId = customerBo.GetCustomerGroupHead(ParentId);
                        }
                        else
                        {
                            CustomerId = customerVo.CustomerId;
                        }
                    }
                    else
                    {
                        CustomerId = customerVo.CustomerId;
                    }
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(CustomerId);
                    Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                    customerVo = customerBo.GetCustomer(CustomerId);
                    Session["CustomerVo"] = customerVo;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMAlertNotifications", "loadcontrol('RMAlertNotifications','login');", true);
                }

                else if (ddlAction.SelectedItem.Value.ToString() == "Delete Profile")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
                }

                else if (ddlAction.SelectedItem.Value.ToString() == "FinancialPlanning")
                {
                    Session["IsDashboard"] = "FP";
                    if (ParentId != 0)
                    {
                        if (customerVo.IsProspect == 0)
                        {
                            Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                        }
                        else
                        {
                            isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                            if (isGrpHead == false)
                            {
                                ParentId = customerBo.GetCustomerGroupHead(ParentId);
                            }
                            else
                            {
                                CustomerId = customerVo.CustomerId;
                            }
                            Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                            Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                            customerVo = customerBo.GetCustomer(ParentId);
                            Session["CustomerVo"] = customerVo;
                        }
                    }
                    Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                    Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "View";
                    Session[SessionContents.FPS_AddProspectListActionStatus] = "FPDashBoard";
                    if (adviserVo.advisorId == 1157 && adviserVo.OrganizationName.Contains("Birla Money"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFPGoalDashBoard','login');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFPDashBoard','login');", true);
                    }
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "QuickLinks")
                {

                    Session["IsDashboard"] = "CusDashBoardQuicklinks";
                    isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                    if (isGrpHead == false)
                    {
                        if (customerVo.IsProspect == 1)
                        {
                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
                        }
                        else
                        {
                            ParentId = customerVo.CustomerId;
                        }
                    }
                    else
                    {
                        ParentId = customerVo.CustomerId;
                    }
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                    Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                    customerVo = customerBo.GetCustomer(ParentId);
                    Session["CustomerVo"] = customerVo;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerDashBoardShortcut", "loadcontrol('CustomerDashBoardShortcut','login');", true);
                }
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserCustomer.ascx:ddlAction_OnSelectedIndexChange()");
                object[] objects = new object[5];
                objects[0] = CustomerId;
                objects[1] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                ParentId = int.Parse(Session["ParentIdForDelete"].ToString());
                hdnassociationcount.Value = customerBo.GetAssociationCount("C", ParentId).ToString();
                string asc = Convert.ToString(hdnassociationcount.Value);
                if (asc == "0")
                {
                    DeleteCustomerProfile();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showassocation();", true);
                }
            }
        }

        private void DeleteCustomerProfile()
        {
            try
            {
                customerVo = (CustomerVo)Session["CustomerVo"];
                userVo = (UserVo)Session[SessionContents.UserVo];
                if (customerBo.DeleteCustomer(customerVo.CustomerId, "D"))
                {
                    string DeleteStatus = "Customer Deleted Successfully";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','CustomerDeleteStatus=" + DeleteStatus + "');", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AdviserCustomer','login');", true);
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
                FunctionInfo.Add("Method", "ViewCustomerIndividualProfile.ascx:btnDelete_Click()");
                object[] objects = new object[3];
                objects[0] = customerVo;
                //objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        string cleanCustomerName(string str)
        {
            string custName = str.Trim();
            return Regex.Replace(custName, @"([\s]+)", " ");
        }


        protected void gvCustomerList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.FilterCommandName)
            {

                //GridFilteringItem item = gvCustomerList.MasterTableView.GetItems(GridItemType.FilteringItem)[0] as GridFilteringItem;
                //gvCustomerList.CurrentPageIndex = 0;
                //hdncustomerCategoryFilter.Value = (item["ACC_CustomerCategoryName"].Controls[0] as TextBox).Text;
                //hdnSystemId.Value = (item["CustomerId"].Controls[0] as TextBox).Text;
                //hdnClientId.Value = (item["custcode"].Controls[0] as TextBox).Text;
                //hdnName.Value = (item["Cust_Comp_Name"].Controls[0] as TextBox).Text;
                //hdnGroup.Value = (item["ParentId"].Controls[0] as TextBox).Text;
                //hdnPAN.Value = (item["PANNumber"].Controls[0] as TextBox).Text;
                ////hdnBranch.Value = (item["BranchName"].Controls[0] as TextBox).Text;
                //hdnArea.Value = (item["Area"].Controls[0] as TextBox).Text;
                //hdnCity.Value = (item["City"].Controls[0] as TextBox).Text;
                //hdnIsMFKYC.Value = (item["IsMFKYC"].Controls[0] as TextBox).Text;
                //hdnProcessId.Value = (item["ADUL_ProcessId"].Controls[0] as TextBox).Text;
                //hdnSystemAddDate.Value = (item["CreatedOn"].Controls[0] as TextBox).Text;


            }
            if (e.CommandName == "Mark")
            {
           
                customerBo.CreateProductAssociation(int.Parse(gvCustomerList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CustomerId"].ToString()), "FP");
                LinkButton btnMark = (LinkButton)e.Item.FindControl("btnMark");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                btnMark.Visible = false;
                btnDelete.Visible = true;
            }
            if (e.CommandName == "Remove")
            {
                customerBo.DeleteProductAssociation(int.Parse(gvCustomerList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CustomerId"].ToString()));
                LinkButton btnMark = (LinkButton)e.Item.FindControl("btnMark");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                btnMark.Visible =true ;
                btnDelete.Visible = false;
            }

        }
        protected void GetGridFilters()
        {

            //if (ViewState["IsProspect"] != null)
            //{
            //    if (ViewState["IsProspect"].ToString() == "Yes")
            //    {
            //        hdnIsProspect.Value = "1";
            //    }
            //    else if (ViewState["IsProspect"].ToString() == "No")
            //    {
            //        hdnIsProspect.Value = "0";
            //    }
            //    else
            //    {
            //        hdnIsProspect.Value = "";
            //    }



            //}

            //if (ViewState["IsActive"] !=null)
            //{
            //    if (ViewState["IsActive"].ToString()=="Active")
            //    {
            //            hdnIsActive.Value = "1";
            //    }
            //    else if (ViewState["IsActive"].ToString() == "In Active")
            //    {

            //        hdnIsActive.Value = "0";
            //    }
            //    else
            //    {
            //        hdnIsActive.Value = "";
            //    }
            //}
            if (hdnIsMFKYC.Value != null)
            {
                if (hdnIsMFKYC.Value.ToLower() == "y")
                {
                    hdnIsMFKYC.Value = "1";
                }
                else if (hdnIsMFKYC.Value.ToLower() == "n")
                {
                    hdnIsMFKYC.Value = "0";
                }

            }




        }
        protected DataTable BindOnDemamd(int adviserId, int rmId, int AgentId, string UserRole, int branchHeadId, string agentCode, int rbtnReg, string filterOn, int customerId, string customerCategoryFilter, string customerFilter, string custcodeFilter, string nameFilter, string parentFilter, string panFilter, string BranchFilter, string Rmfilter, string areaFilter, string cityFilter, string pincodeFilter, string IsProspectFilter, string isActiveFilter, string iskycavailableFilter, string processFilter, int pageSize, int pageindex, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictRM, out Dictionary<string, string> genDictReassignRM, out int RowCount, int reqId, int producttype)
        {
            AdvisorBo adviserBo = new AdvisorBo();
            List<CustomerVo> customerList = new List<CustomerVo>();
            DataTable dtCustomerList = new DataTable();
            RMVo customerRMVo = new RMVo();
            try
            {

                customerList = adviserBo.GetStaffUserCustomerList(adviserId, rmId, AgentId, UserRole, branchHeadId, AgentCode, rbtnReg, filterOn, customerId, customerCategoryFilter, customerFilter, custcodeFilter, nameFilter, parentFilter, panFilter, BranchFilter, Rmfilter, areaFilter, cityFilter, pincodeFilter, IsProspectFilter, isActiveFilter, iskycavailableFilter, processFilter, gvCustomerList.PageSize, gvCustomerList.CurrentPageIndex + 1, out genDictParent, out genDictRM, out genDictReassignRM, out RowCount, reqId, producttype);
                if (customerList != null)
                {

                    dtCustomerList = CreateCustomeListTable(UserRole);
                    HideCustomerGridColumn(UserRole);
                    DataRow drCustomer;
                    for (int i = 0; i < customerList.Count; i++)
                    {
                        drCustomer = dtCustomerList.NewRow();
                        customerVo = new CustomerVo();
                        customerVo = customerList[i];
                        drCustomer["CustomerId"] = customerVo.CustomerId.ToString();
                        //drCustomer["ParentId"] = customerVo.ParentId.ToString();
                        if (customerVo.ProcessId == 0)
                        {
                            drCustomer["ADUL_ProcessId"] = "N/A";
                        }
                        else
                            drCustomer["ADUL_ProcessId"] = customerVo.ProcessId.ToString();
                        if (customerVo.ACC_CustomerCategoryName == null)
                        {
                            drCustomer["ACC_CustomerCategoryName"] = "N/A";
                        }
                        else
                            drCustomer["ACC_CustomerCategoryName"] = customerVo.ACC_CustomerCategoryName.ToString();
                        drCustomer["UserId"] = customerVo.UserId.ToString();
                        drCustomer["RMId"] = customerVo.RmId.ToString();
                        if (customerVo.ParentCustomer != null)
                        {
                            drCustomer["Group"] = customerVo.ParentCustomer.ToString();
                        }
                        drCustomer["Cust_Comp_Name"] = cleanCustomerName(customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString());
                        if (customerVo.PANNum != null)
                            drCustomer["PANNumber"] = customerVo.PANNum.ToString();
                        else
                            drCustomer["PANNumber"] = "";
                        drCustomer["MobileNumber"] = customerVo.Mobile1.ToString();
                        drCustomer["PhoneNumber"] = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                        drCustomer["Email"] = customerVo.Email.ToString();
                        if (customerVo.Adr1Line1 == null)
                            customerVo.Adr1Line1 = "";
                        if (customerVo.Adr1Line2 == null)
                            customerVo.Adr1Line2 = "";
                        if (customerVo.Adr1Line3 == null)
                            customerVo.Adr1Line3 = "";
                        if (customerVo.Adr1City == null)
                            customerVo.Adr1City = "";
                        if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() == "")
                            drCustomer["Address"] = "-";
                        else if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() != "")
                            drCustomer["Address"] = customerVo.Adr1Line2.ToString();
                        else if (customerVo.Adr1Line1.ToString() != "" && customerVo.Adr1Line2.ToString() == "")
                            drCustomer["Address"] = customerVo.Adr1Line1.ToString();
                        else
                            drCustomer["Address"] = customerVo.Adr1Line1.ToString() + "," + customerVo.Adr1Line2.ToString();
                        drCustomer["Area"] = customerVo.Adr1Line3.ToString();
                        drCustomer["City"] = customerVo.Adr1City.ToString();
                        drCustomer["Pincode"] = customerVo.Adr1PinCode.ToString();
                        if (UserRole != "rm")
                        {
                            if (customerVo.AssignedRM != null)
                                drCustomer["AssignedRM"] = customerVo.AssignedRM.ToString();
                            else
                                drCustomer["AssignedRM"] = "";
                        }

                        if (customerVo.IsActive == 1)
                        {
                            drCustomer["IsActive"] = "Active";
                        }
                        else
                        {
                            drCustomer["IsActive"] = "In Active";

                        }
                        if (customerVo.IsProspect == 1)
                        {
                            drCustomer["IsProspect"] = "Yes";
                        }
                        else
                        {
                            drCustomer["IsProspect"] = "No";
                        }
                        if (customerVo.IsFPClient == 1)
                        {
                            drCustomer["IsFPClient"] = "Yes";
                        }
                        else
                        {
                            drCustomer["IsFPClient"] = "No";
                        }

                        if (customerVo.MfKYC == 1)
                        {
                            drCustomer["IsMFKYC"] = "Y";
                        }
                        else
                        {
                            drCustomer["IsMFKYC"] = "N";
                        }
                        if (customerVo.Createdon != null)
                            drCustomer["CreatedOn"] = customerVo.Createdon;
                        if (customerVo.Modifiefon != null)
                            drCustomer["C_ModifiedOn"] = customerVo.Modifiefon;
                        if (drCustomer["C_ModifiedBy"] != null)
                            drCustomer["C_ModifiedBy"] = customerVo.Modifiedby.ToString();
                        if (drCustomer["C_CreatedBy"] != null)
                            drCustomer["C_CreatedBy"] = customerVo.CreatedBy.ToString();

                        if (UserRole != "rm")
                        {
                            drCustomer["BranchName"] = customerVo.BranchName;
                        }
                        if (customerVo.CustCode != null)
                            drCustomer["custcode"] = customerVo.CustCode.ToString();
                        else
                            drCustomer["custcode"] = "";
                        drCustomer["PAG_AssetGroupCode"] = customerVo.CustomerPGroup;
                        dtCustomerList.Rows.Add(drCustomer);

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
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:BindCustomerGrid()");
                object[] objects = new object[4];
                objects[0] = user;
                objects[1] = rmId;
                objects[2] = customerVo;
                objects[3] = customerList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtCustomerList;
        }

        protected void clearControls()
        {
            tdcustomerlist.Visible = false;
            pnlCustomerList.Visible = false;
            gvCustomerList.DataSource = null;
            gvCustomerList.CurrentPageIndex = 0;
            txtClientCode.Text = "";
            txtCustomerName.Text = "";
            txtPansearch.Text = "";
            txtCustomerId.Value = string.Empty;
            txtRequestId.Text = string.Empty; ;



        }

        /// <summary>
        /// Binding Customer List when find customer
        /// </summary>
        protected void rbtnRegister_CheckedChanged(object sender, EventArgs e)
        {
            trSearchtype.Visible = true;
            ddlCOption.Items[3].Enabled = true;
            ddlCOption.Items[5].Enabled = false;
            ddlCOption.Items[6].Enabled = true;
            ddlCOption.Items[7].Enabled = false;
        }
        protected void rbtnNonRegister_CheckedChanged(object sender, EventArgs e)
        {
            trSearchtype.Visible = true;
            ddlCOption.Items[5].Enabled = false;
            ddlCOption.Items[3].Enabled = false;
            tdtxtClientCode.Visible = false;
            ddlCOption.Items[6].Enabled = true;
            ddlCOption.Items[7].Enabled = false;
        }
        protected void rbtnProspect_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnProspect.Checked)
            {
                producttype = 2;

                BindCustomerGrid();

            }

        }
        protected void rbtnFpClient_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnFpClient.Checked)
            {
                producttype = 1;

                BindCustomerGrid();

            }
        }

    }


}













#region Unused code

//private SortDirection GridViewSortDirection
//        {
//            get
//            {
//                if (ViewState["sortDirection"] == null)
//                    ViewState["sortDirection"] = SortDirection.Ascending;
//                return (SortDirection)ViewState["sortDirection"];
//            }
//            set { ViewState["sortDirection"] = value; }
//        }

//        protected override void OnInit(EventArgs e)
//        {
//            try
//            {
//                ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
//                mypager.EnableViewState = true;
//                base.OnInit(e);

//            }
//            catch (BaseApplicationException Ex)
//            {
//                throw Ex;
//            }
//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();
//                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:OnInit()");
//                object[] objects = new object[0];

//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;
//            }
//        }

//        private void GetPageCount()
//        {
//            string upperlimit = null;
//            int rowCount = 0;
//            int ratio = 0;
//            string lowerlimit = null;
//            string PageRecords = null;
//            try
//            {
//                if (hdnRecordCount.Value.ToString() != "")
//                    rowCount = Convert.ToInt32(hdnRecordCount.Value);
//                if (rowCount > 0)
//                {
//                    ratio = rowCount / 10;
//                    mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
//                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
//                    if (((mypager.CurrentPage - 1) * 10) != 0)
//                        lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
//                    else
//                        lowerlimit = "1";
//                    upperlimit = (mypager.CurrentPage * 10).ToString();
//                    if (mypager.CurrentPage == mypager.PageCount)
//                        upperlimit = hdnRecordCount.Value;
//                    PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
//                    lblCurrentPage.Text = PageRecords;
//                    hdnCurrentPage.Value = mypager.CurrentPage.ToString();
//                }
//            }
//            catch (BaseApplicationException Ex)
//            {
//                throw Ex;
//            }
//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();

//                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:GetPageCount()");

//                object[] objects = new object[5];
//                objects[0] = upperlimit;
//                objects[1] = rowCount;
//                objects[2] = ratio;
//                objects[3] = lowerlimit;
//                objects[4] = PageRecords;
//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;
//            }
//        }

//        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
//        {
//            try
//            {
//                GetPageCount();
//                if (Session["Customer"] != null)
//                {
//                    if (Session["Customer"].ToString() == "Customer")
//                    {
//                        this.BindGrid(mypager.CurrentPage, 0);
//                    }
//                    else
//                    {
//                        this.BindCustomer(mypager.CurrentPage);

//                    }
//                }
//                else
//                {
//                    this.BindCustomer(mypager.CurrentPage);
//                }
//            }
//            catch (BaseApplicationException Ex)
//            {
//                throw Ex;
//            }
//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();
//                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:HandlePagerEvent()");
//                object[] objects = new object[2];
//                objects[0] = mypager.CurrentPage;
//                objects[1] = user;
//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;
//            }
//        }

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            SessionBo.CheckSession();
//            userVo = (UserVo)Session["userVo"];
//            CreationSuccessMessage.Visible = false;
//            if (!IsPostBack)
//            {
//                if (Request.QueryString["CustomerDeleteStatus"] != null)
//                {
//                    CreationSuccessMessage.Visible = true;
//                }
//                if (Session["Current_Link"].ToString() == "AdvisorLeftPane" || Session["Current_Link"].ToString() == "RMCustomerIndividualLeftPane" || Session["Current_Link"].ToString() == "LeftPanel_Links")
//                {
//                    if (Session["Customer"] != null)
//                    {
//                        if (Session["Customer"].ToString() == "Customer")
//                        {
//                            this.BindGrid(mypager.CurrentPage, 0);
//                        }
//                        else
//                        {
//                            this.BindCustomer(mypager.CurrentPage);

//                        }
//                    }
//                    else
//                    {
//                        this.BindCustomer(mypager.CurrentPage);
//                    }
//                }

//            }

//        }

//        protected void BindGrid(int CurrentPage, int export)
//        {

//            Dictionary<string, string> genDictParent = new Dictionary<string, string>();
//            genDictReassignRM = new Dictionary<string, string>();
//            genDictRM = new Dictionary<string, string>();

//            RMVo customerRMVo = new RMVo();
//            CustomerBo customerBo = new CustomerBo();

//            try
//            {
//                DropDownList ddl = new DropDownList();
//                Label lbl = new Label();

//                adviserVo = (AdvisorVo)Session["advisorVo"];

//                if (export == 1)
//                {
//                    trMessage.Visible = true;
//                    ErrorMessage.Visible = true;
//                    trPager.Visible = false;
//                    lblTotalRows.Visible = false;
//                    gvCustomers.AllowPaging = false;
//                    customerList = advisorBo.GetAdviserAllCustomerList(adviserVo.advisorId);
//                }
//                else
//                {
//                    if (hdnCurrentPage.Value.ToString() != "")
//                    {
//                        mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
//                        hdnCurrentPage.Value = "";
//                    }

//                    int Count;

//                    customerList = advisorBo.GetAdviserCustomerList(adviserVo.advisorId, mypager.CurrentPage, out Count, hdnSort.Value, hndPAN.Value, hdnNameFilter.Value, hdnAreaFilter.Value, hdnPincodeFilter.Value, hdnParentFilter.Value, hdnRMFilter.Value, hdnactive.Value, hdnIsProspect.Value, out genDictParent, out genDictRM, out genDictReassignRM);
//                    lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
//                }

//                if (customerList != null)
//                {

//                    trMessage.Visible = false;
//                    ErrorMessage.Visible = false;
//                    trPager.Visible = true;
//                    lblTotalRows.Visible = true;
//                    lblCurrentPage.Visible = true;
//                    DataTable dtRMCustomer = new DataTable();
//                    dtRMCustomer.Columns.Add("ParentId");
//                    dtRMCustomer.Columns.Add("ADUL_ProcessId");
//                    dtRMCustomer.Columns.Add("UserId");
//                    dtRMCustomer.Columns.Add("RMId");
//                    dtRMCustomer.Columns.Add("Parent");
//                    dtRMCustomer.Columns.Add("Cust_Comp_Name");
//                    dtRMCustomer.Columns.Add("PAN Number");
//                    dtRMCustomer.Columns.Add("Mobile Number");
//                    dtRMCustomer.Columns.Add("Phone Number");
//                    dtRMCustomer.Columns.Add("Email");
//                    dtRMCustomer.Columns.Add("Address");
//                    dtRMCustomer.Columns.Add("Area");
//                    dtRMCustomer.Columns.Add("City");
//                    dtRMCustomer.Columns.Add("Pincode");
//                    dtRMCustomer.Columns.Add("AssignedRM");
//                    dtRMCustomer.Columns.Add("IsActive");
//                    dtRMCustomer.Columns.Add("IsProspect");
//                    dtRMCustomer.Columns.Add("IsFPClient");
//                    dtRMCustomer.Columns.Add("BranchName");

//                    DataRow drRMCustomer;

//                    for (int i = 0; i < customerList.Count; i++)
//                    {
//                        drRMCustomer = dtRMCustomer.NewRow();
//                        customerVo = new CustomerVo();
//                        customerVo = customerList[i];
//                        drRMCustomer["ParentId"] = customerVo.ParentId.ToString();
//                        if (customerVo.ProcessId == 0)
//                        {
//                            drRMCustomer["ADUL_ProcessId"] = "N/A";
//                        }
//                        else
//                        drRMCustomer["ADUL_ProcessId"] = customerVo.ProcessId.ToString();
//                        drRMCustomer["UserId"] = customerVo.UserId.ToString();
//                        drRMCustomer["RMId"] = customerVo.RmId.ToString();
//                        drRMCustomer["BranchName"] = customerVo.BranchName;
//                        if (customerVo.ParentCustomer != null)
//                        {
//                            drRMCustomer["Parent"] = customerVo.ParentCustomer.ToString();
//                        }
//                        drRMCustomer["Cust_Comp_Name"] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
//                        //if (customerVo.Type.ToUpper().ToString() == "IND" || customerVo.Type == "")
//                        //{
//                        //    if (customerVo.ParentCustomer != null)
//                        //    {
//                        //        drRMCustomer[3] = customerVo.ParentCustomer.ToString();
//                        //    }
//                        //    else
//                        //    {
//                        //        drRMCustomer[3] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
//                        //    }
//                        //    drRMCustomer[4] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
//                        //}
//                        //else if (customerVo.Type.ToUpper().ToString() == "NIND")
//                        //{
//                        //    if (customerVo.ParentCompany != null)
//                        //    {
//                        //        drRMCustomer[3] = customerVo.ParentCompany.ToString();
//                        //    }
//                        //    else
//                        //    {
//                        //        drRMCustomer[3] = customerVo.CompanyName.ToString();
//                        //    }
//                        //    drRMCustomer[4] = customerVo.CompanyName.ToString();
//                        //}
//                        if (customerVo.PANNum != null)
//                            drRMCustomer["PAN Number"] = customerVo.PANNum.ToString();
//                        else
//                            drRMCustomer["PAN Number"] = "";
//                        drRMCustomer["Mobile Number"] = customerVo.Mobile1.ToString();
//                        drRMCustomer["Phone Number"] = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
//                        drRMCustomer["Email"] = customerVo.Email.ToString();
//                        if (customerVo.Adr1City == null)
//                            customerVo.Adr1City = "";
//                        if (customerVo.Adr1Line1 == null)
//                            customerVo.Adr1Line1 = "";
//                        if (customerVo.Adr1Line2 == null)
//                            customerVo.Adr1Line2 = "";
//                        if (customerVo.Adr1Line3 == null)
//                            customerVo.Adr1Line3 = "";
//                        if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() == "")
//                        {
//                            drRMCustomer["Address"] = "-";
//                        }
//                        else if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() != "")
//                        {
//                            drRMCustomer["Address"] = customerVo.Adr1Line2.ToString();
//                        }
//                        else if (customerVo.Adr1Line1.ToString() != "" && customerVo.Adr1Line2.ToString() == "")
//                        {
//                            drRMCustomer["Address"] = customerVo.Adr1Line1.ToString();
//                        }
//                        else
//                            drRMCustomer["Address"] = customerVo.Adr1Line1.ToString() + "," + customerVo.Adr1Line2.ToString();
//                        drRMCustomer["Area"] = customerVo.Adr1Line3.ToString();
//                        drRMCustomer["City"] = customerVo.Adr1City.ToString();
//                        drRMCustomer["Pincode"] = customerVo.Adr1PinCode.ToString();
//                        //customerRMVo = advisorStaffBo.GetAdvisorStaff(advisorStaffBo.GetUserId(customerVo.RmId));
//                        //drRMCustomer[13] = customerVo.AssignedRM.ToString();
//                        //customerRMVo.FirstName.ToString() + " " + customerRMVo.MiddleName.ToString() + " " + customerRMVo.LastName.ToString();
//                        if (customerVo.AssignedRM != null)
//                            drRMCustomer["AssignedRM"] = customerVo.AssignedRM.ToString();
//                        else
//                            drRMCustomer["AssignedRM"] = "-";
//                        if (customerVo.IsActive == 1)
//                        {
//                            drRMCustomer["IsActive"] = "Active";
//                        }
//                        else
//                        {
//                            drRMCustomer["IsActive"] = "In Active";

//                        }
//                        if (customerVo.IsProspect == 1)
//                        {
//                            drRMCustomer["IsProspect"] = "Yes";
//                        }
//                        else
//                        {
//                            drRMCustomer["IsProspect"] = "No";
//                        }
//                        if (customerVo.IsFPClient == 1)
//                        {
//                            drRMCustomer["IsFPClient"] = "Yes";
//                        }
//                        else
//                        {
//                            drRMCustomer["IsFPClient"] = "No";
//                        }
//                        dtRMCustomer.Rows.Add(drRMCustomer);
//                    }
//                    gvCustomers.DataSource = dtRMCustomer;
//                    gvCustomers.DataBind();

//                    //ReAssignRMControl(genDictReassignRM);

//                    if (genDictParent.Count > 0)
//                    {
//                        DropDownList ddlParent = GetParentDDL();
//                        if (ddlParent != null)
//                        {
//                            ddlParent.DataSource = genDictParent;
//                            ddlParent.DataTextField = "Value";
//                            ddlParent.DataValueField = "Key";
//                            ddlParent.DataBind();
//                            ddlParent.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
//                        }
//                        if (hdnParentFilter.Value != "")
//                        {
//                            ddlParent.SelectedValue = hdnParentFilter.Value.ToString();
//                        }
//                        DropDownList ddlActiveFilter = GetActiveDDL();
//                        if (hdnactive.Value != "")
//                        {
//                            ddlActiveFilter.SelectedValue = hdnactive.Value.ToString();
//                        }
//                    }

//                    if (genDictRM.Count > 0)
//                    {
//                        DropDownList ddlRM = GetRMDDL();
//                        if (ddlRM != null)
//                        {
//                            ddlRM.DataSource = genDictRM;
//                            ddlRM.DataTextField = "Value";
//                            ddlRM.DataValueField = "Key";
//                            ddlRM.DataBind();
//                            ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
//                        }
//                        if (hdnRMFilter.Value != "")
//                        {
//                            ddlRM.SelectedValue = hdnRMFilter.Value.ToString();
//                        }
//                    }

//                    TextBox txtName = GetCustNameTextBox();
//                    if (txtName != null)
//                    {
//                        if (hdnNameFilter.Value != "")
//                        {
//                            txtName.Text = hdnNameFilter.Value.ToString();
//                        }
//                    }

//                    TextBox txtPincode = GetPincodeTextBox();
//                    if (txtPincode != null)
//                    {
//                        if (hdnPincodeFilter.Value != "")
//                        {
//                            txtPincode.Text = hdnPincodeFilter.Value.ToString();
//                        }
//                    }

//                    TextBox txtPAN = GetPANTextBox();
//                    if (txtPAN != null)
//                    {
//                        if (hndPAN.Value != "")
//                        {
//                            txtPAN.Text = hndPAN.Value.ToString();
//                        }
//                    }


//                    TextBox txtArea = GetAreaTextBox();
//                    if (txtArea != null)
//                    {
//                        if (hdnAreaFilter.Value != "")
//                        {
//                            txtArea.Text = hdnAreaFilter.Value.ToString();
//                        }
//                    }

//                    this.GetPageCount();
//                }
//                else
//                {
//                    hdnRecordCount.Value = "0";
//                    trMessage.Visible = true;
//                    ErrorMessage.Visible = true;
//                    trPager.Visible = false;
//                    lblTotalRows.Visible = false;
//                    tblGV.Visible = false;
//                }
//            }
//            catch (BaseApplicationException Ex)
//            {
//                throw Ex;
//            }
//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();
//                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:BindGrid()");
//                object[] objects = new object[4];
//                objects[0] = user;
//                objects[1] = rmVo;
//                objects[2] = customerVo;
//                objects[3] = customerList;
//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;
//            }
//        }

//        protected void gvCustomers_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            try
//            {
//                ParentId = int.Parse(gvCustomers.SelectedDataKey.Value.ToString());
//                customerVo = customerBo.GetCustomer(ParentId);
//                Session["CustomerVo"] = customerVo;
//                if (customerVo.Type == "Individual")
//                {
//                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomerIndividualDashboard','none');", true);
//                }
//                if (customerVo.Type == "Non Individual")
//                {
//                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomerNonIndividualDashboard','none');", true);
//                }
//            }
//            catch (BaseApplicationException Ex)
//            {
//                throw Ex;
//            }
//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();
//                FunctionInfo.Add("Method", "AdviserCustomer.ascx:gvCustomers_SelectedIndexChanged()");
//                object[] objects = new object[2];
//                objects[0] = ParentId;
//                objects[1] = customerVo;
//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;

//            }
//        }

//        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
//        {
//            DropDownList ddlAction = null;
//            GridViewRow gvr = null;
//            int selectedRow = 0;
//            int userId = 0;
//            UserVo tempUser = null;
//            UserBo userBo = new UserBo();
//            bool isGrpHead = false;

//            if (Session[SessionContents.PortfolioId] != null)
//            {
//                Session.Remove(SessionContents.PortfolioId);
//            }
//            try
//            {
//                ddlAction = (DropDownList)sender;
//                gvr = (GridViewRow)ddlAction.NamingContainer;
//                selectedRow = gvr.RowIndex;
//                ParentId = int.Parse(gvCustomers.DataKeys[selectedRow].Values["ParentId"].ToString());
//                userId = int.Parse(gvCustomers.DataKeys[selectedRow].Values["UserId"].ToString());
//                Session["ParentIdForDelete"] = ParentId;
//                customerVo = customerBo.GetCustomer(ParentId);
//                Session["CustomerVo"] = customerVo;

//                if (ddlAction.SelectedItem.Value.ToString() != "Delete Profile")
//                {
//                    if (ddlAction.SelectedItem.Value.ToString() != "Profile")
//                    {
//                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
//                    }
//                }
//                if (ddlAction.SelectedItem.Value.ToString() == "Profile")
//                {
//                    if (customerVo.IsProspect == 0)
//                    {
//                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
//                    }
//                    else
//                    {
//                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
//                    }

//                }
//                else
//                {
//                }
//                //to check whether he is group head or not
//                isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
//                //to set portfolio Id and its details
//                customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
//                Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
//                Session["customerPortfolioVo"] = customerPortfolioVo;
//                if (ddlAction.SelectedItem.Value.ToString() == "Dashboard")
//                {
//                    Session["IsDashboard"] = "true";
//                    isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
//                    if (customerVo.IsProspect == 0)
//                    {
//                        if (isGrpHead == true)
//                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorRMCustGroupDashboard", "loadcontrol('AdvisorRMCustGroupDashboard','login');", true);
//                        else
//                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorRMCustIndiDashboard", "loadcontrol('AdvisorRMCustIndiDashboard','login');", true);
//                    }
//                    else
//                    {
//                        if (isGrpHead == true)
//                        {
//                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorRMCustGroupDashboard", "loadcontrol('AdvisorRMCustGroupDashboard','login');", true);
//                        }
//                        else
//                        {
//                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
//                            customerVo = customerBo.GetCustomer(ParentId);
//                            Session["CustomerVo"] = customerVo;

//                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorRMCustGroupDashboard", "loadcontrol('AdvisorRMCustIndiDashboard','login');", true);
//                        }

//                    }
//                }
//                else if (ddlAction.SelectedItem.Value.ToString() == "Profile")
//                {
//                    Session["IsDashboard"] = "false";
//                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
//                    if (customerVo.IsProspect == 0)
//                    {
//                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
//                        Session["customerPortfolioVo"] = customerPortfolioVo;
//                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMCustomerIndividualDashboard", "loadcontrol('RMCustomerIndividualDashboard','login');", true);
//                    }
//                    else
//                    {
//                        //Session["IsDashboard"] = "FP";
//                        //if (ParentId != 0)
//                        //{
//                        //    Session[SessionContents.FPS_ProspectList_ParentId] = ParentId;
//                        //}
//                        /////Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
//                        //Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "View";
//                        ////Session[SessionContents.FPS_AddProspectListActionStatus] = "FPDashBoard";
//                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
//                        isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
//                        if (isGrpHead == false)
//                        {
//                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
//                        }
//                        else
//                        {
//                            ParentId = customerVo.ParentId;
//                        }
//                        Session[SessionContents.FPS_ProspectList_ParentId] = ParentId;
//                        Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
//                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
//                        //Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
//                    }
//                }
//                else if (ddlAction.SelectedItem.Value.ToString() == "Portfolio")
//                {
//                    Session["IsDashboard"] = "portfolio";
//                    if (customerVo.IsProspect == 0)
//                    {
//                        customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
//                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
//                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioDashboard", "loadcontrol('PortfolioDashboard','login');", true);
//                    }
//                    else
//                    {
//                        isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
//                        if (isGrpHead == false)
//                        {
//                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
//                        }
//                        else
//                        {
//                            ParentId = customerVo.ParentId;
//                        }
//                        customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
//                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
//                        customerVo = customerBo.GetCustomer(ParentId);
//                        Session["CustomerVo"] = customerVo;
//                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioDashboard", "loadcontrol('PortfolioDashboard','login');", true);

//                    }

//                }
//                else if (ddlAction.SelectedItem.Value.ToString() == "Alerts")
//                {

//                    Session["IsDashboard"] = "alerts";
//                    isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
//                    if (isGrpHead == false)
//                    {
//                        if (customerVo.IsProspect == 1)
//                        {
//                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
//                        }
//                        else
//                        {
//                            ParentId = customerVo.ParentId;
//                        }
//                    }
//                    else
//                    {
//                        ParentId = customerVo.ParentId;
//                    }
//                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
//                    Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
//                    customerVo = customerBo.GetCustomer(ParentId);
//                    Session["CustomerVo"] = customerVo;
//                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMAlertNotifications", "loadcontrol('RMAlertNotifications','login');", true);
//                }
//                //else if (ddlAction.SelectedItem.Value.ToString() == "User Details")
//                //{
//                //    //tempUser = new UserVo();
//                //    //tempUser = userBo.GetUserDetails(userId);
//                //    //Session["CustomerUser"] = tempUser;
//                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GenerateLoginPassword", "loadcontrol('GenerateLoginPassword','?GenLoginPassword_UserId=" + userId + "');", true);

//                //}
//                else if (ddlAction.SelectedItem.Value.ToString() == "Delete Profile")
//                {
//                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
//                }
//                else if (ddlAction.SelectedItem.Value.ToString() == "FinancialPlanning")
//                {
//                    Session["IsDashboard"] = "FP";
//                    if (ParentId != 0)
//                    {
//                        if (customerVo.IsProspect == 0)
//                        {
//                            Session[SessionContents.FPS_ProspectList_ParentId] = ParentId;
//                        }
//                        else
//                        {
//                            isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
//                            if (isGrpHead == false)
//                            {
//                                ParentId = customerBo.GetCustomerGroupHead(ParentId);
//                            }
//                            else
//                            {
//                                ParentId = customerVo.ParentId;
//                            }
//                            Session[SessionContents.FPS_ProspectList_ParentId] = ParentId;
//                            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
//                            Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
//                            customerVo = customerBo.GetCustomer(ParentId);
//                            Session["CustomerVo"] = customerVo;
//                        }
//                    }
//                    Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
//                    Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "View";
//                    //if (customerVo.Type == "IND")
//                    //{
//                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerProspect", "loadcontrol('CustomerProspect','login');", true);
//                    //}
//                    //if (customerVo.Type == "NIND")
//                    //{
//                    //    //I'm not passing login parameter in this function.... that is becuase in JScript.js page the code corresponding to load RMCustomerIndividualLeftPane or RMCustomerNonIndividualLeftPane
//                    //    //have been written in that way. so Please try to understand before modifying the code
//                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerProspect", "loadcontrol('CustomerProspect');", true);
//                    //}

//                    Session[SessionContents.FPS_AddProspectListActionStatus] = "FPDashBoard";
//                    if (adviserVo.advisorId == 1157 && adviserVo.OrganizationName.Contains("Birla Money"))
//                    {
//                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFPGoalDashBoard','login');", true);
//                    }
//                    else
//                    {
//                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFPDashBoard','login');", true);
//                    }
//                }
//                else if (ddlAction.SelectedItem.Value.ToString() == "QuickLinks")
//                {

//                    Session["IsDashboard"] = "CusDashBoardQuicklinks";
//                    isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
//                    if (isGrpHead == false)
//                    {
//                        if (customerVo.IsProspect == 1)
//                        {
//                            ParentId = customerBo.GetCustomerGroupHead(ParentId);
//                        }
//                        else
//                        {
//                            ParentId = customerVo.ParentId;
//                        }
//                    }
//                    else
//                    {
//                        ParentId = customerVo.ParentId;
//                    }
//                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
//                    Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
//                    customerVo = customerBo.GetCustomer(ParentId);
//                    Session["CustomerVo"] = customerVo;
//                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerDashBoardShortcut", "loadcontrol('CustomerDashBoardShortcut','login');", true);
//                }
//            }
//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();

//                FunctionInfo.Add("Method", "AdviserCustomer.ascx:ddlAction_OnSelectedIndexChange()");


//                object[] objects = new object[5];
//                objects[0] = ParentId;
//                objects[1] = customerVo;
//                objects[2] = ddlAction;
//                objects[3] = gvr;
//                objects[4] = selectedRow;



//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;

//            }

//        }

//        private void BindCustomer(int CurrentPage)
//        {
//            Dictionary<string, string> genDictParent = new Dictionary<string, string>();
//            Dictionary<string, string> genDictRM = new Dictionary<string, string>();
//            Dictionary<string, string> genDictReassignRM = new Dictionary<string, string>();

//            string customer = "";
//            AdvisorBo adviserBo = new AdvisorBo();
//            List<CustomerVo> customerList = new List<CustomerVo>();
//            RMVo customerRMVo = new RMVo();
//            DataTable dtRMCustomer = new DataTable();
//            try
//            {

//                //DropDownList ddl = new DropDownList();
//                //Label lbl = new Label();
//                //if (gvCustomers.HeaderRow != null)
//                //{
//                //    if ((DropDownList)gvCustomers.HeaderRow.FindControl("ddlReassignRM") != null)
//                //    {
//                //        ddl = (DropDownList)gvCustomers.HeaderRow.FindControl("ddlReassignRM");
//                //        lbl = (Label)(gvCustomers.HeaderRow.FindControl("lblAssignedRMHeader"));
//                //        ddl.Visible = false;
//                //        lbl.Visible = true;

//                //    }
//                //}

//                if (Session["Customer"] != null)
//                {
//                    customer = Session["Customer"].ToString();
//                    if (customer.ToLower().Trim() == "find customer" || customer.ToLower().Trim() == "")
//                        customer = "";
//                    // Search Term is input into this hidden field
//                    hdnNameFilter.Value = customer;
//                }

//                rmVo = (RMVo)Session["rmVo"];

//                adviserVo = (AdvisorVo)Session["advisorVo"];

//                if (hdnCurrentPage.Value.ToString() != "")
//                {
//                    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
//                    hdnCurrentPage.Value = "";
//                }

//                int Count = 0;

//                // Search Term is input into this hidden field
//                hdnNameFilter.Value = customer;

//                customerList = adviserBo.GetAdviserCustomerList(adviserVo.advisorId, mypager.CurrentPage, out Count, hdnSort.Value, hndPAN.Value, hdnNameFilter.Value, hdnAreaFilter.Value, hdnPincodeFilter.Value, hdnParentFilter.Value, hdnRMFilter.Value, hdnactive.Value, hdnIsProspect.Value, out genDictParent, out genDictRM, out genDictReassignRM);

//                lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();

//                if (customerList == null)
//                {
//                    hdnRecordCount.Value = "0";
//                    trMessage.Visible = true;
//                    ErrorMessage.Visible = true;
//                    trPager.Visible = false;
//                    lblTotalRows.Visible = false;
//                    tblGV.Visible = false;
//                }
//                else
//                {
//                    trMessage.Visible = false;
//                    ErrorMessage.Visible = false;
//                    trPager.Visible = true;
//                    lblTotalRows.Visible = true;

//                    dtRMCustomer.Columns.Add("ParentId");
//                    dtRMCustomer.Columns.Add("ADUL_ProcessId");
//                    dtRMCustomer.Columns.Add("UserId");
//                    dtRMCustomer.Columns.Add("RMId");
//                    dtRMCustomer.Columns.Add("Parent");
//                    dtRMCustomer.Columns.Add("Cust_Comp_Name");
//                    dtRMCustomer.Columns.Add("PAN Number");
//                    dtRMCustomer.Columns.Add("Mobile Number");
//                    dtRMCustomer.Columns.Add("Phone Number");
//                    dtRMCustomer.Columns.Add("Email");
//                    dtRMCustomer.Columns.Add("Address");
//                    dtRMCustomer.Columns.Add("Area");
//                    dtRMCustomer.Columns.Add("City");
//                    dtRMCustomer.Columns.Add("Pincode");
//                    dtRMCustomer.Columns.Add("AssignedRM");
//                    dtRMCustomer.Columns.Add("IsActive");
//                    dtRMCustomer.Columns.Add("IsProspect");
//                    dtRMCustomer.Columns.Add("IsFPClient");
//                    dtRMCustomer.Columns.Add("BranchName");
//                    DataRow drRMCustomer;

//                    for (int i = 0; i < customerList.Count; i++)
//                    {
//                        drRMCustomer = dtRMCustomer.NewRow();
//                        customerVo = new CustomerVo();
//                        customerVo = customerList[i];
//                        drRMCustomer[0] = customerVo.ParentId.ToString();
//                        if (customerVo.ProcessId == 0)
//                        {
//                            drRMCustomer[1] = "N/A";
//                        }
//                        else
//                            drRMCustomer[1] = customerVo.ProcessId.ToString();
//                        drRMCustomer[2] = customerVo.UserId.ToString();
//                        drRMCustomer[3] = customerVo.RmId.ToString();
//                        drRMCustomer["BranchName"] = customerVo.BranchName;
//                        if (customerVo.ParentCustomer != null)
//                        {
//                            drRMCustomer[4] = customerVo.ParentCustomer.ToString();
//                        }

//                        drRMCustomer[5] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();

//                        //if (customerVo.Type.ToUpper().ToString() == "IND" || customerVo.Type == "")
//                        //{
//                        //    if (customerVo.ParentCustomer != null)
//                        //    {
//                        //        drRMCustomer[3] = customerVo.ParentCustomer.ToString();
//                        //    }
//                        //    else
//                        //    {
//                        //        drRMCustomer[3] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
//                        //    }
//                        //    drRMCustomer[4] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
//                        //}
//                        //else if (customerVo.Type.ToUpper().ToString() == "NIND")
//                        //{
//                        //    if (customerVo.ParentCompany != null)
//                        //    {
//                        //        drRMCustomer[3] = customerVo.ParentCompany.ToString();
//                        //    }
//                        //    else
//                        //    {
//                        //        drRMCustomer[3] = customerVo.CompanyName.ToString();
//                        //    }
//                        //    drRMCustomer[4] = customerVo.CompanyName.ToString();
//                        //}
//                        if (customerVo.PANNum != null)
//                            drRMCustomer[6] = customerVo.PANNum.ToString();
//                        else
//                            drRMCustomer[6] = "";
//                        drRMCustomer[7] = customerVo.Mobile1.ToString();
//                        drRMCustomer[8] = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
//                        drRMCustomer[9] = customerVo.Email.ToString();
//                        if (customerVo.Adr1Line1 == null)
//                            customerVo.Adr1Line1 = "";
//                        if (customerVo.Adr1Line2 == null)
//                            customerVo.Adr1Line2 = "";
//                        if (customerVo.Adr1Line3 == null)
//                            customerVo.Adr1Line3 = "";
//                        if (customerVo.Adr1City == null)
//                            customerVo.Adr1City = "";
//                        if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() == "")
//                            drRMCustomer[10] = "-";
//                        else if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() != "")
//                            drRMCustomer[10] = customerVo.Adr1Line2.ToString();
//                        else if (customerVo.Adr1Line1.ToString() != "" && customerVo.Adr1Line2.ToString() == "")
//                            drRMCustomer[10] = customerVo.Adr1Line1.ToString();
//                        else
//                            drRMCustomer[10] = customerVo.Adr1Line1.ToString() + "," + customerVo.Adr1Line2.ToString();

//                        drRMCustomer[11] = customerVo.Adr1Line3.ToString();
//                        drRMCustomer[12] = customerVo.Adr1City.ToString();
//                        drRMCustomer[13] = customerVo.Adr1PinCode.ToString();
//                        if (customerVo.AssignedRM != null)
//                            drRMCustomer[14] = customerVo.AssignedRM.ToString();
//                        else
//                            drRMCustomer[14] = "";
//                        if (customerVo.IsActive == 1)
//                        {
//                            drRMCustomer[15] = "Active";
//                        }
//                        else
//                        {
//                            drRMCustomer[15] = "In Active";

//                        }
//                        if (customerVo.IsProspect == 1)
//                        {
//                            drRMCustomer[16] = "Yes";
//                        }
//                        else
//                        {
//                            drRMCustomer[16] = "No";
//                        }
//                        if (customerVo.IsFPClient == 1)
//                        {
//                            drRMCustomer[17] = "Yes";
//                        }
//                        else
//                        {
//                            drRMCustomer[17] = "No";
//                        }
//                        dtRMCustomer.Rows.Add(drRMCustomer);
//                    }

//                    gvCustomers.DataSource = dtRMCustomer;
//                    gvCustomers.DataBind();

//                    //ReAssignRMControl(genDictRM);

//                    if (genDictParent.Count > 0)
//                    {
//                        DropDownList ddlParent = GetParentDDL();
//                        if (ddlParent != null)
//                        {
//                            ddlParent.DataSource = genDictParent;
//                            ddlParent.DataTextField = "Value";
//                            ddlParent.DataValueField = "Key";
//                            ddlParent.DataBind();
//                            ddlParent.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
//                        }
//                        if (hdnParentFilter.Value != "")
//                        {
//                            ddlParent.SelectedValue = hdnParentFilter.Value.ToString();
//                        }
//                    }

//                    if (genDictRM.Count > 0)
//                    {
//                        DropDownList ddlRM = GetRMDDL();
//                        if (ddlRM != null)
//                        {
//                            ddlRM.DataSource = genDictRM;
//                            ddlRM.DataTextField = "Value";
//                            ddlRM.DataValueField = "Key";
//                            ddlRM.DataBind();
//                            ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
//                        }
//                        if (hdnRMFilter.Value != "")
//                        {
//                            ddlRM.SelectedValue = hdnRMFilter.Value.ToString();
//                        }
//                    }

//                    TextBox txtName = GetCustNameTextBox();
//                    if (txtName != null)
//                    {
//                        if (hdnNameFilter.Value != "")
//                        {
//                            txtName.Text = hdnNameFilter.Value.ToString();
//                        }
//                    }

//                    TextBox txtPincode = GetPincodeTextBox();
//                    if (txtPincode != null)
//                    {
//                        if (hdnPincodeFilter.Value != "")
//                        {
//                            txtPincode.Text = hdnPincodeFilter.Value.ToString();
//                        }
//                    }

//                    TextBox txtPAN = GetPANTextBox();
//                    if (txtPAN != null)
//                    {
//                        if (hndPAN.Value != "")
//                        {
//                            txtPAN.Text = hndPAN.Value.ToString();
//                        }
//                    }


//                    TextBox txtArea = GetAreaTextBox();
//                    if (txtArea != null)
//                    {
//                        if (hdnNameFilter.Value != "")
//                        {
//                            txtArea.Text = hdnAreaFilter.Value.ToString();
//                        }
//                    }

//                    this.GetPageCount();
//                }
//            }
//            catch (BaseApplicationException Ex)
//            {
//                throw Ex;
//            }
//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();
//                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:BindCustomer()");
//                object[] objects = new object[3];
//                objects[0] = rmVo;
//                objects[1] = customerVo;
//                objects[2] = customerList;
//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;
//            }
//        }
//        /*
//                private void ReAssignRMControl(Dictionary<string, string> genDictReassignRM)
//                {
//                    // genDictRM = new Dictionary<string, string>();
//                    if (gvCustomers.HeaderRow != null)
//                    {
//                        if (hdnReassignRM.Value != "")
//                        {
//                            ((CheckBox)gvCustomers.HeaderRow.FindControl("chkReassignRM")).Checked = true;
//                        }
//                        else
//                        {
//                            ((CheckBox)gvCustomers.HeaderRow.FindControl("chkReassignRM")).Checked = false;
//                        }
//                        if (((CheckBox)gvCustomers.HeaderRow.FindControl("chkReassignRM")).Checked)
//                        {
//                            DropDownList ddl1 = new DropDownList();
//                            Label lbl1 = new Label();
//                            foreach (GridViewRow gvr in gvCustomers.Rows)
//                            {
//                                if ((DropDownList)gvr.FindControl("ddlReassignRM") != null)
//                                {
//                                    ddl1 = (DropDownList)gvr.FindControl("ddlReassignRM");
//                                    ddl1.Visible = true;
//                                    ddl1.DataSource = genDictReassignRM;
//                                    ddl1.DataTextField = "Value";
//                                    ddl1.DataValueField = "Key";
//                                    ddl1.DataBind();
//                                    lbl1 = (Label)(gvr.FindControl("lblAssignedRMHeader"));
//                                    lbl1.Visible = false;
//                                    ddl1.SelectedValue = gvCustomers.DataKeys[gvr.RowIndex].Values["RMId"].ToString();
//                                }
//                            }
//                        }
//                        else
//                        {
//                            DropDownList ddl1 = new DropDownList();
//                            Label lbl1 = new Label();
//                            foreach (GridViewRow gvr in gvCustomers.Rows)
//                            {
//                                if ((DropDownList)gvr.FindControl("ddlReassignRM") != null)
//                                {
//                                    ddl1 = (DropDownList)gvr.FindControl("ddlReassignRM");
//                                    ddl1.Visible = false;
//                                    lbl1 = (Label)(gvr.FindControl("lblAssignedRMHeader"));
//                                    lbl1.Visible = true;
//                                }
//                            }
//                        }

//                    }
//                }
//                */
//        protected void gvCustomers_Sort(object sender, GridViewSortEventArgs e)
//        {
//            string sortExpression = null;
//            try
//            {
//                sortExpression = e.SortExpression;
//                ViewState["sortExpression"] = sortExpression;
//                if (GridViewSortDirection == SortDirection.Ascending)
//                {
//                    GridViewSortDirection = SortDirection.Descending;
//                    hdnSort.Value = sortExpression + " DESC";
//                    this.BindGrid(1, 0);
//                }
//                else
//                {
//                    GridViewSortDirection = SortDirection.Ascending;
//                    hdnSort.Value = sortExpression + " ASC";
//                    this.BindGrid(1, 0);
//                }
//            }
//            catch (BaseApplicationException Ex)
//            {
//                throw Ex;
//            }
//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();

//                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:gvCustomers_Sort()");

//                object[] objects = new object[1];
//                objects[0] = sortExpression;

//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;
//            }

//        }

//        private static void PrepareControlForExport(Control control)
//        {
//            for (int i = 0; i < control.Controls.Count; i++)
//            {
//                Control current = control.Controls[i];
//                if (current is LinkButton)
//                {
//                    control.Controls.Remove(current);
//                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
//                }
//                else if (current is ImageButton)
//                {
//                    control.Controls.Remove(current);
//                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
//                }
//                else if (current is HyperLink)
//                {
//                    control.Controls.Remove(current);
//                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
//                }
//                else if (current is DropDownList)
//                {
//                    control.Controls.Remove(current);
//                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedValue.ToString()));
//                }
//                else if (current is CheckBox)
//                {
//                    control.Controls.Remove(current);
//                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
//                }

//                if (current.HasControls())
//                {
//                    PrepareControlForExport(current);
//                }
//            }
//        }

//        //private void ExportGridView(string ExportFormat)
//        //{


//        //    gvCustomers.Columns[0].Visible = false;
//        //    gvCustomers.HeaderRow.Visible = true;
//        //    if (rbtnMultiple.Checked)
//        //    {

//        //        BindGrid(mypager.CurrentPage, 1);

//        //    }
//        //    else
//        //    {
//        //        BindGrid(mypager.CurrentPage, 0);
//        //    }

//        //    PrepareGridViewForExport(gvCustomers);

//        //    if (ExportFormat == "Excel")
//        //    {
//        //        ExportGridView("Excel");
//        //    }
//        //    if (ExportFormat == "Word")
//        //    {
//        //        ExportGridView("Word");
//        //    }
//        //    if (ExportFormat == "PDF")
//        //    {
//        //        ExportGridView("PDF");

//        //    }
//        //    if (ExportFormat == "Print")
//        //    {
//        //        GridView_Print();
//        //    }

//        //    BindGrid(mypager.CurrentPage, 0);
//        //    gvCustomers.Columns[0].Visible = true;

//        //}

//        private void PrepareGridViewForExport(Control gv)
//        {
//            LinkButton lb = new LinkButton();
//            Literal l = new Literal();
//            string name = String.Empty;

//            for (int i = 0; i < gv.Controls.Count; i++)
//            {
//                if (gv.Controls[i].GetType() == typeof(LinkButton))
//                {
//                    l.Text = (gv.Controls[i] as LinkButton).Text;
//                    gv.Controls.Remove(gv.Controls[i]);
//                }
//                else if (gv.Controls[i].GetType() == typeof(DropDownList))
//                {
//                    l.Text = (gv.Controls[i] as DropDownList).SelectedValue.ToString();
//                    gv.Controls.Remove(gv.Controls[i]);
//                }
//                else if (gv.Controls[i].GetType() == typeof(TextBox))
//                {
//                    l.Text = (gv.Controls[i] as TextBox).Text;
//                    gv.Controls.Remove(gv.Controls[i]);
//                }
//                else if (gv.Controls[i].GetType() == typeof(CheckBox))
//                {
//                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
//                    gv.Controls.Remove(gv.Controls[i]);
//                }
//                if (gv.Controls[i].HasControls())
//                {
//                    PrepareGridViewForExport(gv.Controls[i]);
//                }

//            }


//        }

//        private void ExportGridView(string Filetype)
//        {
//            {
//                HtmlForm frm = new HtmlForm();
//                HtmlImage image = new HtmlImage();

//                frm.Controls.Clear();
//                frm.Attributes["runat"] = "server";
//                if (Filetype.ToLower() == "print")
//                {
//                    GridView_Print();
//                }
//                else if (Filetype.ToLower() == "excel")
//                {
//                    // gvCustomer.Columns.Remove(this.gvCustomer.Columns[0]);
//                    string temp = userVo.FirstName + userVo.LastName + "Customer.xls";
//                    string attachment = "attachment; filename=" + temp;
//                    Response.ClearContent();
//                    Response.AddHeader("content-disposition", attachment);
//                    Response.ContentType = "application/ms-excel";
//                    StringWriter sw = new StringWriter();
//                    HtmlTextWriter htw = new HtmlTextWriter(sw);
//                    Response.Output.Write("<table border=\"0\"><tbody><tr><td>");
//                    Response.Output.Write("Advisor Name : ");
//                    Response.Output.Write("</td>");
//                    Response.Output.Write("<td>");
//                    Response.Output.Write(userVo.FirstName + userVo.LastName);
//                    Response.Output.Write("</td></tr>");
//                    Response.Output.Write("<tr><td>");
//                    Response.Output.Write("Report  : ");
//                    Response.Output.Write("</td>");
//                    Response.Output.Write("<td>");
//                    Response.Output.Write("Customer List");
//                    Response.Output.Write("</td></tr><tr><td>");
//                    Response.Output.Write("Date : ");
//                    Response.Output.Write("</td><td>");
//                    System.DateTime tDate1 = System.DateTime.Now;
//                    Response.Output.Write(tDate1);
//                    Response.Output.Write("</td></tr>");
//                    Response.Output.Write("</tbody></table>");

//                    PrepareGridViewForExport(gvCustomers);

//                    if (gvCustomers.HeaderRow != null)
//                    {
//                        PrepareControlForExport(gvCustomers.HeaderRow);
//                        //tbl.Rows.Add(gvMFTransactions.HeaderRow);
//                    }
//                    foreach (GridViewRow row in gvCustomers.Rows)
//                    {

//                        PrepareControlForExport(row);

//                        //tbl.Rows.Add(row);
//                    }
//                    if (gvCustomers.FooterRow != null)
//                    {
//                        PrepareControlForExport(gvCustomers.FooterRow);
//                        // tbl.Rows.Add(gvMFTransactions.FooterRow);
//                    }

//                    gvCustomers.Parent.Controls.Add(frm);
//                    frm.Controls.Add(gvCustomers);
//                    frm.RenderControl(htw);
//                    HttpContext.Current.Response.Write(sw.ToString());
//                    HttpContext.Current.Response.End();
//                }


//                else if (Filetype.ToLower() == "pdf")
//                {
//                    string temp = userVo.FirstName + userVo.LastName + "_Customer List";

//                    gvCustomers.AllowPaging = false;
//                    gvCustomers.DataBind();
//                    iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gvCustomers.Columns.Count - 1);

//                    table.HeaderRows = 4;
//                    iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);
//                    Phrase phApplicationName = new Phrase("WWW.PrincipalConsulting.net", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
//                    PdfPCell clApplicationName = new PdfPCell(phApplicationName);
//                    clApplicationName.Border = PdfPCell.NO_BORDER;
//                    clApplicationName.HorizontalAlignment = Element.ALIGN_LEFT;


//                    Phrase phDate = new Phrase(DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
//                    PdfPCell clDate = new PdfPCell(phDate);
//                    clDate.HorizontalAlignment = Element.ALIGN_RIGHT;
//                    clDate.Border = PdfPCell.NO_BORDER;


//                    headerTable.AddCell(clApplicationName);
//                    headerTable.AddCell(clDate);
//                    headerTable.DefaultCell.Border = PdfPCell.NO_BORDER;

//                    PdfPCell cellHeader = new PdfPCell(headerTable);
//                    cellHeader.Border = PdfPCell.NO_BORDER;
//                    cellHeader.Colspan = gvCustomers.Columns.Count - 1;
//                    table.AddCell(cellHeader);

//                    Phrase phHeader = new Phrase(temp, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
//                    PdfPCell clHeader = new PdfPCell(phHeader);
//                    clHeader.Colspan = gvCustomers.Columns.Count - 1;
//                    clHeader.Border = PdfPCell.NO_BORDER;
//                    clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
//                    table.AddCell(clHeader);


//                    Phrase phSpace = new Phrase("\n");
//                    PdfPCell clSpace = new PdfPCell(phSpace);
//                    clSpace.Border = PdfPCell.NO_BORDER;
//                    clSpace.Colspan = gvCustomers.Columns.Count - 1;
//                    table.AddCell(clSpace);

//                    GridViewRow HeaderRow = gvCustomers.HeaderRow;
//                    if (HeaderRow != null)
//                    {
//                        string cellText = "";
//                        for (int j = 1; j < gvCustomers.Columns.Count; j++)
//                        {
//                            if (j == 1)
//                            {
//                                cellText = "Parent";
//                            }
//                            else if (j == 2)
//                            {
//                                cellText = "Customer Name / Company Name";
//                            }
//                            else if (j == 7)
//                            {
//                                cellText = "Area";
//                            }
//                            else if (j == 9)
//                            {
//                                cellText = "Pincode";
//                            }
//                            else if (j == 10)
//                            {
//                                cellText = "AssignedRM";
//                            }
//                            else
//                            {
//                                cellText = Server.HtmlDecode(gvCustomers.HeaderRow.Cells[j].Text);
//                            }

//                            Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD));
//                            table.AddCell(ph);
//                        }

//                    }

//                    for (int i = 0; i < gvCustomers.Rows.Count; i++)
//                    {
//                        string cellText = "";
//                        if (gvCustomers.Rows[i].RowType == DataControlRowType.DataRow)
//                        {
//                            for (int j = 1; j < gvCustomers.Columns.Count; j++)
//                            {
//                                if (j == 1)
//                                {
//                                    cellText = ((Label)gvCustomers.Rows[i].FindControl("lblParenteHeader")).Text;
//                                }
//                                else if (j == 2)
//                                {
//                                    cellText = ((Label)gvCustomers.Rows[i].FindControl("lblCustNameHeader")).Text;
//                                }
//                                else if (j == 7)
//                                {
//                                    cellText = ((Label)gvCustomers.Rows[i].FindControl("lblAreaHeader")).Text;
//                                }
//                                else if (j == 9)
//                                {
//                                    cellText = ((Label)gvCustomers.Rows[i].FindControl("lblPincodeHeader")).Text;
//                                }
//                                else if (j == 10)
//                                {
//                                    cellText = ((Label)gvCustomers.Rows[i].FindControl("lblAssignedRMHeader")).Text;
//                                }
//                                else
//                                {
//                                    cellText = Server.HtmlDecode(gvCustomers.Rows[i].Cells[j].Text);
//                                }
//                                Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL));
//                                iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
//                                table.AddCell(ph);

//                            }

//                        }

//                    }



//                    //Create the PDF Document

//                    Document pdfDoc = new Document(PageSize.A5, 10f, 10f, 10f, 0f);
//                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
//                    pdfDoc.Open();
//                    pdfDoc.Add(table);
//                    pdfDoc.Close();
//                    Response.ContentType = "application/pdf";
//                    temp = "filename=" + temp + ".pdf";
//                    //    Response.AddHeader("content-disposition", "attachment;" + "filename=GridViewExport.pdf");
//                    Response.AddHeader("content-disposition", "attachment;" + temp);
//                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
//                    Response.Write(pdfDoc);
//                    Response.End();



//                }
//                else if (Filetype.ToLower() == "word")
//                {
//                    gvCustomers.Columns.Remove(this.gvCustomers.Columns[0]);
//                    string temp = userVo.FirstName + userVo.LastName + "_Customer.doc";
//                    string attachment = "attachment; filename=" + temp;
//                    Response.ClearContent();
//                    Response.AddHeader("content-disposition", attachment);
//                    Response.ContentType = "application/msword";
//                    StringWriter sw = new StringWriter();
//                    HtmlTextWriter htw = new HtmlTextWriter(sw);

//                    Response.Output.Write("<table border=\"0\"><tbody><tr><td>");
//                    Response.Output.Write("Advisor Name : ");
//                    Response.Output.Write("</td>");
//                    Response.Output.Write("<td>");
//                    Response.Output.Write(userVo.FirstName + userVo.LastName);
//                    Response.Output.Write("</td></tr>");
//                    Response.Output.Write("<tr><td>");
//                    Response.Output.Write("Report  : ");
//                    Response.Output.Write("</td>");
//                    Response.Output.Write("<td>");
//                    Response.Output.Write("Customer List");
//                    Response.Output.Write("</td></tr><tr><td>");
//                    Response.Output.Write("Date : ");
//                    Response.Output.Write("</td><td>");
//                    System.DateTime tDate1 = System.DateTime.Now;
//                    Response.Output.Write(tDate1);
//                    Response.Output.Write("</td></tr>");
//                    Response.Output.Write("</tbody></table>");

//                    PrepareGridViewForExport(gvCustomers);


//                    if (gvCustomers.HeaderRow != null)
//                    {
//                        PrepareControlForExport(gvCustomers.HeaderRow);
//                    }
//                    foreach (GridViewRow row in gvCustomers.Rows)
//                    {
//                        PrepareControlForExport(row);
//                    }
//                    if (gvCustomers.FooterRow != null)
//                    {
//                        PrepareControlForExport(gvCustomers.FooterRow);
//                    }
//                    gvCustomers.Parent.Controls.Add(frm);
//                    frm.Controls.Add(gvCustomers);
//                    frm.RenderControl(htw);
//                    Response.Write(sw.ToString());
//                    Response.End();

//                }

//            }

//        }

//        private void ShowPdf(string strS)
//        {
//            Response.ClearContent();
//            Response.ClearHeaders();
//            Response.ContentType = "application/pdf";
//            Response.AddHeader
//            ("Content-Disposition", "attachment; filename=" + strS);
//            Response.TransmitFile(strS);
//            Response.End();
//            Response.Flush();
//            Response.Clear();

//        }

//        private TextBox GetCustNameTextBox()
//        {
//            TextBox txt = new TextBox();
//            if (gvCustomers.HeaderRow != null)
//            {
//                if ((TextBox)gvCustomers.HeaderRow.FindControl("txtCustNameSearch") != null)
//                {
//                    txt = (TextBox)gvCustomers.HeaderRow.FindControl("txtCustNameSearch");
//                }
//            }
//            else
//                txt = null;

//            return txt;
//        }

//        private TextBox GetAreaTextBox()
//        {
//            TextBox txt = new TextBox();
//            if (gvCustomers.HeaderRow != null)
//            {
//                if ((TextBox)gvCustomers.HeaderRow.FindControl("txtAreaSearch") != null)
//                {
//                    txt = (TextBox)gvCustomers.HeaderRow.FindControl("txtAreaSearch");
//                }
//            }
//            else
//                txt = null;

//            return txt;
//        }

//        private TextBox GetPincodeTextBox()
//        {
//            TextBox txt = new TextBox();
//            if (gvCustomers.HeaderRow != null)
//            {
//                if ((TextBox)gvCustomers.HeaderRow.FindControl("txtPincodeSearch") != null)
//                {
//                    txt = (TextBox)gvCustomers.HeaderRow.FindControl("txtPincodeSearch");
//                }
//            }
//            else
//                txt = null;

//            return txt;
//        }

//        private TextBox GetPANTextBox()
//        {
//            TextBox txt = new TextBox();
//            if (gvCustomers.HeaderRow != null)
//            {
//                if ((TextBox)gvCustomers.HeaderRow.FindControl("txtPAN") != null)
//                {
//                    txt = (TextBox)gvCustomers.HeaderRow.FindControl("txtPAN");
//                }
//            }
//            else
//                txt = null;

//            return txt;
//        }



//        private DropDownList GetParentDDL()
//        {
//            DropDownList ddl = new DropDownList();
//            if (gvCustomers.HeaderRow != null)
//            {
//                if ((DropDownList)gvCustomers.HeaderRow.FindControl("ddlParent") != null)
//                {
//                    ddl = (DropDownList)gvCustomers.HeaderRow.FindControl("ddlParent");
//                }
//            }
//            else
//                ddl = null;

//            return ddl;
//        }
//        private DropDownList GetActiveDDL()
//        {
//            DropDownList ddl = new DropDownList();
//            if (gvCustomers.HeaderRow != null)
//            {
//                if ((DropDownList)gvCustomers.HeaderRow.FindControl("ddlActiveFilter") != null)
//                {
//                    ddl = (DropDownList)gvCustomers.HeaderRow.FindControl("ddlActiveFilter");
//                }
//            }
//            else
//                ddl = null;

//            return ddl;
//        }

//        private DropDownList GetRMDDL()
//        {
//            DropDownList ddl = new DropDownList();
//            if (gvCustomers.HeaderRow != null)
//            {
//                if ((DropDownList)gvCustomers.HeaderRow.FindControl("ddlAssignedRM") != null)
//                {
//                    ddl = (DropDownList)gvCustomers.HeaderRow.FindControl("ddlAssignedRM");
//                }
//            }
//            else
//                ddl = null;

//            return ddl;
//        }

//        protected void btnPincodeSearch_Click(object sender, EventArgs e)
//        {
//            TextBox txtPincode = GetPincodeTextBox();
//            hdnCurrentPage.Value = "1";
//            if (txtPincode != null)
//            {
//                hdnPincodeFilter.Value = txtPincode.Text.Trim();
//                if (Session["Customer"].ToString() == "Customer")
//                {
//                    this.BindGrid(mypager.CurrentPage, 0);
//                }
//                else
//                {
//                    this.BindCustomer(mypager.CurrentPage);
//                }
//            }
//        }
//        protected void btnPANSearch_Click(object sender, EventArgs e)
//        {
//            TextBox txtPAN = GetPANTextBox();
//            hdnCurrentPage.Value = "1";
//            if (txtPAN != null)
//            {
//                hndPAN.Value = txtPAN.Text.Trim();
//                if (Session["Customer"].ToString() == "Customer")
//                {
//                    this.BindGrid(mypager.CurrentPage, 0);
//                }
//                else
//                {
//                    this.BindCustomer(mypager.CurrentPage);
//                }
//            }
//        }
//        protected void btnAreaSearch_Click(object sender, EventArgs e)
//        {
//            TextBox txtArea = GetAreaTextBox();
//            hdnCurrentPage.Value = "1";
//            if (txtArea != null)
//            {
//                hdnAreaFilter.Value = txtArea.Text.Trim();
//                if (Session["Customer"].ToString() == "Customer")
//                {
//                    this.BindGrid(mypager.CurrentPage, 0);
//                }
//                else
//                {
//                    this.BindCustomer(mypager.CurrentPage);
//                }
//            }
//        }

//        protected void btnNameSearch_Click(object sender, EventArgs e)
//        {
//            TextBox txtName = GetCustNameTextBox();
//            hdnCurrentPage.Value = "1";
//            if (txtName != null)
//            {
//                hdnNameFilter.Value = txtName.Text.Trim();
//                this.BindGrid(mypager.CurrentPage, 0);
//            }
//        }

//        protected void ddlAssignedRM_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            DropDownList ddlRM = GetRMDDL();

//            if (ddlRM != null)
//            {
//                if (ddlRM.SelectedIndex != 0)
//                {   // Bind the Grid with Only Selected Values
//                    //hdnRMFilter.Value = ddlRM.SelectedValue;
//                    hdnRMFilter.Value = ddlRM.SelectedItem.Value;
//                    hdnCurrentPage.Value = "";
//                }
//                else
//                {   // Bind the Grid with Only All Values
//                    hdnRMFilter.Value = "";
//                }

//                if (Session["Customer"].ToString() == "Customer")
//                {
//                    this.BindGrid(mypager.CurrentPage, 0);
//                }
//                else
//                {
//                    this.BindCustomer(mypager.CurrentPage);
//                }
//            }
//        }

//        protected void ddlActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            DropDownList ddlFilter = (DropDownList)gvCustomers.HeaderRow.FindControl("ddlActiveFilter");

//            hdnactive.Value = ddlFilter.SelectedValue;

//            //if (int.Parse(ddlFilter.SelectedValue) == 0)
//            //{
//            //    hdnactive.Value = "I";
//            //}
//            //if (int.Parse(ddlFilter.SelectedValue) == 2)
//            //{
//            //    hdnactive.Value = "D";
//            //}
//            this.BindGrid(mypager.CurrentPage, 0);
//        }

//        protected void ddlIsProspect_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            DropDownList ddlIsIsProspectFilter = (DropDownList)gvCustomers.HeaderRow.FindControl("ddlIsProspect");

//            hdnIsProspect.Value = ddlIsIsProspectFilter.SelectedValue;

//            this.BindGrid(mypager.CurrentPage, 0);
//        }

//        protected void SetValue(object sender, EventArgs e)
//        {
//            DropDownList ddl = (DropDownList)sender;
//            if (hdnIsProspect.Value.ToString() == "")
//            {
//                ddl.SelectedValue = "2";
//            }
//            else
//                ddl.SelectedValue = hdnIsProspect.Value.ToString();


//        }


//        protected void ddlParent_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            DropDownList ddlParent = GetParentDDL();

//            if (ddlParent != null)
//            {
//                if (ddlParent.SelectedIndex != 0)
//                {   // Bind the Grid with Only Selected Values
//                    hdnParentFilter.Value = ddlParent.SelectedValue;
//                }
//                else
//                {   // Bind the Grid with Only All Values
//                    hdnParentFilter.Value = "";
//                }

//                if (Session["Customer"].ToString() == "Customer")
//                {
//                    this.BindGrid(mypager.CurrentPage, 0);
//                }
//                else
//                {
//                    this.BindCustomer(mypager.CurrentPage);
//                }
//            }
//        }
//        /*
//        protected void chkReassignRM_CheckedChanged(object sender, EventArgs e)
//        {
//            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
//            DataTable ds = new DataTable();

//            if (((CheckBox)gvCustomers.HeaderRow.FindControl("chkReassignRM")).Checked)
//            {
//                hdnReassignRM.Value = "1";
//            }
//            else
//            {
//                hdnReassignRM.Value = "";
//                BindGrid(mypager.CurrentPage, 0);
//            }
//            if (genDictReassignRM != null)
//            {
//                ReAssignRMControl(genDictReassignRM);
//            }
//            else
//            {
//                ds=advisorStaffBo.GetAdviserRM(((AdvisorVo)Session["advisorVo"]).advisorId);
//                genDictReassignRM=new Dictionary<string,string>();
//                foreach (DataRow dr in ds.Rows)
//                {
//                    if (dr["RMName"].ToString().Trim() != "")
//                    {
//                        genDictReassignRM.Add(dr["AR_RMId"].ToString(), dr["RMName"].ToString());
//                    }
//                }
//                ReAssignRMControl(genDictReassignRM);
//            }

//        }
//        */
//        /*
//        protected void btnSave_Click(object sender, EventArgs e)
//        {
//            int[] ParentIds;
//            int[] rmIds;
//            DropDownList ddl1 = new DropDownList();
//            string tempParentId = "";

//            if (((CheckBox)gvCustomers.HeaderRow.FindControl("chkReassignRM")).Checked)
//            {
//                ParentIds = new int[gvCustomers.Rows.Count];
//                rmIds = new int[gvCustomers.Rows.Count];

//                //  foreach (DataKey key in gvCustomers.DataKeys)
//                foreach (GridViewRow gvr in gvCustomers.Rows)
//                {
//                    tempParentId = gvCustomers.DataKeys[gvr.RowIndex].Values[0].ToString();

//                    ParentIds[gvr.RowIndex] = int.Parse(tempParentId);

//                    if ((DropDownList)gvr.FindControl("ddlReassignRM") != null)
//                    {
//                        ddl1 = (DropDownList)gvr.FindControl("ddlReassignRM");
//                        rmIds[gvr.RowIndex] = int.Parse(ddl1.SelectedValue.ToString());

//                    }

//                }
//                if (ParentIds.Count() == rmIds.Count())
//                {
//                    customerBo.UpdateCustomerAssignedRM(ParentIds, rmIds);
//                }
//            }


//        }
//        */
//        protected void btnExportExcel_Click(object sender, EventArgs e)
//        {
//            gvCustomers.Columns[0].Visible = false;
//            gvCustomers.HeaderRow.Visible = true;

//            if (hdnDownloadPageType.Value.ToString() == "single")
//            {
//                BindGrid(mypager.CurrentPage, 0);
//            }
//            else
//            {
//                BindGrid(mypager.CurrentPage, 1);
//                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "AferExportAll('ctrl_AdviserCustomer_btnPrintGrid');", true);
//            }

//            ExportGridView(hdnDownloadFormat.Value.ToString());
//            //
//            BindGrid(mypager.CurrentPage, 0);
//            gvCustomers.Columns[0].Visible = true;
//        }

//        //protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
//        //{
//        //    trPageChoice.Visible = true;
//        //    ExportFormat = "Excel";
//        //}



//        protected void btnPrintGrid_Click(object sender, EventArgs e)
//        {
//            BindGrid(mypager.CurrentPage, 0);
//            gvCustomers.Columns[0].Visible = true;


//            // GridView_Print();
//        }
//        //protected void rbtnSingle_CheckedChanged(object sender, EventArgs e)
//        //{
//        //    gvCustomers.Columns[0].Visible = false;
//        //    gvCustomers.HeaderRow.Visible = true;
//        //    rbtnSingle.Checked = false;
//        //    BindGrid(mypager.CurrentPage, 0);
//        //    PrepareGridViewForExport(gvCustomers);
//        //    ExportGridView(hdnDownloadType.Value.ToString());
//        //    BindGrid(mypager.CurrentPage, 0);
//        //    gvCustomers.Columns[0].Visible = true;
//        //}

//        //protected void rbtnMultiple_CheckedChanged(object sender, EventArgs e)
//        //{
//        //    gvCustomers.Columns[0].Visible = false;
//        //    gvCustomers.HeaderRow.Visible = true;
//        //    BindGrid(mypager.CurrentPage, 1);
//        //    PrepareGridViewForExport(gvCustomers);
//        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "AferExportAll('ctrl_AdviserCustomer_btnPrintGrid');", true);
//        //    ExportGridView(hdnDownloadType.Value.ToString());
//        //    BindGrid(mypager.CurrentPage, 0);
//        //    gvCustomers.Columns[0].Visible = true;
//        //}

//        protected void btnPrint_Click(object sender, EventArgs e)
//        {
//            GridView_Print();

//        }

//        private void GridView_Print()
//        {
//            gvCustomers.Columns[0].Visible = false;
//            if (hdnDownloadPageType.Value.ToString() == "single")
//            {
//                BindGrid(mypager.CurrentPage, 0);
//            }
//            else
//            {
//                BindGrid(mypager.CurrentPage, 1);
//            }

//            if (gvCustomers.HeaderRow != null)
//            {
//                PrepareGridViewForExport(gvCustomers.HeaderRow);
//            }
//            foreach (GridViewRow row in gvCustomers.Rows)
//            {
//                PrepareGridViewForExport(row);
//            }
//            if (gvCustomers.FooterRow != null)
//            {
//                PrepareGridViewForExport(gvCustomers.FooterRow);
//            }

//            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_AdviserCustomer_tbl','ctrl_AdviserCustomer_btnPrintGrid');", true);

//        }

//        protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
//        {
//            ModalPopupExtender1.TargetControlID = "imgBtnExport";
//            ModalPopupExtender1.Show();
//        }

//        protected void imgBtnWord_Click(object sender, ImageClickEventArgs e)
//        {
//            ModalPopupExtender1.TargetControlID = "imgBtnWord";
//            ModalPopupExtender1.Show();

//        }

//        protected void imgBtnPdf_Click(object sender, ImageClickEventArgs e)
//        {
//            ModalPopupExtender1.TargetControlID = "imgBtnPdf";
//            ModalPopupExtender1.Show();
//        }

//        protected void imgBtnPrint_Click(object sender, ImageClickEventArgs e)
//        {
//            if (hdnDownloadPageType.Value.ToString() == "multiple")
//            {
//                BindGrid(mypager.CurrentPage, 1);
//                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "AferExportAll('ctrl_RMCustomer_btnPrintGrid');", true);
//            }
//            ModalPopupExtender1.TargetControlID = "imgBtnPrint";
//            ModalPopupExtender1.Show();

//        }

//        protected void hiddenassociation_Click(object sender, EventArgs e)
//        {
//            string val = Convert.ToString(hdnMsgValue.Value);
//            if (val == "1")
//            {
//                ParentId = int.Parse(Session["ParentIdForDelete"].ToString());
//                hdnassociationcount.Value = customerBo.GetAssociationCount("C", ParentId).ToString();
//                string asc = Convert.ToString(hdnassociationcount.Value);

//                if (asc == "0")

//                    DeleteCustomerProfile();


//                else

//                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showassocation();", true);
//            }
//        }

//        private void DeleteCustomerProfile()
//        {
//            try
//            {
//                customerVo = (CustomerVo)Session["CustomerVo"];
//                userVo = (UserVo)Session[SessionContents.UserVo];


//                if (customerBo.DeleteCustomer(customerVo.ParentId, "D"))
//                {
//                    string DeleteStatus = "Customer Deleted Successfully";
//                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','CustomerDeleteStatus=" + DeleteStatus + "');", true);
//                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AdviserCustomer','login');", true);
//                }

//            }
//            catch (BaseApplicationException Ex)
//            {
//                throw Ex;
//            }

//            catch (Exception Ex)
//            {
//                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//                NameValueCollection FunctionInfo = new NameValueCollection();
//                FunctionInfo.Add("Method", "ViewCustomerIndividualProfile.ascx:btnDelete_Click()");
//                object[] objects = new object[3];
//                objects[0] = customerVo;
//                //objects[1] = userVo;
//                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//                exBase.AdditionalInformation = FunctionInfo;
//                ExceptionManager.Publish(exBase);
//                throw exBase;

//            }
//        }


//    }
//}
#endregion