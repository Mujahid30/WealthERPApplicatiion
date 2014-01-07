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
using Telerik.Web.UI;

namespace WealthERP.Advisor
{
    public partial class CustomerAssociationSetup : System.Web.UI.UserControl
    {

        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        List<CustomerVo> customerList = null;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        int customerId;
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();
        static string user = "";
        static string ExportFormat = string.Empty;
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        AdvisorBranchBo advisorBranchBO = new AdvisorBranchBo();
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
        //        GetPageCount();

        //        this.BindCustomer();

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
        //        objects[1] = user;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            AdvisorVo advisorVo = new AdvisorVo();
            SuccessMsg.Visible = false;
            ErrorMessage.Visible = false;
            adviserVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {

                this.BindCustomer();
                setShowHideControls(1);

            }


        }

        protected void BindGrid(int CurrentPage)
        {

            //if (hdnCurrentPage.Value.ToString() != "")
            //{
            //    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
            //    hdnCurrentPage.Value = "";
            //}

            int Count = 0;

            //customerList = advisorBranchBO.(adviserVo.advisorId, mypager.CurrentPage, out Count, hdnSort.Value, hdnNameFilter.Value, hdnAreaFilter.Value, hdnPincodeFilter.Value, hdnParentFilter.Value, hdnRMFilter.Value, out genDictParent, out genDictRM, out genDictReassignRM);
            //lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();

        }

        /// <summary>
        /// it will give all the selected customers ID from the Grid 
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetSelectedCustomerIdString()
        {
            StringBuilder customerIds = new StringBuilder();

            if (!chkSelectAllpages.Checked)
            {
                //'Navigate through each row in the GridView for checkbox items
                foreach (GridDataItem gvr in gvAssociations.Items)
                {
                    //int rowIndex = gvr.RowIndex;
                    //DataKey dKey = gvAssociation.DataKeys[rowIndex];

                    //int customerId = int.Parse(dKey.Values["CustomerId"].ToString());
                    int selectedRow = gvr.ItemIndex + 1;
                    int customerId = int.Parse((gvAssociations.MasterTableView.DataKeyValues[selectedRow - 1]["CustomerId"].ToString()));

                    CheckBox chkBxItem = (CheckBox)gvr.FindControl("cbRecons");
                    if (chkBxItem.Checked)
                    {
                        //customerIds.Append(dKey.Values["CustomerId"].ToString());
                        customerIds.Append((gvAssociations.MasterTableView.DataKeyValues[selectedRow - 1]["CustomerId"].ToString()));
                        customerIds.Append("~");
                        //customerIds += dKey.Values["CustomerId"].ToString() + "~";
                    }
                }
            }
            else
            {
                DataTable dtGvAssociationDetails = new DataTable();
                dtGvAssociationDetails = (DataTable)Cache["gvAssociationFull" + adviserVo.advisorId];
                int i = 0;

               
                //DataColumn dc = new DataColumn();
                foreach (DataRow dr in dtGvAssociationDetails.Rows)
                {
                    customerIds.Append(dtGvAssociationDetails.Rows[i]["CustomerId"].ToString()).Append("~");
                    i++;
                }
            }

            return customerIds;

        }
        /// <summary>
        /// Show hide the controls on the basics of radio button selection.
        /// </summary>
        /// <param name="branchRm"></param>
        public void setShowHideControls(Int16 branchRm)
        {
            if (branchRm == 1)
            {
                rdReassignBranch.Checked = true;
                trReassignBranch.Visible = true;
                trReassignRMCustomer.Visible = false;
                trReassignRM.Visible = false;
                btnReassignBranch.Visible = true;
                btnReassignRM.Visible = false;
                ErrorMessage.Visible = false;

            }
            else
            {
                rdReassignRM.Checked = true;
                trReassignBranch.Visible = false;
                trReassignRMCustomer.Visible = true;
                trReassignRM.Visible = true;
                btnReassignBranch.Visible = false;
                btnReassignRM.Visible = true;


            }
        }

