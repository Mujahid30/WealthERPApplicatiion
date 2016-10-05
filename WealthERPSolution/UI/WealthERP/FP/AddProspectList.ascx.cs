﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BoCustomerPortfolio;
using System.Data;
using VoUser;
using BoCustomerProfiling;
using BoUploads;
using VoCustomerProfiling;
using VoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoAdvisorProfiling;
using System.Configuration;

namespace WealthERP.FP
{
    public partial class AddProspectList : System.Web.UI.UserControl
    {
        BoDematAccount bodemataccount = new BoDematAccount();
        DataSet dsCustomerAssociation;
        CustomerVo customerVo = new CustomerVo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        CustomerBo customerBo = new CustomerBo();
        List<CustomerFamilyVo> customerFamilyVoList = new List<CustomerFamilyVo>();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        UserVo userVo = new UserVo();
        UserVo tempuservo;
        DataTable dt = new DataTable();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        DataRow dr;
        string Role = "";
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerFamilyVo familyVo = new CustomerFamilyVo();

        //For TaxSlab
        int years = 0;
        DataSet dsGetSlab = new DataSet();
        string userType;
        int bmID = 0;
        //For Edit 
        int totalRecordsCount;
        protected void Page_Init()
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                
                if (Session[SessionContents.CurrentUserRole].ToString() != "")
                    Role = Session[SessionContents.CurrentUserRole].ToString();
                advisorVo = (AdvisorVo)Session["advisorVo"];
                rmVo = (RMVo)Session[SessionContents.RmVo];
                userVo = (UserVo)Session["userVo"];

                if ((Role != "") && (Role == "Admin" || Role == "Ops"))
                {
                    btnConvertToCustomer.Enabled = true;
                }
                else
                {
                    btnConvertToCustomer.Enabled = false;
                }

                btnDelete.Enabled = false;
                int customerId = 0;
                bmID = rmVo.RMId;
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "research")
                {
                    userType = "advisor";
                }

                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                {
                   
                    userType = "rm";
                }
              
                if (Session["customerVo"] != null)
                    customerVo = (CustomerVo)Session["customerVo"];

                    if (!IsPostBack)
                    {
                        dt = new DataTable();
                        dt.Columns.Add("C_CustomerId");
                        dt.Columns.Add("CA_AssociationId");
                        dt.Columns.Add("CustomerRelationship");
                        dt.Columns.Add("FirstName");
                        dt.Columns.Add("MiddleName");
                        dt.Columns.Add("LastName");
                        dt.Columns.Add("DOB");
                        dt.Columns.Add("EmailId");
                        dt.Columns.Add("PanNum");
                        Session[SessionContents.FPS_AddProspect_DataTable] = dt;

                    }
                    else
                    {
                        dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                    }

