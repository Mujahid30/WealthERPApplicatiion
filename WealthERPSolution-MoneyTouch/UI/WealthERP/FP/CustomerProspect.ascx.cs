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
using VoFPSuperlite;
using BoFPSuperlite;
using System.Configuration;
namespace WealthERP.FP
{
    public partial class CustomerProspect : System.Web.UI.UserControl
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
            int customerId = 0;
            CustomerProspectBo customerprospectbo = new CustomerProspectBo();
            SqlDataSourceCustomerRelation.ConnectionString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
            try
            {
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
                BindBranch();
                if (Session[SessionContents.FPS_ProspectList_CustomerId] != null && Session[SessionContents.FPS_ProspectList_CustomerId].ToString() != string.Empty)
                {
                    customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                    customerVo = customerBo.GetCustomer(customerId);
                    Session[SessionContents.CustomerVo] = customerVo;
                    customerFamilyVoList = customerFamilyBo.GetCustomerFamily(customerId);
                    if (customerFamilyVoList != null)
                    {
                        if (!IsPostBack)
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
                                dr["DOB"] = customerFamilyVo.DOB.ToShortDateString();
                                dr["EmailId"] = customerFamilyVo.EmailId;
                                dt.Rows.Add(dr);
                            }
                            Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                        }
                    }
                    else
                    {
                        tblChildCustomer.Visible = false;
                    }
                    txtFirstName.Text = customerVo.FirstName;
                    txtMiddleName.Text = customerVo.MiddleName;
                    txtLastName.Text = customerVo.LastName;
                    //dpDOB.MinDate = DateTime.Parse("01/01/1930 00:00:00");

                    if (customerVo.Dob != DateTime.Parse("01/01/0001 00:00:00"))
                    {
                        dpDOB.SelectedDate = customerVo.Dob;
                    }
                    txtEmail.Text = customerVo.Email;
                    for (int i = 0; i < ddlPickBranch.Items.Count; i++)
                    {
                        if (ddlPickBranch.Items[i].Value == customerVo.BranchId.ToString())
                        {
                            ddlPickBranch.SelectedIndex = i;
                        }
                    }
                    Rebind();
                    Dictionary<string, object> Databuffer = customerprospectbo.Databuffer(customerId);
                    DataRetrival(Databuffer);
                    if (Session[SessionContents.FPS_CustomerPospect_ActionStatus] == null)
                    {
                        Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "View";
                    }

