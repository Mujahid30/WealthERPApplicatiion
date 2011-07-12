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
using BoAdvisorProfiling;
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
        int ParentCustomerId = 0;

        //For Edit 
        int totalRecordsCount;
        float totalEMIForExpense = 0;
        float totalLIPremium = 0;
        float totalGIPremium = 0;
        float finalPremiumtotal = 0;
        float finalExpenseEMItotal = 0;
        BoFPSuperlite.CustomerProspectBo customerProspectBo = new CustomerProspectBo();
        protected void Page_Init()
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            int customerId = 0;
            CustomerProspectBo customerprospectbo = new CustomerProspectBo();
            //SqlDataSourceCustomerRelation.ConnectionString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
            try
            {
                advisorVo = (AdvisorVo)Session["advisorVo"];
                txtWERPDirectEquityM.Attributes.Add("onChange", "javascript:SubTotal(this);");

                //if (!IsPostBack)
                //{
                //    dt = new DataTable();
                //    dt.Columns.Add("C_CustomerId");
                //    dt.Columns.Add("CA_AssociationId");
                //    dt.Columns.Add("CustomerRelationship");
                //    dt.Columns.Add("FirstName");
                //    dt.Columns.Add("MiddleName");
                //    dt.Columns.Add("LastName");
                //    dt.Columns.Add("DOB");
                //    dt.Columns.Add("EmailId");
                //    Session[SessionContents.FPS_AddProspect_DataTable] = dt;

                //}
                //else
                //{
                //    dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                //}
                rmVo = (RMVo)Session["rmVo"];
                //BindBranch(advisorVo, rmVo);
                if ((Session[SessionContents.FPS_ProspectList_CustomerId] != null) && (Session[SessionContents.FPS_ProspectList_CustomerId].ToString() != string.Empty))
                {
                    customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                    customerVo = customerBo.GetCustomer(customerId);
                    hdnIsActive.Value = customerVo.IsActive.ToString();
                    hdnIsProspect.Value = customerVo.IsProspect.ToString();
                    Session[SessionContents.CustomerVo] = customerVo;
                    customerFamilyVoList = customerFamilyBo.GetCustomerFamily(customerId);
                    //if (customerFamilyVoList != null)
                    //{
                    //    if (!IsPostBack)
                    //    {
                    //        totalRecordsCount = customerFamilyVoList.Count;
                    //        dt.Rows.Clear();
                    //        foreach (CustomerFamilyVo customerFamilyVo in customerFamilyVoList)
                    //        {
                    //            DataRow dr = dt.NewRow();
                    //            dr["CA_AssociationId"] = customerFamilyVo.AssociationId;
                    //            dr["C_CustomerId"] = customerFamilyVo.AssociateCustomerId;
                    //            dr["CustomerRelationship"] = customerFamilyVo.RelationshipCode;
                    //            dr["FirstName"] = customerFamilyVo.FirstName;
                    //            dr["MiddleName"] = customerFamilyVo.MiddleName;
                    //            dr["LastName"] = customerFamilyVo.LastName;
                    //            if (customerFamilyVo.DOB != DateTime.Parse("01/01/0001 00:00:00"))
                    //            {
                    //                dr["DOB"] = customerFamilyVo.DOB.ToShortDateString();
                    //            }
                    //            dr["EmailId"] = customerFamilyVo.EmailId;
                    //            dt.Rows.Add(dr);
                    //        }
                    //        Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                    //    }
                    //}
                    //else
                    //{
                        //tblChildCustomer.Visible = false;
                    //}
                    //txtFirstName.Text = customerVo.FirstName;
                    //txtMiddleName.Text = customerVo.MiddleName;
                    //txtLastName.Text = customerVo.LastName;
                    ////dpDOB.MinDate = DateTime.Parse("01/01/1930 00:00:00");

                    //if (customerVo.Dob != DateTime.Parse("01/01/0001 00:00:00"))
                    //{
                    //    dpDOB.SelectedDate = customerVo.Dob;
                    //}
                    //txtEmail.Text = customerVo.Email;
                    //txtPanNumber.Text = customerVo.PANNum;
                    //txtAddress1.Text = customerVo.Adr1Line1;
                    //txtAddress2.Text = customerVo.Adr1Line2;
                    //txtMobileNo.Text = customerVo.Mobile1.ToString();
                    //txtPinCode.Text = customerVo.Adr1PinCode.ToString();
                    //txtCity.Text = customerVo.Adr1City;
                    //txtState.Text = customerVo.Adr1State;
                    //txtCountry.Text = customerVo.Adr1Country;
                    //if (customerVo.ProspectAddDate != DateTime.Parse("01/01/0001 00:00:00") && customerVo.ProspectAddDate != null)
                    //{
                    //    dpProspectAddDate.SelectedDate = customerVo.ProspectAddDate;
                    //}
                    //for (int i = 0; i < ddlPickBranch.Items.Count; i++)
                    //{
                    //    if (ddlPickBranch.Items[i].Value == customerVo.BranchId.ToString())
                    //    {
                    //        ddlPickBranch.SelectedIndex = i;
                    //    }
                    //}
                    //Rebind();
                    if (Session[SessionContents.FPS_CustomerPospect_ActionStatus] == null)
                    {
                        Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "View";
                    }

                    if (Session[SessionContents.FPS_CustomerPospect_ActionStatus].ToString() == "View")
                    {
                        // View things have been handled here
                        aplToolBar.Visible = true;
                        aplToolBar.Controls[1].Visible = false;
                        //Disabling all Fields
                        //if (customerFamilyVoList != null)
                        //{
                        //    RadGrid1.Columns[RadGrid1.Columns.Count - 1].Visible = false;
                        //    RadGrid1.Columns[0].Visible = false;
                        //    RadGrid1.AllowAutomaticInserts = false;
                        //    RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                        //    ChildCustomerGridPanel.Enabled = false;
                        //}
                        //else
                        //{
                        //    ChildCustomerGridPanel.Visible = false;
                        //}
                        DisablingControls();
                        btnCustomerProspect.Visible = false;



                    }
                    else if (Session[SessionContents.FPS_CustomerPospect_ActionStatus].ToString() == "Edit")
                    {
                        // Edit thing have been handled here
                        aplToolBar.Visible = true;
                        aplToolBar.Controls[0].Visible = false;
                        //aplToolBar.Visible = false;
                        //RadToolBaaplToolBarrButton rtb = (RadToolBarButton)aplToolBar.Items.FindItemByValue("Edit");
                        //rtb.Visible = false;
                        btnCustomerProspect.Visible = true;
                        //RadGrid1.Columns[RadGrid1.Columns.Count - 1].Visible = false;

                        //tblChildCustomer.Visible = true;

                    }
                    //DataRetrival(Databuffer);


                }
                txtDirectEquity.Attributes.Add("readonly", "readonly");
                txtMFEquity.Attributes.Add("readonly", "readonly");
                txtMFDebt.Attributes.Add("readonly", "readonly");
                txtMFHybridDebt.Attributes.Add("readonly", "readonly");
                txtMFHybridEquity.Attributes.Add("readonly", "readonly");
                txtFixedIncome.Attributes.Add("readonly", "readonly");
                txtGovtSavings.Attributes.Add("readonly", "readonly");
                txtPensionGratuities.Attributes.Add("readonly", "readonly");
                txtProperty.Attributes.Add("readonly", "readonly");
                txtGold.Attributes.Add("readonly", "readonly");
                txtCollectibles.Attributes.Add("readonly", "readonly");
                txtStructuredProduct.Attributes.Add("readonly", "readonly");
                txtCommodities.Attributes.Add("readonly", "readonly");
                txtCashAndSavings.Attributes.Add("readonly", "readonly");
                txtPrivateEquity.Attributes.Add("readonly", "readonly");
                txtPMS.Attributes.Add("readonly", "readonly");
                txtInvestmentsOthers.Attributes.Add("readonly", "readonly");
                txtTotalTermSA.Attributes.Add("readonly", "readonly");
                txtTotalWholeLifeSA.Attributes.Add("readonly", "readonly");
                txtTotalMoneyBackSA.Attributes.Add("readonly", "readonly");
                txtTotalULIPSA.Attributes.Add("readonly", "readonly");
                txtTotalOthersLISA.Attributes.Add("readonly", "readonly");
                txtHealthInsuranceCoverSA.Attributes.Add("readonly", "readonly");
                txtPropertyInsuranceCoverSA.Attributes.Add("readonly", "readonly");
                txtTotalOthersLISA.Attributes.Add("readonly", "readonly");
                txtHomeLoanLO.Attributes.Add("readonly", "readonly");
                txtAutoLoanLO.Attributes.Add("readonly", "readonly");
                txtPersonalLoanLO.Attributes.Add("readonly", "readonly");
                txtEducationLoanLO.Attributes.Add("readonly", "readonly");
                txtOthersGISA.Attributes.Add("readonly", "readonly");

            }
            catch (Exception ex)
            {
                throw ex;

            }

            if (!IsPostBack)
            {
                customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                AdvisorVo advisorvo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                ManagedUnmanagedDetails(customerId, advisorvo.advisorId, 1);

                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                ParentCustomerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                Dictionary<string, object> Databuffer = customerProspectBo.Databuffer(ParentCustomerId);
                DataRetrival(Databuffer);
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
            //pnlSummary.Enabled = false;
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

        //protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        GridEditableItem editedItem = e.Item as GridEditableItem;
        //        GridEditManager editMan = editedItem.EditManager;
        //        dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
        //        dr = dt.NewRow();
        //        try
        //        {
        //            dt.Rows[e.Item.ItemIndex].Delete();
        //        }
        //        catch (Exception ex)
        //        {
        //            RadGrid1.Controls.Add(new LiteralControl("<strong>Unable to delete</strong>"));
        //            e.Canceled = true;

        //        }
        //        Rebind();
        //    }
        //    catch (Exception ex)
        //    {
        //        e.Canceled = true;
        //        throw ex;
        //    }
        //}


        //protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        GridEditableItem editedItem = e.Item as GridEditableItem;
        //        GridEditManager editMan = editedItem.EditManager;
        //        int i = 2;
        //        dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
        //        dr = dt.NewRow();
        //        foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
        //        {

        //            if (column is IGridEditableColumn)
        //            {
        //                IGridEditableColumn editableCol = (column as IGridEditableColumn);
        //                if (editableCol.IsEditable)
        //                {
        //                    IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);
        //                    string editorType = editor.ToString();
        //                    string editorText = "unknown";
        //                    object editorValue = null;

        //                    if (editor is GridTextColumnEditor)
        //                    {
        //                        editorText = (editor as GridTextColumnEditor).Text;
        //                        editorValue = (editor as GridTextColumnEditor).Text;
        //                    }
        //                    if (editor is GridBoolColumnEditor)
        //                    {
        //                        editorText = (editor as GridBoolColumnEditor).Value.ToString();
        //                        editorValue = (editor as GridBoolColumnEditor).Value;
        //                    }
        //                    if (editor is GridDropDownColumnEditor)
        //                    {
        //                        editorText = (editor as GridDropDownColumnEditor).SelectedValue;
        //                        editorValue = (editor as GridDropDownColumnEditor).SelectedValue;
        //                    }
        //                    if (editor is GridTemplateColumnEditor)
        //                    {
        //                        if (i != 3)
        //                        {
        //                            TextBox txt = (TextBox)e.Item.FindControl("txtGridEmailId");
        //                            editorText = txt.Text;
        //                            editorValue = txt.Text;
        //                        }
        //                        else if (i == 3)
        //                        {
        //                            TextBox txt = (TextBox)e.Item.FindControl("txtChildFirstName");
        //                            editorText = txt.Text;
        //                            editorValue = txt.Text;
        //                        }

        //                    }
        //                    try
        //                    {
        //                        DataRow[] changedrows = dt.Select();
        //                        changedrows[editedItem.ItemIndex][column.UniqueName] = editorValue;


        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        RadGrid1.Controls.Add(new LiteralControl("<strong>Unable to set value of column '" + column.UniqueName + "'</strong> - " + ex.Message));
        //                        e.Canceled = true;
        //                        break;
        //                    }
        //                }
        //                i++;
        //            }

        //        }
        //        Session[SessionContents.FPS_AddProspect_DataTable] = dt;
        //        Rebind();
        //    }
        //    catch (Exception ex)
        //    {
        //        e.Canceled = true;
        //        throw ex;
        //    }
        //}

        //protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        //{


        //    try
        //    {
        //        GridEditableItem editedItem = e.Item as GridEditableItem;
        //        GridEditManager editMan = editedItem.EditManager;
        //        int i = 2;
        //        int j = 0;
        //        dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
        //        dr = dt.NewRow();
        //        foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
        //        {
        //            if (column is IGridEditableColumn)
        //            {
        //                IGridEditableColumn editableCol = (column as IGridEditableColumn);
        //                if (editableCol.IsEditable)
        //                {
        //                    IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);
        //                    string editorType = editor.ToString();
        //                    string editorText = "unknown";
        //                    object editorValue = null;

        //                    if (editor is GridTextColumnEditor)
        //                    {
        //                        editorText = (editor as GridTextColumnEditor).Text;
        //                        editorValue = (editor as GridTextColumnEditor).Text;
        //                    }
        //                    if (editor is GridBoolColumnEditor)
        //                    {
        //                        editorText = (editor as GridBoolColumnEditor).Value.ToString();
        //                        editorValue = (editor as GridBoolColumnEditor).Value;
        //                    }
        //                    if (editor is GridDropDownColumnEditor)
        //                    {
        //                        editorText = (editor as GridDropDownColumnEditor).SelectedValue;
        //                        editorValue = (editor as GridDropDownColumnEditor).SelectedValue;
        //                    }
        //                    if (editor is GridTemplateColumnEditor)
        //                    {
        //                        if (i != 3)
        //                        {
        //                            TextBox txt = (TextBox)e.Item.FindControl("txtGridEmailId");
        //                            editorText = txt.Text;
        //                            editorValue = txt.Text;
        //                        }
        //                        else if (i == 3)
        //                        {
        //                            TextBox txt = (TextBox)e.Item.FindControl("txtChildFirstName");
        //                            editorText = txt.Text;
        //                            editorValue = txt.Text;
        //                        }

        //                    }
        //                    try
        //                    {
        //                        dr[i] = editorText;

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        RadGrid1.Controls.Add(new LiteralControl("<strong>Unable to set value of column '" + column.UniqueName + "'</strong> - " + ex.Message));
        //                        e.Canceled = true;
        //                        break;
        //                    }
        //                }
        //                i++;
        //            }
        //        }
        //        dt.Rows.Add(dr);
        //        Session[SessionContents.FPS_AddProspect_DataTable] = dt;
        //        Rebind();
        //    }
        //    catch (Exception ex)
        //    {
        //        e.Canceled = true;
        //        throw ex;
        //    }
        //}
        //protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{
        //    Rebind();
        //}

        ///// <summary>
        ///// Used to bind Data to RadGrid
        ///// </summary>
        //protected void Rebind()
        //{
        //    dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
        //    RadGrid1.DataSource = dt;
        //}
        //protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        //{

        //}
        //protected void BindRelation(DropDownList ddList)
        //{

        //}

        /// <summary>        
        /// Used to bind branches of the Branch dropdown       
        /// </summary>
        /// <param name="advisorVo"></param>
        /// <param name="rmVo"></param>
        //private void BindBranch(AdvisorVo advisorVo, RMVo rmVo)
        //{
        //    AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        //    UploadCommonBo uploadsCommonDao = new UploadCommonBo();
        //    //DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
        //    DataSet ds = advisorBranchBo.GetRMBranchAssociation(rmVo.RMId, advisorVo.advisorId, "A");
        //    if (ds != null)
        //    {
        //        ddlPickBranch.DataSource = ds;
        //        ddlPickBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
        //        ddlPickBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
        //        ddlPickBranch.DataBind();
        //    }
        //}

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
            if (e.Item.Value == "Edit")
            {
                //if (RadTabStrip1.TabIndex != 0)
                //{
                Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "Edit";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerProspect','login');", true);
                //}
            }
            if (e.Item.Value == "Synchronize")
            {
                int customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                AdvisorVo advisorvo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                if (advisorvo != null)
                {
                    ManagedUnmanagedDetails(customerId, advisorvo.advisorId, 1);

                    dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                    ParentCustomerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                    Dictionary<string, object> Databuffer = customerProspectBo.Databuffer(ParentCustomerId);
                    DataRetrival(Databuffer);
                }
            }

        }


        /// <summary>
        /// Used to create Parent Customer
        /// </summary>
        /// <param name="userVo"></param>
        /// <param name="rmVo"></param>
        /// <param name="createdById"></param>
        /// <returns></returns>
        //protected int CreateCustomerForAddProspect(UserVo userVo, RMVo rmVo, int createdById)
        //{
        //    customerVo = new CustomerVo();
        //    List<int> customerIds = new List<int>();
        //    customerVo.RmId = rmVo.RMId;
        //    customerVo.Type = "IND";
        //    customerVo.FirstName = txtFirstName.Text.ToString();
        //    customerVo.MiddleName = txtMiddleName.Text.ToString();
        //    customerVo.LastName = txtLastName.Text.ToString();
        //    userVo.FirstName = txtFirstName.Text.ToString();
        //    userVo.MiddleName = txtMiddleName.Text.ToString();
        //    userVo.LastName = txtLastName.Text.ToString();
        //    customerVo.BranchId = int.Parse(ddlPickBranch.SelectedValue);
        //    customerVo.Dob = dpDOB.SelectedDate.Value;
        //    customerVo.Email = txtEmail.Text;
        //    if (hdnIsActive.Value == "1")
        //    {
        //        customerVo.IsActive = 1;
        //    }
        //    else
        //    {
        //        customerVo.IsActive = 0;
        //    }
        //    if (hdnIsProspect.Value == "1")
        //    {
        //        customerVo.IsProspect = 1;
        //    }
        //    else
        //    {
        //        customerVo.IsProspect = 0;
        //    }
        //    customerVo.IsFPClient = 1;
        //    customerVo.PANNum = txtPanNumber.Text;
        //    customerVo.Adr1Line1 = txtAddress1.Text;
        //    customerVo.Adr1Line2 = txtAddress2.Text;
        //    customerVo.Adr1City = txtCity.Text;
        //    customerVo.Adr1State = txtState.Text;
        //    customerVo.Adr1Country = txtCountry.Text;
        //    if (!string.IsNullOrEmpty(txtPinCode.Text))
        //    {
        //        customerVo.Adr1PinCode = int.Parse(txtPinCode.Text);
        //    }
        //    if (!string.IsNullOrEmpty(txtMobileNo.Text))
        //    {
        //        customerVo.Mobile1 = Int64.Parse(txtMobileNo.Text);
        //    }
        //    if (dpProspectAddDate.SelectedDate != null)
        //    {
        //        customerVo.ProspectAddDate = dpProspectAddDate.SelectedDate;
        //    }

        //    Session[SessionContents.FPS_CustomerProspect_CustomerVo] = customerVo;
        //    userVo.Email = txtEmail.Text.ToString();
        //    customerPortfolioVo.IsMainPortfolio = 1;
        //    customerPortfolioVo.PortfolioTypeCode = "RGL";
        //    customerPortfolioVo.PortfolioName = "MyPortfolioProspect";
        //    customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, createdById);
        //    Session["Customer"] = "Customer";
        //    if (customerIds != null)
        //    {
        //        CustomerFamilyVo familyVo = new CustomerFamilyVo();
        //        CustomerFamilyBo familyBo = new CustomerFamilyBo();
        //        familyVo.AssociateCustomerId = customerIds[1];
        //        familyVo.CustomerId = customerIds[1];
        //        familyVo.Relationship = "SELF";
        //        familyBo.CreateCustomerFamily(familyVo, customerIds[1], userVo.UserId);
        //    }
        //    return customerIds[1];
        //}

        /// <summary>
        /// Used to Create child customers for AddProspect Screen
        /// </summary>
        /// <param name="userVo"></param>
        /// <param name="rmVo"></param>
        /// <param name="createdById"></param>
        /// <param name="drChildCustomer"></param>
        /// <param name="ParentCustomerId"></param>
        //protected void CreateCustomerForAddProspect(UserVo userVo, RMVo rmVo, int createdById, DataRow drChildCustomer, int ParentCustomerId)
        //{
        //    customerVo = new CustomerVo();
        //    customerVo.RmId = rmVo.RMId;
        //    customerVo.Type = "IND";
        //    customerVo.FirstName = drChildCustomer["FirstName"].ToString();
        //    customerVo.MiddleName = drChildCustomer["MiddleName"].ToString();
        //    customerVo.LastName = drChildCustomer["LastName"].ToString();
        //    userVo.FirstName = drChildCustomer["FirstName"].ToString();
        //    customerVo.BranchId = int.Parse(ddlPickBranch.SelectedValue);
        //    if (dpDOB.SelectedDate != null && drChildCustomer["DOB"].ToString() != null && drChildCustomer["DOB"].ToString() != string.Empty)
        //    {
        //        customerVo.Dob = DateTime.Parse(drChildCustomer["DOB"].ToString());
        //    }
        //    customerVo.Email = drChildCustomer["EmailId"].ToString();
        //    customerVo.IsFPClient = 1;
        //    if (hdnIsActive.Value == "1")
        //    {
        //        customerVo.IsActive = 1;
        //    }
        //    else
        //    {
        //        customerVo.IsActive = 0;
        //    }
        //    if (hdnIsProspect.Value == "1")
        //    {
        //        customerVo.IsProspect = 1;
        //    }
        //    else
        //    {
        //        customerVo.IsProspect = 0;
        //    }
        //    userVo.Email = drChildCustomer["EmailId"].ToString();
        //    customerPortfolioVo.IsMainPortfolio = 1;
        //    customerPortfolioVo.PortfolioTypeCode = "RGL";
        //    customerPortfolioVo.PortfolioName = "MyPortfolioProspect";
        //    List<int> customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, createdById);
        //    if (customerIds != null)
        //    {
        //        CustomerFamilyVo familyVo = new CustomerFamilyVo();
        //        CustomerFamilyBo familyBo = new CustomerFamilyBo();
        //        familyVo.AssociateCustomerId = customerIds[1];
        //        familyVo.CustomerId = ParentCustomerId;
        //        familyVo.Relationship = drChildCustomer["CustomerRelationship"].ToString();
        //        familyBo.CreateCustomerFamily(familyVo, ParentCustomerId, userVo.UserId);
        //    }
        //}
        protected void btnCustomerProspect_Click(object sender, EventArgs e)
        {
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
            double groupTotal = 0.0;
            try
            {
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                ParentCustomerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                //foreach (GridEditableItem item in RadGrid1.MasterTableView.GetItems(GridItemType.EditItem))
                //{
                //    if (item.IsInEditMode)
                //    {
                //        status = false;
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Filling Data for Family Members is Incomplete. Please Click Check or Cancel for data in Edit Mode');", true);
                //    }
                //}
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
                        bdetails = customerProspectBo.DataManipulationInput(DataCapture(), ParentCustomerId, createdById, out totalincome, out totalExpense, out totalLiabilities, out totalLoanOutstanding, out instrumentTotal, out subInstrumentTotal, out groupTotal);
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
                object[] objects = new object[5];
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
        /// Will return Dictionary of complete data for Asset and Financial Details
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
                //Income Disposable (post tax)
                if (txtDisposable.Text != string.Empty)
                {
                    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                    incomedetailsvo.IncomeCategoryCode = 7;
                    incomedetailsvo.IncomeValue = double.Parse(txtDisposable.Text);
                    incomedetailsvolist.Add(incomedetailsvo);
                    //totalincome += incomedetailsvo.IncomeValue;
                }

               


                //Liabilities(Loan)
                //==========================================================================================================================
                VoFPSuperlite.CustomerProspectLiabilitiesDetailsVo liabilitiesdetailsvo;
                List<CustomerProspectLiabilitiesDetailsVo> liabilitiesdetailsvolist = new List<CustomerProspectLiabilitiesDetailsVo>();
                //Home Loan

                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 1;
                if (txtHomeLoanA.Text != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedLoan = Math.Round(double.Parse(txtHomeLoanA.Text),0);
                }
                if (txtHomeLoanLO.Text != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = Math.Round(double.Parse(txtHomeLoanLO.Text),0);
                    totalliabilities += liabilitiesdetailsvo.LoanOutstanding;
                }

                if (txtHomeLoanEMI.Text != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = Math.Round(double.Parse(txtHomeLoanEMI.Text),0);
                }
                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);


                //Auto Loan

                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 2;
                if (txtAutoLoanA.Text != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedLoan = Math.Round(double.Parse(txtAutoLoanA.Text),0);
                }
                if (txtAutoLoanLO.Text != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = Math.Round(double.Parse(txtAutoLoanLO.Text),0);
                    totalliabilities += liabilitiesdetailsvo.LoanOutstanding;
                }

                if (txtAutoLoanEMI.Text != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = Math.Round(double.Parse(txtAutoLoanEMI.Text),0);
                }
                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);


                //Educational Loan

                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 5;
                if (txtEducationLoanA.Text != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedLoan = Math.Round(double.Parse(txtEducationLoanA.Text),0);
                }
                if (txtEducationLoanLO.Text != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = Math.Round(double.Parse(txtEducationLoanLO.Text),0);
                    totalliabilities += liabilitiesdetailsvo.LoanOutstanding;
                }

                if (txtEducationLoanEMI.Text != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = Math.Round(double.Parse(txtEducationLoanEMI.Text),0);
                }
                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);


                //Personal Loan

                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 6;
                if (txtPersonalLoanA.Text != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedLoan = Math.Round(double.Parse(txtPersonalLoanA.Text),0);
                }
                if (txtPersonalLoanLO.Text != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = Math.Round(double.Parse(txtPersonalLoanLO.Text),0);
                    totalliabilities += liabilitiesdetailsvo.LoanOutstanding;
                }
                if (txtPersonalLoanEMI.Text != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = Math.Round(double.Parse(txtPersonalLoanEMI.Text),0);
                }
                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);


                //Other Loan
                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 9;
                if (txtOtherLoanA.Text != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedLoan = Math.Round(double.Parse(txtOtherLoanA.Text),0);
                }
                if (txtOtherLoanLO.Text != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = Math.Round(double.Parse(txtOtherLoanLO.Text),0);
                    totalliabilities += liabilitiesdetailsvo.LoanOutstanding;
                }
                if (txtOtherLoanEMI.Text != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = Math.Round(double.Parse(txtOtherLoanEMI.Text),0);
                }
                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);

                //Calculating Total EMI
                if (txtHomeLoanEMI.Text == "")
                    txtHomeLoanEMI.Text = "0";
                if (txtAutoLoanEMI.Text == "")
                    txtAutoLoanEMI.Text = "0";
                if (txtPersonalLoanEMI.Text == "")
                    txtPersonalLoanEMI.Text = "0";
                if (txtEducationLoanEMI.Text == "")
                    txtEducationLoanEMI.Text = "0";
                if (txtOtherLoanEMI.Text == "")
                    txtOtherLoanEMI.Text = "0";

                if ((txtHomeLoanEMI.Text != "") || (txtAutoLoanEMI.Text != "") || (txtPersonalLoanEMI.Text != "") || (txtEducationLoanEMI.Text != "") || (txtOtherLoanEMI.Text != ""))
                {
                    totalEMIForExpense = (float.Parse(txtHomeLoanEMI.Text) + float.Parse(txtAutoLoanEMI.Text) + float.Parse(txtPersonalLoanEMI.Text) + float.Parse(txtEducationLoanEMI.Text) + float.Parse(txtOtherLoanEMI.Text));
                }

                if (totalEMIForExpense != 0)
                {
                    finalExpenseEMItotal = totalEMIForExpense / 12;
                }

                //==========================================================================================================================

                //Investment
                //==========================================================================================================================
                VoFPSuperlite.CustomerProspectAssetGroupDetails assetgroupdetails;
                VoFPSuperlite.CustomerProspectAssetDetailsVo assetdetailsvo;
                VoFPSuperlite.CustomerProspectAssetSubDetailsVo assetdetailssubvo;
                List<CustomerProspectAssetDetailsVo> assetdetailsvolist = new List<CustomerProspectAssetDetailsVo>();
                List<CustomerProspectAssetSubDetailsVo> assetdetailssubvolist = new List<CustomerProspectAssetSubDetailsVo>();
                List<CustomerProspectAssetGroupDetails> assetgroupdetailslist = new List<CustomerProspectAssetGroupDetails>();

                //Direct Equtiy(Level1)
                if (txtDirectEquityA.Text != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "DE";
                    assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtDirectEquityA.Text),0);
                    assetgroupdetails.Value = Math.Round(double.Parse(txtDirectEquity.Text),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //MF-Equity(Level1)
                if (txtMFEquityA.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "MF";
                    assetdetailsvo.AssetInstrumentCategoryCode = "MFEQ";
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtMFEquityA.Text),0);
                    assetdetailsvo.Value = Math.Round(double.Parse(txtMFEquity.Text),0);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //MF-Debt(Level1)
                if (txtMFDebtA.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "MF";
                    assetdetailsvo.AssetInstrumentCategoryCode = "MFDT";
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtMFDebtA.Text),0);
                    assetdetailsvo.Value = Math.Round(double.Parse(txtMFDebt.Text),0);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //MF Hybrid -Equity(Level2)
                if (txtMFHybridEquityA.Text != string.Empty)
                {
                    assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                    assetdetailssubvo.AssetGroupCode = "MF";
                    assetdetailssubvo.AssetInstrumentCategoryCode = "MFHY";
                    assetdetailssubvo.AssetInstrumentSubCategoryCode = "MFHYEQ";
                    assetdetailssubvo.AdjustedValue = Math.Round(double.Parse(txtMFHybridEquityA.Text),0);
                    assetdetailssubvo.Value = Math.Round(double.Parse(txtMFHybridEquity.Text),0);
                    assetdetailssubvolist.Add(assetdetailssubvo);
                    totalasset += assetdetailssubvo.Value;
                }
                //MF Hybrid -Debt(Level2)
                if (txtMFHybridDebtA.Text != string.Empty)
                {
                    assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                    assetdetailssubvo.AssetGroupCode = "MF";
                    assetdetailssubvo.AssetInstrumentCategoryCode = "MFHY";
                    assetdetailssubvo.AssetInstrumentSubCategoryCode = "MFHYDT";
                    assetdetailssubvo.AdjustedValue = Math.Round(double.Parse(txtMFHybridDebtA.Text),0);
                    assetdetailssubvo.Value = Math.Round(double.Parse(txtMFHybridDebt.Text),0);
                    assetdetailssubvolist.Add(assetdetailssubvo);
                    totalasset += assetdetailssubvo.Value;
                }
                //Fixed Income
                if (txtFixedIncomeA.Text != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "FI";
                    assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtFixedIncomeA.Text),0);
                    assetgroupdetails.Value = Math.Round(double.Parse(txtFixedIncome.Text),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Govt Savings
                if (txtGovtSavingsA.Text != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "GS";
                    assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtGovtSavingsA.Text),0);
                    assetgroupdetails.Value = Math.Round(double.Parse(txtGovtSavings.Text),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Pension & Gratuities
                if (txtPensionGratuitiesA.Text != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "PG";
                    assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtPensionGratuitiesA.Text),0);
                    assetgroupdetails.Value = Math.Round(double.Parse(txtPensionGratuities.Text),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Property
                if (txtPropertyA.Text != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "PR";
                    assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtPropertyA.Text),0);
                    assetgroupdetails.Value = Math.Round(double.Parse(txtProperty.Text),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Gold
                if (txtGoldA.Text != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "GD";
                    assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtGoldA.Text),0);
                    assetgroupdetails.Value = Math.Round(double.Parse(txtGold.Text),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Collectibles
                if (txtCollectiblesA.Text != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "CL";
                    assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtCollectiblesA.Text),0);
                    assetgroupdetails.Value = Math.Round(double.Parse(txtCollectibles.Text),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Private Equity
                if (txtPrivateEquityA.Text != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "PE";
                    assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtPrivateEquityA.Text),0);
                    assetgroupdetails.Value = Math.Round(double.Parse(txtPrivateEquity.Text),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //PMS
                if (txtPMSA.Text != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "PM";
                    assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtPMSA.Text),0);
                    assetgroupdetails.Value = Math.Round(double.Parse(txtPMS.Text),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Cash and Savings
                if (txtCashAndSavingsA.Text != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "CS";
                    assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtCashAndSavingsA.Text),0);
                    assetgroupdetails.Value = Math.Round(double.Parse(txtCashAndSavings.Text),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Structured Product
                if (txtStructuredProductA.Text != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "SP";
                    assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtStructuredProductA.Text),0);
                    assetgroupdetails.Value = Math.Round(double.Parse(txtStructuredProduct.Text),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Commodities
                if (txtCommoditiesA.Text != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "CM";
                    assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtCommoditiesA.Text),0);
                    assetgroupdetails.Value = Math.Round(double.Parse(txtCommodities.Text),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Others
                if (txtInvestmentsOthersA.Text != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "OT";
                    assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtInvestmentsOthersA.Text),0);
                    assetgroupdetails.Value = Math.Round(double.Parse(txtInvestmentsOthers.Text),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //MF Consolidated value

                assetgroupdetails = new CustomerProspectAssetGroupDetails();
                assetgroupdetails.AssetGroupCode = "MF";
                assetgroupdetails.Value = 0.0;
                if (txtMFEquityA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtMFEquityA.Text),0);
                    //assetgroupdetails.Value += double.Parse(txtMFEquity.Text) ;
                }

                if (txtMFDebtA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtMFDebtA.Text),0);
                    //assetgroupdetails.Value +=  double.Parse(txtMFDebt.Text);
                }

                if (txtMFHybridEquityA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtMFHybridEquityA.Text),0);
                    //assetgroupdetails.Value += double.Parse(txtMFHybridEquity.Text);
                }

                if (txtMFHybridDebtA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtMFHybridDebtA.Text),0);
                    //assetgroupdetails.Value += double.Parse(txtMFHybridDebt.Text);
                }
                //MF Total
                if (txtMFEquity.Text != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtMFEquity.Text),0);
                }

                if (txtMFDebt.Text != string.Empty)
                {

                    assetgroupdetails.Value += Math.Round(double.Parse(txtMFDebt.Text),0);
                }

                if (txtMFHybridEquity.Text != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtMFHybridEquity.Text),0);
                }

                if (txtMFHybridDebt.Text != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtMFHybridDebt.Text),0);
                }

                assetgroupdetailslist.Add(assetgroupdetails);
                //MF Hybrd Consolidation
                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "MF";
                assetdetailsvo.AssetInstrumentCategoryCode = "MFHY";
                if (txtMFHybridEquityA.Text != string.Empty)
                {
                    assetdetailsvo.AdjustedValue += Math.Round(double.Parse(txtMFHybridEquityA.Text),0);
                }

                if (txtMFHybridDebtA.Text != string.Empty)
                {
                    assetdetailsvo.AdjustedValue += Math.Round(double.Parse(txtMFHybridDebtA.Text),0);
                }
                //MF-Hybrid Total Value
                if (txtMFHybridEquity.Text != string.Empty)
                {
                    assetdetailsvo.Value += Math.Round(double.Parse(txtMFHybridEquity.Text),0);
                }

                if (txtMFHybridDebt.Text != string.Empty)
                {
                    assetdetailsvo.Value += Math.Round(double.Parse(txtMFHybridDebt.Text),0);
                }
                assetdetailsvolist.Add(assetdetailsvo);
                //==========================================================================================================================

                //Life Insurance
                //==========================================================================================================================
                //Term 

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INTP";
                if (txtAdjustedTermSA.Text != string.Empty)
                {
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtAdjustedTermSA.Text),0);
                }
                if (txtTotalTermSA.Text != string.Empty)
                {
                    assetdetailsvo.Value = Math.Round(double.Parse(txtTotalTermSA.Text),0);
                    totalli += assetdetailsvo.Value;
                }
                if (txtTermP.Text != string.Empty)
                {
                    assetdetailsvo.Premium = Math.Round(double.Parse(txtTermP.Text),0);
                }
                if (txtTermSurrMktVal.Text != string.Empty)
                {
                    assetdetailsvo.SurrMktVal = Math.Round(double.Parse(txtTermSurrMktVal.Text),0);
                }
                assetdetailsvolist.Add(assetdetailsvo);


                //Endowment

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INEP";
                if (txtAdjustedEndowmentSA.Text != string.Empty)
                {
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtAdjustedEndowmentSA.Text),0);
                }
                if (txtTotalEndowmentSA.Text != string.Empty)
                {
                    assetdetailsvo.Value = Math.Round(double.Parse(txtTotalEndowmentSA.Text),0);
                    totalli += assetdetailsvo.Value;
                }
                if (txtEndowmentP.Text != string.Empty)
                {
                    assetdetailsvo.Premium = Math.Round(double.Parse(txtEndowmentP.Text),0);
                }
                if (txtEndowmentSurrMktVal.Text != string.Empty)
                {
                    assetdetailsvo.SurrMktVal = Math.Round(double.Parse(txtEndowmentSurrMktVal.Text),0);
                }
                assetdetailsvolist.Add(assetdetailsvo);


                //Whole Life

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INWP";
                if (txtAdjustedWholeLifeSA.Text != string.Empty)
                {
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtAdjustedWholeLifeSA.Text),0);
                }
                if (txtTotalWholeLifeSA.Text != string.Empty)
                {
                    assetdetailsvo.Value = Math.Round(double.Parse(txtTotalWholeLifeSA.Text),0);
                    totalli += assetdetailsvo.Value;
                }
                if (txtWholeLifeP.Text != string.Empty)
                {
                    assetdetailsvo.Premium = Math.Round(double.Parse(txtWholeLifeP.Text),0);
                }
                if (txtWholeLifeSurrMktVal.Text != string.Empty)
                {
                    assetdetailsvo.SurrMktVal = Math.Round(double.Parse(txtWholeLifeSurrMktVal.Text),0);
                }
                assetdetailsvolist.Add(assetdetailsvo);


                //Money Back

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INMP";
                if (txtAdjustedMoneyBackSA.Text != string.Empty)
                {
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtAdjustedMoneyBackSA.Text),0);
                }
                if (txtTotalMoneyBackSA.Text != string.Empty)
                {
                    assetdetailsvo.Value = Math.Round(double.Parse(txtTotalMoneyBackSA.Text),0);
                    totalli += assetdetailsvo.Value;
                }
                if (txtMoneyBackP.Text != string.Empty)
                {
                    assetdetailsvo.Premium = Math.Round(double.Parse(txtMoneyBackP.Text),0);
                }
                if (txtMoneyBackSurrMktVal.Text != string.Empty)
                {
                    assetdetailsvo.SurrMktVal = Math.Round(double.Parse(txtMoneyBackSurrMktVal.Text),0);
                }
                assetdetailsvolist.Add(assetdetailsvo);


                //ULIP

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INUP";
                if (txtAdjustedULIPSA.Text != string.Empty)
                {
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtAdjustedULIPSA.Text),0);
                }
                if (txtTotalULIPSA.Text != string.Empty)
                {
                    assetdetailsvo.Value = Math.Round(double.Parse(txtTotalULIPSA.Text),0);
                    totalli += assetdetailsvo.Value;
                }
                if (txtULIPP.Text != string.Empty)
                {
                    assetdetailsvo.Premium = Math.Round(double.Parse(txtULIPP.Text),0);
                }
                if (txtULIPSurrMktVal.Text != string.Empty)
                {
                    assetdetailsvo.SurrMktVal = Math.Round(double.Parse(txtULIPSurrMktVal.Text),0);
                }
                assetdetailsvolist.Add(assetdetailsvo);


                //Others

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INOT";
                if (txtAdjustedOthersLISA.Text != string.Empty)
                {
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtAdjustedOthersLISA.Text),0);
                }
                if (txtTotalOthersLISA.Text != string.Empty)
                {
                    assetdetailsvo.Value = Math.Round(double.Parse(txtTotalOthersLISA.Text),0);
                    totalli += assetdetailsvo.Value;
                }
                if (txtOthersLIP.Text != string.Empty)
                {
                    assetdetailsvo.Premium = Math.Round(double.Parse(txtOthersLIP.Text),0);
                }
                if (txtOtherSurrMktVal.Text != string.Empty)
                {
                    assetdetailsvo.SurrMktVal = Math.Round(double.Parse(txtOtherSurrMktVal.Text),0);
                }
                assetdetailsvolist.Add(assetdetailsvo);
                if (txtTermP.Text == "")
                    txtTermP.Text = "0";
                if (txtEndowmentP.Text == "")
                    txtEndowmentP.Text = "0";
                if (txtWholeLifeP.Text == "")
                    txtWholeLifeP.Text = "0";
                if (txtMoneyBackP.Text == "")
                    txtMoneyBackP.Text = "0";
                if (txtULIPP.Text == "")
                    txtULIPP.Text = "0";
                if (txtOthersLIP.Text == "")
                    txtOthersLIP.Text = "0";
                
                if ((txtTermP.Text != "") || (txtEndowmentP.Text != "") || (txtWholeLifeP.Text != "") || (txtMoneyBackP.Text != "") || (txtULIPP.Text != "") || (txtOthersLIP.Text != ""))
                {
                    totalLIPremium = float.Parse(txtTermP.Text) + float.Parse(txtEndowmentP.Text) + float.Parse(txtWholeLifeP.Text) + float.Parse(txtMoneyBackP.Text) + float.Parse(txtULIPP.Text) + float.Parse(txtOthersLIP.Text);
                }


                // Insurance Consolidation
                assetgroupdetails = new CustomerProspectAssetGroupDetails();
                assetgroupdetails.AssetGroupCode = "IN";
                assetgroupdetails.Value = 0.0;
                //Group Adjusted Value
                if (txtAdjustedTermSA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtAdjustedTermSA.Text),0);
                }
                if (txtAdjustedEndowmentSA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtAdjustedEndowmentSA.Text),0);
                }
                if (txtAdjustedWholeLifeSA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtAdjustedWholeLifeSA.Text),0);
                }
                if (txtAdjustedMoneyBackSA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtAdjustedMoneyBackSA.Text),0);
                }
                if (txtAdjustedULIPSA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtAdjustedULIPSA.Text),0);
                }
                if (txtAdjustedOthersLISA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtAdjustedOthersLISA.Text),0);
                }

                //Group Total Value

                if (txtTotalTermSA.Text != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtTotalTermSA.Text),0);
                }
                if (txtTotalEndowmentSA.Text != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtTotalEndowmentSA.Text),0);
                }
                if (txtTotalWholeLifeSA.Text != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtTotalWholeLifeSA.Text),0);
                }
                if (txtTotalMoneyBackSA.Text != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtTotalMoneyBackSA.Text),0);
                }
                if (txtTotalULIPSA.Text != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtTotalULIPSA.Text),0);
                }
                if (txtTotalOthersLISA.Text != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtTotalOthersLISA.Text),0);
                }
                assetgroupdetailslist.Add(assetgroupdetails);
                //==========================================================================================================================

                //General Insurance
                //==========================================================================================================================
                //Health Insurance cover  

                assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                assetdetailssubvo.AssetGroupCode = "GI";
                assetdetailssubvo.AssetInstrumentCategoryCode = "GIRI";
                assetdetailssubvo.AssetInstrumentSubCategoryCode = "GIRIHM";
                if (txtHealthInsuranceCoverA.Text != string.Empty)
                {
                    assetdetailssubvo.AdjustedValue = Math.Round(double.Parse(txtHealthInsuranceCoverA.Text),0);
                }
                if (txtHealthInsuranceCoverSA.Text != string.Empty)
                {
                    assetdetailssubvo.Value = Math.Round(double.Parse(txtHealthInsuranceCoverSA.Text),0);
                    totalgi += assetdetailssubvo.Value;
                }
                if (txtHealthInsuranceCoverP.Text != string.Empty)
                {
                    assetdetailssubvo.Premium = Math.Round(double.Parse(txtHealthInsuranceCoverP.Text),0);
                }
                assetdetailssubvolist.Add(assetdetailssubvo);


                //Property Insurance Cover  

                assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                assetdetailssubvo.AssetGroupCode = "GI";
                assetdetailssubvo.AssetInstrumentCategoryCode = "GIRI";
                assetdetailssubvo.AssetInstrumentSubCategoryCode = "GIRIHO";
                if (txtPropertyInsuranceCoverA.Text != string.Empty)
                {
                    assetdetailssubvo.AdjustedValue = Math.Round(double.Parse(txtPropertyInsuranceCoverA.Text),0);
                }
                if (txtPropertyInsuranceCoverSA.Text != string.Empty)
                {
                    assetdetailssubvo.Value = Math.Round(double.Parse(txtPropertyInsuranceCoverSA.Text),0);
                    totalgi += assetdetailssubvo.Value;
                }
                if (txtPropertyInsuranceCoverP.Text != string.Empty)
                {
                    assetdetailssubvo.Premium = Math.Round(double.Parse(txtPropertyInsuranceCoverP.Text),0);
                }

                assetdetailssubvolist.Add(assetdetailssubvo);


                //Personal Accident           

                assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                assetdetailssubvo.AssetGroupCode = "GI";
                assetdetailssubvo.AssetInstrumentCategoryCode = "GIRI";
                assetdetailssubvo.AssetInstrumentSubCategoryCode = "GIRIPA";
                if (txtPersonalAccidentA.Text != string.Empty)
                {
                    assetdetailssubvo.AdjustedValue = Math.Round(double.Parse(txtPersonalAccidentA.Text),0);
                }
                if (txtPersonalAccidentSA.Text != string.Empty)
                {
                    assetdetailssubvo.Value = Math.Round(double.Parse(txtPersonalAccidentSA.Text),0);
                    totalgi += assetdetailssubvo.Value;
                }
                if (txtPersonalAccidentP.Text != string.Empty)
                {
                    assetdetailssubvo.Premium = Math.Round(double.Parse(txtPersonalAccidentP.Text),0);
                }

                assetdetailssubvolist.Add(assetdetailssubvo);


                //Others

                assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                assetdetailssubvo.AssetGroupCode = "GI";
                assetdetailssubvo.AssetInstrumentCategoryCode = "GIRI";
                assetdetailssubvo.AssetInstrumentSubCategoryCode = "GIRIOT";
                if (txtOthersGIA.Text != string.Empty)
                {
                    assetdetailssubvo.AdjustedValue = Math.Round(double.Parse(txtOthersGIA.Text),0);
                }
                if (txtOthersGISA.Text != string.Empty)
                {
                    assetdetailssubvo.Value = Math.Round(double.Parse(txtOthersGISA.Text),0);
                    totalgi += assetdetailssubvo.Value;
                }
                if (txtOthersGIP.Text != string.Empty)
                {
                    assetdetailssubvo.Premium = Math.Round(double.Parse(txtOthersGIP.Text),0);
                }
                assetdetailssubvolist.Add(assetdetailssubvo);
                if (txtHealthInsuranceCoverP.Text == "")
                    txtHealthInsuranceCoverP.Text = "0";
                if (txtPropertyInsuranceCoverP.Text == "")
                    txtPropertyInsuranceCoverP.Text = "0";
                if (txtPersonalAccidentP.Text == "")
                    txtPersonalAccidentP.Text = "0";
                if (txtOthersGIP.Text == "")
                    txtOthersGIP.Text = "0";
                if ((txtHealthInsuranceCoverP.Text != "") || (txtPropertyInsuranceCoverP.Text != "") || (txtPersonalAccidentP.Text != "") || (txtOthersGIP.Text != ""))
                {
                    totalGIPremium = (float.Parse(txtHealthInsuranceCoverP.Text) + float.Parse(txtPropertyInsuranceCoverP.Text) + float.Parse(txtPersonalAccidentP.Text) + float.Parse(txtOthersGIP.Text));
                }

                if ((totalLIPremium != 0) || (totalGIPremium != 0))
                {
                    finalPremiumtotal = (totalLIPremium + totalGIPremium) / 12;
                }


                //General Insurance Consolidation
                assetgroupdetails = new CustomerProspectAssetGroupDetails();
                assetgroupdetails.AssetGroupCode = "GI";
                assetgroupdetails.AdjustedValue = 0.0;
                //Total Sum Assured For LI
                if (txtHealthInsuranceCoverSA.Text != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtHealthInsuranceCoverSA.Text),0);
                }
                if (txtPropertyInsuranceCoverSA.Text != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtPropertyInsuranceCoverSA.Text),0);
                }
                if (txtPersonalAccidentSA.Text != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtPersonalAccidentSA.Text),0);
                }
                if (txtOthersGISA.Text != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtOthersGISA.Text),0);
                }
                //Adjusted Sum Assured for GI
                if (txtHealthInsuranceCoverA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtHealthInsuranceCoverA.Text),0);
                }
                if (txtPropertyInsuranceCoverA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtPropertyInsuranceCoverA.Text),0);
                }
                if (txtPersonalAccidentA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtPersonalAccidentA.Text),0);
                }
                if (txtOthersGIA.Text != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtOthersGIA.Text),0);
                }
                assetgroupdetailslist.Add(assetgroupdetails);
                // General Insurance second Level
                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "GI";
                assetdetailsvo.AssetInstrumentCategoryCode = "GIRI";
                assetdetailsvo.AdjustedValue = assetgroupdetails.AdjustedValue;
                assetdetailsvo.Value = assetgroupdetails.Value;
                assetdetailsvolist.Add(assetdetailsvo);





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
                    expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtConveyance.Text),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Food
                if (txtFood.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 2;
                    expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtFood.Text),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Rent
                if (txtRent.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 3;
                    expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtRent.Text),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Utilities
                if (txtUtilites.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 4;
                    expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtUtilites.Text),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Health & Personal Care
                if (txtHealthPersonalCare.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 5;
                    expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtHealthPersonalCare.Text),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Personal Wear
                if (txtPersonalWear.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 6;
                    expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtPersonalWear.Text),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Entertainment & Holidays
                if (txtEntertainmentHolidays.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 7;
                    expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtEntertainmentHolidays.Text),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Domestic Help
                if (txtDomesticHelp.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 8;
                    expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtDomesticHelp.Text),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Insurance Premium
                if ((finalPremiumtotal != 0) || (finalPremiumtotal == 0))
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 9;
                    expensedetailsvo.ExpenseValue = finalPremiumtotal;
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Other Expenses
                if (txtOthersExpense.Text != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 10;
                    expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtOthersExpense.Text),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //For EMI 
                if ((finalExpenseEMItotal != 0) || (finalExpenseEMItotal == 0))
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 11;
                    expensedetailsvo.ExpenseValue = finalExpenseEMItotal;
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //For MF SIP
                if ((txtMFSIPMIS.Text != string.Empty))
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 12;
                    expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtMFSIPMIS.Text),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //For Reccuring Deposit
                if ((txtReccuringDeposit.Text != string.Empty))
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 13;
                    expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtReccuringDeposit.Text),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }



                //==========================================================================================================================





                //==========================================================================================================================
                //Main total Details Summing up
                //==========================================================================================================================
                customerprospectvo.TotalAssets = totalasset;
                customerprospectvo.TotalExpense = totalexpense;
                customerprospectvo.TotalGeneralInsurance = totalgi;
                if(txtDisposable.Text != "")
                    customerprospectvo.TotalIncome = int.Parse(txtDisposable.Text);
                customerprospectvo.TotalLiabilities = totalliabilities;
                customerprospectvo.TotalLifeInsurance = totalli;
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
                if (assetgroupdetailslist != null)
                {
                    datacapturelist.Add("AssetGroupDetails", assetgroupdetailslist);
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
                //UpdateCustomerForAddProspect(customerId);
                if (dt != null)
                {
                    customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());

                    dt = CustomerIdList(dt, customerId);

                    foreach (DataRow dr in dt.Rows)
                    {
                        //if (dr["C_CustomerId"] != null && dr["C_CustomerId"].ToString() != "")
                        //{
                        //    //Updating child Customers
                        //    //UpdateCustomerForAddProspect(customerId, dr);
                        //}
                        //else
                        //{
                        //    //Sometimes there might be the Situation that person can add new Client Customers  in Add screen on that situation
                        //    // this particular function works
                        //    // CreateCustomerForAddProspect(userVo, rmVo, createdById, dr, customerId);
                        //}
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
                    i++;
                }
            }
            return dt;
        }
        /// <summary>
        /// Used to Update Parent Customers
        /// </summary>
        /// <param name="customerId"></param>
        /// 
        //protected void UpdateCustomerForAddProspect(int customerId)
        //{
        //    customerVo = new CustomerVo();
        //    customerVo.CustomerId = customerId;
        //    customerVo.RmId = rmVo.RMId;
        //    customerVo.Type = "IND";
        //    customerVo.FirstName = txtFirstName.Text.ToString();
        //    customerVo.MiddleName = txtMiddleName.Text.ToString();
        //    customerVo.LastName = txtLastName.Text.ToString();            
        //    userVo.FirstName = txtFirstName.Text.ToString();
        //    userVo.MiddleName = txtMiddleName.Text.ToString();
        //    userVo.LastName = txtLastName.Text.ToString();
        //    customerVo.BranchId = int.Parse(ddlPickBranch.SelectedValue);
        //    if (dpDOB.SelectedDate != null)
        //    {
        //        customerVo.Dob = dpDOB.SelectedDate.Value;
        //    }
        //    customerVo.Email = txtEmail.Text;
        //    customerVo.PANNum = txtPanNumber.Text;
        //    customerVo.Adr1Line1 = txtAddress1.Text;
        //    customerVo.Adr1Line2 = txtAddress2.Text;
        //    customerVo.Adr1City = txtCity.Text;
        //    customerVo.Adr1State = txtState.Text;
        //    customerVo.Adr1Country = txtCountry.Text;
        //    if (hdnIsActive.Value == "1")
        //    {
        //        customerVo.IsActive = 1;
        //    }
        //    else
        //    {
        //        customerVo.IsActive = 0;
        //    }
        //    if (hdnIsProspect.Value == "1")
        //    {
        //        customerVo.IsProspect = 1;
        //    }
        //    else
        //    {
        //        customerVo.IsProspect = 0;
        //    }
        //    customerVo.IsFPClient = 1;
        //    if (!string.IsNullOrEmpty(txtPinCode.Text))
        //    {
        //        customerVo.Adr1PinCode = int.Parse(txtPinCode.Text);
        //    }
        //    if (!string.IsNullOrEmpty(txtMobileNo.Text))
        //    {
        //        customerVo.Mobile1 = Int64.Parse(txtMobileNo.Text);
        //    }
        //    if (dpProspectAddDate.SelectedDate != null)
        //    {
        //        customerVo.ProspectAddDate = dpProspectAddDate.SelectedDate;
        //    }
        //    Session[SessionContents.FPS_CustomerProspect_CustomerVo] = customerVo;
        //    userVo.Email = txtEmail.Text.ToString();
        //    customerPortfolioVo.IsMainPortfolio = 1;
        //    customerPortfolioVo.PortfolioTypeCode = "RGL";
        //    customerPortfolioVo.PortfolioName = "MyPortfolioProspect";
        //    customerBo.UpdateCustomer(customerVo);
        //    Session["Customer"] = "Customer";

        //}

        /// <summary>
        /// Used to update Child Customers
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="drChildCustomer"></param>
        //protected void UpdateCustomerForAddProspect(int customerId, DataRow drChildCustomer)
        //{
        //    customerVo = new CustomerVo();
        //    customerVo.CustomerId = int.Parse(drChildCustomer["C_CustomerId"].ToString());
        //    customerVo.RmId = rmVo.RMId;
        //    customerVo.Type = "IND";
        //    customerVo.FirstName = drChildCustomer["FirstName"].ToString();
        //    customerVo.MiddleName = drChildCustomer["MiddleName"].ToString();
        //    customerVo.LastName = drChildCustomer["LastName"].ToString();
        //    customerVo.BranchId = int.Parse(ddlPickBranch.SelectedValue);
        //    if (dpDOB.SelectedDate != null && drChildCustomer["DOB"].ToString() != null && drChildCustomer["DOB"].ToString() != string.Empty)
        //    {
        //        customerVo.Dob = DateTime.Parse(drChildCustomer["DOB"].ToString());
        //    }
        //    if (hdnIsActive.Value == "1")
        //    {
        //        customerVo.IsActive = 1;
        //    }
        //    else
        //    {
        //        customerVo.IsActive = 0;
        //    }
        //    if (hdnIsProspect.Value == "1")
        //    {
        //        customerVo.IsProspect = 1;
        //    }
        //    else
        //    {
        //        customerVo.IsProspect = 0;
        //    }
        //    customerVo.IsFPClient = 1;
        //    customerVo.Email = drChildCustomer["EmailId"].ToString();
        //    customerPortfolioVo.IsMainPortfolio = 1;
        //    customerPortfolioVo.PortfolioTypeCode = "RGL";
        //    customerPortfolioVo.PortfolioName = "MyPortfolioProspect";
        //    customerBo.UpdateCustomer(customerVo);
        //    Session["Customer"] = "Customer";
        //    if (drChildCustomer["C_CustomerId"] != null)
        //    {
        //        if (int.Parse(drChildCustomer["C_CustomerId"].ToString()) != 0)
        //        {
        //            CustomerFamilyVo familyVo = new CustomerFamilyVo();
        //            CustomerFamilyBo familyBo = new CustomerFamilyBo();
        //            familyVo.AssociationId = int.Parse(drChildCustomer["CA_AssociationId"].ToString());
        //            familyVo.AssociateCustomerId = int.Parse(drChildCustomer["C_CustomerId"].ToString());
        //            familyVo.CustomerId = customerId;
        //            familyVo.Relationship = drChildCustomer["CustomerRelationship"].ToString();
        //            familyBo.UpdateCustomerAssociate(familyVo, customerId, 0);
        //        }
        //    }

        //}

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
            List<CustomerProspectAssetGroupDetails> CustomerFPAssetGroupDetails = Databuffer["AssetGroupDetailsList"] as List<CustomerProspectAssetGroupDetails>;

            # region
            //Income 
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
                    if (cpid.IncomeCategoryCode == 7)
                    {
                        txtDisposable.Text = cpid.IncomeValue.ToString();
                        //totalincome += cpid.IncomeValue;
                    }

                }
            }
            //Expense
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
                    if (cped.ExpenseCategoryCode == 11)
                    {
                        txtExpenseEMI.Text = Math.Round(decimal.Parse(cped.ExpenseValue.ToString()),0).ToString();
                        totalexpense += cped.ExpenseValue;
                    }
                    if (cped.ExpenseCategoryCode == 12)
                    {
                        txtMFSIPMIS.Text = cped.ExpenseValue.ToString();
                        totalexpense += cped.ExpenseValue;
                    }
                    if (cped.ExpenseCategoryCode == 13)
                    {
                        txtReccuringDeposit.Text = cped.ExpenseValue.ToString();
                        totalexpense += cped.ExpenseValue;
                    }

                }
            }

            //Liabilities
            if (LiabilitiesDetailsForCustomerProspect != null && LiabilitiesDetailsForCustomerProspect.Count > 0)
            {
                foreach (CustomerProspectLiabilitiesDetailsVo cpld in LiabilitiesDetailsForCustomerProspect)
                {
                    if (cpld.LoanTypeCode == 1)
                    {
                        txtHomeLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtHomeLoanA.Text = cpld.AdjustedLoan.ToString();
                        txtHomeLoanEMI.Text = cpld.EMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }
                    if (cpld.LoanTypeCode == 2)
                    {
                        txtAutoLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtAutoLoanA.Text = cpld.AdjustedLoan.ToString();
                        txtAutoLoanEMI.Text = cpld.EMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }

                    if (cpld.LoanTypeCode == 5)
                    {
                        txtEducationLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtEducationLoanA.Text = cpld.AdjustedLoan.ToString();
                        txtEducationLoanEMI.Text = cpld.EMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }
                    if (cpld.LoanTypeCode == 6)
                    {
                        txtPersonalLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtPersonalLoanA.Text = cpld.AdjustedLoan.ToString();
                        txtPersonalLoanEMI.Text = cpld.EMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }
                    if (cpld.LoanTypeCode == 9)
                    {
                        txtOtherLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtOtherLoanA.Text = cpld.AdjustedLoan.ToString();
                        txtOtherLoanEMI.Text = cpld.EMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }

                }
            }
            # endregion
            //First Level Logic
            if (CustomerFPAssetGroupDetails != null && CustomerFPAssetGroupDetails.Count > 0)
            {
                foreach (CustomerProspectAssetGroupDetails cpagd in CustomerFPAssetGroupDetails)
                {
                    if (cpagd.AssetGroupCode == "DE")
                    {
                        txtDirectEquityA.Text = cpagd.AdjustedValue.ToString();
                        txtDirectEquity.Text = cpagd.Value.ToString();
                        totalasset += cpagd.Value;
                    }

                    if (cpagd.AssetGroupCode == "FI")
                    {
                        txtFixedIncomeA.Text = cpagd.AdjustedValue.ToString();
                        txtFixedIncome.Text = cpagd.Value.ToString();
                        totalasset += cpagd.Value;
                    }
                    if (cpagd.AssetGroupCode == "GS")
                    {
                        txtGovtSavingsA.Text = cpagd.AdjustedValue.ToString();
                        txtGovtSavings.Text = cpagd.Value.ToString();
                        totalasset += cpagd.Value;
                    }
                    if (cpagd.AssetGroupCode == "PG")
                    {
                        txtPensionGratuitiesA.Text = cpagd.AdjustedValue.ToString();
                        txtPensionGratuities.Text = cpagd.Value.ToString();
                        totalasset += cpagd.Value;
                    }
                    if (cpagd.AssetGroupCode == "PR")
                    {
                        txtPropertyA.Text = cpagd.AdjustedValue.ToString();
                        txtProperty.Text = cpagd.Value.ToString();
                        totalasset += cpagd.Value;
                    }
                    if (cpagd.AssetGroupCode == "GD")
                    {
                        txtGoldA.Text = cpagd.AdjustedValue.ToString();
                        txtGold.Text = cpagd.Value.ToString();
                        totalasset += cpagd.Value;
                    }
                    if (cpagd.AssetGroupCode == "CL")
                    {
                        txtCollectiblesA.Text = cpagd.AdjustedValue.ToString();
                        txtCollectibles.Text = cpagd.Value.ToString();
                        totalasset += cpagd.Value;
                    }
                    if (cpagd.AssetGroupCode == "CS")
                    {
                        txtCashAndSavingsA.Text = cpagd.AdjustedValue.ToString();
                        txtCashAndSavings.Text = cpagd.Value.ToString();
                        totalasset += cpagd.Value;
                    }
                    if (cpagd.AssetGroupCode == "SP")
                    {
                        txtStructuredProductA.Text = cpagd.AdjustedValue.ToString();
                        txtStructuredProduct.Text = cpagd.Value.ToString();
                        totalasset += cpagd.Value;
                    }
                    if (cpagd.AssetGroupCode == "CM")
                    {
                        txtCommoditiesA.Text = cpagd.AdjustedValue.ToString();
                        txtCommodities.Text = cpagd.Value.ToString();
                        totalasset += cpagd.Value;
                    }
                    if (cpagd.AssetGroupCode == "PE")
                    {
                        txtPrivateEquityA.Text = cpagd.AdjustedValue.ToString();
                        txtPrivateEquity.Text = cpagd.Value.ToString();
                        totalasset += cpagd.Value;
                    }
                    if (cpagd.AssetGroupCode == "PM")
                    {
                        txtPMSA.Text = cpagd.AdjustedValue.ToString();
                        txtPMS.Text = cpagd.Value.ToString();
                        totalasset += cpagd.Value;
                    }
                    if (cpagd.AssetGroupCode == "OT")
                    {
                        txtInvestmentsOthersA.Text = cpagd.AdjustedValue.ToString();
                        txtInvestmentsOthers.Text = cpagd.Value.ToString();
                        totalasset += cpagd.Value;
                    }

                }
            }
            //Second level logic
            if (CustomerFPAssetInstrumentDetails != null && CustomerFPAssetInstrumentDetails.Count > 0)
            {
                foreach (CustomerProspectAssetDetailsVo cpad in CustomerFPAssetInstrumentDetails)
                {

                    if (cpad.AssetGroupCode == "MF" && cpad.AssetInstrumentCategoryCode == "MFEQ")
                    {
                        txtMFEquityA.Text = cpad.AdjustedValue.ToString();
                        txtMFEquity.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "MF" && cpad.AssetInstrumentCategoryCode == "MFDT")
                    {
                        txtMFDebtA.Text = cpad.AdjustedValue.ToString();
                        txtMFDebt.Text = cpad.Value.ToString();
                        totalasset += cpad.Value;
                    }

                    //Life Insurance
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INTP")
                    {
                        txtTotalTermSA.Text = cpad.Value.ToString();
                        txtAdjustedTermSA.Text = cpad.AdjustedValue.ToString();
                        txtTermSurrMktVal.Text = cpad.SurrMktVal.ToString();
                        txtTermP.Text = cpad.Premium.ToString();
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INEP")
                    {
                        txtTotalEndowmentSA.Text = cpad.Value.ToString();
                        txtAdjustedEndowmentSA.Text = cpad.AdjustedValue.ToString();
                        txtEndowmentSurrMktVal.Text = cpad.SurrMktVal.ToString();
                        txtEndowmentP.Text = cpad.Premium.ToString();
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INWP")
                    {
                        txtTotalWholeLifeSA.Text = cpad.Value.ToString();
                        txtAdjustedWholeLifeSA.Text = cpad.AdjustedValue.ToString();
                        txtWholeLifeSurrMktVal.Text = cpad.SurrMktVal.ToString();
                        txtWholeLifeP.Text = cpad.Premium.ToString();
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INMP")
                    {
                        txtTotalMoneyBackSA.Text = cpad.Value.ToString();
                        txtAdjustedMoneyBackSA.Text = cpad.AdjustedValue.ToString();
                        txtMoneyBackSurrMktVal.Text = cpad.SurrMktVal.ToString();
                        txtMoneyBackP.Text = cpad.Premium.ToString();
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INUP")
                    {
                        txtTotalULIPSA.Text = cpad.Value.ToString();
                        txtAdjustedULIPSA.Text = cpad.AdjustedValue.ToString();
                        txtULIPSurrMktVal.Text = cpad.SurrMktVal.ToString();
                        txtULIPP.Text = cpad.Premium.ToString();
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INOT")
                    {
                        txtTotalOthersLISA.Text = cpad.Value.ToString();
                        txtAdjustedOthersLISA.Text = cpad.AdjustedValue.ToString();
                        txtOtherSurrMktVal.Text = cpad.SurrMktVal.ToString();
                        txtOthersLIP.Text = cpad.Premium.ToString();
                        totalli += cpad.Value;
                    }
                }
            }
            //third level Logic
            if (CustomerFPAssetSubInstrumentDetails != null && CustomerFPAssetSubInstrumentDetails.Count > 0)
            {
                foreach (CustomerProspectAssetSubDetailsVo cpasd in CustomerFPAssetSubInstrumentDetails)
                {
                    if (cpasd.AssetGroupCode == "GI" && cpasd.AssetInstrumentCategoryCode == "GIRI" && cpasd.AssetInstrumentSubCategoryCode == "GIRIHM")
                    {
                        txtHealthInsuranceCoverA.Text = cpasd.AdjustedValue.ToString();
                        txtHealthInsuranceCoverSA.Text = cpasd.Value.ToString();
                        txtHealthInsuranceCoverP.Text = cpasd.Premium.ToString();

                        totalgi += cpasd.Value;

                    }
                    if (cpasd.AssetGroupCode == "GI" && cpasd.AssetInstrumentCategoryCode == "GIRI" && cpasd.AssetInstrumentSubCategoryCode == "GIRIHO")
                    {
                        txtPropertyInsuranceCoverA.Text = cpasd.AdjustedValue.ToString();
                        txtPropertyInsuranceCoverSA.Text = cpasd.Value.ToString();
                        txtPropertyInsuranceCoverP.Text = cpasd.Premium.ToString();
                        totalgi += cpasd.Value;

                    }
                    if (cpasd.AssetGroupCode == "GI" && cpasd.AssetInstrumentCategoryCode == "GIRI" && cpasd.AssetInstrumentSubCategoryCode == "GIRIPA")
                    {
                        txtPersonalAccidentA.Text = cpasd.AdjustedValue.ToString();
                        txtPersonalAccidentSA.Text = cpasd.Value.ToString();
                        txtPersonalAccidentP.Text = cpasd.Premium.ToString();
                        totalgi += cpasd.Value;

                    }
                    if (cpasd.AssetGroupCode == "GI" && cpasd.AssetInstrumentCategoryCode == "GIRI" && cpasd.AssetInstrumentSubCategoryCode == "GIRIOT")
                    {
                        txtOthersGIA.Text = cpasd.AdjustedValue.ToString();
                        txtOthersGISA.Text = cpasd.Value.ToString();
                        txtOthersGIP.Text = cpasd.Premium.ToString();
                        totalgi += cpasd.Value;

                    }
                    if (cpasd.AssetGroupCode == "MF" && cpasd.AssetInstrumentCategoryCode == "MFHY" && cpasd.AssetInstrumentSubCategoryCode == "MFHYEQ")
                    {
                        txtMFHybridEquityA.Text = cpasd.AdjustedValue.ToString();
                        txtMFHybridEquity.Text = cpasd.Value.ToString();

                        totalasset += cpasd.Value;
                    }
                    if (cpasd.AssetGroupCode == "MF" && cpasd.AssetInstrumentCategoryCode == "MFHY" && cpasd.AssetInstrumentSubCategoryCode == "MFHYDT")
                    {
                        txtMFHybridDebtA.Text = cpasd.AdjustedValue.ToString();
                        txtMFHybridDebt.Text = cpasd.Value.ToString();
                        totalasset += cpasd.Value;

                    }


                    //txtAssetTotal.Text = totalasset.ToString();
                    //txtIncomeTotal.Text = totalincome.ToString();
                    //txtExpenseTotal.Text = totalexpense.ToString();
                    //txtTotalLO.Text = totalliabilities.ToString();
                    //txtTotalLISA.Text = totalli.ToString();
                    //txtTotalGISA.Text = totalgi.ToString();

                }
            }
            //txtAssets.Text = totalasset.ToString();
            //txtIncome.Text = totalincome.ToString();
            //txtExpense.Text = totalexpense.ToString();
            //txtLiabilities.Text = totalliabilities.ToString();
            //txtLifeInsurance.Text = totalli.ToString();
            //txtGeneralInsurance.Text = totalgi.ToString();

            //txtAssetTotal.Text = totalasset.ToString();
            txtAssetTotal.Text =String.Format("{0:n2}", totalasset.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

            txtIncomeTotal.Text = totalincome.ToString();

            txtIncomePostTax.Text = totalincome.ToString();
            //txtExpenseTotal.Text = totalexpense.ToString();
            txtExpenseTotal.Text = String.Format("{0:n2}", totalexpense.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
            //txtTotalLO.Text = totalliabilities.ToString();
            txtTotalLO.Text = String.Format("{0:n2}", totalliabilities.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
            txtTotalLISA.Text = totalli.ToString();
            txtTotalGISA.Text = totalgi.ToString();

        }


        protected void ManagedUnmanagedDetails(int customerId, int Advisorid, int Switch)
        {
            CustomerProspectBo customerprospectbo = new CustomerProspectBo();
            DataSet dsGetWERPDetails = customerprospectbo.GetUnmanagedManagedDetailsForFP(customerId, Advisorid, Switch);


            if (dsGetWERPDetails != null && dsGetWERPDetails.Tables[3].Rows.Count > 0)
            {
                foreach (DataRow drFourth in dsGetWERPDetails.Tables[3].Rows)
                {

                    if (drFourth["XLT_LoanTypeCode"].ToString() == "1")
                    {
                        txtWERPHomeLoan.Text = drFourth["CFPLD_WERPLoanOutstanding"].ToString();
                        txtHomeLoanLO.Text = drFourth["CFPLD_TotalLoanOutstanding"].ToString();
                    }
                    if (drFourth["XLT_LoanTypeCode"].ToString() == "2")
                    {
                        txtWERPAutoLoan.Text = drFourth["CFPLD_WERPLoanOutstanding"].ToString();
                        txtAutoLoanLO.Text = drFourth["CFPLD_TotalLoanOutstanding"].ToString();
                    }

                    if (drFourth["XLT_LoanTypeCode"].ToString() == "5")
                    {

                        txtWERPEducationLoan.Text = drFourth["CFPLD_WERPLoanOutstanding"].ToString();
                        txtEducationLoanLO.Text = drFourth["CFPLD_TotalLoanOutstanding"].ToString();

                    }
                    if (drFourth["XLT_LoanTypeCode"].ToString() == "6")
                    {
                        txtWERPPersonalLoan.Text = drFourth["CFPLD_WERPLoanOutstanding"].ToString();
                        txtPersonalLoanLO.Text = drFourth["CFPLD_TotalLoanOutstanding"].ToString();
                    }
                    if (drFourth["XLT_LoanTypeCode"].ToString() == "9")
                    {
                        txtWERPOtherLoan.Text = drFourth["CFPLD_WERPLoanOutstanding"].ToString();
                        txtOtherLoanLO.Text = drFourth["CFPLD_TotalLoanOutstanding"].ToString();
                    }

                }
            }

            //First Level Category`
            if (dsGetWERPDetails != null && dsGetWERPDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drFirst in dsGetWERPDetails.Tables[0].Rows)
                {
                    if (drFirst["PAG_AssetGroupCode"].ToString() == "DE")
                    {
                        txtWERPDirectEquityM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPManagedValue"].ToString()), 0).ToString();
                        txtWERPDirectEquityUM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPUnManagedValue"].ToString()), 0).ToString();
                        txtDirectEquity.Text = Math.Round(double.Parse(drFirst["CFPAGD_TotalValue"].ToString()), 0).ToString();

                        if (drFirst["PAG_AssetGroupCode"].ToString() == "FI")
                        {
                            txtWERPFixedIncomeM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPFixedIncomeUM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtFixedIncome.Text = Math.Round(double.Parse(drFirst["CFPAGD_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drFirst["PAG_AssetGroupCode"].ToString() == "GS")
                        {
                            txtWERPGovtSavingsM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPGovtSavingsUM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtGovtSavings.Text = Math.Round(double.Parse(drFirst["CFPAGD_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drFirst["PAG_AssetGroupCode"].ToString() == "PG")
                        {
                            txtWERPPensionGratuitiesM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPPensionGratuitiesUM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtPensionGratuities.Text = Math.Round(double.Parse(drFirst["CFPAGD_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drFirst["PAG_AssetGroupCode"].ToString() == "PR")
                        {
                            txtWERPPropertyM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPPropertyUM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtProperty.Text = Math.Round(double.Parse(drFirst["CFPAGD_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drFirst["PAG_AssetGroupCode"].ToString() == "GD")
                        {
                            txtWERPGoldM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPGoldUM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtGold.Text = Math.Round(double.Parse(drFirst["CFPAGD_TotalValue"].ToString()), 0).ToString();

                        }
                        if (drFirst["PAG_AssetGroupCode"].ToString() == "CL")
                        {
                            txtWERPCollectiblesM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPCollectiblesUM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtCollectibles.Text = Math.Round(double.Parse(drFirst["CFPAGD_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drFirst["PAG_AssetGroupCode"].ToString() == "CS")
                        {
                            txtWERPCashAndSavingsM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPCashAndSavingsUM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtCashAndSavings.Text = Math.Round(double.Parse(drFirst["CFPAGD_TotalValue"].ToString()), 0).ToString();

                        }
                        if (drFirst["PAG_AssetGroupCode"].ToString() == "SP")
                        {
                            txtWERPStructuredProductM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPStructuredProductUM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtStructuredProduct.Text = Math.Round(double.Parse(drFirst["CFPAGD_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drFirst["PAG_AssetGroupCode"].ToString() == "CM")
                        {
                            txtWERPCommoditiesM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPCommoditiesUM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtCommodities.Text = Math.Round(double.Parse(drFirst["CFPAGD_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drFirst["PAG_AssetGroupCode"].ToString() == "PE")
                        {
                            txtWERPPrivateEquityM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPPrivateEquityUM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtPrivateEquity.Text = Math.Round(double.Parse(drFirst["CFPAGD_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drFirst["PAG_AssetGroupCode"].ToString() == "PM")
                        {
                            txtWERPPMSM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPPMSUM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtPMS.Text = Math.Round(double.Parse(drFirst["CFPAGD_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drFirst["PAG_AssetGroupCode"].ToString() == "OT")
                        {
                            txtWERPInvestmentsOthersM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPInvestmentsOthersUM.Text = Math.Round(double.Parse(drFirst["CFPAGD_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtInvestmentsOthers.Text = Math.Round(double.Parse(drFirst["CFPAGD_TotalValue"].ToString()), 0).ToString();
                        }
                    }
                }


                // Second Level Category
                if (dsGetWERPDetails != null && dsGetWERPDetails.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow drSecond in dsGetWERPDetails.Tables[1].Rows)
                    {
                        if (drSecond["PAG_AssetGroupCode"].ToString() == "MF" && drSecond["PAIC_AssetInstrumentCategoryCode"].ToString() == "MFEQ")
                        {
                            txtWERPMFEquityM.Text = Math.Round(double.Parse(drSecond["CFPAID_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPMFEquityUM.Text = Math.Round(double.Parse(drSecond["CFPAID_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtMFEquity.Text = Math.Round(double.Parse(drSecond["CFPAID_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drSecond["PAG_AssetGroupCode"].ToString() == "MF" && drSecond["PAIC_AssetInstrumentCategoryCode"].ToString() == "MFDT")
                        {
                            txtWERPMFDebtM.Text = Math.Round(double.Parse(drSecond["CFPAID_WERPManagedValue"].ToString()), 0).ToString();
                            txtWERPMFDebtUM.Text = Math.Round(double.Parse(drSecond["CFPAID_WERPUnManagedValue"].ToString()), 0).ToString();
                            txtMFDebt.Text = Math.Round(double.Parse(drSecond["CFPAID_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drSecond["PAG_AssetGroupCode"].ToString() == "IN" && drSecond["PAIC_AssetInstrumentCategoryCode"].ToString() == "INTP")
                        {
                            txtWERPTermSA.Text = Math.Round(double.Parse(drSecond["CFPAID_WERPManagedValue"].ToString()), 0).ToString();
                            txtTotalTermSA.Text = Math.Round(double.Parse(drSecond["CFPAID_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drSecond["PAG_AssetGroupCode"].ToString() == "IN" && drSecond["PAIC_AssetInstrumentCategoryCode"].ToString() == "INEP")
                        {
                            txtWERPEndowmentSA.Text = Math.Round(double.Parse(drSecond["CFPAID_WERPManagedValue"].ToString()), 0).ToString();
                            txtTotalEndowmentSA.Text = Math.Round(double.Parse(drSecond["CFPAID_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drSecond["PAG_AssetGroupCode"].ToString() == "IN" && drSecond["PAIC_AssetInstrumentCategoryCode"].ToString() == "INWP")
                        {
                            txtWERPWholeLifeSA.Text = Math.Round(double.Parse(drSecond["CFPAID_WERPManagedValue"].ToString()), 0).ToString();
                            txtTotalWholeLifeSA.Text = Math.Round(double.Parse(drSecond["CFPAID_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drSecond["PAG_AssetGroupCode"].ToString() == "IN" && drSecond["PAIC_AssetInstrumentCategoryCode"].ToString() == "INMP")
                        {
                            txtWERPMoneyBackSA.Text = Math.Round(double.Parse(drSecond["CFPAID_WERPManagedValue"].ToString()), 0).ToString();
                            txtTotalMoneyBackSA.Text = Math.Round(double.Parse(drSecond["CFPAID_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drSecond["PAG_AssetGroupCode"].ToString() == "IN" && drSecond["PAIC_AssetInstrumentCategoryCode"].ToString() == "INUP")
                        {
                            txtWERPULIPSA.Text = Math.Round(double.Parse(drSecond["CFPAID_WERPManagedValue"].ToString()), 0).ToString();
                            txtTotalULIPSA.Text = Math.Round(double.Parse(drSecond["CFPAID_TotalValue"].ToString()), 0).ToString();
                        }
                        if (drSecond["PAG_AssetGroupCode"].ToString() == "IN" && drSecond["PAIC_AssetInstrumentCategoryCode"].ToString() == "INOT")
                        {
                            txtWERPOthersLISA.Text = Math.Round(double.Parse(drSecond["CFPAID_WERPManagedValue"].ToString()), 0).ToString();
                            txtTotalOthersLISA.Text = Math.Round(double.Parse(drSecond["CFPAID_TotalValue"].ToString()), 0).ToString();
                        }

                    }
                }

                // Third Level Category
                if (dsGetWERPDetails != null && dsGetWERPDetails.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow drThird in dsGetWERPDetails.Tables[2].Rows)
                    {
                        if (drThird["PAG_AssetGroupCode"].ToString() == "MF" && drThird["PAIC_AssetInstrumentCategoryCode"].ToString() == "MFHY" && drThird["PAISC_AssetInstrumentSubCategoryCode"].ToString() == "MFHYEQ")
                        {
                            txtWERPMFHybridEquityM.Text = drThird["CFPASID_WERPManagedValue"].ToString();
                            txtWERPMFHybridEquityUM.Text = drThird["CFPASID_WERPUnManagedValue"].ToString();
                            txtMFHybridEquity.Text = drThird["CFPASID_TotalValue"].ToString();
                        }
                        if (drThird["PAG_AssetGroupCode"].ToString() == "MF" && drThird["PAIC_AssetInstrumentCategoryCode"].ToString() == "MFHY" && drThird["PAISC_AssetInstrumentSubCategoryCode"].ToString() == "MFHYDT")
                        {
                            txtWERPMFHybridDebtM.Text = drThird["CFPASID_WERPManagedValue"].ToString();
                            txtWERPMFHybridDebtUM.Text = drThird["CFPASID_WERPUnManagedValue"].ToString();
                            txtMFHybridDebt.Text = drThird["CFPASID_TotalValue"].ToString();
                        }

                        if (drThird["PAG_AssetGroupCode"].ToString() == "GI" && drThird["PAIC_AssetInstrumentCategoryCode"].ToString() == "GIRI" && drThird["PAISC_AssetInstrumentSubCategoryCode"].ToString() == "GIRIHM")
                        {
                            txtWERPHealthInsuranceCover.Text = drThird["CFPASID_WERPManagedValue"].ToString();
                            txtHealthInsuranceCoverSA.Text = drThird["CFPASID_TotalValue"].ToString();

                        }
                        if (drThird["PAG_AssetGroupCode"].ToString() == "GI" && drThird["PAIC_AssetInstrumentCategoryCode"].ToString() == "GIRI" && drThird["PAISC_AssetInstrumentSubCategoryCode"].ToString() == "GIRIHO")
                        {
                            txtWERPPropertyInsuranceCover.Text = drThird["CFPASID_WERPManagedValue"].ToString();
                            txtPropertyInsuranceCoverSA.Text = drThird["CFPASID_TotalValue"].ToString();
                        }
                        if (drThird["PAG_AssetGroupCode"].ToString() == "GI" && drThird["PAIC_AssetInstrumentCategoryCode"].ToString() == "GIRI" && drThird["PAISC_AssetInstrumentSubCategoryCode"].ToString() == "GIRIPA")
                        {
                            txtWERPPersonalAccident.Text = drThird["CFPASID_WERPManagedValue"].ToString();
                            txtPersonalAccidentSA.Text = drThird["CFPASID_TotalValue"].ToString();
                        }
                        if (drThird["PAG_AssetGroupCode"].ToString() == "GI" && drThird["PAIC_AssetInstrumentCategoryCode"].ToString() == "GIRI" && drThird["PAISC_AssetInstrumentSubCategoryCode"].ToString() == "GIRIOT")
                        {
                            txtWERPOthersGI.Text = drThird["CFPASID_WERPManagedValue"].ToString();
                            txtOthersGISA.Text = drThird["CFPASID_TotalValue"].ToString();
                        }

                    }
                }
                if (dsGetWERPDetails.Tables[6].Rows.Count > 0)
                {
                    txtSlabProfile.Text = dsGetWERPDetails.Tables[6].Rows[0]["C_TaxSlab"].ToString();
                    txtSlabAsPerProfile.Text = txtSlabProfile.Text;
                }

                if (dsGetWERPDetails.Tables[7].Rows.Count > 0)
                {
                    txtMFSIPMIS.Text = dsGetWERPDetails.Tables[7].Rows[0]["CFPED_Value"].ToString();
                }

                if (dsGetWERPDetails.Tables[8].Rows.Count > 0)
                {
                    txtReccuringDeposit.Text = dsGetWERPDetails.Tables[8].Rows[0]["CFPED_Value"].ToString();
                }


            }
        }

        protected void btnCalculationSubmit_Click(object sender, EventArgs e)
        {
            lblFinalResults.Text = "Disposable income (post tax) is"+ txtIncomePostTax.Text + "";
        }

        protected void btnSlabCalculate_Click(object sender, EventArgs e)
        {
            mdlPopupSlabCalculate.TargetControlID = "btnSlabCalculate";
            mdlPopupSlabCalculate.Show();
        }
    }
}
