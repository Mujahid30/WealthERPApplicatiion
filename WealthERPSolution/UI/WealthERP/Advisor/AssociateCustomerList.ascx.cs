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

namespace WealthERP.Advisor
{
    public partial class AssociateCustomerList : System.Web.UI.UserControl
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

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            advisorPrefernceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];
            //CreationSuccessMessage.Visible = false;
            rmVo = (RMVo)Session["rmVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            associatesVo = (AssociatesVO)Session["associatesVo"];

            string userType = userVo.UserType;
            string currUserRole = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            if (userVo.UserType == "SuperAdmin") {
                this.UserRole = "advisor";
                adviserId = 1000;
                if (ddlAdviser.SelectedValue != "Select" && ddlAdviser.SelectedValue != "")
                    adviserId = int.Parse(ddlAdviser.SelectedValue);
            }
            else {
                
                switch (currUserRole) {
                    case "admin":
                    case "ops":
                    case "research":
                        this.UserRole = "advisor";
                        break;
                    case "rm":
                    case "bm":
                    case "associates":
                        this.UserRole = currUserRole;
                        break;
                }
                rmId = rmVo.RMId;
                branchHeadId = rmVo.RMId;
                adviserId = adviserVo.advisorId;

                //GUI 
                tdLblAdviser.Visible = false;
                tdDdlAdviser.Visible = false;
                ddlAdviser.Visible = false;
            }

            if (!IsPostBack) {
                BindCustomerGrid();
                if (userVo.UserType == "SuperAdmin") {
                    //BindAdviserDropDownList();
                    gvAssocCustList.Visible = false;
                }
            }
        }

        /// <summary>
        ///  Binding Customer List at diffrent user role
        /// </summary>

        protected void BindCustomerGrid()
        {
            AdvisorBo adviserBo = new AdvisorBo();
            List<CustomerVo> custList = new List<CustomerVo>();
            RMVo customerRMVo = new RMVo();
            try
            {
                custList = adviserBo.GetAssociateCustomerList(adviserId, rmId, AgentId, UserRole, branchHeadId, out genDictParent, out genDictRM, out genDictReassignRM);
                //foreach(cust in custList){
                //} 
                //if (custList == null)
                //{
                //    DivCustomerList.Visible = false;
                //    gvAssocCustList.Visible = false;
                //    imgexportButton.Visible = false;

                //}
                //else
                //{
                //    //DataTable dtCustomerList = CreateCustomeListTable(UserRole);
                //    //HideCustomerGridColumn(UserRole);
                //    DataRow drCustomer;
                //    for (int i = 0; i < custList.Count; i++) {
                //        drCustomer = dtCustomerList.NewRow();
                //        customerVo = new CustomerVo();
                //        customerVo = custList[i];
                //        drCustomer["CustomerId"] = customerVo.CustomerId.ToString();
                //        drCustomer["ParentId"] = customerVo.ParentId.ToString();
                //        if (customerVo.ProcessId == 0)
                //        {
                //            drCustomer["ADUL_ProcessId"] = "N/A";
                //        }
                //        else
                //            drCustomer["ADUL_ProcessId"] = customerVo.ProcessId.ToString();
                //        if (customerVo.ACC_CustomerCategoryName == null)
                //        {
                //            drCustomer["ACC_CustomerCategoryName"] = "N/A";
                //        }
                //        else
                //            drCustomer["ACC_CustomerCategoryName"] = customerVo.ACC_CustomerCategoryName.ToString();
                //        drCustomer["UserId"] = customerVo.UserId.ToString();
                //        drCustomer["RMId"] = customerVo.RmId.ToString();
                //        if (customerVo.ParentCustomer != null)
                //        {
                //            drCustomer["Group"] = customerVo.ParentCustomer.ToString();
                //        }
                //        drCustomer["Cust_Comp_Name"] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                //        if (customerVo.PANNum != null)
                //            drCustomer["PANNumber"] = customerVo.PANNum.ToString();
                //        else
                //            drCustomer["PANNumber"] = "";
                //        drCustomer["MobileNumber"] = customerVo.Mobile1.ToString();
                //        drCustomer["PhoneNumber"] = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                //        drCustomer["Email"] = customerVo.Email.ToString();
                //        if (customerVo.Adr1Line1 == null)
                //            customerVo.Adr1Line1 = "";
                //        if (customerVo.Adr1Line2 == null)
                //            customerVo.Adr1Line2 = "";
                //        if (customerVo.Adr1Line3 == null)
                //            customerVo.Adr1Line3 = "";
                //        if (customerVo.Adr1City == null)
                //            customerVo.Adr1City = "";
                //        if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() == "")
                //            drCustomer["Address"] = "-";
                //        else if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() != "")
                //            drCustomer["Address"] = customerVo.Adr1Line2.ToString();
                //        else if (customerVo.Adr1Line1.ToString() != "" && customerVo.Adr1Line2.ToString() == "")
                //            drCustomer["Address"] = customerVo.Adr1Line1.ToString();
                //        else
                //            drCustomer["Address"] = customerVo.Adr1Line1.ToString() + "," + customerVo.Adr1Line2.ToString();
                //        drCustomer["Area"] = customerVo.Adr1Line3.ToString();
                //        drCustomer["City"] = customerVo.Adr1City.ToString();
                //        drCustomer["Pincode"] = customerVo.Adr1PinCode.ToString();
                //        if (UserRole != "rm")
                //        {
                //            if (customerVo.AssignedRM != null)
                //                drCustomer["AssignedRM"] = customerVo.AssignedRM.ToString();
                //            else
                //                drCustomer["AssignedRM"] = "";
                //        }

                //        if (customerVo.IsActive == 1)
                //        {
                //            drCustomer["IsActive"] = "Active";
                //        }
                //        else
                //        {
                //            drCustomer["IsActive"] = "In Active";

                //        }
                //        if (customerVo.IsProspect == 1)
                //        {
                //            drCustomer["IsProspect"] = "Yes";
                //        }
                //        else
                //        {
                //            drCustomer["IsProspect"] = "No";
                //        }
                //        if (customerVo.IsFPClient == 1)
                //        {
                //            drCustomer["IsFPClient"] = "Yes";
                //        }
                //        else
                //        {
                //            drCustomer["IsFPClient"] = "No";
                //        }
                //        if (UserRole != "rm")
                //        {
                //            drCustomer["BranchName"] = customerVo.BranchName;
                //        }
                //        dtCustomerList.Rows.Add(drCustomer);

                //    }
                //    if (Cache["custList+UserRole" + adviserVo.advisorId + UserRole] == null)
                //    {
                //        Cache.Insert("custList+UserRole" + adviserVo.advisorId + UserRole, dtCustomerList);
                //    }
                //    else
                //    {
                //        Cache.Remove("custList+UserRole" + adviserVo.advisorId + UserRole);
                //        Cache.Insert("custList+UserRole" + adviserVo.advisorId + UserRole, dtCustomerList);
                //    }
                //    gvAssocCustList.DataSource = dtCustomerList;
                //    if (advisorPrefernceVo != null)
                //    {
                //        gvAssocCustList.PageSize = advisorPrefernceVo.GridPageSize;
                //    }
                //    else
                //    { gvAssocCustList.PageSize = 40; }
                //    gvAssocCustList.DataBind();
                //    DivCustomerList.Visible = true;
                //    gvAssocCustList.Visible = true;
                //    // pnlCustomerList.Visible = true;
                //    imgexportButton.Visible = true;
                //    //ErrorMessage.Visible = false;
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
                FunctionInfo.Add("Method", "AssociateCustomerList.ascx.cs:BindCustomerGrid()");
                object[] objects = new object[4];
                objects[0] = user;
                objects[1] = rmId;
                objects[2] = customerVo;
                objects[3] = custList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /// <summary>
        /// Binding Customer List when find customer
        /// </summary>

        //protected void BindGrid()
        //{
        //    AdvisorBo adviserBo = new AdvisorBo();
        //    List<CustomerVo> custList = new List<CustomerVo>();
        //    RMVo customerRMVo = new RMVo();
        //    try
        //    {
        //        if (Session["Customer"] != null)
        //        {
        //            customer = Session["Customer"].ToString();
        //            if (customer.ToLower().Trim() == "find customer" || customer.ToLower().Trim() == "")
        //                customer = string.Empty;
        //        }
        //        custList = adviserBo.GetAssociateCustomerList(adviserVo.advisorId, rmId, AgentId, UserRole, branchHeadId, out genDictParent, out genDictRM, out genDictReassignRM);

        //        if (custList == null)
        //        {
        //            DivCustomerList.Visible = false;
        //            gvAssocCustList.Visible = false;
        //            // pnlCustomerList.Visible = true; 
        //            imgexportButton.Visible = false;
        //            ErrorMessage.Visible = true;

        //        }
        //        else
        //        {
        //            DataTable dtCustomerList = CreateCustomeListTable(UserRole);
        //            HideCustomerGridColumn(UserRole);
        //            DataRow drCustomer;
        //            for (int i = 0; i < custList.Count; i++)
        //            {
        //                drCustomer = dtCustomerList.NewRow();
        //                customerVo = new CustomerVo();
        //                customerVo = custList[i];
        //                drCustomer["CustomerId"] = customerVo.CustomerId.ToString();
        //                drCustomer["ParentId"] = customerVo.ParentId.ToString();
        //                if (customerVo.ProcessId == 0)
        //                {
        //                    drCustomer["ADUL_ProcessId"] = "N/A";
        //                }
        //                else
        //                    drCustomer["ADUL_ProcessId"] = customerVo.ProcessId.ToString();
        //                if (customerVo.ACC_CustomerCategoryName == null)
        //                {
        //                    drCustomer["ACC_CustomerCategoryName"] = "N/A";
        //                }
        //                else
        //                    drCustomer["ACC_CustomerCategoryName"] = customerVo.ACC_CustomerCategoryName.ToString();
        //                drCustomer["UserId"] = customerVo.UserId.ToString();
        //                drCustomer["RMId"] = customerVo.RmId.ToString();
        //                if (customerVo.ParentCustomer != null)
        //                {
        //                    drCustomer["Group"] = customerVo.ParentCustomer.ToString();
        //                }
        //                drCustomer["Cust_Comp_Name"] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
        //                if (customerVo.PANNum != null)
        //                    drCustomer["PANNumber"] = customerVo.PANNum.ToString();
        //                else
        //                    drCustomer["PANNumber"] = "";
        //                drCustomer["MobileNumber"] = customerVo.Mobile1.ToString();
        //                drCustomer["PhoneNumber"] = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
        //                drCustomer["Email"] = customerVo.Email.ToString();
        //                if (customerVo.Adr1Line1 == null)
        //                    customerVo.Adr1Line1 = "";
        //                if (customerVo.Adr1Line2 == null)
        //                    customerVo.Adr1Line2 = "";
        //                if (customerVo.Adr1Line3 == null)
        //                    customerVo.Adr1Line3 = "";
        //                if (customerVo.Adr1City == null)
        //                    customerVo.Adr1City = "";
        //                if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() == "")
        //                    drCustomer["Address"] = "-";
        //                else if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() != "")
        //                    drCustomer["Address"] = customerVo.Adr1Line2.ToString();
        //                else if (customerVo.Adr1Line1.ToString() != "" && customerVo.Adr1Line2.ToString() == "")
        //                    drCustomer["Address"] = customerVo.Adr1Line1.ToString();
        //                else
        //                    drCustomer["Address"] = customerVo.Adr1Line1.ToString() + "," + customerVo.Adr1Line2.ToString();
        //                drCustomer["Area"] = customerVo.Adr1Line3.ToString();
        //                drCustomer["City"] = customerVo.Adr1City.ToString();
        //                drCustomer["Pincode"] = customerVo.Adr1PinCode.ToString();
        //                if (UserRole != "rm")
        //                {
        //                    if (customerVo.AssignedRM != null)
        //                        drCustomer["AssignedRM"] = customerVo.AssignedRM.ToString();
        //                    else
        //                        drCustomer["AssignedRM"] = "";
        //                }

        //                if (customerVo.IsActive == 1)
        //                {
        //                    drCustomer["IsActive"] = "Active";
        //                }
        //                else
        //                {
        //                    drCustomer["IsActive"] = "In Active";

        //                }
        //                if (customerVo.IsProspect == 1)
        //                {
        //                    drCustomer["IsProspect"] = "Yes";
        //                }
        //                else
        //                {
        //                    drCustomer["IsProspect"] = "No";
        //                }
        //                if (customerVo.IsFPClient == 1)
        //                {
        //                    drCustomer["IsFPClient"] = "Yes";
        //                }
        //                else
        //                {
        //                    drCustomer["IsFPClient"] = "No";
        //                }
        //                if (UserRole != "rm")
        //                {
        //                    drCustomer["BranchName"] = customerVo.BranchName;
        //                }
        //                dtCustomerList.Rows.Add(drCustomer);

        //            }
        //            if (Cache["custList+UserRole" + adviserVo.advisorId + UserRole] == null)
        //            {
        //                Cache.Insert("custList+UserRole" + adviserVo.advisorId + UserRole, dtCustomerList);
        //            }
        //            else
        //            {
        //                Cache.Remove("custList+UserRole" + adviserVo.advisorId + UserRole);
        //                Cache.Insert("custList+UserRole" + adviserVo.advisorId + UserRole, dtCustomerList);
        //            }

        //            gvAssocCustList.DataSource = dtCustomerList;
        //            if (advisorPrefernceVo != null)
        //            {
        //                gvAssocCustList.PageSize = advisorPrefernceVo.GridPageSize;
        //            }
        //            else
        //            { gvAssocCustList.PageSize = 40; }
        //            gvAssocCustList.DataBind();
        //            DivCustomerList.Visible = true;
        //            gvAssocCustList.Visible = true;
        //            //pnlCustomerList.Style.Add("Height", "410px");
        //            //  pnlCustomerList.Visible = true;                    
        //            imgexportButton.Visible = true;
        //            ErrorMessage.Visible = false;
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
        //        FunctionInfo.Add("Method", "AssociateCustomerList.ascx.cs:BindGrid()");
        //        object[] objects = new object[4];
        //        objects[0] = user;
        //        objects[1] = rmVo;
        //        objects[2] = customerVo;
        //        objects[3] = custList;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}


        protected void gvAssocCustList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridFilteringItem && e.Item.ItemIndex == -1)
            //{
            //    GridFilteringItem filterItem = (GridFilteringItem)e.Item;
            //    #region Bind group drop down
            //    // RadComboBox RadComboGroup = (RadComboBox)filterItem.FindControl("RadComboGroup");               
            //    //RadComboGroup.DataSource = genDictParent;
            //    //RadComboGroup.DataTextField = "Value";
            //    //RadComboGroup.DataValueField = "Key";
            //    //RadComboGroup.DataBind();
            //    #endregion
            //    RadComboBox RadComboRM = (RadComboBox)filterItem.FindControl("RadComboRM");

            //    DataView view = new DataView();
            //    RadComboRM.DataSource = genDictRM;
            //    RadComboRM.DataTextField = "Value";
            //    RadComboRM.DataValueField = "Key";
            //    RadComboRM.DataBind();
            //}
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem item = (e.Item as GridDataItem);
            //    // RadComboBox rcb = new RadComboBox();
            //    DropDownList rcb = new DropDownList();
            //    if (UserRole != "advisor")
            //    {
            //        rcb = (DropDownList)e.Item.FindControl("ddlAction");
            //        if (rcb != null)
            //        {
            //            // rcb.Items.FindItemByValue("Delete Profile").Remove();
            //            rcb.Items.FindByText("Delete Profile").Enabled = false;
            //        }
            //    }

            //}
        }

        protected void gvAssocCustList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataView dvcustomerList = new DataView();
            string prospectType = string.Empty;
            string statustype = string.Empty;
            string parentType = string.Empty;
            string rmType = string.Empty;
            DataTable dtCustomer = new DataTable();
            dtCustomer = (DataTable)Cache["custList+UserRole" + adviserVo.advisorId + UserRole];

            if (dtCustomer != null)
            {

                if (ViewState["IsActive"] != null)
                    statustype = ViewState["IsActive"].ToString();
                if (ViewState["IsProspect"] != null)
                    prospectType = ViewState["IsProspect"].ToString();
                #region dependency code of group filter and other filter but not use
                //if (ViewState["ParentId"] != null)
                //    parentType = ViewState["ParentId"].ToString();
                #endregion
                if (ViewState["RMId"] != null)
                    rmType = ViewState["RMId"].ToString();

                if (UserRole != "rm")
                {
                    if ((!string.IsNullOrEmpty(statustype)) && (string.IsNullOrEmpty(prospectType)) && (string.IsNullOrEmpty(rmType)))
                    {
                        dvcustomerList = new DataView(dtCustomer, "IsActive = '" + statustype + "'", "CustomerId,ACC_CustomerCategoryName,Cust_Comp_Name,ParentId,PANNumber,BranchName,RMId,Area,Pincode,City,ADUL_ProcessId,IsProspect", DataViewRowState.CurrentRows);
                        gvAssocCustList.DataSource = dvcustomerList.ToTable();
                        //if (dvcustomerList.Count > 10)
                        //    pnlCustomerList.Style.Add("Height", "410px");
                        //else
                        //    pnlCustomerList.Style.Remove("Height");

                    }
                    else if ((string.IsNullOrEmpty(statustype)) && (!string.IsNullOrEmpty(prospectType)) && (string.IsNullOrEmpty(rmType)))
                    {
                        dvcustomerList = new DataView(dtCustomer, "IsProspect = '" + prospectType + "'", "CustomerId,ACC_CustomerCategoryName,Cust_Comp_Name,ParentId,PANNumber,BranchName,RMId,Area,Pincode,City,ADUL_ProcessId,IsActive", DataViewRowState.CurrentRows);
                        gvAssocCustList.DataSource = dvcustomerList.ToTable();
                        //if (dvcustomerList.Count > 10)
                        //    pnlCustomerList.Style.Add("Height", "410px");
                        //else
                        //    pnlCustomerList.Style.Remove("Height");

                    }
                    #region dependency code of group filter and other filter but not use
                    //else if ((string.IsNullOrEmpty(statustype)) && (string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(parentType)) && (string.IsNullOrEmpty(rmType)))
                    //{
                    //    dvcustomerList = new DataView(dtCustomer, "ParentId = '" + parentType + "'", "CustomerId,Cust_Comp_Name,PANNumber,BranchName,RMId,Area,Pincode,City,ADUL_ProcessId,IsProspect,IsActive", DataViewRowState.CurrentRows);
                    //    gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    //}
                    #endregion
                    else if ((string.IsNullOrEmpty(statustype)) && (string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(rmType)))
                    {
                        dvcustomerList = new DataView(dtCustomer, "RMId = '" + rmType + "'", "CustomerId,ACC_CustomerCategoryName,Cust_Comp_Name,ParentId,PANNumber,BranchName,Area,Pincode,City,ADUL_ProcessId,IsProspect,IsActive", DataViewRowState.CurrentRows);
                        gvAssocCustList.DataSource = dvcustomerList.ToTable();

                        //if (dvcustomerList.Count > 10)
                        //    pnlCustomerList.Style.Add("Height", "410px");
                        //else
                        //    pnlCustomerList.Style.Remove("Height");


                    }
                    else if ((!string.IsNullOrEmpty(statustype)) && (!string.IsNullOrEmpty(prospectType)) && (string.IsNullOrEmpty(rmType)))
                    {
                        dvcustomerList = new DataView(dtCustomer, "IsProspect = '" + prospectType + "'and IsActive= '" + statustype + "'", "CustomerId,ACC_CustomerCategoryName,Cust_Comp_Name,ParentId,PANNumber,BranchName,RMId,Area,Pincode,City,ADUL_ProcessId", DataViewRowState.CurrentRows);
                        gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    }
                    #region dependency code of group filter and other filter but not use
                    //else if ((string.IsNullOrEmpty(statustype)) && (!string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(parentType)) && (string.IsNullOrEmpty(rmType)))
                    //{
                    //    dvcustomerList = new DataView(dtCustomer, "IsProspect = '" + prospectType + "'and ParentId= '" + parentType + "'", "CustomerId,Cust_Comp_Name,ParentId,PANNumber,BranchName,RMId,Area,Pincode,City,ADUL_ProcessId,IsActive", DataViewRowState.CurrentRows);
                    //    gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    //}
                    //else if ((!string.IsNullOrEmpty(statustype)) && (string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(parentType)) && (string.IsNullOrEmpty(rmType)))
                    //{
                    //    dvcustomerList = new DataView(dtCustomer, "IsActive = '" + statustype + "'and ParentId= '" + parentType + "'", "CustomerId,Cust_Comp_Name,ParentId,PANNumber,BranchName,RMId,Area,Pincode,City,ADUL_ProcessId,IsProspect", DataViewRowState.CurrentRows);
                    //    gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    //}
                    //else if ((string.IsNullOrEmpty(statustype)) && (string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(parentType)) && (!string.IsNullOrEmpty(rmType)))
                    //{
                    //    dvcustomerList = new DataView(dtCustomer, "ParentId = '" + parentType + "'and RMId= '" + rmType + "'", "CustomerId,Cust_Comp_Name,PANNumber,BranchName,Area,Pincode,City,ADUL_ProcessId,IsProspect,IsActive", DataViewRowState.CurrentRows);
                    //    gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    //}
                    //else if ((!string.IsNullOrEmpty(statustype)) && (!string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(parentType)) && (string.IsNullOrEmpty(rmType)))
                    //{
                    //    dvcustomerList = new DataView(dtCustomer, "IsProspect = '" + prospectType + "'and IsActive= '" + statustype + "'", "CustomerId,Cust_Comp_Name,ParentId,PANNumber,BranchName,RMId,Area,Pincode,City,ADUL_ProcessId", DataViewRowState.CurrentRows);
                    //    gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    //}
                    //else if ((!string.IsNullOrEmpty(statustype)) && (!string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(parentType)) && (string.IsNullOrEmpty(rmType)))
                    //{
                    //    dvcustomerList = new DataView(dtCustomer, "IsProspect = '" + prospectType + "'and IsActive= '" + statustype + "'and ParentId = '" + parentType + "'", "CustomerId,Cust_Comp_Name,PANNumber,BranchName,RMId,Area,Pincode,City,ADUL_ProcessId", DataViewRowState.CurrentRows);
                    //    gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    //}
                    #endregion
                    else if ((!string.IsNullOrEmpty(statustype)) && (string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(rmType)))
                    {
                        dvcustomerList = new DataView(dtCustomer, "IsActive= '" + statustype + "'and RMId = '" + rmType + "'", "CustomerId,ACC_CustomerCategoryName,Cust_Comp_Name,ParentId,PANNumber,BranchName,Area,Pincode,City,ADUL_ProcessId", DataViewRowState.CurrentRows);
                        gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    }
                    #region dependency code of group filter and other filter but not use
                    //else if ((!string.IsNullOrEmpty(statustype)) && (string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(parentType)) && (!string.IsNullOrEmpty(rmType)))
                    //{
                    //    dvcustomerList = new DataView(dtCustomer, "RMId = '" + rmType + "'and IsActive= '" + statustype + "'and ParentId = '" + parentType + "'", "CustomerId,Cust_Comp_Name,PANNumber,BranchName,Area,Pincode,City,ADUL_ProcessId,IsProspect", DataViewRowState.CurrentRows);
                    //    gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    //}
                    #endregion
                    else if ((string.IsNullOrEmpty(statustype)) && (!string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(rmType)))
                    {
                        dvcustomerList = new DataView(dtCustomer, "IsProspect = '" + prospectType + "'and RMId = '" + rmType + "'", "CustomerId,ACC_CustomerCategoryName,Cust_Comp_Name,ParentId,PANNumber,BranchName,Area,Pincode,City,ADUL_ProcessId,IsActive", DataViewRowState.CurrentRows);
                        gvAssocCustList.DataSource = dvcustomerList.ToTable();

                    }
                    else if ((!string.IsNullOrEmpty(statustype)) && (!string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(rmType)))
                    {
                        dvcustomerList = new DataView(dtCustomer, "IsActive= '" + statustype + "' and IsProspect = '" + prospectType + "'and RMId = '" + rmType + "'", "CustomerId,ACC_CustomerCategoryName,Cust_Comp_Name,ParentId,PANNumber,BranchName,Area,Pincode,City,ADUL_ProcessId", DataViewRowState.CurrentRows);
                        gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    }
                    else
                    {
                        gvAssocCustList.DataSource = dtCustomer;
                        //if (dtCustomer.Rows.Count> 10)
                        //pnlCustomerList.Style.Add("Height", "410px");                             
                        //else
                        //pnlCustomerList.Style.Remove("Height");

                    }
                }
                else
                {
                    if ((!string.IsNullOrEmpty(statustype)) && (string.IsNullOrEmpty(prospectType)))
                    {
                        dvcustomerList = new DataView(dtCustomer, "IsActive = '" + statustype + "'", "CustomerId,ACC_CustomerCategoryName,Cust_Comp_Name,ParentId,PANNumber,Area,Pincode,City,ADUL_ProcessId,IsProspect", DataViewRowState.CurrentRows);
                        gvAssocCustList.DataSource = dvcustomerList.ToTable();
                        //if (dvcustomerList.Count > 10)
                        //    pnlCustomerList.Style.Add("Height", "410px");
                        //else
                        //    pnlCustomerList.Style.Remove("Height");

                    }
                    else if ((string.IsNullOrEmpty(statustype)) && (!string.IsNullOrEmpty(prospectType)))
                    {
                        dvcustomerList = new DataView(dtCustomer, "IsProspect = '" + prospectType + "'", "CustomerId,ACC_CustomerCategoryName,Cust_Comp_Name,ParentId,PANNumber,Area,Pincode,City,ADUL_ProcessId,IsActive", DataViewRowState.CurrentRows);
                        gvAssocCustList.DataSource = dvcustomerList.ToTable();
                        //if (dvcustomerList.Count > 10)
                        //    pnlCustomerList.Style.Add("Height", "410px");
                        //else
                        //    pnlCustomerList.Style.Remove("Height");

                    }
                    #region dependency code of group filter and other filter but not use
                    //else if ((string.IsNullOrEmpty(statustype)) && (string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(parentType)))
                    //{
                    //    dvcustomerList = new DataView(dtCustomer, "ParentId = '" + parentType + "'", "CustomerId,Cust_Comp_Name,PANNumber,Area,Pincode,City,ADUL_ProcessId,IsProspect,IsActive", DataViewRowState.CurrentRows);
                    //    gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    //}
                    #endregion
                    else if ((!string.IsNullOrEmpty(statustype)) && (!string.IsNullOrEmpty(prospectType)))
                    {
                        dvcustomerList = new DataView(dtCustomer, "IsProspect = '" + prospectType + "'and IsActive= '" + statustype + "'", "CustomerId,ACC_CustomerCategoryName,Cust_Comp_Name,ParentId,PANNumber,Area,Pincode,City,ADUL_ProcessId", DataViewRowState.CurrentRows);
                        gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    }
                    #region dependency code of group filter and other filter but not use
                    //else if ((string.IsNullOrEmpty(statustype)) && (!string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(parentType)))
                    //{
                    //    dvcustomerList = new DataView(dtCustomer, "IsProspect = '" + prospectType + "'and ParentId= '" + parentType + "'", "CustomerId,Cust_Comp_Name,PANNumber,Area,Pincode,City,ADUL_ProcessId,IsActive", DataViewRowState.CurrentRows);
                    //    gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    //}
                    //else if ((!string.IsNullOrEmpty(statustype)) && (string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(parentType)))
                    //{
                    //    dvcustomerList = new DataView(dtCustomer, "IsActive = '" + statustype + "'and ParentId= '" + parentType + "'", "CustomerId,Cust_Comp_Name,PANNumber,Area,Pincode,City,ADUL_ProcessId,IsProspect", DataViewRowState.CurrentRows);
                    //    gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    //}
                    //else if ((!string.IsNullOrEmpty(statustype)) && (!string.IsNullOrEmpty(prospectType)) && (!string.IsNullOrEmpty(parentType)))
                    //{
                    //    dvcustomerList = new DataView(dtCustomer, "IsActive= '" + statustype + "' and IsProspect = '" + prospectType + "'and ParentId = '" + parentType + "'", "CustomerId,Cust_Comp_Name,PANNumber,Area,Pincode,City,ADUL_ProcessId", DataViewRowState.CurrentRows);
                    //    gvAssocCustList.DataSource = dvcustomerList.ToTable();
                    //}
                    #endregion
                    else
                    {
                        gvAssocCustList.DataSource = dtCustomer;
                        //if (dtCustomer.Rows.Count > 10)
                        //    pnlCustomerList.Style.Add("Height", "410px");
                        //else
                        //    pnlCustomerList.Style.Remove("Height");

                    }
                }
            }

        }
        /// <summary>
        /// this is use to export customer list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvAssocCustList.ExportSettings.OpenInNewWindow = true;
            gvAssocCustList.ExportSettings.IgnorePaging = true;
            gvAssocCustList.ExportSettings.HideStructureColumns = true;
            gvAssocCustList.ExportSettings.ExportOnlyData = true;
            gvAssocCustList.ExportSettings.FileName = "Customer List";
            gvAssocCustList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvAssocCustList.MasterTableView.ExportToExcel();
        }
        #region no need to apply Drop down filter on group

        protected void RadComboGroup_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //RadComboBox dropdown = sender as RadComboBox;
            //ViewState["ParentId"] = dropdown.SelectedValue;
            //if (ViewState["ParentId"] != "")
            //{
            //    GridColumn column = gvAssocCustList.MasterTableView.GetColumnSafe("ParentId");
            //    column.CurrentFilterFunction = GridKnownFunction.EqualTo;
            //    gvAssocCustList.CurrentPageIndex = 0;
            //    gvAssocCustList.MasterTableView.Rebind();
            //}
            //else
            //{
            //    GridColumn column = gvAssocCustList.MasterTableView.GetColumnSafe("ParentId");
            //    column.CurrentFilterFunction = GridKnownFunction.EqualTo;
            //    gvAssocCustList.CurrentPageIndex = 0;
            //    gvAssocCustList.MasterTableView.Rebind();

            //}
        }
        #endregion
        protected void RadComboRM_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox dropdown = sender as RadComboBox;
            ViewState["RMId"] = dropdown.SelectedValue;
            if (ViewState["RMId"] != "")
            {
                GridColumn column = gvAssocCustList.MasterTableView.GetColumnSafe("RMId");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvAssocCustList.CurrentPageIndex = 0;
                gvAssocCustList.MasterTableView.Rebind();
            }
            else
            {
                GridColumn column = gvAssocCustList.MasterTableView.GetColumnSafe("RMId");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvAssocCustList.CurrentPageIndex = 0;
                gvAssocCustList.MasterTableView.Rebind();
            }
        }
        protected void IsProspect_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox dropdown = sender as RadComboBox;
            ViewState["IsProspect"] = dropdown.SelectedValue.ToString();
            if (ViewState["IsProspect"] != "")
            {
                GridColumn column = gvAssocCustList.MasterTableView.GetColumnSafe("IsProspect");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvAssocCustList.CurrentPageIndex = 0;
                gvAssocCustList.MasterTableView.Rebind();
            }
            else
            {
                GridColumn column = gvAssocCustList.MasterTableView.GetColumnSafe("IsProspect");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvAssocCustList.CurrentPageIndex = 0;
                gvAssocCustList.MasterTableView.Rebind();
            }
        }
        protected void Status_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox dropdown = sender as RadComboBox;
            ViewState["IsActive"] = dropdown.SelectedValue.ToString();
            if (ViewState["IsActive"] != "")
            {
                GridColumn column = gvAssocCustList.MasterTableView.GetColumnSafe("IsActive");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvAssocCustList.CurrentPageIndex = 0;
                gvAssocCustList.MasterTableView.Rebind();
            }
            else
            {
                GridColumn column = gvAssocCustList.MasterTableView.GetColumnSafe("IsActive");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvAssocCustList.CurrentPageIndex = 0;
                gvAssocCustList.MasterTableView.Rebind();
            }
        }
        protected void gvAssocCustList_ItemCreated(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridFilteringItem)
            //{
            //    GridFilteringItem fItem = (GridFilteringItem)e.Item;
            //    foreach (GridColumn col in gvAssocCustList.MasterTableView.Columns)
            //    {

            //        (fItem[col.UniqueName].Controls[0] as TextBox).Attributes.Add("onkeyup", "semicolon(this, event)");
            //    }
            //}
        }
        protected void gvAssocCustList_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvAssocCustList.MasterTableView.FilterExpression = "([Cust_Comp_Name] LIKE \'%" + customer.Trim() + "%\') ";
                GridColumn column = gvAssocCustList.MasterTableView.GetColumnSafe("Cust_Comp_Name");
                column.CurrentFilterFunction = GridKnownFunction.Contains;
                column.CurrentFilterValue = customer;
                gvAssocCustList.MasterTableView.Rebind();
            }
            if (gvAssocCustList.MasterTableView.FilterExpression != string.Empty)
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
            dtCustomerList = (DataTable)Cache["custList+UserRole" + adviserVo.advisorId + UserRole];
            if (dtCustomerList != null)
            {
                DataView view = new DataView(dtCustomerList);
                DataTable distinctValues = view.ToTable();
                DataRow[] rows = distinctValues.Select(gvAssocCustList.MasterTableView.FilterExpression.ToString());

                //if(rows.Length>10)
                //    pnlCustomerList.Style.Add("Height", "410px");
                //else
                //    pnlCustomerList.Style.Remove("Height");

                gvAssocCustList.MasterTableView.Rebind();
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
                ParentId = int.Parse(gvAssocCustList.MasterTableView.DataKeyValues[item.ItemIndex]["CustomerId"].ToString());
                userId = int.Parse(gvAssocCustList.MasterTableView.DataKeyValues[item.ItemIndex]["UserId"].ToString());
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

                FunctionInfo.Add("Method", "AssociateCustomerList.ascx:ddlAction_OnSelectedIndexChange()");
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
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AssociateCustomerList','CustomerDeleteStatus=" + DeleteStatus + "');", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AssociateCustomerList','login');", true);
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

    }

}