                //SqlDataSourceCustomerRelation.ConnectionString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
                rmVo = (RMVo)Session["rmVo"];
                if (!IsPostBack)
                {
                    if (userType == "advisor")
                    {
                        BindBranchDropDown();
                        BindRMDropDown();
                    }
                    else if (userType == "rm")
                    {
                        lblRM.Visible = false;
                        rcbRM.Visible = false;
                        BindBranch(advisorVo.advisorId, rmVo.RMId);
                    }
                    //if(Role == "
                   
                    if (Session[SessionContents.FPS_AddProspectListActionStatus] != null)
                    {
                        customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                        customerVo = customerBo.GetCustomer(customerId);
                        hdnIsActive.Value = customerVo.IsActive.ToString();
                        hdnIsProspect.Value = customerVo.IsProspect.ToString();
                        customerFamilyVoList = customerFamilyBo.GetCustomerFamily(customerId);
                        if (customerFamilyVoList != null)
                        {

                            totalRecordsCount = customerFamilyVoList.Count;
                            dt.Rows.Clear();
                            foreach (CustomerFamilyVo customerFamilyVo in customerFamilyVoList)
                            {
                                DataRow dr = dt.NewRow();
                                dr["CA_AssociationId"] = customerFamilyVo.AssociationId;
                                dr["C_CustomerId"] = customerFamilyVo.AssociateCustomerId;
                                dr["CustomerRelationship"] = customerFamilyVo.RelationshipCode;
                                dr["FirstName"] = customerFamilyVo.FirstName;
                                dr["MiddleName"] = customerFamilyVo.MiddleName;
                                dr["LastName"] = customerFamilyVo.LastName;
                                if (customerFamilyVo.DOB != DateTime.Parse("01/01/0001 00:00:00"))
                                {
                                    dr["DOB"] = customerFamilyVo.DOB.ToShortDateString();
                                }
                                dr["EmailId"] = customerFamilyVo.EmailId;
                                dr["PanNum"] = customerFamilyVo.PanNo;
                                dt.Rows.Add(dr);
                            }
                            Session[SessionContents.FPS_AddProspect_DataTable] = dt;

                        }
                        else
                        {
                            tblChildCustomer.Visible = false;
                        }
                        txtFirstName.Text = customerVo.FirstName;
                        txtMiddleName.Text = customerVo.MiddleName;
                        txtLastName.Text = customerVo.LastName;
                        if (!string.IsNullOrEmpty(customerVo.Gender))
                        {
                            if (customerVo.Gender.ToUpper().ToString() == "M")
                            {
                                rbtnMale.Checked = true;
                            }
                            else if (customerVo.Gender.ToUpper().ToString() == "F")
                            {
                                rbtnFemale.Checked = true;
                            }
                        }
                        txtSlab.Text = customerVo.TaxSlab.ToString();

                        if (customerVo.Dob != DateTime.Parse("01/01/0001 00:00:00") && customerVo.Dob != null)
                        {
                            dpDOB.SelectedDate = customerVo.Dob;
                        }
                        txtEmail.Text = customerVo.Email;
                        txtPanNumber.Text = customerVo.PANNum;
                        txtAddress1.Text = customerVo.Adr1Line1;
                        txtAddress2.Text = customerVo.Adr1Line2;
                        txtMobileNo.Text = customerVo.Mobile1.ToString();
                        txtPinCode.Text = customerVo.Adr1PinCode.ToString();
                        txtCity.Text = customerVo.Adr1City;
                        txtState.Text = customerVo.Adr1State;
                        txtCountry.Text = customerVo.Adr1Country;
                        if (customerVo.ProspectAddDate != DateTime.Parse("01/01/0001 00:00:00") && customerVo.ProspectAddDate != null)
                        {
                            dpProspectAddDate.SelectedDate = customerVo.ProspectAddDate;
                        }
                        if (customerVo.ProspectAddDate != DateTime.Parse("01/01/0001 00:00:00") && customerVo.ProspectAddDate != null)
                        {
                            dpProspectAddDate.SelectedDate = customerVo.ProspectAddDate;
                        }
                        //for (int i = 0; i < ddlPickBranch.Items.Count; i++)
                        //{
                        //    if (ddlPickBranch.Items[i].Value == customerVo.BranchId.ToString())
                        //    {
                        //        ddlPickBranch.SelectedIndex = i;
                        //    }
                        //}
                        //ddlPickBranch.SelectedValue = customerVo.BranchId;
                        Rebind();
                        if (Session[SessionContents.FPS_AddProspectListActionStatus].ToString() == "View")
                        {
                            // View things have been handled here

                           

                            aplToolBar.Visible = true;                         
                            btnSubmit.Visible = false;
                            btnSubmitAddDetails.Visible = false;
                            btnConvertToCustomer.Enabled = false;
                            btnGetSlab.Enabled = false;
                            if (customerFamilyVoList != null)
                            {
                                RadGrid1.Columns[RadGrid1.Columns.Count - 1].Visible = false;
                                RadGrid1.Columns[0].Visible = false;
                                RadGrid1.AllowAutomaticInserts = false;
                                RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                                ChildCustomerGridPanel.Enabled = false;
                            }
                            else
                            {
                                ChildCustomerGridPanel.Visible = false;
                            }
                            //Disabling all Fields
                            txtEmail.Enabled = false;
                            txtFirstName.Enabled = false;
                            txtLastName.Enabled = false;
                            txtMiddleName.Enabled = false;
                            ddlPickBranch.Enabled = false;
                            rcbRM.Enabled = false;
                            ddlPickBranch.Enabled = false;
                            dpDOB.Enabled = false;
                            txtPanNumber.Enabled = false;
                            txtAddress1.Enabled = false;
                            txtAddress2.Enabled = false;
                            txtMobileNo.Enabled = false;
                            txtPinCode.Enabled = false;
                            txtCity.Enabled = false;
                            txtState.Enabled = false;
                            txtCountry.Enabled = false;
                            dpProspectAddDate.Enabled = false;
                            rbtnMale.Enabled = false;
                            rbtnFemale.Enabled = false;
                            txtSlab.Enabled = false;
                            headertitle.Text = "View Prospect";
                            btnDelete.Enabled = false;
                            RadGrid1.Columns[RadGrid1.Columns.Count - 1].Visible = false;
                            ddlPickBranch.SelectedValue = customerVo.BranchId.ToString();

                        }
                        else if (Session[SessionContents.FPS_AddProspectListActionStatus].ToString() == "Edit")
                        {
                            // Edit thing have been handled here

                            //BindBranch(advisorVo.advisorId, customerVo.RmId);

                            aplToolBar.Visible = true;
                            btnConvertToCustomer.Visible = true;
                            RadToolBarButton rtb = (RadToolBarButton)aplToolBar.Items.FindItemByValue("Edit");
                            if ((Role != "") && (Role == "Admin" || Role == "Ops"))
                                btnConvertToCustomer.Enabled = true;
                            else
                                btnConvertToCustomer.Enabled = true;
                            rcbRM.Enabled = false;
                            ddlPickBranch.Enabled = false;
                            rtb.Visible = false;
                            btnSubmit.Visible = true;
                            btnSubmitAddDetails.Visible = true;
                            btnSubmit.Text = "Update";
                            btnSubmitAddDetails.Text = "Edit Finance Details";
                            RadGrid1.Columns[RadGrid1.Columns.Count - 1].Visible = false;
                            tblChildCustomer.Visible = true;
                            headertitle.Text = "Edit Prospect";
                            rbtnMale.Enabled = true;
                            rbtnFemale.Enabled = true;
                            btnGetSlab.Enabled = true;
                            txtSlab.Enabled = true;
                            btnDelete.Enabled = true;
                            RadGrid1.Columns[RadGrid1.Columns.Count - 1].Visible = true;
                            ddlPickBranch.SelectedValue = customerVo.BranchId.ToString();

                        }
                       
                    }
                    //else
                    //{
                    //    BindBranch(advisorVo.advisorId, rmVo.RMId);
                    //}
                    //RadGrid1.Columns[RadGrid1.Columns.Count - 1].Visible = true;
                 }
               

            }
            catch (Exception ex)
            {
                throw ex;

            }
            hiddenassociation.Style.Add("display", "none");
        }




        /// <summary>        
        /// Used to bind branches of the Branch dropdown       
        /// </summary>
        /// <param name="advisorVo"></param>
        /// <param name="rmVo"></param>
        private void BindBranch(int adviserId, int rmId)
        {
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            UploadCommonBo uploadsCommonDao = new UploadCommonBo();
            //DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
            DataSet ds = advisorBranchBo.GetRMBranchAssociation(rmId, adviserId, "A");
            if (ds != null)
            {
                ddlPickBranch.DataSource = ds;
                ddlPickBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                ddlPickBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                ddlPickBranch.DataBind();
            }
        }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditManager editMan = editedItem.EditManager;
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                dr = dt.NewRow();
                try
                {
                    if (dt.Rows[e.Item.ItemIndex]["C_CustomerId"].ToString() != "")
                    {
                        ChildDeletionFunction();
                        Session["ChildCustomerId"] = dt.Rows[e.Item.ItemIndex]["C_CustomerId"].ToString();
                    }
                    else
                    {
                        dt.Rows[e.Item.ItemIndex].Delete();
                    }
                }
                catch (Exception ex)
                {
                    RadGrid1.Controls.Add(new LiteralControl("<strong>Unable to delete</strong>"));
                    e.Canceled = true;

                }
                Rebind();
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw ex;
            }
            
        }

        private void ChildDeletionFunction()
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "javascript:showmessage();", true);
        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditManager editMan = editedItem.EditManager;
                int i = 2;
                int count = 0;
                int count1 = 0;
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                dr = dt.NewRow();
                TextBox txt1 = (TextBox)e.Item.FindControl("txtChildPanNo");
                if (txt1.Text == txtPanNumber.Text)
                    count1++;
                if(count1 == 0)
                {
                if (PANValidation(txt1.Text))
                {
                    foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
                    {

                        if (column is IGridEditableColumn)
                        {
                            IGridEditableColumn editableCol = (column as IGridEditableColumn);
                            if (editableCol.IsEditable)
                            {
                                IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);
                                string editorType = editor.ToString();
                                string editorText = "unknown";
                                object editorValue = null;

                                if (editor is GridTextColumnEditor)
                                {
                                    editorText = (editor as GridTextColumnEditor).Text;
                                    editorValue = (editor as GridTextColumnEditor).Text;
                                }
                                if (editor is GridBoolColumnEditor)
                                {
                                    editorText = (editor as GridBoolColumnEditor).Value.ToString();
                                    editorValue = (editor as GridBoolColumnEditor).Value;
                                }
                                if (editor is GridDropDownColumnEditor)
                                {
                                    editorText = (editor as GridDropDownColumnEditor).SelectedValue;
                                    editorValue = (editor as GridDropDownColumnEditor).SelectedValue;
                                }
                                if (editor is GridTemplateColumnEditor)
                                {
                                    if (i == 3)
                                    {
                                        TextBox txt = (TextBox)e.Item.FindControl("txtChildFirstName");
                                        editorText = txt.Text;
                                        editorValue = txt.Text;
                                    }
                                    else if (i == 7)
                                    {
                                        TextBox txt = (TextBox)e.Item.FindControl("txtGridEmailId");
                                        editorText = txt.Text;
                                        editorValue = txt.Text;
                                    }
                                    else if (i == 8)
                                    {
                                        TextBox txt = (TextBox)e.Item.FindControl("txtChildPanNo");
                                        foreach (DataRow drPanChk in dt.Rows)
                                        {
                                            if (drPanChk["PanNum"].ToString() == txt.Text)
                                            {
                                                if (txt.Text == "")
                                                {

                                                }
                                                else
                                                count++;
                                            }

                                        }
                                        if (count == 0)
                                        {
                                            editorText = txt.Text;
                                            editorValue = txt.Text;
                                        }
                                    }

                                }
                                try
                                {
                                    DataRow[] changedrows = dt.Select();
                                    changedrows[editedItem.ItemIndex][column.UniqueName] = editorValue;


                                }
                                catch (Exception ex)
                                {
                                    RadGrid1.Controls.Add(new LiteralControl("<strong>Unable to set value of column '" + column.UniqueName + "'</strong> - " + ex.Message));
                                    e.Canceled = true;
                                    break;
                                }
                            }
                            i++;
                        }

                    }
                    if (count == 0)
                    {
                        //int CountCol = 0; //Counting each column of a row in  Table
                        //DataRow drChk;      //Data row to check which row contains dummy record. 

                        //for (int a = 0; a < dt.Rows.Count; a++)
                        //{
                        //    for (int j = 0; j < dt.Columns.Count; j++)
                        //    {
                        //        if (dt.Rows[a][j] == DBNull.Value)
                        //        {
                        //            CountCol++;
                        //        }
                        //    }
                        //    if (CountCol == dt.Columns.Count)
                        //    {
                        //        dr = dt.Rows[a];
                        //        dt.Rows.Remove(dr);
                        //    }
                        //    CountCol = 0;
                        //}
                        if (dr["FirstName"].ToString() == "")
                        {

                        }
                        else
                       dt.Rows.Add(dr);
                        
                    }

                    //else if (dr["PanNum"].ToString() == "")
                    //{

                    //}

                    else
                    {
                       
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('PAN Number Already Exists.');", true);
                      
                    }

                    Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                    Rebind();
                }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('PAN Number Already Exists.');", true);
                }
                }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw ex;
            }
        }

        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {


            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditManager editMan = editedItem.EditManager;
                int i = 2;
                int j = 0;
                int count = 0;
                int countParentPan = 0;
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                dr = dt.NewRow();
                TextBox txt1 = (TextBox)e.Item.FindControl("txtChildPanNo");

                if (txt1.Text == txtPanNumber.Text)
                    countParentPan++;

                if (countParentPan == 0)
                {
                    if (PANValidation(txt1.Text))
                    {
                        foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
                        {


                            if (column is IGridEditableColumn)
                            {
                                IGridEditableColumn editableCol = (column as IGridEditableColumn);
                                if (editableCol.IsEditable)
                                {
                                    IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);
                                    string editorType = editor.ToString();
                                    string editorText = "unknown";
                                    object editorValue = null;

                                    if (editor is GridTextColumnEditor)
                                    {
                                        editorText = (editor as GridTextColumnEditor).Text;
                                        editorValue = (editor as GridTextColumnEditor).Text;
                                    }
                                    if (editor is GridBoolColumnEditor)
                                    {
                                        editorText = (editor as GridBoolColumnEditor).Value.ToString();
                                        editorValue = (editor as GridBoolColumnEditor).Value;
                                    }
                                    if (editor is GridDropDownColumnEditor)
                                    {
                                        editorText = (editor as GridDropDownColumnEditor).SelectedValue;
                                        editorValue = (editor as GridDropDownColumnEditor).SelectedValue;
                                    }

                                    if (editor is GridTemplateColumnEditor)
                                    {
                                        if (i == 3)
                                        {
                                            TextBox txt = (TextBox)e.Item.FindControl("txtChildFirstName");
                                            editorText = txt.Text;
                                            editorValue = txt.Text;
                                        }
                                        else if (i == 7)
                                        {
                                            TextBox txt = (TextBox)e.Item.FindControl("txtGridEmailId");
                                            editorText = txt.Text;
                                            editorValue = txt.Text;
                                        }
                                        else if (i == 8)
                                        {
                                            TextBox txt = (TextBox)e.Item.FindControl("txtChildPanNo");

                                            foreach (DataRow drPanChk in dt.Rows)
                                            {
                                                if (drPanChk["PanNum"].ToString() == txt.Text)
                                                {
                                                    count++;
                                                }

                                            }
                                            editorText = txt.Text;
                                            editorValue = txt.Text;
                                        }

                                    }
                                    try
                                    {
                                        dr[i] = editorText;

                                    }
                                    catch (Exception ex)
                                    {
                                        RadGrid1.Controls.Add(new LiteralControl("<strong>Unable to set value of column '" + column.UniqueName + "'</strong> - " + ex.Message));
                                        e.Canceled = true;
                                        break;
                                    }
                                }
                                i++;
                            }
                        }
                        if (count == 0)
                        {
                            dt.Rows.Add(dr);
                        }
                        else if (dr["PanNum"].ToString() == "")
                        {
                            dt.Rows.Add(dr);
                        }

                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('PAN Number Already Exists.');", true);
                        }

                        Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                        Rebind();
                    }
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('PAN Number Already Exists.');", true);
                }
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw ex;
            }
        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            Rebind();
        }

        /// <summary>
        /// Used to bind Data to RadGrid
        /// </summary>
        protected void Rebind()
        {
            dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
            RadGrid1.DataSource = dt;
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }
        protected void BindRelation(DropDownList ddList)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "javascript:showmessage();", true);
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int ParentCustomerId = 0;
            int customerId = 0;
            bool bresult;
            bool status = true;
            rmVo = (RMVo)Session["rmVo"];
            string editstatus = "";
            int adviserId = (int)Session["adviserId"];

            if (Session[SessionContents.FPS_AddProspectListActionStatus] != null)
                editstatus = Session[SessionContents.FPS_AddProspectListActionStatus].ToString();

            if (editstatus == "Edit")
            {
                customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                customerVo = customerBo.GetCustomer(customerId);
                if (customerVo.PANNum.ToString() != txtPanNumber.Text.ToString())
                {
                    if (customerBo.PANNumberDuplicateCheck(adviserId, txtPanNumber.Text.ToString(), customerVo.CustomerId))
                    {

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('PAN Number Already Exists.');", true);

                    }
                    else
                    {
                        try
                        {
                            dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                            foreach (GridEditableItem item in RadGrid1.MasterTableView.GetItems(GridItemType.EditItem))
                            {
                                if (item.IsInEditMode)
                                {
                                    status = false;
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Filling Data for Family Members is Incomplete. Please Click Check or Cancel for data in Edit Mode');", true);
                                }
                            }
                            if (status)
                            {
                                if (Validation())
                                {

                                    userVo = new UserVo();
                                    rmVo = (RMVo)Session["rmVo"];
                                    tempuservo = (UserVo)Session["uservo"];
                                    int createdById = tempuservo.UserId;
                                    bresult = DataPopulation(ParentCustomerId, customerId, dt, userVo, rmVo, createdById);
                                    msgRecordStatus.Visible = true;
                                    btnSubmit.Text = "Update";
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :');", true);
                            }

                        }
                        catch (Exception Ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :" + Ex.Message + "');", true);
                        }
                    }
                }
                else
                {
                    try
                    {
                        dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                        foreach (GridEditableItem item in RadGrid1.MasterTableView.GetItems(GridItemType.EditItem))
                        {
                            if (item.IsInEditMode)
                            {
                                status = false;
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Filling Data for Family Members is Incomplete. Please Click Check or Cancel for data in Edit Mode');", true);
                            }
                        }
                        if (status)
                        {
                            if (Validation())
                            {

                                userVo = new UserVo();
                                rmVo = (RMVo)Session["rmVo"];
                                tempuservo = (UserVo)Session["uservo"];
                                int createdById = tempuservo.UserId;
                                bresult = DataPopulation(ParentCustomerId, customerId, dt, userVo, rmVo, createdById);
                                msgRecordStatus.Visible = true;
                                btnSubmit.Text = "Update";
                                btnDelete.Enabled = true;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :');", true);
                        }

                    }
                    catch (Exception Ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :" + Ex.Message + "');", true);
                    }
                }

            }


            if (editstatus != "Edit")
            {
                if (Session[SessionContents.FPS_ProspectList_CustomerId] != null)

                    customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                if (customerId != 0)
                {
                    customerVo = customerBo.GetCustomer(customerId);
                    if (customerVo.PANNum.ToString() != txtPanNumber.Text.ToString())
                    {
                        if (customerBo.PANNumberDuplicateCheck(adviserId, txtPanNumber.Text.ToString(), customerVo.CustomerId))
                        {

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('PAN Number Already Exists.');", true);

                        }
                        else
                        {
                            try
                            {
                                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                                foreach (GridEditableItem item in RadGrid1.MasterTableView.GetItems(GridItemType.EditItem))
                                {
                                    if (item.IsInEditMode)
                                    {
                                        status = false;
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Filling Data for Family Members is Incomplete. Please Click Check or Cancel for data in Edit Mode');", true);
                                    }
                                }
                                if (status)
                                {
                                    if (Validation())
                                    {

                                        userVo = new UserVo();
                                        rmVo = (RMVo)Session["rmVo"];
                                        tempuservo = (UserVo)Session["uservo"];
                                        int createdById = tempuservo.UserId;
                                        bresult = DataPopulation(ParentCustomerId, customerId, dt, userVo, rmVo, createdById);
                                        msgRecordStatus.Visible = true;
                                        btnSubmit.Text = "Update";
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :');", true);
                                }

                            }
                            catch (Exception Ex)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :" + Ex.Message + "');", true);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                            foreach (GridEditableItem item in RadGrid1.MasterTableView.GetItems(GridItemType.EditItem))
                            {
                                if (item.IsInEditMode)
                                {
                                    status = false;
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Filling Data for Family Members is Incomplete. Please Click Check or Cancel for data in Edit Mode');", true);
                                }
                            }
                            if (status)
                            {
                                if (Validation())
                                {

                                    userVo = new UserVo();
                                    rmVo = (RMVo)Session["rmVo"];
                                    tempuservo = (UserVo)Session["uservo"];
                                    int createdById = tempuservo.UserId;
                                    bresult = DataPopulation(ParentCustomerId, customerId, dt, userVo, rmVo, createdById);
                                    msgRecordStatus.Visible = true;
                                    btnSubmit.Text = "Update";
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :');", true);
                            }

                        }
                        catch (Exception Ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :" + Ex.Message + "');", true);
                        }
                    }

                }

                else
                {


                    if (customerBo.PANNumberDuplicateCheck(adviserId, txtPanNumber.Text.ToString(), customerVo.CustomerId))
                    {

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('PAN Number Already Exists.');", true);

                    }



                    else
                    {
                        try
                        {
                            dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                            foreach (GridEditableItem item in RadGrid1.MasterTableView.GetItems(GridItemType.EditItem))
                            {
                                if (item.IsInEditMode)
                                {
                                    status = false;
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Filling Data for Family Members is Incomplete. Please Click Check or Cancel for data in Edit Mode');", true);
                                }
                            }
                            if (status)
                            {
                                if (Validation())
                                {

                                    userVo = new UserVo();
                                    rmVo = (RMVo)Session["rmVo"];
                                    tempuservo = (UserVo)Session["uservo"];
                                    int createdById = tempuservo.UserId;
                                    bresult = DataPopulation(ParentCustomerId, customerId, dt, userVo, rmVo, createdById);
                                    msgRecordStatus.Visible = true;
                                    btnSubmit.Text = "Update";
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :');", true);
                            }

                        }
                        catch (Exception Ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :" + Ex.Message + "');", true);
                        }
                    }
                }
            }
        }
       
    

        /// <summary>
        ///Same set of code has to be used twice so i have created it as Function which will give boolean result
        /// </summary>
        /// <param name="ParentCustomerId"></param>
        /// <param name="customerId"></param>
        /// <param name="dt"></param>
        /// <param name="userVo"></param>
        /// <param name="rmVo"></param>
        /// <param name="createdById"></param>
        /// <returns></returns>
        protected bool DataPopulation(int ParentCustomerId, int customerId, DataTable dt, UserVo userVo, RMVo rmVo, int createdById)
        {
            bool bresult = true;
           
            try
            {

                //Checking whether the Page is for Update or to Submit.

                if (btnSubmit.Text != "Update")
                {
                    ParentCustomerId = CreateCustomerForAddProspect(userVo, rmVo, createdById);
                    Session["FP_ParentCustomerId"] = ParentCustomerId ;
                    Session[SessionContents.FPS_ProspectList_CustomerId] = ParentCustomerId;
                    if (dt != null)
                    {
                        foreach (DataRow drChildCustomers in dt.Rows)
                        {
                            CreateCustomerForAddProspect(userVo, rmVo, createdById, drChildCustomers, ParentCustomerId);
                        }
                    }
                }
                else
                {

                    customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                    if (btnSubmitAddDetails.Text == "Add Finance Details")
                    {
                        dt = CustomerIdList(dt, customerId);
                    }
                    //Updating Parent Customer
                    UpdateCustomerForAddProspect(customerId, false);
                    if (dt != null)
                    {

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["C_CustomerId"] != null && dr["C_CustomerId"].ToString() != "")
                            {
                                //Updating child Customers
                                UpdateCustomerForAddProspect(customerId, dr, false);
                            }
                            else
                            {
                                //Sometimes there might be the Situation that person can add new Client Customers  in Add screen on that situation
                                // this particular function works
                                CreateCustomerForAddProspect(userVo, rmVo, createdById, dr, customerId);
                            }
                        }
                    }
                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentCustomerId);
                    Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;

                }
                bresult = true;
                customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentCustomerId);
                Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;

            }
            catch (Exception ex)
            {
                bresult = false;
            }
            return bresult;

        }

        /// <summary>
        /// Used to Update Parent Customers
        /// </summary>
        /// <param name="customerId"></param>
        /// 
        protected void UpdateCustomerForAddProspect(int customerId, bool convertToCuctomer)
        {
            customerVo = new CustomerVo();

            try
            {
                customerVo.CustomerId = customerId;
                if (userType == "advisor")
                    customerVo.RmId = int.Parse(rcbRM.SelectedValue);
                else if (userType == "rm")
                    customerVo.RmId = rmVo.RMId;
                customerVo.BranchId = int.Parse(ddlPickBranch.SelectedValue);
                customerVo.Type = "IND";
                customerVo.FirstName = txtFirstName.Text.ToString();
                customerVo.MiddleName = txtMiddleName.Text.ToString();
                customerVo.LastName = txtLastName.Text.ToString();
                customerVo.IsProspect = 1;
                customerVo.IsFPClient = 1;
                userVo.FirstName = txtFirstName.Text.ToString();
                userVo.MiddleName = txtMiddleName.Text.ToString();
                userVo.LastName = txtLastName.Text.ToString();

                if (txtSlab.Text != "")
                    customerVo.TaxSlab = int.Parse(txtSlab.Text);

                if (rbtnMale.Checked)
                {
                    customerVo.Gender = "M";
                }
                else if (rbtnFemale.Checked)
                {
                    customerVo.Gender = "F";
                }

                if (dpDOB.SelectedDate != null)
                {
                    customerVo.Dob = dpDOB.SelectedDate.Value;
                }
                customerVo.Email = txtEmail.Text;
                customerVo.PANNum = txtPanNumber.Text;
                customerVo.Adr1Line1 = txtAddress1.Text;
                customerVo.Adr1Line2 = txtAddress2.Text;
                customerVo.Adr1City = txtCity.Text;
                customerVo.Adr1State = txtState.Text;
                customerVo.Adr1Country = txtCountry.Text;
                if (!string.IsNullOrEmpty(txtPinCode.Text))
                {
                    customerVo.Adr1PinCode = int.Parse(txtPinCode.Text);
                }
                if (!string.IsNullOrEmpty(txtMobileNo.Text))
                {
                    customerVo.Mobile1 = Int64.Parse(txtMobileNo.Text);
                }
                if (dpProspectAddDate.SelectedDate != null)
                {
                    customerVo.ProspectAddDate = dpProspectAddDate.SelectedDate;
                }
                if (hdnIsActive.Value == "1")
                {
                    customerVo.IsActive = 1;
                }
                else
                {
                    customerVo.IsActive = 0;
                }
                customerVo.ViaSMS = 1;

                if (dpProspectAddDate.SelectedDate != null)
                {
                    customerVo.ProspectAddDate = dpProspectAddDate.SelectedDate;
                }
                Session[SessionContents.FPS_CustomerProspect_CustomerVo] = customerVo;
                Session["customerVo"] = customerVo;
                Session["CustomerVo"] = customerVo;
                userVo.Email = txtEmail.Text.ToString();
                //if (chkprospect.Checked == true)
                //{
                //    customerPortfolioVo.IsMainPortfolio = 1;
                //    customerPortfolioVo.PortfolioName = "MyPortfolioProspect";
                //}
                //else
                //{
                //    customerPortfolioVo.IsMainPortfolio = 1;
                //    customerPortfolioVo.PortfolioName = "MyPortfolio";
                //}
                customerPortfolioVo.IsMainPortfolio = 1;
                customerPortfolioVo.PortfolioTypeCode = "RGL";

                if (convertToCuctomer == true)
                {
                    customerPortfolioVo.PortfolioName = "MyPortfolio";
                    customerVo.IsProspect = 0;
                }
                else
                {
                    customerPortfolioVo.PortfolioName = "MyPortfolioProspect";
                    customerVo.IsProspect = 1;
                }
                
                customerBo.UpdateCustomer(customerVo,userVo.UserId);
                Session["Customer"] = "Customer";
                Session[SessionContents.CustomerVo] = customerVo;
                Session["customerVo"] = customerVo;
                Session["CustomerVo"] = customerVo;
            }
            catch (Exception ex)
            {

            }

        }



        /// <summary>
        /// Used to update Child Customers
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="drChildCustomer"></param>
        protected void UpdateCustomerForAddProspect(int customerId, DataRow drChildCustomer, bool convertToCuctomer)
        {
            customerVo = new CustomerVo();
            if (drChildCustomer["C_CustomerId"].ToString() != "")
                customerVo.CustomerId = int.Parse(drChildCustomer["C_CustomerId"].ToString());
            else
                customerVo.CustomerId = familyVo.AssociateCustomerId;

            if (userType == "advisor")
                customerVo.RmId = int.Parse(rcbRM.SelectedValue);
            else if (userType == "rm")
                customerVo.RmId = rmVo.RMId;
            customerVo.BranchId = int.Parse(ddlPickBranch.SelectedValue);
            customerVo.Type = "IND";
            customerVo.FirstName = drChildCustomer["FirstName"].ToString();
            customerVo.MiddleName = drChildCustomer["MiddleName"].ToString();
            customerVo.LastName = drChildCustomer["LastName"].ToString();

            if (dpDOB.SelectedDate != null && drChildCustomer["DOB"].ToString() != null && drChildCustomer["DOB"].ToString() != string.Empty)
            {
                customerVo.Dob = DateTime.Parse(drChildCustomer["DOB"].ToString());
            }
            if (hdnIsActive.Value == "1")
            {
                customerVo.IsActive = 1;
            }
            else
            {
                customerVo.IsActive = 0;
            }
            if (hdnIsProspect.Value == "1")
            {
                customerVo.IsProspect = 1;
            }
            else
            {
                customerVo.IsProspect = 0;
            }
            customerVo.Email = drChildCustomer["EmailId"].ToString();
            customerVo.PANNum = drChildCustomer["PanNum"].ToString();
            customerPortfolioVo.IsMainPortfolio = 1;
            customerPortfolioVo.PortfolioTypeCode = "RGL";
            customerPortfolioVo.PortfolioName = "MyPortfolioProspect";

            if (convertToCuctomer == true)
            {
                customerVo.IsProspect = 0;
            }
            else
            {
                customerVo.IsProspect = 1;
            }
            customerVo.ViaSMS = 1;
            customerBo.UpdateCustomer(customerVo,userVo.UserId);
            Session["Customer"] = "Customer";
            if (drChildCustomer["C_CustomerId"].ToString() != "")
            {
                if (int.Parse(drChildCustomer["C_CustomerId"].ToString()) != 0)
                {
                    CustomerFamilyBo familyBo = new CustomerFamilyBo();
                    familyVo.AssociationId = int.Parse(drChildCustomer["CA_AssociationId"].ToString());
                    familyVo.AssociateCustomerId = int.Parse(drChildCustomer["C_CustomerId"].ToString());
                    familyVo.CustomerId = customerId;
                    familyVo.Relationship = drChildCustomer["CustomerRelationship"].ToString();
                    familyBo.UpdateCustomerAssociate(familyVo, customerId, 0);
                }
            }
            //Session[SessionContents.CustomerVo] = customerVo;
            //Session["customerVo"] = customerVo;
            //Session["CustomerVo"] = customerVo;
        }

        /// <summary>
        /// Used to create Parent Customer
        /// </summary>
        /// <param name="userVo"></param>
        /// <param name="rmVo"></param>
        /// <param name="createdById"></param>
        /// <returns></returns>
        protected int CreateCustomerForAddProspect(UserVo userVo, RMVo rmVo, int createdById)
        {
            
                customerVo = new CustomerVo();
                List<int> customerIds = new List<int>();
                try
                {
                    if (userType == "advisor")
                        customerVo.RmId = int.Parse(rcbRM.SelectedValue);
                    else if (userType == "rm")
                        customerVo.RmId = rmVo.RMId;

                    customerVo.BranchId = int.Parse(ddlPickBranch.SelectedValue);
                    customerVo.Type = "IND";
                    customerVo.FirstName = txtFirstName.Text.ToString();
                    customerVo.MiddleName = txtMiddleName.Text.ToString();
                    customerVo.LastName = txtLastName.Text.ToString();
                    userVo.FirstName = txtFirstName.Text.ToString();
                    userVo.MiddleName = txtMiddleName.Text.ToString();
                    userVo.LastName = txtLastName.Text.ToString();
                    
                    if (!string.IsNullOrEmpty(txtSlab.Text.Trim()))
                        customerVo.TaxSlab = int.Parse(txtSlab.Text.Trim());

                    if (rbtnMale.Checked)
                    {
                        customerVo.Gender = "M";
                    }
                    else if (rbtnFemale.Checked)
                    {
                        customerVo.Gender = "F";
                    }

                    if (dpDOB.SelectedDate != null)
                    {
                        customerVo.Dob = dpDOB.SelectedDate.Value;
                    }
                    customerVo.Email = txtEmail.Text;
                    customerVo.IsProspect = 1;
                    customerVo.IsFPClient = 1;
                    customerVo.IsActive = 1;
                    hdnIsProspect.Value = "1";
                    hdnIsActive.Value = "1";
                    customerVo.PANNum = txtPanNumber.Text;
                    customerVo.Adr1Line1 = txtAddress1.Text;
                    customerVo.Adr1Line2 = txtAddress2.Text;
                    customerVo.Adr1City = txtCity.Text;
                    customerVo.Adr1State = txtState.Text;
                    customerVo.Adr1Country = txtCountry.Text;
                    if (!string.IsNullOrEmpty(txtPinCode.Text))
                    {
                        customerVo.Adr1PinCode = int.Parse(txtPinCode.Text);
                    }
                    if (!string.IsNullOrEmpty(txtMobileNo.Text))
                    {
                        customerVo.Mobile1 = Int64.Parse(txtMobileNo.Text);
                    }
                    if (dpProspectAddDate.SelectedDate != null)
                    {
                        customerVo.ProspectAddDate = dpProspectAddDate.SelectedDate;
                    }
                    
                    Session[SessionContents.FPS_CustomerProspect_CustomerVo] = customerVo;
                    userVo.Email = txtEmail.Text.ToString();
                    customerPortfolioVo.IsMainPortfolio = 0;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PortfolioName = "MyPortfolioProspect";
                    customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, createdById);
                    Session["Customer"] = "Customer";
                    Session[SessionContents.CustomerVo] = customerVo;
                    Session["customerVo"] = customerVo;
                    Session["CustomerVo"] = customerVo;
                    if (customerIds != null)
                    {
                        CustomerFamilyVo familyVo = new CustomerFamilyVo();
                        CustomerFamilyBo familyBo = new CustomerFamilyBo();
                        familyVo.AssociateCustomerId = customerIds[1];
                        familyVo.CustomerId = customerIds[1];
                        familyVo.Relationship = "SELF";
                        familyBo.CreateCustomerFamily(familyVo, customerIds[1], userVo.UserId);
                    }
                }
                catch (Exception ex)
                {

                }
            
            return customerIds[1];

        }

        /// <summary>
        /// Used to Create child customers for AddProspect Screen
        /// </summary>
        /// <param name="userVo"></param>
        /// <param name="rmVo"></param>
        /// <param name="createdById"></param>
        /// <param name="drChildCustomer"></param>
        /// <param name="ParentCustomerId"></param>
        protected void CreateCustomerForAddProspect(UserVo userVo, RMVo rmVo, int createdById, DataRow drChildCustomer, int ParentCustomerId)
        {
            customerVo = new CustomerVo();
            if (userType == "advisor")
                customerVo.RmId = int.Parse(rcbRM.SelectedValue);
            else if (userType == "rm")
                customerVo.RmId = rmVo.RMId;
            customerVo.BranchId = int.Parse(ddlPickBranch.SelectedValue);
            customerVo.Type = "IND";
            customerVo.FirstName = drChildCustomer["FirstName"].ToString();
            customerVo.MiddleName = drChildCustomer["MiddleName"].ToString();
            customerVo.LastName = drChildCustomer["LastName"].ToString();
            userVo.FirstName = drChildCustomer["FirstName"].ToString();
            
            if (dpDOB.SelectedDate != null && drChildCustomer["DOB"].ToString() != null && drChildCustomer["DOB"].ToString() != string.Empty)
            {
                customerVo.Dob = DateTime.Parse(drChildCustomer["DOB"].ToString());
            }
            customerVo.Email = drChildCustomer["EmailId"].ToString();
            customerVo.PANNum = drChildCustomer["PanNum"].ToString();
            if (hdnIsActive.Value == "1")
            {
                customerVo.IsActive = 1;
            }
            else
            {
                customerVo.IsActive = 0;
            }
            if (hdnIsProspect.Value == "1")
            {
                customerVo.IsProspect = 1;
            }
            else
            {
                customerVo.IsProspect = 0;
            }


            userVo.Email = drChildCustomer["EmailId"].ToString();
            customerPortfolioVo.IsMainPortfolio = 0;
            customerPortfolioVo.PortfolioTypeCode = "RGL";
            customerPortfolioVo.PortfolioName = "MyPortfolioProspect";
            //Session[SessionContents.CustomerVo] = customerVo;
            //Session["customerVo"] = customerVo;
            //Session["CustomerVo"] = customerVo;
            List<int> customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, createdById);
            if (customerIds != null)
            {
                
                CustomerFamilyBo familyBo = new CustomerFamilyBo();
                familyVo.AssociateCustomerId = customerIds[1];
                familyVo.CustomerId = ParentCustomerId;
                familyVo.Relationship = drChildCustomer["CustomerRelationship"].ToString();
                familyBo.CreateCustomerFamily(familyVo, ParentCustomerId, userVo.UserId);
            }
        }

        protected void btnSubmitAddDetails_Click(object sender, EventArgs e)
        {
            int ParentCustomerId = 0;
            int customerId = 0;
            bool bresult;
            bool status = true;
         
            try
            {
                int adviserId = (int)Session["adviserId"];
                string editstatus = "";
               

                if (Session[SessionContents.FPS_AddProspectListActionStatus] != null)
                    editstatus = Session[SessionContents.FPS_AddProspectListActionStatus].ToString();

                if (editstatus == "Edit")
                {
                    customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                    customerVo = customerBo.GetCustomer(customerId);
                    if (customerVo.PANNum.ToString() != txtPanNumber.Text.ToString())
                    {
                        if (customerBo.PANNumberDuplicateCheck(adviserId, txtPanNumber.Text.ToString(), customerVo.CustomerId))
                        {

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('PAN Number Already Exists.');", true);

                        }
                        else
                        {

                            dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                            foreach (GridEditableItem item in RadGrid1.MasterTableView.GetItems(GridItemType.EditItem))
                            {
                                if (item.IsInEditMode)
                                {
                                    status = false;
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Filling Data for Family Members is Incomplete. Please Click Check or Cancel for data in Edit Mode');", true);
                                }
                            }
                            if (status)
                            {

                                if (Validation())
                                {

                                    userVo = new UserVo();
                                    rmVo = (RMVo)Session["rmVo"];
                                    tempuservo = (UserVo)Session["uservo"];
                                    int createdById = tempuservo.UserId;
                                    bresult = DataPopulation(ParentCustomerId, customerId, dt, userVo, rmVo, createdById);

                                    Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";

                                    Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "Edit";
                                    Session["IsDashboard"] = "FP";
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerIndLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerProspect','login');", true);
                                }
                            }


                            //msgRecordStatus.Visible = true;
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :');", true);
                        }
                    }
                    else
                    {

                        dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                        foreach (GridEditableItem item in RadGrid1.MasterTableView.GetItems(GridItemType.EditItem))
                        {
                            if (item.IsInEditMode)
                            {
                                status = false;
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Filling Data for Family Members is Incomplete. Please Click Check or Cancel for data in Edit Mode');", true);
                            }
                        }
                        if (status)
                        {

                            if (Validation())
                            {

                                userVo = new UserVo();
                                rmVo = (RMVo)Session["rmVo"];
                                tempuservo = (UserVo)Session["uservo"];
                                int createdById = tempuservo.UserId;
                                bresult = DataPopulation(ParentCustomerId, customerId, dt, userVo, rmVo, createdById);

                                Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";

                                Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "Edit";
                                Session["IsDashboard"] = "FP";
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerIndLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerProspect','login');", true);
                            }
                        }


                        //msgRecordStatus.Visible = true;
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :');", true);
                    }


                }
                else
                {

                    dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                    foreach (GridEditableItem item in RadGrid1.MasterTableView.GetItems(GridItemType.EditItem))
                    {
                        if (item.IsInEditMode)
                        {
                            status = false;
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Filling Data for Family Members is Incomplete. Please Click Check or Cancel for data in Edit Mode');", true);
                        }
                    }
                    if (status)
                    {

                        if (Validation())
                        {

                            userVo = new UserVo();
                            rmVo = (RMVo)Session["rmVo"];
                            tempuservo = (UserVo)Session["uservo"];
                            int createdById = tempuservo.UserId;
                            bresult = DataPopulation(ParentCustomerId, customerId, dt, userVo, rmVo, createdById);

                            Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";

                            Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "Edit";
                            Session["IsDashboard"] = "FP";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerIndLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerProspect','login');", true);
                        }
                    }


                    //msgRecordStatus.Visible = true;
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :');", true);
                }
        }
            
            catch (Exception Ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :" + Ex.Message + "');", true);
            }

        }



        /// <summary>
        /// Used to Check validation
        /// </summary>
        /// <returns></returns>
        protected bool Validation()
        {
            //var notValidValidators = Page.Validators.Cast<IValidator>().Where(v => !v.IsValid);
            if (userType == "rm")
            {
                foreach (BaseValidator val in Page.Validators)
                {
                    if (val.ValidationGroup == "ValidateInAdminCase")
                    {
                        val.Enabled = false;
                    }
                }
            }
            if (Page.IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void aplToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string userRole;
            //if(Session["BMCustomer"]!=null)
            //    bmRole=Session["BMCustomer"].ToString();
            //else bmRole=string.Empty;

            userRole = Convert.ToString(Session[SessionContents.CurrentUserRole]);
            Session["IsCustomerGrid"] = "HighlightCustomerNode";

            if (e.Item.Value == "Back")
            {
                if (Session[SessionContents.FPS_AddProspectListActionStatus] != null)
                {
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                }

                if (userRole == "BM")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('BMCustomer','login');", true);
                }
                else if(userRole == "RM")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMCustomer','login');", true);
                }
                else if (userRole == "Admin" || userRole == "Ops"||userRole=="research")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','action=FPClient');", true);
                }
                
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "AdvisorLeftPane", "loadlinks('AdvisorLeftPane','login');", true);
                    
            }
            else if (e.Item.Value == "Edit")
            {
                Session[SessionContents.FPS_AddProspectListActionStatus] = "Edit";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
            }

        }
        protected DataTable CustomerIdList(DataTable dt, int customerId)
        {
            int i = 0;
            List<CustomerFamilyVo> customerFamilyVoList = new List<CustomerFamilyVo>();
            CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
            customerFamilyVoList = customerFamilyBo.GetCustomerFamily(customerId);
            if (customerFamilyVoList != null)
            {
                foreach (CustomerFamilyVo customerfamilyvo in customerFamilyVoList)
                {
                    dt.Rows[i]["C_CustomerId"] = customerfamilyvo.AssociateCustomerId;
                    dt.Rows[i]["CA_AssociationId"] = customerfamilyvo.AssociationId;
                    i++;
                }
            }
            return dt;
        }

        // Update Customer Existing Portfolio..
        private void UpdateCustomerExistingPortfolio(int CustomerId)
        {
            customerPortfolioVo.IsMainPortfolio = 0;
            customerPortfolioVo.PortfolioName = "MyPortfolioProspect";
            customerPortfolioVo.CustomerId = CustomerId;
            portfolioBo.UpdateCustomerPortfolio(customerPortfolioVo, userVo.UserId);
        }

        // Creating Customer Manage PortFolio..
        private void AddCustomerManagePortFolio(int customerId)
        {
            customerPortfolioVo.CustomerId = customerId;
            customerPortfolioVo.IsMainPortfolio = 1;
            customerPortfolioVo.PMSIdentifier = "";
            customerPortfolioVo.PortfolioName = "MyPortfolio";
            customerPortfolioVo.PortfolioTypeCode = "RGL";
            portfolioBo.CreateCustomerPortfolio(customerPortfolioVo, userVo.UserId);
        }
        // To Delete the customers
        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            int customerId = 0;
            string val = Convert.ToString(hdnMsgValue.Value);
            string associationStatus = "";
            if (val == "1")
            {
                if (Session["ChildCustomerId"].ToString() != null)
                {
                    customerId = int.Parse(Session["ChildCustomerId"].ToString());
                    hdnassociationcount.Value = customerBo.CheckAndDeleteTheChildCustomers("C", customerId).ToString();
                    associationStatus = Convert.ToString(hdnassociationcount.Value);
                }
                if (associationStatus == "0")
                {
                    if (customerBo.DeleteChildCustomer(customerId, "D"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddProspectList','login');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "javascript:showassocation();", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showassocation();", true);
                }
            }
        }

        

        // Functions Used for Conver to Customer Functionality: (Added by: Vinayak Patil)
        private void UpdateCustomerPortfolio(int CustomerId)
        {
            customerPortfolioVo.IsMainPortfolio = 0;
            customerPortfolioVo.CustomerId = CustomerId;
            portfolioBo.UpdateCustomerPortfolio(customerPortfolioVo, userVo.UserId);
        }
        private void NewPortFolioInsertion(int customerId)
        {
            customerPortfolioVo.CustomerId = customerId;
            customerPortfolioVo.IsMainPortfolio = 1;
            customerPortfolioVo.PMSIdentifier = "";
            customerPortfolioVo.PortfolioName = "MyPortfolioProspect";
            customerPortfolioVo.PortfolioTypeCode = "RGL";
            portfolioBo.CreateCustomerPortfolio(customerPortfolioVo, userVo.UserId);
        }

        protected void btnConvertToCustomer_Click(object sender, EventArgs e)
        {
            int count = 0;
            dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
            foreach (DataRow drChild in dt.Rows)
            {
                if (drChild["PanNum"].ToString() == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please provide PAN number of associate members');", true);
                }
                else
                    count++;
                
            }
            if (count == dt.Rows.Count)
            {
                int customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());

                //Updating Parent Customer for changing him from Prospect to Non Prospect..
                customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerId);
                int PortFolioId = customerPortfolioVo.PortfolioId;

                //UpdateCustomerExistingPortfolio(PortFolioId);
                AddCustomerManagePortFolio(customerId);
                UpdateCustomerForAddProspect(customerId, true);

                #region Mark Client for FP association 
                CreateFProductionPAssociation(customerId);
                #endregion Mark Client for FP association 


                // Converting all child customers from Prospect to Non Prospect..


                if ((dt.Rows.Count != 0) && (dt != null))
                {
                    int ChildcustomerId = 0;
                    int ChildPortFolioId = 0;
                    foreach (DataRow dr in dt.Rows)
                    {

                        tempuservo = (UserVo)Session["uservo"];
                        int createdById = tempuservo.UserId;

                        if (dr["C_CustomerId"] != null && dr["C_CustomerId"].ToString() != "")
                        {
                            ChildcustomerId = Convert.ToInt32(dr["C_CustomerID"]);
                            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ChildcustomerId);
                            ChildPortFolioId = customerPortfolioVo.PortfolioId;

                            //UpdateCustomerExistingPortfolio(ChildPortFolioId);
                            AddCustomerManagePortFolio(ChildcustomerId);
                            UpdateCustomerForAddProspect(customerId, dr, true);
                            //if (dr["PanNum"].ToString() == "")
                            //{
                            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please provide PAN number of associate members');", true);
                            //    return;
                            //}
                        }
                        else
                        {
                            CreateCustomerForAddProspect(userVo, rmVo, createdById, dr, customerId);
                            ChildcustomerId = familyVo.AssociateCustomerId;
                            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ChildcustomerId);
                            ChildPortFolioId = customerPortfolioVo.PortfolioId;
                            //UpdateCustomerExistingPortfolio(ChildPortFolioId);
                            AddCustomerManagePortFolio(ChildcustomerId);
                            UpdateCustomerForAddProspect(customerId, dr, true);
                        }

                             

                    }
                }
                Session["IsCustomerGrid"] = "HighlightCustomerNode"; 
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','action=FPClient');", true);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "AdvisorLeftPane", "loadlinks('AdvisorLeftPane','login');", true);
            }
            
        }
        private void CreateFProductionPAssociation(int customerId)
        {
            customerBo.CreateProductAssociation(customerId,"FP");
        }
        protected void btnGetSlab_Click(object sender, EventArgs e)
        {
            bool isGenderExist = false;
            //int customerId = 0;
            //if (Session[SessionContents.FPS_ProspectList_CustomerId] != null)
            //{
            //    customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
            //}
            //if ((((customerVo.Gender == "") && (customerVo.Dob == DateTime.MinValue)) && (dpDOB.SelectedDate.ToString() == "")) && ((rbtnMale.Checked == false) && (rbtnFemale.Checked == false)))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select gender and date of birth for the customer to get the tax slab');", true);
            //}
            //if ((customerVo.Gender != "") || ((rbtnMale.Checked != false) || (rbtnFemale.Checked != false)))
            //{
            //    if ((customerVo.Gender == "M") || (rbtnMale.Checked == true))
            //        hdnGender.Value = "Male";
            //    else if ((customerVo.Gender == "F") || (rbtnFemale.Checked == true))
            //        hdnGender.Value = "Female";
            //}
            //if (dpDOB.SelectedDate.ToString() != "")
            //{
            //    CalculateAge(DateTime.Parse(dpDOB.SelectedDate.ToString()));
            //    if ((years < 60) && (hdnGender.Value == ""))
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select gender because customer is not senior citizen');", true);
            //    }
            //    else
            //    {
            //        dsGetSlab = customerBo.GetCustomerTaxSlab(customerId, years, hdnGender.Value);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select date of birth for the customer to get the tax slab');", true);
            //}

            //if (dsGetSlab.Tables.Count != 0)
            //{
            //    if (dsGetSlab.Tables[0].Columns[0].ToString() != "Income")
            //    {
            //        if (dsGetSlab.Tables[0].Rows[0]["WTSR_TaxPer"].ToString() != null)
            //        {
            //            txtSlab.Text = dsGetSlab.Tables[0].Rows[0]["WTSR_TaxPer"].ToString();

            //        }
            //    }
            //    else if ((dsGetSlab.Tables[0].Rows.Count == 0) || (dsGetSlab.Tables[0].Rows[0]["Income"].ToString() == "0.00"))
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please put Income details for the customer to get the tax slab');", true);
            //    }
            //    else if (dsGetSlab.Tables[0].Rows[0]["Income"].ToString() != null)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please provide the proper required customer information to get Tax slab..');", true);
            //    }
            //}
            if(!string.IsNullOrEmpty(dpDOB.SelectedDate.ToString()))
                CalculateAge(DateTime.Parse(dpDOB.SelectedDate.ToString()));

            if ((rbtnFemale.Checked == true || rbtnMale.Checked == true))
            {
                isGenderExist=true;
            }
            if ((!isGenderExist && years < 60) || (string.IsNullOrEmpty(dpDOB.SelectedDate.ToString()) && years < 60))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select gender and date of birth for the customer to get the tax slab');", true);
            }
            else if (!string.IsNullOrEmpty(dpDOB.SelectedDate.ToString()))
            {               

                if ((years < 60) && (!isGenderExist))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select gender because customer is not senior citizen');", true);
                }
                else
                {                    
                    dsGetSlab = customerBo.GetCustomerTaxSlab(customerVo.CustomerId, years, rbtnMale.Checked == true ? "Male" : "Female");
                    if (dsGetSlab.Tables.Count > 0)
                    {
                        if (dsGetSlab.Tables[0].Rows.Count > 0)
                        {
                            if (dsGetSlab.Tables[0].Rows[0]["WTSR_TaxPer"].ToString() != null)
                            {
                                txtSlab.Text = dsGetSlab.Tables[0].Rows[0]["WTSR_TaxPer"].ToString();

                            }
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please put Income details for the customer to get the tax slab');", true);
                    }

                }
            }




        }

        public int CalculateAge(DateTime birthDate)
        {
            DateTime now = DateTime.Today;

            years = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                --years;

            return years;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "javascript:showdeletemessage();", true);
        }
            

        protected void hiddenassociation1_Click(object sender, EventArgs e)
        {
            int customerId;
            string val = Convert.ToString(hdnDeletemsgValue.Value);
            if (val == "1")
            {
                customerId = customerVo.CustomerId;
                //customerId = int.Parse(Session["CustomerIdForDelete"].ToString());
                hdnassociationdeletecount.Value = customerBo.GetAssociationCount("C", customerId).ToString();
                string asc = Convert.ToString(hdnassociationdeletecount.Value);

                if (asc == "0")
                    DeleteCustomerProfile();
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Customer has associations, cannot be deleted');", true);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomer','none');", true);
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
                    if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('BMCustomer','login');", true);
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMCustomer','login');", true);
                    }
                    else if ((Session[SessionContents.CurrentUserRole].ToString() == "Ops"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserCustomer", "loadcontrol('AdviserCustomer','login');", true);
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','login');", true);
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorLeftPane", "loadlinks('AdvisorLeftPane','none');", true);
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
        public bool PANValidation(string Pan)
        {
            bool result = true;
            int adviserId = (int)Session["adviserId"];
            //string childPanNo = RadGrid1.FindControl("txtChildPanNo").ToString();
            if (!string.IsNullOrEmpty(Pan))
            {
               if (customerBo.PANNumberDuplicateChild(adviserId, Pan))
                {
                    result = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('PAN Number Already Exists.');", true);

                }
            }
            return result;
        }
        private void BindBranchDropDown()
        {

            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlPickBranch.DataSource = ds;
                    ddlPickBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlPickBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlPickBranch.DataBind();
                }
                //ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
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
        private void BindRMDropDown()
        {
            try
            {
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
                if (dt.Rows.Count > 0)
                {
                    rcbRM.DataSource = dt;
                    rcbRM.DataValueField = dt.Columns["AR_RMId"].ToString();
                    rcbRM.DataTextField = dt.Columns["RMName"].ToString();
                    rcbRM.DataBind();
                }
                //ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
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

        protected void ddlPickBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (userType == "advisor")
            {
                if (ddlPickBranch.SelectedIndex == -1)
                {
                    BindRMforBranchDropdown(0, bmID);
                }
                else
                {
                    BindRMforBranchDropdown(int.Parse(ddlPickBranch.SelectedValue.ToString()), 0);
                }
            }
        }
        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {

            try
            {

                DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
                if (ds != null)
                {
                    rcbRM.DataSource = ds.Tables[0]; ;
                    rcbRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    rcbRM.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                    rcbRM.DataBind();
                }
                //ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
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
       
    }
}
