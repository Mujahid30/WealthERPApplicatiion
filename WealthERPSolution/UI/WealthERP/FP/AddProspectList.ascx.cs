using System;
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
        UserVo userVo = new UserVo();
        UserVo tempuservo;
        DataTable dt = new DataTable();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        DataRow dr;

        //For Edit 
        int totalRecordsCount;
        protected void Page_Init()
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                int customerId = 0;
                advisorVo = (AdvisorVo)Session["advisorVo"];

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
                    Session[SessionContents.FPS_AddProspect_DataTable] = dt;

                }
                else
                {
                    dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                }

                //SqlDataSourceCustomerRelation.ConnectionString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
                rmVo = (RMVo)Session["rmVo"];
                BindBranch(advisorVo, rmVo);
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
                    for (int i = 0; i < ddlPickBranch.Items.Count; i++)
                    {
                        if (ddlPickBranch.Items[i].Value == customerVo.BranchId.ToString())
                        {
                            ddlPickBranch.SelectedIndex = i;
                        }
                    }
                    Rebind();
                    if (Session[SessionContents.FPS_AddProspectListActionStatus].ToString() == "View")
                    {
                        // View things have been handled here
                        aplToolBar.Visible = true;
                        btnSubmit.Visible = false;
                        btnSubmitAddDetails.Visible = false;
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
                        headertitle.Text = "View Prospect";

                    }
                    else if (Session[SessionContents.FPS_AddProspectListActionStatus].ToString() == "Edit")
                    {
                        // Edit thing have been handled here
                        aplToolBar.Visible = true;
                        RadToolBarButton rtb = (RadToolBarButton)aplToolBar.Items.FindItemByValue("Edit");
                        rtb.Visible = false;
                        btnSubmit.Visible = true;
                        btnSubmitAddDetails.Visible = true;
                        btnSubmit.Text = "Update";
                        btnSubmitAddDetails.Text = "Edit Finance Details";
                        RadGrid1.Columns[RadGrid1.Columns.Count - 1].Visible = false;
                        tblChildCustomer.Visible = true;
                        headertitle.Text = "Edit Prospect";
                    }
                }
                RadGrid1.Columns[RadGrid1.Columns.Count - 1].Visible = false;

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }




        /// <summary>        
        /// Used to bind branches of the Branch dropdown       
        /// </summary>
        /// <param name="advisorVo"></param>
        /// <param name="rmVo"></param>
        private void BindBranch(AdvisorVo advisorVo, RMVo rmVo)
        {
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            UploadCommonBo uploadsCommonDao = new UploadCommonBo();
            //DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
            DataSet ds = advisorBranchBo.GetRMBranchAssociation(rmVo.RMId, advisorVo.advisorId, "A");
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
                    dt.Rows[e.Item.ItemIndex].Delete();
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
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditManager editMan = editedItem.EditManager;
                int i = 2;
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                dr = dt.NewRow();
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
                                if (i != 3)
                                {
                                    TextBox txt = (TextBox)e.Item.FindControl("txtGridEmailId");
                                    editorText = txt.Text;
                                    editorValue = txt.Text;
                                }
                                else if (i == 3)
                                {
                                    TextBox txt = (TextBox)e.Item.FindControl("txtChildFirstName");
                                    editorText = txt.Text;
                                    editorValue = txt.Text;
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
                Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                Rebind();
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
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                dr = dt.NewRow();
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
                                if (i != 3)
                                {
                                    TextBox txt = (TextBox)e.Item.FindControl("txtGridEmailId");
                                    editorText = txt.Text;
                                    editorValue = txt.Text;
                                }
                                else if (i == 3)
                                {
                                    TextBox txt = (TextBox)e.Item.FindControl("txtChildFirstName");
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
                dt.Rows.Add(dr);
                Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                Rebind();
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

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int ParentCustomerId = 0;
            int customerId = 0;
            bool bresult;
            bool status = true;
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
                    UpdateCustomerForAddProspect(customerId);
                    if (dt != null)
                    {

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["C_CustomerId"] != null && dr["C_CustomerId"].ToString() != "")
                            {
                                //Updating child Customers
                                UpdateCustomerForAddProspect(customerId, dr);
                            }
                            else
                            {
                                //Sometimes there might be the Situation that person can add new Client Customers  in Add screen on that situation
                                // this particular function works
                                CreateCustomerForAddProspect(userVo, rmVo, createdById, dr, customerId);
                            }
                        }
                    }

                }
                bresult = true;

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
        protected void UpdateCustomerForAddProspect(int customerId)
        {
            customerVo = new CustomerVo();
            
            try
            {
                customerVo.CustomerId = customerId;
                customerVo.RmId = rmVo.RMId;
                customerVo.Type = "IND";
                customerVo.FirstName = txtFirstName.Text.ToString();
                customerVo.MiddleName = txtMiddleName.Text.ToString();
                customerVo.LastName = txtLastName.Text.ToString();
                customerVo.IsProspect = 1;
                customerVo.IsFPClient = 1;
                userVo.FirstName = txtFirstName.Text.ToString();
                userVo.MiddleName = txtMiddleName.Text.ToString();
                userVo.LastName = txtLastName.Text.ToString();
                customerVo.BranchId = int.Parse(ddlPickBranch.SelectedValue);
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
                if (hdnIsProspect.Value == "1")
                {
                    customerVo.IsProspect = 1;
                }
                else
                {
                    customerVo.IsProspect = 0;
                }
                if (dpProspectAddDate.SelectedDate != null)
                {
                    customerVo.ProspectAddDate = dpProspectAddDate.SelectedDate;
                }
                Session[SessionContents.FPS_CustomerProspect_CustomerVo] = customerVo;
                Session["customerVo"] = customerVo;
                Session["CustomerVo"] = customerVo;
                userVo.Email = txtEmail.Text.ToString();
                customerPortfolioVo.IsMainPortfolio = 1;
                customerPortfolioVo.PortfolioTypeCode = "RGL";
                customerPortfolioVo.PortfolioName = "MyPortfolioUnmanaged";
                customerBo.UpdateCustomer(customerVo);
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
        protected void UpdateCustomerForAddProspect(int customerId, DataRow drChildCustomer)
        {
            customerVo = new CustomerVo();
            customerVo.CustomerId = int.Parse(drChildCustomer["C_CustomerId"].ToString());
            customerVo.RmId = rmVo.RMId;
            customerVo.Type = "IND";
            customerVo.FirstName = drChildCustomer["FirstName"].ToString();
            customerVo.MiddleName = drChildCustomer["MiddleName"].ToString();
            customerVo.LastName = drChildCustomer["LastName"].ToString();
            customerVo.BranchId = int.Parse(ddlPickBranch.SelectedValue);
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
            customerPortfolioVo.IsMainPortfolio = 1;
            customerPortfolioVo.PortfolioTypeCode = "RGL";
            customerPortfolioVo.PortfolioName = "MyPortfolioUnmanaged";
            customerBo.UpdateCustomer(customerVo);
            Session["Customer"] = "Customer";
            if (drChildCustomer["C_CustomerId"] != null)
            {
                if (int.Parse(drChildCustomer["C_CustomerId"].ToString()) != 0)
                {
                    CustomerFamilyVo familyVo = new CustomerFamilyVo();
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
                    customerVo.RmId = rmVo.RMId;
                    customerVo.Type = "IND";
                    customerVo.FirstName = txtFirstName.Text.ToString();
                    customerVo.MiddleName = txtMiddleName.Text.ToString();
                    customerVo.LastName = txtLastName.Text.ToString();
                    userVo.FirstName = txtFirstName.Text.ToString();
                    userVo.MiddleName = txtMiddleName.Text.ToString();
                    userVo.LastName = txtLastName.Text.ToString();
                    customerVo.BranchId = int.Parse(ddlPickBranch.SelectedValue);
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
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PortfolioName = "MyPortfolioUnmanaged";
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
            customerVo.RmId = rmVo.RMId;
            customerVo.Type = "IND";
            customerVo.FirstName = drChildCustomer["FirstName"].ToString();
            customerVo.MiddleName = drChildCustomer["MiddleName"].ToString();
            customerVo.LastName = drChildCustomer["LastName"].ToString();
            userVo.FirstName = drChildCustomer["FirstName"].ToString();
            customerVo.BranchId = int.Parse(ddlPickBranch.SelectedValue);
            if (dpDOB.SelectedDate != null && drChildCustomer["DOB"].ToString() != null && drChildCustomer["DOB"].ToString() != string.Empty)
            {
                customerVo.Dob = DateTime.Parse(drChildCustomer["DOB"].ToString());
            }
            customerVo.Email = drChildCustomer["EmailId"].ToString();
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
            customerPortfolioVo.IsMainPortfolio = 1;
            customerPortfolioVo.PortfolioTypeCode = "RGL";
            customerPortfolioVo.PortfolioName = "MyPortfolioUnmanaged";
            //Session[SessionContents.CustomerVo] = customerVo;
            //Session["customerVo"] = customerVo;
            //Session["CustomerVo"] = customerVo;
            List<int> customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, createdById);
            if (customerIds != null)
            {
                CustomerFamilyVo familyVo = new CustomerFamilyVo();
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

                    }
                }
                Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";

                Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "Edit";
                
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerProspect','login');", true);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerIndLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
                //msgRecordStatus.Visible = true;
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Something Went Wrong \n Record Status: Unsuccessful \n Error Details :');", true);
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
            if (e.Item.Value == "Back")
            {
                if (Session[SessionContents.FPS_AddProspectListActionStatus] != null)
                {
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                }
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','login');", true);
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

    }
}