using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCustomerProfiling;
using VoCustomerProfiling;
using VoUser;
using System.Configuration;
using System.Data;
using WealthERP.Base;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;
using Telerik.Web.UI;


namespace WealthERP.Customer
{
    public partial class ViewBankDetails : System.Web.UI.UserControl
    {
        UserVo userVo = null;
        RMVo rmVo = null;
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo = new AdvisorVo();
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        CustomerBankAccountVo customerBankAccountVo = new CustomerBankAccountVo();
        List<CustomerBankAccountVo> customerBankAccountList = null;
       // CustomerVo customerVo = null;
        //int custBankAccId;
       // int customerId = 0;
        int bankId;
        int CustBankAccId;
        int custBankAccId;
        string path;
        DataSet dsBankDetails;
        string viewForm = string.Empty;
        //string customerId = session["customerId"].ToString();


      protected void Page_Load(object sender, EventArgs e)
        {
                
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            RMVo customerRMVo = new RMVo();
           // rmVo = (RMVo)Session[SessionContents.RmVo];
           if (!IsPostBack)
            {
                if (Session["AddMFFolioLinkIdLinkAction"] != null)
                {
                    gvBankDetails.MasterTableView.IsItemInserted = true;
                    gvBankDetails.Rebind();
                }

                BindBankDetails(customerVo.CustomerId);
            
            }
           
        }
      public void BindBankDetails(int customerIdForGettingBankDetails)
      {
          try
          {
              //SessionBo.CheckSession();
              customerVo = (CustomerVo)Session["CustomerVo"];
              customerIdForGettingBankDetails = customerVo.CustomerId;
              customerBankAccountList = customerBankAccountBo.GetCustomerBankAccounts(customerIdForGettingBankDetails);
              DataTable dtCustomerBankAccounts = new DataTable();
              dtCustomerBankAccounts.Columns.Add("CB_CustBankAccId");
              dtCustomerBankAccounts.Columns.Add("WERPBM_BankCode");
              dtCustomerBankAccounts.Columns.Add("CB_BranchName");
              dtCustomerBankAccounts.Columns.Add("XBAT_BankAccountTypeCode");
              dtCustomerBankAccounts.Columns.Add("XMOH_ModeOfHoldingCode");
              dtCustomerBankAccounts.Columns.Add("CB_AccountNum");
              dtCustomerBankAccounts.Columns.Add("CB_BranchAdrLine1");
              dtCustomerBankAccounts.Columns.Add("CB_BranchAdrLine2");
              dtCustomerBankAccounts.Columns.Add("CB_BranchAdrLine3");
              dtCustomerBankAccounts.Columns.Add("CB_BranchAdrPinCode");
              dtCustomerBankAccounts.Columns.Add("CB_BranchAdrCity");
              dtCustomerBankAccounts.Columns.Add("CB_BranchAdrState");
              dtCustomerBankAccounts.Columns.Add("CB_BranchAdrCountry");
              dtCustomerBankAccounts.Columns.Add("CB_MICR");
              dtCustomerBankAccounts.Columns.Add("CB_IFSC");
              dtCustomerBankAccounts.Columns.Add("BankAccountType");
              dtCustomerBankAccounts.Columns.Add("XMOH_ModeOfHolding");
              dtCustomerBankAccounts.Columns.Add("WERPBMBankName");
              
              DataRow drCustomerBankAccount;
              for (int i = 0; i < customerBankAccountList.Count; i++)
              {
                  drCustomerBankAccount = dtCustomerBankAccounts.NewRow();
                  customerBankAccountVo = new CustomerBankAccountVo();
                  customerBankAccountVo = customerBankAccountList[i];
                  drCustomerBankAccount[0] = customerBankAccountVo.CustBankAccId.ToString();
                  drCustomerBankAccount[1] = customerBankAccountVo.BankName.ToString();
                  drCustomerBankAccount[2] = customerBankAccountVo.BranchName.ToString();
                  drCustomerBankAccount[3] = customerBankAccountVo.AccountTypeCode.ToString().Trim();
                  drCustomerBankAccount[4] = customerBankAccountVo.ModeOfOperationCode.ToString().Trim();
                  drCustomerBankAccount[5] = customerBankAccountVo.BankAccountNum.ToString();
                  if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrLine1))
                      drCustomerBankAccount[6] = customerBankAccountVo.BranchAdrLine1.ToString();
                  if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrLine2))
                      drCustomerBankAccount[7] = customerBankAccountVo.BranchAdrLine2.ToString();
                  if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrLine3))
                      drCustomerBankAccount[8] = customerBankAccountVo.BranchAdrLine3.ToString();
                  if (customerBankAccountVo.BranchAdrPinCode != 0)
                      drCustomerBankAccount["CB_BranchAdrPinCode"] = customerBankAccountVo.BranchAdrPinCode.ToString();
                  if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrCity))
                      drCustomerBankAccount[10] = customerBankAccountVo.BranchAdrCity.ToString();
                  if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrState))
                      drCustomerBankAccount[11] = customerBankAccountVo.BranchAdrState.ToString();
                  if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrCountry))
                      drCustomerBankAccount[12] = customerBankAccountVo.BranchAdrCountry.ToString();
               
                  if (customerBankAccountVo.MICR != 0)
                      drCustomerBankAccount["CB_MICR"] = customerBankAccountVo.MICR.ToString();
                  if (!string.IsNullOrEmpty(customerBankAccountVo.IFSC))
                      drCustomerBankAccount[14] = customerBankAccountVo.IFSC.ToString();
                      drCustomerBankAccount[15] = customerBankAccountVo.AccountType.ToString();
                      drCustomerBankAccount[16] = customerBankAccountVo.ModeOfOperation.ToString();
                  //if(!string.IsNullOrEmpty(customerBankAccountVo.WERPBMBankName))
                      drCustomerBankAccount[17] = customerBankAccountVo.WERPBMBankName.ToString();
                      dtCustomerBankAccounts.Rows.Add(drCustomerBankAccount);
              }

              if (Cache["gvDetailsForBank" + userVo.UserId + customerVo.CustomerId] == null)
              {
                  Cache.Insert("gvDetailsForBank" + userVo.UserId + customerVo.CustomerId, dtCustomerBankAccounts);
              }
              else
              {
                  Cache.Remove("gvDetailsForBank" + userVo.UserId + customerVo.CustomerId);
                  Cache.Insert("gvDetailsForBank" + userVo.UserId + customerVo.CustomerId, dtCustomerBankAccounts);
              }
              gvBankDetails.DataSource = dtCustomerBankAccounts;
              gvBankDetails.DataBind();
              gvBankDetails.Visible = true;
              //}
              //else
              //{
              //    gvBankDetails.DataSource = null;
              //    gvBankDetails.DataBind();
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
              FunctionInfo.Add("Method", "ViewBankDetails.ascx:Page_Load()");
              object[] objects = new object[5];
              objects[0] = customerVo;
              objects[2] = customerBankAccountVo;
              objects[3] = customerBankAccountList;
              FunctionInfo = exBase.AddObject(FunctionInfo, objects);
              exBase.AdditionalInformation = FunctionInfo;
              ExceptionManager.Publish(exBase);
              throw exBase;

          }
      }

      protected void gvBankDetails_ItemDataBound(object sender, GridItemEventArgs e)
      {
          if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
          {
              GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
              DataTable dtAccType = new DataTable();
              DataTable dtModeOfOpn = new DataTable();
              DataTable dtBankState = new DataTable();
              DataTable dtBankName = new DataTable();
                    
              GridEditFormItem gefi = (GridEditFormItem)e.Item;
              DropDownList ddlAccountType = (DropDownList)gefi.FindControl("ddlAccountType");
              dtAccType = customerBankAccountBo.XMLBankAccountType();
              ddlAccountType.DataSource = dtAccType;
              ddlAccountType.DataValueField = dtAccType.Columns["XBAT_BankAccountTypeCode"].ToString();
              ddlAccountType.DataTextField = dtAccType.Columns["XBAT_BankAccountTye"].ToString();
              ddlAccountType.DataBind();
              ddlAccountType.Items.Insert(0, new ListItem("Select", "Select"));

              DropDownList ddlModeofOperation = (DropDownList)gefi.FindControl("ddlModeofOperation");
              dtModeOfOpn = customerBankAccountBo.XMLModeOfHolding();
              ddlModeofOperation.DataSource = dtModeOfOpn;
              ddlModeofOperation.DataValueField = dtModeOfOpn.Columns["XMOH_ModeOfHoldingCode"].ToString();
              ddlModeofOperation.DataTextField = dtModeOfOpn.Columns["XMOH_ModeOfHolding"].ToString();
              ddlModeofOperation.DataBind();
              ddlModeofOperation.Items.Insert(0, new ListItem("Select", "Select"));

              DropDownList ddlBankName = (DropDownList)gefi.FindControl("ddlBankName");
              dtBankName = customerBankAccountBo.GetALLBankName();
              ddlBankName.DataSource = dtBankName;
              ddlBankName.DataValueField = dtBankName.Columns["WERPBM_BankCode"].ToString();
              ddlBankName.DataTextField = dtBankName.Columns["WERPBM_BankName"].ToString();
              ddlBankName.DataBind();
              ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));

              DropDownList ddlBankAdrState = (DropDownList)gefi.FindControl("ddlBankAdrState");
              dtBankState = XMLBo.GetStates(path);
              ddlBankAdrState.DataSource = dtBankState;
              ddlBankAdrState.DataTextField = "StateName";
              ddlBankAdrState.DataValueField = "StateCode";
              ddlBankAdrState.DataBind();
              ddlBankAdrState.Items.Insert(0, new ListItem("Select", "Select"));

           
          }
          if (e.Item is GridDataItem)
          {
              GridDataItem dataItem = e.Item as GridDataItem;
              LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
              if (viewForm == "View")
                  buttonEdit.Visible = false;
              else if (viewForm == "Edit")
                  buttonEdit.Visible = true;
          }
          string strBankAdrState;
          string strModeOfOperation;
          string strAccountType;
          string strBankName;
          if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
          {
              bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());
              strBankAdrState = gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_BranchAdrState"].ToString();
              strModeOfOperation = gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XMOH_ModeOfHoldingCode"].ToString();
              strAccountType = gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XBAT_BankAccountTypeCode"].ToString();
              strBankName = gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WERPBM_BankCode"].ToString();
              
              GridEditFormItem editedItem = (GridEditFormItem)e.Item;
              DataTable dtAccType = new DataTable();
              DataTable dtModeOfOpn = new DataTable();
              DataTable dtBankState = new DataTable();
              DataTable dtBankName = new DataTable();
                          
              DropDownList ddlAccountType = (DropDownList)editedItem.FindControl("ddlAccountType");
              dtAccType = customerBankAccountBo.XMLBankAccountType();
              ddlAccountType.DataSource = dtAccType;
              ddlAccountType.DataValueField = dtAccType.Columns["XBAT_BankAccountTypeCode"].ToString();
              ddlAccountType.DataTextField = dtAccType.Columns["XBAT_BankAccountTye"].ToString();
              ddlAccountType.DataBind();
              ddlAccountType.SelectedValue = strAccountType;

              DropDownList ddlModeofOperation = (DropDownList)editedItem.FindControl("ddlModeofOperation");
              dtModeOfOpn = customerBankAccountBo.XMLModeOfHolding();
              ddlModeofOperation.DataSource = dtModeOfOpn;
              ddlModeofOperation.DataValueField = dtModeOfOpn.Columns["XMOH_ModeOfHoldingCode"].ToString();
              ddlModeofOperation.DataTextField = dtModeOfOpn.Columns["XMOH_ModeOfHolding"].ToString();
              ddlModeofOperation.DataBind();
              ddlModeofOperation.SelectedValue = strModeOfOperation;           

              DropDownList ddlBankName = (DropDownList)editedItem.FindControl("ddlBankName");
              dtBankName = customerBankAccountBo.GetALLBankName();
              ddlBankName.DataSource = dtBankName;
              ddlBankName.DataValueField = dtBankName.Columns["WERPBM_BankCode"].ToString();
              ddlBankName.DataTextField = dtBankName.Columns["WERPBM_BankName"].ToString();
              ddlBankName.DataBind();
              ddlBankName.SelectedValue = strBankName;
           
              DropDownList ddlBankAdrState = (DropDownList)editedItem.FindControl("ddlBankAdrState");
              dtBankState = XMLBo.GetStates(path);
              ddlBankAdrState.DataSource = dtBankState;
              ddlBankAdrState.DataTextField = "StateName";
              ddlBankAdrState.DataValueField = "StateCode";
              ddlBankAdrState.DataBind();
              ddlBankAdrState.SelectedValue = strBankAdrState;
           
          }
      }
      protected void gvBankDetails_ItemCommand(object source, GridCommandEventArgs e)
      {
          int customerId = 0;
          string strExternalCode = string.Empty;
          string strExternalType = string.Empty;
          DateTime createdDate = new DateTime();
          DateTime editedDate = new DateTime();
          DateTime deletedDate = new DateTime();
          if (e.CommandName == RadGrid.UpdateCommandName)
          {
              GridEditableItem gridEditableItem = (GridEditableItem)e.Item;

              DropDownList ddlAccountType = (DropDownList)e.Item.FindControl("ddlAccountType");
              TextBox txtAccountNumber = (TextBox)e.Item.FindControl("txtAccountNumber");
              DropDownList ddlModeofOperation = (DropDownList)e.Item.FindControl("ddlModeOfOperation");
              DropDownList ddlBankName = (DropDownList)e.Item.FindControl("ddlBankName");
              TextBox txtBranchName = (TextBox)e.Item.FindControl("txtBranchName");
              TextBox txtBankAdrLine1 = (TextBox)e.Item.FindControl("txtBankAdrLine1");
              TextBox txtBankAdrLine2 = (TextBox)e.Item.FindControl("txtBankAdrLine2");
              TextBox txtBankAdrLine3 = (TextBox)e.Item.FindControl("txtBankAdrLine3");
              TextBox txtBankAdrCity = (TextBox)e.Item.FindControl("txtBankAdrCity");
              TextBox txtBankAdrPinCode = (TextBox)e.Item.FindControl("txtBankAdrPinCode");
              TextBox txtMicr = (TextBox)e.Item.FindControl("txtMicr");
              DropDownList ddlBankAdrState = (DropDownList)e.Item.FindControl("ddlBankAdrState");
              TextBox txtIfsc = (TextBox)e.Item.FindControl("txtIfsc");

              bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());
              customerVo = (CustomerVo)Session["customerVo"];
              customerId = customerVo.CustomerId;

              customerBankAccountVo.BankAccountNum = txtAccountNumber.Text.ToString();
              customerBankAccountVo.AccountType = ddlAccountType.SelectedItem.Value.ToString();
              customerBankAccountVo.ModeOfOperation = ddlModeofOperation.SelectedItem.Value.ToString();
              customerBankAccountVo.BankName = ddlBankName.SelectedItem.Value.ToString();
              customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
              customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
              customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
              customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
              if (txtBankAdrPinCode.Text.ToString() != "")
                  customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
              else
                  customerBankAccountVo.BranchAdrPinCode = 0;
              customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
              if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
                  customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();

              customerBankAccountVo.CustBankAccId = bankId;
              customerBankAccountVo.BranchAdrCountry = "India";
              customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
              if (txtMicr.Text.ToString() != "")
                  customerBankAccountVo.MICR = int.Parse(txtMicr.Text.ToString());
              else
              customerBankAccountVo.MICR = 0;
              customerBankAccountBo.UpdateCustomerBankAccount(customerBankAccountVo, customerId);

          }
          if (e.CommandName == RadGrid.PerformInsertCommandName)
          {
              CustomerBo customerBo = new CustomerBo();
              bool isInserted = false;
              GridEditableItem gridEditableItem = (GridEditableItem)e.Item;

              DropDownList ddlAccountType = (DropDownList)e.Item.FindControl("ddlAccountType");
              TextBox txtAccountNumber = (TextBox)e.Item.FindControl("txtAccountNumber");
              DropDownList ddlModeofOperation = (DropDownList)e.Item.FindControl("ddlModeOfOperation");
              DropDownList ddlBankName = (DropDownList)e.Item.FindControl("ddlBankName");
              TextBox txtBranchName = (TextBox)e.Item.FindControl("txtBranchName");
              TextBox txtBankAdrLine1 = (TextBox)e.Item.FindControl("txtBankAdrLine1");
              TextBox txtBankAdrLine2 = (TextBox)e.Item.FindControl("txtBankAdrLine2");
              TextBox txtBankAdrLine3 = (TextBox)e.Item.FindControl("txtBankAdrLine3");
              TextBox txtBankAdrCity = (TextBox)e.Item.FindControl("txtBankAdrCity");
              TextBox txtBankAdrPinCode = (TextBox)e.Item.FindControl("txtBankAdrPinCode");
              TextBox txtMicr = (TextBox)e.Item.FindControl("txtMicr");
              DropDownList ddlBankAdrState = (DropDownList)e.Item.FindControl("ddlBankAdrState");
              TextBox txtIfsc = (TextBox)e.Item.FindControl("txtIfsc");


              RMVo rmVo = new RMVo();
              int userId;
              rmVo = (RMVo)Session["RmVo"];
              userId = rmVo.UserId;
              string chk;

              if (Session["Check"] != null)
              {
                  chk = Session["Check"].ToString();
              }

              customerVo = (CustomerVo)Session["customerVo"];
              customerId = customerVo.CustomerId;
              customerBankAccountVo = new CustomerBankAccountVo();

              customerBankAccountVo.AccountType = ddlAccountType.SelectedValue.ToString();
              customerBankAccountVo.BankAccountNum = txtAccountNumber.Text.ToString();

              if (ddlModeofOperation.SelectedValue.ToString() != "Select a Mode of Holding")
              customerBankAccountVo.ModeOfOperation = ddlModeofOperation.SelectedValue.ToString();
              customerBankAccountVo.BankName = ddlBankName.SelectedValue.ToString();
              customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
              customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
              customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
              customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
              if (txtBankAdrPinCode.Text.ToString() != "")
                  customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
              customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
              if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
                  customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();
              customerBankAccountVo.BranchAdrCountry = "India";
              if (txtMicr.Text.ToString() != "")
                  customerBankAccountVo.MICR = long.Parse(txtMicr.Text.ToString());
              customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
              customerBankAccountVo.Balance = 0;
              //customerBankAccountVo.Balance = long.Parse(txtBalance.Text.ToString());
              customerBankAccountBo.CreateCustomerBankAccount(customerBankAccountVo, customerId, userId);
              txtAccountNumber.Text = "";
              txtBankAdrLine1.Text = "";
              txtBankAdrLine2.Text = "";
              txtBankAdrLine3.Text = "";
              txtBankAdrPinCode.Text = "";
              txtBankAdrCity.Text = "";
              ddlBankName.SelectedIndex = 0;
              txtBranchName.Text = "";
              txtIfsc.Text = "";
              txtMicr.Text = "";
              ddlAccountType.SelectedIndex = 0;
              ddlModeofOperation.SelectedIndex = 0;

             //isInserted = customerBo.InsertProductAMCSchemeMappingDetalis(customerId, strExternalCode, strExternalType, createdDate, editedDate, deletedDate);
          }

          if (e.CommandName == "Delete")
          {
              bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());
              customerBankAccountBo.DeleteCustomerBankAccount(bankId);
          }
          BindBankDetails(customerId);
      }


   protected void gvBankDetails_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        DataTable dtGvBankDetails = new DataTable();
        if (Cache["gvDetailsForBank" + userVo.UserId + customerVo.CustomerId] != null)
        {
            dtGvBankDetails = (DataTable)Cache["gvDetailsForBank" + userVo.UserId + customerVo.CustomerId];
            gvBankDetails.DataSource = dtGvBankDetails;
        }
          }
        //protected void gvCustomerBankAccounts_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //   // Session["CustBankAccId"] = gvBankDetails.SelectedDataKey.Value.ToString();
        //   // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerAllBankDetails','none');", true);


 // }
      public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvBankDetails.ExportSettings.OpenInNewWindow = true;
            gvBankDetails.ExportSettings.IgnorePaging = true;
            gvBankDetails.ExportSettings.HideStructureColumns = true;
            gvBankDetails.ExportSettings.ExportOnlyData = true;
            gvBankDetails.ExportSettings.FileName = "ExistMFInvestlist";
            gvBankDetails.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvBankDetails.MasterTableView.ExportToExcel();
        }
    }
}