        private TextBox GetBranchTextBox()
        {
            TextBox txt = new TextBox();
            if (gvAssociation.HeaderRow != null)
            {
                if ((TextBox)gvAssociation.HeaderRow.FindControl("txtBranchSearch") != null)
                {
                    txt = (TextBox)gvAssociation.HeaderRow.FindControl("txtBranchSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetRMNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvAssociation.HeaderRow != null)
            {
                if ((TextBox)gvAssociation.HeaderRow.FindControl("txtRMNameSearch") != null)
                {
                    txt = (TextBox)gvAssociation.HeaderRow.FindControl("txtRMNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetCustNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvAssociation.HeaderRow != null)
            {
                if ((TextBox)gvAssociation.HeaderRow.FindControl("txtCustNameSearch") != null)
                {
                    txt = (TextBox)gvAssociation.HeaderRow.FindControl("txtCustNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetAreaTextBox()
        {
            TextBox txt = new TextBox();
            if (gvAssociation.HeaderRow != null)
            {
                if ((TextBox)gvAssociation.HeaderRow.FindControl("txtAreaSearch") != null)
                {
                    txt = (TextBox)gvAssociation.HeaderRow.FindControl("txtAreaSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetCustCityTextBox()
        {
            TextBox txt = new TextBox();
            if (gvAssociation.HeaderRow != null)
            {
                if ((TextBox)gvAssociation.HeaderRow.FindControl("txtCitySearch") != null)
                {
                    txt = (TextBox)gvAssociation.HeaderRow.FindControl("txtCitySearch");
                }
            }
            else
                txt = null;

            return txt;
        }


        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetCustNameTextBox();

            if (txtName != null)
            {
                hdnNameFilter.Value = txtName.Text.Trim();
                this.BindCustomer();
            }

        }

        protected void btnBranchSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetBranchTextBox();

            if (txtName != null)
            {
                hdnBranchFilter.Value = txtName.Text.Trim();
                //this.showRMUserDetails();
                this.BindCustomer();
            }
        }

        protected void btnRMSearch_Click(object sender, EventArgs e)
        {
            TextBox txtRMName = GetRMNameTextBox();

            if (txtRMName != null)
            {
                hdnRMNameFilter.Value = txtRMName.Text.Trim();
                this.BindCustomer();
            }



        }

        protected void btnAreaSearch_Click(object sender, EventArgs e)
        {
            TextBox txtArea = GetAreaTextBox();

            if (txtArea != null)
            {
                hdnAreaFilter.Value = txtArea.Text.Trim();

                this.BindCustomer();

            }

        }

        protected void btnCitySearch_Click(object sender, EventArgs e)
        {
            TextBox txtCity = GetCustCityTextBox();

            if (txtCity != null)
            {
                hdnCityFilter.Value = txtCity.Text.Trim();
                this.BindCustomer();
            }

        }
        
      

        private void BindCustomer()
        {
            //Dictionary<string, string> genDictParent = new Dictionary<string, string>();
            //Dictionary<string, string> genDictRM = new Dictionary<string, string>();
            //Dictionary<string, string> genDictReassignRM = new Dictionary<string, string>();

            Dictionary<string, string> advisorBranchList = new Dictionary<string, string>();
            string customer = "";
            AdvisorBranchBo adviserBranchBo = new AdvisorBranchBo();
            List<CustomerVo> customerList = new List<CustomerVo>();

            //
            List<CustomerVo> allcustomerList = new List<CustomerVo>();

            RMVo customerRMVo = new RMVo();
            DataTable dtRMCustomer = new DataTable();
            DataTable dtRMCustomerFull = new DataTable();
            try
            {

                DropDownList ddl = new DropDownList();
                Label lbl = new Label();

                //customer = Session["Customer"].ToString();
                //if (customer.ToLower().Trim() == "find customer" || customer.ToLower().Trim() == "")
                //    customer = "";

                rmVo = (RMVo)Session["rmVo"];

                adviserVo = (AdvisorVo)Session["advisorVo"];

                //if (hdnCurrentPage.Value.ToString() != "")
                //{
                //    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                //    hdnCurrentPage.Value = "";
                //}

                int Count = 0;

                // Search Term is input into this hidden field
                //hdnNameFilter.Value = customer;

                //customerList = adviserBranchBo.(adviserVo.advisorId, mypager.CurrentPage, out Count, hdnSort.Value, hdnNameFilter.Value, hdnAreaFilter.Value, hdnPincodeFilter.Value, hdnParentFilter.Value, hdnRMFilter.Value, out genDictParent, out genDictRM, out genDictReassignRM);
                customerList = adviserBranchBo.GetAdviserCustomerListForAssociation(int.Parse(hndBranchIdFilter.Value.ToString()), adviserVo.advisorId,0, out Count, hdnSort.Value, hdnNameFilter.Value, hdnBranchFilter.Value, hdnRMNameFilter.Value, hdnAreaFilter.Value, hdnCityFilter.Value, out advisorBranchList);
                allcustomerList = adviserBranchBo.GetAdviserCustomerListForAssociation(int.Parse(hndBranchIdFilter.Value.ToString()), adviserVo.advisorId,0, out Count, hdnSort.Value, hdnNameFilter.Value, hdnBranchFilter.Value, hdnRMNameFilter.Value, hdnAreaFilter.Value, hdnCityFilter.Value, out advisorBranchList);
               // lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();

                #region forcustomerlist
                dtRMCustomer.Columns.Add("CustomerId");
                dtRMCustomer.Columns.Add("UserId");
                dtRMCustomer.Columns.Add("RMId");
                dtRMCustomer.Columns.Add("Parent");
                dtRMCustomer.Columns.Add("Cust_Comp_Name");
                dtRMCustomer.Columns.Add("PAN Number");
                dtRMCustomer.Columns.Add("Mobile Number");
                dtRMCustomer.Columns.Add("Phone Number");
                dtRMCustomer.Columns.Add("Email");
                dtRMCustomer.Columns.Add("Address");
                dtRMCustomer.Columns.Add("Area");
                dtRMCustomer.Columns.Add("City");
                dtRMCustomer.Columns.Add("Pincode");
                dtRMCustomer.Columns.Add("RMName");
                dtRMCustomer.Columns.Add("BranchName");
                dtRMCustomer.Columns.Add("BranchId");
                dtRMCustomer.Columns.Add("CustomerType");
                #endregion


                #region forallcustomerlist
                dtRMCustomerFull.Columns.Add("CustomerId");
                dtRMCustomerFull.Columns.Add("UserId");
                dtRMCustomerFull.Columns.Add("RMId");
                dtRMCustomerFull.Columns.Add("Parent");
                dtRMCustomerFull.Columns.Add("Cust_Comp_Name");
                dtRMCustomerFull.Columns.Add("PAN Number");
                dtRMCustomerFull.Columns.Add("Mobile Number");
                dtRMCustomerFull.Columns.Add("Phone Number");
                dtRMCustomerFull.Columns.Add("Email");
                dtRMCustomerFull.Columns.Add("Address");
                dtRMCustomerFull.Columns.Add("Area");
                dtRMCustomerFull.Columns.Add("City");
                dtRMCustomerFull.Columns.Add("Pincode");
                dtRMCustomerFull.Columns.Add("RMName");
                dtRMCustomerFull.Columns.Add("BranchName");
                dtRMCustomerFull.Columns.Add("BranchId");
                dtRMCustomerFull.Columns.Add("CustomerType");
                #endregion

                if (customerList == null)
                {
                    //DataRow drRMCustomer = dtRMCustomer.NewRow();
                    //drRMCustomer[0] = string.Empty;
                    //drRMCustomer[1] = string.Empty;
                    //drRMCustomer[2] = string.Empty;
                    //drRMCustomer[3] = string.Empty;
                    //drRMCustomer[4] = string.Empty;
                    //drRMCustomer[5] = string.Empty;
                    //drRMCustomer[6] = string.Empty;
                    //drRMCustomer[7] = string.Empty;
                    //drRMCustomer[8] = string.Empty;
                    //drRMCustomer[9] = string.Empty;
                    //drRMCustomer[10] = string.Empty;
                    //drRMCustomer[11] = string.Empty;
                    //drRMCustomer[12] = string.Empty;
                    //drRMCustomer[13] = string.Empty;
                    //drRMCustomer[14] = string.Empty;
                    //drRMCustomer[15] = string.Empty;
                    //drRMCustomer[16] = string.Empty;
                    //dtRMCustomer.Rows.Add(drRMCustomer);

                    //gvAssociation.DataSource = dtRMCustomer;
                    //gvAssociation.DataBind();
                    //gvAssociation.Columns[4].Visible = false;
                    ////gvAssociation.Columns[5].Visible = false;
                    //gvAssociation.Columns[8].Visible = false;

                    hdnRecordCount.Value = "0";

                    ErrorMessage.Visible = true;
                    trPager.Visible = false;
                   // lblTotalRows.Visible = false;
                    lblCurrentPage.Visible = false;
                    btnReassignRM.Visible = false;
                    trReassignRM.Visible = false;
                }
                else
                {

                    if (rdReassignRM.Checked == true)
                    {
                        trReassignRM.Visible = true;
                        btnReassignRM.Visible = true;
                    }

                    ErrorMessage.Visible = false;
                    //trPager.Visible = true;
                   // lblTotalRows.Visible = true;
                    //lblCurrentPage.Visible = true;

                    DataRow drRMCustomer;
                    DataRow drRMCustomerALL;

                    #region customer list according to pagecount
                    
                    for (int i = 0; i < customerList.Count; i++)
                    {
                        drRMCustomer = dtRMCustomer.NewRow();
                        customerVo = new CustomerVo();
                        customerVo = customerList[i];
                        drRMCustomer[0] = customerVo.CustomerId.ToString();
                        drRMCustomer[1] = customerVo.UserId.ToString();
                        drRMCustomer[2] = customerVo.RmId.ToString();
                        drRMCustomer["BranchId"] = customerVo.BranchId.ToString();
                        if (customerVo.ParentCustomer != null)
                        {
                            drRMCustomer[3] = customerVo.ParentCustomer.ToString();
                        }

                        drRMCustomer[4] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();

                        //if (customerVo.Type.ToUpper().ToString() == "IND" || customerVo.Type == "")
                        //{
                        //    if (customerVo.ParentCustomer != null)
                        //    {
                        //        drRMCustomer[3] = customerVo.ParentCustomer.ToString();
                        //    }
                        //    else
                        //    {
                        //        drRMCustomer[3] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        //    }
                        //    drRMCustomer[4] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        //}
                        //else if (customerVo.Type.ToUpper().ToString() == "NIND")
                        //{
                        //    if (customerVo.ParentCompany != null)
                        //    {
                        //        drRMCustomer[3] = customerVo.ParentCompany.ToString();
                        //    }
                        //    else
                        //    {
                        //        drRMCustomer[3] = customerVo.CompanyName.ToString();
                        //    }
                        //    drRMCustomer[4] = customerVo.CompanyName.ToString();
                        //}
                        if (customerVo.PANNum != null)
                            drRMCustomer[5] = customerVo.PANNum.ToString();
                        else
                            drRMCustomer[5] = "";
                        drRMCustomer[6] = customerVo.Mobile1.ToString();
                        drRMCustomer[7] = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                        drRMCustomer[8] = customerVo.Email.ToString();
                        if (customerVo.Adr1Line1 == null)
                            customerVo.Adr1Line1 = "";
                        if (customerVo.Adr1Line2 == null)
                            customerVo.Adr1Line2 = "";
                        if (customerVo.Adr1Line3 == null)
                            customerVo.Adr1Line3 = "";
                        if (customerVo.Adr1City == null)
                            customerVo.Adr1City = "";
                        if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() == "")
                            drRMCustomer[9] = "-";
                        else if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() != "")
                            drRMCustomer[9] = customerVo.Adr1Line2.ToString();
                        else if (customerVo.Adr1Line1.ToString() != "" && customerVo.Adr1Line2.ToString() == "")
                            drRMCustomer[9] = customerVo.Adr1Line1.ToString();
                        else if (customerVo.Adr1Line1.ToString() != "" && customerVo.Adr1Line2.ToString() != "")
                            drRMCustomer[9] = customerVo.Adr1Line1.ToString() + "," + customerVo.Adr1Line2.ToString() + "," + customerVo.Adr1Line3.ToString() + "," + customerVo.Adr1City.ToString();

                        drRMCustomer[10] = customerVo.Adr1Line3.ToString() + "," + customerVo.Adr1City.ToString();
                        if (customerVo.Adr1City.ToString() != "" && customerVo.Adr1Line3.ToString() != "")
                            drRMCustomer[11] = customerVo.Adr1City.ToString() + "," + customerVo.Adr1Line3.ToString();
                        else if (customerVo.Adr1City.ToString() != "" && customerVo.Adr1Line3.ToString() == "")
                            drRMCustomer[11] = customerVo.Adr1City.ToString();
                        else if (customerVo.Adr1City.ToString() == "" && customerVo.Adr1Line3.ToString() != "")
                            drRMCustomer[11] = customerVo.Adr1Line3.ToString();
                        else if (customerVo.Adr1City.ToString() == "" && customerVo.Adr1Line3.ToString() == "")
                            drRMCustomer[11] = "-";
                        drRMCustomer[12] = customerVo.Adr1PinCode.ToString();
                        drRMCustomer[13] = customerVo.AssignedRM.ToString();
                        drRMCustomer[14] = customerVo.BranchName.ToString();
                        drRMCustomer[16] = customerVo.UserType.ToString();
                        dtRMCustomer.Rows.Add(drRMCustomer);
                    }

                    #endregion

                    #region customerlist full
                    
                    for (int i = 0; i < allcustomerList.Count; i++)
                    {
                        drRMCustomerALL = dtRMCustomerFull.NewRow();
                        customerVo = new CustomerVo();
                        customerVo = allcustomerList[i];
                        drRMCustomerALL[0] = customerVo.CustomerId.ToString();
                        drRMCustomerALL[1] = customerVo.UserId.ToString();
                        drRMCustomerALL[2] = customerVo.RmId.ToString();
                        drRMCustomerALL["BranchId"] = customerVo.BranchId.ToString();
                        if (customerVo.ParentCustomer != null)
                        {
                            drRMCustomerALL[3] = customerVo.ParentCustomer.ToString();
                        }

                        drRMCustomerALL[4] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        if (customerVo.PANNum != null)
                            drRMCustomerALL[5] = customerVo.PANNum.ToString();
                        else
                            drRMCustomerALL[5] = "";
                        drRMCustomerALL[6] = customerVo.Mobile1.ToString();
                        drRMCustomerALL[7] = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                        drRMCustomerALL[8] = customerVo.Email.ToString();
                        if (customerVo.Adr1Line1 == null)
                            customerVo.Adr1Line1 = "";
                        if (customerVo.Adr1Line2 == null)
                            customerVo.Adr1Line2 = "";
                        if (customerVo.Adr1Line3 == null)
                            customerVo.Adr1Line3 = "";
                        if (customerVo.Adr1City == null)
                            customerVo.Adr1City = "";
                        if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() == "")
                            drRMCustomerALL[9] = "-";
                        else if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() != "")
                            drRMCustomerALL[9] = customerVo.Adr1Line2.ToString();
                        else if (customerVo.Adr1Line1.ToString() != "" && customerVo.Adr1Line2.ToString() == "")
                            drRMCustomerALL[9] = customerVo.Adr1Line1.ToString();
                        else if (customerVo.Adr1Line1.ToString() != "" && customerVo.Adr1Line2.ToString() != "")
                            drRMCustomerALL[9] = customerVo.Adr1Line1.ToString() + "," + customerVo.Adr1Line2.ToString() + "," + customerVo.Adr1Line3.ToString() + "," + customerVo.Adr1City.ToString();

                        drRMCustomerALL[10] = customerVo.Adr1Line3.ToString() + "," + customerVo.Adr1City.ToString();
                        if (customerVo.Adr1City.ToString() != "" && customerVo.Adr1Line3.ToString() != "")
                            drRMCustomerALL[11] = customerVo.Adr1City.ToString() + "," + customerVo.Adr1Line3.ToString();
                        else if (customerVo.Adr1City.ToString() != "" && customerVo.Adr1Line3.ToString() == "")
                            drRMCustomerALL[11] = customerVo.Adr1City.ToString();
                        else if (customerVo.Adr1City.ToString() == "" && customerVo.Adr1Line3.ToString() != "")
                            drRMCustomerALL[11] = customerVo.Adr1Line3.ToString();
                        else if (customerVo.Adr1City.ToString() == "" && customerVo.Adr1Line3.ToString() == "")
                            drRMCustomerALL[11] = "-";
                        drRMCustomerALL[12] = customerVo.Adr1PinCode.ToString();
                        drRMCustomerALL[13] = customerVo.AssignedRM.ToString();
                        drRMCustomerALL[14] = customerVo.BranchName.ToString();
                        drRMCustomerALL[16] = customerVo.UserType.ToString();
                        dtRMCustomerFull.Rows.Add(drRMCustomerALL);
                    }

                    #endregion

                    gvAssociations.DataSource = dtRMCustomer;
                    gvAssociations.DataBind();

                    #region customerlist
                    
                    if (Cache["gvAssociation"+adviserVo.advisorId] == null)
                    {
                        Cache.Insert("gvAssociation" + adviserVo.advisorId, dtRMCustomer);
                    }
                    else
                    {
                        Cache.Remove("gvAssociation" + adviserVo.advisorId);
                        Cache.Insert("gvAssociation" + adviserVo.advisorId, dtRMCustomer);
                    }

                    #endregion

                    #region customerlist full
                    if (Cache["gvAssociationFull" + adviserVo.advisorId] == null)
                    {
                        Cache.Insert("gvAssociationFull" + adviserVo.advisorId, dtRMCustomerFull);
                    }
                    else
                    {
                        Cache.Remove("gvAssociationFull" + adviserVo.advisorId);
                        Cache.Insert("gvAssociationFull" + adviserVo.advisorId, dtRMCustomerFull);
                    }

                    #endregion



                    gvAssociations.Columns[4].Visible = false;
                    //gvAssociation.Columns[5].Visible = false;
                    gvAssociations.Columns[8].Visible = false;
                    //ReAssignRMControl(genDictRM);

                    //if (genDictParent.Count > 0)
                    //{
                    //    DropDownList ddlParent = GetParentDDL();
                    //    if (ddlParent != null)
                    //    {
                    //        ddlParent.DataSource = genDictParent;
                    //        ddlParent.DataTextField = "Key";
                    //        ddlParent.DataValueField = "Value";
                    //        ddlParent.DataBind();
                    //        ddlParent.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                    //    }
                    //    if (hdnParentFilter.Value != "")
                    //    {
                    //        ddlParent.SelectedValue = hdnParentFilter.Value.ToString();
                    //    }
                    //}

                    if (advisorBranchList.Count > 0)
                    {
                        if (ddlAdvisorBranchList != null)
                        {
                            ddlAdvisorBranchList.DataSource = advisorBranchList;
                            ddlAdvisorBranchList.DataTextField = "Value";
                            ddlAdvisorBranchList.DataValueField = "Key";
                            ddlAdvisorBranchList.DataBind();
                            ddlAdvisorBranchList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                        }
                        if (hdnRMFilter.Value != "")
                        {
                            ddlAdvisorBranchList.SelectedIndex = 0;
                        }
                    }
                    if (advisorBranchList.Count > 0 && !Page.IsPostBack)
                    {
                        if (ddlAdvisorBranchList != null)
                        {
                            ddlBranchList.DataSource = advisorBranchList;
                            ddlBranchList.DataTextField = "Value";
                            ddlBranchList.DataValueField = "Key";
                            ddlBranchList.DataBind();
                            ddlBranchList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select a Branch", "0"));
                        }
                        if (hdnRMFilter.Value != "")
                        {
                            ddlBranchList.SelectedIndex = 0;
                        }
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
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:BindCustomer()");
                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = customerVo;
                objects[2] = customerList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvAssociation_Sort(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = null;
            try
            {
                sortExpression = e.SortExpression;
                ViewState["sortExpression"] = sortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    hdnSort.Value = sortExpression + " DESC";
                    this.BindCustomer();
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    hdnSort.Value = sortExpression + " ASC";
                    this.BindCustomer();
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

                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:gvCustomers_Sort()");

                object[] objects = new object[1];
                objects[0] = sortExpression;

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

        protected void ddlAdvisorBranchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlBranchRMList.Visible = false;
            bool groupHead = false;
            if (ddlAdvisorBranchList.SelectedIndex != 0)
            {
                string allCustomersId = Convert.ToString(GetSelectedCustomerIdString());
                if (string.IsNullOrEmpty(allCustomersId))
                {
                    ddlAdvisorBranchList.SelectedIndex = 0;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "alert('Please select some customer for branch reassign');", true);
                    return;
                }
                groupHead = advisorBranchBO.CheckCustomerGroupHead(allCustomersId);
                if (groupHead == true)
                {
                    hndIsGroupHead.Value = "true";
                }
                else
                    hndIsGroupHead.Value = "false";
                //if (groupHead == true)
                //{
                //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "window.confirm('The search fee will now be deducted for the criteria entered. Do you wish to continue?');", true);
                //    //AjaxControlToolkit.ConfirmButtonExtender cbExt = new AjaxControlToolkit.ConfirmButtonExtender();
                //    //cbExt.ConfirmText
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "confirm('Unable to locate your search item. Do you want to search the closest match from your item?');", true);

                //}
                DataTable dtRMList = advisorStaffBo.GetBranchRMList(int.Parse(ddlAdvisorBranchList.SelectedValue.ToString()));
                ddlBranchRMList.DataSource = dtRMList;
                ddlBranchRMList.DataTextField = dtRMList.Columns["RMName"].ToString();
                ddlBranchRMList.DataValueField = dtRMList.Columns["AR_RMId"].ToString();
                ddlBranchRMList.DataBind();
                ddlBranchRMList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                foreach (GridDataItem dr in gvAssociations.Items)
                {
               
                    int selectedRow = dr.ItemIndex + 1;
                    int userId = int.Parse((gvAssociations.MasterTableView.DataKeyValues[selectedRow - 1]["UserId"].ToString()));
                    int customerId = int.Parse((gvAssociations.MasterTableView.DataKeyValues[selectedRow - 1]["CustomerId"].ToString()));
                    string rmId = gvAssociations.MasterTableView.DataKeyValues[selectedRow - 1]["RMId"].ToString();
                    int branchId = int.Parse((gvAssociations.MasterTableView.DataKeyValues[selectedRow - 1]["BranchId"].ToString()));


                    if (((CheckBox)dr.FindControl("cbRecons")).Checked == true)
                    {
                        dtRMList.DefaultView.Sort = "AR_RMId";
                        int i = dtRMList.DefaultView.Find(rmId);

                        if (i == (0 - 1))
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "alert('Some of the selected customer need to change RM also, please select the RM');", true);
                            trReassignRM.Visible = true;

                        }
                        else
                        {
                            trReassignRM.Visible = false;

                        }


                    }
                }


            }
        }



        protected void btnReassignBranch_Click(object sender, EventArgs e)
        {
            string selectedCusmoerIds = Convert.ToString(GetSelectedCustomerIdString());
            if (ddlBranchRMList.Visible == true && ddlBranchRMList.SelectedIndex != 0)
                advisorBranchBO.ReassignCustomersBranchRM(selectedCusmoerIds, int.Parse(ddlAdvisorBranchList.SelectedValue), int.Parse(ddlBranchRMList.SelectedValue));
            else
                advisorBranchBO.ReassignCustomersBranchRM(selectedCusmoerIds, int.Parse(ddlAdvisorBranchList.SelectedValue), 0);

            this.BindCustomer();
            SuccessMsg.Visible = true;
            setShowHideControls(1);
        }

        protected void btnReassignRM_Click(object sender, EventArgs e)
        {
            string selectedCusmoerIds = Convert.ToString(GetSelectedCustomerIdString());
            if (ddlBranchRMList.Visible == true && ddlBranchRMList.SelectedIndex != 0)
            {
                advisorBranchBO.ReassignCustomersBranchRM(selectedCusmoerIds, int.Parse(ddlBranchList.SelectedValue), int.Parse(ddlBranchRMList.SelectedValue));
                SuccessMsg.Visible = true;
                this.BindCustomer();

            }

        }

        protected void rdReassignRM_CheckedChanged(object sender, EventArgs e)
        {
            setShowHideControls(0);
            ddlBranchList.SelectedIndex = 0;
            hndBranchIdFilter.Value = "0";
            this.BindCustomer();
            ddlBranchList.SelectedIndex = 0;
            trReassignRM.Visible = false;
            //bindBranchRMDropdown(int.Parse(ddlBranchList.SelectedValue));
        }

        protected void ddlBranchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranchList.SelectedIndex == 0)
            {
                hndBranchIdFilter.Value = "0";
                this.BindCustomer();

            }
            else
            {
                trReassignRM.Visible = true;
                hndBranchIdFilter.Value = ddlBranchList.SelectedValue;
                this.BindCustomer();
                bindBranchRMDropdown(int.Parse(hndBranchIdFilter.Value.ToString()));
                ddlBranchList.SelectedValue = hndBranchIdFilter.Value.ToString();
            }
        }

        protected void rdReassignBranch_CheckedChanged(object sender, EventArgs e)
        {
            hdnBranchFilter.Value = string.Empty;
            hndBranchIdFilter.Value = "0";
            this.BindCustomer();
            setShowHideControls(1);

        }
        /// <summary>
        /// Binding RM list For a Branch
        /// </summary>
        /// <param name="branchId"></param>
        private void bindBranchRMDropdown(int branchId)
        {
            DataTable dtRMList = advisorStaffBo.GetBranchRMList(branchId);
            ddlBranchRMList.DataSource = dtRMList;
            ddlBranchRMList.DataTextField = dtRMList.Columns["RMName"].ToString();
            ddlBranchRMList.DataValueField = dtRMList.Columns["AR_RMId"].ToString();
            ddlBranchRMList.DataBind();
            ddlBranchRMList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }

        protected void ddlBranchRMList_SelectedIndexChanged(object sender, EventArgs e)
        {

            bool groupHead = false;
            if (ddlAdvisorBranchList.SelectedIndex != 0)
            {
                string allCustomersId = Convert.ToString(GetSelectedCustomerIdString());
                if (string.IsNullOrEmpty(allCustomersId))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "alert('Please select some customer for RM reassign');", true);
                    return;
                }
                groupHead = advisorBranchBO.CheckCustomerGroupHead(allCustomersId);
                if (groupHead == true)
                {
                    hndIsGroupHead.Value = "true";
                }
                else
                    hndIsGroupHead.Value = "false";

            }
        }



        protected void chkSelectAllpages_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAllpages.Checked == true)
                gvAssociations.Columns[0].Visible = false;
            else
                gvAssociations.Columns[0].Visible = true;

        }
        protected void gvAssociations_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataTable dtCustomer = new DataTable();
            dtCustomer = (DataTable)Cache["gvAssociation" + adviserVo.advisorId];
            gvAssociations.DataSource = dtCustomer;

        }
        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvAssociations.ExportSettings.OpenInNewWindow = true;
            gvAssociations.ExportSettings.IgnorePaging = true;
            gvAssociations.ExportSettings.HideStructureColumns = true;
            gvAssociations.ExportSettings.ExportOnlyData = true;
            gvAssociations.ExportSettings.FileName = "Customer Branch_RM Association";
            gvAssociations.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvAssociations.MasterTableView.ExportToExcel();
        }
    }
}