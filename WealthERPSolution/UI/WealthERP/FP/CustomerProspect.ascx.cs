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
                txtTotalEndowmentSA.Attributes.Add("readonly", "readonly");
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

                txtTotalTermPremium.Attributes.Add("readonly", "readonly");
                txtTotalEndowmentPremium.Attributes.Add("readonly", "readonly");
                txtTotalWholeLifePremium.Attributes.Add("readonly", "readonly");
                txtTotalMoneyBackPremium.Attributes.Add("readonly", "readonly");
                txtTotalULIPPremium.Attributes.Add("readonly", "readonly");
                txtTotalOthersPremium.Attributes.Add("readonly", "readonly");
                
                
                txtToalHealthInsurancePremium.Attributes.Add("readonly", "readonly");

                txtToalHealthInsurancePremium.Attributes.Add("readonly", "readonly");
                txtTotalPropertyInsurancePremium.Attributes.Add("readonly", "readonly");
                txtTotalPersonalAccidentPremium.Attributes.Add("readonly", "readonly");
                txtTotalPremiumOthers.Attributes.Add("readonly", "readonly");
                txtIncomeTotal.Attributes.Add("readonly", "readonly");
                txtTotalLO.Attributes.Add("readonly", "readonly");

                // EMI Total textboxes
                txtHomeLoanEMITotal.Attributes.Add("readonly", "readonly");
                txtAutoLoanEMITotal.Attributes.Add("readonly", "readonly");
                txtPersonalLoanEMITotal.Attributes.Add("readonly", "readonly");
                txtEducationLoanEMITotal.Attributes.Add("readonly", "readonly");
                txtOtherLoanEMITotal.Attributes.Add("readonly", "readonly");

                
                //txtToalHealthInsurancePremium.Enabled = false;
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
                if (txtSalary.Text.Trim() != string.Empty)
                {
                    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                    incomedetailsvo.IncomeCategoryCode = 1;
                    incomedetailsvo.IncomeValue = double.Parse(txtSalary.Text.Trim());
                    incomedetailsvolist.Add(incomedetailsvo);
                    totalincome += incomedetailsvo.IncomeValue;
                }
                //Rental Property
                if (txtRentalProperty.Text.Trim() != string.Empty)
                {
                    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                    incomedetailsvo.IncomeCategoryCode = 2;
                    incomedetailsvo.IncomeValue = double.Parse(txtRentalProperty.Text.Trim());
                    incomedetailsvolist.Add(incomedetailsvo);
                    totalincome += incomedetailsvo.IncomeValue;
                }
                //Agriculture
                if (txtAgriculturalIncome.Text.Trim() != string.Empty)
                {
                    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                    incomedetailsvo.IncomeCategoryCode = 3;
                    incomedetailsvo.IncomeValue = double.Parse(txtAgriculturalIncome.Text.Trim());
                    incomedetailsvolist.Add(incomedetailsvo);
                    totalincome += incomedetailsvo.IncomeValue;
                }
                //Business & Profession
                if (txtBusinessAndProfession.Text.Trim() != string.Empty)
                {
                    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                    incomedetailsvo.IncomeCategoryCode = 4;
                    incomedetailsvo.IncomeValue = double.Parse(txtBusinessAndProfession.Text.Trim());
                    incomedetailsvolist.Add(incomedetailsvo);
                    totalincome += incomedetailsvo.IncomeValue;
                }
                //CapitalGain
                if (txtCapitalGains.Text.Trim() != string.Empty)
                {
                    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                    incomedetailsvo.IncomeCategoryCode = 5;
                    incomedetailsvo.IncomeValue = double.Parse(txtCapitalGains.Text.Trim());
                    incomedetailsvolist.Add(incomedetailsvo);
                    totalincome += incomedetailsvo.IncomeValue;
                }
                //IncomeOthers
                if (txtOthersIncome.Text.Trim() != string.Empty)
                {
                    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                    incomedetailsvo.IncomeCategoryCode = 6;
                    incomedetailsvo.IncomeValue = double.Parse(txtOthersIncome.Text.Trim());
                    incomedetailsvolist.Add(incomedetailsvo);
                    totalincome += incomedetailsvo.IncomeValue;
                }
                //Income Disposable (post tax)
                //if (txtDisposable.Text != string.Empty)
                //{
                //    incomedetailsvo = new CustomerProspectIncomeDetailsVo();
                //    incomedetailsvo.IncomeCategoryCode = 7;
                //    incomedetailsvo.IncomeValue = double.Parse(txtDisposable.Text);
                //    incomedetailsvolist.Add(incomedetailsvo);
                //    //totalincome += incomedetailsvo.IncomeValue;
                //}




                //Liabilities(Loan)
                //==========================================================================================================================
                VoFPSuperlite.CustomerProspectLiabilitiesDetailsVo liabilitiesdetailsvo;
                List<CustomerProspectLiabilitiesDetailsVo> liabilitiesdetailsvolist = new List<CustomerProspectLiabilitiesDetailsVo>();
                //Home Loan

                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 1;
                if (txtHomeLoanA.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedLoan = Math.Round(double.Parse(txtHomeLoanA.Text.Trim()), 0);
                }
                if (txtHomeLoanLO.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = Math.Round(double.Parse(txtHomeLoanLO.Text.Trim()), 0);
                    totalliabilities += liabilitiesdetailsvo.LoanOutstanding;
                }

                if (txtHomeLoanEMI.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = Math.Round(double.Parse(txtHomeLoanEMI.Text.Trim()), 0);
                }

                if (txtHomeLoanEMIA.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedEMIAmount = Math.Round(double.Parse(txtHomeLoanEMIA.Text.Trim()), 0);
                }

                if (txtHomeLoanEMITotal.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.TotalEMIAmount = Math.Round(double.Parse(txtHomeLoanEMITotal.Text.Trim()), 0);
                }


                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);


                //Auto Loan

                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 2;
                if (txtAutoLoanA.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedLoan = Math.Round(double.Parse(txtAutoLoanA.Text.Trim()), 0);
                }
                if (txtAutoLoanLO.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = Math.Round(double.Parse(txtAutoLoanLO.Text.Trim()), 0);
                    totalliabilities += liabilitiesdetailsvo.LoanOutstanding;
                }

                if (txtAutoLoanEMI.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = Math.Round(double.Parse(txtAutoLoanEMI.Text.Trim()), 0);
                }

                if (txtAutoLoanEMIA.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedEMIAmount = Math.Round(double.Parse(txtAutoLoanEMIA.Text.Trim()), 0);
                }

                if (txtAutoLoanEMITotal.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.TotalEMIAmount = Math.Round(double.Parse(txtAutoLoanEMITotal.Text.Trim()), 0);
                }

                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);


                //Educational Loan

                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 5;
                if (txtEducationLoanA.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedLoan = Math.Round(double.Parse(txtEducationLoanA.Text.Trim()), 0);
                }
                if (txtEducationLoanLO.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = Math.Round(double.Parse(txtEducationLoanLO.Text.Trim()), 0);
                    totalliabilities += liabilitiesdetailsvo.LoanOutstanding;
                }

                if (txtEducationLoanEMI.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = Math.Round(double.Parse(txtEducationLoanEMI.Text.Trim()), 0);
                }

                if (txtEducationLoanEMIA.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedEMIAmount = Math.Round(double.Parse(txtEducationLoanEMIA.Text.Trim()), 0);
                }

                if (txtEducationLoanEMITotal.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.TotalEMIAmount = Math.Round(double.Parse(txtEducationLoanEMITotal.Text.Trim()), 0);
                }

                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);


                //Personal Loan

                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 6;
                if (txtPersonalLoanA.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedLoan = Math.Round(double.Parse(txtPersonalLoanA.Text.Trim()), 0);
                }
                if (txtPersonalLoanLO.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = Math.Round(double.Parse(txtPersonalLoanLO.Text.Trim()), 0);
                    totalliabilities += liabilitiesdetailsvo.LoanOutstanding;
                }
                if (txtPersonalLoanEMI.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = Math.Round(double.Parse(txtPersonalLoanEMI.Text.Trim()), 0);
                }

                if (txtPersonalLoanEMIA.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedEMIAmount = Math.Round(double.Parse(txtPersonalLoanEMIA.Text.Trim()), 0);
                }

                if (txtPersonalLoanEMITotal.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.TotalEMIAmount = Math.Round(double.Parse(txtPersonalLoanEMITotal.Text.Trim()), 0);
                }

                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);


                //Other Loan
                liabilitiesdetailsvo = new CustomerProspectLiabilitiesDetailsVo();
                liabilitiesdetailsvo.LoanTypeCode = 9;
                if (txtOtherLoanA.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedLoan = Math.Round(double.Parse(txtOtherLoanA.Text.Trim()), 0);
                }
                if (txtOtherLoanLO.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.LoanOutstanding = Math.Round(double.Parse(txtOtherLoanLO.Text.Trim()), 0);
                    totalliabilities += liabilitiesdetailsvo.LoanOutstanding;
                }
                if (txtOtherLoanEMI.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.EMIAmount = Math.Round(double.Parse(txtOtherLoanEMI.Text.Trim()), 0);
                }

                if (txtOtherLoanEMIA.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.AdjustedEMIAmount = Math.Round(double.Parse(txtOtherLoanEMIA.Text.Trim()), 0);
                }

                if (txtOtherLoanEMITotal.Text.Trim() != string.Empty)
                {
                    liabilitiesdetailsvo.TotalEMIAmount = Math.Round(double.Parse(txtOtherLoanEMITotal.Text.Trim()), 0);
                }
                liabilitiesdetailsvolist.Add(liabilitiesdetailsvo);

                //Calculating Total EMI
                if (txtHomeLoanEMITotal.Text.Trim() == "")
                    txtHomeLoanEMITotal.Text = "0";
                if (txtAutoLoanEMITotal.Text.Trim() == "")
                    txtAutoLoanEMITotal.Text = "0";
                if (txtPersonalLoanEMITotal.Text.Trim() == "")
                    txtPersonalLoanEMITotal.Text = "0";
                if (txtEducationLoanEMITotal.Text.Trim() == "")
                    txtEducationLoanEMITotal.Text = "0";
                if (txtOtherLoanEMITotal.Text.Trim() == "")
                    txtOtherLoanEMITotal.Text = "0";

                if ((txtHomeLoanEMITotal.Text.Trim() != "") || (txtAutoLoanEMITotal.Text.Trim() != "") || (txtPersonalLoanEMITotal.Text.Trim() != "") || (txtEducationLoanEMITotal.Text.Trim() != "") || (txtOtherLoanEMITotal.Text.Trim() != ""))
                {
                    totalEMIForExpense = (float.Parse(txtHomeLoanEMITotal.Text.Trim()) + float.Parse(txtAutoLoanEMITotal.Text.Trim()) + float.Parse(txtPersonalLoanEMITotal.Text.Trim()) + float.Parse(txtEducationLoanEMITotal.Text.Trim()) + float.Parse(txtOtherLoanEMITotal.Text.Trim()));
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
                if (txtDirectEquityA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "DE";
                    if (txtDirectEquityA.Text.Trim() != string.Empty)
                        assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtDirectEquityA.Text.Trim()),0);

                    if(txtDirectEquity.Text.Trim() != string.Empty)
                        assetgroupdetails.Value = Math.Round(double.Parse(txtDirectEquity.Text.Trim()),0);
                    
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //MF-Equity(Level1)
                if (txtMFEquityA.Text != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "MF";
                    assetdetailsvo.AssetInstrumentCategoryCode = "MFEQ";
                    
                    if (txtMFEquityA.Text.Trim() != string.Empty)
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtMFEquityA.Text.Trim()),0);
                    
                    if(txtMFEquity.Text.Trim() != string.Empty)
                        assetdetailsvo.Value = Math.Round(double.Parse(txtMFEquity.Text.Trim()),0);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //MF-Debt(Level1)
                if (txtMFDebtA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo = new CustomerProspectAssetDetailsVo();
                    assetdetailsvo.AssetGroupCode = "MF";
                    assetdetailsvo.AssetInstrumentCategoryCode = "MFDT";
                    if (txtMFDebtA.Text.Trim() != string.Empty)
                        assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtMFDebtA.Text.Trim()),0);

                    if(txtMFDebt.Text.Trim() != string.Empty)
                        assetdetailsvo.Value = Math.Round(double.Parse(txtMFDebt.Text.Trim()),0);
                    assetdetailsvolist.Add(assetdetailsvo);
                    totalasset += assetdetailsvo.Value;
                }
                //MF Hybrid -Equity(Level2)
                if (txtMFHybridEquityA.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                    assetdetailssubvo.AssetGroupCode = "MF";
                    assetdetailssubvo.AssetInstrumentCategoryCode = "MFHY";
                    assetdetailssubvo.AssetInstrumentSubCategoryCode = "MFHYEQ";
                    if (txtMFHybridEquityA.Text.Trim() != string.Empty)
                        assetdetailssubvo.AdjustedValue = Math.Round(double.Parse(txtMFHybridEquityA.Text.Trim()),0);
                    if(txtMFHybridEquity.Text.Trim() != string.Empty)
                        assetdetailssubvo.Value = Math.Round(double.Parse(txtMFHybridEquity.Text.Trim()),0);
                    assetdetailssubvolist.Add(assetdetailssubvo);
                    totalasset += assetdetailssubvo.Value;
                }
                //MF Hybrid -Debt(Level2)
                if (txtMFHybridDebtA.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                    assetdetailssubvo.AssetGroupCode = "MF";
                    assetdetailssubvo.AssetInstrumentCategoryCode = "MFHY";
                    assetdetailssubvo.AssetInstrumentSubCategoryCode = "MFHYDT";
                    if (txtMFHybridDebtA.Text.Trim() != string.Empty)
                        assetdetailssubvo.AdjustedValue = Math.Round(double.Parse(txtMFHybridDebtA.Text.Trim()),0);
                    if(txtMFHybridDebt.Text.Trim() != string.Empty)
                        assetdetailssubvo.Value = Math.Round(double.Parse(txtMFHybridDebt.Text.Trim()),0);
                    assetdetailssubvolist.Add(assetdetailssubvo);
                    totalasset += assetdetailssubvo.Value;
                }
                //Fixed Income
                if (txtFixedIncomeA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "FI";
                    if (txtFixedIncomeA.Text.Trim() != string.Empty)
                        assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtFixedIncomeA.Text.Trim()),0);
                    if (txtFixedIncome.Text.Trim() != string.Empty)
                        assetgroupdetails.Value = Math.Round(double.Parse(txtFixedIncome.Text.Trim()),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Govt Savings
                if (txtGovtSavingsA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "GS";
                    if (txtGovtSavingsA.Text.Trim() != string.Empty)
                        assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtGovtSavingsA.Text.Trim()),0);
                    if (txtGovtSavings.Text.Trim() != string.Empty)
                        assetgroupdetails.Value = Math.Round(double.Parse(txtGovtSavings.Text.Trim()),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Pension & Gratuities
                if (txtPensionGratuitiesA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "PG";
                    if (txtPensionGratuitiesA.Text.Trim() != string.Empty)
                        assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtPensionGratuitiesA.Text.Trim()),0);
                    if (txtPensionGratuities.Text.Trim() != string.Empty)
                        assetgroupdetails.Value = Math.Round(double.Parse(txtPensionGratuities.Text.Trim()),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Property
                if (txtPropertyA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "PR";
                    if (txtPropertyA.Text.Trim() != string.Empty)
                        assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtPropertyA.Text.Trim()),0);
                    if (txtProperty.Text.Trim() != string.Empty)
                        assetgroupdetails.Value = Math.Round(double.Parse(txtProperty.Text.Trim()),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Gold
                if (txtGoldA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "GD";
                    if (txtGoldA.Text.Trim() != string.Empty)
                        assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtGoldA.Text.Trim()),0);
                    if (txtGold.Text.Trim() != string.Empty)
                        assetgroupdetails.Value = Math.Round(double.Parse(txtGold.Text.Trim()),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Collectibles
                if (txtCollectiblesA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "CL";
                    if (txtCollectiblesA.Text.Trim() != string.Empty)
                        assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtCollectiblesA.Text.Trim()),0);
                    if (txtCollectibles.Text.Trim() != string.Empty)
                        assetgroupdetails.Value = Math.Round(double.Parse(txtCollectibles.Text.Trim()),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Private Equity
                if (txtPrivateEquityA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "PE";
                    if (txtPrivateEquityA.Text.Trim() != string.Empty)
                        assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtPrivateEquityA.Text.Trim()),0);
                    if (txtPrivateEquity.Text.Trim() != string.Empty)
                        assetgroupdetails.Value = Math.Round(double.Parse(txtPrivateEquity.Text.Trim()),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //PMS
                if (txtPMSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "PM";
                    if (txtPMSA.Text.Trim() != string.Empty)
                        assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtPMSA.Text.Trim()),0);
                    if (txtPMS.Text.Trim() != string.Empty)
                        assetgroupdetails.Value = Math.Round(double.Parse(txtPMS.Text.Trim()),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Cash and Savings
                if (txtCashAndSavingsA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "CS";
                    if (txtCashAndSavingsA.Text.Trim() != string.Empty)
                        assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtCashAndSavingsA.Text.Trim()),0);
                    if (txtCashAndSavings.Text.Trim() != string.Empty)
                        assetgroupdetails.Value = Math.Round(double.Parse(txtCashAndSavings.Text.Trim()),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Structured Product
                if (txtStructuredProductA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "SP";
                    if (txtStructuredProductA.Text.Trim() != string.Empty)
                        assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtStructuredProductA.Text.Trim()),0);
                    if (txtStructuredProduct.Text.Trim() != string.Empty)
                        assetgroupdetails.Value = Math.Round(double.Parse(txtStructuredProduct.Text.Trim()),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Commodities
                if (txtCommoditiesA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "CM";
                    if (txtCommoditiesA.Text.Trim() != string.Empty)
                        assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtCommoditiesA.Text.Trim()),0);
                    if (txtCommodities.Text.Trim() != string.Empty)
                        assetgroupdetails.Value = Math.Round(double.Parse(txtCommodities.Text.Trim()),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //Others
                if (txtInvestmentsOthersA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails = new CustomerProspectAssetGroupDetails();
                    assetgroupdetails.AssetGroupCode = "OT";
                    if (txtInvestmentsOthersA.Text.Trim() != string.Empty)
                        assetgroupdetails.AdjustedValue = Math.Round(double.Parse(txtInvestmentsOthersA.Text.Trim()),0);
                    if (txtInvestmentsOthers.Text.Trim() != string.Empty)
                        assetgroupdetails.Value = Math.Round(double.Parse(txtInvestmentsOthers.Text.Trim()),0);
                    assetgroupdetailslist.Add(assetgroupdetails);
                    totalasset += assetgroupdetails.Value;
                }
                //MF Consolidated value

                assetgroupdetails = new CustomerProspectAssetGroupDetails();
                assetgroupdetails.AssetGroupCode = "MF";
                assetgroupdetails.Value = 0.0;
                if (txtMFEquityA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtMFEquityA.Text.Trim()),0);
                    //assetgroupdetails.Value += double.Parse(txtMFEquity.Text) ;
                }

                if (txtMFDebtA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtMFDebtA.Text.Trim()),0);
                    //assetgroupdetails.Value +=  double.Parse(txtMFDebt.Text);
                }

                if (txtMFHybridEquityA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtMFHybridEquityA.Text.Trim()),0);
                    //assetgroupdetails.Value += double.Parse(txtMFHybridEquity.Text.Trim());
                }

                if (txtMFHybridDebtA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtMFHybridDebtA.Text.Trim()),0);
                    //assetgroupdetails.Value += double.Parse(txtMFHybridDebt.Text.Trim());
                }
                //MF Total
                if (txtMFEquity.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtMFEquity.Text.Trim()),0);
                }

                if (txtMFDebt.Text.Trim() != string.Empty)
                {

                    assetgroupdetails.Value += Math.Round(double.Parse(txtMFDebt.Text.Trim()),0);
                }

                if (txtMFHybridEquity.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtMFHybridEquity.Text.Trim()),0);
                }

                if (txtMFHybridDebt.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtMFHybridDebt.Text.Trim()),0);
                }

                assetgroupdetailslist.Add(assetgroupdetails);
                //MF Hybrd Consolidation
                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "MF";
                assetdetailsvo.AssetInstrumentCategoryCode = "MFHY";
                if (txtMFHybridEquityA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedValue += Math.Round(double.Parse(txtMFHybridEquityA.Text.Trim()),0);
                }

                if (txtMFHybridDebtA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedValue += Math.Round(double.Parse(txtMFHybridDebtA.Text.Trim()),0);
                }
                //MF-Hybrid Total Value
                if (txtMFHybridEquity.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Value += Math.Round(double.Parse(txtMFHybridEquity.Text.Trim()),0);
                }

                if (txtMFHybridDebt.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Value += Math.Round(double.Parse(txtMFHybridDebt.Text.Trim()),0);
                }
                assetdetailsvolist.Add(assetdetailsvo);
                //==========================================================================================================================

                //Life Insurance
                //==========================================================================================================================
                //Term 

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INTP";
                if (txtAdjustedTermSA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtAdjustedTermSA.Text.Trim()),0);
                }
                if (txtTotalTermSA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Value = Math.Round(double.Parse(txtTotalTermSA.Text.Trim()),0);
                    totalli += assetdetailsvo.Value;
                }
                if (txtTermP.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Premium = Math.Round(double.Parse(txtTermP.Text.Trim()),0);
                }
                if (txtAdjustedPremium.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedPremium = double.Parse(txtAdjustedPremium.Text.Trim()); 
                }
                if (txtTotalTermPremium.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.TotalPremiumValue = double.Parse(txtTotalTermPremium.Text.Trim()); 
                }
                if (txtTermSurrMktVal.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.SurrMktVal = Math.Round(double.Parse(txtTermSurrMktVal.Text.Trim()),0);
                }
                if (txtTotalSurrMrktValue.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.TotalSurrMkt = Math.Round(double.Parse(txtTotalSurrMrktValue.Text.Trim()), 0);
                }
                if (txtAdjustedTermSurrenderValue.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedSurrMkt = Math.Round(double.Parse(txtAdjustedTermSurrenderValue.Text.Trim()), 0);
                }
                assetdetailsvolist.Add(assetdetailsvo);


                //Endowment

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INEP";
                if (txtAdjustedEndowmentSA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtAdjustedEndowmentSA.Text.Trim()),0);
                }
                if (txtTotalEndowmentSA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Value = Math.Round(double.Parse(txtTotalEndowmentSA.Text.Trim()),0);
                    totalli += assetdetailsvo.Value;
                }
                if (txtEndowmentP.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Premium = Math.Round(double.Parse(txtEndowmentP.Text.Trim()),0);
                }
                if (txtAdjustedEndowmentPremium.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedPremium = double.Parse(txtAdjustedEndowmentPremium.Text.Trim());
                }
                if (txtTotalEndowmentPremium.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.TotalPremiumValue = double.Parse(txtTotalEndowmentPremium.Text.Trim());
                }
                if (txtEndowmentSurrMktVal.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.SurrMktVal = Math.Round(double.Parse(txtEndowmentSurrMktVal.Text.Trim()),0);
                }
                if (txtTotalEndowmentSurrMktVal.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.TotalSurrMkt = Math.Round(double.Parse(txtTotalEndowmentSurrMktVal.Text.Trim()), 0);
                }
                if (txtAdjustedSurrMktVal.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedSurrMkt = Math.Round(double.Parse(txtAdjustedSurrMktVal.Text.Trim()), 0);
                }
                assetdetailsvolist.Add(assetdetailsvo);


                //Whole Life

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INWP";
                if (txtAdjustedWholeLifeSA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtAdjustedWholeLifeSA.Text.Trim()),0);
                }
                if (txtTotalWholeLifeSA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Value = Math.Round(double.Parse(txtTotalWholeLifeSA.Text.Trim()),0);
                    totalli += assetdetailsvo.Value;
                }
                if (txtWholeLifeP.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Premium = Math.Round(double.Parse(txtWholeLifeP.Text.Trim()),0);
                }
                if (txtAdjustedWholeLifePremium.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedPremium = double.Parse(txtAdjustedWholeLifePremium.Text.Trim());
                }
                if (txtTotalWholeLifePremium.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.TotalPremiumValue = double.Parse(txtTotalWholeLifePremium.Text.Trim());
                }
                
                if (txtWholeLifeSurrMktVal.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.SurrMktVal = Math.Round(double.Parse(txtWholeLifeSurrMktVal.Text.Trim()),0);
                }
                if (txtTotalWholeLifeSurrMrktVal.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.TotalSurrMkt = Math.Round(double.Parse(txtTotalWholeLifeSurrMrktVal.Text.Trim()), 0);
                }
                if (txtAdjustedWholeLifeSurrMrktVal.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedSurrMkt = Math.Round(double.Parse(txtAdjustedWholeLifeSurrMrktVal.Text.Trim()), 0);
                }
                assetdetailsvolist.Add(assetdetailsvo);


                //Money Back

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INMP";
                if (txtAdjustedMoneyBackSA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtAdjustedMoneyBackSA.Text.Trim()),0);
                }
                if (txtTotalMoneyBackSA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Value = Math.Round(double.Parse(txtTotalMoneyBackSA.Text.Trim()),0);
                    totalli += assetdetailsvo.Value;
                }
                if (txtMoneyBackP.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Premium = Math.Round(double.Parse(txtMoneyBackP.Text.Trim()),0);
                }
                if (txtAdjustedMoneyBackPremium.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedPremium = double.Parse(txtAdjustedMoneyBackPremium.Text.Trim());
                }
                if (txtTotalMoneyBackPremium.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.TotalPremiumValue = double.Parse(txtTotalMoneyBackPremium.Text.Trim());
                }
                
                if (txtMoneyBackSurrMktVal.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.SurrMktVal = Math.Round(double.Parse(txtMoneyBackSurrMktVal.Text.Trim()),0);
                }
                if (txtTotalMoneyBackSurrenMarkt.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.TotalSurrMkt = Math.Round(double.Parse(txtTotalMoneyBackSurrenMarkt.Text.Trim()), 0);
                }
                if (txtAdjustedMBSurrMrktValue.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedSurrMkt = Math.Round(double.Parse(txtAdjustedMBSurrMrktValue.Text.Trim()), 0);
                }
                assetdetailsvolist.Add(assetdetailsvo);


                //ULIP

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INUP";
                if (txtAdjustedULIPSA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtAdjustedULIPSA.Text.Trim()),0);
                }
                if (txtTotalULIPSA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Value = Math.Round(double.Parse(txtTotalULIPSA.Text.Trim()),0);
                    totalli += assetdetailsvo.Value;
                }
                if (txtULIPP.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Premium = Math.Round(double.Parse(txtULIPP.Text.Trim()),0);
                }
                if (txtAdjustedULIPPremium.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedPremium = double.Parse(txtAdjustedULIPPremium.Text.Trim());
                }
                if (txtTotalULIPPremium.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.TotalPremiumValue = double.Parse(txtTotalULIPPremium.Text.Trim());
                }
                
                if (txtULIPSurrMktVal.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.SurrMktVal = Math.Round(double.Parse(txtULIPSurrMktVal.Text.Trim()),0);
                }
                if (txtTotalULIPSurrMrktVal.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.TotalSurrMkt = Math.Round(double.Parse(txtTotalULIPSurrMrktVal.Text.Trim()), 0);
                }
                if (txtAdjustedULIPSurrMrktValue.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedSurrMkt = Math.Round(double.Parse(txtAdjustedULIPSurrMrktValue.Text.Trim()), 0);
                }
                assetdetailsvolist.Add(assetdetailsvo);


                //Others

                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "IN";
                assetdetailsvo.AssetInstrumentCategoryCode = "INOT";
                if (txtAdjustedOthersLISA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedValue = Math.Round(double.Parse(txtAdjustedOthersLISA.Text.Trim()),0);
                }
                if (txtTotalOthersLISA.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Value = Math.Round(double.Parse(txtTotalOthersLISA.Text.Trim()),0);
                    totalli += assetdetailsvo.Value;
                }
                if (txtOthersLIP.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.Premium = Math.Round(double.Parse(txtOthersLIP.Text.Trim()),0);
                }
                if (txtAdjustedOthersLIPremium.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedPremium = double.Parse(txtAdjustedOthersLIPremium.Text.Trim());
                }
                if (txtTotalOthersPremium.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.TotalPremiumValue = double.Parse(txtTotalOthersPremium.Text.Trim());
                }
                
                if (txtOtherSurrMktVal.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.SurrMktVal = Math.Round(double.Parse(txtOtherSurrMktVal.Text.Trim()),0);
                }

                if (txtTotalOthersSurrenMrktval.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.TotalSurrMkt = Math.Round(double.Parse(txtTotalOthersSurrenMrktval.Text.Trim()), 0);
                }
                if (txtAdjustedOthersSurrMrktVal.Text.Trim() != string.Empty)
                {
                    assetdetailsvo.AdjustedSurrMkt = Math.Round(double.Parse(txtAdjustedOthersSurrMrktVal.Text.Trim()), 0);
                }

                assetdetailsvolist.Add(assetdetailsvo);
                if (txtTotalTermPremium.Text.Trim() == "")
                    txtTotalTermPremium.Text = "0";
                if (txtTotalEndowmentPremium.Text.Trim() == "")
                    txtTotalEndowmentPremium.Text = "0";
                if (txtTotalWholeLifePremium.Text.Trim() == "")
                    txtTotalWholeLifePremium.Text = "0";
                if (txtTotalMoneyBackPremium.Text.Trim() == "")
                    txtTotalMoneyBackPremium.Text = "0";
                if (txtTotalULIPPremium.Text.Trim() == "")
                    txtTotalULIPPremium.Text = "0";
                if (txtTotalOthersPremium.Text.Trim() == "")
                    txtTotalOthersPremium.Text = "0";

                if ((txtTotalTermPremium.Text.Trim() != "") || (txtTotalEndowmentPremium.Text.Trim() != "") || (txtTotalWholeLifePremium.Text.Trim() != "") || (txtTotalMoneyBackPremium.Text.Trim() != "") || (txtTotalULIPPremium.Text.Trim() != "") || (txtTotalOthersPremium.Text.Trim() != ""))
                {
                    totalLIPremium = float.Parse(txtTotalTermPremium.Text.Trim()) + float.Parse(txtTotalEndowmentPremium.Text.Trim()) + float.Parse(txtTotalWholeLifePremium.Text.Trim()) + float.Parse(txtTotalMoneyBackPremium.Text.Trim()) + float.Parse(txtTotalULIPPremium.Text.Trim()) + float.Parse(txtTotalOthersPremium.Text.Trim());
                }


                // Insurance Consolidation
                assetgroupdetails = new CustomerProspectAssetGroupDetails();
                assetgroupdetails.AssetGroupCode = "IN";
                assetgroupdetails.Value = 0.0;
                //Group Adjusted Value
                if (txtAdjustedTermSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtAdjustedTermSA.Text.Trim()),0);
                }
                if (txtAdjustedEndowmentSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtAdjustedEndowmentSA.Text.Trim()),0);
                }
                if (txtAdjustedWholeLifeSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtAdjustedWholeLifeSA.Text.Trim()),0);
                }
                if (txtAdjustedMoneyBackSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtAdjustedMoneyBackSA.Text.Trim()),0);
                }
                if (txtAdjustedULIPSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtAdjustedULIPSA.Text.Trim()),0);
                }
                if (txtAdjustedOthersLISA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtAdjustedOthersLISA.Text.Trim()),0);
                }

                //Adjusted Premium Total Value

                if (txtAdjustedPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedPremiumValue += double.Parse(txtAdjustedPremium.Text.Trim());
                }
                if (txtAdjustedEndowmentPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedPremiumValue += double.Parse(txtAdjustedEndowmentPremium.Text.Trim());
                }
                if (txtAdjustedWholeLifePremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedPremiumValue += double.Parse(txtAdjustedWholeLifePremium.Text.Trim());
                }
                if (txtAdjustedMoneyBackPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedPremiumValue += double.Parse(txtAdjustedMoneyBackPremium.Text.Trim());
                }
                if (txtAdjustedULIPPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedPremiumValue += double.Parse(txtAdjustedULIPPremium.Text.Trim());
                }
                if (txtAdjustedOthersLIPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedPremiumValue += double.Parse(txtAdjustedOthersLIPremium.Text.Trim());
                }

                //Group Total Value

                if (txtTotalTermSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtTotalTermSA.Text.Trim()),0);
                }
                if (txtTotalEndowmentSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtTotalEndowmentSA.Text.Trim()),0);
                }
                if (txtTotalWholeLifeSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtTotalWholeLifeSA.Text.Trim()),0);
                }
                if (txtTotalMoneyBackSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtTotalMoneyBackSA.Text.Trim()),0);
                }
                if (txtTotalULIPSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtTotalULIPSA.Text.Trim()),0);
                }
                if (txtTotalOthersLISA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtTotalOthersLISA.Text.Trim()),0);
                }

                // Premium Total Value..

                if (txtTotalTermPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.TotalPremiumValue += double.Parse(txtTotalTermPremium.Text.Trim());
                }

                if (txtTotalEndowmentPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.TotalPremiumValue += double.Parse(txtTotalEndowmentPremium.Text.Trim());
                }

                if (txtTotalWholeLifePremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.TotalPremiumValue += double.Parse(txtTotalWholeLifePremium.Text.Trim());
                }

                if (txtTotalMoneyBackPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.TotalPremiumValue += double.Parse(txtTotalMoneyBackPremium.Text.Trim());
                }

                if (txtTotalULIPPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.TotalPremiumValue += double.Parse(txtTotalULIPPremium.Text.Trim());
                }

                if (txtTotalOthersPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.TotalPremiumValue += double.Parse(txtTotalOthersPremium.Text.Trim());
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
                if (txtHealthInsuranceCoverA.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.AdjustedValue = Math.Round(double.Parse(txtHealthInsuranceCoverA.Text.Trim()),0);
                }
                if (txtHealthInsuranceCoverSA.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.Value = Math.Round(double.Parse(txtHealthInsuranceCoverSA.Text.Trim()),0);
                    totalgi += assetdetailssubvo.Value;
                }
                if (txtHealthInsuranceCoverP.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.Premium = Math.Round(double.Parse(txtHealthInsuranceCoverP.Text.Trim()),0);
                }
                if (txtAdjustedHealthPremium.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.AdjustedPremium = Math.Round(double.Parse(txtAdjustedHealthPremium.Text.Trim()), 0);
                }
                if (txtToalHealthInsurancePremium.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.TotalPremiumValue = Math.Round(double.Parse(txtToalHealthInsurancePremium.Text.Trim()), 0);
                }
                assetdetailssubvolist.Add(assetdetailssubvo);


                //Property Insurance Cover  

                assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                assetdetailssubvo.AssetGroupCode = "GI";
                assetdetailssubvo.AssetInstrumentCategoryCode = "GIRI";
                assetdetailssubvo.AssetInstrumentSubCategoryCode = "GIRIHO";
                if (txtPropertyInsuranceCoverA.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.AdjustedValue = Math.Round(double.Parse(txtPropertyInsuranceCoverA.Text.Trim()),0);
                }
                if (txtPropertyInsuranceCoverSA.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.Value = Math.Round(double.Parse(txtPropertyInsuranceCoverSA.Text.Trim()),0);
                    totalgi += assetdetailssubvo.Value;
                }
                if (txtPropertyInsuranceCoverP.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.Premium = Math.Round(double.Parse(txtPropertyInsuranceCoverP.Text.Trim()),0);
                }
                if (txtAdjustedPropertyInsurancePremium.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.AdjustedPremium = Math.Round(double.Parse(txtAdjustedPropertyInsurancePremium.Text.Trim()), 0);
                }
                if (txtTotalPropertyInsurancePremium.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.TotalPremiumValue = Math.Round(double.Parse(txtTotalPropertyInsurancePremium.Text.Trim()), 0);
                }
                assetdetailssubvolist.Add(assetdetailssubvo);


                //Personal Accident           

                assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                assetdetailssubvo.AssetGroupCode = "GI";
                assetdetailssubvo.AssetInstrumentCategoryCode = "GIRI";
                assetdetailssubvo.AssetInstrumentSubCategoryCode = "GIRIPA";
                if (txtPersonalAccidentA.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.AdjustedValue = Math.Round(double.Parse(txtPersonalAccidentA.Text.Trim()),0);
                }
                if (txtPersonalAccidentSA.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.Value = Math.Round(double.Parse(txtPersonalAccidentSA.Text.Trim()),0);
                    totalgi += assetdetailssubvo.Value;
                }
                if (txtPersonalAccidentP.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.Premium = Math.Round(double.Parse(txtPersonalAccidentP.Text.Trim()),0);
                }
                if (txtPersonalAccidentPremium.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.AdjustedPremium = Math.Round(double.Parse(txtPersonalAccidentPremium.Text.Trim()), 0);
                }
                if (txtTotalPersonalAccidentPremium.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.TotalPremiumValue = Math.Round(double.Parse(txtTotalPersonalAccidentPremium.Text.Trim()), 0);
                }
                assetdetailssubvolist.Add(assetdetailssubvo);


                //Others

                assetdetailssubvo = new CustomerProspectAssetSubDetailsVo();
                assetdetailssubvo.AssetGroupCode = "GI";
                assetdetailssubvo.AssetInstrumentCategoryCode = "GIRI";
                assetdetailssubvo.AssetInstrumentSubCategoryCode = "GIRIOT";
                if (txtOthersGIA.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.AdjustedValue = Math.Round(double.Parse(txtOthersGIA.Text.Trim()),0);
                }
                if (txtOthersGISA.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.Value = Math.Round(double.Parse(txtOthersGISA.Text.Trim()),0);
                    totalgi += assetdetailssubvo.Value;
                }
                if (txtOthersGIP.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.Premium = Math.Round(double.Parse(txtOthersGIP.Text.Trim()),0);
                }
                if (txtAdjustedOtherGIPremium.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.AdjustedPremium = Math.Round(double.Parse(txtAdjustedOtherGIPremium.Text.Trim()), 0);
                }
                if (txtTotalPremiumOthers.Text.Trim() != string.Empty)
                {
                    assetdetailssubvo.TotalPremiumValue = Math.Round(double.Parse(txtTotalPremiumOthers.Text.Trim()), 0);
                }
                assetdetailssubvolist.Add(assetdetailssubvo);
                if (txtToalHealthInsurancePremium.Text.Trim() == "")
                    txtToalHealthInsurancePremium.Text = "0";
                if (txtTotalPropertyInsurancePremium.Text.Trim() == "")
                    txtTotalPropertyInsurancePremium.Text = "0";
                if (txtTotalPersonalAccidentPremium.Text.Trim() == "")
                    txtTotalPersonalAccidentPremium.Text = "0";
                if (txtTotalPremiumOthers.Text.Trim() == "")
                    txtTotalPremiumOthers.Text = "0";
                if ((txtToalHealthInsurancePremium.Text.Trim() != "") || (txtTotalPropertyInsurancePremium.Text.Trim() != "") || (txtTotalPersonalAccidentPremium.Text.Trim() != "") || (txtTotalPremiumOthers.Text.Trim() != ""))
                {
                    totalGIPremium = (float.Parse(txtToalHealthInsurancePremium.Text.Trim()) + float.Parse(txtTotalPropertyInsurancePremium.Text.Trim()) + float.Parse(txtTotalPersonalAccidentPremium.Text.Trim()) + float.Parse(txtTotalPremiumOthers.Text.Trim()));
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
                if (txtHealthInsuranceCoverSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtHealthInsuranceCoverSA.Text.Trim()),0);
                }
                if (txtPropertyInsuranceCoverSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtPropertyInsuranceCoverSA.Text.Trim()),0);
                }
                if (txtPersonalAccidentSA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtPersonalAccidentSA.Text.Trim()),0);
                }
                if (txtOthersGISA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.Value += Math.Round(double.Parse(txtOthersGISA.Text.Trim()),0);
                }
                //Adjusted Sum Assured for GI
                if (txtHealthInsuranceCoverA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtHealthInsuranceCoverA.Text.Trim()),0);
                }
                if (txtPropertyInsuranceCoverA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtPropertyInsuranceCoverA.Text.Trim()),0);
                }
                if (txtPersonalAccidentA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtPersonalAccidentA.Text.Trim()),0);
                }
                if (txtOthersGIA.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedValue += Math.Round(double.Parse(txtOthersGIA.Text.Trim()),0);
                }

                //Adjusted PremiumTotal For GI
                if (txtAdjustedHealthPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedPremiumValue += Math.Round(double.Parse(txtAdjustedHealthPremium.Text.Trim()), 0); 
                }
                if (txtAdjustedPropertyInsurancePremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedPremiumValue += Math.Round(double.Parse(txtAdjustedPropertyInsurancePremium.Text.Trim()), 0);
                }
                if (txtPersonalAccidentPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedPremiumValue += Math.Round(double.Parse(txtPersonalAccidentPremium.Text.Trim()), 0);
                }
                if (txtAdjustedOtherGIPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.AdjustedPremiumValue += Math.Round(double.Parse(txtAdjustedOtherGIPremium.Text.Trim()), 0);
                }

                //Grand Total PremiumTotal For GI

                if (txtToalHealthInsurancePremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.TotalPremiumValue += Math.Round(double.Parse(txtToalHealthInsurancePremium.Text.Trim()), 0);
                }
                if (txtTotalPropertyInsurancePremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.TotalPremiumValue += Math.Round(double.Parse(txtTotalPropertyInsurancePremium.Text.Trim()), 0);
                }
                if (txtTotalPersonalAccidentPremium.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.TotalPremiumValue += Math.Round(double.Parse(txtTotalPersonalAccidentPremium.Text.Trim()), 0);
                }
                if (txtTotalPremiumOthers.Text.Trim() != string.Empty)
                {
                    assetgroupdetails.TotalPremiumValue += Math.Round(double.Parse(txtTotalPremiumOthers.Text.Trim()), 0);
                }

                assetgroupdetailslist.Add(assetgroupdetails);
                // General Insurance second Level
                assetdetailsvo = new CustomerProspectAssetDetailsVo();
                assetdetailsvo.AssetGroupCode = "GI";
                assetdetailsvo.AssetInstrumentCategoryCode = "GIRI";
                assetdetailsvo.AdjustedValue = assetgroupdetails.AdjustedValue;
                assetdetailsvo.Value = assetgroupdetails.Value;
                assetdetailsvo.AdjustedPremium = assetgroupdetails.AdjustedPremiumValue;
                assetdetailsvo.TotalPremiumValue = assetgroupdetails.TotalPremiumValue;
                assetdetailsvolist.Add(assetdetailsvo);





                //==========================================================================================================================

                //Expense
                //==========================================================================================================================
                VoFPSuperlite.CustomerProspectExpenseDetailsVo expensedetailsvo;
                List<CustomerProspectExpenseDetailsVo> expensedetailsvolist = new List<CustomerProspectExpenseDetailsVo>();
                //Transportation = Nothing but Conveyance
                if (txtConveyance.Text.Trim() != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 1;
                    if(txtConveyance.Text.Trim() != string.Empty)
                        expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtConveyance.Text.Trim()),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Food
                if (txtFood.Text.Trim() != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 2;
                    if (txtFood.Text.Trim() != string.Empty)
                        expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtFood.Text.Trim()),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Rent
                if (txtRent.Text.Trim() != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 3;
                    if (txtRent.Text.Trim() != string.Empty)
                        expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtRent.Text.Trim()),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Utilities
                if (txtUtilites.Text.Trim() != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 4;
                    if (txtUtilites.Text.Trim() != string.Empty)
                        expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtUtilites.Text.Trim()),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Health & Personal Care
                if (txtHealthPersonalCare.Text.Trim() != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 5;
                    if (txtHealthPersonalCare.Text.Trim() != string.Empty)
                        expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtHealthPersonalCare.Text.Trim()),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Personal Wear
                if (txtPersonalWear.Text.Trim() != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 6;
                    if (txtPersonalWear.Text.Trim() != string.Empty)
                        expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtPersonalWear.Text.Trim()),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Entertainment & Holidays
                if (txtEntertainmentHolidays.Text.Trim() != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 7;
                    if (txtEntertainmentHolidays.Text.Trim() != string.Empty)
                        expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtEntertainmentHolidays.Text.Trim()),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //Domestic Help
                if (txtDomesticHelp.Text.Trim() != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 8;
                    if (txtDomesticHelp.Text.Trim() != string.Empty)
                        expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtDomesticHelp.Text.Trim()),0);
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
                if (txtOthersExpense.Text.Trim() != string.Empty)
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 10;
                    if (txtOthersExpense.Text.Trim() != string.Empty)
                        expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtOthersExpense.Text.Trim()),0);
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
                if ((txtMFSIPMIS.Text.Trim() != string.Empty))
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 12;
                    if (txtMFSIPMIS.Text.Trim() != string.Empty)
                        expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtMFSIPMIS.Text.Trim()),0);
                    expensedetailsvolist.Add(expensedetailsvo);
                    totalexpense += expensedetailsvo.ExpenseValue;
                }
                //For Reccuring Deposit
                if ((txtReccuringDeposit.Text.Trim() != string.Empty))
                {
                    expensedetailsvo = new CustomerProspectExpenseDetailsVo();
                    expensedetailsvo.ExpenseCategoryCode = 13;
                    if (txtReccuringDeposit.Text.Trim() != string.Empty)
                        expensedetailsvo.ExpenseValue = Math.Round(double.Parse(txtReccuringDeposit.Text.Trim()),0);
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
                //if(txtDisposable.Text.Trim() != "")
                //    customerprospectvo.TotalIncome = int.Parse(txtDisposable.Text.Trim());

                customerprospectvo.TotalIncome = totalincome;


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
                    //if (cpid.IncomeCategoryCode == 7)
                    //{
                    //    txtDisposable.Text = cpid.IncomeValue.ToString();
                    //    //totalincome += cpid.IncomeValue;
                    //}

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
                        txtHomeLoanEMIA.Text = cpld.AdjustedEMIAmount.ToString();
                        txtHomeLoanEMITotal.Text = cpld.TotalEMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }
                    if (cpld.LoanTypeCode == 2)
                    {
                        txtAutoLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtAutoLoanA.Text = cpld.AdjustedLoan.ToString();
                        txtAutoLoanEMI.Text = cpld.EMIAmount.ToString();
                        txtAutoLoanEMIA.Text = cpld.AdjustedEMIAmount.ToString();
                        txtAutoLoanEMITotal.Text = cpld.TotalEMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }

                    if (cpld.LoanTypeCode == 5)
                    {
                        txtEducationLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtEducationLoanA.Text = cpld.AdjustedLoan.ToString();
                        txtEducationLoanEMI.Text = cpld.EMIAmount.ToString();
                        txtEducationLoanEMIA.Text = cpld.AdjustedEMIAmount.ToString();
                        txtEducationLoanEMITotal.Text = cpld.TotalEMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }
                    if (cpld.LoanTypeCode == 6)
                    {
                        txtPersonalLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtPersonalLoanA.Text = cpld.AdjustedLoan.ToString();
                        txtPersonalLoanEMI.Text = cpld.EMIAmount.ToString();
                        txtPersonalLoanEMIA.Text = cpld.AdjustedEMIAmount.ToString();
                        txtPersonalLoanEMITotal.Text = cpld.TotalEMIAmount.ToString();
                        totalliabilities += cpld.LoanOutstanding;
                    }
                    if (cpld.LoanTypeCode == 9)
                    {
                        txtOtherLoanLO.Text = cpld.LoanOutstanding.ToString();
                        txtOtherLoanA.Text = cpld.AdjustedLoan.ToString();
                        txtOtherLoanEMI.Text = cpld.EMIAmount.ToString();
                        txtOtherLoanEMIA.Text = cpld.AdjustedEMIAmount.ToString();
                        txtOtherLoanEMITotal.Text = cpld.TotalEMIAmount.ToString();
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
                        txtTotalSurrMrktValue.Text = cpad.TotalSurrMkt.ToString();
                        txtAdjustedTermSurrenderValue.Text = cpad.AdjustedSurrMkt.ToString();
                        txtTermP.Text = cpad.Premium.ToString();
                        txtAdjustedPremium.Text = cpad.AdjustedPremium.ToString();
                        txtTotalTermPremium.Text = cpad.TotalPremiumValue.ToString();
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INEP")
                    {
                        txtTotalEndowmentSA.Text = cpad.Value.ToString();
                        txtAdjustedEndowmentSA.Text = cpad.AdjustedValue.ToString();
                        txtEndowmentSurrMktVal.Text = cpad.SurrMktVal.ToString();
                        txtTotalEndowmentSurrMktVal.Text = cpad.TotalSurrMkt.ToString();
                        txtAdjustedSurrMktVal.Text = cpad.AdjustedSurrMkt.ToString();
                        txtEndowmentP.Text = cpad.Premium.ToString();
                        txtAdjustedEndowmentPremium.Text = cpad.AdjustedPremium.ToString();
                        txtTotalEndowmentPremium.Text = cpad.TotalPremiumValue.ToString();
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INWP")
                    {
                        txtTotalWholeLifeSA.Text = cpad.Value.ToString();
                        txtAdjustedWholeLifeSA.Text = cpad.AdjustedValue.ToString();
                        txtWholeLifeSurrMktVal.Text = cpad.SurrMktVal.ToString();
                        txtTotalWholeLifeSurrMrktVal.Text = cpad.TotalSurrMkt.ToString();
                        txtAdjustedWholeLifeSurrMrktVal.Text = cpad.AdjustedSurrMkt.ToString();
                        txtWholeLifeP.Text = cpad.Premium.ToString();
                        txtAdjustedWholeLifePremium.Text = cpad.AdjustedPremium.ToString();
                        txtTotalWholeLifePremium.Text = cpad.TotalPremiumValue.ToString();
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INMP")
                    {
                        txtTotalMoneyBackSA.Text = cpad.Value.ToString();
                        txtAdjustedMoneyBackSA.Text = cpad.AdjustedValue.ToString();
                        txtMoneyBackSurrMktVal.Text = cpad.SurrMktVal.ToString();
                        txtTotalMoneyBackSurrenMarkt.Text = cpad.TotalSurrMkt.ToString();
                        txtAdjustedMBSurrMrktValue.Text = cpad.AdjustedSurrMkt.ToString();
                        txtMoneyBackP.Text = cpad.Premium.ToString();
                        txtAdjustedMoneyBackPremium.Text = cpad.AdjustedPremium.ToString();
                        txtTotalMoneyBackPremium.Text = cpad.TotalPremiumValue.ToString();
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INUP")
                    {
                        txtTotalULIPSA.Text = cpad.Value.ToString();
                        txtAdjustedULIPSA.Text = cpad.AdjustedValue.ToString();
                        txtULIPSurrMktVal.Text = cpad.SurrMktVal.ToString();
                        txtTotalULIPSurrMrktVal.Text = cpad.TotalSurrMkt.ToString();
                        txtAdjustedULIPSurrMrktValue.Text = cpad.AdjustedSurrMkt.ToString();
                        txtULIPP.Text = cpad.Premium.ToString();
                        txtAdjustedULIPPremium.Text = cpad.AdjustedPremium.ToString();
                        txtTotalULIPPremium.Text = cpad.TotalPremiumValue.ToString();
                        totalli += cpad.Value;
                    }
                    if (cpad.AssetGroupCode == "IN" && cpad.AssetInstrumentCategoryCode == "INOT")
                    {
                        txtTotalOthersLISA.Text = cpad.Value.ToString();
                        txtAdjustedOthersLISA.Text = cpad.AdjustedValue.ToString();
                        txtOtherSurrMktVal.Text = cpad.SurrMktVal.ToString();
                        txtTotalOthersSurrenMrktval.Text = cpad.TotalSurrMkt.ToString();
                        txtAdjustedOthersSurrMrktVal.Text = cpad.AdjustedSurrMkt.ToString();
                        txtOthersLIP.Text = cpad.Premium.ToString();
                        txtAdjustedOthersLIPremium.Text = cpad.AdjustedPremium.ToString();
                        txtTotalOthersPremium.Text = cpad.TotalPremiumValue.ToString();
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
                        txtAdjustedHealthPremium.Text = cpasd.AdjustedPremium.ToString();
                        txtToalHealthInsurancePremium.Text = cpasd.TotalPremiumValue.ToString();
                        totalgi += cpasd.Value;

                    }
                    if (cpasd.AssetGroupCode == "GI" && cpasd.AssetInstrumentCategoryCode == "GIRI" && cpasd.AssetInstrumentSubCategoryCode == "GIRIHO")
                    {
                        txtPropertyInsuranceCoverA.Text = cpasd.AdjustedValue.ToString();
                        txtPropertyInsuranceCoverSA.Text = cpasd.Value.ToString();
                        txtPropertyInsuranceCoverP.Text = cpasd.Premium.ToString();
                        txtAdjustedPropertyInsurancePremium.Text = cpasd.AdjustedPremium.ToString();
                        txtTotalPropertyInsurancePremium.Text = cpasd.TotalPremiumValue.ToString();
                        totalgi += cpasd.Value;

                    }
                    if (cpasd.AssetGroupCode == "GI" && cpasd.AssetInstrumentCategoryCode == "GIRI" && cpasd.AssetInstrumentSubCategoryCode == "GIRIPA")
                    {
                        txtPersonalAccidentA.Text = cpasd.AdjustedValue.ToString();
                        txtPersonalAccidentSA.Text = cpasd.Value.ToString();
                        txtPersonalAccidentP.Text = cpasd.Premium.ToString();
                        txtPersonalAccidentPremium.Text = cpasd.AdjustedPremium.ToString();
                        txtTotalPersonalAccidentPremium.Text = cpasd.TotalPremiumValue.ToString();
                        totalgi += cpasd.Value;

                    }
                    if (cpasd.AssetGroupCode == "GI" && cpasd.AssetInstrumentCategoryCode == "GIRI" && cpasd.AssetInstrumentSubCategoryCode == "GIRIOT")
                    {
                        txtOthersGIA.Text = cpasd.AdjustedValue.ToString();
                        txtOthersGISA.Text = cpasd.Value.ToString();
                        txtOthersGIP.Text = cpasd.Premium.ToString();
                        txtAdjustedOtherGIPremium.Text = cpasd.AdjustedPremium.ToString();
                        txtTotalPremiumOthers.Text = cpasd.TotalPremiumValue.ToString();
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
                    }

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
                        txtWERPHealthInsuranceCover.Text = Math.Round(double.Parse(drThird["CFPASID_WERPManagedValue"].ToString()), 0).ToString();
                        txtHealthInsuranceCoverSA.Text = Math.Round(double.Parse(drThird["CFPASID_TotalValue"].ToString()), 0).ToString();

                    }
                    if (drThird["PAG_AssetGroupCode"].ToString() == "GI" && drThird["PAIC_AssetInstrumentCategoryCode"].ToString() == "GIRI" && drThird["PAISC_AssetInstrumentSubCategoryCode"].ToString() == "GIRIHO")
                    {
                        txtWERPPropertyInsuranceCover.Text = Math.Round(double.Parse(drThird["CFPASID_WERPManagedValue"].ToString()), 0).ToString();
                        txtPropertyInsuranceCoverSA.Text = Math.Round(double.Parse(drThird["CFPASID_TotalValue"].ToString()), 0).ToString();
                    }
                    if (drThird["PAG_AssetGroupCode"].ToString() == "GI" && drThird["PAIC_AssetInstrumentCategoryCode"].ToString() == "GIRI" && drThird["PAISC_AssetInstrumentSubCategoryCode"].ToString() == "GIRIPA")
                    {
                        txtWERPPersonalAccident.Text = Math.Round(double.Parse(drThird["CFPASID_WERPManagedValue"].ToString()), 0).ToString();
                        txtPersonalAccidentSA.Text = Math.Round(double.Parse(drThird["CFPASID_TotalValue"].ToString()), 0).ToString();
                    }
                    if (drThird["PAG_AssetGroupCode"].ToString() == "GI" && drThird["PAIC_AssetInstrumentCategoryCode"].ToString() == "GIRI" && drThird["PAISC_AssetInstrumentSubCategoryCode"].ToString() == "GIRIOT")
                    {
                        txtWERPOthersGI.Text = Math.Round(double.Parse(drThird["CFPASID_WERPManagedValue"].ToString()), 0).ToString();
                        txtOthersGISA.Text = Math.Round(double.Parse(drThird["CFPASID_TotalValue"].ToString()), 0).ToString();
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


        protected void btnCalculationSubmit_Click(object sender, EventArgs e)
        {
            lblFinalResults.Text = "Disposable income (post tax) is"+ txtIncomePostTax.Text + "";
        }

        //protected void btnSlabCalculate_Click(object sender, EventArgs e)
        //{
            
        //}

        // Commented for altering the TaxSlab Requirements..
        // Commented by Vinayak Patil

        //protected void btnSlabGettingCalculator_Click(object sender, EventArgs e)
        //{
        //    mdlPopupSlabCalculate.TargetControlID = "btnSlabGettingCalculator";
        //    mdlPopupSlabCalculate.Show();
        //}

        
    }
}