                    if (Session[SessionContents.FPS_CustomerPospect_ActionStatus].ToString() == "View")
                    {
                        // View things have been handled here
                        aplToolBar.Visible = true;
                        //Disabling all Fields
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
                        DisablingControls();
                        btnCustomerProspect.Visible = false;



                    }
                    else if (Session[SessionContents.FPS_CustomerPospect_ActionStatus].ToString() == "Edit")
                    {
                        // Edit thing have been handled here
                        aplToolBar.Visible = true;
                        aplToolBar.Visible = false;
                        //RadToolBaaplToolBarrButton rtb = (RadToolBarButton)aplToolBar.Items.FindItemByValue("Edit");
                        //rtb.Visible = false;
                        btnCustomerProspect.Visible = true;
                        RadGrid1.Columns[RadGrid1.Columns.Count - 1].Visible = false;

                        tblChildCustomer.Visible = true;

                    }
                    //DataRetrival(Databuffer);


                }

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        protected void DisablingControls()
        {
            pnlExpense.Enabled = false;
            pnlGeneralInsurance.Enabled = false;
            pnlIncome.Enabled = false;
            pnlInvestment.Enabled = false;
            pnlLiabilities.Enabled = false;
            pnlLifeInsurance.Enabled = false;
            pnlSummary.Enabled = false;
            btnCustomerProspect.Visible = false;

        }
        /// <summary>
        /// Actually in this screen there are no Delete Command Button. but i have left here because according to specs team in future there
        /// there might be a possiblities of Delete button in grid so thats why i'm leaving this Event as it is. In Future just enable this delete Command
        /// But my suggestion is try connecting this grid directly with Database. so that you will make your coding part as compressed and easy
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Used to bind branches of the Branch dropdown
        /// </summary>
        protected void BindBranch()
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
            //if (e.Item.Value == "Back")
            //{
            //    if (Session[SessionContents.FPS_AddProspectListActionStatus] != null)
            //    {
            //        Session.Remove(SessionContents.FPS_CustomerPospect_ActionStatus);
            //    }
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ProspectList','login');", true);
            //}
            if (e.Item.Value == "Edit")
            {
                Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "Edit";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerProspect','login');", true);
            }
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
            customerVo.RmId = rmVo.RMId;
            customerVo.Type = "IND";
            customerVo.FirstName = txtFirstName.Text.ToString();
            customerVo.MiddleName = txtMiddleName.Text.ToString();
            customerVo.LastName = txtLastName.Text.ToString();
            userVo.FirstName = txtFirstName.Text.ToString();
            userVo.MiddleName = txtMiddleName.Text.ToString();
            userVo.LastName = txtLastName.Text.ToString();
            customerVo.BranchId = int.Parse(ddlPickBranch.SelectedValue);
            customerVo.Dob = dpDOB.SelectedDate.Value;
            customerVo.Email = txtEmail.Text;
            customerVo.IsProspect = 1;
            customerVo.IsFPClient = 1;
            Session[SessionContents.FPS_CustomerProspect_CustomerVo] = customerVo;
            userVo.Email = txtEmail.Text.ToString();
            customerPortfolioVo.IsMainPortfolio = 1;
            customerPortfolioVo.PortfolioTypeCode = "RGL";
            customerPortfolioVo.PortfolioName = "MyPortfolio";
            customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, createdById);
            Session["Customer"] = "Customer";
            if (customerIds != null)
            {
                CustomerFamilyVo familyVo = new CustomerFamilyVo();
                CustomerFamilyBo familyBo = new CustomerFamilyBo();
                familyVo.AssociateCustomerId = customerIds[1];
                familyVo.CustomerId = customerIds[1];
                familyVo.Relationship = "SELF";
                familyBo.CreateCustomerFamily(familyVo, customerIds[1], userVo.UserId);
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
            customerVo.IsProspect = 1;
            customerVo.IsFPClient = 1;
            userVo.Email = drChildCustomer["EmailId"].ToString();
            customerPortfolioVo.IsMainPortfolio = 1;
            customerPortfolioVo.PortfolioTypeCode = "RGL";
            customerPortfolioVo.PortfolioName = "MyPortfolio";
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
        protected void btnCustomerProspect_Click(object sender, EventArgs e)
        {
            int ParentCustomerId = 0;
            int customerId = 0;
            bool bresult;
            bool bdetails;
            bool status = true;
            double totalincome = 0.0;
            double totalExpense = 0.0;
            double totalLiabilities = 0.0;
            double totalLoanOutstanding = 0.0;
            double instrumentTotal = 0.0;
            double subInstrumentTotal = 0.0;

            BoFPSuperlite.CustomerProspectBo customerProspectBo = new CustomerProspectBo();

            try
            {
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                ParentCustomerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
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
                        // Updating user details
                        bresult = DataPopulation(ParentCustomerId, customerId, dt, userVo, rmVo, createdById);

                        //Sending Asset Details to business Logic
                        // there we are seperating
                        //parent customerid is nothing but customerid
                        bdetails = customerProspectBo.DataManipulationInput(DataCapture(), ParentCustomerId, createdById, out totalincome, out totalExpense, out totalLiabilities, out totalLoanOutstanding, out instrumentTotal, out subInstrumentTotal);
                        if (bresult == true && bdetails == true)
                        {
                            msgRecordStatus.Visible = true;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Something Went Wrong \n Record Status: Unsuccessful');", true);
                        }
                    }
                }
                Dictionary<string, object> Databuffer = customerProspectBo.Databuffer(ParentCustomerId);
                DataRetrival(Databuffer);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerType.ascx:btnSubmit_Click()");
                object[] objects = new object[4];
                objects[0] = ParentCustomerId;
                objects[1] = customerVo;
                objects[2] = rmVo;
                objects[3] = userVo;
                objects[4] = customerPortfolioVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        /// <summary>
        /// Will return Dictionary of complete data for Asset and Financial Details        /// 
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, object> DataCapture()
        {
            double totalincome = 0.0;
            double totalexpense = 0.0;
            double totalliabilities = 0.0;
            double totalasset = 0.0;
            double totalli = 0.0;
            double totalgi = 0.0;
            CustomerProspectVo customerprospectvo = new CustomerProspectVo();
            Dictionary<string, object> datacapturelist = new Dictionary<string, object>();
            try
            {
                //Income
                //==========================================================================================================================
                VoFPSuperlite.CustomerProspectIncomeDetailsVo incomedetailsvo;
                List<CustomerProspectIncomeDetailsVo> incomedetailsvolist = new List<CustomerProspectIncomeDetailsVo>();
                //Salary
                if (txtSalary.Text != string.Empty)
                {
                    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                    incomedetailsvo.IncomeCategoryCode = 1;
                    incomedetailsvo.IncomeValue = double.Parse(txtSalary.Text);
                    incomedetailsvolist.Add(incomedetailsvo);
                    totalincome += incomedetailsvo.IncomeValue;
                }
                //Rental Property
                if (txtRentalProperty.Text != string.Empty)
                {
                    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                    incomedetailsvo.IncomeCategoryCode = 2;
                    incomedetailsvo.IncomeValue = double.Parse(txtRentalProperty.Text);
                    incomedetailsvolist.Add(incomedetailsvo);
                    totalincome += incomedetailsvo.IncomeValue;
                }
                //Agriculture
                if (txtAgriculturalIncome.Text != string.Empty)
                {
                    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                    incomedetailsvo.IncomeCategoryCode = 3;
                    incomedetailsvo.IncomeValue = double.Parse(txtAgriculturalIncome.Text);
                    incomedetailsvolist.Add(incomedetailsvo);
                    totalincome += incomedetailsvo.IncomeValue;
                }
                //Business & Profession
                if (txtBusinessAndProfession.Text != string.Empty)
                {
                    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                    incomedetailsvo.IncomeCategoryCode = 4;
                    incomedetailsvo.IncomeValue = double.Parse(txtBusinessAndProfession.Text);
                    incomedetailsvolist.Add(incomedetailsvo);
                    totalincome += incomedetailsvo.IncomeValue;
                }
                //CapitalGain
                if (txtCapitalGains.Text != string.Empty)
                {
                    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                    incomedetailsvo.IncomeCategoryCode = 5;
                    incomedetailsvo.IncomeValue = double.Parse(txtCapitalGains.Text);
                    incomedetailsvolist.Add(incomedetailsvo);
                    totalincome += incomedetailsvo.IncomeValue;
                }
                //IncomeOthers
                if (txtOthersIncome.Text != string.Empty)
                {
                    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                    incomedetailsvo.IncomeCategoryCode = 6;
                    incomedetailsvo.IncomeValue = double.Parse(txtOthersIncome.Text);
                    incomedetailsvolist.Add(incomedetailsvo);
                    totalincome += incomedetailsvo.IncomeValue;
                }
                //==========================================================================================================================

                //Expense
                //==========================================================================================================================
                VoFPSuperlite.CustomerProspectExpenseDetailsVo expensedetailsvo;
                List<CustomerProspectExpenseDetailsVo> expensedetailsvolist = new List<CustomerProspectExpenseDetailsVo>();
                //Transportation = Nothing but Conveyance
                if (txtConveyance.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 1;
                    expensedetailsvo.ExpenseValue = double.Parse(txtConveyance.Text);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Food
                if (txtFood.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 2;
                    expensedetailsvo.ExpenseValue = double.Parse(txtFood.Text);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Rent
                if (txtRent.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 3;
                    expensedetailsvo.ExpenseValue = double.Parse(txtRent.Text);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Utilities
                if (txtUtilites.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 4;
                    expensedetailsvo.ExpenseValue = double.Parse(txtUtilites.Text);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Health & Personal Care
                if (txtHealthPersonalCare.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 5;
                    expensedetailsvo.ExpenseValue = double.Parse(txtHealthPersonalCare.Text);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Personal Wear
                if (txtPersonalWear.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 6;
                    expensedetailsvo.ExpenseValue = double.Parse(txtPersonalWear.Text);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Entertainment & Holidays
                if (txtEntertainmentHolidays.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 7;
                    expensedetailsvo.ExpenseValue = double.Parse(txtEntertainmentHolidays.Text);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Domestic Help
                if (txtDomesticHelp.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 8;
                    expensedetailsvo.ExpenseValue = double.Parse(txtDomesticHelp.Text);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Insurance Premium
                if (txtInsurance.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 9;
                    expensedetailsvo.ExpenseValue = double.Parse(txtInsurance.Text);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Other Expenses
                if (txtOthersExpense.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 10;
                    expensedetailsvo.ExpenseValue = double.Parse(txtOthersExpense.Text);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //==========================================================================================================================


                //Liabilities(Loan)
                //==========================================================================================================================
                VoFPSuperlite.CustomerProspectLiabilitiesDetailsVo liabilitiesdetailsvo;
                List<CustomerProspectLiabilitiesDetailsVo> liabilitiesdetailsvolist = new List<CustomerProspectLiabilitiesDetailsVo>();
                //Home Loan

                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 1;
                if (txtHomeLoanLO.Text != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = double.Parse(txtHomeLoanLO.Text);
                }
                if (txtHomeLoanT.Text != string.Empty)
                {
                    liabilitiesdetailsvo.Tenure = int.Parse(txtHomeLoanT.Text);
                }
                if (txtHomeLoanEMI.Text != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = double.Parse(txtHomeLoanEMI.Text);
                }
                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);
                totalli += liabilitiesdetailsvo.LoanOutstanding;

                //Auto Loan

                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 2;
                if (txtAutoLoanLO.Text != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = double.Parse(txtAutoLoanLO.Text);
                }
                if (txtAutoLoanT.Text != string.Empty)
                {
                    liabilitiesdetailsvo.Tenure = int.Parse(txtAutoLoanT.Text);
                }
                if (txtAutoLoanEMI.Text != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = double.Parse(txtAutoLoanEMI.Text);
                }
                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);
                totalli += liabilitiesdetailsvo.LoanOutstanding;

                //Educational Loan

                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 5;
                if (txtEducationLoanLO.Text != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = double.Parse(txtEducationLoanLO.Text);
                }
                if (txtEducationLoanT.Text != string.Empty)
                {
                    liabilitiesdetailsvo.Tenure = int.Parse(txtEducationLoanT.Text);
                }
                if (txtEducationLoanEMI.Text != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = double.Parse(txtEducationLoanEMI.Text);
                }
                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);
                totalli += liabilitiesdetailsvo.LoanOutstanding;

                //Personal Loan

                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 6;
                if (txtPersonalLoanLO.Text != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = double.Parse(txtPersonalLoanLO.Text);
                }
                if (txtPersonalLoanT.Text != string.Empty)
                {
                    liabilitiesdetailsvo.Tenure = int.Parse(txtPersonalLoanT.Text);
                }
                if (txtPersonalLoanEMI.Text != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = double.Parse(txtPersonalLoanEMI.Text);
                }
                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);
                totalli += liabilitiesdetailsvo.LoanOutstanding;

                //Other Loan
                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 9;
                if (txtOtherLoanLO.Text != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = double.Parse(txtOtherLoanLO.Text);
                }
                if (txtOtherLoanT.Text != string.Empty)
                {
                    liabilitiesdetailsvo.Tenure = int.Parse(txtOtherLoanT.Text);
                }
                if (txtOtherLoanEMI.Text != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = double.Parse(txtOtherLoanEMI.Text);
                }
                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);
                totalli += liabilitiesdetailsvo.LoanOutstanding;
                //==========================================================================================================================

                //Investment
                //==========================================================================================================================
                VoFPSuperlite.CustomerProspectAssetDetailsVo assetdetailsvo;
                VoFPSuperlite.CustomerProspectAssetSubDetailsVo assetdetailssubvo;
                List<CustomerProspectAssetDetailsVo> assetdetailsvolist = new List<CustomerProspectAssetDetailsVo>();
                List<CustomerProspectAssetSubDetailsVo> assetdetailssubvolist = new List<CustomerProspectAssetSubDetailsVo>();

                //Direct Equtiy(Level1)
                if (txtDirectEquity.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "DE";
                    assetdetailsvo.Value = double.Parse(txtDirectEquity.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //MF-Equity(Level1)
                if (txtMFEquity.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "MF";
                    assetdetailsvo.AssetInstrumentCategoryCode = "MFEQ";
                    assetdetailsvo.Value = double.Parse(txtMFEquity.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //MF-Debt(Level1)
                if (txtMFDebt.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "MF";
                    assetdetailsvo.AssetInstrumentCategoryCode = "MFDT";
                    assetdetailsvo.Value = double.Parse(txtMFDebt.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //MF Hybrid -Equity(Level2)
                if (txtMFHybridEquity.Text != string.Empty)
                {
                    assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                    assetdetailssubvo.AssetGroupCode = "MF";
                    assetdetailssubvo.AssetInstrumentCategoryCode = "MFHY";
                    assetdetailssubvo.AssetInstrumentSubCategoryCode = "MFHYEQ";
                    assetdetailssubvo.Value = double.Parse(txtMFHybridEquity.Text);
                    assetdetailssubvolist.Add(assetdetailssubvo);
                    totalasset += assetdetailssubvo.Value;
                }
                //MF Hybrid -Debt(Level2)
                if (txtMFHybridDebt.Text != string.Empty)
                {
                    assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                    assetdetailssubvo.AssetGroupCode = "MF";
                    assetdetailssubvo.AssetInstrumentCategoryCode = "MFHY";
                    assetdetailssubvo.AssetInstrumentSubCategoryCode = "MFHYDT";
                    assetdetailssubvo.Value = double.Parse(txtMFHybridDebt.Text);
                    assetdetailssubvolist.Add(assetdetailssubvo);
                    totalasset += assetdetailssubvo.Value;
                }
                //Fixed Income
                if (txtFixedIncome.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "FI";
                    assetdetailsvo.Value = double.Parse(txtFixedIncome.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //Govt Savings
                if (txtGovtSavings.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "GS";
                    assetdetailsvo.Value = double.Parse(txtGovtSavings.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //Pension & Gratuities
                if (txtPensionGratuities.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "PG";
                    assetdetailsvo.Value = double.Parse(txtPensionGratuities.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //Property
                if (txtProperty.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "PR";
                    assetdetailsvo.Value = double.Parse(txtProperty.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //Gold
                if (txtGold.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "GD";
                    assetdetailsvo.Value = double.Parse(txtGold.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //Collectibles
                if (txtCollectibles.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "CL";
                    assetdetailsvo.Value = double.Parse(txtCollectibles.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //Private Equity
                if (txtPrivateEquity.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "PE";
                    assetdetailsvo.Value = double.Parse(txtPrivateEquity.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //PMS
                if (txtPMS.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "PM";
                    assetdetailsvo.Value = double.Parse(txtPMS.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //Cash and Savings
                if (txtPMS.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "CS";
                    assetdetailsvo.Value = double.Parse(txtCashAndSavings.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //Structured Product
                if (txtPMS.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "SP";
                    assetdetailsvo.Value = double.Parse(txtStructuredProduct.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //Commodities
                if (txtPMS.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "CM";
                    assetdetailsvo.Value = double.Parse(txtCommodities.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //Others
                if (txtPMS.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "OT";
                    assetdetailsvo.Value = double.Parse(txtInvestmentsOthers.Text);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //==========================================================================================================================

                //Life Insurance
                //==========================================================================================================================
                //Term 

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INTP";
                if (txtTermSA.Text != string.Empty)
                {
                    assetdetailsvo.Value = double.Parse(txtTermSA.Text);
                }
                if (txtTermP.Text != string.Empty)
                {
                    assetdetailsvo.Premium = double.Parse(txtTermP.Text);
                }
                assetdetailsvo.MaturityDate = dpTermLIMD.SelectedDate;
                assetdetailsvolist.Add(assetdetailsvo);
                totalli += assetdetailsvo.Value;

                //Endowment

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INEP";
                if (txtEndowmentSA.Text != string.Empty)
                {
                    assetdetailsvo.Value = double.Parse(txtEndowmentSA.Text);
                }
                if (txtEndowmentP.Text != string.Empty)
                {
                    assetdetailsvo.Premium = double.Parse(txtEndowmentP.Text);
                }
                assetdetailsvo.MaturityDate = dpEndowmentLIMD.SelectedDate;
                assetdetailsvolist.Add(assetdetailsvo);
                totalli += assetdetailsvo.Value;

                //Whole Life

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INWP";
                if (txtWholeLifeSA.Text != string.Empty)
                {
                    assetdetailsvo.Value = double.Parse(txtWholeLifeSA.Text);
                }
                if (txtWholeLifeP.Text != string.Empty)
                {
                    assetdetailsvo.Premium = double.Parse(txtWholeLifeP.Text);
                }
                assetdetailsvo.MaturityDate = dpWholeLifeLIMD.SelectedDate;
                assetdetailsvolist.Add(assetdetailsvo);
                totalli += assetdetailsvo.Value;

                //Money Back

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INMP";
                if (txtMoneyBackSA.Text != string.Empty)
                {
                    assetdetailsvo.Value = double.Parse(txtMoneyBackSA.Text);
                }
                if (txtMoneyBackP.Text != string.Empty)
                {
                    assetdetailsvo.Premium = double.Parse(txtMoneyBackP.Text);
                }
                assetdetailsvo.MaturityDate = dpMoneyBackLIMD.SelectedDate;
                assetdetailsvolist.Add(assetdetailsvo);
                totalli += assetdetailsvo.Value;

                //ULIP

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INUP";
                if (txtULIPSA.Text != string.Empty)
                {
                    assetdetailsvo.Value = double.Parse(txtULIPSA.Text);
                }
                if (txtULIPP.Text != string.Empty)
                {
                    assetdetailsvo.Premium = double.Parse(txtULIPP.Text);
                }
                assetdetailsvo.MaturityDate = dpULIPSLIMD.SelectedDate;
                assetdetailsvolist.Add(assetdetailsvo);
                totalli += assetdetailsvo.Value;

                //Others

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INOT";
                if (txtOthersLISA.Text != string.Empty)
                {
                    assetdetailsvo.Value = double.Parse(txtOthersLISA.Text);
                }
                if (txtOthersLIP.Text != string.Empty)
                {
                    assetdetailsvo.Premium = double.Parse(txtOthersLIP.Text);
                }
                assetdetailsvo.MaturityDate = dpOthersLIMD.SelectedDate;
                assetdetailsvolist.Add(assetdetailsvo);
                totalli += assetdetailsvo.Value;

                //==========================================================================================================================

                //General Insurance
                //==========================================================================================================================
                //Health Insurance cover  

                assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                assetdetailssubvo.AssetGroupCode = "GI";
                assetdetailssubvo.AssetInstrumentCategoryCode = "GIRI";
                assetdetailssubvo.AssetInstrumentSubCategoryCode = "GIRIHM";
                if (txtHealthInsuranceCoverSA.Text != string.Empty )
                {
                    assetdetailssubvo.Value = double.Parse(txtHealthInsuranceCoverSA.Text);
                }
                if (txtHealthInsuranceCoverP.Text != string.Empty)
                {
                    assetdetailssubvo.Premium = double.Parse(txtHealthInsuranceCoverP.Text);
                }
                assetdetailssubvo.MaturityDate = dpHealthInsuranceCoverGIMD.SelectedDate;
                assetdetailssubvolist.Add(assetdetailssubvo);
                totalgi += assetdetailssubvo.Value;

                //Property Insurance Cover  

                assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                assetdetailssubvo.AssetGroupCode = "GI";
                assetdetailssubvo.AssetInstrumentCategoryCode = "GIRI";
                assetdetailssubvo.AssetInstrumentSubCategoryCode = "GIRIHO";
                if (txtPropertyInsuranceCoverSA.Text != string.Empty)
                {
                    assetdetailssubvo.Value = double.Parse(txtPropertyInsuranceCoverSA.Text);
                }
                if (txtPropertyInsuranceCoverP.Text != string.Empty)
                {
                    assetdetailssubvo.Premium = double.Parse(txtPropertyInsuranceCoverP.Text);
                }
                assetdetailssubvo.MaturityDate = dpPropertyInsuranceCoverGIMD.SelectedDate;
                assetdetailssubvolist.Add(assetdetailssubvo);
                totalgi += assetdetailssubvo.Value;

                //Personal Accident           

                assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                assetdetailssubvo.AssetGroupCode = "GI";
                assetdetailssubvo.AssetInstrumentCategoryCode = "GIRI";
                assetdetailssubvo.AssetInstrumentSubCategoryCode = "GIRIPA";
                if (txtPersonalAccidentSA.Text != string.Empty)
                {
                    assetdetailssubvo.Value = double.Parse(txtPersonalAccidentSA.Text);
                }
                if (txtPersonalAccidentP.Text != string.Empty)
                {
                    assetdetailssubvo.Premium = double.Parse(txtPersonalAccidentP.Text);
                }
                assetdetailssubvo.MaturityDate = dpPersonalAccidentGIMD.SelectedDate;
                assetdetailssubvolist.Add(assetdetailssubvo);
                totalgi += assetdetailssubvo.Value;

                //Others

                assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                assetdetailssubvo.AssetGroupCode = "GI";
                assetdetailssubvo.AssetInstrumentCategoryCode = "GIRI";
                assetdetailssubvo.AssetInstrumentSubCategoryCode = "GIRIOT";
                if (txtOthersGISA.Text != string.Empty)
                {
                    assetdetailssubvo.Value = double.Parse(txtOthersGISA.Text);
                }
                if (txtOthersGIP.Text != string.Empty)
                {
                    assetdetailssubvo.Premium = double.Parse(txtOthersGIP.Text);
                }
                assetdetailssubvo.MaturityDate = dpOthersGIMD.SelectedDate;
                assetdetailssubvolist.Add(assetdetailssubvo);
                totalgi += assetdetailssubvo.Value;

                //==========================================================================================================================
                //Main total Details Summing up
                //==========================================================================================================================
                customerprospectvo.TotalAssets = totalasset;
                customerprospectvo.TotalExpense = totalexpense;
                customerprospectvo.TotalGeneralInsurance = totalgi;
                customerprospectvo.TotalIncome = totalincome;
                customerprospectvo.TotalLiabilities = totalli;
                customerprospectvo.TotalLifeInsurance = totalliabilities;
                //==========================================================================================================================
                //Bundling up               
                if (incomedetailsvolist != null)
                {
                    datacapturelist.Add("IncomeList", incomedetailsvolist);
                }
                if (expensedetailsvolist != null)
                {
                    datacapturelist.Add("ExpenseList", expensedetailsvolist);
                }
                if (liabilitiesdetailsvolist != null)
                {
                    datacapturelist.Add("Liabilities", liabilitiesdetailsvolist);
                }
                if (assetdetailsvolist != null)
                {
                    datacapturelist.Add("AssetDetails", assetdetailsvolist);
                }
                if (assetdetailssubvolist != null)
                {
                    datacapturelist.Add("AssetSubDetails", assetdetailssubvolist);
                }
                if (customerprospectvo != null)
                {
                    datacapturelist.Add("TotalAssetDetails", customerprospectvo);
                }

            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return datacapturelist;

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

                customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                //Updating Parent Customer
                UpdateCustomerForAddProspect(customerId);
                if (dt != null)
                {
                    customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                   
                        dt = CustomerIdList(dt, customerId);
                    
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


                bresult = true;

            }
            catch (Exception ex)
            {
                bresult = false;
            }
            return bresult;

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
                }
            }
            return dt;
        }
        /// <summary>
        /// Used to Update Parent Customers
        /// </summary>
        /// <param name="customerId"></param>
        /// 
        protected void UpdateCustomerForAddProspect(int customerId)
        {
            customerVo = new CustomerVo();
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
            Session[SessionContents.FPS_CustomerProspect_CustomerVo] = customerVo;
            userVo.Email = txtEmail.Text.ToString();
            customerPortfolioVo.IsMainPortfolio = 1;
            customerPortfolioVo.PortfolioTypeCode = "RGL";
            customerPortfolioVo.PortfolioName = "MyPortfolio";
            customerBo.UpdateCustomer(customerVo);
            Session["Customer"] = "Customer";

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
            customerVo.IsProspect = 1;
            customerVo.IsFPClient = 1;
            customerVo.Email = drChildCustomer["EmailId"].ToString();
            customerPortfolioVo.IsMainPortfolio = 1;
            customerPortfolioVo.PortfolioTypeCode = "RGL";
            customerPortfolioVo.PortfolioName = "MyPortfolio";
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

        }

        protected void DataRetrival(Dictionary<string, object> Databuffer)
        {
            double totalincome = 0.0;
            double totalexpense = 0.0;
            double totalliabilities = 0.0;
            double totalasset = 0.0;
            double totalli = 0.0;
            double totalgi = 0.0;
            CustomerProspectVo customerprospectvo = new CustomerProspectVo();
            List<CustomerProspectIncomeDetailsVo> IncomeDetailsForCustomerProspect = Databuffer["IncomeDetailsList"] as List<CustomerProspectIncomeDetailsVo>;
            List<CustomerProspectExpenseDetailsVo> ExpenseDetailsForCustomerProspect = Databuffer["ExpenseDetailsList"] as List<CustomerProspectExpenseDetailsVo>;
            List<CustomerProspectLiabilitiesDetailsVo> LiabilitiesDetailsForCustomerProspect = Databuffer["LiabilitiesDetailsList"] as List<CustomerProspectLiabilitiesDetailsVo>;
            List<CustomerProspectAssetSubDetailsVo> CustomerFPAssetSubInstrumentDetails = Databuffer["AssetInstrumentSubDetailsList"] as List<CustomerProspectAssetSubDetailsVo>;
            List<CustomerProspectAssetDetailsVo> CustomerFPAssetInstrumentDetails = Databuffer["AssetInstrumentDetailsList"] as List<CustomerProspectAssetDetailsVo>;

            # region
            if (IncomeDetailsForCustomerProspect != null && IncomeDetailsForCustomerProspect.Count > 0)
            {
                foreach (CustomerProspectIncomeDetailsVo cpid in IncomeDetailsForCustomerProspect)
                {
                    if (cpid.IncomeCategoryCode == 1)
                    {
                        txtSalary.Text = cpid.IncomeValue.ToString();
                        totalincome += cpid.IncomeValue;
                    }
                    if (cpid.IncomeCategoryCode == 2)
                    {
                        txtRentalProperty.Text = cpid.IncomeValue.ToString();
                        totalincome += cpid.IncomeValue;
                    }
                    if (cpid.IncomeCategoryCode == 3)
                    {
                        txtAgriculturalIncome.Text = cpid.IncomeValue.ToString();
                        totalincome += cpid.IncomeValue;
                    }
                    if (cpid.IncomeCategoryCode == 4)
                    {
                        txtBusinessAndProfession.Text = cpid.IncomeValue.ToString();
                        totalincome += cpid.IncomeValue;
                    }
                    if (cpid.IncomeCategoryCode == 5)
                    {
                        txtCapitalGains.Text = cpid.IncomeValue.ToString();
                        totalincome += cpid.IncomeValue;
                    }
                    if (cpid.IncomeCategoryCode == 6)
                    {
                        txtOthersIncome.Text = cpid.IncomeValue.ToString();
                        totalincome += cpid.IncomeValue;
                    }

                }
            }

            if (ExpenseDetailsForCustomerProspect != null && ExpenseDetailsForCustomerProspect.Count > 0)
            {
                foreach (CustomerProspectExpenseDetailsVo cped in ExpenseDetailsForCustomerProspect)
                {
                    if (cped.ExpenseCategoryCode == 1)
                    {
                        txtConveyance.Text = cped.ExpenseValue.ToString();
                        totalexpense += cped.ExpenseValue;
                    }
                    if (cped.ExpenseCategoryCode == 2)
                    {
                        txtFood.Text = cped.ExpenseValue.ToString();
                        totalexpense += cped.ExpenseValue;
                    }
                    if (cped.ExpenseCategoryCode == 3)
                    {
                        txtRent.Text = cped.ExpenseValue.ToString();
                        totalexpense += cped.ExpenseValue;
                    }
                    if (cped.ExpenseCategoryCode == 4)
                    {
                        txtUtilites.Text = cped.ExpenseValue.ToString();
                        totalexpense += cped.ExpenseValue;
                    }
                    if (cped.ExpenseCategoryCode == 5)
                    {
                        txtHealthPersonalCare.Text = cped.ExpenseValue.ToString();
                        totalexpense += cped.ExpenseValue;
                    }
                    if (cped.ExpenseCategoryCode == 6)
                    {
                        txtPersonalWear.Text = cped.ExpenseValue.ToString();
                        totalexpense += cped.ExpenseValue;
                    }
                    if (cped.ExpenseCategoryCode == 7)
                    {
                        txtEntertainmentHolidays.Text = cped.ExpenseValue.ToString();
                        totalexpense += cped.ExpenseValue;
                    }
                    if (cped.ExpenseCategoryCode == 8)
                    {
                        txtDomesticHelp.Text = cped.ExpenseValue.ToString();
                        totalexpense += cped.ExpenseValue;
                    }
                    if (cped.ExpenseCategoryCode == 9)
                    {
                        txtInsurance.Text = cped.ExpenseValue.ToString();
                        totalexpense += cped.ExpenseValue;
                    }
                    if (cped.ExpenseCategoryCode == 10)
                    {
                        txtOthersExpense.Text = cped.ExpenseValue.ToString();
                        totalexpense += cped.ExpenseValue;
                    }

                }
            }


            if (LiabilitiesDetailsForCustomerProspect != null && LiabilitiesDetailsForCustomerProspect.Count > 0)
            {
                foreach (CustomerProspectLiabilitiesDetailsVo cpld in LiabilitiesDetailsForCustomerProspect)
                {
                    if (cpld.LoanTypeCode == 1)
                    {
                        txtHomeLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtHomeLoanT.Text = cpld.Tenure.ToString();
                        txtHomeLoanEMI.Text = cpld.EMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }
                    if (cpld.LoanTypeCode == 2)
                    {
                        txtAutoLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtAutoLoanT.Text = cpld.Tenure.ToString();
                        txtAutoLoanEMI.Text = cpld.EMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }

                    if (cpld.LoanTypeCode == 5)
                    {
                        txtEducationLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtEducationLoanT.Text = cpld.Tenure.ToString();
                        txtEducationLoanEMI.Text = cpld.EMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }
                    if (cpld.LoanTypeCode == 6)
                    {
                        txtPersonalLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtPersonalLoanT.Text = cpld.Tenure.ToString();
                        txtPersonalLoanEMI.Text = cpld.EMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }
                    if (cpld.LoanTypeCode == 9)
                    {
                        txtOtherLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtOtherLoanT.Text = cpld.Tenure.ToString();
                        txtOtherLoanEMI.Text = cpld.EMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }

                }
            }
            # endregion

            if (CustomerFPAssetInstrumentDetails != null && CustomerFPAssetInstrumentDetails.Count > 0)
            {
                foreach (CustomerProspectAssetDetailsVo cpad in CustomerFPAssetInstrumentDetails)
                {
                    if (cpad.AssetGroupCode == "DE")
                    {
                        txtDirectEquity.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "MF" && cpad.AssetInstrumentCategoryCode == "MFEQ")
                    {
                        txtMFEquity.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "MF" && cpad.AssetInstrumentCategoryCode == "MFDT")
                    {
                        txtMFDebt.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "FI")
                    {
                        txtFixedIncome.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "GS")
                    {
                        txtGovtSavings.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "PG")
                    {
                        txtPensionGratuities.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "PR")
                    {
                        txtProperty.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "GD")
                    {
                        txtGold.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "CL")
                    {
                        txtCollectibles.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "CS")
                    {
                        txtCashAndSavings.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "SP")
                    {
                        txtStructuredProduct.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "CM")
                    {
                        txtCommodities.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "PE")
                    {
                        txtPrivateEquity.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "PM")
                    {
                        txtPMS.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "OT")
                    {
                        txtInvestmentsOthers.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    //Life Insurance
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INTP")
                    {
                        txtTermSA.Text = cpad.Value.ToString();
                        txtTermP.Text = cpad.Premium.ToString();
                        if (cpad.MaturityDate.HasValue)
                        {
                            dpTermLIMD.SelectedDate = cpad.MaturityDate;
                        }
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INEP")
                    {
                        txtEndowmentSA.Text = cpad.Value.ToString();
                        txtEndowmentP.Text = cpad.Premium.ToString();
                        if (cpad.MaturityDate.HasValue)
                        {
                            dpEndowmentLIMD.SelectedDate = cpad.MaturityDate;
                        }
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INWP")
                    {
                        txtWholeLifeSA.Text = cpad.Value.ToString();
                        txtWholeLifeP.Text = cpad.Premium.ToString();
                        if (cpad.MaturityDate.HasValue)
                        {
                            dpWholeLifeLIMD.SelectedDate = cpad.MaturityDate;
                        }
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INMP")
                    {
                        txtMoneyBackSA.Text = cpad.Value.ToString();
                        txtMoneyBackP.Text = cpad.Premium.ToString();
                        if (cpad.MaturityDate.HasValue)
                        {
                            dpMoneyBackLIMD.SelectedDate = cpad.MaturityDate;
                        }
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INUP")
                    {
                        txtULIPSA.Text = cpad.Value.ToString();
                        txtULIPP.Text = cpad.Premium.ToString();
                        if (cpad.MaturityDate.HasValue)
                        {
                            dpULIPSLIMD.SelectedDate = cpad.MaturityDate;
                        }
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INOT")
                    {
                        txtOthersLISA.Text = cpad.Value.ToString();
                        txtOthersLIP.Text = cpad.Premium.ToString();
                        if (cpad.MaturityDate.HasValue)
                        {
                            dpOthersLIMD.SelectedDate = cpad.MaturityDate;
                        }
                        totalli += cpad.Value;
                    }
                }
            }

            if (CustomerFPAssetSubInstrumentDetails != null && CustomerFPAssetSubInstrumentDetails.Count > 0)
            {
                foreach (CustomerProspectAssetSubDetailsVo cpasd in CustomerFPAssetSubInstrumentDetails)
                {
                    if (cpasd.AssetGroupCode == "GI" && cpasd.AssetInstrumentCategoryCode == "GIRI" && cpasd.AssetInstrumentSubCategoryCode == "GIRIHM")
                    {
                        txtHealthInsuranceCoverSA.Text = cpasd.Value.ToString();
                        txtHealthInsuranceCoverP.Text = cpasd.Premium.ToString();
                        if (cpasd.MaturityDate.HasValue)
                        {
                            dpHealthInsuranceCoverGIMD.SelectedDate = cpasd.MaturityDate;
                        }
                        totalgi += cpasd.Value;

                    }
                    if (cpasd.AssetGroupCode == "GI" && cpasd.AssetInstrumentCategoryCode == "GIRI" && cpasd.AssetInstrumentSubCategoryCode == "GIRIHO")
                    {
                        txtPropertyInsuranceCoverSA.Text = cpasd.Value.ToString();
                        txtPropertyInsuranceCoverP.Text = cpasd.Premium.ToString();
                        if (cpasd.MaturityDate.HasValue)
                        {
                            dpPropertyInsuranceCoverGIMD.SelectedDate = cpasd.MaturityDate;
                        }
                        totalgi += cpasd.Value;

                    }
                    if (cpasd.AssetGroupCode == "GI" && cpasd.AssetInstrumentCategoryCode == "GIRI" && cpasd.AssetInstrumentSubCategoryCode == "GIRIPA")
                    {
                        txtPersonalAccidentSA.Text = cpasd.Value.ToString();
                        txtPersonalAccidentP.Text = cpasd.Premium.ToString();
                        if (cpasd.MaturityDate.HasValue)
                        {
                            dpPersonalAccidentGIMD.SelectedDate = cpasd.MaturityDate;
                        }
                        totalgi += cpasd.Value;

                    }
                    if (cpasd.AssetGroupCode == "GI" && cpasd.AssetInstrumentCategoryCode == "GIRI" && cpasd.AssetInstrumentSubCategoryCode == "GIRIOT")
                    {
                        txtOthersGISA.Text = cpasd.Value.ToString();
                        txtOthersGIP.Text = cpasd.Premium.ToString();
                        if (cpasd.MaturityDate.HasValue)
                        {
                            dpOthersGIMD.SelectedDate = cpasd.MaturityDate;
                        }
                        totalgi += cpasd.Value;

                    }
                    if (cpasd.AssetGroupCode == "MF" && cpasd.AssetInstrumentCategoryCode == "MFHY" && cpasd.AssetInstrumentSubCategoryCode == "MFHYEQ")
                    {
                        txtMFHybridEquity.Text = cpasd.Value.ToString();

                        totalasset += cpasd.Value;
                    }
                    if (cpasd.AssetGroupCode == "MF" && cpasd.AssetInstrumentCategoryCode == "MFHY" && cpasd.AssetInstrumentSubCategoryCode == "MFHYDT")
                    {
                        txtMFHybridDebt.Text = cpasd.Value.ToString();
                        totalasset += cpasd.Value;

                    }
                    txtAssets.Text = totalasset.ToString();
                    txtIncome.Text = totalincome.ToString();
                    txtExpense.Text = totalexpense.ToString();
                    txtLiabilities.Text = totalliabilities.ToString();
                    txtLifeInsurance.Text = totalli.ToString();
                    txtGeneralInsurance.Text = totalgi.ToString();

                    txtAssetTotal.Text = totalasset.ToString();
                    txtIncomeTotal.Text = totalincome.ToString();
                    txtExpenseTotal.Text = totalexpense.ToString();
                    txtTotalLO.Text = totalliabilities.ToString();
                    txtTotalLISA.Text = totalli.ToString();
                    txtTotalGISA.Text = totalgi.ToString();

                }
            }

        }



    }
}